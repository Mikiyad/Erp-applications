
using System.Xml;

namespace Micromind.ClientLibraries
{
	
	public class XmlRegistryKey
	{
		private XmlElement Elem;

		private XmlRegistry Reg;

		public string Name => Elem.Name;

		public XmlRegistryKey this[string path, bool createpath] => GetSubKey(path, createpath);

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

		public XmlRegistryKey[] GetSubKeys()
		{
			int count = Elem.ChildNodes.Count;
			if (count == 0)
			{
				return null;
			}
			XmlRegistryKey[] array = new XmlRegistryKey[count];
			for (int i = 0; i < count; i = checked(i + 1))
			{
				array[i] = new XmlRegistryKey((XmlElement)Elem.ChildNodes[i], Reg);
			}
			return array;
		}

		public XmlRegistryKey GetSubKey(string path, bool createpath)
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
				return new XmlRegistryKey(xmlElement, Reg);
			}
		}

		public XmlRegistryKey(XmlElement e, XmlRegistry r)
		{
			Elem = e;
			Reg = r;
		}
	}
}
