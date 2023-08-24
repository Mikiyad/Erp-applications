using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class MMLabel : Label
	{
		private bool isFieldHeader;

		private bool showBorder;

		private float penWidth = 1f;

		private bool isRequired;

		private Color borderColor = Color.FromArgb(78, 122, 171);

		private PictureBox pictureBoxImage;

		private Container components;

		public bool IsFieldHeader
		{
			get
			{
				return isFieldHeader;
			}
			set
			{
				isFieldHeader = value;
				if (value)
				{
					base.Height = 14;
					ShowBorder = true;
					Invalidate();
				}
			}
		}

		public bool ShowBorder
		{
			get
			{
				return showBorder;
			}
			set
			{
				showBorder = value;
				Invalidate();
			}
		}

		public float PenWidth
		{
			get
			{
				return penWidth;
			}
			set
			{
				penWidth = value;
				Invalidate();
			}
		}

		public Color BorderColor
		{
			get
			{
				return borderColor;
			}
			set
			{
				borderColor = value;
				Invalidate();
			}
		}

		public bool IsRequired
		{
			get
			{
				return isRequired;
			}
			set
			{
				isRequired = value;
				if (value)
				{
					Font = new Font(Font, FontStyle.Bold);
				}
			}
		}

		public MMLabel()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			base.Resize += UserControl1_Resize;
			base.Layout += UserControl1_Layout;
			base.SizeChanged += UserControl1_SizeChanged;
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.UISupport.MMLabel));
			pictureBoxImage = new System.Windows.Forms.PictureBox();
			pictureBoxImage.Image = (System.Drawing.Image)resourceManager.GetObject("pictureBoxImage.Image");
			pictureBoxImage.Location = new System.Drawing.Point(17, 17);
			pictureBoxImage.Name = "pictureBoxImage";
			pictureBoxImage.TabIndex = 0;
			pictureBoxImage.TabStop = false;
		}

		private void UserControl1_Resize(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void UserControl1_SizeChanged(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void UserControl1_Layout(object sender, LayoutEventArgs e)
		{
			Invalidate();
		}
	}
}
