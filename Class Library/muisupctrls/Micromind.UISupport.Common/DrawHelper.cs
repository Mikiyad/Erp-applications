using Micromind.UISupport.Win32;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Micromind.UISupport.Common
{
	public class DrawHelper
	{
		public enum CommandState
		{
			Normal,
			HotTrack,
			Pushed
		}

		protected static IntPtr _halfToneBrush = IntPtr.Zero;

		public static void DrawReverseString(Graphics g, string drawText, Font drawFont, Rectangle drawRect, Brush drawBrush, StringFormat drawFormat)
		{
			GraphicsContainer container = g.BeginContainer();
			g.TranslateTransform(drawRect.Left * 2 + drawRect.Width, drawRect.Top * 2 + drawRect.Height);
			g.RotateTransform(180f);
			g.DrawString(drawText, drawFont, drawBrush, drawRect, drawFormat);
			g.EndContainer(container);
		}

		public static void DrawPlainRaised(Graphics g, Rectangle boxRect, Color baseColor)
		{
			using (Pen pen = new Pen(ControlPaint.LightLight(baseColor)))
			{
				using (Pen pen2 = new Pen(ControlPaint.DarkDark(baseColor)))
				{
					g.DrawLine(pen, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
					g.DrawLine(pen, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
					g.DrawLine(pen2, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
					g.DrawLine(pen2, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
				}
			}
		}

		public static void DrawPlainSunken(Graphics g, Rectangle boxRect, Color baseColor)
		{
			using (Pen pen2 = new Pen(ControlPaint.LightLight(baseColor)))
			{
				using (Pen pen = new Pen(ControlPaint.DarkDark(baseColor)))
				{
					g.DrawLine(pen, boxRect.Left, boxRect.Bottom, boxRect.Left, boxRect.Top);
					g.DrawLine(pen, boxRect.Left, boxRect.Top, boxRect.Right, boxRect.Top);
					g.DrawLine(pen2, boxRect.Right, boxRect.Top, boxRect.Right, boxRect.Bottom);
					g.DrawLine(pen2, boxRect.Right, boxRect.Bottom, boxRect.Left, boxRect.Bottom);
				}
			}
		}

		public static void DrawPlainRaisedBorder(Graphics g, Rectangle rect, Color lightLight, Color baseColor, Color dark, Color darkDark)
		{
			if (rect.Width > 2 && rect.Height > 2)
			{
				using (Pen pen2 = new Pen(lightLight))
				{
					using (Pen pen = new Pen(baseColor))
					{
						using (Pen pen4 = new Pen(dark))
						{
							using (Pen pen3 = new Pen(darkDark))
							{
								int left = rect.Left;
								int top = rect.Top;
								int right = rect.Right;
								int bottom = rect.Bottom;
								g.DrawLine(pen, right - 1, top, left, top);
								g.DrawLine(pen2, right - 2, top + 1, left + 1, top + 1);
								g.DrawLine(pen, right - 3, top + 2, left + 2, top + 2);
								g.DrawLine(pen, left, top, left, bottom - 1);
								g.DrawLine(pen2, left + 1, top + 1, left + 1, bottom - 2);
								g.DrawLine(pen, left + 2, top + 2, left + 2, bottom - 3);
								g.DrawLine(pen3, right - 1, top + 1, right - 1, bottom - 1);
								g.DrawLine(pen4, right - 2, top + 2, right - 2, bottom - 2);
								g.DrawLine(pen, right - 3, top + 3, right - 3, bottom - 3);
								g.DrawLine(pen3, right - 1, bottom - 1, left, bottom - 1);
								g.DrawLine(pen4, right - 2, bottom - 2, left + 1, bottom - 2);
								g.DrawLine(pen, right - 3, bottom - 3, left + 2, bottom - 3);
							}
						}
					}
				}
			}
		}

		public static void DrawPlainRaisedBorderTopOrBottom(Graphics g, Rectangle rect, Color lightLight, Color baseColor, Color dark, Color darkDark, bool drawTop)
		{
			if (rect.Width > 2 && rect.Height > 2)
			{
				using (Pen pen2 = new Pen(lightLight))
				{
					using (Pen pen = new Pen(baseColor))
					{
						using (Pen pen4 = new Pen(dark))
						{
							using (Pen pen3 = new Pen(darkDark))
							{
								int left = rect.Left;
								int top = rect.Top;
								int right = rect.Right;
								int bottom = rect.Bottom;
								if (drawTop)
								{
									g.DrawLine(pen, right - 1, top, left, top);
									g.DrawLine(pen2, right - 1, top + 1, left, top + 1);
									g.DrawLine(pen, right - 1, top + 2, left, top + 2);
								}
								else
								{
									g.DrawLine(pen3, right - 1, bottom - 1, left, bottom - 1);
									g.DrawLine(pen4, right - 1, bottom - 2, left, bottom - 2);
									g.DrawLine(pen, right - 1, bottom - 3, left, bottom - 3);
								}
							}
						}
					}
				}
			}
		}

		public static void DrawPlainSunkenBorder(Graphics g, Rectangle rect, Color lightLight, Color baseColor, Color dark, Color darkDark)
		{
			if (rect.Width > 2 && rect.Height > 2)
			{
				using (Pen pen4 = new Pen(lightLight))
				{
					using (Pen pen3 = new Pen(baseColor))
					{
						using (Pen pen = new Pen(dark))
						{
							using (Pen pen2 = new Pen(darkDark))
							{
								int left = rect.Left;
								int top = rect.Top;
								int right = rect.Right;
								int bottom = rect.Bottom;
								g.DrawLine(pen, right - 1, top, left, top);
								g.DrawLine(pen2, right - 2, top + 1, left + 1, top + 1);
								g.DrawLine(pen3, right - 3, top + 2, left + 2, top + 2);
								g.DrawLine(pen, left, top, left, bottom - 1);
								g.DrawLine(pen2, left + 1, top + 1, left + 1, bottom - 2);
								g.DrawLine(pen3, left + 2, top + 2, left + 2, bottom - 3);
								g.DrawLine(pen4, right - 1, top + 1, right - 1, bottom - 1);
								g.DrawLine(pen3, right - 2, top + 2, right - 2, bottom - 2);
								g.DrawLine(pen3, right - 3, top + 3, right - 3, bottom - 3);
								g.DrawLine(pen4, right - 1, bottom - 1, left, bottom - 1);
								g.DrawLine(pen3, right - 2, bottom - 2, left + 1, bottom - 2);
								g.DrawLine(pen3, right - 3, bottom - 3, left + 2, bottom - 3);
							}
						}
					}
				}
			}
		}

		public static void DrawPlainSunkenBorderTopOrBottom(Graphics g, Rectangle rect, Color lightLight, Color baseColor, Color dark, Color darkDark, bool drawTop)
		{
			if (rect.Width > 2 && rect.Height > 2)
			{
				using (Pen pen4 = new Pen(lightLight))
				{
					using (Pen pen3 = new Pen(baseColor))
					{
						using (Pen pen = new Pen(dark))
						{
							using (Pen pen2 = new Pen(darkDark))
							{
								int left = rect.Left;
								int top = rect.Top;
								int right = rect.Right;
								int bottom = rect.Bottom;
								if (drawTop)
								{
									g.DrawLine(pen, right - 1, top, left, top);
									g.DrawLine(pen2, right - 1, top + 1, left, top + 1);
									g.DrawLine(pen3, right - 1, top + 2, left, top + 2);
								}
								else
								{
									g.DrawLine(pen4, right - 1, bottom - 1, left, bottom - 1);
									g.DrawLine(pen3, right - 1, bottom - 2, left, bottom - 2);
									g.DrawLine(pen3, right - 1, bottom - 3, left, bottom - 3);
								}
							}
						}
					}
				}
			}
		}

		public static void DrawButtonCommand(Graphics g, VisualStyle style, Direction direction, Rectangle drawRect, CommandState state, Color baseColor, Color trackLight, Color trackBorder)
		{
			Rectangle rectangle = new Rectangle(drawRect.Left, drawRect.Top, drawRect.Width - 1, drawRect.Height - 1);
			switch (style)
			{
			case VisualStyle.Plain:
			{
				using (SolidBrush brush3 = new SolidBrush(baseColor))
				{
					g.FillRectangle(brush3, rectangle);
				}
				switch (state)
				{
				case CommandState.HotTrack:
					DrawPlainRaised(g, rectangle, baseColor);
					break;
				case CommandState.Pushed:
					DrawPlainSunken(g, rectangle, baseColor);
					break;
				}
				break;
			}
			case VisualStyle.IDE:
				switch (state)
				{
				case CommandState.Pushed:
					break;
				case CommandState.Normal:
				{
					using (SolidBrush brush2 = new SolidBrush(baseColor))
					{
						g.FillRectangle(brush2, rectangle);
					}
					break;
				}
				case CommandState.HotTrack:
				{
					g.FillRectangle(Brushes.White, rectangle);
					using (SolidBrush brush = new SolidBrush(trackLight))
					{
						g.FillRectangle(brush, rectangle);
					}
					using (Pen pen = new Pen(trackBorder))
					{
						g.DrawRectangle(pen, rectangle);
					}
					break;
				}
				}
				break;
			}
		}

		public static void DrawSeparatorCommand(Graphics g, VisualStyle style, Direction direction, Rectangle drawRect, Color baseColor)
		{
			if (style == VisualStyle.IDE)
			{
				using (Pen pen = new Pen(ControlPaint.Dark(baseColor)))
				{
					if (direction == Direction.Horizontal)
					{
						g.DrawLine(pen, drawRect.Left, drawRect.Top, drawRect.Left, drawRect.Bottom - 1);
					}
					else
					{
						g.DrawLine(pen, drawRect.Left, drawRect.Top, drawRect.Right - 1, drawRect.Top);
					}
				}
			}
			else
			{
				using (Pen pen2 = new Pen(ControlPaint.Dark(baseColor)))
				{
					using (Pen pen3 = new Pen(ControlPaint.LightLight(baseColor)))
					{
						if (direction == Direction.Horizontal)
						{
							g.DrawLine(pen2, drawRect.Left, drawRect.Top, drawRect.Left, drawRect.Bottom - 1);
							g.DrawLine(pen3, drawRect.Left + 1, drawRect.Top, drawRect.Left + 1, drawRect.Bottom - 1);
						}
						else
						{
							g.DrawLine(pen2, drawRect.Left, drawRect.Top, drawRect.Right - 1, drawRect.Top);
							g.DrawLine(pen3, drawRect.Left, drawRect.Top + 1, drawRect.Right - 1, drawRect.Top + 1);
						}
					}
				}
			}
		}

		public static void DrawDragRectangle(Rectangle newRect, int indent)
		{
			DrawDragRectangles(new Rectangle[1]
			{
				newRect
			}, indent);
		}

		public static void DrawDragRectangles(Rectangle[] newRects, int indent)
		{
			if (newRects.Length != 0)
			{
				IntPtr intPtr = CreateRectangleRegion(newRects[0], indent);
				for (int i = 1; i < newRects.Length; i++)
				{
					IntPtr intPtr2 = CreateRectangleRegion(newRects[i], indent);
					Gdi32.CombineRgn(intPtr, intPtr, intPtr2, 3);
					Gdi32.DeleteObject(intPtr2);
				}
				IntPtr dC = User32.GetDC(IntPtr.Zero);
				Gdi32.SelectClipRgn(dC, intPtr);
				RECT rectBox = default(RECT);
				Gdi32.GetClipBox(dC, ref rectBox);
				IntPtr halfToneBrush = GetHalfToneBrush();
				IntPtr hObject = Gdi32.SelectObject(dC, halfToneBrush);
				Gdi32.PatBlt(dC, rectBox.left, rectBox.top, rectBox.right - rectBox.left, rectBox.bottom - rectBox.top, 5898313u);
				Gdi32.SelectObject(dC, hObject);
				Gdi32.SelectClipRgn(dC, IntPtr.Zero);
				Gdi32.DeleteObject(intPtr);
				User32.ReleaseDC(IntPtr.Zero, dC);
			}
		}

		protected static IntPtr CreateRectangleRegion(Rectangle rect, int indent)
		{
			RECT rect2 = default(RECT);
			rect2.left = rect.Left;
			rect2.top = rect.Top;
			rect2.right = rect.Right;
			rect2.bottom = rect.Bottom;
			IntPtr intPtr = Gdi32.CreateRectRgnIndirect(ref rect2);
			if (indent <= 0 || rect.Width <= indent || rect.Height <= indent)
			{
				return intPtr;
			}
			rect2.left += indent;
			rect2.top += indent;
			rect2.right -= indent;
			rect2.bottom -= indent;
			IntPtr intPtr2 = Gdi32.CreateRectRgnIndirect(ref rect2);
			RECT rect3 = default(RECT);
			rect3.left = 0;
			rect3.top = 0;
			rect3.right = 0;
			rect3.bottom = 0;
			IntPtr intPtr3 = Gdi32.CreateRectRgnIndirect(ref rect3);
			Gdi32.CombineRgn(intPtr3, intPtr, intPtr2, 3);
			Gdi32.DeleteObject(intPtr);
			Gdi32.DeleteObject(intPtr2);
			return intPtr3;
		}

		protected static IntPtr GetHalfToneBrush()
		{
			if (_halfToneBrush == IntPtr.Zero)
			{
				Bitmap bitmap = new Bitmap(8, 8, PixelFormat.Format32bppArgb);
				Color color = Color.FromArgb(255, 255, 255, 255);
				Color color2 = Color.FromArgb(255, 0, 0, 0);
				bool flag = true;
				int num = 0;
				while (num < 8)
				{
					int num2 = 0;
					while (num2 < 8)
					{
						bitmap.SetPixel(num, num2, flag ? color : color2);
						num2++;
						flag = !flag;
					}
					num++;
					flag = !flag;
				}
				IntPtr hbitmap = bitmap.GetHbitmap();
				LOGBRUSH brush = default(LOGBRUSH);
				brush.lbStyle = 3u;
				brush.lbHatch = (uint)(int)hbitmap;
				_halfToneBrush = Gdi32.CreateBrushIndirect(ref brush);
			}
			return _halfToneBrush;
		}
	}
}
