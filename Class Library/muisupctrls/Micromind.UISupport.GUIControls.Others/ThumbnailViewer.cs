using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport.GUIControls.Others
{
	public class ThumbnailViewer : UserControl
	{
		private IContainer components;

		private PictureBox pictureBox1;

		public Image Image
		{
			get
			{
				return pictureBox1.Image;
			}
			set
			{
				pictureBox1.Image = value;
			}
		}

		public ThumbnailViewer()
		{
			InitializeComponent();
			pictureBox1.Click += pictureBox1_Click;
			base.LostFocus += ThumbnailViewer_LostFocus;
		}

		private void ThumbnailViewer_LostFocus(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			base.Visible = false;
		}

		public void Show(int left, int top)
		{
			base.Left = left;
			base.Top = top;
			base.Visible = true;
			Focus();
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
			pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			pictureBox1.BackColor = System.Drawing.Color.Transparent;
			pictureBox1.Location = new System.Drawing.Point(-1, -1);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(146, 145);
			pictureBox1.TabIndex = 0;
			pictureBox1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.White;
			base.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			base.Controls.Add(pictureBox1);
			base.Name = "ThumbnailViewer";
			base.Size = new System.Drawing.Size(144, 143);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(false);
		}
	}
}
