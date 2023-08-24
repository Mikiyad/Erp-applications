using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class SlidingPanel : Panel
	{
		private bool underLine = true;

		private bool isMinimized;

		private int lastHeight;

		public Label labelHeader;

		private PictureBox pictureBoxResize;

		private ImageList imageList1;

		private Line lineHeader;

		private IContainer components;

		public bool DrawUnderLine
		{
			get
			{
				return underLine;
			}
			set
			{
				underLine = value;
			}
		}

		public bool IsMinimized
		{
			get
			{
				return isMinimized;
			}
			set
			{
				if (value)
				{
					Minimize();
				}
				else
				{
					Maximize();
				}
			}
		}

		public Label Header => labelHeader;

		public Color HeaderBackColor
		{
			get
			{
				return labelHeader.BackColor;
			}
			set
			{
				labelHeader.BackColor = value;
			}
		}

		public string Title
		{
			get
			{
				return labelHeader.Text;
			}
			set
			{
				labelHeader.Text = value;
			}
		}

		public Color HeaderForeColor
		{
			get
			{
				return labelHeader.ForeColor;
			}
			set
			{
				labelHeader.ForeColor = value;
			}
		}

		public Font HeaderFont
		{
			get
			{
				return Header.Font;
			}
			set
			{
				Header.Font = value;
			}
		}

		public Color LineBackColor
		{
			get
			{
				return lineHeader.LineBackColor;
			}
			set
			{
				lineHeader.LineBackColor = value;
			}
		}

		public event EventHandler OnSlidingPanelResized;

		public SlidingPanel()
		{
			InitializeComponent();
			base.Controls.Add(labelHeader);
			labelHeader.Top = 0;
			labelHeader.Left = 0;
			labelHeader.Width = base.Width;
			labelHeader.BackColor = Color.Beige;
			lineHeader.LineBackColor = Color.DimGray;
			lineHeader.Left = 0;
			lineHeader.Top = labelHeader.Top + labelHeader.Height;
			lineHeader.Width = base.Width;
			base.Controls.Add(lineHeader);
			lineHeader.Visible = true;
			if (DrawUnderLine)
			{
				lineHeader.Visible = true;
			}
			else
			{
				lineHeader.Visible = false;
			}
			base.Controls.Add(pictureBoxResize);
			if (isMinimized)
			{
				pictureBoxResize.Image = imageList1.Images[2];
			}
			else
			{
				pictureBoxResize.Image = imageList1.Images[0];
			}
			pictureBoxResize.Height = 8;
			pictureBoxResize.Width = 8;
			pictureBoxResize.Top = 5;
			pictureBoxResize.Left = base.Width - 2 * pictureBoxResize.Width;
			pictureBoxResize.BringToFront();
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
			components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.UISupport.SlidingPanel));
			labelHeader = new System.Windows.Forms.Label();
			pictureBoxResize = new System.Windows.Forms.PictureBox();
			imageList1 = new System.Windows.Forms.ImageList(components);
			lineHeader = new Micromind.UISupport.Line();
			labelHeader.Location = new System.Drawing.Point(17, 17);
			labelHeader.Name = "labelHeader";
			labelHeader.Size = new System.Drawing.Size(100, 18);
			labelHeader.TabIndex = 0;
			labelHeader.Text = "label1";
			labelHeader.Click += new System.EventHandler(labelHeader_Click);
			labelHeader.Resize += new System.EventHandler(labelHeader_Resize);
			labelHeader.Paint += new System.Windows.Forms.PaintEventHandler(labelHeader_Paint);
			pictureBoxResize.Location = new System.Drawing.Point(127, 17);
			pictureBoxResize.Name = "pictureBoxResize";
			pictureBoxResize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			pictureBoxResize.TabIndex = 0;
			pictureBoxResize.TabStop = false;
			pictureBoxResize.Click += new System.EventHandler(pictureBoxResize_Click);
			pictureBoxResize.MouseEnter += new System.EventHandler(pictureBoxResize_MouseEnter);
			pictureBoxResize.MouseLeave += new System.EventHandler(pictureBoxResize_MouseLeave);
			imageList1.ImageSize = new System.Drawing.Size(7, 8);
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resourceManager.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			lineHeader.BackColor = System.Drawing.Color.White;
			lineHeader.DrawWidth = 1;
			lineHeader.LineBackColor = System.Drawing.Color.Black;
			lineHeader.Location = new System.Drawing.Point(367, 17);
			lineHeader.Name = "lineHeader";
			lineHeader.Size = new System.Drawing.Size(72, 1);
			lineHeader.TabIndex = 0;
			lineHeader.TabStop = false;
			base.SizeChanged += new System.EventHandler(UserControl1_SizeChanged);
		}

		private void labelHeader_Resize(object sender, EventArgs e)
		{
		}

		private void UserControl1_SizeChanged(object sender, EventArgs e)
		{
			labelHeader.Width = base.Width;
			pictureBoxResize.Left = base.Width - 2 * pictureBoxResize.Width;
			lineHeader.Width = base.Width;
		}

		private int BestHeight()
		{
			int height = labelHeader.Height;
			int num = 0;
			foreach (Control control in base.Controls)
			{
				if (control.Visible && control.Top + control.Height > num + height)
				{
					num = control.Top;
					height = control.Height;
				}
			}
			return height + num + 2;
		}

		public void ArrangeControls()
		{
			if (!isMinimized)
			{
				base.Height = BestHeight();
			}
		}

		public void Minimize()
		{
			lastHeight = base.Height;
			base.Height = labelHeader.Height + 1;
			isMinimized = true;
			foreach (Control control in base.Controls)
			{
				try
				{
					if (control.TabStop)
					{
						control.Tag = 1;
						control.TabStop = false;
					}
				}
				catch
				{
				}
			}
			pictureBoxResize.Image = imageList1.Images[2];
		}

		private void labelHeader_Click(object sender, EventArgs e)
		{
		}

		public void Maximize()
		{
			base.Height = BestHeight();
			isMinimized = false;
			foreach (Control control in base.Controls)
			{
				if (control.Tag != null && control.Tag.ToString() == "1")
				{
					control.Tag = "";
					control.TabStop = true;
				}
			}
			pictureBoxResize.Image = imageList1.Images[0];
			Refresh();
		}

		private void SetSize()
		{
			if (isMinimized)
			{
				Maximize();
			}
			else
			{
				Minimize();
			}
			if (this.OnSlidingPanelResized != null)
			{
				this.OnSlidingPanelResized(this, null);
			}
		}

		private void pictureBoxResize_Click(object sender, EventArgs e)
		{
			SetSize();
		}

		private void pictureBoxResize_MouseEnter(object sender, EventArgs e)
		{
			if (isMinimized)
			{
				pictureBoxResize.Image = imageList1.Images[3];
			}
			else
			{
				pictureBoxResize.Image = imageList1.Images[1];
			}
		}

		private void pictureBoxResize_MouseLeave(object sender, EventArgs e)
		{
			if (isMinimized)
			{
				pictureBoxResize.Image = imageList1.Images[2];
			}
			else
			{
				pictureBoxResize.Image = imageList1.Images[0];
			}
		}

		private void labelHeader_Paint(object sender, PaintEventArgs e)
		{
			_ = underLine;
		}
	}
}
