namespace SCMTMainWindow.Component.SCMTControl.FileManager
{
	partial class SystemCapacity
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
			this.label1 = new System.Windows.Forms.Label();
			this.devNameText = new System.Windows.Forms.TextBox();
			this.restCapacityText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.allCapacityText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(39, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "设备名：";
			// 
			// devNameText
			// 
			this.devNameText.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.devNameText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.devNameText.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.devNameText.Location = new System.Drawing.Point(99, 24);
			this.devNameText.Name = "devNameText";
			this.devNameText.ReadOnly = true;
			this.devNameText.Size = new System.Drawing.Size(188, 23);
			this.devNameText.TabIndex = 1;
			// 
			// restCapacityText
			// 
			this.restCapacityText.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.restCapacityText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.restCapacityText.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.restCapacityText.Location = new System.Drawing.Point(99, 63);
			this.restCapacityText.Name = "restCapacityText";
			this.restCapacityText.ReadOnly = true;
			this.restCapacityText.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.restCapacityText.Size = new System.Drawing.Size(160, 23);
			this.restCapacityText.TabIndex = 3;
			this.restCapacityText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(39, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "剩余容量：";
			// 
			// allCapacityText
			// 
			this.allCapacityText.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.allCapacityText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.allCapacityText.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.allCapacityText.Location = new System.Drawing.Point(99, 100);
			this.allCapacityText.Name = "allCapacityText";
			this.allCapacityText.ReadOnly = true;
			this.allCapacityText.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.allCapacityText.Size = new System.Drawing.Size(159, 23);
			this.allCapacityText.TabIndex = 5;
			this.allCapacityText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(39, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "总容量：";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(145, 148);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "关闭";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(265, 68);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 12);
			this.label4.TabIndex = 7;
			this.label4.Text = "KB";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(264, 104);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 12);
			this.label5.TabIndex = 8;
			this.label5.Text = "KB";
			// 
			// SystemCapacity
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ClientSize = new System.Drawing.Size(345, 185);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.allCapacityText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.restCapacityText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.devNameText);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SystemCapacity";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "设备容量查询结果";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox devNameText;
		private System.Windows.Forms.TextBox restCapacityText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox allCapacityText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
	}
}