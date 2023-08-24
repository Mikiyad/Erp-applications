using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class RichTextBoxEx : RichTextBox
	{
		public class UndoBuffer
		{
			protected class RtfData
			{
				public string s_Rtf;

				public string s_Text;

				public int s32_SelStart;

				public int s32_SelLen;

				public RtfData(RichTextBoxEx i_Box)
				{
					s_Rtf = i_Box.Rtf;
					s32_SelStart = i_Box.SelectionStart;
					s32_SelLen = i_Box.SelectionLength;
					if (Defaults.DebugType == typeof(UndoBuffer))
					{
						s_Text = i_Box.Text;
						while (s_Text.EndsWith("\n"))
						{
							s_Text = Functions.Left(s_Text, s_Text.Length - 1);
						}
						s_Text = "\"" + s_Text + "\"";
					}
				}

				public void SetRtf(RichTextBoxEx i_Box)
				{
					i_Box.SetRtf(s_Rtf, s32_SelStart, s32_SelLen);
				}

				public bool Equals(RtfData i_Data)
				{
					if (s_Rtf == i_Data.s_Rtf && s32_SelStart == i_Data.s32_SelStart)
					{
						return s32_SelLen == i_Data.s32_SelLen;
					}
					return false;
				}
			}

			private StackEx mi_Undo = new StackEx();

			private StackEx mi_Redo = new StackEx();

			private RichTextBoxEx mi_Box;

			public bool IsEmpty
			{
				get
				{
					if (mi_Undo.Count == 0)
					{
						return mi_Redo.Count == 0;
					}
					return false;
				}
			}

			public UndoBuffer(RichTextBoxEx i_Box)
			{
				mi_Box = i_Box;
			}

			protected bool TextChanged()
			{
				if (mi_Undo.Count == 0)
				{
					return true;
				}
				return ((RtfData)mi_Undo.Peek()).s_Rtf != mi_Box.Rtf;
			}

			public void SetNewText()
			{
				Functions.PrintDebug("SetNewText() called", typeof(UndoBuffer));
				if (mi_Box.Text.Trim().Length != 0 || mi_Undo.Count != 0)
				{
					if (TextChanged())
					{
						mi_Redo.Clear();
						mi_Undo.Push(new RtfData(mi_Box));
					}
					DebugOut();
				}
			}

			public void Undo()
			{
				Functions.PrintDebug("Undo() called", typeof(UndoBuffer));
				if (mi_Undo.Count != 0)
				{
					RtfData rtfData = (RtfData)mi_Undo.Pop();
					RtfData rtfData2 = new RtfData(mi_Box);
					if (rtfData.Equals(rtfData2) && mi_Undo.Count > 0)
					{
						rtfData = (RtfData)mi_Undo.Pop();
					}
					rtfData.SetRtf(mi_Box);
					mi_Redo.Push(rtfData2);
					DebugOut();
				}
			}

			public void Redo()
			{
				Functions.PrintDebug("Redo() called", typeof(UndoBuffer));
				if (mi_Redo.Count != 0)
				{
					RtfData obj = (RtfData)mi_Redo.Pop();
					RtfData o_Value = new RtfData(mi_Box);
					obj.SetRtf(mi_Box);
					mi_Undo.Push(o_Value);
					DebugOut();
				}
			}

			private void DebugOut()
			{
				if (!(Defaults.DebugType != typeof(UndoBuffer)))
				{
					if (mi_Undo.Count == 0)
					{
						Functions.PrintDebug("Undo Stack empty.");
					}
					if (mi_Redo.Count == 0)
					{
						Functions.PrintDebug("Redo Stack empty.");
					}
					for (int i = 0; i < mi_Undo.Count; i++)
					{
						RtfData rtfData = (RtfData)mi_Undo[i];
						Functions.PrintDebug($"Undo Stack Pos {i}: {rtfData.s_Text}");
					}
					for (int j = 0; j < mi_Redo.Count; j++)
					{
						RtfData rtfData2 = (RtfData)mi_Redo[j];
						Functions.PrintDebug($"Redo Stack Pos {j}: {rtfData2.s_Text}");
					}
					Functions.PrintDebug("------------------------------");
				}
			}
		}

		private const int FALSE = 0;

		private const int TRUE = 1;

		private const int WM_SETREDRAW = 11;

		private string ms_LastContent = "";

		private int ms32_Lock;

		private Keys me_LastKey;

		private UndoBuffer mi_UndoBuf;

		public UndoBuffer UndoRedoBuffer
		{
			get
			{
				return mi_UndoBuf;
			}
			set
			{
				if (value == null)
				{
					mi_UndoBuf = new UndoBuffer(this);
				}
				else
				{
					mi_UndoBuf = value;
				}
			}
		}

		public int FirstVisibleLine
		{
			get
			{
				return SendMessageA(base.Handle, 206, 0, 0) + 1;
			}
			set
			{
				int firstVisibleLine = FirstVisibleLine;
				int val = Math.Max(1, TotalLines - VisibleLines + 1);
				int val2 = Math.Max(1, value);
				int cY = Math.Min(val, val2) - firstVisibleLine;
				Scroll(0, cY);
			}
		}

		public int VisibleLines => (base.Height - 4) / Font.Height;

		public int TotalLines => GetLineFromChar(Text.Length);

		public bool TextModified
		{
			get
			{
				return Text != ms_LastContent;
			}
			set
			{
				if (value)
				{
					ms_LastContent = null;
				}
				else
				{
					ms_LastContent = Text;
				}
			}
		}

		[DllImport("user32.dll")]
		private static extern int SendMessageA(IntPtr h_Wnd, int message, int wParam, int lParam);

		public RichTextBoxEx()
		{
			base.HideSelection = false;
			base.DetectUrls = false;
			mi_UndoBuf = new UndoBuffer(this);
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			BackColor = Defaults.BkgColControl;
		}

		private void LockWindow(bool b_Lock)
		{
			if (b_Lock)
			{
				if (ms32_Lock == 0)
				{
					SendMessageA(base.Handle, 11, 0, 0);
				}
				ms32_Lock++;
				return;
			}
			ms32_Lock--;
			if (ms32_Lock == 0)
			{
				SendMessageA(base.Handle, 11, 1, 0);
				Invalidate();
			}
		}

		public override bool PreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 && (int)msg.WParam == 9 && (Control.ModifierKeys & Keys.Control) > Keys.None)
			{
				OnKeyDown(new KeyEventArgs(Keys.LButton | Keys.Back | Keys.Control));
				return true;
			}
			return base.PreProcessMessage(ref msg);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Control && !e.Alt && !e.Shift)
			{
				switch (e.KeyCode)
				{
				case Keys.I:
				case Keys.L:
					e.Handled = true;
					return;
				case Keys.Z:
					e.Handled = true;
					mi_UndoBuf.Undo();
					return;
				case Keys.Y:
					e.Handled = true;
					mi_UndoBuf.Redo();
					return;
				}
			}
			if (!e.Control && !e.Alt && !e.Shift)
			{
				if (SelectionLength == 0)
				{
					base.SelectionColor = Color.Black;
				}
				switch (e.KeyCode)
				{
				case Keys.F3:
					e.Handled = true;
					return;
				case Keys.F4:
					e.Handled = true;
					return;
				}
			}
			if ((me_LastKey == Keys.Delete || me_LastKey == Keys.Back) && me_LastKey != e.KeyCode)
			{
				mi_UndoBuf.SetNewText();
			}
			me_LastKey = e.KeyCode;
			base.OnKeyDown(e);
		}

		public void Scroll(int cX, int cY)
		{
			SendMessageA(base.Handle, 182, cX, cY);
		}

		public void SetSelection(int s32_Start, int s32_Length)
		{
			base.SelectionStart = s32_Start;
			SelectionLength = s32_Length;
			int lineFromChar = GetLineFromChar(s32_Start);
			FirstVisibleLine = lineFromChar - VisibleLines / 2;
		}

		public int GetLineFromChar(int s32_Pos)
		{
			return SendMessageA(base.Handle, 201, s32_Pos, 0) + 1;
		}

		public int GetLineIndex(int s32_Line)
		{
			return SendMessageA(base.Handle, 187, s32_Line - 1, 0);
		}

		public void GetLineAndCharPos(out int s32_Line, out int s32_CharPos)
		{
			s32_Line = GetLineFromChar(base.SelectionStart);
			s32_CharPos = base.SelectionStart - GetLineIndex(s32_Line) + 1;
		}

		public void ReplaceRtf(string s_Rtf, int s32_SelectionStart, int s32_SelectionLength)
		{
			LockWindow(b_Lock: true);
			int num = SendMessageA(base.Handle, 190, 0, 0);
			base.Rtf = s_Rtf;
			base.SelectionStart = s32_SelectionStart;
			SelectionLength = s32_SelectionLength;
			SendMessageA(base.Handle, 277, 65536 * num + 4, 0);
			LockWindow(b_Lock: false);
			mi_UndoBuf.SetNewText();
		}

		protected void SetRtf(string s_Rtf, int s32_SelectionStart, int s32_SelectionLength)
		{
			LockWindow(b_Lock: true);
			base.Rtf = s_Rtf;
			SetSelection(s32_SelectionStart, s32_SelectionLength);
			LockWindow(b_Lock: false);
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			base.OnSelectionChanged(e);
			GetLineAndCharPos(out int _, out int _);
		}
	}
}
