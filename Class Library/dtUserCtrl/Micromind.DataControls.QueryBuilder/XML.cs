using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.DataControls.QueryBuilder
{
	public class XML
	{
		public static string Read(Form frm, string s_XmlFile, string s_Entry, string s_Default)
		{
			return ReadSubNodeStr(ReadXmlFile(frm, s_XmlFile, "Settings"), s_Entry, s_Default);
		}

		public static int Read(Form frm, string s_XmlFile, string s_Entry, int s32_Default)
		{
			string s = Read(frm, s_XmlFile, s_Entry, s32_Default.ToString());
			try
			{
				return int.Parse(s);
			}
			catch
			{
				return s32_Default;
			}
		}

		public static bool Read(Form frm, string s_XmlFile, string s_Entry, bool b_Default)
		{
			return Read(frm, s_XmlFile, s_Entry, b_Default.ToString()).ToUpper() == "TRUE";
		}

		public static bool Write(Form frm, string s_XmlFile, string s_Entry, object o_Value)
		{
			XmlNode xmlNode = ReadXmlFile(frm, s_XmlFile, "Settings");
			if (xmlNode == null)
			{
				return false;
			}
			WriteSubNode(xmlNode, s_Entry, Functions.DbValueSerialize(o_Value));
			return SaveXmlFile(frm, s_XmlFile, xmlNode, Encoding.UTF8);
		}

		public static XmlNode ReadXmlFile(Form i_Owner, string s_File, string s_RootNode)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = null;
			try
			{
				if (File.Exists(s_File))
				{
					xmlDocument.Load(s_File);
				}
				else
				{
					xmlDocument.LoadXml($"<{s_RootNode} />");
				}
				return xmlDocument.SelectSingleNode(s_RootNode);
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static bool SaveXmlFile(Form i_Owner, string s_File, XmlNode i_xRootNode, Encoding e_Enc)
		{
			Functions.RemoveWriteProtection(s_File);
			XmlTextWriter xmlTextWriter = null;
			try
			{
				xmlTextWriter = new XmlTextWriter(s_File, e_Enc);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.Indentation = 1;
				xmlTextWriter.IndentChar = '\t';
				i_xRootNode.OwnerDocument.Save(xmlTextWriter);
			}
			catch (Exception)
			{
				return false;
			}
			finally
			{
				xmlTextWriter?.Close();
			}
			return true;
		}

		public static XmlNode CreateSubNode(XmlNode i_Node, string s_Name)
		{
			if (i_Node == null || s_Name.Length == 0)
			{
				return null;
			}
			XmlNode xmlNode = FindSubNode(i_Node, s_Name);
			if (xmlNode != null)
			{
				return xmlNode;
			}
			XmlElement newChild = i_Node.OwnerDocument.CreateElement(s_Name);
			return i_Node.AppendChild(newChild);
		}

		public static XmlNode FindSubNode(XmlNode i_Node, string s_Name)
		{
			if (i_Node == null || s_Name.Length == 0)
			{
				return null;
			}
			return i_Node.SelectSingleNode(s_Name);
		}

		public static string ReadSubNodeStr(XmlNode i_Node, string s_Name, string s_Default)
		{
			XmlNode xmlNode = FindSubNode(i_Node, s_Name);
			if (xmlNode == null)
			{
				return s_Default;
			}
			return Functions.ToStr(xmlNode.InnerText);
		}

		public static int ReadSubNodeInt(XmlNode i_Node, string s_Name, int s32_Default)
		{
			XmlNode xmlNode = FindSubNode(i_Node, s_Name);
			if (xmlNode == null)
			{
				return s32_Default;
			}
			try
			{
				return int.Parse(xmlNode.InnerText);
			}
			catch
			{
				return s32_Default;
			}
		}

		public static bool ReadSubNodeBool(XmlNode i_Node, string s_Name, bool b_Default)
		{
			XmlNode xmlNode = FindSubNode(i_Node, s_Name);
			if (xmlNode == null)
			{
				return b_Default;
			}
			try
			{
				return bool.Parse(xmlNode.InnerText);
			}
			catch
			{
				return b_Default;
			}
		}

		public static void WriteSubNode(XmlNode i_Node, string s_Name, object o_Value)
		{
			CreateSubNode(i_Node, s_Name).InnerText = Functions.DbValueSerialize(o_Value);
		}

		public static XmlNode CreateSubNode(XmlNode i_Node, string s_Prop, string s_Name)
		{
			if (i_Node == null || s_Prop.Length == 0 || s_Name.Length == 0)
			{
				return null;
			}
			XmlNode xmlNode = FindSubNode(i_Node, s_Prop, s_Name);
			if (xmlNode != null)
			{
				return xmlNode;
			}
			XmlElement xmlElement = i_Node.OwnerDocument.CreateElement(s_Prop);
			if (s_Name.Length > 0)
			{
				xmlElement.SetAttribute("Name", s_Name);
			}
			return i_Node.AppendChild(xmlElement);
		}

		public static XmlNode FindSubNode(XmlNode i_Node, string s_Prop, string s_Name)
		{
			if (i_Node == null || s_Prop.Length == 0 || s_Name.Length == 0)
			{
				return null;
			}
			foreach (XmlNode item in i_Node)
			{
				if (string.Compare(item.Name, s_Prop, ignoreCase: true) == 0 && CompareAttribute(item, "Name", s_Name))
				{
					return item;
				}
			}
			return null;
		}

		public static string GetAttributeString(XmlNode i_Node, string s_AttrName)
		{
			if (i_Node == null)
			{
				return "";
			}
			XmlAttribute xmlAttribute = i_Node.Attributes[s_AttrName];
			if (xmlAttribute == null)
			{
				return "";
			}
			return xmlAttribute.Value;
		}

		public static int GetAttributeInt(XmlNode i_Node, string s_AttrName)
		{
			if (i_Node == null)
			{
				return 0;
			}
			XmlAttribute xmlAttribute = i_Node.Attributes[s_AttrName];
			if (xmlAttribute == null)
			{
				return 0;
			}
			return int.Parse(xmlAttribute.Value);
		}

		public static bool CompareAttribute(XmlNode i_Node, string s_AttrName, string s_Value)
		{
			if (i_Node == null)
			{
				return false;
			}
			string attributeString = GetAttributeString(i_Node, s_AttrName);
			if (attributeString.Length == 0)
			{
				return false;
			}
			return string.Compare(attributeString, s_Value, ignoreCase: true) == 0;
		}

		public static XmlNode CreateNodeOnPath(XmlDocument i_Doc, string s_Path)
		{
			string[] array = Functions.SplitEx(s_Path, '/');
			s_Path = "";
			XmlNode xmlNode = null;
			for (int i = 1; i < array.Length; i++)
			{
				s_Path = s_Path + "/" + array[i];
				XmlNode xmlNode2 = i_Doc.SelectSingleNode(s_Path);
				if (xmlNode2 == null)
				{
					xmlNode2 = CreateSubNode(xmlNode, array[i]);
				}
				xmlNode = xmlNode2;
			}
			return xmlNode;
		}

		public static string InnerText(XmlNode i_Node)
		{
			if (i_Node == null)
			{
				return "";
			}
			return i_Node.InnerText;
		}
	}
}
