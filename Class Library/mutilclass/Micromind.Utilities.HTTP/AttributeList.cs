using System.Collections;

namespace Micromind.Utilities.HTTP
{
	public class AttributeList : Attribute
	{
		protected ArrayList m_list;

		public int Count => m_list.Count;

		public ArrayList List => m_list;

		public Attribute this[int index]
		{
			get
			{
				if (index < m_list.Count)
				{
					return (Attribute)m_list[index];
				}
				return null;
			}
		}

		public Attribute this[string index]
		{
			get
			{
				for (int i = 0; this[i] != null; i++)
				{
					if (this[i].Name.ToLower().Equals(index.ToLower()))
					{
						return this[i];
					}
				}
				return null;
			}
		}

		public override object Clone()
		{
			AttributeList attributeList = new AttributeList();
			for (int i = 0; i < m_list.Count; i++)
			{
				attributeList.Add((Attribute)this[i].Clone());
			}
			return attributeList;
		}

		public AttributeList()
			: base("", "")
		{
			m_list = new ArrayList();
		}

		public void Add(Attribute a)
		{
			m_list.Add(a);
		}

		public void Clear()
		{
			m_list.Clear();
		}

		public bool IsEmpty()
		{
			return m_list.Count <= 0;
		}

		public void Set(string name, string value)
		{
			if (name != null)
			{
				if (value == null)
				{
					value = "";
				}
				Attribute attribute = this[name];
				if (attribute == null)
				{
					attribute = new Attribute(name, value);
					Add(attribute);
				}
				else
				{
					attribute.Value = value;
				}
			}
		}
	}
}
