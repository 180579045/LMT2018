namespace MsgDispatcher
{
	partial class CDTSynAlarmDlg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.TransProgress = new System.Windows.Forms.ProgressBar();
			this.label1 = new System.Windows.Forms.Label();
			this.EndTransBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TransProgress
			// 
			this.TransProgress.Location = new System.Drawing.Point(13, 45);
			this.TransProgress.Name = "TransProgress";
			this.TransProgress.Size = new System.Drawing.Size(556, 23);
			this.TransProgress.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(143, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "正在上传告警日志文件...";
			// 
			// EndTransBtn
			// 
			this.EndTransBtn.Location = new System.Drawing.Point(493, 87);
			this.EndTransBtn.Name = "EndTransBtn";
			this.EndTransBtn.Size = new System.Drawing.Size(75, 23);
			this.EndTransBtn.TabIndex = 2;
			this.EndTransBtn.Text = "结束";
			this.EndTransBtn.UseVisualStyleBackColor = true;
			this.EndTransBtn.Click += new System.EventHandler(this.EndTransBtn_Click);
			// 
			// CDTSynAlarmDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(581, 127);
			this.Controls.Add(this.EndTransBtn);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TransProgress);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CDTSynAlarmDlg";
			this.Text = "告警同步";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ProgressBar TransProgress;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button EndTransBtn;
	}
}