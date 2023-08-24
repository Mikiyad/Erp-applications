using System.Collections;
using System.Text;

namespace Micromind.Utilities.HTTP
{
	internal class ParseHTML : Parse
	{
		internal AttributeList GetTag()
		{
			AttributeList attributeList = new AttributeList();
			attributeList.Name = m_tag;
			foreach (Attribute item in base.List)
			{
				attributeList.Add((Attribute)item.Clone());
			}
			return attributeList;
		}

		internal string BuildTag()
		{
			string str = "<";
			str += m_tag;
			for (int i = 0; base[i] != null; i++)
			{
				str += " ";
				if (base[i].Value == null)
				{
					if (base[i].Delim != 0)
					{
						str += base[i].Delim.ToString();
					}
					str += base[i].Name;
					if (base[i].Delim != 0)
					{
						str += base[i].Delim.ToString();
					}
					continue;
				}
				str += base[i].Name;
				if (base[i].Value != null)
				{
					str += "=";
					if (base[i].Delim != 0)
					{
						str += base[i].Delim.ToString();
					}
					str += base[i].Value;
					if (base[i].Delim != 0)
					{
						str += base[i].Delim.ToString();
					}
				}
			}
			return str + ">";
		}

		protected void ParseTag()
		{
			m_tag = "";
			Clear();
			if (GetCurrentChar() == '!' && GetCurrentChar(1) == '-' && GetCurrentChar(2) == '-')
			{
				while (!Eof() && (GetCurrentChar() != '-' || GetCurrentChar(1) != '-' || GetCurrentChar(2) != '>'))
				{
					if (GetCurrentChar() != '\r')
					{
						m_tag += GetCurrentChar().ToString();
					}
					Advance();
				}
				m_tag += "--";
				Advance();
				Advance();
				Advance();
				base.ParseDelim = '\0';
				return;
			}
			while (!Eof() && !Micromind.Utilities.HTTP.Parse.IsWhiteSpace(GetCurrentChar()) && GetCurrentChar() != '>')
			{
				m_tag += GetCurrentChar().ToString();
				Advance();
			}
			EatWhiteSpace();
			while (GetCurrentChar() != '>')
			{
				base.ParseName = "";
				base.ParseValue = "";
				base.ParseDelim = '\0';
				ParseAttributeName();
				if (GetCurrentChar() == '>')
				{
					AddAttribute();
					break;
				}
				ParseAttributeValue();
				AddAttribute();
			}
			Advance();
		}

		internal string GetMeta(string metaName)
		{
			if (hasWhiteSpace)
			{
				EatWhiteSpace();
			}
			metaName = metaName.ToLower();
			string text = m_source.ToLower();
			int num = 0;
			int num2 = 0;
			while (true)
			{
				num = text.IndexOf("<meta", num2 + 1);
				if (num < 0)
				{
					return "";
				}
				if (num >= 0)
				{
					num2 = text.IndexOf(">", num);
					if (num2 < 0)
					{
						break;
					}
					if (num2 > 0)
					{
						try
						{
							if (text.IndexOf("name=\"" + metaName + "\"", num + 1, num2 - num) > 0)
							{
								int num3 = text.IndexOf("content=\"", num, num2 - num);
								if (num3 > 0)
								{
									num3 += "content=\"".Length;
									int num4 = text.IndexOf("\"", num3, num2 - num3);
									return text.Substring(num3, num4 - num3);
								}
							}
						}
						catch
						{
						}
					}
					num = text.IndexOf("<meta", num2 + 1);
				}
			}
			return "";
		}

		internal string GetContent()
		{
			if (hasWhiteSpace)
			{
				EatWhiteSpace();
			}
			RemoveTag("style");
			RemoveTag("script");
			string noTagSource = m_noTagSource;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (true)
			{
				num = noTagSource.IndexOf(">", num2 + 1);
				if (num < 0)
				{
					return stringBuilder.ToString();
				}
				if (num >= 0)
				{
					num2 = noTagSource.IndexOf("<", num + 1);
					if (num2 < 0)
					{
						break;
					}
					int num3 = noTagSource.IndexOf(">", num + 1);
					if (num3 < num2)
					{
						num = num3;
					}
					else if (num2 > 0)
					{
						try
						{
							string text = noTagSource.Substring(num + 1, num2 - num - 1);
							if (text.Trim().Length > 0)
							{
								text = RemoveCommonMark(text);
								stringBuilder.Append(text).Append(" ");
							}
						}
						catch
						{
						}
					}
					num = noTagSource.IndexOf(">", num2 + 1);
				}
			}
			return stringBuilder.ToString();
		}

		private string RemoveCommonMark(string str)
		{
			Hashtable specialCharacters = GetSpecialCharacters();
			foreach (object key in specialCharacters.Keys)
			{
				for (int num = str.IndexOf(key.ToString()); num >= 0; num = str.IndexOf(key.ToString()))
				{
					str = str.Remove(num, key.ToString().Length + 1);
					string value = specialCharacters[key].ToString();
					str = str.Insert(num, value);
				}
			}
			return str;
		}

		internal char Parse()
		{
			if (GetCurrentChar() == '<')
			{
				Advance();
				char c = char.ToUpper(GetCurrentChar());
				if (char.IsLetterOrDigit(c) || c == '!' || c == '/')
				{
					ParseTag();
					return '\0';
				}
				return AdvanceCurrentChar();
			}
			return AdvanceCurrentChar();
		}
	}
}
