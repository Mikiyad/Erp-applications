using Micromind.DataControls.QueryBuilder.Forms;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Micromind.DataControls.QueryBuilder.Controls
{
	public class DataGridEx : System.Windows.Forms.DataGrid
	{
		private SqlTable mi_SqlTable;

		private frmFind.cParam mi_FindParam = new frmFind.cParam();

		private ArrayList mi_CustomCols = new ArrayList();

		private string ms_Where = "";

		public string ExecutedCommands => mi_SqlTable.ExecutedCommands;

		public TableDef TableDef => mi_SqlTable.TableDef;

		public string HeadingInfo
		{
			get
			{
				string text = $"Rows: {base.ListManager.Count} / {mi_SqlTable.RowCountOnServer},  Columns: {mi_SqlTable.Table.Columns.Count}";
				if (base.ListManager.Count == mi_SqlTable.RowCountOnServer)
				{
					text = text + ",  Memory usage: " + Functions.FormatSize(mi_SqlTable.MemoryUsage);
				}
				return text;
			}
		}

		public int VertScrollPos
		{
			get
			{
				return base.VertScrollBar.Value;
			}
			set
			{
				GridVScrolled(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, value));
			}
		}

		public int HorScrollPos
		{
			get
			{
				return base.HorizScrollBar.Value;
			}
			set
			{
				GridHScrolled(this, new ScrollEventArgs(ScrollEventType.ThumbPosition, value));
			}
		}

		public string WhereClause
		{
			set
			{
				ms_Where = value;
			}
		}

		public bool DataChanged
		{
			get
			{
				if (mi_SqlTable == null || mi_SqlTable.CheckColumns == SqlTable.eColCheck.Bad)
				{
					return false;
				}
				if (base.ListManager == null)
				{
					return false;
				}
				PushValue(b_EndEdit: true);
				return mi_SqlTable.DataChanged > 0;
			}
		}

		public event KeyEventHandler evGridAction;

		public void BindToTable(string s_DataBase, string s_TableName)
		{
			if (mi_SqlTable != null)
			{
				mi_SqlTable.Dispose();
			}
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			BackColor = Defaults.BkgColControl;
			base.CaptionVisible = false;
		}

		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (mi_SqlTable != null)
			{
				mi_SqlTable.Dispose();
			}
			base.DataSource = null;
			base.OnHandleDestroyed(e);
		}

		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (base.DataSource != null)
			{
				base.OnLayout(levent);
			}
		}

		public void ColumnStartedEditingPublic(Rectangle bounds)
		{
			base.ColumnStartedEditing(bounds);
		}

		protected override void OnCurrentCellChanged(EventArgs e)
		{
			base.OnCurrentCellChanged(e);
		}

		public override bool PreProcessMessage(ref Message k_Msg)
		{
			if (k_Msg.Msg == 256 && (int)k_Msg.WParam == 116)
			{
				OnGridAction(new KeyEventArgs(Keys.F5));
			}
			return base.PreProcessMessage(ref k_Msg);
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			OnGridAction(e);
			if (!e.Handled)
			{
				base.OnKeyDown(e);
			}
		}

		public void OnGridAction(KeyEventArgs e)
		{
			if (e.Shift || e.Alt)
			{
				return;
			}
			if (e.Control)
			{
				switch (e.KeyCode)
				{
				case Keys.F:
				case Keys.R:
					e.Handled = true;
					Find(b_OpenForm: true, b_Foreward: true, b_Increment: true);
					return;
				case Keys.E:
					e.Handled = true;
					CommitChanges();
					return;
				}
			}
			else
			{
				switch (e.KeyCode)
				{
				case Keys.F3:
					e.Handled = true;
					Find(b_OpenForm: false, b_Foreward: true, b_Increment: true);
					return;
				case Keys.F4:
					e.Handled = true;
					Find(b_OpenForm: false, b_Foreward: false, b_Increment: true);
					return;
				case Keys.F5:
					e.Handled = true;
					LoadTable(b_LoadData: true, b_LoadColumns: false, b_Top10: false);
					return;
				case Keys.F6:
					e.Handled = true;
					LoadTable(b_LoadData: true, b_LoadColumns: false, b_Top10: true);
					return;
				case Keys.Tab:
					e.Handled = true;
					base.OnKeyDown(e);
					return;
				}
			}
			if (this.evGridAction != null)
			{
				this.evGridAction(this, e);
			}
		}

		private string OnFindCallback(frmFind.cParam i_Param)
		{
			mi_FindParam = i_Param;
			return Find(b_OpenForm: false, b_Foreward: true, !i_Param.b_Replace);
		}

		public string Find(bool b_OpenForm, bool b_Foreward, bool b_Increment)
		{
			if (b_OpenForm || mi_FindParam.s_Find.Length == 0)
			{
				mi_FindParam.s_Find = Functions.DbValueForDisplay(base[base.CurrentCell], b_ForHtml: false);
				mi_FindParam.b_Replace = !base.ReadOnly;
				mi_FindParam.s_CheckExtra1 = "Search only in the selected column";
				new frmFind(0, mi_FindParam, OnFindCallback).ShowDialog(base.TopLevelControl);
				return "";
			}
			DataTable dataTable = (DataTable)base.DataSource;
			int Col = base.CurrentCell.ColumnNumber;
			int Row = base.CurrentCell.RowNumber;
			int num = dataTable.Columns.Count * dataTable.Rows.Count;
			bool b_SameColumn = false;
			if (mi_FindParam.b_CheckExtra1)
			{
				b_SameColumn = true;
				num = dataTable.Rows.Count;
			}
			if (b_Increment)
			{
				GetNextCell(dataTable, b_Foreward, b_SameColumn, ref Col, ref Row);
			}
			bool flag = false;
			for (int i = 0; i <= num; i++)
			{
				string text = Functions.DbValueForDisplay(base[Row, Col], b_ForHtml: false);
				int num2 = Functions.Find(text, mi_FindParam.s_Find, 0, b_Reverse: false, mi_FindParam.b_WholeWord);
				if (num2 >= 0)
				{
					base.CurrentCell = new DataGridCell(Row, Col);
					if (!mi_FindParam.b_Replace)
					{
						return "";
					}
					if (flag)
					{
						return "";
					}
					if (i > 0)
					{
						return "";
					}
					string text3 = (string)(base[Row, Col] = Functions.Replace(text, num2, mi_FindParam.s_Find.Length, mi_FindParam.s_Replace));
					PushValue(b_EndEdit: false);
					flag = true;
				}
				GetNextCell(dataTable, b_Foreward, b_SameColumn, ref Col, ref Row);
				Application.DoEvents();
			}
			return "Not found!";
		}

		private void GetNextCell(DataTable i_Table, bool b_Foreward, bool b_SameColumn, ref int Col, ref int Row)
		{
			if (b_SameColumn)
			{
				Row += (b_Foreward ? 1 : (-1));
			}
			else
			{
				Col += (b_Foreward ? 1 : (-1));
			}
			if (Col >= i_Table.Columns.Count)
			{
				Col = 0;
				Row++;
			}
			if (Col < 0)
			{
				Col = i_Table.Columns.Count - 1;
				Row--;
			}
			if (Row >= i_Table.Rows.Count)
			{
				Row = 0;
			}
			if (Row < 0)
			{
				Row = i_Table.Rows.Count - 1;
			}
		}

		public bool LoadTable(bool b_LoadData, bool b_LoadColumns, bool b_Top10)
		{
			if (mi_SqlTable == null)
			{
				return false;
			}
			base.DataSource = null;
			if (!mi_SqlTable.LoadTable(b_LoadData, b_LoadColumns, b_Top10, ms_Where))
			{
				return false;
			}
			AttachTable(mi_SqlTable.Table);
			return true;
		}

		public void AttachTable(DataTable i_Table)
		{
			TableDef tableDef = (mi_SqlTable == null) ? null : mi_SqlTable.TableDef;
			mi_CustomCols.Clear();
			base.DataBindings.Clear();
			base.TableStyles.Clear();
			base.DataSource = null;
			if (tableDef == null)
			{
				base.ReadOnly = true;
			}
			DataGridTableStyle dataGridTableStyle = new DataGridTableStyle();
			for (int i = 0; i < i_Table.Columns.Count; i++)
			{
				_ = i_Table.Columns[i];
				if (tableDef != null)
				{
					_ = (TableCol)tableDef.Columns[i];
				}
			}
			if (i_Table.TableName.Length == 0)
			{
				i_Table.TableName = "Nameless Table";
			}
			dataGridTableStyle.MappingName = i_Table.TableName;
			base.TableStyles.Add(dataGridTableStyle);
			base.DataSource = i_Table;
			((DataView)base.ListManager.List).AllowNew = (mi_SqlTable != null && mi_SqlTable.CheckColumns != SqlTable.eColCheck.Bad);
		}

		public void PushValue(bool b_EndEdit)
		{
			if (b_EndEdit)
			{
				EndEdit((DataGridColumnStyle)mi_CustomCols[base.CurrentCell.ColumnNumber], base.CurrentRowIndex, shouldAbort: false);
			}
			base.ListManager.EndCurrentEdit();
		}

		public bool CommitChanges()
		{
			PushValue(b_EndEdit: true);
			if (!ValidateTable(b_DeepCheck: true))
			{
				return false;
			}
			ArrayList i_SqlCmd = new ArrayList();
			mi_SqlTable.BuildUpdateTableCommands(i_SqlCmd, b_All: false);
			return mi_SqlTable.CommitChanges(i_SqlCmd);
		}

		public bool AskCommitChanges()
		{
			if (!DataChanged)
			{
				return true;
			}
			return CommitChanges();
		}

		public void DesignTable(ArrayList i_NewColumns)
		{
			mi_SqlTable.DesignTable(i_NewColumns);
			LoadTable(b_LoadData: false, b_LoadColumns: true, b_Top10: false);
		}

		public void ExportTable()
		{
			ArrayList i_SqlCmd = new ArrayList();
			mi_SqlTable.BuildExportCommands(i_SqlCmd);
		}

		public void DeleteRow(int s32_Index)
		{
			if (ValidateTable(b_DeepCheck: false) && s32_Index >= 0 && s32_Index < mi_SqlTable.Table.Rows.Count)
			{
				base.ListManager.RemoveAt(s32_Index);
			}
		}

		public void AddRow()
		{
			if (ValidateTable(b_DeepCheck: false))
			{
				base.ListManager.AddNew();
			}
		}

		public void DeleteColumn(int s32_Index)
		{
			if (s32_Index >= 0 && s32_Index < mi_SqlTable.Table.Columns.Count)
			{
				CommitChanges();
				if (mi_SqlTable.DeleteColumn(s32_Index))
				{
					LoadTable(b_LoadData: true, b_LoadColumns: true, b_Top10: true);
				}
			}
		}

		public void AddColumn(TableCol i_NewColumn)
		{
			CommitChanges();
			if (mi_SqlTable.AddColumn(i_NewColumn))
			{
				LoadTable(b_LoadData: true, b_LoadColumns: true, b_Top10: true);
			}
		}

		public bool IsEditAllowed(int s32_Row, int s32_Column)
		{
			if (((TableCol)mi_SqlTable.TableDef.Columns[s32_Column]).b_Identity)
			{
				return false;
			}
			return ValidateTable(b_DeepCheck: false);
		}

		public bool ValidateTable(bool b_DeepCheck)
		{
			if (mi_SqlTable.CheckColumns == SqlTable.eColCheck.Bad)
			{
				return false;
			}
			if (!b_DeepCheck)
			{
				return true;
			}
			if ((mi_SqlTable.DataChanged & 0x14) > 0)
			{
				foreach (TableCol column in mi_SqlTable.TableDef.Columns)
				{
					if (!column.b_Identity && column.b_Primary)
					{
						for (int i = 0; i < base.ListManager.Count; i++)
						{
							if (base[i, column.s32_ColIndex] is DBNull)
							{
								_ = $"The column '{column.s_ColName}' has a NULL value in row {i} which is not allowed in Primary columns.";
								base.CurrentCell = new DataGridCell(i, column.s32_ColIndex);
								return false;
							}
						}
					}
				}
			}
			_ = 1;
			return true;
		}

		public void ExportHtml(StringBuilder s_Data, DataTable i_Table, bool b_ForExcel)
		{
			Form form = (Form)base.TopLevelControl;
			form.Cursor = Cursors.WaitCursor;
			DataTable dataTable = (DataTable)base.DataSource;
			if (dataTable == null && i_Table == null)
			{
				return;
			}
			bool flag = false;
			if (i_Table == null || dataTable.TableName == i_Table.TableName)
			{
				flag = true;
				i_Table = dataTable;
			}
			if (b_ForExcel)
			{
				s_Data.AppendFormat("<table><tr><td colspan={0}><font size=4 color=blue>&nbsp;{1}</font> &nbsp; {2} rows, {0} columns</td></tr></table>", i_Table.Columns.Count, i_Table.TableName, i_Table.Rows.Count);
			}
			else
			{
				s_Data.AppendFormat("<font size=5 color=blue><b>&nbsp;{0}</b></font> &nbsp; {1} rows, {2} columns<p>\r\n", i_Table.TableName, i_Table.Rows.Count, i_Table.Columns.Count);
			}
			s_Data.Append("<table cellspacing=0 cellpadding=3 bordercolor=black border=1 style='background-color:white; border-color:black; border-width:1px; border-style:solid; border-collapse:collapse; font-family:Arial; font-size:11px;'>\r\n");
			string str = string.Format("<tr bgcolor={0}>", "#E0D0C0");
			for (int i = 0; i < i_Table.Columns.Count; i++)
			{
				DataColumn dataColumn = i_Table.Columns[i];
				str += string.Format(arg1: (!flag || mi_SqlTable == null) ? Functions.CutBeginAt(dataColumn.DataType.FullName, "System.") : ((TableCol)mi_SqlTable.TableDef.Columns[i]).s_FullType, format: "<th>{0}<br><font color=blue>{1}</font></th>", arg0: Functions.ReplaceHtml(dataColumn.Caption));
			}
			str += "</tr>\r\n";
			for (int j = 0; j < i_Table.Rows.Count; j++)
			{
				int num = 25;
				if (j == 0 || (num > 0 && j % num == 0 && !b_ForExcel))
				{
					s_Data.Append(str);
				}
				s_Data.Append("<tr valign=top>");
				for (int k = 0; k < i_Table.Columns.Count; k++)
				{
					Color white = Color.White;
					if (!flag)
					{
						_ = i_Table.Rows[j][k];
					}
					Functions.GetHtmlColor(white);
				}
				s_Data.Append("</tr>\r\n");
				Application.DoEvents();
			}
			s_Data.Append("</table>\r\n");
			form.Cursor = Cursors.Arrow;
		}

		public void Serialize(XmlNode i_Node)
		{
			Form form = (Form)base.TopLevelControl;
			form.Cursor = Cursors.WaitCursor;
			DataTable dataTable = (DataTable)base.DataSource;
			XML.WriteSubNode(i_Node, "Creator", Functions.FirstToUpper(Environment.UserName, b_IsFile: false));
			XML.WriteSubNode(i_Node, "ExportDate", Functions.DbValueSerialize(DateTime.Now));
			if (mi_SqlTable != null)
			{
				XmlNode x_Node = XML.CreateSubNode(i_Node, "Table");
				mi_SqlTable.TableDef.Serialize(x_Node);
			}
			XmlNode i_Node2 = XML.CreateSubNode(i_Node, "Data");
			for (int i = 0; i < base.ListManager.Count; i++)
			{
				XmlNode i_Node3 = XML.CreateSubNode(i_Node2, "Row_" + i);
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					XML.CreateSubNode(i_Node3, "Col_" + j, dataTable.Columns[j].ColumnName).InnerText = Functions.DbValueSerialize(base[i, j]);
				}
			}
			form.Cursor = Cursors.Arrow;
		}

		public void Deserialize(XmlNode i_Node)
		{
			AskCommitChanges();
			Form form = (Form)base.TopLevelControl;
			TableDef tableDef = TableDef.Deserialize(i_Node.SelectSingleNode("/Xml/Table"));
			int count = mi_SqlTable.TableDef.Columns.Count;
			string str = "";
			if (tableDef == null)
			{
				str += "This XML document does not contain valid column definitions.\nSqlBuilder will try to import the data but this will only work if the XML data can be converted to the corresponding column's data type.";
			}
			else
			{
				Hashtable hashtable = mi_SqlTable.TableDef.FindMatchingColumns(tableDef.Columns);
				if (hashtable.Count != count)
				{
					if (hashtable.Count == 0)
					{
						str += "None of the columns to be imported exist in the current table.\nAn import is only possible column by column ignoring column name and data type.\nSqlBuilder will try to import the data but this will only work if the XML data can be converted to the corresponding column's data type.";
					}
					else
					{
						_ = $"Only {hashtable.Count} of {count} columns of the imported table have been found which match the name and data type.\n\nClick 'Yes' to import only those columns which match name and type.\nClick 'No' to import column by column no matter what name and type the columns have.";
					}
				}
			}
			form.Cursor = Cursors.WaitCursor;
			XmlNode xmlNode = i_Node.SelectSingleNode("/Xml/Data");
			for (int i = 0; xmlNode.SelectSingleNode("Row_" + i) != null; i++)
			{
			}
			base.DataSource = null;
			form.Cursor = Cursors.Arrow;
		}
	}
}
