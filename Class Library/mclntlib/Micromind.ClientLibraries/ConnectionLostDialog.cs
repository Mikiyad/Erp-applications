using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	public class ConnectionLostDialog : Form
	{
		private IContainer components;

		private Panel panel1;

		private Label label1;

		private PictureBox pictureBox1;

		private Button buttonOK;

		public event EventHandler dx;

		public ConnectionLostDialog()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			ErrorHelper.OnConnectionLost(null);
			Close();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientLibraries.ConnectionLostDialog));
			panel1 = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			buttonOK = new System.Windows.Forms.Button();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			panel1.BackColor = System.Drawing.Color.White;
			panel1.Controls.Add(label1);
			panel1.Controls.Add(pictureBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(413, 113);
			panel1.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(71, 39);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(272, 13);
			label1.TabIndex = 1;
			label1.Text = "Your session to server has timed-out. Please login again.";
			pictureBox1.Image = (System.Drawing.Image)resources.GetObject("pictureBox1.Image");
			pictureBox1.Location = new System.Drawing.Point(12, 21);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(53, 50);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(301, 120);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(99, 28);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(413, 153);
			base.Controls.Add(buttonOK);
			base.Controls.Add(panel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ConnectionLostDialog";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Connection Lost";
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
