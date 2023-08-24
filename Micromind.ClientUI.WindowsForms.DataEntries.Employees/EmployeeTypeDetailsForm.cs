using Infragistics.Win;
using Infragistics.Win.AppStyling.Runtime;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class EmployeeTypeDetailsForm : Form, IForm
	{
		private EmployeeTypeData currentData;

		private const string TABLENAME_CONST = "Employee_Type";

		private const string IDFIELD_CONST = "TypeID";

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

		private MMTextBox textBoxAccountName;

		private UltraFormattedLinkLabel linkLabelARAccount;

		private AllAccountsComboBox comboBoxARAccount;

		private ToolStripButton toolStripButtonInformation;

		private GroupBox groupBox1;

		private GroupBox groupBox2;

		private AppStylistRuntime appStylistRuntime1;

		private XPButton buttonAddLeave;

		private XPButton buttonAddOT;

		private ListBox checkedListLeaveTypes;

		private EOSRuleComboBox ComboBoxeosRule;

		private RadioButton radioButtonOA;

		private RadioButton radioButtonCD;

		private RadioButton radioButtonDefault;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private HolidayCalendarComboBox comboBoxHolidayCalendar;

		private MMLabel mmLabel3;

		private CheckBox checkBoxPayroll;

		private CheckedListBox checkedListOTTypes;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5019;

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

		public EmployeeTypeDetailsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EmployeeTypeDetailsForm_Load;
		}

		private void LoadCheckListBoxes()
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.LeaveTypeSystem.GetLeaveTypeComboList();
			checkedListLeaveTypes.DataSource = dataSet.Tables[0];
			checkedListLeaveTypes.DisplayMember = "Name";
			checkedListLeaveTypes.ValueMember = "Code";
			dataSet = new DataSet();
			dataSet = Factory.OverTimeSystem.GetOverTimeComboList();
			checkedListOTTypes.DataSource = dataSet.Tables[0];
			checkedListOTTypes.DisplayMember = "Name";
			checkedListOTTypes.ValueMember = "Code";
		}

		private bool GetData()
		{
			checked
			{
				try
				{
					if (currentData == null || isNewRecord)
					{
						currentData = new EmployeeTypeData();
					}
					DataRow dataRow = (!isNewRecord) ? currentData.EmployeeTypeTable.Rows[0] : currentData.EmployeeTypeTable.NewRow();
					dataRow.BeginEdit();
					_ = checkedListOTTypes.CheckedItems;
					string a = "";
					for (int i = 0; i < checkedListOTTypes.Items.Count; i++)
					{
						if (checkedListOTTypes.GetItemChecked(i) && a == "")
						{
							a = (string)(dataRow["DefaultOTTypeID"] = checkedListOTTypes.Items[i].ToString());
						}
					}
					dataRow["TypeID"] = textBoxCode.Text.Trim();
					dataRow["TypeName"] = textBoxName.Text.Trim();
					dataRow["Note"] = textBoxNote.Text;
					dataRow["Inactive"] = checkBoxInactive.Checked;
					dataRow["IsPayroll"] = checkBoxPayroll.Checked;
					dataRow["EOSID"] = ComboBoxeosRule.SelectedID;
					if (comboBoxHolidayCalendar.SelectedID != "")
					{
						dataRow["CalendarID"] = comboBoxHolidayCalendar.SelectedID;
					}
					else
					{
						dataRow["CalendarID"] = DBNull.Value;
					}
					if (comboBoxARAccount.SelectedID != "")
					{
						dataRow["AccountID"] = comboBoxARAccount.SelectedID;
					}
					else
					{
						dataRow["AccountID"] = DBNull.Value;
					}
					if (radioButtonOA.Checked)
					{
						dataRow["LeaveSelection"] = "OA";
					}
					else if (radioButtonCD.Checked)
					{
						dataRow["LeaveSelection"] = "CD";
					}
					else if (radioButtonDefault.Checked)
					{
						dataRow["LeaveSelection"] = DBNull.Value;
					}
					dataRow.EndEdit();
					if (isNewRecord)
					{
						currentData.EmployeeTypeTable.Rows.Add(dataRow);
					}
					currentData.EmployeeTypeDetailTable.Rows.Clear();
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("TypeID");
					dataTable.Columns.Add("LeaveTypeID");
					foreach (object item in checkedListLeaveTypes.Items)
					{
						NameValue nameValue = item as NameValue;
						dataTable.Rows.Add(textBoxCode.Text.Trim(), nameValue.Name);
					}
					currentData.EmployeeTypeDetailTable.Merge(dataTable, preserveChanges: false, MissingSchemaAction.Add);
					DataTable dataTable2 = new DataTable();
					dataTable2.Columns.Add("TypeID");
					dataTable2.Columns.Add("OTTypeID");
					int num = 0;
					foreach (object item2 in checkedListOTTypes.Items)
					{
						_ = item2;
						string text2 = checkedListOTTypes.Items[num].ToString();
						dataTable2.Rows.Add(textBoxCode.Text.Trim(), text2);
						num++;
					}
					currentData.EmployeeTypeDetailTable.Merge(dataTable2, preserveChanges: false, MissingSchemaAction.Add);
					return true;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
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
					currentData = Factory.EmployeeTypeSystem.GetEmployeeTypeByID(id.Trim());
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
			if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
			{
				return;
			}
			DataRow dataRow = currentData.Tables[0].Rows[0];
			textBoxCode.Text = dataRow["TypeID"].ToString();
			textBoxName.Text = dataRow["TypeName"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			checkBoxInactive.Checked = bool.Parse(dataRow["Inactive"].ToString());
			checkBoxPayroll.Checked = bool.Parse(dataRow["IsPayroll"].ToString());
			RadioButton radioButton = radioButtonOA;
			bool @checked = radioButtonCD.Checked = false;
			radioButton.Checked = @checked;
			comboBoxHolidayCalendar.SelectedID = dataRow["CalendarID"].ToString();
			if (dataRow["IsPayroll"] != DBNull.Value)
			{
				comboBoxARAccount.SelectedID = dataRow["AccountID"].ToString();
			}
			else
			{
				comboBoxARAccount.Clear();
			}
			if (dataRow["EOSID"] != DBNull.Value)
			{
				ComboBoxeosRule.SelectedID = dataRow["EOSID"].ToString();
			}
			else
			{
				ComboBoxeosRule.Clear();
			}
			if (dataRow["LeaveSelection"].ToString().Trim() == "OA")
			{
				radioButtonOA.Checked = true;
			}
			else if (dataRow["LeaveSelection"].ToString().Trim() == "CD")
			{
				radioButtonCD.Checked = true;
			}
			else if (!(dataRow["LeaveSelection"].ToString().Trim() == ""))
			{
				RadioButton radioButton2 = radioButtonOA;
				@checked = (radioButtonCD.Checked = false);
				radioButton2.Checked = @checked;
			}
			checkedListLeaveTypes.Items.Clear();
			checkedListOTTypes.Items.Clear();
			if (currentData.Tables.Contains("Employee_Type_Detail"))
			{
				foreach (DataRow row in currentData.Tables["Employee_Type_Detail"].Rows)
				{
					NameValue item = new NameValue(row["LeaveTypeID"].ToString(), row["LeaveTypeName"].ToString());
					if (!(row["LeaveTypeID"].ToString() == ""))
					{
						checkedListLeaveTypes.Items.Add(item);
					}
				}
				foreach (DataRow row2 in currentData.Tables["Employee_Type_Detail"].Rows)
				{
					NameValue item2 = new NameValue(row2["OTTypeID"].ToString(), row2["OverTimeName"].ToString());
					if (!(row2["OTTypeID"].ToString() == ""))
					{
						checkedListOTTypes.Items.Add(item2);
					}
				}
			}
			for (int i = 0; i < checkedListOTTypes.Items.Count; i = checked(i + 1))
			{
				if (checkedListOTTypes.Items[i].ToString() == dataRow["DefaultOTTypeID"].ToString())
				{
					checkedListOTTypes.SetItemChecked(i, value: true);
				}
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
					flag = Factory.EmployeeTypeSystem.CreateEmployeeType(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.EmployeeType, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EmployeeTypeSystem.UpdateEmployeeType(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Employee_Type", "TypeID", textBoxCode.Text.Trim()))
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
			textBoxCode.Clear();
			textBoxName.Clear();
			textBoxNote.Clear();
			comboBoxARAccount.Clear();
			checkBoxInactive.Checked = false;
			checkedListLeaveTypes.Items.Clear();
			checkedListOTTypes.Items.Clear();
			ComboBoxeosRule.Clear();
			comboBoxHolidayCalendar.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void EmployeeTypeTypeDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void EmployeeTypeTypeDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.EmployeeTypeSystem.DeleteEmployeeType(textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Employee_Type", "TypeID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Employee_Type", "TypeID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Employee_Type", "TypeID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Employee_Type", "TypeID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Employee_Type", "TypeID", toolStripTextBoxFind.Text.Trim()))
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

		private void AccountTypeDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void EmployeeTypeDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
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
			new FormHelper().ShowList(DataComboType.EmployeeType);
		}

		private void linkLabelARAccount_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.SelectedID);
		}

		private void buttonAddLeave_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> list = new List<string>();
			dataSet = Factory.LeaveTypeSystem.GetLeaveTypeComboList();
			dataSet.Tables[0].Columns["Name"].ColumnName = "Doc ID";
			dataSet.Tables[0].Columns["Code"].ColumnName = "Number";
			foreach (NameValue item3 in checkedListLeaveTypes.Items)
			{
				if (item3.ID != null && item3.ID != "")
				{
					string item = item3.ID + item3.Name;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			int num = 0;
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedDocuments = list;
			selectDocumentDialog.Text = "Select Leave Type";
			DialogResult num2 = selectDocumentDialog.ShowDialog(this);
			checkedListLeaveTypes.Items.Clear();
			if (num2 == DialogResult.OK)
			{
				list = selectDocumentDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Doc ID"].Value.ToString();
					string text2 = selectedRow.Cells["Number"].Value.ToString();
					if (bool.Parse(Factory.DatabaseSystem.GetFieldValue("Leave_Type", "IsAnnual", "LeaveTypeID", text2).ToString()))
					{
						num = checked(num + 1);
					}
					if (num > 1)
					{
						ErrorHelper.InformationMessage("Already Annual Leave Type added.");
					}
					else
					{
						bool flag = false;
						foreach (NameValue item4 in checkedListLeaveTypes.Items)
						{
							if (item4.ID + item4.Name == text + text2)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							NameValue item2 = new NameValue(text2, text);
							checkedListLeaveTypes.Items.Add(item2);
						}
					}
				}
			}
		}

		private void buttonAddOT_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> list = new List<string>();
			dataSet = Factory.OverTimeSystem.GetOverTimeComboList();
			dataSet.Tables[0].Columns["Name"].ColumnName = "Doc ID";
			dataSet.Tables[0].Columns["Code"].ColumnName = "Number";
			foreach (NameValue item3 in checkedListOTTypes.Items)
			{
				if (item3.ID != null && item3.ID != "")
				{
					string item = item3.ID + item3.Name;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedDocuments = list;
			selectDocumentDialog.Text = "Select OT Type";
			DialogResult num = selectDocumentDialog.ShowDialog(this);
			checkedListOTTypes.Items.Clear();
			if (num == DialogResult.OK)
			{
				list = selectDocumentDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Doc ID"].Value.ToString();
					string text2 = selectedRow.Cells["Number"].Value.ToString();
					bool flag = false;
					foreach (NameValue item4 in checkedListOTTypes.Items)
					{
						if (item4.ID + item4.Name == text + text2)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						NameValue item2 = new NameValue(text2, text);
						checkedListOTTypes.Items.Add(item2);
					}
				}
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditEOSRule(ComboBoxeosRule.SelectedID);
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditHolidayCalendar(comboBoxHolidayCalendar.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.EmployeeTypeDetailsForm));
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
			labelCode = new Micromind.UISupport.MMLabel();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			textBoxAccountName = new Micromind.UISupport.MMTextBox();
			linkLabelARAccount = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			groupBox1 = new System.Windows.Forms.GroupBox();
			buttonAddLeave = new Micromind.UISupport.XPButton();
			checkedListLeaveTypes = new System.Windows.Forms.ListBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			checkedListOTTypes = new System.Windows.Forms.CheckedListBox();
			buttonAddOT = new Micromind.UISupport.XPButton();
			appStylistRuntime1 = new Infragistics.Win.AppStyling.Runtime.AppStylistRuntime(components);
			radioButtonOA = new System.Windows.Forms.RadioButton();
			radioButtonCD = new System.Windows.Forms.RadioButton();
			radioButtonDefault = new System.Windows.Forms.RadioButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			checkBoxPayroll = new System.Windows.Forms.CheckBox();
			comboBoxHolidayCalendar = new Micromind.DataControls.HolidayCalendarComboBox();
			ComboBoxeosRule = new Micromind.DataControls.EOSRuleComboBox();
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxHolidayCalendar).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxeosRule).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
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
			toolStrip1.Size = new System.Drawing.Size(670, 31);
			toolStrip1.TabIndex = 14;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 28);
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
			toolStripButtonFind.Size = new System.Drawing.Size(55, 28);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 367);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(670, 40);
			panelButtons.TabIndex = 13;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(670, 1);
			linePanelDown.TabIndex = 0;
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
			xpButton1.Location = new System.Drawing.Point(560, 8);
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
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(15, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(74, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Class Code:";
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.Location = new System.Drawing.Point(113, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(160, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(15, 58);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(77, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Class Name:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.Location = new System.Drawing.Point(113, 58);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(329, 20);
			textBoxName.TabIndex = 2;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(15, 81);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(33, 13);
			mmLabel4.TabIndex = 9;
			mmLabel4.Text = "Note:";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(113, 80);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(329, 20);
			textBoxNote.TabIndex = 3;
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(279, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.CustomReportFieldName = "";
			textBoxAccountName.CustomReportKey = "";
			textBoxAccountName.CustomReportValueType = 1;
			textBoxAccountName.IsComboTextBox = false;
			textBoxAccountName.Location = new System.Drawing.Point(113, 124);
			textBoxAccountName.MaxLength = 255;
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(330, 20);
			textBoxAccountName.TabIndex = 5;
			textBoxAccountName.TabStop = false;
			linkLabelARAccount.AutoSize = true;
			linkLabelARAccount.Location = new System.Drawing.Point(15, 104);
			linkLabelARAccount.Name = "linkLabelARAccount";
			linkLabelARAccount.Size = new System.Drawing.Size(47, 14);
			linkLabelARAccount.TabIndex = 17;
			linkLabelARAccount.TabStop = true;
			linkLabelARAccount.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelARAccount.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelARAccount.Value = "Account:";
			appearance.ForeColor = System.Drawing.Color.Blue;
			linkLabelARAccount.VisitedLinkAppearance = appearance;
			linkLabelARAccount.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelARAccount_LinkClicked);
			groupBox1.Controls.Add(buttonAddLeave);
			groupBox1.Controls.Add(checkedListLeaveTypes);
			groupBox1.Location = new System.Drawing.Point(113, 153);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(261, 150);
			groupBox1.TabIndex = 8;
			groupBox1.TabStop = false;
			groupBox1.Text = "Leave Types";
			buttonAddLeave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAddLeave.BackColor = System.Drawing.Color.DarkGray;
			buttonAddLeave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAddLeave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAddLeave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAddLeave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAddLeave.Location = new System.Drawing.Point(187, 13);
			buttonAddLeave.Name = "buttonAddLeave";
			buttonAddLeave.Size = new System.Drawing.Size(68, 24);
			buttonAddLeave.TabIndex = 0;
			buttonAddLeave.Text = "+ Leave";
			buttonAddLeave.UseVisualStyleBackColor = false;
			buttonAddLeave.Click += new System.EventHandler(buttonAddLeave_Click);
			checkedListLeaveTypes.FormattingEnabled = true;
			checkedListLeaveTypes.Location = new System.Drawing.Point(4, 39);
			checkedListLeaveTypes.MultiColumn = true;
			checkedListLeaveTypes.Name = "checkedListLeaveTypes";
			checkedListLeaveTypes.Size = new System.Drawing.Size(252, 108);
			checkedListLeaveTypes.TabIndex = 1;
			groupBox2.Controls.Add(checkedListOTTypes);
			groupBox2.Controls.Add(buttonAddOT);
			groupBox2.Location = new System.Drawing.Point(403, 153);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(261, 150);
			groupBox2.TabIndex = 9;
			groupBox2.TabStop = false;
			groupBox2.Text = "OT Types ----------(Check Default)";
			checkedListOTTypes.FormattingEnabled = true;
			checkedListOTTypes.Location = new System.Drawing.Point(5, 37);
			checkedListOTTypes.Name = "checkedListOTTypes";
			checkedListOTTypes.Size = new System.Drawing.Size(252, 109);
			checkedListOTTypes.TabIndex = 145;
			buttonAddOT.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAddOT.BackColor = System.Drawing.Color.DarkGray;
			buttonAddOT.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAddOT.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAddOT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAddOT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAddOT.Location = new System.Drawing.Point(187, 13);
			buttonAddOT.Name = "buttonAddOT";
			buttonAddOT.Size = new System.Drawing.Size(68, 24);
			buttonAddOT.TabIndex = 0;
			buttonAddOT.Text = "+ OT";
			buttonAddOT.UseVisualStyleBackColor = false;
			buttonAddOT.Click += new System.EventHandler(buttonAddOT_Click);
			radioButtonOA.AutoSize = true;
			radioButtonOA.Location = new System.Drawing.Point(535, 59);
			radioButtonOA.Name = "radioButtonOA";
			radioButtonOA.Size = new System.Drawing.Size(82, 17);
			radioButtonOA.TabIndex = 6;
			radioButtonOA.TabStop = true;
			radioButtonOA.Text = "On Account";
			radioButtonOA.UseVisualStyleBackColor = true;
			radioButtonCD.AutoSize = true;
			radioButtonCD.Location = new System.Drawing.Point(535, 86);
			radioButtonCD.Name = "radioButtonCD";
			radioButtonCD.Size = new System.Drawing.Size(94, 17);
			radioButtonCD.TabIndex = 7;
			radioButtonCD.TabStop = true;
			radioButtonCD.Text = "Calendar Days";
			radioButtonCD.UseVisualStyleBackColor = true;
			radioButtonDefault.AutoSize = true;
			radioButtonDefault.Location = new System.Drawing.Point(537, 110);
			radioButtonDefault.Name = "radioButtonDefault";
			radioButtonDefault.Size = new System.Drawing.Size(59, 17);
			radioButtonDefault.TabIndex = 140;
			radioButtonDefault.TabStop = true;
			radioButtonDefault.Text = "Default";
			radioButtonDefault.UseVisualStyleBackColor = true;
			radioButtonDefault.Visible = false;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(15, 329);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(54, 14);
			ultraFormattedLinkLabel1.TabIndex = 141;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "EOS Rule:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(15, 307);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(87, 14);
			ultraFormattedLinkLabel14.TabIndex = 143;
			ultraFormattedLinkLabel14.TabStop = true;
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Holiday Calendar:";
			appearance3.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance3;
			ultraFormattedLinkLabel14.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel14_LinkClicked);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(461, 60);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(73, 13);
			mmLabel3.TabIndex = 144;
			mmLabel3.Text = "Leave Based:";
			checkBoxPayroll.AutoSize = true;
			checkBoxPayroll.Location = new System.Drawing.Point(300, 333);
			checkBoxPayroll.Name = "checkBoxPayroll";
			checkBoxPayroll.Size = new System.Drawing.Size(83, 17);
			checkBoxPayroll.TabIndex = 12;
			checkBoxPayroll.Text = "Is on Payroll";
			checkBoxPayroll.UseVisualStyleBackColor = true;
			comboBoxHolidayCalendar.Assigned = false;
			comboBoxHolidayCalendar.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxHolidayCalendar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxHolidayCalendar.CustomReportFieldName = "";
			comboBoxHolidayCalendar.CustomReportKey = "";
			comboBoxHolidayCalendar.CustomReportValueType = 1;
			comboBoxHolidayCalendar.DescriptionTextBox = null;
			appearance4.BackColor = System.Drawing.SystemColors.Window;
			appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxHolidayCalendar.DisplayLayout.Appearance = appearance4;
			comboBoxHolidayCalendar.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxHolidayCalendar.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance5.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance5.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance5.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.Appearance = appearance5;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.BandLabelAppearance = appearance6;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance7.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance7.BackColor2 = System.Drawing.SystemColors.Control;
			appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxHolidayCalendar.DisplayLayout.GroupByBox.PromptAppearance = appearance7;
			comboBoxHolidayCalendar.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxHolidayCalendar.DisplayLayout.MaxRowScrollRegions = 1;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxHolidayCalendar.DisplayLayout.Override.ActiveCellAppearance = appearance8;
			appearance9.BackColor = System.Drawing.SystemColors.Highlight;
			appearance9.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxHolidayCalendar.DisplayLayout.Override.ActiveRowAppearance = appearance9;
			comboBoxHolidayCalendar.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxHolidayCalendar.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.Override.CardAreaAppearance = appearance10;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			appearance11.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellAppearance = appearance11;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxHolidayCalendar.DisplayLayout.Override.CellPadding = 0;
			appearance12.BackColor = System.Drawing.SystemColors.Control;
			appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance12.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance12.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxHolidayCalendar.DisplayLayout.Override.GroupByRowAppearance = appearance12;
			appearance13.TextHAlignAsString = "Left";
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderAppearance = appearance13;
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxHolidayCalendar.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.BorderColor = System.Drawing.Color.Silver;
			comboBoxHolidayCalendar.DisplayLayout.Override.RowAppearance = appearance14;
			comboBoxHolidayCalendar.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance15.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxHolidayCalendar.DisplayLayout.Override.TemplateAddRowAppearance = appearance15;
			comboBoxHolidayCalendar.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxHolidayCalendar.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxHolidayCalendar.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxHolidayCalendar.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxHolidayCalendar.Editable = true;
			comboBoxHolidayCalendar.FilterString = "";
			comboBoxHolidayCalendar.HasAllAccount = false;
			comboBoxHolidayCalendar.HasCustom = false;
			comboBoxHolidayCalendar.IsDataLoaded = false;
			comboBoxHolidayCalendar.Location = new System.Drawing.Point(113, 307);
			comboBoxHolidayCalendar.MaxDropDownItems = 12;
			comboBoxHolidayCalendar.Name = "comboBoxHolidayCalendar";
			comboBoxHolidayCalendar.ShowInactiveItems = false;
			comboBoxHolidayCalendar.ShowQuickAdd = true;
			comboBoxHolidayCalendar.Size = new System.Drawing.Size(100, 20);
			comboBoxHolidayCalendar.TabIndex = 10;
			comboBoxHolidayCalendar.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxeosRule.Assigned = false;
			ComboBoxeosRule.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxeosRule.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxeosRule.CustomReportFieldName = "";
			ComboBoxeosRule.CustomReportKey = "";
			ComboBoxeosRule.CustomReportValueType = 1;
			ComboBoxeosRule.DescriptionTextBox = null;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxeosRule.DisplayLayout.Appearance = appearance16;
			ComboBoxeosRule.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxeosRule.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance17.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance17.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance17.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxeosRule.DisplayLayout.GroupByBox.Appearance = appearance17;
			appearance18.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxeosRule.DisplayLayout.GroupByBox.BandLabelAppearance = appearance18;
			ComboBoxeosRule.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance19.BackColor2 = System.Drawing.SystemColors.Control;
			appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxeosRule.DisplayLayout.GroupByBox.PromptAppearance = appearance19;
			ComboBoxeosRule.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxeosRule.DisplayLayout.MaxRowScrollRegions = 1;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxeosRule.DisplayLayout.Override.ActiveCellAppearance = appearance20;
			appearance21.BackColor = System.Drawing.SystemColors.Highlight;
			appearance21.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxeosRule.DisplayLayout.Override.ActiveRowAppearance = appearance21;
			ComboBoxeosRule.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxeosRule.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxeosRule.DisplayLayout.Override.CardAreaAppearance = appearance22;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			appearance23.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxeosRule.DisplayLayout.Override.CellAppearance = appearance23;
			ComboBoxeosRule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxeosRule.DisplayLayout.Override.CellPadding = 0;
			appearance24.BackColor = System.Drawing.SystemColors.Control;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxeosRule.DisplayLayout.Override.GroupByRowAppearance = appearance24;
			appearance25.TextHAlignAsString = "Left";
			ComboBoxeosRule.DisplayLayout.Override.HeaderAppearance = appearance25;
			ComboBoxeosRule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxeosRule.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.Color.Silver;
			ComboBoxeosRule.DisplayLayout.Override.RowAppearance = appearance26;
			ComboBoxeosRule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxeosRule.DisplayLayout.Override.TemplateAddRowAppearance = appearance27;
			ComboBoxeosRule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxeosRule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxeosRule.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxeosRule.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxeosRule.Editable = true;
			ComboBoxeosRule.FilterString = "";
			ComboBoxeosRule.HasAllAccount = false;
			ComboBoxeosRule.HasCustom = false;
			ComboBoxeosRule.IsDataLoaded = false;
			ComboBoxeosRule.Location = new System.Drawing.Point(113, 329);
			ComboBoxeosRule.MaxDropDownItems = 12;
			ComboBoxeosRule.Name = "ComboBoxeosRule";
			ComboBoxeosRule.ShowInactiveItems = false;
			ComboBoxeosRule.ShowQuickAdd = true;
			ComboBoxeosRule.Size = new System.Drawing.Size(160, 20);
			ComboBoxeosRule.TabIndex = 11;
			ComboBoxeosRule.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = textBoxAccountName;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance28;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance29.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance29;
			appearance30.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance30;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance31.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance31.BackColor2 = System.Drawing.SystemColors.Control;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance31;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance32;
			appearance33.BackColor = System.Drawing.SystemColors.Highlight;
			appearance33.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance33;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance34;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			appearance35.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance35;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance36.BackColor = System.Drawing.SystemColors.Control;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance36;
			appearance37.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance37;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance38;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance39.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance39;
			comboBoxARAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxARAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxARAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxARAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxARAccount.Editable = true;
			comboBoxARAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxARAccount.FilterString = "";
			comboBoxARAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxARAccount.FilterSysDocID = "";
			comboBoxARAccount.HasAllAccount = false;
			comboBoxARAccount.HasCustom = false;
			comboBoxARAccount.IsDataLoaded = false;
			comboBoxARAccount.Location = new System.Drawing.Point(113, 102);
			comboBoxARAccount.MaxDropDownItems = 12;
			comboBoxARAccount.MaxLength = 64;
			comboBoxARAccount.Name = "comboBoxARAccount";
			comboBoxARAccount.ShowInactiveItems = false;
			comboBoxARAccount.ShowQuickAdd = true;
			comboBoxARAccount.Size = new System.Drawing.Size(195, 20);
			comboBoxARAccount.TabIndex = 4;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(670, 407);
			base.Controls.Add(checkBoxPayroll);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(ultraFormattedLinkLabel14);
			base.Controls.Add(comboBoxHolidayCalendar);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(radioButtonDefault);
			base.Controls.Add(radioButtonCD);
			base.Controls.Add(radioButtonOA);
			base.Controls.Add(ComboBoxeosRule);
			base.Controls.Add(groupBox1);
			base.Controls.Add(groupBox2);
			base.Controls.Add(textBoxAccountName);
			base.Controls.Add(linkLabelARAccount);
			base.Controls.Add(comboBoxARAccount);
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
			base.Name = "EmployeeTypeDetailsForm";
			Text = "Employee Class";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountTypeDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxHolidayCalendar).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxeosRule).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
