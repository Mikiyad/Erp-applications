using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	internal class ControlResizer
	{
		internal enum MoveOrResize
		{
			Move,
			Resize,
			MoveAndResize
		}

		private static bool _moving;

		private static Point _cursorStartPoint;

		private static bool _moveIsInterNal;

		private static bool _resizing;

		private static Size _currentControlStartSize;

		internal static bool MouseIsInLeftEdge
		{
			get;
			set;
		}

		internal static bool MouseIsInRightEdge
		{
			get;
			set;
		}

		internal static bool MouseIsInTopEdge
		{
			get;
			set;
		}

		internal static bool MouseIsInBottomEdge
		{
			get;
			set;
		}

		internal static MoveOrResize WorkType
		{
			get;
			set;
		}

		internal static void Init(Control control)
		{
			Init(control, control);
		}

		internal static void Init(Control control, Control container)
		{
			_moving = false;
			_resizing = false;
			_moveIsInterNal = false;
			_cursorStartPoint = Point.Empty;
			MouseIsInLeftEdge = false;
			MouseIsInLeftEdge = false;
			MouseIsInRightEdge = false;
			MouseIsInTopEdge = false;
			MouseIsInBottomEdge = false;
			WorkType = MoveOrResize.MoveAndResize;
			control.MouseDown += delegate(object sender, MouseEventArgs e)
			{
				StartMovingOrResizing(control, e);
			};
			control.MouseUp += delegate
			{
				StopDragOrResizing(control);
			};
			control.MouseMove += delegate(object sender, MouseEventArgs e)
			{
				MoveControl(container, e);
			};
		}

		private static void UpdateMouseEdgeProperties(Control control, Point mouseLocationInControl)
		{
			if (WorkType != 0)
			{
				MouseIsInRightEdge = (Math.Abs(mouseLocationInControl.X - control.Width) <= 2);
				MouseIsInTopEdge = (Math.Abs(mouseLocationInControl.Y) <= 2);
				MouseIsInBottomEdge = (Math.Abs(mouseLocationInControl.Y - control.Height) <= 2);
			}
		}

		private static void UpdateMouseCursor(Control control)
		{
			if (WorkType == MoveOrResize.Move)
			{
				return;
			}
			if (MouseIsInLeftEdge)
			{
				if (MouseIsInTopEdge)
				{
					control.Cursor = Cursors.SizeNWSE;
				}
				else if (MouseIsInBottomEdge)
				{
					control.Cursor = Cursors.SizeNESW;
				}
				else
				{
					control.Cursor = Cursors.SizeWE;
				}
			}
			else if (MouseIsInRightEdge)
			{
				if (MouseIsInTopEdge)
				{
					control.Cursor = Cursors.SizeNESW;
				}
				else if (MouseIsInBottomEdge)
				{
					control.Cursor = Cursors.SizeNWSE;
				}
				else
				{
					control.Cursor = Cursors.SizeWE;
				}
			}
			else if (MouseIsInTopEdge || MouseIsInBottomEdge)
			{
				control.Cursor = Cursors.SizeNS;
			}
			else
			{
				control.Cursor = Cursors.Default;
			}
		}

		private static void StartMovingOrResizing(Control control, MouseEventArgs e)
		{
			if (!_moving && !_resizing)
			{
				if (WorkType != 0 && (MouseIsInRightEdge || MouseIsInLeftEdge || MouseIsInTopEdge || MouseIsInBottomEdge))
				{
					_resizing = true;
					_currentControlStartSize = control.Size;
				}
				else if (WorkType != MoveOrResize.Resize)
				{
					_moving = true;
					control.Cursor = Cursors.Hand;
				}
				_cursorStartPoint = new Point(e.X, e.Y);
				control.Capture = true;
			}
		}

		private static void MoveControl(Control control, MouseEventArgs e)
		{
			if (!_resizing && !_moving)
			{
				UpdateMouseEdgeProperties(control, new Point(e.X, e.Y));
				UpdateMouseCursor(control);
			}
			if (_resizing)
			{
				if (MouseIsInLeftEdge)
				{
					if (MouseIsInTopEdge)
					{
						control.Width -= e.X - _cursorStartPoint.X;
						control.Left += e.X - _cursorStartPoint.X;
						control.Height -= e.Y - _cursorStartPoint.Y;
						control.Top += e.Y - _cursorStartPoint.Y;
					}
					else if (MouseIsInBottomEdge)
					{
						control.Width -= e.X - _cursorStartPoint.X;
						control.Left += e.X - _cursorStartPoint.X;
						control.Height = e.Y - _cursorStartPoint.Y + _currentControlStartSize.Height;
					}
					else
					{
						control.Width -= e.X - _cursorStartPoint.X;
						control.Left += e.X - _cursorStartPoint.X;
					}
				}
				else if (MouseIsInRightEdge)
				{
					if (MouseIsInTopEdge)
					{
						control.Width = e.X - _cursorStartPoint.X + _currentControlStartSize.Width;
						control.Height -= e.Y - _cursorStartPoint.Y;
						control.Top += e.Y - _cursorStartPoint.Y;
					}
					else if (MouseIsInBottomEdge)
					{
						control.Width = e.X - _cursorStartPoint.X + _currentControlStartSize.Width;
						control.Height = e.Y - _cursorStartPoint.Y + _currentControlStartSize.Height;
					}
					else
					{
						control.Width = e.X - _cursorStartPoint.X + _currentControlStartSize.Width;
					}
				}
				else if (MouseIsInTopEdge)
				{
					control.Height -= e.Y - _cursorStartPoint.Y;
					control.Top += e.Y - _cursorStartPoint.Y;
				}
				else if (MouseIsInBottomEdge)
				{
					control.Height = e.Y - _cursorStartPoint.Y + _currentControlStartSize.Height;
				}
				else
				{
					StopDragOrResizing(control);
				}
			}
			else if (_moving)
			{
				_moveIsInterNal = !_moveIsInterNal;
				if (!_moveIsInterNal)
				{
					int x = e.X - _cursorStartPoint.X + control.Left;
					int y = e.Y - _cursorStartPoint.Y + control.Top;
					control.Location = new Point(x, y);
				}
			}
		}

		private static void StopDragOrResizing(Control control)
		{
			_resizing = false;
			_moving = false;
			control.Capture = false;
			UpdateMouseCursor(control);
		}

		private static List<Control> GetAllChildControls(Control control, List<Control> list)
		{
			List<Control> list2 = control.Controls.Cast<Control>().ToList();
			list.AddRange(list2);
			return list2.SelectMany((Control ctrl) => GetAllChildControls(ctrl, list)).ToList();
		}

		internal static string GetSizeAndPositionOfControlsToString(Control container)
		{
			List<Control> list = new List<Control>();
			GetAllChildControls(container, list);
			CultureInfo provider = new CultureInfo("en");
			string text = string.Empty;
			foreach (Control item in list)
			{
				text = text + item.Name + ":" + item.Left.ToString(provider) + "," + item.Top.ToString(provider) + "," + item.Width.ToString(provider) + "," + item.Height.ToString(provider) + "*";
			}
			return text;
		}

		internal static void SetSizeAndPositionOfControlsFromString(Control container, string controlsInfoStr)
		{
			List<Control> list = new List<Control>();
			GetAllChildControls(container, list);
			string[] array = controlsInfoStr.Split(new string[1]
			{
				"*"
			}, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string[] array3 = array2[i].Split(new string[1]
				{
					":"
				}, StringSplitOptions.RemoveEmptyEntries);
				dictionary.Add(array3[0], array3[1]);
			}
			foreach (Control item in list)
			{
				dictionary.TryGetValue(item.Name, out string value);
				string[] array4 = value.Split(new string[1]
				{
					","
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array4.Length == 4)
				{
					item.Left = int.Parse(array4[0]);
					item.Top = int.Parse(array4[1]);
					item.Width = int.Parse(array4[2]);
					item.Height = int.Parse(array4[3]);
				}
			}
		}
	}
}
