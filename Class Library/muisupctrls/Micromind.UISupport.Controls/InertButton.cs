using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Micromind.UISupport.Controls
{
	[ToolboxBitmap(typeof(InertButton))]
	[DefaultProperty("PopupStyle")]
	public class InertButton : Control
	{
		protected int _borderWidth;

		protected bool _mouseOver;

		protected bool _mouseCapture;

		protected bool _popupStyle;

		protected int _imageIndexEnabled;

		protected int _imageIndexDisabled;

		protected ImageList _imageList;

		protected ImageAttributes _imageAttr;

		protected MouseButtons _mouseButton;

		[Category("Appearance")]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return _imageList;
			}
			set
			{
				if (_imageList != value)
				{
					_imageList = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(-1)]
		public int ImageIndexEnabled
		{
			get
			{
				return _imageIndexEnabled;
			}
			set
			{
				if (_imageIndexEnabled != value)
				{
					_imageIndexEnabled = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(-1)]
		public int ImageIndexDisabled
		{
			get
			{
				return _imageIndexDisabled;
			}
			set
			{
				if (_imageIndexDisabled != value)
				{
					_imageIndexDisabled = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(null)]
		public ImageAttributes ImageAttributes
		{
			get
			{
				return _imageAttr;
			}
			set
			{
				if (_imageAttr != value)
				{
					_imageAttr = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(2)]
		public int BorderWidth
		{
			get
			{
				return _borderWidth;
			}
			set
			{
				if (_borderWidth != value)
				{
					_borderWidth = value;
					Invalidate();
				}
			}
		}

		[Category("Appearance")]
		[DefaultValue(true)]
		public bool PopupStyle
		{
			get
			{
				return _popupStyle;
			}
			set
			{
				if (_popupStyle != value)
				{
					_popupStyle = value;
					Invalidate();
				}
			}
		}

		public InertButton()
		{
			InternalConstruct(null, -1, -1, null);
		}

		public InertButton(ImageList imageList, int imageIndexEnabled)
		{
			InternalConstruct(imageList, imageIndexEnabled, -1, null);
		}

		public InertButton(ImageList imageList, int imageIndexEnabled, int imageIndexDisabled)
		{
			InternalConstruct(imageList, imageIndexEnabled, imageIndexDisabled, null);
		}

		public InertButton(ImageList imageList, int imageIndexEnabled, int imageIndexDisabled, ImageAttributes imageAttr)
		{
			InternalConstruct(imageList, imageIndexEnabled, imageIndexDisabled, imageAttr);
		}

		public void InternalConstruct(ImageList imageList, int imageIndexEnabled, int imageIndexDisabled, ImageAttributes imageAttr)
		{
			_imageList = imageList;
			_imageIndexEnabled = imageIndexEnabled;
			_imageIndexDisabled = imageIndexDisabled;
			_imageAttr = imageAttr;
			_borderWidth = 2;
			_mouseOver = false;
			_mouseCapture = false;
			_popupStyle = true;
			_mouseButton = MouseButtons.None;
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
			SetStyle(ControlStyles.StandardDoubleClick, value: false);
			SetStyle(ControlStyles.Selectable, value: false);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!_mouseCapture)
			{
				_mouseOver = true;
				_mouseCapture = true;
				_mouseButton = e.Button;
				Invalidate();
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button == _mouseButton)
			{
				_mouseOver = false;
				_mouseCapture = false;
				Invalidate();
			}
			else
			{
				base.Capture = true;
			}
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			bool flag = base.ClientRectangle.Contains(new Point(e.X, e.Y));
			if (flag != _mouseOver)
			{
				_mouseOver = flag;
				Invalidate();
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			_mouseOver = true;
			Invalidate();
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			_mouseOver = false;
			Invalidate();
			base.OnMouseLeave(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (_imageList != null)
			{
				if (!base.Enabled)
				{
					if (_imageIndexDisabled != -1)
					{
						if (_imageAttr == null)
						{
							e.Graphics.DrawImage(_imageList.Images[_imageIndexDisabled], new Point(1, 1));
						}
						else
						{
							Image image = _imageList.Images[_imageIndexDisabled];
							Point[] array = new Point[3];
							array[0].X = 1;
							array[0].Y = 1;
							array[1].X = array[0].X + image.Width;
							array[1].Y = array[0].Y;
							array[2].X = array[0].X;
							array[2].Y = array[1].Y + image.Height;
							e.Graphics.DrawImage(_imageList.Images[_imageIndexDisabled], array, new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel, _imageAttr);
						}
					}
					else if (_imageIndexEnabled != -1)
					{
						ControlPaint.DrawImageDisabled(e.Graphics, _imageList.Images[_imageIndexEnabled], 1, 1, BackColor);
					}
				}
				else
				{
					if (_imageAttr == null)
					{
						e.Graphics.DrawImage(_imageList.Images[_imageIndexEnabled], (_mouseOver && _mouseCapture) ? new Point(2, 2) : new Point(1, 1));
					}
					else
					{
						Image image2 = _imageList.Images[_imageIndexEnabled];
						Point[] array2 = new Point[3];
						array2[0].X = ((!_mouseOver || !_mouseCapture) ? 1 : 2);
						array2[0].Y = ((!_mouseOver || !_mouseCapture) ? 1 : 2);
						array2[1].X = array2[0].X + image2.Width;
						array2[1].Y = array2[0].Y;
						array2[2].X = array2[0].X;
						array2[2].Y = array2[1].Y + image2.Height;
						e.Graphics.DrawImage(_imageList.Images[_imageIndexEnabled], array2, new Rectangle(0, 0, image2.Width, image2.Height), GraphicsUnit.Pixel, _imageAttr);
					}
					ButtonBorderStyle buttonBorderStyle = _popupStyle ? ((!_mouseOver || !base.Enabled) ? ButtonBorderStyle.Solid : (_mouseCapture ? ButtonBorderStyle.Inset : ButtonBorderStyle.Outset)) : ((!base.Enabled) ? ButtonBorderStyle.Solid : ((_mouseOver && _mouseCapture) ? ButtonBorderStyle.Inset : ButtonBorderStyle.Outset));
					ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, BackColor, _borderWidth, buttonBorderStyle, BackColor, _borderWidth, buttonBorderStyle, BackColor, _borderWidth, buttonBorderStyle, BackColor, _borderWidth, buttonBorderStyle);
				}
			}
			base.OnPaint(e);
		}
	}
}
