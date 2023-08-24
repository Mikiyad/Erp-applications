using System.Collections;
using System.Data;
using System.Xml;

namespace Micromind.DataControls.QueryBuilder
{
	public class TableDef
	{
		private string ms_Name;

		private ArrayList mi_Columns = new ArrayList();

		private ArrayList mi_Indexes = new ArrayList();

		public string Name => ms_Name;

		public ArrayList Columns => mi_Columns;

		public void AddIndex(string s_Name, bool b_Unique, bool b_Primary, TableCol i_Col)
		{
			if (s_Name.Length != 0)
			{
				TableCol.Index index = null;
				foreach (TableCol.Index mi_Index in mi_Indexes)
				{
					if (mi_Index.s_Name == s_Name)
					{
						index = mi_Index;
						break;
					}
				}
				if (index == null)
				{
					index = new TableCol.Index(s_Name, b_Unique, b_Primary);
					mi_Indexes.Add(index);
				}
				index.AddColumn(i_Col);
				i_Col.i_Indexes.AddIndex(index);
			}
		}

		public string BuildCreateCommand()
		{
			string str = $"IF objectproperty(object_id('{ms_Name}'), 'IsTable') != 1\n" + $"CREATE TABLE [{ms_Name}]\n(\n";
			for (int i = 0; i < mi_Columns.Count; i++)
			{
				TableCol tableCol = (TableCol)mi_Columns[i];
				if (i > 0)
				{
					str += ",\n";
				}
				str += tableCol.BuildCreateCommand(b_New: true);
			}
			return str + "\n)";
		}

		private void AppendColumn(DataRow i_Row)
		{
			ms_Name = Functions.ToStr(i_Row["TblName"]);
			string a = (string)i_Row["ColName"];
			if (mi_Columns.Count > 0)
			{
				TableCol tableCol = (TableCol)mi_Columns[mi_Columns.Count - 1];
				if (a == tableCol.s_ColName)
				{
					tableCol.InsertData(i_Row, -1);
					return;
				}
			}
			TableCol tableCol2 = new TableCol(this);
			tableCol2.InsertData(i_Row, mi_Columns.Count);
			mi_Columns.Add(tableCol2);
		}

		public static TableDef FromDataTable(DataTable i_DataTable)
		{
			TableDef tableDef = new TableDef();
			foreach (DataRow row in i_DataTable.Rows)
			{
				tableDef.AppendColumn(row);
			}
			return tableDef;
		}

		public static Hashtable TableFactory(DataTable i_DataTable)
		{
			Hashtable hashtable = new Hashtable();
			TableDef tableDef = new TableDef();
			for (int i = 0; i < i_DataTable.Rows.Count; i++)
			{
				DataRow dataRow = i_DataTable.Rows[i];
				string b = Functions.ToStr(dataRow["TblName"]);
				if (i == i_DataTable.Rows.Count - 1 || tableDef.Name != b)
				{
					if (tableDef.Columns.Count > 0)
					{
						hashtable[tableDef.Name] = tableDef;
					}
					tableDef = new TableDef();
				}
				tableDef.AppendColumn(dataRow);
			}
			return hashtable;
		}

		public int FindColumnByTypeAndName(TableCol i_Col)
		{
			foreach (TableCol mi_Column in mi_Columns)
			{
				if (mi_Column.t_ManagedType == i_Col.t_ManagedType && string.Compare(mi_Column.s_ColName, i_Col.s_ColName, ignoreCase: true) == 0)
				{
					return mi_Column.s32_ColIndex;
				}
			}
			return -1;
		}

		public Hashtable FindMatchingColumns(ArrayList i_External)
		{
			Hashtable hashtable = new Hashtable();
			foreach (TableCol item in i_External)
			{
				int num = FindColumnByTypeAndName(item);
				if (num >= 0)
				{
					hashtable[num] = item.s32_ColIndex;
				}
			}
			return hashtable;
		}

		public void Serialize(XmlNode x_Node)
		{
			XML.WriteSubNode(x_Node, "Name", ms_Name);
			foreach (TableCol mi_Column in mi_Columns)
			{
				XmlNode x_Node2 = XML.CreateSubNode(x_Node, "Column_" + mi_Column.s32_ColIndex);
				mi_Column.Serialize(x_Node2);
			}
		}

		public static TableDef Deserialize(XmlNode x_Node)
		{
			TableDef tableDef = new TableDef();
			if (x_Node == null)
			{
				return null;
			}
			tableDef.ms_Name = XML.ReadSubNodeStr(x_Node, "Name", "");
			int num = 0;
			while (true)
			{
				XmlNode xmlNode = x_Node.SelectSingleNode("Column_" + num);
				if (xmlNode == null)
				{
					break;
				}
				TableCol tableCol = new TableCol(tableDef);
				if (!tableCol.Deserialize(xmlNode))
				{
					return null;
				}
				tableCol.s32_ColIndex = num;
				tableDef.mi_Columns.Add(tableCol);
				num++;
			}
			return tableDef;
		}
	}
}
