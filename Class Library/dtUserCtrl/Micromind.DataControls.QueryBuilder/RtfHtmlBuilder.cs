using System;
using System.Collections;
using System.Drawing;
using System.Text;

namespace Micromind.DataControls.QueryBuilder
{
	public class RtfHtmlBuilder
	{
		private StringBuilder ms_Rtf;

		private StringBuilder ms_Html;

		private StringBuilder ms_Plain;

		private ArrayList mi_Colors;

		private bool mb_Linebreak;

		private bool mb_DivOpen;

		private int ms32_Color;

		private int ms32_Indent;

		private int ms32_RtfPosition;

		public Color SelectionColor
		{
			get
			{
				if (ms32_Color < 0)
				{
					return Color.Black;
				}
				return (Color)mi_Colors[ms32_Color];
			}
			set
			{
				if (mi_Colors == null)
				{
					mi_Colors = new ArrayList();
				}
				int num = mi_Colors.IndexOf(value);
				if (num < 0)
				{
					num = mi_Colors.Add(value);
				}
				ms32_Color = num;
			}
		}

		public int SelectionIndent
		{
			get
			{
				return ms32_Indent;
			}
			set
			{
				ms32_Indent = Math.Max(0, value);
			}
		}

		public int RtfPosition => ms32_RtfPosition;

		public RtfHtmlBuilder()
		{
			Clear();
		}

		public void Clear()
		{
			ms_Rtf = new StringBuilder(50000);
			ms_Html = new StringBuilder(50000);
			ms_Plain = new StringBuilder(50000);
			mi_Colors = null;
			mb_Linebreak = false;
			mb_DivOpen = false;
			ms32_Color = -1;
			ms32_Indent = 0;
			ms32_RtfPosition = 0;
		}

		public void AppendText(string s_Text)
		{
			s_Text = s_Text.Replace("\r", "");
			ms32_RtfPosition += s_Text.Length;
			string[] array = s_Text.Split("\n".ToCharArray());
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i > 0)
				{
					if (!mb_DivOpen)
					{
						ms_Html.Append("<div>");
					}
					if (mb_Linebreak)
					{
						ms_Rtf.AppendFormat("\\li{0} ", ms32_Indent * 300);
					}
					ms_Plain.Append("\r\n");
					ms_Html.Append("&nbsp;</div>\r\n");
					ms_Rtf.Append("\\par\\pard\r\n");
					mb_Linebreak = true;
					mb_DivOpen = false;
				}
				if (text.Length == 0)
				{
					continue;
				}
				if (mb_Linebreak)
				{
					mb_Linebreak = false;
					if (ms32_Indent > 0)
					{
						ms_Plain.Append(' ', ms32_Indent * 4);
						ms_Rtf.AppendFormat("\\li{0} ", ms32_Indent * 300);
						ms_Html.AppendFormat("<div style='margin-left:{0}px;'>", ms32_Indent * 20);
						mb_DivOpen = true;
					}
				}
				if (!mb_DivOpen)
				{
					mb_DivOpen = true;
					ms_Html.Append("<div>");
				}
				ms_Plain.Append(text);
				if (text == " ")
				{
					ms_Rtf.Append(" ");
					ms_Html.Append(" ");
					continue;
				}
				text = text.TrimStart(' ');
				ms_Rtf.AppendFormat("\\cf{0} ", ms32_Color + 1);
				ms_Rtf.Append(Functions.ReplaceRtf(text));
				ms_Html.AppendFormat("<font color={0}>", Functions.GetHtmlColor(SelectionColor));
				ms_Html.Append(Functions.ReplaceHtml(text));
				ms_Html.Append("</font>");
			}
		}

		public string BuildRtf(Font i_Font)
		{
			StringBuilder stringBuilder = new StringBuilder(1000);
			if (mi_Colors != null)
			{
				stringBuilder.Append("{\\colortbl;");
				foreach (Color mi_Color in mi_Colors)
				{
					stringBuilder.Append("\\red");
					stringBuilder.Append(mi_Color.R);
					stringBuilder.Append("\\green");
					stringBuilder.Append(mi_Color.G);
					stringBuilder.Append("\\blue");
					stringBuilder.Append(mi_Color.B);
					stringBuilder.Append(";");
				}
				stringBuilder.Append("}\r\n");
			}
			return "{\\rtf1\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset0 " + i_Font.Name + ";}}\r\n" + stringBuilder + "\\viewkind4\\uc1\\pard\\lang3082\\f0\\fs" + (int)(i_Font.Size * 2f) + "\r\n" + ms_Rtf + "\\par}\r\n";
		}

		public string GetHtml()
		{
			if (mb_DivOpen)
			{
				ms_Html.Append("&nbsp;</div>");
				mb_DivOpen = false;
			}
			return ms_Html.ToString();
		}

		public string GetText()
		{
			return ms_Plain.ToString();
		}
	}
}
