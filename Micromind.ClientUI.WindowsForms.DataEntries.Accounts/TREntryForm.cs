using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Customers;
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
	public class TREntryForm : Form, IForm
	{
		private BankFacilityTransactionData currentData;

		private const string TABLENAME_CONST = "Bank_Facility_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool showAllocationForm = CompanyPreferences.ShowAllocationForm;

		private bool isCostCenterMandatory = CompanyPreferences.IsCostCenterMandatory;

		private string requestSysDocID = "";

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

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

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private CurrencySelector currencySelector;

		private UltraFormattedLinkLabel labelCurrency;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private CostCenterComboBox comboBoxCostCenter;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxPayeeName;

		private flatDatePicker dateTimePickerDate;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private AmountTextBox textBoxAmount;

		private TextBox textBoxDescription;

		private Label label4;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private Label label2;

		private TextBox textBoxRegBalance;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private TextBox textBoxFacilityName;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator4;

		private BankFacilityComboBox comboBoxFacility;

		private vendorsFlatComboBox comboBoxVendor;

		private flatDatePicker datePickerDueDate;

		private MMLabel mmLabel3;

		private Label labelAmountBase;

		private TextBox textBoxAmountBase;

		private NumericUpDown numericUpDownTenorDay;

		private Label label5;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private UltraTabPageControl tabPageExpense;

		private NumberTextBox textBoxTotalExpense;

		private Label label7;

		private DataEntryGrid dataGridExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private CurrencyComboBox comboBoxGridCurrency;

		private Panel panelDetails;

		private FormManager formManager;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonDistribution;

		private Line line1;

		private Label label8;

		private TextBox textBoxTotalTRAmount;

		private Label label6;

		private TextBox textBoxFinancedFees;

		private XPButton buttonSelectRequest;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxRequest;

		private ToolStripButton toolStripButtonInformation;

		private AmountTextBox textBoxTaxAmount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private XPButton buttonSelectInvoice;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem createFromTRApplicationToolStripMenuItem;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1006;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.Enabled = false;
					comboBoxSysDoc.Enabled = false;
					buttonSelectInvoice.Enabled = false;
				}
				buttonSelectRequest.Enabled = value;
				toolStripButtonDistribution.Enabled = !value;
				buttonSelectRequest.Enabled = CompanyPreferences.DirectTREntry;
				comboBoxSysDoc.Enabled = value;
				toolStripButtonAttach.Enabled = !value;
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
					buttonSave.Enabled = !value;
					panelDetails.Enabled = !value;
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

		public TREntryForm()
		{
			InitializeComponent();
			AddEvents();
			labelAmountBase.Text = "Amount " + Global.BaseCurrencyID + ":";
			comboBoxGridExpenseCode.ExpenseCodeType = ExpenseCodeTypes.BankFee;
			createFromTRApplicationToolStripMenuItem.Visible = !CompanyPreferences.DirectTREntry;
			if (!CompanyPreferences.DirectTREntry)
			{
				comboBoxVendor.ReadOnly = true;
			}
		}

		private void AddEvents()
		{
			base.Load += TransactionLeavesForm_Load;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCostCenter.SelectedIndexChanged += comboBoxCostCenter_SelectedIndexChanged;
			comboBoxVendor.SelectedIndexChanged += comboBoxVendor_SelectedItemChanged;
			comboBoxFacility.SelectedIndexChanged += comboBoxFacility_SelectedIndexChanged;
			base.KeyDown += Form_KeyDown;
			comboBoxFacility.DescriptionTextBox = textBoxFacilityName;
			currencySelector.CurrencyRateChanged += currencySelector_CurrencyRateChanged;
			currencySelector.SelectedIndexChanged += currencySelector_SelectedIndexChanged;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
		}

		private void currencySelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void currencySelector_CurrencyRateChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
		}

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridExpense.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "FeeCode")
					{
						dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
						dataGridExpense.ActiveRow.Cells["ExpenseAccountID"].Value = comboBoxGridExpenseCode.AccountID;
						if (dataGridExpense.ActiveRow.Cells["Currency"].Value.ToString() == "")
						{
							dataGridExpense.ActiveRow.Cells["Currency"].Value = Global.BaseCurrencyID;
						}
						ItemTaxOptions taxOption = comboBoxGridExpenseCode.TaxOption;
						dataGridExpense.ActiveRow.Cells["TaxOption"].Value = taxOption;
						switch (taxOption)
						{
						case ItemTaxOptions.BasedOnCustomer:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
							break;
						case ItemTaxOptions.Taxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridExpenseCode.TaxGroupID;
							break;
						case ItemTaxOptions.NonTaxable:
							dataGridExpense.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
							break;
						}
					}
					if (e.Cell.Column.Key == "TaxGroupID")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.NonTaxable;
						if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(PayeeTaxOptions.Taxable, comboBoxGridExpenseCode.TaxGroupID, itemTaxOption, comboBoxGridExpenseCode.TaxGroupID);
						e.Cell.Row.Cells["Tax"].Tag = tag;
					}
					if (e.Cell.Column.Key == "Amount")
					{
						decimal amount = decimal.Parse(e.Cell.Value.ToString());
						decimal subtotal = decimal.Parse(textBoxTotalExpense.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", amount, subtotal, default(decimal));
						SetRowLCAmount(dataGridExpense.ActiveRow);
						CalculateTotalExpense();
					}
					if (e.Cell.Column.Key == "Currency")
					{
						dataGridExpense.ActiveRow.Cells["Rate"].Value = comboBoxGridCurrency.SelectedRate.ToString();
						dataGridExpense.ActiveRow.Cells["RateType"].Value = comboBoxGridCurrency.SelectedRateType;
					}
					if (e.Cell.Column.Key == "Amount" || e.Cell.Column.Key == "Rate" || e.Cell.Column.Key == "Currency" || e.Cell.Column.Key == "IsWithTR")
					{
						SetRowLCAmount(dataGridExpense.ActiveRow);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetRowLCAmount(UltraGridRow row)
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
			decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result2);
			result += result2;
			decimal num = 1m;
			if (row.Cells["Rate"].Value.ToString() != "")
			{
				num = decimal.Parse(row.Cells["Rate"].Value.ToString());
			}
			string a = row.Cells["RateType"].Value.ToString();
			if (row.Cells["Currency"].Value.ToString() == "" || row.Cells["Currency"].Value.ToString() == Global.BaseCurrencyID)
			{
				num = 1m;
			}
			if (row.Cells["Currency"].Value.ToString() != "" && row.Cells["Currency"].Value.ToString() != Global.BaseCurrencyID && num > 0m)
			{
				if (a == "D")
				{
					result /= num;
				}
				else if (a == "M")
				{
					result *= num;
				}
			}
			row.Cells["AmountLC"].Value = result.ToString(Format.TotalAmountFormat);
			CalculateTotalExpense();
		}

		private void comboBoxFacility_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxFacility.SelectedID == "")
			{
				textBoxRegBalance.Clear();
				return;
			}
			decimal availableLimit = comboBoxFacility.AvailableLimit;
			textBoxRegBalance.Text = availableLimit.ToString(Format.TotalAmountFormat, CultureInfo.CurrentCulture);
			numericUpDownTenorDay.Value = comboBoxFacility.TenorDays;
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void comboBoxVendor_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxPayeeName.Text = comboBoxVendor.SelectedName;
			if (comboBoxVendor.AllowOAP)
			{
				textBoxAmount.ReadOnly = false;
			}
			else
			{
				textBoxAmount.ReadOnly = true;
			}
			if (textBoxAmount.Tag != null)
			{
				textBoxAmount.Tag = null;
				textBoxAmount.Clear();
			}
		}

		private void comboBoxCostCenter_SelectedIndexChanged(object sender, EventArgs e)
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

		private void SetupExpenseGrid()
		{
			dataGridExpense.SetupUI();
			dataGridExpense.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("FeeCode");
			dataTable.Columns.Add("Description");
			dataTable.Columns.Add("ExpenseAccountID");
			dataTable.Columns.Add("Ref");
			dataTable.Columns.Add("Currency");
			dataTable.Columns.Add("RateType");
			dataTable.Columns.Add("Rate", typeof(decimal));
			dataTable.Columns.Add("TaxGroupID");
			dataTable.Columns.Add("TaxOption", typeof(byte));
			dataTable.Columns.Add("Tax", typeof(decimal));
			dataTable.Columns.Add("Amount", typeof(decimal));
			dataTable.Columns.Add("AmountLC", typeof(decimal));
			dataTable.Columns.Add("IsWithTR", typeof(bool));
			dataGridExpense.DataSource = dataTable;
			dataGridExpense.DisplayLayout.Bands[0].Columns["RateType"].Hidden = true;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].CharacterCasing = CharacterCasing.Upper;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].Header.Caption = "Fee Code";
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].MaxLength = 64;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].ValueList = comboBoxGridExpenseCode;
			dataGridExpense.DisplayLayout.Bands[0].Columns["IsWithTR"].Header.Caption = "W/TR";
			dataGridExpense.DisplayLayout.Bands[0].Columns["IsWithTR"].DefaultCellValue = false;
			dataGridExpense.DisplayLayout.Bands[0].Columns["ExpenseAccountID"].Hidden = true;
			dataGridExpense.DisplayLayout.Bands[0].Columns["ExpenseAccountID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].Header.Caption = "Amount (" + Global.BaseCurrencyID + ")";
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellActivation = Activation.Disabled;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridExpense.DisplayLayout.Bands[0].Columns["AmountLC"].Format = Format.GridAmountFormat;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].ValueList = comboBoxGridCurrency;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 64;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].MaxLength = 20;
			dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
			dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
			Color color2 = dataGridExpense.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance.BackColorDisabled = (dataGridExpense.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke);
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridExpense.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].MinValue = 0;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].MaxValue = new decimal(999999999999L);
			dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].Format = Format.GridAmountFormat;
			dataGridExpense.DisplayLayout.Bands[0].Columns["FeeCode"].Width = 100;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Ref"].Width = 100;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 70;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Currency"].Width = 70;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Rate"].Width = 70;
			dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = 250;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new BankFacilityTransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.BankFacilityTransactionTable.Rows[0] : currentData.BankFacilityTransactionTable.NewRow();
				dataRow["TransactionID"] = 0;
				dataRow["SysDocType"] = (byte)79;
				if (textBoxDescription.Text.Trim() != "")
				{
					dataRow["Description"] = textBoxDescription.Text;
				}
				else
				{
					dataRow["Description"] = textBoxNote.Text;
				}
				dataRow["CostCenterID"] = comboBoxCostCenter.SelectedID;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				if (textBoxRequest.Text != "")
				{
					dataRow["RequestSysDocID"] = requestSysDocID;
					dataRow["RequestVoucherID"] = textBoxRequest.Text;
				}
				else
				{
					dataRow["RequestSysDocID"] = DBNull.Value;
					dataRow["RequestVoucherID"] = DBNull.Value;
				}
				dataRow["SourceSysDocID"] = sourceSysDocID;
				dataRow["SourceVoucherID"] = sourceVoucherID;
				dataRow["BankFacilityID"] = comboBoxFacility.SelectedID;
				dataRow["FacilityType"] = 2;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["PayeeType"] = "V";
				dataRow["PayeeID"] = comboBoxVendor.SelectedID;
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
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.BankFacilityTransactionTable.Rows.Add(dataRow);
				}
				currentData.BankFacilityTransactionDetailsTable.Rows.Clear();
				DataRow dataRow2 = currentData.BankFacilityTransactionDetailsTable.NewRow();
				dataRow2.BeginEdit();
				dataRow2["AccountID"] = comboBoxFacility.PayableAccountID;
				dataRow2["Description"] = textBoxDescription.Text;
				dataRow2["BankFacilityID"] = comboBoxFacility.SelectedID;
				dataRow2["DueDate"] = datePickerDueDate.Value;
				if (currencySelector.SelectedID == "" || currencySelector.IsBaseCurrency)
				{
					dataRow2["Amount"] = textBoxAmount.Text;
					dataRow2["AmountFC"] = DBNull.Value;
				}
				else
				{
					dataRow2["AmountFC"] = textBoxAmount.Text;
				}
				dataRow2["RowIndex"] = 0;
				dataRow2.EndEdit();
				currentData.BankFacilityTransactionDetailsTable.Rows.Add(dataRow2);
				currentData.BankFeeDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridExpense.Rows)
				{
					DataRow dataRow3 = currentData.BankFeeDetailsTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["GLTransactionSysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow3["GLTransactionVoucherID"] = textBoxVoucherNumber.Text;
					dataRow3["BankFeeID"] = row.Cells["FeeCode"].Value.ToString();
					dataRow3["Description"] = row.Cells["Description"].Value.ToString();
					dataRow3["Reference"] = row.Cells["Ref"].Value.ToString();
					dataRow3["BankFacilityID"] = comboBoxFacility.SelectedID;
					dataRow3["BankAccountID"] = comboBoxFacility.CurrentAccountID;
					dataRow3["ExpenseAccountID"] = row.Cells["ExpenseAccountID"].Value.ToString();
					dataRow3["RowIndex"] = row.Index;
					string a = row.Cells["Currency"].Value.ToString();
					if (a == "" || a == Global.BaseCurrencyID)
					{
						dataRow3["Amount"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow3["AmountFC"] = row.Cells["Amount"].Value.ToString();
						dataRow3["Amount"] = row.Cells["AmountLC"].Value.ToString();
					}
					dataRow3["CurrencyID"] = row.Cells["Currency"].Value.ToString();
					dataRow3["CurrencyRate"] = row.Cells["Rate"].Value.ToString();
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow3["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow3["TaxOption"] = (byte)2;
					}
					if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
					{
						dataRow3["TaxAmount"] = row.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow3["TaxAmount"] = DBNull.Value;
					}
					if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow3["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow3["TaxGroupID"] = DBNull.Value;
					}
					if (!row.Cells["IsWithTR"].Value.IsNullOrEmpty())
					{
						dataRow3["IsWithTR"] = row.Cells["IsWithTR"].Value.ToString();
					}
					dataRow3.EndEdit();
					currentData.BankFeeDetailsTable.Rows.Add(dataRow3);
				}
				if (textBoxAmount.Tag != null)
				{
					DataSet dataSet = textBoxAmount.Tag as DataSet;
					dataSet = dataSet.Copy();
					if (currentData.Tables.Contains("AP_Payment_Advice"))
					{
						currentData.Tables.Remove("AP_Payment_Advice");
					}
					string a2 = Global.BaseCurrencyID;
					foreach (DataRow row2 in dataSet.Tables["AP_Payment_Advice"].Rows)
					{
						if (!row2["CurrencyID"].IsDBNullOrEmpty())
						{
							a2 = row2["CurrencyID"].ToString();
						}
						row2["PaymentSysDocID"] = comboBoxSysDoc.SelectedID;
						row2["PaymentVoucherID"] = textBoxVoucherNumber.Text;
						row2["AllocationDate"] = dateTimePickerDate.Value;
					}
					if (a2 != currencySelector.SelectedID)
					{
						ErrorHelper.WarningMessage("Currency should not change after selecting invoices.");
						return false;
					}
					currentData.Merge(dataSet.Tables["AP_Payment_Advice"]);
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				int num = 0;
				num = 0;
				foreach (UltraGridRow row3 in dataGridExpense.Rows)
				{
					if (row3.Cells["Tax"].Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, row3.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Expenses, selectedID, text, num, comboBoxGridCurrency.SelectedID, comboBoxGridCurrency.SelectedRate);
					}
					num = checked(num + 1);
				}
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text, -1, currencySelector.SelectedID, currencySelector.Rate);
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

		public void LoadData(string journalID)
		{
			try
			{
				if (!(journalID.Trim() == "") && CanClose())
				{
					currentData = Factory.BankFacilityTransactionSystem.GetBankFacilityTransactionByID(SystemDocID, journalID);
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
					DataRow dataRow = currentData.Tables["Bank_Facility_Transaction"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					comboBoxCostCenter.SelectedID = dataRow["CostCenterID"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					comboBoxVendor.SelectedID = dataRow["PayeeID"].ToString();
					requestSysDocID = dataRow["RequestSysDocID"].ToString();
					textBoxRequest.Text = dataRow["RequestVoucherID"].ToString();
					textBoxTaxAmount.Text = dataRow["TaxAmount"].ToString();
					sourceSysDocID = dataRow["SourceSysDocID"].ToString();
					sourceVoucherID = dataRow["SourceVoucherID"].ToString();
					if (textBoxRequest.Text != "")
					{
						comboBoxVendor.Enabled = false;
						comboBoxFacility.Enabled = false;
					}
					comboBoxFacility.SelectedID = dataRow["BankFacilityID"].ToString();
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
					if (dataRow["IsVoid"] != DBNull.Value)
					{
						IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
					}
					else
					{
						IsVoid = false;
					}
					if (currentData.Tables.Contains("Bank_Facility_Transaction_Details") && currentData.BankFacilityTransactionDetailsTable.Rows.Count != 0)
					{
						DataRow dataRow2 = currentData.Tables["Bank_Facility_Transaction_Details"].Rows[0];
						if (dataRow2["AmountFC"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow2["AmountFC"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else if (dataRow2["Amount"] != DBNull.Value)
						{
							textBoxAmount.Text = Math.Round(decimal.Parse(dataRow2["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
						}
						else
						{
							textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
						}
						datePickerDueDate.Value = DateTime.Parse(dataRow2["DueDate"].ToString());
						TimeSpan timeSpan = dateTimePickerDate.Value - datePickerDueDate.Value;
						numericUpDownTenorDay.Value = Math.Abs(timeSpan.Days);
						textBoxDescription.Text = dataRow2["Description"].ToString();
						DataTable dataTable = dataGridExpense.DataSource as DataTable;
						dataTable.Clear();
						textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
						foreach (DataRow row in currentData.Tables["Bank_Fee_Details"].Rows)
						{
							DataRow dataRow4 = dataTable.NewRow();
							bool flag = false;
							dataRow4["FeeCode"] = row["BankFeeID"];
							dataRow4["Description"] = row["Description"];
							dataRow4["Currency"] = row["CurrencyID"];
							dataRow4["Ref"] = row["Reference"];
							dataRow4["Rate"] = row["CurrencyRate"];
							dataRow4["RateType"] = row["RateType"];
							dataRow4["ExpenseAccountID"] = row["ExpenseAccountID"];
							dataRow4["IsWithTR"] = row["IsWithTR"];
							if (row["CurrencyID"].ToString() != "" && row["CurrencyID"].ToString() != Global.BaseCurrencyID)
							{
								flag = true;
							}
							dataRow4["Rate"] = row["CurrencyRate"];
							if (row["TaxOption"] != DBNull.Value)
							{
								dataRow4["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
							}
							else
							{
								dataRow4["TaxOption"] = ItemTaxOptions.NonTaxable;
							}
							if (row["TaxAmount"] != DBNull.Value)
							{
								dataRow4["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
							}
							if (!row["TaxGroupID"].IsDBNullOrEmpty())
							{
								dataRow4["TaxGroupID"] = row["TaxGroupID"];
							}
							if (flag)
							{
								dataRow4["Amount"] = decimal.Parse(row["AmountFC"].ToString()).ToString(Format.TotalAmountFormat);
							}
							else
							{
								dataRow4["Amount"] = decimal.Parse(row["Amount"].ToString()).ToString(Format.TotalAmountFormat);
							}
							dataRow4.EndEdit();
							dataTable.Rows.Add(dataRow4);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridExpense.Rows)
						{
							SetRowLCAmount(row2);
						}
						if (currentData.Tables["AP_Payment_Advice"] != null)
						{
							textBoxAmount.Tag = currentData;
							textBoxAmount.ReadOnly = true;
						}
						CalculateTotalExpense();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		private void CalculateTotalExpense()
		{
			decimal num = default(decimal);
			decimal d = default(decimal);
			decimal num2 = default(decimal);
			foreach (UltraGridRow row in dataGridExpense.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				if (row.Cells["AmountLC"].Value != null && !(row.Cells["AmountLC"].Value.ToString() == ""))
				{
					decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result2);
					num2 += result2;
					decimal.TryParse(row.Cells["AmountLC"].Value.ToString(), out result);
					num += result;
					if (!row.Cells["IsWithTR"].Value.IsNullOrEmpty() && bool.Parse(row.Cells["IsWithTR"].Value.ToString()))
					{
						d += result;
					}
				}
			}
			textBoxTaxAmount.Text = num2.ToString(Format.TotalAmountFormat);
			textBoxTotalExpense.Text = num.ToString(Format.TotalAmountFormat);
			textBoxFinancedFees.Text = d.ToString(Format.TotalAmountFormat);
			decimal d2 = decimal.Parse(textBoxAmountBase.Text);
			textBoxTotalTRAmount.Text = (d2 + d).ToString(Format.TotalAmountFormat);
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
			bool flag = false;
			try
			{
				bool flag2 = (!isNewRecord) ? Factory.BankFacilityTransactionSystem.UpdateBankFacilityTransaction(currentData) : Factory.BankFacilityTransactionSystem.CreateBankFacilityTransaction(currentData);
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage();
				}
				else
				{
					if (showAllocationForm && textBoxAmount.Tag == null)
					{
						CustomerPaymentAllocationForm customerPaymentAllocationForm = new CustomerPaymentAllocationForm();
						customerPaymentAllocationForm.IsARPayment = false;
						customerPaymentAllocationForm.SetData(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, comboBoxVendor.SelectedID, -1, isPDC: false);
						customerPaymentAllocationForm.ShowDialog();
					}
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
				return flag2;
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
			finally
			{
				if (flag)
				{
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Bank_Facility_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			if (comboBoxFacility.PayableAccountID == "")
			{
				ErrorHelper.WarningMessage("Payable account is not set for the selected bank facility.");
				return false;
			}
			if (comboBoxFacility.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "" || comboBoxVendor.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (isCostCenterMandatory && comboBoxCostCenter.SelectedID == string.Empty)
			{
				ErrorHelper.InformationMessage("Please select cost center");
				return false;
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Bank_Facility_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
				textBoxVoucherNumber.Focus();
				return false;
			}
			return true;
		}

		private decimal GetTransactionBalance()
		{
			return 0m;
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			panelButtons.Visible = false;
			toolStrip1.Enabled = false;
			formManager.ShowApprovalPanel(approvalTaskID, "Bank_Facility_Transaction", "VoucherID");
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
				comboBoxFacility.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				buttonSelectInvoice.Enabled = true;
				textBoxAmount.ReadOnly = false;
				textBoxAmount.Tag = null;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxVendor.Clear();
				textBoxPayeeName.Clear();
				textBoxDescription.Clear();
				textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotalExpense.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotalTRAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxFinancedFees.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxAmountBase.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxRequest.Clear();
				requestSysDocID = "";
				sourceSysDocID = "";
				sourceVoucherID = "";
				comboBoxVendor.Enabled = true;
				comboBoxFacility.Enabled = true;
				currencySelector.Enabled = true;
				textBoxRegBalance.Clear();
				datePickerDueDate.Value = dateTimePickerDate.Value.AddDays(int.Parse(numericUpDownTenorDay.Value.ToString()));
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridExpense.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxVendor.Focus();
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
				return Factory.BankFacilityTransactionSystem.DeleteBankFacilityTransaction(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Bank_Facility_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Bank_Facility_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Bank_Facility_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Bank_Facility_Transaction", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Bank_Facility_Transaction", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void TransactionLeavesForm_Load(object sender, EventArgs e)
		{
			try
			{
				UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
				bool visible = currencySelector.Visible = CompanyPreferences.UseMultiCurrency;
				ultraFormattedLinkLabel.Visible = visible;
				SetupExpenseGrid();
				labelAmountBase.Text = "Amount (" + Global.BaseCurrencyID + "):";
				SetSecurity();
				if (!base.IsDisposed)
				{
					dateTimePickerDate.Value = DateTime.Now;
					IsNewRecord = true;
					ClearForm();
					comboBoxSysDoc.FilterByType(SysDocTypes.TR);
				}
			}
			catch (Exception e2)
			{
				dataGridExpense.LoadLayoutFailed = true;
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
				return Factory.BankFacilityTransactionSystem.VoidBankFacilityTransaction(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCenter(comboBoxCostCenter.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.TR);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBankFacility(comboBoxFacility.SelectedID);
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
					DataSet bankFacilityTransactionToPrint = Factory.BankFacilityTransactionSystem.GetBankFacilityTransactionToPrint(systemDocID, text);
					if (bankFacilityTransactionToPrint == null || bankFacilityTransactionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(bankFacilityTransactionToPrint, systemDocID, "TT Receipt", SysDocTypes.TR, isPrint, showPrintDialog);
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

		private void labelVoided_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.TRListObj);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.TR);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 79.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.TR);
					currentData = (dataSet as BankFacilityTransactionData);
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

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					if (!(comboBoxSysDoc.SelectedID == "") || !(textBoxVoucherNumber.Text.Trim() == ""))
					{
						docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
						docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
						docManagementForm.EntityName = comboBoxSysDoc.SelectedID;
						docManagementForm.EntityType = EntityTypesEnum.Transactions;
						docManagementForm.ShowDialog(this);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxAmount_TextChanged(object sender, EventArgs e)
		{
			CalculateBaseAmount();
			CalculateTotalExpense();
		}

		private void CalculateBaseAmount()
		{
			if (currencySelector.IsBaseCurrency)
			{
				textBoxAmountBase.Text = textBoxAmount.Text;
				return;
			}
			decimal result = default(decimal);
			decimal.TryParse(textBoxAmount.Text, out result);
			textBoxAmountBase.Text = currencySelector.GetBaseEquivalant(result).ToString(Format.TotalAmountFormat);
		}

		private void numericUpDownTenorDay_ValueChanged(object sender, EventArgs e)
		{
			datePickerDueDate.Value = dateTimePickerDate.Value.AddDays(int.Parse(numericUpDownTenorDay.Value.ToString()));
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonSelectRequest_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet openPaymentRequests = Factory.PaymentRequestSystem.GetOpenPaymentRequests(3);
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = openPaymentRequests;
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.Text = "Select Payment Request";
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					UltraGridRow activeRow = selectDocumentDialog.Grid.ActiveRow;
					string sysDocID = activeRow.Cells["DocID"].Value.ToString();
					string text = activeRow.Cells["Number"].Value.ToString();
					PaymentRequestData paymentRequestByID = Factory.PaymentRequestSystem.GetPaymentRequestByID(sysDocID, text);
					if (paymentRequestByID != null && paymentRequestByID.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = paymentRequestByID.Tables[0].Rows[0];
						textBoxRequest.Text = text;
						requestSysDocID = sysDocID;
						comboBoxVendor.SelectedID = dataRow["PayeeID"].ToString();
						comboBoxFacility.SelectedID = dataRow["PayFromID"].ToString();
						if (!dataRow["AmountFC"].IsDBNullOrEmpty())
						{
							textBoxAmount.Text = dataRow["AmountFC"].ToString();
						}
						else
						{
							textBoxAmount.Text = dataRow["Amount"].ToString();
						}
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						comboBoxVendor.Enabled = false;
						comboBoxFacility.Enabled = false;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransaction(TransactionListType.PaymentRequest, requestSysDocID, textBoxRequest.Text);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			DataSet dataSet = null;
			if (textBoxAmount.Tag != null)
			{
				dataSet = (textBoxAmount.Tag as DataSet);
				PaymentAdviceDetailsForm paymentAdviceDetailsForm = new PaymentAdviceDetailsForm();
				paymentAdviceDetailsForm.SetData(comboBoxVendor.SelectedID, comboBoxVendor.SelectedName, currencySelector.SelectedID, currencySelector.Rate);
				paymentAdviceDetailsForm.PaymentData = dataSet;
				paymentAdviceDetailsForm.IsViewOnly = true;
				paymentAdviceDetailsForm.ShowDialog();
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void buttonSelectInvoice_Click(object sender, EventArgs e)
		{
			string text = "";
			text = comboBoxVendor.SelectedID;
			SelectInvoicesToPay(text, comboBoxVendor.SelectedName, currencySelector.SelectedID);
		}

		private void createFromTRApplicationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet openTransactions = Factory.TRApplicationSystem.GetOpenTransactions("uninvoiced");
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = openTransactions;
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.Text = "Select TR Application";
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					UltraGridRow activeRow = selectDocumentDialog.Grid.ActiveRow;
					string sysDocID = activeRow.Cells["sysDocID"].Value.ToString();
					string voucherID = activeRow.Cells["VoucherID"].Value.ToString();
					TRApplicationData tRApplicationByID = Factory.TRApplicationSystem.GetTRApplicationByID(sysDocID, voucherID);
					if (tRApplicationByID != null && tRApplicationByID.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = tRApplicationByID.Tables[0].Rows[0];
						sourceVoucherID = voucherID;
						sourceSysDocID = sysDocID;
						comboBoxVendor.SelectedID = dataRow["PayeeID"].ToString();
						comboBoxFacility.SelectedID = dataRow["BankFacilityID"].ToString();
						comboBoxCostCenter.SelectedID = dataRow["CostCenterID"].ToString();
						textBoxDescription.Text = dataRow["Description"].ToString();
						textBoxNote.Text = dataRow["Note"].ToString();
						if (!dataRow["AmountFC"].IsDBNullOrEmpty())
						{
							textBoxAmount.Text = dataRow["AmountFC"].ToString();
						}
						else
						{
							textBoxAmount.Text = dataRow["Amount"].ToString();
						}
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						decimal result = 1m;
						decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result);
						currencySelector.Rate = result;
						comboBoxVendor.Enabled = false;
						comboBoxFacility.Enabled = false;
						textBoxAmount.ReadOnly = true;
						buttonSelectInvoice.Enabled = false;
						CalculateBaseAmount();
					}
					DataRow[] array = tRApplicationByID.Tables["AP_Payment_Advice"].Select("IsDraft=1");
					if (array.Length != 0)
					{
						DataRow[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							array2[i]["IsDraft"] = DBNull.Value;
						}
						DataSet dataSet = new DataSet();
						dataSet.Merge(array);
						textBoxAmount.Tag = dataSet;
					}
					bool enabled = true;
					if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID && array.Length != 0)
					{
						_ = tRApplicationByID.Tables["AP_Payment_Advice"].Rows;
						foreach (DataRow row in tRApplicationByID.Tables["AP_Payment_Advice"].Rows)
						{
							if (row["CurrencyID"].ToString() != Global.BaseCurrencyID.ToString())
							{
								enabled = false;
							}
						}
					}
					currencySelector.Enabled = enabled;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void SelectInvoicesToPay(string vendorID, string vendorName, string currencyID)
		{
			try
			{
				if (vendorID != "")
				{
					PaymentAdviceDetailsForm paymentAdviceDetailsForm = new PaymentAdviceDetailsForm();
					paymentAdviceDetailsForm.IsARPayment = false;
					paymentAdviceDetailsForm.SetData(vendorID, vendorName, currencyID, currencySelector.Rate);
					if (paymentAdviceDetailsForm.ShowDialog() == DialogResult.OK)
					{
						textBoxAmount.Tag = paymentAdviceDetailsForm.PaymentData;
						decimal d = default(decimal);
						foreach (DataRow row in paymentAdviceDetailsForm.PaymentData.Tables["AP_Payment_Advice"].Rows)
						{
							d += decimal.Parse(row["PaymentAmount"].ToString());
						}
						textBoxAmount.Text = d.ToString(Format.TotalAmountFormat);
						if (d > 0m)
						{
							textBoxAmount.ReadOnly = true;
						}
					}
				}
				else
				{
					ErrorHelper.InformationMessage("Please select a vendor.");
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
			Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.TREntryForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelectInvoice = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			label8 = new System.Windows.Forms.Label();
			textBoxTotalTRAmount = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxFinancedFees = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			numericUpDownTenorDay = new System.Windows.Forms.NumericUpDown();
			textBoxRegBalance = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			datePickerDueDate = new Micromind.UISupport.flatDatePicker();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			labelAmountBase = new System.Windows.Forms.Label();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			labelVoided = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			textBoxAmountBase = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			comboBoxFacility = new Micromind.DataControls.BankFacilityComboBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			textBoxFacilityName = new System.Windows.Forms.TextBox();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
			textBoxTotalExpense = new Micromind.UISupport.NumberTextBox();
			label7 = new System.Windows.Forms.Label();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			createFromTRApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxDescription = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			panelDetails = new System.Windows.Forms.Panel();
			buttonSelectRequest = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxRequest = new System.Windows.Forms.TextBox();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			formManager = new Micromind.DataControls.FormManager();
			tabPageItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownTenorDay).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).BeginInit();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			SuspendLayout();
			tabPageItems.Controls.Add(ultraFormattedLinkLabel8);
			tabPageItems.Controls.Add(buttonSelectInvoice);
			tabPageItems.Controls.Add(line1);
			tabPageItems.Controls.Add(label8);
			tabPageItems.Controls.Add(textBoxTotalTRAmount);
			tabPageItems.Controls.Add(label6);
			tabPageItems.Controls.Add(textBoxFinancedFees);
			tabPageItems.Controls.Add(ultraFormattedLinkLabel6);
			tabPageItems.Controls.Add(numericUpDownTenorDay);
			tabPageItems.Controls.Add(textBoxRegBalance);
			tabPageItems.Controls.Add(label5);
			tabPageItems.Controls.Add(datePickerDueDate);
			tabPageItems.Controls.Add(textBoxRef1);
			tabPageItems.Controls.Add(labelAmountBase);
			tabPageItems.Controls.Add(textBoxAmount);
			tabPageItems.Controls.Add(labelVoided);
			tabPageItems.Controls.Add(label1);
			tabPageItems.Controls.Add(textBoxAmountBase);
			tabPageItems.Controls.Add(label2);
			tabPageItems.Controls.Add(comboBoxFacility);
			tabPageItems.Controls.Add(mmLabel3);
			tabPageItems.Controls.Add(textBoxFacilityName);
			tabPageItems.Location = new System.Drawing.Point(1, 23);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(661, 194);
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(3, 63);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(52, 15);
			ultraFormattedLinkLabel8.TabIndex = 163;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Amount:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			buttonSelectInvoice.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectInvoice.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectInvoice.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectInvoice.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectInvoice.Enabled = false;
			buttonSelectInvoice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectInvoice.Location = new System.Drawing.Point(192, 61);
			buttonSelectInvoice.Name = "buttonSelectInvoice";
			buttonSelectInvoice.Size = new System.Drawing.Size(27, 21);
			buttonSelectInvoice.TabIndex = 7;
			buttonSelectInvoice.Text = "...";
			buttonSelectInvoice.UseVisualStyleBackColor = false;
			buttonSelectInvoice.Click += new System.EventHandler(buttonSelectInvoice_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(222, 117);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(327, 1);
			line1.TabIndex = 140;
			line1.TabStop = false;
			label8.AutoSize = true;
			label8.BackColor = System.Drawing.Color.Transparent;
			label8.Location = new System.Drawing.Point(241, 125);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(91, 13);
			label8.TabIndex = 139;
			label8.Text = "Total TR Amount:";
			textBoxTotalTRAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalTRAmount.Location = new System.Drawing.Point(339, 122);
			textBoxTotalTRAmount.Name = "textBoxTotalTRAmount";
			textBoxTotalTRAmount.ReadOnly = true;
			textBoxTotalTRAmount.Size = new System.Drawing.Size(131, 20);
			textBoxTotalTRAmount.TabIndex = 10;
			textBoxTotalTRAmount.TabStop = false;
			textBoxTotalTRAmount.Text = "0.00";
			textBoxTotalTRAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label6.AutoSize = true;
			label6.BackColor = System.Drawing.Color.Transparent;
			label6.Location = new System.Drawing.Point(241, 92);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(80, 13);
			label6.TabIndex = 137;
			label6.Text = "Financed Fees:";
			textBoxFinancedFees.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFinancedFees.Location = new System.Drawing.Point(339, 89);
			textBoxFinancedFees.Name = "textBoxFinancedFees";
			textBoxFinancedFees.ReadOnly = true;
			textBoxFinancedFees.Size = new System.Drawing.Size(131, 20);
			textBoxFinancedFees.TabIndex = 9;
			textBoxFinancedFees.TabStop = false;
			textBoxFinancedFees.Text = "0.00";
			textBoxFinancedFees.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			appearance3.BackColor = System.Drawing.Color.Transparent;
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance3;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(3, 15);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(60, 15);
			ultraFormattedLinkLabel6.TabIndex = 121;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Pay From:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			numericUpDownTenorDay.Location = new System.Drawing.Point(306, 37);
			numericUpDownTenorDay.Maximum = new decimal(new int[4]
			{
				500,
				0,
				0,
				0
			});
			numericUpDownTenorDay.Name = "numericUpDownTenorDay";
			numericUpDownTenorDay.Size = new System.Drawing.Size(61, 20);
			numericUpDownTenorDay.TabIndex = 4;
			numericUpDownTenorDay.Value = new decimal(new int[4]
			{
				30,
				0,
				0,
				0
			});
			numericUpDownTenorDay.ValueChanged += new System.EventHandler(numericUpDownTenorDay_ValueChanged);
			textBoxRegBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRegBalance.Location = new System.Drawing.Point(476, 15);
			textBoxRegBalance.Name = "textBoxRegBalance";
			textBoxRegBalance.ReadOnly = true;
			textBoxRegBalance.Size = new System.Drawing.Size(133, 20);
			textBoxRegBalance.TabIndex = 2;
			textBoxRegBalance.TabStop = false;
			textBoxRegBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label5.AutoSize = true;
			label5.BackColor = System.Drawing.Color.Transparent;
			label5.Location = new System.Drawing.Point(239, 40);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(65, 13);
			label5.TabIndex = 135;
			label5.Text = "Tenor Days:";
			datePickerDueDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			datePickerDueDate.Location = new System.Drawing.Point(444, 38);
			datePickerDueDate.Name = "datePickerDueDate";
			datePickerDueDate.Size = new System.Drawing.Size(127, 20);
			datePickerDueDate.TabIndex = 5;
			datePickerDueDate.Value = new System.DateTime(2015, 5, 13, 0, 0, 0, 0);
			textBoxRef1.Location = new System.Drawing.Point(80, 38);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(111, 20);
			textBoxRef1.TabIndex = 3;
			labelAmountBase.AutoSize = true;
			labelAmountBase.BackColor = System.Drawing.Color.Transparent;
			labelAmountBase.Location = new System.Drawing.Point(241, 66);
			labelAmountBase.Name = "labelAmountBase";
			labelAmountBase.Size = new System.Drawing.Size(71, 13);
			labelAmountBase.TabIndex = 135;
			labelAmountBase.Text = "Amount AED:";
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(80, 62);
			textBoxAmount.MaxLength = 15;
			textBoxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAmount.Name = "textBoxAmount";
			textBoxAmount.NullText = "0";
			textBoxAmount.Size = new System.Drawing.Size(111, 20);
			textBoxAmount.TabIndex = 6;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxAmount.TextChanged += new System.EventHandler(textBoxAmount_TextChanged);
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelVoided.BackColor = System.Drawing.Color.Transparent;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(6, 162);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(91, 21);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			labelVoided.Click += new System.EventHandler(labelVoided_Click);
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.Transparent;
			label1.Location = new System.Drawing.Point(3, 40);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(45, 13);
			label1.TabIndex = 20;
			label1.Text = "TR Ref:";
			textBoxAmountBase.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmountBase.Location = new System.Drawing.Point(339, 63);
			textBoxAmountBase.Name = "textBoxAmountBase";
			textBoxAmountBase.ReadOnly = true;
			textBoxAmountBase.Size = new System.Drawing.Size(131, 20);
			textBoxAmountBase.TabIndex = 8;
			textBoxAmountBase.TabStop = false;
			textBoxAmountBase.Text = "0.00";
			textBoxAmountBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label2.AutoSize = true;
			label2.BackColor = System.Drawing.Color.Transparent;
			label2.Location = new System.Drawing.Point(424, 17);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(31, 13);
			label2.TabIndex = 129;
			label2.Text = "Limit:";
			comboBoxFacility.Assigned = false;
			comboBoxFacility.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxFacility.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxFacility.CustomReportFieldName = "";
			comboBoxFacility.CustomReportKey = "";
			comboBoxFacility.CustomReportValueType = 1;
			comboBoxFacility.DescriptionTextBox = null;
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
			comboBoxFacility.FilterFacilityType = Micromind.Common.Data.BankFacilityTypes.TR;
			comboBoxFacility.FilterString = "";
			comboBoxFacility.HasAllAccount = false;
			comboBoxFacility.HasCustom = false;
			comboBoxFacility.IsDataLoaded = false;
			comboBoxFacility.Location = new System.Drawing.Point(80, 15);
			comboBoxFacility.MaxDropDownItems = 12;
			comboBoxFacility.Name = "comboBoxFacility";
			comboBoxFacility.ShowInactiveItems = false;
			comboBoxFacility.ShowQuickAdd = true;
			comboBoxFacility.Size = new System.Drawing.Size(111, 20);
			comboBoxFacility.TabIndex = 0;
			comboBoxFacility.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel3.AutoSize = true;
			mmLabel3.BackColor = System.Drawing.Color.Transparent;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(373, 41);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(65, 13);
			mmLabel3.TabIndex = 2;
			mmLabel3.Text = "Due Date:";
			textBoxFacilityName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxFacilityName.Location = new System.Drawing.Point(194, 15);
			textBoxFacilityName.Name = "textBoxFacilityName";
			textBoxFacilityName.ReadOnly = true;
			textBoxFacilityName.Size = new System.Drawing.Size(226, 20);
			textBoxFacilityName.TabIndex = 1;
			textBoxFacilityName.TabStop = false;
			tabPageExpense.Controls.Add(textBoxTaxAmount);
			tabPageExpense.Controls.Add(dataGridExpense);
			tabPageExpense.Controls.Add(textBoxTotalExpense);
			tabPageExpense.Controls.Add(label7);
			tabPageExpense.Controls.Add(comboBoxGridExpenseCode);
			tabPageExpense.Controls.Add(comboBoxGridCurrency);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(661, 194);
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(191, 173);
			textBoxTaxAmount.MaxLength = 15;
			textBoxTaxAmount.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTaxAmount.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTaxAmount.Name = "textBoxTaxAmount";
			textBoxTaxAmount.NullText = "0";
			textBoxTaxAmount.ReadOnly = true;
			textBoxTaxAmount.Size = new System.Drawing.Size(134, 20);
			textBoxTaxAmount.TabIndex = 138;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTaxAmount.Visible = false;
			dataGridExpense.AllowAddNew = false;
			dataGridExpense.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance17;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance24;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance26;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance27;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExpense.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExpense.ExitEditModeOnLeave = false;
			dataGridExpense.IncludeLotItems = false;
			dataGridExpense.LoadLayoutFailed = false;
			dataGridExpense.Location = new System.Drawing.Point(9, 9);
			dataGridExpense.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(642, 159);
			dataGridExpense.TabIndex = 2;
			dataGridExpense.Text = "dataEntryGrid1";
			textBoxTotalExpense.AllowDecimal = true;
			textBoxTotalExpense.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			textBoxTotalExpense.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalExpense.CustomReportFieldName = "";
			textBoxTotalExpense.CustomReportKey = "";
			textBoxTotalExpense.CustomReportValueType = 1;
			textBoxTotalExpense.ForeColor = System.Drawing.Color.Black;
			textBoxTotalExpense.IsComboTextBox = false;
			textBoxTotalExpense.IsModified = false;
			textBoxTotalExpense.Location = new System.Drawing.Point(511, 171);
			textBoxTotalExpense.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotalExpense.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotalExpense.Name = "textBoxTotalExpense";
			textBoxTotalExpense.NullText = "0";
			textBoxTotalExpense.ReadOnly = true;
			textBoxTotalExpense.Size = new System.Drawing.Size(138, 20);
			textBoxTotalExpense.TabIndex = 0;
			textBoxTotalExpense.TabStop = false;
			textBoxTotalExpense.Text = "0.00";
			textBoxTotalExpense.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label7.BackColor = System.Drawing.Color.Transparent;
			label7.Location = new System.Drawing.Point(6, 171);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(498, 18);
			label7.TabIndex = 134;
			label7.Text = "Total Bank Fees:";
			label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			comboBoxGridExpenseCode.AlwaysInEditMode = true;
			comboBoxGridExpenseCode.Assigned = false;
			comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridExpenseCode.CustomReportFieldName = "";
			comboBoxGridExpenseCode.CustomReportKey = "";
			comboBoxGridExpenseCode.CustomReportValueType = 1;
			comboBoxGridExpenseCode.DescriptionTextBox = null;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance29;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance30.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance30.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance30.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance30.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance30;
			appearance31.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance31;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance32.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance32.BackColor2 = System.Drawing.SystemColors.Control;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance32;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance33;
			appearance34.BackColor = System.Drawing.SystemColors.Highlight;
			appearance34.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance34;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance35;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			appearance36.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance36;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance37.BackColor = System.Drawing.SystemColors.Control;
			appearance37.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance37.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance37.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance37.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance37;
			appearance38.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance38;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance39;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance40;
			comboBoxGridExpenseCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridExpenseCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridExpenseCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridExpenseCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridExpenseCode.Editable = true;
			comboBoxGridExpenseCode.FilterString = "";
			comboBoxGridExpenseCode.HasAllAccount = false;
			comboBoxGridExpenseCode.HasCustom = false;
			comboBoxGridExpenseCode.IsDataLoaded = false;
			comboBoxGridExpenseCode.Location = new System.Drawing.Point(557, 40);
			comboBoxGridExpenseCode.MaxDropDownItems = 12;
			comboBoxGridExpenseCode.Name = "comboBoxGridExpenseCode";
			comboBoxGridExpenseCode.ShowInactiveItems = false;
			comboBoxGridExpenseCode.ShowQuickAdd = true;
			comboBoxGridExpenseCode.Size = new System.Drawing.Size(124, 20);
			comboBoxGridExpenseCode.TabIndex = 121;
			comboBoxGridExpenseCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridExpenseCode.Visible = false;
			comboBoxGridCurrency.AlwaysInEditMode = true;
			comboBoxGridCurrency.Assigned = false;
			comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCurrency.CustomReportFieldName = "";
			comboBoxGridCurrency.CustomReportKey = "";
			comboBoxGridCurrency.CustomReportValueType = 1;
			comboBoxGridCurrency.DescriptionTextBox = null;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance41;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance42.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance42;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance43;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance44.BackColor2 = System.Drawing.SystemColors.Control;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance44.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance44;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance45;
			appearance46.BackColor = System.Drawing.SystemColors.Highlight;
			appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance46;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance47;
			appearance48.BorderColor = System.Drawing.Color.Silver;
			appearance48.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance48;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance49.BackColor = System.Drawing.SystemColors.Control;
			appearance49.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance49.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance49.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance49.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance49;
			appearance50.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance50;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance51;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance52;
			comboBoxGridCurrency.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCurrency.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCurrency.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCurrency.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCurrency.Editable = true;
			comboBoxGridCurrency.FilterString = "";
			comboBoxGridCurrency.HasAllAccount = false;
			comboBoxGridCurrency.HasCustom = false;
			comboBoxGridCurrency.IsDataLoaded = false;
			comboBoxGridCurrency.Location = new System.Drawing.Point(350, 104);
			comboBoxGridCurrency.MaxDropDownItems = 12;
			comboBoxGridCurrency.Name = "comboBoxGridCurrency";
			comboBoxGridCurrency.ShowInactiveItems = false;
			comboBoxGridCurrency.ShowQuickAdd = true;
			comboBoxGridCurrency.Size = new System.Drawing.Size(95, 20);
			comboBoxGridCurrency.TabIndex = 122;
			comboBoxGridCurrency.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCurrency.Visible = false;
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator3,
				toolStripButtonOpenList,
				toolStripSeparator1,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator4,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonInformation,
				toolStripDropDownButton1
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(684, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(28, 28);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				saveAsDraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator6,
				createFromTRApplicationToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(217, 6);
			createFromTRApplicationToolStripMenuItem.Name = "createFromTRApplicationToolStripMenuItem";
			createFromTRApplicationToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
			createFromTRApplicationToolStripMenuItem.Text = "Create From TR Application";
			createFromTRApplicationToolStripMenuItem.Click += new System.EventHandler(createFromTRApplicationToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 395);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(684, 40);
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
			linePanelDown.Size = new System.Drawing.Size(684, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(579, 8);
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(308, 7);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(111, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Location = new System.Drawing.Point(86, 97);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(534, 20);
			textBoxNote.TabIndex = 10;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 100);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance53.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance53;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(204, 32);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 14);
			labelCurrency.TabIndex = 4;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance54.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance54;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance55.FontData.BoldAsString = "True";
			appearance55.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance55;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(202, 9);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 1;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(268, 52);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(352, 20);
			textBoxPayeeName.TabIndex = 8;
			textBoxPayeeName.TabStop = false;
			appearance57.FontData.BoldAsString = "True";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance57;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 9);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 116;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			appearance59.FontData.BoldAsString = "False";
			appearance59.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance59;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(10, 29);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel4.TabIndex = 114;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Cost Center:";
			appearance60.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance60;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			appearance61.FontData.BoldAsString = "True";
			appearance61.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance61;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(10, 53);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel3.TabIndex = 112;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Vendor:";
			appearance62.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance62;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			textBoxDescription.Location = new System.Drawing.Point(86, 75);
			textBoxDescription.MaxLength = 255;
			textBoxDescription.Name = "textBoxDescription";
			textBoxDescription.Size = new System.Drawing.Size(534, 20);
			textBoxDescription.TabIndex = 9;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(9, 78);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(49, 13);
			label4.TabIndex = 127;
			label4.Text = "Paid For:";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(10, 169);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(665, 220);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "TR Details";
			ultraTab2.TabPage = tabPageExpense;
			ultraTab2.Text = "Bank Fees";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(661, 194);
			panelDetails.Controls.Add(buttonSelectRequest);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(textBoxRequest);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(textBoxDescription);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(comboBoxCostCenter);
			panelDetails.Controls.Add(textBoxPayeeName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Location = new System.Drawing.Point(0, 37);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(672, 128);
			panelDetails.TabIndex = 0;
			buttonSelectRequest.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectRequest.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectRequest.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectRequest.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectRequest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectRequest.Location = new System.Drawing.Point(596, 7);
			buttonSelectRequest.Name = "buttonSelectRequest";
			buttonSelectRequest.Size = new System.Drawing.Size(26, 22);
			buttonSelectRequest.TabIndex = 3;
			buttonSelectRequest.Text = "...";
			buttonSelectRequest.UseVisualStyleBackColor = false;
			buttonSelectRequest.Click += new System.EventHandler(buttonSelectRequest_Click);
			appearance63.FontData.BoldAsString = "False";
			appearance63.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance63;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(425, 9);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel1.TabIndex = 129;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Request:";
			appearance64.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance64;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			textBoxRequest.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRequest.Location = new System.Drawing.Point(487, 7);
			textBoxRequest.Name = "textBoxRequest";
			textBoxRequest.ReadOnly = true;
			textBoxRequest.Size = new System.Drawing.Size(108, 20);
			textBoxRequest.TabIndex = 2;
			textBoxRequest.TabStop = false;
			comboBoxVendor.Assigned = false;
			comboBoxVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance65;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			comboBoxVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVendor.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVendor.Editable = true;
			comboBoxVendor.FilterString = "";
			comboBoxVendor.FilterSysDocID = "";
			comboBoxVendor.HasAll = false;
			comboBoxVendor.HasCustom = false;
			comboBoxVendor.IsDataLoaded = false;
			comboBoxVendor.Location = new System.Drawing.Point(86, 52);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(179, 20);
			comboBoxVendor.TabIndex = 7;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(439, 32);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(259, 29);
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
			currencySelector.Size = new System.Drawing.Size(174, 20);
			currencySelector.TabIndex = 5;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(487, 29);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(133, 20);
			dateTimePickerDate.TabIndex = 6;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 57, 9, 732);
			comboBoxCostCenter.AlwaysInEditMode = true;
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance77;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance78.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance78.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance78.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance78;
			appearance79.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance79;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance80.BackColor2 = System.Drawing.SystemColors.Control;
			appearance80.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance80.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance80;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance81;
			appearance82.BackColor = System.Drawing.SystemColors.Highlight;
			appearance82.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance82;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance83;
			appearance84.BorderColor = System.Drawing.Color.Silver;
			appearance84.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance84;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance85.BackColor = System.Drawing.SystemColors.Control;
			appearance85.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance85.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance85;
			appearance86.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance86;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance87;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance88;
			comboBoxCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCenter.Editable = true;
			comboBoxCostCenter.FilterString = "";
			comboBoxCostCenter.HasAllAccount = false;
			comboBoxCostCenter.HasCustom = false;
			comboBoxCostCenter.IsDataLoaded = false;
			comboBoxCostCenter.Location = new System.Drawing.Point(86, 29);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(111, 20);
			comboBoxCostCenter.TabIndex = 4;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance89;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance96;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance98;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance99;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(86, 7);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(111, 20);
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
			formManager.TabIndex = 139;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(684, 435);
			base.Controls.Add(formManager);
			base.Controls.Add(panelDetails);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(630, 166);
			base.Name = "TREntryForm";
			Text = "TR Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownTenorDay).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxFacility).EndInit();
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
