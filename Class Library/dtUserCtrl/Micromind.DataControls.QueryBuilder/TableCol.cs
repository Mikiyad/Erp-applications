using System;
using System.Collections;
using System.Data;
using System.Xml;

namespace Micromind.DataControls.QueryBuilder
{
	public class TableCol
	{
		public class TableKeys
		{
			public struct kConstraint
			{
				public string s_Table;

				public string s_Columns;
			}

			private TableCol mi_Column;

			private string ms_Type;

			private Hashtable mi_Keys;

			public ArrayList Keys => new ArrayList(mi_Keys.Keys);

			public kConstraint this[string s_Key] => (kConstraint)mi_Keys[s_Key];

			public string Display
			{
				get
				{
					string text = "";
					ArrayList arrayList = new ArrayList(mi_Keys.Keys);
					arrayList.Sort();
					foreach (string item in arrayList)
					{
						kConstraint kConstraint = (kConstraint)mi_Keys[item];
						if (text.Length > 0)
						{
							text += "\r\n";
						}
						text = text + kConstraint.s_Table + " (";
						bool flag = true;
						string[] array = kConstraint.s_Columns.Split(',');
						foreach (string str in array)
						{
							if (!flag)
							{
								text += ", ";
							}
							flag = false;
							text += str;
						}
						text += ")";
					}
					return text;
				}
			}

			public TableKeys(TableCol i_Column, string s_Type)
			{
				mi_Column = i_Column;
				ms_Type = s_Type;
				mi_Keys = new Hashtable();
			}

			public void AddKey(string s_Name, string s_Table, string s_Columns)
			{
				kConstraint kConstraint = default(kConstraint);
				kConstraint.s_Table = s_Table;
				kConstraint.s_Columns = s_Columns.TrimEnd(',');
				mi_Keys[s_Name] = kConstraint;
			}

			public void Serialize(XmlNode x_Node)
			{
				if (mi_Keys.Count != 0)
				{
					XmlNode i_Node = XML.CreateSubNode(x_Node, ms_Type);
					int num = 0;
					foreach (string key in mi_Keys.Keys)
					{
						XmlNode i_Node2 = XML.CreateSubNode(i_Node, "Constraint_" + num++);
						kConstraint kConstraint = (kConstraint)mi_Keys[key];
						XML.WriteSubNode(i_Node2, "Name", key);
						XML.WriteSubNode(i_Node2, "Table", kConstraint.s_Table);
						XML.WriteSubNode(i_Node2, "Columns", kConstraint.s_Columns);
					}
				}
			}

			public void Deserialize(XmlNode x_Node)
			{
				mi_Keys.Clear();
				XmlNode xmlNode = XML.FindSubNode(x_Node, ms_Type);
				if (xmlNode == null)
				{
					return;
				}
				int num = 0;
				while (true)
				{
					XmlNode xmlNode2 = XML.FindSubNode(xmlNode, "Constraint_" + num);
					if (xmlNode2 != null)
					{
						kConstraint kConstraint = default(kConstraint);
						kConstraint.s_Table = XML.ReadSubNodeStr(xmlNode2, "Table", "");
						kConstraint.s_Columns = XML.ReadSubNodeStr(xmlNode2, "Columns", "");
						string key = XML.ReadSubNodeStr(xmlNode2, "Name", "");
						mi_Keys[key] = kConstraint;
						num++;
						continue;
					}
					break;
				}
			}
		}

		public class Index
		{
			public string s_Name;

			public bool b_Unique;

			public bool b_Primary;

			public ArrayList i_Columns = new ArrayList();

			public Index(string sName, bool bUnique, bool bPrimary)
			{
				s_Name = sName;
				b_Unique = bUnique;
				b_Primary = bPrimary;
			}

			public void AddColumn(TableCol i_Col)
			{
				if (!i_Columns.Contains(i_Col))
				{
					i_Columns.Add(i_Col);
				}
			}
		}

		public class ColIndexes
		{
			private ArrayList mi_Indexes = new ArrayList();

			public bool Primary => GetByType(b_Unique: false, b_Primary: true).Count > 0;

			public bool Unique => GetByType(b_Unique: true, b_Primary: false).Count > 0;

			public ArrayListEx Names
			{
				get
				{
					ArrayListEx arrayListEx = new ArrayListEx();
					foreach (Index mi_Index in mi_Indexes)
					{
						arrayListEx.Add(mi_Index.s_Name);
					}
					arrayListEx.Sort();
					return arrayListEx;
				}
			}

			public string Display => Names.ToList("\r\n");

			public void AddIndex(Index i_Index)
			{
				if (!mi_Indexes.Contains(i_Index))
				{
					mi_Indexes.Add(i_Index);
				}
			}

			public ArrayList GetByType(bool b_Unique, bool b_Primary)
			{
				ArrayList arrayList = new ArrayList();
				foreach (Index mi_Index in mi_Indexes)
				{
					if ((b_Primary && mi_Index.b_Primary) || (b_Unique && mi_Index.b_Unique))
					{
						arrayList.Add(mi_Index);
					}
				}
				return arrayList;
			}

			public void Serialize(XmlNode x_Node)
			{
				ArrayListEx arrayListEx = new ArrayListEx();
				ArrayListEx arrayListEx2 = new ArrayListEx();
				foreach (Index mi_Index in mi_Indexes)
				{
					if (mi_Index.b_Primary)
					{
						arrayListEx.Add(mi_Index.s_Name);
					}
					if (mi_Index.b_Unique)
					{
						arrayListEx2.Add(mi_Index.s_Name);
					}
				}
				if (arrayListEx.Count > 0)
				{
					XML.WriteSubNode(x_Node, "Primary", arrayListEx.ToList(","));
				}
				if (arrayListEx2.Count > 0)
				{
					XML.WriteSubNode(x_Node, "Unique", arrayListEx2.ToList(","));
				}
			}

			public void Deserialize(XmlNode x_Node)
			{
				throw new Exception("Deserialization not possible here. Deserialization takes place in TableDef!");
			}
		}

		public int s32_ColIndex = -1;

		public string s_ColName;

		public string s_BaseType;

		public int s32_Bytes;

		public int s32_Precision;

		public int s32_Scale;

		public string s_Default;

		public string s_DefConstr;

		public bool b_Nullable;

		public bool b_Identity;

		public int s32_Seed;

		public int s32_Increment;

		public Type t_ManagedType = typeof(DBNull);

		public TableKeys i_FkOut;

		public TableKeys i_FkIn;

		public ColIndexes i_Indexes;

		public bool b_Primary;

		public bool b_Unique;

		public string s_FullType;

		private TableDef i_Table;

		private string s_Table;

		public TableCol(string sTable)
		{
			s_Table = sTable;
		}

		public TableCol(TableDef iTable)
		{
			i_Table = iTable;
			s_Table = iTable.Name;
			i_FkOut = new TableKeys(this, "ForeignKeyTo");
			i_FkIn = new TableKeys(this, "ReferencedBy");
			i_Indexes = new ColIndexes();
		}

		public void InsertData(DataRow i_Row, int s32_Column)
		{
			if (i_Row["FkName"] != DBNull.Value)
			{
				i_FkOut.AddKey((string)i_Row["FkName"], (string)i_Row["FkTable"], (string)i_Row["FkCols"]);
			}
			if (i_Row["RefName"] != DBNull.Value)
			{
				i_FkIn.AddKey((string)i_Row["RefName"], (string)i_Row["RefTable"], (string)i_Row["RefCols"]);
			}
			int num = Functions.ToInt(i_Row["IndexStatus"]);
			if ((num & 0x20) == 0)
			{
				bool flag = (num & 0x1000) > 0;
				bool flag2 = (num & 0x800) > 0;
				i_Table.AddIndex(Functions.ToStr(i_Row["IndexName"]), flag, flag2, this);
				b_Primary |= flag2;
				b_Unique |= flag;
			}
			if (s32_ColIndex < 0)
			{
				s32_ColIndex = s32_Column;
				s_ColName = Functions.ToStr(i_Row["ColName"]);
				s_BaseType = Functions.ToStr(i_Row["BaseType"]).ToLower();
				s32_Bytes = Functions.ToInt(i_Row["DataLen"]);
				s32_Precision = Functions.ToInt(i_Row["PrecVal"]);
				s32_Scale = Functions.ToInt(i_Row["ScaleVal"]);
				s_Default = Functions.ToStr(i_Row["DefaultVal"]);
				s_DefConstr = Functions.ToStr(i_Row["DefConstr"]);
				b_Nullable = ((Functions.ToInt(i_Row["ColStatus"]) & 8) > 0);
				b_Identity = ((Functions.ToInt(i_Row["ColStatus"]) & 0x80) > 0);
				if (b_Identity)
				{
					s32_Seed = Functions.ToInt(i_Row["Seed"]);
					s32_Increment = Functions.ToInt(i_Row["Increment"]);
				}
				s_FullType = GetFullType();
			}
		}

		private string GetFullType()
		{
			switch (s_BaseType)
			{
			case "char":
			case "binary":
			case "varchar":
			case "varbinary":
				return $"{s_BaseType} ({s32_Bytes})";
			case "nchar":
			case "nvarchar":
				return $"{s_BaseType} ({s32_Bytes / 2})";
			case "decimal":
			case "numeric":
				return $"{s_BaseType} ({s32_Precision},{s32_Scale})";
			default:
				return s_BaseType;
			}
		}

		public string BuildCreateCommand(bool b_New)
		{
			string str = $"[{s_ColName}] {s_FullType}";
			if (b_New)
			{
				if (b_Identity)
				{
					str += $" IDENTITY({s32_Seed}, {s32_Increment})";
				}
				if (b_Primary)
				{
					str += " PRIMARY KEY";
				}
				if (b_Unique)
				{
					str += " UNIQUE";
				}
				if (s_Default != "")
				{
					str += $" DEFAULT {s_Default}";
				}
			}
			if (b_Nullable)
			{
				return str + " NULL";
			}
			return str + " NOT NULL";
		}

		public string BuildAddColumnCommand()
		{
			return $"IF col_length('{s_Table}', '{s_ColName}') IS NULL\n" + $"ALTER TABLE [{s_Table}] ADD {BuildCreateCommand(b_New: true)}" + "\nELSE\n" + $"ALTER TABLE [{s_Table}] ALTER COLUMN {BuildCreateCommand(b_New: false)}";
		}

		public void Serialize(XmlNode x_Node)
		{
			XML.WriteSubNode(x_Node, "BaseType", s_BaseType);
			XML.WriteSubNode(x_Node, "ColName", s_ColName);
			XML.WriteSubNode(x_Node, "Bytes", s32_Bytes);
			XML.WriteSubNode(x_Node, "Default", s_Default);
			XML.WriteSubNode(x_Node, "DefConstraint", s_DefConstr);
			XML.WriteSubNode(x_Node, "Identity", b_Identity);
			XML.WriteSubNode(x_Node, "Increment", s32_Increment);
			XML.WriteSubNode(x_Node, "Nullable", b_Nullable);
			XML.WriteSubNode(x_Node, "Precision", s32_Precision);
			XML.WriteSubNode(x_Node, "Scale", s32_Scale);
			XML.WriteSubNode(x_Node, "Seed", s32_Seed);
			XML.WriteSubNode(x_Node, "ManagedType", t_ManagedType);
			i_Indexes.Serialize(x_Node);
			i_FkOut.Serialize(x_Node);
			i_FkIn.Serialize(x_Node);
		}

		public bool Deserialize(XmlNode x_Node)
		{
			try
			{
				s_BaseType = XML.ReadSubNodeStr(x_Node, "BaseType", "");
				s_ColName = XML.ReadSubNodeStr(x_Node, "ColName", "");
				s32_Bytes = XML.ReadSubNodeInt(x_Node, "Bytes", 0);
				s_Default = XML.ReadSubNodeStr(x_Node, "Default", "");
				s_DefConstr = XML.ReadSubNodeStr(x_Node, "DefConstraint", "");
				b_Identity = XML.ReadSubNodeBool(x_Node, "Identity", b_Default: false);
				s32_Increment = XML.ReadSubNodeInt(x_Node, "Increment", 0);
				b_Nullable = XML.ReadSubNodeBool(x_Node, "Nullable", b_Default: false);
				s32_Precision = XML.ReadSubNodeInt(x_Node, "Precision", 0);
				s32_Scale = XML.ReadSubNodeInt(x_Node, "Scale", 0);
				s32_Seed = XML.ReadSubNodeInt(x_Node, "Seed", 0);
				t_ManagedType = Type.GetType(XML.ReadSubNodeStr(x_Node, "ManagedType", ""), throwOnError: true);
				i_FkOut.Deserialize(x_Node);
				i_FkIn.Deserialize(x_Node);
				string[] array = Functions.SplitEx(XML.ReadSubNodeStr(x_Node, "Primary", ""), ',');
				foreach (string s_Name in array)
				{
					i_Table.AddIndex(s_Name, b_Unique: false, b_Primary: true, this);
				}
				array = Functions.SplitEx(XML.ReadSubNodeStr(x_Node, "Unique", ""), ',');
				foreach (string s_Name2 in array)
				{
					i_Table.AddIndex(s_Name2, b_Unique: true, b_Primary: false, this);
				}
				s_FullType = GetFullType();
				b_Primary = i_Indexes.Primary;
				b_Unique = i_Indexes.Unique;
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
