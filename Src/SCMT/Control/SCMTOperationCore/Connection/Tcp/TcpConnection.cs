using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace SCMTOperationCore.Connection.Tcp
{
	/// <summary>
	///     Represents a connection that uses the TCP protocol.
	/// </summary>
	/// <inheritdoc />
	public sealed class TcpConnection : NetworkConnection
	{
	/// <summary>
	///     The socket we're managing.
	/// </summary>
	private Socket socket;

		/// <summary>
		///     Lock for the socket.
		/// </summary>
		private Object socketLock = new Object();

		private IAsyncResult _result;

		/// <summary>
		///     Creates a TcpConnection from a given TCP Socket.
		/// </summary>
		/// <param name="socket">The TCP socket to wrap.</param>
		internal TcpConnection(Socket socket)
		{
			//Check it's a TCP socket
			if (socket.ProtocolType != System.Net.Sockets.ProtocolType.Tcp)
				throw new ArgumentException("A TcpConnection requires a TCP socket.");

			lock (this.socketLock)
			{
				this.EndPoint = new NetworkEndPoint(socket.RemoteEndPoint);
				this.RemoteEndPoint = socket.RemoteEndPoint;

				this.socket = socket;
				this.socket.NoDelay = true;

				State = ConnectionState.Connected;
			}
		}

		/// <summary>
		///     Creates a new TCP connection.
		/// </summary>
		/// <param name="remoteEndPoint">A <see cref="NetworkEndPoint"/> to connect to.</param>
		public TcpConnection(NetworkEndPoint remoteEndPoint)
		{
			lock (socketLock)
			{
				if (State != ConnectionState.NotConnected)
					throw new InvalidOperationException("Cannot connect as the Connection is already connected.");

				this.EndPoint = remoteEndPoint;
				this.RemoteEndPoint = remoteEndPoint.EndPoint;
				this.IPMode = remoteEndPoint.IPMode;

				//Create a socket
				if (remoteEndPoint.IPMode == IPMode.IPv4)
					socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
				else
				{
					if (!Socket.OSSupportsIPv6)
						throw new HazelException("IPV6 not supported!");

					socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
					socket.SetSocketOption(SocketOptionLevel.IPv6, (SocketOptionName)27, false);
				}

				socket.NoDelay = true;
			}
		}

		/*
		 * struct tcp_keepalive
		 * { 
		 *		u_long  onoff; //是否启用Keep-Alive
		 *		u_long  keepalivetime; //多长时间后开始第一次探测（单位：毫秒）
		 *		u_long  keepaliveinterval; //探测时间间隔（单位：毫秒）
		 * };
		 */

		/// <inheritdoc />
		public override bool Connect(byte[] bytes = null, int timeout = 2000)
		{
			lock (socketLock)
			{

				if (null != _result && !_result.IsCompleted)
				{
					return false;
				}

				if (null != _result && _result.IsCompleted)
				{
					try
					{
						socket.EndConnect(_result);
					}
					catch (Exception e)
					{
						Console.WriteLine($"{DateTime.Now.ToString()} 连接失败");
					}
					finally
					{
						_result.AsyncWaitHandle.Close();
						_result = null;
					}
				}

					State = ConnectionState.Connecting;
				try
				{
					Console.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} 开始连接....");

					_result = socket.BeginConnect(RemoteEndPoint, null, null);
					bool bSucceed = _result.AsyncWaitHandle.WaitOne(timeout);

					if (bSucceed)
					{
						socket.EndConnect(_result);

						uint dummy = 0;
						byte[] inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
						BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);
						BitConverter.GetBytes((uint)2000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
						BitConverter.GetBytes((uint)1000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);

						// 设置TCP协议自保活。scmt与基站之间没有保活操作
						socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, 1);
						socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);

						HandleConnect();
					}
					else
					{
						//throw new HazelException($"Could not connect as an exception occured.\r\n");
						Console.WriteLine("Could not connect as an exception occured.\r\n");
						return false;
					}
				}
				catch (InvalidOperationException e)
				{
					Console.WriteLine($"Could not connect as an exception occured.(invalid operation)\r\n{e.Message}");
					return false;
				}
				catch (SocketException e)
				{
					Console.WriteLine($"Could not connect as an exception occured.(socket exception)\r\n{e.Message}");
					return false;
				}
				catch (Exception e)
				{
					Console.WriteLine($"Could not connect as an exception occured.(other exception)\r\n{e.Message}");
					return false;
				}

				//Start receiving data
				try
				{
					Debug.WriteLine($"Connect:waiting data ...");
					//StartWaitingForHeader(BodyReadCallback);
					StartReceiving();
				}
				catch (Exception e)
				{
					Console.WriteLine("An exception occured while initiating the first receive operation.", e);
					return false;
				}

				//Send handshake
				State = ConnectionState.Connected;
				if (bytes != null)
				{
					Debug.WriteLine($"Connect:send date length {bytes.Length}");
					SendBytes(bytes);
				}
			}
			return true;
		}

		/// <inheritdoc/>
		/// <remarks>
		///     <include file="DocInclude/common.xml" path="docs/item[@name='Connection_SendBytes_General']/*" />
		///     <para>
		///         The sendOption parameter is ignored by the TcpConnection as TCP only supports FragmentedReliable
		///         communication, specifying anything else will have no effect.
		///     </para>
		/// </remarks>
		public override void SendBytes(byte[] bytes, SendOption sendOption = SendOption.FragmentedReliable)
		{
			//Get bytes for length
			//byte[] fullBytes = AppendLengthHeader(bytes);		//TODO 追加信息是个什么鬼
			Debug.WriteLine($"SendBytes: send data length {bytes.Length}");

			//Write the bytes to the socket
			lock (socketLock)
			{
				if (State != ConnectionState.Connected)
					throw new InvalidOperationException("Could not send data as this Connection is not connected. Did you disconnect?");

				try
				{
					socket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, null, null);
				}
				catch (Exception e)
				{
					HazelException he = new HazelException("Could not send data as an occured.", e);
					HandleDisconnect(he);
					throw he;
				}
			}

			Statistics.LogFragmentedSend(bytes.Length, bytes.Length);
		}

		/// <summary>
		///     Called when a 4 byte header has been received.
		/// </summary>
		/// <param name="bytes">The 4 header bytes read.</param>
		/// <param name="callback">The callback to invoke when the body has been received.</param>
		private void HeaderReadCallback(byte[] bytes, Action<byte[]> callback)
		{
			//Get length
			int length = GetLengthFromBytes(bytes);
			Debug.WriteLine($"HeaderReadCallback:get bytes length:{length}");

			//Begin receiving the body
			try
			{
				StartWaitingForBytes(length, callback);
			}
			catch (Exception e)
			{
				HandleDisconnect(new HazelException("An exception occured while initiating a body receive operation.", e));
			}
		}

		/// <summary>
		///     Callback for when a body has been read.
		/// </summary>
		/// <param name="bytes">The data bytes received by the connection.</param>
		private void BodyReadCallback(byte[] bytes)
		{
			//Begin receiving from the start

			//try
			//{
			//    StartWaitingForHeader(BodyReadCallback);
			//}
			//catch (Exception e)
			//{
			//    HandleDisconnect(new HazelException("An exception occured while initiating a header receive operation.", e));
			//}

			Statistics.LogFragmentedReceive(bytes.Length, bytes.Length);

			Debug.WriteLine($"BodyReadCallback: recv data len {bytes.Length}");

			//Fire DataReceived event
			InvokeDataReceived(bytes, SendOption.FragmentedReliable);
		}

		/// <summary>
		///     Starts this connection receiving data.
		/// </summary>
		internal void StartReceiving()
		{
			try
			{
				//StartWaitingForHeader(BodyReadCallback);
				var state = new StateObject(50 * 1024, BodyReadCallback);   // TODO 此处先设置为接收50KB的数据
				StartWaitingForChunk(state);        // 直接开始收数据，没有头信息
			}
			catch (Exception e)
			{
				HandleDisconnect(new HazelException("An exception occured while initiating the first receive operation.", e));
			}
		}

		/// <summary>
		///     Starts waiting for a first handshake packet to be received.
		/// </summary>
		/// <param name="callback">The callback to invoke when the handshake has been received.</param>
		internal void StartWaitingForHandshake(Action<byte[]> callback)
		{
			try
			{
				StartWaitingForHeader(
					delegate (byte[] bytes)
					{
						//Remove version byte
						byte[] dataBytes = new byte[bytes.Length];
						Buffer.BlockCopy(bytes, 0, dataBytes, 0, bytes.Length);

						Debug.WriteLine($"StartWaitingForHandshake: dataBytes length {dataBytes.Length}, data {BitConverter.ToString(dataBytes)}");
						callback.Invoke(dataBytes);
					}
				);
			}
			catch (Exception e)
			{
				HandleDisconnect(new HazelException("An exception occured while initiating the first receive operation.", e));
			}
		}

		/// <summary>
		///     Starts this connections waiting for the header.
		/// </summary>
		/// <param name="callback">The callback to invoke when the body has been read.</param>
		private void StartWaitingForHeader(Action<byte[]> callback)
		{
			StartWaitingForBytes(0, (bytes) => HeaderReadCallback(bytes, callback));
		}

		/// <summary>
		///     Waits for the specified amount of bytes to be received.
		/// </summary>
		/// <param name="length">The number of bytes to receive.</param>
		/// <param name="callback">The callback </param>
		private void StartWaitingForBytes(int length, Action<byte[]> callback)
		{
			StateObject state = new StateObject(length, callback);

			StartWaitingForChunk(state);
		}

		/// <summary>
		///     Waits for the next chunk of data from this socket.
		/// </summary>
		/// <param name="state">The StateObject for the receive operation.</param>
		private void StartWaitingForChunk(StateObject state)
		{
			lock (socketLock)
			{
				//Double check we've not disconnected then begin receiving
				if (State == ConnectionState.Connected || State == ConnectionState.Connecting)
				{
					socket.BeginReceive(state.buffer, state.totalBytesReceived, state.buffer.Length - state.totalBytesReceived, SocketFlags.None, ChunkReadCallback, state);
				}
			}
		}

		/// <summary>
		///     Called when a chunk has been read.
		/// </summary>
		/// <param name="result"></param>
		private void ChunkReadCallback(IAsyncResult result)
		{
			int bytesReceived;

			//End the receive operation
			try
			{
				lock (socketLock)
					bytesReceived = socket.EndReceive(result);
			}
			catch (ObjectDisposedException)
			{
				//If the socket's been disposed then we can just end there.
				throw new HazelException("ChunkReadCallback:ObjectDisposedException");
			}
			catch (Exception e)
			{
				HandleDisconnect(new HazelException("An exception occured while completing a chunk read operation.", e));
				return;
			}

			StateObject state = (StateObject)result.AsyncState;

			//state.totalBytesReceived += bytesReceived;      //TODO threading issues on state?
			Debug.WriteLine($"ChunkReadCallback:received data length {bytesReceived}, total received {state.totalBytesReceived}");

			//Exit if receive nothing
			if (bytesReceived == 0)
			{
				Debug.WriteLine("ChunkReadCallback:recv nothing, disconnect");
				HandleDisconnect();
				return;
			}

			//If we need to receive more then wait for more, else process it.
			//        if (state.totalBytesReceived < state.buffer.Length)
			//        {
			//Debug.WriteLine($"ChunkReadCallback:{state.totalBytesReceived} < {state.buffer.Length}, wait next ...");
			//            try
			//            {
			//                StartWaitingForChunk(state);
			//            }
			//            catch (Exception e)
			//            {
			//                HandleDisconnect(new HazelException("An exception occured while initiating a chunk receive operation.", e));
			//                return;
			//            }
			//        }
			//        else
			{
				Debug.WriteLine($"ChunkReadCallback:invoke callback, buffer length {state.buffer.Length}");
				state.callback.Invoke(state.buffer.Take(bytesReceived).ToArray());
				state.totalBytesReceived = 0;
				state.buffer = new byte[state.buffer.Length];   // 重新申请空间，丢弃原来的数据
				try
				{
					StartWaitingForChunk(state);
				}
				catch (Exception e)
				{
					HandleDisconnect(new HazelException("An exception occured while initiating a chunk receive operation.", e));
				}
			}
		}

		/// <summary>
		///     Called when the socket has been disconnected at the remote host.
		/// </summary>
		/// <param name="e">The exception if one was the cause.</param>
		private void HandleDisconnect(HazelException e = null)
		{
			lock (socketLock)
			{
				//Only invoke the disconnected event if we're not already disconnecting
				if (State != ConnectionState.NotConnected)
				{
					State = ConnectionState.NotConnected;
					InvokeDisconnected(e);
				}

				try
				{
					socket.Shutdown(SocketShutdown.Both);
					socket.Disconnect(true);
				}
				catch (Exception exception)
				{
					Console.WriteLine(exception);
				}

				if (null != _result)
				{
					try
					{
						socket.EndConnect(_result);
					}
					catch (Exception exception)
					{
						Console.WriteLine($"{DateTime.Now.ToString()} cancel connect ");
					}
					finally
					{
						_result.AsyncWaitHandle.Close();
						_result = null;
					}
				}
			}
		}

		private void HandleConnect(HazelException e = null)
		{
			bool invoke = false;
			lock (socketLock)
			{
				if (State != ConnectionState.Connected)
				{
					State = ConnectionState.Connected;
					invoke = true;
				}
			}

			if (invoke)
			{
				Debug.WriteLine($"HandleConnect:connect succeed");
				InvokeConnected(e);
			}
		}

		/// <summary>
		///     Appends the length header to the bytes.
		/// </summary>
		/// <param name="bytes">The source bytes.</param>
		/// <returns>The new bytes.</returns>
		private static byte[] AppendLengthHeader(byte[] bytes)
		{
			byte[] fullBytes = new byte[bytes.Length + 4];

			//Append length
			fullBytes[0] = (byte)(((uint)bytes.Length >> 24) & 0xFF);
			fullBytes[1] = (byte)(((uint)bytes.Length >> 16) & 0xFF);
			fullBytes[2] = (byte)(((uint)bytes.Length >> 8) & 0xFF);
			fullBytes[3] = (byte)(uint)bytes.Length;

			//Add rest of bytes
			Buffer.BlockCopy(bytes, 0, fullBytes, 4, bytes.Length);

			return fullBytes;
		}

		/// <summary>
		///     Returns the length from a length header.
		/// </summary>
		/// <param name="bytes">The bytes received.</param>
		/// <returns>The number of bytes.</returns>
		private static int GetLengthFromBytes(byte[] bytes)
		{
			if (bytes.Length < 4)
				throw new IndexOutOfRangeException("Not enough bytes passed to calculate length.");

			return bytes.Length;
		}

		/// <inheritdoc />
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (socketLock)
				{
					State = ConnectionState.NotConnected;

					if (socket.Connected)
						socket.Shutdown(SocketShutdown.Send);
					socket.Close();
				}
			}

			base.Dispose(disposing);
		}

		public override void Close()
		{
			HandleDisconnect();
		}
	}
}