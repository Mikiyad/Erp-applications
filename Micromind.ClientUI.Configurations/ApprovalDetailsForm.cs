using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class ApprovalDetailsForm : Form, IForm
	{
		private ApprovalData currentData;

		private const string TABLENAME_CONST = "Approval";

		private const string IDFIELD_CONST = "ApprovalID";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private SysDocTypes doctype;

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

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private CheckBox checkBoxInactive;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ComboBox comboBoxObjectType;

		private MMLabel mmLabel2;

		private Panel panelTransaction;

		private RadioButton radioButtonAllDocs;

		private RadioButton radioButtonSpecific;

		private SysDocComboBox comboBoxSysDoc;

		private MMLabel mmLabel3;

		private Panel panelCard;

		private MMLabel mmLabel4;

		private DataEntryGrid dataGridItems;

		private UserComboBox comboBoxUser;

		private UserGroupComboBox comboBoxUserGroup;

		private ApprovalTransactionTypesComboBox comboBoxSysDocType;

		private ApprovalCardTypesComboBox comboBoxCardType;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private JobComboBox comboBoxJob;

		private UltraTabPageControl tabPageExpense;

		private CheckBox checkBoxNotAllowbyothers;

		private CheckBox checkBoxNoTransaction;

		private CheckBox checkBoxonPrint;

		public ScreenAreas ScreenArea => ScreenAreas.HR;

		public int ScreenID => 5021;

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

		public ApprovalDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxObjectType.SelectedIndex = 0;
			comboBoxCardType.LoadData();
			comboBoxSysDocType.LoadData();
			comboBoxSysDocType.Sorted = true;
			comboBoxCardType.Sorted = true;
		}

		private void AddEvents()
		{
			base.Load += ApprovalDetailsForm_Load;
			dataGridItems.AfterRowInsert += dataGridItems_AfterRowInsert;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			comboBoxObjectType.SelectedIndexChanged += comboBoxObjectType_SelectedIndexChanged;
			comboBoxUserGroup.LoadData(isReferesh: false);
			comboBoxUser.LoadData(isReferesh: false);
		}

		private void comboBoxObjectType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (checked(comboBoxObjectType.SelectedIndex + 1) == 1)
			{
				panelTransaction.Visible = true;
				panelCard.Visible = false;
			}
			else
			{
				panelTransaction.Visible = false;
				panelCard.Visible = true;
			}
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add("None");
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!row.IsActiveRow)
				{
					valueList.ValueListItems.Add(checked(row.Index + 1));
				}
			}
			dataGridItems.ActiveRow.Cells["Prerequisite"].ValueList = valueList;
			if (dataGridItems.ActiveRow.Cells["Prerequisite"].Value.IsNullOrEmpty())
			{
				dataGridItems.ActiveRow.Cells["Prerequisite"].Value = "None";
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "ApproverType")
			{
				if (int.Parse(e.Cell.Value.ToString()) == 1)
				{
					e.Cell.Row.Cells["ApproverID"].ValueList = comboBoxUser;
				}
				else
				{
					e.Cell.Row.Cells["ApproverID"].ValueList = comboBoxUserGroup;
				}
			}
		}

		private void dataGridItems_AfterRowInsert(object sender, RowEventArgs e)
		{
			e.Row.Cells["No"].Value = checked(e.Row.Index + 1);
			e.Row.Cells["ApproverType"].Value = 1;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ApprovalData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ApprovalTable.Rows[0] : currentData.ApprovalTable.NewRow();
				dataRow.BeginEdit();
				dataRow["ApprovalID"] = textBoxCode.Text.Trim();
				dataRow["ApprovalName"] = textBoxName.Text.Trim();
				dataRow["ApprovalType"] = (byte)1;
				dataRow["ObjectType"] = checked(comboBoxObjectType.SelectedIndex + 1);
				dataRow["NotifyonPrint"] = checkBoxonPrint.Checked;
				dataRow["AllowtoEdit"] = checkBoxNotAllowbyothers.Checked;
				dataRow["AllownextTransaction"] = checkBoxNoTransaction.Checked;
				if (checked(comboBoxObjectType.SelectedIndex + 1) == 1)
				{
					dataRow["ObjectID"] = (int)comboBoxSysDocType.SelectedID;
				}
				else
				{
					dataRow["ObjectID"] = (int)comboBoxCardType.SelectedID;
				}
				if (comboBoxSysDoc.SelectedID != "" && radioButtonSpecific.Checked)
				{
					dataRow["ObjectSysDocID"] = comboBoxSysDoc.SelectedID;
				}
				else
				{
					dataRow["ObjectSysDocID"] = DBNull.Value;
				}
				dataRow["IsInactive"] = checkBoxInactive.Checked;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.ApprovalTable.Rows.Add(dataRow);
				}
				DataTable approvalLevelTable = currentData.ApprovalLevelTable;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = approvalLevelTable.NewRow();
					dataRow2["ApproverType"] = int.Parse(row.Cells["ApproverType"].Value.ToString());
					dataRow2["ApproverID"] = row.Cells["ApproverID"].Value.ToString();
					dataRow2["ApprovalID"] = textBoxCode.Text;
					dataRow2["ApprovalType"] = 1;
					dataRow2["RowIndex"] = row.Index;
					if (row.Cells["PreRequisite"].Value.IsNullOrEmpty() || row.Cells["PreRequisite"].Value.ToString() == "None")
					{
						dataRow2["PreRequisiteIndex"] = DBNull.Value;
					}
					else
					{
						dataRow2["PreRequisiteIndex"] = row.Cells["PreRequisite"].Value.ToString();
					}
					dataRow2["Condition"] = row.Cells["Condition"].Value.ToString();
					dataRow2.EndEdit();
					approvalLevelTable.Rows.Add(dataRow2);
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

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					currentData = Factory.ApprovalSystem.GetApprovalByID(ApprovalTypes.Approval, id.Trim());
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
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
				textBoxCode.Text = dataRow["ApprovalID"].ToString();
				textBoxName.Text = dataRow["ApprovalName"].ToString();
				if (dataRow["NotifyonPrint"] != DBNull.Value)
				{
					checkBoxonPrint.Checked = bool.Parse(dataRow["NotifyonPrint"].ToString());
				}
				if (dataRow["AllowtoEdit"] != DBNull.Value)
				{
					checkBoxNotAllowbyothers.Checked = bool.Parse(dataRow["AllowtoEdit"].ToString());
				}
				if (dataRow["AllownextTransaction"] != DBNull.Value)
				{
					checkBoxNoTransaction.Checked = bool.Parse(dataRow["AllownextTransaction"].ToString());
				}
				int num = int.Parse(dataRow["ObjectType"].ToString());
				comboBoxObjectType.SelectedIndex = checked(num - 1);
				if (num == 1)
				{
					comboBoxSysDocType.SelectedID = (SysDocTypes)int.Parse(dataRow["ObjectID"].ToString());
					panelTransaction.Visible = true;
					panelCard.Visible = false;
				}
				else
				{
					comboBoxCardType.SelectedID = (DataComboType)int.Parse(dataRow["ObjectID"].ToString());
					panelTransaction.Visible = false;
					panelCard.Visible = true;
				}
				if (dataRow["ObjectSysDocID"] != DBNull.Value)
				{
					comboBoxSysDoc.SelectedID = dataRow["ObjectSysDocID"].ToString();
					radioButtonSpecific.Checked = true;
				}
				else
				{
					comboBoxSysDoc.SelectedID = "";
					radioButtonAllDocs.Checked = true;
				}
				if (dataRow["IsInactive"] != DBNull.Value)
				{
					checkBoxInactive.Checked = bool.Parse(dataRow["IsInactive"].ToString());
				}
				else
				{
					checkBoxInactive.Checked = false;
				}
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				foreach (DataRow row in currentData.ApprovalLevelTable.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["No"] = checked(int.Parse(row["RowIndex"].ToString()) + 1);
					dataRow3["ApproverType"] = int.Parse(row["ApproverType"].ToString());
					dataRow3["ApproverID"] = row["ApproverID"].ToString();
					dataRow3["PreRequisite"] = row["PreRequisiteIndex"].ToString();
					dataRow3["Condition"] = row["Condition"].ToString();
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
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
				bool flag = (!isNewRecord) ? Factory.ApprovalSystem.UpdateApproval(currentData) : Factory.ApprovalSystem.CreateApproval(currentData);
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
			if (isNewRecord && Factory.DatabaseSystem.ExistFieldValue("Approval", "ApprovalID", textBoxCode.Text.Trim(), "ApprovalType", 1))
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
			textBoxCode.Text = PublicFunctions.GetNextCardNumber("Approval", "ApprovalID", "ApprovalType", 1.ToString());
			textBoxName.Clear();
			checkBoxInactive.Checked = false;
			comboBoxCardType.Clear();
			comboBoxSysDoc.Clear();
			comboBoxSysDocType.Clear();
			radioButtonAllDocs.Checked = true;
			checkBoxonPrint.Checked = false;
			checkBoxNotAllowbyothers.Checked = false;
			checkBoxNoTransaction.Checked = false;
			(dataGridItems.DataSource as DataTable)?.Rows.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("No");
				dataTable.Columns.Add("ApproverType");
				dataTable.Columns.Add("ApproverID");
				dataTable.Columns.Add("PreRequisite");
				dataTable.Columns.Add("Condition");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["NO"].Width = 50;
				dataGridItems.DisplayLayout.Bands[0].Columns["No"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "User");
				valueList.ValueListItems.Add(2, "User Role");
				dataGridItems.DisplayLayout.Bands[0].Columns["ApproverType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["ApproverType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["ApproverType"].Header.Caption = "Approver Type";
				dataGridItems.DisplayLayout.Bands[0].Columns["ApproverID"].ValueList = comboBoxUser;
				dataGridItems.DisplayLayout.Bands[0].Columns["ApproverID"].ValueList = comboBoxUserGroup;
				dataGridItems.DisplayLayout.Bands[0].Columns["PreRequisite"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				valueList = new ValueList();
				valueList.ValueListItems.Add(0, "1");
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void ApprovalGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void ApprovalGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.ApprovalSystem.DeleteApproval(ApprovalTypes.Approval, textBoxCode.Text);
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
			LoadData(DatabaseHelper.GetNextID("Approval", "ApprovalID", textBoxCode.Text, "ApprovalType", ((byte)1).ToString()));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Approval", "ApprovalID", textBoxCode.Text, "ApprovalType", ((byte)1).ToString()));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Approval", "ApprovalID", "ApprovalType", ((byte)1).ToString()));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Approval", "ApprovalID", "ApprovalType", ((byte)1).ToString()));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Approval", "ApprovalID", toolStripTextBoxFind.Text.Trim(), "ApprovalType", (byte)1))
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

		private void ApprovalDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
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
				dataGridItems.LoadLayoutFailed = true;
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
			new FormHelper().ShowList(DataComboType.Approval);
		}

		private void comboBoxSysDocType_SelectedIndexChanged(object sender, EventArgs e)
		{
			doctype = comboBoxSysDocType.SelectedID;
			if (IsNewRecord)
			{
				comboBoxSysDoc.FilterByType(doctype);
			}
		}

		private void radioButtonSpecific_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxSysDoc.Enabled = false;
			if (radioButtonSpecific.Checked)
			{
				comboBoxSysDoc.Enabled = true;
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.ApprovalDetailsForm));
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxNotAllowbyothers = new System.Windows.Forms.CheckBox();
			checkBoxNoTransaction = new System.Windows.Forms.CheckBox();
			checkBoxonPrint = new System.Windows.Forms.CheckBox();
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
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkBoxInactive = new System.Windows.Forms.CheckBox();
			comboBoxObjectType = new System.Windows.Forms.ComboBox();
			panelTransaction = new System.Windows.Forms.Panel();
			radioButtonAllDocs = new System.Windows.Forms.RadioButton();
			radioButtonSpecific = new System.Windows.Forms.RadioButton();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			panelCard = new System.Windows.Forms.Panel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxUserGroup = new Micromind.DataControls.UserGroupComboBox();
			comboBoxUser = new Micromind.DataControls.UserComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxCardType = new Micromind.DataControls.ApprovalCardTypesComboBox();
			comboBoxSysDocType = new Micromind.DataControls.ApprovalTransactionTypesComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			tabPageItems.SuspendLayout();
			tabPageExpense.SuspendLayout();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelTransaction.SuspendLayout();
			panelCard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			tabPageItems.Controls.Add(comboBoxJob);
			tabPageItems.Controls.Add(dataGridItems);
			tabPageItems.Controls.Add(comboBoxUserGroup);
			tabPageItems.Controls.Add(comboBoxUser);
			tabPageItems.Location = new System.Drawing.Point(1, 23);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(675, 189);
			tabPageExpense.Controls.Add(checkBoxNotAllowbyothers);
			tabPageExpense.Controls.Add(checkBoxNoTransaction);
			tabPageExpense.Controls.Add(checkBoxonPrint);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(675, 189);
			checkBoxNotAllowbyothers.AutoSize = true;
			checkBoxNotAllowbyothers.Location = new System.Drawing.Point(8, 65);
			checkBoxNotAllowbyothers.Name = "checkBoxNotAllowbyothers";
			checkBoxNotAllowbyothers.Size = new System.Drawing.Size(231, 17);
			checkBoxNotAllowbyothers.TabIndex = 6;
			checkBoxNotAllowbyothers.Text = "Don't Allow to edit document by other party.";
			checkBoxNotAllowbyothers.UseVisualStyleBackColor = true;
			checkBoxNoTransaction.AutoSize = true;
			checkBoxNoTransaction.Location = new System.Drawing.Point(8, 42);
			checkBoxNoTransaction.Name = "checkBoxNoTransaction";
			checkBoxNoTransaction.Size = new System.Drawing.Size(176, 17);
			checkBoxNoTransaction.TabIndex = 5;
			checkBoxNoTransaction.Text = "Don't Allow further Transactions";
			checkBoxNoTransaction.UseVisualStyleBackColor = true;
			checkBoxonPrint.AutoSize = true;
			checkBoxonPrint.Location = new System.Drawing.Point(8, 19);
			checkBoxonPrint.Name = "checkBoxonPrint";
			checkBoxonPrint.Size = new System.Drawing.Size(92, 17);
			checkBoxonPrint.TabIndex = 4;
			checkBoxonPrint.Text = "Notify on Print";
			checkBoxonPrint.UseVisualStyleBackColor = true;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[11]
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
				toolStripSeparator2
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(696, 31);
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
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 411);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(696, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(696, 1);
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
			buttonDelete.TabIndex = 6;
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
			xpButton1.Location = new System.Drawing.Point(586, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 7;
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
			buttonNew.TabIndex = 5;
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
			buttonSave.TabIndex = 4;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkBoxInactive.AutoSize = true;
			checkBoxInactive.Location = new System.Drawing.Point(307, 38);
			checkBoxInactive.Name = "checkBoxInactive";
			checkBoxInactive.Size = new System.Drawing.Size(64, 17);
			checkBoxInactive.TabIndex = 1;
			checkBoxInactive.Text = "Inactive";
			checkBoxInactive.UseVisualStyleBackColor = true;
			comboBoxObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxObjectType.FormattingEnabled = true;
			comboBoxObjectType.Items.AddRange(new object[2]
			{
				"Transaction",
				"Card"
			});
			comboBoxObjectType.Location = new System.Drawing.Point(139, 94);
			comboBoxObjectType.Name = "comboBoxObjectType";
			comboBoxObjectType.Size = new System.Drawing.Size(162, 21);
			comboBoxObjectType.TabIndex = 17;
			panelTransaction.Controls.Add(comboBoxSysDocType);
			panelTransaction.Controls.Add(radioButtonAllDocs);
			panelTransaction.Controls.Add(radioButtonSpecific);
			panelTransaction.Controls.Add(comboBoxSysDoc);
			panelTransaction.Controls.Add(mmLabel3);
			panelTransaction.Location = new System.Drawing.Point(0, 122);
			panelTransaction.Name = "panelTransaction";
			panelTransaction.Size = new System.Drawing.Size(508, 65);
			panelTransaction.TabIndex = 20;
			panelTransaction.Visible = false;
			radioButtonAllDocs.AutoSize = true;
			radioButtonAllDocs.Checked = true;
			radioButtonAllDocs.Location = new System.Drawing.Point(15, 31);
			radioButtonAllDocs.Name = "radioButtonAllDocs";
			radioButtonAllDocs.Size = new System.Drawing.Size(93, 17);
			radioButtonAllDocs.TabIndex = 24;
			radioButtonAllDocs.TabStop = true;
			radioButtonAllDocs.Text = "All Documents";
			radioButtonAllDocs.UseVisualStyleBackColor = true;
			radioButtonSpecific.AutoSize = true;
			radioButtonSpecific.Location = new System.Drawing.Point(114, 31);
			radioButtonSpecific.Name = "radioButtonSpecific";
			radioButtonSpecific.Size = new System.Drawing.Size(115, 17);
			radioButtonSpecific.TabIndex = 24;
			radioButtonSpecific.Text = "Specific Document";
			radioButtonSpecific.UseVisualStyleBackColor = true;
			radioButtonSpecific.CheckedChanged += new System.EventHandler(radioButtonSpecific_CheckedChanged);
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(12, 6);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(110, 13);
			mmLabel3.TabIndex = 20;
			mmLabel3.Text = "Transaction Type:";
			panelCard.Controls.Add(comboBoxCardType);
			panelCard.Controls.Add(mmLabel4);
			panelCard.Location = new System.Drawing.Point(0, 122);
			panelCard.Name = "panelCard";
			panelCard.Size = new System.Drawing.Size(508, 65);
			panelCard.TabIndex = 21;
			panelCard.Visible = false;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(12, 6);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(69, 13);
			mmLabel4.TabIndex = 20;
			mmLabel4.Text = "Card Type:";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(3, 190);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(679, 215);
			ultraTabControl1.TabIndex = 127;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "Details";
			ultraTab2.TabPage = tabPageExpense;
			ultraTab2.Text = "Settings";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(675, 189);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(12, 97);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(80, 13);
			mmLabel2.TabIndex = 18;
			mmLabel2.Text = "Object Type:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(139, 58);
			textBoxName.MaxLength = 30;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(308, 20);
			textBoxName.TabIndex = 2;
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(139, 36);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(162, 20);
			textBoxCode.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 59);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(97, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Approval Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(12, 36);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(94, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Approval Code:";
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = null;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(556, 50);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 127;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(3, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(677, 183);
			dataGridItems.TabIndex = 22;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxUserGroup.Assigned = false;
			comboBoxUserGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxUserGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUserGroup.CustomReportFieldName = "";
			comboBoxUserGroup.CustomReportKey = "";
			comboBoxUserGroup.CustomReportValueType = 1;
			comboBoxUserGroup.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUserGroup.DisplayLayout.Appearance = appearance13;
			comboBoxUserGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUserGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxUserGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUserGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxUserGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUserGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUserGroup.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUserGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUserGroup.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxUserGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUserGroup.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUserGroup.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxUserGroup.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxUserGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUserGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxUserGroup.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxUserGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUserGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxUserGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUserGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUserGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUserGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUserGroup.Editable = true;
			comboBoxUserGroup.FilterString = "";
			comboBoxUserGroup.HasAllAccount = false;
			comboBoxUserGroup.HasCustom = false;
			comboBoxUserGroup.IsDataLoaded = false;
			comboBoxUserGroup.Location = new System.Drawing.Point(404, 50);
			comboBoxUserGroup.MaxDropDownItems = 12;
			comboBoxUserGroup.Name = "comboBoxUserGroup";
			comboBoxUserGroup.ShowInactiveItems = false;
			comboBoxUserGroup.ShowQuickAdd = true;
			comboBoxUserGroup.Size = new System.Drawing.Size(100, 20);
			comboBoxUserGroup.TabIndex = 24;
			comboBoxUserGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUserGroup.Visible = false;
			comboBoxUser.Assigned = false;
			comboBoxUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxUser.CustomReportFieldName = "";
			comboBoxUser.CustomReportKey = "";
			comboBoxUser.CustomReportValueType = 1;
			comboBoxUser.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxUser.DisplayLayout.Appearance = appearance25;
			comboBoxUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxUser.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxUser.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxUser.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxUser.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxUser.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxUser.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxUser.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxUser.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxUser.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxUser.Editable = true;
			comboBoxUser.FilterString = "";
			comboBoxUser.HasAllAccount = false;
			comboBoxUser.HasCustom = false;
			comboBoxUser.IsDataLoaded = false;
			comboBoxUser.Location = new System.Drawing.Point(399, 76);
			comboBoxUser.MaxDropDownItems = 12;
			comboBoxUser.Name = "comboBoxUser";
			comboBoxUser.ShowInactiveItems = false;
			comboBoxUser.ShowQuickAdd = true;
			comboBoxUser.Size = new System.Drawing.Size(100, 20);
			comboBoxUser.TabIndex = 23;
			comboBoxUser.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxUser.Visible = false;
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
			comboBoxCardType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxCardType.FormattingEnabled = true;
			comboBoxCardType.Location = new System.Drawing.Point(139, 5);
			comboBoxCardType.Name = "comboBoxCardType";
			comboBoxCardType.SelectedID = Micromind.Common.Data.DataComboType.None;
			comboBoxCardType.Size = new System.Drawing.Size(162, 21);
			comboBoxCardType.TabIndex = 26;
			comboBoxSysDocType.ApprovalType = Micromind.Common.Data.ApprovalTypes.Approval;
			comboBoxSysDocType.FormattingEnabled = true;
			comboBoxSysDocType.Location = new System.Drawing.Point(139, 3);
			comboBoxSysDocType.Name = "comboBoxSysDocType";
			comboBoxSysDocType.SelectedID = Micromind.Common.Data.SysDocTypes.None;
			comboBoxSysDocType.Size = new System.Drawing.Size(162, 21);
			comboBoxSysDocType.TabIndex = 25;
			comboBoxSysDocType.SelectedIndexChanged += new System.EventHandler(comboBoxSysDocType_SelectedIndexChanged);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance37;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.Enabled = false;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(230, 29);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(121, 20);
			comboBoxSysDoc.TabIndex = 21;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.SelectedIndexChanged += new System.EventHandler(comboBoxSysDocType_SelectedIndexChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(696, 451);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(comboBoxObjectType);
			base.Controls.Add(checkBoxInactive);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(panelCard);
			base.Controls.Add(panelTransaction);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "ApprovalDetailsForm";
			Text = "Approval";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelTransaction.ResumeLayout(false);
			panelTransaction.PerformLayout();
			panelCard.ResumeLayout(false);
			panelCard.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUserGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxUser).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
