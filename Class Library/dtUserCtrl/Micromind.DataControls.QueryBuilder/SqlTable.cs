using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder
{
	public class SqlTable
	{
		public enum eColCheck
		{
			Bad,
			Ident,
			Good
		}

		private int ms32_RowsOnServer;

		private string ms_Server;

		private string ms_DataBase;

		private string ms_User;

		private string ms_Password;

		private string ms_TableName;

		private DataTable mi_TableGrid;

		private DataTable mi_TableOrig;

		private Form mi_Owner;

		private TableDef mi_TableDef;

		private ArrayList mi_Commands = new ArrayList();

		public DataTable Table => mi_TableGrid;

		public TableDef TableDef => mi_TableDef;

		public int RowCountOnServer => ms32_RowsOnServer;

		public string ExecutedCommands
		{
			get
			{
				string text = "";
				foreach (string mi_Command in mi_Commands)
				{
					text = text + mi_Command + "\n";
				}
				return text;
			}
		}

		public eColCheck CheckColumns
		{
			get
			{
				eColCheck result = eColCheck.Bad;
				if (mi_TableDef != null)
				{
					foreach (TableCol column in mi_TableDef.Columns)
					{
						if (column.b_Primary || column.b_Unique)
						{
							result = eColCheck.Ident;
							if (!column.b_Identity)
							{
								return eColCheck.Good;
							}
						}
					}
					return result;
				}
				return result;
			}
		}

		public int DataChanged
		{
			get
			{
				int num = 0;
				foreach (DataRow row in mi_TableGrid.Rows)
				{
					if (row.RowState != DataRowState.Unchanged)
					{
						num |= (int)row.RowState;
					}
				}
				return num;
			}
		}

		public long MemoryUsage
		{
			get
			{
				long num = 0L;
				int count = mi_TableGrid.Rows.Count;
				foreach (TableCol column in mi_TableDef.Columns)
				{
					bool flag = column.s_BaseType.EndsWith("text");
					bool flag2 = mi_TableGrid.Columns[column.s32_ColIndex].DataType == typeof(byte[]);
					for (int i = 0; i < count; i++)
					{
						if (mi_TableGrid.Rows[i].RowState != DataRowState.Deleted)
						{
							object obj = mi_TableGrid.Rows[i][column.s32_ColIndex];
							num = ((!(obj is DBNull)) ? ((!flag) ? ((!flag2) ? (num + column.s32_Bytes) : (num + ((byte[])obj).Length)) : (num + ((string)obj).Length)) : (num + column.s32_Bytes));
						}
					}
				}
				return num;
			}
		}

		public void Dispose()
		{
			if (mi_TableGrid != null)
			{
				mi_TableGrid.Reset();
			}
			if (mi_TableOrig != null)
			{
				mi_TableOrig.Reset();
			}
		}

		public bool LoadTable(bool b_LoadData, bool b_LoadColumns, bool b_Top10, string s_Where)
		{
			if (b_LoadColumns)
			{
				SQL sQL = new SQL(mi_Owner, ms_Server, ms_DataBase, ms_User, ms_Password);
				mi_TableDef = sQL.LoadTableDefFromServer(ms_TableName);
			}
			if (mi_TableDef == null)
			{
				return false;
			}
			if (b_LoadData)
			{
				Dispose();
				if (mi_TableGrid != null)
				{
					mi_TableGrid.Reset();
				}
				mi_TableGrid = null;
				ms32_RowsOnServer = 0;
				s_Where = s_Where.Trim();
				if (s_Where.Length > 0 && !s_Where.ToUpper().StartsWith("WHERE"))
				{
					s_Where = "WHERE " + s_Where;
				}
				string text = b_Top10 ? "TOP 10" : "";
				string text2 = BuildOrderClause();
				string s_Sql = string.Format("SELECT {0} * FROM [{1}] {2} {3}\nSELECT count(*) FROM [{1}]", text, ms_TableName, s_Where, text2);
				DataSet dataSet = new SQL(mi_Owner, ms_Server, ms_DataBase, ms_User, ms_Password).ExecuteSQL(s_Sql, -1);
				if (dataSet != null)
				{
					mi_TableGrid = dataSet.Tables[0];
					mi_TableGrid.TableName = ms_TableName;
					ms32_RowsOnServer = (int)dataSet.Tables[1].Rows[0][0];
				}
			}
			if (mi_TableGrid == null)
			{
				return false;
			}
			if (mi_TableOrig != null)
			{
				mi_TableOrig.Reset();
			}
			mi_TableOrig = mi_TableGrid.Copy();
			WriteColumnManagedTypes();
			return true;
		}

		public void AttachTable(DataTable i_Table, TableDef i_TableDef)
		{
			if (mi_TableGrid != null)
			{
				mi_TableGrid.Reset();
			}
			if (mi_TableOrig != null)
			{
				mi_TableOrig.Reset();
			}
			mi_TableGrid = i_Table;
			mi_TableOrig = i_Table.Copy();
			mi_TableDef = i_TableDef;
			WriteColumnManagedTypes();
		}

		protected void WriteColumnManagedTypes()
		{
			foreach (TableCol column in mi_TableDef.Columns)
			{
				column.t_ManagedType = mi_TableGrid.Columns[column.s32_ColIndex].DataType;
			}
		}

		public void BuildExportCommands(ArrayList i_SqlCmd)
		{
			i_SqlCmd.Add(mi_TableDef.BuildCreateCommand());
			foreach (TableCol column in mi_TableDef.Columns)
			{
				i_SqlCmd.Add(column.BuildAddColumnCommand());
			}
			BuildUpdateTableCommands(i_SqlCmd, b_All: true);
			if (i_SqlCmd.Count > 0)
			{
				AddSpacer();
			}
			foreach (string item in i_SqlCmd)
			{
				mi_Commands.Add(item + "\nGO\n");
			}
		}

		public void BuildUpdateTableCommands(ArrayList i_SqlCmd, bool b_All)
		{
			if (mi_TableGrid == null)
			{
				return;
			}
			eColCheck checkColumns = CheckColumns;
			if (checkColumns == eColCheck.Bad)
			{
				return;
			}
			for (int i = 0; i < mi_TableGrid.Rows.Count; i++)
			{
				DataRow dataRow = mi_TableGrid.Rows[i];
				DataRow dataRow2 = dataRow;
				DataRowState dataRowState = dataRow.RowState;
				if (dataRowState == DataRowState.Unchanged)
				{
					if (!b_All)
					{
						continue;
					}
					dataRowState = DataRowState.Modified;
				}
				if (dataRowState != DataRowState.Added)
				{
					dataRow2 = mi_TableOrig.Rows[i];
				}
				if (dataRowState == DataRowState.Deleted)
				{
					dataRow = mi_TableOrig.Rows[i];
				}
				string text = "";
				string text2 = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				foreach (TableCol column in mi_TableDef.Columns)
				{
					object obj = dataRow[column.s_ColName];
					object obj2 = dataRow2[column.s_ColName];
					string text6 = Functions.DbValueForSql(obj);
					string arg = Functions.DbValueForSql(obj2);
					if (!(obj.GetType() == typeof(byte[])))
					{
						bool flag = false;
						if (column.b_Primary || column.b_Unique)
						{
							if (column.b_Identity && checkColumns == eColCheck.Good)
							{
								continue;
							}
							if (text.Length > 0)
							{
								text += " AND ";
							}
							if (text2.Length > 0)
							{
								text2 += " AND ";
							}
							text = ((obj2 != DBNull.Value) ? (text + $"[{column.s_ColName}] = {arg}") : (text + $"[{column.s_ColName}] IS NULL"));
							text2 = ((obj != DBNull.Value) ? (text2 + $"[{column.s_ColName}] = {text6}") : (text2 + $"[{column.s_ColName}] IS NULL"));
							if (!obj.Equals(obj2))
							{
								flag = true;
							}
						}
						else
						{
							flag = true;
						}
						if (!column.b_Identity)
						{
							if (flag)
							{
								if (text3.Length > 0)
								{
									text3 += ", ";
								}
								text3 += $"[{column.s_ColName}] = {text6}";
							}
							if (text5.Length > 0)
							{
								text5 += ", ";
								text4 += ", ";
							}
							text5 += $"[{column.s_ColName}]";
							text4 += text6;
						}
					}
				}
				if (dataRowState == DataRowState.Deleted && text.Length > 0)
				{
					i_SqlCmd.Add($"DELETE FROM [{ms_TableName}] WHERE {text}");
				}
				if (dataRowState != DataRowState.Added && dataRowState != DataRowState.Modified)
				{
					continue;
				}
				string text7 = "";
				if (text2.Length > 0)
				{
					text7 = $"IF NOT EXISTS (SELECT * FROM [{ms_TableName}] WHERE {text2})\n";
				}
				string text8 = "";
				if (text5.Length > 0 && text4.Length > 0)
				{
					text8 = $"INSERT INTO [{ms_TableName}]\n({text5})\nVALUES ({text4})";
				}
				string text9 = "";
				if (text3.Length > 0 && text.Length > 0)
				{
					text9 = $"UPDATE [{ms_TableName}]\nSET {text3}\nWHERE {text}";
				}
				if (text9.Length > 0)
				{
					i_SqlCmd.Add(text9);
				}
				if (text8.Length > 0)
				{
					string str = "";
					if (text7.Length > 0)
					{
						str += text7;
					}
					str += text8;
					i_SqlCmd.Add(str);
				}
			}
		}

		private void AddSpacer()
		{
			string value = string.Format("\n-------------------------------------- {0} - {1} --------------------------------------\n\n", Functions.FirstToUpper(Environment.UserName, b_IsFile: false), DateTime.Now.ToString("dd.MM.yyyy HH.mm"));
			mi_Commands.Add(value);
		}

		public bool CommitChanges(ArrayList i_SqlCmd)
		{
			if (i_SqlCmd.Count > 0)
			{
				AddSpacer();
			}
			mi_Owner.Cursor = Cursors.WaitCursor;
			bool flag = false;
			int num = 0;
			foreach (string item in i_SqlCmd)
			{
				if (flag)
				{
					mi_Commands.Add("/* NOT EXECUTED DUE TO PREVIOUS FAILURE:\n" + item + "\n*/\n");
				}
				else
				{
					SQL sQL = new SQL(mi_Owner, ms_Server, ms_DataBase, ms_User, ms_Password);
					if (sQL.ExecuteSQL(item, -1) == null)
					{
						mi_Commands.Add("/* EXECUTION FAILED:\n" + item + "\n*/\n");
						flag = true;
					}
					else
					{
						mi_Commands.Add(item + "\nGO\n");
						num++;
					}
				}
			}
			mi_TableGrid.AcceptChanges();
			mi_TableOrig.Reset();
			mi_TableOrig = mi_TableGrid.Copy();
			mi_Owner.Cursor = Cursors.Arrow;
			return !flag;
		}

		public bool DesignTable(ArrayList i_NewColumns)
		{
			if (mi_TableDef.Columns.Count != i_NewColumns.Count)
			{
				throw new Exception("Internal error");
			}
			ArrayListEx arrayListEx = new ArrayListEx();
			ArrayListEx arrayListEx2 = new ArrayListEx();
			ArrayListEx arrayListEx3 = new ArrayListEx();
			ArrayListEx arrayListEx4 = new ArrayListEx();
			ArrayListEx arrayListEx5 = new ArrayListEx();
			ArrayListEx arrayListEx6 = new ArrayListEx();
			bool flag = false;
			bool flag2 = false;
			string text = "";
			for (int i = 0; i < mi_TableDef.Columns.Count; i++)
			{
				TableCol tableCol = (TableCol)mi_TableDef.Columns[i];
				TableCol tableCol2 = (TableCol)i_NewColumns[i];
				if (tableCol.s_ColName != tableCol2.s_ColName)
				{
					throw new Exception("Internal error");
				}
				bool flag3 = false;
				if (tableCol.b_Nullable != tableCol2.b_Nullable || string.Compare(tableCol.s_FullType, tableCol2.s_FullType, ignoreCase: true) != 0)
				{
					string value = $"ALTER TABLE [{mi_TableDef.Name}] ALTER COLUMN {tableCol2.BuildCreateCommand(b_New: false)}";
					arrayListEx2.Add(value);
					flag3 = true;
				}
				if (tableCol.b_Unique && (!tableCol2.b_Unique | flag3))
				{
					foreach (TableCol.Index item in tableCol.i_Indexes.GetByType(b_Unique: true, b_Primary: false))
					{
						arrayListEx.AddOnce(BuildDropConstraint(item.s_Name, null));
					}
				}
				if (tableCol2.b_Unique && (!tableCol.b_Unique | flag3))
				{
					arrayListEx3.Add($"ALTER TABLE [{mi_TableDef.Name}] ADD UNIQUE ({tableCol2.s_ColName})");
				}
				if (tableCol.b_Primary != tableCol2.b_Primary)
				{
					flag = true;
				}
				if (tableCol.b_Primary)
				{
					if (flag3)
					{
						flag2 = true;
					}
					foreach (string key in tableCol.i_FkIn.Keys)
					{
						TableCol.TableKeys.kConstraint kConstraint = tableCol.i_FkIn[key];
						text = text + kConstraint.s_Table + " (" + kConstraint.s_Columns + ")\n";
						arrayListEx4.AddOnce(BuildDropConstraint(key, kConstraint.s_Table));
						arrayListEx5.AddOnce($"ALTER TABLE [{kConstraint.s_Table}] ADD FOREIGN KEY ({kConstraint.s_Columns}) REFERENCES [{mi_TableDef.Name}]({tableCol.s_ColName})");
					}
					foreach (TableCol.Index item2 in tableCol.i_Indexes.GetByType(b_Unique: false, b_Primary: true))
					{
						arrayListEx4.AddOnce(BuildDropConstraint(item2.s_Name, null));
					}
				}
				if (tableCol2.b_Primary)
				{
					arrayListEx6.Add(tableCol2.s_ColName);
				}
				if (tableCol.s_Default.Length > 0 && tableCol2.s_Default.Length == 0)
				{
					arrayListEx.AddOnce(BuildDropConstraint(tableCol.s_DefConstr, null));
				}
			}
			if (flag | flag2)
			{
				if (text.Length > 0)
				{
					return false;
				}
				arrayListEx.AddRange(arrayListEx4);
				if (arrayListEx6.Count > 0)
				{
					arrayListEx3.Add(string.Format("ALTER TABLE [{0}] ADD PRIMARY KEY ({1})", mi_TableDef.Name, arrayListEx6.ToList(",")));
				}
				if (flag2 && !flag)
				{
					arrayListEx3.AddRange(arrayListEx5);
				}
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(arrayListEx);
			arrayList.AddRange(arrayListEx2);
			arrayList.AddRange(arrayListEx3);
			return CommitChanges(arrayList);
		}

		public bool AddColumn(TableCol i_Col)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(i_Col.BuildAddColumnCommand());
			return CommitChanges(arrayList);
		}

		public bool DeleteColumn(int s32_Index)
		{
			ArrayList arrayList = new ArrayList();
			TableCol tableCol = (TableCol)mi_TableDef.Columns[s32_Index];
			if (tableCol.s_DefConstr.Length > 0)
			{
				arrayList.Add(BuildDropConstraint(tableCol.s_DefConstr, null));
			}
			foreach (string name in tableCol.i_Indexes.Names)
			{
				arrayList.Add(BuildDropConstraint(name, null));
			}
			foreach (string key in tableCol.i_FkOut.Keys)
			{
				arrayList.Add(BuildDropConstraint(key, null));
			}
			string value = string.Format("IF NOT col_length('{0}', '{1}') IS NULL\nALTER TABLE [{0}] DROP COLUMN [{1}]", ms_TableName, tableCol.s_ColName);
			arrayList.Add(value);
			return CommitChanges(arrayList);
		}

		public static bool IsTableScalarOrEmpty(DataTable i_Table, out object o_Value)
		{
			o_Value = null;
			if (i_Table == null || i_Table.Rows.Count == 0)
			{
				return true;
			}
			if (i_Table.Rows.Count == 1 && i_Table.Columns.Count == 1)
			{
				o_Value = i_Table.Rows[0][0];
				return true;
			}
			return false;
		}

		public string BuildOrderClause()
		{
			string text = "";
			foreach (TableCol column in mi_TableDef.Columns)
			{
				if (column.b_Primary || column.b_Unique)
				{
					if (text.Length > 0)
					{
						text += ", ";
					}
					text = text + "[" + column.s_ColName + "]";
				}
			}
			if (text.Length == 0)
			{
				return "";
			}
			return " ORDER BY " + text;
		}

		public string BuildDropConstraint(string s_Constraint, string s_Table)
		{
			if (s_Table == null)
			{
				s_Table = ms_TableName;
			}
			return string.Format("IF objectproperty(object_id('{0}'), 'IsConstraint') = 1\nALTER TABLE [{1}] DROP CONSTRAINT [{0}]", s_Constraint, s_Table);
		}

		public static ArrayList[] CalculateTableHashes(Form i_Owner, DataTable i_Table1, DataTable i_Table2, ArrayList i_Columns)
		{
			i_Owner.Cursor = Cursors.WaitCursor;
			DataTable[] array = new DataTable[2]
			{
				i_Table1,
				i_Table2
			};
			ArrayList[] array2 = new ArrayList[2]
			{
				new ArrayList(),
				new ArrayList()
			};
			StringBuilder stringBuilder = new StringBuilder(5000);
			for (int i = 0; i < 2; i++)
			{
				if (array[i] != null)
				{
					for (int j = 0; j < array[i].Rows.Count; j++)
					{
						stringBuilder.Length = 0;
						foreach (string i_Column in i_Columns)
						{
							stringBuilder.Append("{");
							stringBuilder.Append(Functions.DbValueForSql(array[i].Rows[j][i_Column]));
							stringBuilder.Append("}");
						}
						array2[i].Add(Functions.CalcMD5(stringBuilder.ToString()));
						Application.DoEvents();
					}
				}
			}
			i_Owner.Cursor = Cursors.Arrow;
			return array2;
		}

		public static ArrayList[] CompareTableHashes(Form i_Owner, ArrayList[] i_MD5s)
		{
			i_Owner.Cursor = Cursors.WaitCursor;
			ArrayList[] array = new ArrayList[2]
			{
				new ArrayList(),
				new ArrayList()
			};
			for (int i = 0; i < 2; i++)
			{
				for (int j = 0; j < i_MD5s[i].Count; j++)
				{
					string item = (string)i_MD5s[i][j];
					if (!i_MD5s[1 - i].Contains(item))
					{
						array[i].Add(j);
					}
					Application.DoEvents();
				}
			}
			i_Owner.Cursor = Cursors.Arrow;
			return array;
		}
	}
}
