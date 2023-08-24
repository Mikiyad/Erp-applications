using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class BudgetingForm : Form, IForm, IWorkFlowForm
	{
		private BudgetingData currentData;

		private const string TABLENAME_CONST = "Budget";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool isCostCenterMandatory = CompanyPreferences.IsCostCenterMandatory;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private IContainer components;

		private ToolStrip toolStrip1;

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

		private DataEntryGrid dataGridItems;

		private DateTimePicker dateTimePickerDateFrom;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private CurrencySelector currencySelector;

		private AllAccountsComboBox comboBoxGridAccount;

		private AnalysisComboBox comboBoxGridanalysis;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraLabel ultraLabel1;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelTotal;

		private PayeeTypeComboBox payeeTypeComboBox1;

		private customersFlatComboBox comboBoxGridCustomer;

		private vendorsFlatComboBox comboBoxGridVendor;

		private EmployeeComboBox comboBoxGridEmployee;

		private CostCenterComboBox comboBoxGridCostCenter;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private JobComboBox comboBoxJob;

		private CostCategoryComboBox comboBoxCostCategory;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem reverseToolStripMenuItem;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonAttach;

		private Label label3;

		private Label label2;

		private Label label1;

		private DateTimePicker dateTimePickerDateTo;

		private RadioButton radioButtonExpense;

		private RadioButton radioButtonIncome;

		private Label label4;

		private Label label5;

		private TextBox textBoxRef2;

		private TextBox textBoxRef1;

		private UltraFormattedLinkLabel labelCurrency;

		private CurrencySelector currencySelector1;

		private AccountGroupComboBox comboBoxAccountGroup;

		private MMLabel mmLabel2;

		private DateTimePicker dateTimePickerDate;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1024;

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
				if (value)
				{
					comboBoxSysDoc.Enabled = true;
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.Enabled = true;
					comboBoxSysDoc.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
					comboBoxSysDoc.Enabled = false;
				}
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
				SetApprovalStatus();
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
					panelDetails.Enabled = !value;
					dataGridItems.Enabled = !value;
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
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

		public BudgetingForm()
		{
			InitializeComponent();
			comboBoxSysDoc.FilterByType(SysDocTypes.Budgeting);
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			comboBoxGridAccount.SelectedIndexChanged += comboBoxGridAccount_SelectedIndexChanged;
			comboBoxGridCustomer.SelectedIndexChanged += comboBoxGridAccount_SelectedIndexChanged;
			comboBoxGridVendor.SelectedIndexChanged += comboBoxGridAccount_SelectedIndexChanged;
			comboBoxGridEmployee.SelectedIndexChanged += comboBoxGridAccount_SelectedIndexChanged;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += Form_KeyDown;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
			comboBoxGridAccount.FilterSysDocID = comboBoxSysDoc.SelectedID;
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && e.Cell.Column.Key == "Account Code")
			{
				dataGridItems.ActiveRow.Cells["Account Name"].Value = comboBoxGridAccount.SelectedName;
			}
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			labelTotal.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			labelTotal.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && sender.GetType() == typeof(AllAccountsComboBox) && dataGridItems.ActiveRow.Cells["Analysis"].Text != "" && !comboBoxGridanalysis.IsValidAccountAnalysis(comboBoxGridAccount.SelectedID, dataGridItems.ActiveRow.Cells["Analysis"].Text))
			{
				dataGridItems.ActiveRow.Cells["Analysis"].Value = "";
			}
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Account Code")
			{
				if (dataGridItems.ActiveRow.Cells["Account Code"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Account Name"].Value = "";
				}
			}
			else if ((dataGridItems.ActiveCell.Column.Key == "" || dataGridItems.ActiveCell.Column.Key == "") && dataGridItems.ActiveCell.Text != "")
			{
				dataGridItems.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridItems.ActiveCell.Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Account Code"].Value.ToString() == "" && activeRow.Cells["Description"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage(UIMessages.SelectAccount);
					e.Cancel = true;
					activeRow.Cells["Account Code"].Activate();
				}
				else
				{
					labelTotal.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "" || e.Cell.Column.Key == "")
			{
				labelTotal.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Account Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridAccount.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridAccount.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "C.C.")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridCostCenter.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridCostCenter.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Analysis")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "" || dataGridItems.ActiveCell.Column.Key.ToString() == "")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BudgetingData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BudgetTable.Rows[0] : currentData.BudgetTable.NewRow();
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["DateFrom"] = dateTimePickerDateFrom.Text;
				dataRow["DateTo"] = dateTimePickerDateTo.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				if (radioButtonIncome.Checked)
				{
					dataRow["BudgetType"] = "I";
				}
				else
				{
					dataRow["BudgetType"] = "E";
				}
				if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID)
				{
					dataRow["CurrencyID"] = currencySelector.SelectedID;
					dataRow["CurrencyRate"] = currencySelector.Rate;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = 1;
				}
				string a = dataRow["BudgetType"].ToString();
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.BudgetTable.Rows.Add(dataRow);
				}
				currentData.BudgetDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.BudgetDetailsTable.NewRow();
					dataRow2.BeginEdit();
					if (a == "I")
					{
						dataRow2["Debit"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow2["Credit"] = row.Cells["Amount"].Value.ToString();
					}
					if (row.Cells["Group ID"].Value.ToString() != "")
					{
						dataRow2["GroupID"] = row.Cells["Group ID"].Value.ToString();
					}
					else
					{
						dataRow2["GroupID"] = DBNull.Value;
					}
					if (row.Cells["Account Code"].Value.ToString() != "")
					{
						dataRow2["AccountID"] = row.Cells["Account Code"].Value.ToString();
					}
					else
					{
						dataRow2["AccountID"] = DBNull.Value;
					}
					if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
					{
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					if (row.Cells["C.C."].Value.ToString() != "")
					{
						dataRow2["CostCenterID"] = row.Cells["C.C."].Value.ToString();
					}
					else
					{
						dataRow2["CostCenterID"] = DBNull.Value;
					}
					if (row.Cells["Analysis"].Value.ToString() != "")
					{
						dataRow2["AnalysisID"] = row.Cells["Analysis"].Value.ToString();
					}
					else
					{
						dataRow2["AnalysisID"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.BudgetDetailsTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Group ID");
				dataTable.Columns.Add("Account Code");
				dataTable.Columns.Add("Account Name");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("C.C.");
				dataTable.Columns.Add("Analysis");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Code"].ValueList = comboBoxGridAccount;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Header.Caption = "Job";
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["C.C."].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["C.C."].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["C.C."].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["C.C."].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["C.C."].ValueList = comboBoxGridCostCenter;
				dataGridItems.DisplayLayout.Bands[0].Columns["Group ID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Group ID"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Group ID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Group ID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Group ID"].ValueList = comboBoxAccountGroup;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Analysis"].ValueList = comboBoxGridanalysis;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Total", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Total"].DisplayFormat = "{0:n}";
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		public void LoadData(string journalID)
		{
			try
			{
				if (!(journalID.Trim() == "") && CanClose())
				{
					currentData = Factory.BudgetingSystem.GetBudgetingByID(SystemDocID, journalID);
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
					DataRow dataRow = currentData.Tables["Budget"].Rows[0];
					textBoxVoucherNumber.Clear();
					if (dataRow["DateFrom"] != DBNull.Value)
					{
						dateTimePickerDateFrom.Value = DateTime.Parse(dataRow["DateFrom"].ToString());
					}
					if (dataRow["DateTo"] != DBNull.Value)
					{
						dateTimePickerDateTo.Value = DateTime.Parse(dataRow["DateTo"].ToString());
					}
					if (dataRow["TransactionDate"] != DBNull.Value)
					{
						dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					}
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					string a = dataRow["BudgetType"].ToString().TrimEnd();
					if (dataRow["CurrencyID"] != DBNull.Value)
					{
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						currencySelector.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					}
					else
					{
						currencySelector.SelectedID = "";
						currencySelector.Rate = 1m;
					}
					if (a == "I")
					{
						radioButtonIncome.Checked = true;
					}
					if (a == "E")
					{
						radioButtonExpense.Checked = true;
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Budget_Details") && currentData.BudgetDetailsTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Budget_Details"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Account Code"] = row["AccountID"];
							if (a == "I")
							{
								dataRow3["Amount"] = row["Debit"];
							}
							if (a == "E")
							{
								dataRow3["Amount"] = row["Credit"];
							}
							dataRow3["Account Name"] = row["AccountName"];
							if (row["JobID"] != DBNull.Value)
							{
								dataRow3["Job"] = row["JobID"];
							}
							dataRow3["Group ID"] = row["GroupID"].ToString();
							dataRow3["Description"] = row["Description"].ToString();
							dataRow3["C.C."] = row["CostCenterID"].ToString();
							dataRow3["Analysis"] = row["AnalysisID"].ToString();
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
					}
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
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
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
				bool flag = (!isNewRecord) ? Factory.BudgetingSystem.CreateBugeting(currentData, isUpdate: true) : Factory.BudgetingSystem.CreateBugeting(currentData, isUpdate: false);
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
				if (ex.Number == 1028)
				{
					ErrorHelper.WarningMessage(UIMessages.PayeeMustHaveAccount);
				}
				else
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
				}
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Budget", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59, 999);
			if (isNewRecord && dateTimePickerDateFrom.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDateFrom.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!dataGridItems.HasRowAnyValue(row))
				{
					row.Delete(displayPrompt: false);
				}
				else
				{
					if (row.Cells["Account Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage(UIMessages.SelectAccount);
						row.Activate();
						return false;
					}
					if (isCostCenterMandatory && row.Cells["C.C."].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select C.C. for the row");
						row.Activate();
						return false;
					}
				}
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Budget", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Amount"].Value != null && row.Cells["Amount"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Amount"].Value.ToString());
				}
			}
			return result;
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
				dateTimePickerDateFrom.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				radioButtonIncome.Checked = true;
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerDateFrom.Value = DateTime.Now;
				dateTimePickerDateTo.Value = DateTime.Now;
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				labelTotal.Text = "";
				currencySelector.SelectedID = Global.BaseCurrencyID;
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				SetApprovalStatus();
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
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
				return Factory.BudgetingSystem.DeleteBudgeting(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Budget", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Budget", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Budget", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Budget", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Budget", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
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
				dataGridItems.SetupUI();
				SetupGrid();
				dataGridItems.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
				comboBoxSysDoc.FilterByType(SysDocTypes.Budgeting);
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Analysis")
			{
				string accountID = "jfk23lf";
				if (dataGridItems.ActiveRow.Cells["Account Code"].Value.ToString() != "")
				{
					accountID = dataGridItems.ActiveRow.Cells["Account Code"].Value.ToString();
				}
				comboBoxGridanalysis.FilterByAccount(accountID);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(currencySelector.SelectedID);
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
				DialogResult dialogResult = (!isVoid) ? ErrorHelper.QuestionMessageYesNo(UIMessages.WantToUnvoid) : ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid);
				if (dialogResult == DialogResult.No)
				{
					return false;
				}
				return Factory.JournalSystem.VoidJournalVoucher(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
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
					string systemDocID = SystemDocID;
					string text = textBoxVoucherNumber.Text;
					DataSet budgetingToPrint = Factory.BudgetingSystem.GetBudgetingToPrint(systemDocID, text);
					if (budgetingToPrint == null || budgetingToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(budgetingToPrint, systemDocID, "Budgeting", SysDocTypes.Budgeting, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void toolStripSeparator2_Click(object sender, EventArgs e)
		{
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != SystemDocID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.BudgetingListFormObj);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.Budgeting);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.Show();
		}

		private void toolStripButtonDistribution_Click_1(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to copy this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridCell cell in row.Cells)
					{
						if (cell.Activation == Activation.Disabled)
						{
							cell.Activation = Activation.AllowEdit;
						}
					}
				}
			}
		}

		private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to create a reverse copy of this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					foreach (UltraGridCell cell in row.Cells)
					{
						if (cell.Activation == Activation.Disabled)
						{
							cell.Activation = Activation.AllowEdit;
						}
					}
				}
			}
		}

		public void SetApprovalStatus()
		{
			if (IsNewRecord)
			{
				ToolStripButton toolStripButton = toolStripButtonApproval;
				ToolStripLabel toolStripLabel = toolStripLabelApproval;
				bool flag2 = toolStripSeparatorApproval.Visible = false;
				bool visible = toolStripLabel.Visible = flag2;
				toolStripButton.Visible = visible;
			}
			else
			{
				if (currentData == null || currentData.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				DataRow dataRow = currentData.Tables[0].Rows[0];
				bool flag2;
				bool visible;
				if (!dataRow.Table.Columns.Contains("ApprovalStatus") || dataRow["ApprovalStatus"].IsDBNullOrEmpty())
				{
					ToolStripButton toolStripButton2 = toolStripButtonApproval;
					ToolStripLabel toolStripLabel2 = toolStripLabelApproval;
					flag2 = (toolStripSeparatorApproval.Visible = false);
					visible = (toolStripLabel2.Visible = flag2);
					toolStripButton2.Visible = visible;
					return;
				}
				switch (int.Parse(dataRow["ApprovalStatus"].ToString()))
				{
				case 3:
					toolStripButtonApproval.Text = "Rejected";
					toolStripButtonApproval.ForeColor = Color.Red;
					break;
				case 10:
					toolStripButtonApproval.Text = "Approved";
					toolStripButtonApproval.ForeColor = Color.ForestGreen;
					break;
				default:
					toolStripButtonApproval.Text = "Pending";
					toolStripButtonApproval.ForeColor = Color.Orange;
					break;
				}
				ToolStripButton toolStripButton3 = toolStripButtonApproval;
				ToolStripLabel toolStripLabel3 = toolStripLabelApproval;
				flag2 = (toolStripSeparatorApproval.Visible = true);
				visible = (toolStripLabel3.Visible = flag2);
				toolStripButton3.Visible = visible;
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Budget", "VoucherID");
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BudgetingForm));
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
			Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			reverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimePickerDateFrom = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelTotal = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			currencySelector1 = new Micromind.DataControls.CurrencySelector();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			radioButtonExpense = new System.Windows.Forms.RadioButton();
			radioButtonIncome = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			dateTimePickerDateTo = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			labelVoided = new System.Windows.Forms.Label();
			comboBoxGridCostCenter = new Micromind.DataControls.CostCenterComboBox();
			payeeTypeComboBox1 = new Micromind.DataControls.PayeeTypeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			comboBoxGridanalysis = new Micromind.DataControls.AnalysisComboBox();
			comboBoxGridAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxGridVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxGridCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxAccountGroup = new Micromind.DataControls.AccountGroupComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)payeeTypeComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[19]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator5,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(923, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 31);
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripSeparator2.Click += new System.EventHandler(toolStripSeparator2_Click);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
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
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator4,
				reverseToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(111, 6);
			reverseToolStripMenuItem.Name = "reverseToolStripMenuItem";
			reverseToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			reverseToolStripMenuItem.Text = "Reverse";
			reverseToolStripMenuItem.Click += new System.EventHandler(reverseToolStripMenuItem_Click);
			toolStripButtonApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonApproval.AutoSize = false;
			toolStripButtonApproval.BackColor = System.Drawing.Color.Transparent;
			toolStripButtonApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripButtonApproval.ForeColor = System.Drawing.Color.Green;
			toolStripButtonApproval.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			toolStripButtonApproval.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonApproval.Name = "toolStripButtonApproval";
			toolStripButtonApproval.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			toolStripButtonApproval.Size = new System.Drawing.Size(70, 22);
			toolStripButtonApproval.Text = "Pending";
			toolStripLabelApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripLabelApproval.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
			toolStripLabelApproval.Name = "toolStripLabelApproval";
			toolStripLabelApproval.Size = new System.Drawing.Size(45, 28);
			toolStripLabelApproval.Text = "Status:";
			toolStripSeparatorApproval.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripSeparatorApproval.Name = "toolStripSeparatorApproval";
			toolStripSeparatorApproval.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 458);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(923, 40);
			panelButtons.TabIndex = 2;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 2;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Click += new System.EventHandler(buttonVoid_Click);
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(318, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 3;
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
			buttonNew.TabIndex = 1;
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
			linePanelDown.Size = new System.Drawing.Size(923, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(813, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
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
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimePickerDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDateFrom.Location = new System.Drawing.Point(379, 49);
			dateTimePickerDateFrom.Name = "dateTimePickerDateFrom";
			dateTimePickerDateFrom.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDateFrom.TabIndex = 4;
			textBoxVoucherNumber.Location = new System.Drawing.Point(379, 4);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(139, 20);
			textBoxVoucherNumber.TabIndex = 2;
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(271, 6);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance3.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			appearance3.TextHAlignAsString = "Right";
			appearance3.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance3;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(754, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Total:";
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance4.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance4.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance4;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelTotal);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 426);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(899, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelTotal.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance5.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			appearance5.TextHAlignAsString = "Right";
			appearance5.TextVAlignAsString = "Middle";
			labelTotal.Appearance = appearance5;
			labelTotal.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelTotal.Location = new System.Drawing.Point(756, 3);
			labelTotal.Name = "labelTotal";
			labelTotal.Size = new System.Drawing.Size(141, 16);
			labelTotal.TabIndex = 113;
			labelTotal.Text = "0.00";
			panelDetails.Controls.Add(mmLabel2);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(currencySelector1);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(radioButtonExpense);
			panelDetails.Controls.Add(radioButtonIncome);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(dateTimePickerDateTo);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDateFrom);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(777, 95);
			panelDetails.TabIndex = 0;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = true;
			mmLabel2.Location = new System.Drawing.Point(530, 8);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(38, 13);
			mmLabel2.TabIndex = 131;
			mmLabel2.Text = "Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(571, 4);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDate.TabIndex = 132;
			appearance6.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance6;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(271, 28);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(49, 14);
			labelCurrency.TabIndex = 130;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance7.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance7;
			currencySelector1.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector1.Location = new System.Drawing.Point(379, 26);
			currencySelector1.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector1.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector1.Name = "currencySelector1";
			currencySelector1.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector1.SelectedID = "";
			currencySelector1.Size = new System.Drawing.Size(139, 20);
			currencySelector1.TabIndex = 3;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(15, 51);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 127;
			label4.Text = "Reference 2:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(15, 29);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(69, 13);
			label5.TabIndex = 128;
			label5.Text = "Reference 1:";
			textBoxRef2.Location = new System.Drawing.Point(123, 47);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(139, 20);
			textBoxRef2.TabIndex = 7;
			textBoxRef1.Location = new System.Drawing.Point(123, 26);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(139, 20);
			textBoxRef1.TabIndex = 6;
			radioButtonExpense.AutoSize = true;
			radioButtonExpense.Location = new System.Drawing.Point(200, 72);
			radioButtonExpense.Name = "radioButtonExpense";
			radioButtonExpense.Size = new System.Drawing.Size(66, 17);
			radioButtonExpense.TabIndex = 9;
			radioButtonExpense.Text = "Expense";
			radioButtonExpense.UseVisualStyleBackColor = true;
			radioButtonIncome.AutoSize = true;
			radioButtonIncome.Checked = true;
			radioButtonIncome.Location = new System.Drawing.Point(123, 72);
			radioButtonIncome.Name = "radioButtonIncome";
			radioButtonIncome.Size = new System.Drawing.Size(60, 17);
			radioButtonIncome.TabIndex = 8;
			radioButtonIncome.TabStop = true;
			radioButtonIncome.Text = "Income";
			radioButtonIncome.UseVisualStyleBackColor = true;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 74);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(71, 13);
			label3.TabIndex = 122;
			label3.Text = "Budget Type:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(340, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 13);
			label2.TabIndex = 121;
			label2.Text = "From:";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(530, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(23, 13);
			label1.TabIndex = 120;
			label1.Text = "To:";
			dateTimePickerDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDateTo.Location = new System.Drawing.Point(571, 51);
			dateTimePickerDateTo.Name = "dateTimePickerDateTo";
			dateTimePickerDateTo.Size = new System.Drawing.Size(139, 20);
			dateTimePickerDateTo.TabIndex = 5;
			appearance8.FontData.BoldAsString = "True";
			appearance8.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance8;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(18, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(44, 15);
			ultraFormattedLinkLabel5.TabIndex = 118;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance9;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance10;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance11.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance11;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance12;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance13.BackColor2 = System.Drawing.SystemColors.Control;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance13;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance14;
			appearance15.BackColor = System.Drawing.SystemColors.Highlight;
			appearance15.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance15;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance16;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance17;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance18.BackColor = System.Drawing.SystemColors.Control;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance18;
			appearance19.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance19;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance20;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance21;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = true;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(123, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(139, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(271, 52);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(70, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Valid Date:";
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(691, 82);
			currencySelector.MaximumSize = new System.Drawing.Size(99999, 20);
			currencySelector.MinimumSize = new System.Drawing.Size(5, 20);
			currencySelector.Name = "currencySelector";
			currencySelector.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			currencySelector.SelectedID = "";
			currencySelector.Size = new System.Drawing.Size(17, 20);
			currencySelector.TabIndex = 3;
			currencySelector.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 347);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(895, 81);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			comboBoxGridCostCenter.Assigned = false;
			comboBoxGridCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCostCenter.CustomReportFieldName = "";
			comboBoxGridCostCenter.CustomReportKey = "";
			comboBoxGridCostCenter.CustomReportValueType = 1;
			comboBoxGridCostCenter.DescriptionTextBox = null;
			appearance22.BackColor = System.Drawing.SystemColors.Window;
			appearance22.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCostCenter.DisplayLayout.Appearance = appearance22;
			comboBoxGridCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance23.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance23.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance23.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance23.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.Appearance = appearance23;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance24;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance25.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance25.BackColor2 = System.Drawing.SystemColors.Control;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance25;
			comboBoxGridCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance26;
			appearance27.BackColor = System.Drawing.SystemColors.Highlight;
			appearance27.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance27;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance28;
			appearance29.BorderColor = System.Drawing.Color.Silver;
			appearance29.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCostCenter.DisplayLayout.Override.CellAppearance = appearance29;
			comboBoxGridCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance30.BackColor = System.Drawing.SystemColors.Control;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance30;
			appearance31.TextHAlignAsString = "Left";
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderAppearance = appearance31;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCostCenter.DisplayLayout.Override.RowAppearance = appearance32;
			comboBoxGridCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance33.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance33;
			comboBoxGridCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCostCenter.Editable = true;
			comboBoxGridCostCenter.FilterString = "";
			comboBoxGridCostCenter.HasAllAccount = false;
			comboBoxGridCostCenter.HasCustom = false;
			comboBoxGridCostCenter.IsDataLoaded = false;
			comboBoxGridCostCenter.Location = new System.Drawing.Point(620, 251);
			comboBoxGridCostCenter.MaxDropDownItems = 12;
			comboBoxGridCostCenter.Name = "comboBoxGridCostCenter";
			comboBoxGridCostCenter.ShowInactiveItems = false;
			comboBoxGridCostCenter.ShowQuickAdd = true;
			comboBoxGridCostCenter.Size = new System.Drawing.Size(73, 20);
			comboBoxGridCostCenter.TabIndex = 0;
			comboBoxGridCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCostCenter.Visible = false;
			payeeTypeComboBox1.Assigned = false;
			payeeTypeComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			payeeTypeComboBox1.CustomReportFieldName = "";
			payeeTypeComboBox1.CustomReportKey = "";
			payeeTypeComboBox1.CustomReportValueType = 1;
			payeeTypeComboBox1.DescriptionTextBox = null;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			payeeTypeComboBox1.DisplayLayout.Appearance = appearance34;
			payeeTypeComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			payeeTypeComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance35.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance35.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance35.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance35.BorderColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.Appearance = appearance35;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance36;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance37.BackColor2 = System.Drawing.SystemColors.Control;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance37;
			payeeTypeComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			payeeTypeComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance38.BackColor = System.Drawing.SystemColors.Window;
			appearance38.ForeColor = System.Drawing.SystemColors.ControlText;
			payeeTypeComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance38;
			appearance39.BackColor = System.Drawing.SystemColors.Highlight;
			appearance39.ForeColor = System.Drawing.SystemColors.HighlightText;
			payeeTypeComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance39;
			payeeTypeComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			payeeTypeComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance40;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			appearance41.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			payeeTypeComboBox1.DisplayLayout.Override.CellAppearance = appearance41;
			payeeTypeComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			payeeTypeComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance42.BackColor = System.Drawing.SystemColors.Control;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance42;
			appearance43.TextHAlignAsString = "Left";
			payeeTypeComboBox1.DisplayLayout.Override.HeaderAppearance = appearance43;
			payeeTypeComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			payeeTypeComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			payeeTypeComboBox1.DisplayLayout.Override.RowAppearance = appearance44;
			payeeTypeComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance45.BackColor = System.Drawing.SystemColors.ControlLight;
			payeeTypeComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance45;
			payeeTypeComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			payeeTypeComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			payeeTypeComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			payeeTypeComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			payeeTypeComboBox1.Editable = true;
			payeeTypeComboBox1.FilterString = "";
			payeeTypeComboBox1.HasAllAccount = false;
			payeeTypeComboBox1.HasCustom = false;
			payeeTypeComboBox1.IsDataLoaded = false;
			payeeTypeComboBox1.Location = new System.Drawing.Point(672, 56);
			payeeTypeComboBox1.MaxDropDownItems = 12;
			payeeTypeComboBox1.Name = "payeeTypeComboBox1";
			payeeTypeComboBox1.ShowInactiveItems = false;
			payeeTypeComboBox1.ShowQuickAdd = true;
			payeeTypeComboBox1.Size = new System.Drawing.Size(69, 20);
			payeeTypeComboBox1.TabIndex = 111;
			payeeTypeComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			payeeTypeComboBox1.Visible = false;
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			appearance46.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance46;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance47.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance47.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance47.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance47.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance47;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance48;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance49.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance49.BackColor2 = System.Drawing.SystemColors.Control;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance49;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance50;
			appearance51.BackColor = System.Drawing.SystemColors.Highlight;
			appearance51.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance51;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance52;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			appearance53.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance53;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance54.BackColor = System.Drawing.SystemColors.Control;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance54;
			appearance55.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance55;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance56;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance57;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 126);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(897, 302);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCategory.DisplayLayout.Appearance = appearance58;
			comboBoxCostCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance59.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.GroupByBox.Appearance = appearance59;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance60;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance61.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance61.BackColor2 = System.Drawing.SystemColors.Control;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance61;
			comboBoxCostCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveCellAppearance = appearance62;
			appearance63.BackColor = System.Drawing.SystemColors.Highlight;
			appearance63.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveRowAppearance = appearance63;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.CardAreaAppearance = appearance64;
			appearance65.BorderColor = System.Drawing.Color.Silver;
			appearance65.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCategory.DisplayLayout.Override.CellAppearance = appearance65;
			comboBoxCostCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCategory.DisplayLayout.Override.CellPadding = 0;
			appearance66.BackColor = System.Drawing.SystemColors.Control;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.GroupByRowAppearance = appearance66;
			appearance67.TextHAlignAsString = "Left";
			comboBoxCostCategory.DisplayLayout.Override.HeaderAppearance = appearance67;
			comboBoxCostCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance68.BackColor = System.Drawing.SystemColors.Window;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCategory.DisplayLayout.Override.RowAppearance = appearance68;
			comboBoxCostCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance69;
			comboBoxCostCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(662, 138);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(79, 20);
			comboBoxCostCategory.TabIndex = 123;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxCostCategory.Visible = false;
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
			comboBoxJob.Location = new System.Drawing.Point(640, 173);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 122;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			comboBoxGridanalysis.Assigned = false;
			comboBoxGridanalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridanalysis.CustomReportFieldName = "";
			comboBoxGridanalysis.CustomReportKey = "";
			comboBoxGridanalysis.CustomReportValueType = 1;
			comboBoxGridanalysis.DescriptionTextBox = null;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridanalysis.DisplayLayout.Appearance = appearance70;
			comboBoxGridanalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridanalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance71.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.Appearance = appearance71;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance72;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance73.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance73.BackColor2 = System.Drawing.SystemColors.Control;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridanalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance73;
			comboBoxGridanalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridanalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance74.BackColor = System.Drawing.SystemColors.Window;
			appearance74.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveCellAppearance = appearance74;
			appearance75.BackColor = System.Drawing.SystemColors.Highlight;
			appearance75.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridanalysis.DisplayLayout.Override.ActiveRowAppearance = appearance75;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridanalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.CardAreaAppearance = appearance76;
			appearance77.BorderColor = System.Drawing.Color.Silver;
			appearance77.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridanalysis.DisplayLayout.Override.CellAppearance = appearance77;
			comboBoxGridanalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridanalysis.DisplayLayout.Override.CellPadding = 0;
			appearance78.BackColor = System.Drawing.SystemColors.Control;
			appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance78.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridanalysis.DisplayLayout.Override.GroupByRowAppearance = appearance78;
			appearance79.TextHAlignAsString = "Left";
			comboBoxGridanalysis.DisplayLayout.Override.HeaderAppearance = appearance79;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridanalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance80.BackColor = System.Drawing.SystemColors.Window;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridanalysis.DisplayLayout.Override.RowAppearance = appearance80;
			comboBoxGridanalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance81.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridanalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance81;
			comboBoxGridanalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridanalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridanalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridanalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridanalysis.Editable = true;
			comboBoxGridanalysis.FilterString = "";
			comboBoxGridanalysis.HasAllAccount = false;
			comboBoxGridanalysis.HasCustom = false;
			comboBoxGridanalysis.IsDataLoaded = false;
			comboBoxGridanalysis.Location = new System.Drawing.Point(595, 199);
			comboBoxGridanalysis.MaxDropDownItems = 12;
			comboBoxGridanalysis.Name = "comboBoxGridanalysis";
			comboBoxGridanalysis.ShowInactiveItems = false;
			comboBoxGridanalysis.ShowQuickAdd = true;
			comboBoxGridanalysis.Size = new System.Drawing.Size(146, 20);
			comboBoxGridanalysis.TabIndex = 109;
			comboBoxGridanalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridanalysis.Visible = false;
			comboBoxGridAccount.Assigned = false;
			comboBoxGridAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridAccount.CustomReportFieldName = "";
			comboBoxGridAccount.CustomReportKey = "";
			comboBoxGridAccount.CustomReportValueType = 1;
			comboBoxGridAccount.DescriptionTextBox = null;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridAccount.DisplayLayout.Appearance = appearance82;
			comboBoxGridAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.GroupByBox.Appearance = appearance83;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance84;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance85.BackColor2 = System.Drawing.SystemColors.Control;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance85;
			comboBoxGridAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveCellAppearance = appearance86;
			appearance87.BackColor = System.Drawing.SystemColors.Highlight;
			appearance87.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveRowAppearance = appearance87;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.CardAreaAppearance = appearance88;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			appearance89.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridAccount.DisplayLayout.Override.CellAppearance = appearance89;
			comboBoxGridAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridAccount.DisplayLayout.Override.CellPadding = 0;
			appearance90.BackColor = System.Drawing.SystemColors.Control;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.GroupByRowAppearance = appearance90;
			appearance91.TextHAlignAsString = "Left";
			comboBoxGridAccount.DisplayLayout.Override.HeaderAppearance = appearance91;
			comboBoxGridAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridAccount.DisplayLayout.Override.RowAppearance = appearance92;
			comboBoxGridAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance93;
			comboBoxGridAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridAccount.Editable = true;
			comboBoxGridAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGridAccount.FilterString = "";
			comboBoxGridAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGridAccount.FilterSysDocID = "";
			comboBoxGridAccount.HasAllAccount = false;
			comboBoxGridAccount.HasCustom = false;
			comboBoxGridAccount.IsDataLoaded = false;
			comboBoxGridAccount.Location = new System.Drawing.Point(608, 225);
			comboBoxGridAccount.MaxDropDownItems = 12;
			comboBoxGridAccount.Name = "comboBoxGridAccount";
			comboBoxGridAccount.ShowInactiveItems = false;
			comboBoxGridAccount.ShowQuickAdd = true;
			comboBoxGridAccount.Size = new System.Drawing.Size(132, 20);
			comboBoxGridAccount.TabIndex = 108;
			comboBoxGridAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridAccount.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance94;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance95.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance95;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance96;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance97.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance97.BackColor2 = System.Drawing.SystemColors.Control;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance97;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance98.BackColor = System.Drawing.SystemColors.Window;
			appearance98.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance98;
			appearance99.BackColor = System.Drawing.SystemColors.Highlight;
			appearance99.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance99;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance100;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			appearance101.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance101;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance102.BackColor = System.Drawing.SystemColors.Control;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance102;
			appearance103.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance103;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance104.BackColor = System.Drawing.SystemColors.Window;
			appearance104.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance104;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance105;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(662, 282);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(94, 20);
			comboBoxGridEmployee.TabIndex = 114;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxGridVendor.Assigned = false;
			comboBoxGridVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridVendor.CustomReportFieldName = "";
			comboBoxGridVendor.CustomReportKey = "";
			comboBoxGridVendor.CustomReportValueType = 1;
			comboBoxGridVendor.DescriptionTextBox = null;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridVendor.DisplayLayout.Appearance = appearance106;
			comboBoxGridVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance107.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.GroupByBox.Appearance = appearance107;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance108;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance109.BackColor2 = System.Drawing.SystemColors.Control;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance109;
			comboBoxGridVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveCellAppearance = appearance110;
			appearance111.BackColor = System.Drawing.SystemColors.Highlight;
			appearance111.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveRowAppearance = appearance111;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.CardAreaAppearance = appearance112;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			appearance113.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridVendor.DisplayLayout.Override.CellAppearance = appearance113;
			comboBoxGridVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridVendor.DisplayLayout.Override.CellPadding = 0;
			appearance114.BackColor = System.Drawing.SystemColors.Control;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.GroupByRowAppearance = appearance114;
			appearance115.TextHAlignAsString = "Left";
			comboBoxGridVendor.DisplayLayout.Override.HeaderAppearance = appearance115;
			comboBoxGridVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridVendor.DisplayLayout.Override.RowAppearance = appearance116;
			comboBoxGridVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance117;
			comboBoxGridVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridVendor.Editable = true;
			comboBoxGridVendor.FilterString = "";
			comboBoxGridVendor.FilterSysDocID = "";
			comboBoxGridVendor.HasAll = false;
			comboBoxGridVendor.HasCustom = false;
			comboBoxGridVendor.IsDataLoaded = false;
			comboBoxGridVendor.Location = new System.Drawing.Point(657, 308);
			comboBoxGridVendor.MaxDropDownItems = 12;
			comboBoxGridVendor.Name = "comboBoxGridVendor";
			comboBoxGridVendor.ShowConsignmentOnly = false;
			comboBoxGridVendor.ShowQuickAdd = true;
			comboBoxGridVendor.Size = new System.Drawing.Size(88, 20);
			comboBoxGridVendor.TabIndex = 113;
			comboBoxGridVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridVendor.Visible = false;
			comboBoxGridCustomer.Assigned = false;
			comboBoxGridCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCustomer.CustomReportFieldName = "";
			comboBoxGridCustomer.CustomReportKey = "";
			comboBoxGridCustomer.CustomReportValueType = 1;
			comboBoxGridCustomer.DescriptionTextBox = null;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCustomer.DisplayLayout.Appearance = appearance118;
			comboBoxGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance119.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance119;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance120;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance121.BackColor2 = System.Drawing.SystemColors.Control;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance121;
			comboBoxGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance122;
			appearance123.BackColor = System.Drawing.SystemColors.Highlight;
			appearance123.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance123;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance124;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			appearance125.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCustomer.DisplayLayout.Override.CellAppearance = appearance125;
			comboBoxGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance126.BackColor = System.Drawing.SystemColors.Control;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance126;
			appearance127.TextHAlignAsString = "Left";
			comboBoxGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance127;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCustomer.DisplayLayout.Override.RowAppearance = appearance128;
			comboBoxGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance129;
			comboBoxGridCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCustomer.Editable = true;
			comboBoxGridCustomer.FilterString = "";
			comboBoxGridCustomer.FilterSysDocID = "";
			comboBoxGridCustomer.HasAll = false;
			comboBoxGridCustomer.HasCustom = false;
			comboBoxGridCustomer.IsDataLoaded = false;
			comboBoxGridCustomer.Location = new System.Drawing.Point(662, 334);
			comboBoxGridCustomer.MaxDropDownItems = 12;
			comboBoxGridCustomer.Name = "comboBoxGridCustomer";
			comboBoxGridCustomer.ShowConsignmentOnly = false;
			comboBoxGridCustomer.ShowInactive = false;
			comboBoxGridCustomer.ShowLPOCustomersOnly = false;
			comboBoxGridCustomer.ShowPROCustomersOnly = false;
			comboBoxGridCustomer.ShowQuickAdd = true;
			comboBoxGridCustomer.Size = new System.Drawing.Size(84, 20);
			comboBoxGridCustomer.TabIndex = 112;
			comboBoxGridCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCustomer.Visible = false;
			comboBoxAccountGroup.Assigned = false;
			comboBoxAccountGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAccountGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAccountGroup.CustomReportFieldName = "";
			comboBoxAccountGroup.CustomReportKey = "";
			comboBoxAccountGroup.CustomReportValueType = 1;
			comboBoxAccountGroup.DescriptionTextBox = null;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAccountGroup.DisplayLayout.Appearance = appearance130;
			comboBoxAccountGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAccountGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAccountGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			comboBoxAccountGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAccountGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAccountGroup.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAccountGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAccountGroup.DisplayLayout.Override.CellAppearance = appearance137;
			comboBoxAccountGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAccountGroup.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAccountGroup.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			comboBoxAccountGroup.DisplayLayout.Override.HeaderAppearance = appearance139;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAccountGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			comboBoxAccountGroup.DisplayLayout.Override.RowAppearance = appearance140;
			comboBoxAccountGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAccountGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			comboBoxAccountGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAccountGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAccountGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAccountGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAccountGroup.Editable = true;
			comboBoxAccountGroup.FilterString = "";
			comboBoxAccountGroup.HasAllAccount = false;
			comboBoxAccountGroup.HasCustom = false;
			comboBoxAccountGroup.IsDataLoaded = false;
			comboBoxAccountGroup.Location = new System.Drawing.Point(748, 256);
			comboBoxAccountGroup.MaxDropDownItems = 12;
			comboBoxAccountGroup.Name = "comboBoxAccountGroup";
			comboBoxAccountGroup.ShowInactiveItems = false;
			comboBoxAccountGroup.ShowQuickAdd = true;
			comboBoxAccountGroup.Size = new System.Drawing.Size(100, 20);
			comboBoxAccountGroup.TabIndex = 124;
			comboBoxAccountGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAccountGroup.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(923, 498);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(payeeTypeComboBox1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(currencySelector);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(comboBoxGridanalysis);
			base.Controls.Add(comboBoxGridAccount);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxGridVendor);
			base.Controls.Add(comboBoxGridCustomer);
			base.Controls.Add(comboBoxAccountGroup);
			base.Controls.Add(comboBoxGridCostCenter);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "BudgetingForm";
			Text = "Budgeting";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)payeeTypeComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridanalysis).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAccountGroup).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
