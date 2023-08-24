using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.ClientUI.WindowsForms.DataEntries.Vendors;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Projects
{
	public class TaskTransactionForm : Form, IForm
	{
		private bool supressExpenseMessage;

		private TaskTransactionData currentData;

		private const string TABLENAME_CONST = "Task_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private int daysAllowed;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private SysDocTypes doctype;

		private string strSelectSysDocID = "";

		private string StrSelectVoucherID = "";

		private string tableName = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem saveADraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator3;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private UltraFormattedLinkLabel ulinkDocId;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraLinkVoucherNumber;

		private TextBox textBoxVoucherNumber;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private TaskTypeComboBox comboBoxTaskType;

		private MMLabel mmLabel4;

		private DateTimePicker dateTimePickerDate;

		private ToolStripMenuItem changeAssigneeToolStripMenuItem;

		private ToolStripMenuItem changeStepToolStripMenuItem;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimePickerStartDate;

		private GroupBox groupBox3;

		private SysDocTypeComboBox comboBoxDocType;

		private UserComboBox comboBoxGridUser;

		private TaskStepsComboBox comboBoxGridTaskSteps;

		private DataEntryGrid dataGridTaskSteps;

		private MMTextBox textBoxName;

		private MMLabel mmLabel5;

		private MMLabel mmLabel7;

		private MMLabel mmLabel6;

		private TextBox textBoxAssgnDocNumber;

		private XPButton buttonSelectDoc;

		private TransactionTypeComboBox comboBoxTransactionType;

		private SysDocComboBox textBoxAssgnSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private Label label5;

		private SysDocTypeComboBox sysDocTypeComboBox1;

		private Label label1;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		public ScreenAreas ScreenArea => ScreenAreas.Project;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private bool IsDirty
		{
			get
			{
				if (IsVoid)
				{
					return false;
				}
				return formManager.GetDirtyStatus();
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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
				toolStripButtonDistribution.Enabled = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
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

		private bool IsVoid
		{
			get
			{
				return isVoid;
			}
			set
			{
				if (isVoid != value)
				{
					isVoid = value;
					if (value)
					{
						buttonVoid.Text = UIMessages.Unvoid;
					}
					else
					{
						buttonVoid.Text = UIMessages.Void;
					}
				}
			}
		}

		public TaskTransactionForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			base.FormClosing += ItemGroupDetailsForm_FormClosing;
			dataGridTaskSteps.DoubleClickRow += dataGridTaskSteps_DoubleClickRow;
			dataGridTaskSteps.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridTaskSteps.HeaderClicked += dataGridTaskSteps_HeaderClicked;
			dataGridTaskSteps.AfterRowInsert += dataGridItems_AfterRowInsert;
			dataGridTaskSteps.AfterRowActivate += dataGridItems_AfterRowActivate;
		}

		private void dataGridItems_AfterRowInsert(object sender, RowEventArgs e)
		{
			e.Row.Cells["No"].Value = checked(e.Row.Index + 1);
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

		private void dataGridTaskSteps_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			try
			{
				if (dataGridTaskSteps.ActiveRow != null)
				{
					string text = "";
					string text2 = "";
					int num = 0;
					int num2 = 0;
					text2 = dataGridTaskSteps.ActiveRow.Cells["StepID"].Text.ToString();
					text = dataGridTaskSteps.ActiveRow.Cells["Description"].Text.ToString();
					num = int.Parse(dataGridTaskSteps.ActiveRow.Cells["Status"].Value.ToString());
					num2 = dataGridTaskSteps.ActiveRow.Index;
					FormActivator.TaskTransactionStatusFormObj.StepName = text;
					FormActivator.TaskTransactionStatusFormObj.StepID = text2;
					FormActivator.TaskTransactionStatusFormObj.Status = num;
					FormActivator.TaskTransactionStatusFormObj.RowIndex = num2;
					FormActivator.TaskTransactionStatusFormObj.SourceSysDocID = comboBoxSysDoc.SelectedID;
					FormActivator.TaskTransactionStatusFormObj.SourceVoucherID = textBoxVoucherNumber.Text;
					FormActivator.TaskTransactionStatusFormObj.SourceDocType = comboBoxTransactionType.SelectedID;
					FormActivator.TaskTransactionStatusFormObj.TaskName = textBoxName.Text;
					FormActivator.BringFormToFront(FormActivator.TaskTransactionStatusFormObj);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxClose_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxJob_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridExpenseCode_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			DateTime result = DateTime.Now;
			try
			{
				if (dataGridTaskSteps.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "StepID")
					{
						dataGridTaskSteps.ActiveRow.Cells["Description"].Value = comboBoxGridTaskSteps.SelectedName;
					}
					if (e.Cell.Column.Key == "DaysAllowed")
					{
						int.TryParse(dataGridTaskSteps.ActiveRow.Cells["DaysAllowed"].Value.ToString(), out daysAllowed);
						if (daysAllowed > 0)
						{
							DateTime.TryParse(dataGridTaskSteps.ActiveRow.Cells["StartDate"].Value.ToString(), out result);
							if (result > DateTime.MinValue)
							{
								dataGridTaskSteps.ActiveRow.Cells["DeadLine"].Value = result.AddDays(daysAllowed);
							}
						}
					}
					if (e.Cell.Column.Key == "StartDate")
					{
						int.TryParse(dataGridTaskSteps.ActiveRow.Cells["DaysAllowed"].Value.ToString(), out daysAllowed);
						if (daysAllowed > 0)
						{
							DateTime.TryParse(dataGridTaskSteps.ActiveRow.Cells["StartDate"].Value.ToString(), out result);
							if (result > DateTime.MinValue)
							{
								dataGridTaskSteps.ActiveRow.Cells["DeadLine"].Value = result.AddDays(daysAllowed);
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
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

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void SetupTaskTypeGrid()
		{
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("StepID");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("DefaultAssignee");
			dataTable.Columns.Add("StartDate", typeof(DateTime));
			dataTable.Columns.Add("DeadLine", typeof(DateTime));
			dataTable.Columns.Add("Status");
			dataTable.Columns.Add("PreRequisite");
			dataTable.Columns.Add("DocType");
			dataGridTaskSteps.DataSource = dataTable;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].CharacterCasing = CharacterCasing.Upper;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].MaxLength = 64;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].ValueList = comboBoxGridTaskSteps;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StartDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DeadLine"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StartDate"].Header.Caption = "Start Date";
			dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DocType"].CharacterCasing = CharacterCasing.Upper;
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
			dataGridTaskSteps.SetupUI();
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridExpenseCode_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new TaskTransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TaskTransactionTable.Rows[0] : currentData.TaskTransactionTable.NewRow();
				dataRow.BeginEdit();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text.Trim();
				dataRow["TaskTypeID"] = comboBoxTaskType.SelectedID.Trim();
				dataRow["TaskName"] = textBoxName.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["AssignedVouherID"] = textBoxAssgnDocNumber.Text;
				dataRow["AssignedSysDocID"] = textBoxAssgnSysDoc.SelectedID;
				if (!string.IsNullOrEmpty(comboBoxTransactionType.SelectedID))
				{
					dataRow[TaskTransactionData.DOCTYPE_FIELD] = comboBoxTransactionType.SelectedID;
				}
				else
				{
					dataRow[TaskTransactionData.DOCTYPE_FIELD] = 0;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.TaskTransactionTable.Rows.Add(dataRow);
				}
				currentData.TaskTransactionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridTaskSteps.Rows)
				{
					DataRow dataRow2 = currentData.TaskTransactionDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["TaskStepID"] = row.Cells["StepID"].Value.ToString();
					dataRow2[TaskTransactionData.DESCRIPTION_FIELD] = row.Cells["Description"].Value.ToString();
					dataRow2[TaskTransactionData.DEFAULTASSIGNEEID_FIELD] = (row.Cells["AssignedTo"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["AssignedTo"].Value.ToString()));
					dataRow2[TaskTransactionData.DAYSALLOWED_FIELD] = (row.Cells["DaysAllowed"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["DaysAllowed"].Value.ToString()));
					dataRow2["StartDate"] = (row.Cells["StartDate"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["StartDate"].Value.ToString()));
					dataRow2["DeadLine"] = (row.Cells["DeadLine"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["DeadLine"].Value.ToString()));
					dataRow2["Status"] = (row.Cells["Status"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["Status"].Value.ToString()));
					dataRow2[TaskTransactionData.DOCTYPE_FIELD] = (row.Cells["TrDocType"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["TrDocType"].Value.ToString()));
					dataRow2[TaskTransactionData.PREREQUEST_FIELD] = (row.Cells["PreRequisite"].Value.IsNullOrEmpty() ? ((IConvertible)DBNull.Value) : ((IConvertible)row.Cells["PreRequisite"].Value.ToString()));
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.TaskTransactionDetailTable.Rows.Add(dataRow2);
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
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!(voucherID.Trim() == ""))
				{
					currentData = Factory.TaskTransactionSystem.GetTaskTransactionByID(comboBoxSysDoc.SelectedID, voucherID);
					if (currentData != null)
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
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxTaskType.SelectedID = dataRow["TaskTypeID"].ToString();
					textBoxName.Text = dataRow["TaskName"].ToString();
					textBoxAssgnDocNumber.Text = dataRow["AssignedVouherID"].ToString();
					comboBoxTransactionType.SelectedID = dataRow[TaskTransactionData.DOCTYPE_FIELD].ToString();
					textBoxAssgnSysDoc.SelectedID = dataRow["AssignedSysDocID"].ToString();
					dataRow["CreatedBy"].ToString();
					(dataGridTaskSteps.DataSource as DataTable)?.Rows.Clear();
					DataTable dataTable = dataGridTaskSteps.DataSource as DataTable;
					if (currentData.Tables.Contains("Task_Transaction_Detail") && currentData.TaskTransactionDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Task_Transaction_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["No"] = checked(int.Parse(row["RowIndex"].ToString()) + 1);
							dataRow3["StepID"] = row["TaskStepID"].ToString();
							dataRow3["Description"] = row[TaskTransactionData.DESCRIPTION_FIELD].ToString();
							dataRow3["AssignedTo"] = row[TaskTransactionData.DEFAULTASSIGNEEID_FIELD].ToString();
							if (!string.IsNullOrEmpty(row["StartDate"].ToString()))
							{
								dataRow3["StartDate"] = row["StartDate"].ToString();
							}
							if (!string.IsNullOrEmpty(row["DeadLine"].ToString()))
							{
								dataRow3["DeadLine"] = row["DeadLine"].ToString();
							}
							dataRow3["DaysAllowed"] = row[TaskTransactionData.DAYSALLOWED_FIELD].ToString();
							dataRow3["Status"] = row["Status"].ToString();
							if (row[TaskTransactionData.DOCTYPE_FIELD] != DBNull.Value)
							{
								dataRow3["TrDocType"] = row[TaskTransactionData.DOCTYPE_FIELD].ToString();
							}
							else
							{
								dataRow3["TrDocType"] = 0;
							}
							if (row[TaskTransactionData.PREREQUEST_FIELD] != DBNull.Value)
							{
								dataRow3["PreRequisite"] = row[TaskTransactionData.PREREQUEST_FIELD].ToString();
							}
							else
							{
								dataRow3["PreRequisite"] = 0;
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridTaskSteps.Rows)
						{
							string assignedTo = row2.Cells["AssignedTo"].Value.ToString();
							if (isEnable(assignedTo))
							{
								row2.Activation = Activation.AllowEdit;
								dataGridTaskSteps.AllowAddNew = true;
							}
							else
							{
								row2.Activation = Activation.Disabled;
								dataGridTaskSteps.AllowAddNew = false;
							}
						}
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private bool isEnable(string assignedTo)
		{
			if (Global.CurrentUser.ToLower() == assignedTo.ToLower())
			{
				return true;
			}
			return false;
		}

		private bool SaveData()
		{
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
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
				bool flag = true;
				flag = Factory.TaskTransactionSystem.CreateTaskTransaction(currentData, !isNewRecord);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					bool result = false;
					bool result2 = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("DoPrint").ToString(), out result);
					if (result)
					{
						bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result2);
						if (result2)
						{
							Print(isPrint: true, showPrintDialog: true, saveChanges: false);
						}
						else
						{
							Print(isPrint: false, showPrintDialog: true, saveChanges: false);
						}
					}
					if (clearAfter)
					{
						ClearForm();
						IsNewRecord = true;
					}
					else
					{
						formManager.ResetDirty();
					}
				}
				return flag;
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1046)
				{
					string nextVoucherNumber = GetNextVoucherNumber();
					if (nextVoucherNumber == textBoxVoucherNumber.Text)
					{
						ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
						return false;
					}
					if (nextVoucherNumber != "")
					{
						textBoxVoucherNumber.Text = nextVoucherNumber;
					}
					formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
					return SaveData();
				}
				ErrorHelper.ProcessError(ex);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Task_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Task_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
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
			try
			{
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxTaskType.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxTaskType.Clear();
				textBoxAssgnSysDoc.Clear();
				textBoxAssgnDocNumber.Clear();
				comboBoxTaskType.Clear();
				comboBoxTransactionType.Clear();
				(dataGridTaskSteps.DataSource as DataTable)?.Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void JournalLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void JournalLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.TaskTransactionSystem.DeleteTaskTransaction(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Task_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Task_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Task_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Task_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else
				{
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Task_Transaction", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
					if (text != "")
					{
						LoadData(text);
					}
					else
					{
						ErrorHelper.InformationMessage(UIMessages.DocumentNotFound);
						toolStripTextBoxFind.SelectAll();
						toolStripTextBoxFind.Focus();
					}
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

		private void ItemGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
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

		private void JournalLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridTaskSteps.SetupUI();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.TaskTransaction);
				dateTimePickerDate.Value = DateTime.Now;
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
				return;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDocNumber))
			{
				textBoxVoucherNumber.ReadOnly = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransaction))
			{
				AllowEditTransaction = false;
			}
			else
			{
				AllowEditTransaction = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTransDiffLocation))
			{
				AllowEditTransDiffLocation = false;
			}
			else
			{
				AllowEditTransDiffLocation = true;
			}
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private string GetNextVoucherNumber()
		{
			try
			{
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(SystemDocID);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return "";
			}
		}

		private void buttonVoid_Click(object sender, EventArgs e)
		{
			if (Void(!IsVoid))
			{
				IsVoid = !IsVoid;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				if (isVoid)
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				}
				else
				{
					DialogResult dialogResult = ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid);
				}
				_ = 7;
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private bool SaveDraft()
		{
			try
			{
				if (GetData())
				{
					EnterNameDialog enterNameDialog = new EnterNameDialog();
					if (enterNameDialog.ShowDialog() == DialogResult.OK)
					{
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.TaskTransaction);
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool LoadDraft()
		{
			try
			{
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 246.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.TaskTransaction);
					currentData = (dataSet as TaskTransactionData);
					FillData();
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print()
		{
			Print(isPrint: true, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet taskTransactionToPrint = Factory.TaskTransactionSystem.GetTaskTransactionToPrint(selectedID, text);
					if (taskTransactionToPrint == null || taskTransactionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(taskTransactionToPrint, selectedID, "Task Transaction", SysDocTypes.TaskTransaction, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = SystemDocID;
			LoadData(voucherID);
		}

		private void printListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintList();
		}

		private void SetupGrid()
		{
			try
			{
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("No");
				dataTable.Columns.Add("StepID");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("AssignedTo");
				dataTable.Columns.Add("DaysAllowed", typeof(int));
				dataTable.Columns.Add("StartDate", typeof(DateTime));
				dataTable.Columns.Add("DeadLine", typeof(DateTime));
				dataTable.Columns.Add("Status", typeof(decimal));
				dataTable.Columns.Add("TrDocType");
				dataTable.Columns.Add("PreRequisite");
				dataGridTaskSteps.DataSource = dataTable;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellActivation = Activation.NoEdit;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["No"].Width = checked(10 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Width = checked(30 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].Width = checked(20 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StartDate"].Width = checked(10 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["DeadLine"].Width = checked(10 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["TrDocType"].Width = checked(20 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["PreRequisite"].Width = checked(20 * dataGridTaskSteps.Width) / 100;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["TrDocType"].MaxLength = 64;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["TrDocType"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["TrDocType"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["TrDocType"].ValueList = comboBoxDocType;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["PreRequisite"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				new ValueList();
				new ValueList().ValueListItems.Add(0, "1");
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].CharacterCasing = CharacterCasing.Normal;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].MaxLength = 64;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["StepID"].ValueList = comboBoxGridTaskSteps;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].CharacterCasing = CharacterCasing.Normal;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].MaxLength = 64;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["AssignedTo"].ValueList = comboBoxGridUser;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(1, "Open");
				valueList.ValueListItems.Add(2, "Started");
				valueList.ValueListItems.Add(3, "Hold");
				valueList.ValueListItems.Add(4, "Completed");
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList;
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Status"].Header.Caption = "Status";
				dataGridTaskSteps.DisplayLayout.Bands[0].Columns["Status"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridTaskSteps.SetupUI();
			}
			catch (Exception e)
			{
				dataGridTaskSteps.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private string GetDocumentTitle()
		{
			return "Job Expense Issue";
		}

		private void PrintList()
		{
			try
			{
				PrintHelper.PreviewDocument(ultraGridPrintDocument1, Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void ultraLinkVoucherNumber_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			textBoxVoucherNumber.Text = GetNextVoucherNumber();
		}

		private void ulinkDocId_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.TaskTransaction);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void changeAssigneeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateAssigneeDialog updateAssigneeDialog = new UpdateAssigneeDialog();
			updateAssigneeDialog.SetDocument(SysDocTypes.TaskTransaction, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
			updateAssigneeDialog.ShowDialog(this);
		}

		private void changeStepToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UpdateTaskStepDialog updateTaskStepDialog = new UpdateTaskStepDialog();
			updateTaskStepDialog.SetDocument(SysDocTypes.TaskTransaction, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
			updateTaskStepDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void comboBoxTaskType_SelectedIndexChanged(object sender, EventArgs e)
		{
			int num = 0;
			DateTime dateTime = DateTime.Now;
			if (dataGridTaskSteps.Rows.Count > 0 || string.IsNullOrEmpty(comboBoxTaskType.SelectedID))
			{
				return;
			}
			DataSet taskTypeByID = Factory.TaskTypeSystem.GetTaskTypeByID(comboBoxTaskType.SelectedID);
			if (taskTypeByID == null || taskTypeByID.Tables.Count == 0 || taskTypeByID.Tables[1].Rows.Count == 0)
			{
				return;
			}
			DataTable dataTable = dataGridTaskSteps.DataSource as DataTable;
			dataTable?.Clear();
			dataTable.BeginLoadData();
			checked
			{
				foreach (DataRow row in taskTypeByID.Tables["Task_Type_Detail"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["No"] = int.Parse(row["RowIndex"].ToString()) + 1;
					dataRow2["StepID"] = row["TaskStepID"];
					dataRow2["Description"] = row[TaskTypeData.DESCRIPTION_FIELD];
					dataRow2["AssignedTo"] = row[TaskTypeData.DEFAULTASSIGNEEID_FIELD];
					dataRow2["DaysAllowed"] = row[TaskTypeData.DAYSALLOWED_FIELD];
					dataRow2["PreRequisite"] = row[TaskTypeData.PREREQUEST_FIELD];
					dataRow2["TrDocType"] = row[TaskTypeData.DOCTYPENAME_FIELD];
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.EndLoadData();
				dataTable.AcceptChanges();
				for (int i = 0; i < dataGridTaskSteps.Rows.Count; i++)
				{
					if (i == 0)
					{
						dataGridTaskSteps.Rows[i].Cells["StartDate"].Value = dateTimePickerStartDate.Value;
						if (!string.IsNullOrEmpty(dataGridTaskSteps.Rows[i].Cells["DaysAllowed"].Value.ToString()))
						{
							num = int.Parse(dataGridTaskSteps.Rows[i].Cells["DaysAllowed"].Value.ToString());
						}
						dataGridTaskSteps.Rows[i].Cells["DeadLine"].Value = dateTime.AddDays(num);
						dataGridTaskSteps.Rows[i].Cells["Status"].Value = 1;
						continue;
					}
					if (!string.IsNullOrEmpty(dataGridTaskSteps.Rows[i].Cells["DaysAllowed"].Value.ToString()))
					{
						num = int.Parse(dataGridTaskSteps.Rows[i].Cells["DaysAllowed"].Value.ToString());
					}
					dateTime = DateTime.Parse(dataGridTaskSteps.Rows[i - 1].Cells["DeadLine"].Value.ToString());
					dataGridTaskSteps.Rows[i].Cells["StartDate"].Value = dataGridTaskSteps.Rows[i - 1].Cells["DeadLine"].Value.ToString();
					dataGridTaskSteps.Rows[i].Cells["DeadLine"].Value = dateTime.AddDays(num);
					dataGridTaskSteps.Rows[i].Cells["Status"].Value = 1;
				}
			}
		}

		private void comboBoxTransactionType_SelectedIndexChanged(object sender, EventArgs e)
		{
			doctype = (SysDocTypes)byte.Parse(comboBoxTransactionType.SelectedID);
			if (IsNewRecord)
			{
				textBoxAssgnSysDoc.FilterByType(doctype);
			}
		}

		private void dateTimePickerStartDate_ValueChanged(object sender, EventArgs e)
		{
			dataGridTaskSteps.Rows[0].Cells["StartDate"].Value = dateTimePickerStartDate.Value;
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaskType(comboBoxTaskType.SelectedID);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityType = EntityTypesEnum.Transactions;
					docManagementForm.ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSelectDoc_Click(object sender, EventArgs e)
		{
			if (!(textBoxAssgnSysDoc.SelectedID == string.Empty))
			{
				int objectID = (int)Enum.Parse(value: Factory.SystemDocumentSystem.GetBarCodeSystemDocumentType(textBoxAssgnSysDoc.SelectedID).ToString(), enumType: typeof(SysDocTypes));
				DoubleString doubleString = Factory.ApprovalSystem.GetTableName(1, objectID);
				tableName = doubleString.FirstString;
				try
				{
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.AllowDateFilter = true;
					selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
					selectDocumentDialog.Text = "Select Value";
					selectDocumentDialog.IsMultiSelect = false;
					selectDocumentDialog.AllowDateFilter = true;
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							strSelectSysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
							if (selectedRow.Cells.Exists("Doc Number"))
							{
								StrSelectVoucherID = selectedRow.Cells["Doc Number"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("VoucherID"))
							{
								StrSelectVoucherID = selectedRow.Cells["VoucherID"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("Number"))
							{
								StrSelectVoucherID = selectedRow.Cells["Number"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("Batch Number"))
							{
								StrSelectVoucherID = selectedRow.Cells["Batch Number"].Text.ToString();
							}
						}
					}
					textBoxAssgnDocNumber.Text = StrSelectVoucherID;
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			SelectDocumentDialog obj = sender as SelectDocumentDialog;
			DataSet dataSet = new DataSet();
			dataSet = (obj.DataSource = Factory.PurchaseReceiptSystem.GetVoucherNumbersFromTransaction(tableName, textBoxAssgnSysDoc.SelectedID, "", e.From, e.To));
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.TaskTransactionListFormObj);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Projects.TaskTransactionForm));
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
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			saveADraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			changeAssigneeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			changeStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ulinkDocId = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLinkVoucherNumber = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			dateTimePickerStartDate = new System.Windows.Forms.DateTimePicker();
			groupBox3 = new System.Windows.Forms.GroupBox();
			sysDocTypeComboBox1 = new Micromind.DataControls.SysDocTypeComboBox();
			comboBoxDocType = new Micromind.DataControls.SysDocTypeComboBox();
			comboBoxGridUser = new Micromind.DataControls.UserComboBox();
			comboBoxGridTaskSteps = new Micromind.DataControls.TaskStepsComboBox();
			dataGridTaskSteps = new Micromind.DataControls.DataEntryGrid();
			textBoxAssgnDocNumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label5 = new System.Windows.Forms.Label();
			textBoxAssgnSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxTransactionType = new Micromind.DataControls.TransactionTypeComboBox();
			buttonSelectDoc = new Micromind.UISupport.XPButton();
			mmLabel7 = new Micromind.UISupport.MMLabel();
			mmLabel6 = new Micromind.UISupport.MMLabel();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			comboBoxTaskType = new Micromind.DataControls.TaskTypeComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			formManager = new Micromind.DataControls.FormManager();
			label1 = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			contextMenuStrip1.SuspendLayout();
			groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)sysDocTypeComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDocType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUser).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTaskSteps).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridTaskSteps).BeginInit();
			((System.ComponentModel.ISupportInitialize)textBoxAssgnSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransactionType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaskType).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[17]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonInformation,
				toolStripButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(1064, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 28);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 28);
			toolStripButtonLast.Text = "Last Record";
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
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(60, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Visible = false;
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				saveADraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				changeAssigneeToolStripMenuItem,
				changeStepToolStripMenuItem
			});
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(60, 28);
			toolStripButton1.Text = "Actions";
			saveADraftToolStripMenuItem.Name = "saveADraftToolStripMenuItem";
			saveADraftToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			saveADraftToolStripMenuItem.Text = "Save as Draft";
			saveADraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			changeAssigneeToolStripMenuItem.Name = "changeAssigneeToolStripMenuItem";
			changeAssigneeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			changeAssigneeToolStripMenuItem.Text = "Change Assignee";
			changeAssigneeToolStripMenuItem.Click += new System.EventHandler(changeAssigneeToolStripMenuItem_Click);
			changeStepToolStripMenuItem.Name = "changeStepToolStripMenuItem";
			changeStepToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			changeStepToolStripMenuItem.Text = "Change Step";
			changeStepToolStripMenuItem.Click += new System.EventHandler(changeStepToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 555);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(1064, 40);
			panelButtons.TabIndex = 12;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(76, 24);
			buttonVoid.TabIndex = 16;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 15;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 14;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(1064, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(954, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 17;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			buttonSave.TabIndex = 13;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				printListToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(130, 26);
			printListToolStripMenuItem.Name = "printListToolStripMenuItem";
			printListToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
			printListToolStripMenuItem.Text = "Print List...";
			printListToolStripMenuItem.Click += new System.EventHandler(printListToolStripMenuItem_Click);
			ultraGridPrintDocument1.DocumentName = "Inventory Adjustment - List";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ulinkDocId.Appearance = appearance;
			ulinkDocId.AutoSize = true;
			ulinkDocId.Location = new System.Drawing.Point(11, 43);
			ulinkDocId.Name = "ulinkDocId";
			ulinkDocId.Size = new System.Drawing.Size(45, 15);
			ulinkDocId.TabIndex = 159;
			ulinkDocId.TabStop = true;
			ulinkDocId.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ulinkDocId.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ulinkDocId.Value = "Doc ID:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ulinkDocId.VisitedLinkAppearance = appearance2;
			ulinkDocId.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ulinkDocId_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraLinkVoucherNumber.Appearance = appearance3;
			ultraLinkVoucherNumber.AutoSize = true;
			ultraLinkVoucherNumber.Location = new System.Drawing.Point(215, 44);
			ultraLinkVoucherNumber.Name = "ultraLinkVoucherNumber";
			ultraLinkVoucherNumber.Size = new System.Drawing.Size(101, 15);
			ultraLinkVoucherNumber.TabIndex = 161;
			ultraLinkVoucherNumber.TabStop = true;
			ultraLinkVoucherNumber.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraLinkVoucherNumber.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraLinkVoucherNumber.Value = "Voucher Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraLinkVoucherNumber.VisitedLinkAppearance = appearance4;
			ultraLinkVoucherNumber.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraLinkVoucherNumber_LinkClicked);
			textBoxVoucherNumber.Location = new System.Drawing.Point(322, 41);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(121, 20);
			textBoxVoucherNumber.TabIndex = 1;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(566, 39);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(116, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerStartDate.Location = new System.Drawing.Point(109, 143);
			dateTimePickerStartDate.Name = "dateTimePickerStartDate";
			dateTimePickerStartDate.Size = new System.Drawing.Size(121, 20);
			dateTimePickerStartDate.TabIndex = 8;
			groupBox3.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			groupBox3.Controls.Add(sysDocTypeComboBox1);
			groupBox3.Controls.Add(comboBoxDocType);
			groupBox3.Controls.Add(comboBoxGridUser);
			groupBox3.Controls.Add(comboBoxGridTaskSteps);
			groupBox3.Controls.Add(dataGridTaskSteps);
			groupBox3.Location = new System.Drawing.Point(15, 178);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(1035, 371);
			groupBox3.TabIndex = 9;
			groupBox3.TabStop = false;
			groupBox3.Text = "Tasks";
			sysDocTypeComboBox1.Assigned = false;
			sysDocTypeComboBox1.CalcManager = ultraCalcManager1;
			sysDocTypeComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			sysDocTypeComboBox1.CustomReportFieldName = "";
			sysDocTypeComboBox1.CustomReportKey = "";
			sysDocTypeComboBox1.CustomReportValueType = 1;
			sysDocTypeComboBox1.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			sysDocTypeComboBox1.DisplayLayout.Appearance = appearance5;
			sysDocTypeComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			sysDocTypeComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			sysDocTypeComboBox1.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			sysDocTypeComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			sysDocTypeComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			sysDocTypeComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			sysDocTypeComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			sysDocTypeComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			sysDocTypeComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			sysDocTypeComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			sysDocTypeComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			sysDocTypeComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			sysDocTypeComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			sysDocTypeComboBox1.DisplayLayout.Override.CellAppearance = appearance12;
			sysDocTypeComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			sysDocTypeComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			sysDocTypeComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			sysDocTypeComboBox1.DisplayLayout.Override.HeaderAppearance = appearance14;
			sysDocTypeComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			sysDocTypeComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			sysDocTypeComboBox1.DisplayLayout.Override.RowAppearance = appearance15;
			sysDocTypeComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			sysDocTypeComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			sysDocTypeComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			sysDocTypeComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			sysDocTypeComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			sysDocTypeComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			sysDocTypeComboBox1.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			sysDocTypeComboBox1.Editable = true;
			sysDocTypeComboBox1.FilterString = "";
			sysDocTypeComboBox1.HasAllAccount = false;
			sysDocTypeComboBox1.HasCustom = false;
			sysDocTypeComboBox1.IsDataLoaded = false;
			sysDocTypeComboBox1.Location = new System.Drawing.Point(551, 61);
			sysDocTypeComboBox1.MaxDropDownItems = 12;
			sysDocTypeComboBox1.Name = "sysDocTypeComboBox1";
			sysDocTypeComboBox1.ShowInactiveItems = false;
			sysDocTypeComboBox1.ShowQuickAdd = true;
			sysDocTypeComboBox1.Size = new System.Drawing.Size(100, 20);
			sysDocTypeComboBox1.TabIndex = 4;
			sysDocTypeComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			sysDocTypeComboBox1.Visible = false;
			comboBoxDocType.Assigned = false;
			comboBoxDocType.CalcManager = ultraCalcManager1;
			comboBoxDocType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDocType.CustomReportFieldName = "";
			comboBoxDocType.CustomReportKey = "";
			comboBoxDocType.CustomReportValueType = 1;
			comboBoxDocType.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDocType.DisplayLayout.Appearance = appearance17;
			comboBoxDocType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDocType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxDocType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDocType.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxDocType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDocType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDocType.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDocType.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxDocType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDocType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDocType.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxDocType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDocType.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDocType.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxDocType.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxDocType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDocType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxDocType.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxDocType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDocType.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
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
			comboBoxGridUser.CalcManager = ultraCalcManager1;
			comboBoxGridUser.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridUser.CustomReportFieldName = "";
			comboBoxGridUser.CustomReportKey = "";
			comboBoxGridUser.CustomReportValueType = 1;
			comboBoxGridUser.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridUser.DisplayLayout.Appearance = appearance29;
			comboBoxGridUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUser.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridUser.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridUser.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridUser.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridUser.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridUser.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridUser.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridUser.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridUser.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridUser.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridUser.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridUser.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
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
			comboBoxGridTaskSteps.CalcManager = ultraCalcManager1;
			comboBoxGridTaskSteps.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridTaskSteps.CustomReportFieldName = "";
			comboBoxGridTaskSteps.CustomReportKey = "";
			comboBoxGridTaskSteps.CustomReportValueType = 1;
			comboBoxGridTaskSteps.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridTaskSteps.DisplayLayout.Appearance = appearance41;
			comboBoxGridTaskSteps.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridTaskSteps.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridTaskSteps.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxGridTaskSteps.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridTaskSteps.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridTaskSteps.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridTaskSteps.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxGridTaskSteps.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridTaskSteps.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridTaskSteps.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridTaskSteps.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridTaskSteps.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridTaskSteps.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxGridTaskSteps.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridTaskSteps.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
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
			dataGridTaskSteps.CalcManager = ultraCalcManager1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridTaskSteps.DisplayLayout.Appearance = appearance53;
			dataGridTaskSteps.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridTaskSteps.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTaskSteps.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			dataGridTaskSteps.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridTaskSteps.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			dataGridTaskSteps.DisplayLayout.MaxColScrollRegions = 1;
			dataGridTaskSteps.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridTaskSteps.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridTaskSteps.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			dataGridTaskSteps.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridTaskSteps.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridTaskSteps.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridTaskSteps.DisplayLayout.Override.CellAppearance = appearance60;
			dataGridTaskSteps.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridTaskSteps.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			dataGridTaskSteps.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			dataGridTaskSteps.DisplayLayout.Override.HeaderAppearance = appearance62;
			dataGridTaskSteps.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridTaskSteps.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			dataGridTaskSteps.DisplayLayout.Override.RowAppearance = appearance63;
			dataGridTaskSteps.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridTaskSteps.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
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
			dataGridTaskSteps.Size = new System.Drawing.Size(1026, 346);
			dataGridTaskSteps.TabIndex = 0;
			dataGridTaskSteps.Text = "dataEntryGrid1";
			textBoxAssgnDocNumber.Location = new System.Drawing.Point(441, 119);
			textBoxAssgnDocNumber.MaxLength = 15;
			textBoxAssgnDocNumber.Name = "textBoxAssgnDocNumber";
			textBoxAssgnDocNumber.Size = new System.Drawing.Size(170, 20);
			textBoxAssgnDocNumber.TabIndex = 7;
			appearance65.FontData.BoldAsString = "False";
			appearance65.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance65;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 92);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(58, 15);
			ultraFormattedLinkLabel1.TabIndex = 201;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Task Type:";
			appearance66.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance66;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(11, 121);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(57, 13);
			label5.TabIndex = 199;
			label5.Text = "Doc Type:";
			textBoxAssgnSysDoc.AlwaysInEditMode = true;
			textBoxAssgnSysDoc.Assigned = false;
			textBoxAssgnSysDoc.CalcManager = ultraCalcManager1;
			textBoxAssgnSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxAssgnSysDoc.CustomReportFieldName = "";
			textBoxAssgnSysDoc.CustomReportKey = "";
			textBoxAssgnSysDoc.CustomReportValueType = 1;
			textBoxAssgnSysDoc.DescriptionTextBox = null;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			textBoxAssgnSysDoc.DisplayLayout.Appearance = appearance67;
			textBoxAssgnSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			textBoxAssgnSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance68.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance68.BorderColor = System.Drawing.SystemColors.Window;
			textBoxAssgnSysDoc.DisplayLayout.GroupByBox.Appearance = appearance68;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			textBoxAssgnSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance69;
			textBoxAssgnSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance70.BackColor2 = System.Drawing.SystemColors.Control;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance70.ForeColor = System.Drawing.SystemColors.GrayText;
			textBoxAssgnSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance70;
			textBoxAssgnSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			textBoxAssgnSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.ForeColor = System.Drawing.SystemColors.ControlText;
			textBoxAssgnSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance71;
			appearance72.BackColor = System.Drawing.SystemColors.Highlight;
			appearance72.ForeColor = System.Drawing.SystemColors.HighlightText;
			textBoxAssgnSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance72;
			textBoxAssgnSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			textBoxAssgnSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			textBoxAssgnSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance73;
			appearance74.BorderColor = System.Drawing.Color.Silver;
			appearance74.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			textBoxAssgnSysDoc.DisplayLayout.Override.CellAppearance = appearance74;
			textBoxAssgnSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			textBoxAssgnSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance75.BackColor = System.Drawing.SystemColors.Control;
			appearance75.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance75.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance75.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance75.BorderColor = System.Drawing.SystemColors.Window;
			textBoxAssgnSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance75;
			appearance76.TextHAlignAsString = "Left";
			textBoxAssgnSysDoc.DisplayLayout.Override.HeaderAppearance = appearance76;
			textBoxAssgnSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			textBoxAssgnSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			textBoxAssgnSysDoc.DisplayLayout.Override.RowAppearance = appearance77;
			textBoxAssgnSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLight;
			textBoxAssgnSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance78;
			textBoxAssgnSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			textBoxAssgnSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			textBoxAssgnSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			textBoxAssgnSysDoc.DivisionID = "";
			textBoxAssgnSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			textBoxAssgnSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			textBoxAssgnSysDoc.Editable = true;
			textBoxAssgnSysDoc.ExcludeFromSecurity = false;
			textBoxAssgnSysDoc.FilterString = "";
			textBoxAssgnSysDoc.HasAllAccount = false;
			textBoxAssgnSysDoc.HasCustom = false;
			textBoxAssgnSysDoc.IsDataLoaded = false;
			textBoxAssgnSysDoc.Location = new System.Drawing.Point(334, 119);
			textBoxAssgnSysDoc.MaxDropDownItems = 12;
			textBoxAssgnSysDoc.Name = "textBoxAssgnSysDoc";
			textBoxAssgnSysDoc.ShowAll = false;
			textBoxAssgnSysDoc.ShowInactiveItems = false;
			textBoxAssgnSysDoc.ShowQuickAdd = true;
			textBoxAssgnSysDoc.Size = new System.Drawing.Size(100, 20);
			textBoxAssgnSysDoc.TabIndex = 6;
			textBoxAssgnSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTransactionType.Assigned = false;
			comboBoxTransactionType.CalcManager = ultraCalcManager1;
			comboBoxTransactionType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransactionType.CustomReportFieldName = "";
			comboBoxTransactionType.CustomReportKey = "";
			comboBoxTransactionType.CustomReportValueType = 1;
			comboBoxTransactionType.DescriptionTextBox = null;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTransactionType.DisplayLayout.Appearance = appearance79;
			comboBoxTransactionType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTransactionType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance80.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance80.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransactionType.DisplayLayout.GroupByBox.Appearance = appearance80;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransactionType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance81;
			comboBoxTransactionType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance82.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance82.BackColor2 = System.Drawing.SystemColors.Control;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance82.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransactionType.DisplayLayout.GroupByBox.PromptAppearance = appearance82;
			comboBoxTransactionType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTransactionType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTransactionType.DisplayLayout.Override.ActiveCellAppearance = appearance83;
			appearance84.BackColor = System.Drawing.SystemColors.Highlight;
			appearance84.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTransactionType.DisplayLayout.Override.ActiveRowAppearance = appearance84;
			comboBoxTransactionType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTransactionType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTransactionType.DisplayLayout.Override.CardAreaAppearance = appearance85;
			appearance86.BorderColor = System.Drawing.Color.Silver;
			appearance86.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTransactionType.DisplayLayout.Override.CellAppearance = appearance86;
			comboBoxTransactionType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTransactionType.DisplayLayout.Override.CellPadding = 0;
			appearance87.BackColor = System.Drawing.SystemColors.Control;
			appearance87.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance87.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance87.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance87.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransactionType.DisplayLayout.Override.GroupByRowAppearance = appearance87;
			appearance88.TextHAlignAsString = "Left";
			comboBoxTransactionType.DisplayLayout.Override.HeaderAppearance = appearance88;
			comboBoxTransactionType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTransactionType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			comboBoxTransactionType.DisplayLayout.Override.RowAppearance = appearance89;
			comboBoxTransactionType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTransactionType.DisplayLayout.Override.TemplateAddRowAppearance = appearance90;
			comboBoxTransactionType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTransactionType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTransactionType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTransactionType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransactionType.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxTransactionType.Editable = true;
			comboBoxTransactionType.FilterString = "";
			comboBoxTransactionType.HasAllAccount = false;
			comboBoxTransactionType.HasCustom = false;
			comboBoxTransactionType.IsDataLoaded = false;
			comboBoxTransactionType.Location = new System.Drawing.Point(109, 117);
			comboBoxTransactionType.MaxDropDownItems = 12;
			comboBoxTransactionType.Name = "comboBoxTransactionType";
			comboBoxTransactionType.ShowInactiveItems = false;
			comboBoxTransactionType.ShowQuickAdd = true;
			comboBoxTransactionType.Size = new System.Drawing.Size(121, 20);
			comboBoxTransactionType.TabIndex = 5;
			comboBoxTransactionType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTransactionType.SelectedIndexChanged += new System.EventHandler(comboBoxTransactionType_SelectedIndexChanged);
			buttonSelectDoc.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDoc.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDoc.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDoc.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDoc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSelectDoc.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDoc.Location = new System.Drawing.Point(611, 117);
			buttonSelectDoc.Name = "buttonSelectDoc";
			buttonSelectDoc.Size = new System.Drawing.Size(34, 24);
			buttonSelectDoc.TabIndex = 197;
			buttonSelectDoc.Text = "...";
			buttonSelectDoc.UseVisualStyleBackColor = false;
			buttonSelectDoc.Click += new System.EventHandler(buttonSelectDoc_Click);
			mmLabel7.AutoSize = true;
			mmLabel7.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel7.IsFieldHeader = false;
			mmLabel7.IsRequired = false;
			mmLabel7.Location = new System.Drawing.Point(444, 103);
			mmLabel7.Name = "mmLabel7";
			mmLabel7.PenWidth = 1f;
			mmLabel7.ShowBorder = false;
			mmLabel7.Size = new System.Drawing.Size(90, 13);
			mmLabel7.TabIndex = 196;
			mmLabel7.Text = "Voucher Number:";
			mmLabel6.AutoSize = true;
			mmLabel6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel6.IsFieldHeader = false;
			mmLabel6.IsRequired = false;
			mmLabel6.Location = new System.Drawing.Point(337, 102);
			mmLabel6.Name = "mmLabel6";
			mmLabel6.PenWidth = 1f;
			mmLabel6.ShowBorder = false;
			mmLabel6.Size = new System.Drawing.Size(58, 13);
			mmLabel6.TabIndex = 195;
			mmLabel6.Text = "SysDocID:";
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(267, 123);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(65, 13);
			mmLabel5.TabIndex = 192;
			mmLabel5.Text = "Linked Doc:";
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(109, 65);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(334, 20);
			textBoxName.TabIndex = 3;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(11, 147);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(69, 13);
			mmLabel1.TabIndex = 188;
			mmLabel1.Text = "Start Date:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(497, 43);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(38, 13);
			mmLabel4.TabIndex = 177;
			mmLabel4.Text = "Date:";
			comboBoxTaskType.Assigned = false;
			comboBoxTaskType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTaskType.CalcManager = ultraCalcManager1;
			comboBoxTaskType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTaskType.CustomReportFieldName = "";
			comboBoxTaskType.CustomReportKey = "";
			comboBoxTaskType.CustomReportValueType = 1;
			comboBoxTaskType.DescriptionTextBox = null;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTaskType.DisplayLayout.Appearance = appearance91;
			comboBoxTaskType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTaskType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaskType.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaskType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxTaskType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTaskType.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxTaskType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTaskType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTaskType.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTaskType.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxTaskType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTaskType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTaskType.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTaskType.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxTaskType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTaskType.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTaskType.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxTaskType.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxTaskType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTaskType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxTaskType.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxTaskType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTaskType.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
			comboBoxTaskType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTaskType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTaskType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTaskType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTaskType.Editable = true;
			comboBoxTaskType.FilterString = "";
			comboBoxTaskType.HasAllAccount = false;
			comboBoxTaskType.HasCustom = false;
			comboBoxTaskType.IsDataLoaded = false;
			comboBoxTaskType.Location = new System.Drawing.Point(109, 91);
			comboBoxTaskType.MaxDropDownItems = 12;
			comboBoxTaskType.Name = "comboBoxTaskType";
			comboBoxTaskType.ShowInactiveItems = false;
			comboBoxTaskType.ShowQuickAdd = true;
			comboBoxTaskType.Size = new System.Drawing.Size(121, 20);
			comboBoxTaskType.TabIndex = 4;
			comboBoxTaskType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTaskType.SelectedIndexChanged += new System.EventHandler(comboBoxTaskType_SelectedIndexChanged);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance103;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance104.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance104.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance104;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance105;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance106.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance106.BackColor2 = System.Drawing.SystemColors.Control;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance106.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance106;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance107;
			appearance108.BackColor = System.Drawing.SystemColors.Highlight;
			appearance108.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance108;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance109;
			appearance110.BorderColor = System.Drawing.Color.Silver;
			appearance110.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance110;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance111.BackColor = System.Drawing.SystemColors.Control;
			appearance111.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance111.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance111.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance111.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance111;
			appearance112.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance112;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance113;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance114;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(109, 40);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
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
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(11, 69);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(65, 13);
			label1.TabIndex = 202;
			label1.Text = "Task Name:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(1064, 595);
			base.Controls.Add(label1);
			base.Controls.Add(ultraFormattedLinkLabel1);
			base.Controls.Add(textBoxAssgnSysDoc);
			base.Controls.Add(label5);
			base.Controls.Add(comboBoxTransactionType);
			base.Controls.Add(buttonSelectDoc);
			base.Controls.Add(mmLabel7);
			base.Controls.Add(mmLabel6);
			base.Controls.Add(textBoxAssgnDocNumber);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(textBoxName);
			base.Controls.Add(groupBox3);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(dateTimePickerStartDate);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(comboBoxTaskType);
			base.Controls.Add(ulinkDocId);
			base.Controls.Add(comboBoxSysDoc);
			base.Controls.Add(ultraLinkVoucherNumber);
			base.Controls.Add(textBoxVoucherNumber);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(570, 285);
			base.Name = "TaskTransactionForm";
			Text = "Task Transaction";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)sysDocTypeComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDocType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridUser).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridTaskSteps).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridTaskSteps).EndInit();
			((System.ComponentModel.ISupportInitialize)textBoxAssgnSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransactionType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTaskType).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
