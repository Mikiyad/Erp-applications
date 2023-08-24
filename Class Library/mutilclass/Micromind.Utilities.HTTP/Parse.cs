using System.Collections;

namespace Micromind.Utilities.HTTP
{
	internal class Parse : AttributeList
	{
		private Hashtable specialCharacter = new Hashtable();

		protected bool hasWhiteSpace = true;

		protected bool hasTag = true;

		protected string m_noTagSource = "";

		protected string sourceURL;

		protected string m_source;

		private int m_idx;

		private char m_parseDelim;

		private string m_parseName;

		private string m_parseValue;

		internal string m_tag;

		internal string ParseName
		{
			get
			{
				return m_parseName;
			}
			set
			{
				m_parseName = value;
			}
		}

		internal string ParseValue
		{
			get
			{
				return m_parseValue;
			}
			set
			{
				m_parseValue = value;
			}
		}

		internal char ParseDelim
		{
			get
			{
				return m_parseDelim;
			}
			set
			{
				m_parseDelim = value;
			}
		}

		internal string Source
		{
			get
			{
				return m_source;
			}
			set
			{
				m_source = value;
				m_noTagSource = m_source;
			}
		}

		internal string SourceURL
		{
			get
			{
				return sourceURL;
			}
			set
			{
				sourceURL = value;
				HTTPClient hTTPClient = new HTTPClient(null, null);
				Source = hTTPClient.GetPage(value);
				_ = m_source;
			}
		}

		internal static bool IsWhiteSpace(char ch)
		{
			return "\t\n\r ".IndexOf(ch) != -1;
		}

		protected Hashtable GetSpecialCharacters()
		{
			specialCharacter.Clear();
			specialCharacter.Add("&lt", "<");
			specialCharacter.Add("&gt", ">");
			specialCharacter.Add("&amp", "&");
			specialCharacter.Add("&quot", "\"");
			specialCharacter.Add("&nbsp", " ");
			specialCharacter.Add("&ensp", " ");
			specialCharacter.Add("&emsp", " ");
			specialCharacter.Add("&endash", " ");
			specialCharacter.Add("&emdash", " ");
			specialCharacter.Add("&copy", "©");
			specialCharacter.Add("&#32;", " ");
			specialCharacter.Add("&#33;", "!");
			specialCharacter.Add("&#34;", "\"");
			specialCharacter.Add("&#35;", "#");
			specialCharacter.Add("&#36;", "$");
			specialCharacter.Add("&#37;", "%");
			specialCharacter.Add("&#38;", "&");
			specialCharacter.Add("&#39;", "'");
			specialCharacter.Add("&#40;", "(");
			specialCharacter.Add("&#41;", ")");
			specialCharacter.Add("&#42;", "*");
			specialCharacter.Add("&#43;", "+");
			specialCharacter.Add("&#44;", ",");
			specialCharacter.Add("&#45;", "-");
			specialCharacter.Add("&#46;", ".");
			specialCharacter.Add("&#47;", "/");
			specialCharacter.Add("&#58;", ":");
			specialCharacter.Add("&#59;", ";");
			specialCharacter.Add("&#60;", "<");
			specialCharacter.Add("&#61;", "=");
			specialCharacter.Add("&#62;", ">");
			specialCharacter.Add("&#63;", "?");
			specialCharacter.Add("&#64;", "@");
			specialCharacter.Add("&#91;", "[");
			specialCharacter.Add("&#92;", "\\");
			specialCharacter.Add("&#93;", "]");
			specialCharacter.Add("&#94;", "^");
			specialCharacter.Add("&#95;", "_");
			specialCharacter.Add("&#96;", "`");
			specialCharacter.Add("&#123;", "{");
			specialCharacter.Add("&#124;", "|");
			specialCharacter.Add("&#125;", "}");
			specialCharacter.Add("&#126;", "~");
			specialCharacter.Add("&#161;", "¡");
			specialCharacter.Add("&#162;", "¢");
			specialCharacter.Add("&#163;", "£");
			specialCharacter.Add("&#164;", "¤");
			specialCharacter.Add("&#165;", "¥");
			specialCharacter.Add("&#166;", "¦");
			specialCharacter.Add("&#167;", "§");
			specialCharacter.Add("&#168;", "\u00a8");
			specialCharacter.Add("&#169;", "©");
			specialCharacter.Add("&#170;", "ª");
			specialCharacter.Add("&#171;", "«");
			specialCharacter.Add("&#172;", "¬");
			specialCharacter.Add("&#173;", " ");
			specialCharacter.Add("&#174", "®");
			specialCharacter.Add("&#175;", "\u00af");
			specialCharacter.Add("&#176;", "°");
			specialCharacter.Add("&#177;", "±");
			specialCharacter.Add("&#178;", "²");
			specialCharacter.Add("&#179;", "³");
			specialCharacter.Add("&#180;", "`");
			specialCharacter.Add("&#181;", "µ");
			specialCharacter.Add("&#182;", "¶");
			specialCharacter.Add("&#183;", "·");
			specialCharacter.Add("&#184;", "\u00b8");
			specialCharacter.Add("&#185;", "¹");
			specialCharacter.Add("&#186;", "º");
			specialCharacter.Add("&#187;", "»");
			specialCharacter.Add("&#188;", "¼");
			specialCharacter.Add("&#189;", "½");
			specialCharacter.Add("&#190;", "¾");
			specialCharacter.Add("&#191;", "¿");
			return specialCharacter;
		}

		internal void EatWhiteSpace()
		{
			while (!Eof() && IsWhiteSpace(GetCurrentChar()))
			{
				m_idx++;
			}
		}

		internal void RemoveTag(string tag)
		{
			int num = m_noTagSource.IndexOf("<" + tag);
			while (num >= 0)
			{
				int num2 = m_noTagSource.IndexOf("/" + tag, num + tag.Length + 1);
				if (num2 > 0 && num2 > num)
				{
					num2 += ("/" + tag + ">").Length;
					m_noTagSource = m_noTagSource.Remove(num, num2 - num);
					num = m_noTagSource.IndexOf("<" + tag);
				}
				else
				{
					num = m_noTagSource.IndexOf("<" + tag, num2);
				}
			}
		}

		internal bool Eof()
		{
			return m_idx >= m_source.Length;
		}

		internal void ParseAttributeName()
		{
			EatWhiteSpace();
			while (!Eof() && !IsWhiteSpace(GetCurrentChar()) && GetCurrentChar() != '=' && GetCurrentChar() != '>')
			{
				m_parseName += GetCurrentChar().ToString();
				m_idx++;
			}
			EatWhiteSpace();
		}

		internal void ParseAttributeValue()
		{
			if (m_parseDelim != 0 || GetCurrentChar() != '=')
			{
				return;
			}
			m_idx++;
			EatWhiteSpace();
			if (GetCurrentChar() == '\'' || GetCurrentChar() == '"')
			{
				m_parseDelim = GetCurrentChar();
				m_idx++;
				while (GetCurrentChar() != m_parseDelim)
				{
					m_parseValue += GetCurrentChar().ToString();
					m_idx++;
				}
				m_idx++;
			}
			else
			{
				while (!Eof() && !IsWhiteSpace(GetCurrentChar()) && GetCurrentChar() != '>')
				{
					m_parseValue += GetCurrentChar().ToString();
					m_idx++;
				}
			}
			EatWhiteSpace();
		}

		internal void AddAttribute()
		{
			Attribute a = new Attribute(m_parseName, m_parseValue, m_parseDelim);
			Add(a);
		}

		internal char GetCurrentChar()
		{
			return GetCurrentChar(0);
		}

		internal char GetCurrentChar(int peek)
		{
			if (m_idx + peek < m_source.Length)
			{
				return m_source[m_idx + peek];
			}
			return '\0';
		}

		internal char AdvanceCurrentChar()
		{
			return m_source[m_idx++];
		}

		internal void Advance()
		{
			m_idx++;
		}
	}
}
