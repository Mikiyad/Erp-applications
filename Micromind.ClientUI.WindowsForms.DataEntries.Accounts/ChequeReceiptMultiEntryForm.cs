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
	public class ChequeReceiptMultiEntryForm : Form, IForm, ITransactionForm, IWorkFlowForm
	{
		private string requestSysDocID = "";

		private TransactionData currentData;

		private const string TABLENAME_CONST = "GL_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool useProperty = CompanyPreferences.UseProperty;

		private string attributeID1Title = CompanyPreferences.AttributeID1Title;

		private string attributeID2Title = CompanyPreferences.AttributeID2Title;

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

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private CostCenterComboBox comboBoxCostCenter;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private Label labelAnalysis;

		private flatDatePicker dateTimePickerDate;

		private ChequebookComboBox comboBoxChequebook;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private ExpenseCodeComboBox comboBoxConsignExpense;

		private ConsignInComboBox comboBoxConsignIn;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private AmountTextBox textBoxAmount;

		private PropertyComboBox comboBoxProperty;

		private PropertyUnitComboBox comboBoxPropertyUnit;

		private AllAccountsComboBox comboBoxGridAccount;

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

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonInformation;

		private FormManager formManager;

		private customersFlatComboBox comboBoxGridCustomer;

		private EmployeeComboBox comboBoxGridEmployee;

		private vendorsFlatComboBox comboBoxGridVendor;

		private PayeeSelector payeeSelector1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private RegisterComboBox comboBoxRegister;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelChequeBook;

		private MMLabel mmLabel4;

		private MMLabel mmLabel3;

		private DateTimePicker dateTimePickerChequeDate;

		private TextBox textBoxChequeNumber;

		private BankComboBox comboBoxBank;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		private JobComboBox ComboBoxitemJob;

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

		public ChequeReceiptMultiEntryForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
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
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			base.KeyDown += Form_KeyDown;
			base.FormClosing += AccountGroupDetailsForm_FormClosing;
		}

		private void comboBoxChequebook_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				textBoxChequeNumber.Text = Factory.ChequebookSystem.GetNextChequeNumber(comboBoxChequebook.SelectedID).ToString();
			}
			catch
			{
			}
		}

		private void Form_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
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
			checked
			{
				if (e.Cell.Column.Key == "Account")
				{
					if (e.Cell.Row.Cells["T"].Value.ToString() == "A")
					{
						dataGridItems.ActiveRow.Cells["Account Name"].Value = comboBoxGridAccount.SelectedName;
					}
					else if (e.Cell.Row.Cells["T"].Value.ToString() == "C")
					{
						dataGridItems.ActiveRow.Cells["Account Name"].Value = comboBoxGridCustomer.SelectedName;
					}
					else if (e.Cell.Row.Cells["T"].Value.ToString() == "V")
					{
						dataGridItems.ActiveRow.Cells["Account Name"].Value = comboBoxGridVendor.SelectedName;
						if (!comboBoxGridVendor.AllowOAP)
						{
							ErrorHelper.WarningMessage("Open account payment is not allowed for selected vendor.", "Please use other screens to do the payment.");
							dataGridItems.ActiveRow.Delete(displayPrompt: false);
							return;
						}
					}
					else if (e.Cell.Row.Cells["T"].Value.ToString() == "E")
					{
						dataGridItems.ActiveRow.Cells["Account Name"].Value = comboBoxGridEmployee.SelectedName;
					}
					dataGridItems.ActiveRow.Cells["Description"].Value = textBoxNote.Text;
					if ((dataGridItems.ActiveRow.Cells["AttributeID1"].Value == null || dataGridItems.ActiveRow.Cells["AttributeID1"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
					{
						dataGridItems.ActiveRow.Cells["AttributeID1"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["AttributeID1"].Value;
					}
					if ((dataGridItems.ActiveRow.Cells["AttributeID2"].Value == null || dataGridItems.ActiveRow.Cells["AttributeID2"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
					{
						dataGridItems.ActiveRow.Cells["AttributeID2"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["AttributeID2"].Value;
					}
				}
				if (e.Cell.Column.Key == "Amount")
				{
					textBoxAmount.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
					decimal result = default(decimal);
					decimal.TryParse(e.Cell.Value.ToString(), out result);
				}
				if (e.Cell.Column.Key == "AttributeID1")
				{
					for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["AttributeID1"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["AttributeID1"].Value = e.Cell.Value.ToString();
						}
					}
				}
				if (!(e.Cell.Column.Key == "AttributeID2"))
				{
					return;
				}
				for (int j = e.Cell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
				{
					if (dataGridItems.Rows[j].Cells["AttributeID2"].Value.ToString() == "")
					{
						dataGridItems.Rows[j].Cells["AttributeID2"].Value = e.Cell.Value.ToString();
					}
				}
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

		private void payeeSelector1_SelectedItemChanged(object sender, EventArgs e)
		{
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["T"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["T"].Value = "C";
			}
			string a = dataGridItems.ActiveRow.Cells["T"].Value.ToString();
			if (a == "A")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridAccount;
			}
			else if (a == "C")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridCustomer;
			}
			else if (a == "V")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridVendor;
			}
			else if (a == "E")
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridEmployee;
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Activated && e.Cell.Column.Key == "T")
			{
				object obj3 = dataGridItems.ActiveRow.Cells["Account"].Value = (dataGridItems.ActiveRow.Cells["Account Name"].Value = "");
			}
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
						return;
					}
				}
			}
			if (e.Cell.Column.Key == "T")
			{
				if (e.NewValue.ToString() == "A")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridAccount;
				}
				else if (e.NewValue.ToString() == "C")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridCustomer;
				}
				else if (e.NewValue.ToString() == "V")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridVendor;
				}
				else if (e.NewValue.ToString() == "E")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridEmployee;
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
					currentData = new TransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.TransactionTable.Rows[0] : currentData.TransactionTable.NewRow();
				dataRow["TransactionID"] = 0;
				dataRow["SysDocType"] = (byte)253;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["CostCenterID"] = comboBoxCostCenter.SelectedID;
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["RegisterID"] = comboBoxRegister.SelectedID;
				dataRow["CheckNumber"] = textBoxChequeNumber.Text;
				dataRow["CheckDate"] = dateTimePickerChequeDate.Value.ToShortDateString();
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
					string a = (string)(dataRow2["PayeeType"] = row.Cells["T"].Value.ToString());
					if (a != "A")
					{
						dataRow2["PayeeID"] = row.Cells["Account"].Value.ToString();
					}
					else
					{
						dataRow2["AccountID"] = row.Cells["Account"].Value.ToString();
					}
					dataRow2["PaymentMethodType"] = (byte)2;
					dataRow2["BankID"] = comboBoxBank.SelectedID;
					dataRow2["CheckDate"] = dateTimePickerChequeDate.Value.ToShortDateString();
					dataRow2["CheckNumber"] = textBoxChequeNumber.Text;
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
					if (row.Cells["AttributeID1"].Value != null && row.Cells["AttributeID1"].Value.ToString() != "")
					{
						dataRow2["AttributeID1"] = row.Cells["AttributeID1"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeID1"] = DBNull.Value;
					}
					if (row.Cells["AttributeID2"].Value != null && row.Cells["AttributeID2"].Value.ToString() != "")
					{
						dataRow2["AttributeID2"] = row.Cells["AttributeID2"].Value.ToString();
					}
					else
					{
						dataRow2["AttributeID2"] = DBNull.Value;
					}
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
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.TransactionDetailsTable.Rows.Add(dataRow2);
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
				dataTable.Columns.Add("T");
				dataTable.Columns.Add("Account");
				dataTable.Columns.Add("Account Name");
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Chequebook");
				dataTable.Columns.Add("Chq Number");
				dataTable.Columns.Add("Chq Date");
				dataTable.Columns.Add("AttributeID1");
				dataTable.Columns.Add("AttributeID2");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				bool hidden = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = !useProperty);
				ultraGridColumn2.Hidden = hidden;
				if (!useProperty)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add("A", "Account");
				valueList.ValueListItems.Add("C", "Customer");
				valueList.ValueListItems.Add("V", "Vendor");
				valueList.ValueListItems.Add("E", "Employee");
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].AutoSizeMode = ColumnAutoSizeMode.VisibleRows;
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].MaxWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].MinWidth = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["T"].LockedWidth = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].ValueList = comboBoxGridAccount;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].CellAppearance.ForeColor = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Account Name"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = ComboBoxitemJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = ComboBoxitemcostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = true);
				ultraGridColumn3.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(7 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chequebook"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Number"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Chq Date"].Hidden = true;
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Hidden = !useProperty);
				ultraGridColumn4.Hidden = hidden;
				if (!useProperty)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Header.Caption = attributeID1Title;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID1"].ValueList = comboBoxProperty;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Header.Caption = attributeID2Title;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["AttributeID2"].ValueList = comboBoxPropertyUnit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.TotalAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Width = checked(12 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = 75;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				if (!useJobCosting)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
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
					currentData = Factory.TransactionSystem.GetTransactionByID(SystemDocID, journalID);
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
					comboBoxRegister.SelectedID = dataRow["RegisterID"].ToString();
					comboBoxChequebook.SelectedID = dataRow["ChequebookID"].ToString();
					textBoxChequeNumber.Text = dataRow["CheckNumber"].ToString();
					dateTimePickerChequeDate.Value = DateTime.Parse(dataRow["CheckDate"].ToString());
					if (dataRow["Amount"] != DBNull.Value)
					{
						textBoxAmount.Text = Math.Round(decimal.Parse(dataRow["Amount"].ToString()), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxAmount.Text = 0.ToString(Format.TotalAmountFormat);
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
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Transaction_Details") && currentData.TransactionDetailsTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Transaction_Details"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							string a = row["PayeeType"].ToString();
							if (row["PayeeType"] != DBNull.Value)
							{
								dataRow3["T"] = row["PayeeType"];
							}
							else
							{
								dataRow3["T"] = "A";
							}
							if (a == "C" || a == "V" || a == "E")
							{
								dataRow3["Account"] = row["PayeeID"];
							}
							else
							{
								dataRow3["Account"] = row["AccountID"];
							}
							dataRow3["Account Name"] = row["AccountName"];
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
							if (row["AttributeID1"] != DBNull.Value)
							{
								dataRow3["AttributeID1"] = row["AttributeID1"];
							}
							if (row["AttributeID2"] != DBNull.Value)
							{
								dataRow3["AttributeID2"] = row["AttributeID2"];
							}
							dataRow3["Description"] = row["Description"].ToString();
							comboBoxBank.SelectedID = row["BankID"].ToString();
							if (useJobCosting)
							{
								dataRow3["Job"] = row["JobID"];
								dataRow3["CostCategory"] = row["CostCategoryID"];
							}
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
			bool flag = false;
			try
			{
				bool flag2 = (!isNewRecord) ? Factory.TransactionSystem.InsertUpdateChequeReceiptMultipleTransaction(currentData, isUpdate: true) : Factory.TransactionSystem.InsertUpdateChequeReceiptMultipleTransaction(currentData, isUpdate: false);
				if (flag2)
				{
					flag = true;
				}
				if (!flag2)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					if (showAllocationForm)
					{
						_ = textBoxAmount.Tag;
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
				return flag2;
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
			if (isCostCenterMandatory && comboBoxCostCenter.SelectedID == string.Empty)
			{
				ErrorHelper.InformationMessage("Please select cost center");
				return false;
			}
			if (comboBoxRegister.SelectedID == "" || comboBoxSysDoc.SelectedID == "" || textBoxVoucherNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (comboBoxBank.SelectedID == "")
			{
				ErrorHelper.InformationMessage("Select the Bank");
				comboBoxBank.Focus();
				return false;
			}
			if (textBoxChequeNumber.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterChequeNumber);
				comboBoxChequebook.Focus();
				return false;
			}
			decimal num = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!dataGridItems.HasRowAnyValue(row))
				{
					row.Delete(displayPrompt: false);
				}
				else if (row.Cells["Amount"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterChequeAmount);
					row.Activate();
					return false;
				}
				num += decimal.Parse(row.Cells["Amount"].Value.ToString());
			}
			decimal.Parse(textBoxAmount.Text);
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage(UIMessages.EnterAtLeastOneRowOfPayment);
				return false;
			}
			if (GetTransactionBalance() == 0m && ErrorHelper.QuestionMessageYesNo(UIMessages.TransactionWithZeroAmount) == DialogResult.No)
			{
				return false;
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
				textBoxAmount.Tag = null;
				textBoxAmount.ReadOnly = false;
				textBoxAmount.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxCostCenter.Clear();
				labelBalance.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
				labelBalance.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxBank.Clear();
				comboBoxChequebook.Enabled = true;
				dataGridItems.AllowAddNew = true;
				textBoxChequeNumber.Clear();
				dateTimePickerChequeDate.Value = DateTime.Now;
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
				return Factory.TransactionSystem.DeleteTransaction(SystemDocID, textBoxVoucherNumber.Text);
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
			else
			{
				dataGridItems.SaveLayout();
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
					comboBoxSysDoc.FilterByType(SysDocTypes.ChequeReceiptMultiple);
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
			if (!Security.IsAllowedSecurityUser(GeneralSecurityRoles.ChangeTransactionRegister))
			{
				comboBoxRegister.ShowDefaultRegisterOnly = true;
			}
			else
			{
				comboBoxRegister.ShowDefaultRegisterOnly = false;
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
				return Factory.TransactionSystem.VoidTransaction(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCenter(comboBoxCostCenter.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ChequeReceiptMultiple);
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
					DataSet transactionToPrint = Factory.TransactionSystem.GetTransactionToPrint(systemDocID, text);
					if (transactionToPrint == null || transactionToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(transactionToPrint, systemDocID, "Cheque Receipt Multiple", SysDocTypes.ChequeReceiptMultiple, isPrint, showPrintDialog);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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

		public void SetApprovalStatus()
		{
		}

		public void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID)
		{
			EditDocument(sysDocID, voucherID);
			formManager.ShowApprovalPanel(approvalTaskID, "GL_Transaction", "VoucherID");
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonSelectInvoice_Click(object sender, EventArgs e)
		{
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			DataSet dataSet = null;
			if (textBoxAmount.Tag != null)
			{
				dataSet = (textBoxAmount.Tag as DataSet);
				PaymentAdviceDetailsForm paymentAdviceDetailsForm = new PaymentAdviceDetailsForm();
				paymentAdviceDetailsForm.PaymentData = dataSet;
				paymentAdviceDetailsForm.IsViewOnly = true;
				paymentAdviceDetailsForm.ShowDialog();
			}
		}

		private void formManager_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(firstID == ""))
			{
				LoadData(firstID);
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

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("GL_Transaction", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("GL_Transaction", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.ChequeReceiptMultiEntryListFormObj);
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonPrint_Click(object sender, EventArgs e)
		{
			Print(isPrint: true, showPrintDialog: true, saveChanges: true);
		}

		private void toolStripButtonPreview_Click(object sender, EventArgs e)
		{
			Preview();
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
						docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
						docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
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

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditRegister(comboBoxRegister.SelectedID);
		}

		private void ultraFormattedLinkLabelChequeBook_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditChequebook(comboBoxChequebook.SelectedID);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
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
					string voucherID = activeRow.Cells["Number"].Value.ToString();
					PaymentRequestData paymentRequestByID = Factory.PaymentRequestSystem.GetPaymentRequestByID(sysDocID, voucherID);
					if (paymentRequestByID != null && paymentRequestByID.Tables[0].Rows.Count > 0)
					{
						DataRow dataRow = paymentRequestByID.Tables[0].Rows[0];
						comboBoxChequebook.SelectedID = dataRow["PayFromID"].ToString();
						currencySelector.SelectedID = dataRow["CurrencyID"].ToString();
						comboBoxChequebook.Enabled = false;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null && currentData != null && dataGridItems.ActiveRow.Cells["Chq Number"].Value != null && dataGridItems.ActiveRow.Cells["Chq Number"].Value.ToString() != "")
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
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
						decimal num = default(decimal);
						foreach (DataRow row in paymentAdviceDetailsForm.PaymentData.Tables["AP_Payment_Advice"].Rows)
						{
							num += decimal.Parse(row["PaymentAmount"].ToString());
						}
						textBoxAmount.Text = num.ToString(Format.TotalAmountFormat);
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
			Infragistics.Win.Appearance appearance186 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance187 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance188 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance189 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance190 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance191 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance192 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance193 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance194 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance195 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance196 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance197 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance198 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance199 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance200 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance201 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance202 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance203 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance204 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance205 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance206 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance207 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance208 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance209 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance210 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance211 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance212 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance213 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Accounts.ChequeReceiptMultiEntryForm));
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
			comboBoxBank = new Micromind.DataControls.BankComboBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			dateTimePickerChequeDate = new System.Windows.Forms.DateTimePicker();
			textBoxChequeNumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRegister = new Micromind.DataControls.RegisterComboBox();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxAmount = new Micromind.UISupport.AmountTextBox();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			labelAnalysis = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCostCenter = new Micromind.DataControls.CostCenterComboBox();
			currencySelector = new Micromind.DataControls.CurrencySelector();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxAnalysis = new Micromind.DataControls.AnalysisComboBox();
			ultraFormattedLinkLabelChequeBook = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			payeeSelector1 = new Micromind.DataControls.PayeeSelector();
			comboBoxGridVendor = new Micromind.DataControls.vendorsFlatComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxGridCustomer = new Micromind.DataControls.customersFlatComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxPropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			comboBoxChequebook = new Micromind.DataControls.ChequebookComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxConsignExpense = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxConsignIn = new Micromind.DataControls.ConsignInComboBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
			ComboBoxitemJob = new Micromind.DataControls.JobComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).BeginInit();
			toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignExpense).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignIn).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 476);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(702, 40);
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
			linePanelDown.Size = new System.Drawing.Size(702, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(592, 8);
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
			textBoxVoucherNumber.Location = new System.Drawing.Point(312, 3);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(123, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(207, 70);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(312, 66);
			textBoxRef1.MaxLength = 15;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(123, 20);
			textBoxRef1.TabIndex = 10;
			textBoxNote.Location = new System.Drawing.Point(90, 88);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(572, 20);
			textBoxNote.TabIndex = 11;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(11, 91);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "False";
			labelCurrency.Appearance = appearance;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(206, 27);
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
			ultraLabel1.Size = new System.Drawing.Size(533, 16);
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
			ultraGroupBox1.Location = new System.Drawing.Point(11, 441);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(678, 21);
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
			labelBalance.Location = new System.Drawing.Point(535, 3);
			labelBalance.Name = "labelBalance";
			labelBalance.Size = new System.Drawing.Size(141, 16);
			labelBalance.TabIndex = 113;
			labelBalance.Text = "0.00";
			panelDetails.Controls.Add(comboBoxBank);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(mmLabel4);
			panelDetails.Controls.Add(mmLabel3);
			panelDetails.Controls.Add(dateTimePickerChequeDate);
			panelDetails.Controls.Add(textBoxChequeNumber);
			panelDetails.Controls.Add(ultraFormattedLinkLabel10);
			panelDetails.Controls.Add(comboBoxRegister);
			panelDetails.Controls.Add(ultraFormattedLinkLabel8);
			panelDetails.Controls.Add(textBoxAmount);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(labelAnalysis);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCostCenter);
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
			panelDetails.Location = new System.Drawing.Point(0, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(702, 111);
			panelDetails.TabIndex = 0;
			comboBoxBank.Assigned = false;
			comboBoxBank.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBank.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBank.CustomReportFieldName = "";
			comboBoxBank.CustomReportKey = "";
			comboBoxBank.CustomReportValueType = 1;
			comboBoxBank.DescriptionTextBox = null;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			appearance8.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBank.DisplayLayout.Appearance = appearance8;
			comboBoxBank.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBank.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance9.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.GroupByBox.Appearance = appearance9;
			appearance10.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.BandLabelAppearance = appearance10;
			comboBoxBank.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance11.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance11.BackColor2 = System.Drawing.SystemColors.Control;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBank.DisplayLayout.GroupByBox.PromptAppearance = appearance11;
			comboBoxBank.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBank.DisplayLayout.MaxRowScrollRegions = 1;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBank.DisplayLayout.Override.ActiveCellAppearance = appearance12;
			appearance13.BackColor = System.Drawing.SystemColors.Highlight;
			appearance13.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBank.DisplayLayout.Override.ActiveRowAppearance = appearance13;
			comboBoxBank.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBank.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance14.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.CardAreaAppearance = appearance14;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			appearance15.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBank.DisplayLayout.Override.CellAppearance = appearance15;
			comboBoxBank.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBank.DisplayLayout.Override.CellPadding = 0;
			appearance16.BackColor = System.Drawing.SystemColors.Control;
			appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBank.DisplayLayout.Override.GroupByRowAppearance = appearance16;
			appearance17.TextHAlignAsString = "Left";
			comboBoxBank.DisplayLayout.Override.HeaderAppearance = appearance17;
			comboBoxBank.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBank.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance18.BackColor = System.Drawing.SystemColors.Window;
			appearance18.BorderColor = System.Drawing.Color.Silver;
			comboBoxBank.DisplayLayout.Override.RowAppearance = appearance18;
			comboBoxBank.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance19.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBank.DisplayLayout.Override.TemplateAddRowAppearance = appearance19;
			comboBoxBank.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBank.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBank.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBank.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBank.Editable = true;
			comboBoxBank.FilterString = "";
			comboBoxBank.HasAllAccount = false;
			comboBoxBank.HasCustom = false;
			comboBoxBank.IsDataLoaded = false;
			comboBoxBank.Location = new System.Drawing.Point(90, 46);
			comboBoxBank.MaxDropDownItems = 12;
			comboBoxBank.Name = "comboBoxBank";
			comboBoxBank.ShowInactiveItems = false;
			comboBoxBank.ShowQuickAdd = true;
			comboBoxBank.Size = new System.Drawing.Size(110, 20);
			comboBoxBank.TabIndex = 6;
			comboBoxBank.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance20.FontData.BoldAsString = "True";
			appearance20.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance20;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(13, 47);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel1.TabIndex = 173;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bank:";
			appearance21.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance21;
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = true;
			mmLabel4.Location = new System.Drawing.Point(206, 49);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(53, 13);
			mmLabel4.TabIndex = 171;
			mmLabel4.Text = "Chq No:";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = true;
			mmLabel3.Location = new System.Drawing.Point(442, 49);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(64, 13);
			mmLabel3.TabIndex = 170;
			mmLabel3.Text = "Chq Date:";
			dateTimePickerChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerChequeDate.Location = new System.Drawing.Point(515, 45);
			dateTimePickerChequeDate.Name = "dateTimePickerChequeDate";
			dateTimePickerChequeDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerChequeDate.TabIndex = 8;
			textBoxChequeNumber.Location = new System.Drawing.Point(312, 45);
			textBoxChequeNumber.Name = "textBoxChequeNumber";
			textBoxChequeNumber.Size = new System.Drawing.Size(123, 20);
			textBoxChequeNumber.TabIndex = 7;
			appearance22.FontData.BoldAsString = "True";
			appearance22.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel10.Appearance = appearance22;
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(441, 5);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel10.TabIndex = 166;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Register:";
			appearance23.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance23;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			comboBoxRegister.Assigned = false;
			comboBoxRegister.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRegister.CustomReportFieldName = "";
			comboBoxRegister.CustomReportKey = "";
			comboBoxRegister.CustomReportValueType = 1;
			comboBoxRegister.DescriptionTextBox = null;
			appearance24.BackColor = System.Drawing.SystemColors.Window;
			appearance24.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRegister.DisplayLayout.Appearance = appearance24;
			comboBoxRegister.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRegister.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance25.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.GroupByBox.Appearance = appearance25;
			appearance26.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.BandLabelAppearance = appearance26;
			comboBoxRegister.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance27.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance27.BackColor2 = System.Drawing.SystemColors.Control;
			appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRegister.DisplayLayout.GroupByBox.PromptAppearance = appearance27;
			comboBoxRegister.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRegister.DisplayLayout.MaxRowScrollRegions = 1;
			appearance28.BackColor = System.Drawing.SystemColors.Window;
			appearance28.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRegister.DisplayLayout.Override.ActiveCellAppearance = appearance28;
			appearance29.BackColor = System.Drawing.SystemColors.Highlight;
			appearance29.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRegister.DisplayLayout.Override.ActiveRowAppearance = appearance29;
			comboBoxRegister.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRegister.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance30.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.CardAreaAppearance = appearance30;
			appearance31.BorderColor = System.Drawing.Color.Silver;
			appearance31.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRegister.DisplayLayout.Override.CellAppearance = appearance31;
			comboBoxRegister.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRegister.DisplayLayout.Override.CellPadding = 0;
			appearance32.BackColor = System.Drawing.SystemColors.Control;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRegister.DisplayLayout.Override.GroupByRowAppearance = appearance32;
			appearance33.TextHAlignAsString = "Left";
			comboBoxRegister.DisplayLayout.Override.HeaderAppearance = appearance33;
			comboBoxRegister.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRegister.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance34.BackColor = System.Drawing.SystemColors.Window;
			appearance34.BorderColor = System.Drawing.Color.Silver;
			comboBoxRegister.DisplayLayout.Override.RowAppearance = appearance34;
			comboBoxRegister.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance35.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRegister.DisplayLayout.Override.TemplateAddRowAppearance = appearance35;
			comboBoxRegister.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRegister.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRegister.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRegister.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRegister.Editable = true;
			comboBoxRegister.FilterString = "";
			comboBoxRegister.HasAllAccount = false;
			comboBoxRegister.HasCustom = false;
			comboBoxRegister.IsDataLoaded = false;
			comboBoxRegister.Location = new System.Drawing.Point(515, 3);
			comboBoxRegister.MaxDropDownItems = 12;
			comboBoxRegister.Name = "comboBoxRegister";
			comboBoxRegister.ShowDefaultRegisterOnly = false;
			comboBoxRegister.ShowInactiveItems = false;
			comboBoxRegister.ShowQuickAdd = true;
			comboBoxRegister.Size = new System.Drawing.Size(107, 20);
			comboBoxRegister.TabIndex = 2;
			comboBoxRegister.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance36.FontData.BoldAsString = "True";
			appearance36.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance36;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(12, 69);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(31, 15);
			ultraFormattedLinkLabel8.TabIndex = 164;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Bills:";
			appearance37.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance37;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			textBoxAmount.AllowDecimal = true;
			textBoxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAmount.CustomReportFieldName = "";
			textBoxAmount.CustomReportKey = "";
			textBoxAmount.CustomReportValueType = 1;
			textBoxAmount.IsComboTextBox = false;
			textBoxAmount.IsModified = false;
			textBoxAmount.Location = new System.Drawing.Point(90, 67);
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
			textBoxAmount.ReadOnly = true;
			textBoxAmount.Size = new System.Drawing.Size(110, 20);
			textBoxAmount.TabIndex = 9;
			textBoxAmount.Text = "0.00";
			textBoxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(90, 25);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(110, 20);
			dateTimePickerDate.TabIndex = 3;
			dateTimePickerDate.Value = new System.DateTime(2008, 5, 15, 11, 58, 51, 836);
			labelAnalysis.AutoSize = true;
			labelAnalysis.Location = new System.Drawing.Point(458, 70);
			labelAnalysis.Name = "labelAnalysis";
			labelAnalysis.Size = new System.Drawing.Size(48, 13);
			labelAnalysis.TabIndex = 119;
			labelAnalysis.Text = "Analysis:";
			labelAnalysis.Visible = false;
			appearance38.FontData.BoldAsString = "True";
			appearance38.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance38;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(10, 6);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 116;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance39.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance39;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.AlwaysInEditMode = true;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance40.BackColor = System.Drawing.SystemColors.Window;
			appearance40.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance40;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance41.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance41;
			appearance42.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance42;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance43.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance43.BackColor2 = System.Drawing.SystemColors.Control;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance43;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance44.BackColor = System.Drawing.SystemColors.Window;
			appearance44.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance44;
			appearance45.BackColor = System.Drawing.SystemColors.Highlight;
			appearance45.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance45;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance46.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance46;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			appearance47.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance48.BackColor = System.Drawing.SystemColors.Control;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance48;
			appearance49.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance49;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance50.BackColor = System.Drawing.SystemColors.Window;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance51.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance51;
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
			comboBoxSysDoc.Size = new System.Drawing.Size(110, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance52.FontData.BoldAsString = "False";
			appearance52.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance52;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(441, 27);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel4.TabIndex = 114;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Cost Center:";
			appearance53.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance53;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCostCenter.AlwaysInEditMode = true;
			comboBoxCostCenter.Assigned = false;
			comboBoxCostCenter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCenter.CustomReportFieldName = "";
			comboBoxCostCenter.CustomReportKey = "";
			comboBoxCostCenter.CustomReportValueType = 1;
			comboBoxCostCenter.DescriptionTextBox = null;
			appearance54.BackColor = System.Drawing.SystemColors.Window;
			appearance54.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCenter.DisplayLayout.Appearance = appearance54;
			comboBoxCostCenter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCenter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance55.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.GroupByBox.Appearance = appearance55;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance56;
			comboBoxCostCenter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance57.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance57.BackColor2 = System.Drawing.SystemColors.Control;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCenter.DisplayLayout.GroupByBox.PromptAppearance = appearance57;
			comboBoxCostCenter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCenter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance58.BackColor = System.Drawing.SystemColors.Window;
			appearance58.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveCellAppearance = appearance58;
			appearance59.BackColor = System.Drawing.SystemColors.Highlight;
			appearance59.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCenter.DisplayLayout.Override.ActiveRowAppearance = appearance59;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCenter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance60.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.CardAreaAppearance = appearance60;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			appearance61.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCenter.DisplayLayout.Override.CellAppearance = appearance61;
			comboBoxCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCenter.DisplayLayout.Override.CellPadding = 0;
			appearance62.BackColor = System.Drawing.SystemColors.Control;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCenter.DisplayLayout.Override.GroupByRowAppearance = appearance62;
			appearance63.TextHAlignAsString = "Left";
			comboBoxCostCenter.DisplayLayout.Override.HeaderAppearance = appearance63;
			comboBoxCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCenter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance64.BackColor = System.Drawing.SystemColors.Window;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCenter.DisplayLayout.Override.RowAppearance = appearance64;
			comboBoxCostCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance65.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCenter.DisplayLayout.Override.TemplateAddRowAppearance = appearance65;
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
			comboBoxCostCenter.Size = new System.Drawing.Size(107, 20);
			comboBoxCostCenter.TabIndex = 5;
			comboBoxCostCenter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			currencySelector.BackColor = System.Drawing.Color.WhiteSmoke;
			currencySelector.Location = new System.Drawing.Point(312, 24);
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
			currencySelector.Size = new System.Drawing.Size(123, 20);
			currencySelector.TabIndex = 4;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(10, 28);
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
			appearance66.BackColor = System.Drawing.SystemColors.Window;
			appearance66.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAnalysis.DisplayLayout.Appearance = appearance66;
			comboBoxAnalysis.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAnalysis.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance67.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.GroupByBox.Appearance = appearance67;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BandLabelAppearance = appearance68;
			comboBoxAnalysis.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance69.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance69.BackColor2 = System.Drawing.SystemColors.Control;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAnalysis.DisplayLayout.GroupByBox.PromptAppearance = appearance69;
			comboBoxAnalysis.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAnalysis.DisplayLayout.MaxRowScrollRegions = 1;
			appearance70.BackColor = System.Drawing.SystemColors.Window;
			appearance70.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveCellAppearance = appearance70;
			appearance71.BackColor = System.Drawing.SystemColors.Highlight;
			appearance71.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAnalysis.DisplayLayout.Override.ActiveRowAppearance = appearance71;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAnalysis.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance72.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.CardAreaAppearance = appearance72;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			appearance73.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAnalysis.DisplayLayout.Override.CellAppearance = appearance73;
			comboBoxAnalysis.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAnalysis.DisplayLayout.Override.CellPadding = 0;
			appearance74.BackColor = System.Drawing.SystemColors.Control;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAnalysis.DisplayLayout.Override.GroupByRowAppearance = appearance74;
			appearance75.TextHAlignAsString = "Left";
			comboBoxAnalysis.DisplayLayout.Override.HeaderAppearance = appearance75;
			comboBoxAnalysis.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAnalysis.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance76.BackColor = System.Drawing.SystemColors.Window;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			comboBoxAnalysis.DisplayLayout.Override.RowAppearance = appearance76;
			comboBoxAnalysis.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance77.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAnalysis.DisplayLayout.Override.TemplateAddRowAppearance = appearance77;
			comboBoxAnalysis.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAnalysis.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAnalysis.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAnalysis.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAnalysis.Editable = true;
			comboBoxAnalysis.FilterString = "";
			comboBoxAnalysis.HasAllAccount = false;
			comboBoxAnalysis.HasCustom = false;
			comboBoxAnalysis.IsDataLoaded = false;
			comboBoxAnalysis.Location = new System.Drawing.Point(532, 67);
			comboBoxAnalysis.MaxDropDownItems = 12;
			comboBoxAnalysis.Name = "comboBoxAnalysis";
			comboBoxAnalysis.ShowInactiveItems = false;
			comboBoxAnalysis.ShowQuickAdd = true;
			comboBoxAnalysis.Size = new System.Drawing.Size(130, 20);
			comboBoxAnalysis.TabIndex = 12;
			comboBoxAnalysis.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxAnalysis.Visible = false;
			appearance78.FontData.BoldAsString = "True";
			appearance78.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelChequeBook.Appearance = appearance78;
			ultraFormattedLinkLabelChequeBook.AutoSize = true;
			ultraFormattedLinkLabelChequeBook.Location = new System.Drawing.Point(76, 326);
			ultraFormattedLinkLabelChequeBook.Name = "ultraFormattedLinkLabelChequeBook";
			ultraFormattedLinkLabelChequeBook.Size = new System.Drawing.Size(77, 15);
			ultraFormattedLinkLabelChequeBook.TabIndex = 172;
			ultraFormattedLinkLabelChequeBook.TabStop = true;
			ultraFormattedLinkLabelChequeBook.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelChequeBook.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelChequeBook.Value = "Chequebook:";
			ultraFormattedLinkLabelChequeBook.Visible = false;
			appearance79.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelChequeBook.VisitedLinkAppearance = appearance79;
			ultraFormattedLinkLabelChequeBook.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelChequeBook_LinkClicked);
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(14, 358);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(675, 55);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			ultraFormattedLinkLabel6.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			appearance80.FontData.BoldAsString = "False";
			appearance80.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance80;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(12, 461);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel6.TabIndex = 135;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Clearance Details";
			ultraFormattedLinkLabel6.Visible = false;
			appearance81.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance81;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[16]
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
				toolStripSeparator5,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(702, 31);
			toolStrip1.TabIndex = 140;
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
			toolStripButtonPreview.Image = Micromind.ClientUI.Properties.Resources.preview;
			toolStripButtonPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPreview.Name = "toolStripButtonPreview";
			toolStripButtonPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonPreview.Text = "Preview";
			toolStripButtonPreview.ToolTipText = "Preview";
			toolStripButtonPreview.Click += new System.EventHandler(toolStripButtonPreview_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			payeeSelector1.BackColor = System.Drawing.Color.Transparent;
			payeeSelector1.Location = new System.Drawing.Point(192, 254);
			payeeSelector1.MaximumSize = new System.Drawing.Size(2000, 20);
			payeeSelector1.MinimumSize = new System.Drawing.Size(0, 20);
			payeeSelector1.Name = "payeeSelector1";
			payeeSelector1.SelectedID = "";
			payeeSelector1.SelectedType = "";
			payeeSelector1.Size = new System.Drawing.Size(177, 20);
			payeeSelector1.TabIndex = 145;
			payeeSelector1.Visible = false;
			comboBoxGridVendor.Assigned = false;
			comboBoxGridVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridVendor.CustomReportFieldName = "";
			comboBoxGridVendor.CustomReportKey = "";
			comboBoxGridVendor.CustomReportValueType = 1;
			comboBoxGridVendor.DescriptionTextBox = null;
			appearance82.BackColor = System.Drawing.SystemColors.Window;
			appearance82.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridVendor.DisplayLayout.Appearance = appearance82;
			comboBoxGridVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance83.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.GroupByBox.Appearance = appearance83;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance84;
			comboBoxGridVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance85.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance85.BackColor2 = System.Drawing.SystemColors.Control;
			appearance85.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance85.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance85;
			comboBoxGridVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance86.BackColor = System.Drawing.SystemColors.Window;
			appearance86.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveCellAppearance = appearance86;
			appearance87.BackColor = System.Drawing.SystemColors.Highlight;
			appearance87.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridVendor.DisplayLayout.Override.ActiveRowAppearance = appearance87;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance88.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.CardAreaAppearance = appearance88;
			appearance89.BorderColor = System.Drawing.Color.Silver;
			appearance89.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridVendor.DisplayLayout.Override.CellAppearance = appearance89;
			comboBoxGridVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridVendor.DisplayLayout.Override.CellPadding = 0;
			appearance90.BackColor = System.Drawing.SystemColors.Control;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridVendor.DisplayLayout.Override.GroupByRowAppearance = appearance90;
			appearance91.TextHAlignAsString = "Left";
			comboBoxGridVendor.DisplayLayout.Override.HeaderAppearance = appearance91;
			comboBoxGridVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance92.BackColor = System.Drawing.SystemColors.Window;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridVendor.DisplayLayout.Override.RowAppearance = appearance92;
			comboBoxGridVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance93.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance93;
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
			comboBoxGridVendor.Location = new System.Drawing.Point(425, 326);
			comboBoxGridVendor.MaxDropDownItems = 12;
			comboBoxGridVendor.Name = "comboBoxGridVendor";
			comboBoxGridVendor.ShowConsignmentOnly = false;
			comboBoxGridVendor.ShowQuickAdd = true;
			comboBoxGridVendor.Size = new System.Drawing.Size(88, 20);
			comboBoxGridVendor.TabIndex = 144;
			comboBoxGridVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridVendor.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
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
			comboBoxGridEmployee.Location = new System.Drawing.Point(425, 190);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(100, 20);
			comboBoxGridEmployee.TabIndex = 143;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxGridCustomer.Assigned = false;
			comboBoxGridCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCustomer.CustomReportFieldName = "";
			comboBoxGridCustomer.CustomReportKey = "";
			comboBoxGridCustomer.CustomReportValueType = 1;
			comboBoxGridCustomer.DescriptionTextBox = null;
			appearance106.BackColor = System.Drawing.SystemColors.Window;
			appearance106.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCustomer.DisplayLayout.Appearance = appearance106;
			comboBoxGridCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance107.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.Appearance = appearance107;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance108;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance109.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance109.BackColor2 = System.Drawing.SystemColors.Control;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance109;
			comboBoxGridCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance110.BackColor = System.Drawing.SystemColors.Window;
			appearance110.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance110;
			appearance111.BackColor = System.Drawing.SystemColors.Highlight;
			appearance111.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance111;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance112.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.CardAreaAppearance = appearance112;
			appearance113.BorderColor = System.Drawing.Color.Silver;
			appearance113.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCustomer.DisplayLayout.Override.CellAppearance = appearance113;
			comboBoxGridCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance114.BackColor = System.Drawing.SystemColors.Control;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance114;
			appearance115.TextHAlignAsString = "Left";
			comboBoxGridCustomer.DisplayLayout.Override.HeaderAppearance = appearance115;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance116.BackColor = System.Drawing.SystemColors.Window;
			appearance116.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCustomer.DisplayLayout.Override.RowAppearance = appearance116;
			comboBoxGridCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance117.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance117;
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
			comboBoxGridCustomer.Location = new System.Drawing.Point(157, 194);
			comboBoxGridCustomer.MaxDropDownItems = 12;
			comboBoxGridCustomer.Name = "comboBoxGridCustomer";
			comboBoxGridCustomer.ShowConsignmentOnly = false;
			comboBoxGridCustomer.ShowInactive = false;
			comboBoxGridCustomer.ShowLPOCustomersOnly = false;
			comboBoxGridCustomer.ShowPROCustomersOnly = false;
			comboBoxGridCustomer.ShowQuickAdd = true;
			comboBoxGridCustomer.Size = new System.Drawing.Size(112, 20);
			comboBoxGridCustomer.TabIndex = 142;
			comboBoxGridCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCustomer.Visible = false;
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 31);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 141;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			formManager.Click += new System.EventHandler(formManager_Click);
			comboBoxGridAccount.Assigned = false;
			comboBoxGridAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridAccount.CustomReportFieldName = "";
			comboBoxGridAccount.CustomReportKey = "";
			comboBoxGridAccount.CustomReportValueType = 1;
			comboBoxGridAccount.DescriptionTextBox = null;
			appearance118.BackColor = System.Drawing.SystemColors.Window;
			appearance118.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridAccount.DisplayLayout.Appearance = appearance118;
			comboBoxGridAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance119.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance119.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance119.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance119.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.GroupByBox.Appearance = appearance119;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance120;
			comboBoxGridAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance121.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance121.BackColor2 = System.Drawing.SystemColors.Control;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance121;
			comboBoxGridAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance122.BackColor = System.Drawing.SystemColors.Window;
			appearance122.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveCellAppearance = appearance122;
			appearance123.BackColor = System.Drawing.SystemColors.Highlight;
			appearance123.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridAccount.DisplayLayout.Override.ActiveRowAppearance = appearance123;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance124.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.CardAreaAppearance = appearance124;
			appearance125.BorderColor = System.Drawing.Color.Silver;
			appearance125.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridAccount.DisplayLayout.Override.CellAppearance = appearance125;
			comboBoxGridAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridAccount.DisplayLayout.Override.CellPadding = 0;
			appearance126.BackColor = System.Drawing.SystemColors.Control;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridAccount.DisplayLayout.Override.GroupByRowAppearance = appearance126;
			appearance127.TextHAlignAsString = "Left";
			comboBoxGridAccount.DisplayLayout.Override.HeaderAppearance = appearance127;
			comboBoxGridAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance128.BackColor = System.Drawing.SystemColors.Window;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridAccount.DisplayLayout.Override.RowAppearance = appearance128;
			comboBoxGridAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance129.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance129;
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
			comboBoxGridAccount.Location = new System.Drawing.Point(211, 164);
			comboBoxGridAccount.MaxDropDownItems = 12;
			comboBoxGridAccount.Name = "comboBoxGridAccount";
			comboBoxGridAccount.ShowInactiveItems = false;
			comboBoxGridAccount.ShowQuickAdd = true;
			comboBoxGridAccount.Size = new System.Drawing.Size(100, 20);
			comboBoxGridAccount.TabIndex = 139;
			comboBoxGridAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridAccount.Visible = false;
			comboBoxPropertyUnit.Assigned = false;
			comboBoxPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyUnit.CustomReportFieldName = "";
			comboBoxPropertyUnit.CustomReportKey = "";
			comboBoxPropertyUnit.CustomReportValueType = 1;
			comboBoxPropertyUnit.DescriptionTextBox = null;
			appearance130.BackColor = System.Drawing.SystemColors.Window;
			appearance130.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyUnit.DisplayLayout.Appearance = appearance130;
			comboBoxPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance131.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance131.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance131.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance131.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance131;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance132;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance133.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance133.BackColor2 = System.Drawing.SystemColors.Control;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance133;
			comboBoxPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance134.BackColor = System.Drawing.SystemColors.Window;
			appearance134.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance134;
			appearance135.BackColor = System.Drawing.SystemColors.Highlight;
			appearance135.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance135;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance136.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance136;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			appearance137.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyUnit.DisplayLayout.Override.CellAppearance = appearance137;
			comboBoxPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance138.BackColor = System.Drawing.SystemColors.Control;
			appearance138.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance138.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance138.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance138.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance138;
			appearance139.TextHAlignAsString = "Left";
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance139;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance140.BackColor = System.Drawing.SystemColors.Window;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyUnit.DisplayLayout.Override.RowAppearance = appearance140;
			comboBoxPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance141.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance141;
			comboBoxPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyUnit.Editable = true;
			comboBoxPropertyUnit.FilterString = "";
			comboBoxPropertyUnit.HasAllAccount = false;
			comboBoxPropertyUnit.HasCustom = false;
			comboBoxPropertyUnit.IsDataLoaded = false;
			comboBoxPropertyUnit.Location = new System.Drawing.Point(426, 300);
			comboBoxPropertyUnit.MaxDropDownItems = 12;
			comboBoxPropertyUnit.Name = "comboBoxPropertyUnit";
			comboBoxPropertyUnit.ShowInactiveItems = false;
			comboBoxPropertyUnit.ShowQuickAdd = true;
			comboBoxPropertyUnit.Size = new System.Drawing.Size(100, 20);
			comboBoxPropertyUnit.TabIndex = 138;
			comboBoxPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPropertyUnit.Visible = false;
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = null;
			appearance142.BackColor = System.Drawing.SystemColors.Window;
			appearance142.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance142;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance143.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance143.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance143.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance143.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance143;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance144;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance145.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance145.BackColor2 = System.Drawing.SystemColors.Control;
			appearance145.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance145;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance146.BackColor = System.Drawing.SystemColors.Window;
			appearance146.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance146;
			appearance147.BackColor = System.Drawing.SystemColors.Highlight;
			appearance147.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance147;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance148.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance148;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			appearance149.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance149;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance150.BackColor = System.Drawing.SystemColors.Control;
			appearance150.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance150.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance150.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance150;
			appearance151.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance151;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance152.BackColor = System.Drawing.SystemColors.Window;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance152;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance153.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance153;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(420, 254);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(176, 20);
			comboBoxProperty.TabIndex = 137;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxProperty.Visible = false;
			comboBoxChequebook.AlwaysInEditMode = true;
			comboBoxChequebook.Assigned = false;
			comboBoxChequebook.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxChequebook.CustomReportFieldName = "";
			comboBoxChequebook.CustomReportKey = "";
			comboBoxChequebook.CustomReportValueType = 1;
			comboBoxChequebook.DescriptionTextBox = null;
			appearance154.BackColor = System.Drawing.SystemColors.Window;
			appearance154.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxChequebook.DisplayLayout.Appearance = appearance154;
			comboBoxChequebook.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxChequebook.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance155.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance155.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance155.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance155.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.GroupByBox.Appearance = appearance155;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.BandLabelAppearance = appearance156;
			comboBoxChequebook.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance157.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance157.BackColor2 = System.Drawing.SystemColors.Control;
			appearance157.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance157.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxChequebook.DisplayLayout.GroupByBox.PromptAppearance = appearance157;
			comboBoxChequebook.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxChequebook.DisplayLayout.MaxRowScrollRegions = 1;
			appearance158.BackColor = System.Drawing.SystemColors.Window;
			appearance158.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxChequebook.DisplayLayout.Override.ActiveCellAppearance = appearance158;
			appearance159.BackColor = System.Drawing.SystemColors.Highlight;
			appearance159.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxChequebook.DisplayLayout.Override.ActiveRowAppearance = appearance159;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxChequebook.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance160.BackColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.CardAreaAppearance = appearance160;
			appearance161.BorderColor = System.Drawing.Color.Silver;
			appearance161.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxChequebook.DisplayLayout.Override.CellAppearance = appearance161;
			comboBoxChequebook.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxChequebook.DisplayLayout.Override.CellPadding = 0;
			appearance162.BackColor = System.Drawing.SystemColors.Control;
			appearance162.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance162.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance162.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance162.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxChequebook.DisplayLayout.Override.GroupByRowAppearance = appearance162;
			appearance163.TextHAlignAsString = "Left";
			comboBoxChequebook.DisplayLayout.Override.HeaderAppearance = appearance163;
			comboBoxChequebook.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxChequebook.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance164.BackColor = System.Drawing.SystemColors.Window;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			comboBoxChequebook.DisplayLayout.Override.RowAppearance = appearance164;
			comboBoxChequebook.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance165.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxChequebook.DisplayLayout.Override.TemplateAddRowAppearance = appearance165;
			comboBoxChequebook.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxChequebook.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxChequebook.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxChequebook.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxChequebook.Editable = true;
			comboBoxChequebook.FilterString = "";
			comboBoxChequebook.HasAllAccount = false;
			comboBoxChequebook.HasCustom = false;
			comboBoxChequebook.IsDataLoaded = false;
			comboBoxChequebook.Location = new System.Drawing.Point(182, 326);
			comboBoxChequebook.MaxDropDownItems = 12;
			comboBoxChequebook.Name = "comboBoxChequebook";
			comboBoxChequebook.ShowInactiveItems = false;
			comboBoxChequebook.ShowQuickAdd = true;
			comboBoxChequebook.Size = new System.Drawing.Size(110, 20);
			comboBoxChequebook.TabIndex = 30;
			comboBoxChequebook.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxChequebook.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance166.BackColor = System.Drawing.SystemColors.Window;
			appearance166.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance166;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance167.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance167.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance167.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance167.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance167;
			appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance168;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance169.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance169.BackColor2 = System.Drawing.SystemColors.Control;
			appearance169.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance169.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance169;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance170.BackColor = System.Drawing.SystemColors.Window;
			appearance170.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance170;
			appearance171.BackColor = System.Drawing.SystemColors.Highlight;
			appearance171.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance171;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance172.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance172;
			appearance173.BorderColor = System.Drawing.Color.Silver;
			appearance173.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance173;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance174.BackColor = System.Drawing.SystemColors.Control;
			appearance174.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance174.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance174.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance174.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance174;
			appearance175.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance175;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance176.BackColor = System.Drawing.SystemColors.Window;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance176;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance177.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance177;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 146);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(677, 297);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.AfterCellActivate += new System.EventHandler(dataGridItems_AfterCellActivate);
			comboBoxConsignExpense.Assigned = false;
			comboBoxConsignExpense.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxConsignExpense.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignExpense.CustomReportFieldName = "";
			comboBoxConsignExpense.CustomReportKey = "";
			comboBoxConsignExpense.CustomReportValueType = 1;
			comboBoxConsignExpense.DescriptionTextBox = null;
			appearance178.BackColor = System.Drawing.SystemColors.Window;
			appearance178.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignExpense.DisplayLayout.Appearance = appearance178;
			comboBoxConsignExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance179.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance179.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance179.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance179.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.Appearance = appearance179;
			appearance180.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance180;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance181.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance181.BackColor2 = System.Drawing.SystemColors.Control;
			appearance181.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance181.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance181;
			comboBoxConsignExpense.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance182.BackColor = System.Drawing.SystemColors.Window;
			appearance182.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignExpense.DisplayLayout.Override.ActiveCellAppearance = appearance182;
			appearance183.BackColor = System.Drawing.SystemColors.Highlight;
			appearance183.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignExpense.DisplayLayout.Override.ActiveRowAppearance = appearance183;
			comboBoxConsignExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance184.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.Override.CardAreaAppearance = appearance184;
			appearance185.BorderColor = System.Drawing.Color.Silver;
			appearance185.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignExpense.DisplayLayout.Override.CellAppearance = appearance185;
			comboBoxConsignExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignExpense.DisplayLayout.Override.CellPadding = 0;
			appearance186.BackColor = System.Drawing.SystemColors.Control;
			appearance186.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance186.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance186.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance186.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignExpense.DisplayLayout.Override.GroupByRowAppearance = appearance186;
			appearance187.TextHAlignAsString = "Left";
			comboBoxConsignExpense.DisplayLayout.Override.HeaderAppearance = appearance187;
			comboBoxConsignExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance188.BackColor = System.Drawing.SystemColors.Window;
			appearance188.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignExpense.DisplayLayout.Override.RowAppearance = appearance188;
			comboBoxConsignExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance189.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance189;
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
			appearance190.BackColor = System.Drawing.SystemColors.Window;
			appearance190.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignIn.DisplayLayout.Appearance = appearance190;
			comboBoxConsignIn.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignIn.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance191.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance191.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance191.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance191.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.GroupByBox.Appearance = appearance191;
			appearance192.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignIn.DisplayLayout.GroupByBox.BandLabelAppearance = appearance192;
			comboBoxConsignIn.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance193.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance193.BackColor2 = System.Drawing.SystemColors.Control;
			appearance193.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance193.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignIn.DisplayLayout.GroupByBox.PromptAppearance = appearance193;
			comboBoxConsignIn.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignIn.DisplayLayout.MaxRowScrollRegions = 1;
			appearance194.BackColor = System.Drawing.SystemColors.Window;
			appearance194.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignIn.DisplayLayout.Override.ActiveCellAppearance = appearance194;
			appearance195.BackColor = System.Drawing.SystemColors.Highlight;
			appearance195.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignIn.DisplayLayout.Override.ActiveRowAppearance = appearance195;
			comboBoxConsignIn.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignIn.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance196.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.Override.CardAreaAppearance = appearance196;
			appearance197.BorderColor = System.Drawing.Color.Silver;
			appearance197.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignIn.DisplayLayout.Override.CellAppearance = appearance197;
			comboBoxConsignIn.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignIn.DisplayLayout.Override.CellPadding = 0;
			appearance198.BackColor = System.Drawing.SystemColors.Control;
			appearance198.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance198.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance198.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance198.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignIn.DisplayLayout.Override.GroupByRowAppearance = appearance198;
			appearance199.TextHAlignAsString = "Left";
			comboBoxConsignIn.DisplayLayout.Override.HeaderAppearance = appearance199;
			comboBoxConsignIn.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignIn.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance200.BackColor = System.Drawing.SystemColors.Window;
			appearance200.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignIn.DisplayLayout.Override.RowAppearance = appearance200;
			comboBoxConsignIn.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance201.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignIn.DisplayLayout.Override.TemplateAddRowAppearance = appearance201;
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
			appearance202.BackColor = System.Drawing.SystemColors.Window;
			appearance202.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCostCategory.DisplayLayout.Appearance = appearance202;
			comboBoxCostCategory.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCostCategory.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance203.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance203.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance203.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance203.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.GroupByBox.Appearance = appearance203;
			appearance204.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BandLabelAppearance = appearance204;
			comboBoxCostCategory.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance205.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance205.BackColor2 = System.Drawing.SystemColors.Control;
			appearance205.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance205.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCostCategory.DisplayLayout.GroupByBox.PromptAppearance = appearance205;
			comboBoxCostCategory.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCostCategory.DisplayLayout.MaxRowScrollRegions = 1;
			appearance206.BackColor = System.Drawing.SystemColors.Window;
			appearance206.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveCellAppearance = appearance206;
			appearance207.BackColor = System.Drawing.SystemColors.Highlight;
			appearance207.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCostCategory.DisplayLayout.Override.ActiveRowAppearance = appearance207;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCostCategory.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance208.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.CardAreaAppearance = appearance208;
			appearance209.BorderColor = System.Drawing.Color.Silver;
			appearance209.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCostCategory.DisplayLayout.Override.CellAppearance = appearance209;
			comboBoxCostCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCostCategory.DisplayLayout.Override.CellPadding = 0;
			appearance210.BackColor = System.Drawing.SystemColors.Control;
			appearance210.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance210.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance210.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance210.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCostCategory.DisplayLayout.Override.GroupByRowAppearance = appearance210;
			appearance211.TextHAlignAsString = "Left";
			comboBoxCostCategory.DisplayLayout.Override.HeaderAppearance = appearance211;
			comboBoxCostCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCostCategory.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance212.BackColor = System.Drawing.SystemColors.Window;
			appearance212.BorderColor = System.Drawing.Color.Silver;
			comboBoxCostCategory.DisplayLayout.Override.RowAppearance = appearance212;
			comboBoxCostCategory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance213.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCostCategory.DisplayLayout.Override.TemplateAddRowAppearance = appearance213;
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
			ComboBoxitemcostCategory.Assigned = false;
			ComboBoxitemcostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemcostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxitemcostCategory.CustomReportFieldName = "";
			ComboBoxitemcostCategory.CustomReportKey = "";
			ComboBoxitemcostCategory.CustomReportValueType = 1;
			ComboBoxitemcostCategory.DescriptionTextBox = null;
			ComboBoxitemcostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxitemcostCategory.Editable = true;
			ComboBoxitemcostCategory.FilterString = "";
			ComboBoxitemcostCategory.HasAllAccount = false;
			ComboBoxitemcostCategory.HasCustom = false;
			ComboBoxitemcostCategory.IsDataLoaded = false;
			ComboBoxitemcostCategory.Location = new System.Drawing.Point(441, 216);
			ComboBoxitemcostCategory.MaxDropDownItems = 12;
			ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
			ComboBoxitemcostCategory.ShowInactiveItems = false;
			ComboBoxitemcostCategory.ShowQuickAdd = true;
			ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
			ComboBoxitemcostCategory.TabIndex = 174;
			ComboBoxitemcostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemcostCategory.Visible = false;
			ComboBoxitemJob.Assigned = false;
			ComboBoxitemJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxitemJob.CustomReportFieldName = "";
			ComboBoxitemJob.CustomReportKey = "";
			ComboBoxitemJob.CustomReportValueType = 1;
			ComboBoxitemJob.DescriptionTextBox = null;
			ComboBoxitemJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxitemJob.Editable = true;
			ComboBoxitemJob.FilterString = "";
			ComboBoxitemJob.HasAllAccount = false;
			ComboBoxitemJob.HasCustom = false;
			ComboBoxitemJob.IsDataLoaded = false;
			ComboBoxitemJob.Location = new System.Drawing.Point(268, 216);
			ComboBoxitemJob.MaxDropDownItems = 12;
			ComboBoxitemJob.Name = "ComboBoxitemJob";
			ComboBoxitemJob.ShowInactiveItems = false;
			ComboBoxitemJob.ShowQuickAdd = true;
			ComboBoxitemJob.Size = new System.Drawing.Size(100, 20);
			ComboBoxitemJob.TabIndex = 173;
			ComboBoxitemJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemJob.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(702, 516);
			base.Controls.Add(ComboBoxitemcostCategory);
			base.Controls.Add(ComboBoxitemJob);
			base.Controls.Add(ultraFormattedLinkLabelChequeBook);
			base.Controls.Add(payeeSelector1);
			base.Controls.Add(comboBoxGridVendor);
			base.Controls.Add(comboBoxGridEmployee);
			base.Controls.Add(comboBoxGridCustomer);
			base.Controls.Add(formManager);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxGridAccount);
			base.Controls.Add(comboBoxPropertyUnit);
			base.Controls.Add(comboBoxProperty);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(dataGridItems);
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
			base.Name = "ChequeReceiptMultiEntryForm";
			Text = "Cheque Receipt Multiple Entry";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBank).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRegister).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCenter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAnalysis).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxChequebook).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignExpense).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignIn).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
