using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Accounts
{
	public class BillDiscountForm : Form, IForm
	{
		private ArrayList selectedCheques = new ArrayList();

		private bool pdcDirectMaturity = true;

		private DiscountBillData currentData;

		private const string TABLENAME_CONST = "Bill_Discount";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool pdcByMaturity;

		private bool isLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

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

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxAccountName;

		private flatDatePicker dateTimePickerDate;

		private flatDatePicker datePickerCheckDate;

		private MMLabel mmLabel2;

		private DataEntryGrid dataGridItems;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private Label label2;

		private TextBox textBoxAmount;

		private Label labelVoided;

		private BankFacilityComboBox comboBoxFacility;

		private TextBox textBoxFacilityName;

		private Label label9;

		private TextBox textBoxDiscPercent;

		private Label label8;

		private Label label7;

		private Label label6;

		private TextBox textBoxAvailableLimit;

		private Label label5;

		private TextBox textBoxLimit;

		private Label label4;

		private TextBox textBoxBankChargeAmount;

		private TextBox textBoxBankChargePercent;

		private AmountTextBox textBoxBankInterestAmount;

		private AmountTextBox textBoxBankCommission;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonFilter;

		private XPButton buttonSelectDocument;

		private CurrencySelector currencySelector;

		private UltraFormattedLinkLabel labelCurrency;

		private CheckBox checkBoxDistributeDiscount;

		private flatDatePicker datePickerDueDate;

		private MMLabel mmLabel3;

		private NumericUpDown numericUpDownTenorDay;

		private Label label10;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1009;

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
					textBoxVoucherNumber.Enabled = true;
					comboBoxSysDoc.Enabled = true;
					buttonVoid.Enabled = false;
					datePickerCheckDate.Enabled = true;
					panelDetails.Enabled = true;
					buttonSave.Enabled = true;
					if (dataGridItems.DisplayLayout != null && dataGridItems.DisplayLayout.Bands.Count > 0 && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("D"))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["D"].CellActivation = Activation.AllowEdit;
					}
					if (dataGridItems.ContextMenuStrip != null)
					{
						dataGridItems.ContextMenuStrip.Enabled = true;
					}
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!isVoid)
					{
						buttonVoid.Enabled = true;
					}
					textBoxVoucherNumber.Enabled = false;
					comboBoxSysDoc.Enabled = false;
					dataGridItems.DisplayLayout.Bands[0].Override.AllowDelete = DefaultableBoolean.False;
					dataGridItems.ContextMenuStrip.Enabled = false;
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
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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
					buttonSave.Enabled = !value;
					labelVoided.Visible = value;
					if (value)
					{
						buttonVoid.Enabled = false;
					}
					else if (!isNewRecord)
					{
						buttonVoid.Enabled = true;
					}
				}
			}
		}

		public BillDiscountForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			pdcByMaturity = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCByMaturity, defaultValue: true);
			pdcDirectMaturity = CompanyPreferences.PDCDirectMaturity;
			comboBoxFacility.FilterFacilityType = BankFacilityTypes.BillDiscount;
		}

		private void bankAccountsComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isLoading)
			{
				textBoxAccountName.Text = comboBoxFacility.CurrentAccountID + " - " + comboBoxFacility.CurrentAccountName;
				textBoxAvailableLimit.Text = comboBoxFacility.AvailableLimit.ToString(Format.TotalAmountFormat);
				textBoxLimit.Text = comboBoxFacility.LimitAmount.ToString(Format.TotalAmountFormat);
			}
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridItems.BeforeCellListDropDown += dataGridItems_BeforeCellListDropDown;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.SummaryValueChanged += dataGridItems_SummaryValueChanged;
			comboBoxFacility.SelectedIndexChanged += bankAccountsComboBox1_SelectedIndexChanged;
		}

		private void dataGridItems_SummaryValueChanged(object sender, SummaryValueChangedEventArgs e)
		{
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "DiscountAmount")
			{
				if (e.Cell.Value.ToString() != "")
				{
					e.Cell.Row.Cells["D"].Value = true;
				}
				else
				{
					e.Cell.Row.Cells["D"].Value = false;
				}
				CalculateDiscountAmount();
			}
			else if (e.Cell.Column.Key == "BankCharge")
			{
				decimal result = default(decimal);
				decimal d = decimal.Parse(textBoxBankCommission.Text);
				decimal.TryParse(e.Cell.Value.ToString(), out result);
				textBoxBankCommission.Text = (d + result).ToString(Format.TotalAmountFormat);
			}
		}

		private void comboBoxChequebook_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridItems_BeforeCellListDropDown(object sender, CancelableCellEventArgs e)
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

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Column.Key == "D")
			{
				if (bool.Parse(e.Cell.Text.ToString()))
				{
					e.Cell.Row.Cells["DiscountAmount"].Value = e.Cell.Row.Cells["Amount"].Value;
				}
				else
				{
					e.Cell.Row.Cells["DiscountAmount"].Value = DBNull.Value;
				}
			}
			CalculateDiscountAmount();
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
		}

		private void comboBoxGridAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount" && dataGridItems.ActiveCell.Text != "")
			{
				dataGridItems.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridItems.ActiveCell.Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.Cells["Amount"].Value.ToString() == "")
			{
				activeRow.Cells["Amount"].Value = 0;
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Chequebook" || e.Cell.Column.Key == "Chq Number")
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.Rows.Count > 1)
				{
					string b = dataGridItems.ActiveRow.Cells["Chq Number"].Text.Trim();
					string b2 = dataGridItems.ActiveRow.Cells["Chequebook"].Text.Trim();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row != dataGridItems.ActiveRow && row.Cells["Chequebook"].Value.ToString().Trim() == b2 && row.Cells["Chq Number"].Value.ToString().Trim() == b)
						{
							ErrorHelper.WarningMessage(UIMessages.ChequeNumberExist);
							e.Cancel = true;
							break;
						}
					}
				}
			}
			else if (e.Cell.Column.Key == "BankCharge")
			{
				decimal result = default(decimal);
				decimal d = decimal.Parse(textBoxBankCommission.Text);
				decimal.TryParse(e.Cell.Value.ToString(), out result);
				textBoxBankCommission.Text = (d - result).ToString(Format.TotalAmountFormat);
			}
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
					currentData = new DiscountBillData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BillDiscountTable.Rows[0] : currentData.BillDiscountTable.NewRow();
				currentData.BillDiscountDetailTable.Rows.Clear();
				dataRow["Note"] = textBoxNote.Text;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["DueDate"] = datePickerDueDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["FacilityType"] = 4;
				dataRow["BankFacilityID"] = comboBoxFacility.SelectedID;
				dataRow["BankChargeAmount"] = textBoxBankCommission.Text;
				dataRow["BankChargeAccountID"] = comboBoxFacility.BankChargeAccountID;
				dataRow["BankInterestAccountID"] = comboBoxFacility.BankInterestAccountID;
				dataRow["BankCommission"] = textBoxBankInterestAmount.Text;
				dataRow["BankChargePercent"] = textBoxDiscPercent.Text;
				dataRow["BankAccountID"] = comboBoxFacility.CurrentAccountID;
				dataRow["LiabilityAccountID"] = comboBoxFacility.PayableAccountID;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID)
				{
					dataRow["CurrencyID"] = currencySelector.SelectedID;
					dataRow["CurrencyRate"] = currencySelector.Rate;
					dataRow["AmountFC"] = textBoxAmount.Text;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = 1;
					dataRow["Amount"] = textBoxAmount.Text;
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.BillDiscountTable.Rows.Add(dataRow);
				}
				currentData.BillDiscountDetailTable.Rows.Clear();
				selectedCheques.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (IsNewRecord && string.IsNullOrEmpty(row.Cells["D"].Value.ToString()))
					{
						row.Cells["D"].Value = true;
					}
					if (bool.Parse(row.Cells["D"].Value.ToString()))
					{
						DataRow dataRow2 = currentData.BillDiscountDetailTable.NewRow();
						dataRow2.BeginEdit();
						dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow2["InvoiceVoucherID"] = row.Cells["InvoiceVoucherID"].Value.ToString();
						dataRow2["InvoiceSysDocID"] = row.Cells["InvoiceSysDocID"].Value.ToString();
						dataRow2["Date"] = row.Cells["Date"].Value.ToString();
						dataRow2["DueDate"] = row.Cells["DueDate"].Value.ToString();
						dataRow2["Total"] = row.Cells["Amount"].Value.ToString();
						dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow2["PayeeID"] = row.Cells["PayeeID"].Value.ToString();
						if (!string.IsNullOrEmpty(row.Cells["DiscountAmount"].Value.ToString()))
						{
							dataRow2["DiscountAmount"] = row.Cells["DiscountAmount"].Value.ToString();
						}
						if (row.Cells["BankCharge"].Value != null && row.Cells["BankCharge"].Value.ToString() != "")
						{
							dataRow2["BankChargeAmount"] = row.Cells["BankCharge"].Value.ToString();
						}
						dataRow2.EndEdit();
						currentData.BillDiscountDetailTable.Rows.Add(dataRow2);
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void CalculateDiscountAmount()
		{
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!string.IsNullOrEmpty(row.Cells["DiscountAmount"].Value.ToString()))
				{
					num += decimal.Parse(row.Cells["DiscountAmount"].Value.ToString());
				}
			}
			if (string.IsNullOrEmpty(textBoxAmount.Text))
			{
				textBoxAmount.Text = num.ToString(Format.TotalAmountFormat);
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			CalculateBankInterestPercent();
		}

		private void SetupGrid()
		{
			try
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("D", typeof(bool));
				dataTable.Columns.Add("SysDocID", typeof(string));
				dataTable.Columns.Add("VoucherID", typeof(string));
				dataTable.Columns.Add("InvoiceSysDocID", typeof(string));
				dataTable.Columns.Add("InvoiceVoucherID", typeof(string));
				dataTable.Columns.Add("Date", typeof(DateTime));
				dataTable.Columns.Add("Currency", typeof(string));
				dataTable.Columns.Add("DueDate", typeof(DateTime));
				dataTable.Columns.Add("PayeeID", typeof(string));
				dataTable.Columns.Add("PayeeName", typeof(string));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("DiscountAmount", typeof(decimal));
				dataTable.Columns.Add("BankCharge", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["SysDocID"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["VoucherID"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["Currency"].Hidden = true;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.LoadLayout();
				dataGridItems.AllowAddNew = false;
				dataGridItems.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
				dataGridItems.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
				dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
				dataGridItems.DisplayLayout.TabNavigation = TabNavigation.NextControl;
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					column.CellActivation = Activation.NoEdit;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].CellClickAction = CellClickAction.Edit;
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].Format = "#,###.00";
				dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceSysDocID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["InvoiceVoucherID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Date"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["DueDate"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["PayeeID"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["PayeeName"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].CellClickAction = CellClickAction.Edit;
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].Format = "#,###.00";
				if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("D"))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].CellClickAction = CellClickAction.Edit;
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.Always;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = "#,###.00";
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["D"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["D"].Width = 24;
				dataGridItems.DisplayLayout.Bands[0].Columns["D"].MaxWidth = 24;
				dataGridItems.DisplayLayout.Bands[0].Columns["D"].MinWidth = 24;
				dataGridItems.DisplayLayout.Bands[0].Columns["D"].LockedWidth = true;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.ShowInsertMenu = false;
				dataGridItems.DisplayLayout.Bands[0].Override.AllowDelete = DefaultableBoolean.False;
				dataGridItems.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.SortSingle;
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["PayeeName"].Header.Caption = "Payee Name";
				dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"].Header.Caption = "Discount Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"].Header.Caption = "Bank Charge";
				dataGridItems.DisplayLayout.Bands[0].Columns["DueDate"].Header.Caption = "Due Date";
				if (dataGridItems.DisplayLayout.Bands[0].Summaries.Count == 0)
				{
					dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
					dataGridItems.DisplayLayout.Bands[0].Summaries.Add("DiscountAmount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["DiscountAmount"], SummaryPosition.UseSummaryPositionColumn);
					dataGridItems.DisplayLayout.Bands[0].Summaries.Add("BankCharge", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["BankCharge"], SummaryPosition.UseSummaryPositionColumn);
					dataGridItems.DisplayLayout.Override.SummaryFooterAppearance.BackColor = UITheme.ThemeColors.GridFooter;
					dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
					dataGridItems.DisplayLayout.Bands[0].Summaries["DiscountAmount"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
					dataGridItems.DisplayLayout.Bands[0].Summaries["BankCharge"].Appearance.BackColor = UITheme.ThemeColors.GridFooter;
				}
				dataGridItems.DisplayLayout.Bands[0].Override.FilterUIType = FilterUIType.FilterRow;
				dataGridItems.DisplayLayout.Bands[0].Override.AllowRowFiltering = DefaultableBoolean.False;
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

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed)
				{
					isLoading = true;
					if (!(voucherID.Trim() == "") && CanClose())
					{
						currentData = Factory.DiscountBillSystem.GetDiscountBillByID(SystemDocID, voucherID);
						if (currentData != null)
						{
							FillData();
							IsNewRecord = false;
							CalculateDiscountAmount();
							formManager.ResetDirty();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ClearForm();
			}
			finally
			{
				isLoading = false;
			}
		}

		private void FillData()
		{
			try
			{
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Bill_Discount"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxFacility.SelectedID = dataRow["BankFacilityID"].ToString();
					textBoxAccountName.Text = dataRow["CurrentAccountID"].ToString() + " - " + dataRow["AccountName"].ToString();
					textBoxLimit.Text = decimal.Parse(dataRow["LimitAmount"].ToString()).ToString(Format.TotalAmountFormat);
					textBoxAvailableLimit.Text = comboBoxFacility.AvailableLimit.ToString(Format.TotalAmountFormat);
					textBoxBankCommission.Text = dataRow["BankChargeAmount"].ToString();
					if (dataRow["BankCommission"] != DBNull.Value)
					{
						textBoxBankInterestAmount.Text = decimal.Parse(dataRow["BankCommission"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxBankInterestAmount.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["BankChargePercent"] != DBNull.Value)
					{
						textBoxDiscPercent.Text = decimal.Parse(dataRow["BankChargePercent"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxDiscPercent.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (!string.IsNullOrEmpty(dataRow["Amount"].ToString()))
					{
						textBoxAmount.Text = dataRow["Amount"].ToString();
					}
					else if (!string.IsNullOrEmpty(dataRow["AmountFC"].ToString()))
					{
						textBoxAmount.Text = dataRow["AmountFC"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["DueDate"].ToString()))
					{
						datePickerDueDate.Value = DateTime.Parse(dataRow["DueDate"].ToString());
					}
					TimeSpan timeSpan = dateTimePickerDate.Value - datePickerDueDate.Value;
					numericUpDownTenorDay.Value = Math.Abs(timeSpan.Days);
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					new DataTable().Rows.Clear();
					if (currentData.Tables.Contains("Bill_Discount_Detail") && currentData.Tables["Bill_Discount_Detail"].Rows.Count != 0)
					{
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						dataTable.Rows.Clear();
						foreach (DataRow row in currentData.Tables["Bill_Discount_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["D"] = true;
							dataRow3["SysDocID"] = row["SysDocID"];
							dataRow3["VoucherID"] = row["SysDocID"];
							dataRow3["Date"] = row["Date"];
							dataRow3["DueDate"] = row["DueDate"];
							dataRow3["InvoiceVoucherID"] = row["InvoiceVoucherID"];
							dataRow3["InvoiceSysDocID"] = row["InvoiceSysDocID"];
							dataRow3["PayeeID"] = row["PayeeID"];
							dataRow3["PayeeName"] = row["Payee Name"];
							dataRow3["Amount"] = row["Amount"];
							dataRow3["DiscountAmount"] = row["DiscountAmount"];
							dataRow3["BankCharge"] = row["BankChargeAmount"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.DisplayLayout.Bands[0].Columns["D"].Width = 24;
						dataGridItems.DisplayLayout.Bands[0].Columns["D"].LockedWidth = true;
						dataGridItems.DisplayLayout.Bands[0].Columns["D"].MaxWidth = 24;
						dataGridItems.DisplayLayout.Bands[0].Columns["D"].MinWidth = 24;
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
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			int[] array = new int[selectedCheques.Count];
			for (int i = 0; i < array.Length; i = checked(i + 1))
			{
				array[i] = int.Parse(selectedCheques[i].ToString());
			}
			try
			{
				bool flag = (!isNewRecord) ? Factory.DiscountBillSystem.CreateDiscountBill(currentData, isUpdate: true) : Factory.DiscountBillSystem.CreateDiscountBill(currentData, isUpdate: false);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Bill_Discount", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "" || comboBoxFacility.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Bill_Discount", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxFacility.Clear();
				datePickerCheckDate.Value = DateTime.Today;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				textBoxBankCommission.Clear();
				textBoxBankInterestAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxDiscPercent.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmount.Clear();
				textBoxAccountName.Clear();
				textBoxAvailableLimit.Clear();
				textBoxLimit.Clear();
				checkBoxDistributeDiscount.Checked = false;
				datePickerDueDate.Value = dateTimePickerDate.Value.AddDays(int.Parse(numericUpDownTenorDay.Value.ToString()));
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
				(dataGridItems.DataSource as DataTable).Rows.Clear();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void TransactionLeaveGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void TransactionLeaveGroupDetailsForm_Validated(object sender, EventArgs e)
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
				return Factory.DiscountBillSystem.DeleteDiscountBill(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Bill_Discount", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Bill_Discount", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Bill_Discount", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Bill_Discount", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Bill_Discount", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
				return;
			}
			Global.GlobalSettings.SaveFormProperties(this);
			dataGridItems.SaveLayout();
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

		private void TransactionLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				isLoading = true;
				dataGridItems.SetupUI();
				SetupGrid();
				dataGridItems.ApplyFormat();
				SetSecurity();
				if (!base.IsDisposed)
				{
					dateTimePickerDate.Value = DateTime.Now;
					IsNewRecord = true;
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.BillDiscount);
					Global.GlobalSettings.LoadFormProperties(this);
					dataGridItems.DisplayLayout.Bands[0].Columns["Currency"].Width = 57;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = 90;
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].Width = 25;
					dataGridItems.DisplayLayout.Bands[0].Columns["D"].LockedWidth = true;
					isLoading = false;
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
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
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
				return Factory.DiscountBillSystem.VoidDiscountBill(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.BillDiscount);
		}

		private void datePickerCheckDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBankFacility(comboBoxFacility.SelectedID);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Print(isPrint: false, showPrintDialog: false, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData()))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet discountBillToPrint = Factory.DiscountBillSystem.GetDiscountBillToPrint(selectedID, text);
					if (discountBillToPrint == null || discountBillToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(discountBillToPrint, selectedID, "Bill Discount", SysDocTypes.BillDiscount, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Bill_Discount", "VoucherID");
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		public void EditDocument(string sysDocID, string voucherID)
		{
			if (!comboBoxSysDoc.Enabled && sysDocID != comboBoxSysDoc.SelectedID)
			{
				ErrorHelper.ErrorMessage("Cannot edit this document because you do not have access to this document.");
				return;
			}
			comboBoxSysDoc.SelectedID = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.BillDiscountListFormObj);
		}

		private void textBoxBankInterestAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateBankInterestPercent();
		}

		private void CalculateBankInterestPercent()
		{
			decimal num = default(decimal);
			decimal num2 = default(decimal);
			if (textBoxAmount.Text != "" && textBoxBankInterestAmount.Text != "")
			{
				num = decimal.Parse(textBoxAmount.Text);
				num2 = decimal.Parse(textBoxBankInterestAmount.Text);
				if (num > 0m)
				{
					_ = num2 / num * 100m;
				}
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonFilter_Click(object sender, EventArgs e)
		{
			if (toolStripButtonFilter.Checked)
			{
				dataGridItems.DisplayLayout.Bands[0].Override.AllowRowFiltering = DefaultableBoolean.True;
				return;
			}
			dataGridItems.DisplayLayout.Bands[0].Override.AllowRowFiltering = DefaultableBoolean.False;
			dataGridItems.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			SelectDocumentDialog obj = sender as SelectDocumentDialog;
			DataSet dataSet = new DataSet();
			dataSet = (obj.DataSource = Factory.SalesInvoiceSystem.GetInvoicesForBillDiscountOnLocation(isExport: false, e.From, e.To));
		}

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
			selectDocumentDialog.AllowDateFilter = true;
			selectDocumentDialog.Text = "Select Sales Invoice";
			DialogResult num = selectDocumentDialog.ShowDialog(this);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (num == DialogResult.OK)
			{
				dataTable.Rows.Clear();
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["InvoiceSysDocID"] = selectedRow.Cells["Doc ID"].Value.ToString();
					dataRow["InvoiceVoucherID"] = selectedRow.Cells["Number"].Value.ToString();
					dataRow["Date"] = selectedRow.Cells["Date"].Value.ToString();
					dataRow["DueDate"] = selectedRow.Cells["DueDate"].Value.ToString();
					dataRow["PayeeName"] = selectedRow.Cells["Customer"].Value.ToString();
					dataRow["PayeeID"] = selectedRow.Cells["CustomerID"].Value.ToString();
					dataRow["Amount"] = selectedRow.Cells["Total"].Value.ToString();
					dataTable.Rows.Add(dataRow);
				}
			}
		}

		private void checkBoxDistributeDiscount_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxDistributeDiscount.Checked)
			{
				TextBox textBox = textBoxDiscPercent;
				bool visible = label7.Visible = true;
				textBox.Visible = visible;
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal result = default(decimal);
				decimal.TryParse(textBoxAmount.Text, out result);
				decimal d = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!string.IsNullOrEmpty(row.Cells["Amount"].Value.ToString()))
					{
						num2 = decimal.Parse(row.Cells["Amount"].Value.ToString());
					}
					num += num2;
				}
				if (num != 0m)
				{
					d = result / num * 100m;
				}
				textBoxDiscPercent.Text = d.ToString(Format.TotalAmountFormat);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					num2 = decimal.Parse(row2.Cells["Amount"].Value.ToString());
					row2.Cells["DiscountAmount"].Value = (num2 * d / 100m).ToString(Format.GridAmountFormat);
				}
			}
			else
			{
				TextBox textBox2 = textBoxDiscPercent;
				bool visible = label7.Visible = false;
				textBox2.Visible = visible;
				textBoxDiscPercent.Clear();
			}
		}

		private void textBoxBankInterestPercent_TextChanged(object sender, EventArgs e)
		{
		}

		private void textBoxBankInterestPercent_Validated(object sender, EventArgs e)
		{
		}

		private void comboBoxFacility_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxFacility.SelectedID != "")
			{
				numericUpDownTenorDay.Value = comboBoxFacility.TenorDays;
			}
		}

		private void numericUpDownTenorDay_ValueChanged(object sender, EventArgs e)
		{
			datePickerDueDate.Value = dateTimePickerDate.Value.AddDays(int.Parse(numericUpDownTenorDay.Value.ToString()));
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.BillDiscountForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonFilter = new System.Windows.Forms.ToolStripButton();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			datePickerDueDate = new Micromind.UISupport.flatDatePicker();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			checkBoxDistributeDiscount = new System.Windows.Forms.CheckBox();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxBankCommission = new Micromind.UISupport.AmountTextBox();
			label9 = new System.Windows.Forms.Label();
			textBoxDiscPercent = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			textBoxAvailableLimit = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			textBoxLimit = new System.Windows.Forms.TextBox();
			comboBoxFacility = new Micromind.DataControls.BankFacilityComboBox();
			textBoxFacilityName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxAmount = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			textBoxAccountName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			label4 = new System.Windows.Forms.Label();
			labelVoided = new System.Windows.Forms.Label();
			textBoxBankChargeAmount = new System.Windows.Forms.TextBox();
			textBoxBankChargePercent = new System.Windows.Forms.TextBox();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			textBoxBankInterestAmount = new Micromind.UISupport.AmountTextBox();
			datePickerCheckDate = new Micromind.UISupport.flatDatePicker();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			numericUpDownTenorDay = new System.Windows.Forms.NumericUpDown();
			label10 = new System.Windows.Forms.Label();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTenorDay).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[15]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonFilter,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(663, 31);
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Click += new System.EventHandler(toolStripButtonPrint_Click);
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonPreview.Image");
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonFilter.CheckOnClick = true;
			toolStripButtonFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFilter.Image = Micromind.ClientUI.Properties.Resources.filter;
			toolStripButtonFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFilter.Name = "toolStripButtonFilter";
			toolStripButtonFilter.Size = new System.Drawing.Size(28, 28);
			toolStripButtonFilter.Text = "Filter";
			toolStripButtonFilter.Click += new System.EventHandler(toolStripButtonFilter_Click);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 433);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(663, 40);
			panelButtons.TabIndex = 3;
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
			linePanelDown.Size = new System.Drawing.Size(663, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(553, 8);
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(356, 2);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(123, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(248, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(321, 24);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(104, 20);
			textBoxRef1.TabIndex = 3;
			textBoxNote.Location = new System.Drawing.Point(107, 140);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(540, 20);
			textBoxNote.TabIndex = 15;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(14, 146);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(251, 5);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 0;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(numericUpDownTenorDay);
			panelDetails.Controls.Add(label10);
			panelDetails.Controls.Add(datePickerDueDate);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(checkBoxDistributeDiscount);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(textBoxBankCommission);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(textBoxDiscPercent);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(textBoxAvailableLimit);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label7);
			panelDetails.Controls.Add(textBoxLimit);
			panelDetails.Controls.Add(comboBoxFacility);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(textBoxFacilityName);
			panelDetails.Controls.Add(textBoxAccountName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(0, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 169);
			panelDetails.TabIndex = 0;
			datePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			datePickerDueDate.Location = new System.Drawing.Point(429, 117);
			datePickerDueDate.Name = "datePickerDueDate";
			datePickerDueDate.Size = new System.Drawing.Size(127, 20);
			datePickerDueDate.TabIndex = 14;
			datePickerDueDate.Value = new System.DateTime(2015, 5, 13, 0, 0, 0, 0);
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(358, 120);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(65, 13);
			mmLabel3.TabIndex = 126;
			mmLabel3.Text = "Due Date:";
			checkBoxDistributeDiscount.AutoSize = true;
			checkBoxDistributeDiscount.Location = new System.Drawing.Point(249, 96);
			checkBoxDistributeDiscount.Name = "checkBoxDistributeDiscount";
			checkBoxDistributeDiscount.Size = new System.Drawing.Size(115, 17);
			checkBoxDistributeDiscount.TabIndex = 11;
			checkBoxDistributeDiscount.Text = "Distribute Discount";
			checkBoxDistributeDiscount.UseVisualStyleBackColor = true;
			checkBoxDistributeDiscount.CheckedChanged += new System.EventHandler(checkBoxDistributeDiscount_CheckedChanged);
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(489, 26);
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
			currencySelector.Size = new System.Drawing.Size(157, 20);
			currencySelector.TabIndex = 4;
			appearance3.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance3;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(430, 27);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 14);
			labelCurrency.TabIndex = 131;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance4;
			textBoxBankCommission.AllowDecimal = true;
			textBoxBankCommission.CustomReportFieldName = "";
			textBoxBankCommission.CustomReportKey = "";
			textBoxBankCommission.CustomReportValueType = 1;
			textBoxBankCommission.IsComboTextBox = false;
			textBoxBankCommission.IsModified = false;
			textBoxBankCommission.Location = new System.Drawing.Point(107, 117);
			textBoxBankCommission.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBankCommission.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBankCommission.Name = "textBoxBankCommission";
			textBoxBankCommission.NullText = "0";
			textBoxBankCommission.Size = new System.Drawing.Size(103, 20);
			textBoxBankCommission.TabIndex = 13;
			textBoxBankCommission.Text = "0.00";
			textBoxBankCommission.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBankCommission.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(14, 120);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(93, 13);
			label9.TabIndex = 130;
			label9.Text = "Bank Commission:";
			textBoxDiscPercent.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscPercent.Location = new System.Drawing.Point(462, 96);
			textBoxDiscPercent.Name = "textBoxDiscPercent";
			textBoxDiscPercent.ReadOnly = true;
			textBoxDiscPercent.Size = new System.Drawing.Size(55, 20);
			textBoxDiscPercent.TabIndex = 10;
			textBoxDiscPercent.TabStop = false;
			textBoxDiscPercent.Text = "0.00";
			textBoxDiscPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxDiscPercent.Visible = false;
			textBoxDiscPercent.TextChanged += new System.EventHandler(textBoxBankInterestPercent_TextChanged);
			textBoxDiscPercent.Validated += new System.EventHandler(textBoxBankInterestPercent_Validated);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(463, 73);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(77, 13);
			label6.TabIndex = 127;
			label6.Text = "Available Limit:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(519, 98);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(15, 13);
			label8.TabIndex = 128;
			label8.Text = "%";
			label8.Visible = false;
			textBoxAvailableLimit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAvailableLimit.Location = new System.Drawing.Point(546, 69);
			textBoxAvailableLimit.Name = "textBoxAvailableLimit";
			textBoxAvailableLimit.ReadOnly = true;
			textBoxAvailableLimit.Size = new System.Drawing.Size(100, 20);
			textBoxAvailableLimit.TabIndex = 9;
			textBoxAvailableLimit.TabStop = false;
			textBoxAvailableLimit.Text = "0.00";
			textBoxAvailableLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(319, 73);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(31, 13);
			label5.TabIndex = 127;
			label5.Text = "Limit:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(366, 98);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(92, 13);
			label7.TabIndex = 12;
			label7.Text = "Discount Percent:";
			label7.Visible = false;
			textBoxLimit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLimit.Location = new System.Drawing.Point(354, 69);
			textBoxLimit.Name = "textBoxLimit";
			textBoxLimit.ReadOnly = true;
			textBoxLimit.Size = new System.Drawing.Size(107, 20);
			textBoxLimit.TabIndex = 8;
			textBoxLimit.TabStop = false;
			textBoxLimit.Text = "0.00";
			textBoxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			comboBoxFacility.Assigned = false;
			comboBoxFacility.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFacility.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFacility.CustomReportFieldName = "";
			comboBoxFacility.CustomReportKey = "";
			comboBoxFacility.CustomReportValueType = 1;
			comboBoxFacility.DescriptionTextBox = textBoxFacilityName;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxFacility.DisplayLayout.Appearance = appearance5;
			comboBoxFacility.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxFacility.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFacility.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxFacility.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxFacility.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxFacility.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxFacility.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxFacility.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxFacility.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxFacility.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxFacility.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxFacility.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxFacility.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxFacility.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxFacility.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxFacility.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxFacility.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxFacility.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxFacility.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxFacility.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxFacility.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxFacility.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxFacility.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxFacility.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxFacility.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxFacility.Editable = true;
			comboBoxFacility.FilterFacilityType = Micromind.Common.Data.BankFacilityTypes.BillDiscount;
			comboBoxFacility.FilterString = "";
			comboBoxFacility.HasAllAccount = false;
			comboBoxFacility.HasCustom = false;
			comboBoxFacility.IsDataLoaded = false;
			comboBoxFacility.Location = new System.Drawing.Point(107, 48);
			comboBoxFacility.MaxDropDownItems = 12;
			comboBoxFacility.Name = "comboBoxFacility";
			comboBoxFacility.ShowInactiveItems = false;
			comboBoxFacility.ShowQuickAdd = true;
			comboBoxFacility.Size = new System.Drawing.Size(208, 20);
			comboBoxFacility.TabIndex = 5;
			comboBoxFacility.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxFacility.SelectedIndexChanged += new System.EventHandler(comboBoxFacility_SelectedIndexChanged);
			textBoxFacilityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFacilityName.Location = new System.Drawing.Point(318, 47);
			textBoxFacilityName.Name = "textBoxFacilityName";
			textBoxFacilityName.ReadOnly = true;
			textBoxFacilityName.Size = new System.Drawing.Size(328, 20);
			textBoxFacilityName.TabIndex = 6;
			textBoxFacilityName.TabStop = false;
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance17;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(14, 48);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel1.TabIndex = 122;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bank Facility:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance18;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 97);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(73, 13);
			label2.TabIndex = 121;
			label2.Text = "Total Amount:";
			textBoxAmount.BackColor = System.Drawing.SystemColors.Window;
			textBoxAmount.Location = new System.Drawing.Point(107, 94);
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.Size = new System.Drawing.Size(136, 20);
			textBoxAmount.TabIndex = 10;
			textBoxAmount.TabStop = false;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(107, 25);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(140, 20);
			dateTimePickerDate.TabIndex = 2;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 45, 508);
			textBoxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAccountName.Location = new System.Drawing.Point(107, 71);
			textBoxAccountName.Name = "textBoxAccountName";
			textBoxAccountName.ReadOnly = true;
			textBoxAccountName.Size = new System.Drawing.Size(209, 20);
			textBoxAccountName.TabIndex = 7;
			textBoxAccountName.TabStop = false;
			appearance19.FontData.BoldAsString = "True";
			appearance19.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance19;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(14, 4);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 116;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance20.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance20;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance21;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance22.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance22.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance22.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance22;
			appearance23.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance23;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance24.BackColor2 = System.Drawing.SystemColors.Control;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance24.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance24;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance25;
			appearance26.BackColor = System.Drawing.SystemColors.Highlight;
			appearance26.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance26;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance27;
			appearance28.BorderColor = System.Drawing.Color.Silver;
			appearance28.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance28;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance29.BackColor = System.Drawing.SystemColors.Control;
			appearance29.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance29.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance29;
			appearance30.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance30;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance32;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(107, 2);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(140, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(14, 26);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(92, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Discount Date:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(14, 72);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(50, 13);
			label4.TabIndex = 20;
			label4.Text = "Account:";
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(506, 283);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(91, 27);
			labelVoided.TabIndex = 124;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			textBoxBankChargeAmount.BackColor = System.Drawing.Color.White;
			textBoxBankChargeAmount.Location = new System.Drawing.Point(188, 92);
			textBoxBankChargeAmount.Name = "textBoxBankChargeAmount";
			textBoxBankChargeAmount.Size = new System.Drawing.Size(100, 20);
			textBoxBankChargeAmount.TabIndex = 129;
			textBoxBankChargeAmount.TabStop = false;
			textBoxBankChargeAmount.Text = "0.00";
			textBoxBankChargeAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBankChargePercent.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBankChargePercent.Location = new System.Drawing.Point(106, 92);
			textBoxBankChargePercent.Name = "textBoxBankChargePercent";
			textBoxBankChargePercent.ReadOnly = true;
			textBoxBankChargePercent.Size = new System.Drawing.Size(55, 20);
			textBoxBankChargePercent.TabIndex = 129;
			textBoxBankChargePercent.TabStop = false;
			textBoxBankChargePercent.Text = "0.00";
			textBoxBankChargePercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(507, 208);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(139, 22);
			buttonSelectDocument.TabIndex = 125;
			buttonSelectDocument.Text = "Load Invoices";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			textBoxBankInterestAmount.AllowDecimal = true;
			textBoxBankInterestAmount.CustomReportFieldName = "";
			textBoxBankInterestAmount.CustomReportKey = "";
			textBoxBankInterestAmount.CustomReportValueType = 1;
			textBoxBankInterestAmount.IsComboTextBox = false;
			textBoxBankInterestAmount.IsModified = false;
			textBoxBankInterestAmount.Location = new System.Drawing.Point(376, 215);
			textBoxBankInterestAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxBankInterestAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxBankInterestAmount.Name = "textBoxBankInterestAmount";
			textBoxBankInterestAmount.NullText = "0";
			textBoxBankInterestAmount.Size = new System.Drawing.Size(103, 20);
			textBoxBankInterestAmount.TabIndex = 11;
			textBoxBankInterestAmount.Text = "0.00";
			textBoxBankInterestAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBankInterestAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxBankInterestAmount.Visible = false;
			textBoxBankInterestAmount.TextChanged += new System.EventHandler(textBoxBankInterestAmount_TextChanged);
			datePickerCheckDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			datePickerCheckDate.Location = new System.Drawing.Point(93, 215);
			datePickerCheckDate.Name = "datePickerCheckDate";
			datePickerCheckDate.Size = new System.Drawing.Size(100, 20);
			datePickerCheckDate.TabIndex = 1;
			datePickerCheckDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 45, 548);
			datePickerCheckDate.Visible = false;
			datePickerCheckDate.ValueChanged += new System.EventHandler(datePickerCheckDate_ValueChanged);
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(14, 219);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(73, 13);
			mmLabel2.TabIndex = 123;
			mmLabel2.Text = "Cheque Date:";
			mmLabel2.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance33;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance34;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance36.BackColor2 = System.Drawing.SystemColors.Control;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance37;
			appearance38.BackColor = System.Drawing.SystemColors.Highlight;
			appearance38.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance38;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance39;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			appearance40.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance40;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance41.BackColor = System.Drawing.SystemColors.Control;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance41;
			appearance42.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance42;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance43;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance44;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 236);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(638, 191);
			dataGridItems.TabIndex = 2;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
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
			numericUpDownTenorDay.Location = new System.Drawing.Point(284, 117);
			numericUpDownTenorDay.Maximum = new decimal(new int[4]
			{
				500,
				0,
				0,
				0
			});
			numericUpDownTenorDay.Name = "numericUpDownTenorDay";
			numericUpDownTenorDay.Size = new System.Drawing.Size(61, 20);
			numericUpDownTenorDay.TabIndex = 136;
			numericUpDownTenorDay.Value = new decimal(new int[4]
			{
				30,
				0,
				0,
				0
			});
			numericUpDownTenorDay.ValueChanged += new System.EventHandler(numericUpDownTenorDay_ValueChanged);
			label10.AutoSize = true;
			label10.BackColor = System.Drawing.Color.Transparent;
			label10.Location = new System.Drawing.Point(217, 120);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(65, 13);
			label10.TabIndex = 137;
			label10.Text = "Tenor Days:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(663, 473);
			base.Controls.Add(buttonSelectDocument);
			base.Controls.Add(labelVoided);
			base.Controls.Add(textBoxBankInterestAmount);
			base.Controls.Add(datePickerCheckDate);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(655, 425);
			base.Name = "BillDiscountForm";
			Text = "Bill Discount Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownTenorDay).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
