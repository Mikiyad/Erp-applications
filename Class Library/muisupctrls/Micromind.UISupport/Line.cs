using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class Line : UserControl
    {
		private bool isVertical;

		private Color backColor = Color.Black;

		private PictureBox pictureBoxShape;

		private Container components;

		private int drawWidth = 1;

		public bool IsVertical
		{
			get
			{
				return isVertical;
			}
			set
			{
				isVertical = value;
				SetSize();
			}
		}

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
					SetSize();
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
			base.Size = new System.Drawing.Size(88, 8);
			base.Load += new System.EventHandler(Line_Load);
			ResumeLayout(false);
		}

		private void Line_Load(object sender, EventArgs e)
		{
			pictureBoxShape.Location = new Point(0, 0);
			SetSize();
			pictureBoxShape.BackColor = backColor;
			base.TabStop = false;
			if (isVertical)
			{
				pictureBoxShape.BackColor = Color.FromArgb(188, 218, 247);
			}
			else
			{
				pictureBoxShape.BackColor = Color.FromArgb(136, 176, 228);
			}
		}

		private void SetSize()
		{
			if (isVertical)
			{
				pictureBoxShape.Width = drawWidth;
				base.Width = pictureBoxShape.Width;
				pictureBoxShape.Height = base.Height;
			}
			else
			{
				pictureBoxShape.Height = drawWidth;
				base.Height = pictureBoxShape.Height;
				pictureBoxShape.Width = base.Width;
			}
		}

		private void OnResize(object o, EventArgs e)
		{
			if (isVertical)
			{
				pictureBoxShape.Height = base.Height;
				base.Width = pictureBoxShape.Width;
			}
			else
			{
				pictureBoxShape.Width = base.Width;
				base.Height = pictureBoxShape.Height;
			}
		}

		private void pictureBoxShape_Click(object sender, EventArgs e)
		{
		}
	}
}
