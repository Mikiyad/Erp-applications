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
	public class PropertyRentCancellationForm : Form, IForm
	{
		private PropertyCancelData currentData;

		private const string TABLENAME_CONST = "Property_Cancel";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool isDataLoading;

		private string _agreestatus = "";

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

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

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

		private TextBox textBoxCustomerName;

		private Label labelTotalCost;

		private XPButton buttonPayCash;

		private XPButton buttonPayCheque;

		private PropertyIncomeCodeComboBox comboBoxGridIncomeCode;

		private MMLabel mmLabel5;

		private TextBox textBoxPrevDocumentNo;

		private XPButton buttonPropertyRent;

		private TextBox textBoxPrevSysID;

		private Label label7;

		private Label label5;

		private Label label4;

		private Label label1;

		private GroupBox groupBox1;

		private ToolStripButton toolStripButtonOpenList;

		private DateTimePicker dateTimeLastStay;

		private Label label8;

		private UltraTabPageControl ultraTabPageControl1;

		private DataEntryGrid dataGridCharges;

		private UltraTabPageControl ultraTabPageControl2;

		private XPButton buttonCalculateCharges;

		private PropertyIncomeCodeComboBox comboBoxGridCharges;

		private Label label9;

		private DataEntryGrid dataGridPayment;

		private SysDocComboBox comboBoxSysDoc;

		private TextBox textBoxPropertyName;

		private TextBox textBoxUnit;

		private ToolStripButton toolStripButtonInformation;

		private Panel panel1;

		private Panel panelNonTax;

		private UltraFormattedLinkLabel linkLabelTax;

		private AmountTextBox textBoxTaxAmount;

		private Label label2;

		private AmountTextBox textBoxTotal;

		private Label label10;

		private AmountTextBox textBoxSubtotal;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButton1;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private PropertyAgentComboBox comboBoxPropertyAgent;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelAgent;

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
				}
				toolStripButtonDistribution.Enabled = !value;
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

		public PropertyRentCancellationForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += PropertyRentCancellationForm_Load;
			dataGridIncome.AfterCellUpdate += dataGridIncome_AfterCellUpdate;
			dataGridIncome.BeforeRowDeactivate += dataGridIncome_BeforeRowDeactivate;
			dataGridIncome.BeforeCellDeactivate += dataGridIncome_BeforeCellDeactivate;
			dataGridIncome.AfterRowsDeleted += dataGridIncome_AfterRowsDeleted;
			dataGridIncome.CellDataError += dataGridIncome_CellDataError;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			dataGridCharges.AfterCellUpdate += dataGridCharges_AfterCellUpdate;
			comboBoxPayeeTaxGroup.SelectedIndexChanged += comboBoxPayeeTaxGroup_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
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

		private void CalculateTotalCost()
		{
			decimal d = default(decimal);
			decimal num = default(decimal);
			decimal result = default(decimal);
			try
			{
				decimal d2 = default(decimal);
				decimal num2 = default(decimal);
				decimal d3 = default(decimal);
				decimal d4 = default(decimal);
				foreach (UltraGridRow row in dataGridIncome.Rows)
				{
					decimal result2 = default(decimal);
					if (row.Cells["Refund Amount"].Value != null && !(row.Cells["Refund Amount"].Value.ToString() == ""))
					{
						decimal.TryParse(row.Cells["Refund Amount"].Value.ToString(), out result2);
						d2 += result2;
						if (!row.Cells["Tax"].Value.IsNullOrEmpty())
						{
							decimal.TryParse(row.Cells["Tax"].Value.ToString(), out result);
							row.Cells["TaxTotal"].Value = result2 + result;
						}
						if (!row.Cells["Tax"].Value.IsNullOrEmpty())
						{
							num += decimal.Parse(row.Cells["Tax"].Value.ToString());
						}
					}
				}
				foreach (UltraGridRow row2 in dataGridCharges.Rows)
				{
					decimal result3 = default(decimal);
					if (row2.Cells["Amount"].Value != null && !(row2.Cells["Amount"].Value.ToString() == ""))
					{
						decimal.TryParse(row2.Cells["Amount"].Value.ToString(), out result3);
						d3 += result3;
					}
				}
				foreach (UltraGridRow row3 in dataGridPayment.Rows)
				{
					decimal result4 = default(decimal);
					if (row3.Cells["UnclearedAmount"].Value != null && !(row3.Cells["UnclearedAmount"].Value.ToString() == ""))
					{
						decimal.TryParse(row3.Cells["UnclearedAmount"].Value.ToString(), out result4);
					}
				}
				num2 = d + d2 - d3 - d4;
				textBoxSubtotal.Text = num2.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = num.ToString(Format.TotalAmountFormat);
				labelTotalCost.Text = num2.ToString(Format.TotalAmountFormat);
				decimal.TryParse(labelTotalCost.Text, out num2);
				num2 += num;
				textBoxTotal.Text = d.ToString(Format.TotalAmountFormat);
				CalculateTotalTaxes();
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
					decimal amount = decimal.Parse(row.Cells["Refund Amount"].Value.ToString());
					decimal subtotal = decimal.Parse(textBoxSubtotal.Text, NumberStyles.Any);
					TaxTransactionData taxTransactionData = TaxHelper.CreateTaxDetailData(comboBoxCustomer.TaxOption, comboBoxPayeeTaxGroup.SelectedID, itemTaxOptions, row.Cells["TaxGroupID"].Value.ToString());
					row.Cells["Tax"].Tag = taxTransactionData.Copy();
					UIGlobal.CalculateRowTax(row, "Tax", amount, subtotal, 0m, priceIncludeTax: true);
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
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotalCost();
		}

		private void dataGridIncome_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridIncome.ActiveRow != null)
				{
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
					if (e.Cell.Column.Key == "Refund Amount")
					{
						decimal result = default(decimal);
						decimal.TryParse(e.Cell.Value.ToString(), out result);
						decimal subtotal = decimal.Parse(textBoxSubtotal.Text);
						UIGlobal.CalculateRowTax(e.Cell.Row, "Tax", result, subtotal, 0m);
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
				if (activeRow.Cells["Paid Amount"].Value.ToString() == "")
				{
					activeRow.Cells["Paid Amount"].Value = 0;
				}
				if (activeRow.Cells["Charges"].Value.ToString() == "")
				{
					activeRow.Cells["Charges"].Value = 0;
				}
				if (activeRow.Cells["Refund Amount"].Value.ToString() == "")
				{
					activeRow.Cells["Refund Amount"].Value = 0;
				}
			}
		}

		private void dataGridIncome_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridIncome.ActiveCell.Column.Key.ToString() == "Paid Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridIncome.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridIncome.ActiveCell.Value = result;
			}
			if (dataGridIncome.ActiveCell.Column.Key.ToString() == "Charges")
			{
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridIncome.ActiveCell.Value.ToString(), out result2);
				result2 = Math.Round(result2, Global.CurDecimalPoints);
				dataGridIncome.ActiveCell.Value = result2;
			}
			if (dataGridIncome.ActiveCell.Column.Key.ToString() == "Refund Amount")
			{
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridIncome.ActiveCell.Value.ToString(), out result3);
				result3 = Math.Round(result3, Global.CurDecimalPoints);
				dataGridIncome.ActiveCell.Value = result3;
			}
		}

		private void dataGridIncome_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new PropertyCancelData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PropertyCancelTable.Rows[0] : currentData.PropertyCancelTable.NewRow();
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
				dataRow["AgreementType"] = 2;
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
				dataRow["PropertyID"] = comboBoxProperty.Text;
				dataRow["UnitID"] = comboBoxPropertyUnit.Text;
				dataRow["CustomerID"] = comboBoxCustomer.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["SourceSysDocID"] = textBoxPrevSysID.Text;
				dataRow["SourceVoucherID"] = textBoxPrevDocumentNo.Text;
				dataRow["LastStayDate"] = dateTimeLastStay.Value;
				if (textBoxTotalDays.Text != "")
				{
					dataRow["TotalDays"] = textBoxTotalDays.Text;
				}
				else
				{
					dataRow["TotalDays"] = (dateTimeEndDate.Value - dateTimeStartDate.Value).Days.ToString();
				}
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
				dataRow["TaxAmount"] = textBoxTaxAmount.Text;
				if (comboBoxPropertyAgent.SelectedID != "")
				{
					dataRow["PropertyAgentID"] = comboBoxPropertyAgent.SelectedID;
				}
				else
				{
					dataRow["PropertyAgentID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PropertyCancelTable.Rows.Add(dataRow);
				}
				currentData.PropertyCancelDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridCharges.Rows)
				{
					DataRow dataRow2 = currentData.PropertyCancelDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["IncomeID"] = row.Cells["Charges"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Reference"] = row.Cells["Reference"].Value.ToString();
					dataRow2["Amount"] = row.Cells["Amount"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.PropertyCancelDetailTable.Rows.Add(dataRow2);
				}
				currentData.PropertyRefundDetailTable.Rows.Clear();
				foreach (UltraGridRow row2 in dataGridIncome.Rows)
				{
					DataRow dataRow3 = currentData.PropertyRefundDetailTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["IncomeID"] = row2.Cells["Income Code"].Text.ToString();
					dataRow3["Description"] = row2.Cells["Description"].Text.ToString();
					dataRow3["PaidAmount"] = row2.Cells["Paid Amount"].Value.ToString();
					dataRow3["Charges"] = row2.Cells["Charges"].Value.ToString();
					if (!row2.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow3["TaxOption"] = row2.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow3["TaxOption"] = (byte)2;
					}
					if (row2.Cells["Tax"].Value != null && row2.Cells["Tax"].Value.ToString() != "")
					{
						dataRow3["TaxAmount"] = row2.Cells["Tax"].Value.ToString();
					}
					else
					{
						dataRow3["TaxAmount"] = DBNull.Value;
					}
					if (row2.Cells["TaxGroupID"].Value != null && row2.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow3["TaxGroupID"] = row2.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow3["TaxGroupID"] = DBNull.Value;
					}
					dataRow3["RefundAmount"] = row2.Cells["Refund Amount"].Value.ToString();
					dataRow3["RowIndex"] = row2.Index;
					dataRow3.EndEdit();
					currentData.PropertyRefundDetailTable.Rows.Add(dataRow3);
				}
				currentData.Tables["Tax_Detail"].Rows.Clear();
				string selectedID = comboBoxSysDoc.SelectedID;
				string text = textBoxVoucherNumber.Text;
				int num = 0;
				foreach (UltraGridRow row3 in dataGridIncome.Rows)
				{
					if (row3.Cells["Tax"].Tag != null)
					{
						TaxHelper.CreateTaxRows(currentData, row3.Cells["Tax"].Tag as TaxTransactionData, TaxDetailLevel.Items, selectedID, text, num, comboBoxCustomer.DefaultCurrencyID, 1m);
					}
					num = checked(num + 1);
				}
				if (textBoxTaxAmount.Tag != null)
				{
					TaxHelper.CreateTaxRows(currentData, textBoxTaxAmount.Tag as TaxTransactionData, TaxDetailLevel.Transaction, selectedID, text, -1, comboBoxCustomer.DefaultCurrencyID, 1m);
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
				dataTable.Columns.Add("Paid Amount", typeof(decimal));
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("Tax", typeof(decimal));
				dataTable.Columns.Add("TaxTotal", typeof(decimal));
				dataTable.Columns.Add("Charges", typeof(decimal));
				dataTable.Columns.Add("Refund Amount", typeof(decimal));
				dataGridIncome.DataSource = dataTable;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].MaxLength = 64;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].ValueList = comboBoxGridIncomeCode;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Description"].Width = 70;
				UltraGridColumn ultraGridColumn = dataGridIncome.DisplayLayout.Bands[0].Columns["Paid Amount"];
				UltraGridColumn ultraGridColumn2 = dataGridIncome.DisplayLayout.Bands[0].Columns["Charges"];
				string text2 = dataGridIncome.DisplayLayout.Bands[0].Columns["Refund Amount"].Format = "#,##0.00";
				string text5 = ultraGridColumn.Format = (ultraGridColumn2.Format = text2);
				AppearanceBase cellAppearance = dataGridIncome.DisplayLayout.Bands[0].Columns["Paid Amount"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridIncome.DisplayLayout.Bands[0].Columns["Charges"].CellAppearance;
				HAlign hAlign2 = dataGridIncome.DisplayLayout.Bands[0].Columns["Refund Amount"].CellAppearance.TextHAlign = HAlign.Right;
				HAlign hAlign5 = cellAppearance.TextHAlign = (cellAppearance2.TextHAlign = hAlign2);
				UltraGridColumn ultraGridColumn3 = dataGridIncome.DisplayLayout.Bands[0].Columns["Paid Amount"];
				UltraGridColumn ultraGridColumn4 = dataGridIncome.DisplayLayout.Bands[0].Columns["Charges"];
				text2 = (dataGridIncome.DisplayLayout.Bands[0].Columns["Refund Amount"].Format = Format.GridAmountFormat);
				text5 = (ultraGridColumn3.Format = (ultraGridColumn4.Format = text2));
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
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].Width = 30;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Paid Amount"].Width = 40;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Charges"].Width = 40;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Refund Amount"].Width = 40;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Income Code"].CellActivation = Activation.NoEdit;
				dataGridIncome.DisplayLayout.Bands[0].Columns["Paid Amount"].CellActivation = Activation.NoEdit;
				dataGridIncome.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridIncome.DisplayLayout.Bands[0].Columns["Refund Amount"], SummaryPosition.UseSummaryPositionColumn);
			}
			catch
			{
			}
		}

		private void SetupChargesGrid()
		{
			try
			{
				dataGridCharges.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Charges");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridCharges.DataSource = dataTable;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].CharacterCasing = CharacterCasing.Upper;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].MaxLength = 64;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].ValueList = comboBoxGridCharges;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 20;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Charges"].Width = 50;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Reference"].Width = 40;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"].Width = 30;
				dataGridCharges.DisplayLayout.Bands[0].Columns["Description"].Width = 70;
				dataGridCharges.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridCharges.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
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
					currentData = Factory.PropertyCancelSystem.GetPropertyCancelByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						ClearForm();
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
					DataRow dataRow = currentData.Tables["Property_Cancel"].Rows[0];
					dateTimeTransactionDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					dateTimeStartDate.Value = DateTime.Parse(dataRow["ContractStartDate"].ToString());
					dateTimeLastStay.Value = DateTime.Parse(dataRow["LastStayDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxProperty.SelectedID = dataRow["PropertyID"].ToString();
					comboBoxPropertyUnit.SelectedID = dataRow["UnitID"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					textBoxTotalDays.Text = dataRow["TotalDays"].ToString();
					textBoxPrevSysID.Text = dataRow["SourceSysDocID"].ToString();
					textBoxPrevDocumentNo.Text = dataRow["SourceVoucherID"].ToString();
					dateTimeEndDate.Value = DateTime.Parse(dataRow["ContractEndDate"].ToString());
					dateTimeStartDate.Value = DateTime.Parse(dataRow["ContractStartDate"].ToString());
					comboBoxCustomer.Text = dataRow["CustomerID"].ToString();
					_agreestatus = dataRow["AgreementStatus"].ToString();
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
					if (!string.IsNullOrEmpty(dataRow["PropertyAgentID"].ToString()))
					{
						comboBoxPropertyAgent.SelectedID = dataRow["PropertyAgentID"].ToString();
					}
					DataTable dataTable = dataGridCharges.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Property_Cancel_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Charges"] = row["IncomeID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Reference"] = row["Reference"];
						dataRow3["Amount"] = row["Amount"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					DataTable dataTable2 = dataGridIncome.DataSource as DataTable;
					dataTable2.Rows.Clear();
					foreach (DataRow row2 in currentData.Tables["Property_Refund_Detail"].Rows)
					{
						DataRow dataRow5 = dataTable2.NewRow();
						dataRow5["Income Code"] = row2["IncomeID"];
						dataRow5["Description"] = row2["Description"];
						dataRow5["Paid Amount"] = row2["PaidAmount"];
						dataRow5["Charges"] = row2["Charges"];
						if (row2["TaxOption"] != DBNull.Value)
						{
							dataRow5["TaxOption"] = byte.Parse(row2["TaxOption"].ToString());
						}
						else
						{
							dataRow5["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
						}
						if (!row2["TaxGroupID"].IsDBNullOrEmpty())
						{
							dataRow5["TaxGroupID"] = row2["TaxGroupID"];
						}
						dataRow5["Refund Amount"] = row2["RefundAmount"];
						if (row2["TaxAmount"] != DBNull.Value)
						{
							dataRow5["Tax"] = decimal.Parse(row2["TaxAmount"].ToString()).ToString(Format.UnitPriceFormat);
						}
						dataRow5.EndEdit();
						dataTable2.Rows.Add(dataRow5);
					}
					foreach (UltraGridRow row3 in dataGridIncome.Rows)
					{
						DataRow[] array = currentData.TaxDetailsTable.Select("RowIndex = " + row3.Index + " AND TaxLevel = " + (byte)2);
						if (array.Length != 0)
						{
							TaxTransactionData taxTransactionData = new TaxTransactionData();
							taxTransactionData.Merge(array);
							row3.Cells["Tax"].Tag = taxTransactionData;
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
				}
			}
			catch
			{
				throw;
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
				bool flag = Factory.PropertyCancelSystem.CreatePropertyCancel(currentData, !isNewRecord);
				flag = true;
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Property_Cancel", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
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
			if (isNewRecord && dateTimeTransactionDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
			{
				ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
				return false;
			}
			if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.Text == "" || textBoxPrevDocumentNo.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
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
			if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Property_Cancel", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				comboBoxCustomer.Enabled = true;
				comboBoxPropertyUnit.Clear();
				comboBoxProperty.Clear();
				comboBoxCustomer.Clear();
				textBoxCustomerName.Clear();
				textBoxPrevDocumentNo.Clear();
				textBoxPrevSysID.Clear();
				textBoxTotalDays.Clear();
				textBoxSubtotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxTotal.Text = 0.ToString(Format.TotalAmountFormat);
				textBoxPropertyName.Clear();
				textBoxUnit.Clear();
				dateTimeTransactionDate.Value = DateTime.Now;
				dateTimeStartDate.Value = DateTime.Now;
				dateTimeEndDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				textBoxTaxAmount.Text = 0.ToString(Format.TotalAmountFormat);
				(dataGridIncome.DataSource as DataTable).Rows.Clear();
				(dataGridCharges.DataSource as DataTable).Rows.Clear();
				(dataGridPayment.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxPayeeTaxGroup.Clear();
				comboBoxPropertyAgent.Clear();
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
				return Factory.PropertyCancelSystem.DeletePropertyCancel(SystemDocID, textBoxVoucherNumber.Text, comboBoxPropertyUnit.SelectedID, textBoxPrevSysID.Text, textBoxPrevDocumentNo.Text);
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
			string nextID = DatabaseHelper.GetNextID("Property_Cancel", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Property_Cancel", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Property_Cancel", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Property_Cancel", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Property_Cancel", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void PropertyRentCancellationForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridIncome.SetupUI();
				SetupIncomeGrid();
				dataGridCharges.SetupUI();
				SetupChargesGrid();
				dataGridPayment.SetupUI();
				SetupPaymentGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.PropertyCancel);
				dateTimeTransactionDate.Value = DateTime.Now;
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PropertyCancel);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PropertyCancel);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 103.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PropertyCancel);
					currentData = (dataSet as PropertyCancelData);
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
				DataSet propertyCancelToPrint = Factory.PropertyCancelSystem.GetPropertyCancelToPrint(selectedID, text);
				if (propertyCancelToPrint == null || propertyCancelToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(propertyCancelToPrint, selectedID, "Rental Cancellation", SysDocTypes.PropertyCancel, isPrint, showPrintDialog);
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
			new FormHelper().EditProduct(comboBoxProperty.SelectedID);
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
				cashReceiptForm.Description = "Rental Cancellation Refund & Charges for the tenant :" + comboBoxCustomer.SelectedID;
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
				chequeReceiptForm.Description = "Rental Cancellation Refund & Charges for the tenant :" + comboBoxCustomer.SelectedID;
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
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void buttonPropertyRent_Click(object sender, EventArgs e)
		{
			try
			{
				ClearForm();
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.Text = "Select Reference Property ";
				selectDocumentDialog.IsMultiSelect = false;
				selectDocumentDialog.AllowDateFilter = true;
				selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						textBoxPrevSysID.Text = selectedRow.Cells["Doc ID"].Value.ToString();
						textBoxPrevDocumentNo.Text = selectedRow.Cells["Number"].Value.ToString();
						comboBoxProperty.Text = selectedRow.Cells["PropertyID"].Value.ToString();
						comboBoxPropertyUnit.Text = selectedRow.Cells["UnitID"].Value.ToString();
						textBoxPropertyName.Text = selectedRow.Cells["Property"].Value.ToString();
						textBoxUnit.Text = selectedRow.Cells["Unit"].Value.ToString();
						comboBoxCustomer.Text = selectedRow.Cells["CustomerID"].Value.ToString();
						comboBoxPropertyAgent.SelectedID = selectedRow.Cells["PropertyAgentID"].Value.ToString();
						textBoxCustomerName.Text = selectedRow.Cells["Tenant"].Value.ToString();
						dateTimeStartDate.Value = DateTime.Parse(selectedRow.Cells["ContractStartDate"].Value.ToString());
						dateTimeEndDate.Value = DateTime.Parse(selectedRow.Cells["ContractEndDate"].Value.ToString());
						if (!string.IsNullOrEmpty(selectedRow.Cells["PayeeTaxGroupID"].Value.ToString()))
						{
							comboBoxPayeeTaxGroup.SelectedID = selectedRow.Cells["PayeeTaxGroupID"].Value.ToString();
						}
						TimeSpan timeSpan = dateTimeEndDate.Value - dateTimeStartDate.Value;
						textBoxTotalDays.Text = timeSpan.Days.ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			try
			{
				DataSet dataSet = (sender as SelectDocumentDialog).DataSource = Factory.PropertyRentSystem.GetPropertyRentDetailList("", comboBoxCustomer.SelectedID, e.From, e.To);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxPrevDocumentNo_Update()
		{
			try
			{
				DataSet dataSet = new DataSet();
				dataSet = Factory.PropertyCancelSystem.GetPropertypaymentdetails(textBoxPrevSysID.Text, textBoxPrevDocumentNo.Text);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PropertyCancelListFormObj);
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

		private void dateTimeEndDate_TabIndexChanged(object sender, EventArgs e)
		{
			TimeSpan timeSpan = dateTimeEndDate.Value - dateTimeStartDate.Value;
			textBoxTotalDays.Text = timeSpan.Days.ToString();
		}

		private void buttonCalculateCharges_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBoxPrevDocumentNo.Text != string.Empty)
				{
					DataSet dataSet = new DataSet();
					dataSet = Factory.PropertyRentSystem.GetPropertyRentDetail(textBoxPrevSysID.Text, textBoxPrevDocumentNo.Text);
					DataTable dataTable = dataGridIncome.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in dataSet.Tables["Property_Rent_Detail"].Rows)
					{
						double num = 0.0;
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["Income Code"] = row["IncomeID"];
						dataRow2["Description"] = row["Description"];
						dataRow2["Paid Amount"] = row["Amount"];
						dataRow2["TaxGroupID"] = row["TaxGroupID"];
						if (row["TaxOption"] != DBNull.Value)
						{
							dataRow2["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
						}
						else
						{
							dataRow2["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
						}
						string a = row["IncomeType"].ToString();
						if (a == "1")
						{
							int num2 = int.Parse(textBoxTotalDays.Text);
							int num3 = int.Parse((dateTimeLastStay.Value - dateTimeStartDate.Value).Days.ToString());
							double num4 = 0.0;
							num = double.Parse(row["Amount"].ToString());
							if (num2 > 0)
							{
								num4 = num / (double)num2 * (double)num3;
							}
							dataRow2["Charges"] = Math.Round(num4, Global.CurDecimalPoints);
							dataRow2["Refund Amount"] = Math.Round(num - num4, Global.CurDecimalPoints);
						}
						else if (a == "3")
						{
							num = double.Parse(row["Amount"].ToString());
							dataRow2["Charges"] = 0.0;
							dataRow2["Refund Amount"] = Math.Round(num - 0.0, Global.CurDecimalPoints);
						}
						else if (a == "2")
						{
							num = double.Parse(row["Amount"].ToString());
							dataRow2["Charges"] = num;
							dataRow2["Refund Amount"] = Math.Round(num - num, Global.CurDecimalPoints);
						}
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					DataRow dataRow3 = dataSet.Tables["Property_Rent"].Rows[0];
					if (int.Parse(dataRow3["AgreementType"].ToString()) == 2)
					{
						string sysDocID = dataRow3["SourceSysDocID"].ToString();
						string voucherID = dataRow3["SourceVoucherID"].ToString();
						new DataSet();
						foreach (DataRow row2 in Factory.PropertyRentSystem.GetPropertyRentDetail(sysDocID, voucherID).Tables["Property_Rent_Detail"].Rows)
						{
							double num = 0.0;
							DataRow dataRow5 = dataTable.NewRow();
							dataRow5["Income Code"] = row2["IncomeID"];
							dataRow5["Description"] = row2["Description"];
							dataRow5["Paid Amount"] = row2["Amount"];
							dataRow5["TaxGroupID"] = row2["TaxGroupID"];
							if (row2["TaxOption"] != DBNull.Value)
							{
								dataRow5["TaxOption"] = byte.Parse(row2["TaxOption"].ToString());
							}
							else
							{
								dataRow5["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
							}
							switch (row2["IncomeType"].ToString())
							{
							case "3":
								num = double.Parse(row2["Amount"].ToString());
								dataRow5["Charges"] = 0.0;
								dataRow5["Refund Amount"] = Math.Round(num - 0.0, Global.CurDecimalPoints);
								break;
							case "2":
								num = double.Parse(row2["Amount"].ToString());
								dataRow5["Charges"] = num;
								dataRow5["Refund Amount"] = Math.Round(num - num, Global.CurDecimalPoints);
								break;
							case "1":
								continue;
							}
							dataRow5.EndEdit();
							dataTable.Rows.Add(dataRow5);
						}
					}
					CalculateAllRowsTaxes();
					CalculateTotalCost();
				}
				else
				{
					ErrorHelper.InformationMessage("Select a Ref. Document");
					buttonPropertyRent.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridCharges_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (e.Cell.Column.Key == "Charges")
				{
					dataGridCharges.ActiveRow.Cells["Description"].Value = comboBoxGridCharges.SelectedName;
				}
				if (e.Cell.Column.Key == "Amount")
				{
					CalculateTotalCost();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void textBoxPrevDocumentNo_TextChanged(object sender, EventArgs e)
		{
			if (textBoxPrevDocumentNo.Text != "")
			{
				textBoxPrevDocumentNo_Update();
			}
		}

		private void ultraFormattedLinkLabelAgent_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPropertyAgent(comboBoxPropertyAgent.SelectedID);
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
			attachementDetailsForm.SysDocType = SysDocTypes.PropertyCancel;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.PropertyRental.PropertyRentCancellationForm));
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridIncome = new Micromind.DataControls.DataEntryGrid();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			comboBoxGridIncomeCode = new Micromind.DataControls.PropertyIncomeCodeComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridCharges = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridCharges = new Micromind.DataControls.PropertyIncomeCodeComboBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
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
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
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
			labelTotalCost = new System.Windows.Forms.Label();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			comboBoxPropertyAgent = new Micromind.DataControls.PropertyAgentComboBox();
			ultraFormattedLinkLabelAgent = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			textBoxUnit = new System.Windows.Forms.TextBox();
			textBoxPropertyName = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			buttonCalculateCharges = new Micromind.UISupport.XPButton();
			dateTimeLastStay = new System.Windows.Forms.DateTimePicker();
			label8 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxPrevSysID = new System.Windows.Forms.TextBox();
			buttonPropertyRent = new Micromind.UISupport.XPButton();
			textBoxPrevDocumentNo = new System.Windows.Forms.TextBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			label6 = new System.Windows.Forms.Label();
			textBoxTotalDays = new System.Windows.Forms.TextBox();
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
			panel1 = new System.Windows.Forms.Panel();
			panelNonTax = new System.Windows.Forms.Panel();
			linkLabelTax = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxTaxAmount = new Micromind.UISupport.AmountTextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxTotal = new Micromind.UISupport.AmountTextBox();
			label10 = new System.Windows.Forms.Label();
			textBoxSubtotal = new Micromind.UISupport.AmountTextBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			bomComboBox2 = new Micromind.DataControls.BOMComboBox();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridIncome).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCharges).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCharges).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPayment).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
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
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).BeginInit();
			SuspendLayout();
			tabPageExpense.Controls.Add(dataGridIncome);
			tabPageExpense.Controls.Add(comboBoxGridIncomeCode);
			tabPageExpense.Location = new System.Drawing.Point(1, 23);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(937, 262);
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
			dataGridIncome.Location = new System.Drawing.Point(0, 1);
			dataGridIncome.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridIncome.Name = "dataGridIncome";
			dataGridIncome.ShowClearMenu = true;
			dataGridIncome.ShowDeleteMenu = true;
			dataGridIncome.ShowInsertMenu = true;
			dataGridIncome.ShowMoveRowsMenu = true;
			dataGridIncome.Size = new System.Drawing.Size(936, 260);
			dataGridIncome.TabIndex = 1;
			dataGridIncome.Text = "dataEntryGrid1";
			ultraCalcManager1.ContainingControl = this;
			comboBoxGridIncomeCode.Assigned = false;
			comboBoxGridIncomeCode.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
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
			comboBoxGridIncomeCode.Location = new System.Drawing.Point(56, 121);
			comboBoxGridIncomeCode.MaxDropDownItems = 12;
			comboBoxGridIncomeCode.Name = "comboBoxGridIncomeCode";
			comboBoxGridIncomeCode.ShowInactiveItems = false;
			comboBoxGridIncomeCode.ShowQuickAdd = true;
			comboBoxGridIncomeCode.Size = new System.Drawing.Size(100, 20);
			comboBoxGridIncomeCode.TabIndex = 136;
			comboBoxGridIncomeCode.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridIncomeCode.Visible = false;
			ultraTabPageControl1.Controls.Add(dataGridCharges);
			ultraTabPageControl1.Controls.Add(comboBoxGridCharges);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(937, 262);
			dataGridCharges.AllowAddNew = true;
			dataGridCharges.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridCharges.CalcManager = ultraCalcManager1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridCharges.DisplayLayout.Appearance = appearance25;
			dataGridCharges.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridCharges.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCharges.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCharges.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridCharges.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridCharges.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridCharges.DisplayLayout.MaxColScrollRegions = 1;
			dataGridCharges.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridCharges.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridCharges.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridCharges.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridCharges.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridCharges.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridCharges.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridCharges.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridCharges.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridCharges.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridCharges.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridCharges.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridCharges.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridCharges.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridCharges.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridCharges.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridCharges.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridCharges.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridCharges.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridCharges.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridCharges.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridCharges.ExitEditModeOnLeave = false;
			dataGridCharges.IncludeLotItems = false;
			dataGridCharges.LoadLayoutFailed = false;
			dataGridCharges.Location = new System.Drawing.Point(-1, -1);
			dataGridCharges.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridCharges.Name = "dataGridCharges";
			dataGridCharges.ShowClearMenu = true;
			dataGridCharges.ShowDeleteMenu = true;
			dataGridCharges.ShowInsertMenu = true;
			dataGridCharges.ShowMoveRowsMenu = true;
			dataGridCharges.Size = new System.Drawing.Size(937, 260);
			dataGridCharges.TabIndex = 2;
			dataGridCharges.Text = "dataEntryGrid1";
			comboBoxGridCharges.Assigned = false;
			comboBoxGridCharges.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridCharges.CalcManager = ultraCalcManager1;
			comboBoxGridCharges.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCharges.CustomReportFieldName = "";
			comboBoxGridCharges.CustomReportKey = "";
			comboBoxGridCharges.CustomReportValueType = 1;
			comboBoxGridCharges.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCharges.DisplayLayout.Appearance = appearance37;
			comboBoxGridCharges.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCharges.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCharges.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCharges.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxGridCharges.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCharges.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxGridCharges.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCharges.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCharges.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCharges.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxGridCharges.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCharges.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCharges.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCharges.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxGridCharges.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCharges.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCharges.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxGridCharges.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxGridCharges.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCharges.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCharges.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxGridCharges.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCharges.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxGridCharges.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridCharges.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridCharges.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridCharges.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridCharges.Editable = true;
			comboBoxGridCharges.FilterString = "";
			comboBoxGridCharges.HasAllAccount = false;
			comboBoxGridCharges.HasCustom = false;
			comboBoxGridCharges.IsDataLoaded = false;
			comboBoxGridCharges.Location = new System.Drawing.Point(149, 51);
			comboBoxGridCharges.MaxDropDownItems = 12;
			comboBoxGridCharges.Name = "comboBoxGridCharges";
			comboBoxGridCharges.ShowInactiveItems = false;
			comboBoxGridCharges.ShowQuickAdd = true;
			comboBoxGridCharges.Size = new System.Drawing.Size(100, 20);
			comboBoxGridCharges.TabIndex = 172;
			comboBoxGridCharges.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridCharges.Visible = false;
			ultraTabPageControl2.Controls.Add(dataGridPayment);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(937, 262);
			dataGridPayment.AllowAddNew = true;
			dataGridPayment.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridPayment.CalcManager = ultraCalcManager1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPayment.DisplayLayout.Appearance = appearance49;
			dataGridPayment.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPayment.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridPayment.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPayment.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridPayment.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPayment.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPayment.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPayment.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridPayment.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridPayment.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPayment.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPayment.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridPayment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPayment.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPayment.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridPayment.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridPayment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPayment.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridPayment.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridPayment.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPayment.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridPayment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPayment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPayment.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridPayment.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPayment.ExitEditModeOnLeave = false;
			dataGridPayment.IncludeLotItems = false;
			dataGridPayment.LoadLayoutFailed = false;
			dataGridPayment.Location = new System.Drawing.Point(1, 0);
			dataGridPayment.MinimumSize = new System.Drawing.Size(622, 95);
			dataGridPayment.Name = "dataGridPayment";
			dataGridPayment.ShowClearMenu = true;
			dataGridPayment.ShowDeleteMenu = true;
			dataGridPayment.ShowInsertMenu = true;
			dataGridPayment.ShowMoveRowsMenu = true;
			dataGridPayment.Size = new System.Drawing.Size(936, 262);
			dataGridPayment.TabIndex = 3;
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
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(966, 31);
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
			panelButtons.Location = new System.Drawing.Point(0, 627);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(966, 40);
			panelButtons.TabIndex = 4;
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
			linePanelDown.Size = new System.Drawing.Size(966, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(855, 8);
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
			dateTimeTransactionDate.Location = new System.Drawing.Point(784, 5);
			dateTimeTransactionDate.Name = "dateTimeTransactionDate";
			dateTimeTransactionDate.Size = new System.Drawing.Size(116, 20);
			dateTimeTransactionDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(382, 4);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(113, 20);
			textBoxVoucherNumber.TabIndex = 1;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(80, 528);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(405, 94);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(35, 537);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance61.FontData.BoldAsString = "True";
			appearance61.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance61;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(274, 8);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance62.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance62;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance63.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance63.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance63;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelTotalCost);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(12, 502);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(938, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelTotalCost.Dock = System.Windows.Forms.DockStyle.Right;
			labelTotalCost.Location = new System.Drawing.Point(784, 2);
			labelTotalCost.Name = "labelTotalCost";
			labelTotalCost.Size = new System.Drawing.Size(152, 17);
			labelTotalCost.TabIndex = 139;
			labelTotalCost.Text = "0.00";
			labelTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance64.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance64.FontData.BoldAsString = "True";
			appearance64.FontData.Name = "Tahoma";
			appearance64.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance64.TextHAlignAsString = "Right";
			appearance64.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance64;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 3);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(789, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			panelDetails.Controls.Add(comboBoxPropertyAgent);
			panelDetails.Controls.Add(ultraFormattedLinkLabelAgent);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(textBoxUnit);
			panelDetails.Controls.Add(textBoxPropertyName);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(buttonCalculateCharges);
			panelDetails.Controls.Add(dateTimeLastStay);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(label7);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxPrevSysID);
			panelDetails.Controls.Add(buttonPropertyRent);
			panelDetails.Controls.Add(textBoxPrevDocumentNo);
			panelDetails.Controls.Add(mmLabel5);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxTotalDays);
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
			panelDetails.Location = new System.Drawing.Point(12, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(935, 171);
			panelDetails.TabIndex = 0;
			comboBoxPropertyAgent.Assigned = false;
			comboBoxPropertyAgent.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyAgent.CalcManager = ultraCalcManager1;
			comboBoxPropertyAgent.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyAgent.CustomReportFieldName = "";
			comboBoxPropertyAgent.CustomReportKey = "";
			comboBoxPropertyAgent.CustomReportValueType = 1;
			comboBoxPropertyAgent.DescriptionTextBox = null;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyAgent.DisplayLayout.Appearance = appearance65;
			comboBoxPropertyAgent.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyAgent.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyAgent.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxPropertyAgent.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyAgent.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyAgent.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyAgent.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyAgent.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxPropertyAgent.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyAgent.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyAgent.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyAgent.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyAgent.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxPropertyAgent.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyAgent.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			comboBoxPropertyAgent.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyAgent.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyAgent.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyAgent.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyAgent.Editable = true;
			comboBoxPropertyAgent.FilterString = "";
			comboBoxPropertyAgent.HasAllAccount = false;
			comboBoxPropertyAgent.HasCustom = false;
			comboBoxPropertyAgent.IsDataLoaded = false;
			comboBoxPropertyAgent.Location = new System.Drawing.Point(611, 99);
			comboBoxPropertyAgent.MaxDropDownItems = 12;
			comboBoxPropertyAgent.Name = "comboBoxPropertyAgent";
			comboBoxPropertyAgent.ShowInactiveItems = false;
			comboBoxPropertyAgent.ShowQuickAdd = true;
			comboBoxPropertyAgent.Size = new System.Drawing.Size(116, 20);
			comboBoxPropertyAgent.TabIndex = 19;
			comboBoxPropertyAgent.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPropertyAgent.Visible = false;
			appearance77.FontData.BoldAsString = "False";
			appearance77.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelAgent.Appearance = appearance77;
			ultraFormattedLinkLabelAgent.AutoSize = true;
			ultraFormattedLinkLabelAgent.Location = new System.Drawing.Point(568, 101);
			ultraFormattedLinkLabelAgent.Name = "ultraFormattedLinkLabelAgent";
			ultraFormattedLinkLabelAgent.Size = new System.Drawing.Size(37, 15);
			ultraFormattedLinkLabelAgent.TabIndex = 178;
			ultraFormattedLinkLabelAgent.TabStop = true;
			ultraFormattedLinkLabelAgent.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelAgent.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelAgent.Value = "Agent:";
			ultraFormattedLinkLabelAgent.Visible = false;
			appearance78.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelAgent.VisitedLinkAppearance = appearance78;
			ultraFormattedLinkLabelAgent.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabelAgent_LinkClicked);
			appearance79.FontData.BoldAsString = "False";
			appearance79.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance79;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(8, 123);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 176;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance80.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance80;
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CalcManager = ultraCalcManager1;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance81;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(105, 120);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(128, 20);
			comboBoxPayeeTaxGroup.TabIndex = 7;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxUnit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnit.Location = new System.Drawing.Point(238, 97);
			textBoxUnit.MaxLength = 64;
			textBoxUnit.Name = "textBoxUnit";
			textBoxUnit.ReadOnly = true;
			textBoxUnit.Size = new System.Drawing.Size(260, 20);
			textBoxUnit.TabIndex = 174;
			textBoxUnit.TabStop = false;
			textBoxPropertyName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPropertyName.Location = new System.Drawing.Point(238, 74);
			textBoxPropertyName.MaxLength = 64;
			textBoxPropertyName.Name = "textBoxPropertyName";
			textBoxPropertyName.ReadOnly = true;
			textBoxPropertyName.Size = new System.Drawing.Size(260, 20);
			textBoxPropertyName.TabIndex = 173;
			textBoxPropertyName.TabStop = false;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(8, 32);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(44, 13);
			label9.TabIndex = 172;
			label9.Text = "Tenant:";
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance93;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(105, 5);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonCalculateCharges.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCalculateCharges.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			buttonCalculateCharges.BackColor = System.Drawing.Color.DarkGray;
			buttonCalculateCharges.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCalculateCharges.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCalculateCharges.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCalculateCharges.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCalculateCharges.Location = new System.Drawing.Point(753, 69);
			buttonCalculateCharges.Name = "buttonCalculateCharges";
			buttonCalculateCharges.Size = new System.Drawing.Size(131, 24);
			buttonCalculateCharges.TabIndex = 13;
			buttonCalculateCharges.Text = "Calculate Charges...";
			buttonCalculateCharges.UseVisualStyleBackColor = false;
			buttonCalculateCharges.Click += new System.EventHandler(buttonCalculateCharges_Click);
			dateTimeLastStay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeLastStay.Location = new System.Drawing.Point(611, 73);
			dateTimeLastStay.Name = "dateTimeLastStay";
			dateTimeLastStay.Size = new System.Drawing.Size(116, 20);
			dateTimeLastStay.TabIndex = 12;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(517, 76);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(88, 13);
			label8.TabIndex = 169;
			label8.Text = "Last Day of Stay:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(507, 51);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(98, 26);
			label4.TabIndex = 168;
			label4.Text = "Contract End Date:\r\n\r\n";
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(504, 29);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(101, 26);
			label1.TabIndex = 167;
			label1.Text = "Contract Start Date:\r\n\r\n";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(8, 101);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(29, 13);
			label7.TabIndex = 166;
			label7.Text = "Unit:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(8, 78);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(52, 13);
			label5.TabIndex = 165;
			label5.Text = "Property :";
			textBoxPrevSysID.Location = new System.Drawing.Point(105, 51);
			textBoxPrevSysID.MaxLength = 15;
			textBoxPrevSysID.Name = "textBoxPrevSysID";
			textBoxPrevSysID.ReadOnly = true;
			textBoxPrevSysID.Size = new System.Drawing.Size(52, 20);
			textBoxPrevSysID.TabIndex = 4;
			buttonPropertyRent.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPropertyRent.BackColor = System.Drawing.Color.DarkGray;
			buttonPropertyRent.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPropertyRent.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPropertyRent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonPropertyRent.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPropertyRent.Location = new System.Drawing.Point(317, 49);
			buttonPropertyRent.Name = "buttonPropertyRent";
			buttonPropertyRent.Size = new System.Drawing.Size(34, 24);
			buttonPropertyRent.TabIndex = 5;
			buttonPropertyRent.Text = "...";
			buttonPropertyRent.UseVisualStyleBackColor = false;
			buttonPropertyRent.Click += new System.EventHandler(buttonPropertyRent_Click);
			textBoxPrevDocumentNo.Location = new System.Drawing.Point(159, 51);
			textBoxPrevDocumentNo.MaxLength = 15;
			textBoxPrevDocumentNo.Name = "textBoxPrevDocumentNo";
			textBoxPrevDocumentNo.ReadOnly = true;
			textBoxPrevDocumentNo.Size = new System.Drawing.Size(158, 20);
			textBoxPrevDocumentNo.TabIndex = 7;
			textBoxPrevDocumentNo.TextChanged += new System.EventHandler(textBoxPrevDocumentNo_TextChanged);
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = true;
			mmLabel5.Location = new System.Drawing.Point(8, 55);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(78, 13);
			mmLabel5.TabIndex = 153;
			mmLabel5.Text = "Ref Doc No:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(750, 51);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(61, 13);
			label6.TabIndex = 148;
			label6.Text = "Total Days:";
			textBoxTotalDays.Location = new System.Drawing.Point(818, 48);
			textBoxTotalDays.MaxLength = 255;
			textBoxTotalDays.Name = "textBoxTotalDays";
			textBoxTotalDays.ReadOnly = true;
			textBoxTotalDays.Size = new System.Drawing.Size(66, 20);
			textBoxTotalDays.TabIndex = 11;
			textBoxTotalDays.TabIndexChanged += new System.EventHandler(textBoxTotalDays_TabIndexChanged);
			textBoxTotalDays.Leave += new System.EventHandler(textBoxTotalDays_Leave);
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(238, 26);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(260, 20);
			textBoxCustomerName.TabIndex = 143;
			textBoxCustomerName.TabStop = false;
			dateTimeEndDate.Enabled = false;
			dateTimeEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeEndDate.Location = new System.Drawing.Point(611, 49);
			dateTimeEndDate.Name = "dateTimeEndDate";
			dateTimeEndDate.Size = new System.Drawing.Size(116, 20);
			dateTimeEndDate.TabIndex = 10;
			dateTimeEndDate.TabIndexChanged += new System.EventHandler(dateTimeEndDate_TabIndexChanged);
			comboBoxCustomer.AlwaysInEditMode = true;
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CalcManager = ultraCalcManager1;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = textBoxCustomerName;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance105;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
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
			comboBoxCustomer.Location = new System.Drawing.Point(105, 28);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ReadOnly = true;
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = true;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(128, 20);
			comboBoxCustomer.TabIndex = 3;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxPropertyUnit.Assigned = false;
			comboBoxPropertyUnit.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPropertyUnit.CalcManager = ultraCalcManager1;
			comboBoxPropertyUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPropertyUnit.CustomReportFieldName = "";
			comboBoxPropertyUnit.CustomReportKey = "";
			comboBoxPropertyUnit.CustomReportValueType = 1;
			comboBoxPropertyUnit.DescriptionTextBox = textBoxUnit;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPropertyUnit.DisplayLayout.Appearance = appearance117;
			comboBoxPropertyUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPropertyUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPropertyUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxPropertyUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPropertyUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPropertyUnit.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPropertyUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPropertyUnit.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxPropertyUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPropertyUnit.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPropertyUnit.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPropertyUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxPropertyUnit.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxPropertyUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPropertyUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			comboBoxPropertyUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPropertyUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPropertyUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPropertyUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPropertyUnit.Editable = true;
			comboBoxPropertyUnit.FilterString = "";
			comboBoxPropertyUnit.HasAllAccount = false;
			comboBoxPropertyUnit.HasCustom = false;
			comboBoxPropertyUnit.IsDataLoaded = false;
			comboBoxPropertyUnit.Location = new System.Drawing.Point(105, 97);
			comboBoxPropertyUnit.MaxDropDownItems = 12;
			comboBoxPropertyUnit.Name = "comboBoxPropertyUnit";
			comboBoxPropertyUnit.ReadOnly = true;
			comboBoxPropertyUnit.ShowActiveOnly = false;
			comboBoxPropertyUnit.ShowInactiveItems = false;
			comboBoxPropertyUnit.ShowQuickAdd = true;
			comboBoxPropertyUnit.Size = new System.Drawing.Size(128, 20);
			comboBoxPropertyUnit.TabIndex = 6;
			comboBoxPropertyUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxProperty.Assigned = false;
			comboBoxProperty.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxProperty.CalcManager = ultraCalcManager1;
			comboBoxProperty.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProperty.CustomReportFieldName = "";
			comboBoxProperty.CustomReportKey = "";
			comboBoxProperty.CustomReportValueType = 1;
			comboBoxProperty.DescriptionTextBox = textBoxPropertyName;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProperty.DisplayLayout.Appearance = appearance129;
			comboBoxProperty.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProperty.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			comboBoxProperty.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProperty.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			comboBoxProperty.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProperty.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProperty.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProperty.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			comboBoxProperty.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProperty.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProperty.DisplayLayout.Override.CellAppearance = appearance136;
			comboBoxProperty.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProperty.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProperty.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			comboBoxProperty.DisplayLayout.Override.HeaderAppearance = appearance138;
			comboBoxProperty.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProperty.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			comboBoxProperty.DisplayLayout.Override.RowAppearance = appearance139;
			comboBoxProperty.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProperty.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			comboBoxProperty.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProperty.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProperty.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProperty.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProperty.Editable = true;
			comboBoxProperty.FilterString = "";
			comboBoxProperty.HasAllAccount = false;
			comboBoxProperty.HasCustom = false;
			comboBoxProperty.IsDataLoaded = false;
			comboBoxProperty.Location = new System.Drawing.Point(105, 74);
			comboBoxProperty.MaxDropDownItems = 12;
			comboBoxProperty.Name = "comboBoxProperty";
			comboBoxProperty.ReadOnly = true;
			comboBoxProperty.ShowInactiveItems = false;
			comboBoxProperty.ShowQuickAdd = true;
			comboBoxProperty.Size = new System.Drawing.Size(128, 20);
			comboBoxProperty.TabIndex = 5;
			comboBoxProperty.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateTimeStartDate.Enabled = false;
			dateTimeStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimeStartDate.Location = new System.Drawing.Point(611, 24);
			dateTimeStartDate.Name = "dateTimeStartDate";
			dateTimeStartDate.Size = new System.Drawing.Size(116, 20);
			dateTimeStartDate.TabIndex = 9;
			appearance141.FontData.BoldAsString = "True";
			appearance141.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance141;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(4, 92);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(4, 1);
			ultraFormattedLinkLabel1.TabIndex = 124;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "";
			appearance142.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance142;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance143.FontData.BoldAsString = "True";
			appearance143.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance143;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(8, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance144.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance144;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(746, 6);
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
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Location = new System.Drawing.Point(13, 208);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(941, 288);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageExpense;
			ultraTab.Text = "Refund";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "Cancellation Charges";
			ultraTab3.TabPage = ultraTabPageControl2;
			ultraTab3.Text = "Payment Details";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[3]
			{
				ultraTab,
				ultraTab2,
				ultraTab3
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(937, 262);
			dataSet1.DataSetName = "NewDataSet";
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			groupBox1.Controls.Add(buttonPayCash);
			groupBox1.Controls.Add(buttonPayCheque);
			groupBox1.Location = new System.Drawing.Point(489, 561);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(240, 61);
			groupBox1.TabIndex = 24;
			groupBox1.TabStop = false;
			groupBox1.Text = "Receipt";
			buttonPayCash.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonPayCash.BackColor = System.Drawing.Color.DarkGray;
			buttonPayCash.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonPayCash.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonPayCash.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonPayCash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonPayCash.Location = new System.Drawing.Point(10, 21);
			buttonPayCash.Name = "buttonPayCash";
			buttonPayCash.Size = new System.Drawing.Size(88, 24);
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
			buttonPayCheque.Location = new System.Drawing.Point(129, 21);
			buttonPayCheque.Name = "buttonPayCheque";
			buttonPayCheque.Size = new System.Drawing.Size(89, 24);
			buttonPayCheque.TabIndex = 22;
			buttonPayCheque.Text = "Cheque";
			buttonPayCheque.UseVisualStyleBackColor = false;
			buttonPayCheque.Click += new System.EventHandler(buttonPayCheque_Click);
			panel1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panel1.Controls.Add(panelNonTax);
			panel1.Controls.Add(label10);
			panel1.Controls.Add(textBoxSubtotal);
			panel1.Location = new System.Drawing.Point(731, 531);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(220, 78);
			panel1.TabIndex = 3;
			panelNonTax.Controls.Add(linkLabelTax);
			panelNonTax.Controls.Add(textBoxTaxAmount);
			panelNonTax.Controls.Add(label2);
			panelNonTax.Controls.Add(textBoxTotal);
			panelNonTax.Location = new System.Drawing.Point(0, 19);
			panelNonTax.Name = "panelNonTax";
			panelNonTax.Size = new System.Drawing.Size(219, 59);
			panelNonTax.TabIndex = 144;
			appearance145.FontData.BoldAsString = "False";
			appearance145.FontData.Name = "Tahoma";
			linkLabelTax.Appearance = appearance145;
			linkLabelTax.AutoSize = true;
			linkLabelTax.Location = new System.Drawing.Point(7, 4);
			linkLabelTax.Name = "linkLabelTax";
			linkLabelTax.Size = new System.Drawing.Size(25, 15);
			linkLabelTax.TabIndex = 166;
			linkLabelTax.TabStop = true;
			linkLabelTax.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelTax.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelTax.Value = "Tax:";
			appearance146.ForeColor = System.Drawing.Color.Blue;
			linkLabelTax.VisitedLinkAppearance = appearance146;
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
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(4, 26);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 133;
			label2.Text = "Total:";
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
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(4, 3);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(49, 13);
			label10.TabIndex = 133;
			label10.Text = "Subtotal:";
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
			comboBoxGridExpenseCode.AlwaysInEditMode = true;
			comboBoxGridExpenseCode.Assigned = false;
			comboBoxGridExpenseCode.CalcManager = ultraCalcManager1;
			comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridExpenseCode.CustomReportFieldName = "";
			comboBoxGridExpenseCode.CustomReportKey = "";
			comboBoxGridExpenseCode.CustomReportValueType = 1;
			comboBoxGridExpenseCode.DescriptionTextBox = null;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			appearance147.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance147;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance148.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance148.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance148.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance148;
			appearance149.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance149;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance150.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance150.BackColor2 = System.Drawing.SystemColors.Control;
			appearance150.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance150.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance150;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance151;
			appearance152.BackColor = System.Drawing.SystemColors.Highlight;
			appearance152.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance152;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance153;
			appearance154.BorderColor = System.Drawing.Color.Silver;
			appearance154.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance154;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance155.BackColor = System.Drawing.SystemColors.Control;
			appearance155.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance155.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance155.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance155.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance155;
			appearance156.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance156;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance157;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance158;
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
			comboBoxGridCurrency.CalcManager = ultraCalcManager1;
			comboBoxGridCurrency.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridCurrency.CustomReportFieldName = "";
			comboBoxGridCurrency.CustomReportKey = "";
			comboBoxGridCurrency.CustomReportValueType = 1;
			comboBoxGridCurrency.DescriptionTextBox = null;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			appearance159.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance159;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance160.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance160.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance160.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance160;
			appearance161.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance161;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance162.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance162.BackColor2 = System.Drawing.SystemColors.Control;
			appearance162.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance162.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance162;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance163;
			appearance164.BackColor = System.Drawing.SystemColors.Highlight;
			appearance164.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance164;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance165;
			appearance166.BorderColor = System.Drawing.Color.Silver;
			appearance166.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance166;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance167.BackColor = System.Drawing.SystemColors.Control;
			appearance167.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance167.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance167.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance167.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance167;
			appearance168.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance168;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance169;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance170;
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
			toolStripButtonMultiPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonMultiPreview.Image = Micromind.ClientUI.Properties.Resources.multi_doc;
			toolStripButtonMultiPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMultiPreview.Name = "toolStripButtonMultiPreview";
			toolStripButtonMultiPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonMultiPreview.Text = "MultiPreview";
			toolStripButtonMultiPreview.Click += new System.EventHandler(toolStripButtonMultiPreview_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(966, 667);
			base.Controls.Add(panel1);
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
			base.Name = "PropertyRentCancellationForm";
			Text = "Rental Cancellation";
			tabPageExpense.ResumeLayout(false);
			tabPageExpense.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridIncome).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridIncomeCode).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridCharges).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCharges).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPayment).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPropertyAgent).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
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
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
