using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
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
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental
{
	public class PropertyRentDetailsForm : Form, IForm
	{
		private PropertyRentData currentData;

		private SalesInvoiceNIData propertyServiceInvoiceData;

		private RecurringInvoiceData recurringInvoiceData;

		private const string TABLENAME_CONST = "Property_Rent";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string description = "";

		private bool isDataLoading;

		private bool isChanged;

		private bool isPeriodicInvoice;

		private bool isrecurring;

		private bool allowMultiTemplate;

		private string _agreestatus = "";

		private ScreenAccessRight screenRight;

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

		private FormManager formManager;

		private DateTimePicker dateTimeTransactionDate;

		private TextBox textBoxVoucherNumber;

		private MMLabel mmLabel1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraGroupBox ultraGroupBox1;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private UltraLabel ultraLabel1;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private BOMComboBox bomComboBox2;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageExpense;

		private CurrencyComboBox comboBoxGridCurrency;

		private DataEntryGrid dataGridIncome;

		private DateTimePicker dateTimeStartDate;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonDistribution;

		private PropertyComboBox comboBoxProperty;

		private DataSet dataSet1;

		private PropertyUnitComboBox comboBoxPropertyUnit;

		private DateTimePicker dateTimeEndDate;

		private customersFlatComboBox comboBoxCustomer;

		private Label label6;

		private TextBox textBoxTotalDays;

		private Label label2;

		private TextBox textBoxCustomerName;

		private Label labelTotalCost;

		private XPButton buttonPayCash;

		private XPButton buttonPayCheque;

		private PropertyIncomeCodeComboBox comboBoxGridIncomeCode;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private Label label5;

		private Label label1;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxPropertyName;

		private GroupBox groupBox1;

		private ToolStripButton toolStripButtonOpenList;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxUnit;

		private UltraTabPageControl ultraTabPageControl1;

		private DataEntryGrid dataGridPayment;

		private NumericUpDown NumericNoofinstallments;

		private ToolStripButton toolStripButtonInformation;

		private RichTextBox TestrichTextBox;

		private ToolStripSeparator toolStripSeparator4;

		private Panel panel1;

		private Panel panelNonTax;

		private UltraFormattedLinkLabel linkLabelTax;

		private AmountTextBox textBoxTaxAmount;

		private Label label8;

		private AmountTextBox textBoxTotal;

		private Label label4;

		private AmountTextBox textBoxSubtotal;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator5;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelAgent;

		private PropertyAgentComboBox comboBoxPropertyAgent;

		private GroupBox groupBoxPeriodicInvoice;

		private MMLabel mmLabel2;

		private MonthComboBox comboBoxFrequencyCount;

		private MMSDateTimePicker dateTimePickerInvoiceDate;

		private MMLabel mmLabel38;

		private CheckBox checkBoxPeriodicInvoice;

		private ComboBox comboBoxFrequency;

		private Label label9;

		private UltraFormattedLinkLabel ultraFormattedLinkCurrency;

		private CurrencySelector comboBoxCurrency;

		private ToolStripButton toolStripButtonMultiPreview;

		public ScreenAreas ScreenArea => ScreenAreas.PropertyRental;

		public int ScreenID => 4002;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string SystemDocID => comboBoxSysDoc.SelectedID;

		private string AgreementStatus
		{
			get
			{
				return _agreestatus;
			}
			set
			{
			}
		}

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
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
					XPButton xPButton2 = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = false);
					xPButton2.Enabled = enabled;
					comboBoxSysDoc.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton3 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton3.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
					XPButton xPButton4 = buttonPayCash;
					enabled = (buttonPayCheque.Enabled = true);
					xPButton4.Enabled = enabled;
					comboBoxSysDoc.Enabled = false;
				}
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonMultiPreview.Enabled = !value;
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
					buttonSave.Enabled = !value;
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

		public PropertyRentDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridIncome.AllowCustomizeHeaders = true;
		}

		private void AddEvents()
		{
			base.Load += WorkOrderForm_Load;
			dataGridIncome.AfterCellUpdate += dataGridIncome_AfterCellUpdate;
			dataGridIncome.BeforeRowDeactivate += dataGridIncome_BeforeRowDeactivate;
			dataGridIncome.BeforeCellDeactivate += dataGridIncome_BeforeCellDeactivate;
			dataGridIncome.AfterRowsDeleted += dataGridIncome_AfterRowsDeleted;
			dataGridIncome.CellDataError += dataGridIncome_CellDataError;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			dataGridIncome.HeaderClicked += dataGridItems_HeaderClicked;
			base.FormClosing += Form_FormClosing;
			comboBoxProperty.SelectedIndexChanged += comboBoxProperty_SelectedIndexChanged;
			comboBoxPropertyUnit.SelectedIndexChanged += comboBoxPropertyUnit_SelectedIndexChanged;
		}

		private void comboBoxProperty_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxProperty.SelectedID != "")
			{
				comboBoxPropertyUnit.FilterByPropertyId(comboBoxProperty.SelectedID);
			}
			else
			{
				comboBoxPropertyUnit.FilterByPropertyId("");
			}
			isPeriodicInvoice = comboBoxProperty.IsPeriodicInv;
			if (isPeriodicInvoice)
			{
				groupBoxPeriodicInvoice.Visible = true;
				checkBoxPeriodicInvoice.Checked = true;
			}
			else
			{
				groupBoxPeriodicInvoice.Visible = false;
				checkBoxPeriodicInvoice.Checked = false;
			}
		}

		private void comboBoxPropertyUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGridIncome_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridIncome.ActiveCell.Column.Key == "Income Code")
			{
				ErrorHelper.InformationMessage("Please select a valid item.");
				e.RaiseErrorEvent = false;
			}
		}

		private void comboBoxPayeeTaxGroup_SelectedIndexChanged(object sender, EventArgs e)
		{
			CalculateAllRowsTaxes();
			CalculateTotalCost();
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (!isDataLoading && comboBoxCustomer.SelectedID != "")
				{
					if (CompanyPreferences.IsTax)
					{
						comboBoxPayeeTaxGroup.Clear();
						if (comboBoxCustomer.TaxOption == PayeeTaxOptions.Taxable)
						{
							comboBoxPayeeTaxGroup.SelectedID = comboBoxCustomer.DefaultTaxGroupID;
						}
					}
					else
					{
						comboBoxPayeeTaxGroup.Clear();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxQuantity_TextChanged(object sender, EventArgs e)
		{
			CalculateUnitCost();
		}

		private void CalculateTotalCost()
		{
			decimal d = default(decimal);
			try
			{
				decimal d2 = default(decimal);
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal result = default(decimal);
				foreach (UltraGridRow row in dataGridIncome.Rows)
				{
					decimal result2 = default(decimal);
					if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
					{
						decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result2);
						d2 += result2;
						if (!row.Cells["Tax"].Value.IsNullOrEmpty())
						{
							decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result);
							row.Cells["TaxTotal"].Value = result2 + result;
						}
						if (!row.Cells["Tax"].Value.IsNullOrEmpty())
						{
							num2 += decimal.Parse(row.Cells["Tax"].Value.ToString());
						}
					}
				}
				num = d + d2;
				labelTotalCost.Text = num.ToString(Format.TotalAmountFormat);
				textBoxSubtotal.Text = num.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = num2.ToString(Format.TotalAmountFormat);
				num += num2;
				textBoxTotal.Text = num.ToString(Format.TotalAmountFormat);
				CalculateTotalTaxes();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateUnitCost()
		{
			decimal result = default(decimal);
			try
			{
				decimal.TryParse(labelTotalCost.Text, out result);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateAllRowsTaxes()
		{
			try
			{
				foreach (UltraGridRow row in dataGridIncome.Rows)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.BasedOnCustomer;
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						itemTaxOptions = (ItemTaxOptions)byte.Parse(row.Cells["TaxOption"].Value.ToString());
					}
					if (itemTaxOptions == ItemTaxOptions.BasedOnCustomer)
					{
						row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
					}
					decimal amount = decimal.Parse(row.Cells["Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
					decimal cost = default(decimal);
					TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = taxTransactionData.Copy();
					UIGlobal.CalculateRowTax(row, "Tax", cost, amount, subtotal, 0m, priceIncludeTax: false);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalTaxes()
		{
			TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID);
			DataTable taxDetailTable = taxTransactionData.TaxDetailTable;
			foreach (UltraGridRow row in dataGridIncome.Rows)
			{
				if (row.Cells["Tax"].Tag != null)
				{
					foreach (DataRow row2 in (row.Cells["Tax"].Tag as TaxTransactionData).TaxDetailTable.Rows)
					{
						string text = row2["TaxItemID"].ToString();
						decimal result = default(decimal);
						decimal.TryParse(row2["TaxAmount"].ToString(), out result);
						DataRow[] array = taxDetailTable.Select("TaxItemID  = '" + text + "'");
						if (array.Count() > 0)
						{
							decimal result2 = default(decimal);
							decimal.TryParse(array[0]["TaxAmount"].ToString(), out result2);
							result2 += result;
							array[0]["TaxAmount"] = result2;
						}
						else
						{
							DataRow dataRow = taxDetailTable.NewRow();
							dataRow["TaxItemID"] = text;
							dataRow["TaxAmount"] = result;
							taxDetailTable.Rows.Add(dataRow);
						}
					}
				}
			}
			textBoxTaxAmount.Tag = taxTransactionData;
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
			formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
			allowMultiTemplate = false;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "AllowMultiTemplate", "SysDocID", comboBoxSysDoc.SelectedID);
			if (fieldValue != null && fieldValue.ToString() != "")
			{
				allowMultiTemplate = bool.Parse(fieldValue.ToString());
			}
			if (allowMultiTemplate)
			{
				toolStripButtonMultiPreview.Visible = true;
			}
			else
			{
				toolStripButtonMultiPreview.Visible = false;
			}
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridIncome.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Tax" && dataGridIncome.ActiveRow != null)
			{
				TaxTransactionData taxData = new TaxTransactionData();
				if (dataGridIncome.ActiveRow.Cells["Tax"].Tag != null)
				{
					taxData = (dataGridIncome.ActiveRow.Cells["Tax"].Tag as TaxTransactionData);
				}
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog();
			}
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
			else
			{
				dataGridIncome.SaveLayout();
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			_ = (e.Cell.Column.Key == "Qty");
		}

		private void dataGridIncome_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridIncome.ActiveRow != null)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(dataGridIncome.ActiveRow.Cells["Discount"].Value.ToString(), out result2);
					decimal.TryParse(dataGridIncome.ActiveRow.Cells["Amount"].Value.ToString(), out result);
					decimal.TryParse(dataGridIncome.ActiveRow.Cells["Price"].Value.ToString(), out result3);
					if (e.Cell.Column.Key == "Income Code")
					{
						dataGridIncome.ActiveRow.Cells["Description"].Value = comboBoxGridIncomeCode.SelectedName;
						ItemTaxOptions taxOption = comboBoxGridIncomeCode.TaxOption;
						dataGridIncome.ActiveRow.Cells["TaxOption"].Value = taxOption;
						switch (taxOption)
						{
						case ItemTaxOptions.BasedOnCustomer:
							dataGridIncome.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
							break;
						case ItemTaxOptions.Taxable:
							dataGridIncome.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridIncomeCode.TaxGroupID;
							break;
						case ItemTaxOptions.NonTaxable:
							dataGridIncome.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
							break;
						}
					}
					else if (e.Cell.Column.Key == "TaxGroupID")
					{
						ItemTaxOptions itemTaxOption = ItemTaxOptions.BasedOnCustomer;
						if (!e.Cell.Row.Cells["TaxOption"].Value.IsNullOrEmpty())
						{
							itemTaxOption = (ItemTaxOptions)byte.Parse(e.Cell.Row.Cells["TaxOption"].Value.ToString());
						}
						TaxTransactionData tag = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOption, comboBoxGridIncomeCode.TaxGroupID);
						e.Cell.Row.Cells["Tax"].Tag = tag;
					}
					else if (e.Cell.Column.Key == "Price" && dataGridIncome.ActiveCell != null && dataGridIncome.ActiveCell.Column.Key == "Price")
					{
						result = Math.Round(result3 - result2, Global.CurDecimalPoints);
						dataGridIncome.ActiveRow.Cells["Amount"].Value = result;
					}
					if (e.Cell.Column.Key == "Discount" && dataGridIncome.ActiveCell != null && dataGridIncome.ActiveCell.Column.Key == "Discount")
					{
						result = Math.Round(result3 - result2, Global.CurDecimalPoints);
						dataGridIncome.ActiveRow.Cells["Amount"].Value = result;
					}
					if (e.Cell.Column.Key == "Amount")
					{
						decimal result4 = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result4);
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result4, subtotal, 0m);
						CalculateTotalCost();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridIncome_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridIncome.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Amount"].Value.ToString() == "")
				{
					activeRow.Cells["Amount"].Value = 0;
				}
				if (activeRow.Cells["Income Code"].Value == null || activeRow.Cells["Income Code"].Value.ToString() == string.Empty)
				{
					ErrorHelper.InformationMessage("Please select an expense code for the row");
					e.Cancel = true;
					activeRow.Cells["Income Code"].Activate();
				}
			}
		}

		private void dataGridIncome_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridIncome.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridIncome.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridIncome.ActiveCell.Value = result;
			}
		}

		private void dataGridIncome_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotalCost();
			CalculateAllRowsTaxes();
			CalculateTotalCost();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyRentData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyRentTable.Rows[0] : currentData.PropertyRentTable.NewRow();
				if (dateTimeTransactionDate.Value > DateTime.Today)
				{
					dataRow["TransactionDate"] = dateTimeTransactionDate.Value;
				}
				else
				{
					DateTime value = dateTimeTransactionDate.Value;
					dataRow["TransactionDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				if (dateTimeStartDate.Value > DateTime.Today)
				{
					dataRow["ContractStartDate"] = dateTimeStartDate.Value;
				}
				else
				{
					DateTime value2 = dateTimeStartDate.Value;
					dataRow["ContractStartDate"] = new DateTime(value2.Year, value2.Month, value2.Day, 11, 59, 59);
				}
				if (dateTimeEndDate.Value > DateTime.Today)
				{
					dataRow["ContractEndDate"] = dateTimeEndDate.Value;
				}
				else
				{
					DateTime value3 = dateTimeEndDate.Value;
					dataRow["ContractEndDate"] = new DateTime(value3.Year, value3.Month, value3.Day, 11, 59, 59);
				}
				dataRow["AgreementType"] = 1;
				if (!isNewRecord)
				{
					dataRow["AgreementStatus"] = AgreementStatus;
				}
				else
				{
					dataRow["AgreementStatus"] = "1";
				}
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
				dataRow["CurrencyRate"] = comboBoxCurrency.Rate;
				dataRow["PropertyID"] = comboBoxProperty.SelectedID;
				dataRow["UnitID"] = comboBoxPropertyUnit.SelectedID;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Total"] = labelTotalCost.Text;
				if (textBoxTotalDays.Text != "")
				{
					dataRow["TotalDays"] = textBoxTotalDays.Text;
				}
				else
				{
					dataRow["TotalDays"] = (dateTimeEndDate.Value - dateTimeStartDate.Value).Days.ToString();
				}
				dataRow["NoofInstallments"] = NumericNoofinstallments.Value;
				if (CompanyPreferences.IsTax)
				{
					dataRow["TaxOption"] = (byte)comboBoxCustomer.TaxOption;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
				}
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
				}
				if (comboBoxPropertyAgent.SelectedID != "")
				{
					dataRow["PropertyAgentID"] = comboBoxPropertyAgent.SelectedID;
				}
				else
				{
					dataRow["PropertyAgentID"] = DBNull.Value;
				}
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				if (isPeriodicInvoice && checkBoxPeriodicInvoice.Checked)
				{
					dataRow["IsPeriodicInvoice"] = checkBoxPeriodicInvoice.Checked;
					if (dateTimeStartDate.Checked)
					{
						dataRow["InvoiceStartDate"] = dateTimeStartDate.Value;
					}
					else
					{
						dataRow["InvoiceStartDate"] = DBNull.Value;
					}
					if (!string.IsNullOrEmpty(comboBoxFrequency.SelectedItem.ToString()))
					{
						dataRow["Frequency"] = comboBoxFrequency.SelectedItem.ToString().Trim();
					}
					else
					{
						dataRow["Frequency"] = DBNull.Value;
					}
					if (!string.IsNullOrEmpty(comboBoxFrequencyCount.SelectedItem.ToString()))
					{
						dataRow["FrequencyCount"] = int.Parse(comboBoxFrequencyCount.SelectedItem.ToString());
					}
					else
					{
						dataRow["FrequencyCount"] = DBNull.Value;
					}
					if (IsNewRecord)
					{
						string companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.DefaultRecurringInvoiceSysDocID, 101, comboBoxSysDoc.SelectedID, "");
						string text = "";
						if (companyOption == "")
						{
							ErrorHelper.WarningMessage("Please set one sysdoc id for recurring invoice!");
							return false;
						}
						text = Factory.SystemDocumentSystem.GetNextDocumentNumber(companyOption);
						dataRow["PeriodicSysDocID"] = companyOption;
						dataRow["PeriodicVoucherID"] = text;
						dataRow["TransactionID"] = Factory.SystemDocumentSystem.GetNextNumber("Recurring_Transaction", "TransactionID");
						dataRow["ProcessedBy"] = Global.CurrentUser;
						string text2 = "";
						int num = 0;
						int month = int.Parse(dateTimePickerInvoiceDate.Value.Month.ToString());
						description = "RENT FOR " + DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(month) + "-" + dateTimePickerInvoiceDate.Value.Year.ToString() + " ";
						if (comboBoxFrequency.SelectedItem.ToString().Equals("M"))
						{
							num = int.Parse(comboBoxFrequencyCount.SelectedItem.ToString());
							text2 = " [" + dateTimePickerInvoiceDate.Value.ToShortDateString() + " to " + dateTimePickerInvoiceDate.Value.AddMonths(num).AddDays(-1.0).ToShortDateString() + "]";
						}
						else if (comboBoxFrequency.SelectedItem.ToString().Equals("D"))
						{
							num = int.Parse(comboBoxFrequencyCount.SelectedItem.ToString());
							text2 = " [" + dateTimePickerInvoiceDate.Value.ToShortDateString() + " to " + dateTimePickerInvoiceDate.Value.AddDays(num).ToShortDateString() + "]";
						}
						description += text2;
						dataRow["InvoiceNote"] = Environment.NewLine + description;
					}
				}
				dataRow["IsPeriodicInvoice"] = checkBoxPeriodicInvoice.Checked;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PropertyRentTable.Rows.Add(dataRow);
				}
				currentData.PropertyRentDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridIncome.Rows)
				{
					DataRow dataRow2 = currentData.PropertyRentDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["IncomeID"] = row.Cells["Income Code"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Reference"] = row.Cells["Reference"].Value.ToString();
					dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow2["TaxOption"] = (byte)2;
					}
					if (row.Cells["Tax"].Value != null && row.Cells["Tax"].Value.ToString() != "")
					{
						dataRow2["TaxAmount"] = row.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow2["TaxAmount"] = DBNull.Value;
					}
					if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow2["TaxGroupID"] = DBNull.Value;
					}
					if (!string.IsNullOrEmpty(row.Cells["Price"].Value.ToString()))
					{
						dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
					}
					if (row.Cells["Discount"].Value != null && row.Cells["Discount"].Value.ToString() != "")
					{
						dataRow2["Discount"] = row.Cells["Discount"].Value.ToString();
					}
					else
					{
						dataRow2["Discount"] = DBNull.Value;
					}
					dataRow2.EndEdit();
					currentData.PropertyRentDetailTable.Rows.Add(dataRow2);
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text3 = textBoxVoucherNumber.Text;
				int num2 = 0;
				foreach (UltraGridRow row2 in dataGridIncome.Rows)
				{
					if (row2.Cells["Tax"].Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, row2.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text3, num2, comboBoxCustomer.DefaultCurrencyID, 1m);
					}
					num2 = checked(num2 + 1);
				}
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text3, -1, comboBoxCustomer.DefaultCurrencyID, 1m);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void SetupIncomeGrid()
		{
			try
			{
				dataGridIncome.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Income Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("Discount", typeof(decimal));
				dataGridIncome.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridIncome.DisplayLayout.Bands[0].Columns["TaxGroupID"];
				bool hidden = dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].Hidden = true;
				ultraGridColumn.Hidden = hidden;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].Hidden = true;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].Hidden = true;
				dataGridIncome.LoadLayout();
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].MaxLength = 64;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].ValueList = comboBoxGridIncomeCode;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 30;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].MaxLength = 20;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].Format = Format.GridAmountFormat;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].MinValue = new decimal(-999999999999L);
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].MaxValue = new decimal(999999999999L);
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].CellActivation = Activation.NoEdit;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].TabStop = false;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].Header.Caption = "Net Amount";
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxTotal"].TabStop = false;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridIncome.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].MaxLength = 20;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].Format = "#,0.00##";
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].MinValue = 0;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].MaxValue = new decimal(999999999999L);
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].Width = 30;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Reference"].Width = 60;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"].Width = 30;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Price"].Width = 30;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Description"].Width = 90;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].MinWidth = 50;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Discount"].MaxWidth = 100;
				dataGridIncome.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridIncome.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridIncome.SetupUI();
			}
			catch
			{
			}
		}

		private void SetupPaymentGrid()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("PayType");
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("TransactionDate", typeof(DateTime));
			dataTable.Columns.Add("ChequeNo");
			dataTable.Columns.Add("ChequeDate", typeof(DateTime));
			dataTable.Columns.Add("Status");
			dataTable.Columns.Add("Amount");
			dataTable.Columns.Add("UnclearedAmount");
			dataGridPayment.DataSource = dataTable;
			dataGridPayment.DisplayLayout.Bands[0].Columns["PayType"].Header.Caption = "PayType";
			dataGridPayment.DisplayLayout.Bands[0].Columns["SysDocID"].Header.Caption = "Doc ID";
			dataGridPayment.DisplayLayout.Bands[0].Columns["VoucherID"].Header.Caption = "Number";
			dataGridPayment.DisplayLayout.Bands[0].Columns["TransactionDate"].Header.Caption = "Transaction Date";
			dataGridPayment.DisplayLayout.Bands[0].Columns["Amount"].Header.Caption = "Amount";
			dataGridPayment.DisplayLayout.Bands[0].Columns["ChequeNo"].Header.Caption = "ChequeNo";
			dataGridPayment.DisplayLayout.Bands[0].Columns["ChequeDate"].Header.Caption = "ChequeDate";
			dataGridPayment.DisplayLayout.Bands[0].Columns["TransactionDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridPayment.DisplayLayout.Bands[0].Columns["ChequeDate"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Date;
			dataGridPayment.DisplayLayout.Bands[0].Columns["Status"].Header.Caption = "Status";
			dataGridPayment.DisplayLayout.Bands[0].Columns["UnclearedAmount"].Header.Caption = "PendingAmount";
			HAlign hAlign3 = dataGridPayment.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = (dataGridPayment.DisplayLayout.Bands[0].Columns["UnclearedAmount"].CellAppearance.TextHAlign = HAlign.Right);
			string text2 = dataGridPayment.DisplayLayout.Bands[0].Columns["Amount"].Format = (dataGridPayment.DisplayLayout.Bands[0].Columns["UnclearedAmount"].Format = Format.GridAmountFormat);
			dataGridPayment.DisplayLayout.Override.HeaderClickAction = HeaderClickAction.Select;
			dataGridPayment.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
			dataGridPayment.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridPayment.DisplayLayout.Bands[0].Columns["UnclearedAmount"], SummaryPosition.UseSummaryPositionColumn);
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridIncome.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					ClearForm();
					currentData = Factory.PropertyRentSystem.GetPropertyRentByID(SystemDocID, voucherID);
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
				isDataLoading = true;
				if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
				{
					DataRow dataRow = currentData.Tables["Property_Rent"].Rows[0];
					dateTimeTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimeStartDate.Value = DateTime.Parse(dataRow["ContractStartDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
					comboBoxPropertyUnit.SelectedID = dataRow["UnitID"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					textBoxTotalDays.Text = dataRow["TotalDays"].ToString();
					dateTimeEndDate.Value = DateTime.Parse(dataRow["ContractEndDate"].ToString());
					dateTimeStartDate.Value = DateTime.Parse(dataRow["ContractStartDate"].ToString());
					NumericNoofinstallments.Value = int.Parse(dataRow["NoofInstallments"].ToString());
					TimeSpan timeSpan = dateTimeEndDate.Value - dateTimeStartDate.Value;
					textBoxTotalDays.Text = timeSpan.Days.ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					if (dataRow["TaxAmount"] != DBNull.Value)
					{
						textBoxTaxAmount.Text = decimal.Parse(dataRow["TaxAmount"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxTaxAmount.Text = decimal.Parse("0").ToString(Format.TotalAmountFormat);
					}
					_agreestatus = dataRow["AgreementStatus"].ToString();
					if (!string.IsNullOrEmpty(dataRow["PropertyAgentID"].ToString()))
					{
						comboBoxPropertyAgent.SelectedID = dataRow["PropertyAgentID"].ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["InvoiceStartDate"].ToString()))
					{
						dateTimePickerInvoiceDate.Value = DateTime.Parse(dataRow["InvoiceStartDate"].ToString());
						dateTimePickerInvoiceDate.Checked = true;
					}
					else
					{
						dateTimePickerInvoiceDate.Checked = false;
					}
					if (!string.IsNullOrEmpty(dataRow["InvoiceStartDate"].ToString()))
					{
						isPeriodicInvoice = bool.Parse(dataRow["IsPeriodicInvoice"].ToString());
						if (isPeriodicInvoice)
						{
							groupBoxPeriodicInvoice.Visible = true;
							checkBoxPeriodicInvoice.Checked = bool.Parse(dataRow["IsPeriodicInvoice"].ToString());
							comboBoxFrequency.SelectedItem = dataRow["Frequency"].ToString().TrimEnd();
							string text = dataRow["FrequencyCount"].ToString();
							comboBoxFrequencyCount.Text = text;
						}
					}
					else
					{
						isPeriodicInvoice = false;
						groupBoxPeriodicInvoice.Visible = false;
					}
					DataTable dataTable = dataGridIncome.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Property_Rent_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Income Code"] = row["IncomeID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Reference"] = row["Reference"];
						if (row["UnitPrice"] != DBNull.Value)
						{
							dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
						}
						if (row["Discount"] != DBNull.Value)
						{
							dataRow3["Discount"] = decimal.Parse(row["Discount"].ToString()).ToString(Format.UnitPriceFormat);
						}
						if (row["TaxOption"] != DBNull.Value)
						{
							dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
						}
						else
						{
							dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
						}
						if (!row["TaxGroupID"].IsDBNullOrEmpty())
						{
							dataRow3["TaxGroupID"] = row["TaxGroupID"];
						}
						dataRow3["Amount"] = row["Amount"];
						if (row["TaxAmount"] != DBNull.Value)
						{
							dataRow3["Tax"] = decimal.Parse(row["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					foreach (UltraGridRow row2 in dataGridIncome.Rows)
					{
						DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = " + row2.Index + " AND TaxLevel = " + (byte)2);
						if (array.Length != 0)
						{
							TaxTransactionData taxTransactionData = new TaxTransactionData();
							taxTransactionData.Merge(array);
							row2.Cells["Tax"].Tag = taxTransactionData;
						}
					}
					DataRow[] array2 = currentData.TaxDetailsTable.Select("RowIndex = -1 AND TaxLevel = " + (byte)1);
					if (array2.Length != 0)
					{
						TaxTransactionData taxTransactionData2 = new TaxTransactionData();
						taxTransactionData2.Merge(array2);
						textBoxTaxAmount.Tag = taxTransactionData2;
					}
					CalculateTotalCost();
					if (isrecurring)
					{
						recurringInvoiceData = new RecurringInvoiceData();
						recurringInvoiceData = Factory.RecurringInvoiceSystem.GetRecurringInvoiceByID(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
						if (recurringInvoiceData.Tables["Recurring_Transaction"].Rows.Count > 0)
						{
							DataRow dataRow4 = recurringInvoiceData.Tables["Recurring_Transaction"].Rows[0];
							string sysDocID = dataRow4["SysDocID"].ToString();
							string voucherID = dataRow4["VoucherID"].ToString();
							propertyServiceInvoiceData = new SalesInvoiceNIData();
							propertyServiceInvoiceData = Factory.SalesInvoiceNISystem.GetServiceInvoiceByID(sysDocID, voucherID);
						}
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				isDataLoading = false;
			}
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
				bool flag = Factory.PropertyRentSystem.CreatePropertyRent(currentData, !isNewRecord);
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
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimeTransactionDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			if (timeSpan.Days <= checked(num + 1))
			{
				flag = true;
			}
			else if (Global.isUserAdmin)
			{
				flag = true;
			}
			else if (num == 0)
			{
				flag = true;
			}
			if (isNewRecord && dateTimeTransactionDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
				return false;
			}
			if (!flag)
			{
				ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
				return false;
			}
			if (isNewRecord && dateTimeTransactionDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxProperty.SelectedID == "" || comboBoxPropertyUnit.SelectedID == "" || comboBoxCustomer.SelectedID == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
				return false;
			}
			if (dateTimeStartDate.Value > dateTimeEndDate.Value)
			{
				ErrorHelper.InformationMessage("Contract Start date cannot be greater than End date.");
				dateTimeEndDate.Focus();
				return false;
			}
			if (dataGridIncome.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("There should be at least one row of item.");
				return false;
			}
			if (AgreementStatus == "2")
			{
				ErrorHelper.InformationMessage("The doument is already Renewed.You are not allowed to modify.");
				return false;
			}
			if (AgreementStatus == "3")
			{
				ErrorHelper.InformationMessage("The doument is already Cancelled.You are not allowed to modify.");
				return false;
			}
			foreach (UltraGridRow row in dataGridIncome.Rows)
			{
				if (row.Cells["Income Code"].Value == null || !(row.Cells["Income Code"].Value.ToString() != string.Empty))
				{
					ErrorHelper.InformationMessage("Please select an Income code for a row");
					return false;
				}
			}
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Property_Rent", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				labelTotalCost.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxPropertyUnit.Clear();
				comboBoxProperty.Clear();
				comboBoxCustomer.Clear();
				textBoxCustomerName.Clear();
				NumericNoofinstallments.Value = 1m;
				textBoxTotalDays.Clear();
				textBoxTotalDays.Clear();
				textBoxPropertyName.Clear();
				dateTimeTransactionDate.Value = DateTime.Now;
				dateTimeStartDate.Value = DateTime.Now;
				dateTimeEndDate.Value = DateTime.Now;
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxPropertyAgent.Clear();
				comboBoxPropertyUnit.FilterByPropertyId("");
				(dataGridIncome.DataSource as DataTable).Rows.Clear();
				(dataGridPayment.DataSource as DataTable).Rows.Clear();
				comboBoxPayeeTaxGroup.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				isrecurring = true;
				isPeriodicInvoice = false;
				propertyServiceInvoiceData = null;
				groupBoxPeriodicInvoice.Visible = false;
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
			else
			{
				ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
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
				return Factory.PropertyRentSystem.DeletePropertyRent(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Property_Rent", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Property_Rent", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Property_Rent", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Property_Rent", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Property_Rent", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
			else
			{
				dataGridIncome.SaveLayout();
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

		private void WorkOrderForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridIncome.SetupUI();
				SetupIncomeGrid();
				dataGridPayment.SetupUI();
				SetupPaymentGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyRental);
				dateTimeTransactionDate.Value = DateTime.Now;
				comboBoxPropertyUnit.FilterByVirtual(isVirtualOnly: true);
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
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyRental);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PropertyRental);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 101.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PropertyRental);
					currentData = (dataSet as PropertyRentData);
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
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				DataSet propertyRentToPrint = Factory.PropertyRentSystem.GetPropertyRentToPrint(selectedID, text);
				if (propertyRentToPrint == null || propertyRentToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(propertyRentToPrint, selectedID, "Rental Regestration", SysDocTypes.PropertyRental, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void printListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PrintList();
		}

		private string GetDocumentTitle()
		{
			return "Inventory Adjustment - Products List";
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditProperty(comboBoxProperty.SelectedID);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void buttonPayCash_Click(object sender, EventArgs e)
		{
			try
			{
				CashReceiptForm cashReceiptForm = new CashReceiptForm();
				cashReceiptForm.EntityID = comboBoxCustomer.SelectedID;
				cashReceiptForm.EntityType = "C";
				cashReceiptForm.SourceSysDocID = comboBoxSysDoc.SelectedID;
				cashReceiptForm.SourceVoucherID = textBoxVoucherNumber.Text;
				cashReceiptForm.Reference = textBoxVoucherNumber.Text;
				cashReceiptForm.Description = "Rental Registration for the tenant :" + comboBoxCustomer.SelectedID;
				cashReceiptForm.Unit = comboBoxPropertyUnit.SelectedID;
				cashReceiptForm.Property = comboBoxProperty.SelectedID;
				cashReceiptForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonPayCheque_Click(object sender, EventArgs e)
		{
			try
			{
				ChequeReceiptForm chequeReceiptForm = new ChequeReceiptForm();
				chequeReceiptForm.EntityID = comboBoxCustomer.SelectedID;
				chequeReceiptForm.EntityType = "C";
				chequeReceiptForm.SourceSysDocID = comboBoxSysDoc.SelectedID;
				chequeReceiptForm.SourceVoucherID = textBoxVoucherNumber.Text;
				chequeReceiptForm.Reference = textBoxVoucherNumber.Text;
				chequeReceiptForm.Description = "Rental Registration for the tenant :" + comboBoxCustomer.SelectedID;
				chequeReceiptForm.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxTotalDays_TabIndexChanged(object sender, EventArgs e)
		{
		}

		private void textBoxTotalDays_Leave(object sender, EventArgs e)
		{
			TimeSpan timeSpan = dateTimeEndDate.Value - dateTimeStartDate.Value;
			textBoxTotalDays.Text = timeSpan.Days.ToString();
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTenant(comboBoxCustomer.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyRentListFormObj);
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

		private void dateTimeEndDate_TabIndexChanged(object sender, EventArgs e)
		{
			TimeSpan timeSpan = dateTimeEndDate.Value - dateTimeStartDate.Value;
			textBoxTotalDays.Text = timeSpan.Days.ToString();
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyUnit(comboBoxPropertyUnit.SelectedID);
		}

		private void textBoxPrevDocumentNo_Update()
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataSet = Factory.PropertyCancelSystem.GetPropertypaymentdetails(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					DataTable dataTable = dataGridPayment.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in dataSet.Tables["Property_Payment"].Rows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["PayType"] = row["payType"];
						dataRow2["SysDocID"] = row["SysDocID"];
						dataRow2["VoucherID"] = row["VoucherID"];
						dataRow2["TransactionDate"] = row["TransactionDate"];
						dataRow2["Amount"] = row["Amount"];
						dataRow2["ChequeNo"] = row["ChequeNumber"];
						dataRow2["ChequeDate"] = row["ChequeDate"];
						dataRow2["Status"] = row["Status"];
						if (row["ChequeDate"].ToString() != null && row["ChequeDate"].ToString() != string.Empty)
						{
							if (row["Status"].ToString() != "Cleared")
							{
								dataRow2["UnclearedAmount"] = row["Amount"];
							}
							else
							{
								dataRow2["UnclearedAmount"] = "0.0";
							}
						}
						else
						{
							dataRow2["UnclearedAmount"] = "0.0";
						}
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void textBoxVoucherNumber_TextChanged(object sender, EventArgs e)
		{
			if (textBoxVoucherNumber.Text != "")
			{
				textBoxPrevDocumentNo_Update();
			}
		}

		private void ultraLabel1_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void linkLabelTax_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			try
			{
				TaxDistibutionDialog taxDistibutionDialog = new TaxDistibutionDialog();
				TaxTransactionData taxData = new TaxTransactionData();
				if (textBoxTaxAmount.Tag != null)
				{
					taxData = (textBoxTaxAmount.Tag as TaxTransactionData);
				}
				taxDistibutionDialog.TaxData = taxData;
				taxDistibutionDialog.ShowDialog(this);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void labelTaxGroup_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
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

		private void ultraFormattedLinkLabelAgent_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyAgent(comboBoxPropertyAgent.SelectedID);
		}

		private void comboBoxFrequency_SelectedValueChanged(object sender, EventArgs e)
		{
			if (comboBoxFrequency.SelectedIndex < 0)
			{
				return;
			}
			checked
			{
				if (comboBoxFrequency.SelectedItem.ToString() == "M")
				{
					comboBoxFrequencyCount.Items.Clear();
					for (int i = 1; i < 5; i++)
					{
						comboBoxFrequencyCount.Items.Add(i);
					}
				}
				else if (comboBoxFrequency.SelectedItem.ToString() == "D")
				{
					comboBoxFrequencyCount.Items.Clear();
					for (int j = 1; j <= 30; j++)
					{
						comboBoxFrequencyCount.Items.Add(j);
					}
				}
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			new RecurringTransactionPostForm().Show();
		}

		private void xpButton2_Click_1(object sender, EventArgs e)
		{
			new RecurringTransactionPostForm().Show();
		}

		private void dateTimePickerInvoiceDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void xpButton2_Click_2(object sender, EventArgs e)
		{
		}

		private void checkBoxPeriodicInvoice_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxPeriodicInvoice.Checked)
			{
				dateTimePickerInvoiceDate.Checked = true;
				return;
			}
			dateTimePickerInvoiceDate.Checked = false;
			comboBoxFrequency.SelectedIndex = -1;
			comboBoxFrequencyCount.SelectedIndex = -1;
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.PropertyRental;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyRentDetailsForm));
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridIncome = new Micromind.DataControls.DataEntryGrid();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			comboBoxGridIncomeCode = new Micromind.DataControls.PropertyIncomeCodeComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridPayment = new Micromind.DataControls.DataEntryGrid();
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
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			dateTimeTransactionDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			labelTotalCost = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			ultraFormattedLinkCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			groupBoxPeriodicInvoice = new System.Windows.Forms.GroupBox();
			comboBoxFrequency = new System.Windows.Forms.ComboBox();
			label9 = new System.Windows.Forms.Label();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			comboBoxFrequencyCount = new Micromind.DataControls.MonthComboBox();
			dateTimePickerInvoiceDate = new Micromind.UISupport.MMSDateTimePicker(components);
			mmLabel38 = new Micromind.UISupport.MMLabel();
			checkBoxPeriodicInvoice = new System.Windows.Forms.CheckBox();
			comboBoxPropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			ultraFormattedLinkLabelAgent = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			NumericNoofinstallments = new System.Windows.Forms.NumericUpDown();
			textBoxUnit = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxPropertyName = new System.Windows.Forms.TextBox();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			label5 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label6 = new System.Windows.Forms.Label();
			textBoxTotalDays = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			dateTimeEndDate = new System.Windows.Forms.DateTimePicker();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			comboBoxPropertyUnit = new Micromind.DataControls.PropertyUnitComboBox();
			comboBoxProperty = new Micromind.DataControls.PropertyComboBox();
			dateTimeStartDate = new System.Windows.Forms.DateTimePicker();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			dataSet1 = new System.Data.DataSet();
			groupBox1 = new System.Windows.Forms.GroupBox();
			buttonPayCash = new Micromind.UISupport.XPButton();
			buttonPayCheque = new Micromind.UISupport.XPButton();
			TestrichTextBox = new System.Windows.Forms.RichTextBox();
			panel1 = new System.Windows.Forms.Panel();
			panelNonTax = new System.Windows.Forms.Panel();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			bomComboBox2 = new Micromind.DataControls.BOMComboBox();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridIncome).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayment).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			groupBoxPeriodicInvoice.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)NumericNoofinstallments).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataSet1).BeginInit();
			groupBox1.SuspendLayout();
			panel1.SuspendLayout();
			panelNonTax.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).BeginInit();
			SuspendLayout();
			tabPageExpense.Controls.Add(dataGridIncome);
			tabPageExpense.Controls.Add(comboBoxGridIncomeCode);
			tabPageExpense.Location = new System.Drawing.Point(1, 23);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(964, 215);
			dataGridIncome.AllowAddNew = true;
			dataGridIncome.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridIncome.CalcManager = ultraCalcManager1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridIncome.DisplayLayout.Appearance = appearance;
			dataGridIncome.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridIncome.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridIncome.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridIncome.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridIncome.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridIncome.DisplayLayout.MaxColScrollRegions = 1;
			dataGridIncome.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridIncome.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridIncome.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridIncome.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridIncome.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridIncome.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridIncome.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridIncome.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridIncome.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridIncome.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridIncome.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridIncome.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridIncome.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridIncome.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridIncome.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridIncome.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridIncome.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridIncome.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridIncome.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridIncome.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridIncome.ExitEditModeOnLeave = false;
			dataGridIncome.IncludeLotItems = false;
			dataGridIncome.LoadLayoutFailed = false;
			dataGridIncome.Location = new System.Drawing.Point(-1, 0);
			dataGridIncome.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridIncome.Name = "dataGridIncome";
			dataGridIncome.ShowClearMenu = true;
			dataGridIncome.ShowDeleteMenu = true;
			dataGridIncome.ShowInsertMenu = true;
			dataGridIncome.ShowMoveRowsMenu = true;
			dataGridIncome.Size = new System.Drawing.Size(962, 215);
			dataGridIncome.TabIndex = 0;
			dataGridIncome.Text = "dataEntryGrid1";
			ultraCalcManager1.ContainingControl = this;
			comboBoxGridIncomeCode.AlwaysInEditMode = true;
			comboBoxGridIncomeCode.Assigned = false;
			comboBoxGridIncomeCode.CalcManager = ultraCalcManager1;
			comboBoxGridIncomeCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridIncomeCode.CustomReportFieldName = "";
			comboBoxGridIncomeCode.CustomReportKey = "";
			comboBoxGridIncomeCode.CustomReportValueType = 1;
			comboBoxGridIncomeCode.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridIncomeCode.DisplayLayout.Appearance = appearance13;
			comboBoxGridIncomeCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridIncomeCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridIncomeCode.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridIncomeCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridIncomeCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridIncomeCode.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridIncomeCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridIncomeCode.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridIncomeCode.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridIncomeCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridIncomeCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridIncomeCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridIncomeCode.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridIncomeCode.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridIncomeCode.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridIncomeCode.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridIncomeCode.Editable = true;
			comboBoxGridIncomeCode.FilterString = "";
			comboBoxGridIncomeCode.HasAllAccount = false;
			comboBoxGridIncomeCode.HasCustom = false;
			comboBoxGridIncomeCode.IsDataLoaded = false;
			comboBoxGridIncomeCode.Location = new System.Drawing.Point(539, 66);
			comboBoxGridIncomeCode.MaxDropDownItems = 12;
			comboBoxGridIncomeCode.Name = "comboBoxGridIncomeCode";
			comboBoxGridIncomeCode.ShowInactiveItems = false;
			comboBoxGridIncomeCode.ShowQuickAdd = true;
			comboBoxGridIncomeCode.Size = new System.Drawing.Size(100, 20);
			comboBoxGridIncomeCode.TabIndex = 136;
			comboBoxGridIncomeCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridIncomeCode.Visible = false;
			ultraTabPageControl1.Controls.Add(dataGridPayment);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(964, 215);
			dataGridPayment.AllowAddNew = true;
			dataGridPayment.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridPayment.CalcManager = ultraCalcManager1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayment.DisplayLayout.Appearance = appearance25;
			dataGridPayment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridPayment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridPayment.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayment.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayment.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridPayment.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridPayment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayment.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridPayment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayment.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridPayment.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridPayment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridPayment.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridPayment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayment.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridPayment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayment.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridPayment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayment.ExitEditModeOnLeave = false;
			dataGridPayment.IncludeLotItems = false;
			dataGridPayment.LoadLayoutFailed = false;
			dataGridPayment.Location = new System.Drawing.Point(-1, -1);
			dataGridPayment.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridPayment.Name = "dataGridPayment";
			dataGridPayment.ShowClearMenu = true;
			dataGridPayment.ShowDeleteMenu = true;
			dataGridPayment.ShowInsertMenu = true;
			dataGridPayment.ShowMoveRowsMenu = true;
			dataGridPayment.Size = new System.Drawing.Size(965, 240);
			dataGridPayment.TabIndex = 4;
			dataGridPayment.Text = "dataEntryGrid1";
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
				toolStripButton1,
				toolStripSeparator5,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(979, 31);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButton1.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(91, 28);
			toolStripButton1.Text = "Attach File";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonMultiPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonMultiPreview.Image = Micromind.ClientUI.Properties.Resources.multi_doc;
			toolStripButtonMultiPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMultiPreview.Name = "toolStripButtonMultiPreview";
			toolStripButtonMultiPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonMultiPreview.Text = "MultiPreview";
			toolStripButtonMultiPreview.Click += new System.EventHandler(toolStripButtonMultiPreview_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
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
			panelButtons.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Location = new System.Drawing.Point(0, 576);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(979, 40);
			panelButtons.TabIndex = 5;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 19;
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
			buttonDelete.TabIndex = 18;
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
			buttonNew.TabIndex = 17;
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
			linePanelDown.Size = new System.Drawing.Size(979, 1);
			linePanelDown.TabIndex = 1;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(868, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 20;
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
			buttonSave.TabIndex = 16;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			dateTimeTransactionDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeTransactionDate.Location = new System.Drawing.Point(595, 3);
			dateTimeTransactionDate.Name = "dateTimeTransactionDate";
			dateTimeTransactionDate.Size = new System.Drawing.Size(116, 20);
			dateTimeTransactionDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(368, 5);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(113, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxVoucherNumber.TextChanged += new System.EventHandler(textBoxVoucherNumber_TextChanged);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(66, 459);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(379, 89);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(20, 482);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance37.FontData.BoldAsString = "True";
			appearance37.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance37;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(248, 7);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance38.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance38;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance39.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance39.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance39;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.Controls.Add(labelTotalCost);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 432);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(966, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance40.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance40.FontData.BoldAsString = "True";
			appearance40.FontData.Name = "Tahoma";
			appearance40.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance40.TextHAlignAsString = "Right";
			appearance40.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance40;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(1, 2);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(824, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			ultraLabel1.Click += new System.EventHandler(ultraLabel1_Click);
			labelTotalCost.Dock = System.Windows.Forms.DockStyle.Right;
			labelTotalCost.Location = new System.Drawing.Point(820, 2);
			labelTotalCost.Name = "labelTotalCost";
			labelTotalCost.Size = new System.Drawing.Size(144, 17);
			labelTotalCost.TabIndex = 139;
			labelTotalCost.Text = "0.00";
			labelTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			panelDetails.Controls.Add(ultraFormattedLinkCurrency);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(groupBoxPeriodicInvoice);
			panelDetails.Controls.Add(comboBoxPropertyAgent);
			panelDetails.Controls.Add(ultraFormattedLinkLabelAgent);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(NumericNoofinstallments);
			panelDetails.Controls.Add(textBoxUnit);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(textBoxPropertyName);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxTotalDays);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(dateTimeEndDate);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(comboBoxPropertyUnit);
			panelDetails.Controls.Add(comboBoxProperty);
			panelDetails.Controls.Add(dateTimeStartDate);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimeTransactionDate);
			panelDetails.Location = new System.Drawing.Point(10, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(962, 152);
			panelDetails.TabIndex = 0;
			appearance41.FontData.BoldAsString = "False";
			appearance41.FontData.Name = "Tahoma";
			ultraFormattedLinkCurrency.Appearance = appearance41;
			ultraFormattedLinkCurrency.AutoSize = true;
			ultraFormattedLinkCurrency.Location = new System.Drawing.Point(725, 48);
			ultraFormattedLinkCurrency.Name = "ultraFormattedLinkCurrency";
			ultraFormattedLinkCurrency.Size = new System.Drawing.Size(52, 15);
			ultraFormattedLinkCurrency.TabIndex = 168;
			ultraFormattedLinkCurrency.TabStop = true;
			ultraFormattedLinkCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkCurrency.Value = "Currency:";
			ultraFormattedLinkCurrency.Visible = false;
			appearance42.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkCurrency.VisitedLinkAppearance = appearance42;
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(814, 44);
			comboBoxCurrency.MaximumSize = new System.Drawing.Size(99999, 20);
			comboBoxCurrency.MinimumSize = new System.Drawing.Size(5, 20);
			comboBoxCurrency.Name = "comboBoxCurrency";
			comboBoxCurrency.Rate = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			comboBoxCurrency.SelectedID = "";
			comboBoxCurrency.Size = new System.Drawing.Size(128, 20);
			comboBoxCurrency.TabIndex = 167;
			comboBoxCurrency.Visible = false;
			groupBoxPeriodicInvoice.Controls.Add(comboBoxFrequency);
			groupBoxPeriodicInvoice.Controls.Add(label9);
			groupBoxPeriodicInvoice.Controls.Add(mmLabel2);
			groupBoxPeriodicInvoice.Controls.Add(comboBoxFrequencyCount);
			groupBoxPeriodicInvoice.Controls.Add(dateTimePickerInvoiceDate);
			groupBoxPeriodicInvoice.Controls.Add(mmLabel38);
			groupBoxPeriodicInvoice.Controls.Add(checkBoxPeriodicInvoice);
			groupBoxPeriodicInvoice.Location = new System.Drawing.Point(725, 69);
			groupBoxPeriodicInvoice.Name = "groupBoxPeriodicInvoice";
			groupBoxPeriodicInvoice.Size = new System.Drawing.Size(215, 80);
			groupBoxPeriodicInvoice.TabIndex = 166;
			groupBoxPeriodicInvoice.TabStop = false;
			groupBoxPeriodicInvoice.Visible = false;
			comboBoxFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFrequency.FormattingEnabled = true;
			comboBoxFrequency.Items.AddRange(new object[2]
			{
				"M",
				"D"
			});
			comboBoxFrequency.Location = new System.Drawing.Point(70, 50);
			comboBoxFrequency.Name = "comboBoxFrequency";
			comboBoxFrequency.Size = new System.Drawing.Size(39, 21);
			comboBoxFrequency.TabIndex = 170;
			comboBoxFrequency.SelectedValueChanged += new System.EventHandler(comboBoxFrequency_SelectedValueChanged);
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(14, 46);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(42, 26);
			label9.TabIndex = 169;
			label9.Text = "Month/\r\nDay:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(120, 53);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(37, 13);
			mmLabel2.TabIndex = 165;
			mmLabel2.Text = "Every:";
			comboBoxFrequencyCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxFrequencyCount.FormattingEnabled = true;
			comboBoxFrequencyCount.IsMonthNumbers = true;
			comboBoxFrequencyCount.Location = new System.Drawing.Point(163, 50);
			comboBoxFrequencyCount.Name = "comboBoxFrequencyCount";
			comboBoxFrequencyCount.Size = new System.Drawing.Size(46, 21);
			comboBoxFrequencyCount.TabIndex = 164;
			dateTimePickerInvoiceDate.Checked = false;
			dateTimePickerInvoiceDate.CustomFormat = " ";
			dateTimePickerInvoiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerInvoiceDate.Location = new System.Drawing.Point(70, 24);
			dateTimePickerInvoiceDate.Name = "dateTimePickerInvoiceDate";
			dateTimePickerInvoiceDate.ShowCheckBox = true;
			dateTimePickerInvoiceDate.Size = new System.Drawing.Size(139, 20);
			dateTimePickerInvoiceDate.TabIndex = 160;
			dateTimePickerInvoiceDate.Value = new System.DateTime(0L);
			dateTimePickerInvoiceDate.ValueChanged += new System.EventHandler(dateTimePickerInvoiceDate_ValueChanged);
			mmLabel38.AutoSize = true;
			mmLabel38.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel38.IsFieldHeader = false;
			mmLabel38.IsRequired = false;
			mmLabel38.Location = new System.Drawing.Point(5, 26);
			mmLabel38.Name = "mmLabel38";
			mmLabel38.PenWidth = 1f;
			mmLabel38.ShowBorder = false;
			mmLabel38.Size = new System.Drawing.Size(58, 13);
			mmLabel38.TabIndex = 161;
			mmLabel38.Text = "Start Date:";
			checkBoxPeriodicInvoice.AutoSize = true;
			checkBoxPeriodicInvoice.Location = new System.Drawing.Point(22, 0);
			checkBoxPeriodicInvoice.Name = "checkBoxPeriodicInvoice";
			checkBoxPeriodicInvoice.Size = new System.Drawing.Size(152, 17);
			checkBoxPeriodicInvoice.TabIndex = 140;
			checkBoxPeriodicInvoice.Text = "Based On Periodic Invoice";
			checkBoxPeriodicInvoice.UseVisualStyleBackColor = true;
			checkBoxPeriodicInvoice.CheckedChanged += new System.EventHandler(checkBoxPeriodicInvoice_CheckedChanged);
			comboBoxPropertyAgent.Assigned = false;
			comboBoxPropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyAgent.CalcManager = ultraCalcManager1;
			comboBoxPropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyAgent.CustomReportFieldName = "";
			comboBoxPropertyAgent.CustomReportKey = "";
			comboBoxPropertyAgent.CustomReportValueType = 1;
			comboBoxPropertyAgent.DescriptionTextBox = null;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyAgent.DisplayLayout.Appearance = appearance43;
			comboBoxPropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance44.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance44.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance44.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance44;
			appearance45.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance45;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance46.BackColor2 = System.Drawing.SystemColors.Control;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance46.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance46;
			comboBoxPropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance47;
			appearance48.BackColor = System.Drawing.SystemColors.Highlight;
			appearance48.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance48;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance49;
			appearance50.BorderColor = System.Drawing.Color.Silver;
			appearance50.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyAgent.DisplayLayout.Override.CellAppearance = appearance50;
			comboBoxPropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance51.BackColor = System.Drawing.SystemColors.Control;
			appearance51.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance51.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance51.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance51.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance51;
			appearance52.TextHAlignAsString = "Left";
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance52;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyAgent.DisplayLayout.Override.RowAppearance = appearance53;
			comboBoxPropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance54;
			comboBoxPropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyAgent.Editable = true;
			comboBoxPropertyAgent.FilterString = "";
			comboBoxPropertyAgent.HasAllAccount = false;
			comboBoxPropertyAgent.HasCustom = false;
			comboBoxPropertyAgent.IsDataLoaded = false;
			comboBoxPropertyAgent.Location = new System.Drawing.Point(595, 123);
			comboBoxPropertyAgent.MaxDropDownItems = 12;
			comboBoxPropertyAgent.Name = "comboBoxPropertyAgent";
			comboBoxPropertyAgent.ShowInactiveItems = false;
			comboBoxPropertyAgent.ShowQuickAdd = true;
			comboBoxPropertyAgent.Size = new System.Drawing.Size(116, 20);
			comboBoxPropertyAgent.TabIndex = 12;
			comboBoxPropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance55.FontData.BoldAsString = "False";
			appearance55.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelAgent.Appearance = appearance55;
			ultraFormattedLinkLabelAgent.AutoSize = true;
			ultraFormattedLinkLabelAgent.Location = new System.Drawing.Point(552, 125);
			ultraFormattedLinkLabelAgent.Name = "ultraFormattedLinkLabelAgent";
			ultraFormattedLinkLabelAgent.Size = new System.Drawing.Size(37, 15);
			ultraFormattedLinkLabelAgent.TabIndex = 164;
			ultraFormattedLinkLabelAgent.TabStop = true;
			ultraFormattedLinkLabelAgent.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelAgent.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelAgent.Value = "Agent:";
			appearance56.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelAgent.VisitedLinkAppearance = appearance56;
			ultraFormattedLinkLabelAgent.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelAgent_LinkClicked);
			appearance57.FontData.BoldAsString = "False";
			appearance57.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance57;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(7, 103);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 162;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance58;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelTaxGroup_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CalcManager = ultraCalcManager1;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance59;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(111, 100);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(128, 20);
			comboBoxPayeeTaxGroup.TabIndex = 6;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			NumericNoofinstallments.Location = new System.Drawing.Point(595, 99);
			NumericNoofinstallments.Name = "NumericNoofinstallments";
			NumericNoofinstallments.Size = new System.Drawing.Size(41, 20);
			NumericNoofinstallments.TabIndex = 11;
			NumericNoofinstallments.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			textBoxUnit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnit.Location = new System.Drawing.Point(248, 75);
			textBoxUnit.MaxLength = 64;
			textBoxUnit.Name = "textBoxUnit";
			textBoxUnit.ReadOnly = true;
			textBoxUnit.Size = new System.Drawing.Size(233, 20);
			textBoxUnit.TabIndex = 154;
			textBoxUnit.TabStop = false;
			appearance71.FontData.BoldAsString = "True";
			appearance71.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance71;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(7, 79);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(31, 15);
			ultraFormattedLinkLabel4.TabIndex = 153;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Unit:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance72;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			textBoxPropertyName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPropertyName.Location = new System.Drawing.Point(248, 27);
			textBoxPropertyName.MaxLength = 64;
			textBoxPropertyName.Name = "textBoxPropertyName";
			textBoxPropertyName.ReadOnly = true;
			textBoxPropertyName.Size = new System.Drawing.Size(233, 20);
			textBoxPropertyName.TabIndex = 152;
			textBoxPropertyName.TabStop = false;
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			comboBoxSysDoc.DivisionID = "";
			comboBoxSysDoc.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSysDoc.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxSysDoc.Editable = true;
			comboBoxSysDoc.ExcludeFromSecurity = false;
			comboBoxSysDoc.FilterString = "";
			comboBoxSysDoc.HasAllAccount = false;
			comboBoxSysDoc.HasCustom = false;
			comboBoxSysDoc.IsDataLoaded = false;
			comboBoxSysDoc.Location = new System.Drawing.Point(111, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(489, 55);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(101, 13);
			label5.TabIndex = 151;
			label5.Text = "Contract End Date :\r\n";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(489, 30);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(101, 26);
			label1.TabIndex = 150;
			label1.Text = "Contract Start Date:\r\n\r\n";
			appearance73.FontData.BoldAsString = "True";
			appearance73.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance73;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(7, 55);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(47, 15);
			ultraFormattedLinkLabel3.TabIndex = 149;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Tenant:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(489, 79);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(61, 13);
			label6.TabIndex = 148;
			label6.Text = "Total Days:";
			textBoxTotalDays.Location = new System.Drawing.Point(595, 74);
			textBoxTotalDays.MaxLength = 255;
			textBoxTotalDays.Name = "textBoxTotalDays";
			textBoxTotalDays.ReadOnly = true;
			textBoxTotalDays.Size = new System.Drawing.Size(77, 20);
			textBoxTotalDays.TabIndex = 10;
			textBoxTotalDays.TabIndexChanged += new System.EventHandler(textBoxTotalDays_TabIndexChanged);
			textBoxTotalDays.Leave += new System.EventHandler(textBoxTotalDays_Leave);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(489, 102);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(103, 13);
			label2.TabIndex = 146;
			label2.Text = "No: of  Installments :\r\n";
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(248, 51);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(233, 20);
			textBoxCustomerName.TabIndex = 143;
			textBoxCustomerName.TabStop = false;
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeEndDate.Location = new System.Drawing.Point(595, 51);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.Size = new System.Drawing.Size(116, 20);
			dateTimeEndDate.TabIndex = 9;
			dateTimeEndDate.TabIndexChanged += new System.EventHandler(dateTimeEndDate_TabIndexChanged);
			comboBoxCustomer.AlwaysInEditMode = true;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CalcManager = ultraCalcManager1;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance75;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(111, 52);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = true;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(128, 20);
			comboBoxCustomer.TabIndex = 4;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPropertyUnit.Assigned = false;
			comboBoxPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyUnit.CalcManager = ultraCalcManager1;
			comboBoxPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyUnit.CustomReportFieldName = "";
			comboBoxPropertyUnit.CustomReportKey = "";
			comboBoxPropertyUnit.CustomReportValueType = 1;
			comboBoxPropertyUnit.DescriptionTextBox = textBoxUnit;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			appearance87.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyUnit.DisplayLayout.Appearance = appearance87;
			comboBoxPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance88.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance88.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance88.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance88;
			appearance89.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance89;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance90.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance90.BackColor2 = System.Drawing.SystemColors.Control;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance90.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance90;
			comboBoxPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance91;
			appearance92.BackColor = System.Drawing.SystemColors.Highlight;
			appearance92.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance92;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance93;
			appearance94.BorderColor = System.Drawing.Color.Silver;
			appearance94.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyUnit.DisplayLayout.Override.CellAppearance = appearance94;
			comboBoxPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance95.BackColor = System.Drawing.SystemColors.Control;
			appearance95.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance95.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance95.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance95.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance95;
			appearance96.TextHAlignAsString = "Left";
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance96;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyUnit.DisplayLayout.Override.RowAppearance = appearance97;
			comboBoxPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance98.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance98;
			comboBoxPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyUnit.Editable = true;
			comboBoxPropertyUnit.FilterString = "";
			comboBoxPropertyUnit.HasAllAccount = false;
			comboBoxPropertyUnit.HasCustom = false;
			comboBoxPropertyUnit.IsDataLoaded = false;
			comboBoxPropertyUnit.Location = new System.Drawing.Point(111, 76);
			comboBoxPropertyUnit.MaxDropDownItems = 12;
			comboBoxPropertyUnit.Name = "comboBoxPropertyUnit";
			comboBoxPropertyUnit.ShowActiveOnly = true;
			comboBoxPropertyUnit.ShowInactiveItems = false;
			comboBoxPropertyUnit.ShowQuickAdd = true;
			comboBoxPropertyUnit.Size = new System.Drawing.Size(128, 20);
			comboBoxPropertyUnit.TabIndex = 5;
			comboBoxPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CalcManager = ultraCalcManager1;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = textBoxPropertyName;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance99;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance100.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance100.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance100.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance100;
			appearance101.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance101;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance102.BackColor2 = System.Drawing.SystemColors.Control;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance102.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance102;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance103;
			appearance104.BackColor = System.Drawing.SystemColors.Highlight;
			appearance104.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance104;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance105;
			appearance106.BorderColor = System.Drawing.Color.Silver;
			appearance106.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance106;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance107.BackColor = System.Drawing.SystemColors.Control;
			appearance107.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance107.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance107.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance107.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance107;
			appearance108.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance108;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance109;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance110;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(111, 28);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(128, 20);
			comboBoxProperty.TabIndex = 3;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeStartDate.Location = new System.Drawing.Point(595, 26);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.Size = new System.Drawing.Size(116, 20);
			dateTimeStartDate.TabIndex = 8;
			appearance111.FontData.BoldAsString = "True";
			appearance111.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance111;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(7, 31);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel1.TabIndex = 124;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Property:";
			appearance112.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance112;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance113.FontData.BoldAsString = "True";
			appearance113.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance113;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(7, 7);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance114.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance114;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(551, 4);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
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
			ultraGridPrintDocument1.SettingsKey = "BuildAssemblyForm.ultraGridPrintDocument1";
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.CausesValidation = false;
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Location = new System.Drawing.Point(10, 189);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(968, 241);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageExpense;
			ultraTab.Text = "Fees";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "Payment Details";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(964, 215);
			dataSet1.DataSetName = "NewDataSet";
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			groupBox1.Controls.Add(buttonPayCash);
			groupBox1.Controls.Add(buttonPayCheque);
			groupBox1.Location = new System.Drawing.Point(451, 494);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(256, 51);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Receipt";
			buttonPayCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCash.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCash.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCash.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCash.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCash.Location = new System.Drawing.Point(20, 21);
			buttonPayCash.Name = "buttonPayCash";
			buttonPayCash.Size = new System.Drawing.Size(96, 24);
			buttonPayCash.TabIndex = 21;
			buttonPayCash.Text = "Cash";
			buttonPayCash.UseVisualStyleBackColor = false;
			buttonPayCash.Click += new System.EventHandler(buttonPayCash_Click);
			buttonPayCheque.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCheque.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCheque.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCheque.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCheque.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCheque.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCheque.Location = new System.Drawing.Point(138, 21);
			buttonPayCheque.Name = "buttonPayCheque";
			buttonPayCheque.Size = new System.Drawing.Size(96, 24);
			buttonPayCheque.TabIndex = 22;
			buttonPayCheque.Text = "Cheque";
			buttonPayCheque.UseVisualStyleBackColor = false;
			buttonPayCheque.Click += new System.EventHandler(buttonPayCheque_Click);
			TestrichTextBox.Location = new System.Drawing.Point(787, 308);
			TestrichTextBox.Name = "TestrichTextBox";
			TestrichTextBox.Size = new System.Drawing.Size(100, 28);
			TestrichTextBox.TabIndex = 24;
			TestrichTextBox.Text = "";
			TestrichTextBox.Visible = false;
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(759, 461);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 78);
			panel1.TabIndex = 4;
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Controls.Add(label8);
			panelNonTax.Controls.Add(textBoxTotal);
			panelNonTax.Location = new System.Drawing.Point(0, 19);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 59);
			panelNonTax.TabIndex = 144;
			appearance115.FontData.BoldAsString = "False";
			appearance115.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance115;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(7, 4);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 166;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance116.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance116;
			linkLabelTax.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelTax_LinkClicked);
			textBoxTaxAmount.AllowDecimal = true;
			textBoxTaxAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTaxAmount.CustomReportFieldName = "";
			textBoxTaxAmount.CustomReportKey = "";
			textBoxTaxAmount.CustomReportValueType = 1;
			textBoxTaxAmount.IsComboTextBox = false;
			textBoxTaxAmount.IsModified = false;
			textBoxTaxAmount.Location = new System.Drawing.Point(80, 0);
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
			textBoxTaxAmount.Size = new System.Drawing.Size(138, 20);
			textBoxTaxAmount.TabIndex = 0;
			textBoxTaxAmount.Text = "0.00";
			textBoxTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTaxAmount.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(4, 26);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 133;
			label8.Text = "Total:";
			textBoxTotal.AllowDecimal = true;
			textBoxTotal.CustomReportFieldName = "";
			textBoxTotal.CustomReportKey = "";
			textBoxTotal.CustomReportValueType = 1;
			textBoxTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			textBoxTotal.IsComboTextBox = false;
			textBoxTotal.IsModified = false;
			textBoxTotal.Location = new System.Drawing.Point(80, 22);
			textBoxTotal.MaxLength = 15;
			textBoxTotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxTotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxTotal.Name = "textBoxTotal";
			textBoxTotal.NullText = "0";
			textBoxTotal.Size = new System.Drawing.Size(138, 20);
			textBoxTotal.TabIndex = 1;
			textBoxTotal.Text = "0.00";
			textBoxTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxTotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(4, 3);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(49, 13);
			label4.TabIndex = 133;
			label4.Text = "Subtotal:";
			textBoxSubtotal.AllowDecimal = true;
			textBoxSubtotal.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSubtotal.CustomReportFieldName = "";
			textBoxSubtotal.CustomReportKey = "";
			textBoxSubtotal.CustomReportValueType = 1;
			textBoxSubtotal.ForeColor = System.Drawing.Color.Black;
			textBoxSubtotal.IsComboTextBox = false;
			textBoxSubtotal.IsModified = false;
			textBoxSubtotal.Location = new System.Drawing.Point(80, -1);
			textBoxSubtotal.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxSubtotal.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxSubtotal.Name = "textBoxSubtotal";
			textBoxSubtotal.NullText = "0";
			textBoxSubtotal.ReadOnly = true;
			textBoxSubtotal.Size = new System.Drawing.Size(138, 20);
			textBoxSubtotal.TabIndex = 0;
			textBoxSubtotal.TabStop = false;
			textBoxSubtotal.Text = "0.00";
			textBoxSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSubtotal.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
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
			comboBoxGridCurrency.AlwaysInEditMode = true;
			comboBoxGridCurrency.Assigned = false;
			comboBoxGridCurrency.CalcManager = ultraCalcManager1;
			comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCurrency.CustomReportFieldName = "";
			comboBoxGridCurrency.CustomReportKey = "";
			comboBoxGridCurrency.CustomReportValueType = 1;
			comboBoxGridCurrency.DescriptionTextBox = null;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance117;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
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
			bomComboBox2.Assigned = false;
			bomComboBox2.CalcManager = ultraCalcManager1;
			bomComboBox2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			bomComboBox2.CustomReportFieldName = "";
			bomComboBox2.CustomReportKey = "";
			bomComboBox2.CustomReportValueType = 1;
			bomComboBox2.DescriptionTextBox = null;
			bomComboBox2.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			bomComboBox2.Editable = true;
			bomComboBox2.FilterString = "";
			bomComboBox2.HasAllAccount = false;
			bomComboBox2.HasCustom = false;
			bomComboBox2.IsDataLoaded = false;
			bomComboBox2.Location = new System.Drawing.Point(0, 0);
			bomComboBox2.MaxDropDownItems = 12;
			bomComboBox2.Name = "bomComboBox2";
			bomComboBox2.ShowInactiveItems = false;
			bomComboBox2.ShowQuickAdd = true;
			bomComboBox2.Size = new System.Drawing.Size(100, 20);
			bomComboBox2.TabIndex = 0;
			bomComboBox2.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(979, 616);
			base.Controls.Add(panel1);
			base.Controls.Add(TestrichTextBox);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Controls.Add(groupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "PropertyRentDetailsForm";
			Text = "Rental Registration";
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridIncome).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPayment).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			groupBoxPeriodicInvoice.ResumeLayout(false);
			groupBoxPeriodicInvoice.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)NumericNoofinstallments).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProperty).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataSet1).EndInit();
			groupBox1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panelNonTax.ResumeLayout(false);
			panelNonTax.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
