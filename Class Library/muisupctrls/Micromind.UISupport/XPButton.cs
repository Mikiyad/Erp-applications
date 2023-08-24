using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class XPButton : Button
	{
		public enum ControlState
		{
			Normal,
			Hover,
			Pressed,
			Default,
			Disabled
		}

		private Container components;

		private ControlState enmState;

		private bool bCanClick;

		private Point locPoint;

		private static readonly Size sizeBorderPixelIndent;

		private static readonly Color clrOuterShadow1;

		private static readonly Color clrOuterShadow2;

		private static readonly Color clrBackground1;

		private static readonly Color clrBackground2;

		private static readonly Color clrBorder;

		private static readonly Color clrInnerShadowBottom1;

		private static readonly Color clrInnerShadowBottom2;

		private static readonly Color clrInnerShadowBottom3;

		private static readonly Color clrInnerShadowRight1a;

		private static readonly Color clrInnerShadowRight1b;

		private static readonly Color clrInnerShadowRight2a;

		private static readonly Color clrInnerShadowRight2b;

		private static readonly Color clrInnerShadowBottomPressed1;

		private static readonly Color clrInnerShadowBottomPressed2;

		private static readonly Color clrInnerShadowTopPressed1;

		private static readonly Color clrInnerShadowTopPressed2;

		private static readonly Color clrInnerShadowLeftPressed1;

		private static readonly Color clrInnerShadowLeftPressed2;

		private XPStyle m_btnStyle;

		private BtnShape m_btnShape;

		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
			set
			{
				base.FlatStyle = FlatStyle.Standard;
			}
		}

		public BtnShape BtnShape
		{
			get
			{
				return m_btnShape;
			}
			set
			{
				m_btnShape = value;
				Invalidate();
			}
		}

		[DefaultValue("Blue")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public XPStyle BtnStyle
		{
			get
			{
				return m_btnStyle;
			}
			set
			{
				m_btnStyle = value;
				Invalidate();
			}
		}

		public Point AdjustImageLocation
		{
			get
			{
				return locPoint;
			}
			set
			{
				locPoint = value;
				Invalidate();
			}
		}

		private Rectangle BorderRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				return new Rectangle(1, 1, clientRectangle.Width - 3, clientRectangle.Height - 3);
			}
		}

		public XPButton()
		{
			InitializeComponent();
			SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, value: true);
		}

		static XPButton()
		{
			sizeBorderPixelIndent = new Size(4, 4);
			clrOuterShadow1 = Color.WhiteSmoke;
			clrOuterShadow2 = Color.WhiteSmoke;
			clrBackground1 = Color.FromArgb(250, 250, 248);
			clrBackground2 = Color.FromArgb(240, 240, 234);
			clrBorder = Color.FromArgb(0, 60, 116);
			clrInnerShadowBottom1 = Color.FromArgb(236, 235, 230);
			clrInnerShadowBottom2 = Color.FromArgb(226, 223, 214);
			clrInnerShadowBottom3 = Color.FromArgb(214, 208, 197);
			clrInnerShadowRight1a = Color.FromArgb(128, 236, 234, 230);
			clrInnerShadowRight1b = Color.FromArgb(128, 224, 220, 212);
			clrInnerShadowRight2a = Color.FromArgb(128, 234, 228, 218);
			clrInnerShadowRight2b = Color.FromArgb(128, 212, 208, 196);
			clrInnerShadowBottomPressed1 = Color.FromArgb(234, 233, 227);
			clrInnerShadowBottomPressed2 = Color.FromArgb(242, 241, 238);
			clrInnerShadowTopPressed1 = Color.FromArgb(209, 204, 193);
			clrInnerShadowTopPressed2 = Color.FromArgb(220, 216, 207);
			clrInnerShadowLeftPressed1 = Color.FromArgb(216, 213, 203);
			clrInnerShadowLeftPressed2 = Color.FromArgb(222, 220, 211);
		}

		protected override void OnClick(EventArgs ea)
		{
			base.Capture = false;
			bCanClick = false;
			if (base.ClientRectangle.Contains(PointToClient(Control.MousePosition)))
			{
				enmState = ControlState.Hover;
			}
			else
			{
				enmState = ControlState.Normal;
			}
			Invalidate();
			base.OnClick(ea);
		}

		protected override void OnMouseEnter(EventArgs ea)
		{
			base.OnMouseEnter(ea);
			enmState = ControlState.Hover;
			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs mea)
		{
			base.OnMouseDown(mea);
			if (mea.Button == MouseButtons.Left)
			{
				bCanClick = true;
				enmState = ControlState.Pressed;
				Invalidate();
			}
		}

		protected override void OnMouseMove(MouseEventArgs mea)
		{
			base.OnMouseMove(mea);
			if (base.ClientRectangle.Contains(mea.X, mea.Y))
			{
				if (enmState == ControlState.Hover && base.Capture && !bCanClick)
				{
					bCanClick = true;
					enmState = ControlState.Pressed;
					Invalidate();
				}
			}
			else if (enmState == ControlState.Pressed)
			{
				bCanClick = false;
				enmState = ControlState.Hover;
				Invalidate();
			}
		}

		protected override void OnMouseLeave(EventArgs ea)
		{
			base.OnMouseLeave(ea);
			enmState = ControlState.Normal;
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs pea)
		{
			OnPaintBackground(pea);
			switch (enmState)
			{
			case ControlState.Normal:
				if (base.Enabled)
				{
					if (Focused || base.IsDefault)
					{
						switch (m_btnShape)
						{
						case BtnShape.Rectangle:
							OnDrawDefault(pea.Graphics);
							break;
						case BtnShape.Ellipse:
							OnDrawDefaultEllipse(pea.Graphics);
							break;
						}
					}
					else
					{
						switch (m_btnShape)
						{
						case BtnShape.Rectangle:
							OnDrawNormal(pea.Graphics);
							break;
						case BtnShape.Ellipse:
							OnDrawNormalEllipse(pea.Graphics);
							break;
						}
					}
				}
				else
				{
					OnDrawDisabled(pea.Graphics);
				}
				break;
			case ControlState.Hover:
				switch (m_btnShape)
				{
				case BtnShape.Rectangle:
					OnDrawHover(pea.Graphics);
					break;
				case BtnShape.Ellipse:
					OnDrawHoverEllipse(pea.Graphics);
					break;
				}
				break;
			case ControlState.Pressed:
				switch (m_btnShape)
				{
				case BtnShape.Rectangle:
					OnDrawPressed(pea.Graphics);
					break;
				case BtnShape.Ellipse:
					OnDrawPressedEllipse(pea.Graphics);
					break;
				}
				break;
			}
			OnDrawTextAndImage(pea.Graphics);
		}

		protected override void OnEnabledChanged(EventArgs ea)
		{
			base.OnEnabledChanged(ea);
			enmState = ControlState.Normal;
			Invalidate();
		}

		private void OnDrawNormal(Graphics g)
		{
			DrawNormalButton(g);
		}

		private void OnDrawHoverEllipse(Graphics g)
		{
			DrawNormalEllipse(g);
			DrawEllipseHoverBorder(g);
			DrawEllipseBorder(g);
		}

		private void OnDrawHover(Graphics g)
		{
			DrawNormalButton(g);
			Rectangle borderRectangle = BorderRectangle;
			Pen pen = new Pen(Color.FromArgb(255, 240, 207));
			Pen pen2 = new Pen(Color.FromArgb(253, 216, 137));
			g.DrawLine(pen, borderRectangle.Left + 2, borderRectangle.Top + 1, borderRectangle.Right - 2, borderRectangle.Top + 1);
			g.DrawLine(pen2, borderRectangle.Left + 1, borderRectangle.Top + 2, borderRectangle.Right - 1, borderRectangle.Top + 2);
			pen.Dispose();
			pen2.Dispose();
			Pen pen3 = new Pen(Color.FromArgb(248, 178, 48));
			Pen pen4 = new Pen(Color.FromArgb(229, 151, 0));
			g.DrawLine(pen3, borderRectangle.Left + 1, borderRectangle.Bottom - 2, borderRectangle.Right - 1, borderRectangle.Bottom - 2);
			g.DrawLine(pen4, borderRectangle.Left + 2, borderRectangle.Bottom - 1, borderRectangle.Right - 2, borderRectangle.Bottom - 1);
			pen3.Dispose();
			pen4.Dispose();
			Rectangle rect = new Rectangle(borderRectangle.Left + 1, borderRectangle.Top + 3, 2, borderRectangle.Height - 5);
			Rectangle rect2 = new Rectangle(borderRectangle.Right - 2, borderRectangle.Top + 3, 2, borderRectangle.Height - 5);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(254, 221, 149), Color.FromArgb(249, 180, 53), LinearGradientMode.Vertical);
			g.FillRectangle(linearGradientBrush, rect);
			g.FillRectangle(linearGradientBrush, rect2);
			linearGradientBrush.Dispose();
		}

		private void OnDrawPressedEllipse(Graphics g)
		{
			DrawPressedEllipse(g);
			DrawEllipseBorder(g);
		}

		private void DrawPressedEllipse(Graphics g)
		{
			Rectangle borderRectangle = BorderRectangle;
			Rectangle rect = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 1, borderRectangle.Height - 1);
			SolidBrush brush = new SolidBrush(Color.FromArgb(226, 225, 218));
			g.FillEllipse(brush, rect);
		}

		private void OnDrawPressed(Graphics g)
		{
			Rectangle borderRectangle = BorderRectangle;
			DrawOuterShadow(g);
			Rectangle rect = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 1, borderRectangle.Height - 1);
			SolidBrush solidBrush = new SolidBrush(Color.FromArgb(226, 225, 218));
			g.FillRectangle(solidBrush, rect);
			solidBrush.Dispose();
			DrawBorder(g);
			Pen pen = new Pen(clrInnerShadowBottomPressed1);
			Pen pen2 = new Pen(clrInnerShadowBottomPressed2);
			g.DrawLine(pen, borderRectangle.Left + 1, borderRectangle.Bottom - 2, borderRectangle.Right - 1, borderRectangle.Bottom - 2);
			g.DrawLine(pen2, borderRectangle.Left + 2, borderRectangle.Bottom - 1, borderRectangle.Right - 2, borderRectangle.Bottom - 1);
			pen.Dispose();
			pen2.Dispose();
			Pen pen3 = new Pen(clrInnerShadowTopPressed1);
			Pen pen4 = new Pen(clrInnerShadowTopPressed2);
			g.DrawLine(pen3, borderRectangle.Left + 2, borderRectangle.Top + 1, borderRectangle.Right - 2, borderRectangle.Top + 1);
			g.DrawLine(pen4, borderRectangle.Left + 1, borderRectangle.Top + 2, borderRectangle.Right - 1, borderRectangle.Top + 2);
			pen3.Dispose();
			pen4.Dispose();
			Pen pen5 = new Pen(clrInnerShadowLeftPressed1);
			Pen pen6 = new Pen(clrInnerShadowLeftPressed2);
			g.DrawLine(pen5, borderRectangle.Left + 1, borderRectangle.Top + 3, borderRectangle.Left + 1, borderRectangle.Bottom - 3);
			g.DrawLine(pen6, borderRectangle.Left + 2, borderRectangle.Top + 3, borderRectangle.Left + 2, borderRectangle.Bottom - 3);
			pen5.Dispose();
			pen6.Dispose();
		}

		private void OnDrawNormalEllipse(Graphics g)
		{
			DrawNormalEllipse(g);
			DrawEllipseBorder(g);
		}

		private void OnDrawDefaultEllipse(Graphics g)
		{
			DrawNormalEllipse(g);
			DrawEllipseDefaultBorder(g);
			DrawEllipseBorder(g);
		}

		private void OnDrawDefault(Graphics g)
		{
			DrawNormalButton(g);
			Rectangle borderRectangle = BorderRectangle;
			Pen pen = new Pen(Color.FromArgb(206, 231, 255));
			Pen pen2 = new Pen(Color.FromArgb(188, 212, 246));
			g.DrawLine(pen, borderRectangle.Left + 2, borderRectangle.Top + 1, borderRectangle.Right - 2, borderRectangle.Top + 1);
			g.DrawLine(pen2, borderRectangle.Left + 1, borderRectangle.Top + 2, borderRectangle.Right - 1, borderRectangle.Top + 2);
			pen.Dispose();
			pen2.Dispose();
			Pen pen3 = new Pen(Color.FromArgb(137, 173, 228));
			Pen pen4 = new Pen(Color.FromArgb(105, 130, 238));
			g.DrawLine(pen3, borderRectangle.Left + 1, borderRectangle.Bottom - 2, borderRectangle.Right - 1, borderRectangle.Bottom - 2);
			g.DrawLine(pen4, borderRectangle.Left + 2, borderRectangle.Bottom - 1, borderRectangle.Right - 2, borderRectangle.Bottom - 1);
			pen3.Dispose();
			pen4.Dispose();
			Rectangle rect = new Rectangle(borderRectangle.Left + 1, borderRectangle.Top + 3, 2, borderRectangle.Height - 5);
			Rectangle rect2 = new Rectangle(borderRectangle.Right - 2, borderRectangle.Top + 3, 2, borderRectangle.Height - 5);
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(186, 211, 245), Color.FromArgb(137, 173, 228), LinearGradientMode.Vertical);
			g.FillRectangle(linearGradientBrush, rect);
			g.FillRectangle(linearGradientBrush, rect2);
			linearGradientBrush.Dispose();
		}

		private void OnDrawDisabled(Graphics g)
		{
			Rectangle borderRectangle = BorderRectangle;
			Rectangle rect = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 1, borderRectangle.Height - 1);
			SolidBrush solidBrush = new SolidBrush(Color.FromArgb(245, 244, 234));
			g.FillRectangle(solidBrush, rect);
			solidBrush.Dispose();
			Pen pen = new Pen(Color.FromArgb(201, 199, 186));
			ButtonControlPaint.DrawRoundedRectangle(g, pen, borderRectangle, sizeBorderPixelIndent);
			pen.Dispose();
		}

		private void OnDrawTextAndImage(Graphics g)
		{
			SolidBrush solidBrush = (!base.Enabled) ? new SolidBrush(ButtonControlPaint.DisabledForeColor) : new SolidBrush(ForeColor);
			StringFormat stringFormat = ButtonControlPaint.GetStringFormat(TextAlign);
			stringFormat.HotkeyPrefix = HotkeyPrefix.Show;
			if (base.Image != null)
			{
				Rectangle r = default(Rectangle);
				Point point = new Point(6, 4);
				switch (base.ImageAlign)
				{
				case ContentAlignment.MiddleRight:
					r.Width = base.ClientRectangle.Width - base.Image.Width - 8;
					r.Height = base.ClientRectangle.Height;
					r.X = 0;
					r.Y = 0;
					point.X = r.Width;
					point.Y = base.ClientRectangle.Height / 2 - base.Image.Height / 2;
					break;
				case ContentAlignment.TopCenter:
					point.Y = 2;
					point.X = (base.ClientRectangle.Width - base.Image.Width) / 2;
					r.Width = base.ClientRectangle.Width;
					r.Height = base.ClientRectangle.Height - base.Image.Height - 4;
					r.X = base.ClientRectangle.X;
					r.Y = base.Image.Height;
					break;
				case ContentAlignment.MiddleCenter:
					point.X = (base.ClientRectangle.Width - base.Image.Width) / 2;
					point.Y = (base.ClientRectangle.Height - base.Image.Height) / 2;
					r.Width = 0;
					r.Height = 0;
					r.X = base.ClientRectangle.Width;
					r.Y = base.ClientRectangle.Height;
					break;
				case ContentAlignment.TopLeft:
					point.X = (base.ClientRectangle.Width - base.Image.Width) / 2;
					point.Y = (base.ClientRectangle.Height - base.Image.Height) / 2;
					r.Width = 0;
					r.Height = 0;
					r.X = base.ClientRectangle.Width;
					r.Y = base.ClientRectangle.Height;
					break;
				case ContentAlignment.MiddleLeft:
					point.X = 4;
					point.Y = (base.ClientRectangle.Height - base.Image.Height) / 2 - 2;
					r.Width = base.ClientRectangle.Width;
					r.Height = base.ClientRectangle.Height;
					r.X = base.ClientRectangle.X;
					r.Y = base.ClientRectangle.Y;
					break;
				default:
					point.X = 0;
					point.Y = 0;
					r.Width = base.ClientRectangle.Width - base.Image.Width;
					r.Height = base.ClientRectangle.Height;
					r.X = base.Image.Width;
					r.Y = 0;
					break;
				}
				if (base.Enabled)
				{
					g.DrawImage(base.Image, point);
				}
				else
				{
					ControlPaint.DrawImageDisabled(g, base.Image, locPoint.X, locPoint.Y, BackColor);
				}
				if (ContentAlignment.MiddleCenter != base.ImageAlign)
				{
					g.DrawString(Text, Font, solidBrush, r, stringFormat);
				}
			}
			else
			{
				g.DrawString(Text, Font, solidBrush, base.ClientRectangle, stringFormat);
			}
			solidBrush.Dispose();
			stringFormat.Dispose();
		}

		private void DrawNormalEllipse(Graphics g)
		{
			Rectangle borderRectangle = BorderRectangle;
			LinearGradientBrush linearGradientBrush = null;
			switch (m_btnStyle)
			{
			case XPStyle.Default:
				linearGradientBrush = new LinearGradientBrush(borderRectangle, clrBackground1, clrBackground2, LinearGradientMode.Vertical);
				break;
			case XPStyle.Blue:
				linearGradientBrush = new LinearGradientBrush(borderRectangle, Color.FromArgb(248, 252, 253), Color.FromArgb(172, 171, 201), LinearGradientMode.Vertical);
				break;
			case XPStyle.OliveGreen:
				linearGradientBrush = new LinearGradientBrush(borderRectangle, Color.FromArgb(250, 250, 240), Color.FromArgb(235, 220, 190), LinearGradientMode.Vertical);
				break;
			case XPStyle.Silver:
				linearGradientBrush = new LinearGradientBrush(borderRectangle, Color.FromArgb(253, 253, 253), Color.FromArgb(205, 205, 205), LinearGradientMode.Vertical);
				break;
			}
			float[] factors = new float[3]
			{
				0f,
				0.008f,
				1f
			};
			float[] positions = new float[3]
			{
				0f,
				0.22f,
				1f
			};
			Blend blend = new Blend();
			blend.Factors = factors;
			blend.Positions = positions;
			linearGradientBrush.Blend = blend;
			g.FillEllipse(linearGradientBrush, borderRectangle);
		}

		private void DrawNormalButton(Graphics g)
		{
			Rectangle borderRectangle = BorderRectangle;
			DrawOuterShadow(g);
			Rectangle rect = new Rectangle(borderRectangle.X + 1, borderRectangle.Y + 1, borderRectangle.Width - 1, borderRectangle.Height - 1);
			LinearGradientBrush linearGradientBrush = null;
			switch (m_btnStyle)
			{
			case XPStyle.Default:
				linearGradientBrush = new LinearGradientBrush(rect, clrBackground1, clrBackground2, LinearGradientMode.Vertical);
				break;
			case XPStyle.Blue:
				linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(248, 252, 253), Color.FromArgb(172, 171, 201), LinearGradientMode.Vertical);
				break;
			case XPStyle.OliveGreen:
				linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(250, 250, 240), Color.FromArgb(235, 220, 190), LinearGradientMode.Vertical);
				break;
			case XPStyle.Silver:
				linearGradientBrush = new LinearGradientBrush(rect, Color.FromArgb(253, 253, 253), Color.FromArgb(205, 205, 205), LinearGradientMode.Vertical);
				break;
			}
			float[] factors = new float[3]
			{
				0f,
				0.08f,
				1f
			};
			float[] positions = new float[3]
			{
				0f,
				0.32f,
				1f
			};
			Blend blend = new Blend();
			blend.Factors = factors;
			blend.Positions = positions;
			linearGradientBrush.Blend = blend;
			g.FillRectangle(linearGradientBrush, rect);
			linearGradientBrush.Dispose();
			DrawBorder(g);
			if (m_btnStyle == XPStyle.Default)
			{
				Pen pen = new Pen(clrInnerShadowBottom1);
				Pen pen2 = new Pen(clrInnerShadowBottom2);
				Pen pen3 = new Pen(clrInnerShadowBottom3);
				g.DrawLine(pen, borderRectangle.Left + 1, borderRectangle.Bottom - 3, borderRectangle.Right - 1, borderRectangle.Bottom - 3);
				g.DrawLine(pen2, borderRectangle.Left + 1, borderRectangle.Bottom - 2, borderRectangle.Right - 1, borderRectangle.Bottom - 2);
				g.DrawLine(pen3, borderRectangle.Left + 2, borderRectangle.Bottom - 1, borderRectangle.Right - 2, borderRectangle.Bottom - 1);
				pen.Dispose();
				pen2.Dispose();
				pen3.Dispose();
				Point point = new Point(borderRectangle.Right - 2, borderRectangle.Top + 1);
				Point point2 = new Point(borderRectangle.Right - 2, borderRectangle.Bottom - 1);
				Point point3 = new Point(borderRectangle.Right - 1, borderRectangle.Top + 2);
				Point point4 = new Point(borderRectangle.Right - 1, borderRectangle.Bottom - 2);
				LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(point, point2, clrInnerShadowRight1a, clrInnerShadowRight1b);
				Pen pen4 = new Pen(linearGradientBrush2);
				LinearGradientBrush linearGradientBrush3 = new LinearGradientBrush(point3, point4, clrInnerShadowRight2a, clrInnerShadowRight2b);
				Pen pen5 = new Pen(linearGradientBrush3);
				g.DrawLine(pen4, point, point2);
				g.DrawLine(pen5, point3, point4);
				pen4.Dispose();
				pen5.Dispose();
				linearGradientBrush2.Dispose();
				linearGradientBrush3.Dispose();
				Pen pen6 = new Pen(Color.White);
				g.DrawLine(pen6, borderRectangle.Left + 2, borderRectangle.Top + 1, borderRectangle.Right - 2, borderRectangle.Top + 1);
				g.DrawLine(pen6, borderRectangle.Left + 1, borderRectangle.Top + 2, borderRectangle.Right - 1, borderRectangle.Top + 2);
				g.DrawLine(pen6, borderRectangle.Left + 1, borderRectangle.Top + 3, borderRectangle.Right - 1, borderRectangle.Top + 3);
				pen6.Dispose();
			}
		}

		private void DrawOuterShadow(Graphics g)
		{
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, clrOuterShadow1, clrOuterShadow2, LinearGradientMode.Vertical);
			g.FillRectangle(linearGradientBrush, base.ClientRectangle);
			linearGradientBrush.Dispose();
		}

		private void DrawEllipseOuterShadow(Graphics g)
		{
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush(base.ClientRectangle, clrOuterShadow1, clrOuterShadow2, LinearGradientMode.Vertical);
			g.FillRectangle(linearGradientBrush, base.ClientRectangle);
			linearGradientBrush.Dispose();
		}

		private void DrawBorder(Graphics g)
		{
			Pen pen = new Pen(clrBorder);
			ButtonControlPaint.DrawRoundedRectangle(g, pen, BorderRectangle, sizeBorderPixelIndent);
			pen.Dispose();
		}

		private void DrawEllipseBorder(Graphics g)
		{
			Pen pen = new Pen(Color.FromArgb(0, 0, 0));
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.DrawEllipse(pen, BorderRectangle);
			g.SmoothingMode = smoothingMode;
			pen.Dispose();
		}

		private void DrawEllipseDefaultBorder(Graphics g)
		{
			Pen pen = new Pen(Color.FromArgb(137, 173, 228), 2f);
			Rectangle rect = new Rectangle(BorderRectangle.X + 2, BorderRectangle.Y + 1, BorderRectangle.Width - 4, BorderRectangle.Height - 2);
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.DrawEllipse(pen, rect);
			g.SmoothingMode = smoothingMode;
			pen.Dispose();
		}

		private void DrawEllipseHoverBorder(Graphics g)
		{
			Pen pen = new Pen(Color.FromArgb(248, 178, 48), 2f);
			Rectangle rect = new Rectangle(BorderRectangle.X + 2, BorderRectangle.Y + 1, BorderRectangle.Width - 4, BorderRectangle.Height - 2);
			SmoothingMode smoothingMode = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			g.DrawEllipse(pen, rect);
			g.SmoothingMode = smoothingMode;
			pen.Dispose();
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
		}
	}
}
