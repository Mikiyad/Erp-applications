using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class BAPanel : Panel
	{
		private Image image;

		private Design design;

		private bool showBorder;

		private bool showTitle;

		private string title = "";

		private int titleHeaderHeight = 24;

		private Color titleHeaderBackColor = Color.FromArgb(171, 204, 239);

		private Color titleHeaderTextColor = Color.FromArgb(16, 37, 127);

		private float penWidth = 1f;

		private Color borderColor = Color.FromArgb(78, 122, 171);

		private Label labelTitle;

		private Panel panelHeaderLeft;

		private Panel panelHeaderRight;

		private Container components;

		public virtual bool ShowTitle
		{
			get
			{
				return showTitle;
			}
			set
			{
				showTitle = value;
				if (value)
				{
					labelTitle.Visible = true;
					panelHeaderLeft.Visible = true;
					panelHeaderRight.Visible = true;
					AdjustHeader();
				}
				else
				{
					panelHeaderLeft.Visible = false;
					panelHeaderRight.Visible = false;
					labelTitle.Visible = false;
				}
			}
		}

		public virtual Color TitleHeaderTextColor
		{
			get
			{
				return titleHeaderTextColor;
			}
			set
			{
				titleHeaderTextColor = value;
				labelTitle.ForeColor = value;
			}
		}

		public Design DesignModel
		{
			get
			{
				return design;
			}
			set
			{
				design = value;
				switch (value)
				{
				case Design.TransactionDetails:
					ShowTitle = true;
					ShowBorder = true;
					break;
				case Design.None:
					showTitle = false;
					break;
				}
			}
		}

		public virtual Color TitleHeaderBackColor
		{
			get
			{
				return titleHeaderBackColor;
			}
			set
			{
				titleHeaderBackColor = value;
			}
		}

		public virtual int TitleHeaderHeight
		{
			get
			{
				return titleHeaderHeight;
			}
			set
			{
				titleHeaderHeight = value;
				labelTitle.Height = value;
			}
		}

		public virtual string TitleText
		{
			get
			{
				return title;
			}
			set
			{
				title = value;
				labelTitle.Text = value;
			}
		}

		public Label Header
		{
			get
			{
				return labelTitle;
			}
			set
			{
				labelTitle = value;
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

		public Image Image
		{
			get
			{
				return image;
			}
			set
			{
				this.image = value;
				try
				{
					if (value != null)
					{
						Size size = base.Size;
						size.Width += 100;
						Bitmap bitmap = (Bitmap)(BackgroundImage = new Bitmap(this.image, size));
					}
					else
					{
						this.image = null;
					}
				}
				catch
				{
				}
			}
		}

		public BAPanel()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			base.Resize += UserControl1_Resize;
			base.Layout += UserControl1_Layout;
			base.SizeChanged += UserControl1_SizeChanged;
			base.Controls.Add(panelHeaderRight);
			base.Controls.Add(panelHeaderLeft);
			panelHeaderLeft.Controls.Add(labelTitle);
			AdjustHeader();
			if (!base.DesignMode && design == Design.TransactionDetails)
			{
				BackColor = Color.FromArgb(239, 247, 253);
			}
		}

		private void AdjustHeader()
		{
			panelHeaderRight.Width = panelHeaderRight.BackgroundImage.Width;
			panelHeaderRight.Height = panelHeaderRight.BackgroundImage.Height;
			panelHeaderRight.Left = base.Width - panelHeaderRight.Width - 1;
			panelHeaderRight.Top = 1;
			panelHeaderRight.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			panelHeaderLeft.Height = panelHeaderLeft.BackgroundImage.Height;
			panelHeaderLeft.Top = 1;
			panelHeaderLeft.Left = 1;
			panelHeaderLeft.Width = base.Width - panelHeaderRight.Width;
			panelHeaderLeft.SendToBack();
			panelHeaderLeft.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			labelTitle.Dock = DockStyle.Fill;
			labelTitle.BackColor = Color.Transparent;
			labelTitle.ForeColor = titleHeaderTextColor;
			labelTitle.Font = new Font("Tahoma", labelTitle.Font.Size, FontStyle.Bold);
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
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.UISupport.BAPanel));
			labelTitle = new System.Windows.Forms.Label();
			panelHeaderLeft = new System.Windows.Forms.Panel();
			panelHeaderRight = new System.Windows.Forms.Panel();
			labelTitle.BackColor = System.Drawing.Color.Transparent;
			labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
			labelTitle.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelTitle.ForeColor = System.Drawing.Color.FromArgb(16, 37, 127);
			labelTitle.Location = new System.Drawing.Point(17, 17);
			labelTitle.Name = "labelTitle";
			labelTitle.TabIndex = 0;
			labelTitle.Text = "label1";
			labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			panelHeaderLeft.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelHeaderLeft.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("panelHeaderLeft.BackgroundImage");
			panelHeaderLeft.Location = new System.Drawing.Point(17, 53);
			panelHeaderLeft.Name = "panelHeaderLeft";
			panelHeaderLeft.Size = new System.Drawing.Size(80, 24);
			panelHeaderLeft.TabIndex = 282;
			panelHeaderRight.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			panelHeaderRight.BackgroundImage = (System.Drawing.Image)resourceManager.GetObject("panelHeaderRight.BackgroundImage");
			panelHeaderRight.Location = new System.Drawing.Point(249, 43);
			panelHeaderRight.Name = "panelHeaderRight";
			panelHeaderRight.Size = new System.Drawing.Size(416, 24);
			panelHeaderRight.TabIndex = 149;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			OnPaintBackground(e);
			Graphics graphics = e.Graphics;
			if (showBorder)
			{
				Pen pen = new Pen(new SolidBrush(BorderColor), PenWidth);
				Rectangle rect = new Rectangle(base.Location, base.Size);
				rect.X = 0;
				rect.Y = 0;
				rect.Width--;
				rect.Height--;
				graphics.DrawRectangle(pen, rect);
			}
			if (design == Design.TransactionDetails || panelHeaderLeft.Visible)
			{
				Pen pen = new Pen(new SolidBrush(borderColor), 1f);
				graphics.DrawLine(pen, new Point(0, panelHeaderLeft.Height + 1), new Point(base.Width, panelHeaderLeft.Height + 1));
			}
			base.OnPaint(e);
		}

		private void UserControl1_Resize(object sender, EventArgs e)
		{
			Invalidate();
		}

		private void UserControl1_SizeChanged(object sender, EventArgs e)
		{
			try
			{
				if (this.image != null)
				{
					Size clientSize = base.ClientSize;
					clientSize.Width += 100;
					Bitmap bitmap = (Bitmap)(BackgroundImage = new Bitmap(this.image, clientSize));
				}
			}
			catch
			{
			}
			AdjustHeader();
			Invalidate();
		}

		private void UserControl1_Layout(object sender, LayoutEventArgs e)
		{
			Invalidate();
		}
	}
}
