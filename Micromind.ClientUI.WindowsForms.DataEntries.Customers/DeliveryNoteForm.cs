using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Accounts;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.DataControls.OtherControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class DeliveryNoteForm : Form, IForm
	{
		public class translist
		{
			private string itemcode = "";

			private string itemdescription = "";

			private decimal trqty;

			private bool istaken;

			private int trindex;

			public string ItemCode
			{
				get
				{
					return itemcode;
				}
				set
				{
					itemcode = value;
				}
			}

			public string ItemDescription
			{
				get
				{
					return itemdescription;
				}
				set
				{
					itemdescription = value;
				}
			}

			public decimal Trqty
			{
				get
				{
					return trqty;
				}
				set
				{
					trqty = value;
				}
			}

			public bool Istaken
			{
				get
				{
					return istaken;
				}
				set
				{
					istaken = value;
				}
			}

			public int Trindex
			{
				get
				{
					return trindex;
				}
				set
				{
					trindex = value;
				}
			}

			public translist(string Dtstart, decimal Trqty, bool Istaken, string ItemDescription, int Trindex)
			{
				itemcode = Dtstart;
				trqty = Trqty;
				istaken = Istaken;
				itemdescription = ItemDescription;
				trindex = Trindex;
			}
		}

		private bool allowEdit = true;

		private ItemSourceTypes sourceDocType;

		private bool allowChangeCustomer;

		private string clUserID = "";

		private bool isitemfilter;

		private bool IsPriceList;

		private DeliveryNoteData currentData;

		private const string TABLENAME_CONST = "Delivery_Note";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private DataTable invoiceDNoteTable;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private DataSet priceListData;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool showLotDetail = CompanyPreferences.ShowLotdetailinPrintout;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool AllowCustomerChangeInDN = CompanyPreferences.AllowCustomerChangeInDN;

		private bool ExclueZeroQtyInDN = CompanyPreferences.ExcludeZeroQtyInDN;

		private bool ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool allowMultiTemplate;

		private bool IsBinSelected;

		private string currencyID = Global.BaseCurrencyID;

		private decimal priceDiscountPercAllowed;

		private decimal TotalDiscountPercAllowed;

		private bool isLoadZeroQty;

		private bool AllowLSQtyMoreThanSO = CompanyPreferences.AllowLSQtyMoreThanSO;

		private bool showLoadDraft = CompanyPreferences.EnableDocTempSaving;

		private string binID = "";

		private bool isChecked;

		private int tempAutoKeyID;

		private int slNo = 1;

		private string strLocation = "";

		private bool supressInventoryMessage;

		private bool isDataLoading;

		private bool restrictTransaction;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isDiscountPercent;

		private bool showCreditValidation = true;

		private bool restrictonDataload;

		private DataTable dtTransaction = new DataTable();

		private DataTable dtPO = new DataTable();

		private decimal creditAmount;

		private decimal pdcBalance;

		private decimal pdcUnsecuredLimitAmount;

		private decimal tempCL;

		private byte creditLimitType;

		private bool limitPDCUnsecured;

		private bool IsTempRecordexists;

		private bool IsDocExists;

		private bool IsTemporaryTransaction;

		public string currentkey = "";

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

		private DateTimePicker dateTimePickerDate;

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

		private Label labelVoided;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private SysDocComboBox comboBoxSysDoc;

		private ProductComboBox comboBoxGridItem;

		private customersFlatComboBox comboBoxCustomer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxCustomerName;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripMenuItem selectInvoiceToolStripMenuItem;

		private ToolStripMenuItem createFromSalesOrderToolStripMenuItem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private SalespersonComboBox comboBoxSalesperson;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private Label label2;

		private TextBox textBoxPONumber;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxOrders;

		private XPButton buttonSelectDocument;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private UltraFormattedLinkLabel linkLabelVehicle;

		private VehicleComboBox comboBoxVehicle;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonDistribution;

		private Label label4;

		private TextBox textBoxRef2;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonBalance;

		private UltraCalcManager ultraCalcManager1;

		private DocStatusLabel docStatusLabel;

		private ToolStripMenuItem createFromSalesQuoteToolStripMenuItem;

		private CostCategoryComboBox comboBoxCostCategory;

		private JobComboBox comboBoxJob;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		private JobComboBox ComboBoxitemJob;

		private MMTextBox textBoxShipto;

		private CustomerAddressComboBox comboBoxBillingAddress;

		private CustomerAddressComboBox comboBoxShippingAddressID;

		private MMTextBox textBoxBilltoAddress;

		private UltraFormattedLinkLabel labelcostcategory;

		private UltraFormattedLinkLabel labelJob;

		private UltraFormattedLinkLabel ultraFormattedLinkShipping;

		private TextBox textBoxVechicleName;

		private Label label5;

		private UltraToolTipManager ultraToolTipManager1;

		private ToolStripButton toolStripButtonInfo;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripMenuItem createFromTransactionListToolStripMenuItem;

		private ProductStyleComboBox comboBoxStyle;

		private ProductSpecificationComboBox comboBoxSpecification;

		private ToolStripButton toolStripButtonMultiPreview;

		private ToolStripSeparator toolStripSeparator8;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripSeparator toolStripSeparator9;

		private Label label6;

		private DriverComboBox comboBoxDriver;

		private UltraFormattedLinkLabel linkLabelDriver;

		private MMSDateTimePicker dateTimePickerPO;

		private Panel panelxml;

		private SimpleButton simpleButtonSave;

		private SimpleButton simpleButtonFind;

		private SimpleButton simpleButtonLast;

		private SimpleButton simpleButtonNext;

		private SimpleButton simpleButtonPrevious;

		private SimpleButton simpleButtonFirst;

		private ToolStripMenuItem grantEditPermissionToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem deleteTempTransactionToolStripMenuItem;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private ToolStripMenuItem createfromBintoolStripMenuItem;

		private ToolStripSeparator toolStripSeparator10;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripMenuItem toolStripMenuRelationshipMap;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2007;

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
					customersFlatComboBox customersFlatComboBox = comboBoxCustomer;
					SysDocComboBox sysDocComboBox = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = true;
					enabled = (sysDocComboBox.Enabled = flag3);
					customersFlatComboBox.Enabled = enabled;
					dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].Hidden = true;
					comboBoxShippingAddressID.Filter("");
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					customersFlatComboBox customersFlatComboBox2 = comboBoxCustomer;
					SysDocComboBox sysDocComboBox2 = comboBoxSysDoc;
					bool flag3 = textBoxVoucherNumber.Enabled = false;
					enabled = (sysDocComboBox2.Enabled = flag3);
					customersFlatComboBox2.Enabled = enabled;
					if (allowChangeCustomer && sourceDocType == ItemSourceTypes.None)
					{
						comboBoxCustomer.Enabled = true;
					}
					dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].Hidden = false;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonInformation.Enabled = !value;
				toolStripButtonMultiPreview.Visible = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
					buttonVoid.Enabled = false;
					grantEditPermissionToolStripMenuItem.Visible = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (IsNewRecord)
				{
					grantEditPermissionToolStripMenuItem.Visible = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
				SetApprovalStatus();
				if (Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint))
				{
					ToolStripButton toolStripButton2 = toolStripButtonPrint;
					ToolStripButton toolStripButton3 = toolStripButtonPreview;
					bool flag3 = toolStripButtonMultiPreview.Enabled = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.BlockPrint);
					enabled = (toolStripButton3.Enabled = flag3);
					toolStripButton2.Enabled = enabled;
				}
				else
				{
					ToolStripButton toolStripButton4 = toolStripButtonPrint;
					ToolStripButton toolStripButton5 = toolStripButtonPreview;
					bool flag3 = toolStripButtonMultiPreview.Enabled = true;
					enabled = (toolStripButton5.Enabled = flag3);
					toolStripButton4.Enabled = enabled;
				}
				if (Global.isUserAdmin)
				{
					ToolStripButton toolStripButton6 = toolStripButtonPrint;
					ToolStripButton toolStripButton7 = toolStripButtonPreview;
					bool flag3 = toolStripButtonMultiPreview.Enabled = true;
					enabled = (toolStripButton7.Enabled = flag3);
					toolStripButton6.Enabled = enabled;
				}
			}
		}

		private bool RestrictTransaction
		{
			get
			{
				return restrictTransaction;
			}
			set
			{
				restrictTransaction = value;
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				buttonSave.Enabled = !value;
				textBoxNote.Enabled = !value;
				labelVoided.Visible = value;
				if (value)
				{
					buttonVoid.Enabled = false;
					return;
				}
				buttonVoid.Text = UIMessages.Void;
				if (!IsNewRecord)
				{
					buttonVoid.Enabled = true;
				}
				else
				{
					buttonVoid.Enabled = false;
				}
			}
		}

		public DataTable dttran
		{
			get
			{
				return dtTransaction;
			}
			set
			{
				dtTransaction = value;
			}
		}

		public string CurrentKey
		{
			get
			{
				return currentkey;
			}
			set
			{
				currentkey = value;
			}
		}

		public string SelectedBin
		{
			get
			{
				return binID;
			}
			set
			{
				binID = value;
			}
		}

		public int TempAutoKeyID
		{
			get
			{
				return tempAutoKeyID;
			}
			set
			{
				tempAutoKeyID = value;
			}
		}

		public DataTable dtpo
		{
			get
			{
				return dtPO;
			}
			set
			{
				dtPO = value;
			}
		}

		public DeliveryNoteForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxGridLocation.ShowWarehouseOnly = true;
			panelxml.Visible = showLoadDraft;
			labelJob.Visible = (labelcostcategory.Visible = (comboBoxJob.Visible = (comboBoxCostCategory.Visible = useJobCosting)));
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxCustomer.SelectedIndexChanged += comboBoxCustomer_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			comboBoxBillingAddress.SelectedIndexChanged += comboBoxBillingAddress_SelectedIndexChanged;
			comboBoxShippingAddressID.SelectedIndexChanged += comboBoxShippingAddressID_SelectedIndexChanged;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			docStatusLabel.LinkClicked += docStatusLabel_LinkClicked;
			dataGridItems.KeyPress += dataGridItems_KeyDown;
		}

		private void comboBoxShippingAddressID_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxShippingAddressID.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxShipto.Clear();
				}
				else
				{
					textBoxShipto.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxBillingAddress_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				string selectedID = comboBoxBillingAddress.SelectedID;
				string selectedID2 = comboBoxCustomer.SelectedID;
				if (selectedID == "" || selectedID2 == "")
				{
					textBoxBilltoAddress.Clear();
				}
				else
				{
					textBoxBilltoAddress.Text = Factory.CustomerSystem.GetCustomerAddressPrintFormat(selectedID2, selectedID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void docStatusLabel_LinkClicked(object sender, EventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			Control control = sender as Control;
			if (control != null)
			{
				formHelper.EditTransaction(control.Tag.ToString(), control.Text);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridItems_BeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result);
				if (result < 0m)
				{
					ErrorHelper.InformationMessage("Negative quantity is not allowed.", "Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveCell.Text.ToString(), out result2);
				if (result2 < 0m)
				{
					ErrorHelper.InformationMessage("Negative amount is not allowed. Please enter a number greater than or equal to zero.");
					e.Cancel = true;
				}
			}
		}

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow == null || activeRow.Cells["Item Code"].Value == null || activeRow.Cells["Item Code"].Value.ToString() == "")
			{
				return;
			}
			if (activeRow != null && activeRow.DataChanged && Factory.ProductSystem.IsHoldSaleonProduct(activeRow.Cells["Item Code"].Value.ToString()))
			{
				ErrorHelper.WarningMessage("Sale Hold for product-" + activeRow.Cells["Item Code"].Value.ToString() + ".");
				activeRow.CancelUpdate();
				e.Cancel = true;
				return;
			}
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			if (activeRow != null && activeRow.DataChanged)
			{
				decimal.TryParse(activeRow.Cells["Price"].Value.ToString(), out result2);
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				string text = "MinPrice";
				text = (Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice) ? "MinPrice" : "UnitPrice3");
				decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString()).ToString(), out result);
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", comboBoxSysDoc.LocationID);
				if (fieldValue == null)
				{
					fieldValue = Factory.DatabaseSystem.GetFieldValue("Product_PriceList_Detail", text, "ProductID", activeRow.Cells["Item Code"].Value.ToString(), "UnitID", activeRow.Cells["Unit"].Value.ToString(), "LocationID", "");
				}
				if (fieldValue != null)
				{
					decimal.TryParse(fieldValue.ToString(), out result);
				}
				string text2 = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
				string text3 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
				if (text3 != "" && text2 != "")
				{
					DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text3, text2, comboBoxSysDoc.LocationID);
					if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
					{
						DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
						string a = dataRow["FactorType"].ToString();
						decimal num3 = decimal.Parse(dataRow["Factor"].ToString());
						if (a == "M")
						{
							result /= num3;
						}
						else
						{
							result *= num3;
						}
					}
				}
				num = result * (priceDiscountPercAllowed / 100m);
				num2 = result - num;
				if (result2 < num2)
				{
					_ = (priceDiscountPercAllowed > 0m);
				}
				if (!Global.IsUserAdmin)
				{
					_ = (priceDiscountPercAllowed == 0m);
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowPriceDiscount))
				{
					_ = (result2 < num2);
				}
				_ = (result2 == 0m);
			}
			if (CompanyPreferences.LocalSalesFlow != SalesFlows.SOThenDNThenInvoice && activeRow != null && activeRow.DataChanged)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (activeRow.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString()));
				}
				if (CompanyPreferences.NegativeQuantityAction != 1 || itemTypes == ItemTypes.ConsignmentItem)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					if (!Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Delivery_Note_Detail", sysDocID, voucherID, decimal.Parse(activeRow.Cells["Quantity"].Value.ToString())) && !supressInventoryMessage)
					{
						if (itemTypes == ItemTypes.ConsignmentItem)
						{
							ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
							e.Cancel = true;
							return;
						}
						if (CompanyPreferences.NegativeQuantityAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
							{
								e.Row.Cells["Quantity"].Value = 0;
								e.Row.Cells["Amount"].Value = 0;
								e.Row.Update();
								if (!e.Row.IsAddRow)
								{
									e.Cancel = true;
								}
								return;
							}
						}
						else if (CompanyPreferences.NegativeQuantityAction == 3)
						{
							ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
							e.Row.Cells["Quantity"].Value = 0;
							e.Row.Cells["Amount"].Value = 0;
							e.Row.Update();
							if (!e.Row.IsAddRow)
							{
								e.Cancel = true;
							}
							return;
						}
					}
				}
			}
			if (activeRow == null || !activeRow.DataChanged || activeRow.Cells["Price"].Value == null || !(activeRow.Cells["Price"].Value.ToString() != "") || CompanyPreferences.PricelessCostAction == 1 || !Factory.SalesInvoiceSystem.IsBelowAverageCost(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), Global.BaseCurrencyID, 1m, decimal.Parse(activeRow.Cells["Price"].Value.ToString())) || !(result2 != 0m))
			{
				return;
			}
			if (CompanyPreferences.PricelessCostAction == 2)
			{
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. Do you want to continue?") != DialogResult.Yes && !e.Row.IsAddRow)
				{
					e.Cancel = true;
				}
			}
			else if (CompanyPreferences.PricelessCostAction == 3)
			{
				ErrorHelper.WarningMessage("The price you have entered is below the cost. You are not allowed to sell at this price.");
				if (!e.Row.IsAddRow)
				{
					e.Cancel = true;
				}
			}
			else
			{
				if (CompanyPreferences.PricelessCostAction != 4)
				{
					return;
				}
				if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "The price you have entered is below the cost. You need to enter password to continue. \nDo you want to continue?") == DialogResult.Yes)
				{
					if (new ApprovalPasswordForm
					{
						CorrectPassword = CompanyPreferences.PricelessCostPassword
					}.ShowDialog() != DialogResult.OK && !e.Row.IsAddRow)
					{
						e.Cancel = true;
					}
					return;
				}
				if (!e.Row.IsAddRow)
				{
					e.Cancel = true;
				}
				if (e.Row.IsAddRow)
				{
					activeRow.Cells["price"].Value = 0;
					activeRow.Cells["Amount"].Value = 0;
				}
			}
		}

		private void dataGridItems_KeyDown(object sender, KeyPressEventArgs e)
		{
			if (dataGridItems.ActiveCell == null)
			{
				return;
			}
			UltraGridColumn column = dataGridItems.ActiveCell.Column;
			if (dataGridItems.ActiveRow == null || column == null || !(column.Key == "Price") || !char.IsLetter(e.KeyChar))
			{
				return;
			}
			ProductPriceListForm productPriceListForm = new ProductPriceListForm();
			productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
			if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
			{
				e.Handled = true;
				string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
				string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				if (text2 != "" && text != "")
				{
					DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
					if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
					{
						DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
						string a = dataRow["FactorType"].ToString();
						decimal num = decimal.Parse(dataRow["Factor"].ToString());
						decimal num2 = default(decimal);
						num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						if (a == "M")
						{
							num2 /= num;
						}
						else
						{
							num2 *= num;
						}
						dataGridItems.ActiveRow.Cells["Price"].Value = num2;
					}
					else
					{
						dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, Global.CurDecimalPoints);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
			else
			{
				e.Handled = true;
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (e.KeyCode == Keys.G && e.Alt)
			{
				dataGridItems.Focus();
				comboBoxGridItem.Focus();
			}
			if (LoadItemFeatures && e.KeyCode == Keys.F3 && !ActivatePartsDetails)
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				_ = dataGridItems.ActiveRow;
				DataSet dataSet = new DataSet();
				dataSet = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
				if (dataGridItems.ActiveCell == null)
				{
					comboSearchDialogNew.SelectedItem = "";
				}
				else
				{
					comboBoxGridItem.SelectedID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					comboSearchDialogNew.SelectedItem = comboBoxGridItem.SelectedID;
				}
				if (comboBoxCustomer.SelectedID != "" && comboBoxCustomer.SelectedID != null)
				{
					comboSearchDialogNew.SelectedProvider = comboBoxCustomer.SelectedID;
				}
				comboSearchDialogNew.ShowDialog();
				_ = 1;
			}
			if (e.KeyCode == Keys.F6 && dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				ProductQuantityForm productQuantityForm = new ProductQuantityForm();
				productQuantityForm.LoadData(productID);
				productQuantityForm.ShowDialog();
			}
		}

		private void dataGridItems_GotFocus(object sender, EventArgs e)
		{
			if (!dataGridItems.Focused)
			{
				dataGridItems.DisplayLayout.Bands[0].AddNew();
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
			FormActivator.ImageViewerFormObj.Image = productPhotoViewer.Image;
			FormActivator.BringFormToFront(FormActivator.ImageViewerFormObj);
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow == null || ultraGridColumn == null)
			{
				return;
			}
			if (ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
			else
			{
				if (!(ultraGridColumn.Key == "Price"))
				{
					return;
				}
				ProductPriceListForm productPriceListForm = new ProductPriceListForm();
				productPriceListForm.LoadData(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				if (productPriceListForm.ShowDialog(this) == DialogResult.OK)
				{
					string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
					string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					if (text2 != "" && text != "")
					{
						DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
						if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
						{
							DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
							string a = dataRow["FactorType"].ToString();
							decimal num = decimal.Parse(dataRow["Factor"].ToString());
							decimal num2 = default(decimal);
							num2 = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
							if (a == "M")
							{
								num2 /= num;
							}
							else
							{
								num2 *= num;
							}
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(num2, Global.CurDecimalPoints);
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Round(productPriceListForm.SelectedPrice, Global.CurDecimalPoints);
						}
					}
				}
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
				result3 = Math.Round(result * result2, 2);
				dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
			}
		}

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxCustomerName.Text = comboBoxCustomer.SelectedName;
			LoadCustomerPriceList();
			try
			{
				bool result = false;
				string str = "";
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result);
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "InsRemarks", "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					str = fieldValue.ToString();
				}
				if (result && IsNewRecord)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction. \n \n Reason: " + str);
				}
				else
				{
					GetcustomerData();
					comboBoxShippingAddressID.Clear();
					comboBoxBillingAddress.Clear();
					if (comboBoxCustomer.SelectedID != "")
					{
						comboBoxSalesperson.SelectedID = comboBoxCustomer.DefaultSalesPersonID;
						if (comboBoxSalesperson.SelectedID == "")
						{
							comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
						}
						comboBoxShippingMethod.SelectedID = comboBoxCustomer.DefaultShippingMethod;
						string defaultShipToAddress = comboBoxCustomer.DefaultShipToAddress;
						comboBoxShippingAddressID.IsDataLoaded = true;
						comboBoxShippingAddressID.Filter(comboBoxCustomer.SelectedID);
						if (defaultShipToAddress == "")
						{
							comboBoxShippingAddressID.SelectedID = "PRIMARY";
						}
						else
						{
							comboBoxShippingAddressID.SelectedID = defaultShipToAddress;
						}
						if (useJobCosting)
						{
							comboBoxJob.Clear();
							comboBoxJob.Filter(comboBoxCustomer.SelectedID.Trim());
						}
						defaultShipToAddress = comboBoxCustomer.DefaultBillToAddress;
						comboBoxBillingAddress.IsDataLoaded = true;
						comboBoxBillingAddress.Filter(comboBoxCustomer.SelectedID);
						if (defaultShipToAddress == "")
						{
							comboBoxBillingAddress.SelectedID = "PRIMARY";
						}
						else
						{
							comboBoxBillingAddress.SelectedID = defaultShipToAddress;
						}
						object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
						UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo();
						ultraToolTipInfo.ToolTipText = "";
						ultraToolTipInfo.ToolTipText = fieldValue2.ToString();
						if (fieldValue2.ToString() != "" || fieldValue2.ToString() != string.Empty)
						{
							toolStripButtonInfo.Visible = true;
							ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
						}
						else
						{
							ultraToolTipInfo.ToolTipText = "";
							ultraToolTipManager1.SetUltraToolTip(textBoxCustomerName, ultraToolTipInfo);
							toolStripButtonInfo.Visible = false;
						}
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
						bool.TryParse(Factory.DatabaseSystem.GetFieldValue("System_Document", "ItemFilterBasedonCustomer", "SysDocID", comboBoxSysDoc.SelectedID).ToString(), out isitemfilter);
						if (isitemfilter)
						{
							isDataLoading = false;
							LoadCustomerPriceList();
						}
						if (isitemfilter && IsPriceList)
						{
							comboBoxGridItem.FilterCustomerID = comboBoxCustomer.SelectedID;
							comboBoxGridItem.LoadData();
						}
						else
						{
							comboBoxGridItem.FilterCustomerID = "";
							comboBoxGridItem.LoadData();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadCustomerPriceList()
		{
			try
			{
				if (!isDataLoading)
				{
					priceListData = Factory.PriceListSystem.GetActivePriceListByCustomerID(comboBoxCustomer.SelectedID);
					IsPriceList = false;
					if (priceListData.Tables[0].Rows.Count > 0)
					{
						IsPriceList = true;
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxSysDoc_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isDataLoading)
			{
				if (isNewRecord)
				{
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
				formManager.SetControlDirtyStatus(textBoxVoucherNumber, textBoxVoucherNumber.Text);
				comboBoxCustomer.FilterSysDocID = comboBoxSysDoc.SelectedID;
				comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
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
				strLocation = Factory.DatabaseSystem.GetFieldValue("System_Document", "LocationID", "SysDocID", comboBoxSysDoc.SelectedID).ToString();
				if (strLocation != "")
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = strLocation;
				}
				isLoadZeroQty = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.LoadZeroQuantityinDN, 24, comboBoxSysDoc.SelectedID, defaultValue: false);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null && !isDataLoading)
					{
						if (!(e.Cell.Column.Key == "Item Code"))
						{
							if (e.Cell.Column.Key == "Location" && e.Cell.Row.Cells["Quantity"].Tag != null && e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline != DefaultableBoolean.True)
							{
								e.Cell.Row.Cells["Quantity"].Tag = null;
								e.Cell.Row.Cells["Quantity"].Value = 0;
								e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
							}
							goto IL_0b9e;
						}
						if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
						{
							comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
							goto IL_009e;
						}
						if (comboBoxGridItem.SelectedRow != null)
						{
							goto IL_009e;
						}
					}
					goto end_IL_0000;
					IL_009e:
					ItemTypes itemTypes = ItemTypes.None;
					unchecked
					{
						if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow != null && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
						{
							itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
						}
					}
					if (itemTypes != ItemTypes.Matrix)
					{
						dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
						dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
						dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
						dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
						dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
						dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
						dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
						dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
						if (comboBoxGridItem.SelectedRow != null)
						{
							dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackLot"].Value.ToString();
						}
						else
						{
							dataGridItems.ActiveRow.Cells["IsTrackLot"].Value = null;
						}
						if (comboBoxGridItem.SelectedRow != null)
						{
							dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = comboBoxGridItem.SelectedRow.Cells["IsTrackSerial"].Value.ToString();
						}
						else
						{
							dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value = null;
						}
						if (comboBoxGridItem.SelectedID != "")
						{
							object obj2 = dataGridItems.ActiveRow.Cells["Description"].Value = (dataGridItems.ActiveRow.Cells["DefaultDescription"].Value = comboBoxGridItem.SelectedName);
							dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
							ItemTaxOptions taxOption = comboBoxGridItem.TaxOption;
							dataGridItems.ActiveRow.Cells["TaxOption"].Value = taxOption;
							switch (taxOption)
							{
							case ItemTaxOptions.BasedOnCustomer:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = comboBoxGridItem.TaxGroupID;
								break;
							case ItemTaxOptions.NonTaxable:
								dataGridItems.ActiveRow.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							decimal result = default(decimal);
							decimal.TryParse(comboBoxGridItem.GetSelectedCellValue("StandardCost").ToString(), out result);
							if (result == 0m)
							{
								result = comboBoxGridItem.SelectedAverageCost;
							}
							dataGridItems.ActiveRow.Cells["Cost"].Value = result;
							dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
							if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Location"].Value;
							}
							if ((dataGridItems.ActiveRow.Cells["SpecificationID"].Value == null || dataGridItems.ActiveRow.Cells["SpecificationID"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["SpecificationID"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["SpecificationID"].Value;
							}
							if ((dataGridItems.ActiveRow.Cells["Style"].Value == null || dataGridItems.ActiveRow.Cells["Style"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
							{
								dataGridItems.ActiveRow.Cells["Style"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Style"].Value;
							}
							if (priceListData != null)
							{
								SetPriceAndDescription();
							}
							else
							{
								decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
								dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
							}
						}
						goto IL_0b9e;
					}
					supressInventoryMessage = true;
					MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
					matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
					matrixSelectionForm.AllowNegativeQuantity = false;
					string text = "";
					if (dataGridItems.ActiveRow.Index > 0)
					{
						text = dataGridItems.Rows[0].Cells["Location"].Value.ToString();
					}
					dataGridItems.ActiveRow.Delete(displayPrompt: false);
					if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
					{
						foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
						{
							UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
							ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
							ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
							if (text == "")
							{
								text = Security.DefaultInventoryLocationID;
							}
							ultraGridRow.Cells["Location"].Value = text;
							ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
							ultraGridRow.Cells["Price"].Value = row["UnitPrice"].ToString();
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result2);
							decimal.TryParse(ultraGridRow.Cells["Price"].Value.ToString(), out result3);
							ultraGridRow.Cells["Amount"].Value = (result2 * result3).ToString(Format.TotalAmountFormat);
							ultraGridRow.Update();
						}
					}
					CalculateTotal();
					supressInventoryMessage = false;
					goto end_IL_0000;
					IL_0df6:
					try
					{
						if (dataGridItems.ActiveRow != null)
						{
							string key = e.Cell.Column.Key;
							decimal result4 = default(decimal);
							decimal result5 = default(decimal);
							decimal result6 = default(decimal);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result4);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result5);
							decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result6);
							if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
							{
								result6 = Math.Round(result4 * result5, Global.CurDecimalPoints);
								dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
							}
							else if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
							{
								result6 = Math.Round(result4 * result5, Global.CurDecimalPoints);
								dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
							}
							else if (key == "Unit" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Unit")
							{
								result6 = Math.Round(result4 * result5, Global.CurDecimalPoints);
								dataGridItems.ActiveRow.Cells["Amount"].Value = result6;
							}
							if (!isDataLoading && e.Cell.Column.Key == "Amount")
							{
								CalculateTotal();
							}
						}
					}
					catch (Exception e2)
					{
						dataGridItems.ActiveRow.Cells["Price"].Value = 0;
						dataGridItems.ActiveRow.Cells["Amount"].Value = 0;
						ErrorHelper.ProcessError(e2);
					}
					goto end_IL_0000;
					IL_0b9e:
					if (dataGridItems.ActiveRow.Index == 0)
					{
						string text2 = "";
						text2 = dataGridItems.Rows[0].Cells["Location"].Value.ToString();
						if (text2 == "")
						{
							text2 = Security.DefaultInventoryLocationID;
							if (!string.IsNullOrEmpty(text2))
							{
								dataGridItems.Rows[0].Cells["Location"].Value = text2;
							}
						}
						if (dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue.ToString() != "")
						{
							dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = "";
						}
					}
					if (e.Cell.Column.Key == "Location")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["Location"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (!(e.Cell.Column.Key == "Unit") || dataGridItems.ActiveCell == null || !(dataGridItems.ActiveCell.Column.Key == "Unit"))
					{
						goto IL_0df6;
					}
					if (priceListData != null && (dataGridItems.ActiveRow.Cells["Unit"].Value != null || !(e.Cell.Value.ToString() == "")))
					{
						SetPriceAndDescription();
						goto IL_0df6;
					}
					end_IL_0000:;
				}
				catch (Exception e3)
				{
					ErrorHelper.ProcessError(e3);
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			checked
			{
				if (activeRow != null)
				{
					if (dataGridItems.ActiveRow.Index == 0)
					{
						activeRow.Cells["Job"].Value = (comboBoxJob.SelectedID ?? null);
					}
					if (dataGridItems.ActiveRow.Index == 0)
					{
						activeRow.Cells["CostCategory"].Value = (comboBoxCostCategory.SelectedID ?? null);
					}
					if (dataGridItems.ActiveRow.Index > 0)
					{
						activeRow.Cells["Job"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Job"].Value ?? null);
					}
					if (dataGridItems.ActiveRow.Index > 0)
					{
						activeRow.Cells["CostCategory"].Value = (dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["CostCategory"].Value ?? null);
					}
					if ((dataGridItems.ActiveRow.Cells["RefSlNo"].Value == null || dataGridItems.ActiveRow.Cells["RefSlNo"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
					{
						int.TryParse(dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["RefSlNo"].Text.ToString(), out slNo);
						dataGridItems.ActiveRow.Cells["RefSlNo"].Value = slNo + 1;
					}
				}
			}
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void SetPriceAndDescription()
		{
			string text = dataGridItems.ActiveRow.Cells["Unit"].Value.ToString();
			string text2 = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
			if (priceListData == null || priceListData.Tables.Count == 0)
			{
				return;
			}
			string text3 = "ProductID='" + text2 + "' ";
			if (text != "")
			{
				text3 = text3 + " AND UnitID='" + text + "'";
			}
			DataRow[] array = priceListData.Tables[0].Select(text3);
			if (array.Length != 0)
			{
				if (loadItemDescFromPriceList)
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = array[0]["Description"].ToString();
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = decimal.Parse(array[0]["UnitPrice"].ToString());
			}
			else
			{
				decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString(), comboBoxCustomer.SelectedID, comboBoxSysDoc.LocationID, dataGridItems.ActiveRow.Cells["Unit"].Value.ToString());
				dataGridItems.ActiveRow.Cells["Price"].Value = productSalesPrice;
			}
			if (!(text2 != "") || !(text != ""))
			{
				return;
			}
			DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
			decimal result = default(decimal);
			if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
			{
				DataRow dataRow = productUnitDetails.Tables[0].Rows[0];
				string a = dataRow["FactorType"].ToString();
				decimal num = decimal.Parse(dataRow["Factor"].ToString());
				decimal num2 = decimal.Parse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString());
				if (a == "M")
				{
					num2 /= num;
				}
				else
				{
					num2 *= num;
				}
				dataGridItems.ActiveRow.Cells["Price"].Value = num2;
				decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", "StandardCost", "ProductID", text2).ToString(), out result);
				if (result == 0m)
				{
					result = decimal.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "AverageCost", "ProductID", text2).ToString());
				}
				if (a == "M")
				{
					result /= num;
				}
				else
				{
					result *= num;
				}
				dataGridItems.ActiveRow.Cells["Cost"].Value = result;
			}
			else
			{
				decimal.TryParse(Factory.DatabaseSystem.GetFieldValue("Product", "StandardCost", "ProductID", text2).ToString(), out result);
				if (result == 0m)
				{
					result = decimal.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "AverageCost", "ProductID", text2).ToString());
				}
				dataGridItems.ActiveRow.Cells["Cost"].Value = result;
			}
		}

		private void SetupForm()
		{
			checked
			{
				switch (CompanyPreferences.LocalSalesFlow)
				{
				case SalesFlows.SOThenDNThenInvoice:
					if (CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.AllowAddNew = true;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
					}
					selectInvoiceToolStripMenuItem.Visible = false;
					toolStripButtonExcelImport.Visible = false;
					duplicateToolStripMenuItem.Enabled = false;
					buttonSelectDocument.Visible = true;
					textBoxCustomerName.Width = textBoxCustomerName.Width - buttonSelectDocument.Width - 3;
					checkedListBoxOrders.Visible = true;
					labelSelectedDocs.Visible = true;
					if (!CompanyPreferences.AllowLSaleAddNew && !CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					}
					break;
				case SalesFlows.SOThenInvoiceThenDN:
					if (CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.AllowAddNew = true;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
					}
					dataGridItems.ShowDeleteMenu = false;
					createFromSalesOrderToolStripMenuItem.Visible = false;
					buttonSelectDocument.Visible = true;
					duplicateToolStripMenuItem.Enabled = false;
					textBoxCustomerName.Width = textBoxCustomerName.Width - buttonSelectDocument.Width - 3;
					textBoxNote.Width = 306;
					checkedListBoxOrders.Visible = true;
					labelSelectedDocs.Visible = true;
					if (!CompanyPreferences.AllowLSaleAddNew && !CompanyPreferences.AllowLSellWithoutSO)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					}
					break;
				case SalesFlows.DirectInvoice:
					buttonSelectDocument.Visible = false;
					checkedListBoxOrders.Visible = false;
					labelSelectedDocs.Visible = false;
					break;
				}
			}
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
			comboBoxGridItem.IsLoadingData = true;
		}

		private void comboBoxGridItem_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dataGrid_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
			checked
			{
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Price")
				{
					decimal result = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
					result = Math.Round(result, 5);
					dataGridItems.ActiveCell.Value = result;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
				{
					decimal result2 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result2);
					result2 = Math.Round(result2, 4);
					dataGridItems.ActiveCell.Value = result2;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Amount")
				{
					decimal result3 = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result3);
					result3 = Math.Round(result3, Global.CurDecimalPoints);
					dataGridItems.ActiveCell.Value = result3;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
				{
					for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["Location"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Job")
				{
					for (int j = dataGridItems.ActiveCell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
					{
						if (dataGridItems.Rows[j].Cells["Job"].Value.ToString() == "")
						{
							dataGridItems.Rows[j].Cells["Job"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else
				{
					if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "CostCategory"))
					{
						return;
					}
					for (int k = dataGridItems.ActiveCell.Row.Index + 1; k < dataGridItems.Rows.Count; k++)
					{
						if (dataGridItems.Rows[k].Cells["CostCategory"].Value.ToString() == "")
						{
							dataGridItems.Rows[k].Cells["CostCategory"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.IsActiveRow && row.Cells["Item Code"].Value == dataGridItems.ActiveRow.Cells["Item Code"].Value && !isChecked)
					{
						switch (ErrorHelper.QuestionMessageYesNoCancel(row.Cells["Item Code"].Value.ToString() + " item is already in the list. Do you want to continue?"))
						{
						case DialogResult.Cancel:
						case DialogResult.No:
							row.Delete();
							break;
						case DialogResult.Yes:
							isChecked = true;
							break;
						}
					}
				}
				if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Quantity"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["Item Code"].Activate();
				}
				else if (activeRow.Cells["Quantity"].Value.ToString() == "")
				{
					activeRow.Cells["Quantity"].Value = 0;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenDNThenInvoice && activeRow != null && activeRow.DataChanged && activeRow.IsAddRow && e.Cell.Column.Key == "Quantity")
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (activeRow.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString()));
				}
				if (CompanyPreferences.NegativeQuantityAction != 1 || itemTypes == ItemTypes.ConsignmentItem)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					if (!Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Delivery_Note_Detail", sysDocID, voucherID, decimal.Parse(e.NewValue.ToString())) && !supressInventoryMessage)
					{
						if (itemTypes == ItemTypes.ConsignmentItem)
						{
							ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
							e.Cancel = true;
							return;
						}
						if (CompanyPreferences.NegativeQuantityAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You do not have sufficient quantity in this location. Do you want to continue?") != DialogResult.Yes)
							{
								e.Cancel = true;
							}
						}
						else if (CompanyPreferences.NegativeQuantityAction == 3)
						{
							ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
							e.Cancel = true;
							return;
						}
					}
				}
			}
			if (e.Cell.DataChanged && e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
			if (!AllowLSQtyMoreThanSO && sourceDocType == ItemSourceTypes.SalesOrder && !string.IsNullOrEmpty(sourceDocType.ToString()) && e.Cell.Column.Key == "Quantity" && e.Cell.DataChanged)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(e.Cell.OriginalValue.ToString(), out result2);
				decimal.TryParse(e.NewValue.ToString(), out result);
				if (result > result2)
				{
					ErrorHelper.WarningMessage("Quantity is more than Ordered");
					e.Cancel = true;
				}
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			bool result = false;
			bool result2 = false;
			if (dataGridItems.ActiveRow.Cells["IsTrackLot"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackLot"].Value.ToString(), out result);
			}
			if (dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value != null)
			{
				bool.TryParse(dataGridItems.ActiveRow.Cells["IsTrackSerial"].Value.ToString(), out result2);
			}
			if (result)
			{
				if (dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				issueLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
				issueLotSelectionForm.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
				issueLotSelectionForm.RowQuantity = float.Parse(dataGridItems.ActiveCell.Text);
				issueLotSelectionForm.IsDefaultLoad = IsBinSelected;
				issueLotSelectionForm.BinID = SelectedBin;
				if (!isNewRecord)
				{
					issueLotSelectionForm.SysDocID = comboBoxSysDoc.SelectedID;
					issueLotSelectionForm.VoucherID = textBoxVoucherNumber.Text;
				}
				if (cell.Tag != null)
				{
					issueLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
				}
				if (issueLotSelectionForm.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				cell.Tag = issueLotSelectionForm.ProductLotTable;
				cell.Appearance.FontData.Underline = DefaultableBoolean.True;
			}
			return true;
		}

		private void dataGrid_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridItems.ActiveCell.Column.Key.ToString() == "Item Code")
			{
				e.RaiseErrorEvent = false;
				comboBoxGridItem.Text = dataGridItems.ActiveCell.Text;
				comboBoxGridItem.QuickAddItem();
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Cost")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.InvalidAmount);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "New Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Unit")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid UOM for the item.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Location")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid location for the item.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new DeliveryNoteData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.DeliveryNoteTable.Rows[0] : currentData.DeliveryNoteTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				if (dateTimePickerPO.Checked)
				{
					dataRow["PODate"] = dateTimePickerPO.Value;
				}
				else
				{
					dataRow["PODate"] = DBNull.Value;
				}
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["PONumber"] = textBoxPONumber.Text;
				if (clUserID != "")
				{
					dataRow["CLUserID"] = clUserID;
				}
				else
				{
					dataRow["CLUserID"] = DBNull.Value;
				}
				dataRow["SalesFlow"] = CompanyPreferences.LocalSalesFlow;
				if (comboBoxSalesperson.SelectedID != "")
				{
					dataRow["SalespersonID"] = comboBoxSalesperson.SelectedID;
				}
				else
				{
					dataRow["SalespersonID"] = DBNull.Value;
				}
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["CustomerID"] = comboBoxCustomer.SelectedID;
				dataRow["CustomerAddress"] = textBoxBilltoAddress.Text;
				dataRow["ShipToAddress"] = textBoxShipto.Text;
				if (comboBoxJob.SelectedID != "")
				{
					dataRow["JobID"] = comboBoxJob.SelectedID;
				}
				else
				{
					dataRow["JobID"] = DBNull.Value;
				}
				if (comboBoxCostCategory.SelectedID != "")
				{
					dataRow["CostCategoryID"] = comboBoxCostCategory.SelectedID;
				}
				else
				{
					dataRow["CostCategoryID"] = DBNull.Value;
				}
				if (comboBoxShippingAddressID.SelectedID != "")
				{
					dataRow["ShippingAddressID"] = comboBoxShippingAddressID.SelectedID;
				}
				else
				{
					dataRow["ShippingAddressID"] = DBNull.Value;
				}
				dataRow["CurrencyID"] = currencyID;
				if (comboBoxBillingAddress.SelectedID != "")
				{
					dataRow["BillingAddressID"] = comboBoxBillingAddress.SelectedID;
				}
				else
				{
					dataRow["BillingAddressID"] = DBNull.Value;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxDriver.SelectedID != "")
				{
					dataRow["DriverID"] = comboBoxDriver.SelectedID;
				}
				else
				{
					dataRow["DriverID"] = DBNull.Value;
				}
				if (comboBoxVehicle.SelectedID != "")
				{
					dataRow["VehicleID"] = comboBoxVehicle.SelectedID;
				}
				else if (comboBoxVehicle.SelectedID == "")
				{
					dataRow["VehicleID"] = textBoxVechicleName.Text;
				}
				else
				{
					dataRow["VehicleID"] = DBNull.Value;
				}
				dataRow["SourceDocType"] = sourceDocType;
				if (comboBoxPayeeTaxGroup.SelectedID != "")
				{
					dataRow["PayeeTaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				}
				else
				{
					dataRow["PayeeTaxGroupID"] = DBNull.Value;
				}
				if (CompanyPreferences.IsTax)
				{
					dataRow["TaxOption"] = (byte)comboBoxCustomer.TaxOption;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
				}
				dataRow["TempKey"] = CurrentKey;
				dataRow["AutoKeyID"] = TempAutoKeyID;
				dataRow["CurrentUser"] = Global.CurrentUser.ToString();
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.DeliveryNoteTable.Rows.Add(dataRow);
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.DeliveryNoteDetailTable.Columns.Contains(column.Key))
					{
						currentData.DeliveryNoteDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.DeliveryNoteDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.DeliveryNoteDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SourceSysDocID"))
					{
						if (row.Cells["SourceSysDocID"].Value.ToString() != "")
						{
							dataRow2["SourceSysDocID"] = row.Cells["SourceSysDocID"].Value.ToString();
						}
						if (row.Cells["SourceVoucherID"].Value.ToString() != "")
						{
							dataRow2["SourceVoucherID"] = row.Cells["SourceVoucherID"].Value.ToString();
						}
						if (row.Cells["SourceRowIndex"].Value.ToString() != "")
						{
							dataRow2["SourceRowIndex"] = row.Cells["SourceRowIndex"].Value.ToString();
						}
					}
					dataRow2["SpecificationID"] = row.Cells["SpecificationID"].Value.ToString();
					dataRow2["StyleID"] = row.Cells["Style"].Value.ToString();
					dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
					dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
					dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
					dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					if (row.Cells["RowSourceType"].Value.ToString() != "")
					{
						dataRow2["RowSource"] = row.Cells["RowSourceType"].Value.ToString();
					}
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
					if (row.Cells["Price"].Value != null && row.Cells["Price"].Value.ToString() != "")
					{
						dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
					}
					else
					{
						dataRow2["UnitPrice"] = 0;
					}
					dataRow2["RowIndex"] = row.Index;
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
					if (!row.Cells["TaxOption"].Value.IsNullOrEmpty())
					{
						dataRow2["TaxOption"] = row.Cells["TaxOption"].Value.ToString();
					}
					else
					{
						dataRow2["TaxOption"] = (byte)2;
					}
					if (row.Cells["TaxGroupID"].Value != null && row.Cells["TaxGroupID"].Value.ToString() != "")
					{
						dataRow2["TaxGroupID"] = row.Cells["TaxGroupID"].Value.ToString();
					}
					else
					{
						dataRow2["TaxGroupID"] = DBNull.Value;
					}
					if (row.Cells["ListRowIndex"].Value != null && row.Cells["ListRowIndex"].Value.ToString() != "")
					{
						dataRow2["ListRowIndex"] = row.Cells["ListRowIndex"].Value.ToString();
					}
					else
					{
						dataRow2["ListRowIndex"] = DBNull.Value;
					}
					dataRow2["ListSysDocID"] = row.Cells["ListSysDocID"].Value.ToString();
					dataRow2["ListVoucherID"] = row.Cells["ListVoucherID"].Value.ToString();
					if (!string.IsNullOrEmpty(row.Cells["RefSlNo"].Value.ToString()))
					{
						dataRow2["RefSlNo"] = row.Cells["RefSlNo"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefText1"].Value.ToString()))
					{
						dataRow2["RefText1"] = row.Cells["RefText1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefText2"].Value.ToString()))
					{
						dataRow2["RefText2"] = row.Cells["RefText2"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefNum1"].Value.ToString()))
					{
						dataRow2["RefNum1"] = row.Cells["RefNum1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefNum2"].Value.ToString()))
					{
						dataRow2["RefNum2"] = row.Cells["RefNum2"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefDate1"].Value.ToString()))
					{
						dataRow2["RefDate1"] = row.Cells["RefDate1"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(row.Cells["RefDate2"].Value.ToString()))
					{
						dataRow2["RefDate2"] = row.Cells["RefDate2"].Value.ToString();
					}
					dataRow2.EndEdit();
					currentData.DeliveryNoteDetailTable.Rows.Add(dataRow2);
					string b = row.Cells["Location"].Value.ToString();
					string text = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Quantity"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
						{
							if (row2["LocationID"].ToString() != b || row2["ProductID"].ToString() != text)
							{
								ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code: '" + text + "'");
								return false;
							}
							DataRow dataRow4 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
							dataRow4["ProductID"] = row2["ProductID"];
							dataRow4["LocationID"] = row2["LocationID"];
							dataRow4["LotNumber"] = row2["LotNumber"];
							dataRow4["Reference"] = row2["Reference"];
							dataRow4["BinID"] = row2["BinID"];
							dataRow4["RackID"] = row2["RackID"];
							dataRow4["Reference2"] = row2["Reference2"];
							dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
							dataRow4["SoldQty"] = row2["SoldQty"];
							dataRow4["Cost"] = row2["Cost"];
							dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
							dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow4["UnitPrice"] = 0;
							dataRow4["RowIndex"] = row.Index;
							if (string.IsNullOrEmpty(SelectedBin) || !(row2["BinID"].ToString() != SelectedBin))
							{
								currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
							}
						}
					}
				}
				if (sourceDocType == ItemSourceTypes.SalesInvoice)
				{
					currentData.InvoiceDNoteTable.Rows.Clear();
					foreach (object item in checkedListBoxOrders.Items)
					{
						NameValue nameValue = item as NameValue;
						if (currentData.InvoiceDNoteTable.Select("InvoiceVoucherID = '" + nameValue.Name + "'").Length == 0)
						{
							DataRow dataRow5 = currentData.InvoiceDNoteTable.NewRow();
							dataRow5["InvoiceSysDocID"] = nameValue.ID;
							dataRow5["InvoiceVoucherID"] = nameValue.Name;
							dataRow5.EndEdit();
							currentData.InvoiceDNoteTable.Rows.Add(dataRow5);
						}
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

		private void SetupGrid()
		{
			try
			{
				dataGridItems.Clear();
				dataGridItems.DataSource = null;
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("SpecificationID");
				dataTable.Columns.Add("Style");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("SourceRowIndex", typeof(int));
				dataTable.Columns.Add("RowSourceType", typeof(int));
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("QtyReturned", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("ListSysDocID");
				dataTable.Columns.Add("ListVoucherID");
				dataTable.Columns.Add("ListRowIndex", typeof(int));
				dataTable.Columns.Add("RefSlNo", typeof(int));
				dataTable.Columns.Add("RefText1");
				dataTable.Columns.Add("RefText2");
				dataTable.Columns.Add("RefNum1", typeof(decimal));
				dataTable.Columns.Add("RefNum2", typeof(decimal));
				dataTable.Columns.Add("RefDate1", typeof(DateTime));
				dataTable.Columns.Add("RefDate2", typeof(DateTime));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].Hidden = true;
				bool flag4 = ultraGridColumn4.Hidden = flag2;
				bool flag6 = ultraGridColumn3.Hidden = flag4;
				bool hidden = ultraGridColumn2.Hidden = flag6;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"];
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"];
				bool flag9 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				bool flag11 = ultraGridColumn10.Hidden = flag9;
				flag2 = (ultraGridColumn9.Hidden = flag11);
				flag4 = (ultraGridColumn8.Hidden = flag2);
				flag6 = (ultraGridColumn7.Hidden = flag4);
				hidden = (ultraGridColumn6.Hidden = flag6);
				ultraGridColumn5.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = !useJobCosting;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = !useJobCosting;
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = false;
				}
				if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = false;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn13.CellActivation = activation2;
				Activation activation7 = ultraGridColumn11.CellActivation = (ultraGridColumn12.CellActivation = activation4);
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeAdditionalColumns();
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color4 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color);
				dataGridItems.DisplayLayout.Bands[0].Columns["RowSourceType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Header.Caption = "Default Description";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Style"].ValueList = comboBoxStyle;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].ValueList = comboBoxSpecification;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.CalcManager = ultraCalcManager1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Format = Format.GridAmountFormat;
				if (CompanyPreferences.CheckCreditLimitInDeliveryNote)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = false;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = false;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = true;
				}
				UltraGridColumn ultraGridColumn14 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn14.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].Header.Caption = CompanyPreferences.Attribute1Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].Header.Caption = CompanyPreferences.Attribute2Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].Header.Caption = CompanyPreferences.Attribute3Name;
				dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Header.Caption = CompanyPreferences.SpecificationID;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Header.Caption = CompanyPreferences.RefSlNo;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Header.Caption = CompanyPreferences.RefText1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Header.Caption = CompanyPreferences.RefText2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Header.Caption = CompanyPreferences.RefNum1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Header.Caption = CompanyPreferences.RefNum2;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Header.Caption = CompanyPreferences.RefDate1;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Header.Caption = CompanyPreferences.RefDate2;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
				UltraGridColumn ultraGridColumn15 = dataGridItems.DisplayLayout.Bands[0].Columns["ListSysDocID"];
				UltraGridColumn ultraGridColumn16 = dataGridItems.DisplayLayout.Bands[0].Columns["ListRowIndex"];
				flag6 = (dataGridItems.DisplayLayout.Bands[0].Columns["ListVoucherID"].Hidden = true);
				hidden = (ultraGridColumn16.Hidden = flag6);
				ultraGridColumn15.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].Hidden = true;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Description"].Activation = Activation.Disabled;
					row.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Location"].Activation = Activation.Disabled;
					row.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].Format = "0.####";
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].MaxValue = 99999999m;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].Header.Caption = "Returned";
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].CellActivation = Activation.NoEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].TabStop = false;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].CellAppearance.BackColor = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["QtyReturned"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Header.Appearance.Cursor = Cursors.Hand;
			if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].DisplayFormat = "{0:n}";
			}
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Shipped"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Invoiced"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Invoiced", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Invoiced"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Invoiced"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Shipped", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Shipped"].DisplayFormat = "{0:n}";
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenDNThenInvoice && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped"].CellAppearance.ForeColorDisabled = Color.Black;
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					row2.Cells["Item Code"].Activation = Activation.Disabled;
					row2.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row2.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row2.Cells["Description"].Activation = Activation.Disabled;
					row2.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row2.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row2.Cells["Unit"].Activation = Activation.Disabled;
					row2.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row2.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			else
			{
				foreach (UltraGridRow row3 in dataGridItems.Rows)
				{
					row3.Cells["Item Code"].Activation = Activation.AllowEdit;
					row3.Cells["Item Code"].Appearance.BackColorDisabled = Color.White;
					row3.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row3.Cells["Description"].Activation = Activation.AllowEdit;
					row3.Cells["Description"].Appearance.BackColorDisabled = Color.White;
					row3.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row3.Cells["Unit"].Activation = Activation.AllowEdit;
					row3.Cells["Unit"].Appearance.BackColorDisabled = Color.White;
					row3.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.AllowEdit;
				comboBoxGridLocation.ReadOnly = false;
			}
			if (!showLoadDraft)
			{
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangePrice))
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.NoEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
				}
				else
				{
					dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.AllowEdit;
				}
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeDescription))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.NoEdit;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
			}
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
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Hidden = true;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Hidden = true;
			}
		}

		private void ExcludeAdditionalColumns()
		{
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefDate2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefText2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefSlNo))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum1))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
			if (string.IsNullOrEmpty(CompanyPreferences.RefNum2))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
			}
			else
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			comboBoxCustomer.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.DeliveryNoteSystem.GetDeliveryNoteByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
						comboBoxJob.Filter(comboBoxCustomer.SelectedID);
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
					DataRow dataRow = currentData.Tables["Delivery_Note"].Rows[0];
					dataGridItems.BeginUpdate();
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					if (dataRow["PODate"] != DBNull.Value)
					{
						dateTimePickerPO.Value = DateTime.Parse(dataRow["PODate"].ToString());
						dateTimePickerPO.Checked = true;
					}
					else
					{
						dateTimePickerPO.Checked = false;
					}
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
					comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
					comboBoxBillingAddress.SelectedID = dataRow["BillingAddressID"].ToString();
					textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
					textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					comboBoxDriver.SelectedID = dataRow["DriverID"].ToString();
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						sourceDocType = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
					}
					else
					{
						sourceDocType = ItemSourceTypes.None;
					}
					comboBoxVehicle.SelectedID = dataRow["VehicleID"].ToString();
					if (comboBoxVehicle.SelectedID == "")
					{
						comboBoxVehicle.Clear();
						textBoxVechicleName.ReadOnly = false;
						textBoxVechicleName.Text = dataRow["VehicleID"].ToString();
					}
					comboBoxJob.SelectedID = dataRow["JobID"].ToString();
					comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
					bool result = false;
					bool.TryParse(dataRow["IsInvoiced"].ToString(), out result);
					if (result)
					{
						docStatusLabel.Visible = true;
						docStatusLabel.DocumentNumber = dataRow["InvoiceVoucherID"].ToString();
						docStatusLabel.Tag = dataRow["InvoiceSysDocID"].ToString();
						docStatusLabel.DisplayStatus = "INVOICED";
					}
					else
					{
						docStatusLabel.Visible = false;
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Delivery_Note_Detail") && currentData.DeliveryNoteDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Delivery_Note_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							if (currentData.Tables["Delivery_Note_Detail"].Columns.Contains("QuantityReturned"))
							{
								if (row["QuantityReturned"] != DBNull.Value)
								{
									dataRow3["QtyReturned"] = row["QuantityReturned"];
								}
								else
								{
									dataRow3["QtyReturned"] = DBNull.Value;
								}
							}
							dataRow3["Attribute1"] = row["Attribute1"];
							dataRow3["Attribute2"] = row["Attribute2"];
							dataRow3["Attribute3"] = row["Attribute3"];
							dataRow3["MatrixParentID"] = row["MatrixParentID"];
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["RowSourceType"] = row["RowSource"];
							dataRow3["Price"] = row["UnitPrice"];
							dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
							dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
							dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
							dataRow3["ListSysDocID"] = row["ListSysDocID"];
							dataRow3["ListVoucherID"] = row["ListVoucherID"];
							dataRow3["ListRowIndex"] = row["ListRowIndex"];
							dataRow3["Job"] = row["JobID"];
							dataRow3["CostCategory"] = row["CostCategoryID"];
							dataRow3["SpecificationID"] = row["SpecificationID"];
							dataRow3["Style"] = row["StyleID"];
							dataRow3["RefSlNo"] = row["RefSlNo"];
							dataRow3["RefText1"] = row["RefText1"];
							dataRow3["RefText2"] = row["RefText2"];
							dataRow3["RefNum1"] = row["RefNum1"];
							dataRow3["RefNum2"] = row["RefNum2"];
							dataRow3["RefDate1"] = row["RefDate1"];
							dataRow3["RefDate2"] = row["RefDate2"];
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
							decimal d = default(decimal);
							decimal d2 = default(decimal);
							if (!dataRow3["Quantity"].IsDBNullOrEmpty())
							{
								d = decimal.Parse(dataRow3["Quantity"].ToString());
							}
							if (!dataRow3["Price"].IsDBNullOrEmpty())
							{
								d2 = decimal.Parse(dataRow3["Price"].ToString());
							}
							dataRow3["Amount"] = Math.Round(d * d2, Format.PriceNumberOfFixedDecimals);
							dataRow3["Cost"] = Factory.ProductSystem.GetProductCostwithMultiUnit(row["ProductID"].ToString(), row["UnitID"].ToString(), comboBoxSysDoc.LocationID);
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						dataGridItems.EventManager.AllEventsEnabled = false;
						dataGridItems.SetDataBinding(dataTable, dataTable.TableName);
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
							if (row2.Cells["RowSourceType"].Value != null && row2.Cells["RowSourceType"].Value.ToString() != "")
							{
								itemSourceTypes = (ItemSourceTypes)int.Parse(row2.Cells["RowSourceType"].Value.ToString());
							}
							if (itemSourceTypes == ItemSourceTypes.SalesInvoice || itemSourceTypes == ItemSourceTypes.SalesOrder)
							{
								row2.Cells["Item Code"].Activation = Activation.Disabled;
								row2.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Description"].Activation = Activation.Disabled;
								row2.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Location"].Activation = Activation.Disabled;
								row2.Cells["Location"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Location"].Appearance.ForeColorDisabled = Color.Black;
								row2.Cells["Unit"].Activation = Activation.Disabled;
								row2.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
							}
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
								if (array.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
									SelectedBin = dataSet.Tables[0].Rows[0]["BinID"].ToString();
								}
							}
						}
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						CalculateTotal();
						EnableButton();
						checkedListBoxOrders.Items.Clear();
						if (currentData.Tables.Contains("SourceTableDetails"))
						{
							foreach (DataRow row3 in currentData.Tables["SourceTableDetails"].Rows)
							{
								NameValue item = new NameValue(row3["SourceVoucherID"].ToString(), row3["SourceSysDocID"].ToString());
								checkedListBoxOrders.Items.Add(item);
							}
						}
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				dataGridItems.EndUpdate();
				dataGridItems.EventManager.AllEventsEnabled = true;
				isDataLoading = false;
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
			if (CompanyPreferences.EnableDocTempSaving)
			{
				GetUpdatedDocumentNo();
			}
			if (!allowEdit)
			{
				ErrorHelper.InformationMessage(Application.ProductName, "You cannot edit this transfer transaction because it is already accepted or rejected.", "Document is in use.");
				return false;
			}
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
				bool flag2 = Factory.DeliveryNoteSystem.CreateDeliveryNote(currentData, !isNewRecord, showLoadDraft);
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
					if (!string.IsNullOrEmpty(currentkey))
					{
						flag2 = Factory.SettingSystem.DeleteSettingTemporary(currentkey, "", "");
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
				if (ex.Number == 1047)
				{
					ErrorHelper.WarningMessage("This transaction is already in use by another transaction and cannot be modified.");
				}
				else
				{
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
			formManager.ShowApprovalPanel(approvalTaskID, "Delivery_Note", "VoucherID");
		}

		private void EnableButton()
		{
			bool flag = Factory.DeliveryNoteSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, Global.CurrentUser.ToUpper(), isModify: false, "");
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.Edit && flag)
			{
				buttonSave.Enabled = true;
				screenRight.Edit = true;
			}
			else if (!screenRight.Edit)
			{
				buttonSave.Enabled = false;
				screenRight.Edit = false;
			}
			if (Factory.SystemDocumentSystem.ExistDocumentNumber("Delivery_Note", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				simpleButtonSave.Enabled = false;
			}
			else
			{
				simpleButtonSave.Enabled = true;
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Delivery_Note", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit.");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (RestrictTransaction)
			{
				ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.DeliveryNote, Global.CurrentUser, includeApproveUser: false);
				if (entityApprovalStatus.Tables[0].Rows.Count > 0 && !bool.Parse(entityApprovalStatus.Tables[0].Rows[0]["ModifyTransaction"].ToString()))
				{
					ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
					return false;
				}
			}
			DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
			int num = 0;
			num = Security.AllowedDays(GeneralSecurityRoles.EnterBackDatedTransaction);
			DateTime value = dateTimePickerDate.Value;
			TimeSpan timeSpan = t.Add(TimeSpan.FromDays(1.0)) - value;
			bool flag = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
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
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxCustomer.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
						continue;
					}
					if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
					if (dataGridItems.Rows[i].Cells["Location"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select a location for all the items.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					decimal result = default(decimal);
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result);
					if (result < 0m)
					{
						row.Activate();
						ErrorHelper.InformationMessage("One or more rows have negative quantity. Cannot enter negative quantity.Please enter a quantity equal or greater than zero.");
						return false;
					}
				}
				bool result2 = false;
				bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Customer", "IsHold", "CustomerID", comboBoxCustomer.SelectedID).ToString(), out result2);
				if (result2)
				{
					ErrorHelper.WarningMessage("This customer is on hold status and does not allow transaction.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Delivery_Note", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				if (CompanyPreferences.OverCLAction != 1)
				{
					string sysDocID = "";
					string voucherID = "";
					if (!IsNewRecord)
					{
						sysDocID = comboBoxSysDoc.SelectedID;
						voucherID = textBoxVoucherNumber.Text;
					}
					decimal num2 = default(decimal);
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						decimal num3 = default(decimal);
						decimal result3 = default(decimal);
						decimal result4 = default(decimal);
						num3 = decimal.Parse(row2.Cells["Amount"].Value.ToString());
						if (num3 == 0m)
						{
							decimal.TryParse(row2.Cells["Quantity"].Value.ToString(), out result3);
							decimal.TryParse(row2.Cells["Cost"].Value.ToString(), out result4);
							num3 = result3 * result4;
						}
						num2 += Math.Round(num3, Global.CurDecimalPoints);
					}
					if (Factory.CustomerSystem.IsOverCreditLimit(comboBoxCustomer.SelectedID, sysDocID, voucherID, num2, checkOpenDN: true, dateTimePickerDate.Value))
					{
						if (CompanyPreferences.OverCLAction == 2)
						{
							if (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "This transaction exceeds the customer's credit limit.", "Do you want to continue?") == DialogResult.No)
							{
								return false;
							}
						}
						else
						{
							if (CompanyPreferences.OverCLAction == 3)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "You are not allowed to sell over the customer's credit limit.");
								return false;
							}
							if (CompanyPreferences.OverCLAction == 4)
							{
								ErrorHelper.WarningMessage("This transaction exceeds the customer's credit limit.", "The transaction should be approved to proceed.");
								CLPasswordForm cLPasswordForm = new CLPasswordForm();
								cLPasswordForm.CustomerID = comboBoxCustomer.SelectedID;
								cLPasswordForm.CustomerName = comboBoxCustomer.SelectedName;
								cLPasswordForm.InvoiceAmount = num2;
								if (cLPasswordForm.ShowDialog() == DialogResult.Cancel)
								{
									return false;
								}
								clUserID = cLPasswordForm.ApproverID;
							}
						}
					}
				}
				return true;
			}
		}

		private decimal GetTransactionBalance()
		{
			decimal result = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Quantity"].Value.ToString());
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
				allowEdit = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxRef2.Clear();
				clUserID = "";
				CurrentKey = "";
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerPO.Value = DateTime.Now;
				dateTimePickerPO.Checked = false;
				comboBoxShippingAddressID.Clear();
				textBoxShipto.Clear();
				comboBoxBillingAddress.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				comboBoxCustomer.Clear();
				comboBoxShippingAddressID.Clear();
				textBoxPONumber.Clear();
				comboBoxSalesperson.Clear();
				comboBoxShippingMethod.Clear();
				comboBoxDriver.Clear();
				comboBoxJob.Clear();
				comboBoxJob.Filter("");
				comboBoxCostCategory.Clear();
				comboBoxVehicle.Clear();
				textBoxBilltoAddress.Clear();
				isDiscountPercent = false;
				isDiscountPercent = false;
				docStatusLabel.Visible = false;
				textBoxVechicleName.Clear();
				simpleButtonSave.Enabled = true;
				IsTemporaryTransaction = false;
				showCreditValidation = true;
				restrictonDataload = false;
				TempAutoKeyID = 0;
				sourceDocType = ItemSourceTypes.None;
				isChecked = false;
				if (comboBoxVehicle.SelectedID == "")
				{
					textBoxVechicleName.ReadOnly = false;
				}
				else
				{
					textBoxVechicleName.ReadOnly = true;
				}
				clUserID = "";
				checkedListBoxOrders.Items.Clear();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Invoiced"))
				{
					dataTable.Columns.Remove("Invoiced");
					dataTable.Columns.Remove("Shipped");
				}
				if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
				{
					if (CompanyPreferences.AllowLSDNoteWithoutInvoice)
					{
						dataGridItems.AllowAddNew = true;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
					}
				}
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				comboBoxSalesperson.SelectedID = Security.DefaultSalespersonID;
				dataTable.Rows.Clear();
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeSysDoc))
				{
					comboBoxSysDoc.ReadOnly = true;
				}
				else
				{
					comboBoxSysDoc.ReadOnly = false;
				}
				IsNewRecord = true;
				IsVoid = false;
				IsBinSelected = false;
				formManager.ResetDirty();
				comboBoxCustomer.Focus();
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
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				return Factory.DeliveryNoteSystem.DeleteDeliveryNote(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Delivery_Note", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Delivery_Note", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Delivery_Note", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Delivery_Note", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Delivery_Note", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
			else
			{
				dataGridItems.SaveLayout();
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
				SetupGrid();
				labelTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxSysDoc.FilterByType(SysDocTypes.DeliveryNote);
				SetSecurity();
				if (!base.IsDisposed)
				{
					_ = dataGridItems.ActiveColScrollRegion.VisibleHeaders.Count;
					IsNewRecord = true;
					ClearForm();
					SetupForm();
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
			allowChangeCustomer = Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeSalesCustomer);
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowChangeBillAddress))
			{
				comboBoxBillingAddress.ReadOnly = true;
				textBoxBilltoAddress.ReadOnly = true;
			}
			else
			{
				comboBoxBillingAddress.ReadOnly = false;
				textBoxBilltoAddress.ReadOnly = false;
			}
			TotalDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.GiveDiscount);
			priceDiscountPercAllowed = Security.AllowedDiscount(GeneralSecurityRoles.AllowPriceDiscount);
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell != null)
			{
				comboBoxGridItem.IsLoadingData = false;
			}
		}

		private void ultraFormattedLinkLabel2_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (isNewRecord)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				simpleButtonSave.Enabled = true;
			}
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
			if (Void(isVoid: true))
			{
				IsVoid = true;
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
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				bool flag = Factory.DeliveryNoteSystem.VoidDeliveryNote(SystemDocID, textBoxVoucherNumber.Text, isVoid);
				if (!flag)
				{
					ErrorHelper.ErrorMessage("Could not void the transaction because of an error.");
					return false;
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.DeliveryNote);
		}

		private void LoadCustomerBillingAddress()
		{
			try
			{
				if (!isDataLoading)
				{
					DataSet customerDocumentAddress = Factory.CustomerSystem.GetCustomerDocumentAddress(comboBoxCustomer.SelectedID, "BillToAddressID");
					DataRow dataRow = null;
					if (customerDocumentAddress != null && customerDocumentAddress.Tables.Count > 0 && customerDocumentAddress.Tables[0].Rows.Count > 0)
					{
						dataRow = customerDocumentAddress.Tables[0].Rows[0];
					}
					if (dataRow != null)
					{
						currentCustomerAddressID = dataRow["BillToAddressID"].ToString();
						textBoxBilltoAddress.Text = dataRow["AddressPrintFormat"].ToString();
					}
					else
					{
						textBoxBilltoAddress.Clear();
						currentCustomerAddressID = "";
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string nextID = DatabaseHelper.GetNextID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (nextID != "")
			{
				currentCustomerAddressID = nextID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", nextID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void buttonPrevBillto_Click(object sender, EventArgs e)
		{
			if (comboBoxCustomer.SelectedID == "")
			{
				return;
			}
			string previousID = DatabaseHelper.GetPreviousID("Customer_Address", "AddressID", currentCustomerAddressID, "CustomerID", comboBoxCustomer.SelectedID);
			if (previousID != "")
			{
				currentCustomerAddressID = previousID;
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer_Address", "AddressPrintFormat", "AddressID", previousID, "CustomerID", comboBoxCustomer.SelectedID);
				if (fieldValue != null)
				{
					textBoxBilltoAddress.Text = fieldValue.ToString();
				}
				else
				{
					textBoxBilltoAddress.Clear();
				}
			}
		}

		private void CalculateTotal()
		{
			decimal total = default(decimal);
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal result3 = default(decimal);
				decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
				if (result == 0m)
				{
					decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result2);
					decimal.TryParse(row.Cells["Cost"].Value.ToString(), out result3);
					result = result2 * result3;
				}
				total += Math.Round(result, Global.CurDecimalPoints);
				if (isDataLoading)
				{
					restrictonDataload = true;
				}
			}
			if (showCreditValidation)
			{
				CheckCreditValidity(total);
			}
		}

		private void CheckCreditValidity(decimal total)
		{
			if (Factory.CustomerSystem.IsOverCreditLimit(comboBoxCustomer.SelectedID, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, total, checkOpenDN: true, dateTimePickerDate.Value))
			{
				showCreditValidation = false;
				if (!restrictonDataload)
				{
					ErrorHelper.WarningMessage("This customer's credit exceeds, may not allow further transaction.");
				}
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void itemPicToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridItems.Left + 20, dataGridItems.Top + 20);
				}
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxShippingAddressID.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomerAddress(comboBoxCustomer.SelectedID, comboBoxBillingAddress.SelectedID);
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCustomer(comboBoxCustomer.SelectedID);
		}

		private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to duplicate this document?") == DialogResult.Yes)
			{
				string text = textBoxVoucherNumber.Text;
				if (!IsDirty)
				{
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					docStatusLabel.Visible = false;
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					docStatusLabel.Visible = false;
				}
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
		}

		private void selectInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataSet invoicesForDelivery = Factory.SalesInvoiceSystem.GetInvoicesForDelivery(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.DataSource = invoicesForDelivery;
			selectDocumentDialog.Text = "Select Sales Invoice for Delivery";
			selectDocumentDialog.ValidateSelection += form_ValidateSelection;
			checked
			{
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					ClearForm();
					bool flag = false;
					int num = 0;
					StringBuilder stringBuilder = new StringBuilder();
					invoiceDNoteTable = new DataTable("Invoice");
					invoiceDNoteTable.Columns.Add("SysDocID");
					invoiceDNoteTable.Columns.Add("VoucherID");
					foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
					{
						string text = selectedRow.Cells["Doc ID"].Value.ToString();
						string text2 = selectedRow.Cells["Number"].Value.ToString();
						NameValue item = new NameValue(text2, text);
						checkedListBoxOrders.Items.Add(item);
						sourceDocType = ItemSourceTypes.SalesInvoice;
						stringBuilder.Append(text2);
						invoiceDNoteTable.Rows.Add(text, text2);
						comboBoxCustomer.Enabled = false;
						if (num < selectDocumentDialog.SelectedRows.Count - 1)
						{
							stringBuilder.Append(",");
						}
						num++;
						SalesInvoiceData salesInvoiceByID = Factory.SalesInvoiceSystem.GetSalesInvoiceByID(text, text2);
						DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesInvoiceByID, SysDocTypes.SalesInvoice, Global.CurrentUser, includeApproveUser: true);
						if (entityApprovalStatus.Tables[0].Rows.Count > 0)
						{
							bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
							int num2 = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
							if (restrictTransaction && num2 != 10)
							{
								restrictTransaction = true;
							}
							else
							{
								restrictTransaction = false;
							}
							if (restrictTransaction)
							{
								ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
							}
						}
						if (!flag)
						{
							DataRow dataRow = salesInvoiceByID.SalesInvoiceTable.Rows[0];
							textBoxNote.Text = dataRow["Note"].ToString();
							if (comboBoxCustomer.SelectedID == "")
							{
								comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
							}
							textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
							if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
							{
								comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
							{
								comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
							{
								comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
							{
								comboBoxJob.SelectedID = dataRow["JobID"].ToString();
							}
							if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
							{
								comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
							}
							flag = true;
						}
						DataTable dataTable = dataGridItems.DataSource as DataTable;
						if (!dataTable.Columns.Contains("Invoiced"))
						{
							dataTable.Columns.Remove("Quantity");
							dataTable.Columns.Add("Invoiced", typeof(decimal));
							dataTable.Columns.Add("Shipped", typeof(decimal));
							dataTable.Columns.Add("Quantity", typeof(decimal));
							if (!dataTable.Columns.Contains("SourceSysDocID"))
							{
								dataTable.Columns.Add("SourceSysDocID");
								dataTable.Columns.Add("SourceVoucherID");
								dataTable.Columns.Add("SourceRowIndex", typeof(int));
							}
						}
						if (!salesInvoiceByID.Tables.Contains("Sales_Invoice_Detail") || salesInvoiceByID.SalesInvoiceDetailTable.Rows.Count == 0)
						{
							return;
						}
						foreach (DataRow row in salesInvoiceByID.Tables["Sales_Invoice_Detail"].Rows)
						{
							decimal num3 = default(decimal);
							decimal num4 = default(decimal);
							decimal num5 = default(decimal);
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3["SourceSysDocID"] = text;
							dataRow3["SourceVoucherID"] = text2;
							dataRow3["SourceRowIndex"] = row["RowIndex"];
							dataRow3["RowSourceType"] = ItemSourceTypes.SalesInvoice;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							dataRow3["Description"] = row["Description"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							num5 = default(decimal);
							num3 = default(decimal);
							num4 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out num3);
							decimal.TryParse(row["QuantityShipped"].ToString(), out num5);
							dataRow3["Invoiced"] = num3;
							dataRow3["Shipped"] = num5;
							num3 -= num5;
							dataRow3["Quantity"] = num3;
							if (num3 < 0m)
							{
								num3 = default(decimal);
							}
							dataRow3["Amount"] = Math.Round(num3 * num4, Global.CurDecimalPoints);
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
					}
					if (dataGridItems.Rows.Count > 0)
					{
						dataGridItems.AllowAddNew = false;
					}
					textBoxNote.Text = "Invoice Numbers:\r\n";
					textBoxNote.AppendText(stringBuilder.ToString());
					AdjustGridColumnSettings();
					CalculateTotal();
				}
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			if (selectDocumentDialog != null)
			{
				List<UltraGridRow> selectedRows = selectDocumentDialog.SelectedRows;
				if (selectedRows != null)
				{
					string a = "";
					foreach (UltraGridRow item in selectedRows)
					{
						if (a == "")
						{
							a = item.Cells["Customer"].Value.ToString();
						}
						else if (a != item.Cells["Customer"].Value.ToString())
						{
							ErrorHelper.InformationMessage("Only invoices that are from same customer can be selected together.");
							selectDocumentDialog.CanClose = false;
							break;
						}
					}
				}
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
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet deliveryNoteToPrint = Factory.DeliveryNoteSystem.GetDeliveryNoteToPrint(selectedID, text, showLotDetail, ExclueZeroQtyInDN);
					if (deliveryNoteToPrint == null || deliveryNoteToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(deliveryNoteToPrint, selectedID, printTemplateName, SysDocTypes.DeliveryNote, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(deliveryNoteToPrint, selectedID, "Delivery Note", SysDocTypes.DeliveryNote, isPrint, showPrintDialog);
						}
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

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenDNThenInvoice)
			{
				createFromSalesOrderToolStripMenuItem_Click(sender, e);
			}
			else
			{
				selectInvoiceToolStripMenuItem_Click(sender, e);
			}
		}

		private void createFromSalesOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.SalesOrderSystem.GetOpenOrdersSummary(comboBoxCustomer.SelectedID, isExport: false);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Sales Order";
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			SalesOrderData salesOrderByID = Factory.SalesOrderSystem.GetSalesOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesOrderByID, SysDocTypes.SalesOrder, Global.CurrentUser, includeApproveUser: true);
			if (entityApprovalStatus.Tables[0].Rows.Count > 0)
			{
				bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
				int num = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
				if (restrictTransaction && num != 10)
				{
					restrictTransaction = true;
				}
				else
				{
					restrictTransaction = false;
				}
				if (restrictTransaction)
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				}
			}
			DataRow dataRow = salesOrderByID.SalesOrderTable.Rows[0];
			if (!string.IsNullOrEmpty(dataRow["TransactionDate"].ToString()))
			{
				dateTimePickerPO.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
				dateTimePickerPO.Checked = true;
			}
			else
			{
				dateTimePickerPO.Checked = false;
			}
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			if (!AllowCustomerChangeInDN)
			{
				comboBoxCustomer.Enabled = false;
			}
			else
			{
				comboBoxCustomer.Enabled = true;
			}
			sourceDocType = ItemSourceTypes.SalesOrder;
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShipToAddress"].ToString()))
			{
				textBoxShipto.Text = dataRow["ShipToAddress"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			currencyID = dataRow["CurrencyID"].ToString();
			comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
			textBoxPONumber.Text = dataRow["PONumber"].ToString();
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Shipped", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				if (!dataTable.Columns.Contains("SourceSysDocID"))
				{
					dataTable.Columns.Add("SourceSysDocID");
					dataTable.Columns.Add("SourceVoucherID");
					dataTable.Columns.Add("SourceRowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!salesOrderByID.Tables.Contains("Sales_Order_Detail") || salesOrderByID.SalesOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			foreach (DataRow row in salesOrderByID.Tables["Sales_Order_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["SourceSysDocID"] = text;
				dataRow3["SourceVoucherID"] = text2;
				dataRow3["SourceRowIndex"] = row["RowIndex"];
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				dataRow3["ItemType"] = row["ItemType"];
				dataRow3["RowSourceType"] = ItemSourceTypes.SalesOrder;
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["IsTrackLot"] = row["IsTrackLot"];
				dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Remarks"] = row["Remarks"];
				dataRow3["TaxGroupID"] = row["TaxGroupID"];
				if (row["TaxOption"] != DBNull.Value)
				{
					dataRow3["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
				}
				else
				{
					dataRow3["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
				}
				dataRow3["Unit"] = row["UnitID"];
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				decimal num2 = default(decimal);
				decimal d = default(decimal);
				decimal num3 = default(decimal);
				if (dataRow3["Quantity"].ToString() != "")
				{
					num2 = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
				}
				if (dataRow3["Price"].ToString() != "")
				{
					d = decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
				}
				if (row["QuantityShipped"].ToString() != "")
				{
					num3 = decimal.Parse(row["QuantityShipped"].ToString(), NumberStyles.Any);
				}
				dataRow3["Ordered"] = num2;
				dataRow3["Shipped"] = num3;
				if (isLoadZeroQty)
				{
					dataRow3["Quantity"] = 0;
				}
				else
				{
					num2 -= num3;
					dataRow3["Quantity"] = num2;
				}
				bool result = false;
				bool.TryParse(row["IsTrackLot"].ToString(), out result);
				dataRow3["Amount"] = Math.Round(num2 * d, Global.CurDecimalPoints);
				dataRow3["RefSlNo"] = row["RefSlNo"];
				dataRow3["RefText1"] = row["RefText1"];
				dataRow3["RefText2"] = row["RefText2"];
				dataRow3["RefNum1"] = row["RefNum1"];
				dataRow3["RefNum2"] = row["RefNum2"];
				dataRow3["RefDate1"] = row["RefDate1"];
				dataRow3["RefDate2"] = row["RefDate2"];
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (row2.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(row2.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Discount)
				{
					row2.Cells["Quantity"].Activation = Activation.Disabled;
				}
				if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
				{
					DataRow[] array = salesOrderByID.Tables["Sales_Order_ProductLot_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
					if (array.Length != 0)
					{
						DataSet dataSet = new DataSet();
						dataSet.Merge(array);
						DataTable tag = dataSet.Tables[0];
						row2.Cells["Quantity"].Tag = tag;
					}
					row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
			}
			if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenInvoiceThenDN)
			{
				if (!CompanyPreferences.AllowLSaleAddNew)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
			CalculateTotal();
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.DeliveryNoteListFormObj);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSalesperson(comboBoxSalesperson.SelectedID);
		}

		private void linkLabelDriver_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDriver(comboBoxDriver.SelectedID);
		}

		private void linkLabelVehicle_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicle(comboBoxVehicle.SelectedID);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntitySysDocID = comboBoxSysDoc.SelectedID;
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
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

		private void toolStripButtonBalance_Click(object sender, EventArgs e)
		{
			if (!(comboBoxCustomer.SelectedID == ""))
			{
				CustomerBalanceSnapDialog customerBalanceSnapDialog = new CustomerBalanceSnapDialog();
				customerBalanceSnapDialog.LoadData(comboBoxCustomer.SelectedID);
				customerBalanceSnapDialog.ShowDialog(this);
			}
		}

		private void createFromSalesQuoteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openQuotesSummary = Factory.SalesQuoteSystem.GetOpenQuotesSummary(comboBoxCustomer.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openQuotesSummary;
			selectDocumentDialog.Grid.DisplayLayout.Bands[0].Columns["JobName"].Hidden = !useJobCosting;
			selectDocumentDialog.Text = "Select Sales Quote";
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			SalesQuoteData salesQuoteByID = Factory.SalesQuoteSystem.GetSalesQuoteByID(sysDocID, voucherID);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(salesQuoteByID, SysDocTypes.SalesQuote, Global.CurrentUser, includeApproveUser: true);
			if (entityApprovalStatus.Tables[0].Rows.Count > 0)
			{
				bool.TryParse(entityApprovalStatus.Tables[0].Rows[0]["AllownextTransaction"].ToString(), out restrictTransaction);
				int num = int.Parse(entityApprovalStatus.Tables[0].Rows[0]["ApprovalStatus"].ToString());
				if (restrictTransaction && num != 10)
				{
					restrictTransaction = true;
				}
				else
				{
					restrictTransaction = false;
				}
				if (restrictTransaction)
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNotApproved);
				}
			}
			DataRow dataRow = salesQuoteByID.SalesQuoteTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["CustomerID"].ToString();
			}
			textBoxBilltoAddress.Text = dataRow["CustomerAddress"].ToString();
			comboBoxCustomer.Enabled = false;
			sourceDocType = ItemSourceTypes.None;
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingAddressID"].ToString()))
			{
				comboBoxShippingAddressID.SelectedID = dataRow["ShippingAddressID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["SalespersonID"].ToString()))
			{
				comboBoxSalesperson.SelectedID = dataRow["SalespersonID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["JobID"].ToString()))
			{
				comboBoxJob.SelectedID = dataRow["JobID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["CostCategoryID"].ToString()))
			{
				comboBoxCostCategory.SelectedID = dataRow["CostCategoryID"].ToString();
			}
			comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
			currencyID = dataRow["CurrencyID"].ToString();
			SelectItemRowsDialog selectItemRowsDialog = new SelectItemRowsDialog();
			DataSet dataSet = new DataSet();
			dataSet.Merge(salesQuoteByID.SalesQuoteDetailTable, preserveChanges: true, MissingSchemaAction.Add);
			selectItemRowsDialog.DataSource = dataSet;
			selectItemRowsDialog.IsMultiSelect = true;
			if (selectItemRowsDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxPONumber.Text = dataRow["PONumber"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (selectItemRowsDialog.SelectedRows.Count != 0)
				{
					ItemTaxOptions itemTaxOptions = ItemTaxOptions.Taxable;
					foreach (UltraGridRow selectedRow in selectItemRowsDialog.SelectedRows)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["Item Code"] = selectedRow.Cells["ProductID"].Value;
						dataRow2["Quantity"] = selectedRow.Cells["Quantity"].Value;
						dataRow2["ItemType"] = selectedRow.Cells["ItemType"].Value;
						dataRow2["Description"] = selectedRow.Cells["Description"].Value;
						dataRow2["TaxGroupID"] = selectedRow.Cells["TaxGroupID"].Value;
						itemTaxOptions = (ItemTaxOptions)((!selectedRow.Cells["TaxOption"].Value.ToString().IsNullOrEmpty()) ? byte.Parse(selectedRow.Cells["TaxOption"].Value.ToString()) : 0);
						dataRow2["TaxOption"] = itemTaxOptions;
						dataRow2["Unit"] = selectedRow.Cells["UnitID"].Value;
						dataRow2["Price"] = selectedRow.Cells["UnitPrice"].Value.ToString();
						decimal d = default(decimal);
						decimal d2 = default(decimal);
						if (dataRow2["Quantity"].ToString() != "")
						{
							d = decimal.Parse(dataRow2["Quantity"].ToString(), NumberStyles.Any);
						}
						if (dataRow2["Price"].ToString() != "")
						{
							d2 = decimal.Parse(dataRow2["Price"].ToString(), NumberStyles.Any);
						}
						dataRow2["Amount"] = Math.Round(d * d2, Global.CurDecimalPoints);
						dataRow2.EndEdit();
						dataTable.Rows.Add(dataRow2);
					}
					dataTable.AcceptChanges();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						ItemTypes itemTypes = ItemTypes.Inventory;
						if (row.Cells["ItemType"].Value.ToString() != "")
						{
							itemTypes = (ItemTypes)checked((byte)int.Parse(row.Cells["ItemType"].Value.ToString()));
						}
						if (itemTypes == ItemTypes.Discount)
						{
							row.Cells["Quantity"].Activation = Activation.Disabled;
						}
					}
					AdjustGridColumnSettings();
					CalculateTotal();
				}
			}
		}

		private void checkedListBoxOrders_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			NameValue nameValue = checkedListBoxOrders.SelectedItem as NameValue;
			if (nameValue != null)
			{
				new FormHelper().EditTransaction(TransactionListType.SalesOrder, nameValue.ID, nameValue.Name);
			}
		}

		private void labelcostcategory_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCostCategory(comboBoxCostCategory.SelectedID);
		}

		private void labelJob_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditJob(comboBoxJob.SelectedID);
		}

		private void ultraFormattedLinkShipping_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void comboBoxVehicle_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxVehicle.SelectedID == "")
			{
				textBoxVechicleName.ReadOnly = false;
			}
			else
			{
				textBoxVechicleName.ReadOnly = true;
			}
		}

		private void toolStripButtonInfo_MouseEnter(object sender, EventArgs e)
		{
			toolTip.OwnerDraw = true;
			toolTip.Draw += tooltip_Draw;
			toolTip.Popup += tooltip_Popup;
			toolTip.AutoPopDelay = 5000;
			toolTip.AutomaticDelay = 5000;
			toolTip.InitialDelay = 500;
			toolTip.IsBalloon = true;
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Customer", "DeliveryInstructions", "CustomerID", comboBoxCustomer.SelectedID);
			if (fieldValue.ToString() != "" || fieldValue.ToString() != string.Empty)
			{
				toolStripButtonInfo.Visible = true;
				toolTip.Show(fieldValue.ToString(), toolStrip1.Parent, 550, 10);
			}
			else
			{
				toolTip.Show("", toolStrip1);
				toolStripButtonInfo.Visible = false;
			}
		}

		private void toolStripButtonInfo_MouseLeave(object sender, EventArgs e)
		{
			toolTip.Hide(textBoxCustomerName);
		}

		private void tooltip_Draw(object sender, DrawToolTipEventArgs e)
		{
			Rectangle bounds = e.Bounds;
			bounds.Offset(200, 400);
			toolTip.BackColor = Color.Yellow;
			DrawToolTipEventArgs drawToolTipEventArgs = new DrawToolTipEventArgs(e.Graphics, e.AssociatedWindow, e.AssociatedControl, bounds, e.ToolTipText, Color.Yellow, Color.Yellow, new Font("Courier New", 10f, FontStyle.Bold));
			drawToolTipEventArgs.DrawBackground();
			drawToolTipEventArgs.DrawBorder();
			drawToolTipEventArgs.DrawText(TextFormatFlags.TextBoxControl);
		}

		private void tooltip_Popup(object sender, PopupEventArgs e)
		{
		}

		private void createFromTransactionListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet itemTransactionList = Factory.ItemTransactionSystem.GetItemTransactionList(comboBoxCustomer.SelectedID, SysDocTypes.DeliveryNote);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = itemTransactionList;
			selectDocumentDialog.Text = "Select Purchase Order";
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.ValidateSelection += form_ValidateSelection;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			string text = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
			string text2 = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
			ItemTransactionData itemTransactionByID = Factory.ItemTransactionSystem.GetItemTransactionByID(text, text2);
			DataRow dataRow = itemTransactionByID.ItemTransactionTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			if (comboBoxCustomer.SelectedID == "")
			{
				comboBoxCustomer.SelectedID = dataRow["PartyID"].ToString();
			}
			comboBoxSysDoc.SelectedID = text;
			textBoxVoucherNumber.Text = text2;
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
			textBoxNote.Text = dataRow["Note"].ToString();
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			DataTable dtDetails = dataTable.Copy();
			if (SetTransasctionTable(dtDetails, itemTransactionByID.ItemTransactionDetailTable).Rows.Count > 0)
			{
				if (!dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Quantity");
					dataTable.Columns.Add("Ordered", typeof(decimal));
					dataTable.Columns.Add("Shipped", typeof(decimal));
					dataTable.Columns.Add("Quantity", typeof(decimal));
					if (!dataTable.Columns.Contains("SourceSysDocID"))
					{
						dataTable.Columns.Add("SourceSysDocID");
						dataTable.Columns.Add("SourceVoucherID");
						dataTable.Columns.Add("SourceRowIndex", typeof(int));
					}
				}
				dataTable.Rows.Clear();
				_ = selectDocumentDialog.SelectedRows;
				foreach (DataRow row in dtTransaction.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["Item Code"];
					dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
					dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
					dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
					if (row["Quantity"] != DBNull.Value)
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					else
					{
						dataRow3["Quantity"] = row["Quantity"];
					}
					dataRow3["Job"] = row["Job"];
					dataRow3["ListSysDocID"] = row["ListSysDocID"];
					dataRow3["ListVoucherID"] = row["ListVoucherID"];
					dataRow3["ListRowIndex"] = row["ListRowIndex"];
					dataRow3["Description"] = row["Description"];
					decimal num = default(decimal);
					decimal num2 = default(decimal);
					decimal num3 = default(decimal);
					if (dataRow3["Quantity"].ToString() != "")
					{
						num = decimal.Parse(dataRow3["Quantity"].ToString(), NumberStyles.Any);
					}
					if (dataRow3["Price"].ToString() != "")
					{
						decimal.Parse(dataRow3["Price"].ToString(), NumberStyles.Any);
					}
					if (row["Shipped"].ToString() != "")
					{
						num2 = decimal.Parse(row["Shipped"].ToString(), NumberStyles.Any);
					}
					if (row["Ordered"].ToString() != "")
					{
						num3 = decimal.Parse(row["Ordered"].ToString(), NumberStyles.Any);
					}
					dataRow3["Ordered"] = num3;
					dataRow3["Shipped"] = num2;
					num -= num2;
					dataRow3["Quantity"] = num;
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
			}
			else
			{
				dataTable.Rows.Clear();
				if (itemTransactionByID.Tables.Contains("Item_Transaction_Detail") && itemTransactionByID.ItemTransactionDetailTable.Rows.Count != 0)
				{
					_ = selectDocumentDialog.SelectedRows;
					foreach (DataRow row2 in itemTransactionByID.Tables["Item_Transaction_Detail"].Rows)
					{
						DataRow dataRow5 = dataTable.NewRow();
						dataRow5["Item Code"] = row2["ProductID"];
						dataRow5["SourceSysDocID"] = "";
						dataRow5["SourceVoucherID"] = "";
						dataRow5["SourceRowIndex"] = 0;
						if (row2["Quantity"] != DBNull.Value)
						{
							dataRow5["Quantity"] = row2["Quantity"];
						}
						decimal productSalesPrice = Factory.ProductSystem.GetProductSalesPrice(row2["ProductID"].ToString(), comboBoxCustomer.SelectedID);
						dataRow5["Price"] = productSalesPrice;
						dataRow5["Unit"] = row2["UnitID"];
						dataRow5["Location"] = dataRow["LocationID"].ToString();
						dataRow5["Description"] = row2["Description"];
						dataRow5["ListSysDocID"] = row2["SysDocID"];
						dataRow5["ListVoucherID"] = row2["VoucherID"];
						dataRow5["ListRowIndex"] = row2["RowIndex"];
						dataRow5.EndEdit();
						dataTable.Rows.Add(dataRow5);
					}
				}
			}
		}

		private DataTable SetTransasctionTable(DataTable DtDetails, DataTable DtTransactionDetails)
		{
			List<translist> list = new List<translist>();
			decimal num = default(decimal);
			string text = "";
			string text2 = "";
			int num2 = 0;
			checked
			{
				for (int i = 0; i < DtTransactionDetails.Rows.Count; i++)
				{
					num = decimal.Parse(DtTransactionDetails.Rows[i]["Quantity"].ToString());
					text = DtTransactionDetails.Rows[i]["Item Code"].ToString();
					DtTransactionDetails.Rows[i]["SysDocID"].ToString();
					DtTransactionDetails.Rows[i]["VoucherID"].ToString();
					text2 = DtTransactionDetails.Rows[i]["Description"].ToString();
					num2 = int.Parse(DtTransactionDetails.Rows[i]["Rowindex"].ToString());
					list.Add(new translist(text, num, Istaken: false, text2, num2));
				}
				for (int j = 0; j < DtDetails.Rows.Count; j++)
				{
					text = DtDetails.Rows[j]["Item Code"].ToString();
					decimal num3 = default(decimal);
					string text3 = "";
					string text4 = "";
					text3 = DtTransactionDetails.Rows[1]["SysDocID"].ToString();
					text4 = DtTransactionDetails.Rows[1]["VoucherID"].ToString();
					foreach (translist item in list)
					{
						DataView defaultView = DtDetails.DefaultView;
						defaultView.RowFilter = "[Item Code]='" + item.ItemCode.ToString() + "'";
						DataSet dataSet = new DataSet();
						DataTable dataTable = defaultView.ToTable();
						dataSet.Tables.Add(dataTable);
						if (item.ItemCode == text && !item.Istaken)
						{
							decimal.Parse(DtDetails.Rows[j]["Ordered"].ToString());
							num = item.Trqty;
							num3 = num;
							DtDetails.Rows[j]["Quantity"] = num3;
							DtDetails.Rows[j]["ListSysDocID"] = text3;
							DtDetails.Rows[j]["ListVoucherID"] = text4;
							DtDetails.Rows[j]["ListRowIndex"] = item.Trindex;
							DtDetails.AcceptChanges();
							item.Istaken = true;
						}
						else if (!item.ItemCode.Contains(text) && !item.Istaken && dataTable.Rows.Count == 0)
						{
							DataRow dataRow = DtDetails.NewRow();
							dataRow["Item Code"] = item.ItemCode;
							dataRow["ListSysDocID"] = text3;
							dataRow["ListVoucherID"] = text4;
							dataRow["Description"] = item.ItemDescription;
							dataRow["ListRowIndex"] = item.Trindex;
							dataRow["Quantity"] = item.Trqty;
							dataRow.EndEdit();
							DtDetails.Rows.Add(dataRow);
							DtDetails.AcceptChanges();
							item.Istaken = true;
						}
					}
					DtDetails.AcceptChanges();
				}
				DtDetails.AcceptChanges();
				dtTransaction = DtDetails;
				return dtTransaction;
			}
		}

		public void GetcustomerData()
		{
			DataSet customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(comboBoxCustomer.SelectedID);
			if (customerSnapBalance != null && customerSnapBalance.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = customerSnapBalance.Tables[0].Rows[0];
				if (dataRow["CreditAmount"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["CreditAmount"].ToString(), out creditAmount);
				}
				if (dataRow["CreditLimitType"] != DBNull.Value)
				{
					creditLimitType = byte.Parse(dataRow["CreditLimitType"].ToString());
				}
				if (dataRow["PDCAmount"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["PDCAmount"].ToString(), out pdcBalance);
				}
				if (dataRow["PDCUnsecuredLimitAmount"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["PDCUnsecuredLimitAmount"].ToString(), out pdcUnsecuredLimitAmount);
				}
				if (!dataRow["TempCL"].IsDBNullOrEmpty())
				{
					tempCL = decimal.Parse(dataRow["TempCL"].ToString());
				}
				if (dataRow["LimitPDCUnsecured"] != DBNull.Value)
				{
					limitPDCUnsecured = bool.Parse(dataRow["LimitPDCUnsecured"].ToString());
				}
			}
		}

		private void toolStripButtonMultiPreview_Click(object sender, EventArgs e)
		{
			AttachementDetailsForm attachementDetailsForm = new AttachementDetailsForm();
			attachementDetailsForm.SysDocID = comboBoxSysDoc.SelectedID;
			attachementDetailsForm.VoucherID = textBoxVoucherNumber.Text;
			attachementDetailsForm.SysDocType = SysDocTypes.DeliveryNote;
			attachementDetailsForm.FormType = "Transaction";
			attachementDetailsForm.Show();
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
		}

		private void loadDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CanClose())
			{
				LoadDraft();
			}
		}

		private void comboBoxVehicle_InitializeLayout(object sender, InitializeLayoutEventArgs e)
		{
		}

		private void textBoxVechicleName_TextChanged(object sender, EventArgs e)
		{
		}

		private void label5_Click(object sender, EventArgs e)
		{
		}

		private void simpleButtonSave_Click(object sender, EventArgs e)
		{
			if (SaveDraft())
			{
				ClearForm();
			}
		}

		private void GetUpdatedDocumentNo()
		{
			IsTempRecordexists = Factory.SystemDocumentSystem.ExistDocumentNumber("Temporary_Transaction", "VoucherID", SystemDocID, textBoxVoucherNumber.Text);
			IsDocExists = Factory.SystemDocumentSystem.ExistDocumentNumber("Delivery_Note", "VoucherID", SystemDocID, textBoxVoucherNumber.Text);
			if ((IsTempRecordexists || IsDocExists) && IsNewRecord && !IsTemporaryTransaction)
			{
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
			}
		}

		private bool SaveDraft()
		{
			try
			{
				GetUpdatedDocumentNo();
				if (!ValidateData())
				{
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				if (GetData())
				{
					string text = "";
					text = SystemDocID + textBoxVoucherNumber.Text + "-" + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString() + "-" + Global.CurrentUser;
					return Global.CompanySettings.SaveTransactionDraftForDashBoard(currentData, text, SysDocTypes.DeliveryNote, SystemDocID, textBoxVoucherNumber.Text, TempAutoKeyID, isNewRecord);
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
				DataTable dataTable = Factory.SettingSystem.GetSettingsListData("", 24.ToString()).Tables[0].DefaultView.ToTable(true, "SysDocID", "VoucherID", "CustomerID", "TransactionDate", "AutoKeyID");
				if (dataTable == null && dataTable.Rows.Count == 0)
				{
					return false;
				}
				DataSet dataSet = new DataSet();
				dataSet.Tables.Add(dataTable);
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSet;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = currentkey = Factory.SettingSystem.GetTempTransactionByKey(selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString() + selectDocumentDialog.SelectedRow.Cells["VoucherID"].Value.ToString());
					DataSet dataSet2 = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.DeliveryNote);
					comboBoxSysDoc.SelectedID = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
					TempAutoKeyID = int.Parse(selectDocumentDialog.SelectedRow.Cells["AutoKeyID"].Value.ToString());
					currentData = (dataSet2 as DeliveryNoteData);
					IsTemporaryTransaction = true;
					currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
					comboBoxSysDoc.ReadOnly = true;
					FillData();
					buttonSave.Enabled = true;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void simpleButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("Temporary_Transaction", "SKey", currentkey, "SName", 24.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(nextID == ""))
			{
				string key = nextID;
				if (!string.IsNullOrEmpty(nextID))
				{
					currentkey = nextID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.DeliveryNote);
				currentData = (dataSet as DeliveryNoteData);
				if (!currentData.Tables[0].Columns.Contains("IsInvoiced"))
				{
					currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
				}
				comboBoxSysDoc.ReadOnly = true;
				IsTemporaryTransaction = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButtonFind_Click(object sender, EventArgs e)
		{
			LoadDraft();
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string selectedID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				InventorySalesStatisticForm inventorySalesStatisticForm = new InventorySalesStatisticForm();
				inventorySalesStatisticForm.SelectedID = selectedID;
				inventorySalesStatisticForm.Show();
				inventorySalesStatisticForm.BringToFront();
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void createfromBintoolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet availbleProductBin = Factory.ProductSystem.GetAvailbleProductBin(IsBinOnly: true, "");
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = availbleProductBin;
			selectDocumentDialog.Text = "Select Available Bin";
			DialogResult num = selectDocumentDialog.ShowDialog(this);
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (num == DialogResult.OK)
			{
				ClearForm();
				binID = selectDocumentDialog.SelectedRow.Cells["BinID"].Value.ToString();
				IsBinSelected = true;
				DataSet availbleProductBin2 = Factory.ProductSystem.GetAvailbleProductBin(IsBinOnly: false, binID);
				foreach (DataRow row in availbleProductBin2.Tables["AvailProductBin"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Item Code"] = row["ProductID"];
					dataRow2["Description"] = row["Description"];
					dataRow2["Quantity"] = row["Quantity"];
					textBoxRef1.Text = binID;
					textBoxNote.Text = row["BinName"].ToString();
					dataRow2["IsTrackLot"] = true;
					dataRow2["IsTrackSerial"] = true;
					dataRow2["Location"] = row["LocationID"];
					dataRow2["TaxGroupID"] = row["TaxGroupID"];
					if (row["TaxOption"] != DBNull.Value)
					{
						dataRow2["TaxOption"] = byte.Parse(row["TaxOption"].ToString());
					}
					else
					{
						dataRow2["TaxOption"] = ItemTaxOptions.BasedOnCustomer;
					}
					dataRow2["Unit"] = row["UnitID"];
					decimal num2 = default(decimal);
					decimal d = default(decimal);
					decimal num3 = default(decimal);
					if (dataRow2["Quantity"].ToString() != "")
					{
						num2 = decimal.Parse(dataRow2["Quantity"].ToString(), NumberStyles.Any);
					}
					if (dataRow2["Price"].ToString() != "")
					{
						d = decimal.Parse(dataRow2["Price"].ToString(), NumberStyles.Any);
					}
					num2 -= num3;
					dataRow2["Quantity"] = num2;
					dataRow2["Amount"] = Math.Round(num2 * d, Global.CurDecimalPoints);
					bool result = false;
					bool.TryParse(row["IsTrackLot"].ToString(), out result);
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
					{
						DataRow[] array = availbleProductBin2.Tables[1].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "'");
						if (array.Length != 0)
						{
							DataSet dataSet = new DataSet();
							dataSet.Merge(array);
							DataTable tag = dataSet.Tables[0];
							row2.Cells["Quantity"].Tag = tag;
						}
						row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
					}
				}
				if (IsBinSelected)
				{
					foreach (UltraGridRow row3 in dataGridItems.Rows)
					{
						DataSet productAvailableLotsAndBins = Factory.ProductSystem.GetProductAvailableLotsAndBins(row3.Cells["Item Code"].Value.ToString(), row3.Cells["Location"].Value.ToString(), comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, comboBoxCustomer.SelectedID);
						DataTable dataTable2 = InventoryTransactionData.AddProductLotIssueDetailTable(new DataSet()).Tables[0];
						for (int i = 0; i < productAvailableLotsAndBins.Tables[0].Rows.Count; i = checked(i + 1))
						{
							DataRow dataRow3 = productAvailableLotsAndBins.Tables[0].Rows[i];
							DataRow dataRow4 = dataTable2.NewRow();
							dataRow4["LotNumber"] = dataRow3["LotNumber"];
							if (dataRow3["SourceLotNumber"] != null && !string.IsNullOrWhiteSpace(dataRow3["SourceLotNumber"].ToString()))
							{
								dataRow4["SourceLotNumber"] = dataRow3["SourceLotNumber"];
							}
							else
							{
								dataRow4["SourceLotNumber"] = DBNull.Value;
							}
							dataRow4["Reference"] = dataRow3["Reference"];
							dataRow4["ProductID"] = row3.Cells["Item Code"].Value.ToString();
							dataRow4["LocationID"] = row3.Cells["Location"].Value.ToString();
							dataRow4["Cost"] = dataRow3["Cost"];
							dataRow4["SoldQty"] = dataRow3["LotQty"];
							dataRow4["BinID"] = dataRow3["BinID"];
							dataRow4["RackID"] = dataRow3["RackID"];
							dataRow4["Reference2"] = dataRow3["Reference2"];
							dataRow4.EndEdit();
							dataTable2.Rows.Add(dataRow4);
							row3.Cells["Quantity"].Tag = dataTable2;
						}
					}
				}
			}
		}

		private void toolStripButtonExcelImport_Click(object sender, EventArgs e)
		{
			bool result = false;
			bool.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString(), out result);
			if (result)
			{
				DataSet dataSet = dataGridItems.ImportFromExcel(autoFill: true);
				string text = "";
				string text2 = "";
				decimal num = default(decimal);
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (row.Cells["Item Code"].Value.ToString() != "")
					{
						text = row.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text))
						{
							string value = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text).ToString() ?? null;
							row.Cells["Description"].Value = value;
							string value2 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text).ToString() ?? null;
							row.Cells["Unit"].Value = value2;
							text2 = row.Cells["LotNumber"].Value.ToString();
							num = decimal.Parse(row.Cells["Quantity"].Value.ToString());
							decimal d = decimal.Parse(row.Cells["Price"].Value.ToString());
							row.Cells["Amount"].Value = (num * d).ToString(Format.TotalAmountFormat);
							if (row.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
							ItemTaxOptions itemTaxOptions = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text).ToString() ?? null);
							row.Cells["TaxOption"].Value = itemTaxOptions;
							switch (itemTaxOptions)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
						}
					}
					DataRow[] array = dataSet.Tables[0].Select("[Item Code]='" + text + "'    AND LotNumber='" + text2 + "'");
					DataTable dataTable = new DataTable();
					dataTable = dataSet.Tables[0].Clone();
					for (int i = 0; i < array.Length; i = checked(i + 1))
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["Item Code"] = array[i]["Item Code"];
						dataRow["LotNumber"] = array[i]["LotNumber"];
						dataRow["BinID"] = array[i]["BinID"];
						if (!dataTable.Columns.Contains("ProductID"))
						{
							dataTable.Columns.Add("ProductID");
						}
						dataRow["ProductID"] = array[i]["Item Code"];
						if (!dataTable.Columns.Contains("LocationID"))
						{
							dataTable.Columns.Add("LocationID");
							dataRow["LocationID"] = array[i]["Location"];
						}
						if (!dataTable.Columns.Contains("SourceLotNumber"))
						{
							dataTable.Columns.Add("SourceLotNumber");
						}
						if (!dataTable.Columns.Contains("SoldQty"))
						{
							dataTable.Columns.Add("SoldQty");
							dataRow["SoldQty"] = num;
						}
						dataRow["Cost"] = array[i]["Cost"];
						dataTable.Rows.Add(dataRow);
					}
					row.Cells["Quantity"].Tag = dataTable;
					row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["LotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["BinID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceLotNumber"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SoldQty"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Reference2"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ProductionDate"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ExpiryDate"].Hidden = true;
			}
			else
			{
				dataGridItems.ImportFromExcel(autoFill: true);
				foreach (UltraGridRow row2 in dataGridItems.Rows)
				{
					if (row2.Cells["Item Code"].Value.ToString() != "")
					{
						string text3 = row2.Cells["Item Code"].Value.ToString();
						if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", text3))
						{
							string value3 = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", text3).ToString() ?? null;
							row2.Cells["Description"].Value = value3;
							string value4 = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", text3).ToString() ?? null;
							row2.Cells["Unit"].Value = value4;
							decimal d2 = decimal.Parse(row2.Cells["Quantity"].Value.ToString());
							decimal d3 = decimal.Parse(row2.Cells["Price"].Value.ToString());
							ItemTaxOptions itemTaxOptions2 = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text3).ToString() ?? null);
							row2.Cells["TaxOption"].Value = itemTaxOptions2;
							switch (itemTaxOptions2)
							{
							case ItemTaxOptions.BasedOnCustomer:
								row2.Cells["TaxGroupID"].Value = comboBoxPayeeTaxGroup.SelectedID;
								break;
							case ItemTaxOptions.Taxable:
								row2.Cells["TaxGroupID"].Value = (Factory.DatabaseSystem.GetFieldValue("Product", "TaxGroupID", "ProductID", text3).ToString() ?? null);
								break;
							case ItemTaxOptions.NonTaxable:
								row2.Cells["TaxGroupID"].Value = DBNull.Value;
								break;
							}
							row2.Cells["Amount"].Value = (d2 * d3).ToString(Format.TotalAmountFormat);
							if (row2.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotal();
							}
						}
					}
				}
			}
			CalculateTotal();
		}

		private void simpleButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Temporary_Transaction", "SKey", "SName", 24.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(firstID == ""))
			{
				string key = firstID;
				if (!string.IsNullOrEmpty(firstID))
				{
					currentkey = firstID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.DeliveryNote);
				currentData = (dataSet as DeliveryNoteData);
				if (!currentData.Tables[0].Columns.Contains("IsInvoiced"))
				{
					currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
				}
				comboBoxSysDoc.ReadOnly = true;
				IsTemporaryTransaction = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Temporary_Transaction", "SKey", currentkey, "SName", 24.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(previousID == ""))
			{
				string key = previousID;
				if (!string.IsNullOrEmpty(previousID))
				{
					currentkey = previousID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.DeliveryNote);
				currentData = (dataSet as DeliveryNoteData);
				if (!currentData.Tables[0].Columns.Contains("IsInvoiced"))
				{
					currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
				}
				comboBoxSysDoc.ReadOnly = true;
				IsTemporaryTransaction = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void simpleButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Temporary_Transaction", "SKey", "SName", 24.ToString(), "SysDocID", comboBoxSysDoc.SelectedID);
			if (!(lastID == ""))
			{
				string key = lastID;
				if (!string.IsNullOrEmpty(lastID))
				{
					currentkey = lastID;
				}
				DataSet dataSet = Global.CompanySettings.LoadTransactionDraftForTemporary(key, SysDocTypes.DeliveryNote);
				currentData = (dataSet as DeliveryNoteData);
				if (!currentData.Tables[0].Columns.Contains("IsInvoiced"))
				{
					currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
				}
				comboBoxSysDoc.ReadOnly = true;
				IsTemporaryTransaction = true;
				TempAutoKeyID = Factory.SettingSystem.GetTemporaryAutoKeyID(key);
				FillData();
			}
		}

		private void grantEditPermissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> selectedCodes = new List<string>();
			dataSet = Factory.UserSystem.GetUserComboList();
			bool flag = false;
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedCodes = selectedCodes;
			selectDocumentDialog.Text = "Select User";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				selectedCodes = selectDocumentDialog.SelectedCodes;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Code"].Value.ToString();
					string name = selectedRow.Cells["Name"].Value.ToString();
					if (0 == 0)
					{
						new NameValue(name, text);
						flag = Factory.DeliveryNoteSystem.ModifyTransactions(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text, text, isModify: true, "");
					}
				}
				if (flag)
				{
					ErrorHelper.WarningMessage("User can able to modify");
				}
			}
		}

		private void deleteTempTransactionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (currentkey == "")
			{
				ErrorHelper.WarningMessage("No Temporary transaction has selected!");
			}
			if (!string.IsNullOrEmpty(currentkey) && Factory.SettingSystem.DeleteSettingTemporary(currentkey, "", ""))
			{
				ClearForm();
				ErrorHelper.WarningMessage("Temporary transaction Deleted Succesfully!");
			}
		}

		private void toolStripMenuRelationshipMap_Click(object sender, EventArgs e)
		{
			RelationshipMapForm relationshipMapForm = new RelationshipMapForm();
			relationshipMapForm.sysDocID = comboBoxSysDoc.SelectedID;
			relationshipMapForm.voucherID = textBoxVoucherNumber.Text;
			relationshipMapForm.Show();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.DeliveryNoteForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripButtonMultiPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonBalance = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuRelationshipMap = new System.Windows.Forms.ToolStripMenuItem();
			selectInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesQuoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createfromBintoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromTransactionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			grantEditPermissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteTempTransactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
			toolStripButtonApproval = new System.Windows.Forms.ToolStripButton();
			toolStripLabelApproval = new System.Windows.Forms.ToolStripLabel();
			toolStripSeparatorApproval = new System.Windows.Forms.ToolStripSeparator();
			panelButtons = new System.Windows.Forms.Panel();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			panelxml = new System.Windows.Forms.Panel();
			simpleButtonSave = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonFind = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonLast = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonNext = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonPrevious = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonFirst = new DevExpress.XtraEditors.SimpleButton();
			dateTimePickerPO = new Micromind.UISupport.MMSDateTimePicker(components);
			label6 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			textBoxVechicleName = new System.Windows.Forms.TextBox();
			labelcostcategory = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelJob = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkShipping = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			textBoxShipto = new Micromind.UISupport.MMTextBox();
			comboBoxBillingAddress = new Micromind.DataControls.CustomerAddressComboBox();
			comboBoxShippingAddressID = new Micromind.DataControls.CustomerAddressComboBox();
			textBoxBilltoAddress = new Micromind.UISupport.MMTextBox();
			comboBoxCostCategory = new Micromind.DataControls.CostCategoryComboBox();
			label4 = new System.Windows.Forms.Label();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			comboBoxVehicle = new Micromind.DataControls.VehicleComboBox();
			linkLabelVehicle = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxDriver = new Micromind.DataControls.DriverComboBox();
			linkLabelDriver = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			comboBoxSalesperson = new Micromind.DataControls.SalespersonComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxPONumber = new System.Windows.Forms.TextBox();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxOrders = new System.Windows.Forms.ListBox();
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			comboBoxSpecification = new Micromind.DataControls.ProductSpecificationComboBox();
			docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
			ComboBoxitemJob = new Micromind.DataControls.JobComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			panelxml.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[26]
			{
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator6,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripButtonMultiPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonBalance,
				toolStripSeparator10,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripDropDownButton1,
				toolStripButtonInfo,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(898, 31);
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
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonMultiPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonMultiPreview.Image = Micromind.ClientUI.Properties.Resources.multi_doc;
			toolStripButtonMultiPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonMultiPreview.Name = "toolStripButtonMultiPreview";
			toolStripButtonMultiPreview.Size = new System.Drawing.Size(28, 28);
			toolStripButtonMultiPreview.Text = "MultiPreview";
			toolStripButtonMultiPreview.Click += new System.EventHandler(toolStripButtonMultiPreview_Click);
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButtonBalance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonBalance.Image = (System.Drawing.Image)resources.GetObject("toolStripButtonBalance.Image");
			toolStripButtonBalance.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonBalance.Name = "toolStripButtonBalance";
			toolStripButtonBalance.Size = new System.Drawing.Size(28, 28);
			toolStripButtonBalance.Text = "Check Customer Balance";
			toolStripButtonBalance.Click += new System.EventHandler(toolStripButtonBalance_Click);
			toolStripSeparator10.Name = "toolStripSeparator10";
			toolStripSeparator10.Size = new System.Drawing.Size(6, 31);
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator8,
				saveAsDraftToolStripMenuItem,
				toolStripMenuItem2,
				toolStripSeparator9,
				toolStripMenuRelationshipMap,
				selectInvoiceToolStripMenuItem,
				createFromSalesOrderToolStripMenuItem,
				createFromSalesQuoteToolStripMenuItem,
				createfromBintoolStripMenuItem,
				createFromTransactionListToolStripMenuItem,
				toolStripSeparator4,
				grantEditPermissionToolStripMenuItem,
				deleteTempTransactionToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(225, 6);
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new System.Drawing.Size(228, 22);
			toolStripMenuItem2.Text = "Load Draft...";
			toolStripMenuItem2.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator9.Name = "toolStripSeparator9";
			toolStripSeparator9.Size = new System.Drawing.Size(225, 6);
			toolStripMenuRelationshipMap.Name = "toolStripMenuRelationshipMap";
			toolStripMenuRelationshipMap.Size = new System.Drawing.Size(228, 22);
			toolStripMenuRelationshipMap.Text = "RelationshipMap";
			toolStripMenuRelationshipMap.Click += new System.EventHandler(toolStripMenuRelationshipMap_Click);
			selectInvoiceToolStripMenuItem.Name = "selectInvoiceToolStripMenuItem";
			selectInvoiceToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			selectInvoiceToolStripMenuItem.Text = "Create From Sales Invoice...";
			selectInvoiceToolStripMenuItem.Click += new System.EventHandler(selectInvoiceToolStripMenuItem_Click);
			createFromSalesOrderToolStripMenuItem.Name = "createFromSalesOrderToolStripMenuItem";
			createFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			createFromSalesOrderToolStripMenuItem.Text = "Create from Sales Order...";
			createFromSalesOrderToolStripMenuItem.Click += new System.EventHandler(createFromSalesOrderToolStripMenuItem_Click);
			createFromSalesQuoteToolStripMenuItem.Name = "createFromSalesQuoteToolStripMenuItem";
			createFromSalesQuoteToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			createFromSalesQuoteToolStripMenuItem.Text = "Create from Sales Quote...";
			createFromSalesQuoteToolStripMenuItem.Click += new System.EventHandler(createFromSalesQuoteToolStripMenuItem_Click);
			createfromBintoolStripMenuItem.Name = "createfromBintoolStripMenuItem";
			createfromBintoolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			createfromBintoolStripMenuItem.Text = "Create from Bin..";
			createfromBintoolStripMenuItem.Click += new System.EventHandler(createfromBintoolStripMenuItem_Click);
			createFromTransactionListToolStripMenuItem.Name = "createFromTransactionListToolStripMenuItem";
			createFromTransactionListToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			createFromTransactionListToolStripMenuItem.Text = "Create from Transaction List..";
			createFromTransactionListToolStripMenuItem.Click += new System.EventHandler(createFromTransactionListToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
			grantEditPermissionToolStripMenuItem.Name = "grantEditPermissionToolStripMenuItem";
			grantEditPermissionToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			grantEditPermissionToolStripMenuItem.Text = "Grant Edit Permission";
			grantEditPermissionToolStripMenuItem.Click += new System.EventHandler(grantEditPermissionToolStripMenuItem_Click);
			deleteTempTransactionToolStripMenuItem.Name = "deleteTempTransactionToolStripMenuItem";
			deleteTempTransactionToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			deleteTempTransactionToolStripMenuItem.Text = "Delete Temp Transaction";
			deleteTempTransactionToolStripMenuItem.Click += new System.EventHandler(deleteTempTransactionToolStripMenuItem_Click);
			toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInfo.Image = Micromind.ClientUI.Properties.Resources.Alert;
			toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInfo.Name = "toolStripButtonInfo";
			toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInfo.Visible = false;
			toolStripButtonInfo.MouseEnter += new System.EventHandler(toolStripButtonInfo_MouseEnter);
			toolStripButtonInfo.MouseLeave += new System.EventHandler(toolStripButtonInfo_MouseLeave);
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
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 553);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(898, 40);
			panelButtons.TabIndex = 16;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(216, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 24);
			buttonVoid.TabIndex = 18;
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
			buttonDelete.TabIndex = 19;
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
			linePanelDown.Size = new System.Drawing.Size(898, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(788, 8);
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
			ultraCalcManager1.ContainingControl = this;
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(727, 9);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 14;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(143, 20);
			textBoxVoucherNumber.TabIndex = 3;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(645, 34);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 28;
			label1.Text = "Reference 1:";
			textBoxRef1.Location = new System.Drawing.Point(727, 31);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(153, 20);
			textBoxRef1.TabIndex = 15;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 459);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(457, 81);
			textBoxNote.TabIndex = 15;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 458);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 2;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance2.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance2;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(panelxml);
			panelDetails.Controls.Add(dateTimePickerPO);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxVechicleName);
			panelDetails.Controls.Add(labelcostcategory);
			panelDetails.Controls.Add(labelJob);
			panelDetails.Controls.Add(ultraFormattedLinkShipping);
			panelDetails.Controls.Add(textBoxShipto);
			panelDetails.Controls.Add(comboBoxBillingAddress);
			panelDetails.Controls.Add(comboBoxShippingAddressID);
			panelDetails.Controls.Add(textBoxBilltoAddress);
			panelDetails.Controls.Add(comboBoxCostCategory);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(comboBoxJob);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(comboBoxVehicle);
			panelDetails.Controls.Add(linkLabelVehicle);
			panelDetails.Controls.Add(comboBoxDriver);
			panelDetails.Controls.Add(linkLabelDriver);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(comboBoxSalesperson);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxPONumber);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxCustomer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(884, 210);
			panelDetails.TabIndex = 0;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance3;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(9, 168);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 176;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance4;
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CalcManager = ultraCalcManager1;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance5;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(99, 166);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(117, 20);
			comboBoxPayeeTaxGroup.TabIndex = 175;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			panelxml.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelxml.Controls.Add(simpleButtonSave);
			panelxml.Controls.Add(simpleButtonFind);
			panelxml.Controls.Add(simpleButtonLast);
			panelxml.Controls.Add(simpleButtonNext);
			panelxml.Controls.Add(simpleButtonPrevious);
			panelxml.Controls.Add(simpleButtonFirst);
			panelxml.Location = new System.Drawing.Point(561, 167);
			panelxml.Name = "panelxml";
			panelxml.Size = new System.Drawing.Size(318, 30);
			panelxml.TabIndex = 174;
			simpleButtonSave.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonSave.Appearance.Options.UseFont = true;
			simpleButtonSave.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.save;
			simpleButtonSave.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonSave.Location = new System.Drawing.Point(270, 5);
			simpleButtonSave.Name = "simpleButtonSave";
			simpleButtonSave.Size = new System.Drawing.Size(39, 20);
			simpleButtonSave.TabIndex = 250;
			simpleButtonSave.Click += new System.EventHandler(simpleButtonSave_Click);
			simpleButtonFind.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFind.Appearance.Options.UseFont = true;
			simpleButtonFind.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.find;
			simpleButtonFind.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFind.Location = new System.Drawing.Point(108, 5);
			simpleButtonFind.Name = "simpleButtonFind";
			simpleButtonFind.Size = new System.Drawing.Size(60, 20);
			simpleButtonFind.TabIndex = 249;
			simpleButtonFind.Click += new System.EventHandler(simpleButtonFind_Click);
			simpleButtonLast.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonLast.Appearance.Options.UseFont = true;
			simpleButtonLast.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.last;
			simpleButtonLast.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonLast.Location = new System.Drawing.Point(220, 5);
			simpleButtonLast.Name = "simpleButtonLast";
			simpleButtonLast.Size = new System.Drawing.Size(50, 20);
			simpleButtonLast.TabIndex = 6;
			simpleButtonLast.Click += new System.EventHandler(simpleButtonLast_Click);
			simpleButtonNext.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonNext.Appearance.Options.UseFont = true;
			simpleButtonNext.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.next;
			simpleButtonNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonNext.Location = new System.Drawing.Point(169, 5);
			simpleButtonNext.Name = "simpleButtonNext";
			simpleButtonNext.Size = new System.Drawing.Size(50, 20);
			simpleButtonNext.TabIndex = 5;
			simpleButtonNext.Click += new System.EventHandler(simpleButtonNext_Click);
			simpleButtonPrevious.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonPrevious.Appearance.Options.UseFont = true;
			simpleButtonPrevious.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.prev;
			simpleButtonPrevious.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonPrevious.Location = new System.Drawing.Point(56, 5);
			simpleButtonPrevious.Name = "simpleButtonPrevious";
			simpleButtonPrevious.Size = new System.Drawing.Size(50, 20);
			simpleButtonPrevious.TabIndex = 4;
			simpleButtonPrevious.Click += new System.EventHandler(simpleButtonPrevious_Click);
			simpleButtonFirst.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFirst.Appearance.Options.UseFont = true;
			simpleButtonFirst.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.first;
			simpleButtonFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFirst.Location = new System.Drawing.Point(4, 5);
			simpleButtonFirst.Name = "simpleButtonFirst";
			simpleButtonFirst.Size = new System.Drawing.Size(50, 20);
			simpleButtonFirst.TabIndex = 3;
			simpleButtonFirst.Click += new System.EventHandler(simpleButtonFirst_Click);
			dateTimePickerPO.Checked = false;
			dateTimePickerPO.CustomFormat = " ";
			dateTimePickerPO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerPO.Location = new System.Drawing.Point(516, 94);
			dateTimePickerPO.Name = "dateTimePickerPO";
			dateTimePickerPO.ShowCheckBox = true;
			dateTimePickerPO.Size = new System.Drawing.Size(125, 20);
			dateTimePickerPO.TabIndex = 18;
			dateTimePickerPO.Value = new System.DateTime(0L);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(459, 98);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(51, 13);
			label6.TabIndex = 160;
			label6.Text = "PO Date:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(645, 120);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(76, 13);
			label5.TabIndex = 158;
			label5.Text = "Vehicle Name:";
			label5.Click += new System.EventHandler(label5_Click);
			textBoxVechicleName.Location = new System.Drawing.Point(727, 117);
			textBoxVechicleName.MaxLength = 20;
			textBoxVechicleName.Name = "textBoxVechicleName";
			textBoxVechicleName.ReadOnly = true;
			textBoxVechicleName.Size = new System.Drawing.Size(153, 20);
			textBoxVechicleName.TabIndex = 21;
			textBoxVechicleName.TextChanged += new System.EventHandler(textBoxVechicleName_TextChanged);
			appearance17.FontData.BoldAsString = "False";
			appearance17.FontData.Name = "Tahoma";
			labelcostcategory.Appearance = appearance17;
			labelcostcategory.AutoSize = true;
			labelcostcategory.Location = new System.Drawing.Point(422, 147);
			labelcostcategory.Name = "labelcostcategory";
			labelcostcategory.Size = new System.Drawing.Size(76, 15);
			labelcostcategory.TabIndex = 156;
			labelcostcategory.TabStop = true;
			labelcostcategory.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelcostcategory.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelcostcategory.Value = "Cost Category:";
			appearance18.ForeColor = System.Drawing.Color.Blue;
			labelcostcategory.VisitedLinkAppearance = appearance18;
			labelcostcategory.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelcostcategory_LinkClicked);
			appearance19.FontData.BoldAsString = "False";
			appearance19.FontData.Name = "Tahoma";
			labelJob.Appearance = appearance19;
			labelJob.AutoSize = true;
			labelJob.Location = new System.Drawing.Point(230, 145);
			labelJob.Name = "labelJob";
			labelJob.Size = new System.Drawing.Size(42, 15);
			labelJob.TabIndex = 155;
			labelJob.TabStop = true;
			labelJob.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelJob.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelJob.Value = "Project:";
			appearance20.ForeColor = System.Drawing.Color.Blue;
			labelJob.VisitedLinkAppearance = appearance20;
			labelJob.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelJob_LinkClicked);
			appearance21.FontData.BoldAsString = "False";
			appearance21.FontData.Name = "Tahoma";
			ultraFormattedLinkShipping.Appearance = appearance21;
			ultraFormattedLinkShipping.AutoSize = true;
			ultraFormattedLinkShipping.Location = new System.Drawing.Point(9, 146);
			ultraFormattedLinkShipping.Name = "ultraFormattedLinkShipping";
			ultraFormattedLinkShipping.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkShipping.TabIndex = 154;
			ultraFormattedLinkShipping.TabStop = true;
			ultraFormattedLinkShipping.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkShipping.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkShipping.Value = "Shipping Method:";
			appearance22.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkShipping.VisitedLinkAppearance = appearance22;
			ultraFormattedLinkShipping.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkShipping_LinkClicked);
			textBoxShipto.BackColor = System.Drawing.Color.White;
			textBoxShipto.CustomReportFieldName = "";
			textBoxShipto.CustomReportKey = "";
			textBoxShipto.CustomReportValueType = 1;
			textBoxShipto.IsComboTextBox = false;
			textBoxShipto.IsModified = false;
			textBoxShipto.Location = new System.Drawing.Point(230, 68);
			textBoxShipto.MaxLength = 255;
			textBoxShipto.Multiline = true;
			textBoxShipto.Name = "textBoxShipto";
			textBoxShipto.Size = new System.Drawing.Size(215, 72);
			textBoxShipto.TabIndex = 11;
			comboBoxBillingAddress.AlwaysInEditMode = true;
			comboBoxBillingAddress.Assigned = false;
			comboBoxBillingAddress.CalcManager = ultraCalcManager1;
			comboBoxBillingAddress.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBillingAddress.CustomReportFieldName = "";
			comboBoxBillingAddress.CustomReportKey = "";
			comboBoxBillingAddress.CustomReportValueType = 1;
			comboBoxBillingAddress.DescriptionTextBox = null;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBillingAddress.DisplayLayout.Appearance = appearance23;
			comboBoxBillingAddress.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBillingAddress.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance24.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance24.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance24.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.Appearance = appearance24;
			appearance25.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BandLabelAppearance = appearance25;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance26.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance26.BackColor2 = System.Drawing.SystemColors.Control;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance26.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBillingAddress.DisplayLayout.GroupByBox.PromptAppearance = appearance26;
			comboBoxBillingAddress.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBillingAddress.DisplayLayout.MaxRowScrollRegions = 1;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveCellAppearance = appearance27;
			appearance28.BackColor = System.Drawing.SystemColors.Highlight;
			appearance28.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBillingAddress.DisplayLayout.Override.ActiveRowAppearance = appearance28;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBillingAddress.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.CardAreaAppearance = appearance29;
			appearance30.BorderColor = System.Drawing.Color.Silver;
			appearance30.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBillingAddress.DisplayLayout.Override.CellAppearance = appearance30;
			comboBoxBillingAddress.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBillingAddress.DisplayLayout.Override.CellPadding = 0;
			appearance31.BackColor = System.Drawing.SystemColors.Control;
			appearance31.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance31.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance31.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance31.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBillingAddress.DisplayLayout.Override.GroupByRowAppearance = appearance31;
			appearance32.TextHAlignAsString = "Left";
			comboBoxBillingAddress.DisplayLayout.Override.HeaderAppearance = appearance32;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBillingAddress.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.Color.Silver;
			comboBoxBillingAddress.DisplayLayout.Override.RowAppearance = appearance33;
			comboBoxBillingAddress.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBillingAddress.DisplayLayout.Override.TemplateAddRowAppearance = appearance34;
			comboBoxBillingAddress.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBillingAddress.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBillingAddress.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBillingAddress.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBillingAddress.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxBillingAddress.Editable = true;
			comboBoxBillingAddress.FilterString = "";
			comboBoxBillingAddress.HasAllAccount = false;
			comboBoxBillingAddress.HasCustom = false;
			comboBoxBillingAddress.IsDataLoaded = false;
			comboBoxBillingAddress.Location = new System.Drawing.Point(99, 45);
			comboBoxBillingAddress.MaxDropDownItems = 12;
			comboBoxBillingAddress.Name = "comboBoxBillingAddress";
			comboBoxBillingAddress.ShowInactiveItems = false;
			comboBoxBillingAddress.ShowQuickAdd = true;
			comboBoxBillingAddress.Size = new System.Drawing.Size(128, 20);
			comboBoxBillingAddress.TabIndex = 8;
			comboBoxBillingAddress.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxShippingAddressID.AlwaysInEditMode = true;
			comboBoxShippingAddressID.Assigned = false;
			comboBoxShippingAddressID.CalcManager = ultraCalcManager1;
			comboBoxShippingAddressID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingAddressID.CustomReportFieldName = "";
			comboBoxShippingAddressID.CustomReportKey = "";
			comboBoxShippingAddressID.CustomReportValueType = 1;
			comboBoxShippingAddressID.DescriptionTextBox = null;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingAddressID.DisplayLayout.Appearance = appearance35;
			comboBoxShippingAddressID.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingAddressID.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance36.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance36.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.Appearance = appearance36;
			appearance37.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BandLabelAppearance = appearance37;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance38.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance38.BackColor2 = System.Drawing.SystemColors.Control;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance38.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingAddressID.DisplayLayout.GroupByBox.PromptAppearance = appearance38;
			comboBoxShippingAddressID.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingAddressID.DisplayLayout.MaxRowScrollRegions = 1;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			appearance39.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveCellAppearance = appearance39;
			appearance40.BackColor = System.Drawing.SystemColors.Highlight;
			appearance40.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingAddressID.DisplayLayout.Override.ActiveRowAppearance = appearance40;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingAddressID.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.CardAreaAppearance = appearance41;
			appearance42.BorderColor = System.Drawing.Color.Silver;
			appearance42.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingAddressID.DisplayLayout.Override.CellAppearance = appearance42;
			comboBoxShippingAddressID.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingAddressID.DisplayLayout.Override.CellPadding = 0;
			appearance43.BackColor = System.Drawing.SystemColors.Control;
			appearance43.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance43.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance43.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance43.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingAddressID.DisplayLayout.Override.GroupByRowAppearance = appearance43;
			appearance44.TextHAlignAsString = "Left";
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderAppearance = appearance44;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingAddressID.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingAddressID.DisplayLayout.Override.RowAppearance = appearance45;
			comboBoxShippingAddressID.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingAddressID.DisplayLayout.Override.TemplateAddRowAppearance = appearance46;
			comboBoxShippingAddressID.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingAddressID.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingAddressID.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingAddressID.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingAddressID.Editable = true;
			comboBoxShippingAddressID.FilterString = "";
			comboBoxShippingAddressID.HasAllAccount = false;
			comboBoxShippingAddressID.HasCustom = false;
			comboBoxShippingAddressID.IsDataLoaded = false;
			comboBoxShippingAddressID.Location = new System.Drawing.Point(305, 46);
			comboBoxShippingAddressID.MaxDropDownItems = 12;
			comboBoxShippingAddressID.Name = "comboBoxShippingAddressID";
			comboBoxShippingAddressID.ShowInactiveItems = false;
			comboBoxShippingAddressID.ShowQuickAdd = true;
			comboBoxShippingAddressID.Size = new System.Drawing.Size(140, 20);
			comboBoxShippingAddressID.TabIndex = 9;
			comboBoxShippingAddressID.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxBilltoAddress.BackColor = System.Drawing.Color.White;
			textBoxBilltoAddress.CustomReportFieldName = "";
			textBoxBilltoAddress.CustomReportKey = "";
			textBoxBilltoAddress.CustomReportValueType = 1;
			textBoxBilltoAddress.IsComboTextBox = false;
			textBoxBilltoAddress.IsModified = false;
			textBoxBilltoAddress.Location = new System.Drawing.Point(12, 67);
			textBoxBilltoAddress.MaxLength = 255;
			textBoxBilltoAddress.Multiline = true;
			textBoxBilltoAddress.Name = "textBoxBilltoAddress";
			textBoxBilltoAddress.Size = new System.Drawing.Size(215, 72);
			textBoxBilltoAddress.TabIndex = 10;
			comboBoxCostCategory.Assigned = false;
			comboBoxCostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCostCategory.CalcManager = ultraCalcManager1;
			comboBoxCostCategory.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCostCategory.CustomReportFieldName = "";
			comboBoxCostCategory.CustomReportKey = "";
			comboBoxCostCategory.CustomReportValueType = 1;
			comboBoxCostCategory.DescriptionTextBox = null;
			comboBoxCostCategory.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCostCategory.Editable = true;
			comboBoxCostCategory.FilterString = "";
			comboBoxCostCategory.HasAllAccount = false;
			comboBoxCostCategory.HasCustom = false;
			comboBoxCostCategory.IsDataLoaded = false;
			comboBoxCostCategory.Location = new System.Drawing.Point(516, 144);
			comboBoxCostCategory.MaxDropDownItems = 12;
			comboBoxCostCategory.Name = "comboBoxCostCategory";
			comboBoxCostCategory.ShowInactiveItems = false;
			comboBoxCostCategory.ShowQuickAdd = true;
			comboBoxCostCategory.Size = new System.Drawing.Size(126, 20);
			comboBoxCostCategory.TabIndex = 22;
			comboBoxCostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(645, 56);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 149;
			label4.Text = "Reference 2:";
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CalcManager = ultraCalcManager1;
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
			comboBoxJob.Location = new System.Drawing.Point(299, 144);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(117, 20);
			comboBoxJob.TabIndex = 13;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRef2.Location = new System.Drawing.Point(727, 53);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(153, 20);
			textBoxRef2.TabIndex = 16;
			comboBoxVehicle.Assigned = false;
			comboBoxVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicle.CalcManager = ultraCalcManager1;
			comboBoxVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicle.CustomReportFieldName = "";
			comboBoxVehicle.CustomReportKey = "";
			comboBoxVehicle.CustomReportValueType = 1;
			comboBoxVehicle.DescriptionTextBox = textBoxVechicleName;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicle.DisplayLayout.Appearance = appearance47;
			comboBoxVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance48.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance48.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.GroupByBox.Appearance = appearance48;
			appearance49.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance49;
			comboBoxVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance50.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance50.BackColor2 = System.Drawing.SystemColors.Control;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance50.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance50;
			comboBoxVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance51;
			appearance52.BackColor = System.Drawing.SystemColors.Highlight;
			appearance52.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance52;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.CardAreaAppearance = appearance53;
			appearance54.BorderColor = System.Drawing.Color.Silver;
			appearance54.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicle.DisplayLayout.Override.CellAppearance = appearance54;
			comboBoxVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance55.BackColor = System.Drawing.SystemColors.Control;
			appearance55.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance55.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance55.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance55.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance55;
			appearance56.TextHAlignAsString = "Left";
			comboBoxVehicle.DisplayLayout.Override.HeaderAppearance = appearance56;
			comboBoxVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicle.DisplayLayout.Override.RowAppearance = appearance57;
			comboBoxVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance58;
			comboBoxVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicle.Editable = true;
			comboBoxVehicle.FilterString = "";
			comboBoxVehicle.HasAllAccount = false;
			comboBoxVehicle.HasCustom = false;
			comboBoxVehicle.IsDataLoaded = false;
			comboBoxVehicle.Location = new System.Drawing.Point(516, 117);
			comboBoxVehicle.MaxDropDownItems = 12;
			comboBoxVehicle.Name = "comboBoxVehicle";
			comboBoxVehicle.ShowInactiveItems = false;
			comboBoxVehicle.ShowQuickAdd = true;
			comboBoxVehicle.Size = new System.Drawing.Size(126, 20);
			comboBoxVehicle.TabIndex = 20;
			comboBoxVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(comboBoxVehicle_SelectedIndexChanged);
			comboBoxVehicle.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(comboBoxVehicle_InitializeLayout);
			appearance59.FontData.BoldAsString = "False";
			appearance59.FontData.Name = "Tahoma";
			linkLabelVehicle.Appearance = appearance59;
			linkLabelVehicle.AutoSize = true;
			linkLabelVehicle.Location = new System.Drawing.Point(459, 120);
			linkLabelVehicle.Name = "linkLabelVehicle";
			linkLabelVehicle.Size = new System.Drawing.Size(43, 15);
			linkLabelVehicle.TabIndex = 147;
			linkLabelVehicle.TabStop = true;
			linkLabelVehicle.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVehicle.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVehicle.Value = "Vehicle:";
			appearance60.ForeColor = System.Drawing.Color.Blue;
			linkLabelVehicle.VisitedLinkAppearance = appearance60;
			linkLabelVehicle.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVehicle_LinkClicked);
			comboBoxDriver.Assigned = false;
			comboBoxDriver.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDriver.CalcManager = ultraCalcManager1;
			comboBoxDriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDriver.CustomReportFieldName = "";
			comboBoxDriver.CustomReportKey = "";
			comboBoxDriver.CustomReportValueType = 1;
			comboBoxDriver.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDriver.DisplayLayout.Appearance = appearance61;
			comboBoxDriver.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDriver.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxDriver.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxDriver.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDriver.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDriver.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDriver.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxDriver.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDriver.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDriver.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxDriver.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDriver.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxDriver.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxDriver.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDriver.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxDriver.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxDriver.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDriver.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxDriver.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDriver.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDriver.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDriver.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDriver.Editable = true;
			comboBoxDriver.FilterString = "";
			comboBoxDriver.HasAllAccount = false;
			comboBoxDriver.HasCustom = false;
			comboBoxDriver.IsDataLoaded = false;
			comboBoxDriver.Location = new System.Drawing.Point(727, 139);
			comboBoxDriver.MaxDropDownItems = 12;
			comboBoxDriver.Name = "comboBoxDriver";
			comboBoxDriver.ShowInactiveItems = false;
			comboBoxDriver.ShowQuickAdd = true;
			comboBoxDriver.Size = new System.Drawing.Size(153, 20);
			comboBoxDriver.TabIndex = 23;
			comboBoxDriver.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance73.FontData.BoldAsString = "False";
			appearance73.FontData.Name = "Tahoma";
			linkLabelDriver.Appearance = appearance73;
			linkLabelDriver.AutoSize = true;
			linkLabelDriver.Location = new System.Drawing.Point(645, 142);
			linkLabelDriver.Name = "linkLabelDriver";
			linkLabelDriver.Size = new System.Drawing.Size(38, 15);
			linkLabelDriver.TabIndex = 145;
			linkLabelDriver.TabStop = true;
			linkLabelDriver.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelDriver.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelDriver.Value = "Driver:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			linkLabelDriver.VisitedLinkAppearance = appearance74;
			linkLabelDriver.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelDriver_LinkClicked);
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(566, 21);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 7;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			comboBoxSalesperson.Assigned = false;
			comboBoxSalesperson.CalcManager = ultraCalcManager1;
			comboBoxSalesperson.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSalesperson.CustomReportFieldName = "";
			comboBoxSalesperson.CustomReportKey = "";
			comboBoxSalesperson.CustomReportValueType = 1;
			comboBoxSalesperson.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSalesperson.DisplayLayout.Appearance = appearance75;
			comboBoxSalesperson.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSalesperson.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxSalesperson.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSalesperson.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxSalesperson.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSalesperson.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSalesperson.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSalesperson.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSalesperson.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxSalesperson.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSalesperson.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSalesperson.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxSalesperson.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxSalesperson.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSalesperson.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxSalesperson.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxSalesperson.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSalesperson.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxSalesperson.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSalesperson.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSalesperson.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSalesperson.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSalesperson.Editable = true;
			comboBoxSalesperson.FilterString = "";
			comboBoxSalesperson.HasAllAccount = false;
			comboBoxSalesperson.HasCustom = false;
			comboBoxSalesperson.IsDataLoaded = false;
			comboBoxSalesperson.Location = new System.Drawing.Point(727, 74);
			comboBoxSalesperson.MaxDropDownItems = 12;
			comboBoxSalesperson.Name = "comboBoxSalesperson";
			comboBoxSalesperson.ShowInactiveItems = false;
			comboBoxSalesperson.ShowQuickAdd = true;
			comboBoxSalesperson.Size = new System.Drawing.Size(153, 20);
			comboBoxSalesperson.TabIndex = 17;
			comboBoxSalesperson.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance87.FontData.BoldAsString = "False";
			appearance87.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance87;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(645, 75);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(66, 15);
			ultraFormattedLinkLabel6.TabIndex = 144;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Salesperson:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(645, 98);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 143;
			label2.Text = "Customer PO#:";
			textBoxPONumber.Location = new System.Drawing.Point(727, 95);
			textBoxPONumber.MaxLength = 50;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(152, 20);
			textBoxPONumber.TabIndex = 19;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CalcManager = ultraCalcManager1;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance89;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance90.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance90.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance90.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance90.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance90;
			appearance91.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance91;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance92.BackColor2 = System.Drawing.SystemColors.Control;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance92.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance92;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance93;
			appearance94.BackColor = System.Drawing.SystemColors.Highlight;
			appearance94.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance94;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance95;
			appearance96.BorderColor = System.Drawing.Color.Silver;
			appearance96.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance96;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance97.BackColor = System.Drawing.SystemColors.Control;
			appearance97.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance97.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance97.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance97.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance97;
			appearance98.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance98;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			appearance99.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance99;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance100.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance100;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(99, 143);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(117, 20);
			comboBoxShippingMethod.TabIndex = 12;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance101.FontData.BoldAsString = "False";
			appearance101.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance101;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(233, 47);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel3.TabIndex = 18;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Ship To:";
			appearance102.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance102;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance103.FontData.BoldAsString = "False";
			appearance103.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance103;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(12, 46);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(38, 15);
			ultraFormattedLinkLabel1.TabIndex = 16;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Bill To:";
			appearance104.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance104;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance105.FontData.BoldAsString = "True";
			appearance105.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance105;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(12, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(79, 15);
			ultraFormattedLinkLabel4.TabIndex = 4;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Customer ID:";
			appearance106.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance106;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.CalcManager = ultraCalcManager1;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			appearance107.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance107;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance108.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance108.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance108.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance108;
			appearance109.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance109;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance110.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance110.BackColor2 = System.Drawing.SystemColors.Control;
			appearance110.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance110.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance110;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance111;
			appearance112.BackColor = System.Drawing.SystemColors.Highlight;
			appearance112.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance112;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance113;
			appearance114.BorderColor = System.Drawing.Color.Silver;
			appearance114.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance114;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance115.BackColor = System.Drawing.SystemColors.Control;
			appearance115.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance115.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance115.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance115.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance115;
			appearance116.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance116;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance117;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance118;
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
			comboBoxCustomer.Location = new System.Drawing.Point(99, 23);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(109, 20);
			comboBoxCustomer.TabIndex = 5;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance119.FontData.BoldAsString = "True";
			appearance119.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance119;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance120.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance120;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance121;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance122.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance122.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance122.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance122;
			appearance123.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance123;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance124.BackColor2 = System.Drawing.SystemColors.Control;
			appearance124.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance124.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance124;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance125;
			appearance126.BackColor = System.Drawing.SystemColors.Highlight;
			appearance126.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance126;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance127;
			appearance128.BorderColor = System.Drawing.Color.Silver;
			appearance128.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance128;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance129.BackColor = System.Drawing.SystemColors.Control;
			appearance129.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance129.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance129.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance129.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance129;
			appearance130.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance130;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			appearance131.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance131;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance132;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(99, 1);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(109, 20);
			comboBoxSysDoc.TabIndex = 1;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(645, 11);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 8;
			mmLabel1.Text = "Date:";
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Location = new System.Drawing.Point(210, 23);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(385, 20);
			textBoxCustomerName.TabIndex = 6;
			textBoxCustomerName.TabStop = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(14, 369);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(867, 49);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator3,
				removeRowToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 120);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			salesStatisticsToolStripMenuItem.Name = "salesStatisticsToolStripMenuItem";
			salesStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			salesStatisticsToolStripMenuItem.Text = "Sales Statistics...";
			salesStatisticsToolStripMenuItem.Click += new System.EventHandler(salesStatisticsToolStripMenuItem_Click);
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(512, 455);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 126;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxOrders.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxOrders.FormattingEnabled = true;
			checkedListBoxOrders.Location = new System.Drawing.Point(515, 471);
			checkedListBoxOrders.Name = "checkedListBoxOrders";
			checkedListBoxOrders.Size = new System.Drawing.Size(155, 69);
			checkedListBoxOrders.TabIndex = 127;
			checkedListBoxOrders.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(checkedListBoxOrders_MouseDoubleClick);
			ultraToolTipManager1.ContainingControl = this;
			comboBoxSpecification.Assigned = false;
			comboBoxSpecification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSpecification.CalcManager = ultraCalcManager1;
			comboBoxSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSpecification.CustomReportFieldName = "";
			comboBoxSpecification.CustomReportKey = "";
			comboBoxSpecification.CustomReportValueType = 1;
			comboBoxSpecification.DescriptionTextBox = null;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSpecification.DisplayLayout.Appearance = appearance133;
			comboBoxSpecification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSpecification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance134.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance134.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance134.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance134.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.GroupByBox.Appearance = appearance134;
			appearance135.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance135;
			comboBoxSpecification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance136.BackColor2 = System.Drawing.SystemColors.Control;
			appearance136.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance136.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.PromptAppearance = appearance136;
			comboBoxSpecification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSpecification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance137.BackColor = System.Drawing.SystemColors.Window;
			appearance137.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSpecification.DisplayLayout.Override.ActiveCellAppearance = appearance137;
			appearance138.BackColor = System.Drawing.SystemColors.Highlight;
			appearance138.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSpecification.DisplayLayout.Override.ActiveRowAppearance = appearance138;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.CardAreaAppearance = appearance139;
			appearance140.BorderColor = System.Drawing.Color.Silver;
			appearance140.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSpecification.DisplayLayout.Override.CellAppearance = appearance140;
			comboBoxSpecification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSpecification.DisplayLayout.Override.CellPadding = 0;
			appearance141.BackColor = System.Drawing.SystemColors.Control;
			appearance141.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance141.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance141.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance141.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.GroupByRowAppearance = appearance141;
			appearance142.TextHAlignAsString = "Left";
			comboBoxSpecification.DisplayLayout.Override.HeaderAppearance = appearance142;
			comboBoxSpecification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSpecification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.BorderColor = System.Drawing.Color.Silver;
			comboBoxSpecification.DisplayLayout.Override.RowAppearance = appearance143;
			comboBoxSpecification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSpecification.DisplayLayout.Override.TemplateAddRowAppearance = appearance144;
			comboBoxSpecification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSpecification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSpecification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSpecification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSpecification.Editable = true;
			comboBoxSpecification.FilterString = "";
			comboBoxSpecification.HasAllAccount = false;
			comboBoxSpecification.HasCustom = false;
			comboBoxSpecification.IsDataLoaded = false;
			comboBoxSpecification.Location = new System.Drawing.Point(566, 302);
			comboBoxSpecification.MaxDropDownItems = 12;
			comboBoxSpecification.Name = "comboBoxSpecification";
			comboBoxSpecification.ShowInactiveItems = false;
			comboBoxSpecification.Size = new System.Drawing.Size(100, 20);
			comboBoxSpecification.TabIndex = 158;
			comboBoxSpecification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSpecification.Visible = false;
			docStatusLabel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			docStatusLabel.BackColor = System.Drawing.Color.Transparent;
			docStatusLabel.DocumentNumber = "";
			docStatusLabel.LinkEnabled = true;
			docStatusLabel.Location = new System.Drawing.Point(721, 471);
			docStatusLabel.Name = "docStatusLabel";
			docStatusLabel.ShowDocNumber = true;
			docStatusLabel.Size = new System.Drawing.Size(131, 56);
			docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
			docStatusLabel.TabIndex = 128;
			docStatusLabel.Visible = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(48, 251);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 120;
			productPhotoViewer.Visible = false;
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
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance145;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance146.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance146.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance146.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance146.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance146;
			appearance147.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance147;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance148.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance148.BackColor2 = System.Drawing.SystemColors.Control;
			appearance148.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance148.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance148;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance149;
			appearance150.BackColor = System.Drawing.SystemColors.Highlight;
			appearance150.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance150;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance151;
			appearance152.BorderColor = System.Drawing.Color.Silver;
			appearance152.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance152;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance153.BackColor = System.Drawing.SystemColors.Control;
			appearance153.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance153.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance153.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance153.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance153;
			appearance154.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance154;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance155.BackColor = System.Drawing.SystemColors.Window;
			appearance155.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance155;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance156;
			comboBoxGridItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridItem.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridItem.Editable = true;
			comboBoxGridItem.FilterCustomerID = "";
			comboBoxGridItem.FilterString = "";
			comboBoxGridItem.FilterSysDocID = "";
			comboBoxGridItem.HasAllAccount = false;
			comboBoxGridItem.HasCustom = false;
			comboBoxGridItem.IsDataLoaded = false;
			comboBoxGridItem.Location = new System.Drawing.Point(675, 167);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowOnlyLotItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			ComboBoxitemcostCategory.Assigned = false;
			ComboBoxitemcostCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemcostCategory.CalcManager = ultraCalcManager1;
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
			ComboBoxitemcostCategory.Location = new System.Drawing.Point(520, 275);
			ComboBoxitemcostCategory.MaxDropDownItems = 12;
			ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
			ComboBoxitemcostCategory.ShowInactiveItems = false;
			ComboBoxitemcostCategory.ShowQuickAdd = true;
			ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
			ComboBoxitemcostCategory.TabIndex = 156;
			ComboBoxitemcostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemcostCategory.Visible = false;
			ComboBoxitemJob.Assigned = false;
			ComboBoxitemJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxitemJob.CalcManager = ultraCalcManager1;
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
			ComboBoxitemJob.Location = new System.Drawing.Point(347, 275);
			ComboBoxitemJob.MaxDropDownItems = 12;
			ComboBoxitemJob.Name = "ComboBoxitemJob";
			ComboBoxitemJob.ShowInactiveItems = false;
			ComboBoxitemJob.ShowQuickAdd = true;
			ComboBoxitemJob.Size = new System.Drawing.Size(100, 20);
			ComboBoxitemJob.TabIndex = 129;
			ComboBoxitemJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemJob.Visible = false;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance157;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(767, 220);
			comboBoxGridLocation.MaxDropDownItems = 12;
			comboBoxGridLocation.Name = "comboBoxGridLocation";
			comboBoxGridLocation.ShowAll = false;
			comboBoxGridLocation.ShowConsignIn = false;
			comboBoxGridLocation.ShowConsignOut = false;
			comboBoxGridLocation.ShowDefaultLocationOnly = false;
			comboBoxGridLocation.ShowInactiveItems = false;
			comboBoxGridLocation.ShowNormalLocations = true;
			comboBoxGridLocation.ShowPOSOnly = false;
			comboBoxGridLocation.ShowQuickAdd = true;
			comboBoxGridLocation.ShowWarehouseOnly = false;
			comboBoxGridLocation.Size = new System.Drawing.Size(114, 20);
			comboBoxGridLocation.TabIndex = 121;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			comboBoxStyle.Assigned = false;
			comboBoxStyle.CalcManager = ultraCalcManager1;
			comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStyle.CustomReportFieldName = "";
			comboBoxStyle.CustomReportKey = "";
			comboBoxStyle.CustomReportValueType = 1;
			comboBoxStyle.DescriptionTextBox = null;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStyle.DisplayLayout.Appearance = appearance169;
			comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance170.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance170.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance170.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance170.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance170;
			appearance171.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance171;
			comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance172.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance172.BackColor2 = System.Drawing.SystemColors.Control;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance172.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance172;
			comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance173.BackColor = System.Drawing.SystemColors.Window;
			appearance173.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance173;
			appearance174.BackColor = System.Drawing.SystemColors.Highlight;
			appearance174.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance174;
			comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance175;
			appearance176.BorderColor = System.Drawing.Color.Silver;
			appearance176.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance176;
			comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
			appearance177.BackColor = System.Drawing.SystemColors.Control;
			appearance177.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance177.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance177.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance177.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance177;
			appearance178.TextHAlignAsString = "Left";
			comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance178;
			comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance179.BackColor = System.Drawing.SystemColors.Window;
			appearance179.BorderColor = System.Drawing.Color.Silver;
			comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance179;
			comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance180.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance180;
			comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStyle.Editable = true;
			comboBoxStyle.FilterString = "";
			comboBoxStyle.HasAllAccount = false;
			comboBoxStyle.HasCustom = false;
			comboBoxStyle.IsDataLoaded = false;
			comboBoxStyle.Location = new System.Drawing.Point(372, 275);
			comboBoxStyle.MaxDropDownItems = 12;
			comboBoxStyle.Name = "comboBoxStyle";
			comboBoxStyle.ShowInactiveItems = false;
			comboBoxStyle.Size = new System.Drawing.Size(154, 20);
			comboBoxStyle.TabIndex = 157;
			comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStyle.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance181;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance182.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance182.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance182.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance182.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance182;
			appearance183.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance183;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance184.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance184.BackColor2 = System.Drawing.SystemColors.Control;
			appearance184.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance184.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance184;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance185.BackColor = System.Drawing.SystemColors.Window;
			appearance185.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance185;
			appearance186.BackColor = System.Drawing.SystemColors.Highlight;
			appearance186.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance186;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance187;
			appearance188.BorderColor = System.Drawing.Color.Silver;
			appearance188.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance188;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance189.BackColor = System.Drawing.SystemColors.Control;
			appearance189.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance189.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance189.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance189.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance189;
			appearance190.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance190;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance191.BackColor = System.Drawing.SystemColors.Window;
			appearance191.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance191;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance192.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance192;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 246);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(872, 204);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(898, 593);
			base.Controls.Add(comboBoxSpecification);
			base.Controls.Add(docStatusLabel);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxOrders);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(label3);
			base.Controls.Add(ComboBoxitemcostCategory);
			base.Controls.Add(ComboBoxitemJob);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(comboBoxStyle);
			base.Controls.Add(dataGridItems);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "DeliveryNoteForm";
			Text = "Delivery Note";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			panelxml.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxBillingAddress).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingAddressID).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSalesperson).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
