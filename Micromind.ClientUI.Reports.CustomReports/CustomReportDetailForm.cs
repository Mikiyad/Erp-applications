using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomReports
{
	public class CustomReportDetailForm : Form
	{
		private CustomReportData currentData;

		private CustomReport currentReport;

		private const string TABLENAME_CONST = "Custom_Report";

		private const string IDFIELD_CONST = "CustomReportID";

		private bool isNewRecord = true;

		private string query = string.Empty;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private DataSet dataSetData;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraTabPageControl tabPageDetails;

		private UltraTabPageControl tabPageContacts;

		private Label label2;

		private Label label1;

		private ListBox listBoxRelations;

		private ListBox listBoxTables;

		private Button buttonAddTable;

		private MMTextBox textBoxName;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMLabel labelCode;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripButton toolStripButtonFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private Button buttonDeleteRelation;

		private Button buttonEditRelation;

		private Button buttonAddRelation;

		private Button buttonDeleteTable;

		private Button buttonEditTable;

		private Button buttonDeleteParameter;

		private Button buttonEditParameter;

		private Button buttonAddParameter;

		private Label label3;

		private ListBox listBoxParameters;

		private DataEntryGrid dataGridControls;

		private MMLabel mmLabel2;

		private ComboBox comboBoxMenu;

		private Label label4;

		private Button buttonSelect;

		private MMTextBox mmTextBox2;

		private MMTextBox textBoxTemplateName;

		private MMLabel mmLabel3;

		private OpenFileDialog openFileDialog1;

		private ToolStripButton toolStripButtonSaveSchema;

		private SaveFileDialog saveFileDialog1;

		private ToolStripButton toolStripButtonLayout;

		private ToolStripButton toolStripButton1;

		private ToolStripButton toolStripButtonImport;

		private Panel panelNote;

		private Label label19;

		private TextBox textBoxDisplayNote;

		private TextBox textBoxNote;

		private Label label20;

		public ScreenAreas ScreenCustomReport => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		private string Query
		{
			get
			{
				return query;
			}
			set
			{
				query = value;
			}
		}

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
					textBoxCode.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					textBoxCode.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonLayout;
				bool enabled = toolStripButtonSaveSchema.Enabled = !value;
				toolStripButton.Enabled = enabled;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public CustomReportDetailForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += CustomReportDetailsForm_Load;
			dataGridControls.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridControls.InitializeRow += dataGridList_InitializeRow;
			dataGridControls.ClickCellButton += dataGridControls_ClickCellButton;
		}

		private bool GetData_old()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomReportData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomReportTable.Rows[0] : currentData.CustomReportTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomReportID"] = textBoxCode.Text.Trim();
				dataRow["CustomReportName"] = textBoxName.Text.Trim();
				dataRow["TemplateName"] = textBoxTemplateName.Text.Trim();
				if (comboBoxMenu.SelectedItem != null)
				{
					NameValue nameValue = comboBoxMenu.SelectedItem as NameValue;
					dataRow["ParentMenuName"] = nameValue.ID;
				}
				CustomReport customReport = new CustomReport();
				foreach (object item4 in listBoxTables.Items)
				{
					CustomReportTable item = item4 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				foreach (object item5 in listBoxRelations.Items)
				{
					ReportRelation item2 = item5 as ReportRelation;
					customReport.Relations.Add(item2);
				}
				foreach (object item6 in listBoxParameters.Items)
				{
					ReportParameter item3 = item6 as ReportParameter;
					customReport.Parameters.Add(item3);
				}
				foreach (UltraGridRow row in dataGridControls.Rows)
				{
					CustomReportControl customReportControl = new CustomReportControl();
					customReportControl.ControlType = (CRCTypes)int.Parse(row.Cells["ControlType"].Value.ToString());
					customReportControl.ValueType = byte.Parse(row.Cells["ValueType"].Value.ToString());
					customReportControl.Key = row.Cells["Key"].Value.ToString();
					customReportControl.FieldName = row.Cells["FieldName"].Value.ToString();
					customReportControl.DisplayText = row.Cells["DisplayText"].Value.ToString();
					dataRow["Query"] = row.Cells["Query"].Value.ToString();
					if (customReportControl.ControlType == CRCTypes.DateBetween)
					{
						customReportControl.Key1 = row.Cells["Key1"].Value.ToString();
					}
					customReport.Controls.Add(customReportControl);
				}
				MemoryStream memoryStream = Global.SerializeToStream(customReport);
				dataRow["ReportData"] = memoryStream.ToArray();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomReportTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new CustomReportData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.CustomReportTable.Rows[0] : currentData.CustomReportTable.NewRow();
				dataRow.BeginEdit();
				dataRow["CustomReportID"] = textBoxCode.Text.Trim();
				dataRow["CustomReportName"] = textBoxName.Text.Trim();
				dataRow["TemplateName"] = textBoxTemplateName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["DisplayNote"] = textBoxDisplayNote.Text;
				if (comboBoxMenu.SelectedItem != null)
				{
					NameValue nameValue = comboBoxMenu.SelectedItem as NameValue;
					dataRow["ParentMenuName"] = nameValue.ID;
				}
				CustomReport customReport = new CustomReport();
				foreach (object item4 in listBoxTables.Items)
				{
					CustomReportTable item = item4 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				foreach (object item5 in listBoxRelations.Items)
				{
					ReportRelation item2 = item5 as ReportRelation;
					customReport.Relations.Add(item2);
				}
				foreach (object item6 in listBoxParameters.Items)
				{
					ReportParameter item3 = item6 as ReportParameter;
					customReport.Parameters.Add(item3);
				}
				foreach (UltraGridRow row in dataGridControls.Rows)
				{
					CustomReportControl customReportControl = new CustomReportControl();
					customReportControl.ControlType = (CRCTypes)int.Parse(row.Cells["ControlType"].Value.ToString());
					customReportControl.ValueType = byte.Parse(row.Cells["ValueType"].Value.ToString());
					customReportControl.Key = row.Cells["Key"].Value.ToString();
					customReportControl.FieldName = row.Cells["FieldName"].Value.ToString();
					customReportControl.DisplayText = row.Cells["DisplayText"].Value.ToString();
					if (customReportControl.ControlType == CRCTypes.DateBetween)
					{
						customReportControl.Key1 = row.Cells["Key1"].Value.ToString();
					}
					if (customReportControl.ControlType == CRCTypes.SelectionBox)
					{
						customReportControl.Query = row.Cells["Query"].Value.ToString();
					}
					customReport.Controls.Add(customReportControl);
					if (customReportControl.ControlType == CRCTypes.SelectionBox && !string.IsNullOrEmpty(Query))
					{
						dataRow["Query"] = Query;
					}
				}
				MemoryStream memoryStream = Global.SerializeToStream(customReport);
				dataRow["ReportData"] = memoryStream.ToArray();
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.CustomReportTable.Rows.Add(dataRow);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		private void dataGridList_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			if (!e.ReInitialize)
			{
				e.Row.Cells["Query"].Value = "Query";
			}
		}

		public void LoadData(string id)
		{
			try
			{
				if (!(id.Trim() == "") && CanClose())
				{
					currentData = Factory.CustomReportSystem.GetCustomReportByID(id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxCode.Text = dataRow["CustomReportID"].ToString();
				textBoxName.Text = dataRow["CustomReportName"].ToString();
				textBoxTemplateName.Text = dataRow["TemplateName"].ToString();
				textBoxNote.Text = dataRow["Note"].ToString();
				textBoxDisplayNote.Text = dataRow["DisplayNote"].ToString();
				string text = dataRow["ParentMenuName"].ToString();
				if (text != "")
				{
					foreach (object item in comboBoxMenu.Items)
					{
						if ((item as NameValue).ID == text)
						{
							comboBoxMenu.SelectedItem = item;
							break;
						}
					}
				}
				byte[] streamBytes = (byte[])dataRow["ReportData"];
				currentReport = (CustomReport)Global.DeserializeFromStream(streamBytes);
				listBoxTables.Items.Clear();
				if (currentReport.Tables.Count > 0)
				{
					foreach (CustomReportTable table in currentReport.Tables)
					{
						listBoxTables.Items.Add(table);
					}
				}
				listBoxRelations.Items.Clear();
				if (currentReport.Relations != null && currentReport.Relations.Count > 0)
				{
					foreach (ReportRelation relation in currentReport.Relations)
					{
						listBoxRelations.Items.Add(relation);
					}
				}
				listBoxParameters.Items.Clear();
				if (currentReport.Parameters.Count > 0)
				{
					foreach (ReportParameter parameter in currentReport.Parameters)
					{
						listBoxParameters.Items.Add(parameter);
					}
				}
				DataTable dataTable = dataGridControls.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentReport.Controls != null && currentReport.Controls.Count > 0)
				{
					foreach (CustomReportControl control in currentReport.Controls)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["ControlType"] = (int)control.ControlType;
						dataRow2["ValueType"] = control.ValueType;
						dataRow2["Key"] = control.Key;
						dataRow2["FieldName"] = control.FieldName;
						dataRow2["DisplayText"] = control.DisplayText;
						dataRow2["Query"] = control.Query;
						if (control.ControlType == CRCTypes.DateBetween)
						{
							dataRow2["Key1"] = control.Key1;
							dataGridControls.DisplayLayout.Bands[0].Columns["Key1"].Hidden = false;
						}
						else
						{
							dataGridControls.DisplayLayout.Bands[0].Columns["Key1"].Hidden = true;
						}
						if (control.ControlType == CRCTypes.SelectionBox)
						{
							dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Hidden = false;
						}
						else
						{
							dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Hidden = true;
						}
						dataTable.Rows.Add(dataRow2);
					}
				}
				dataTable.AcceptChanges();
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = (!isNewRecord) ? Factory.CustomReportSystem.UpdateCustomReport(currentData) : Factory.CustomReportSystem.CreateCustomReport(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
				return false;
			}
			if (textBoxCode.Text.Trim().Length == 0 || textBoxName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Custom_Report", "CustomReportID", textBoxCode.Text.Trim()))
			{
				ErrorHelper.InformationMessage("Code already exist.");
				textBoxCode.Focus();
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Custom_Report", "CustomReportID");
			textBoxName.Clear();
			listBoxTables.Items.Clear();
			listBoxRelations.Items.Clear();
			listBoxParameters.Items.Clear();
			textBoxTemplateName.Clear();
			textBoxNote.Clear();
			textBoxDisplayNote.Clear();
			IsNewRecord = true;
			(dataGridControls.DataSource as DataTable)?.Rows.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void CustomReportGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void CustomReportGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridControls.ActiveRow != null && e.Cell.Column.Key == "ControlType")
				{
					if (e.Cell.Value != null && e.Cell.Value.ToString() != "" && e.Cell.Value.ToString() == "150")
					{
						dataGridControls.DisplayLayout.Bands[0].Columns["Key1"].Hidden = false;
					}
					else
					{
						dataGridControls.DisplayLayout.Bands[0].Columns["Key1"].Hidden = true;
					}
					if (e.Cell.Value != null && e.Cell.Value.ToString() != "" && e.Cell.Value.ToString() == "149")
					{
						dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Hidden = false;
					}
					else
					{
						dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Hidden = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.CustomReportSystem.DeleteCustomReport(textBoxCode.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetNextID("Custom_Report", "CustomReportID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Custom_Report", "CustomReportID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Custom_Report", "CustomReportID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Custom_Report", "CustomReportID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Custom_Report", "CustomReportID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void CustomReportDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				LoadMenuComboBox();
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadMenuComboBox()
		{
			foreach (object dropDownItem in ((formMain)FormActivator.MainForm).mReport.DropDownItems)
			{
				ToolStripMenuItem toolStripMenuItem = null;
				if (!(dropDownItem.GetType() != typeof(ToolStripMenuItem)))
				{
					toolStripMenuItem = (dropDownItem as ToolStripMenuItem);
					if (!(toolStripMenuItem.Name == "chartCenterToolStripMenuItem") && !(toolStripMenuItem.Name == "smartReportToolStripMenuItem"))
					{
						NameValue item = new NameValue(toolStripMenuItem.Text.Replace("&&", "&"), toolStripMenuItem.Name);
						comboBoxMenu.Items.Add(item);
					}
				}
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomReport);
		}

		private void buttonAddTable_Click(object sender, EventArgs e)
		{
			AddTableDialog addTableDialog = new AddTableDialog();
			if (addTableDialog.ShowDialog() == DialogResult.OK)
			{
				CustomReportTable customReportTable = new CustomReportTable();
				customReportTable.tableName = addTableDialog.TableName;
				customReportTable.query = addTableDialog.Query;
				listBoxTables.Items.Add(customReportTable);
			}
		}

		private void buttonAddRelation_Click(object sender, EventArgs e)
		{
			CustomReport customReport = new CustomReport();
			foreach (object item2 in listBoxTables.Items)
			{
				CustomReportTable item = item2 as CustomReportTable;
				customReport.Tables.Add(item);
			}
			AddRelationDialog addRelationDialog = new AddRelationDialog();
			addRelationDialog.LoadReportData(customReport);
			if (addRelationDialog.ShowDialog() == DialogResult.OK)
			{
				listBoxRelations.Items.Add(addRelationDialog.Relationship);
			}
		}

		private void buttonDeleteRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the relationship?") == DialogResult.Yes)
			{
				listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
			}
		}

		private void buttonEditRelation_Click(object sender, EventArgs e)
		{
			if (listBoxRelations.SelectedItem != null)
			{
				CustomReport customReport = new CustomReport();
				foreach (object item2 in listBoxTables.Items)
				{
					CustomReportTable item = item2 as CustomReportTable;
					customReport.Tables.Add(item);
				}
				AddRelationDialog addRelationDialog = new AddRelationDialog();
				addRelationDialog.LoadReportData(customReport);
				addRelationDialog.EditRelation((ReportRelation)listBoxRelations.SelectedItem);
				if (addRelationDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxRelations.Items.Remove(listBoxRelations.SelectedItem);
					listBoxRelations.Items.Add(addRelationDialog.Relationship);
				}
			}
		}

		private void buttonDeleteTable_Click(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the table?") == DialogResult.Yes)
			{
				listBoxTables.Items.Remove(listBoxTables.SelectedItem);
			}
		}

		private void buttonEditTable_Click(object sender, EventArgs e)
		{
			if (listBoxTables.SelectedItem != null)
			{
				AddTableDialog addTableDialog = new AddTableDialog();
				addTableDialog.EditTable((CustomReportTable)listBoxTables.SelectedItem);
				if (addTableDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxTables.Items.Remove(listBoxTables.SelectedItem);
					listBoxTables.Items.Add(addTableDialog.ReportTable);
				}
			}
		}

		private void SetupGrid()
		{
			dataGridControls.SetupUI();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ControlType", typeof(string));
			dataTable.Columns.Add("ValueType", typeof(string));
			dataTable.Columns.Add("DisplayText", typeof(string));
			dataTable.Columns.Add("FieldName");
			dataTable.Columns.Add("Key");
			dataTable.Columns.Add("Key1");
			dataTable.Columns.Add("Query");
			dataGridControls.DataSource = dataTable;
			ValueList valueList = new ValueList();
			CRCTypes[] array = (CRCTypes[])Enum.GetValues(typeof(CRCTypes));
			for (int i = 0; i < array.Length; i++)
			{
				CRCTypes cRCTypes = array[i];
				valueList.ValueListItems.Add((int)cRCTypes, cRCTypes.ToString());
			}
			dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Button;
			dataGridControls.Rows.Band.Columns["Query"].ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
			dataGridControls.DisplayLayout.Bands[0].Columns["Query"].MaxWidth = 50;
			dataGridControls.DisplayLayout.Bands[0].Columns["Query"].CellAppearance.TextHAlign = HAlign.Center;
			dataGridControls.DisplayLayout.Bands[0].Columns["Key1"].Hidden = true;
			dataGridControls.DisplayLayout.Bands[0].Columns["Query"].Hidden = true;
			valueList.SortStyle = ValueListSortStyle.Ascending;
			dataGridControls.DisplayLayout.Bands[0].Columns["ControlType"].ValueList = valueList;
			dataGridControls.DisplayLayout.Bands[0].Columns["ControlType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			valueList = new ValueList();
			valueList.ValueListItems.Add(1, "Parameter");
			valueList.ValueListItems.Add(2, "Subquery");
			dataGridControls.DisplayLayout.Bands[0].Columns["ValueType"].ValueList = valueList;
			dataGridControls.DisplayLayout.Bands[0].Columns["ValueType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
		}

		private void buttonAddParameter_Click(object sender, EventArgs e)
		{
			AddParameterDialog addParameterDialog = new AddParameterDialog();
			if (addParameterDialog.ShowDialog() == DialogResult.OK)
			{
				ReportParameter reportParameter = new ReportParameter();
				reportParameter.ParameterName = addParameterDialog.ParameterName;
				reportParameter.ParameterType = addParameterDialog.DataType;
				listBoxParameters.Items.Add(reportParameter);
			}
		}

		private void buttonDeleteParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null && ErrorHelper.QuestionMessageYesNo("Delete the parameter?") == DialogResult.Yes)
			{
				listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
			}
		}

		private void buttonEditParameter_Click(object sender, EventArgs e)
		{
			if (listBoxParameters.SelectedItem != null)
			{
				AddParameterDialog addParameterDialog = new AddParameterDialog();
				addParameterDialog.EditParameter((ReportParameter)listBoxParameters.SelectedItem);
				if (addParameterDialog.ShowDialog() == DialogResult.OK)
				{
					listBoxParameters.Items.Remove(listBoxParameters.SelectedItem);
					listBoxParameters.Items.Add(addParameterDialog.ReportParameter);
				}
			}
		}

		private void buttonSelect_Click(object sender, EventArgs e)
		{
			string initialDirectory = Path.GetDirectoryName(Application.ExecutablePath) + "\\Print Templates\\Reports";
			openFileDialog1.Filter = "Axolon Report Templates (.repx)|*.repx";
			openFileDialog1.Multiselect = false;
			openFileDialog1.InitialDirectory = initialDirectory;
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBoxTemplateName.Text = Path.GetFileName(openFileDialog1.FileName);
			}
		}

		private void dataGridControls_ClickCellButton(object sender, CellEventArgs e)
		{
			if (IsNewRecord)
			{
				AddTableDialog addTableDialog = new AddTableDialog();
				if (currentData != null)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					if (!string.IsNullOrEmpty(dataRow["Query"].ToString()))
					{
						Query = dataRow["Query"].ToString();
						new AddTableDialog();
						addTableDialog.Query = Query;
						if (addTableDialog.ShowDialog() == DialogResult.OK)
						{
							Query = addTableDialog.Query;
						}
					}
				}
				else if (addTableDialog.ShowDialog() == DialogResult.OK)
				{
					Query = addTableDialog.Query;
				}
			}
			else
			{
				if (currentData == null)
				{
					return;
				}
				DataRow dataRow2 = currentData.Tables[0].Rows[0];
				if (!string.IsNullOrEmpty(dataRow2["Query"].ToString()))
				{
					Query = dataRow2["Query"].ToString();
					AddTableDialog addTableDialog2 = new AddTableDialog();
					addTableDialog2.Query = Query;
					if (addTableDialog2.ShowDialog() == DialogResult.OK)
					{
						Query = addTableDialog2.Query;
					}
				}
				else
				{
					AddTableDialog addTableDialog3 = new AddTableDialog();
					if (addTableDialog3.ShowDialog() == DialogResult.OK)
					{
						Query = addTableDialog3.Query;
					}
				}
			}
		}

		private void toolStripButtonSaveSchema_Click(object sender, EventArgs e)
		{
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				foreach (ReportParameter parameter in currentReport.Parameters)
				{
					if (!sqlCommand.Parameters.Contains(parameter.ParameterName))
					{
						sqlCommand.Parameters.AddWithValue(parameter.ParameterName, " 1=1 ");
					}
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				foreach (SqlParameter parameter2 in sqlCommand.Parameters)
				{
					arrayList.Add(parameter2.ParameterName);
					arrayList2.Add(parameter2.Value);
				}
				Array array = arrayList.ToArray(typeof(string));
				Array array2 = arrayList2.ToArray(typeof(string));
				DataSet customReportData = Factory.CustomReportSystem.GetCustomReportData(textBoxCode.Text, (string[])array, (string[])array2);
				saveFileDialog1.Filter = "Schema Files (*.xml)|*.xml";
				if (saveFileDialog1.ShowDialog() == DialogResult.OK)
				{
					customReportData.WriteXmlSchema(saveFileDialog1.FileName);
					ErrorHelper.InformationMessage("Report schema saved successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Unable to save schema for this report.");
			}
		}

		private void toolStripButtonLayout_Click(object sender, EventArgs e)
		{
			CustomReportDisplayForm customReportDisplayForm = new CustomReportDisplayForm();
			customReportDisplayForm.IsDesignMode = true;
			customReportDisplayForm.LoadReport(textBoxCode.Text);
			customReportDisplayForm.ShowDialog(this);
			new MMSelectionBox();
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0)
				{
					saveFileDialog1.AddExtension = true;
					saveFileDialog1.Filter = "Axolon Pivot Reports|*.axr";
					saveFileDialog1.FileName = currentData.CustomReportTable.Rows[0]["CustomReportID"].ToString();
					if (saveFileDialog1.ShowDialog() == DialogResult.OK && currentData != null)
					{
						currentData.WriteXml(saveFileDialog1.FileName, XmlWriteMode.WriteSchema);
						ErrorHelper.InformationMessage("Custom report exported successfully.");
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonImport_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog1.Filter = "Axolon Reports|*.axr";
				openFileDialog1.FileName = "";
				if (openFileDialog1.ShowDialog() == DialogResult.OK)
				{
					currentData = new CustomReportData();
					currentData.ReadXml(openFileDialog1.FileName, XmlReadMode.ReadSchema);
					FillData();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomReports.CustomReportDetailForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			panelNote = new System.Windows.Forms.Panel();
			label19 = new System.Windows.Forms.Label();
			textBoxDisplayNote = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			buttonSelect = new System.Windows.Forms.Button();
			mmTextBox2 = new Micromind.UISupport.MMTextBox();
			textBoxTemplateName = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxMenu = new System.Windows.Forms.ComboBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			tabPageDetails = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonDeleteParameter = new System.Windows.Forms.Button();
			buttonEditParameter = new System.Windows.Forms.Button();
			buttonAddParameter = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			listBoxParameters = new System.Windows.Forms.ListBox();
			buttonDeleteRelation = new System.Windows.Forms.Button();
			buttonEditRelation = new System.Windows.Forms.Button();
			buttonAddRelation = new System.Windows.Forms.Button();
			buttonDeleteTable = new System.Windows.Forms.Button();
			buttonEditTable = new System.Windows.Forms.Button();
			buttonAddTable = new System.Windows.Forms.Button();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			listBoxRelations = new System.Windows.Forms.ListBox();
			listBoxTables = new System.Windows.Forms.ListBox();
			tabPageContacts = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridControls = new Micromind.DataControls.DataEntryGrid();
			dataSetData = new System.Data.DataSet();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonLayout = new System.Windows.Forms.ToolStripButton();
			toolStripButtonSaveSchema = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripButtonImport = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			formManager = new Micromind.DataControls.FormManager();
			saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			tabPageGeneral.SuspendLayout();
			panelNote.SuspendLayout();
			tabPageDetails.SuspendLayout();
			tabPageContacts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridControls).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataSetData).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(panelNote);
			tabPageGeneral.Controls.Add(label4);
			tabPageGeneral.Controls.Add(buttonSelect);
			tabPageGeneral.Controls.Add(mmTextBox2);
			tabPageGeneral.Controls.Add(textBoxTemplateName);
			tabPageGeneral.Controls.Add(mmLabel3);
			tabPageGeneral.Controls.Add(mmLabel2);
			tabPageGeneral.Controls.Add(comboBoxMenu);
			tabPageGeneral.Controls.Add(textBoxName);
			tabPageGeneral.Controls.Add(textBoxCode);
			tabPageGeneral.Controls.Add(mmLabel1);
			tabPageGeneral.Controls.Add(labelCode);
			tabPageGeneral.Location = new System.Drawing.Point(2, 21);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(709, 353);
			panelNote.Controls.Add(label19);
			panelNote.Controls.Add(textBoxDisplayNote);
			panelNote.Controls.Add(textBoxNote);
			panelNote.Controls.Add(label20);
			panelNote.Location = new System.Drawing.Point(13, 146);
			panelNote.Name = "panelNote";
			panelNote.Size = new System.Drawing.Size(526, 172);
			panelNote.TabIndex = 26;
			label19.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(3, 7);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(33, 13);
			label19.TabIndex = 22;
			label19.Text = "Note:";
			textBoxDisplayNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxDisplayNote.Location = new System.Drawing.Point(103, 85);
			textBoxDisplayNote.MaxLength = 4000;
			textBoxDisplayNote.Multiline = true;
			textBoxDisplayNote.Name = "textBoxDisplayNote";
			textBoxDisplayNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxDisplayNote.Size = new System.Drawing.Size(420, 79);
			textBoxDisplayNote.TabIndex = 1;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(103, 3);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(420, 79);
			textBoxNote.TabIndex = 0;
			label20.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(3, 90);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(70, 13);
			label20.TabIndex = 24;
			label20.Text = "Display Note:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(17, 129);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(32, 13);
			label4.TabIndex = 14;
			label4.Text = "Path:";
			buttonSelect.Location = new System.Drawing.Point(443, 103);
			buttonSelect.Name = "buttonSelect";
			buttonSelect.Size = new System.Drawing.Size(25, 21);
			buttonSelect.TabIndex = 4;
			buttonSelect.Text = "...";
			buttonSelect.UseVisualStyleBackColor = true;
			buttonSelect.Click += new System.EventHandler(buttonSelect_Click);
			mmTextBox2.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox2.CustomReportFieldName = "";
			mmTextBox2.CustomReportKey = "";
			mmTextBox2.CustomReportValueType = 1;
			mmTextBox2.IsComboTextBox = false;
			mmTextBox2.IsModified = false;
			mmTextBox2.Location = new System.Drawing.Point(116, 126);
			mmTextBox2.MaxLength = 64;
			mmTextBox2.Name = "mmTextBox2";
			mmTextBox2.ReadOnly = true;
			mmTextBox2.Size = new System.Drawing.Size(324, 20);
			mmTextBox2.TabIndex = 5;
			mmTextBox2.Text = "[Application Path]\\Print Templates\\Reports\\";
			textBoxTemplateName.BackColor = System.Drawing.Color.White;
			textBoxTemplateName.CustomReportFieldName = "";
			textBoxTemplateName.CustomReportKey = "";
			textBoxTemplateName.CustomReportValueType = 1;
			textBoxTemplateName.IsComboTextBox = false;
			textBoxTemplateName.IsModified = false;
			textBoxTemplateName.Location = new System.Drawing.Point(116, 104);
			textBoxTemplateName.MaxLength = 64;
			textBoxTemplateName.Name = "textBoxTemplateName";
			textBoxTemplateName.Size = new System.Drawing.Size(324, 20);
			textBoxTemplateName.TabIndex = 3;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(16, 106);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(93, 13);
			mmLabel3.TabIndex = 11;
			mmLabel3.Text = "Print Template:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(16, 80);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(42, 13);
			mmLabel2.TabIndex = 10;
			mmLabel2.Text = "Menu:";
			comboBoxMenu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxMenu.FormattingEnabled = true;
			comboBoxMenu.Location = new System.Drawing.Point(116, 77);
			comboBoxMenu.Name = "comboBoxMenu";
			comboBoxMenu.Size = new System.Drawing.Size(179, 21);
			comboBoxMenu.TabIndex = 2;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(116, 43);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(324, 20);
			textBoxName.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(116, 21);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(16, 43);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 7;
			mmLabel1.Text = "Report Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(16, 21);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(82, 13);
			labelCode.TabIndex = 5;
			labelCode.Text = "Report Code:";
			tabPageDetails.Controls.Add(buttonDeleteParameter);
			tabPageDetails.Controls.Add(buttonEditParameter);
			tabPageDetails.Controls.Add(buttonAddParameter);
			tabPageDetails.Controls.Add(label3);
			tabPageDetails.Controls.Add(listBoxParameters);
			tabPageDetails.Controls.Add(buttonDeleteRelation);
			tabPageDetails.Controls.Add(buttonEditRelation);
			tabPageDetails.Controls.Add(buttonAddRelation);
			tabPageDetails.Controls.Add(buttonDeleteTable);
			tabPageDetails.Controls.Add(buttonEditTable);
			tabPageDetails.Controls.Add(buttonAddTable);
			tabPageDetails.Controls.Add(label2);
			tabPageDetails.Controls.Add(label1);
			tabPageDetails.Controls.Add(listBoxRelations);
			tabPageDetails.Controls.Add(listBoxTables);
			tabPageDetails.Location = new System.Drawing.Point(-10000, -10000);
			tabPageDetails.Name = "tabPageDetails";
			tabPageDetails.Size = new System.Drawing.Size(709, 353);
			buttonDeleteParameter.Location = new System.Drawing.Point(614, 85);
			buttonDeleteParameter.Name = "buttonDeleteParameter";
			buttonDeleteParameter.Size = new System.Drawing.Size(75, 23);
			buttonDeleteParameter.TabIndex = 12;
			buttonDeleteParameter.Text = "Delete";
			buttonDeleteParameter.UseVisualStyleBackColor = true;
			buttonDeleteParameter.Click += new System.EventHandler(buttonDeleteParameter_Click);
			buttonEditParameter.Location = new System.Drawing.Point(614, 56);
			buttonEditParameter.Name = "buttonEditParameter";
			buttonEditParameter.Size = new System.Drawing.Size(75, 23);
			buttonEditParameter.TabIndex = 11;
			buttonEditParameter.Text = "Edit";
			buttonEditParameter.UseVisualStyleBackColor = true;
			buttonEditParameter.Click += new System.EventHandler(buttonEditParameter_Click);
			buttonAddParameter.Location = new System.Drawing.Point(614, 27);
			buttonAddParameter.Name = "buttonAddParameter";
			buttonAddParameter.Size = new System.Drawing.Size(75, 23);
			buttonAddParameter.TabIndex = 10;
			buttonAddParameter.Text = "Add";
			buttonAddParameter.UseVisualStyleBackColor = true;
			buttonAddParameter.Click += new System.EventHandler(buttonAddParameter_Click);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(387, 11);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(63, 13);
			label3.TabIndex = 9;
			label3.Text = "Parameters:";
			listBoxParameters.FormattingEnabled = true;
			listBoxParameters.Location = new System.Drawing.Point(390, 27);
			listBoxParameters.Name = "listBoxParameters";
			listBoxParameters.Size = new System.Drawing.Size(218, 160);
			listBoxParameters.TabIndex = 8;
			buttonDeleteRelation.Location = new System.Drawing.Point(239, 270);
			buttonDeleteRelation.Name = "buttonDeleteRelation";
			buttonDeleteRelation.Size = new System.Drawing.Size(75, 23);
			buttonDeleteRelation.TabIndex = 7;
			buttonDeleteRelation.Text = "Delete";
			buttonDeleteRelation.UseVisualStyleBackColor = true;
			buttonDeleteRelation.Click += new System.EventHandler(buttonDeleteRelation_Click);
			buttonEditRelation.Location = new System.Drawing.Point(239, 241);
			buttonEditRelation.Name = "buttonEditRelation";
			buttonEditRelation.Size = new System.Drawing.Size(75, 23);
			buttonEditRelation.TabIndex = 6;
			buttonEditRelation.Text = "Edit";
			buttonEditRelation.UseVisualStyleBackColor = true;
			buttonEditRelation.Click += new System.EventHandler(buttonEditRelation_Click);
			buttonAddRelation.Location = new System.Drawing.Point(239, 212);
			buttonAddRelation.Name = "buttonAddRelation";
			buttonAddRelation.Size = new System.Drawing.Size(75, 23);
			buttonAddRelation.TabIndex = 5;
			buttonAddRelation.Text = "Add";
			buttonAddRelation.UseVisualStyleBackColor = true;
			buttonAddRelation.Click += new System.EventHandler(buttonAddRelation_Click);
			buttonDeleteTable.Location = new System.Drawing.Point(239, 85);
			buttonDeleteTable.Name = "buttonDeleteTable";
			buttonDeleteTable.Size = new System.Drawing.Size(75, 23);
			buttonDeleteTable.TabIndex = 4;
			buttonDeleteTable.Text = "Delete";
			buttonDeleteTable.UseVisualStyleBackColor = true;
			buttonDeleteTable.Click += new System.EventHandler(buttonDeleteTable_Click);
			buttonEditTable.Location = new System.Drawing.Point(239, 56);
			buttonEditTable.Name = "buttonEditTable";
			buttonEditTable.Size = new System.Drawing.Size(75, 23);
			buttonEditTable.TabIndex = 3;
			buttonEditTable.Text = "Edit";
			buttonEditTable.UseVisualStyleBackColor = true;
			buttonEditTable.Click += new System.EventHandler(buttonEditTable_Click);
			buttonAddTable.Location = new System.Drawing.Point(239, 27);
			buttonAddTable.Name = "buttonAddTable";
			buttonAddTable.Size = new System.Drawing.Size(75, 23);
			buttonAddTable.TabIndex = 2;
			buttonAddTable.Text = "Add";
			buttonAddTable.UseVisualStyleBackColor = true;
			buttonAddTable.Click += new System.EventHandler(buttonAddTable_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 196);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(80, 13);
			label2.TabIndex = 1;
			label2.Text = "Data Relations:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(42, 13);
			label1.TabIndex = 1;
			label1.Text = "Tables:";
			listBoxRelations.FormattingEnabled = true;
			listBoxRelations.Location = new System.Drawing.Point(15, 212);
			listBoxRelations.Name = "listBoxRelations";
			listBoxRelations.Size = new System.Drawing.Size(218, 134);
			listBoxRelations.TabIndex = 0;
			listBoxTables.FormattingEnabled = true;
			listBoxTables.Location = new System.Drawing.Point(15, 27);
			listBoxTables.Name = "listBoxTables";
			listBoxTables.Size = new System.Drawing.Size(218, 160);
			listBoxTables.TabIndex = 0;
			tabPageContacts.Controls.Add(dataGridControls);
			tabPageContacts.Location = new System.Drawing.Point(-10000, -10000);
			tabPageContacts.Name = "tabPageContacts";
			tabPageContacts.Size = new System.Drawing.Size(709, 353);
			dataGridControls.AllowAddNew = false;
			dataGridControls.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridControls.DisplayLayout.Appearance = appearance;
			dataGridControls.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridControls.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridControls.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridControls.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridControls.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridControls.DisplayLayout.MaxColScrollRegions = 1;
			dataGridControls.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridControls.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridControls.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridControls.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridControls.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridControls.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridControls.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridControls.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridControls.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridControls.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridControls.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridControls.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridControls.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridControls.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridControls.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridControls.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridControls.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridControls.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridControls.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridControls.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridControls.ExitEditModeOnLeave = false;
			dataGridControls.IncludeLotItems = false;
			dataGridControls.LoadLayoutFailed = false;
			dataGridControls.Location = new System.Drawing.Point(14, 21);
			dataGridControls.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridControls.Name = "dataGridControls";
			dataGridControls.ShowClearMenu = true;
			dataGridControls.ShowDeleteMenu = true;
			dataGridControls.ShowInsertMenu = true;
			dataGridControls.ShowMoveRowsMenu = true;
			dataGridControls.Size = new System.Drawing.Size(692, 317);
			dataGridControls.TabIndex = 2;
			dataGridControls.Text = "dataEntryGrid1";
			dataSetData.DataSetName = "Report";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageDetails);
			ultraTabControl1.Controls.Add(tabPageContacts);
			ultraTabControl1.Location = new System.Drawing.Point(12, 37);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(713, 376);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 1;
			appearance13.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance13;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageDetails;
			ultraTab2.Text = "&Data";
			ultraTab3.TabPage = tabPageContacts;
			ultraTab3.Text = "Controls";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(709, 353);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonLayout,
				toolStripButtonSaveSchema,
				toolStripButton1,
				toolStripButtonImport
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(737, 31);
			toolStrip1.TabIndex = 2;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonLayout.Image = Micromind.ClientUI.Properties.Resources._1408154720_advancedsettings;
			toolStripButtonLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLayout.Name = "toolStripButtonLayout";
			toolStripButtonLayout.Size = new System.Drawing.Size(110, 28);
			toolStripButtonLayout.Text = "Design Layout";
			toolStripButtonLayout.Click += new System.EventHandler(toolStripButtonLayout_Click);
			toolStripButtonSaveSchema.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonSaveSchema.Image");
			toolStripButtonSaveSchema.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonSaveSchema.Name = "toolStripButtonSaveSchema";
			toolStripButtonSaveSchema.Size = new System.Drawing.Size(104, 28);
			toolStripButtonSaveSchema.Text = "Save Schema";
			toolStripButtonSaveSchema.Click += new System.EventHandler(toolStripButtonSaveSchema_Click);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.ExportToXMLFile48;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(68, 28);
			toolStripButton1.Text = "Export";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripButtonImport.Image = Micromind.ClientUI.Properties.Resources.download_icon;
			toolStripButtonImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonImport.Name = "toolStripButtonImport";
			toolStripButtonImport.Size = new System.Drawing.Size(71, 28);
			toolStripButtonImport.Text = "Import";
			toolStripButtonImport.Click += new System.EventHandler(toolStripButtonImport_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 425);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(737, 40);
			panelButtons.TabIndex = 18;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(737, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 2;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(627, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 1;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			openFileDialog1.FileName = "openFileDialog1";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 17;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(737, 465);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomReportDetailForm";
			Text = "Custom Report Designer";
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			panelNote.ResumeLayout(false);
			panelNote.PerformLayout();
			tabPageDetails.ResumeLayout(false);
			tabPageDetails.PerformLayout();
			tabPageContacts.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridControls).EndInit();
			((System.ComponentModel.ISupportInitialize)dataSetData).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
