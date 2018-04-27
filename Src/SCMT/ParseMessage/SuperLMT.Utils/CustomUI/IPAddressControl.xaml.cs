// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPAddressControl.xaml.cs" company="dtMobile">
//   dtMobile
// </copyright>
// <summary>
//   IPAddressControl.xaml 的交互逻辑
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// IPAddressControl.xaml 的交互逻辑
    /// </summary>
    public partial class IPAddressControl
    {
        // Using a DependencyProperty as the backing store for IpAddress.  This enables animation, styling, binding, etc...

        /// <summary>
        /// The ip address property.
        /// </summary>
        public static readonly DependencyProperty IpAddressProperty =
            DependencyProperty.Register("IpAddress", typeof(string), typeof(IPAddressControl), new FrameworkPropertyMetadata("", HandleIpAddrChanged));

        /// <summary>
        /// The focus moved.
        /// </summary>
        private bool focusMoved;

        /// <summary>
        /// The text.
        /// </summary>
        private string text;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressControl"/> class. 
        ///  Constructor for the control.
        /// </summary>
        public IPAddressControl()
        {
            this.InitializeComponent();
            this.Loaded += this.IpAddressControlLoaded;
        }

        #endregion

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        public string IpAddress
        {
            get { return (string)this.GetValue(IpAddressProperty); }
            set { this.SetValue(IpAddressProperty, value); }
        }

        /// <summary>
        /// The handle IP address changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void HandleIpAddrChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var addressControl = d as IPAddressControl;
            if (addressControl != null)
            {
                addressControl.Text = e.NewValue as string;
                if (addressControl.Text == "0.0.0.0")
                {
                    addressControl.Text = ".0.0.0";
                }
            }
        }

        #region Private Methods

        /// <summary>
        /// The show error message box.
        /// </summary>
        /// <param name="iptext">
        /// The IP text.
        /// </param>
        private static void ShowErrorMessageBox(string iptext)
        {
            MessageBox.Show(
               string.Format("{0}是一个无效值，请输入介于0和255之间的数字", iptext),
               "错误",
               MessageBoxButton.OK,
               MessageBoxImage.Exclamation);
        }

        /// <summary>
        /// The textbox text check.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        private static void TextboxTextCheck(object sender)
        {
            TextBox txtbox = (TextBox)sender;
            txtbox.Text = GetNumberFromString(txtbox.Text);
            if (!string.IsNullOrWhiteSpace(txtbox.Text))
            {
                if (Convert.ToInt32(txtbox.Text) > 255)
                {
                    ShowErrorMessageBox(txtbox.Text);
                    txtbox.Text = "255";
                }
                else if (Convert.ToInt32(txtbox.Text) < 0)
                {
                    ShowErrorMessageBox(txtbox.Text);
                    txtbox.Text = "0";
                }
                else if (txtbox.Text[0] == '0' && txtbox.Text.Length != 1)
                {
                    txtbox.Text = txtbox.Text.Remove(0, 1);
                    txtbox.SelectionStart = 1;
                }
            }

            // txtbox.CaretIndex = txtbox.Text.Length;
        }

        /// <summary>
        /// The get number from string.
        /// </summary>
        /// <param name="str">
        /// The string.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetNumberFromString(string str)
        {
            StringBuilder numberBuilder = new StringBuilder();
            foreach (char c in str)
            {
                if (char.IsNumber(c))
                {
                    numberBuilder.Append(c);
                }
            }

            return numberBuilder.ToString();
        }

        #endregion

        #region Private Events

        /// <summary>
        /// The IP address control loaded.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void IpAddressControlLoaded(object sender, RoutedEventArgs e)
        {
            this.txtboxFirstPart.Focus();
        }

        /// <summary>
        /// The textbox text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextboxTextCheck(sender);
            }
            catch (Exception ex)
            {
                // throw new Exception(Constants.ErrorGeneralMessage, ex);
            }
        }

        /// <summary>
        /// The textbox first part key down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxFirstPartKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.OemPeriod || e.Key == Key.Tab)
                {
                    this.txtboxSecondPart.Focus();
                    this.txtboxSecondPart.SelectAll();
                    this.focusMoved = true;
                }
                else
                {
                    this.focusMoved = false;
                }
            }
            catch (Exception ex)
            {
                // throw new Exception(Constants.ErrorGeneralMessage, ex);
            }
        }

        /// <summary>
        /// The textbox first part preview key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxFirstPartPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && txtboxFirstPart.SelectionStart == txtboxFirstPart.Text.Length)
            {
                this.txtboxSecondPart.Focus();
                this.txtboxSecondPart.SelectionStart = 0;
            }

            TextBox txtbox = (TextBox)sender;
            if (txtbox.Text.Length == 3 && txtbox.SelectionStart == 3)
            {
                this.txtboxSecondPart.Focus();
                this.txtboxSecondPart.SelectAll();
            }
        }

        /// <summary>
        /// The textbox second part key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxSecondPartKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.OemPeriod || e.Key == Key.Tab) && !this.focusMoved)
            {
                this.txtboxThirdPart.Focus();
                this.txtboxThirdPart.SelectAll();
                this.focusMoved = true;
            }
            else
            {
                this.focusMoved = false;
            }
        }

        /// <summary>
        /// The textbox second part preview key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxSecondPartPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && txtboxSecondPart.SelectionStart == 0)
            {
                this.txtboxFirstPart.Focus();
                this.txtboxFirstPart.SelectionStart = txtboxFirstPart.Text.Length + 1;
            }

            if (e.Key == Key.Right && txtboxSecondPart.SelectionStart == txtboxSecondPart.Text.Length)
            {
                this.txtboxThirdPart.Focus();
                this.txtboxThirdPart.SelectionStart = 0;
            }

            TextBox txtbox = (TextBox)sender;
            if (txtbox.Text.Length == 3 && txtbox.SelectionStart == 3)
            {
                this.txtboxThirdPart.Focus();
                this.txtboxThirdPart.SelectAll();
            }
        }

        /// <summary>
        /// The textbox third part key down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxThirdPartKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.OemPeriod || e.Key == Key.Tab)
            {
                this.txtboxFourthPart.Focus();
                this.txtboxFourthPart.SelectAll();
            }
        }

        /// <summary>
        /// The textbox third part preview key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxThridPartPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && txtboxThirdPart.SelectionStart == 0)
            {
                this.txtboxSecondPart.Focus();
                this.txtboxSecondPart.SelectionStart = txtboxFirstPart.Text.Length;
            }

            if (e.Key == Key.Right && txtboxThirdPart.SelectionStart == txtboxThirdPart.Text.Length)
            {
                this.txtboxFourthPart.Focus();
                this.txtboxFourthPart.SelectionStart = 0;
            }

            var txtbox = (TextBox)sender;
            if (txtbox.Text.Length == 3 && txtbox.SelectionStart == 3)
            {
                this.txtboxFourthPart.Focus();
                this.txtboxFourthPart.SelectAll();
            }
        }

        /// <summary>
        /// The textbox fourth part preview key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtboxForthPartPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && txtboxFourthPart.SelectionStart == 0)
            {
                this.txtboxThirdPart.Focus();
                this.txtboxThirdPart.SelectionStart = txtboxThirdPart.Text.Length;
            }
        }

        #endregion

        /// <summary>
        /// Gets or Sets the text of the control.
        /// If input text is not of IP type type then throws and argument exception.
        /// </summary>
        public string Text
        {
            get
            {
                this.text = this.txtboxFirstPart.Text + "." + this.txtboxSecondPart.Text + "." + this.txtboxThirdPart.Text + "." + this.txtboxFourthPart.Text;
                return this.text;
            }

            set
            {
                try
                {
                    string[] splitValues = value.Split('.');
                    this.txtboxFirstPart.Text = splitValues[0];
                    this.txtboxSecondPart.Text = splitValues[1];
                    this.txtboxThirdPart.Text = splitValues[2];
                    this.txtboxFourthPart.Text = splitValues[3];
                    this.text = value;
                }
                catch (Exception)
                {
                    // throw new ArgumentException(Constants.ErrorInputNotIPTypeMessage, ex);
                }
            }
        }
    }
}
