using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ReminderNotifierForm : Form
	{
		public enum TaskbarStates
		{
			hidden,
			appearing,
			visible,
			disappearing
		}

		protected Bitmap BackgroundBitmap;

		protected Bitmap CloseBitmap;

		protected Point CloseBitmapLocation;

		protected Size CloseBitmapSize;

		protected Rectangle RealTitleRectangle;

		protected Rectangle RealContentRectangle;

		protected Rectangle WorkAreaRectangle;

		protected Timer timer = new Timer();

		protected TaskbarStates taskbarState;

		protected string titleText;

		protected string contentText;

		protected Color normalTitleColor = Color.FromArgb(255, 0, 0);

		protected Color hoverTitleColor = Color.FromArgb(255, 0, 0);

		protected Color normalContentColor = Color.FromArgb(0, 0, 0);

		protected Color hoverContentColor = Color.FromArgb(0, 0, 102);

		protected Font normalTitleFont = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Pixel);

		protected Font hoverTitleFont = new Font("Arial", 12f, FontStyle.Bold, GraphicsUnit.Pixel);

		protected Font normalContentFont = new Font("Arial", 11f, FontStyle.Regular, GraphicsUnit.Pixel);

		protected Font hoverContentFont = new Font("Arial", 11f, FontStyle.Regular, GraphicsUnit.Pixel);

		protected int nShowEvents;

		protected int nHideEvents;

		protected int nVisibleEvents;

		protected int nIncrementShow;

		protected int nIncrementHide;

		protected bool bIsMouseOverPopup;

		protected bool bIsMouseOverClose;

		protected bool bIsMouseOverContent;

		protected bool bIsMouseOverTitle;

		protected bool bIsMouseDown;

		protected bool bKeepVisibleOnMouseOver = true;

		protected bool bReShowOnMouseOver;

		public Rectangle TitleRectangle;

		public Rectangle ContentRectangle;

		public bool TitleClickable;

		public bool ContentClickable = true;

		public bool CloseClickable = true;

		private UltraTextEditor ultraTextEditor1;

		private UltraCombo ultraCombo1;

		public bool EnableSelectionRectangle = true;

		public TaskbarStates TaskbarState => taskbarState;

		public string TitleText
		{
			get
			{
				return titleText;
			}
			set
			{
				titleText = value;
				Refresh();
			}
		}

		public string ContentText
		{
			get
			{
				return contentText;
			}
			set
			{
				contentText = value;
				Refresh();
			}
		}

		public Color NormalTitleColor
		{
			get
			{
				return normalTitleColor;
			}
			set
			{
				normalTitleColor = value;
				Refresh();
			}
		}

		public Color HoverTitleColor
		{
			get
			{
				return hoverTitleColor;
			}
			set
			{
				hoverTitleColor = value;
				Refresh();
			}
		}

		public Color NormalContentColor
		{
			get
			{
				return normalContentColor;
			}
			set
			{
				normalContentColor = value;
				Refresh();
			}
		}

		public Color HoverContentColor
		{
			get
			{
				return hoverContentColor;
			}
			set
			{
				hoverContentColor = value;
				Refresh();
			}
		}

		public Font NormalTitleFont
		{
			get
			{
				return normalTitleFont;
			}
			set
			{
				normalTitleFont = value;
				Refresh();
			}
		}

		public Font HoverTitleFont
		{
			get
			{
				return hoverTitleFont;
			}
			set
			{
				hoverTitleFont = value;
				Refresh();
			}
		}

		public Font NormalContentFont
		{
			get
			{
				return normalContentFont;
			}
			set
			{
				normalContentFont = value;
				Refresh();
			}
		}

		public Font HoverContentFont
		{
			get
			{
				return hoverContentFont;
			}
			set
			{
				hoverContentFont = value;
				Refresh();
			}
		}

		public bool KeepVisibleOnMousOver
		{
			get
			{
				return bKeepVisibleOnMouseOver;
			}
			set
			{
				bKeepVisibleOnMouseOver = value;
			}
		}

		public bool ReShowOnMouseOver
		{
			get
			{
				return bReShowOnMouseOver;
			}
			set
			{
				bReShowOnMouseOver = value;
			}
		}

		public event EventHandler CloseClick;

		public event EventHandler TitleClick;

		public event EventHandler ContentClick;

		public ReminderNotifierForm()
		{
			base.FormBorderStyle = FormBorderStyle.None;
			base.WindowState = FormWindowState.Minimized;
			Show();
			base.Hide();
			base.WindowState = FormWindowState.Normal;
			base.ShowInTaskbar = false;
			base.TopMost = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.ControlBox = false;
			timer.Enabled = true;
			timer.Tick += OnTimer;
		}

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		public void Show(string strTitle, string strContent, int nTimeToShow, int nTimeToStay, int nTimeToHide)
		{
			WorkAreaRectangle = Screen.GetWorkingArea(WorkAreaRectangle);
			titleText = strTitle;
			contentText = strContent;
			nVisibleEvents = nTimeToStay;
			CalculateMouseRectangles();
			if (nTimeToShow > 10)
			{
				int num = Math.Min(nTimeToShow / 10, BackgroundBitmap.Height);
				nShowEvents = nTimeToShow / num;
				nIncrementShow = BackgroundBitmap.Height / num;
			}
			else
			{
				nShowEvents = 10;
				nIncrementShow = BackgroundBitmap.Height;
			}
			if (nTimeToHide > 10)
			{
				int num = Math.Min(nTimeToHide / 10, BackgroundBitmap.Height);
				nHideEvents = nTimeToHide / num;
				nIncrementHide = BackgroundBitmap.Height / num;
			}
			else
			{
				nHideEvents = 10;
				nIncrementHide = BackgroundBitmap.Height;
			}
			switch (taskbarState)
			{
			case TaskbarStates.hidden:
				taskbarState = TaskbarStates.appearing;
				SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width - 17, WorkAreaRectangle.Bottom - 1, BackgroundBitmap.Width, 0);
				timer.Interval = nShowEvents;
				timer.Start();
				ShowWindow(base.Handle, 4);
				break;
			case TaskbarStates.appearing:
				Refresh();
				break;
			case TaskbarStates.visible:
				timer.Stop();
				timer.Interval = nVisibleEvents;
				timer.Start();
				Refresh();
				break;
			case TaskbarStates.disappearing:
				timer.Stop();
				taskbarState = TaskbarStates.visible;
				SetBounds(WorkAreaRectangle.Right - BackgroundBitmap.Width - 17, WorkAreaRectangle.Bottom - BackgroundBitmap.Height - 1, BackgroundBitmap.Width, BackgroundBitmap.Height);
				timer.Interval = nVisibleEvents;
				timer.Start();
				Refresh();
				break;
			}
			Global.IsNotifierVisible = true;
		}

		public new void Hide()
		{
			if (taskbarState != 0)
			{
				timer.Stop();
				taskbarState = TaskbarStates.hidden;
				base.Hide();
			}
			Global.IsNotifierVisible = false;
		}

		public void SetBackgroundBitmap(string strFilename, Color transparencyColor)
		{
			BackgroundBitmap = new Bitmap(strFilename);
			base.Width = BackgroundBitmap.Width;
			base.Height = BackgroundBitmap.Height;
			base.Region = BitmapToRegion(BackgroundBitmap, transparencyColor);
		}

		public void SetBackgroundBitmap(Image image, Color transparencyColor)
		{
			BackgroundBitmap = new Bitmap(image);
			base.Width = BackgroundBitmap.Width;
			base.Height = BackgroundBitmap.Height;
			base.Region = BitmapToRegion(BackgroundBitmap, transparencyColor);
		}

		public void SetCloseBitmap(string strFilename, Color transparencyColor, Point position)
		{
			CloseBitmap = new Bitmap(strFilename);
			CloseBitmap.MakeTransparent(transparencyColor);
			CloseBitmapSize = new Size(CloseBitmap.Width / 3, CloseBitmap.Height);
			CloseBitmapLocation = position;
		}

		public void SetCloseBitmap(Image image, Color transparencyColor, Point position)
		{
			CloseBitmap = new Bitmap(image);
			CloseBitmap.MakeTransparent(transparencyColor);
			CloseBitmapSize = new Size(CloseBitmap.Width / 3, CloseBitmap.Height);
			CloseBitmapLocation = position;
		}

		protected void DrawCloseButton(Graphics grfx)
		{
			if (CloseBitmap != null)
			{
				grfx.DrawImage(destRect: new Rectangle(CloseBitmapLocation, CloseBitmapSize), srcRect: bIsMouseOverClose ? (bIsMouseDown ? new Rectangle(new Point(CloseBitmapSize.Width * 2, 0), CloseBitmapSize) : new Rectangle(new Point(CloseBitmapSize.Width, 0), CloseBitmapSize)) : new Rectangle(new Point(0, 0), CloseBitmapSize), image: CloseBitmap, srcUnit: GraphicsUnit.Pixel);
			}
		}

		protected void DrawText(Graphics grfx)
		{
			if (titleText != null && titleText.Length != 0)
			{
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Near;
				stringFormat.LineAlignment = StringAlignment.Center;
				stringFormat.FormatFlags = StringFormatFlags.NoWrap;
				stringFormat.Trimming = StringTrimming.EllipsisCharacter;
				if (bIsMouseOverTitle)
				{
					grfx.DrawString(titleText, hoverTitleFont, new SolidBrush(hoverTitleColor), TitleRectangle, stringFormat);
				}
				else
				{
					grfx.DrawString(titleText, normalTitleFont, new SolidBrush(normalTitleColor), TitleRectangle, stringFormat);
				}
			}
			if (contentText == null || contentText.Length == 0)
			{
				return;
			}
			StringFormat stringFormat2 = new StringFormat();
			stringFormat2.Alignment = StringAlignment.Center;
			stringFormat2.LineAlignment = StringAlignment.Center;
			stringFormat2.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
			stringFormat2.Trimming = StringTrimming.Word;
			if (bIsMouseOverContent)
			{
				grfx.DrawString(contentText, hoverContentFont, new SolidBrush(hoverContentColor), ContentRectangle, stringFormat2);
				if (EnableSelectionRectangle)
				{
					ControlPaint.DrawBorder3D(grfx, RealContentRectangle, Border3DStyle.Etched, Border3DSide.Left | Border3DSide.Top | Border3DSide.Right | Border3DSide.Bottom);
				}
			}
			else
			{
				grfx.DrawString(contentText, normalContentFont, new SolidBrush(normalContentColor), ContentRectangle, stringFormat2);
			}
		}

		protected void CalculateMouseRectangles()
		{
			Graphics graphics = CreateGraphics();
			StringFormat format = new StringFormat
			{
				Alignment = StringAlignment.Center,
				LineAlignment = StringAlignment.Center,
				FormatFlags = StringFormatFlags.MeasureTrailingSpaces
			};
			SizeF sizeF = graphics.MeasureString(titleText, hoverTitleFont, TitleRectangle.Width, format);
			SizeF sizeF2 = graphics.MeasureString(contentText, hoverContentFont, ContentRectangle.Width, format);
			graphics.Dispose();
			if (sizeF.Height > (float)TitleRectangle.Height)
			{
				RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, TitleRectangle.Width, TitleRectangle.Height);
			}
			else
			{
				RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, (int)sizeF.Width, (int)sizeF.Height);
			}
			RealTitleRectangle.Inflate(0, 2);
			if (sizeF2.Height > (float)ContentRectangle.Height)
			{
				RealContentRectangle = new Rectangle((ContentRectangle.Width - (int)sizeF2.Width) / 2 + ContentRectangle.Left, ContentRectangle.Top, (int)sizeF2.Width, ContentRectangle.Height);
			}
			else
			{
				RealContentRectangle = new Rectangle((ContentRectangle.Width - (int)sizeF2.Width) / 2 + ContentRectangle.Left, (ContentRectangle.Height - (int)sizeF2.Height) / 2 + ContentRectangle.Top, (int)sizeF2.Width, (int)sizeF2.Height);
			}
			RealContentRectangle.Inflate(0, 2);
		}

		protected Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
		{
			if (bitmap == null)
			{
				throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");
			}
			int height = bitmap.Height;
			int width = bitmap.Width;
			GraphicsPath graphicsPath = new GraphicsPath();
			for (int i = 0; i < height; i++)
			{
				for (int j = 0; j < width; j++)
				{
					if (!(bitmap.GetPixel(j, i) == transparencyColor))
					{
						int num = j;
						for (; j < width && bitmap.GetPixel(j, i) != transparencyColor; j++)
						{
						}
						graphicsPath.AddRectangle(new Rectangle(num, i, j - num, 1));
					}
				}
			}
			Region result = new Region(graphicsPath);
			graphicsPath.Dispose();
			return result;
		}

		protected void OnTimer(object obj, EventArgs ea)
		{
			switch (taskbarState)
			{
			case TaskbarStates.appearing:
				if (base.Height < BackgroundBitmap.Height)
				{
					SetBounds(base.Left, base.Top - nIncrementShow, base.Width, base.Height + nIncrementShow);
					break;
				}
				timer.Stop();
				base.Height = BackgroundBitmap.Height;
				timer.Interval = nVisibleEvents;
				taskbarState = TaskbarStates.visible;
				timer.Start();
				break;
			case TaskbarStates.visible:
				timer.Stop();
				timer.Interval = nHideEvents;
				if ((bKeepVisibleOnMouseOver && !bIsMouseOverPopup) || !bKeepVisibleOnMouseOver)
				{
					taskbarState = TaskbarStates.disappearing;
				}
				timer.Start();
				break;
			case TaskbarStates.disappearing:
				if (bReShowOnMouseOver && bIsMouseOverPopup)
				{
					taskbarState = TaskbarStates.appearing;
				}
				else if (base.Top < WorkAreaRectangle.Bottom)
				{
					SetBounds(base.Left, base.Top + nIncrementHide, base.Width, base.Height - nIncrementHide);
				}
				else
				{
					Hide();
				}
				break;
			}
		}

		protected override void OnMouseEnter(EventArgs ea)
		{
			base.OnMouseEnter(ea);
			bIsMouseOverPopup = true;
			Refresh();
		}

		protected override void OnMouseLeave(EventArgs ea)
		{
			base.OnMouseLeave(ea);
			bIsMouseOverPopup = false;
			bIsMouseOverClose = false;
			bIsMouseOverTitle = false;
			bIsMouseOverContent = false;
			Refresh();
		}

		protected override void OnMouseMove(MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			bool flag = false;
			if (mea.X > CloseBitmapLocation.X && mea.X < CloseBitmapLocation.X + CloseBitmapSize.Width && mea.Y > CloseBitmapLocation.Y && mea.Y < CloseBitmapLocation.Y + CloseBitmapSize.Height && CloseClickable)
			{
				if (!bIsMouseOverClose)
				{
					bIsMouseOverClose = true;
					bIsMouseOverTitle = false;
					bIsMouseOverContent = false;
					Cursor = Cursors.Hand;
					flag = true;
				}
			}
			else if (RealContentRectangle.Contains(new Point(mea.X, mea.Y)) && ContentClickable)
			{
				if (!bIsMouseOverContent)
				{
					bIsMouseOverClose = false;
					bIsMouseOverTitle = false;
					bIsMouseOverContent = true;
					Cursor = Cursors.Hand;
					flag = true;
				}
			}
			else if (RealTitleRectangle.Contains(new Point(mea.X, mea.Y)) && TitleClickable)
			{
				if (!bIsMouseOverTitle)
				{
					bIsMouseOverClose = false;
					bIsMouseOverTitle = true;
					bIsMouseOverContent = false;
					Cursor = Cursors.Hand;
					flag = true;
				}
			}
			else
			{
				if (bIsMouseOverClose || bIsMouseOverTitle || bIsMouseOverContent)
				{
					flag = true;
				}
				bIsMouseOverClose = false;
				bIsMouseOverTitle = false;
				bIsMouseOverContent = false;
				Cursor = Cursors.Default;
			}
			if (flag)
			{
				Refresh();
			}
		}

		protected override void OnMouseDown(MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			bIsMouseDown = true;
			if (bIsMouseOverClose)
			{
				Refresh();
			}
		}

		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
			bIsMouseDown = false;
			if (bIsMouseOverClose)
			{
				Hide();
				if (this.CloseClick != null)
				{
					this.CloseClick(this, new EventArgs());
				}
			}
			else if (bIsMouseOverTitle)
			{
				if (this.TitleClick != null)
				{
					this.TitleClick(this, new EventArgs());
				}
			}
			else if (bIsMouseOverContent && this.ContentClick != null)
			{
				this.ContentClick(this, new EventArgs());
			}
		}

		private void InitializeComponent()
		{
			ultraTextEditor1 = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
			ultraCombo1 = new Infragistics.Win.UltraWinGrid.UltraCombo();
			((System.ComponentModel.ISupportInitialize)ultraTextEditor1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCombo1).BeginInit();
			SuspendLayout();
			ultraTextEditor1.Location = new System.Drawing.Point(81, 38);
			ultraTextEditor1.Name = "ultraTextEditor1";
			ultraTextEditor1.Size = new System.Drawing.Size(100, 21);
			ultraTextEditor1.TabIndex = 0;
			ultraCombo1.Location = new System.Drawing.Point(164, 122);
			ultraCombo1.Name = "ultraCombo1";
			ultraCombo1.Size = new System.Drawing.Size(112, 22);
			ultraCombo1.TabIndex = 1;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(292, 266);
			base.Controls.Add(ultraCombo1);
			base.Controls.Add(ultraTextEditor1);
			base.Name = "ReminderNotifierForm";
			((System.ComponentModel.ISupportInitialize)ultraTextEditor1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCombo1).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		protected override void OnPaintBackground(PaintEventArgs pea)
		{
			Graphics graphics = pea.Graphics;
			graphics.PageUnit = GraphicsUnit.Pixel;
			Bitmap image = new Bitmap(BackgroundBitmap.Width, BackgroundBitmap.Height);
			Graphics graphics2 = Graphics.FromImage(image);
			if (BackgroundBitmap != null)
			{
				graphics2.DrawImage(BackgroundBitmap, 0, 0, BackgroundBitmap.Width, BackgroundBitmap.Height);
			}
			DrawCloseButton(graphics2);
			DrawText(graphics2);
			graphics.DrawImage(image, 0, 0);
		}
	}
}
