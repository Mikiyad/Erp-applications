using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
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
	public class OpeningChequePaymentEntryForm : Form, IForm, ITransactionForm, IWorkFlowForm
	{
		private string requestSysDocID = "";

		private OpenEntryTransactionData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool trackConsignInExpense = CompanyPreferences.TrackConsignInExpense;

		private bool isCostCenterMandatory = CompanyPreferences.IsCostCenterMandatory;

		private bool showAllocationForm = CompanyPreferences.ShowAllocationForm;

		private ScreenAccessRight screenRight;

		private bool ShowSupplierBal;

		private bool ShowCustomerBal;

		private bool ShowAccountBal;

		private bool ShowEmployeeBal;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private string _entityType = string.Empty;

		private decimal _amount;

		private string _entityID = string.Empty;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private DataEntryGrid dataGridItems;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private CurrencySelector currencySelector;

		private UltraFormattedLinkLabel labelCurrency;

		private AnalysisComboBox comboBoxAnalysis;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraLabel ultraLabel1;

		private UltraGroupBox ultraGroupBox1;

		private UltraLabel labelBalance;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private Label labelVoided;

		private PayeeSelector payeeSelector1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private CostCenterComboBox comboBoxCostCenter;

		private paymentMethodsComboBox comboBoxPaymentMethod;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxPayeeName;

		private Label labelAnalysis;

		private flatDatePicker dateTimePickerDate;

		private ChequebookComboBox comboBoxChequebook;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ExpenseCodeComboBox comboBoxConsignExpense;

		private ConsignInComboBox comboBoxConsignIn;

		private XPButton buttonSelectRequest;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxRequest;

		private Label label5;

		private TextBox textBoxBalance;

		private MMLabel mmLabel4;

		private AmountTextBox textBoxTotalDue;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private Label label2;

		private MMSDateTimePicker dateTimePickerDeliveredDate;

		private ToolStrip toolStrip1;

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

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonInformation;

		private TextBox textBoxPayeeType;

		private PayeeTypeComboBox payeeTypeComboBox1;

		private EmployeeComboBox comboBoxGridEmployee;

		private vendorsFlatComboBox comboBoxGridVendor;

		private customersFlatComboBox comboBoxGridCustomer;

		private AllAccountsComboBox comboBoxGridAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripSeparator toolStripSeparator5;

		public ScreenAreas ScreenArea => ScreenAreas.Accounts;

		public int ScreenID => 1011;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public string SystemDocID
		{
			get
			{
				return comboBoxSysDoc.SelectedID;
			}
			set
			{
				comboBoxSysDoc.SelectedID = value;
			}
		}

		public string VoucherID => textBoxVoucherNumber.Text;

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

		public bool IsNewRecord
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
				buttonSelectRequest.Enabled = value;
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

		public string EntityType
		{
			private get
			{
				return _entityType;
			}
			set
			{
				_entityType = value;
			}
		}

		public decimal Amount
		{
			private get
			{
				return _amount;
			}
			set
			{
				_amount = value;
			}
		}

		public string EntityID
		{
			private get
			{
				return _entityID;
			}
			set
			{
				_entityID = value;
			}
		}

		public event EventHandler SelectedItemChanged;

		public OpeningChequePaymentEntryForm()
		{
			InitializeComponent();
			AddEvents();
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
			comboBoxCostCenter.SelectedIndexChanged += comboBoxCostCenter_SelectedIndexChanged;
			payeeSelector1.SelectedItemChanged += payeeSelector1_SelectedItemChanged;
			comboBoxChequebook.SelectedIndexChanged += comboBoxChequebook_SelectedIndexChanged;
			comboBoxPaymentMethod.SelectedIndexChanged += comboBoxPaymentMethod_SelectedIndexChanged;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			base.KeyDown += Form_KeyDown;
			payeeTypeComboBox1.SelectedIndexChanged += payeeTypeComboBox1_SelectedIndexChanged;
		}

		private void payeeTypeComboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			customersFlatComboBox customersFlatComboBox = comboBoxGridCustomer;
			vendorsFlatComboBox vendorsFlatComboBox = comboBoxGridVendor;
			EmployeeComboBox employeeComboBox = comboBoxGridEmployee;
			bool flag2 = comboBoxGridAccount.Visible = false;
			bool flag4 = employeeComboBox.Visible = flag2;
			bool visible = vendorsFlatComboBox.Visible = flag4;
			customersFlatComboBox.Visible = visible;
			ValueList valueList = new ValueList();
			dataGridItems.DisplayLayout.Bands[0].Columns["Code"].ValueList = valueList;
			if (payeeTypeComboBox1.SelectedID == "C")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].ValueList = comboBoxGridCustomer;
			}
			else if (payeeTypeComboBox1.SelectedID == "V")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].ValueList = comboBoxGridVendor;
			}
			else if (payeeTypeComboBox1.SelectedID == "E")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].ValueList = comboBoxGridEmployee;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].ValueList = comboBoxGridAccount;
			}
			if (this.SelectedItemChanged != null)
			{
				this.SelectedItemChanged(this, null);
			}
		}

		private void comboBoxChequebook_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "Code")
			{
				if (payeeTypeComboBox1.SelectedID != "")
				{
					if (payeeTypeComboBox1.SelectedID == "C" && ShowCustomerBal)
					{
						dataGridItems.ActiveRow.Cells["Name"].Value = comboBoxGridCustomer.SelectedName;
						DataSet customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(comboBoxGridCustomer.SelectedID);
						decimal d = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["Balance"].ToString());
						decimal d2 = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["PDCAmount"].ToString());
						decimal num = decimal.Parse((d - d2).ToString(Format.TotalAmountFormat));
						dataGridItems.ActiveRow.Cells["Current Bal"].Value = num;
					}
					else if (payeeTypeComboBox1.SelectedID == "V" && ShowSupplierBal)
					{
						dataGridItems.ActiveRow.Cells["Name"].Value = comboBoxGridVendor.SelectedName;
						DataSet vendorBalanceAmount = Factory.VendorSystem.GetVendorBalanceAmount(comboBoxGridVendor.SelectedID);
						decimal d3 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["Balance"].ToString());
						decimal d4 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["PDCAmount"].ToString());
						decimal result = default(decimal);
						decimal.TryParse((d3 - d4).ToString(), out result);
						dataGridItems.ActiveRow.Cells["Current Bal"].Value = Math.Round(result, Global.CurDecimalPoints);
						decimal num2 = default(decimal);
						vendorBalanceAmount = Factory.VendorSystem.GetVendorDueBalanceSummary(comboBoxGridVendor.SelectedID, currencySelector.SelectedID, dateTimePickerDate.Value);
						if (vendorBalanceAmount.Tables[0].Rows.Count > 0)
						{
							num2 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["BalanceDue"].ToString());
							dataGridItems.ActiveRow.Cells["Current Due"].Value = decimal.Parse(num2.ToString(Format.TotalAmountFormat));
						}
					}
					else if (payeeTypeComboBox1.SelectedID == "E" && ShowEmployeeBal)
					{
						dataGridItems.ActiveRow.Cells["Name"].Value = comboBoxGridEmployee.SelectedName;
						decimal num3 = decimal.Parse(Factory.EmployeeSystem.GetEmployeeSnapBalance(comboBoxGridEmployee.SelectedID).Tables[0].Rows[0]["Balance"].ToString());
						dataGridItems.ActiveRow.Cells["Current Bal"].Value = decimal.Parse(num3.ToString(Format.TotalAmountFormat));
					}
					else if (payeeTypeComboBox1.SelectedID == "A" && ShowAccountBal)
					{
						dataGridItems.ActiveRow.Cells["Name"].Value = comboBoxGridAccount.SelectedName;
						decimal num4 = decimal.Parse(Factory.JournalSystem.GetAccountSnapBalance(comboBoxGridAccount.SelectedID).Tables[0].Rows[0]["Balance"].ToString());
						dataGridItems.ActiveRow.Cells["Current Bal"].Value = decimal.Parse(num4.ToString(Format.TotalAmountFormat));
					}
					else
					{
						dataGridItems.ActiveRow.Cells["Current Bal"].Value = 0.0;
					}
				}
				else
				{
					dataGridItems.ActiveRow.Cells["Current Bal"].Value = 0.0;
				}
			}
			if (!(e.Cell.Column.Key == "Chequebook"))
			{
				return;
			}
			checked
			{
				if (dataGridItems.ActiveRow.Index > 0 && e.Cell.Value != null && e.Cell.Value.ToString() != "")
				{
					int result2 = -1;
					for (int num5 = dataGridItems.ActiveRow.Index - 1; num5 >= 0; num5--)
					{
						if (dataGridItems.Rows[num5].Cells["Chequebook"].Value.ToString() == comboBoxChequebook.SelectedID)
						{
							int.TryParse(dataGridItems.Rows[num5].Cells["Chq Number"].Value.ToString(), out result2);
							break;
						}
					}
					if (result2 > 0)
					{
						dataGridItems.ActiveRow.Cells["Chq Number"].Value = result2 + 1;
					}
					else
					{
						try
						{
							dataGridItems.ActiveRow.Cells["Chq Number"].Value = Factory.ChequebookSystem.GetNextChequeNumber(comboBoxChequebook.SelectedID).ToString();
						}
						catch
						{
						}
					}
				}
				else
				{
					try
					{
						dataGridItems.ActiveRow.Cells["Chq Number"].Value = Factory.ChequebookSystem.GetNextChequeNumber(comboBoxChequebook.SelectedID).ToString();
					}
					catch
					{
					}
				}
				dataGridItems.ActiveRow.Cells["Description"].Value = textBoxNote.Text;
			}
		}

		private void dataGridItems_BeforeCellListDropDown(object sender, CancelableCellEventArgs e)
		{
			if (e.Cell.Column.ValueList != null && e.Cell.Column.ValueList.GetType().BaseType == typeof(MultiColumnComboBox))
			{
				MultiColumnComboBox multiColumnComboBox = e.Cell.Column.ValueList as MultiColumnComboBox;
				if (e.Cell.Value.ToString() == "")
				{
					multiColumnComboBox.Clear();
				}
				else
				{
					multiColumnComboBox.SelectedID = e.Cell.Value.ToString();
				}
			}
		}

		private void comboBoxPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxPaymentMethod.SelectedPaymentType == PaymentMethodTypes.Check)
			{
				dataGridItems.ActiveRow.Cells["Chq Date"].Activation = Activation.AllowEdit;
			}
			else
			{
				dataGridItems.ActiveRow.Cells["Chq Date"].Activation = Activation.Disabled;
			}
		}

		private void payeeSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
			textBoxPayeeName.Text = payeeSelector1.SelectedName;
			if (payeeSelector1.SelectedType == "A")
			{
				comboBoxAnalysis.FilterByAccount(payeeSelector1.SelectedID);
				labelAnalysis.Visible = true;
				comboBoxAnalysis.Visible = true;
			}
			else
			{
				comboBoxAnalysis.Clear();
				comboBoxAnalysis.FilterByAccount("$#$$LDF");
				labelAnalysis.Visible = false;
				comboBoxAnalysis.Visible = false;
			}
			if (payeeSelector1.SelectedID != "")
			{
				if (payeeSelector1.SelectedType == "C" && ShowCustomerBal)
				{
					DataSet customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(payeeSelector1.SelectedID);
					decimal d = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["Balance"].ToString());
					decimal d2 = decimal.Parse(customerSnapBalance.Tables[0].Rows[0]["PDCAmount"].ToString());
					textBoxBalance.Text = (d - d2).ToString(Format.TotalAmountFormat);
				}
				else if (payeeSelector1.SelectedType == "V" && ShowSupplierBal)
				{
					DataSet vendorBalanceAmount = Factory.VendorSystem.GetVendorBalanceAmount(payeeSelector1.SelectedID);
					decimal d3 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["Balance"].ToString());
					decimal d4 = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["PDCAmount"].ToString());
					textBoxBalance.Text = (d3 - d4).ToString(Format.TotalAmountFormat);
					decimal num = default(decimal);
					vendorBalanceAmount = Factory.VendorSystem.GetVendorDueBalanceSummary(payeeSelector1.SelectedID, currencySelector.SelectedID, dateTimePickerDate.Value);
					if (vendorBalanceAmount.Tables[0].Rows.Count > 0)
					{
						num = decimal.Parse(vendorBalanceAmount.Tables[0].Rows[0]["BalanceDue"].ToString());
						textBoxTotalDue.Text = num.ToString(Format.TotalAmountFormat);
					}
				}
				else if (payeeSelector1.SelectedType == "E" && ShowEmployeeBal)
				{
					decimal num2 = decimal.Parse(Factory.EmployeeSystem.GetEmployeeSnapBalance(payeeSelector1.SelectedID).Tables[0].Rows[0]["Balance"].ToString());
					textBoxBalance.Text = num2.ToString(Format.TotalAmountFormat);
				}
				else if (payeeSelector1.SelectedType == "A" && ShowAccountBal)
				{
					decimal num3 = decimal.Parse(Factory.JournalSystem.GetAccountSnapBalance(payeeSelector1.SelectedID).Tables[0].Rows[0]["Balance"].ToString());
					textBoxBalance.Text = num3.ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxBalance.Text = "";
				}
			}
			else
			{
				textBoxBalance.Text = "";
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

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
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
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key == "Amount" && dataGridItems.ActiveCell.Text != "")
				{
					dataGridItems.ActiveCell.Value = decimal.Round(decimal.Parse(dataGridItems.ActiveCell.Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
				}
				if (dataGridItems.ActiveCell.Column.Key == "Chq Number" && dataGridItems.ActiveCell.Text.Length < 6)
				{
					dataGridItems.ActiveCell.Value = dataGridItems.ActiveCell.Text.PadLeft(6, '0');
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Amount"].Value.ToString() == "")
				{
					activeRow.Cells["Amount"].Value = 0;
				}
				labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Amount")
			{
				labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
			}
			if ((e.Cell.Column.Key == "Chequebook" || e.Cell.Column.Key == "Chq Number") && dataGridItems.ActiveRow != null && dataGridItems.Rows.Count > 1)
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

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Chequebook")
			{
				e.RaiseErrorEvent = false;
				comboBoxChequebook.Text = dataGridItems.ActiveCell.Text;
				comboBoxChequebook.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
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
					currentData = new OpenEntryTransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TransactionTable.Rows[0] : currentData.TransactionTable.NewRow();
				dataRow["TransactionID"] = 0;
				dataRow["SysDocType"] = (byte)235;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["CostCenterID"] = comboBoxCostCenter.SelectedID;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				if (dateTimePickerDeliveredDate.Checked)
				{
					dataRow["CheckDeliveredDate"] = dateTimePickerDeliveredDate.Value;
				}
				else
				{
					dataRow["CheckDeliveredDate"] = DBNull.Value;
				}
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
				string text = (string)(dataRow["PayeeType"] = payeeSelector1.SelectedType);
				dataRow["PayeeID"] = payeeSelector1.SelectedID;
				if (currencySelector.SelectedID != "" && currencySelector.SelectedID != Global.BaseCurrencyID)
				{
					dataRow["CurrencyID"] = currencySelector.SelectedID;
					dataRow["CurrencyRate"] = currencySelector.Rate;
					dataRow["AmountFC"] = GetTransactionBalance();
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = 1;
					dataRow["Amount"] = GetTransactionBalance();
				}
				if (comboBoxAnalysis.SelectedID != null && comboBoxAnalysis.SelectedID.ToString() != "")
				{
					dataRow["AnalysisID"] = comboBoxAnalysis.SelectedID.ToString();
				}
				else
				{
					dataRow["AnalysisID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.TransactionTable.Rows.Add(dataRow);
				}
				currentData.TransactionDetailsTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.TransactionDetailsTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["PaymentMethodType"] = (byte)2;
					dataRow2["ChequebookID"] = row.Cells["Chequebook"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					if (currencySelector.SelectedID == "" || currencySelector.IsBaseCurrency)
					{
						if (row.Cells["Amount"].Value.ToString() != "")
						{
							dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
						}
						else
						{
							dataRow2["Amount"] = DBNull.Value;
						}
						dataRow2["AmountFC"] = DBNull.Value;
					}
					else if (row.Cells["Amount"].Value.ToString() != "")
					{
						dataRow2["AmountFC"] = row.Cells["Amount"].Value.ToString();
					}
					else
					{
						dataRow2["AmountFC"] = DBNull.Value;
					}
					if (row.Cells["Chq Date"].Text != "")
					{
						dataRow2["CheckDate"] = row.Cells["Chq Date"].Value.ToString();
					}
					dataRow2["CheckNumber"] = row.Cells["Chq Number"].Value.ToString();
					if (row.Cells["Job"].Value != null && row.Cells["Job"].Value.ToString() != "")
					{
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
					}
					else
					{
						dataRow2["JobID"] = DBNull.Value;
					}
					if (row.Cells["CostCategory"].Value != null && row.Cells["CostCategory"].Value.ToString() != "")
					{
						dataRow2["CostCategoryID"] = row.Cells["CostCategory"].Value.ToString();
					}
					else
					{
						dataRow2["CostCategoryID"] = DBNull.Value;
					}
					if (row.Cells["Consignment"].Value != null && row.Cells["Consignment"].Value.ToString() != "")
					{
						dataRow2["ConsignID"] = row.Cells["Consignment"].Value.ToString();
					}
					else
					{
						dataRow2["ConsignID"] = DBNull.Value;
					}
					if (row.Cells["ConsignExpenseID"].Value != null && row.Cells["ConsignExpenseID"].Value.ToString() != "")
					{
						dataRow2["ConsignExpenseID"] = row.Cells["ConsignExpenseID"].Value.ToString();
					}
					else
					{
						dataRow2["ConsignExpenseID"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.TransactionDetailsTable.Rows.Add(dataRow2);
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
				dataTable.Columns.Add("Code");
				dataTable.Columns.Add("Name");
				dataTable.Columns.Add("Current Bal", typeof(decimal));
				dataTable.Columns.Add("Current Due", typeof(decimal));
				dataTable.Columns.Add("Chequebook");
				dataTable.Columns.Add("Chq Number");
				dataTable.Columns.Add("Chq Date");
				dataTable.Columns.Add("Consignment");
				dataTable.Columns.Add("ConsignExpenseID");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Date"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Number"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].ValueList = comboBoxChequebook;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Date"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Header.Caption = "Project";
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Header.Caption = "Cost Category";
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = comboBoxCostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].MaxLength = 22;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].ValueList = comboBoxConsignIn;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].Header.Caption = "Expense Category";
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].ValueList = comboBoxConsignExpense;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Job"];
				bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				ultraGridColumn.Hidden = hidden;
				if (!useJobCosting)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].Hidden = !trackConsignInExpense);
				ultraGridColumn2.Hidden = hidden;
				if (!trackConsignInExpense)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Consignment"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["ConsignExpenseID"].Hidden = true);
				ultraGridColumn3.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.TotalAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Code"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Name"].Width = checked(14 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Bal"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Due"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Number"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Date"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Bal"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Current Due"].CellActivation = Activation.NoEdit;
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
					currentData = Factory.OpeningEntryTransactionSystem.GetTransactionByID(SystemDocID, journalID);
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
					DataRow dataRow = currentData.Tables["GL_Transaction"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					comboBoxCostCenter.SelectedID = dataRow["CostCenterID"].ToString();
					textBoxNote.Text = dataRow["Description"].ToString();
					payeeSelector1.SelectedType = dataRow["PayeeType"].ToString();
					payeeSelector1.SelectedID = dataRow["PayeeID"].ToString();
					comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
					requestSysDocID = dataRow["RequestSysDocID"].ToString();
					textBoxRequest.Text = dataRow["RequestVoucherID"].ToString();
					if (dataRow["CheckDeliveredDate"] != DBNull.Value)
					{
						dateTimePickerDeliveredDate.Value = DateTime.Parse(dataRow["CheckDeliveredDate"].ToString());
						dateTimePickerDeliveredDate.Checked = true;
					}
					else
					{
						dateTimePickerDeliveredDate.Clear();
					}
					if (textBoxRequest.Text != "")
					{
						payeeSelector1.Enabled = false;
						comboBoxChequebook.Enabled = false;
					}
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
					if (dataRow["AnalysisID"] != DBNull.Value)
					{
						comboBoxAnalysis.SelectedID = dataRow["AnalysisID"].ToString();
					}
					else
					{
						comboBoxAnalysis.Clear();
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Transaction_Details") && currentData.TransactionDetailsTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Transaction_Details"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Chequebook"] = row["ChequebookID"];
							dataRow3["Chq Number"] = row["CheckNumber"];
							dataRow3["Chq Date"] = row["CheckDate"];
							if (row["AmountFC"] != DBNull.Value)
							{
								dataRow3["Amount"] = Math.Round(decimal.Parse(row["AmountFC"].ToString()), Global.CurDecimalPoints);
							}
							else if (row["Amount"] != DBNull.Value)
							{
								dataRow3["Amount"] = Math.Round(decimal.Parse(row["Amount"].ToString()), Global.CurDecimalPoints);
							}
							else
							{
								dataRow3["Amount"] = DBNull.Value;
							}
							if (row["JobID"] != DBNull.Value)
							{
								dataRow3["Job"] = row["JobID"];
							}
							if (row["CostCategoryID"] != DBNull.Value)
							{
								dataRow3["CostCategory"] = row["CostCategoryID"];
							}
							if (row["ConsignID"] != DBNull.Value)
							{
								dataRow3["Consignment"] = row["ConsignID"];
							}
							if (row["ConsignExpenseID"] != DBNull.Value)
							{
								dataRow3["ConsignExpenseID"] = row["ConsignExpenseID"];
							}
							dataRow3["Description"] = row["Description"].ToString();
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							if (row2.Cells["Chq Date"].Value == DBNull.Value)
							{
								row2.Cells["Chq Date"].Activation = Activation.Disabled;
							}
							else
							{
								row2.Cells["Chq Date"].Activation = Activation.AllowEdit;
							}
						}
						if (currentData.Tables["Cheque_Issued"].Select("Status>2").Length != 0)
						{
							foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
							{
								column.CellActivation = Activation.NoEdit;
							}
							dataGridItems.ShowDeleteMenu = false;
							dataGridItems.AllowAddNew = false;
							foreach (UltraGridRow row3 in dataGridItems.Rows)
							{
								row3.Appearance.BackColor = Color.WhiteSmoke;
							}
							payeeSelector1.Enabled = false;
						}
						else
						{
							foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
							{
								column2.CellActivation = Activation.AllowEdit;
							}
							dataGridItems.ShowDeleteMenu = true;
							dataGridItems.AllowAddNew = true;
							if (textBoxRequest.Text == "")
							{
								payeeSelector1.Enabled = true;
							}
						}
						labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
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
				bool flag = (!isNewRecord) ? Factory.OpeningEntryTransactionSystem.UpdateTransaction(currentData) : Factory.OpeningEntryTransactionSystem.CreateTransaction(currentData);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					if (showAllocationForm && payeeSelector1.SelectedType == "V")
					{
						CustomerPaymentAllocationForm customerPaymentAllocationForm = new CustomerPaymentAllocationForm();
						customerPaymentAllocationForm.IsARPayment = false;
						customerPaymentAllocationForm.SetData(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, payeeSelector1.SelectedID, -1, isPDC: false);
						customerPaymentAllocationForm.ShowDialog();
					}
					bool result = false;
					bool.TryParse(comboBoxSysDoc.GetSelectedCellValue("PrintAfterSave").ToString(), out result);
					if (result)
					{
						Print(isPrint: true, showPrintDialog: true, saveChanges: false);
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
				if (ex.Number == 2006)
				{
					ErrorHelper.WarningMessage(ex.Message);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("GL_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
			if (comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "" || payeeSelector1.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (isCostCenterMandatory && comboBoxCostCenter.SelectedID == string.Empty)
			{
				ErrorHelper.InformationMessage("Please select cost center");
				return false;
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!dataGridItems.HasRowAnyValue(row))
				{
					row.Delete(displayPrompt: false);
				}
				else if (row.Cells["Chq Date"].Activation == Activation.AllowEdit)
				{
					if (row.Cells["Chequebook"].Value.ToString().Trim() == "")
					{
						ErrorHelper.InformationMessage(UIMessages.SelectAChequebook);
						row.Activate();
						return false;
					}
					if (row.Cells["Chq Number"].Value.ToString().Trim() == "")
					{
						ErrorHelper.InformationMessage(UIMessages.EnterChequeNumber);
						row.Activate();
						return false;
					}
					int result = 0;
					if (!int.TryParse(row.Cells["Chq Number"].Value.ToString(), out result))
					{
						ErrorHelper.InformationMessage("Cheque numbers must be numeric value. Please enter a numeric value for the cheque numbers.");
						row.Activate();
						return false;
					}
					if (row.Cells["Chq Date"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage(UIMessages.EnterChequeDate);
						row.Activate();
						return false;
					}
					if (row.Cells["Amount"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage(UIMessages.EnterChequeAmount);
						row.Activate();
						return false;
					}
				}
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterAtLeastOneRowOfPayment);
				return false;
			}
			if (GetTransactionBalance() == 0m && ErrorHelper.QuestionMessageYesNo(UIMessages.TransactionWithZeroAmount) == DialogResult.No)
			{
				return false;
			}
			try
			{
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					int num = (!isNewRecord) ? Factory.IssuedChequeSystem.ValidateBlankCheque(row2.Cells["Chequebook"].Value.ToString(), row2.Cells["Chq Number"].Value.ToString(), comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text) : Factory.IssuedChequeSystem.ValidateBlankCheque(row2.Cells["Chequebook"].Value.ToString(), row2.Cells["Chq Number"].Value.ToString(), "", "");
					if (num < 0)
					{
						switch (num)
						{
						case -1:
							ErrorHelper.InformationMessage(UIMessages.ChequeNotRegistered);
							row2.Activate();
							return false;
						case -2:
							ErrorHelper.InformationMessage(UIMessages.ChequeInUseNotBlank);
							row2.Activate();
							return false;
						default:
							ErrorHelper.InformationMessage(UIMessages.InvalidChequeNumber);
							row2.Activate();
							return false;
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("GL_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxAnalysis.Clear();
				payeeSelector1.Clear();
				textBoxPayeeName.Clear();
				labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				labelBalance.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxRequest.Clear();
				requestSysDocID = "";
				payeeSelector1.Enabled = true;
				comboBoxChequebook.Enabled = true;
				dateTimePickerDeliveredDate.Clear();
				dataGridItems.AllowAddNew = true;
				if (payeeSelector1.SelectedType == "")
				{
					payeeSelector1.SelectedType = "V";
				}
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				(base.ParentForm as IWorkFlowForm)?.SetApprovalStatus();
				EventHelper.OnFormCleared(this, null);
				IsVoid = false;
				formManager.ResetDirty();
				dateTimePickerDate.Focus();
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
				return Factory.OpeningEntryTransactionSystem.DeleteTransaction(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
		}

		private void GoLast()
		{
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void GoFirst()
		{
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
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

		public bool CanClose()
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
				dataGridItems.SetupUI();
				SetupGrid();
				dataGridItems.ApplyFormat();
				UltraFormattedLinkLabel ultraFormattedLinkLabel = labelCurrency;
				bool visible = currencySelector.Visible = CompanyPreferences.UseMultiCurrency;
				ultraFormattedLinkLabel.Visible = visible;
				SetSecurity();
				if (!base.IsDisposed)
				{
					dateTimePickerDate.Value = DateTime.Now;
					IsNewRecord = true;
					comboBoxSysDoc.FilterByType(SysDocTypes.OpeningChequePayment);
					LoadDataIfExist();
					ClearForm();
				}
			}
			catch (Exception e2)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadDataIfExist()
		{
			payeeSelector1.SelectedType = EntityType;
			payeeSelector1.SelectedID = EntityID;
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessSupplierBal))
			{
				ShowSupplierBal = false;
			}
			else
			{
				ShowSupplierBal = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCustomerBal))
			{
				ShowCustomerBal = false;
			}
			else
			{
				ShowCustomerBal = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessAccountBal))
			{
				ShowAccountBal = false;
			}
			else
			{
				ShowAccountBal = true;
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessEmployeeBal))
			{
				ShowEmployeeBal = false;
			}
			else
			{
				ShowEmployeeBal = true;
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
				return Factory.OpeningEntryTransactionSystem.VoidTransaction(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			string selectedType = payeeSelector1.SelectedType;
			if (!(selectedType == ""))
			{
				FormHelper formHelper = new FormHelper();
				if (selectedType == "A")
				{
					formHelper.EditAccount(payeeSelector1.SelectedID);
				}
				else if (selectedType == "C")
				{
					formHelper.EditCustomer(payeeSelector1.SelectedID);
				}
				else if (selectedType == "V")
				{
					formHelper.EditVendor(payeeSelector1.SelectedID);
				}
				else if (selectedType == "E")
				{
					formHelper.EditEmployee(payeeSelector1.SelectedID);
				}
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCenter(comboBoxCostCenter.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.OpeningChequePayment);
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		public void Print()
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string systemDocID = SystemDocID;
					string text = textBoxVoucherNumber.Text;
					DataSet transactionToPrint = Factory.OpeningEntryTransactionSystem.GetTransactionToPrint(systemDocID, text);
					if (transactionToPrint == null || transactionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(transactionToPrint, systemDocID, "Cheque Payment", SysDocTypes.OpeningChequePayment, isPrint, showPrintDialog);
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
			Preview();
		}

		public void Preview()
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

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ChequePaymentOpeningVoucherListObj);
		}

		public void SetApprovalStatus()
		{
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			formManager.ShowApprovalPanel(approvalTaskID, "GL_Transaction", "VoucherID");
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
			}
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("GL_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonAttach_Click_1(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntityID = comboBoxSysDoc.SelectedID + textBoxVoucherNumber.Text.Trim();
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ChequePaymentOpeningVoucherListObj);
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			dataGridItems.ImportFromExcel(autoFill: true);
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
				DataSet openPaymentRequests = Factory.PaymentRequestSystem.GetOpenPaymentRequests(1);
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
						payeeSelector1.SelectedType = "V";
						payeeSelector1.SelectedID = dataRow["PayeeID"].ToString();
						comboBoxChequebook.SelectedID = dataRow["PayFromID"].ToString();
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						payeeSelector1.Enabled = false;
						comboBoxChequebook.Enabled = false;
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

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Chq Number"].Value != null && dataGridItems.ActiveRow.Cells["Chq Number"].Value.ToString() != "")
			{
				string text = "";
				string text2 = "";
				int index = dataGridItems.ActiveRow.Index;
				text = currentData.Tables["Cheque_Issued"].Rows[index]["ClearanceSysDocID"].ToString();
				text2 = currentData.Tables["Cheque_Issued"].Rows[index]["ClearanceVoucherID"].ToString();
				if (text != "" && text2 != "")
				{
					new FormHelper().EditTransaction(TransactionListType.ChequeClearance, text, text2);
				}
				else
				{
					ErrorHelper.InformationMessage("This Cheque have to be Cleared!.");
				}
			}
		}

		public void DuplicateData()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.OpeningChequePaymentEntryForm));
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
			Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance153 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance154 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance155 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance156 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance157 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance158 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance159 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance160 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance161 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance162 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance163 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance164 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance165 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance166 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance167 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance168 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance169 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance170 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance171 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance172 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance173 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance174 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance175 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance176 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance177 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance178 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance179 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance180 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance181 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance182 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance183 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance184 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance185 = new Infragistics.Win.Appearance();
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
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelBalance = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxPayeeType = new System.Windows.Forms.TextBox();
			dateTimePickerDeliveredDate = new Micromind.UISupport.MMSDateTimePicker(components);
			payeeTypeComboBox1 = new Micromind.DataControls.PayeeTypeComboBox();
			label2 = new System.Windows.Forms.Label();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			textBoxTotalDue = new Micromind.UISupport.AmountTextBox();
			label5 = new System.Windows.Forms.Label();
			buttonSelectRequest = new Micromind.UISupport.XPButton();
			textBoxBalance = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxRequest = new System.Windows.Forms.TextBox();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			labelAnalysis = new System.Windows.Forms.Label();
			textBoxPayeeName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			payeeSelector1 = new Micromind.DataControls.PayeeSelector();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			labelVoided = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxGridVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxGridCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxGridAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxPaymentMethod = new Micromind.DataControls.paymentMethodsComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxChequebook = new Micromind.DataControls.ChequebookComboBox();
			comboBoxConsignExpense = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxConsignIn = new Micromind.DataControls.ConsignInComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)payeeTypeComboBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignIn).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 447);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(675, 40);
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
			linePanelDown.Size = new System.Drawing.Size(675, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(565, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 4;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Visible = false;
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(312, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(123, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 72);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(90, 69);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(111, 20);
			textBoxRef1.TabIndex = 9;
			textBoxNote.Location = new System.Drawing.Point(90, 91);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(425, 20);
			textBoxNote.TabIndex = 11;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 94);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(206, 28);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.Size = new System.Drawing.Size(52, 14);
			labelCurrency.TabIndex = 4;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance2;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance3.FontData.BoldAsString = "True";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance3;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(206, 5);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 110;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance5.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance5.FontData.BoldAsString = "True";
			appearance5.FontData.Name = "Tahoma";
			appearance5.TextHAlignAsString = "Right";
			appearance5.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance5;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(506, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Total:";
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance6.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance6.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance6;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelBalance);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 412);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(651, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelBalance.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			appearance7.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance7.FontData.BoldAsString = "True";
			appearance7.FontData.Name = "Tahoma";
			appearance7.TextHAlignAsString = "Right";
			appearance7.TextVAlignAsString = "Middle";
			labelBalance.Appearance = appearance7;
			labelBalance.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			labelBalance.Location = new System.Drawing.Point(508, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(textBoxPayeeType);
			panelDetails.Controls.Add(dateTimePickerDeliveredDate);
			panelDetails.Controls.Add(payeeTypeComboBox1);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(textBoxTotalDue);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(buttonSelectRequest);
			panelDetails.Controls.Add(textBoxBalance);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(textBoxRequest);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(labelAnalysis);
			panelDetails.Controls.Add(textBoxPayeeName);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCostCenter);
			panelDetails.Controls.Add(payeeSelector1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(currencySelector);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(comboBoxAnalysis);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Location = new System.Drawing.Point(0, 27);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(675, 115);
			panelDetails.TabIndex = 0;
			appearance8.FontData.BoldAsString = "True";
			appearance8.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance8;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(11, 49);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(71, 15);
			ultraFormattedLinkLabel3.TabIndex = 162;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Payee Type:";
			appearance9.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance9;
			textBoxPayeeType.Location = new System.Drawing.Point(145, 47);
			textBoxPayeeType.MaxLength = 15;
			textBoxPayeeType.Name = "textBoxPayeeType";
			textBoxPayeeType.ReadOnly = true;
			textBoxPayeeType.Size = new System.Drawing.Size(238, 20);
			textBoxPayeeType.TabIndex = 138;
			dateTimePickerDeliveredDate.Checked = false;
			dateTimePickerDeliveredDate.CustomFormat = " ";
			dateTimePickerDeliveredDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDeliveredDate.Location = new System.Drawing.Point(567, 91);
			dateTimePickerDeliveredDate.Name = "dateTimePickerDeliveredDate";
			dateTimePickerDeliveredDate.ShowCheckBox = true;
			dateTimePickerDeliveredDate.Size = new System.Drawing.Size(94, 20);
			dateTimePickerDeliveredDate.TabIndex = 161;
			dateTimePickerDeliveredDate.Value = new System.DateTime(0L);
			payeeTypeComboBox1.Assigned = false;
			payeeTypeComboBox1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			payeeTypeComboBox1.CustomReportFieldName = "";
			payeeTypeComboBox1.CustomReportKey = "";
			payeeTypeComboBox1.CustomReportValueType = 1;
			payeeTypeComboBox1.DescriptionTextBox = textBoxPayeeType;
			appearance10.BackColor = System.Drawing.SystemColors.Window;
			appearance10.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			payeeTypeComboBox1.DisplayLayout.Appearance = appearance10;
			payeeTypeComboBox1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			payeeTypeComboBox1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance11.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.Appearance = appearance11;
			appearance12.ForeColor = System.Drawing.SystemColors.GrayText;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance12;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance13.BackColor2 = System.Drawing.SystemColors.Control;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.ForeColor = System.Drawing.SystemColors.GrayText;
			payeeTypeComboBox1.DisplayLayout.GroupByBox.PromptAppearance = appearance13;
			payeeTypeComboBox1.DisplayLayout.MaxColScrollRegions = 1;
			payeeTypeComboBox1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			payeeTypeComboBox1.DisplayLayout.Override.ActiveCellAppearance = appearance14;
			appearance15.BackColor = System.Drawing.SystemColors.Highlight;
			appearance15.ForeColor = System.Drawing.SystemColors.HighlightText;
			payeeTypeComboBox1.DisplayLayout.Override.ActiveRowAppearance = appearance15;
			payeeTypeComboBox1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			payeeTypeComboBox1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance16.BackColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.Override.CardAreaAppearance = appearance16;
			appearance17.BorderColor = System.Drawing.Color.Silver;
			appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			payeeTypeComboBox1.DisplayLayout.Override.CellAppearance = appearance17;
			payeeTypeComboBox1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			payeeTypeComboBox1.DisplayLayout.Override.CellPadding = 0;
			appearance18.BackColor = System.Drawing.SystemColors.Control;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			payeeTypeComboBox1.DisplayLayout.Override.GroupByRowAppearance = appearance18;
			appearance19.TextHAlignAsString = "Left";
			payeeTypeComboBox1.DisplayLayout.Override.HeaderAppearance = appearance19;
			payeeTypeComboBox1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			payeeTypeComboBox1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance20.BackColor = System.Drawing.SystemColors.Window;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			payeeTypeComboBox1.DisplayLayout.Override.RowAppearance = appearance20;
			payeeTypeComboBox1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance21.BackColor = System.Drawing.SystemColors.ControlLight;
			payeeTypeComboBox1.DisplayLayout.Override.TemplateAddRowAppearance = appearance21;
			payeeTypeComboBox1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			payeeTypeComboBox1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			payeeTypeComboBox1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			payeeTypeComboBox1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			payeeTypeComboBox1.Editable = true;
			payeeTypeComboBox1.FilterString = "";
			payeeTypeComboBox1.HasAllAccount = false;
			payeeTypeComboBox1.HasCustom = false;
			payeeTypeComboBox1.IsDataLoaded = false;
			payeeTypeComboBox1.Location = new System.Drawing.Point(90, 47);
			payeeTypeComboBox1.MaxDropDownItems = 12;
			payeeTypeComboBox1.MaximumSize = new System.Drawing.Size(51, 20);
			payeeTypeComboBox1.MinimumSize = new System.Drawing.Size(51, 20);
			payeeTypeComboBox1.Name = "payeeTypeComboBox1";
			payeeTypeComboBox1.ShowInactiveItems = false;
			payeeTypeComboBox1.ShowQuickAdd = true;
			payeeTypeComboBox1.Size = new System.Drawing.Size(51, 20);
			payeeTypeComboBox1.TabIndex = 137;
			payeeTypeComboBox1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(519, 88);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(41, 26);
			label2.TabIndex = 160;
			label2.Text = "Delvrd \r\nDate:";
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(517, 71);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(49, 13);
			mmLabel4.TabIndex = 159;
			mmLabel4.Text = "Cur Due:";
			mmLabel4.Visible = false;
			textBoxTotalDue.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTotalDue.CustomReportFieldName = "";
			textBoxTotalDue.CustomReportKey = "";
			textBoxTotalDue.CustomReportValueType = 1;
			textBoxTotalDue.IsComboTextBox = false;
			textBoxTotalDue.Location = new System.Drawing.Point(567, 69);
			textBoxTotalDue.MaxLength = 15;
			textBoxTotalDue.Name = "textBoxTotalDue";
			textBoxTotalDue.ReadOnly = true;
			textBoxTotalDue.Size = new System.Drawing.Size(95, 20);
			textBoxTotalDue.TabIndex = 158;
			textBoxTotalDue.Text = "0.00";
			textBoxTotalDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotalDue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			textBoxTotalDue.Visible = false;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(377, 73);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(44, 13);
			label5.TabIndex = 136;
			label5.Text = "Cur Bal:";
			label5.Visible = false;
			buttonSelectRequest.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectRequest.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectRequest.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectRequest.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectRequest.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectRequest.Location = new System.Drawing.Point(636, 2);
			buttonSelectRequest.Name = "buttonSelectRequest";
			buttonSelectRequest.Size = new System.Drawing.Size(26, 22);
			buttonSelectRequest.TabIndex = 3;
			buttonSelectRequest.Text = "...";
			buttonSelectRequest.UseVisualStyleBackColor = false;
			buttonSelectRequest.Visible = false;
			buttonSelectRequest.Click += new System.EventHandler(buttonSelectRequest_Click);
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.Location = new System.Drawing.Point(427, 69);
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(88, 20);
			textBoxBalance.TabIndex = 135;
			textBoxBalance.TabStop = false;
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBalance.Visible = false;
			appearance22.FontData.BoldAsString = "False";
			appearance22.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance22;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(441, 5);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(48, 15);
			ultraFormattedLinkLabel1.TabIndex = 132;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Request:";
			ultraFormattedLinkLabel1.Visible = false;
			appearance23.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance23;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			textBoxRequest.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRequest.Location = new System.Drawing.Point(515, 2);
			textBoxRequest.Name = "textBoxRequest";
			textBoxRequest.ReadOnly = true;
			textBoxRequest.Size = new System.Drawing.Size(119, 20);
			textBoxRequest.TabIndex = 2;
			textBoxRequest.TabStop = false;
			textBoxRequest.Visible = false;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(90, 25);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(111, 20);
			dateTimePickerDate.TabIndex = 4;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 58, 51, 836);
			labelAnalysis.AutoSize = true;
			labelAnalysis.Location = new System.Drawing.Point(205, 72);
			labelAnalysis.Name = "labelAnalysis";
			labelAnalysis.Size = new System.Drawing.Size(48, 13);
			labelAnalysis.TabIndex = 119;
			labelAnalysis.Text = "Analysis:";
			labelAnalysis.Visible = false;
			textBoxPayeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPayeeName.Location = new System.Drawing.Point(575, 47);
			textBoxPayeeName.Name = "textBoxPayeeName";
			textBoxPayeeName.ReadOnly = true;
			textBoxPayeeName.Size = new System.Drawing.Size(87, 20);
			textBoxPayeeName.TabIndex = 8;
			textBoxPayeeName.TabStop = false;
			textBoxPayeeName.Visible = false;
			appearance24.FontData.BoldAsString = "True";
			appearance24.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance24;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(13, 6);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 116;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance25.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance25;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance26.BackColor = System.Drawing.SystemColors.Window;
			appearance26.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance26;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance27.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance27.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance27.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance27;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance28;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance29.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance29.BackColor2 = System.Drawing.SystemColors.Control;
			appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance29.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance29;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			appearance30.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance30;
			appearance31.BackColor = System.Drawing.SystemColors.Highlight;
			appearance31.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance31;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance32.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance32;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			appearance33.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance33;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance34.BackColor = System.Drawing.SystemColors.Control;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance34;
			appearance35.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance35;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance36.BackColor = System.Drawing.SystemColors.Window;
			appearance36.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance36;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance37.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance37;
			comboBoxSysDoc.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSysDoc.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSysDoc.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(90, 3);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(111, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance38.FontData.BoldAsString = "False";
			appearance38.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance38;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(441, 27);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel4.TabIndex = 114;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Cost Center:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCostCenter.AlwaysInEditMode = true;
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance40;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
			comboBoxCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCenter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCenter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCenter.Editable = true;
			comboBoxCostCenter.FilterString = "";
			comboBoxCostCenter.HasAllAccount = false;
			comboBoxCostCenter.HasCustom = false;
			comboBoxCostCenter.IsDataLoaded = false;
			comboBoxCostCenter.Location = new System.Drawing.Point(515, 24);
			comboBoxCostCenter.MaxDropDownItems = 12;
			comboBoxCostCenter.Name = "comboBoxCostCenter";
			comboBoxCostCenter.ShowInactiveItems = false;
			comboBoxCostCenter.ShowQuickAdd = true;
			comboBoxCostCenter.Size = new System.Drawing.Size(147, 20);
			comboBoxCostCenter.TabIndex = 6;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			payeeSelector1.BackColor = System.Drawing.Color.Transparent;
			payeeSelector1.Location = new System.Drawing.Point(481, 47);
			payeeSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			payeeSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			payeeSelector1.Name = "payeeSelector1";
			payeeSelector1.SelectedID = "";
			payeeSelector1.SelectedType = "";
			payeeSelector1.Size = new System.Drawing.Size(88, 20);
			payeeSelector1.TabIndex = 7;
			payeeSelector1.Visible = false;
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(263, 25);
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
			currencySelector.Size = new System.Drawing.Size(172, 20);
			currencySelector.TabIndex = 5;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(12, 28);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			comboBoxAnalysis.AlwaysInEditMode = true;
			comboBoxAnalysis.Assigned = false;
			comboBoxAnalysis.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAnalysis.CustomReportFieldName = "";
			comboBoxAnalysis.CustomReportKey = "";
			comboBoxAnalysis.CustomReportValueType = 1;
			comboBoxAnalysis.DescriptionTextBox = null;
			appearance52.BackColor = System.Drawing.SystemColors.Window;
			appearance52.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance52;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance53.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance53;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance54;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance55.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance55.BackColor2 = System.Drawing.SystemColors.Control;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance55;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance56.BackColor = System.Drawing.SystemColors.Window;
			appearance56.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance56;
			appearance57.BackColor = System.Drawing.SystemColors.Highlight;
			appearance57.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance57;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance58;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			appearance59.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance59;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance60.BackColor = System.Drawing.SystemColors.Control;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance60;
			appearance61.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance61;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance62.BackColor = System.Drawing.SystemColors.Window;
			appearance62.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance62;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance63.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance63;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(254, 69);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(120, 20);
			comboBoxAnalysis.TabIndex = 10;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysis.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(14, 303);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(648, 81);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			ultraFormattedLinkLabel6.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			appearance64.FontData.BoldAsString = "False";
			appearance64.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance64;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(12, 432);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel6.TabIndex = 135;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Clearance Details";
			ultraFormattedLinkLabel6.Visible = false;
			appearance65.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance65;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[18]
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
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator4,
				toolStripButtonAttach,
				toolStripSeparator6,
				toolStripButtonExcelImport,
				toolStripSeparator5,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(20, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(655, 25);
			toolStrip1.TabIndex = 136;
			toolStrip1.Text = "toolStrip1";
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First Record";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous Record";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next Record";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last Record";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(23, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(50, 22);
			toolStripButtonFind.Text = "Find";
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(83, 22);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click_1);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(23, 22);
			toolStripButtonInformation.Text = "Document Information";
			comboBoxGridEmployee.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance66;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance67;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance68;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance69.BackColor2 = System.Drawing.SystemColors.Control;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance69;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance70;
			appearance71.BackColor = System.Drawing.SystemColors.Highlight;
			appearance71.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance71;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance72;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			appearance73.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance73;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance74.BackColor = System.Drawing.SystemColors.Control;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance74;
			appearance75.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance75;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance76;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance77;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(283, 214);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(122, 20);
			comboBoxGridEmployee.TabIndex = 140;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxGridVendor.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxGridVendor.Assigned = false;
			comboBoxGridVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridVendor.CustomReportFieldName = "";
			comboBoxGridVendor.CustomReportKey = "";
			comboBoxGridVendor.CustomReportValueType = 1;
			comboBoxGridVendor.DescriptionTextBox = null;
			appearance78.BackColor = System.Drawing.SystemColors.Window;
			appearance78.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridVendor.DisplayLayout.Appearance = appearance78;
			comboBoxGridVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance79.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance79.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance79.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance79.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.GroupByBox.Appearance = appearance79;
			appearance80.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance80;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance81.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance81.BackColor2 = System.Drawing.SystemColors.Control;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance81;
			comboBoxGridVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveCellAppearance = appearance82;
			appearance83.BackColor = System.Drawing.SystemColors.Highlight;
			appearance83.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveRowAppearance = appearance83;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance84.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.CardAreaAppearance = appearance84;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			appearance85.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridVendor.DisplayLayout.Override.CellAppearance = appearance85;
			comboBoxGridVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridVendor.DisplayLayout.Override.CellPadding = 0;
			appearance86.BackColor = System.Drawing.SystemColors.Control;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.GroupByRowAppearance = appearance86;
			appearance87.TextHAlignAsString = "Left";
			comboBoxGridVendor.DisplayLayout.Override.HeaderAppearance = appearance87;
			comboBoxGridVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridVendor.DisplayLayout.Override.RowAppearance = appearance88;
			comboBoxGridVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance89.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance89;
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
			comboBoxGridVendor.Location = new System.Drawing.Point(411, 214);
			comboBoxGridVendor.MaxDropDownItems = 12;
			comboBoxGridVendor.Name = "comboBoxGridVendor";
			comboBoxGridVendor.ShowConsignmentOnly = false;
			comboBoxGridVendor.ShowQuickAdd = true;
			comboBoxGridVendor.Size = new System.Drawing.Size(122, 20);
			comboBoxGridVendor.TabIndex = 139;
			comboBoxGridVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridVendor.Visible = false;
			comboBoxGridCustomer.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxGridCustomer.Assigned = false;
			comboBoxGridCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCustomer.CustomReportFieldName = "";
			comboBoxGridCustomer.CustomReportKey = "";
			comboBoxGridCustomer.CustomReportValueType = 1;
			comboBoxGridCustomer.DescriptionTextBox = null;
			appearance90.BackColor = System.Drawing.SystemColors.Window;
			appearance90.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCustomer.DisplayLayout.Appearance = appearance90;
			comboBoxGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance91.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance91.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance91.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance91.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance91;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance92;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance93.BackColor2 = System.Drawing.SystemColors.Control;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance93;
			comboBoxGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance94.BackColor = System.Drawing.SystemColors.Window;
			appearance94.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance94;
			appearance95.BackColor = System.Drawing.SystemColors.Highlight;
			appearance95.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance95;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance96.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance96;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			appearance97.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCustomer.DisplayLayout.Override.CellAppearance = appearance97;
			comboBoxGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance98.BackColor = System.Drawing.SystemColors.Control;
			appearance98.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance98.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance98.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance98.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance98;
			appearance99.TextHAlignAsString = "Left";
			comboBoxGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance99;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance100.BackColor = System.Drawing.SystemColors.Window;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCustomer.DisplayLayout.Override.RowAppearance = appearance100;
			comboBoxGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance101.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance101;
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
			comboBoxGridCustomer.Location = new System.Drawing.Point(142, 252);
			comboBoxGridCustomer.MaxDropDownItems = 12;
			comboBoxGridCustomer.Name = "comboBoxGridCustomer";
			comboBoxGridCustomer.ShowConsignmentOnly = false;
			comboBoxGridCustomer.ShowInactive = false;
			comboBoxGridCustomer.ShowLPOCustomersOnly = false;
			comboBoxGridCustomer.ShowPROCustomersOnly = false;
			comboBoxGridCustomer.ShowQuickAdd = true;
			comboBoxGridCustomer.Size = new System.Drawing.Size(122, 20);
			comboBoxGridCustomer.TabIndex = 138;
			comboBoxGridCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCustomer.Visible = false;
			comboBoxGridAccount.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			comboBoxGridAccount.Assigned = false;
			comboBoxGridAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridAccount.CustomReportFieldName = "";
			comboBoxGridAccount.CustomReportKey = "";
			comboBoxGridAccount.CustomReportValueType = 1;
			comboBoxGridAccount.DescriptionTextBox = null;
			appearance102.BackColor = System.Drawing.SystemColors.Window;
			appearance102.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridAccount.DisplayLayout.Appearance = appearance102;
			comboBoxGridAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance103.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance103.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance103.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance103.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.GroupByBox.Appearance = appearance103;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance104;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance105.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance105.BackColor2 = System.Drawing.SystemColors.Control;
			appearance105.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance105.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance105;
			comboBoxGridAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveCellAppearance = appearance106;
			appearance107.BackColor = System.Drawing.SystemColors.Highlight;
			appearance107.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveRowAppearance = appearance107;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance108.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.CardAreaAppearance = appearance108;
			appearance109.BorderColor = System.Drawing.Color.Silver;
			appearance109.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridAccount.DisplayLayout.Override.CellAppearance = appearance109;
			comboBoxGridAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridAccount.DisplayLayout.Override.CellPadding = 0;
			appearance110.BackColor = System.Drawing.SystemColors.Control;
			appearance110.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance110.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.GroupByRowAppearance = appearance110;
			appearance111.TextHAlignAsString = "Left";
			comboBoxGridAccount.DisplayLayout.Override.HeaderAppearance = appearance111;
			comboBoxGridAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridAccount.DisplayLayout.Override.RowAppearance = appearance112;
			comboBoxGridAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance113.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance113;
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
			comboBoxGridAccount.Location = new System.Drawing.Point(142, 214);
			comboBoxGridAccount.MaxDropDownItems = 12;
			comboBoxGridAccount.Name = "comboBoxGridAccount";
			comboBoxGridAccount.ShowInactiveItems = false;
			comboBoxGridAccount.ShowQuickAdd = true;
			comboBoxGridAccount.Size = new System.Drawing.Size(122, 20);
			comboBoxGridAccount.TabIndex = 137;
			comboBoxGridAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridAccount.Visible = false;
			comboBoxPaymentMethod.AlwaysInEditMode = true;
			comboBoxPaymentMethod.Assigned = false;
			comboBoxPaymentMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPaymentMethod.CustomReportFieldName = "";
			comboBoxPaymentMethod.CustomReportKey = "";
			comboBoxPaymentMethod.CustomReportValueType = 1;
			comboBoxPaymentMethod.DescriptionTextBox = null;
			appearance114.BackColor = System.Drawing.SystemColors.Window;
			appearance114.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPaymentMethod.DisplayLayout.Appearance = appearance114;
			comboBoxPaymentMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPaymentMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance115.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethod.DisplayLayout.GroupByBox.Appearance = appearance115;
			appearance116.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance116;
			comboBoxPaymentMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance117.BackColor2 = System.Drawing.SystemColors.Control;
			appearance117.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance117.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPaymentMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance117;
			comboBoxPaymentMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPaymentMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPaymentMethod.DisplayLayout.Override.ActiveCellAppearance = appearance118;
			appearance119.BackColor = System.Drawing.SystemColors.Highlight;
			appearance119.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPaymentMethod.DisplayLayout.Override.ActiveRowAppearance = appearance119;
			comboBoxPaymentMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPaymentMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance120.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethod.DisplayLayout.Override.CardAreaAppearance = appearance120;
			appearance121.BorderColor = System.Drawing.Color.Silver;
			appearance121.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPaymentMethod.DisplayLayout.Override.CellAppearance = appearance121;
			comboBoxPaymentMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPaymentMethod.DisplayLayout.Override.CellPadding = 0;
			appearance122.BackColor = System.Drawing.SystemColors.Control;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPaymentMethod.DisplayLayout.Override.GroupByRowAppearance = appearance122;
			appearance123.TextHAlignAsString = "Left";
			comboBoxPaymentMethod.DisplayLayout.Override.HeaderAppearance = appearance123;
			comboBoxPaymentMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPaymentMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			comboBoxPaymentMethod.DisplayLayout.Override.RowAppearance = appearance124;
			comboBoxPaymentMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance125.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPaymentMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance125;
			comboBoxPaymentMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPaymentMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPaymentMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPaymentMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPaymentMethod.Editable = true;
			comboBoxPaymentMethod.FilterString = "";
			comboBoxPaymentMethod.IsDataLoaded = false;
			comboBoxPaymentMethod.Location = new System.Drawing.Point(661, 81);
			comboBoxPaymentMethod.MaxDropDownItems = 12;
			comboBoxPaymentMethod.Name = "comboBoxPaymentMethod";
			comboBoxPaymentMethod.Size = new System.Drawing.Size(58, 20);
			comboBoxPaymentMethod.TabIndex = 118;
			comboBoxPaymentMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPaymentMethod.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance126.BackColor = System.Drawing.SystemColors.Window;
			appearance126.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance126;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance127.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance127.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance127;
			appearance128.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance128;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance129.BackColor2 = System.Drawing.SystemColors.Control;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance129;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance130;
			appearance131.BackColor = System.Drawing.SystemColors.Highlight;
			appearance131.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance131;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance132.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance132;
			appearance133.BorderColor = System.Drawing.Color.Silver;
			appearance133.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance133;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance134.BackColor = System.Drawing.SystemColors.Control;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance134;
			appearance135.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance135;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance136;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance137.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance137;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(12, 145);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(650, 269);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			comboBoxChequebook.AlwaysInEditMode = true;
			comboBoxChequebook.Assigned = false;
			comboBoxChequebook.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxChequebook.CustomReportFieldName = "";
			comboBoxChequebook.CustomReportKey = "";
			comboBoxChequebook.CustomReportValueType = 1;
			comboBoxChequebook.DescriptionTextBox = null;
			appearance138.BackColor = System.Drawing.SystemColors.Window;
			appearance138.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxChequebook.DisplayLayout.Appearance = appearance138;
			comboBoxChequebook.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxChequebook.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance139.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance139.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance139.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance139.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.GroupByBox.Appearance = appearance139;
			appearance140.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.BandLabelAppearance = appearance140;
			comboBoxChequebook.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance141.BackColor2 = System.Drawing.SystemColors.Control;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.PromptAppearance = appearance141;
			comboBoxChequebook.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxChequebook.DisplayLayout.MaxRowScrollRegions = 1;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxChequebook.DisplayLayout.Override.ActiveCellAppearance = appearance142;
			appearance143.BackColor = System.Drawing.SystemColors.Highlight;
			appearance143.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxChequebook.DisplayLayout.Override.ActiveRowAppearance = appearance143;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance144.BackColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.CardAreaAppearance = appearance144;
			appearance145.BorderColor = System.Drawing.Color.Silver;
			appearance145.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxChequebook.DisplayLayout.Override.CellAppearance = appearance145;
			comboBoxChequebook.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxChequebook.DisplayLayout.Override.CellPadding = 0;
			appearance146.BackColor = System.Drawing.SystemColors.Control;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.GroupByRowAppearance = appearance146;
			appearance147.TextHAlignAsString = "Left";
			comboBoxChequebook.DisplayLayout.Override.HeaderAppearance = appearance147;
			comboBoxChequebook.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxChequebook.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			comboBoxChequebook.DisplayLayout.Override.RowAppearance = appearance148;
			comboBoxChequebook.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance149.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxChequebook.DisplayLayout.Override.TemplateAddRowAppearance = appearance149;
			comboBoxChequebook.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxChequebook.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxChequebook.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxChequebook.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxChequebook.Editable = true;
			comboBoxChequebook.FilterString = "";
			comboBoxChequebook.HasAllAccount = false;
			comboBoxChequebook.HasCustom = false;
			comboBoxChequebook.IsDataLoaded = false;
			comboBoxChequebook.Location = new System.Drawing.Point(400, 220);
			comboBoxChequebook.MaxDropDownItems = 12;
			comboBoxChequebook.Name = "comboBoxChequebook";
			comboBoxChequebook.ShowInactiveItems = false;
			comboBoxChequebook.ShowQuickAdd = true;
			comboBoxChequebook.Size = new System.Drawing.Size(46, 20);
			comboBoxChequebook.TabIndex = 122;
			comboBoxChequebook.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxChequebook.Visible = false;
			comboBoxConsignExpense.Assigned = false;
			comboBoxConsignExpense.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxConsignExpense.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignExpense.CustomReportFieldName = "";
			comboBoxConsignExpense.CustomReportKey = "";
			comboBoxConsignExpense.CustomReportValueType = 1;
			comboBoxConsignExpense.DescriptionTextBox = null;
			appearance150.BackColor = System.Drawing.SystemColors.Window;
			appearance150.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignExpense.DisplayLayout.Appearance = appearance150;
			comboBoxConsignExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance151.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.Appearance = appearance151;
			appearance152.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance152;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance153.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance153.BackColor2 = System.Drawing.SystemColors.Control;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance153;
			comboBoxConsignExpense.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance154.BackColor = System.Drawing.SystemColors.Window;
			appearance154.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignExpense.DisplayLayout.Override.ActiveCellAppearance = appearance154;
			appearance155.BackColor = System.Drawing.SystemColors.Highlight;
			appearance155.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignExpense.DisplayLayout.Override.ActiveRowAppearance = appearance155;
			comboBoxConsignExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance156.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.Override.CardAreaAppearance = appearance156;
			appearance157.BorderColor = System.Drawing.Color.Silver;
			appearance157.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignExpense.DisplayLayout.Override.CellAppearance = appearance157;
			comboBoxConsignExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignExpense.DisplayLayout.Override.CellPadding = 0;
			appearance158.BackColor = System.Drawing.SystemColors.Control;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.Override.GroupByRowAppearance = appearance158;
			appearance159.TextHAlignAsString = "Left";
			comboBoxConsignExpense.DisplayLayout.Override.HeaderAppearance = appearance159;
			comboBoxConsignExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance160.BackColor = System.Drawing.SystemColors.Window;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignExpense.DisplayLayout.Override.RowAppearance = appearance160;
			comboBoxConsignExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance161.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance161;
			comboBoxConsignExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignExpense.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignExpense.Editable = true;
			comboBoxConsignExpense.ExpenseCodeType = Micromind.Common.Data.ExpenseCodeTypes.ConsignmentIn;
			comboBoxConsignExpense.FilterString = "";
			comboBoxConsignExpense.HasAllAccount = false;
			comboBoxConsignExpense.HasCustom = false;
			comboBoxConsignExpense.IsDataLoaded = false;
			comboBoxConsignExpense.Location = new System.Drawing.Point(575, 216);
			comboBoxConsignExpense.MaxDropDownItems = 12;
			comboBoxConsignExpense.Name = "comboBoxConsignExpense";
			comboBoxConsignExpense.ShowInactiveItems = false;
			comboBoxConsignExpense.ShowQuickAdd = true;
			comboBoxConsignExpense.Size = new System.Drawing.Size(100, 20);
			comboBoxConsignExpense.TabIndex = 133;
			comboBoxConsignExpense.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxConsignExpense.Visible = false;
			comboBoxConsignIn.Assigned = false;
			comboBoxConsignIn.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxConsignIn.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignIn.CustomReportFieldName = "";
			comboBoxConsignIn.CustomReportKey = "";
			comboBoxConsignIn.CustomReportValueType = 1;
			comboBoxConsignIn.DescriptionTextBox = null;
			appearance162.BackColor = System.Drawing.SystemColors.Window;
			appearance162.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignIn.DisplayLayout.Appearance = appearance162;
			comboBoxConsignIn.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignIn.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance163.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance163.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance163.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance163.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.GroupByBox.Appearance = appearance163;
			appearance164.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignIn.DisplayLayout.GroupByBox.BandLabelAppearance = appearance164;
			comboBoxConsignIn.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance165.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance165.BackColor2 = System.Drawing.SystemColors.Control;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignIn.DisplayLayout.GroupByBox.PromptAppearance = appearance165;
			comboBoxConsignIn.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignIn.DisplayLayout.MaxRowScrollRegions = 1;
			appearance166.BackColor = System.Drawing.SystemColors.Window;
			appearance166.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignIn.DisplayLayout.Override.ActiveCellAppearance = appearance166;
			appearance167.BackColor = System.Drawing.SystemColors.Highlight;
			appearance167.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignIn.DisplayLayout.Override.ActiveRowAppearance = appearance167;
			comboBoxConsignIn.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignIn.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance168.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.Override.CardAreaAppearance = appearance168;
			appearance169.BorderColor = System.Drawing.Color.Silver;
			appearance169.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignIn.DisplayLayout.Override.CellAppearance = appearance169;
			comboBoxConsignIn.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignIn.DisplayLayout.Override.CellPadding = 0;
			appearance170.BackColor = System.Drawing.SystemColors.Control;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.Override.GroupByRowAppearance = appearance170;
			appearance171.TextHAlignAsString = "Left";
			comboBoxConsignIn.DisplayLayout.Override.HeaderAppearance = appearance171;
			comboBoxConsignIn.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignIn.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			appearance172.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignIn.DisplayLayout.Override.RowAppearance = appearance172;
			comboBoxConsignIn.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance173.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignIn.DisplayLayout.Override.TemplateAddRowAppearance = appearance173;
			comboBoxConsignIn.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignIn.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignIn.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignIn.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignIn.Editable = true;
			comboBoxConsignIn.FilterString = "";
			comboBoxConsignIn.HasAllAccount = false;
			comboBoxConsignIn.HasCustom = false;
			comboBoxConsignIn.IsDataLoaded = false;
			comboBoxConsignIn.Location = new System.Drawing.Point(575, 242);
			comboBoxConsignIn.MaxDropDownItems = 12;
			comboBoxConsignIn.Name = "comboBoxConsignIn";
			comboBoxConsignIn.ShowInactiveItems = false;
			comboBoxConsignIn.ShowQuickAdd = true;
			comboBoxConsignIn.Size = new System.Drawing.Size(100, 20);
			comboBoxConsignIn.TabIndex = 134;
			comboBoxConsignIn.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxConsignIn.Visible = false;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			appearance174.BackColor = System.Drawing.SystemColors.Window;
			appearance174.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCategory.DisplayLayout.Appearance = appearance174;
			comboBoxCostCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance175.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance175.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance175.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance175.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.GroupByBox.Appearance = appearance175;
			appearance176.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance176;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance177.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance177.BackColor2 = System.Drawing.SystemColors.Control;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance177;
			comboBoxCostCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			appearance178.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveCellAppearance = appearance178;
			appearance179.BackColor = System.Drawing.SystemColors.Highlight;
			appearance179.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveRowAppearance = appearance179;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance180.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.CardAreaAppearance = appearance180;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			appearance181.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCategory.DisplayLayout.Override.CellAppearance = appearance181;
			comboBoxCostCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCategory.DisplayLayout.Override.CellPadding = 0;
			appearance182.BackColor = System.Drawing.SystemColors.Control;
			appearance182.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance182.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance182.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.GroupByRowAppearance = appearance182;
			appearance183.TextHAlignAsString = "Left";
			comboBoxCostCategory.DisplayLayout.Override.HeaderAppearance = appearance183;
			comboBoxCostCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance184.BackColor = System.Drawing.SystemColors.Window;
			appearance184.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCategory.DisplayLayout.Override.RowAppearance = appearance184;
			comboBoxCostCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance185.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance185;
			comboBoxCostCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCostCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCostCategory.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(596, 164);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(79, 20);
			comboBoxCostCategory.TabIndex = 125;
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
			comboBoxJob.Location = new System.Drawing.Point(575, 190);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 124;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(23, 22);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(675, 487);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxGridVendor);
			base.Controls.Add(comboBoxGridCustomer);
			base.Controls.Add(comboBoxGridAccount);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxPaymentMethod);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(comboBoxChequebook);
			base.Controls.Add(comboBoxConsignExpense);
			base.Controls.Add(comboBoxConsignIn);
			base.Controls.Add(comboBoxCostCategory);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(ultraFormattedLinkLabel6);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "OpeningChequePaymentEntryForm";
			Text = "Opening Cheque Payment Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)payeeTypeComboBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPaymentMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignIn).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
