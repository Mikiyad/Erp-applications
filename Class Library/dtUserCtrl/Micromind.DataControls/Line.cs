using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	internal class Line : UserControl
	{
		private Color backColor = Color.Black;

		private PictureBox pictureBoxShape;

		private Container components;

		private int drawWidth = 1;

		public int DrawWidth
		{
			get
			{
				return drawWidth;
			}
			set
			{
				if (value <= 50)
				{
					drawWidth = value;
					pictureBoxShape.Height = value;
					base.Height = pictureBoxShape.Height;
				}
			}
		}

		public Color LineBackColor
		{
			get
			{
				return backColor;
			}
			set
			{
				backColor = value;
				pictureBoxShape.BackColor = value;
			}
		}

		public Line()
		{
			InitializeComponent();
			base.SizeChanged += OnResize;
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
			pictureBoxShape = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)pictureBoxShape).BeginInit();
			SuspendLayout();
			pictureBoxShape.BackColor = System.Drawing.Color.Black;
			pictureBoxShape.Location = new System.Drawing.Point(0, 0);
			pictureBoxShape.Name = "pictureBoxShape";
			pictureBoxShape.Size = new System.Drawing.Size(88, 1);
			pictureBoxShape.TabIndex = 1;
			pictureBoxShape.TabStop = false;
			pictureBoxShape.Click += new System.EventHandler(pictureBoxShape_Click);
			BackColor = System.Drawing.Color.White;
			base.Controls.Add(pictureBoxShape);
			base.Name = "Line";
			base.Size = new System.Drawing.Size(104, 8);
			base.Load += new System.EventHandler(Line_Load);
			((System.ComponentModel.ISupportInitialize)pictureBoxShape).EndInit();
			ResumeLayout(false);
		}

		private void Line_Load(object sender, EventArgs e)
		{
			pictureBoxShape.Location = new Point(0, 0);
			pictureBoxShape.Height = drawWidth;
			base.Height = pictureBoxShape.Height;
			pictureBoxShape.Width = base.Width;
			pictureBoxShape.BackColor = backColor;
			base.TabStop = false;
		}

		private void OnResize(object o, EventArgs e)
		{
			pictureBoxShape.Width = base.Width;
			base.Height = pictureBoxShape.Height;
		}

		private void pictureBoxShape_Click(object sender, EventArgs e)
		{
		}
	}
}
