using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class LocationAccountsForm : Form, IForm
	{
		private LocationData currentData;

		private const string TABLENAME_CONST = "Location";

		private const string IDFIELD_CONST = "LocationID";

		private bool isNewRecord = true;

		private DataSet taxData;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonSave;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel1;

		private MMTextBox textBoxName;

		private FormManager formManager;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel16;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel17;

		private AllAccountsComboBox comboBoxProjectCostAccount;

		private AllAccountsComboBox comboBoxProjectIncomeAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel18;

		private AllAccountsComboBox comboBoxProjectWIPAccount;

		private MMTextBox textBoxProjectCostAccount;

		private MMTextBox textBoxProjectIncomeAccount;

		private MMTextBox textBoxProjectWIPAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel12;

		private AllAccountsComboBox comboBoxManuWIPAccount;

		private MMTextBox textBoxManuWIPAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel11;

		private AllAccountsComboBox comboBoxProjectTimesheetContra;

		private MMTextBox textBoxProjectTimesheetContra;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel10;

		private AllAccountsComboBox comboBoxManuTimesheetContra;

		private MMTextBox textBoxManuTimesheetContra;

		private MMTextBox textBoxRetentionAccountName;

		private AllAccountsComboBox comboBoxRetentionAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel13;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel9;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private AllAccountsComboBox comboBoxDiscountReceived;

		private MMTextBox textBoxDiscountReceivedAccountName;

		private AllAccountsComboBox comboBoxGainLossAccount;

		private MMTextBox textBoxGainLoss;

		private AllAccountsComboBox comboBoxDiscountGiven;

		private MMTextBox textBoxDiscountGivenAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private AllAccountsComboBox comboBoxSalesTax;

		private MMTextBox textBoxSalesTaxAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private AllAccountsComboBox comboBoxCOGSAccount;

		private MMTextBox textBoxGOGSAccountName;

		private MMTextBox textBoxInventoryAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private AllAccountsComboBox comboBoxAPAccount;

		private MMTextBox textBoxAP;

		private AllAccountsComboBox comboBoxSalesAccount;

		private MMTextBox textBoxSalesAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private AllAccountsComboBox comboBoxInventoryAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private MMTextBox textBoxARName;

		private AllAccountsComboBox comboBoxARAccount;

		private UltraTabPageControl tabPageProject;

		private MMTextBox textBoxAdvanceAccountName;

		private AllAccountsComboBox comboBoxAdvanceAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel14;

		private UltraTabPageControl tabPageManufacturing;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel15;

		private AllAccountsComboBox comboBoxEmployeeAccount;

		private MMTextBox textBoxEmpAccountName;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraGroupBox ultraGroupBox2;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel23;

		private MMTextBox textBoxConsignOutCOGS;

		private AllAccountsComboBox comboBoxConsignOutCOGS;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel24;

		private MMTextBox textBoxConsignOutSales;

		private AllAccountsComboBox comboBoxConsignOutSales;

		private UltraGroupBox ultraGroupBox1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel21;

		private MMTextBox textBoxConsignInDiff;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel20;

		private AllAccountsComboBox comboBoxConsignInDiff;

		private MMTextBox textBoxConsignInComm;

		private AllAccountsComboBox comboBoxConsignInCommissionAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel19;

		private MMTextBox textBoxConsignInAccount;

		private AllAccountsComboBox comboBoxConsignInAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel22;

		private AllAccountsComboBox comboBoxInventoryOnDNoteAccount;

		private MMTextBox mmTextBox1;

		private UltraTabPageControl ultraTabPageControl2;

		private DataEntryGrid dataEntryGridTax;

		private AllAccountsComboBox comboBoxGridSalesAccount;

		private AllAccountsComboBox comboBoxGridPurchaseAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel25;

		private AllAccountsComboBox comboBoxAllocationDiscount;

		private MMTextBox textBoxAllocationDiscount;

		private XPButton buttonCopy;

		private RadioButton radioButtonLocation;

		private LocationComboBox comboBoxLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel26;

		private AllAccountsComboBox comboBoxRoundOffAccount;

		private MMTextBox textboxRoundOffAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel27;

		private AllAccountsComboBox ComboBoxPurchasePrePaymentAccount;

		private MMTextBox textBoxPurchasePrePaymentAccount;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel28;

		private AllAccountsComboBox comboBoxPrepaymentPaybleAccount;

		private MMTextBox textBoxPrepaymentPayableAccount;

		private UltraTabPageControl ultraTabPageControl3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel29;

		private AllAccountsComboBox comboBoxTicketAccount;

		private MMTextBox textBoxTicketAccountName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel30;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel31;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel32;

		private AllAccountsComboBox comboBoxEOSBenefitAccount;

		private MMTextBox textBoxEOSBenefitAccountName;

		private MMTextBox textBoxLeaveExpenseAccountName;

		private AllAccountsComboBox comboBoxProvisionAccount;

		private MMTextBox textBoxProvisionAccountName;

		private AllAccountsComboBox comboBoxLeaveExpenseAccount;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6008;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public string ItemCode
		{
			get
			{
				return textBoxCode.Text;
			}
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string ItemName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		public bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public LocationAccountsForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void SetupTaxGrid()
		{
			dataEntryGridTax.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Tax");
			dataTable.Columns.Add("Sales Account", typeof(string));
			dataTable.Columns.Add("Purchase Account", typeof(string));
			dataTable.Columns.Add("Percent", typeof(decimal));
			dataEntryGridTax.DataSource = dataTable;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].MaxLength = 64;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Tax"].CellActivation = Activation.NoEdit;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].MaxLength = 64;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].ValueList = comboBoxGridSalesAccount;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Sales Account"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].CharacterCasing = CharacterCasing.Upper;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].MaxLength = 64;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].ValueList = comboBoxGridPurchaseAccount;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Purchase Account"].Header.Appearance.Cursor = Cursors.Hand;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].MinValue = 0;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].MaxLength = 100000;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].CellAppearance.TextHAlign = HAlign.Right;
			dataEntryGridTax.DisplayLayout.Bands[0].Columns["Percent"].Format = "#,##0.####";
			dataEntryGridTax.SetupUI();
		}

		private void AddEvents()
		{
			base.Load += LocationAccountsForm_Load;
			comboBoxGainLossAccount.SelectedIndexChanged += comboBoxGainLossAccount_SelectedIndexChanged;
			dataEntryGridTax.HeaderClicked += dataGridItems_HeaderClicked;
		}

		private void dataEntryGridTax_AfterCellUpdate(object sender, InitializeRowEventArgs e)
		{
		}

		private void comboBoxGainLossAccount_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxGainLoss.Text = comboBoxGainLossAccount.SelectedName;
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataEntryGridTax.ActiveRow == null)
			{
				return;
			}
			FormHelper formHelper = new FormHelper();
			string text = "";
			if (ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Tax")
			{
				if (!string.IsNullOrEmpty(dataEntryGridTax.ActiveRow.Cells["Tax"].Value.ToString()))
				{
					text = dataEntryGridTax.ActiveRow.Cells["Tax"].Value.ToString();
					formHelper.EditTax(text);
				}
			}
			else if (ultraGridColumn.Key == "Sales Account")
			{
				if (!string.IsNullOrEmpty(dataEntryGridTax.ActiveRow.Cells["Sales Account"].Value.ToString()))
				{
					text = dataEntryGridTax.ActiveRow.Cells["Sales Account"].Value.ToString();
					formHelper.EditAccount(text);
				}
			}
			else if (ultraGridColumn.Key == "Purchase Account" && !string.IsNullOrEmpty(dataEntryGridTax.ActiveRow.Cells["Purchase Account"].Value.ToString()))
			{
				text = dataEntryGridTax.ActiveRow.Cells["Purchase Account"].Value.ToString();
				formHelper.EditAccount(text);
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null)
				{
					currentData = new LocationData();
				}
				DataRow dataRow = currentData.LocationTable.Rows[0];
				dataRow.BeginEdit();
				if (comboBoxInventoryAccount.SelectedID != "")
				{
					dataRow["InventoryAccountID"] = comboBoxInventoryAccount.SelectedID;
				}
				else
				{
					dataRow["InventoryAccountID"] = DBNull.Value;
				}
				if (comboBoxSalesAccount.SelectedID != "")
				{
					dataRow["SalesAccountID"] = comboBoxSalesAccount.SelectedID;
				}
				else
				{
					dataRow["SalesAccountID"] = DBNull.Value;
				}
				if (comboBoxCOGSAccount.SelectedID != "")
				{
					dataRow["COGSAccountID"] = comboBoxCOGSAccount.SelectedID;
				}
				else
				{
					dataRow["COGSAccountID"] = DBNull.Value;
				}
				if (comboBoxInventoryOnDNoteAccount.SelectedID != "")
				{
					dataRow["UnInvoicedInventoryAccountID"] = comboBoxInventoryOnDNoteAccount.SelectedID;
				}
				else
				{
					dataRow["UnInvoicedInventoryAccountID"] = DBNull.Value;
				}
				if (comboBoxDiscountGiven.SelectedID != "")
				{
					dataRow["DiscountGivenAccountID"] = comboBoxDiscountGiven.SelectedID;
				}
				else
				{
					dataRow["DiscountGivenAccountID"] = DBNull.Value;
				}
				if (comboBoxDiscountReceived.SelectedID != "")
				{
					dataRow["DiscountReceivedAccountID"] = comboBoxDiscountReceived.SelectedID;
				}
				else
				{
					dataRow["DiscountReceivedAccountID"] = DBNull.Value;
				}
				if (comboBoxSalesTax.SelectedID != "")
				{
					dataRow["SalesTaxAccountID"] = comboBoxSalesTax.SelectedID;
				}
				else
				{
					dataRow["SalesTaxAccountID"] = DBNull.Value;
				}
				if (comboBoxARAccount.SelectedID != "")
				{
					dataRow["ARAccountID"] = comboBoxARAccount.SelectedID;
				}
				else
				{
					dataRow["ARAccountID"] = DBNull.Value;
				}
				if (comboBoxAPAccount.SelectedID != "")
				{
					dataRow["APAccountID"] = comboBoxAPAccount.SelectedID;
				}
				else
				{
					dataRow["APAccountID"] = DBNull.Value;
				}
				if (comboBoxEmployeeAccount.SelectedID != "")
				{
					dataRow["EmployeeAccountID"] = comboBoxEmployeeAccount.SelectedID;
				}
				else
				{
					dataRow["EmployeeAccountID"] = DBNull.Value;
				}
				if (comboBoxGainLossAccount.SelectedID != "")
				{
					dataRow["ExchangeGainLossAccountID"] = comboBoxGainLossAccount.SelectedID;
				}
				else
				{
					dataRow["ExchangeGainLossAccountID"] = DBNull.Value;
				}
				if (comboBoxProjectWIPAccount.SelectedID != "")
				{
					dataRow["ProjectWIPAccountID"] = comboBoxProjectWIPAccount.SelectedID;
				}
				else
				{
					dataRow["ProjectWIPAccountID"] = DBNull.Value;
				}
				if (comboBoxProjectIncomeAccount.SelectedID != "")
				{
					dataRow["ProjectIncomeAccountID"] = comboBoxProjectIncomeAccount.SelectedID;
				}
				else
				{
					dataRow["ProjectIncomeAccountID"] = DBNull.Value;
				}
				if (comboBoxProjectCostAccount.SelectedID != "")
				{
					dataRow["ProjectCostAccountID"] = comboBoxProjectCostAccount.SelectedID;
				}
				else
				{
					dataRow["ProjectCostAccountID"] = DBNull.Value;
				}
				if (comboBoxProjectTimesheetContra.SelectedID != "")
				{
					dataRow["ProjectTimesheetContraAccountID"] = comboBoxProjectTimesheetContra.SelectedID;
				}
				else
				{
					dataRow["ProjectTimesheetContraAccountID"] = DBNull.Value;
				}
				if (comboBoxRetentionAccount.SelectedID != "")
				{
					dataRow["ProjectRetentionAccountID"] = comboBoxRetentionAccount.SelectedID;
				}
				else
				{
					dataRow["ProjectRetentionAccountID"] = DBNull.Value;
				}
				if (comboBoxAdvanceAccount.SelectedID != "")
				{
					dataRow["ProjectAdvanceAccountID"] = comboBoxAdvanceAccount.SelectedID;
				}
				else
				{
					dataRow["ProjectAdvanceAccountID"] = DBNull.Value;
				}
				if (comboBoxManuWIPAccount.SelectedID != "")
				{
					dataRow["ManuWIPAccountID"] = comboBoxManuWIPAccount.SelectedID;
				}
				else
				{
					dataRow["ManuWIPAccountID"] = DBNull.Value;
				}
				if (comboBoxManuTimesheetContra.SelectedID != "")
				{
					dataRow["ManuTimesheetContraAccountID"] = comboBoxManuTimesheetContra.SelectedID;
				}
				else
				{
					dataRow["ManuTimesheetContraAccountID"] = DBNull.Value;
				}
				if (comboBoxConsignInAccount.SelectedID != "")
				{
					dataRow["ConsignInAccountID"] = comboBoxConsignInAccount.SelectedID;
				}
				else
				{
					dataRow["ConsignInAccountID"] = DBNull.Value;
				}
				if (comboBoxConsignInCommissionAccount.SelectedID != "")
				{
					dataRow["ConsignInCommissionAccountID"] = comboBoxConsignInCommissionAccount.SelectedID;
				}
				else
				{
					dataRow["ConsignInCommissionAccountID"] = DBNull.Value;
				}
				if (comboBoxConsignInDiff.SelectedID != "")
				{
					dataRow["ConsignInDiffAccountID"] = comboBoxConsignInDiff.SelectedID;
				}
				else
				{
					dataRow["ConsignInDiffAccountID"] = DBNull.Value;
				}
				if (comboBoxConsignOutSales.SelectedID != "")
				{
					dataRow["ConsignOutSalesAccountID"] = comboBoxConsignOutSales.SelectedID;
				}
				else
				{
					dataRow["ConsignOutSalesAccountID"] = DBNull.Value;
				}
				if (comboBoxConsignOutCOGS.SelectedID != "")
				{
					dataRow["ConsignOutCOGSAccountID"] = comboBoxConsignOutCOGS.SelectedID;
				}
				else
				{
					dataRow["ConsignOutCOGSAccountID"] = DBNull.Value;
				}
				if (comboBoxAllocationDiscount.SelectedID != "")
				{
					dataRow["AllocationDiscountAccountID"] = comboBoxAllocationDiscount.SelectedID;
				}
				else
				{
					dataRow["AllocationDiscountAccountID"] = DBNull.Value;
				}
				if (comboBoxRoundOffAccount.SelectedID != "")
				{
					dataRow["RoundOffAccountID"] = comboBoxRoundOffAccount.SelectedID;
				}
				else
				{
					dataRow["RoundOffAccountID"] = DBNull.Value;
				}
				if (ComboBoxPurchasePrePaymentAccount.SelectedID != "")
				{
					dataRow["PurchasePrePaymentAccountID"] = ComboBoxPurchasePrePaymentAccount.SelectedID;
				}
				else
				{
					dataRow["PurchasePrePaymentAccountID"] = DBNull.Value;
				}
				if (comboBoxPrepaymentPaybleAccount.SelectedID != "")
				{
					dataRow["PrepaymentAPAccountID"] = comboBoxPrepaymentPaybleAccount.SelectedID;
				}
				else
				{
					dataRow["PrepaymentAPAccountID"] = DBNull.Value;
				}
				dataRow.EndEdit();
				if (comboBoxLeaveExpenseAccount.SelectedID != "")
				{
					dataRow["LeaveExpenseAccountID"] = comboBoxLeaveExpenseAccount.SelectedID;
				}
				else
				{
					dataRow["LeaveExpenseAccountID"] = DBNull.Value;
				}
				if (comboBoxProvisionAccount.SelectedID != "")
				{
					dataRow["ProvisionAccountID"] = comboBoxProvisionAccount.SelectedID;
				}
				else
				{
					dataRow["ProvisionAccountID"] = DBNull.Value;
				}
				if (comboBoxEOSBenefitAccount.SelectedID != "")
				{
					dataRow["EOSBenefitAccountID"] = comboBoxEOSBenefitAccount.SelectedID;
				}
				else
				{
					dataRow["EOSBenefitAccountID"] = DBNull.Value;
				}
				if (comboBoxTicketAccount.SelectedID != "")
				{
					dataRow["TicketAccountID"] = comboBoxTicketAccount.SelectedID;
				}
				else
				{
					dataRow["TicketAccountID"] = DBNull.Value;
				}
				currentData.TaxLocationDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataEntryGridTax.Rows)
				{
					DataRow dataRow2 = currentData.TaxLocationDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["PurchaseAccountID"] = row.Cells["Purchase Account"].Value.ToString();
					dataRow2["SalesAccountID"] = row.Cells["Sales Account"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["Percent"].Value.ToString()))
					{
						dataRow2["TaxPercent"] = row.Cells["Percent"].Value.ToString();
					}
					else
					{
						dataRow2["TaxPercent"] = 0;
					}
					dataRow2["TaxID"] = row.Cells["Tax"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					currentData.TaxLocationDetailTable.Rows.Add(dataRow2);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void LoadTaxData()
		{
			taxData = Factory.TaxSystem.GetTaxList();
			if (taxData != null && taxData.Tables.Count != 0 && taxData.Tables[0].Rows.Count != 0)
			{
				FillTaxData();
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			textBoxCode.Focus();
		}

		public void LoadData(LocationData data)
		{
			try
			{
				currentData = data;
				if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
				{
					textBoxCode.Focus();
				}
				else
				{
					FillData();
					formManager.ResetDirty();
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
				comboBoxInventoryAccount.SelectedID = dataRow["InventoryAccountID"].ToString();
				comboBoxSalesAccount.SelectedID = dataRow["SalesAccountID"].ToString();
				comboBoxCOGSAccount.SelectedID = dataRow["COGSAccountID"].ToString();
				comboBoxInventoryOnDNoteAccount.SelectedID = dataRow["UnInvoicedInventoryAccountID"].ToString();
				comboBoxDiscountGiven.SelectedID = dataRow["DiscountGivenAccountID"].ToString();
				comboBoxDiscountReceived.SelectedID = dataRow["DiscountReceivedAccountID"].ToString();
				comboBoxSalesTax.SelectedID = dataRow["SalesTaxAccountID"].ToString();
				comboBoxARAccount.SelectedID = dataRow["ARAccountID"].ToString();
				comboBoxAPAccount.SelectedID = dataRow["APAccountID"].ToString();
				comboBoxEmployeeAccount.SelectedID = dataRow["EmployeeAccountID"].ToString();
				comboBoxGainLossAccount.SelectedID = dataRow["ExchangeGainLossAccountID"].ToString();
				comboBoxProjectWIPAccount.SelectedID = dataRow["ProjectWIPAccountID"].ToString();
				comboBoxProjectIncomeAccount.SelectedID = dataRow["ProjectIncomeAccountID"].ToString();
				comboBoxProjectCostAccount.SelectedID = dataRow["ProjectCostAccountID"].ToString();
				comboBoxProjectTimesheetContra.SelectedID = dataRow["ProjectTimesheetContraAccountID"].ToString();
				comboBoxRetentionAccount.SelectedID = dataRow["ProjectRetentionAccountID"].ToString();
				comboBoxAdvanceAccount.SelectedID = dataRow["ProjectAdvanceAccountID"].ToString();
				comboBoxManuWIPAccount.SelectedID = dataRow["ManuWIPAccountID"].ToString();
				comboBoxManuTimesheetContra.SelectedID = dataRow["ManuTimesheetContraAccountID"].ToString();
				comboBoxConsignInAccount.SelectedID = dataRow["ConsignInAccountID"].ToString();
				comboBoxConsignInCommissionAccount.SelectedID = dataRow["ConsignInCommissionAccountID"].ToString();
				comboBoxConsignInDiff.SelectedID = dataRow["ConsignInDiffAccountID"].ToString();
				comboBoxConsignOutSales.SelectedID = dataRow["ConsignOutSalesAccountID"].ToString();
				comboBoxConsignOutCOGS.SelectedID = dataRow["ConsignOutCOGSAccountID"].ToString();
				comboBoxAllocationDiscount.SelectedID = dataRow["AllocationDiscountAccountID"].ToString();
				comboBoxRoundOffAccount.SelectedID = dataRow["RoundOffAccountID"].ToString();
				ComboBoxPurchasePrePaymentAccount.SelectedID = dataRow["PurchasePrePaymentAccountID"].ToString();
				comboBoxPrepaymentPaybleAccount.SelectedID = dataRow["PrepaymentAPAccountID"].ToString();
				comboBoxLeaveExpenseAccount.SelectedID = dataRow["LeaveExpenseAccountID"].ToString();
				comboBoxProvisionAccount.SelectedID = dataRow["ProvisionAccountID"].ToString();
				comboBoxEOSBenefitAccount.SelectedID = dataRow["EOSBenefitAccountID"].ToString();
				comboBoxTicketAccount.SelectedID = dataRow["TicketAccountID"].ToString();
				SetupTaxGrid();
				DataTable dataTable = dataEntryGridTax.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (currentData.Tables.Contains("LocationAccounts_Tax_Detail") && currentData.TaxLocationDetailTable.Rows.Count != 0)
				{
					foreach (DataRow row in currentData.Tables["LocationAccounts_Tax_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Tax"] = row["TaxID"];
						dataRow3["Sales Account"] = row["SalesAccountID"];
						dataRow3["Purchase Account"] = row["PurchaseAccountID"];
						dataRow3["Percent"] = row["TaxPercent"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
				}
			}
		}

		private void FillTaxData()
		{
			if (taxData != null && taxData.Tables.Count != 0 && taxData.Tables[0].Rows.Count != 0)
			{
				_ = dataEntryGridTax.Rows.Count;
				DataTable dataTable = dataEntryGridTax.DataSource as DataTable;
				_ = dataEntryGridTax.DataSource;
				foreach (DataRow row in taxData.Tables["Tax"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Tax"] = row["Tax Code"];
					dataTable.Rows.Add(dataRow2);
					dataTable.AcceptChanges();
				}
			}
		}

		private bool SaveData()
		{
			if (!GetData())
			{
				return false;
			}
			try
			{
				Close();
				return true;
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
			comboBoxSalesTax.Clear();
			comboBoxSalesAccount.Clear();
			comboBoxInventoryAccount.Clear();
			comboBoxCOGSAccount.Clear();
			comboBoxDiscountReceived.Clear();
			comboBoxDiscountGiven.Clear();
			comboBoxARAccount.Clear();
			comboBoxAPAccount.Clear();
			comboBoxInventoryOnDNoteAccount.Clear();
			comboBoxEmployeeAccount.Clear();
			textBoxDiscountGivenAccountName.Clear();
			textBoxDiscountReceivedAccountName.Clear();
			textBoxGOGSAccountName.Clear();
			textBoxInventoryAccountName.Clear();
			textBoxSalesAccountName.Clear();
			textBoxSalesTaxAccountName.Clear();
			textBoxGainLoss.Clear();
			comboBoxGainLossAccount.Clear();
			textBoxARName.Clear();
			textBoxAP.Clear();
			textBoxLeaveExpenseAccountName.Clear();
			textBoxProvisionAccountName.Clear();
			textBoxEOSBenefitAccountName.Clear();
			textBoxTicketAccountName.Clear();
			comboBoxManuWIPAccount.Clear();
			comboBoxProjectWIPAccount.Clear();
			comboBoxProjectIncomeAccount.Clear();
			comboBoxProjectCostAccount.Clear();
			comboBoxManuTimesheetContra.Clear();
			comboBoxProjectTimesheetContra.Clear();
			comboBoxRetentionAccount.Clear();
			comboBoxAdvanceAccount.Clear();
			comboBoxConsignInAccount.Clear();
			comboBoxConsignInCommissionAccount.Clear();
			comboBoxConsignInDiff.Clear();
			comboBoxConsignOutCOGS.Clear();
			comboBoxConsignOutSales.Clear();
			comboBoxAllocationDiscount.Clear();
			comboBoxRoundOffAccount.Clear();
			ComboBoxPurchasePrePaymentAccount.Clear();
			comboBoxPrepaymentPaybleAccount.Clear();
			comboBoxLeaveExpenseAccount.Clear();
			comboBoxProvisionAccount.Clear();
			comboBoxEOSBenefitAccount.Clear();
			comboBoxTicketAccount.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void LocationGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void LocationGroupDetailsForm_Validated(object sender, EventArgs e)
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				return Factory.LocationSystem.DeleteLocation(textBoxCode.Text);
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
			return true;
		}

		private void LocationAccountsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
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
			new FormHelper().ShowList(DataComboType.Location);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxInventoryAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxCOGSAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxSalesTax.SelectedID);
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountGiven.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxDiscountReceived.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxARAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAPAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel9_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxGainLossAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel11_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxProjectTimesheetContra.SelectedID);
		}

		private void ultraFormattedLinkLabel10_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxManuTimesheetContra.SelectedID);
		}

		private void ultraFormattedLinkLabel12_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxManuWIPAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel16_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxProjectCostAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel17_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxProjectIncomeAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel18_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxProjectWIPAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel13_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxRetentionAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel14_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxAdvanceAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel15_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxEmployeeAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel22_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxInventoryOnDNoteAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel19_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxConsignInAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel20_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxConsignInCommissionAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel21_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxConsignInDiff.SelectedID);
		}

		private void ultraFormattedLinkLabel24_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxConsignOutSales.SelectedID);
		}

		private void ultraFormattedLinkLabel23_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxConsignOutCOGS.SelectedID);
		}

		private void buttonCopy_Click(object sender, EventArgs e)
		{
			if (!(comboBoxLocation.SelectedID == ""))
			{
				currentData = Factory.LocationSystem.GetLocationByID(comboBoxLocation.SelectedID);
				LoadData(currentData);
				formManager.IsForcedDirty = true;
				IsNewRecord = false;
				base.DialogResult = DialogResult.None;
			}
		}

		private void radioButtonLocation_CheckedChanged(object sender, EventArgs e)
		{
			comboBoxLocation.Enabled = radioButtonLocation.Checked;
		}

		private void ultraFormattedLinkLabel25_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxGainLossAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel26_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel28_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxPrepaymentPaybleAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel30_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxLeaveExpenseAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel31_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxProvisionAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel32_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxEOSBenefitAccount.SelectedID);
		}

		private void ultraFormattedLinkLabel29_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditAccount(comboBoxTicketAccount.SelectedID);
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
			Infragistics.Win.Appearance appearance214 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance215 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance216 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance217 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance218 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance219 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance220 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance221 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance222 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance223 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance224 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance225 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance226 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance227 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance228 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance229 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance230 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance231 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance232 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance233 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance234 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance235 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance236 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance237 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance238 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance239 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance240 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance241 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance242 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance243 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance244 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance245 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance246 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance247 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance248 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance249 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance250 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance251 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance252 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance253 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance254 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance255 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance256 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance257 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance258 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance259 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance260 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance261 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance262 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance263 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance264 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance265 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance266 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance267 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance268 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance269 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance270 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance271 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance272 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance273 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance274 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance275 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance276 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance277 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance278 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance279 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance280 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance281 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance282 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance283 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance284 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance285 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance286 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance287 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance288 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance289 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance290 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance291 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance292 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance293 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance294 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance295 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance296 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance297 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance298 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance299 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance300 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance301 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance302 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance303 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance304 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance305 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance306 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance307 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance308 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance309 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance310 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance311 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance312 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance313 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance314 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance315 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance316 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance317 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance318 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance319 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance320 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance321 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance322 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance323 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance324 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance325 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance326 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance327 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance328 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance329 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance330 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance331 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance332 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance333 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance334 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance335 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance336 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance337 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance338 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance339 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance340 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance341 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance342 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance343 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance344 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance345 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance346 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance347 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance348 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance349 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance350 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance351 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance352 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance353 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance354 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance355 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance356 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance357 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance358 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance359 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance360 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance361 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance362 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance363 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance364 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance365 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance366 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance367 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance368 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance369 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance370 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance371 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance372 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance373 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance374 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance375 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance376 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance377 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance378 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance379 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance380 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance381 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance382 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance383 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance384 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance385 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance386 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance387 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance388 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance389 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance390 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance391 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance392 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance393 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance394 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance395 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance396 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance397 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance398 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance399 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance400 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance401 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance402 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance403 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance404 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance405 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance406 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance407 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance408 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance409 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance410 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance411 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance412 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance413 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance414 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance415 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance416 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance417 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance418 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance419 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance420 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance421 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance422 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance423 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance424 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance425 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance426 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance427 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance428 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance429 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance430 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance431 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance432 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance433 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance434 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance435 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance436 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance437 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance438 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance439 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance440 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance441 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance442 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance443 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance444 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance445 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance446 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance447 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance448 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance449 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance450 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance451 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance452 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance453 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance454 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance455 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance456 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance457 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance458 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance459 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance460 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance461 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance462 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance463 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance464 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance465 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance466 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance467 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance468 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance469 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance470 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance471 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.LocationAccountsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel28 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPrepaymentPaybleAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPrepaymentPayableAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel27 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ComboBoxPurchasePrePaymentAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxPurchasePrePaymentAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel26 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRoundOffAccount = new Micromind.DataControls.AllAccountsComboBox();
			textboxRoundOffAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel25 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAllocationDiscount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAllocationDiscount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel22 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxInventoryOnDNoteAccount = new Micromind.DataControls.AllAccountsComboBox();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel15 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEmployeeAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxEmpAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel9 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDiscountReceived = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountReceivedAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxGainLossAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxGainLoss = new Micromind.UISupport.MMTextBox();
			comboBoxDiscountGiven = new Micromind.DataControls.AllAccountsComboBox();
			textBoxDiscountGivenAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSalesTax = new Micromind.DataControls.AllAccountsComboBox();
			textBoxSalesTaxAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCOGSAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxGOGSAccountName = new Micromind.UISupport.MMTextBox();
			textBoxInventoryAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxAPAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxAP = new Micromind.UISupport.MMTextBox();
			comboBoxSalesAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxSalesAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxInventoryAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxARName = new Micromind.UISupport.MMTextBox();
			comboBoxARAccount = new Micromind.DataControls.AllAccountsComboBox();
			tabPageProject = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			textBoxAdvanceAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxAdvanceAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel14 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxRetentionAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel18 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxRetentionAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProjectWIPAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel13 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxProjectIncomeAccount = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel11 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxProjectCostAccount = new Micromind.UISupport.MMTextBox();
			comboBoxProjectTimesheetContra = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProjectTimesheetContra = new Micromind.UISupport.MMTextBox();
			comboBoxProjectWIPAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxProjectIncomeAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel16 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxProjectCostAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel17 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			tabPageManufacturing = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel10 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel12 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxManuWIPAccount = new Micromind.UISupport.MMTextBox();
			comboBoxManuTimesheetContra = new Micromind.DataControls.AllAccountsComboBox();
			textBoxManuTimesheetContra = new Micromind.UISupport.MMTextBox();
			comboBoxManuWIPAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel23 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxConsignOutCOGS = new Micromind.UISupport.MMTextBox();
			comboBoxConsignOutCOGS = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel24 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxConsignOutSales = new Micromind.UISupport.MMTextBox();
			comboBoxConsignOutSales = new Micromind.DataControls.AllAccountsComboBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			ultraFormattedLinkLabel21 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxConsignInDiff = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel20 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxConsignInDiff = new Micromind.DataControls.AllAccountsComboBox();
			textBoxConsignInComm = new Micromind.UISupport.MMTextBox();
			comboBoxConsignInCommissionAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraFormattedLinkLabel19 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxConsignInAccount = new Micromind.UISupport.MMTextBox();
			comboBoxConsignInAccount = new Micromind.DataControls.AllAccountsComboBox();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataEntryGridTax = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridPurchaseAccount = new Micromind.DataControls.AllAccountsComboBox();
			comboBoxGridSalesAccount = new Micromind.DataControls.AllAccountsComboBox();
			panelButtons = new System.Windows.Forms.Panel();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			radioButtonLocation = new System.Windows.Forms.RadioButton();
			buttonCopy = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			ultraFormattedLinkLabel29 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxTicketAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxTicketAccountName = new Micromind.UISupport.MMTextBox();
			ultraFormattedLinkLabel30 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel31 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel32 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxEOSBenefitAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxEOSBenefitAccountName = new Micromind.UISupport.MMTextBox();
			textBoxLeaveExpenseAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxProvisionAccount = new Micromind.DataControls.AllAccountsComboBox();
			textBoxProvisionAccountName = new Micromind.UISupport.MMTextBox();
			comboBoxLeaveExpenseAccount = new Micromind.DataControls.AllAccountsComboBox();
			tabPageGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPrepaymentPaybleAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxPurchasePrePaymentAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRoundOffAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAllocationDiscount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryOnDNoteAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployeeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountReceived).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGainLossAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).BeginInit();
			tabPageProject.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetentionAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectTimesheetContra).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectWIPAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectIncomeAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).BeginInit();
			tabPageManufacturing.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxManuTimesheetContra).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManuWIPAccount).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutCOGS).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutSales).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInDiff).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInCommissionAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInAccount).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTax).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPurchaseAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridSalesAccount).BeginInit();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTicketAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEOSBenefitAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProvisionAccount).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveExpenseAccount).BeginInit();
			SuspendLayout();
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel28);
			tabPageGeneral.Controls.Add(comboBoxPrepaymentPaybleAccount);
			tabPageGeneral.Controls.Add(textBoxPrepaymentPayableAccount);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel27);
			tabPageGeneral.Controls.Add(ComboBoxPurchasePrePaymentAccount);
			tabPageGeneral.Controls.Add(textBoxPurchasePrePaymentAccount);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel26);
			tabPageGeneral.Controls.Add(comboBoxRoundOffAccount);
			tabPageGeneral.Controls.Add(textboxRoundOffAccount);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel25);
			tabPageGeneral.Controls.Add(comboBoxAllocationDiscount);
			tabPageGeneral.Controls.Add(textBoxAllocationDiscount);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel22);
			tabPageGeneral.Controls.Add(comboBoxInventoryOnDNoteAccount);
			tabPageGeneral.Controls.Add(mmTextBox1);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel15);
			tabPageGeneral.Controls.Add(comboBoxEmployeeAccount);
			tabPageGeneral.Controls.Add(textBoxEmpAccountName);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel9);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel1);
			tabPageGeneral.Controls.Add(comboBoxDiscountReceived);
			tabPageGeneral.Controls.Add(comboBoxGainLossAccount);
			tabPageGeneral.Controls.Add(comboBoxDiscountGiven);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel2);
			tabPageGeneral.Controls.Add(textBoxGainLoss);
			tabPageGeneral.Controls.Add(comboBoxSalesTax);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel3);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel8);
			tabPageGeneral.Controls.Add(comboBoxCOGSAccount);
			tabPageGeneral.Controls.Add(textBoxInventoryAccountName);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel4);
			tabPageGeneral.Controls.Add(comboBoxAPAccount);
			tabPageGeneral.Controls.Add(comboBoxSalesAccount);
			tabPageGeneral.Controls.Add(textBoxSalesAccountName);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel5);
			tabPageGeneral.Controls.Add(textBoxAP);
			tabPageGeneral.Controls.Add(comboBoxInventoryAccount);
			tabPageGeneral.Controls.Add(textBoxGOGSAccountName);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel6);
			tabPageGeneral.Controls.Add(ultraFormattedLinkLabel7);
			tabPageGeneral.Controls.Add(textBoxDiscountReceivedAccountName);
			tabPageGeneral.Controls.Add(textBoxSalesTaxAccountName);
			tabPageGeneral.Controls.Add(textBoxARName);
			tabPageGeneral.Controls.Add(comboBoxARAccount);
			tabPageGeneral.Controls.Add(textBoxDiscountGivenAccountName);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(643, 365);
			appearance.FontData.BoldAsString = "False";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel28.Appearance = appearance;
			ultraFormattedLinkLabel28.AutoSize = true;
			ultraFormattedLinkLabel28.Location = new System.Drawing.Point(9, 329);
			ultraFormattedLinkLabel28.Name = "ultraFormattedLinkLabel28";
			ultraFormattedLinkLabel28.Size = new System.Drawing.Size(107, 15);
			ultraFormattedLinkLabel28.TabIndex = 182;
			ultraFormattedLinkLabel28.TabStop = true;
			ultraFormattedLinkLabel28.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel28.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel28.Value = "Prepayment Payable:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel28.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel28.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel28_LinkClicked);
			comboBoxPrepaymentPaybleAccount.AlwaysInEditMode = true;
			comboBoxPrepaymentPaybleAccount.Assigned = false;
			comboBoxPrepaymentPaybleAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPrepaymentPaybleAccount.CustomReportFieldName = "";
			comboBoxPrepaymentPaybleAccount.CustomReportKey = "";
			comboBoxPrepaymentPaybleAccount.CustomReportValueType = 1;
			comboBoxPrepaymentPaybleAccount.DescriptionTextBox = textBoxPrepaymentPayableAccount;
			appearance3.BackColor = System.Drawing.SystemColors.Window;
			appearance3.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Appearance = appearance3;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance4.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance4.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance4.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.GroupByBox.Appearance = appearance4;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance5;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance6.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance6.BackColor2 = System.Drawing.SystemColors.Control;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance6.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance6;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.ActiveCellAppearance = appearance7;
			appearance8.BackColor = System.Drawing.SystemColors.Highlight;
			appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.ActiveRowAppearance = appearance8;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.CardAreaAppearance = appearance9;
			appearance10.BorderColor = System.Drawing.Color.Silver;
			appearance10.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.CellAppearance = appearance10;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.CellPadding = 0;
			appearance11.BackColor = System.Drawing.SystemColors.Control;
			appearance11.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance11.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance11.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.GroupByRowAppearance = appearance11;
			appearance12.TextHAlignAsString = "Left";
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.HeaderAppearance = appearance12;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.Color.Silver;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.RowAppearance = appearance13;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance14;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPrepaymentPaybleAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPrepaymentPaybleAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPrepaymentPaybleAccount.Editable = true;
			comboBoxPrepaymentPaybleAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxPrepaymentPaybleAccount.FilterString = "";
			comboBoxPrepaymentPaybleAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxPrepaymentPaybleAccount.FilterSysDocID = "";
			comboBoxPrepaymentPaybleAccount.HasAllAccount = false;
			comboBoxPrepaymentPaybleAccount.HasCustom = false;
			comboBoxPrepaymentPaybleAccount.IsDataLoaded = false;
			comboBoxPrepaymentPaybleAccount.Location = new System.Drawing.Point(132, 327);
			comboBoxPrepaymentPaybleAccount.MaxDropDownItems = 12;
			comboBoxPrepaymentPaybleAccount.Name = "comboBoxPrepaymentPaybleAccount";
			comboBoxPrepaymentPaybleAccount.ShowInactiveItems = false;
			comboBoxPrepaymentPaybleAccount.ShowQuickAdd = true;
			comboBoxPrepaymentPaybleAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxPrepaymentPaybleAccount.TabIndex = 180;
			comboBoxPrepaymentPaybleAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPrepaymentPayableAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPrepaymentPayableAccount.CustomReportFieldName = "";
			textBoxPrepaymentPayableAccount.CustomReportKey = "";
			textBoxPrepaymentPayableAccount.CustomReportValueType = 1;
			textBoxPrepaymentPayableAccount.ForeColor = System.Drawing.Color.Black;
			textBoxPrepaymentPayableAccount.IsComboTextBox = false;
			textBoxPrepaymentPayableAccount.IsModified = false;
			textBoxPrepaymentPayableAccount.Location = new System.Drawing.Point(251, 328);
			textBoxPrepaymentPayableAccount.MaxLength = 255;
			textBoxPrepaymentPayableAccount.Name = "textBoxPrepaymentPayableAccount";
			textBoxPrepaymentPayableAccount.ReadOnly = true;
			textBoxPrepaymentPayableAccount.Size = new System.Drawing.Size(303, 21);
			textBoxPrepaymentPayableAccount.TabIndex = 181;
			textBoxPrepaymentPayableAccount.TabStop = false;
			appearance15.FontData.BoldAsString = "False";
			appearance15.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel27.Appearance = appearance15;
			ultraFormattedLinkLabel27.AutoSize = true;
			ultraFormattedLinkLabel27.Location = new System.Drawing.Point(9, 306);
			ultraFormattedLinkLabel27.Name = "ultraFormattedLinkLabel27";
			ultraFormattedLinkLabel27.Size = new System.Drawing.Size(114, 15);
			ultraFormattedLinkLabel27.TabIndex = 179;
			ultraFormattedLinkLabel27.TabStop = true;
			ultraFormattedLinkLabel27.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel27.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel27.Value = "Purchase PrePayment:";
			appearance16.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel27.VisitedLinkAppearance = appearance16;
			ComboBoxPurchasePrePaymentAccount.AlwaysInEditMode = true;
			ComboBoxPurchasePrePaymentAccount.Assigned = false;
			ComboBoxPurchasePrePaymentAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxPurchasePrePaymentAccount.CustomReportFieldName = "";
			ComboBoxPurchasePrePaymentAccount.CustomReportKey = "";
			ComboBoxPurchasePrePaymentAccount.CustomReportValueType = 1;
			ComboBoxPurchasePrePaymentAccount.DescriptionTextBox = textBoxPurchasePrePaymentAccount;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Appearance = appearance17;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.CellAppearance = appearance24;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.HeaderAppearance = appearance26;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.RowAppearance = appearance27;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxPurchasePrePaymentAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxPurchasePrePaymentAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxPurchasePrePaymentAccount.Editable = true;
			ComboBoxPurchasePrePaymentAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			ComboBoxPurchasePrePaymentAccount.FilterString = "";
			ComboBoxPurchasePrePaymentAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			ComboBoxPurchasePrePaymentAccount.FilterSysDocID = "";
			ComboBoxPurchasePrePaymentAccount.HasAllAccount = false;
			ComboBoxPurchasePrePaymentAccount.HasCustom = false;
			ComboBoxPurchasePrePaymentAccount.IsDataLoaded = false;
			ComboBoxPurchasePrePaymentAccount.Location = new System.Drawing.Point(132, 304);
			ComboBoxPurchasePrePaymentAccount.MaxDropDownItems = 12;
			ComboBoxPurchasePrePaymentAccount.Name = "ComboBoxPurchasePrePaymentAccount";
			ComboBoxPurchasePrePaymentAccount.ShowInactiveItems = false;
			ComboBoxPurchasePrePaymentAccount.ShowQuickAdd = true;
			ComboBoxPurchasePrePaymentAccount.Size = new System.Drawing.Size(117, 21);
			ComboBoxPurchasePrePaymentAccount.TabIndex = 177;
			ComboBoxPurchasePrePaymentAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxPurchasePrePaymentAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPurchasePrePaymentAccount.CustomReportFieldName = "";
			textBoxPurchasePrePaymentAccount.CustomReportKey = "";
			textBoxPurchasePrePaymentAccount.CustomReportValueType = 1;
			textBoxPurchasePrePaymentAccount.ForeColor = System.Drawing.Color.Black;
			textBoxPurchasePrePaymentAccount.IsComboTextBox = false;
			textBoxPurchasePrePaymentAccount.IsModified = false;
			textBoxPurchasePrePaymentAccount.Location = new System.Drawing.Point(251, 305);
			textBoxPurchasePrePaymentAccount.MaxLength = 255;
			textBoxPurchasePrePaymentAccount.Name = "textBoxPurchasePrePaymentAccount";
			textBoxPurchasePrePaymentAccount.ReadOnly = true;
			textBoxPurchasePrePaymentAccount.Size = new System.Drawing.Size(303, 21);
			textBoxPurchasePrePaymentAccount.TabIndex = 178;
			textBoxPurchasePrePaymentAccount.TabStop = false;
			appearance29.FontData.BoldAsString = "False";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel26.Appearance = appearance29;
			ultraFormattedLinkLabel26.AutoSize = true;
			ultraFormattedLinkLabel26.Location = new System.Drawing.Point(9, 284);
			ultraFormattedLinkLabel26.Name = "ultraFormattedLinkLabel26";
			ultraFormattedLinkLabel26.Size = new System.Drawing.Size(58, 15);
			ultraFormattedLinkLabel26.TabIndex = 176;
			ultraFormattedLinkLabel26.TabStop = true;
			ultraFormattedLinkLabel26.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel26.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel26.Value = "Round Off:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel26.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel26.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel26_LinkClicked);
			comboBoxRoundOffAccount.AlwaysInEditMode = true;
			comboBoxRoundOffAccount.Assigned = false;
			comboBoxRoundOffAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRoundOffAccount.CustomReportFieldName = "";
			comboBoxRoundOffAccount.CustomReportKey = "";
			comboBoxRoundOffAccount.CustomReportValueType = 1;
			comboBoxRoundOffAccount.DescriptionTextBox = textboxRoundOffAccount;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxRoundOffAccount.DisplayLayout.Appearance = appearance31;
			comboBoxRoundOffAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxRoundOffAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRoundOffAccount.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRoundOffAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxRoundOffAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxRoundOffAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxRoundOffAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxRoundOffAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxRoundOffAccount.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxRoundOffAccount.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxRoundOffAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxRoundOffAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxRoundOffAccount.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxRoundOffAccount.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxRoundOffAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxRoundOffAccount.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxRoundOffAccount.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxRoundOffAccount.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxRoundOffAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxRoundOffAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxRoundOffAccount.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxRoundOffAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxRoundOffAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
			comboBoxRoundOffAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxRoundOffAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxRoundOffAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxRoundOffAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRoundOffAccount.Editable = true;
			comboBoxRoundOffAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxRoundOffAccount.FilterString = "";
			comboBoxRoundOffAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxRoundOffAccount.FilterSysDocID = "";
			comboBoxRoundOffAccount.HasAllAccount = false;
			comboBoxRoundOffAccount.HasCustom = false;
			comboBoxRoundOffAccount.IsDataLoaded = false;
			comboBoxRoundOffAccount.Location = new System.Drawing.Point(132, 282);
			comboBoxRoundOffAccount.MaxDropDownItems = 12;
			comboBoxRoundOffAccount.Name = "comboBoxRoundOffAccount";
			comboBoxRoundOffAccount.ShowInactiveItems = false;
			comboBoxRoundOffAccount.ShowQuickAdd = true;
			comboBoxRoundOffAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxRoundOffAccount.TabIndex = 174;
			comboBoxRoundOffAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textboxRoundOffAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textboxRoundOffAccount.CustomReportFieldName = "";
			textboxRoundOffAccount.CustomReportKey = "";
			textboxRoundOffAccount.CustomReportValueType = 1;
			textboxRoundOffAccount.ForeColor = System.Drawing.Color.Black;
			textboxRoundOffAccount.IsComboTextBox = false;
			textboxRoundOffAccount.IsModified = false;
			textboxRoundOffAccount.Location = new System.Drawing.Point(251, 282);
			textboxRoundOffAccount.MaxLength = 255;
			textboxRoundOffAccount.Name = "textboxRoundOffAccount";
			textboxRoundOffAccount.ReadOnly = true;
			textboxRoundOffAccount.Size = new System.Drawing.Size(303, 21);
			textboxRoundOffAccount.TabIndex = 175;
			textboxRoundOffAccount.TabStop = false;
			appearance43.FontData.BoldAsString = "False";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel25.Appearance = appearance43;
			ultraFormattedLinkLabel25.AutoSize = true;
			ultraFormattedLinkLabel25.Location = new System.Drawing.Point(9, 262);
			ultraFormattedLinkLabel25.Name = "ultraFormattedLinkLabel25";
			ultraFormattedLinkLabel25.Size = new System.Drawing.Size(100, 15);
			ultraFormattedLinkLabel25.TabIndex = 173;
			ultraFormattedLinkLabel25.TabStop = true;
			ultraFormattedLinkLabel25.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel25.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel25.Value = "Allocation Discount:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel25.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel25.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel25_LinkClicked);
			comboBoxAllocationDiscount.AlwaysInEditMode = true;
			comboBoxAllocationDiscount.Assigned = false;
			comboBoxAllocationDiscount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAllocationDiscount.CustomReportFieldName = "";
			comboBoxAllocationDiscount.CustomReportKey = "";
			comboBoxAllocationDiscount.CustomReportValueType = 1;
			comboBoxAllocationDiscount.DescriptionTextBox = textBoxAllocationDiscount;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAllocationDiscount.DisplayLayout.Appearance = appearance45;
			comboBoxAllocationDiscount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAllocationDiscount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAllocationDiscount.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAllocationDiscount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxAllocationDiscount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAllocationDiscount.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxAllocationDiscount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAllocationDiscount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAllocationDiscount.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAllocationDiscount.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxAllocationDiscount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAllocationDiscount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAllocationDiscount.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAllocationDiscount.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxAllocationDiscount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAllocationDiscount.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAllocationDiscount.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxAllocationDiscount.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxAllocationDiscount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAllocationDiscount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxAllocationDiscount.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxAllocationDiscount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAllocationDiscount.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
			comboBoxAllocationDiscount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAllocationDiscount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAllocationDiscount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAllocationDiscount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAllocationDiscount.Editable = true;
			comboBoxAllocationDiscount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAllocationDiscount.FilterString = "";
			comboBoxAllocationDiscount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAllocationDiscount.FilterSysDocID = "";
			comboBoxAllocationDiscount.HasAllAccount = false;
			comboBoxAllocationDiscount.HasCustom = false;
			comboBoxAllocationDiscount.IsDataLoaded = false;
			comboBoxAllocationDiscount.Location = new System.Drawing.Point(132, 260);
			comboBoxAllocationDiscount.MaxDropDownItems = 12;
			comboBoxAllocationDiscount.Name = "comboBoxAllocationDiscount";
			comboBoxAllocationDiscount.ShowInactiveItems = false;
			comboBoxAllocationDiscount.ShowQuickAdd = true;
			comboBoxAllocationDiscount.Size = new System.Drawing.Size(117, 21);
			comboBoxAllocationDiscount.TabIndex = 171;
			comboBoxAllocationDiscount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAllocationDiscount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAllocationDiscount.CustomReportFieldName = "";
			textBoxAllocationDiscount.CustomReportKey = "";
			textBoxAllocationDiscount.CustomReportValueType = 1;
			textBoxAllocationDiscount.ForeColor = System.Drawing.Color.Black;
			textBoxAllocationDiscount.IsComboTextBox = false;
			textBoxAllocationDiscount.IsModified = false;
			textBoxAllocationDiscount.Location = new System.Drawing.Point(251, 260);
			textBoxAllocationDiscount.MaxLength = 255;
			textBoxAllocationDiscount.Name = "textBoxAllocationDiscount";
			textBoxAllocationDiscount.ReadOnly = true;
			textBoxAllocationDiscount.Size = new System.Drawing.Size(303, 21);
			textBoxAllocationDiscount.TabIndex = 172;
			textBoxAllocationDiscount.TabStop = false;
			appearance57.FontData.BoldAsString = "False";
			appearance57.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel22.Appearance = appearance57;
			ultraFormattedLinkLabel22.AutoSize = true;
			ultraFormattedLinkLabel22.Location = new System.Drawing.Point(9, 80);
			ultraFormattedLinkLabel22.Name = "ultraFormattedLinkLabel22";
			ultraFormattedLinkLabel22.Size = new System.Drawing.Size(115, 15);
			ultraFormattedLinkLabel22.TabIndex = 170;
			ultraFormattedLinkLabel22.TabStop = true;
			ultraFormattedLinkLabel22.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel22.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel22.Value = "Inventory On Delivery:";
			appearance58.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel22.VisitedLinkAppearance = appearance58;
			ultraFormattedLinkLabel22.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel22_LinkClicked);
			comboBoxInventoryOnDNoteAccount.AlwaysInEditMode = true;
			comboBoxInventoryOnDNoteAccount.Assigned = false;
			comboBoxInventoryOnDNoteAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInventoryOnDNoteAccount.CustomReportFieldName = "";
			comboBoxInventoryOnDNoteAccount.CustomReportKey = "";
			comboBoxInventoryOnDNoteAccount.CustomReportValueType = 1;
			comboBoxInventoryOnDNoteAccount.DescriptionTextBox = mmTextBox1;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Appearance = appearance59;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance60.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance60.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.GroupByBox.Appearance = appearance60;
			appearance61.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance61;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance62.BackColor2 = System.Drawing.SystemColors.Control;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance62.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance62;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.ActiveCellAppearance = appearance63;
			appearance64.BackColor = System.Drawing.SystemColors.Highlight;
			appearance64.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.ActiveRowAppearance = appearance64;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.CardAreaAppearance = appearance65;
			appearance66.BorderColor = System.Drawing.Color.Silver;
			appearance66.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.CellAppearance = appearance66;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.CellPadding = 0;
			appearance67.BackColor = System.Drawing.SystemColors.Control;
			appearance67.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance67.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance67.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance67.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.GroupByRowAppearance = appearance67;
			appearance68.TextHAlignAsString = "Left";
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.HeaderAppearance = appearance68;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.Color.Silver;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.RowAppearance = appearance69;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance70;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInventoryOnDNoteAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInventoryOnDNoteAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInventoryOnDNoteAccount.Editable = true;
			comboBoxInventoryOnDNoteAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxInventoryOnDNoteAccount.FilterString = "";
			comboBoxInventoryOnDNoteAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxInventoryOnDNoteAccount.FilterSysDocID = "";
			comboBoxInventoryOnDNoteAccount.HasAllAccount = false;
			comboBoxInventoryOnDNoteAccount.HasCustom = false;
			comboBoxInventoryOnDNoteAccount.IsDataLoaded = false;
			comboBoxInventoryOnDNoteAccount.Location = new System.Drawing.Point(132, 78);
			comboBoxInventoryOnDNoteAccount.MaxDropDownItems = 12;
			comboBoxInventoryOnDNoteAccount.Name = "comboBoxInventoryOnDNoteAccount";
			comboBoxInventoryOnDNoteAccount.ShowInactiveItems = false;
			comboBoxInventoryOnDNoteAccount.ShowQuickAdd = true;
			comboBoxInventoryOnDNoteAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxInventoryOnDNoteAccount.TabIndex = 7;
			comboBoxInventoryOnDNoteAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.ForeColor = System.Drawing.Color.Black;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(251, 78);
			mmTextBox1.MaxLength = 255;
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.ReadOnly = true;
			mmTextBox1.Size = new System.Drawing.Size(303, 21);
			mmTextBox1.TabIndex = 8;
			mmTextBox1.TabStop = false;
			appearance71.FontData.BoldAsString = "False";
			appearance71.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel15.Appearance = appearance71;
			ultraFormattedLinkLabel15.AutoSize = true;
			ultraFormattedLinkLabel15.Location = new System.Drawing.Point(9, 216);
			ultraFormattedLinkLabel15.Name = "ultraFormattedLinkLabel15";
			ultraFormattedLinkLabel15.Size = new System.Drawing.Size(97, 15);
			ultraFormattedLinkLabel15.TabIndex = 167;
			ultraFormattedLinkLabel15.TabStop = true;
			ultraFormattedLinkLabel15.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel15.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel15.Value = "Employee Account:";
			appearance72.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel15.VisitedLinkAppearance = appearance72;
			ultraFormattedLinkLabel15.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel15_LinkClicked);
			comboBoxEmployeeAccount.AlwaysInEditMode = true;
			comboBoxEmployeeAccount.Assigned = false;
			comboBoxEmployeeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEmployeeAccount.CustomReportFieldName = "";
			comboBoxEmployeeAccount.CustomReportKey = "";
			comboBoxEmployeeAccount.CustomReportValueType = 1;
			comboBoxEmployeeAccount.DescriptionTextBox = textBoxEmpAccountName;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEmployeeAccount.DisplayLayout.Appearance = appearance73;
			comboBoxEmployeeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEmployeeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployeeAccount.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployeeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			comboBoxEmployeeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEmployeeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			comboBoxEmployeeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEmployeeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEmployeeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEmployeeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			comboBoxEmployeeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEmployeeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEmployeeAccount.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEmployeeAccount.DisplayLayout.Override.CellAppearance = appearance80;
			comboBoxEmployeeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEmployeeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEmployeeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			comboBoxEmployeeAccount.DisplayLayout.Override.HeaderAppearance = appearance82;
			comboBoxEmployeeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEmployeeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			comboBoxEmployeeAccount.DisplayLayout.Override.RowAppearance = appearance83;
			comboBoxEmployeeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEmployeeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			comboBoxEmployeeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEmployeeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEmployeeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEmployeeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEmployeeAccount.Editable = true;
			comboBoxEmployeeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxEmployeeAccount.FilterString = "";
			comboBoxEmployeeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxEmployeeAccount.FilterSysDocID = "";
			comboBoxEmployeeAccount.HasAllAccount = false;
			comboBoxEmployeeAccount.HasCustom = false;
			comboBoxEmployeeAccount.IsDataLoaded = false;
			comboBoxEmployeeAccount.Location = new System.Drawing.Point(132, 214);
			comboBoxEmployeeAccount.MaxDropDownItems = 12;
			comboBoxEmployeeAccount.Name = "comboBoxEmployeeAccount";
			comboBoxEmployeeAccount.ShowInactiveItems = false;
			comboBoxEmployeeAccount.ShowQuickAdd = true;
			comboBoxEmployeeAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxEmployeeAccount.TabIndex = 19;
			comboBoxEmployeeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxEmpAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmpAccountName.CustomReportFieldName = "";
			textBoxEmpAccountName.CustomReportKey = "";
			textBoxEmpAccountName.CustomReportValueType = 1;
			textBoxEmpAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxEmpAccountName.IsComboTextBox = false;
			textBoxEmpAccountName.IsModified = false;
			textBoxEmpAccountName.Location = new System.Drawing.Point(251, 214);
			textBoxEmpAccountName.MaxLength = 255;
			textBoxEmpAccountName.Name = "textBoxEmpAccountName";
			textBoxEmpAccountName.ReadOnly = true;
			textBoxEmpAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxEmpAccountName.TabIndex = 20;
			textBoxEmpAccountName.TabStop = false;
			appearance85.FontData.BoldAsString = "False";
			appearance85.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel9.Appearance = appearance85;
			ultraFormattedLinkLabel9.AutoSize = true;
			ultraFormattedLinkLabel9.Location = new System.Drawing.Point(9, 239);
			ultraFormattedLinkLabel9.Name = "ultraFormattedLinkLabel9";
			ultraFormattedLinkLabel9.Size = new System.Drawing.Size(106, 15);
			ultraFormattedLinkLabel9.TabIndex = 164;
			ultraFormattedLinkLabel9.TabStop = true;
			ultraFormattedLinkLabel9.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel9.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel9.Value = "Exchange Gain/Loss:";
			appearance86.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel9.VisitedLinkAppearance = appearance86;
			ultraFormattedLinkLabel9.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel9_LinkClicked);
			appearance87.FontData.BoldAsString = "False";
			appearance87.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance87;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(9, 13);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(84, 15);
			ultraFormattedLinkLabel1.TabIndex = 0;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Inventory Asset:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxDiscountReceived.AlwaysInEditMode = true;
			comboBoxDiscountReceived.Assigned = false;
			comboBoxDiscountReceived.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountReceived.CustomReportFieldName = "";
			comboBoxDiscountReceived.CustomReportKey = "";
			comboBoxDiscountReceived.CustomReportValueType = 1;
			comboBoxDiscountReceived.DescriptionTextBox = textBoxDiscountReceivedAccountName;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountReceived.DisplayLayout.Appearance = appearance89;
			comboBoxDiscountReceived.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountReceived.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountReceived.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			comboBoxDiscountReceived.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountReceived.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountReceived.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountReceived.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			comboBoxDiscountReceived.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountReceived.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountReceived.DisplayLayout.Override.CellAppearance = appearance96;
			comboBoxDiscountReceived.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountReceived.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountReceived.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderAppearance = appearance98;
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountReceived.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountReceived.DisplayLayout.Override.RowAppearance = appearance99;
			comboBoxDiscountReceived.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountReceived.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
			comboBoxDiscountReceived.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDiscountReceived.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDiscountReceived.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDiscountReceived.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDiscountReceived.Editable = true;
			comboBoxDiscountReceived.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDiscountReceived.FilterString = "";
			comboBoxDiscountReceived.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDiscountReceived.FilterSysDocID = "";
			comboBoxDiscountReceived.HasAllAccount = false;
			comboBoxDiscountReceived.HasCustom = false;
			comboBoxDiscountReceived.IsDataLoaded = false;
			comboBoxDiscountReceived.Location = new System.Drawing.Point(132, 145);
			comboBoxDiscountReceived.MaxDropDownItems = 12;
			comboBoxDiscountReceived.Name = "comboBoxDiscountReceived";
			comboBoxDiscountReceived.ShowInactiveItems = false;
			comboBoxDiscountReceived.ShowQuickAdd = true;
			comboBoxDiscountReceived.Size = new System.Drawing.Size(117, 21);
			comboBoxDiscountReceived.TabIndex = 13;
			comboBoxDiscountReceived.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDiscountReceivedAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountReceivedAccountName.CustomReportFieldName = "";
			textBoxDiscountReceivedAccountName.CustomReportKey = "";
			textBoxDiscountReceivedAccountName.CustomReportValueType = 1;
			textBoxDiscountReceivedAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountReceivedAccountName.IsComboTextBox = false;
			textBoxDiscountReceivedAccountName.IsModified = false;
			textBoxDiscountReceivedAccountName.Location = new System.Drawing.Point(251, 145);
			textBoxDiscountReceivedAccountName.MaxLength = 255;
			textBoxDiscountReceivedAccountName.Name = "textBoxDiscountReceivedAccountName";
			textBoxDiscountReceivedAccountName.ReadOnly = true;
			textBoxDiscountReceivedAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxDiscountReceivedAccountName.TabIndex = 14;
			textBoxDiscountReceivedAccountName.TabStop = false;
			comboBoxGainLossAccount.AlwaysInEditMode = true;
			comboBoxGainLossAccount.Assigned = false;
			comboBoxGainLossAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGainLossAccount.CustomReportFieldName = "";
			comboBoxGainLossAccount.CustomReportKey = "";
			comboBoxGainLossAccount.CustomReportValueType = 1;
			comboBoxGainLossAccount.DescriptionTextBox = textBoxGainLoss;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGainLossAccount.DisplayLayout.Appearance = appearance101;
			comboBoxGainLossAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGainLossAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.Appearance = appearance102;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance104.BackColor2 = System.Drawing.SystemColors.Control;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGainLossAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
			comboBoxGainLossAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGainLossAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGainLossAccount.DisplayLayout.Override.ActiveCellAppearance = appearance105;
			appearance106.BackColor = System.Drawing.SystemColors.Highlight;
			appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGainLossAccount.DisplayLayout.Override.ActiveRowAppearance = appearance106;
			comboBoxGainLossAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGainLossAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.Override.CardAreaAppearance = appearance107;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGainLossAccount.DisplayLayout.Override.CellAppearance = appearance108;
			comboBoxGainLossAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGainLossAccount.DisplayLayout.Override.CellPadding = 0;
			appearance109.BackColor = System.Drawing.SystemColors.Control;
			appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGainLossAccount.DisplayLayout.Override.GroupByRowAppearance = appearance109;
			appearance110.TextHAlignAsString = "Left";
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderAppearance = appearance110;
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGainLossAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			comboBoxGainLossAccount.DisplayLayout.Override.RowAppearance = appearance111;
			comboBoxGainLossAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGainLossAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
			comboBoxGainLossAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGainLossAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGainLossAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGainLossAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGainLossAccount.Editable = true;
			comboBoxGainLossAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGainLossAccount.FilterString = "";
			comboBoxGainLossAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGainLossAccount.FilterSysDocID = "";
			comboBoxGainLossAccount.HasAllAccount = false;
			comboBoxGainLossAccount.HasCustom = false;
			comboBoxGainLossAccount.IsDataLoaded = false;
			comboBoxGainLossAccount.Location = new System.Drawing.Point(132, 237);
			comboBoxGainLossAccount.MaxDropDownItems = 12;
			comboBoxGainLossAccount.Name = "comboBoxGainLossAccount";
			comboBoxGainLossAccount.ShowInactiveItems = false;
			comboBoxGainLossAccount.ShowQuickAdd = true;
			comboBoxGainLossAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxGainLossAccount.TabIndex = 21;
			comboBoxGainLossAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGainLoss.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGainLoss.CustomReportFieldName = "";
			textBoxGainLoss.CustomReportKey = "";
			textBoxGainLoss.CustomReportValueType = 1;
			textBoxGainLoss.ForeColor = System.Drawing.Color.Black;
			textBoxGainLoss.IsComboTextBox = false;
			textBoxGainLoss.IsModified = false;
			textBoxGainLoss.Location = new System.Drawing.Point(251, 237);
			textBoxGainLoss.MaxLength = 255;
			textBoxGainLoss.Name = "textBoxGainLoss";
			textBoxGainLoss.ReadOnly = true;
			textBoxGainLoss.Size = new System.Drawing.Size(303, 21);
			textBoxGainLoss.TabIndex = 22;
			textBoxGainLoss.TabStop = false;
			comboBoxDiscountGiven.AlwaysInEditMode = true;
			comboBoxDiscountGiven.Assigned = false;
			comboBoxDiscountGiven.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDiscountGiven.CustomReportFieldName = "";
			comboBoxDiscountGiven.CustomReportKey = "";
			comboBoxDiscountGiven.CustomReportValueType = 1;
			comboBoxDiscountGiven.DescriptionTextBox = textBoxDiscountGivenAccountName;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDiscountGiven.DisplayLayout.Appearance = appearance113;
			comboBoxDiscountGiven.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDiscountGiven.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.Appearance = appearance114;
			appearance115.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BandLabelAppearance = appearance115;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance116.BackColor2 = System.Drawing.SystemColors.Control;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance116.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDiscountGiven.DisplayLayout.GroupByBox.PromptAppearance = appearance116;
			comboBoxDiscountGiven.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDiscountGiven.DisplayLayout.MaxRowScrollRegions = 1;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveCellAppearance = appearance117;
			appearance118.BackColor = System.Drawing.SystemColors.Highlight;
			appearance118.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDiscountGiven.DisplayLayout.Override.ActiveRowAppearance = appearance118;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDiscountGiven.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.CardAreaAppearance = appearance119;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			appearance120.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDiscountGiven.DisplayLayout.Override.CellAppearance = appearance120;
			comboBoxDiscountGiven.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDiscountGiven.DisplayLayout.Override.CellPadding = 0;
			appearance121.BackColor = System.Drawing.SystemColors.Control;
			appearance121.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance121.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDiscountGiven.DisplayLayout.Override.GroupByRowAppearance = appearance121;
			appearance122.TextHAlignAsString = "Left";
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderAppearance = appearance122;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDiscountGiven.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.Color.Silver;
			comboBoxDiscountGiven.DisplayLayout.Override.RowAppearance = appearance123;
			comboBoxDiscountGiven.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDiscountGiven.DisplayLayout.Override.TemplateAddRowAppearance = appearance124;
			comboBoxDiscountGiven.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDiscountGiven.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDiscountGiven.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDiscountGiven.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDiscountGiven.Editable = true;
			comboBoxDiscountGiven.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxDiscountGiven.FilterString = "";
			comboBoxDiscountGiven.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxDiscountGiven.FilterSysDocID = "";
			comboBoxDiscountGiven.HasAllAccount = false;
			comboBoxDiscountGiven.HasCustom = false;
			comboBoxDiscountGiven.IsDataLoaded = false;
			comboBoxDiscountGiven.Location = new System.Drawing.Point(132, 123);
			comboBoxDiscountGiven.MaxDropDownItems = 12;
			comboBoxDiscountGiven.Name = "comboBoxDiscountGiven";
			comboBoxDiscountGiven.ShowInactiveItems = false;
			comboBoxDiscountGiven.ShowQuickAdd = true;
			comboBoxDiscountGiven.Size = new System.Drawing.Size(117, 21);
			comboBoxDiscountGiven.TabIndex = 11;
			comboBoxDiscountGiven.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxDiscountGivenAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDiscountGivenAccountName.CustomReportFieldName = "";
			textBoxDiscountGivenAccountName.CustomReportKey = "";
			textBoxDiscountGivenAccountName.CustomReportValueType = 1;
			textBoxDiscountGivenAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxDiscountGivenAccountName.IsComboTextBox = false;
			textBoxDiscountGivenAccountName.IsModified = false;
			textBoxDiscountGivenAccountName.Location = new System.Drawing.Point(251, 123);
			textBoxDiscountGivenAccountName.MaxLength = 255;
			textBoxDiscountGivenAccountName.Name = "textBoxDiscountGivenAccountName";
			textBoxDiscountGivenAccountName.ReadOnly = true;
			textBoxDiscountGivenAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxDiscountGivenAccountName.TabIndex = 12;
			textBoxDiscountGivenAccountName.TabStop = false;
			appearance125.FontData.BoldAsString = "False";
			appearance125.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance125;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(9, 35);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(34, 15);
			ultraFormattedLinkLabel2.TabIndex = 157;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Sales:";
			appearance126.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance126;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			comboBoxSalesTax.AlwaysInEditMode = true;
			comboBoxSalesTax.Assigned = false;
			comboBoxSalesTax.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesTax.CustomReportFieldName = "";
			comboBoxSalesTax.CustomReportKey = "";
			comboBoxSalesTax.CustomReportValueType = 1;
			comboBoxSalesTax.DescriptionTextBox = textBoxSalesTaxAccountName;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesTax.DisplayLayout.Appearance = appearance127;
			comboBoxSalesTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance128.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance128.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance128.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.GroupByBox.Appearance = appearance128;
			appearance129.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance129;
			comboBoxSalesTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance130.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance130.BackColor2 = System.Drawing.SystemColors.Control;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance130.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesTax.DisplayLayout.GroupByBox.PromptAppearance = appearance130;
			comboBoxSalesTax.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveCellAppearance = appearance131;
			appearance132.BackColor = System.Drawing.SystemColors.Highlight;
			appearance132.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesTax.DisplayLayout.Override.ActiveRowAppearance = appearance132;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.CardAreaAppearance = appearance133;
			appearance134.BorderColor = System.Drawing.Color.Silver;
			appearance134.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesTax.DisplayLayout.Override.CellAppearance = appearance134;
			comboBoxSalesTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesTax.DisplayLayout.Override.CellPadding = 0;
			appearance135.BackColor = System.Drawing.SystemColors.Control;
			appearance135.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance135.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance135.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance135.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesTax.DisplayLayout.Override.GroupByRowAppearance = appearance135;
			appearance136.TextHAlignAsString = "Left";
			comboBoxSalesTax.DisplayLayout.Override.HeaderAppearance = appearance136;
			comboBoxSalesTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesTax.DisplayLayout.Override.RowAppearance = appearance137;
			comboBoxSalesTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance138.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance138;
			comboBoxSalesTax.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesTax.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesTax.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesTax.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesTax.Editable = true;
			comboBoxSalesTax.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesTax.FilterString = "";
			comboBoxSalesTax.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesTax.FilterSysDocID = "";
			comboBoxSalesTax.HasAllAccount = false;
			comboBoxSalesTax.HasCustom = false;
			comboBoxSalesTax.IsDataLoaded = false;
			comboBoxSalesTax.Location = new System.Drawing.Point(132, 101);
			comboBoxSalesTax.MaxDropDownItems = 12;
			comboBoxSalesTax.Name = "comboBoxSalesTax";
			comboBoxSalesTax.ShowInactiveItems = false;
			comboBoxSalesTax.ShowQuickAdd = true;
			comboBoxSalesTax.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesTax.TabIndex = 9;
			comboBoxSalesTax.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSalesTaxAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesTaxAccountName.CustomReportFieldName = "";
			textBoxSalesTaxAccountName.CustomReportKey = "";
			textBoxSalesTaxAccountName.CustomReportValueType = 1;
			textBoxSalesTaxAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesTaxAccountName.IsComboTextBox = false;
			textBoxSalesTaxAccountName.IsModified = false;
			textBoxSalesTaxAccountName.Location = new System.Drawing.Point(251, 101);
			textBoxSalesTaxAccountName.MaxLength = 255;
			textBoxSalesTaxAccountName.Name = "textBoxSalesTaxAccountName";
			textBoxSalesTaxAccountName.ReadOnly = true;
			textBoxSalesTaxAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesTaxAccountName.TabIndex = 10;
			textBoxSalesTaxAccountName.TabStop = false;
			appearance139.FontData.BoldAsString = "False";
			appearance139.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance139;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(9, 57);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel3.TabIndex = 161;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "COGS:";
			appearance140.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance140;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance141.FontData.BoldAsString = "False";
			appearance141.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance141;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(9, 193);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(63, 15);
			ultraFormattedLinkLabel8.TabIndex = 163;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "A/P Account";
			appearance142.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance142;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			comboBoxCOGSAccount.AlwaysInEditMode = true;
			comboBoxCOGSAccount.Assigned = false;
			comboBoxCOGSAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCOGSAccount.CustomReportFieldName = "";
			comboBoxCOGSAccount.CustomReportKey = "";
			comboBoxCOGSAccount.CustomReportValueType = 1;
			comboBoxCOGSAccount.DescriptionTextBox = textBoxGOGSAccountName;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCOGSAccount.DisplayLayout.Appearance = appearance143;
			comboBoxCOGSAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCOGSAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance144.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance144.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.Appearance = appearance144;
			appearance145.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance145;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance146.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance146.BackColor2 = System.Drawing.SystemColors.Control;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance146.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCOGSAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance146;
			comboBoxCOGSAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCOGSAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			appearance147.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveCellAppearance = appearance147;
			appearance148.BackColor = System.Drawing.SystemColors.Highlight;
			appearance148.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCOGSAccount.DisplayLayout.Override.ActiveRowAppearance = appearance148;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCOGSAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.CardAreaAppearance = appearance149;
			appearance150.BorderColor = System.Drawing.Color.Silver;
			appearance150.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCOGSAccount.DisplayLayout.Override.CellAppearance = appearance150;
			comboBoxCOGSAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCOGSAccount.DisplayLayout.Override.CellPadding = 0;
			appearance151.BackColor = System.Drawing.SystemColors.Control;
			appearance151.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance151.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance151.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance151.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCOGSAccount.DisplayLayout.Override.GroupByRowAppearance = appearance151;
			appearance152.TextHAlignAsString = "Left";
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderAppearance = appearance152;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCOGSAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.Color.Silver;
			comboBoxCOGSAccount.DisplayLayout.Override.RowAppearance = appearance153;
			comboBoxCOGSAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCOGSAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance154;
			comboBoxCOGSAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCOGSAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCOGSAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCOGSAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCOGSAccount.Editable = true;
			comboBoxCOGSAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxCOGSAccount.FilterString = "";
			comboBoxCOGSAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxCOGSAccount.FilterSysDocID = "";
			comboBoxCOGSAccount.HasAllAccount = false;
			comboBoxCOGSAccount.HasCustom = false;
			comboBoxCOGSAccount.IsDataLoaded = false;
			comboBoxCOGSAccount.Location = new System.Drawing.Point(132, 55);
			comboBoxCOGSAccount.MaxDropDownItems = 12;
			comboBoxCOGSAccount.Name = "comboBoxCOGSAccount";
			comboBoxCOGSAccount.ShowInactiveItems = false;
			comboBoxCOGSAccount.ShowQuickAdd = true;
			comboBoxCOGSAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxCOGSAccount.TabIndex = 5;
			comboBoxCOGSAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxGOGSAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxGOGSAccountName.CustomReportFieldName = "";
			textBoxGOGSAccountName.CustomReportKey = "";
			textBoxGOGSAccountName.CustomReportValueType = 1;
			textBoxGOGSAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxGOGSAccountName.IsComboTextBox = false;
			textBoxGOGSAccountName.IsModified = false;
			textBoxGOGSAccountName.Location = new System.Drawing.Point(251, 55);
			textBoxGOGSAccountName.MaxLength = 255;
			textBoxGOGSAccountName.Name = "textBoxGOGSAccountName";
			textBoxGOGSAccountName.ReadOnly = true;
			textBoxGOGSAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxGOGSAccountName.TabIndex = 6;
			textBoxGOGSAccountName.TabStop = false;
			textBoxInventoryAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInventoryAccountName.CustomReportFieldName = "";
			textBoxInventoryAccountName.CustomReportKey = "";
			textBoxInventoryAccountName.CustomReportValueType = 1;
			textBoxInventoryAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxInventoryAccountName.IsComboTextBox = false;
			textBoxInventoryAccountName.IsModified = false;
			textBoxInventoryAccountName.Location = new System.Drawing.Point(251, 11);
			textBoxInventoryAccountName.MaxLength = 255;
			textBoxInventoryAccountName.Name = "textBoxInventoryAccountName";
			textBoxInventoryAccountName.ReadOnly = true;
			textBoxInventoryAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxInventoryAccountName.TabIndex = 2;
			textBoxInventoryAccountName.TabStop = false;
			appearance155.FontData.BoldAsString = "False";
			appearance155.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance155;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(9, 103);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel4.TabIndex = 158;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Sales Tax:";
			appearance156.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance156;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxAPAccount.AlwaysInEditMode = true;
			comboBoxAPAccount.Assigned = false;
			comboBoxAPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAPAccount.CustomReportFieldName = "";
			comboBoxAPAccount.CustomReportKey = "";
			comboBoxAPAccount.CustomReportValueType = 1;
			comboBoxAPAccount.DescriptionTextBox = textBoxAP;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxAPAccount.DisplayLayout.Appearance = appearance157;
			comboBoxAPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxAPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxAPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxAPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxAPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxAPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxAPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxAPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxAPAccount.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxAPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxAPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxAPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxAPAccount.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxAPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxAPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxAPAccount.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxAPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxAPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxAPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxAPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxAPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxAPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAPAccount.Editable = true;
			comboBoxAPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAPAccount.FilterString = "";
			comboBoxAPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAPAccount.FilterSysDocID = "";
			comboBoxAPAccount.HasAllAccount = false;
			comboBoxAPAccount.HasCustom = false;
			comboBoxAPAccount.IsDataLoaded = false;
			comboBoxAPAccount.Location = new System.Drawing.Point(132, 191);
			comboBoxAPAccount.MaxDropDownItems = 12;
			comboBoxAPAccount.Name = "comboBoxAPAccount";
			comboBoxAPAccount.ShowInactiveItems = false;
			comboBoxAPAccount.ShowQuickAdd = true;
			comboBoxAPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxAPAccount.TabIndex = 17;
			comboBoxAPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAP.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAP.CustomReportFieldName = "";
			textBoxAP.CustomReportKey = "";
			textBoxAP.CustomReportValueType = 1;
			textBoxAP.ForeColor = System.Drawing.Color.Black;
			textBoxAP.IsComboTextBox = false;
			textBoxAP.IsModified = false;
			textBoxAP.Location = new System.Drawing.Point(251, 191);
			textBoxAP.MaxLength = 255;
			textBoxAP.Name = "textBoxAP";
			textBoxAP.ReadOnly = true;
			textBoxAP.Size = new System.Drawing.Size(303, 21);
			textBoxAP.TabIndex = 18;
			textBoxAP.TabStop = false;
			comboBoxSalesAccount.AlwaysInEditMode = true;
			comboBoxSalesAccount.Assigned = false;
			comboBoxSalesAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesAccount.CustomReportFieldName = "";
			comboBoxSalesAccount.CustomReportKey = "";
			comboBoxSalesAccount.CustomReportValueType = 1;
			comboBoxSalesAccount.DescriptionTextBox = textBoxSalesAccountName;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesAccount.DisplayLayout.Appearance = appearance169;
			comboBoxSalesAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.Appearance = appearance170;
			appearance171.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance171;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance172.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance172.BackColor2 = System.Drawing.SystemColors.Control;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance172;
			comboBoxSalesAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			appearance173.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveCellAppearance = appearance173;
			appearance174.BackColor = System.Drawing.SystemColors.Highlight;
			appearance174.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesAccount.DisplayLayout.Override.ActiveRowAppearance = appearance174;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.CardAreaAppearance = appearance175;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			appearance176.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesAccount.DisplayLayout.Override.CellAppearance = appearance176;
			comboBoxSalesAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesAccount.DisplayLayout.Override.CellPadding = 0;
			appearance177.BackColor = System.Drawing.SystemColors.Control;
			appearance177.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance177.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesAccount.DisplayLayout.Override.GroupByRowAppearance = appearance177;
			appearance178.TextHAlignAsString = "Left";
			comboBoxSalesAccount.DisplayLayout.Override.HeaderAppearance = appearance178;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesAccount.DisplayLayout.Override.RowAppearance = appearance179;
			comboBoxSalesAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance180;
			comboBoxSalesAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesAccount.Editable = true;
			comboBoxSalesAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxSalesAccount.FilterString = "";
			comboBoxSalesAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxSalesAccount.FilterSysDocID = "";
			comboBoxSalesAccount.HasAllAccount = false;
			comboBoxSalesAccount.HasCustom = false;
			comboBoxSalesAccount.IsDataLoaded = false;
			comboBoxSalesAccount.Location = new System.Drawing.Point(132, 33);
			comboBoxSalesAccount.MaxDropDownItems = 12;
			comboBoxSalesAccount.Name = "comboBoxSalesAccount";
			comboBoxSalesAccount.ShowInactiveItems = false;
			comboBoxSalesAccount.ShowQuickAdd = true;
			comboBoxSalesAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxSalesAccount.TabIndex = 3;
			comboBoxSalesAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxSalesAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSalesAccountName.CustomReportFieldName = "";
			textBoxSalesAccountName.CustomReportKey = "";
			textBoxSalesAccountName.CustomReportValueType = 1;
			textBoxSalesAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxSalesAccountName.IsComboTextBox = false;
			textBoxSalesAccountName.IsModified = false;
			textBoxSalesAccountName.Location = new System.Drawing.Point(251, 33);
			textBoxSalesAccountName.MaxLength = 255;
			textBoxSalesAccountName.Name = "textBoxSalesAccountName";
			textBoxSalesAccountName.ReadOnly = true;
			textBoxSalesAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxSalesAccountName.TabIndex = 4;
			textBoxSalesAccountName.TabStop = false;
			appearance181.FontData.BoldAsString = "False";
			appearance181.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance181;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(9, 125);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(81, 15);
			ultraFormattedLinkLabel5.TabIndex = 159;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Discount Given:";
			appearance182.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance182;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxInventoryAccount.AlwaysInEditMode = true;
			comboBoxInventoryAccount.Assigned = false;
			comboBoxInventoryAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxInventoryAccount.CustomReportFieldName = "";
			comboBoxInventoryAccount.CustomReportKey = "";
			comboBoxInventoryAccount.CustomReportValueType = 1;
			comboBoxInventoryAccount.DescriptionTextBox = textBoxInventoryAccountName;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			appearance183.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxInventoryAccount.DisplayLayout.Appearance = appearance183;
			comboBoxInventoryAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxInventoryAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance184.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance184.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance184.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance184.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.Appearance = appearance184;
			appearance185.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance185;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance186.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance186.BackColor2 = System.Drawing.SystemColors.Control;
			appearance186.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance186.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxInventoryAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance186;
			comboBoxInventoryAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxInventoryAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			appearance187.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveCellAppearance = appearance187;
			appearance188.BackColor = System.Drawing.SystemColors.Highlight;
			appearance188.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxInventoryAccount.DisplayLayout.Override.ActiveRowAppearance = appearance188;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxInventoryAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.CardAreaAppearance = appearance189;
			appearance190.BorderColor = System.Drawing.Color.Silver;
			appearance190.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxInventoryAccount.DisplayLayout.Override.CellAppearance = appearance190;
			comboBoxInventoryAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxInventoryAccount.DisplayLayout.Override.CellPadding = 0;
			appearance191.BackColor = System.Drawing.SystemColors.Control;
			appearance191.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance191.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance191.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance191.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxInventoryAccount.DisplayLayout.Override.GroupByRowAppearance = appearance191;
			appearance192.TextHAlignAsString = "Left";
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderAppearance = appearance192;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxInventoryAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance193.BackColor = System.Drawing.SystemColors.Window;
			appearance193.BorderColor = System.Drawing.Color.Silver;
			comboBoxInventoryAccount.DisplayLayout.Override.RowAppearance = appearance193;
			comboBoxInventoryAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance194.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxInventoryAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance194;
			comboBoxInventoryAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxInventoryAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxInventoryAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxInventoryAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxInventoryAccount.Editable = true;
			comboBoxInventoryAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxInventoryAccount.FilterString = "";
			comboBoxInventoryAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxInventoryAccount.FilterSysDocID = "";
			comboBoxInventoryAccount.HasAllAccount = false;
			comboBoxInventoryAccount.HasCustom = false;
			comboBoxInventoryAccount.IsDataLoaded = false;
			comboBoxInventoryAccount.Location = new System.Drawing.Point(132, 11);
			comboBoxInventoryAccount.MaxDropDownItems = 12;
			comboBoxInventoryAccount.Name = "comboBoxInventoryAccount";
			comboBoxInventoryAccount.ShowInactiveItems = false;
			comboBoxInventoryAccount.ShowQuickAdd = true;
			comboBoxInventoryAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxInventoryAccount.TabIndex = 1;
			comboBoxInventoryAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance195.FontData.BoldAsString = "False";
			appearance195.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance195;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(9, 147);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(97, 15);
			ultraFormattedLinkLabel6.TabIndex = 160;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Discount Received:";
			appearance196.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance196;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance197.FontData.BoldAsString = "False";
			appearance197.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance197;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(9, 170);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(68, 15);
			ultraFormattedLinkLabel7.TabIndex = 162;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "A/R Account:";
			appearance198.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance198;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			textBoxARName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxARName.CustomReportFieldName = "";
			textBoxARName.CustomReportKey = "";
			textBoxARName.CustomReportValueType = 1;
			textBoxARName.ForeColor = System.Drawing.Color.Black;
			textBoxARName.IsComboTextBox = false;
			textBoxARName.IsModified = false;
			textBoxARName.Location = new System.Drawing.Point(251, 168);
			textBoxARName.MaxLength = 255;
			textBoxARName.Name = "textBoxARName";
			textBoxARName.ReadOnly = true;
			textBoxARName.Size = new System.Drawing.Size(303, 21);
			textBoxARName.TabIndex = 16;
			textBoxARName.TabStop = false;
			comboBoxARAccount.AlwaysInEditMode = true;
			comboBoxARAccount.Assigned = false;
			comboBoxARAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxARAccount.CustomReportFieldName = "";
			comboBoxARAccount.CustomReportKey = "";
			comboBoxARAccount.CustomReportValueType = 1;
			comboBoxARAccount.DescriptionTextBox = textBoxARName;
			appearance199.BackColor = System.Drawing.SystemColors.Window;
			appearance199.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxARAccount.DisplayLayout.Appearance = appearance199;
			comboBoxARAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxARAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance200.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance200.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance200.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance200.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.GroupByBox.Appearance = appearance200;
			appearance201.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance201;
			comboBoxARAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance202.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance202.BackColor2 = System.Drawing.SystemColors.Control;
			appearance202.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance202.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxARAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance202;
			comboBoxARAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxARAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance203.BackColor = System.Drawing.SystemColors.Window;
			appearance203.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxARAccount.DisplayLayout.Override.ActiveCellAppearance = appearance203;
			appearance204.BackColor = System.Drawing.SystemColors.Highlight;
			appearance204.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxARAccount.DisplayLayout.Override.ActiveRowAppearance = appearance204;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxARAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance205.BackColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.CardAreaAppearance = appearance205;
			appearance206.BorderColor = System.Drawing.Color.Silver;
			appearance206.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxARAccount.DisplayLayout.Override.CellAppearance = appearance206;
			comboBoxARAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxARAccount.DisplayLayout.Override.CellPadding = 0;
			appearance207.BackColor = System.Drawing.SystemColors.Control;
			appearance207.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance207.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance207.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance207.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxARAccount.DisplayLayout.Override.GroupByRowAppearance = appearance207;
			appearance208.TextHAlignAsString = "Left";
			comboBoxARAccount.DisplayLayout.Override.HeaderAppearance = appearance208;
			comboBoxARAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxARAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance209.BackColor = System.Drawing.SystemColors.Window;
			appearance209.BorderColor = System.Drawing.Color.Silver;
			comboBoxARAccount.DisplayLayout.Override.RowAppearance = appearance209;
			comboBoxARAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance210.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxARAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance210;
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
			comboBoxARAccount.Location = new System.Drawing.Point(132, 168);
			comboBoxARAccount.MaxDropDownItems = 12;
			comboBoxARAccount.Name = "comboBoxARAccount";
			comboBoxARAccount.ShowInactiveItems = false;
			comboBoxARAccount.ShowQuickAdd = true;
			comboBoxARAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxARAccount.TabIndex = 15;
			comboBoxARAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			tabPageProject.Controls.Add(textBoxAdvanceAccountName);
			tabPageProject.Controls.Add(comboBoxAdvanceAccount);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel14);
			tabPageProject.Controls.Add(textBoxRetentionAccountName);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel18);
			tabPageProject.Controls.Add(comboBoxRetentionAccount);
			tabPageProject.Controls.Add(textBoxProjectWIPAccount);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel13);
			tabPageProject.Controls.Add(textBoxProjectIncomeAccount);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel11);
			tabPageProject.Controls.Add(textBoxProjectCostAccount);
			tabPageProject.Controls.Add(comboBoxProjectTimesheetContra);
			tabPageProject.Controls.Add(comboBoxProjectWIPAccount);
			tabPageProject.Controls.Add(textBoxProjectTimesheetContra);
			tabPageProject.Controls.Add(comboBoxProjectIncomeAccount);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel16);
			tabPageProject.Controls.Add(comboBoxProjectCostAccount);
			tabPageProject.Controls.Add(ultraFormattedLinkLabel17);
			tabPageProject.Location = new System.Drawing.Point(-10000, -10000);
			tabPageProject.Name = "tabPageProject";
			tabPageProject.Size = new System.Drawing.Size(643, 365);
			textBoxAdvanceAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAdvanceAccountName.CustomReportFieldName = "";
			textBoxAdvanceAccountName.CustomReportKey = "";
			textBoxAdvanceAccountName.CustomReportValueType = 1;
			textBoxAdvanceAccountName.IsComboTextBox = false;
			textBoxAdvanceAccountName.IsModified = false;
			textBoxAdvanceAccountName.Location = new System.Drawing.Point(240, 125);
			textBoxAdvanceAccountName.MaxLength = 64;
			textBoxAdvanceAccountName.Name = "textBoxAdvanceAccountName";
			textBoxAdvanceAccountName.ReadOnly = true;
			textBoxAdvanceAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxAdvanceAccountName.TabIndex = 136;
			textBoxAdvanceAccountName.TabStop = false;
			comboBoxAdvanceAccount.Assigned = false;
			comboBoxAdvanceAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxAdvanceAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxAdvanceAccount.CustomReportFieldName = "";
			comboBoxAdvanceAccount.CustomReportKey = "";
			comboBoxAdvanceAccount.CustomReportValueType = 1;
			comboBoxAdvanceAccount.DescriptionTextBox = textBoxAdvanceAccountName;
			comboBoxAdvanceAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxAdvanceAccount.Editable = true;
			comboBoxAdvanceAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxAdvanceAccount.FilterString = "";
			comboBoxAdvanceAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxAdvanceAccount.FilterSysDocID = "";
			comboBoxAdvanceAccount.HasAllAccount = false;
			comboBoxAdvanceAccount.HasCustom = false;
			comboBoxAdvanceAccount.IsDataLoaded = false;
			comboBoxAdvanceAccount.Location = new System.Drawing.Point(121, 125);
			comboBoxAdvanceAccount.MaxDropDownItems = 12;
			comboBoxAdvanceAccount.Name = "comboBoxAdvanceAccount";
			comboBoxAdvanceAccount.ShowInactiveItems = false;
			comboBoxAdvanceAccount.ShowQuickAdd = true;
			comboBoxAdvanceAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxAdvanceAccount.TabIndex = 135;
			comboBoxAdvanceAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraFormattedLinkLabel14.AutoSize = true;
			ultraFormattedLinkLabel14.Location = new System.Drawing.Point(7, 127);
			ultraFormattedLinkLabel14.Name = "ultraFormattedLinkLabel14";
			ultraFormattedLinkLabel14.Size = new System.Drawing.Size(50, 15);
			ultraFormattedLinkLabel14.TabIndex = 137;
			ultraFormattedLinkLabel14.TabStop = true;
			ultraFormattedLinkLabel14.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel14.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel14.Value = "Advance:";
			appearance211.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel14.VisitedLinkAppearance = appearance211;
			ultraFormattedLinkLabel14.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel14_LinkClicked);
			textBoxRetentionAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRetentionAccountName.CustomReportFieldName = "";
			textBoxRetentionAccountName.CustomReportKey = "";
			textBoxRetentionAccountName.CustomReportValueType = 1;
			textBoxRetentionAccountName.IsComboTextBox = false;
			textBoxRetentionAccountName.IsModified = false;
			textBoxRetentionAccountName.Location = new System.Drawing.Point(240, 101);
			textBoxRetentionAccountName.MaxLength = 64;
			textBoxRetentionAccountName.Name = "textBoxRetentionAccountName";
			textBoxRetentionAccountName.ReadOnly = true;
			textBoxRetentionAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxRetentionAccountName.TabIndex = 9;
			textBoxRetentionAccountName.TabStop = false;
			appearance212.FontData.BoldAsString = "False";
			appearance212.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel18.Appearance = appearance212;
			ultraFormattedLinkLabel18.AutoSize = true;
			ultraFormattedLinkLabel18.Location = new System.Drawing.Point(7, 13);
			ultraFormattedLinkLabel18.Name = "ultraFormattedLinkLabel18";
			ultraFormattedLinkLabel18.Size = new System.Drawing.Size(92, 15);
			ultraFormattedLinkLabel18.TabIndex = 128;
			ultraFormattedLinkLabel18.TabStop = true;
			ultraFormattedLinkLabel18.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel18.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel18.Value = "Work In Progress:";
			appearance213.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel18.VisitedLinkAppearance = appearance213;
			ultraFormattedLinkLabel18.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel18_LinkClicked);
			comboBoxRetentionAccount.Assigned = false;
			comboBoxRetentionAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxRetentionAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxRetentionAccount.CustomReportFieldName = "";
			comboBoxRetentionAccount.CustomReportKey = "";
			comboBoxRetentionAccount.CustomReportValueType = 1;
			comboBoxRetentionAccount.DescriptionTextBox = textBoxRetentionAccountName;
			comboBoxRetentionAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxRetentionAccount.Editable = true;
			comboBoxRetentionAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxRetentionAccount.FilterString = "";
			comboBoxRetentionAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxRetentionAccount.FilterSysDocID = "";
			comboBoxRetentionAccount.HasAllAccount = false;
			comboBoxRetentionAccount.HasCustom = false;
			comboBoxRetentionAccount.IsDataLoaded = false;
			comboBoxRetentionAccount.Location = new System.Drawing.Point(121, 101);
			comboBoxRetentionAccount.MaxDropDownItems = 12;
			comboBoxRetentionAccount.Name = "comboBoxRetentionAccount";
			comboBoxRetentionAccount.ShowInactiveItems = false;
			comboBoxRetentionAccount.ShowQuickAdd = true;
			comboBoxRetentionAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxRetentionAccount.TabIndex = 8;
			comboBoxRetentionAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProjectWIPAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectWIPAccount.CustomReportFieldName = "";
			textBoxProjectWIPAccount.CustomReportKey = "";
			textBoxProjectWIPAccount.CustomReportValueType = 1;
			textBoxProjectWIPAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectWIPAccount.IsComboTextBox = false;
			textBoxProjectWIPAccount.IsModified = false;
			textBoxProjectWIPAccount.Location = new System.Drawing.Point(240, 11);
			textBoxProjectWIPAccount.MaxLength = 255;
			textBoxProjectWIPAccount.Name = "textBoxProjectWIPAccount";
			textBoxProjectWIPAccount.ReadOnly = true;
			textBoxProjectWIPAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectWIPAccount.TabIndex = 1;
			textBoxProjectWIPAccount.TabStop = false;
			ultraFormattedLinkLabel13.AutoSize = true;
			ultraFormattedLinkLabel13.Location = new System.Drawing.Point(7, 104);
			ultraFormattedLinkLabel13.Name = "ultraFormattedLinkLabel13";
			ultraFormattedLinkLabel13.Size = new System.Drawing.Size(55, 15);
			ultraFormattedLinkLabel13.TabIndex = 134;
			ultraFormattedLinkLabel13.TabStop = true;
			ultraFormattedLinkLabel13.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel13.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel13.Value = "Retention:";
			appearance214.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel13.VisitedLinkAppearance = appearance214;
			ultraFormattedLinkLabel13.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel13_LinkClicked);
			textBoxProjectIncomeAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectIncomeAccount.CustomReportFieldName = "";
			textBoxProjectIncomeAccount.CustomReportKey = "";
			textBoxProjectIncomeAccount.CustomReportValueType = 1;
			textBoxProjectIncomeAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectIncomeAccount.IsComboTextBox = false;
			textBoxProjectIncomeAccount.IsModified = false;
			textBoxProjectIncomeAccount.Location = new System.Drawing.Point(240, 33);
			textBoxProjectIncomeAccount.MaxLength = 255;
			textBoxProjectIncomeAccount.Name = "textBoxProjectIncomeAccount";
			textBoxProjectIncomeAccount.ReadOnly = true;
			textBoxProjectIncomeAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectIncomeAccount.TabIndex = 3;
			textBoxProjectIncomeAccount.TabStop = false;
			appearance215.FontData.BoldAsString = "False";
			appearance215.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel11.Appearance = appearance215;
			ultraFormattedLinkLabel11.AutoSize = true;
			ultraFormattedLinkLabel11.Location = new System.Drawing.Point(7, 80);
			ultraFormattedLinkLabel11.Name = "ultraFormattedLinkLabel11";
			ultraFormattedLinkLabel11.Size = new System.Drawing.Size(88, 15);
			ultraFormattedLinkLabel11.TabIndex = 131;
			ultraFormattedLinkLabel11.TabStop = true;
			ultraFormattedLinkLabel11.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel11.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel11.Value = "Timsheet Contra:";
			appearance216.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel11.VisitedLinkAppearance = appearance216;
			ultraFormattedLinkLabel11.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel11_LinkClicked);
			textBoxProjectCostAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectCostAccount.CustomReportFieldName = "";
			textBoxProjectCostAccount.CustomReportKey = "";
			textBoxProjectCostAccount.CustomReportValueType = 1;
			textBoxProjectCostAccount.ForeColor = System.Drawing.Color.Black;
			textBoxProjectCostAccount.IsComboTextBox = false;
			textBoxProjectCostAccount.IsModified = false;
			textBoxProjectCostAccount.Location = new System.Drawing.Point(240, 55);
			textBoxProjectCostAccount.MaxLength = 255;
			textBoxProjectCostAccount.Name = "textBoxProjectCostAccount";
			textBoxProjectCostAccount.ReadOnly = true;
			textBoxProjectCostAccount.Size = new System.Drawing.Size(303, 21);
			textBoxProjectCostAccount.TabIndex = 5;
			textBoxProjectCostAccount.TabStop = false;
			comboBoxProjectTimesheetContra.AlwaysInEditMode = true;
			comboBoxProjectTimesheetContra.Assigned = false;
			comboBoxProjectTimesheetContra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectTimesheetContra.CustomReportFieldName = "";
			comboBoxProjectTimesheetContra.CustomReportKey = "";
			comboBoxProjectTimesheetContra.CustomReportValueType = 1;
			comboBoxProjectTimesheetContra.DescriptionTextBox = textBoxProjectTimesheetContra;
			appearance217.BackColor = System.Drawing.SystemColors.Window;
			appearance217.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectTimesheetContra.DisplayLayout.Appearance = appearance217;
			comboBoxProjectTimesheetContra.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectTimesheetContra.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance218.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance218.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance218.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance218.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectTimesheetContra.DisplayLayout.GroupByBox.Appearance = appearance218;
			appearance219.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectTimesheetContra.DisplayLayout.GroupByBox.BandLabelAppearance = appearance219;
			comboBoxProjectTimesheetContra.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance220.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance220.BackColor2 = System.Drawing.SystemColors.Control;
			appearance220.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance220.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectTimesheetContra.DisplayLayout.GroupByBox.PromptAppearance = appearance220;
			comboBoxProjectTimesheetContra.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectTimesheetContra.DisplayLayout.MaxRowScrollRegions = 1;
			appearance221.BackColor = System.Drawing.SystemColors.Window;
			appearance221.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.ActiveCellAppearance = appearance221;
			appearance222.BackColor = System.Drawing.SystemColors.Highlight;
			appearance222.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.ActiveRowAppearance = appearance222;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance223.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.CardAreaAppearance = appearance223;
			appearance224.BorderColor = System.Drawing.Color.Silver;
			appearance224.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.CellAppearance = appearance224;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.CellPadding = 0;
			appearance225.BackColor = System.Drawing.SystemColors.Control;
			appearance225.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance225.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance225.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance225.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.GroupByRowAppearance = appearance225;
			appearance226.TextHAlignAsString = "Left";
			comboBoxProjectTimesheetContra.DisplayLayout.Override.HeaderAppearance = appearance226;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance227.BackColor = System.Drawing.SystemColors.Window;
			appearance227.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.RowAppearance = appearance227;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance228.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectTimesheetContra.DisplayLayout.Override.TemplateAddRowAppearance = appearance228;
			comboBoxProjectTimesheetContra.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectTimesheetContra.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectTimesheetContra.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectTimesheetContra.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectTimesheetContra.Editable = true;
			comboBoxProjectTimesheetContra.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectTimesheetContra.FilterString = "";
			comboBoxProjectTimesheetContra.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectTimesheetContra.FilterSysDocID = "";
			comboBoxProjectTimesheetContra.HasAllAccount = false;
			comboBoxProjectTimesheetContra.HasCustom = false;
			comboBoxProjectTimesheetContra.IsDataLoaded = false;
			comboBoxProjectTimesheetContra.Location = new System.Drawing.Point(121, 78);
			comboBoxProjectTimesheetContra.MaxDropDownItems = 12;
			comboBoxProjectTimesheetContra.Name = "comboBoxProjectTimesheetContra";
			comboBoxProjectTimesheetContra.ShowInactiveItems = false;
			comboBoxProjectTimesheetContra.ShowQuickAdd = true;
			comboBoxProjectTimesheetContra.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectTimesheetContra.TabIndex = 6;
			comboBoxProjectTimesheetContra.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProjectTimesheetContra.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProjectTimesheetContra.CustomReportFieldName = "";
			textBoxProjectTimesheetContra.CustomReportKey = "";
			textBoxProjectTimesheetContra.CustomReportValueType = 1;
			textBoxProjectTimesheetContra.ForeColor = System.Drawing.Color.Black;
			textBoxProjectTimesheetContra.IsComboTextBox = false;
			textBoxProjectTimesheetContra.IsModified = false;
			textBoxProjectTimesheetContra.Location = new System.Drawing.Point(240, 78);
			textBoxProjectTimesheetContra.MaxLength = 255;
			textBoxProjectTimesheetContra.Name = "textBoxProjectTimesheetContra";
			textBoxProjectTimesheetContra.ReadOnly = true;
			textBoxProjectTimesheetContra.Size = new System.Drawing.Size(303, 21);
			textBoxProjectTimesheetContra.TabIndex = 7;
			textBoxProjectTimesheetContra.TabStop = false;
			comboBoxProjectWIPAccount.AlwaysInEditMode = true;
			comboBoxProjectWIPAccount.Assigned = false;
			comboBoxProjectWIPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectWIPAccount.CustomReportFieldName = "";
			comboBoxProjectWIPAccount.CustomReportKey = "";
			comboBoxProjectWIPAccount.CustomReportValueType = 1;
			comboBoxProjectWIPAccount.DescriptionTextBox = textBoxProjectWIPAccount;
			appearance229.BackColor = System.Drawing.SystemColors.Window;
			appearance229.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectWIPAccount.DisplayLayout.Appearance = appearance229;
			comboBoxProjectWIPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectWIPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance230.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance230.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance230.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance230.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.Appearance = appearance230;
			appearance231.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance231;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance232.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance232.BackColor2 = System.Drawing.SystemColors.Control;
			appearance232.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance232.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectWIPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance232;
			comboBoxProjectWIPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectWIPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance233.BackColor = System.Drawing.SystemColors.Window;
			appearance233.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance233;
			appearance234.BackColor = System.Drawing.SystemColors.Highlight;
			appearance234.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance234;
			comboBoxProjectWIPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectWIPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance235.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CardAreaAppearance = appearance235;
			appearance236.BorderColor = System.Drawing.Color.Silver;
			appearance236.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellAppearance = appearance236;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectWIPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance237.BackColor = System.Drawing.SystemColors.Control;
			appearance237.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance237.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance237.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance237.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectWIPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance237;
			appearance238.TextHAlignAsString = "Left";
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderAppearance = appearance238;
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectWIPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance239.BackColor = System.Drawing.SystemColors.Window;
			appearance239.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectWIPAccount.DisplayLayout.Override.RowAppearance = appearance239;
			comboBoxProjectWIPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance240.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectWIPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance240;
			comboBoxProjectWIPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectWIPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectWIPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectWIPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectWIPAccount.Editable = true;
			comboBoxProjectWIPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectWIPAccount.FilterString = "";
			comboBoxProjectWIPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectWIPAccount.FilterSysDocID = "";
			comboBoxProjectWIPAccount.HasAllAccount = false;
			comboBoxProjectWIPAccount.HasCustom = false;
			comboBoxProjectWIPAccount.IsDataLoaded = false;
			comboBoxProjectWIPAccount.Location = new System.Drawing.Point(121, 11);
			comboBoxProjectWIPAccount.MaxDropDownItems = 12;
			comboBoxProjectWIPAccount.Name = "comboBoxProjectWIPAccount";
			comboBoxProjectWIPAccount.ShowInactiveItems = false;
			comboBoxProjectWIPAccount.ShowQuickAdd = true;
			comboBoxProjectWIPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectWIPAccount.TabIndex = 0;
			comboBoxProjectWIPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxProjectIncomeAccount.AlwaysInEditMode = true;
			comboBoxProjectIncomeAccount.Assigned = false;
			comboBoxProjectIncomeAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectIncomeAccount.CustomReportFieldName = "";
			comboBoxProjectIncomeAccount.CustomReportKey = "";
			comboBoxProjectIncomeAccount.CustomReportValueType = 1;
			comboBoxProjectIncomeAccount.DescriptionTextBox = textBoxProjectIncomeAccount;
			appearance241.BackColor = System.Drawing.SystemColors.Window;
			appearance241.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectIncomeAccount.DisplayLayout.Appearance = appearance241;
			comboBoxProjectIncomeAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectIncomeAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance242.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance242.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance242.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance242.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.Appearance = appearance242;
			appearance243.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance243;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance244.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance244.BackColor2 = System.Drawing.SystemColors.Control;
			appearance244.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance244.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectIncomeAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance244;
			comboBoxProjectIncomeAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectIncomeAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance245.BackColor = System.Drawing.SystemColors.Window;
			appearance245.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.ActiveCellAppearance = appearance245;
			appearance246.BackColor = System.Drawing.SystemColors.Highlight;
			appearance246.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.ActiveRowAppearance = appearance246;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance247.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CardAreaAppearance = appearance247;
			appearance248.BorderColor = System.Drawing.Color.Silver;
			appearance248.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellAppearance = appearance248;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.CellPadding = 0;
			appearance249.BackColor = System.Drawing.SystemColors.Control;
			appearance249.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance249.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance249.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance249.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.GroupByRowAppearance = appearance249;
			appearance250.TextHAlignAsString = "Left";
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderAppearance = appearance250;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance251.BackColor = System.Drawing.SystemColors.Window;
			appearance251.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.RowAppearance = appearance251;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance252.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectIncomeAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance252;
			comboBoxProjectIncomeAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectIncomeAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectIncomeAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectIncomeAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectIncomeAccount.Editable = true;
			comboBoxProjectIncomeAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectIncomeAccount.FilterString = "";
			comboBoxProjectIncomeAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectIncomeAccount.FilterSysDocID = "";
			comboBoxProjectIncomeAccount.HasAllAccount = false;
			comboBoxProjectIncomeAccount.HasCustom = false;
			comboBoxProjectIncomeAccount.IsDataLoaded = false;
			comboBoxProjectIncomeAccount.Location = new System.Drawing.Point(121, 33);
			comboBoxProjectIncomeAccount.MaxDropDownItems = 12;
			comboBoxProjectIncomeAccount.Name = "comboBoxProjectIncomeAccount";
			comboBoxProjectIncomeAccount.ShowInactiveItems = false;
			comboBoxProjectIncomeAccount.ShowQuickAdd = true;
			comboBoxProjectIncomeAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectIncomeAccount.TabIndex = 2;
			comboBoxProjectIncomeAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance253.FontData.BoldAsString = "False";
			appearance253.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel16.Appearance = appearance253;
			ultraFormattedLinkLabel16.AutoSize = true;
			ultraFormattedLinkLabel16.Location = new System.Drawing.Point(7, 57);
			ultraFormattedLinkLabel16.Name = "ultraFormattedLinkLabel16";
			ultraFormattedLinkLabel16.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel16.TabIndex = 128;
			ultraFormattedLinkLabel16.TabStop = true;
			ultraFormattedLinkLabel16.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel16.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel16.Value = "Cost of Project:";
			appearance254.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel16.VisitedLinkAppearance = appearance254;
			ultraFormattedLinkLabel16.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel16_LinkClicked);
			comboBoxProjectCostAccount.AlwaysInEditMode = true;
			comboBoxProjectCostAccount.Assigned = false;
			comboBoxProjectCostAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProjectCostAccount.CustomReportFieldName = "";
			comboBoxProjectCostAccount.CustomReportKey = "";
			comboBoxProjectCostAccount.CustomReportValueType = 1;
			comboBoxProjectCostAccount.DescriptionTextBox = textBoxProjectCostAccount;
			appearance255.BackColor = System.Drawing.SystemColors.Window;
			appearance255.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProjectCostAccount.DisplayLayout.Appearance = appearance255;
			comboBoxProjectCostAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProjectCostAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance256.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance256.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance256.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance256.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.Appearance = appearance256;
			appearance257.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance257;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance258.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance258.BackColor2 = System.Drawing.SystemColors.Control;
			appearance258.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance258.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProjectCostAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance258;
			comboBoxProjectCostAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProjectCostAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance259.BackColor = System.Drawing.SystemColors.Window;
			appearance259.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveCellAppearance = appearance259;
			appearance260.BackColor = System.Drawing.SystemColors.Highlight;
			appearance260.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProjectCostAccount.DisplayLayout.Override.ActiveRowAppearance = appearance260;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProjectCostAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance261.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.CardAreaAppearance = appearance261;
			appearance262.BorderColor = System.Drawing.Color.Silver;
			appearance262.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellAppearance = appearance262;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProjectCostAccount.DisplayLayout.Override.CellPadding = 0;
			appearance263.BackColor = System.Drawing.SystemColors.Control;
			appearance263.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance263.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance263.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance263.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProjectCostAccount.DisplayLayout.Override.GroupByRowAppearance = appearance263;
			appearance264.TextHAlignAsString = "Left";
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderAppearance = appearance264;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProjectCostAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance265.BackColor = System.Drawing.SystemColors.Window;
			appearance265.BorderColor = System.Drawing.Color.Silver;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowAppearance = appearance265;
			comboBoxProjectCostAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance266.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProjectCostAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance266;
			comboBoxProjectCostAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProjectCostAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProjectCostAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProjectCostAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProjectCostAccount.Editable = true;
			comboBoxProjectCostAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProjectCostAccount.FilterString = "";
			comboBoxProjectCostAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProjectCostAccount.FilterSysDocID = "";
			comboBoxProjectCostAccount.HasAllAccount = false;
			comboBoxProjectCostAccount.HasCustom = false;
			comboBoxProjectCostAccount.IsDataLoaded = false;
			comboBoxProjectCostAccount.Location = new System.Drawing.Point(121, 55);
			comboBoxProjectCostAccount.MaxDropDownItems = 12;
			comboBoxProjectCostAccount.Name = "comboBoxProjectCostAccount";
			comboBoxProjectCostAccount.ShowInactiveItems = false;
			comboBoxProjectCostAccount.ShowQuickAdd = true;
			comboBoxProjectCostAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProjectCostAccount.TabIndex = 4;
			comboBoxProjectCostAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance267.FontData.BoldAsString = "False";
			appearance267.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel17.Appearance = appearance267;
			ultraFormattedLinkLabel17.AutoSize = true;
			ultraFormattedLinkLabel17.Location = new System.Drawing.Point(7, 35);
			ultraFormattedLinkLabel17.Name = "ultraFormattedLinkLabel17";
			ultraFormattedLinkLabel17.Size = new System.Drawing.Size(82, 15);
			ultraFormattedLinkLabel17.TabIndex = 128;
			ultraFormattedLinkLabel17.TabStop = true;
			ultraFormattedLinkLabel17.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel17.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel17.Value = "Project Income:";
			appearance268.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel17.VisitedLinkAppearance = appearance268;
			ultraFormattedLinkLabel17.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel17_LinkClicked);
			tabPageManufacturing.Controls.Add(ultraFormattedLinkLabel10);
			tabPageManufacturing.Controls.Add(ultraFormattedLinkLabel12);
			tabPageManufacturing.Controls.Add(textBoxManuWIPAccount);
			tabPageManufacturing.Controls.Add(comboBoxManuTimesheetContra);
			tabPageManufacturing.Controls.Add(textBoxManuTimesheetContra);
			tabPageManufacturing.Controls.Add(comboBoxManuWIPAccount);
			tabPageManufacturing.Location = new System.Drawing.Point(-10000, -10000);
			tabPageManufacturing.Name = "tabPageManufacturing";
			tabPageManufacturing.Size = new System.Drawing.Size(643, 365);
			appearance269.FontData.BoldAsString = "False";
			appearance269.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel10.Appearance = appearance269;
			ultraFormattedLinkLabel10.AutoSize = true;
			ultraFormattedLinkLabel10.Location = new System.Drawing.Point(7, 37);
			ultraFormattedLinkLabel10.Name = "ultraFormattedLinkLabel10";
			ultraFormattedLinkLabel10.Size = new System.Drawing.Size(94, 15);
			ultraFormattedLinkLabel10.TabIndex = 128;
			ultraFormattedLinkLabel10.TabStop = true;
			ultraFormattedLinkLabel10.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel10.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel10.Value = "Timesheet Contra:";
			appearance270.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel10.VisitedLinkAppearance = appearance270;
			ultraFormattedLinkLabel10.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel10_LinkClicked);
			appearance271.FontData.BoldAsString = "False";
			appearance271.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel12.Appearance = appearance271;
			ultraFormattedLinkLabel12.AutoSize = true;
			ultraFormattedLinkLabel12.Location = new System.Drawing.Point(7, 14);
			ultraFormattedLinkLabel12.Name = "ultraFormattedLinkLabel12";
			ultraFormattedLinkLabel12.Size = new System.Drawing.Size(92, 15);
			ultraFormattedLinkLabel12.TabIndex = 128;
			ultraFormattedLinkLabel12.TabStop = true;
			ultraFormattedLinkLabel12.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel12.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel12.Value = "Work In Progress:";
			appearance272.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel12.VisitedLinkAppearance = appearance272;
			ultraFormattedLinkLabel12.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel12_LinkClicked);
			textBoxManuWIPAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxManuWIPAccount.CustomReportFieldName = "";
			textBoxManuWIPAccount.CustomReportKey = "";
			textBoxManuWIPAccount.CustomReportValueType = 1;
			textBoxManuWIPAccount.ForeColor = System.Drawing.Color.Black;
			textBoxManuWIPAccount.IsComboTextBox = false;
			textBoxManuWIPAccount.IsModified = false;
			textBoxManuWIPAccount.Location = new System.Drawing.Point(240, 12);
			textBoxManuWIPAccount.MaxLength = 255;
			textBoxManuWIPAccount.Name = "textBoxManuWIPAccount";
			textBoxManuWIPAccount.ReadOnly = true;
			textBoxManuWIPAccount.Size = new System.Drawing.Size(303, 21);
			textBoxManuWIPAccount.TabIndex = 1;
			textBoxManuWIPAccount.TabStop = false;
			comboBoxManuTimesheetContra.AlwaysInEditMode = true;
			comboBoxManuTimesheetContra.Assigned = false;
			comboBoxManuTimesheetContra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManuTimesheetContra.CustomReportFieldName = "";
			comboBoxManuTimesheetContra.CustomReportKey = "";
			comboBoxManuTimesheetContra.CustomReportValueType = 1;
			comboBoxManuTimesheetContra.DescriptionTextBox = textBoxManuTimesheetContra;
			appearance273.BackColor = System.Drawing.SystemColors.Window;
			appearance273.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManuTimesheetContra.DisplayLayout.Appearance = appearance273;
			comboBoxManuTimesheetContra.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManuTimesheetContra.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance274.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance274.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance274.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance274.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuTimesheetContra.DisplayLayout.GroupByBox.Appearance = appearance274;
			appearance275.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuTimesheetContra.DisplayLayout.GroupByBox.BandLabelAppearance = appearance275;
			comboBoxManuTimesheetContra.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance276.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance276.BackColor2 = System.Drawing.SystemColors.Control;
			appearance276.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance276.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuTimesheetContra.DisplayLayout.GroupByBox.PromptAppearance = appearance276;
			comboBoxManuTimesheetContra.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManuTimesheetContra.DisplayLayout.MaxRowScrollRegions = 1;
			appearance277.BackColor = System.Drawing.SystemColors.Window;
			appearance277.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManuTimesheetContra.DisplayLayout.Override.ActiveCellAppearance = appearance277;
			appearance278.BackColor = System.Drawing.SystemColors.Highlight;
			appearance278.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManuTimesheetContra.DisplayLayout.Override.ActiveRowAppearance = appearance278;
			comboBoxManuTimesheetContra.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManuTimesheetContra.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance279.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManuTimesheetContra.DisplayLayout.Override.CardAreaAppearance = appearance279;
			appearance280.BorderColor = System.Drawing.Color.Silver;
			appearance280.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManuTimesheetContra.DisplayLayout.Override.CellAppearance = appearance280;
			comboBoxManuTimesheetContra.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManuTimesheetContra.DisplayLayout.Override.CellPadding = 0;
			appearance281.BackColor = System.Drawing.SystemColors.Control;
			appearance281.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance281.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance281.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance281.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuTimesheetContra.DisplayLayout.Override.GroupByRowAppearance = appearance281;
			appearance282.TextHAlignAsString = "Left";
			comboBoxManuTimesheetContra.DisplayLayout.Override.HeaderAppearance = appearance282;
			comboBoxManuTimesheetContra.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManuTimesheetContra.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance283.BackColor = System.Drawing.SystemColors.Window;
			appearance283.BorderColor = System.Drawing.Color.Silver;
			comboBoxManuTimesheetContra.DisplayLayout.Override.RowAppearance = appearance283;
			comboBoxManuTimesheetContra.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance284.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManuTimesheetContra.DisplayLayout.Override.TemplateAddRowAppearance = appearance284;
			comboBoxManuTimesheetContra.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManuTimesheetContra.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManuTimesheetContra.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManuTimesheetContra.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManuTimesheetContra.Editable = true;
			comboBoxManuTimesheetContra.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxManuTimesheetContra.FilterString = "";
			comboBoxManuTimesheetContra.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxManuTimesheetContra.FilterSysDocID = "";
			comboBoxManuTimesheetContra.HasAllAccount = false;
			comboBoxManuTimesheetContra.HasCustom = false;
			comboBoxManuTimesheetContra.IsDataLoaded = false;
			comboBoxManuTimesheetContra.Location = new System.Drawing.Point(121, 35);
			comboBoxManuTimesheetContra.MaxDropDownItems = 12;
			comboBoxManuTimesheetContra.Name = "comboBoxManuTimesheetContra";
			comboBoxManuTimesheetContra.ShowInactiveItems = false;
			comboBoxManuTimesheetContra.ShowQuickAdd = true;
			comboBoxManuTimesheetContra.Size = new System.Drawing.Size(117, 21);
			comboBoxManuTimesheetContra.TabIndex = 0;
			comboBoxManuTimesheetContra.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxManuTimesheetContra.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxManuTimesheetContra.CustomReportFieldName = "";
			textBoxManuTimesheetContra.CustomReportKey = "";
			textBoxManuTimesheetContra.CustomReportValueType = 1;
			textBoxManuTimesheetContra.ForeColor = System.Drawing.Color.Black;
			textBoxManuTimesheetContra.IsComboTextBox = false;
			textBoxManuTimesheetContra.IsModified = false;
			textBoxManuTimesheetContra.Location = new System.Drawing.Point(240, 35);
			textBoxManuTimesheetContra.MaxLength = 255;
			textBoxManuTimesheetContra.Name = "textBoxManuTimesheetContra";
			textBoxManuTimesheetContra.ReadOnly = true;
			textBoxManuTimesheetContra.Size = new System.Drawing.Size(303, 21);
			textBoxManuTimesheetContra.TabIndex = 1;
			textBoxManuTimesheetContra.TabStop = false;
			comboBoxManuWIPAccount.AlwaysInEditMode = true;
			comboBoxManuWIPAccount.Assigned = false;
			comboBoxManuWIPAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxManuWIPAccount.CustomReportFieldName = "";
			comboBoxManuWIPAccount.CustomReportKey = "";
			comboBoxManuWIPAccount.CustomReportValueType = 1;
			comboBoxManuWIPAccount.DescriptionTextBox = textBoxManuWIPAccount;
			appearance285.BackColor = System.Drawing.SystemColors.Window;
			appearance285.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxManuWIPAccount.DisplayLayout.Appearance = appearance285;
			comboBoxManuWIPAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxManuWIPAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance286.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance286.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance286.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance286.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.Appearance = appearance286;
			appearance287.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance287;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance288.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance288.BackColor2 = System.Drawing.SystemColors.Control;
			appearance288.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance288.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxManuWIPAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance288;
			comboBoxManuWIPAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxManuWIPAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance289.BackColor = System.Drawing.SystemColors.Window;
			appearance289.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxManuWIPAccount.DisplayLayout.Override.ActiveCellAppearance = appearance289;
			appearance290.BackColor = System.Drawing.SystemColors.Highlight;
			appearance290.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxManuWIPAccount.DisplayLayout.Override.ActiveRowAppearance = appearance290;
			comboBoxManuWIPAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxManuWIPAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance291.BackColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.Override.CardAreaAppearance = appearance291;
			appearance292.BorderColor = System.Drawing.Color.Silver;
			appearance292.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellAppearance = appearance292;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxManuWIPAccount.DisplayLayout.Override.CellPadding = 0;
			appearance293.BackColor = System.Drawing.SystemColors.Control;
			appearance293.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance293.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance293.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance293.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxManuWIPAccount.DisplayLayout.Override.GroupByRowAppearance = appearance293;
			appearance294.TextHAlignAsString = "Left";
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderAppearance = appearance294;
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxManuWIPAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance295.BackColor = System.Drawing.SystemColors.Window;
			appearance295.BorderColor = System.Drawing.Color.Silver;
			comboBoxManuWIPAccount.DisplayLayout.Override.RowAppearance = appearance295;
			comboBoxManuWIPAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance296.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxManuWIPAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance296;
			comboBoxManuWIPAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxManuWIPAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxManuWIPAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxManuWIPAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxManuWIPAccount.Editable = true;
			comboBoxManuWIPAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxManuWIPAccount.FilterString = "";
			comboBoxManuWIPAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxManuWIPAccount.FilterSysDocID = "";
			comboBoxManuWIPAccount.HasAllAccount = false;
			comboBoxManuWIPAccount.HasCustom = false;
			comboBoxManuWIPAccount.IsDataLoaded = false;
			comboBoxManuWIPAccount.Location = new System.Drawing.Point(121, 12);
			comboBoxManuWIPAccount.MaxDropDownItems = 12;
			comboBoxManuWIPAccount.Name = "comboBoxManuWIPAccount";
			comboBoxManuWIPAccount.ShowInactiveItems = false;
			comboBoxManuWIPAccount.ShowQuickAdd = true;
			comboBoxManuWIPAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxManuWIPAccount.TabIndex = 0;
			comboBoxManuWIPAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraTabPageControl1.Controls.Add(ultraGroupBox2);
			ultraTabPageControl1.Controls.Add(ultraGroupBox1);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(643, 365);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel23);
			ultraGroupBox2.Controls.Add(textBoxConsignOutCOGS);
			ultraGroupBox2.Controls.Add(comboBoxConsignOutCOGS);
			ultraGroupBox2.Controls.Add(ultraFormattedLinkLabel24);
			ultraGroupBox2.Controls.Add(textBoxConsignOutSales);
			ultraGroupBox2.Controls.Add(comboBoxConsignOutSales);
			ultraGroupBox2.Location = new System.Drawing.Point(7, 124);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(610, 96);
			ultraGroupBox2.TabIndex = 162;
			ultraGroupBox2.Text = "Consignment Out";
			appearance297.FontData.BoldAsString = "False";
			appearance297.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel23.Appearance = appearance297;
			ultraFormattedLinkLabel23.AutoSize = true;
			ultraFormattedLinkLabel23.Location = new System.Drawing.Point(6, 44);
			ultraFormattedLinkLabel23.Name = "ultraFormattedLinkLabel23";
			ultraFormattedLinkLabel23.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel23.TabIndex = 163;
			ultraFormattedLinkLabel23.TabStop = true;
			ultraFormattedLinkLabel23.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel23.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel23.Value = "COGS:";
			appearance298.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel23.VisitedLinkAppearance = appearance298;
			ultraFormattedLinkLabel23.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel23_LinkClicked);
			textBoxConsignOutCOGS.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignOutCOGS.CustomReportFieldName = "";
			textBoxConsignOutCOGS.CustomReportKey = "";
			textBoxConsignOutCOGS.CustomReportValueType = 1;
			textBoxConsignOutCOGS.ForeColor = System.Drawing.Color.Black;
			textBoxConsignOutCOGS.IsComboTextBox = false;
			textBoxConsignOutCOGS.IsModified = false;
			textBoxConsignOutCOGS.Location = new System.Drawing.Point(239, 42);
			textBoxConsignOutCOGS.MaxLength = 255;
			textBoxConsignOutCOGS.Name = "textBoxConsignOutCOGS";
			textBoxConsignOutCOGS.ReadOnly = true;
			textBoxConsignOutCOGS.Size = new System.Drawing.Size(303, 21);
			textBoxConsignOutCOGS.TabIndex = 162;
			textBoxConsignOutCOGS.TabStop = false;
			comboBoxConsignOutCOGS.AlwaysInEditMode = true;
			comboBoxConsignOutCOGS.Assigned = false;
			comboBoxConsignOutCOGS.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignOutCOGS.CustomReportFieldName = "";
			comboBoxConsignOutCOGS.CustomReportKey = "";
			comboBoxConsignOutCOGS.CustomReportValueType = 1;
			comboBoxConsignOutCOGS.DescriptionTextBox = textBoxConsignOutCOGS;
			appearance299.BackColor = System.Drawing.SystemColors.Window;
			appearance299.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignOutCOGS.DisplayLayout.Appearance = appearance299;
			comboBoxConsignOutCOGS.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignOutCOGS.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance300.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance300.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance300.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance300.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutCOGS.DisplayLayout.GroupByBox.Appearance = appearance300;
			appearance301.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutCOGS.DisplayLayout.GroupByBox.BandLabelAppearance = appearance301;
			comboBoxConsignOutCOGS.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance302.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance302.BackColor2 = System.Drawing.SystemColors.Control;
			appearance302.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance302.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutCOGS.DisplayLayout.GroupByBox.PromptAppearance = appearance302;
			comboBoxConsignOutCOGS.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignOutCOGS.DisplayLayout.MaxRowScrollRegions = 1;
			appearance303.BackColor = System.Drawing.SystemColors.Window;
			appearance303.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignOutCOGS.DisplayLayout.Override.ActiveCellAppearance = appearance303;
			appearance304.BackColor = System.Drawing.SystemColors.Highlight;
			appearance304.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignOutCOGS.DisplayLayout.Override.ActiveRowAppearance = appearance304;
			comboBoxConsignOutCOGS.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignOutCOGS.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance305.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutCOGS.DisplayLayout.Override.CardAreaAppearance = appearance305;
			appearance306.BorderColor = System.Drawing.Color.Silver;
			appearance306.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignOutCOGS.DisplayLayout.Override.CellAppearance = appearance306;
			comboBoxConsignOutCOGS.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignOutCOGS.DisplayLayout.Override.CellPadding = 0;
			appearance307.BackColor = System.Drawing.SystemColors.Control;
			appearance307.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance307.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance307.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance307.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutCOGS.DisplayLayout.Override.GroupByRowAppearance = appearance307;
			appearance308.TextHAlignAsString = "Left";
			comboBoxConsignOutCOGS.DisplayLayout.Override.HeaderAppearance = appearance308;
			comboBoxConsignOutCOGS.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignOutCOGS.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance309.BackColor = System.Drawing.SystemColors.Window;
			appearance309.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignOutCOGS.DisplayLayout.Override.RowAppearance = appearance309;
			comboBoxConsignOutCOGS.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance310.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignOutCOGS.DisplayLayout.Override.TemplateAddRowAppearance = appearance310;
			comboBoxConsignOutCOGS.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignOutCOGS.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignOutCOGS.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignOutCOGS.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignOutCOGS.Editable = true;
			comboBoxConsignOutCOGS.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxConsignOutCOGS.FilterString = "";
			comboBoxConsignOutCOGS.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxConsignOutCOGS.FilterSysDocID = "";
			comboBoxConsignOutCOGS.HasAllAccount = false;
			comboBoxConsignOutCOGS.HasCustom = false;
			comboBoxConsignOutCOGS.IsDataLoaded = false;
			comboBoxConsignOutCOGS.Location = new System.Drawing.Point(120, 42);
			comboBoxConsignOutCOGS.MaxDropDownItems = 12;
			comboBoxConsignOutCOGS.Name = "comboBoxConsignOutCOGS";
			comboBoxConsignOutCOGS.ShowInactiveItems = false;
			comboBoxConsignOutCOGS.ShowQuickAdd = true;
			comboBoxConsignOutCOGS.Size = new System.Drawing.Size(117, 21);
			comboBoxConsignOutCOGS.TabIndex = 161;
			comboBoxConsignOutCOGS.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance311.FontData.BoldAsString = "False";
			appearance311.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel24.Appearance = appearance311;
			ultraFormattedLinkLabel24.AutoSize = true;
			ultraFormattedLinkLabel24.Location = new System.Drawing.Point(6, 20);
			ultraFormattedLinkLabel24.Name = "ultraFormattedLinkLabel24";
			ultraFormattedLinkLabel24.Size = new System.Drawing.Size(73, 15);
			ultraFormattedLinkLabel24.TabIndex = 160;
			ultraFormattedLinkLabel24.TabStop = true;
			ultraFormattedLinkLabel24.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel24.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel24.Value = "Sales Income:";
			appearance312.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel24.VisitedLinkAppearance = appearance312;
			ultraFormattedLinkLabel24.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel24_LinkClicked);
			textBoxConsignOutSales.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignOutSales.CustomReportFieldName = "";
			textBoxConsignOutSales.CustomReportKey = "";
			textBoxConsignOutSales.CustomReportValueType = 1;
			textBoxConsignOutSales.ForeColor = System.Drawing.Color.Black;
			textBoxConsignOutSales.IsComboTextBox = false;
			textBoxConsignOutSales.IsModified = false;
			textBoxConsignOutSales.Location = new System.Drawing.Point(239, 18);
			textBoxConsignOutSales.MaxLength = 255;
			textBoxConsignOutSales.Name = "textBoxConsignOutSales";
			textBoxConsignOutSales.ReadOnly = true;
			textBoxConsignOutSales.Size = new System.Drawing.Size(303, 21);
			textBoxConsignOutSales.TabIndex = 159;
			textBoxConsignOutSales.TabStop = false;
			comboBoxConsignOutSales.AlwaysInEditMode = true;
			comboBoxConsignOutSales.Assigned = false;
			comboBoxConsignOutSales.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignOutSales.CustomReportFieldName = "";
			comboBoxConsignOutSales.CustomReportKey = "";
			comboBoxConsignOutSales.CustomReportValueType = 1;
			comboBoxConsignOutSales.DescriptionTextBox = textBoxConsignOutSales;
			appearance313.BackColor = System.Drawing.SystemColors.Window;
			appearance313.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignOutSales.DisplayLayout.Appearance = appearance313;
			comboBoxConsignOutSales.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignOutSales.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance314.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance314.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance314.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance314.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutSales.DisplayLayout.GroupByBox.Appearance = appearance314;
			appearance315.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutSales.DisplayLayout.GroupByBox.BandLabelAppearance = appearance315;
			comboBoxConsignOutSales.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance316.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance316.BackColor2 = System.Drawing.SystemColors.Control;
			appearance316.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance316.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignOutSales.DisplayLayout.GroupByBox.PromptAppearance = appearance316;
			comboBoxConsignOutSales.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignOutSales.DisplayLayout.MaxRowScrollRegions = 1;
			appearance317.BackColor = System.Drawing.SystemColors.Window;
			appearance317.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignOutSales.DisplayLayout.Override.ActiveCellAppearance = appearance317;
			appearance318.BackColor = System.Drawing.SystemColors.Highlight;
			appearance318.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignOutSales.DisplayLayout.Override.ActiveRowAppearance = appearance318;
			comboBoxConsignOutSales.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignOutSales.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance319.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutSales.DisplayLayout.Override.CardAreaAppearance = appearance319;
			appearance320.BorderColor = System.Drawing.Color.Silver;
			appearance320.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignOutSales.DisplayLayout.Override.CellAppearance = appearance320;
			comboBoxConsignOutSales.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignOutSales.DisplayLayout.Override.CellPadding = 0;
			appearance321.BackColor = System.Drawing.SystemColors.Control;
			appearance321.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance321.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance321.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance321.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignOutSales.DisplayLayout.Override.GroupByRowAppearance = appearance321;
			appearance322.TextHAlignAsString = "Left";
			comboBoxConsignOutSales.DisplayLayout.Override.HeaderAppearance = appearance322;
			comboBoxConsignOutSales.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignOutSales.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance323.BackColor = System.Drawing.SystemColors.Window;
			appearance323.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignOutSales.DisplayLayout.Override.RowAppearance = appearance323;
			comboBoxConsignOutSales.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance324.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignOutSales.DisplayLayout.Override.TemplateAddRowAppearance = appearance324;
			comboBoxConsignOutSales.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignOutSales.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignOutSales.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignOutSales.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignOutSales.Editable = true;
			comboBoxConsignOutSales.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxConsignOutSales.FilterString = "";
			comboBoxConsignOutSales.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxConsignOutSales.FilterSysDocID = "";
			comboBoxConsignOutSales.HasAllAccount = false;
			comboBoxConsignOutSales.HasCustom = false;
			comboBoxConsignOutSales.IsDataLoaded = false;
			comboBoxConsignOutSales.Location = new System.Drawing.Point(120, 18);
			comboBoxConsignOutSales.MaxDropDownItems = 12;
			comboBoxConsignOutSales.Name = "comboBoxConsignOutSales";
			comboBoxConsignOutSales.ShowInactiveItems = false;
			comboBoxConsignOutSales.ShowQuickAdd = true;
			comboBoxConsignOutSales.Size = new System.Drawing.Size(117, 21);
			comboBoxConsignOutSales.TabIndex = 158;
			comboBoxConsignOutSales.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel21);
			ultraGroupBox1.Controls.Add(textBoxConsignInDiff);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel20);
			ultraGroupBox1.Controls.Add(comboBoxConsignInDiff);
			ultraGroupBox1.Controls.Add(textBoxConsignInComm);
			ultraGroupBox1.Controls.Add(comboBoxConsignInCommissionAccount);
			ultraGroupBox1.Controls.Add(ultraFormattedLinkLabel19);
			ultraGroupBox1.Controls.Add(textBoxConsignInAccount);
			ultraGroupBox1.Controls.Add(comboBoxConsignInAccount);
			ultraGroupBox1.Location = new System.Drawing.Point(7, 13);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(610, 96);
			ultraGroupBox1.TabIndex = 161;
			ultraGroupBox1.Text = "Consignment In";
			appearance325.FontData.BoldAsString = "False";
			appearance325.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel21.Appearance = appearance325;
			ultraFormattedLinkLabel21.AutoSize = true;
			ultraFormattedLinkLabel21.Location = new System.Drawing.Point(6, 67);
			ultraFormattedLinkLabel21.Name = "ultraFormattedLinkLabel21";
			ultraFormattedLinkLabel21.Size = new System.Drawing.Size(99, 15);
			ultraFormattedLinkLabel21.TabIndex = 163;
			ultraFormattedLinkLabel21.TabStop = true;
			ultraFormattedLinkLabel21.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel21.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel21.Value = "Difference in Sales:";
			appearance326.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel21.VisitedLinkAppearance = appearance326;
			ultraFormattedLinkLabel21.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel21_LinkClicked);
			textBoxConsignInDiff.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignInDiff.CustomReportFieldName = "";
			textBoxConsignInDiff.CustomReportKey = "";
			textBoxConsignInDiff.CustomReportValueType = 1;
			textBoxConsignInDiff.ForeColor = System.Drawing.Color.Black;
			textBoxConsignInDiff.IsComboTextBox = false;
			textBoxConsignInDiff.IsModified = false;
			textBoxConsignInDiff.Location = new System.Drawing.Point(239, 65);
			textBoxConsignInDiff.MaxLength = 255;
			textBoxConsignInDiff.Name = "textBoxConsignInDiff";
			textBoxConsignInDiff.ReadOnly = true;
			textBoxConsignInDiff.Size = new System.Drawing.Size(303, 21);
			textBoxConsignInDiff.TabIndex = 162;
			textBoxConsignInDiff.TabStop = false;
			appearance327.FontData.BoldAsString = "False";
			appearance327.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel20.Appearance = appearance327;
			ultraFormattedLinkLabel20.AutoSize = true;
			ultraFormattedLinkLabel20.Location = new System.Drawing.Point(6, 44);
			ultraFormattedLinkLabel20.Name = "ultraFormattedLinkLabel20";
			ultraFormattedLinkLabel20.Size = new System.Drawing.Size(106, 15);
			ultraFormattedLinkLabel20.TabIndex = 163;
			ultraFormattedLinkLabel20.TabStop = true;
			ultraFormattedLinkLabel20.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel20.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel20.Value = "Commission Income:";
			appearance328.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel20.VisitedLinkAppearance = appearance328;
			ultraFormattedLinkLabel20.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel20_LinkClicked);
			comboBoxConsignInDiff.AlwaysInEditMode = true;
			comboBoxConsignInDiff.Assigned = false;
			comboBoxConsignInDiff.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignInDiff.CustomReportFieldName = "";
			comboBoxConsignInDiff.CustomReportKey = "";
			comboBoxConsignInDiff.CustomReportValueType = 1;
			comboBoxConsignInDiff.DescriptionTextBox = textBoxConsignInDiff;
			appearance329.BackColor = System.Drawing.SystemColors.Window;
			appearance329.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignInDiff.DisplayLayout.Appearance = appearance329;
			comboBoxConsignInDiff.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignInDiff.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance330.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance330.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance330.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance330.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInDiff.DisplayLayout.GroupByBox.Appearance = appearance330;
			appearance331.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInDiff.DisplayLayout.GroupByBox.BandLabelAppearance = appearance331;
			comboBoxConsignInDiff.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance332.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance332.BackColor2 = System.Drawing.SystemColors.Control;
			appearance332.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance332.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInDiff.DisplayLayout.GroupByBox.PromptAppearance = appearance332;
			comboBoxConsignInDiff.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignInDiff.DisplayLayout.MaxRowScrollRegions = 1;
			appearance333.BackColor = System.Drawing.SystemColors.Window;
			appearance333.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignInDiff.DisplayLayout.Override.ActiveCellAppearance = appearance333;
			appearance334.BackColor = System.Drawing.SystemColors.Highlight;
			appearance334.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignInDiff.DisplayLayout.Override.ActiveRowAppearance = appearance334;
			comboBoxConsignInDiff.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignInDiff.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance335.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInDiff.DisplayLayout.Override.CardAreaAppearance = appearance335;
			appearance336.BorderColor = System.Drawing.Color.Silver;
			appearance336.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignInDiff.DisplayLayout.Override.CellAppearance = appearance336;
			comboBoxConsignInDiff.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignInDiff.DisplayLayout.Override.CellPadding = 0;
			appearance337.BackColor = System.Drawing.SystemColors.Control;
			appearance337.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance337.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance337.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance337.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInDiff.DisplayLayout.Override.GroupByRowAppearance = appearance337;
			appearance338.TextHAlignAsString = "Left";
			comboBoxConsignInDiff.DisplayLayout.Override.HeaderAppearance = appearance338;
			comboBoxConsignInDiff.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignInDiff.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance339.BackColor = System.Drawing.SystemColors.Window;
			appearance339.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignInDiff.DisplayLayout.Override.RowAppearance = appearance339;
			comboBoxConsignInDiff.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance340.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignInDiff.DisplayLayout.Override.TemplateAddRowAppearance = appearance340;
			comboBoxConsignInDiff.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignInDiff.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignInDiff.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignInDiff.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignInDiff.Editable = true;
			comboBoxConsignInDiff.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxConsignInDiff.FilterString = "";
			comboBoxConsignInDiff.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxConsignInDiff.FilterSysDocID = "";
			comboBoxConsignInDiff.HasAllAccount = false;
			comboBoxConsignInDiff.HasCustom = false;
			comboBoxConsignInDiff.IsDataLoaded = false;
			comboBoxConsignInDiff.Location = new System.Drawing.Point(120, 65);
			comboBoxConsignInDiff.MaxDropDownItems = 12;
			comboBoxConsignInDiff.Name = "comboBoxConsignInDiff";
			comboBoxConsignInDiff.ShowInactiveItems = false;
			comboBoxConsignInDiff.ShowQuickAdd = true;
			comboBoxConsignInDiff.Size = new System.Drawing.Size(117, 21);
			comboBoxConsignInDiff.TabIndex = 161;
			comboBoxConsignInDiff.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxConsignInComm.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignInComm.CustomReportFieldName = "";
			textBoxConsignInComm.CustomReportKey = "";
			textBoxConsignInComm.CustomReportValueType = 1;
			textBoxConsignInComm.ForeColor = System.Drawing.Color.Black;
			textBoxConsignInComm.IsComboTextBox = false;
			textBoxConsignInComm.IsModified = false;
			textBoxConsignInComm.Location = new System.Drawing.Point(239, 42);
			textBoxConsignInComm.MaxLength = 255;
			textBoxConsignInComm.Name = "textBoxConsignInComm";
			textBoxConsignInComm.ReadOnly = true;
			textBoxConsignInComm.Size = new System.Drawing.Size(303, 21);
			textBoxConsignInComm.TabIndex = 162;
			textBoxConsignInComm.TabStop = false;
			comboBoxConsignInCommissionAccount.AlwaysInEditMode = true;
			comboBoxConsignInCommissionAccount.Assigned = false;
			comboBoxConsignInCommissionAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignInCommissionAccount.CustomReportFieldName = "";
			comboBoxConsignInCommissionAccount.CustomReportKey = "";
			comboBoxConsignInCommissionAccount.CustomReportValueType = 1;
			comboBoxConsignInCommissionAccount.DescriptionTextBox = textBoxConsignInComm;
			appearance341.BackColor = System.Drawing.SystemColors.Window;
			appearance341.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignInCommissionAccount.DisplayLayout.Appearance = appearance341;
			comboBoxConsignInCommissionAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignInCommissionAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance342.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance342.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance342.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance342.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInCommissionAccount.DisplayLayout.GroupByBox.Appearance = appearance342;
			appearance343.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInCommissionAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance343;
			comboBoxConsignInCommissionAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance344.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance344.BackColor2 = System.Drawing.SystemColors.Control;
			appearance344.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance344.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInCommissionAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance344;
			comboBoxConsignInCommissionAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignInCommissionAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance345.BackColor = System.Drawing.SystemColors.Window;
			appearance345.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.ActiveCellAppearance = appearance345;
			appearance346.BackColor = System.Drawing.SystemColors.Highlight;
			appearance346.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.ActiveRowAppearance = appearance346;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance347.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.CardAreaAppearance = appearance347;
			appearance348.BorderColor = System.Drawing.Color.Silver;
			appearance348.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.CellAppearance = appearance348;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.CellPadding = 0;
			appearance349.BackColor = System.Drawing.SystemColors.Control;
			appearance349.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance349.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance349.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance349.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.GroupByRowAppearance = appearance349;
			appearance350.TextHAlignAsString = "Left";
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.HeaderAppearance = appearance350;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance351.BackColor = System.Drawing.SystemColors.Window;
			appearance351.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.RowAppearance = appearance351;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance352.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignInCommissionAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance352;
			comboBoxConsignInCommissionAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignInCommissionAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignInCommissionAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignInCommissionAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignInCommissionAccount.Editable = true;
			comboBoxConsignInCommissionAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxConsignInCommissionAccount.FilterString = "";
			comboBoxConsignInCommissionAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxConsignInCommissionAccount.FilterSysDocID = "";
			comboBoxConsignInCommissionAccount.HasAllAccount = false;
			comboBoxConsignInCommissionAccount.HasCustom = false;
			comboBoxConsignInCommissionAccount.IsDataLoaded = false;
			comboBoxConsignInCommissionAccount.Location = new System.Drawing.Point(120, 42);
			comboBoxConsignInCommissionAccount.MaxDropDownItems = 12;
			comboBoxConsignInCommissionAccount.Name = "comboBoxConsignInCommissionAccount";
			comboBoxConsignInCommissionAccount.ShowInactiveItems = false;
			comboBoxConsignInCommissionAccount.ShowQuickAdd = true;
			comboBoxConsignInCommissionAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxConsignInCommissionAccount.TabIndex = 161;
			comboBoxConsignInCommissionAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance353.FontData.BoldAsString = "False";
			appearance353.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel19.Appearance = appearance353;
			ultraFormattedLinkLabel19.AutoSize = true;
			ultraFormattedLinkLabel19.Location = new System.Drawing.Point(6, 20);
			ultraFormattedLinkLabel19.Name = "ultraFormattedLinkLabel19";
			ultraFormattedLinkLabel19.Size = new System.Drawing.Size(85, 15);
			ultraFormattedLinkLabel19.TabIndex = 160;
			ultraFormattedLinkLabel19.TabStop = true;
			ultraFormattedLinkLabel19.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel19.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel19.Value = "Consignment In:";
			appearance354.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel19.VisitedLinkAppearance = appearance354;
			ultraFormattedLinkLabel19.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel19_LinkClicked);
			textBoxConsignInAccount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxConsignInAccount.CustomReportFieldName = "";
			textBoxConsignInAccount.CustomReportKey = "";
			textBoxConsignInAccount.CustomReportValueType = 1;
			textBoxConsignInAccount.ForeColor = System.Drawing.Color.Black;
			textBoxConsignInAccount.IsComboTextBox = false;
			textBoxConsignInAccount.IsModified = false;
			textBoxConsignInAccount.Location = new System.Drawing.Point(239, 18);
			textBoxConsignInAccount.MaxLength = 255;
			textBoxConsignInAccount.Name = "textBoxConsignInAccount";
			textBoxConsignInAccount.ReadOnly = true;
			textBoxConsignInAccount.Size = new System.Drawing.Size(303, 21);
			textBoxConsignInAccount.TabIndex = 159;
			textBoxConsignInAccount.TabStop = false;
			comboBoxConsignInAccount.AlwaysInEditMode = true;
			comboBoxConsignInAccount.Assigned = false;
			comboBoxConsignInAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxConsignInAccount.CustomReportFieldName = "";
			comboBoxConsignInAccount.CustomReportKey = "";
			comboBoxConsignInAccount.CustomReportValueType = 1;
			comboBoxConsignInAccount.DescriptionTextBox = textBoxConsignInAccount;
			appearance355.BackColor = System.Drawing.SystemColors.Window;
			appearance355.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxConsignInAccount.DisplayLayout.Appearance = appearance355;
			comboBoxConsignInAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxConsignInAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance356.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance356.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance356.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance356.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInAccount.DisplayLayout.GroupByBox.Appearance = appearance356;
			appearance357.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance357;
			comboBoxConsignInAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance358.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance358.BackColor2 = System.Drawing.SystemColors.Control;
			appearance358.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance358.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxConsignInAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance358;
			comboBoxConsignInAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxConsignInAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance359.BackColor = System.Drawing.SystemColors.Window;
			appearance359.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxConsignInAccount.DisplayLayout.Override.ActiveCellAppearance = appearance359;
			appearance360.BackColor = System.Drawing.SystemColors.Highlight;
			appearance360.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxConsignInAccount.DisplayLayout.Override.ActiveRowAppearance = appearance360;
			comboBoxConsignInAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxConsignInAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance361.BackColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInAccount.DisplayLayout.Override.CardAreaAppearance = appearance361;
			appearance362.BorderColor = System.Drawing.Color.Silver;
			appearance362.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxConsignInAccount.DisplayLayout.Override.CellAppearance = appearance362;
			comboBoxConsignInAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxConsignInAccount.DisplayLayout.Override.CellPadding = 0;
			appearance363.BackColor = System.Drawing.SystemColors.Control;
			appearance363.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance363.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance363.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance363.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxConsignInAccount.DisplayLayout.Override.GroupByRowAppearance = appearance363;
			appearance364.TextHAlignAsString = "Left";
			comboBoxConsignInAccount.DisplayLayout.Override.HeaderAppearance = appearance364;
			comboBoxConsignInAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxConsignInAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance365.BackColor = System.Drawing.SystemColors.Window;
			appearance365.BorderColor = System.Drawing.Color.Silver;
			comboBoxConsignInAccount.DisplayLayout.Override.RowAppearance = appearance365;
			comboBoxConsignInAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance366.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxConsignInAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance366;
			comboBoxConsignInAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxConsignInAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxConsignInAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxConsignInAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxConsignInAccount.Editable = true;
			comboBoxConsignInAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxConsignInAccount.FilterString = "";
			comboBoxConsignInAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxConsignInAccount.FilterSysDocID = "";
			comboBoxConsignInAccount.HasAllAccount = false;
			comboBoxConsignInAccount.HasCustom = false;
			comboBoxConsignInAccount.IsDataLoaded = false;
			comboBoxConsignInAccount.Location = new System.Drawing.Point(120, 18);
			comboBoxConsignInAccount.MaxDropDownItems = 12;
			comboBoxConsignInAccount.Name = "comboBoxConsignInAccount";
			comboBoxConsignInAccount.ShowInactiveItems = false;
			comboBoxConsignInAccount.ShowQuickAdd = true;
			comboBoxConsignInAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxConsignInAccount.TabIndex = 158;
			comboBoxConsignInAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraTabPageControl2.Controls.Add(dataEntryGridTax);
			ultraTabPageControl2.Controls.Add(comboBoxGridPurchaseAccount);
			ultraTabPageControl2.Controls.Add(comboBoxGridSalesAccount);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(643, 365);
			dataEntryGridTax.AllowAddNew = false;
			dataEntryGridTax.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance367.BackColor = System.Drawing.SystemColors.Window;
			appearance367.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataEntryGridTax.DisplayLayout.Appearance = appearance367;
			dataEntryGridTax.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataEntryGridTax.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance368.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance368.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance368.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance368.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.GroupByBox.Appearance = appearance368;
			appearance369.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTax.DisplayLayout.GroupByBox.BandLabelAppearance = appearance369;
			dataEntryGridTax.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance370.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance370.BackColor2 = System.Drawing.SystemColors.Control;
			appearance370.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance370.ForeColor = System.Drawing.SystemColors.GrayText;
			dataEntryGridTax.DisplayLayout.GroupByBox.PromptAppearance = appearance370;
			dataEntryGridTax.DisplayLayout.MaxColScrollRegions = 1;
			dataEntryGridTax.DisplayLayout.MaxRowScrollRegions = 1;
			appearance371.BackColor = System.Drawing.SystemColors.Window;
			appearance371.ForeColor = System.Drawing.SystemColors.ControlText;
			dataEntryGridTax.DisplayLayout.Override.ActiveCellAppearance = appearance371;
			appearance372.BackColor = System.Drawing.SystemColors.Highlight;
			appearance372.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataEntryGridTax.DisplayLayout.Override.ActiveRowAppearance = appearance372;
			dataEntryGridTax.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataEntryGridTax.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataEntryGridTax.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance373.BackColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.Override.CardAreaAppearance = appearance373;
			appearance374.BorderColor = System.Drawing.Color.Silver;
			appearance374.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataEntryGridTax.DisplayLayout.Override.CellAppearance = appearance374;
			dataEntryGridTax.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataEntryGridTax.DisplayLayout.Override.CellPadding = 0;
			appearance375.BackColor = System.Drawing.SystemColors.Control;
			appearance375.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance375.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance375.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance375.BorderColor = System.Drawing.SystemColors.Window;
			dataEntryGridTax.DisplayLayout.Override.GroupByRowAppearance = appearance375;
			appearance376.TextHAlignAsString = "Left";
			dataEntryGridTax.DisplayLayout.Override.HeaderAppearance = appearance376;
			dataEntryGridTax.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataEntryGridTax.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance377.BackColor = System.Drawing.SystemColors.Window;
			appearance377.BorderColor = System.Drawing.Color.Silver;
			dataEntryGridTax.DisplayLayout.Override.RowAppearance = appearance377;
			dataEntryGridTax.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance378.BackColor = System.Drawing.SystemColors.ControlLight;
			dataEntryGridTax.DisplayLayout.Override.TemplateAddRowAppearance = appearance378;
			dataEntryGridTax.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataEntryGridTax.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataEntryGridTax.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataEntryGridTax.IncludeLotItems = false;
			dataEntryGridTax.LoadLayoutFailed = false;
			dataEntryGridTax.Location = new System.Drawing.Point(1, 14);
			dataEntryGridTax.Name = "dataEntryGridTax";
			dataEntryGridTax.ShowClearMenu = true;
			dataEntryGridTax.ShowDeleteMenu = true;
			dataEntryGridTax.ShowInsertMenu = true;
			dataEntryGridTax.ShowMoveRowsMenu = true;
			dataEntryGridTax.Size = new System.Drawing.Size(635, 287);
			dataEntryGridTax.TabIndex = 6;
			comboBoxGridPurchaseAccount.Assigned = false;
			comboBoxGridPurchaseAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridPurchaseAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridPurchaseAccount.CustomReportFieldName = "";
			comboBoxGridPurchaseAccount.CustomReportKey = "";
			comboBoxGridPurchaseAccount.CustomReportValueType = 1;
			comboBoxGridPurchaseAccount.DescriptionTextBox = null;
			appearance379.BackColor = System.Drawing.SystemColors.Window;
			appearance379.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridPurchaseAccount.DisplayLayout.Appearance = appearance379;
			comboBoxGridPurchaseAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridPurchaseAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance380.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance380.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance380.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance380.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPurchaseAccount.DisplayLayout.GroupByBox.Appearance = appearance380;
			appearance381.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPurchaseAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance381;
			comboBoxGridPurchaseAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance382.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance382.BackColor2 = System.Drawing.SystemColors.Control;
			appearance382.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance382.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPurchaseAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance382;
			comboBoxGridPurchaseAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridPurchaseAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance383.BackColor = System.Drawing.SystemColors.Window;
			appearance383.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.ActiveCellAppearance = appearance383;
			appearance384.BackColor = System.Drawing.SystemColors.Highlight;
			appearance384.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.ActiveRowAppearance = appearance384;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance385.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.CardAreaAppearance = appearance385;
			appearance386.BorderColor = System.Drawing.Color.Silver;
			appearance386.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.CellAppearance = appearance386;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.CellPadding = 0;
			appearance387.BackColor = System.Drawing.SystemColors.Control;
			appearance387.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance387.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance387.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance387.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.GroupByRowAppearance = appearance387;
			appearance388.TextHAlignAsString = "Left";
			comboBoxGridPurchaseAccount.DisplayLayout.Override.HeaderAppearance = appearance388;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance389.BackColor = System.Drawing.SystemColors.Window;
			appearance389.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.RowAppearance = appearance389;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance390.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridPurchaseAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance390;
			comboBoxGridPurchaseAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridPurchaseAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridPurchaseAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridPurchaseAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridPurchaseAccount.Editable = true;
			comboBoxGridPurchaseAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGridPurchaseAccount.FilterString = "";
			comboBoxGridPurchaseAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGridPurchaseAccount.FilterSysDocID = "";
			comboBoxGridPurchaseAccount.HasAllAccount = false;
			comboBoxGridPurchaseAccount.HasCustom = false;
			comboBoxGridPurchaseAccount.IsDataLoaded = false;
			comboBoxGridPurchaseAccount.Location = new System.Drawing.Point(271, 151);
			comboBoxGridPurchaseAccount.MaxDropDownItems = 12;
			comboBoxGridPurchaseAccount.Name = "comboBoxGridPurchaseAccount";
			comboBoxGridPurchaseAccount.ShowInactiveItems = false;
			comboBoxGridPurchaseAccount.ShowQuickAdd = true;
			comboBoxGridPurchaseAccount.Size = new System.Drawing.Size(100, 21);
			comboBoxGridPurchaseAccount.TabIndex = 8;
			comboBoxGridPurchaseAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridSalesAccount.Assigned = false;
			comboBoxGridSalesAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridSalesAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridSalesAccount.CustomReportFieldName = "";
			comboBoxGridSalesAccount.CustomReportKey = "";
			comboBoxGridSalesAccount.CustomReportValueType = 1;
			comboBoxGridSalesAccount.DescriptionTextBox = null;
			appearance391.BackColor = System.Drawing.SystemColors.Window;
			appearance391.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridSalesAccount.DisplayLayout.Appearance = appearance391;
			comboBoxGridSalesAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridSalesAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance392.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance392.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance392.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance392.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridSalesAccount.DisplayLayout.GroupByBox.Appearance = appearance392;
			appearance393.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridSalesAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance393;
			comboBoxGridSalesAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance394.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance394.BackColor2 = System.Drawing.SystemColors.Control;
			appearance394.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance394.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridSalesAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance394;
			comboBoxGridSalesAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridSalesAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance395.BackColor = System.Drawing.SystemColors.Window;
			appearance395.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridSalesAccount.DisplayLayout.Override.ActiveCellAppearance = appearance395;
			appearance396.BackColor = System.Drawing.SystemColors.Highlight;
			appearance396.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridSalesAccount.DisplayLayout.Override.ActiveRowAppearance = appearance396;
			comboBoxGridSalesAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridSalesAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance397.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridSalesAccount.DisplayLayout.Override.CardAreaAppearance = appearance397;
			appearance398.BorderColor = System.Drawing.Color.Silver;
			appearance398.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridSalesAccount.DisplayLayout.Override.CellAppearance = appearance398;
			comboBoxGridSalesAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridSalesAccount.DisplayLayout.Override.CellPadding = 0;
			appearance399.BackColor = System.Drawing.SystemColors.Control;
			appearance399.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance399.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance399.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance399.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridSalesAccount.DisplayLayout.Override.GroupByRowAppearance = appearance399;
			appearance400.TextHAlignAsString = "Left";
			comboBoxGridSalesAccount.DisplayLayout.Override.HeaderAppearance = appearance400;
			comboBoxGridSalesAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridSalesAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance401.BackColor = System.Drawing.SystemColors.Window;
			appearance401.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridSalesAccount.DisplayLayout.Override.RowAppearance = appearance401;
			comboBoxGridSalesAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance402.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridSalesAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance402;
			comboBoxGridSalesAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridSalesAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridSalesAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridSalesAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridSalesAccount.Editable = true;
			comboBoxGridSalesAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxGridSalesAccount.FilterString = "";
			comboBoxGridSalesAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxGridSalesAccount.FilterSysDocID = "";
			comboBoxGridSalesAccount.HasAllAccount = false;
			comboBoxGridSalesAccount.HasCustom = false;
			comboBoxGridSalesAccount.IsDataLoaded = false;
			comboBoxGridSalesAccount.Location = new System.Drawing.Point(479, 73);
			comboBoxGridSalesAccount.MaxDropDownItems = 12;
			comboBoxGridSalesAccount.Name = "comboBoxGridSalesAccount";
			comboBoxGridSalesAccount.ShowInactiveItems = false;
			comboBoxGridSalesAccount.ShowQuickAdd = true;
			comboBoxGridSalesAccount.Size = new System.Drawing.Size(100, 21);
			comboBoxGridSalesAccount.TabIndex = 7;
			comboBoxGridSalesAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelButtons.Controls.Add(comboBoxLocation);
			panelButtons.Controls.Add(radioButtonLocation);
			panelButtons.Controls.Add(buttonCopy);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 450);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(670, 40);
			panelButtons.TabIndex = 3;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance403.BackColor = System.Drawing.SystemColors.Window;
			appearance403.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance403;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance404.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance404.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance404.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance404.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance404;
			appearance405.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance405;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance406.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance406.BackColor2 = System.Drawing.SystemColors.Control;
			appearance406.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance406.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance406;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance407.BackColor = System.Drawing.SystemColors.Window;
			appearance407.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance407;
			appearance408.BackColor = System.Drawing.SystemColors.Highlight;
			appearance408.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance408;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance409.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance409;
			appearance410.BorderColor = System.Drawing.Color.Silver;
			appearance410.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance410;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance411.BackColor = System.Drawing.SystemColors.Control;
			appearance411.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance411.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance411.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance411.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance411;
			appearance412.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance412;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance413.BackColor = System.Drawing.SystemColors.Window;
			appearance413.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance413;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance414.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance414;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(141, 11);
			comboBoxLocation.MaxDropDownItems = 12;
			comboBoxLocation.Name = "comboBoxLocation";
			comboBoxLocation.ShowAll = false;
			comboBoxLocation.ShowConsignIn = false;
			comboBoxLocation.ShowConsignOut = false;
			comboBoxLocation.ShowDefaultLocationOnly = false;
			comboBoxLocation.ShowInactiveItems = false;
			comboBoxLocation.ShowNormalLocations = true;
			comboBoxLocation.ShowPOSOnly = false;
			comboBoxLocation.ShowQuickAdd = true;
			comboBoxLocation.ShowWarehouseOnly = false;
			comboBoxLocation.Size = new System.Drawing.Size(164, 21);
			comboBoxLocation.TabIndex = 148;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLocation.Visible = false;
			radioButtonLocation.AutoSize = true;
			radioButtonLocation.Location = new System.Drawing.Point(11, 12);
			radioButtonLocation.Name = "radioButtonLocation";
			radioButtonLocation.Size = new System.Drawing.Size(124, 17);
			radioButtonLocation.TabIndex = 16;
			radioButtonLocation.Text = "Copy From Location:";
			radioButtonLocation.UseVisualStyleBackColor = true;
			radioButtonLocation.Visible = false;
			radioButtonLocation.CheckedChanged += new System.EventHandler(radioButtonLocation_CheckedChanged);
			buttonCopy.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCopy.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonCopy.BackColor = System.Drawing.Color.Silver;
			buttonCopy.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCopy.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCopy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonCopy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCopy.Location = new System.Drawing.Point(311, 8);
			buttonCopy.Name = "buttonCopy";
			buttonCopy.Size = new System.Drawing.Size(96, 24);
			buttonCopy.TabIndex = 15;
			buttonCopy.Text = "Copy";
			buttonCopy.UseVisualStyleBackColor = false;
			buttonCopy.Visible = false;
			buttonCopy.Click += new System.EventHandler(buttonCopy_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(670, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(563, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(tabPageProject);
			ultraTabControl1.Controls.Add(tabPageManufacturing);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Location = new System.Drawing.Point(11, 57);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(647, 388);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.PropertyPage2003;
			ultraTabControl1.TabIndex = 2;
			appearance415.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance415;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = tabPageProject;
			ultraTab2.Text = "&Project";
			ultraTab3.TabPage = tabPageManufacturing;
			ultraTab3.Text = "&Manufacturing";
			ultraTab4.TabPage = ultraTabPageControl1;
			ultraTab4.Text = "Consignment";
			ultraTab5.TabPage = ultraTabPageControl2;
			ultraTab5.Text = "Tax";
			ultraTab5.Visible = false;
			ultraTab6.TabPage = ultraTabPageControl3;
			ultraTab6.Text = "HR";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[6]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(643, 365);
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
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(123, 30);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(309, 21);
			textBoxName.TabIndex = 1;
			textBoxName.TabStop = false;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(123, 8);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(139, 21);
			textBoxCode.TabIndex = 0;
			textBoxCode.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(8, 30);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(96, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Location Name:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(8, 8);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(93, 13);
			labelCode.TabIndex = 0;
			labelCode.Text = "Location Code:";
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel29);
			ultraTabPageControl3.Controls.Add(comboBoxTicketAccount);
			ultraTabPageControl3.Controls.Add(textBoxTicketAccountName);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel30);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel31);
			ultraTabPageControl3.Controls.Add(ultraFormattedLinkLabel32);
			ultraTabPageControl3.Controls.Add(comboBoxEOSBenefitAccount);
			ultraTabPageControl3.Controls.Add(textBoxLeaveExpenseAccountName);
			ultraTabPageControl3.Controls.Add(comboBoxProvisionAccount);
			ultraTabPageControl3.Controls.Add(textBoxProvisionAccountName);
			ultraTabPageControl3.Controls.Add(comboBoxLeaveExpenseAccount);
			ultraTabPageControl3.Controls.Add(textBoxEOSBenefitAccountName);
			ultraTabPageControl3.Location = new System.Drawing.Point(2, 21);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(643, 365);
			appearance416.FontData.BoldAsString = "False";
			appearance416.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel29.Appearance = appearance416;
			ultraFormattedLinkLabel29.AutoSize = true;
			ultraFormattedLinkLabel29.Location = new System.Drawing.Point(10, 82);
			ultraFormattedLinkLabel29.Name = "ultraFormattedLinkLabel29";
			ultraFormattedLinkLabel29.Size = new System.Drawing.Size(37, 15);
			ultraFormattedLinkLabel29.TabIndex = 182;
			ultraFormattedLinkLabel29.TabStop = true;
			ultraFormattedLinkLabel29.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel29.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel29.Value = "Ticket:";
			appearance417.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel29.VisitedLinkAppearance = appearance417;
			ultraFormattedLinkLabel29.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel29_LinkClicked);
			comboBoxTicketAccount.AlwaysInEditMode = true;
			comboBoxTicketAccount.Assigned = false;
			comboBoxTicketAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTicketAccount.CustomReportFieldName = "";
			comboBoxTicketAccount.CustomReportKey = "";
			comboBoxTicketAccount.CustomReportValueType = 1;
			comboBoxTicketAccount.DescriptionTextBox = textBoxTicketAccountName;
			appearance418.BackColor = System.Drawing.SystemColors.Window;
			appearance418.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTicketAccount.DisplayLayout.Appearance = appearance418;
			comboBoxTicketAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTicketAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance419.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance419.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance419.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance419.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTicketAccount.DisplayLayout.GroupByBox.Appearance = appearance419;
			appearance420.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTicketAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance420;
			comboBoxTicketAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance421.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance421.BackColor2 = System.Drawing.SystemColors.Control;
			appearance421.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance421.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTicketAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance421;
			comboBoxTicketAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTicketAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance422.BackColor = System.Drawing.SystemColors.Window;
			appearance422.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTicketAccount.DisplayLayout.Override.ActiveCellAppearance = appearance422;
			appearance423.BackColor = System.Drawing.SystemColors.Highlight;
			appearance423.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTicketAccount.DisplayLayout.Override.ActiveRowAppearance = appearance423;
			comboBoxTicketAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTicketAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance424.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTicketAccount.DisplayLayout.Override.CardAreaAppearance = appearance424;
			appearance425.BorderColor = System.Drawing.Color.Silver;
			appearance425.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTicketAccount.DisplayLayout.Override.CellAppearance = appearance425;
			comboBoxTicketAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTicketAccount.DisplayLayout.Override.CellPadding = 0;
			appearance426.BackColor = System.Drawing.SystemColors.Control;
			appearance426.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance426.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance426.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance426.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTicketAccount.DisplayLayout.Override.GroupByRowAppearance = appearance426;
			appearance427.TextHAlignAsString = "Left";
			comboBoxTicketAccount.DisplayLayout.Override.HeaderAppearance = appearance427;
			comboBoxTicketAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTicketAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance428.BackColor = System.Drawing.SystemColors.Window;
			appearance428.BorderColor = System.Drawing.Color.Silver;
			comboBoxTicketAccount.DisplayLayout.Override.RowAppearance = appearance428;
			comboBoxTicketAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance429.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTicketAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance429;
			comboBoxTicketAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTicketAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTicketAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTicketAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTicketAccount.Editable = true;
			comboBoxTicketAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxTicketAccount.FilterString = "";
			comboBoxTicketAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxTicketAccount.FilterSysDocID = "";
			comboBoxTicketAccount.HasAllAccount = false;
			comboBoxTicketAccount.HasCustom = false;
			comboBoxTicketAccount.IsDataLoaded = false;
			comboBoxTicketAccount.Location = new System.Drawing.Point(133, 80);
			comboBoxTicketAccount.MaxDropDownItems = 12;
			comboBoxTicketAccount.Name = "comboBoxTicketAccount";
			comboBoxTicketAccount.ShowInactiveItems = false;
			comboBoxTicketAccount.ShowQuickAdd = true;
			comboBoxTicketAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxTicketAccount.TabIndex = 178;
			comboBoxTicketAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxTicketAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTicketAccountName.CustomReportFieldName = "";
			textBoxTicketAccountName.CustomReportKey = "";
			textBoxTicketAccountName.CustomReportValueType = 1;
			textBoxTicketAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxTicketAccountName.IsComboTextBox = false;
			textBoxTicketAccountName.IsModified = false;
			textBoxTicketAccountName.Location = new System.Drawing.Point(252, 80);
			textBoxTicketAccountName.MaxLength = 255;
			textBoxTicketAccountName.Name = "textBoxTicketAccountName";
			textBoxTicketAccountName.ReadOnly = true;
			textBoxTicketAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxTicketAccountName.TabIndex = 179;
			textBoxTicketAccountName.TabStop = false;
			appearance430.FontData.BoldAsString = "False";
			appearance430.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel30.Appearance = appearance430;
			ultraFormattedLinkLabel30.AutoSize = true;
			ultraFormattedLinkLabel30.Location = new System.Drawing.Point(10, 15);
			ultraFormattedLinkLabel30.Name = "ultraFormattedLinkLabel30";
			ultraFormattedLinkLabel30.Size = new System.Drawing.Size(80, 15);
			ultraFormattedLinkLabel30.TabIndex = 171;
			ultraFormattedLinkLabel30.TabStop = true;
			ultraFormattedLinkLabel30.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel30.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel30.Value = "Leave Expense:";
			appearance431.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel30.VisitedLinkAppearance = appearance431;
			ultraFormattedLinkLabel30.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel30_LinkClicked);
			appearance432.FontData.BoldAsString = "False";
			appearance432.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel31.Appearance = appearance432;
			ultraFormattedLinkLabel31.AutoSize = true;
			ultraFormattedLinkLabel31.Location = new System.Drawing.Point(10, 37);
			ultraFormattedLinkLabel31.Name = "ultraFormattedLinkLabel31";
			ultraFormattedLinkLabel31.Size = new System.Drawing.Size(52, 15);
			ultraFormattedLinkLabel31.TabIndex = 180;
			ultraFormattedLinkLabel31.TabStop = true;
			ultraFormattedLinkLabel31.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel31.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel31.Value = "Provision:";
			appearance433.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel31.VisitedLinkAppearance = appearance433;
			ultraFormattedLinkLabel31.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel31_LinkClicked);
			appearance434.FontData.BoldAsString = "False";
			appearance434.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel32.Appearance = appearance434;
			ultraFormattedLinkLabel32.AutoSize = true;
			ultraFormattedLinkLabel32.Location = new System.Drawing.Point(10, 59);
			ultraFormattedLinkLabel32.Name = "ultraFormattedLinkLabel32";
			ultraFormattedLinkLabel32.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel32.TabIndex = 181;
			ultraFormattedLinkLabel32.TabStop = true;
			ultraFormattedLinkLabel32.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel32.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel32.Value = "EOS Benefit:";
			appearance435.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel32.VisitedLinkAppearance = appearance435;
			ultraFormattedLinkLabel32.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel32_LinkClicked);
			comboBoxEOSBenefitAccount.AlwaysInEditMode = true;
			comboBoxEOSBenefitAccount.Assigned = false;
			comboBoxEOSBenefitAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxEOSBenefitAccount.CustomReportFieldName = "";
			comboBoxEOSBenefitAccount.CustomReportKey = "";
			comboBoxEOSBenefitAccount.CustomReportValueType = 1;
			comboBoxEOSBenefitAccount.DescriptionTextBox = textBoxEOSBenefitAccountName;
			appearance436.BackColor = System.Drawing.SystemColors.Window;
			appearance436.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxEOSBenefitAccount.DisplayLayout.Appearance = appearance436;
			comboBoxEOSBenefitAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxEOSBenefitAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance437.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance437.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance437.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance437.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEOSBenefitAccount.DisplayLayout.GroupByBox.Appearance = appearance437;
			appearance438.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEOSBenefitAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance438;
			comboBoxEOSBenefitAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance439.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance439.BackColor2 = System.Drawing.SystemColors.Control;
			appearance439.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance439.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxEOSBenefitAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance439;
			comboBoxEOSBenefitAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxEOSBenefitAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance440.BackColor = System.Drawing.SystemColors.Window;
			appearance440.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.ActiveCellAppearance = appearance440;
			appearance441.BackColor = System.Drawing.SystemColors.Highlight;
			appearance441.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.ActiveRowAppearance = appearance441;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance442.BackColor = System.Drawing.SystemColors.Window;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.CardAreaAppearance = appearance442;
			appearance443.BorderColor = System.Drawing.Color.Silver;
			appearance443.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.CellAppearance = appearance443;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.CellPadding = 0;
			appearance444.BackColor = System.Drawing.SystemColors.Control;
			appearance444.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance444.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance444.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance444.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.GroupByRowAppearance = appearance444;
			appearance445.TextHAlignAsString = "Left";
			comboBoxEOSBenefitAccount.DisplayLayout.Override.HeaderAppearance = appearance445;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance446.BackColor = System.Drawing.SystemColors.Window;
			appearance446.BorderColor = System.Drawing.Color.Silver;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.RowAppearance = appearance446;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance447.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxEOSBenefitAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance447;
			comboBoxEOSBenefitAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxEOSBenefitAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxEOSBenefitAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxEOSBenefitAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxEOSBenefitAccount.Editable = true;
			comboBoxEOSBenefitAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxEOSBenefitAccount.FilterString = "";
			comboBoxEOSBenefitAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxEOSBenefitAccount.FilterSysDocID = "";
			comboBoxEOSBenefitAccount.HasAllAccount = false;
			comboBoxEOSBenefitAccount.HasCustom = false;
			comboBoxEOSBenefitAccount.IsDataLoaded = false;
			comboBoxEOSBenefitAccount.Location = new System.Drawing.Point(133, 57);
			comboBoxEOSBenefitAccount.MaxDropDownItems = 12;
			comboBoxEOSBenefitAccount.Name = "comboBoxEOSBenefitAccount";
			comboBoxEOSBenefitAccount.ShowInactiveItems = false;
			comboBoxEOSBenefitAccount.ShowQuickAdd = true;
			comboBoxEOSBenefitAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxEOSBenefitAccount.TabIndex = 176;
			comboBoxEOSBenefitAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxEOSBenefitAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEOSBenefitAccountName.CustomReportFieldName = "";
			textBoxEOSBenefitAccountName.CustomReportKey = "";
			textBoxEOSBenefitAccountName.CustomReportValueType = 1;
			textBoxEOSBenefitAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxEOSBenefitAccountName.IsComboTextBox = false;
			textBoxEOSBenefitAccountName.IsModified = false;
			textBoxEOSBenefitAccountName.Location = new System.Drawing.Point(252, 57);
			textBoxEOSBenefitAccountName.MaxLength = 255;
			textBoxEOSBenefitAccountName.Name = "textBoxEOSBenefitAccountName";
			textBoxEOSBenefitAccountName.ReadOnly = true;
			textBoxEOSBenefitAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxEOSBenefitAccountName.TabIndex = 177;
			textBoxEOSBenefitAccountName.TabStop = false;
			textBoxLeaveExpenseAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLeaveExpenseAccountName.CustomReportFieldName = "";
			textBoxLeaveExpenseAccountName.CustomReportKey = "";
			textBoxLeaveExpenseAccountName.CustomReportValueType = 1;
			textBoxLeaveExpenseAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxLeaveExpenseAccountName.IsComboTextBox = false;
			textBoxLeaveExpenseAccountName.IsModified = false;
			textBoxLeaveExpenseAccountName.Location = new System.Drawing.Point(252, 13);
			textBoxLeaveExpenseAccountName.MaxLength = 255;
			textBoxLeaveExpenseAccountName.Name = "textBoxLeaveExpenseAccountName";
			textBoxLeaveExpenseAccountName.ReadOnly = true;
			textBoxLeaveExpenseAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxLeaveExpenseAccountName.TabIndex = 173;
			textBoxLeaveExpenseAccountName.TabStop = false;
			comboBoxProvisionAccount.AlwaysInEditMode = true;
			comboBoxProvisionAccount.Assigned = false;
			comboBoxProvisionAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxProvisionAccount.CustomReportFieldName = "";
			comboBoxProvisionAccount.CustomReportKey = "";
			comboBoxProvisionAccount.CustomReportValueType = 1;
			comboBoxProvisionAccount.DescriptionTextBox = textBoxProvisionAccountName;
			appearance448.BackColor = System.Drawing.SystemColors.Window;
			appearance448.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxProvisionAccount.DisplayLayout.Appearance = appearance448;
			comboBoxProvisionAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxProvisionAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance449.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance449.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance449.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance449.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionAccount.DisplayLayout.GroupByBox.Appearance = appearance449;
			appearance450.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProvisionAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance450;
			comboBoxProvisionAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance451.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance451.BackColor2 = System.Drawing.SystemColors.Control;
			appearance451.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance451.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxProvisionAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance451;
			comboBoxProvisionAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxProvisionAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance452.BackColor = System.Drawing.SystemColors.Window;
			appearance452.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxProvisionAccount.DisplayLayout.Override.ActiveCellAppearance = appearance452;
			appearance453.BackColor = System.Drawing.SystemColors.Highlight;
			appearance453.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxProvisionAccount.DisplayLayout.Override.ActiveRowAppearance = appearance453;
			comboBoxProvisionAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxProvisionAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance454.BackColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionAccount.DisplayLayout.Override.CardAreaAppearance = appearance454;
			appearance455.BorderColor = System.Drawing.Color.Silver;
			appearance455.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxProvisionAccount.DisplayLayout.Override.CellAppearance = appearance455;
			comboBoxProvisionAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxProvisionAccount.DisplayLayout.Override.CellPadding = 0;
			appearance456.BackColor = System.Drawing.SystemColors.Control;
			appearance456.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance456.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance456.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance456.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxProvisionAccount.DisplayLayout.Override.GroupByRowAppearance = appearance456;
			appearance457.TextHAlignAsString = "Left";
			comboBoxProvisionAccount.DisplayLayout.Override.HeaderAppearance = appearance457;
			comboBoxProvisionAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxProvisionAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance458.BackColor = System.Drawing.SystemColors.Window;
			appearance458.BorderColor = System.Drawing.Color.Silver;
			comboBoxProvisionAccount.DisplayLayout.Override.RowAppearance = appearance458;
			comboBoxProvisionAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance459.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxProvisionAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance459;
			comboBoxProvisionAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxProvisionAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxProvisionAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxProvisionAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxProvisionAccount.Editable = true;
			comboBoxProvisionAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxProvisionAccount.FilterString = "";
			comboBoxProvisionAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxProvisionAccount.FilterSysDocID = "";
			comboBoxProvisionAccount.HasAllAccount = false;
			comboBoxProvisionAccount.HasCustom = false;
			comboBoxProvisionAccount.IsDataLoaded = false;
			comboBoxProvisionAccount.Location = new System.Drawing.Point(133, 35);
			comboBoxProvisionAccount.MaxDropDownItems = 12;
			comboBoxProvisionAccount.Name = "comboBoxProvisionAccount";
			comboBoxProvisionAccount.ShowInactiveItems = false;
			comboBoxProvisionAccount.ShowQuickAdd = true;
			comboBoxProvisionAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxProvisionAccount.TabIndex = 174;
			comboBoxProvisionAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxProvisionAccountName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxProvisionAccountName.CustomReportFieldName = "";
			textBoxProvisionAccountName.CustomReportKey = "";
			textBoxProvisionAccountName.CustomReportValueType = 1;
			textBoxProvisionAccountName.ForeColor = System.Drawing.Color.Black;
			textBoxProvisionAccountName.IsComboTextBox = false;
			textBoxProvisionAccountName.IsModified = false;
			textBoxProvisionAccountName.Location = new System.Drawing.Point(252, 35);
			textBoxProvisionAccountName.MaxLength = 255;
			textBoxProvisionAccountName.Name = "textBoxProvisionAccountName";
			textBoxProvisionAccountName.ReadOnly = true;
			textBoxProvisionAccountName.Size = new System.Drawing.Size(303, 21);
			textBoxProvisionAccountName.TabIndex = 175;
			textBoxProvisionAccountName.TabStop = false;
			comboBoxLeaveExpenseAccount.AlwaysInEditMode = true;
			comboBoxLeaveExpenseAccount.Assigned = false;
			comboBoxLeaveExpenseAccount.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeaveExpenseAccount.CustomReportFieldName = "";
			comboBoxLeaveExpenseAccount.CustomReportKey = "";
			comboBoxLeaveExpenseAccount.CustomReportValueType = 1;
			comboBoxLeaveExpenseAccount.DescriptionTextBox = textBoxLeaveExpenseAccountName;
			appearance460.BackColor = System.Drawing.SystemColors.Window;
			appearance460.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLeaveExpenseAccount.DisplayLayout.Appearance = appearance460;
			comboBoxLeaveExpenseAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLeaveExpenseAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance461.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance461.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance461.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance461.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveExpenseAccount.DisplayLayout.GroupByBox.Appearance = appearance461;
			appearance462.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveExpenseAccount.DisplayLayout.GroupByBox.BandLabelAppearance = appearance462;
			comboBoxLeaveExpenseAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance463.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance463.BackColor2 = System.Drawing.SystemColors.Control;
			appearance463.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance463.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLeaveExpenseAccount.DisplayLayout.GroupByBox.PromptAppearance = appearance463;
			comboBoxLeaveExpenseAccount.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLeaveExpenseAccount.DisplayLayout.MaxRowScrollRegions = 1;
			appearance464.BackColor = System.Drawing.SystemColors.Window;
			appearance464.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.ActiveCellAppearance = appearance464;
			appearance465.BackColor = System.Drawing.SystemColors.Highlight;
			appearance465.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.ActiveRowAppearance = appearance465;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance466.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.CardAreaAppearance = appearance466;
			appearance467.BorderColor = System.Drawing.Color.Silver;
			appearance467.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.CellAppearance = appearance467;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.CellPadding = 0;
			appearance468.BackColor = System.Drawing.SystemColors.Control;
			appearance468.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance468.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance468.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance468.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.GroupByRowAppearance = appearance468;
			appearance469.TextHAlignAsString = "Left";
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.HeaderAppearance = appearance469;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance470.BackColor = System.Drawing.SystemColors.Window;
			appearance470.BorderColor = System.Drawing.Color.Silver;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.RowAppearance = appearance470;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance471.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLeaveExpenseAccount.DisplayLayout.Override.TemplateAddRowAppearance = appearance471;
			comboBoxLeaveExpenseAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLeaveExpenseAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLeaveExpenseAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLeaveExpenseAccount.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeaveExpenseAccount.Editable = true;
			comboBoxLeaveExpenseAccount.FilterAccountType = Micromind.Common.Data.AccountTypes.None;
			comboBoxLeaveExpenseAccount.FilterString = "";
			comboBoxLeaveExpenseAccount.FilterSubType = Micromind.Common.Data.AccountSubTypes.None;
			comboBoxLeaveExpenseAccount.FilterSysDocID = "";
			comboBoxLeaveExpenseAccount.HasAllAccount = false;
			comboBoxLeaveExpenseAccount.HasCustom = false;
			comboBoxLeaveExpenseAccount.IsDataLoaded = false;
			comboBoxLeaveExpenseAccount.Location = new System.Drawing.Point(133, 13);
			comboBoxLeaveExpenseAccount.MaxDropDownItems = 12;
			comboBoxLeaveExpenseAccount.Name = "comboBoxLeaveExpenseAccount";
			comboBoxLeaveExpenseAccount.ShowInactiveItems = false;
			comboBoxLeaveExpenseAccount.ShowQuickAdd = true;
			comboBoxLeaveExpenseAccount.Size = new System.Drawing.Size(117, 21);
			comboBoxLeaveExpenseAccount.TabIndex = 172;
			comboBoxLeaveExpenseAccount.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(670, 490);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(620, 160);
			base.Name = "LocationAccountsForm";
			Text = "Location Accounts";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPrepaymentPaybleAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxPurchasePrePaymentAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRoundOffAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAllocationDiscount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryOnDNoteAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEmployeeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountReceived).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGainLossAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDiscountGiven).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesTax).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCOGSAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxAPAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxInventoryAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxARAccount).EndInit();
			tabPageProject.ResumeLayout(false);
			tabPageProject.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxAdvanceAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxRetentionAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectTimesheetContra).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectWIPAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectIncomeAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProjectCostAccount).EndInit();
			tabPageManufacturing.ResumeLayout(false);
			tabPageManufacturing.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxManuTimesheetContra).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxManuWIPAccount).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutCOGS).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignOutSales).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInDiff).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInCommissionAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxConsignInAccount).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataEntryGridTax).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPurchaseAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridSalesAccount).EndInit();
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxTicketAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxEOSBenefitAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxProvisionAccount).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeaveExpenseAccount).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
