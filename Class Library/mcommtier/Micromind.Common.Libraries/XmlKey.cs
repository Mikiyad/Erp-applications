
using System.Xml;

namespace Micromind.Common.Libraries
{
	public class XmlKey
	{
		private XmlElement Elem;

		private XmlManager Reg;

		public string Name => Elem.Name;

		public XmlKey this[string path, bool createpath] => GetSubKey(path, createpath);

		public void SetValue(string name, object val)
		{
			XmlAttribute xmlAttribute = Elem.Attributes[name];
			if (xmlAttribute == null)
			{
				Elem.Attributes.Append(xmlAttribute = Elem.OwnerDocument.CreateAttribute(name));
			}
			xmlAttribute.Value = val.ToString();
		}

		public bool GetBooleanValue(string name, bool defaultval)
		{
			XmlAttribute xmlAttribute = Elem.Attributes[name];
			if (xmlAttribute == null)
			{
				return defaultval;
			}
			string a = xmlAttribute.Value.ToUpper();
			if (!(a == "YES"))
			{
				return a == "TRUE";
			}
			return true;
		}

		public object GetValue(string name, object defaultval)
		{
			XmlAttribute xmlAttribute = Elem.Attributes[name];
			if (xmlAttribute == null)
			{
				return defaultval;
			}
			return xmlAttribute.Value;
		}

		public string GetStringValue(string name, string defaultval)
		{
			XmlAttribute xmlAttribute = Elem.Attributes[name];
			if (xmlAttribute == null)
			{
				return defaultval;
			}
			return xmlAttribute.Value;
		}

		public int GetIntValue(string name, int defaultval)
		{
			XmlAttribute xmlAttribute = Elem.Attributes[name];
			if (xmlAttribute == null)
			{
				return defaultval;
			}
			return int.Parse(xmlAttribute.Value);
		}

		public XmlKey[] GetSubKeys()
		{
			int count = Elem.ChildNodes.Count;
			if (count == 0)
			{
				return null;
			}
			XmlKey[] array = new XmlKey[count];
			for (int i = 0; i < count; i = checked(i + 1))
			{
				array[i] = new XmlKey((XmlElement)Elem.ChildNodes[i], Reg);
			}
			return array;
		}

		public XmlKey GetSubKey(string path, bool createpath)
		{
			XmlElement xmlElement = Elem;
			XmlElement xmlElement2 = null;
			checked
			{
				int num;
				for (int i = 0; i < path.Length; i += num + 1)
				{
					num = path.IndexOf('/', i);
					if (num == -1)
					{
						num = path.Length;
					}
					num -= i;
					string name = path.Substring(i, num);
					xmlElement2 = xmlElement;
					xmlElement = xmlElement[name];
					if (xmlElement == null)
					{
						if (!createpath)
						{
							return null;
						}
						xmlElement2.AppendChild(xmlElement = Elem.OwnerDocument.CreateElement(name));
					}
				}
				return new XmlKey(xmlElement, Reg);
			}
		}

		public XmlKey(XmlElement e, XmlManager r)
		{
			Elem = e;
			Reg = r;
		}
	}
}
