using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinDock;
using Infragistics.Win.UltraWinGrid;
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
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class PurchaseGRNForm : Form, IForm
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

		private bool allowChangeVendor;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private string poSysDocID = "";

		private string poVoucherID = "";

		private ItemSourceTypes sourceDocType;

		private DataTable deliveryNoteTable;

		private bool allowEdit = true;

		private bool loadZeroQuantity;

		private PurchaseReceiptData currentData;

		private const string TABLENAME_CONST = "Purchase_Receipt";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private bool allowGRNtoProject = CompanyPreferences.IssueGRNtoProject;

		private bool isIssued;

		private bool showLotDetail = CompanyPreferences.ShowLotdetailinPrintout;

		private bool showOrderandShipmentinGRN = CompanyPreferences.ShowOrderandShipmentinGRN;

		private bool ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool canAccessCost = true;

		private bool activateGRNEditing;

		private bool isGRNEditAllowed;

		private int slNo = 1;

		private string currencyID = Global.BaseCurrencyID;

		private string termID = "";

		private ProductSelectionForm ProductSelection = new ProductSelectionForm();

		private int RowIndex;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool restrictTransaction;

		private DataTable dtTransaction = new DataTable();

		private DataTable dtPO = new DataTable();

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

		private TextBox textBoxVendorName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem purchaseStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem createFromPurchaseOrderToolStripMenuItem;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private CurrencySelector comboBoxCurrency;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private BuyerComboBox comboBoxBuyer;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private XPButton buttonSelectDocument;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private vendorsFlatComboBox comboBoxVendor;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonDistribution;

		private TransporterComboBox comboBoxTransporter;

		private Label label4;

		private TextBox textBoxRef2;

		private DocStatusLabel docStatusLabel;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private JobComboBox comboBoxJob;

		private ToolStripMenuItem issueToProjectToolStripMenuItem;

		private UltraDockManager ultraDockManager1;

		private AutoHideControl _PurchaseGRNFormAutoHideControl;

		private UnpinnedTabArea _PurchaseGRNFormUnpinnedTabAreaBottom;

		private UnpinnedTabArea _PurchaseGRNFormUnpinnedTabAreaTop;

		private UnpinnedTabArea _PurchaseGRNFormUnpinnedTabAreaRight;

		private UnpinnedTabArea _PurchaseGRNFormUnpinnedTabAreaLeft;

		private ToolStripButton toolStripButtonInformation;

		private UltraFormattedLinkLabel labelCurrency;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private ToolStripMenuItem createFromTransactionListToolStripMenuItem;

		private ProductSpecificationComboBox comboBoxSpecification;

		private ProductStyleComboBox comboBoxStyle;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxSelectedDocs;

		private Label labelProcessedDocs;

		private ListBox checkedListBoxProcessedDocs;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripSeparator toolStripSeparator8;

		private UltraFormattedLinkLabel labelTaxGroup;

		private TaxGroupComboBox comboBoxPayeeTaxGroup;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonExcelImport;

		private ToolStripSeparator toolStripSeparator9;

		private UltraCalcManager ultraCalcManager1;

		private XPButton xpProductSelection;

		private Label label2;

		private TextBox textBoxvendorReferenceNumber;

		private UltraFormattedLinkLabel linkLabelVehicle;

		private Label label6;

		private TextBox textBoxVechicleName;

		private VehicleComboBox comboBoxVehicle;

		private DriverComboBox comboBoxDriver;

		private UltraFormattedLinkLabel linkLabelDriver;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3013;

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
					comboBoxSysDoc.Enabled = true;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
					if (sourceDocType != 0)
					{
						comboBoxVendor.Enabled = false;
					}
					else
					{
						comboBoxVendor.Enabled = true;
					}
					if (allowChangeVendor && sourceDocType == ItemSourceTypes.None)
					{
						comboBoxVendor.Enabled = true;
					}
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				if (isNewRecord || isIssued)
				{
					issueToProjectToolStripMenuItem.Enabled = false;
				}
				else
				{
					issueToProjectToolStripMenuItem.Enabled = true;
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
				if (isVoid == value)
				{
					return;
				}
				isVoid = value;
				panelDetails.Enabled = !value;
				dataGridItems.Enabled = !value;
				textBoxNote.Enabled = !value;
				buttonSave.Enabled = !value;
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

		public bool IsGRNEditAllowed
		{
			get
			{
				return isGRNEditAllowed;
			}
			set
			{
				if (value)
				{
					dataGridItems.ContextMenuStrip.Enabled = false;
				}
				else
				{
					dataGridItems.ContextMenuStrip.Enabled = true;
				}
				isGRNEditAllowed = value;
			}
		}

		public PurchaseGRNForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			loadZeroQuantity = CompanyPreferences.LoadZeroQuantityinGRN;
			comboBoxGridLocation.ShowWarehouseOnly = true;
			docStatusLabel.LinkClicked += docStatusLabel_LinkClicked;
			if (!useJobCosting)
			{
				issueToProjectToolStripMenuItem.Visible = false;
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

		private void AddEvents()
		{
			base.Load += Form_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxVendor.SelectedIndexChanged += comboBoxVendor_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.DoubleClickCell += dataGridItems_DoubleClickCell;
			ProductSelection.xpOkButton.Click += xpOkButton_Click;
			dataGridItems.InitializeTemplateAddRow += dataGridItems_InitializeTemplateAddRow;
		}

		private void dataGridItems_InitializeTemplateAddRow(object sender, InitializeTemplateAddRowEventArgs e)
		{
			e.TemplateAddRow.Cells["IsNew"].Value = false;
		}

		private void xpOkButton_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt = ProductSelection.CreateDt(dt);
			DataRow dataRow = dt.NewRow();
			dataRow["ProductID"] = ProductSelection.textBoxProduct.Text;
			dataRow["UPC"] = ProductSelection.textBoxUPC.Text;
			dataRow["LotNumber"] = ProductSelection.textBoxLotNo.Text;
			dataRow["LotQty"] = ((ProductSelection.textBoxQty.Text == "") ? 1m : Convert.ToDecimal(ProductSelection.textBoxQty.Text));
			dataRow["LocationId"] = ProductSelection.textLocation.Text;
			dataRow["RackId"] = ProductSelection.textRack.Text;
			dataRow["BinId"] = ProductSelection.textBin.Text;
			dataRow["Reference"] = ProductSelection.textBoxLotNo.Text;
			dataRow["RowIndex"] = "0";
			dataRow["VoucherId"] = textBoxVoucherNumber.Text;
			dataRow["SysDocId"] = comboBoxSysDoc.Text;
			if (ProductSelection.ExpDate.Checked)
			{
				dataRow["ExpiryDate"] = ProductSelection.ExpDate.Value;
			}
			else
			{
				dataRow["ExpiryDate"] = DBNull.Value;
			}
			if (ProductSelection.PrdDate.Checked)
			{
				dataRow["ProductionDate"] = ProductSelection.PrdDate.Value;
			}
			else
			{
				dataRow["ProductionDate"] = DBNull.Value;
			}
			dt.Rows.Add(dataRow);
			comboBoxGridItem.IsLoadingData = true;
			DataTable obj = dataGridItems.DataSource as DataTable;
			DataRow dataRow2 = obj.NewRow();
			comboBoxGridItem.SelectedID = ProductSelection.textBoxProduct.Text;
			comboBoxGridItem.Value = ProductSelection.textBoxProduct.Text;
			dataRow2["Item Code"] = ProductSelection.textBoxProduct.Text;
			dataRow2["Description"] = comboBoxGridItem.SelectedName;
			dataRow2["Quantity"] = ProductSelection.textBoxQty.Text;
			dataRow2["IsTrackLot"] = true;
			dataRow2["IsTrackSerial"] = true;
			dataRow2["Unit"] = Factory.DatabaseSystem.GetFieldValue("Product", "UnitID", "ProductID", dataRow["ProductID"]);
			ItemTaxOptions itemTaxOptions = (ItemTaxOptions)Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", dataRow["ProductID"]);
			dataRow2["TaxOption"] = itemTaxOptions;
			switch (itemTaxOptions)
			{
			case ItemTaxOptions.BasedOnCustomer:
				dataRow2["TaxGroupID"] = comboBoxPayeeTaxGroup.SelectedID;
				break;
			case ItemTaxOptions.Taxable:
				dataRow2["TaxGroupID"] = comboBoxGridItem.TaxGroupID;
				break;
			case ItemTaxOptions.NonTaxable:
				dataRow2["TaxGroupID"] = DBNull.Value;
				break;
			}
			obj.Rows.Add(dataRow2);
			UltraGridCell ultraGridCell = dataGridItems.Rows[RowIndex].Cells["Quantity"];
			ultraGridCell.Tag = dt;
			ultraGridCell.Appearance.FontData.Underline = DefaultableBoolean.True;
			ultraGridCell.Appearance.ForeColor = Color.Black;
			checked
			{
				RowIndex++;
				ProductSelection.ClearControls();
			}
		}

		private void dataGridItems_DoubleClickCell(object sender, DoubleClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity" && e.Cell.Text != "0")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity" && e.Cell.Text != "0")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			try
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
					ReceiveLotSelectionForm receiveLotSelectionForm = new ReceiveLotSelectionForm();
					int index = cell.Row.Index;
					receiveLotSelectionForm.ProductID = dataGridItems.Rows[index].Cells["Item Code"].Value.ToString();
					receiveLotSelectionForm.ProductDescription = dataGridItems.Rows[index].Cells["Description"].Value.ToString();
					receiveLotSelectionForm.LocationID = dataGridItems.Rows[index].Cells["Location"].Value.ToString();
					receiveLotSelectionForm.RowQuantity = float.Parse(cell.Text);
					if (cell.Tag != null)
					{
						receiveLotSelectionForm.ProductLotTable = (DataTable)cell.Tag;
					}
					if (receiveLotSelectionForm.ShowDialog() != DialogResult.OK)
					{
						return false;
					}
					cell.Tag = receiveLotSelectionForm.ProductLotTable;
					cell.Appearance.FontData.Underline = DefaultableBoolean.True;
					cell.Appearance.ForeColor = Color.Black;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
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

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
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
				if (comboBoxVendor.SelectedID != "" && comboBoxVendor.SelectedID != null)
				{
					comboSearchDialogNew.SelectedVendor = comboBoxVendor.SelectedID;
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
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip1.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVendorName.Text = comboBoxVendor.SelectedName;
			try
			{
				if (!isDataLoading && comboBoxVendor.SelectedID != "")
				{
					string text = Factory.DatabaseSystem.GetFieldValue("Vendor", "CurrencyID", "VendorID", comboBoxVendor.SelectedID).ToString();
					if (text != "")
					{
						comboBoxCurrency.SelectedID = text;
						comboBoxCurrency.GetLastRate();
					}
					comboBoxShippingMethod.SelectedID = comboBoxVendor.DefaultShippingMethod;
					comboBoxBuyer.SelectedID = comboBoxVendor.DefaultBuyer;
					if (CompanyPreferences.IsTax)
					{
						comboBoxPayeeTaxGroup.Clear();
						PayeeTaxOptions taxOption = comboBoxVendor.TaxOption;
						if (taxOption == PayeeTaxOptions.Taxable || taxOption == PayeeTaxOptions.ReverseCharge)
						{
							comboBoxPayeeTaxGroup.SelectedID = comboBoxVendor.DefaultTaxGroupID;
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
				comboBoxVendor.FilterSysDocID = comboBoxSysDoc.SelectedID;
				comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
				activateGRNEditing = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.ActivateGRNEditing, 32, comboBoxSysDoc.SelectedID, defaultValue: false);
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null)
					{
						if (!(e.Cell.Column.Key == "Item Code"))
						{
							goto IL_0ba8;
						}
						if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
						{
							comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
							goto IL_0096;
						}
						if (comboBoxGridItem.SelectedRow != null)
						{
							goto IL_0096;
						}
					}
					goto end_IL_0000;
					IL_0ba8:
					if (e.Cell.Column.Key == "SpecificationID")
					{
						for (int i = e.Cell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
						{
							if (dataGridItems.Rows[i].Cells["SpecificationID"].Value.ToString() == "")
							{
								dataGridItems.Rows[i].Cells["SpecificationID"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (e.Cell.Column.Key == "Style")
					{
						for (int j = e.Cell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
						{
							if (dataGridItems.Rows[j].Cells["Style"].Value.ToString() == "")
							{
								dataGridItems.Rows[j].Cells["Style"].Value = e.Cell.Value.ToString();
							}
						}
					}
					if (dataGridItems.ActiveRow != null)
					{
						string key = e.Cell.Column.Key;
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString(), out result);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Price"].Value.ToString(), out result2);
						decimal.TryParse(dataGridItems.ActiveRow.Cells["Amount"].Value.ToString(), out result3);
						if (key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity")
						{
							result3 = Math.Round(result * result2, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						}
						else if (key == "Price" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Price")
						{
							result3 = Math.Round(result * result2, Global.CurDecimalPoints);
							dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
						}
						else if (key == "Amount" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Amount")
						{
							if (result == 0m)
							{
								result = 1m;
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
							}
							if (((byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()) == 4 && result3 > 0m) || (result < 0m && result3 > 0m))
							{
								result3 = -1m * Math.Abs(result3);
								dataGridItems.ActiveRow.Cells["Amount"].Value = result3;
							}
							result2 = Math.Round(result3 / result, 4);
							if (result3 < 0m)
							{
								result = -1m * Math.Abs(result);
								dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
							}
							dataGridItems.ActiveRow.Cells["Price"].Value = Math.Abs(result2);
						}
						else if (key == "Item Code" && (dataGridItems.ActiveRow.Cells["Quantity"].Value == null || dataGridItems.ActiveRow.Cells["Quantity"].Value.ToString() == ""))
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = result;
						}
					}
					goto end_IL_0000;
					IL_0096:
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
						dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
						dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
						dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
						dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
						dataGridItems.ActiveRow.Cells["IsNew"].Value = true;
						if (comboBoxGridItem.SelectedID != "")
						{
							dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
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
							if (canAccessCost)
							{
								Factory.ProductSystem.GetProductPurchasePrice(comboBoxGridItem.SelectedID);
								ProductData productByID = Factory.ProductSystem.GetProductByID(comboBoxGridItem.SelectedID);
								if (productByID.Tables[0].Rows[0]["LastPurchaseCost"] != DBNull.Value)
								{
									dataGridItems.ActiveRow.Cells["Price"].Value = decimal.Parse(productByID.Tables[0].Rows[0]["LastPurchaseCost"].ToString()).ToString(Format.TotalAmountFormat);
								}
								else
								{
									dataGridItems.ActiveRow.Cells["Price"].Value = 0.ToString(Format.TotalAmountFormat);
								}
							}
							else
							{
								dataGridItems.ActiveRow.Cells["Price"].Value = 0.ToString(Format.TotalAmountFormat);
							}
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
							if (sourceDocType == ItemSourceTypes.PurchaseOrder)
							{
								dataGridItems.ActiveRow.Cells["POSysDocID"].Value = sourceSysDocID;
								dataGridItems.ActiveRow.Cells["POVoucherID"].Value = sourceVoucherID;
								dataGridItems.ActiveRow.Cells["PORowIndex"].Value = -1;
							}
							else if (sourceDocType == ItemSourceTypes.PackingList)
							{
								dataGridItems.ActiveRow.Cells["PKSysDocID"].Value = sourceSysDocID;
								dataGridItems.ActiveRow.Cells["PKVoucherID"].Value = sourceVoucherID;
								dataGridItems.ActiveRow.Cells["PKRowIndex"].Value = -1;
							}
							if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
							{
								object obj3 = dataGridItems.ActiveRow.Cells["Ordered"].Value = (dataGridItems.ActiveRow.Cells["Received"].Value = 0);
							}
						}
						goto IL_0ba8;
					}
					MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
					matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
					string text = "";
					matrixSelectionForm.AllowNegativeQuantity = false;
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
							decimal result4 = default(decimal);
							decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result4);
							ultraGridRow.Update();
						}
					}
					end_IL_0000:;
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridItems.ActiveRow.Cells["RefSlNo"].Value == null || dataGridItems.ActiveRow.Cells["RefSlNo"].Value.ToString() == "")
				{
					if (dataGridItems.Rows[0].Cells["RefSlNo"].Value == null)
					{
						dataGridItems.Rows[0].Cells["RefSlNo"].Value = 1;
					}
					if (dataGridItems.ActiveRow.Index > 0)
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
				if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
				{
					decimal result = default(decimal);
					decimal.TryParse(dataGridItems.ActiveCell.Value.ToString(), out result);
					result = Math.Round(result, 4);
					dataGridItems.ActiveCell.Value = result;
				}
				else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Job")
				{
					for (int i = dataGridItems.ActiveCell.Row.Index + 1; i < dataGridItems.Rows.Count; i++)
					{
						if (dataGridItems.Rows[i].Cells["Job"].Value.ToString() == "")
						{
							dataGridItems.Rows[i].Cells["Job"].Value = dataGridItems.ActiveCell.Value;
						}
					}
				}
				else
				{
					if (!(dataGridItems.ActiveCell.Column.Key.ToString() == "Location"))
					{
						return;
					}
					for (int j = dataGridItems.ActiveCell.Row.Index + 1; j < dataGridItems.Rows.Count; j++)
					{
						if (dataGridItems.Rows[j].Cells["Location"].Value.ToString() == "")
						{
							dataGridItems.Rows[j].Cells["Location"].Value = dataGridItems.ActiveCell.Value;
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
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice && e.Cell.Column.Key == "Quantity" && sourceDocType == ItemSourceTypes.PurchaseOrder && !CompanyPreferences.AllowLPurchaseQtyMoreThanPO && e.Cell.Row.Cells["ISPORRow"].Value.ToString() != "")
			{
				decimal d = decimal.Parse(e.NewValue.ToString());
				decimal result = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Received"].Value.ToString(), out result);
				decimal result2 = default(decimal);
				decimal.TryParse(e.Cell.Row.Cells["Ordered"].Value.ToString(), out result2);
				if (d > result2 - result)
				{
					ErrorHelper.InformationMessage("Received quantity cannot be greater than ordered quantity.");
					e.Cancel = true;
					return;
				}
			}
			if (e.Cell != null && e.Cell.Column.Key == "Quantity" && e.Cell.Text != "0" && !AllocateQuantityToLot(e.Cell))
			{
				e.Cancel = true;
			}
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
					currentData = new PurchaseReceiptData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.PurchaseReceiptTable.Rows[0] : currentData.PurchaseReceiptTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["PurchaseFlow"] = CompanyPreferences.LocalPurchaseFlow;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Reference2"] = textBoxRef2.Text;
				dataRow["VendorReferenceNo"] = textBoxvendorReferenceNumber.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["VendorID"] = comboBoxVendor.SelectedID;
				dataRow["SourceDocType"] = sourceDocType;
				dataRow["IsImport"] = false;
				dataRow["TransporterID"] = comboBoxTransporter.SelectedID;
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				if (comboBoxBuyer.SelectedID != "")
				{
					dataRow["BuyerID"] = comboBoxBuyer.SelectedID;
				}
				else
				{
					dataRow["BuyerID"] = DBNull.Value;
				}
				dataRow["TermID"] = termID;
				if (comboBoxCurrency.SelectedID != "")
				{
					dataRow["CurrencyID"] = comboBoxCurrency.SelectedID;
					dataRow["CurrencyRate"] = comboBoxCurrency.Rate;
				}
				else
				{
					dataRow["CurrencyID"] = DBNull.Value;
					dataRow["CurrencyRate"] = DBNull.Value;
				}
				if (sourceDocType == ItemSourceTypes.PurchaseOrder)
				{
					dataRow["POSysDocID"] = poSysDocID;
					dataRow["POVoucherID"] = poVoucherID;
				}
				if (sourceDocType != 0)
				{
					dataRow["SourceSysDocID"] = sourceSysDocID;
					dataRow["SourceVoucherID"] = sourceVoucherID;
				}
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
					dataRow["TaxOption"] = (byte)comboBoxVendor.TaxOption;
				}
				else
				{
					dataRow["TaxOption"] = PayeeTaxOptions.NonTaxable;
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
				dataRow["ActivateGrnEdit"] = activateGRNEditing;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.PurchaseReceiptTable.Rows.Add(dataRow);
				}
				decimal d = default(decimal);
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.PurchaseReceiptDetailTable.Columns.Contains(column.Key))
					{
						currentData.PurchaseReceiptDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.PurchaseReceiptDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.PurchaseReceiptDetailTable.NewRow();
					dataRow2.BeginEdit();
					decimal num = default(decimal);
					if (row.Cells["Quantity"].Value != null)
					{
						num = decimal.Parse(row.Cells["Quantity"].Value.ToString());
					}
					if (!(num == 0m))
					{
						d += num;
						dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
						if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
						{
							dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
						}
						if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("POSysDocID"))
						{
							if (row.Cells["POSysDocID"].Value.ToString() != "")
							{
								dataRow2["OrderSysDocID"] = row.Cells["POSysDocID"].Value.ToString();
							}
							if (row.Cells["POVoucherID"].Value.ToString() != "")
							{
								dataRow2["OrderVoucherID"] = row.Cells["POVoucherID"].Value.ToString();
							}
							if (row.Cells["PORowIndex"].Value.ToString() != "")
							{
								dataRow2["OrderRowIndex"] = row.Cells["PORowIndex"].Value.ToString();
							}
							if (row.Cells.Exists("RowDocType") && row.Cells["RowDocType"].Value.ToString() != "")
							{
								dataRow2["IsPORRow"] = true;
							}
						}
						dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
						dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
						dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
						dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
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
						dataRow2["ROWSOURCE"] = checked((byte)sourceDocType);
						dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
						dataRow2["JobID"] = row.Cells["Job"].Value.ToString();
						if (row.Cells["CostCategory"].Value != null && row.Cells["CostCategory"].Value.ToString() != "")
						{
							dataRow2["CostCategoryID"] = row.Cells["CostCategory"].Value.ToString();
						}
						else
						{
							dataRow2["CostCategoryID"] = DBNull.Value;
						}
						if (!string.IsNullOrEmpty(row.Cells["Price"].Value.ToString()))
						{
							dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
						}
						else
						{
							dataRow2["UnitPrice"] = 0;
						}
						dataRow2["Description"] = row.Cells["Description"].Value.ToString();
						dataRow2["Remarks"] = row.Cells["Remarks"].Value.ToString();
						dataRow2["RowIndex"] = row.Index;
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
						dataRow2["SpecificationID"] = row.Cells["SpecificationID"].Value.ToString();
						dataRow2["StyleID"] = row.Cells["Style"].Value.ToString();
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
						bool result = false;
						bool.TryParse(row.Cells["IsNew"].Value.ToString(), out result);
						dataRow2["IsNew"] = result;
						dataRow2.EndEdit();
						currentData.PurchaseReceiptDetailTable.Rows.Add(dataRow2);
						if ((result || !IsGRNEditAllowed) && row.Cells["Quantity"].Tag != null)
						{
							foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
							{
								DataRow dataRow4 = currentData.Tables["Product_Lot_Receiving_Detail"].NewRow();
								dataRow4["ProductID"] = row2["ProductID"];
								dataRow4["LocationID"] = row2["LocationID"];
								dataRow4["LotNumber"] = row2["LotNumber"];
								dataRow4["BinID"] = row2["BinID"];
								dataRow4["RackID"] = row2["RackID"];
								dataRow4["ProductionDate"] = row2["ProductionDate"];
								dataRow4["ExpiryDate"] = row2["ExpiryDate"];
								dataRow4["LotQty"] = row2["LotQty"];
								dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
								dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
								dataRow4["RowIndex"] = row.Index;
								dataRow4["Reference2"] = row2["Reference2"];
								currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow4);
							}
						}
					}
				}
				if (d == 0m)
				{
					ErrorHelper.InformationMessage("Total quantity should not be zero");
					return false;
				}
				if (deliveryNoteTable != null)
				{
					currentData.Tables.Add(deliveryNoteTable);
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
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("POSysDocID");
				dataTable.Columns.Add("POVoucherID");
				dataTable.Columns.Add("PORowIndex", typeof(int));
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("TaxGroupID");
				dataTable.Columns.Add("TaxOption", typeof(byte));
				dataTable.Columns.Add("Remarks");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("RefSlNo", typeof(int));
				dataTable.Columns.Add("RefText1");
				dataTable.Columns.Add("RefText2");
				dataTable.Columns.Add("RefNum1", typeof(decimal));
				dataTable.Columns.Add("RefNum2", typeof(decimal));
				dataTable.Columns.Add("RefDate1", typeof(DateTime));
				dataTable.Columns.Add("RefDate2", typeof(DateTime));
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("SpecificationID");
				dataTable.Columns.Add("Style");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("ISPORRow", typeof(bool));
				dataTable.Columns.Add("RowDocType", typeof(byte));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataTable.Columns.Add("ListSysDocID");
				dataTable.Columns.Add("ListVoucherID");
				dataTable.Columns.Add("ListRowIndex", typeof(int));
				dataTable.Columns.Add("IsNew", typeof(bool));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].Hidden = true;
				bool flag4 = ultraGridColumn3.Hidden = flag2;
				bool hidden = ultraGridColumn2.Hidden = flag4;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Hidden = true;
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["RefSlNo"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText1"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["RefText2"];
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"];
				UltraGridColumn ultraGridColumn8 = dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"];
				UltraGridColumn ultraGridColumn9 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate1"];
				bool flag7 = dataGridItems.DisplayLayout.Bands[0].Columns["RefDate2"].Hidden = true;
				bool flag9 = ultraGridColumn9.Hidden = flag7;
				bool flag11 = ultraGridColumn8.Hidden = flag9;
				flag2 = (ultraGridColumn7.Hidden = flag11);
				flag4 = (ultraGridColumn6.Hidden = flag2);
				hidden = (ultraGridColumn5.Hidden = flag4);
				ultraGridColumn4.Hidden = hidden;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.LoadLayout();
				UltraGridColumn ultraGridColumn10 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn11 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn12 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn12.CellActivation = activation2;
				Activation activation7 = ultraGridColumn10.CellActivation = (ultraGridColumn11.CellActivation = activation4);
				AppearanceBase cellAppearance = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"].CellAppearance;
				AppearanceBase cellAppearance2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"].CellAppearance;
				Color color = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				Color color4 = cellAppearance.BackColorDisabled = (cellAppearance2.BackColorDisabled = color);
				dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["POSysDocID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["POVoucherID"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["PORowIndex"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsNew"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeAdditionalColumns();
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxOption"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsNew"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Format = "#,0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.CalcManager = ultraCalcManager1;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Formula = "[Quantity]*[Price]";
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ValueList = comboBoxJob;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellAppearance.BackColor = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].TabStop = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["TaxGroupID"].Header.Caption = "Tax Group";
				if (!useJobCosting)
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True);
				}
				else
				{
					ExcludeFromColumnChooser excludeFromColumnChooser3 = dataGridItems.DisplayLayout.Bands[0].Columns["Job"].ExcludeFromColumnChooser = (dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.False);
				}
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].ValueList = ComboBoxitemcostCategory;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum1"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["RefNum2"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsNew"].DefaultCellValue = false;
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
				UltraGridColumn ultraGridColumn13 = dataGridItems.DisplayLayout.Bands[0].Columns["IsNew"];
				UltraGridColumn ultraGridColumn14 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				flag4 = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				hidden = (ultraGridColumn14.Hidden = flag4);
				ultraGridColumn13.Hidden = hidden;
				UltraGridColumn ultraGridColumn15 = dataGridItems.DisplayLayout.Bands[0].Columns["ListSysDocID"];
				UltraGridColumn ultraGridColumn16 = dataGridItems.DisplayLayout.Bands[0].Columns["ListRowIndex"];
				flag4 = (dataGridItems.DisplayLayout.Bands[0].Columns["ListVoucherID"].Hidden = true);
				hidden = (ultraGridColumn16.Hidden = flag4);
				ultraGridColumn15.Hidden = hidden;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Received"].CellAppearance.ForeColorDisabled = Color.Black;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Description"].Activation = Activation.Disabled;
					row.Cells["Description"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Description"].Appearance.ForeColorDisabled = Color.Black;
					row.Cells["Unit"].Activation = Activation.Disabled;
					row.Cells["Unit"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Unit"].Appearance.ForeColorDisabled = Color.Black;
				}
			}
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.DirectInvoice || CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.GRNThenInvoice)
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.BackColorDisabled = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.BackColorDisabled = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.ForeColorDisabled = Color.Black;
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("POSysDocID"))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["POSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["POVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["PORowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["RowDocType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["ISPORRow"].Hidden = true;
			}
			dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 3000;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["SpecificationID"].Header.Caption = CompanyPreferences.SpecificationID;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
			dataGridItems.DisplayLayout.Bands[0].Columns["Remarks"].CellMultiLine = DefaultableBoolean.True;
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Amount"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Amount", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["Amount"].DisplayFormat = "{0:n}";
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("Received"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["Received"]);
			}
			if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("Ordered"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Ordered", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Ordered"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Ordered"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Received", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Received"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Received"].DisplayFormat = "{0:n}";
			}
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
			}
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
			comboBoxVendor.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.PurchaseReceiptSystem.GetPurchaseReceiptByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["Purchase_Receipt"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxRef2.Text = dataRow["Reference2"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
					comboBoxCurrency.Rate = decimal.Parse(dataRow["CurrencyRate"].ToString());
					poSysDocID = dataRow["SysDocID"].ToString();
					poVoucherID = dataRow["VoucherID"].ToString();
					textBoxvendorReferenceNumber.Text = dataRow["VendorReferenceNo"].ToString();
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
					sourceSysDocID = dataRow["SourceSysDocID"].ToString();
					sourceVoucherID = dataRow["SourceVoucherID"].ToString();
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
					if (dataRow["SourceDocType"] != DBNull.Value)
					{
						sourceDocType = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
					}
					else
					{
						sourceDocType = ItemSourceTypes.None;
					}
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
					comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
					comboBoxDriver.SelectedID = dataRow["DriverID"].ToString();
					DataRow[] array = Factory.VehicleSystem.GetVehicleComboList().Tables[0].Select("Code = '" + dataRow["VehicleID"].ToString() + "'");
					if (array.Length != 0)
					{
						comboBoxVehicle.SelectedID = dataRow["VehicleID"].ToString();
					}
					else if (array.Length == 0)
					{
						comboBoxVehicle.Clear();
						textBoxVechicleName.ReadOnly = false;
						textBoxVechicleName.Text = dataRow["VehicleID"].ToString();
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Purchase_Receipt_Detail") && currentData.PurchaseReceiptDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Purchase_Receipt_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							bool result2 = false;
							if (row["IsPORRow"] != DBNull.Value && bool.TryParse(row["IsPORRow"].ToString(), out result2))
							{
								dataRow3["RowDocType"] = row["IsPORRow"];
							}
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Quantity"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Quantity"] = row["Quantity"];
							}
							if (row["UnitPrice1"] != DBNull.Value)
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPrice1"].ToString()).ToString(Format.UnitPriceFormat);
							}
							else if (row["UnitPrice"] != DBNull.Value)
							{
								dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
							}
							dataRow3["Attribute1"] = row["Attribute1"];
							dataRow3["Attribute2"] = row["Attribute2"];
							dataRow3["Attribute3"] = row["Attribute3"];
							dataRow3["MatrixParentID"] = row["MatrixParentID"];
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3["RefSlNo"] = row["RefSlNo"];
							dataRow3["RefText1"] = row["RefText1"];
							dataRow3["RefText2"] = row["RefText2"];
							dataRow3["RefNum1"] = row["RefNum1"];
							dataRow3["RefNum2"] = row["RefNum2"];
							dataRow3["RefDate1"] = row["RefDate1"];
							dataRow3["RefDate2"] = row["RefDate2"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Remarks"] = row["Remarks"];
							dataRow3["Job"] = row["JobID"];
							if (row["CostCategoryID"] != DBNull.Value)
							{
								dataRow3["CostCategory"] = row["CostCategoryID"];
							}
							dataRow3["Location"] = row["LocationID"];
							dataRow3["Unit"] = row["UnitID"];
							decimal result3 = default(decimal);
							decimal result4 = default(decimal);
							decimal.TryParse(dataRow3["Quantity"].ToString(), out result3);
							decimal.TryParse(dataRow3["Price"].ToString(), out result4);
							dataRow3["Amount"] = Math.Round(result3 * result4, Global.CurDecimalPoints);
							dataRow3["POSysDocID"] = row["OrderSysDocID"];
							dataRow3["POVoucherID"] = row["OrderVoucherID"];
							dataRow3["PORowIndex"] = row["OrderRowIndex"];
							dataRow3["ListSysDocID"] = row["ListSysDocID"];
							dataRow3["ListVoucherID"] = row["ListVoucherID"];
							dataRow3["ListRowIndex"] = row["ListRowIndex"];
							dataRow3["SpecificationID"] = row["SpecificationID"];
							dataRow3["Style"] = row["StyleID"];
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
							dataRow3["IsNew"] = false;
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
						}
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							if (row2.Cells["RowDocType"].Value != null && row2.Cells["RowDocType"].Value.ToString() != "" && int.Parse(row2.Cells["RowDocType"].Value.ToString()) == 1)
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
								row2.Cells["Quantity"].Activation = Activation.Disabled;
								row2.Cells["Quantity"].Appearance.BackColorDisabled = Color.WhiteSmoke;
								row2.Cells["Quantity"].Appearance.ForeColorDisabled = Color.Black;
							}
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array2 = currentData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "'");
								if (array2.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array2);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
						}
						isIssued = false;
						if (currentData.Tables.Contains("Job_Inventory_Issue") && currentData.Tables["Job_Inventory_Issue"].Rows.Count > 0)
						{
							isIssued = true;
						}
						checkedListBoxSelectedDocs.Items.Clear();
						checkedListBoxProcessedDocs.Items.Clear();
						if (currentData.Tables.Contains("Purchase_Order"))
						{
							labelSelectedDocs.Visible = true;
							checkedListBoxSelectedDocs.Visible = true;
							foreach (DataRow row3 in currentData.Tables["Purchase_Order"].Rows)
							{
								NameValue item = new NameValue(row3["VoucherID"].ToString(), row3["SysDocID"].ToString());
								checkedListBoxSelectedDocs.Items.Add(item);
							}
						}
						if (currentData.Tables.Contains("Purchase_Invoice_Detail"))
						{
							labelProcessedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row4 in currentData.Tables["Purchase_Invoice_Detail"].Rows)
							{
								NameValue item2 = new NameValue(row4["VoucherID"].ToString(), row4["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item2);
							}
						}
						if (currentData.Tables.Contains("GRN_Return_Detail"))
						{
							labelSelectedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row5 in currentData.Tables["GRN_Return_Detail"].Rows)
							{
								NameValue item3 = new NameValue(row5["VoucherID"].ToString(), row5["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item3);
							}
						}
						if (currentData.Tables.Contains("Job_Inventory_Issue"))
						{
							labelSelectedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row6 in currentData.Tables["Job_Inventory_Issue"].Rows)
							{
								NameValue item4 = new NameValue(row6["VoucherID"].ToString(), row6["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item4);
							}
						}
						if (checkedListBoxProcessedDocs.Items.Count == 0)
						{
							checkedListBoxProcessedDocs.Visible = false;
							labelProcessedDocs.Visible = false;
						}
						if (checkedListBoxSelectedDocs.Items.Count == 0)
						{
							checkedListBoxSelectedDocs.Visible = false;
							labelSelectedDocs.Visible = false;
						}
						bool flag = flag = Factory.PurchaseReceiptSystem.CanEdit(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
						if (activateGRNEditing && !flag)
						{
							IsGRNEditAllowed = true;
						}
						foreach (UltraGridRow row7 in dataGridItems.Rows)
						{
							if (!bool.Parse(row7.Cells["IsNew"].Value.ToString()) && IsGRNEditAllowed)
							{
								foreach (UltraGridCell cell in row7.Cells)
								{
									cell.Activation = Activation.Disabled;
									cell.Appearance.BackColorDisabled = Color.WhiteSmoke;
									cell.Appearance.ForeColorDisabled = Color.Black;
								}
							}
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
				bool result = false;
				bool.TryParse(currentData.PurchaseReceiptTable.Rows[0]["IsInvoiced"].ToString(), out result);
				if (result)
				{
					ErrorHelper.ErrorMessage("You are not able to modify this document because it is already costed with a purchase invoice.");
					return false;
				}
			}
			return SaveData(clearAfter: true);
		}

		private bool SaveData(bool clearAfter)
		{
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
				bool flag2 = Factory.PurchaseReceiptSystem.CreatePurchaseReceipt(currentData, !isNewRecord);
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
					if (flag2 && allowGRNtoProject && useJobCosting)
					{
						FormActivator.BringFormToFront(FormActivator.JobInventoryIssueFormObj);
						FormActivator.JobInventoryIssueFormObj.issueToProject(comboBoxSysDoc.Text, textBoxVoucherNumber.Text);
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
			formManager.ShowApprovalPanel(approvalTaskID, "Purchase_Receipt", "VoucherID");
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
			if (!isNewRecord)
			{
				bool flag = Factory.PurchaseReceiptSystem.CanEdit(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
				if (!activateGRNEditing && !flag)
				{
					ErrorHelper.WarningMessage("This document cannot be modified because some items are refered by other transactions.");
					return false;
				}
				if (activateGRNEditing && !flag)
				{
					IsGRNEditAllowed = true;
				}
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Purchase_Receipt", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to (SecurityRoleID:116).");
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
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.GoodsReceivedNote, Global.CurrentUser, includeApproveUser: false);
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
			bool flag2 = false;
			checked
			{
				if (timeSpan.Days <= num + 1)
				{
					flag2 = true;
				}
				else if (Global.isUserAdmin)
				{
					flag2 = true;
				}
				else if (num == 0)
				{
					flag2 = true;
				}
				if (isNewRecord && dateTimePickerDate.Value < t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EnterBackDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions.");
					return false;
				}
				if (!flag2)
				{
					ErrorHelper.WarningMessage("You are not allowed to enter back-dated transactions not more than " + num + " days.");
					return false;
				}
				if (isNewRecord && dateTimePickerDate.Value > t && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxVendor.SelectedID == "")
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
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Purchase_Receipt", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
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
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxVendorName.Clear();
				comboBoxVendor.Clear();
				comboBoxBuyer.Clear();
				comboBoxShippingMethod.Clear();
				docStatusLabel.Visible = false;
				comboBoxTransporter.Clear();
				comboBoxVendor.Enabled = true;
				checkedListBoxSelectedDocs.Items.Clear();
				checkedListBoxProcessedDocs.Items.Clear();
				labelSelectedDocs.Visible = false;
				checkedListBoxSelectedDocs.Visible = false;
				textBoxvendorReferenceNumber.Clear();
				labelProcessedDocs.Visible = false;
				checkedListBoxProcessedDocs.Visible = false;
				IsGRNEditAllowed = false;
				if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice && !CompanyPreferences.AllowLocalGRNWithoutPO)
				{
					dataGridItems.AllowAddNew = false;
				}
				comboBoxPayeeTaxGroup.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Ordered"))
				{
					dataTable.Columns.Remove("Ordered");
					dataTable.Columns.Remove("Received");
				}
				poSysDocID = "";
				poVoucherID = "";
				sourceSysDocID = "";
				sourceVoucherID = "";
				dataTable.Rows.Clear();
				deliveryNoteTable = null;
				sourceDocType = ItemSourceTypes.None;
				AdjustGridColumnSettings();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxVehicle.Clear();
				comboBoxVehicle.Clear();
				comboBoxDriver.Clear();
				textBoxVechicleName.Clear();
				if (comboBoxVehicle.SelectedID == "")
				{
					textBoxVechicleName.ReadOnly = false;
				}
				else
				{
					textBoxVechicleName.ReadOnly = true;
				}
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
				if (!Factory.PurchaseReceiptSystem.CanEdit(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("This transaction cannot be deleted because some items are refered by other transactions.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				bool flag = false;
				if (!string.IsNullOrEmpty(currentData.PurchaseReceiptTable.Rows[0]["IsInvoiced"].ToString()))
				{
					flag = bool.Parse(currentData.PurchaseReceiptTable.Rows[0]["IsInvoiced"].ToString());
				}
				if (flag)
				{
					ErrorHelper.ErrorMessage("You are not able to delete this document because it is already costed with a purchase invoice.");
					return false;
				}
				return Factory.PurchaseReceiptSystem.DeletePurchaseReceipt(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Purchase_Receipt", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Purchase_Receipt", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Purchase_Receipt", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Purchase_Receipt", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Purchase_Receipt", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
				labelTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxPayeeTaxGroup.Visible = CompanyPreferences.IsTax;
				comboBoxSysDoc.FilterByType(SysDocTypes.GoodsReceivedNote);
				SetSecurity();
				if (!base.IsDisposed)
				{
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
			allowChangeVendor = Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeSalesCustomer);
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
			comboBoxPayeeTaxGroup.ReadOnly = !Security.IsAllowedSecurityRole(GeneralSecurityRoles.EditTaxGroup);
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
		}

		private void SetupForm()
		{
			PurchaseFlows localPurchaseFlow = CompanyPreferences.LocalPurchaseFlow;
			checked
			{
				switch (localPurchaseFlow)
				{
				case PurchaseFlows.POThenGRNThenInvoice:
					if (CompanyPreferences.AllowLocalGRNWithoutPO)
					{
						dataGridItems.AllowAddNew = CompanyPreferences.AllowLocalGRNAddNewRow;
					}
					else
					{
						dataGridItems.AllowAddNew = false;
					}
					dataGridItems.ShowDeleteMenu = false;
					createFromPurchaseOrderToolStripMenuItem.Visible = true;
					duplicateToolStripMenuItem.Enabled = false;
					buttonSelectDocument.Visible = true;
					textBoxVendorName.Width = textBoxVendorName.Width - buttonSelectDocument.Width - 3;
					if (!CompanyPreferences.AllowLocalGRNAddNewRow)
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellAppearance.ForeColorDisabled = Color.Black;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
						dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
						dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
					}
					break;
				case PurchaseFlows.GRNThenInvoice:
					dataGridItems.ShowDeleteMenu = true;
					createFromPurchaseOrderToolStripMenuItem.Visible = true;
					duplicateToolStripMenuItem.Enabled = true;
					buttonSelectDocument.Visible = true;
					textBoxVendorName.Width = textBoxVendorName.Width - buttonSelectDocument.Width - 3;
					break;
				case PurchaseFlows.DirectInvoice:
				case PurchaseFlows.POThenInvoice:
				{
					dataGridItems.AllowAddNew = false;
					buttonSelectDocument.Visible = false;
					duplicateToolStripMenuItem.Enabled = false;
					Panel panel = panelDetails;
					bool enabled = panelButtons.Enabled = false;
					panel.Enabled = enabled;
					Text += " - Not allowed in current Purchase Flow";
					createFromPurchaseOrderToolStripMenuItem.Enabled = false;
					textBoxVendorName.Width = textBoxVendorName.Width - buttonSelectDocument.Width - 3;
					break;
				}
				default:
					if (localPurchaseFlow != 0)
					{
						buttonSelectDocument.Visible = false;
					}
					break;
				}
			}
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
			else
			{
				ErrorHelper.ErrorMessage("Unable to void the transaction.");
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
				bool flag = false;
				if (!string.IsNullOrEmpty(currentData.PurchaseReceiptTable.Rows[0]["IsInvoiced"].ToString()))
				{
					flag = bool.Parse(currentData.PurchaseReceiptTable.Rows[0]["IsInvoiced"].ToString());
				}
				if (flag)
				{
					ErrorHelper.ErrorMessage("You are not able to void this document because it is already costed with a purchase invoice.");
					return false;
				}
				return Factory.PurchaseReceiptSystem.VoidPurchaseReceipt(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.GoodsReceivedNote);
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

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			if (!(currentVendorAddressID == ""))
			{
				new FormHelper().EditVendorAddress(comboBoxVendor.SelectedID, currentVendorAddressID);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBuyer(comboBoxBuyer.SelectedID);
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
					docStatusLabel.Visible = false;
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
					docStatusLabel.Visible = false;
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
					row.Cells["RowDocType"].Value = DBNull.Value;
				}
				deliveryNoteTable = null;
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			if (textBoxRef1.Text == "")
			{
				duplicateToolStripMenuItem.Enabled = !IsNewRecord;
			}
			duplicateToolStripMenuItem.Enabled = false;
		}

		private void createFromPurchaseOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet openOrdersSummary = Factory.PurchaseOrderSystem.GetOpenOrdersSummary(comboBoxVendor.SelectedID, includeImport: false, includeLocal: true);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = openOrdersSummary;
			selectDocumentDialog.Text = "Select Purchase Order";
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.ValidateSelection += formSelectPO_ValidateSelection;
			if (selectDocumentDialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}
			ClearForm();
			string text = poSysDocID = (sourceSysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString());
			string text2 = poVoucherID = (sourceVoucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString());
			PurchaseOrderData purchaseOrderByID = Factory.PurchaseOrderSystem.GetPurchaseOrderByID(text, text2);
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(purchaseOrderByID, SysDocTypes.PurchaseOrder, Global.CurrentUser, includeApproveUser: true);
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
			DataRow dataRow = purchaseOrderByID.PurchaseOrderTable.Rows[0];
			textBoxRef1.Text = dataRow["VoucherID"].ToString();
			textBoxNote.Text = dataRow["Note"].ToString();
			textBoxvendorReferenceNumber.Text = dataRow["VendorReferenceNo"].ToString();
			if (comboBoxVendor.SelectedID == "")
			{
				comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
			}
			comboBoxVendor.Enabled = false;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				labelCurrency.Visible = true;
				comboBoxCurrency.Visible = true;
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
			else
			{
				labelCurrency.Visible = false;
				comboBoxCurrency.Visible = false;
			}
			if (!string.IsNullOrEmpty(dataRow["ShippingMethodID"].ToString()))
			{
				comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["BuyerID"].ToString()))
			{
				comboBoxBuyer.SelectedID = dataRow["BuyerID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["PayeeTaxGroupID"].ToString()))
			{
				comboBoxPayeeTaxGroup.SelectedID = dataRow["PayeeTaxGroupID"].ToString();
			}
			if (!string.IsNullOrEmpty(dataRow["TermID"].ToString()))
			{
				termID = dataRow["TermID"].ToString();
			}
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			if (!dataTable.Columns.Contains("Ordered"))
			{
				dataTable.Columns.Remove("Quantity");
				dataTable.Columns.Add("Ordered", typeof(decimal));
				dataTable.Columns.Add("Received", typeof(decimal));
				dataTable.Columns.Add("Quantity", typeof(decimal));
				if (!dataTable.Columns.Contains("POSysDocID"))
				{
					dataTable.Columns.Add("POSysDocID");
					dataTable.Columns.Add("POVoucherID");
					dataTable.Columns.Add("PORowIndex", typeof(int));
				}
			}
			dataTable.Rows.Clear();
			if (!purchaseOrderByID.Tables.Contains("Purchase_Order_Detail") || purchaseOrderByID.PurchaseOrderDetailTable.Rows.Count == 0)
			{
				return;
			}
			_ = selectDocumentDialog.SelectedRows;
			foreach (DataRow row in purchaseOrderByID.Tables["Purchase_Order_Detail"].Rows)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["Item Code"] = row["ProductID"];
				dataRow3["POSysDocID"] = text;
				dataRow3["POVoucherID"] = text2;
				dataRow3["PORowIndex"] = row["RowIndex"];
				dataRow3["RowDocType"] = ItemSourceTypes.PurchaseOrder;
				dataRow3["ISPORRow"] = true;
				if (row["UnitQuantity"] != DBNull.Value)
				{
					dataRow3["Quantity"] = row["UnitQuantity"];
				}
				else
				{
					dataRow3["Quantity"] = row["Quantity"];
				}
				if (row["SubunitPrice"] != DBNull.Value)
				{
					dataRow3["Price"] = decimal.Parse(row["SubunitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				else
				{
					dataRow3["Price"] = decimal.Parse(row["UnitPrice"].ToString()).ToString(Format.UnitPriceFormat);
				}
				dataRow3["Remarks"] = row["Remarks"];
				dataRow3["Job"] = row["JobID"];
				dataRow3["CostCategory"] = row["CostCategoryID"];
				dataRow3["IsTrackLot"] = row["IsTrackLot"];
				dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
				dataRow3["Attribute1"] = row["Attribute1"];
				dataRow3["Attribute2"] = row["Attribute2"];
				dataRow3["Attribute3"] = row["Attribute3"];
				dataRow3["MatrixParentID"] = row["MatrixParentID"];
				dataRow3["RefSlNo"] = row["RefSlNo"];
				dataRow3["RefText1"] = row["RefText1"];
				dataRow3["RefText2"] = row["RefText2"];
				dataRow3["RefNum1"] = row["RefNum1"];
				dataRow3["RefNum2"] = row["RefNum2"];
				dataRow3["RefDate1"] = row["RefDate1"];
				dataRow3["RefDate2"] = row["RefDate2"];
				dataRow3["Description"] = row["Description"];
				dataRow3["Unit"] = row["UnitID"];
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
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow3["Quantity"].ToString(), out result);
				decimal.TryParse(row["QuantityReceived"].ToString(), out result2);
				dataRow3["Ordered"] = result;
				dataRow3["Received"] = result2;
				result -= result2;
				if (loadZeroQuantity)
				{
					dataRow3["Quantity"] = 0;
				}
				else
				{
					dataRow3["Quantity"] = result;
				}
				if (result < 0m)
				{
					result = default(decimal);
				}
				dataRow3.EndEdit();
				dataTable.Rows.Add(dataRow3);
			}
			sourceDocType = ItemSourceTypes.PurchaseOrder;
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				bool result3 = false;
				bool result4 = false;
				if (row2.Cells["IsTrackLot"].Value != null)
				{
					bool.TryParse(row2.Cells["IsTrackLot"].Value.ToString(), out result3);
				}
				if (row2.Cells["IsTrackSerial"].Value != null)
				{
					bool.TryParse(row2.Cells["IsTrackSerial"].Value.ToString(), out result4);
				}
				if (result3)
				{
					row2.Cells["Quantity"].Appearance.ForeColor = Color.DarkRed;
					row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
				}
			}
			if (CompanyPreferences.LocalPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice)
			{
				if (!CompanyPreferences.AllowLocalGRNAddNewRow)
				{
					dataGridItems.AllowAddNew = false;
				}
				else
				{
					dataGridItems.AllowAddNew = true;
				}
			}
			AdjustGridColumnSettings();
		}

		private void formSelectPO_ValidateSelection(object sender, EventArgs e)
		{
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
					DataSet purchaseReceiptToPrint = Factory.PurchaseReceiptSystem.GetPurchaseReceiptToPrint(selectedID, text, showLotDetail, showOrderandShipmentinGRN);
					if (purchaseReceiptToPrint == null || purchaseReceiptToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(purchaseReceiptToPrint, selectedID, "Purchase Receipt", SysDocTypes.GoodsReceivedNote, isPrint, showPrintDialog);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.SelectedSysDocID = comboBoxSysDoc.SelectedID;
			FormActivator.BringFormToFront(FormActivator.PurchaseGRNListFormObj);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
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

		private void purchaseStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string selectedID = GetSelectedID();
			if (!(selectedID == ""))
			{
				InventoryPurchasesStatisticForm inventoryPurchasesStatisticForm = new InventoryPurchasesStatisticForm();
				inventoryPurchasesStatisticForm.SelectedID = selectedID;
				inventoryPurchasesStatisticForm.Show();
				inventoryPurchasesStatisticForm.BringToFront();
			}
		}

		private string GetSelectedID()
		{
			string result = "";
			if (dataGridItems.ActiveRow == null)
			{
				return "";
			}
			dataGridItems.ActiveRow.GetType();
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveRow.GetType() != typeof(UltraGridRow))
				{
					return "";
				}
				result = dataGridItems.ActiveRow.Cells["Item Code"].Text.ToString();
			}
			return result;
		}

		private void issueToProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.JobInventoryIssueFormObj);
			FormActivator.JobInventoryIssueFormObj.SourceSysDocType = Convert.ToByte(SysDocTypes.JobInventoryIssue);
			FormActivator.JobInventoryIssueFormObj.SourceSysDocID = comboBoxSysDoc.Text;
			FormActivator.JobInventoryIssueFormObj.SourceVoucherID = textBoxVoucherNumber.Text;
			FormActivator.JobInventoryIssueFormObj.issueToProject(comboBoxSysDoc.Text, textBoxVoucherNumber.Text);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void checkedListBoxPO_DoubleClick(object sender, EventArgs e)
		{
		}

		private void panelDetails_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel6_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBuyer(comboBoxBuyer.SelectedID);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransporter(comboBoxTransporter.SelectedID);
		}

		private void createFromTransactionListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet itemTransactionList = Factory.ItemTransactionSystem.GetItemTransactionList(comboBoxVendor.SelectedID, SysDocTypes.GoodsReceivedNote);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = itemTransactionList;
			selectDocumentDialog.Text = "Select Purchase Order";
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.ValidateSelection += formSelectPO_ValidateSelection;
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
			if (comboBoxVendor.SelectedID == "")
			{
				comboBoxVendor.SelectedID = dataRow["PartyID"].ToString();
			}
			comboBoxSysDoc.SelectedID = text;
			textBoxVoucherNumber.Text = text2;
			if (!string.IsNullOrEmpty(dataRow["CurrencyID"].ToString()))
			{
				comboBoxCurrency.SelectedID = dataRow["CurrencyID"].ToString();
			}
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
					dataTable.Columns.Add("Received", typeof(decimal));
					dataTable.Columns.Add("Quantity", typeof(decimal));
					if (!dataTable.Columns.Contains("POSysDocID"))
					{
						dataTable.Columns.Add("POSysDocID");
						dataTable.Columns.Add("POVoucherID");
						dataTable.Columns.Add("PORowIndex", typeof(int));
					}
				}
				dataTable.Rows.Clear();
				_ = selectDocumentDialog.SelectedRows;
				foreach (DataRow row in dtTransaction.Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Item Code"] = row["Item Code"];
					dataRow3["POSysDocID"] = row["POSysDocID"];
					dataRow3["POVoucherID"] = row["POVoucherID"];
					dataRow3["PORowIndex"] = row["PORowIndex"];
					dataRow3["ISPORRow"] = true;
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
					dataRow3["Location"] = row["LocationID"];
					dataRow3["Unit"] = row["UnitID"];
					decimal result = default(decimal);
					decimal num = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					decimal.TryParse(row["Ordered"].ToString(), out result2);
					dataRow3["Ordered"] = result2;
					dataRow3["Received"] = num;
					result -= num;
					dataRow3["Quantity"] = result;
					if (result < 0m)
					{
						result = default(decimal);
					}
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
						dataRow5["POSysDocID"] = "";
						dataRow5["POVoucherID"] = "";
						dataRow5["PORowIndex"] = 0;
						dataRow5["RowDocType"] = ItemSourceTypes.None;
						dataRow5["ISPORRow"] = true;
						if (row2["Quantity"] != DBNull.Value)
						{
							dataRow5["Quantity"] = row2["Quantity"];
						}
						dataRow5["Location"] = dataRow["LocationID"];
						dataRow5["Unit"] = row2["UnitID"];
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
							DtDetails.Rows[j]["RowDocType"] = ItemSourceTypes.PurchaseOrder;
							DtDetails.AcceptChanges();
							item.Istaken = true;
						}
						else if (!item.ItemCode.Contains(text) && !item.Istaken && dataTable.Rows.Count == 0)
						{
							DataRow dataRow = DtDetails.NewRow();
							dataRow["Item Code"] = item.ItemCode;
							dataRow["RowDocType"] = ItemSourceTypes.None;
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

		private void checkedListBoxProcessedDocs_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (checkedListBoxProcessedDocs.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxProcessedDocs.SelectedItem as NameValue;
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void checkedListBoxSelectedDocs_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (checkedListBoxSelectedDocs.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxSelectedDocs.SelectedItem as NameValue;
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void saveAsDraftToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveDraft();
			ClearForm();
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.GoodsReceivedNote);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 32.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.GoodsReceivedNote);
					currentData = (dataSet as PurchaseReceiptData);
					if (!currentData.Tables[0].Columns.Contains("IsInvoiced"))
					{
						currentData.Tables[0].Columns.Add("IsInvoiced", typeof(bool));
					}
					if (!currentData.Tables[1].Columns.Contains("unitPrice1"))
					{
						currentData.Tables[1].Columns.Add("unitPrice1", typeof(decimal));
					}
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

		private void labelTaxGroup_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTaxGroup(comboBoxPayeeTaxGroup.SelectedID);
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
				try
				{
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
							}
						}
						DataRow[] array = dataSet.Tables[0].Select("[Item Code]='" + text + "' AND LotNumber='" + text2 + "'");
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
							}
							dataRow["LocationID"] = array[i]["Location"];
							if (!dataTable.Columns.Contains("SourceLotNumber"))
							{
								dataTable.Columns.Add("SourceLotNumber");
							}
							dataRow["Cost"] = array[i]["Cost"];
							if (!dataTable.Columns.Contains("ProductionDate"))
							{
								dataTable.Columns.Add("ProductionDate");
							}
							if (!dataTable.Columns.Contains("ExpiryDate"))
							{
								dataTable.Columns.Add("ExpiryDate");
							}
							if (!dataTable.Columns.Contains("LotQty"))
							{
								dataTable.Columns.Add("LotQty");
								dataRow["LotQty"] = num;
							}
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
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
			else
			{
				dataGridItems.ImportFromExcel(autoFill: true);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		private void xpProductSelection_Click(object sender, EventArgs e)
		{
			ProductSelection.Show();
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

		private void linkLabelVehicle_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVehicle(comboBoxVehicle.SelectedID);
		}

		private void linkLabelDriver_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditDriver(comboBoxDriver.SelectedID);
		}

		private void toolStripTextBoxFind_KeyPress_1(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
			}
		}

		private void labelCurrency_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditCurrency(comboBoxCurrency.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.PurchaseGRNForm));
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
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
			createFromPurchaseOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			issueToProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromTransactionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			panelDetails = new System.Windows.Forms.Panel();
			linkLabelVehicle = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label6 = new System.Windows.Forms.Label();
			textBoxVechicleName = new System.Windows.Forms.TextBox();
			comboBoxVehicle = new Micromind.DataControls.VehicleComboBox();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			comboBoxDriver = new Micromind.DataControls.DriverComboBox();
			linkLabelDriver = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label2 = new System.Windows.Forms.Label();
			textBoxvendorReferenceNumber = new System.Windows.Forms.TextBox();
			xpProductSelection = new Micromind.UISupport.XPButton();
			labelTaxGroup = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxPayeeTaxGroup = new Micromind.DataControls.TaxGroupComboBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			labelCurrency = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label4 = new System.Windows.Forms.Label();
			textBoxRef2 = new System.Windows.Forms.TextBox();
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			comboBoxCurrency = new Micromind.DataControls.CurrencySelector();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			comboBoxBuyer = new Micromind.DataControls.BuyerComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraDockManager1 = new Infragistics.Win.UltraWinDock.UltraDockManager(components);
			_PurchaseGRNFormUnpinnedTabAreaLeft = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
			_PurchaseGRNFormUnpinnedTabAreaRight = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
			_PurchaseGRNFormUnpinnedTabAreaTop = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
			_PurchaseGRNFormUnpinnedTabAreaBottom = new Infragistics.Win.UltraWinDock.UnpinnedTabArea();
			_PurchaseGRNFormAutoHideControl = new Infragistics.Win.UltraWinDock.AutoHideControl();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxSelectedDocs = new System.Windows.Forms.ListBox();
			labelProcessedDocs = new System.Windows.Forms.Label();
			checkedListBoxProcessedDocs = new System.Windows.Forms.ListBox();
			ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
			comboBoxSpecification = new Micromind.DataControls.ProductSpecificationComboBox();
			comboBoxStyle = new Micromind.DataControls.ProductStyleComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraDockManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[24]
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
				toolStripSeparator3,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripSeparator7,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripSeparator9,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(778, 31);
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress_1);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripSeparator7.Name = "toolStripSeparator7";
			toolStripSeparator7.Size = new System.Drawing.Size(6, 31);
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
			toolStripSeparator9.Name = "toolStripSeparator9";
			toolStripSeparator9.Size = new System.Drawing.Size(6, 31);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator4,
				saveAsDraftToolStripMenuItem,
				toolStripMenuItem2,
				toolStripSeparator8,
				createFromPurchaseOrderToolStripMenuItem,
				issueToProjectToolStripMenuItem,
				createFromTransactionListToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Enabled = false;
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(227, 6);
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new System.Drawing.Size(230, 22);
			toolStripMenuItem2.Text = "Load Draft...";
			toolStripMenuItem2.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator8.Name = "toolStripSeparator8";
			toolStripSeparator8.Size = new System.Drawing.Size(227, 6);
			createFromPurchaseOrderToolStripMenuItem.Name = "createFromPurchaseOrderToolStripMenuItem";
			createFromPurchaseOrderToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
			createFromPurchaseOrderToolStripMenuItem.Text = "Create from Purchase Order...";
			createFromPurchaseOrderToolStripMenuItem.Click += new System.EventHandler(createFromPurchaseOrderToolStripMenuItem_Click);
			issueToProjectToolStripMenuItem.Name = "issueToProjectToolStripMenuItem";
			issueToProjectToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
			issueToProjectToolStripMenuItem.Text = "Issue to Project";
			issueToProjectToolStripMenuItem.Click += new System.EventHandler(issueToProjectToolStripMenuItem_Click);
			createFromTransactionListToolStripMenuItem.Name = "createFromTransactionListToolStripMenuItem";
			createFromTransactionListToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
			createFromTransactionListToolStripMenuItem.Text = "Create from Transaction List..";
			createFromTransactionListToolStripMenuItem.Click += new System.EventHandler(createFromTransactionListToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 537);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(778, 40);
			panelButtons.TabIndex = 4;
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
			linePanelDown.Size = new System.Drawing.Size(778, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(668, 8);
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
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(522, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(318, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(117, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(440, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(69, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference 1:";
			textBoxRef1.Location = new System.Drawing.Point(522, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(138, 20);
			textBoxRef1.TabIndex = 6;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 451);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(263, 80);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(9, 451);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(214, 4);
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
			panelDetails.Controls.Add(linkLabelVehicle);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxVechicleName);
			panelDetails.Controls.Add(comboBoxVehicle);
			panelDetails.Controls.Add(comboBoxDriver);
			panelDetails.Controls.Add(linkLabelDriver);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxvendorReferenceNumber);
			panelDetails.Controls.Add(xpProductSelection);
			panelDetails.Controls.Add(labelTaxGroup);
			panelDetails.Controls.Add(comboBoxPayeeTaxGroup);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(labelCurrency);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxRef2);
			panelDetails.Controls.Add(comboBoxTransporter);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(comboBoxCurrency);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(comboBoxBuyer);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxVendorName);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 35);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(763, 167);
			panelDetails.TabIndex = 0;
			panelDetails.Paint += new System.Windows.Forms.PaintEventHandler(panelDetails_Paint);
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			linkLabelVehicle.Appearance = appearance3;
			linkLabelVehicle.AutoSize = true;
			linkLabelVehicle.Location = new System.Drawing.Point(255, 94);
			linkLabelVehicle.Name = "linkLabelVehicle";
			linkLabelVehicle.Size = new System.Drawing.Size(43, 15);
			linkLabelVehicle.TabIndex = 188;
			linkLabelVehicle.TabStop = true;
			linkLabelVehicle.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelVehicle.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelVehicle.Value = "Vehicle:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			linkLabelVehicle.VisitedLinkAppearance = appearance4;
			linkLabelVehicle.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelVehicle_LinkClicked);
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(440, 93);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(76, 13);
			label6.TabIndex = 187;
			label6.Text = "Vehicle Name:";
			textBoxVechicleName.Location = new System.Drawing.Point(522, 89);
			textBoxVechicleName.MaxLength = 30;
			textBoxVechicleName.Name = "textBoxVechicleName";
			textBoxVechicleName.ReadOnly = true;
			textBoxVechicleName.Size = new System.Drawing.Size(138, 20);
			textBoxVechicleName.TabIndex = 14;
			comboBoxVehicle.Assigned = false;
			comboBoxVehicle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxVehicle.CalcManager = ultraCalcManager1;
			comboBoxVehicle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVehicle.CustomReportFieldName = "";
			comboBoxVehicle.CustomReportKey = "";
			comboBoxVehicle.CustomReportValueType = 1;
			comboBoxVehicle.DescriptionTextBox = textBoxVechicleName;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVehicle.DisplayLayout.Appearance = appearance5;
			comboBoxVehicle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVehicle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxVehicle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVehicle.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxVehicle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVehicle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVehicle.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVehicle.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVehicle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVehicle.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxVehicle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVehicle.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVehicle.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxVehicle.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxVehicle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVehicle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxVehicle.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxVehicle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVehicle.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxVehicle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxVehicle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxVehicle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxVehicle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxVehicle.Editable = true;
			comboBoxVehicle.FilterString = "";
			comboBoxVehicle.HasAllAccount = false;
			comboBoxVehicle.HasCustom = false;
			comboBoxVehicle.IsDataLoaded = false;
			comboBoxVehicle.Location = new System.Drawing.Point(317, 92);
			comboBoxVehicle.MaxDropDownItems = 12;
			comboBoxVehicle.Name = "comboBoxVehicle";
			comboBoxVehicle.ShowInactiveItems = false;
			comboBoxVehicle.ShowQuickAdd = true;
			comboBoxVehicle.Size = new System.Drawing.Size(116, 20);
			comboBoxVehicle.TabIndex = 13;
			comboBoxVehicle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxVehicle.SelectedIndexChanged += new System.EventHandler(comboBoxVehicle_SelectedIndexChanged);
			ultraCalcManager1.ContainingControl = this;
			comboBoxDriver.Assigned = false;
			comboBoxDriver.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDriver.CalcManager = ultraCalcManager1;
			comboBoxDriver.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDriver.CustomReportFieldName = "";
			comboBoxDriver.CustomReportKey = "";
			comboBoxDriver.CustomReportValueType = 1;
			comboBoxDriver.DescriptionTextBox = null;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDriver.DisplayLayout.Appearance = appearance17;
			comboBoxDriver.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDriver.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxDriver.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDriver.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxDriver.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDriver.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDriver.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDriver.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxDriver.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDriver.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDriver.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxDriver.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDriver.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDriver.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxDriver.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxDriver.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDriver.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxDriver.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxDriver.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDriver.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxDriver.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDriver.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDriver.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDriver.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDriver.Editable = true;
			comboBoxDriver.FilterString = "";
			comboBoxDriver.HasAllAccount = false;
			comboBoxDriver.HasCustom = false;
			comboBoxDriver.IsDataLoaded = false;
			comboBoxDriver.Location = new System.Drawing.Point(522, 112);
			comboBoxDriver.MaxDropDownItems = 12;
			comboBoxDriver.Name = "comboBoxDriver";
			comboBoxDriver.ShowInactiveItems = false;
			comboBoxDriver.ShowQuickAdd = true;
			comboBoxDriver.Size = new System.Drawing.Size(138, 20);
			comboBoxDriver.TabIndex = 15;
			comboBoxDriver.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "False";
			appearance29.FontData.Name = "Tahoma";
			linkLabelDriver.Appearance = appearance29;
			linkLabelDriver.AutoSize = true;
			linkLabelDriver.Location = new System.Drawing.Point(440, 113);
			linkLabelDriver.Name = "linkLabelDriver";
			linkLabelDriver.Size = new System.Drawing.Size(38, 15);
			linkLabelDriver.TabIndex = 186;
			linkLabelDriver.TabStop = true;
			linkLabelDriver.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			linkLabelDriver.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			linkLabelDriver.Value = "Driver:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			linkLabelDriver.VisitedLinkAppearance = appearance30;
			linkLabelDriver.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(linkLabelDriver_LinkClicked);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(440, 70);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(74, 13);
			label2.TabIndex = 167;
			label2.Text = "Vendor Ref #:";
			textBoxvendorReferenceNumber.Location = new System.Drawing.Point(522, 67);
			textBoxvendorReferenceNumber.MaxLength = 40;
			textBoxvendorReferenceNumber.Name = "textBoxvendorReferenceNumber";
			textBoxvendorReferenceNumber.Size = new System.Drawing.Size(138, 20);
			textBoxvendorReferenceNumber.TabIndex = 12;
			xpProductSelection.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpProductSelection.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpProductSelection.BackColor = System.Drawing.Color.DarkGray;
			xpProductSelection.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpProductSelection.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpProductSelection.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpProductSelection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpProductSelection.Location = new System.Drawing.Point(682, 134);
			xpProductSelection.Name = "xpProductSelection";
			xpProductSelection.Size = new System.Drawing.Size(72, 28);
			xpProductSelection.TabIndex = 14;
			xpProductSelection.Text = "Select Item";
			xpProductSelection.UseVisualStyleBackColor = false;
			xpProductSelection.Visible = false;
			xpProductSelection.Click += new System.EventHandler(xpProductSelection_Click);
			appearance31.FontData.BoldAsString = "False";
			appearance31.FontData.Name = "Tahoma";
			labelTaxGroup.Appearance = appearance31;
			labelTaxGroup.AutoSize = true;
			labelTaxGroup.Location = new System.Drawing.Point(255, 71);
			labelTaxGroup.Name = "labelTaxGroup";
			labelTaxGroup.Size = new System.Drawing.Size(59, 15);
			labelTaxGroup.TabIndex = 164;
			labelTaxGroup.TabStop = true;
			labelTaxGroup.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelTaxGroup.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelTaxGroup.Value = "Tax Group:";
			appearance32.ForeColor = System.Drawing.Color.Blue;
			labelTaxGroup.VisitedLinkAppearance = appearance32;
			labelTaxGroup.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelTaxGroup_LinkClicked);
			comboBoxPayeeTaxGroup.Assigned = false;
			comboBoxPayeeTaxGroup.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxPayeeTaxGroup.CalcManager = ultraCalcManager1;
			comboBoxPayeeTaxGroup.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxPayeeTaxGroup.CustomReportFieldName = "";
			comboBoxPayeeTaxGroup.CustomReportKey = "";
			comboBoxPayeeTaxGroup.CustomReportValueType = 1;
			comboBoxPayeeTaxGroup.DescriptionTextBox = null;
			appearance33.BackColor = System.Drawing.SystemColors.Window;
			appearance33.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxPayeeTaxGroup.DisplayLayout.Appearance = appearance33;
			comboBoxPayeeTaxGroup.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxPayeeTaxGroup.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance34.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance34.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance34.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.Appearance = appearance34;
			appearance35.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BandLabelAppearance = appearance35;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance36.BackColor2 = System.Drawing.SystemColors.Control;
			appearance36.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance36.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxPayeeTaxGroup.DisplayLayout.GroupByBox.PromptAppearance = appearance36;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxPayeeTaxGroup.DisplayLayout.MaxRowScrollRegions = 1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveCellAppearance = appearance37;
			appearance38.BackColor = System.Drawing.SystemColors.Highlight;
			appearance38.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.ActiveRowAppearance = appearance38;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance39.BackColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CardAreaAppearance = appearance39;
			appearance40.BorderColor = System.Drawing.Color.Silver;
			appearance40.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellAppearance = appearance40;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.CellPadding = 0;
			appearance41.BackColor = System.Drawing.SystemColors.Control;
			appearance41.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance41.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance41.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance41.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.GroupByRowAppearance = appearance41;
			appearance42.TextHAlignAsString = "Left";
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderAppearance = appearance42;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			appearance43.BorderColor = System.Drawing.Color.Silver;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowAppearance = appearance43;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance44.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxPayeeTaxGroup.DisplayLayout.Override.TemplateAddRowAppearance = appearance44;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxPayeeTaxGroup.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxPayeeTaxGroup.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxPayeeTaxGroup.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxPayeeTaxGroup.Editable = true;
			comboBoxPayeeTaxGroup.FilterString = "";
			comboBoxPayeeTaxGroup.HasAllAccount = false;
			comboBoxPayeeTaxGroup.HasCustom = false;
			comboBoxPayeeTaxGroup.IsDataLoaded = false;
			comboBoxPayeeTaxGroup.Location = new System.Drawing.Point(317, 69);
			comboBoxPayeeTaxGroup.MaxDropDownItems = 12;
			comboBoxPayeeTaxGroup.Name = "comboBoxPayeeTaxGroup";
			comboBoxPayeeTaxGroup.ReadOnly = true;
			comboBoxPayeeTaxGroup.ShowInactiveItems = false;
			comboBoxPayeeTaxGroup.ShowQuickAdd = true;
			comboBoxPayeeTaxGroup.Size = new System.Drawing.Size(116, 20);
			comboBoxPayeeTaxGroup.TabIndex = 11;
			comboBoxPayeeTaxGroup.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance45.FontData.BoldAsString = "False";
			appearance45.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance45;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(13, 68);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel3.TabIndex = 162;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Transporter:";
			appearance46.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance46;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			appearance47.FontData.BoldAsString = "False";
			appearance47.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance47;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(255, 48);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.ShowTitleToolTips = false;
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel1.TabIndex = 161;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Method:";
			appearance48.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance48;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			appearance49.FontData.BoldAsString = "False";
			appearance49.FontData.Name = "Tahoma";
			labelCurrency.Appearance = appearance49;
			labelCurrency.AutoSize = true;
			labelCurrency.Location = new System.Drawing.Point(440, 136);
			labelCurrency.Name = "labelCurrency";
			labelCurrency.ShowTitleToolTips = false;
			labelCurrency.Size = new System.Drawing.Size(52, 15);
			labelCurrency.TabIndex = 160;
			labelCurrency.TabStop = true;
			labelCurrency.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			labelCurrency.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			labelCurrency.Value = "Currency:";
			labelCurrency.Visible = false;
			appearance50.ForeColor = System.Drawing.Color.Blue;
			labelCurrency.VisitedLinkAppearance = appearance50;
			labelCurrency.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(labelCurrency_LinkClicked);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(440, 48);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(69, 13);
			label4.TabIndex = 159;
			label4.Text = "Reference 2:";
			textBoxRef2.Location = new System.Drawing.Point(522, 45);
			textBoxRef2.MaxLength = 20;
			textBoxRef2.Name = "textBoxRef2";
			textBoxRef2.Size = new System.Drawing.Size(138, 20);
			textBoxRef2.TabIndex = 9;
			comboBoxTransporter.Assigned = false;
			comboBoxTransporter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransporter.CalcManager = ultraCalcManager1;
			comboBoxTransporter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransporter.CustomReportFieldName = "";
			comboBoxTransporter.CustomReportKey = "";
			comboBoxTransporter.CustomReportValueType = 1;
			comboBoxTransporter.DescriptionTextBox = null;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			appearance51.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTransporter.DisplayLayout.Appearance = appearance51;
			comboBoxTransporter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTransporter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance52.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance52.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance52.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.GroupByBox.Appearance = appearance52;
			appearance53.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance53;
			comboBoxTransporter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance54.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance54.BackColor2 = System.Drawing.SystemColors.Control;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance54.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.PromptAppearance = appearance54;
			comboBoxTransporter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTransporter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTransporter.DisplayLayout.Override.ActiveCellAppearance = appearance55;
			appearance56.BackColor = System.Drawing.SystemColors.Highlight;
			appearance56.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTransporter.DisplayLayout.Override.ActiveRowAppearance = appearance56;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.CardAreaAppearance = appearance57;
			appearance58.BorderColor = System.Drawing.Color.Silver;
			appearance58.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTransporter.DisplayLayout.Override.CellAppearance = appearance58;
			comboBoxTransporter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTransporter.DisplayLayout.Override.CellPadding = 0;
			appearance59.BackColor = System.Drawing.SystemColors.Control;
			appearance59.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance59.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance59.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance59.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.GroupByRowAppearance = appearance59;
			appearance60.TextHAlignAsString = "Left";
			comboBoxTransporter.DisplayLayout.Override.HeaderAppearance = appearance60;
			comboBoxTransporter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTransporter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.Color.Silver;
			comboBoxTransporter.DisplayLayout.Override.RowAppearance = appearance61;
			comboBoxTransporter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTransporter.DisplayLayout.Override.TemplateAddRowAppearance = appearance62;
			comboBoxTransporter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTransporter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTransporter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTransporter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransporter.Editable = true;
			comboBoxTransporter.FilterString = "";
			comboBoxTransporter.HasAllAccount = false;
			comboBoxTransporter.HasCustom = false;
			comboBoxTransporter.IsDataLoaded = false;
			comboBoxTransporter.Location = new System.Drawing.Point(99, 67);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(109, 20);
			comboBoxTransporter.TabIndex = 10;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(406, 22);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 5;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(createFromPurchaseOrderToolStripMenuItem_Click);
			comboBoxCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
			comboBoxCurrency.Location = new System.Drawing.Point(522, 134);
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
			comboBoxCurrency.Size = new System.Drawing.Size(138, 20);
			comboBoxCurrency.TabIndex = 16;
			comboBoxCurrency.Visible = false;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CalcManager = ultraCalcManager1;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance63;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance64.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance64.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance64;
			appearance65.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance65;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance66.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance66.BackColor2 = System.Drawing.SystemColors.Control;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance66.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance66;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance67;
			appearance68.BackColor = System.Drawing.SystemColors.Highlight;
			appearance68.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance68;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance69;
			appearance70.BorderColor = System.Drawing.Color.Silver;
			appearance70.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance70;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance71.BackColor = System.Drawing.SystemColors.Control;
			appearance71.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance71.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance71.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance71.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance71;
			appearance72.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance72;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance73;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance74;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(317, 45);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(116, 20);
			comboBoxShippingMethod.TabIndex = 8;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBuyer.Assigned = false;
			comboBoxBuyer.CalcManager = ultraCalcManager1;
			comboBoxBuyer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBuyer.CustomReportFieldName = "";
			comboBoxBuyer.CustomReportKey = "";
			comboBoxBuyer.CustomReportValueType = 1;
			comboBoxBuyer.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxBuyer.DisplayLayout.Appearance = appearance75;
			comboBoxBuyer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxBuyer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxBuyer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxBuyer.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxBuyer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxBuyer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxBuyer.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxBuyer.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxBuyer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxBuyer.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxBuyer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxBuyer.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxBuyer.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxBuyer.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxBuyer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxBuyer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxBuyer.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxBuyer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxBuyer.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
			comboBoxBuyer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxBuyer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxBuyer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxBuyer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBuyer.Editable = true;
			comboBoxBuyer.FilterString = "";
			comboBoxBuyer.HasAllAccount = false;
			comboBoxBuyer.HasCustom = false;
			comboBoxBuyer.IsDataLoaded = false;
			comboBoxBuyer.Location = new System.Drawing.Point(99, 45);
			comboBoxBuyer.MaxDropDownItems = 12;
			comboBoxBuyer.Name = "comboBoxBuyer";
			comboBoxBuyer.ShowInactiveItems = false;
			comboBoxBuyer.ShowQuickAdd = true;
			comboBoxBuyer.Size = new System.Drawing.Size(149, 20);
			comboBoxBuyer.TabIndex = 7;
			comboBoxBuyer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance87.FontData.BoldAsString = "False";
			appearance87.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance87;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(13, 47);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(36, 15);
			ultraFormattedLinkLabel6.TabIndex = 146;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Buyer:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked_1);
			appearance89.FontData.BoldAsString = "True";
			appearance89.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance89;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance90.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance90;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CalcManager = ultraCalcManager1;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance91;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance92.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance92.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance92.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance92;
			appearance93.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance93;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance94.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance94.BackColor2 = System.Drawing.SystemColors.Control;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance94.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance94;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance95;
			appearance96.BackColor = System.Drawing.SystemColors.Highlight;
			appearance96.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance96;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance97;
			appearance98.BorderColor = System.Drawing.Color.Silver;
			appearance98.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance98;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance99.BackColor = System.Drawing.SystemColors.Control;
			appearance99.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance99.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance99.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance99.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance99;
			appearance100.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance100;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance101;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance102;
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
			comboBoxVendor.Location = new System.Drawing.Point(99, 23);
			comboBoxVendor.MaxDropDownItems = 12;
			comboBoxVendor.Name = "comboBoxVendor";
			comboBoxVendor.ShowConsignmentOnly = false;
			comboBoxVendor.ShowQuickAdd = true;
			comboBoxVendor.Size = new System.Drawing.Size(109, 20);
			comboBoxVendor.TabIndex = 3;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance103.FontData.BoldAsString = "True";
			appearance103.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance103;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance104.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance104;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CalcManager = ultraCalcManager1;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance105;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
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
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(440, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(210, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(225, 20);
			textBoxVendorName.TabIndex = 4;
			textBoxVendorName.TabStop = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 385);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(750, 59);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				purchaseStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
			availableQuantityToolStripMenuItem.Name = "availableQuantityToolStripMenuItem";
			availableQuantityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			availableQuantityToolStripMenuItem.Text = "Available Quantity...";
			availableQuantityToolStripMenuItem.Click += new System.EventHandler(availableQuantityToolStripMenuItem_Click);
			purchaseStatisticsToolStripMenuItem.Name = "purchaseStatisticsToolStripMenuItem";
			purchaseStatisticsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			purchaseStatisticsToolStripMenuItem.Text = "Purchase Statistics...";
			purchaseStatisticsToolStripMenuItem.Click += new System.EventHandler(purchaseStatisticsToolStripMenuItem_Click);
			itemPicToolStripMenuItem.Name = "itemPicToolStripMenuItem";
			itemPicToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemPicToolStripMenuItem.Text = "Item Photo...";
			itemPicToolStripMenuItem.Click += new System.EventHandler(itemPicToolStripMenuItem_Click);
			itemDetailsToolStripMenuItem.Name = "itemDetailsToolStripMenuItem";
			itemDetailsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			itemDetailsToolStripMenuItem.Text = "Item Details...";
			itemDetailsToolStripMenuItem.Click += new System.EventHandler(itemDetailsToolStripMenuItem_Click);
			ultraDockManager1.CompressUnpinnedTabs = false;
			ultraDockManager1.HostControl = this;
			_PurchaseGRNFormUnpinnedTabAreaLeft.Dock = System.Windows.Forms.DockStyle.Left;
			_PurchaseGRNFormUnpinnedTabAreaLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			_PurchaseGRNFormUnpinnedTabAreaLeft.Location = new System.Drawing.Point(0, 31);
			_PurchaseGRNFormUnpinnedTabAreaLeft.Name = "_PurchaseGRNFormUnpinnedTabAreaLeft";
			_PurchaseGRNFormUnpinnedTabAreaLeft.Owner = ultraDockManager1;
			_PurchaseGRNFormUnpinnedTabAreaLeft.Size = new System.Drawing.Size(0, 546);
			_PurchaseGRNFormUnpinnedTabAreaLeft.TabIndex = 126;
			_PurchaseGRNFormUnpinnedTabAreaRight.Dock = System.Windows.Forms.DockStyle.Right;
			_PurchaseGRNFormUnpinnedTabAreaRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			_PurchaseGRNFormUnpinnedTabAreaRight.Location = new System.Drawing.Point(778, 31);
			_PurchaseGRNFormUnpinnedTabAreaRight.Name = "_PurchaseGRNFormUnpinnedTabAreaRight";
			_PurchaseGRNFormUnpinnedTabAreaRight.Owner = ultraDockManager1;
			_PurchaseGRNFormUnpinnedTabAreaRight.Size = new System.Drawing.Size(0, 546);
			_PurchaseGRNFormUnpinnedTabAreaRight.TabIndex = 127;
			_PurchaseGRNFormUnpinnedTabAreaTop.Dock = System.Windows.Forms.DockStyle.Top;
			_PurchaseGRNFormUnpinnedTabAreaTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			_PurchaseGRNFormUnpinnedTabAreaTop.Location = new System.Drawing.Point(0, 31);
			_PurchaseGRNFormUnpinnedTabAreaTop.Name = "_PurchaseGRNFormUnpinnedTabAreaTop";
			_PurchaseGRNFormUnpinnedTabAreaTop.Owner = ultraDockManager1;
			_PurchaseGRNFormUnpinnedTabAreaTop.Size = new System.Drawing.Size(778, 0);
			_PurchaseGRNFormUnpinnedTabAreaTop.TabIndex = 128;
			_PurchaseGRNFormUnpinnedTabAreaBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			_PurchaseGRNFormUnpinnedTabAreaBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			_PurchaseGRNFormUnpinnedTabAreaBottom.Location = new System.Drawing.Point(0, 577);
			_PurchaseGRNFormUnpinnedTabAreaBottom.Name = "_PurchaseGRNFormUnpinnedTabAreaBottom";
			_PurchaseGRNFormUnpinnedTabAreaBottom.Owner = ultraDockManager1;
			_PurchaseGRNFormUnpinnedTabAreaBottom.Size = new System.Drawing.Size(778, 0);
			_PurchaseGRNFormUnpinnedTabAreaBottom.TabIndex = 129;
			_PurchaseGRNFormAutoHideControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			_PurchaseGRNFormAutoHideControl.Location = new System.Drawing.Point(0, 0);
			_PurchaseGRNFormAutoHideControl.Name = "_PurchaseGRNFormAutoHideControl";
			_PurchaseGRNFormAutoHideControl.Owner = ultraDockManager1;
			_PurchaseGRNFormAutoHideControl.Size = new System.Drawing.Size(0, 0);
			_PurchaseGRNFormAutoHideControl.TabIndex = 130;
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(317, 448);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 165;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxSelectedDocs.FormattingEnabled = true;
			checkedListBoxSelectedDocs.Location = new System.Drawing.Point(317, 466);
			checkedListBoxSelectedDocs.Name = "checkedListBoxSelectedDocs";
			checkedListBoxSelectedDocs.Size = new System.Drawing.Size(142, 56);
			checkedListBoxSelectedDocs.TabIndex = 166;
			checkedListBoxSelectedDocs.Visible = false;
			checkedListBoxSelectedDocs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(checkedListBoxSelectedDocs_MouseDoubleClick);
			labelProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelProcessedDocs.AutoSize = true;
			labelProcessedDocs.Location = new System.Drawing.Point(465, 446);
			labelProcessedDocs.Name = "labelProcessedDocs";
			labelProcessedDocs.Size = new System.Drawing.Size(117, 13);
			labelProcessedDocs.TabIndex = 163;
			labelProcessedDocs.Text = "Processed Documents:";
			labelProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxProcessedDocs.FormattingEnabled = true;
			checkedListBoxProcessedDocs.Location = new System.Drawing.Point(465, 465);
			checkedListBoxProcessedDocs.Name = "checkedListBoxProcessedDocs";
			checkedListBoxProcessedDocs.Size = new System.Drawing.Size(145, 56);
			checkedListBoxProcessedDocs.TabIndex = 164;
			checkedListBoxProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(checkedListBoxProcessedDocs_MouseDoubleClick);
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
			ComboBoxitemcostCategory.Location = new System.Drawing.Point(326, 278);
			ComboBoxitemcostCategory.MaxDropDownItems = 12;
			ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
			ComboBoxitemcostCategory.ShowInactiveItems = false;
			ComboBoxitemcostCategory.ShowQuickAdd = true;
			ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
			ComboBoxitemcostCategory.TabIndex = 176;
			ComboBoxitemcostCategory.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemcostCategory.Visible = false;
			comboBoxSpecification.Assigned = false;
			comboBoxSpecification.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSpecification.CalcManager = ultraCalcManager1;
			comboBoxSpecification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSpecification.CustomReportFieldName = "";
			comboBoxSpecification.CustomReportKey = "";
			comboBoxSpecification.CustomReportValueType = 1;
			comboBoxSpecification.DescriptionTextBox = null;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSpecification.DisplayLayout.Appearance = appearance117;
			comboBoxSpecification.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSpecification.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance118.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance118.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance118.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance118.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.GroupByBox.Appearance = appearance118;
			appearance119.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.BandLabelAppearance = appearance119;
			comboBoxSpecification.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance120.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance120.BackColor2 = System.Drawing.SystemColors.Control;
			appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance120.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSpecification.DisplayLayout.GroupByBox.PromptAppearance = appearance120;
			comboBoxSpecification.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSpecification.DisplayLayout.MaxRowScrollRegions = 1;
			appearance121.BackColor = System.Drawing.SystemColors.Window;
			appearance121.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSpecification.DisplayLayout.Override.ActiveCellAppearance = appearance121;
			appearance122.BackColor = System.Drawing.SystemColors.Highlight;
			appearance122.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSpecification.DisplayLayout.Override.ActiveRowAppearance = appearance122;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSpecification.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.CardAreaAppearance = appearance123;
			appearance124.BorderColor = System.Drawing.Color.Silver;
			appearance124.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSpecification.DisplayLayout.Override.CellAppearance = appearance124;
			comboBoxSpecification.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSpecification.DisplayLayout.Override.CellPadding = 0;
			appearance125.BackColor = System.Drawing.SystemColors.Control;
			appearance125.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance125.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance125.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance125.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSpecification.DisplayLayout.Override.GroupByRowAppearance = appearance125;
			appearance126.TextHAlignAsString = "Left";
			comboBoxSpecification.DisplayLayout.Override.HeaderAppearance = appearance126;
			comboBoxSpecification.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSpecification.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance127.BackColor = System.Drawing.SystemColors.Window;
			appearance127.BorderColor = System.Drawing.Color.Silver;
			comboBoxSpecification.DisplayLayout.Override.RowAppearance = appearance127;
			comboBoxSpecification.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSpecification.DisplayLayout.Override.TemplateAddRowAppearance = appearance128;
			comboBoxSpecification.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSpecification.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSpecification.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSpecification.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSpecification.Editable = true;
			comboBoxSpecification.FilterString = "";
			comboBoxSpecification.HasAllAccount = false;
			comboBoxSpecification.HasCustom = false;
			comboBoxSpecification.IsDataLoaded = false;
			comboBoxSpecification.Location = new System.Drawing.Point(436, 281);
			comboBoxSpecification.MaxDropDownItems = 12;
			comboBoxSpecification.Name = "comboBoxSpecification";
			comboBoxSpecification.ShowInactiveItems = false;
			comboBoxSpecification.Size = new System.Drawing.Size(100, 20);
			comboBoxSpecification.TabIndex = 162;
			comboBoxSpecification.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSpecification.Visible = false;
			comboBoxStyle.Assigned = false;
			comboBoxStyle.CalcManager = ultraCalcManager1;
			comboBoxStyle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxStyle.CustomReportFieldName = "";
			comboBoxStyle.CustomReportKey = "";
			comboBoxStyle.CustomReportValueType = 1;
			comboBoxStyle.DescriptionTextBox = null;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxStyle.DisplayLayout.Appearance = appearance129;
			comboBoxStyle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxStyle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance130.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance130.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance130.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance130.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.GroupByBox.Appearance = appearance130;
			appearance131.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance131;
			comboBoxStyle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance132.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance132.BackColor2 = System.Drawing.SystemColors.Control;
			appearance132.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance132.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxStyle.DisplayLayout.GroupByBox.PromptAppearance = appearance132;
			comboBoxStyle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxStyle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance133.BackColor = System.Drawing.SystemColors.Window;
			appearance133.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxStyle.DisplayLayout.Override.ActiveCellAppearance = appearance133;
			appearance134.BackColor = System.Drawing.SystemColors.Highlight;
			appearance134.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxStyle.DisplayLayout.Override.ActiveRowAppearance = appearance134;
			comboBoxStyle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxStyle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.CardAreaAppearance = appearance135;
			appearance136.BorderColor = System.Drawing.Color.Silver;
			appearance136.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxStyle.DisplayLayout.Override.CellAppearance = appearance136;
			comboBoxStyle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxStyle.DisplayLayout.Override.CellPadding = 0;
			appearance137.BackColor = System.Drawing.SystemColors.Control;
			appearance137.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance137.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance137.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance137.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxStyle.DisplayLayout.Override.GroupByRowAppearance = appearance137;
			appearance138.TextHAlignAsString = "Left";
			comboBoxStyle.DisplayLayout.Override.HeaderAppearance = appearance138;
			comboBoxStyle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxStyle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.Color.Silver;
			comboBoxStyle.DisplayLayout.Override.RowAppearance = appearance139;
			comboBoxStyle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxStyle.DisplayLayout.Override.TemplateAddRowAppearance = appearance140;
			comboBoxStyle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxStyle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxStyle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxStyle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxStyle.Editable = true;
			comboBoxStyle.FilterString = "";
			comboBoxStyle.HasAllAccount = false;
			comboBoxStyle.HasCustom = false;
			comboBoxStyle.IsDataLoaded = false;
			comboBoxStyle.Location = new System.Drawing.Point(242, 254);
			comboBoxStyle.MaxDropDownItems = 12;
			comboBoxStyle.Name = "comboBoxStyle";
			comboBoxStyle.ShowInactiveItems = false;
			comboBoxStyle.Size = new System.Drawing.Size(154, 20);
			comboBoxStyle.TabIndex = 161;
			comboBoxStyle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxStyle.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.AllowDrop = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance141.BackColor = System.Drawing.SystemColors.Window;
			appearance141.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance141;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance142.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance142.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance142.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance142;
			appearance143.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance143;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance144.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance144.BackColor2 = System.Drawing.SystemColors.Control;
			appearance144.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance144.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance144;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			appearance145.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance145;
			appearance146.BackColor = System.Drawing.SystemColors.Highlight;
			appearance146.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance146;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance147.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance147;
			appearance148.BorderColor = System.Drawing.Color.Silver;
			appearance148.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance148;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance149.BackColor = System.Drawing.SystemColors.Control;
			appearance149.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance149.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance149.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance149.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance149;
			appearance150.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance150;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance151.BackColor = System.Drawing.SystemColors.Window;
			appearance151.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance151;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance152.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance152;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 208);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(752, 237);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			docStatusLabel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			docStatusLabel.BackColor = System.Drawing.Color.Transparent;
			docStatusLabel.DocumentNumber = "";
			docStatusLabel.LinkEnabled = true;
			docStatusLabel.Location = new System.Drawing.Point(618, 451);
			docStatusLabel.Name = "docStatusLabel";
			docStatusLabel.ShowDocNumber = true;
			docStatusLabel.Size = new System.Drawing.Size(148, 56);
			docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
			docStatusLabel.TabIndex = 123;
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
			appearance153.BackColor = System.Drawing.SystemColors.Window;
			appearance153.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance153;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance154.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance154.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance154.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance154.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance154;
			appearance155.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance155;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance156.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance156.BackColor2 = System.Drawing.SystemColors.Control;
			appearance156.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance156.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance156;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance157;
			appearance158.BackColor = System.Drawing.SystemColors.Highlight;
			appearance158.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance158;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance159.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance159;
			appearance160.BorderColor = System.Drawing.Color.Silver;
			appearance160.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance160;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance161.BackColor = System.Drawing.SystemColors.Control;
			appearance161.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance161.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance161.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance161.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance161;
			appearance162.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance162;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			appearance163.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance163;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance164.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance164;
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
			comboBoxGridItem.ShowConsignmentItems = false;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowOnlyLotItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance165.BackColor = System.Drawing.SystemColors.Window;
			appearance165.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance165;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance166.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance166.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance166.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance166.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance166;
			appearance167.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance167;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance168.BackColor2 = System.Drawing.SystemColors.Control;
			appearance168.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance168.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance168;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance169.BackColor = System.Drawing.SystemColors.Window;
			appearance169.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance169;
			appearance170.BackColor = System.Drawing.SystemColors.Highlight;
			appearance170.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance170;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance171;
			appearance172.BorderColor = System.Drawing.Color.Silver;
			appearance172.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance172;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance173.BackColor = System.Drawing.SystemColors.Control;
			appearance173.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance173.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance173.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance173.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance173;
			appearance174.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance174;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance175;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance176.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance176;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(649, 182);
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
			comboBoxGridLocation.TabIndex = 122;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
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
			comboBoxJob.Location = new System.Drawing.Point(633, 182);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(100, 20);
			comboBoxJob.TabIndex = 125;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJob.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(778, 577);
			base.Controls.Add(_PurchaseGRNFormAutoHideControl);
			base.Controls.Add(ComboBoxitemcostCategory);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxSelectedDocs);
			base.Controls.Add(labelProcessedDocs);
			base.Controls.Add(checkedListBoxProcessedDocs);
			base.Controls.Add(comboBoxSpecification);
			base.Controls.Add(comboBoxStyle);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(docStatusLabel);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(panelDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(_PurchaseGRNFormUnpinnedTabAreaBottom);
			base.Controls.Add(_PurchaseGRNFormUnpinnedTabAreaTop);
			base.Controls.Add(_PurchaseGRNFormUnpinnedTabAreaRight);
			base.Controls.Add(_PurchaseGRNFormUnpinnedTabAreaLeft);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "PurchaseGRNForm";
			Text = "Goods Received Note";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxVehicle).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDriver).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxPayeeTaxGroup).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBuyer).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraDockManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSpecification).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxStyle).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
