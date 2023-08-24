using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class QueryEditor : RichTextBoxEx
	{
		private bool mb_Reparsing;

		private Parser mi_ReParser = new Parser();

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				mb_Reparsing = true;
				mi_ReParser.Abort();
				if (mb_Reparsing && !base.ReadOnly)
				{
					mb_Reparsing = false;
					ReplaceRtf(mi_ReParser, b_ModifyLinebreaks: false);
				}
			}
		}

		public void ReplaceRtf(Parser i_Parser, bool b_ModifyLinebreaks)
		{
			int s32_CursorPos = base.SelectionStart;
			int selectionLength = SelectionLength;
			if (i_Parser.Parse(Text, b_ModifyLinebreaks, b_Append_LF: true, ref s32_CursorPos))
			{
				ReplaceRtf(i_Parser.GetRtf(Font), s32_CursorPos, selectionLength);
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			mi_ReParser.Abort();
			if (!e.Alt && !e.Shift)
			{
				if (e.Control)
				{
					Parser i_Parser = new Parser();
					switch (e.KeyCode)
					{
					case Keys.V:
						e.Handled = false;
						mb_Reparsing = true;
						break;
					case Keys.P:
						e.Handled = true;
						ReplaceRtf(i_Parser, b_ModifyLinebreaks: true);
						break;
					}
				}
				else
				{
					Keys keyCode = e.KeyCode;
					if (keyCode == Keys.Return || keyCode == Keys.Space)
					{
						mb_Reparsing = true;
					}
				}
			}
			base.OnKeyDown(e);
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			mi_ReParser.Abort();
			if (mb_Reparsing && !base.ReadOnly)
			{
				mb_Reparsing = false;
				ReplaceRtf(mi_ReParser, b_ModifyLinebreaks: false);
			}
			base.OnKeyUp(e);
		}
	}
}
