using System;

namespace Micromind.Utilities.HTTP
{
	public class Attribute : ICloneable
	{
		private string m_name;

		private string m_value;

		private char m_delim;

		public char Delim
		{
			get
			{
				return m_delim;
			}
			set
			{
				m_delim = value;
			}
		}

		public string Name
		{
			get
			{
				return m_name;
			}
			set
			{
				m_name = value;
			}
		}

		public string Value
		{
			get
			{
				return m_value;
			}
			set
			{
				m_value = value;
			}
		}

		public Attribute(string name, string value, char delim)
		{
			m_name = name;
			m_value = value;
			m_delim = delim;
		}

		public Attribute()
			: this("", "", '\0')
		{
		}

		public Attribute(string name, string value)
			: this(name, value, '\0')
		{
		}

		public virtual object Clone()
		{
			return new Attribute(m_name, m_value, m_delim);
		}
	}
}
