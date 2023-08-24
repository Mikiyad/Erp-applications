using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.CRM
{
	public class TaskTypeForm : Form, IForm
	{
		private TaskTypeData currentData;

		private const string TABLENAME_CONST = "Task_Type";

		private const string IDFIELD_CONST = "TaskTypeID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private MMLabel mmLabel4;

		private MMTextBox textBoxNote;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private GroupBox groupBox3;

		private DataEntryGrid dataGridTaskSteps;

		private UserComboBox comboBoxGridUser;

		private TaskStepsComboBox comboBoxGridTaskSteps;

		private SysDocTypeComboBox comboBoxDocType;

		public ScreenAreas ScreenArea => ScreenAreas.CRM;

		public int ScreenID => 4012;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

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

		public TaskTypeForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += TaskTypeForm_Load;
			dataGridTaskSteps.AfterCellUpdate += dataGridTaskSteps_AfterCellUpdate;
			dataGridTaskSteps.HeaderClicked += dataGridTaskSteps_HeaderClicked;
			dataGridTaskSteps.AfterRowInsert += dataGridItems_AfterRowInsert;
			dataGridTaskSteps.AfterRowActivate += dataGridItems_AfterRowActivate;
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add("None");
			foreach (UltraGridRow row in dataGridTaskSteps.Rows)
			{
				if (!row.IsActiveRow)
				{
					valueList.ValueListItems.Add(checked(row.Index + 1));
				}
			}
			dataGridTaskSteps.ActiveRow.Cells["Prerequisite"].ValueList = valueList;
			if (dataGridTaskSteps.ActiveRow.Cells["Prerequisite"].Value.IsNullOrEmpty())
			{
				dataGridTaskSteps.ActiveRow.Cells["Prerequisite"].Value = "None";
			}
		}

		private void dataGridItems_AfterRowInsert(object sender, RowEventArgs e)
		{
			e.Row.Cells["No"].Value = checked(e.Row.Index + 1);
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new TaskTypeData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TaskTypeTable.Rows[0] : currentData.TaskTypeTable.NewRow();
				dataRow.BeginEdit();
				dataRow["TaskTypeID"] = textBoxCode.Text.Trim();
				dataRow["Name"] = textBoxName.Text.Trim();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.TaskTypeTable.Rows.Add(dataRow);
				}
				currentData.TaskTypeDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridTaskSteps.Rows)
				{
					DataRow dataRow2 = currentData.TaskTypeDetailTable.NewRow();
					dataRow2["TaskTypeID"] = textBoxCode.Text;
					dataRow2["TaskStepID"] = row.Cells["StepID"].Value.ToString();
					dataRow2[TaskTypeData.DESCRIPTION_FIELD] = row.Cells["Description"].Value.ToString();
					dataRow2[TaskTypeData.DEFAULTASSIGNEEID_FIELD] = row.Cells["DefaultAssignee"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["DaysAllowed"].Value.ToString()))
					{
						dataRow2[TaskTypeData.DAYSALLOWED_FIELD] = row.Cells["DaysAllowed"].Value.ToString();
					}
					else
					{
						dataRow2[TaskTypeData.DAYSALLOWED_FIELD] = 0;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2[TaskTypeData.PREREQUEST_FIELD] = row.Cells["PreRequisite"].Value.ToString();
					dataRow2[TaskTypeData.DOCTYPEID_FIELD] = row.Cells["DocType"].Value.ToString();
					dataRow2[TaskTypeData.DOCTYPENAME_FIELD] = row.Cells["DocType"].Text.ToString();
					currentData.TaskTypeDetailTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupTaskTypeGrid()
		{
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("No");
			dataTable.Columns.Add("StepID");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("DefaultAssignee");
			dataTable.Columns.Add("DaysAllowed", typeof(int));
			dataTable.Columns.Add("PreRequisite");
			dataTable.Columns.Add("DocType");
			dataGridTaskSteps.DataSource = dataTable;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellActivation = Activation.NoEdit;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].CharacterCasing = CharacterCasing.Upper;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].MaxLength = 64;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].ValueList = comboBoxGridTaskSteps;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Caption = "Step";
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DocType"].MaxLength = 64;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DocType"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DocType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DocType"].ValueList = comboBoxDocType;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].CharacterCasing = CharacterCasing.Upper;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].MaxLength = 64;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].ValueList = comboBoxGridUser;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DefaultAssignee"].Header.Caption = "Default Assignee";
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DaysAllowed"].Header.Caption = "Days Allowed";
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns[0].Width = checked(30 * dataGridTaskSteps.Width) / 100;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns[1].Width = checked(50 * dataGridTaskSteps.Width) / 100;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns[2].Width = checked(10 * dataGridTaskSteps.Width) / 100;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["PreRequisite"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			new ValueList();
			new ValueList().ValueListItems.Add(0, "1");
			dataGridTaskSteps.SetupUI();
		}

		private void dataGridTaskSteps_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridTaskSteps.ActiveRow != null && e.Cell.Column.Key == "StepID")
				{
					dataGridTaskSteps.ActiveRow.Cells["Description"].Value = comboBoxGridTaskSteps.SelectedName;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridTaskSteps_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridTaskSteps.ActiveRow == null)
			{
				return;
			}
			string text = "";
			if (ultraGridColumn != null && ultraGridColumn.Key == "StepID")
			{
				FormHelper formHelper = new FormHelper();
				text = dataGridTaskSteps.ActiveRow.Cells["StepID"].Value.ToString();
				if (!string.IsNullOrEmpty(text))
				{
					formHelper.EditTaskSteps(text);
				}
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.TaskTypeSystem.GetTaskTypeByID(id.Trim());
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
				ClearForm();
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables[0].Rows[0];
					textBoxCode.Text = dataRow["TaskTypeID"].ToString();
					textBoxName.Text = dataRow["Name"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
					}
					else
					{
						checkBoxInactive.Checked = false;
					}
					DataTable dataTable = dataGridTaskSteps.DataSource as DataTable;
					dataTable?.Rows.Clear();
					dataTable.BeginLoadData();
					foreach (DataRow row in currentData.TaskTypeDetailTable.Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["No"] = checked(int.Parse(row["RowIndex"].ToString()) + 1);
						dataRow3["StepID"] = row["TaskStepID"];
						dataRow3["Description"] = row[TaskTypeData.DESCRIPTION_FIELD];
						dataRow3["DefaultAssignee"] = row[TaskTypeData.DEFAULTASSIGNEEID_FIELD];
						if (!string.IsNullOrEmpty(row[TaskTypeData.DAYSALLOWED_FIELD].ToString()))
						{
							dataRow3["DaysAllowed"] = row[TaskTypeData.DAYSALLOWED_FIELD];
						}
						dataRow3["PreRequisite"] = row[TaskTypeData.PREREQUEST_FIELD];
						dataRow3["DocType"] = row[TaskTypeData.DOCTYPEID_FIELD];
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.EndLoadData();
					dataTable.AcceptChanges();
				}
			}
			catch
			{
				throw;
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.TaskTypeSystem.CreateTaskType(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.TaskType, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.TaskTypeSystem.UpdateTaskType(currentData);
				}
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Task_Type", "TaskTypeID", textBoxCode.Text.Trim()))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Task_Type", "TaskTypeID");
			textBoxName.Clear();
			textBoxNote.Clear();
			dataGridTaskSteps.Clear();
			checkBoxInactive.Checked = false;
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void ProductManufacturerGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ProductManufacturerGroupDetailsForm_Validated(object sender, EventArgs e)
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

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.TaskTypeSystem.DeleteTaskType(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.ReleaseType, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Task_Type", "TaskTypeID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Task_Type", "TaskTypeID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Task_Type", "TaskTypeID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Task_Type", "TaskTypeID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Task_Type", "TaskTypeID", toolStripTextBoxFind.Text.Trim()))
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

		private void RelaeseTypeForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetupTaskTypeGrid();
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
			new FormHelper().ShowList(DataComboType.TaskType);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxCode.Text, "", this);
			}
		}

		private void TaskTypeForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				SetupTaskTypeGrid();
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
			components = new System.ComponentModel.Container();
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.CRM.TaskTypeForm));
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
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			comboBoxDocType = new Micromind.DataControls.SysDocTypeComboBox();
			comboBoxGridUser = new Micromind.DataControls.UserComboBox();
			comboBoxGridTaskSteps = new Micromind.DataControls.TaskStepsComboBox();
			dataGridTaskSteps = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDocType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTaskSteps).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridTaskSteps).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
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
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1086, 31);
			toolStrip1.TabIndex = 0;
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 539);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1086, 40);
			panelButtons.TabIndex = 7;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1086, 1);
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
			xpButton1.Location = new System.Drawing.Point(976, 8);
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
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(310, 37);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 2;
			checkBoxInactive.Text = "Inactvie";
			checkBoxInactive.UseVisualStyleBackColor = true;
			groupBox3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			groupBox3.Controls.Add(comboBoxDocType);
			groupBox3.Controls.Add(comboBoxGridUser);
			groupBox3.Controls.Add(comboBoxGridTaskSteps);
			groupBox3.Controls.Add(dataGridTaskSteps);
			groupBox3.Location = new System.Drawing.Point(11, 110);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(1075, 423);
			groupBox3.TabIndex = 6;
			groupBox3.TabStop = false;
			groupBox3.Text = "Steps";
			comboBoxDocType.Assigned = false;
			comboBoxDocType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDocType.CustomReportFieldName = "";
			comboBoxDocType.CustomReportKey = "";
			comboBoxDocType.CustomReportValueType = 1;
			comboBoxDocType.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDocType.DisplayLayout.Appearance = appearance;
			comboBoxDocType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDocType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxDocType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocType.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxDocType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDocType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDocType.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDocType.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxDocType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDocType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDocType.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxDocType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDocType.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxDocType.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxDocType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDocType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxDocType.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxDocType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDocType.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxDocType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDocType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDocType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDocType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDocType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxDocType.Editable = true;
			comboBoxDocType.FilterString = "";
			comboBoxDocType.HasAllAccount = false;
			comboBoxDocType.HasCustom = false;
			comboBoxDocType.IsDataLoaded = false;
			comboBoxDocType.Location = new System.Drawing.Point(389, 61);
			comboBoxDocType.MaxDropDownItems = 12;
			comboBoxDocType.Name = "comboBoxDocType";
			comboBoxDocType.ShowInactiveItems = false;
			comboBoxDocType.ShowQuickAdd = true;
			comboBoxDocType.Size = new System.Drawing.Size(100, 20);
			comboBoxDocType.TabIndex = 3;
			comboBoxDocType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDocType.Visible = false;
			comboBoxGridUser.Assigned = false;
			comboBoxGridUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridUser.CustomReportFieldName = "";
			comboBoxGridUser.CustomReportKey = "";
			comboBoxGridUser.CustomReportValueType = 1;
			comboBoxGridUser.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridUser.DisplayLayout.Appearance = appearance13;
			comboBoxGridUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUser.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridUser.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridUser.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridUser.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridUser.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridUser.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridUser.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridUser.Editable = true;
			comboBoxGridUser.FilterString = "";
			comboBoxGridUser.HasAllAccount = false;
			comboBoxGridUser.HasCustom = false;
			comboBoxGridUser.IsDataLoaded = false;
			comboBoxGridUser.Location = new System.Drawing.Point(320, 110);
			comboBoxGridUser.MaxDropDownItems = 12;
			comboBoxGridUser.Name = "comboBoxGridUser";
			comboBoxGridUser.ShowInactiveItems = false;
			comboBoxGridUser.ShowQuickAdd = true;
			comboBoxGridUser.Size = new System.Drawing.Size(100, 20);
			comboBoxGridUser.TabIndex = 2;
			comboBoxGridUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridUser.Visible = false;
			comboBoxGridTaskSteps.Assigned = false;
			comboBoxGridTaskSteps.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridTaskSteps.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridTaskSteps.CustomReportFieldName = "";
			comboBoxGridTaskSteps.CustomReportKey = "";
			comboBoxGridTaskSteps.CustomReportValueType = 1;
			comboBoxGridTaskSteps.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridTaskSteps.DisplayLayout.Appearance = appearance25;
			comboBoxGridTaskSteps.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridTaskSteps.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridTaskSteps.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridTaskSteps.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridTaskSteps.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridTaskSteps.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridTaskSteps.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridTaskSteps.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridTaskSteps.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridTaskSteps.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridTaskSteps.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridTaskSteps.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridTaskSteps.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridTaskSteps.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridTaskSteps.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridTaskSteps.Editable = true;
			comboBoxGridTaskSteps.FilterString = "";
			comboBoxGridTaskSteps.HasAllAccount = false;
			comboBoxGridTaskSteps.HasCustom = false;
			comboBoxGridTaskSteps.IsDataLoaded = false;
			comboBoxGridTaskSteps.Location = new System.Drawing.Point(348, 91);
			comboBoxGridTaskSteps.MaxDropDownItems = 12;
			comboBoxGridTaskSteps.Name = "comboBoxGridTaskSteps";
			comboBoxGridTaskSteps.ShowInactiveItems = false;
			comboBoxGridTaskSteps.ShowQuickAdd = true;
			comboBoxGridTaskSteps.Size = new System.Drawing.Size(100, 20);
			comboBoxGridTaskSteps.TabIndex = 1;
			comboBoxGridTaskSteps.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridTaskSteps.Visible = false;
			dataGridTaskSteps.AllowAddNew = false;
			dataGridTaskSteps.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridTaskSteps.DisplayLayout.Appearance = appearance37;
			dataGridTaskSteps.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridTaskSteps.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTaskSteps.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridTaskSteps.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTaskSteps.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridTaskSteps.DisplayLayout.MaxColScrollRegions = 1;
			dataGridTaskSteps.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridTaskSteps.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridTaskSteps.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridTaskSteps.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridTaskSteps.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridTaskSteps.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridTaskSteps.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridTaskSteps.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridTaskSteps.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridTaskSteps.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridTaskSteps.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridTaskSteps.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridTaskSteps.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridTaskSteps.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridTaskSteps.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridTaskSteps.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridTaskSteps.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridTaskSteps.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridTaskSteps.IncludeLotItems = false;
			dataGridTaskSteps.LoadLayoutFailed = false;
			dataGridTaskSteps.Location = new System.Drawing.Point(3, 19);
			dataGridTaskSteps.Name = "dataGridTaskSteps";
			dataGridTaskSteps.ShowClearMenu = true;
			dataGridTaskSteps.ShowDeleteMenu = true;
			dataGridTaskSteps.ShowInsertMenu = true;
			dataGridTaskSteps.ShowMoveRowsMenu = true;
			dataGridTaskSteps.Size = new System.Drawing.Size(1066, 398);
			dataGridTaskSteps.TabIndex = 0;
			dataGridTaskSteps.Text = "dataEntryGrid1";
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(131, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(318, 20);
			textBoxNote.TabIndex = 5;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(131, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(318, 20);
			textBoxName.TabIndex = 3;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(131, 36);
			textBoxCode.MaxLength = 30;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(173, 20);
			textBoxCode.TabIndex = 1;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(8, 83);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(43, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 38);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(40, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Code:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(1086, 579);
			base.Controls.Add(groupBox3);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "TaskTypeForm";
			Text = "Task Type ";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(TaskTypeForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxDocType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUser).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTaskSteps).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridTaskSteps).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
