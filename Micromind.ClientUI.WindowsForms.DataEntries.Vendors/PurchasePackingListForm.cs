using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.WindowsForms.DataEntries.Inventory;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.DataControls.OtherControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Vendors
{
	public class PurchasePackingListForm : Form, IForm
	{
		private bool allowEdit = true;

		private string sourceSysDocID = string.Empty;

		private string sourceVoucherID = string.Empty;

		private POShipmentData currentData;

		private const string TABLENAME_CONST = "PO_Shipment";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private bool allowReceiveMoreQuantity = CompanyPreferences.AllowIPurchaseQtyMoreThanPO;

		private bool allowImportGRNPackingListAddNew = CompanyPreferences.AllowImportGRNPackingListAddNew;

		private bool ShowBOLListinPackingList = CompanyPreferences.ShowBOLListinPackingList;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private bool isDataLoading;

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

		private vendorsFlatComboBox comboBoxVendor;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private TextBox textBoxVendorName;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem purchaseStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ShippingMethodsComboBox comboBoxShippingMethod;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripMenuItem transferToPOShipmentToolStripMenuItem;

		private Label label2;

		private TextBox textBoxContainerNumber;

		private PortComboBox comboBoxDestPort;

		private Label label9;

		private Label label14;

		private TextBox textBoxClearingAgent;

		private Label label13;

		private TextBox textBoxBOL;

		private MMSDateTimePicker dateTimePickerETA;

		private QuantityTextBox textBoxWeight;

		private Label label15;

		private Label label16;

		private TextBox textBoxShipper;

		private ToolStripSeparator toolStripSeparator4;

		private AmountTextBox textBoxValue;

		private Label label5;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem saveAsDraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private TransporterComboBox comboBoxTransporter;

		private ContainerSizeComboBox comboBoxContainerSize;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem toolStripMenuItemCloseOrder;

		private DocStatusLabel docStatusLabel;

		private PortComboBox comboBoxSourcePort;

		private Label label12;

		private MMSDateTimePicker dateTimePickerATD;

		private Label label11;

		private XPButton buttonSelectDocument;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel7;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel8;

		private XPButton buttonSelectBol;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private Label label4;

		private TextBox textBoxvendorRefNo;

		private Label labelSelectedDocs;

		private ListBox checkedListBoxSelectedDocs;

		private Label labelProcessedDocs;

		private ListBox checkedListBoxProcessedDocs;

		public ScreenAreas ScreenArea => ScreenAreas.Purchases;

		public int ScreenID => 3007;

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
					comboBoxSysDoc.Enabled = false;
					textBoxVoucherNumber.ReadOnly = true;
				}
				toolStripMenuItemCloseOrder.Enabled = !isNewRecord;
				docStatusLabel.Visible = !value;
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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

		public PurchasePackingListForm()
		{
			InitializeComponent();
			AddEvents();
			textBoxBOL.ReadOnly = (textBoxContainerNumber.ReadOnly = (buttonSelectBol.Visible = ShowBOLListinPackingList));
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
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
			docStatusLabel.LinkClicked += docStatusLabel_LinkClicked;
			dataGridItems.DropDownMenu.Opening += DropDownMenu_Opening;
		}

		private void DropDownMenu_Opening(object sender, CancelEventArgs e)
		{
			if (dataGridItems.ActiveRow != null && !dataGridItems.ActiveRow.Cells["SourceVoucherID"].Value.IsDBNullOrEmpty())
			{
				dataGridItems.DropDownMenu.Enabled = false;
			}
			else
			{
				dataGridItems.DropDownMenu.Enabled = true;
			}
		}

		private void docStatusLabel_LinkClicked(object sender, EventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			Control control = sender as Control;
			if (control != null)
			{
				formHelper.EditTransaction(TransactionListType.ImportGRN, control.Tag.ToString(), control.Text);
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
					_ = comboBoxVendor.DefaultCurrencyID;
					comboBoxShippingMethod.SelectedID = comboBoxVendor.DefaultShippingMethod;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			LoadVendorBillingAddress();
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
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow == null)
				{
					return;
				}
				if (e.Cell.Column.Key == "Item Code")
				{
					if (comboBoxGridItem.SelectedRow == null && e.Cell.Value != null && e.Cell.Value.ToString() != "")
					{
						comboBoxGridItem.SelectedID = e.Cell.Value.ToString();
					}
					else if (comboBoxGridItem.SelectedRow == null)
					{
						return;
					}
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
					dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
					dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
					dataGridItems.ActiveRow.Cells["Order Qty"].Value = 0;
					dataGridItems.ActiveRow.Cells["SourceVoucherID"].Value = sourceVoucherID;
					dataGridItems.ActiveRow.Cells["SourceSysDocID"].Value = sourceSysDocID;
					dataGridItems.ActiveRow.Cells["SourceRowIndex"].Value = -1;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			_ = dataGridItems.ActiveRow;
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			if (e.Cell.Activated && e.Cell.Column.Key.ToString() == "Shipped Qty")
			{
				decimal result = default(decimal);
				decimal.TryParse(e.Cell.Text, out result);
				if (result != Math.Round(result, 4))
				{
					e.Cell.Value = Math.Round(result, 4);
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
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = "";
			}
		}

		private void dataGrid_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null)
			{
				if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Shipped Qty"].Value.ToString() != "")
				{
					ErrorHelper.InformationMessage("Please select an item.");
					e.Cancel = true;
					activeRow.Cells["Item Code"].Activate();
				}
				else if (activeRow.Cells["Shipped Qty"].Value.ToString() == "")
				{
					activeRow.Cells["Shipped Qty"].Value = 0;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Shipped Qty" && isNewRecord)
			{
				decimal d = default(decimal);
				decimal d2 = decimal.Parse(e.NewValue.ToString());
				if (dataGridItems.ActiveRow.Cells["Order Qty"].Value != null && dataGridItems.ActiveRow.Cells["Order Qty"].Value.ToString() != string.Empty)
				{
					d = decimal.Parse(dataGridItems.ActiveRow.Cells["Order Qty"].Value.ToString());
				}
				if (!allowReceiveMoreQuantity && dataGridItems.ActiveRow.Cells["SourceSysDocID"].Value != null && dataGridItems.ActiveRow.Cells["SourceSysDocID"].Value.ToString() != string.Empty && dataGridItems.ActiveRow.Cells["SourceRowIndex"].Value != null && dataGridItems.ActiveRow.Cells["SourceRowIndex"].Value.ToString() != "-1" && d2 > d)
				{
					ErrorHelper.InformationMessage("Shipped quantity cannot be greater than ordered quantity.");
					e.Cancel = true;
				}
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "New Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage(UIMessages.AnalysisNotAdded);
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Shipped Qty")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Unit")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Please select a valid UOM for the item.");
			}
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new POShipmentData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.POShipmentTable.Rows[0] : currentData.POShipmentTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["VendorID"] = comboBoxVendor.SelectedID;
				dataRow["PurchaseFlow"] = CompanyPreferences.LocalPurchaseFlow;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["ContainerNumber"] = textBoxContainerNumber.Text;
				dataRow["BOLNumber"] = textBoxBOL.Text;
				dataRow["ClearingAgent"] = textBoxClearingAgent.Text;
				dataRow["VendorReferenceNo"] = textBoxvendorRefNo.Text;
				if (dateTimePickerETA.Checked)
				{
					dataRow["ETA"] = dateTimePickerETA.Value;
				}
				else
				{
					dataRow["ETA"] = DBNull.Value;
				}
				if (dateTimePickerATD.Checked)
				{
					dataRow["ATD"] = dateTimePickerATD.Value;
				}
				else
				{
					dataRow["ATD"] = DBNull.Value;
				}
				dataRow["Weight"] = textBoxWeight.Text;
				dataRow["Shipper"] = textBoxShipper.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Value"] = textBoxValue.Text;
				if (comboBoxDestPort.SelectedID != "")
				{
					dataRow["Port"] = comboBoxDestPort.SelectedID;
				}
				else
				{
					dataRow["Port"] = DBNull.Value;
				}
				if (comboBoxSourcePort.SelectedID != "")
				{
					dataRow["LoadingPort"] = comboBoxSourcePort.SelectedID;
				}
				else
				{
					dataRow["LoadingPort"] = DBNull.Value;
				}
				if (comboBoxShippingMethod.SelectedID != "")
				{
					dataRow["ShippingMethodID"] = comboBoxShippingMethod.SelectedID;
				}
				else
				{
					dataRow["ShippingMethodID"] = DBNull.Value;
				}
				dataRow["TransporterID"] = comboBoxTransporter.SelectedID;
				dataRow["ContainerSizeID"] = comboBoxContainerSize.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.POShipmentTable.Rows.Add(dataRow);
				}
				currentData.POShipmentDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.POShipmentDetailTable.NewRow();
					dataRow2.BeginEdit();
					if (row.Cells["Shipped Qty"].Value != null && !(row.Cells["Shipped Qty"].Value.ToString() == ""))
					{
						decimal d = default(decimal);
						if (row.Cells["Shipped Qty"].Value != null)
						{
							d = decimal.Parse(row.Cells["Shipped Qty"].Value.ToString());
						}
						if (!(d == 0m))
						{
							dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
							dataRow2["Quantity"] = row.Cells["Shipped Qty"].Value.ToString();
							if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
							{
								dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
							}
							dataRow2["Description"] = row.Cells["Description"].Value.ToString();
							if (row.Cells["Price"].Value != null && row.Cells["Price"].Value.ToString() != string.Empty)
							{
								dataRow2["UnitPrice"] = row.Cells["Price"].Value.ToString();
							}
							else
							{
								dataRow2["UnitPrice"] = 0;
							}
							dataRow2["RowIndex"] = row.Index;
							if (!string.IsNullOrEmpty(row.Cells["SourceRowIndex"].Value.ToString()))
							{
								dataRow2["SourceRowIndex"] = row.Cells["SourceRowIndex"].Value.ToString();
							}
							else
							{
								dataRow2["SourceRowIndex"] = DBNull.Value;
							}
							if (!string.IsNullOrEmpty(row.Cells["SourceSysDocID"].Value.ToString()))
							{
								dataRow2["SourceSysDocID"] = row.Cells["SourceSysDocID"].Value.ToString();
							}
							else
							{
								dataRow2["SourceSysDocID"] = DBNull.Value;
							}
							if (!string.IsNullOrEmpty(row.Cells["SourceVoucherID"].Value.ToString()))
							{
								dataRow2["SourceVoucherID"] = row.Cells["SourceVoucherID"].Value.ToString();
							}
							else
							{
								dataRow2["SourceVoucherID"] = DBNull.Value;
							}
							if (!string.IsNullOrEmpty(row.Cells["SourceDocType"].Value.ToString()))
							{
								dataRow2["SourceDocType"] = row.Cells["SourceDocType"].Value.ToString();
							}
							if (!string.IsNullOrEmpty(row.Cells["IsSourcedRow"].Value.ToString()))
							{
								dataRow2["IsSourcedRow"] = row.Cells["IsSourcedRow"].Value.ToString();
							}
							dataRow2.EndEdit();
							currentData.POShipmentDetailTable.Rows.Add(dataRow2);
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
				dataGridItems.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID").Caption = "PO Number";
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("SourceRowIndex", typeof(int));
				dataTable.Columns.Add("SourceDocType", typeof(int));
				dataTable.Columns.Add("IsSourcedRow", typeof(bool));
				dataTable.Columns.Add("Order Qty", typeof(decimal));
				dataTable.Columns.Add("Shipped Qty", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceSysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceRowIndex"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SourceDocType"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsSourcedRow"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(50 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Order Qty"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Shipped Qty"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				dataGridItems.AllowAddNew = false;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				dataGridItems.ShowDeleteMenu = false;
				dataGridItems.DropDownMenu.Items.Add("Ship All").Click += menuItem_Click;
				_ = CompanyPreferences.AllowImportPackingListAddNew;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void menuItem_Click(object sender, EventArgs e)
		{
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				row.Cells["Shipped Qty"].Value = row.Cells["Order Qty"].Value;
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
					currentData = Factory.POShipmentSystem.GetPOShipmentByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["PO_Shipment"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					textBoxvendorRefNo.Text = dataRow["VendorReferenceNo"].ToString();
					textBoxContainerNumber.Text = dataRow["ContainerNumber"].ToString();
					textBoxShipper.Text = dataRow["Shipper"].ToString();
					textBoxClearingAgent.Text = dataRow["ClearingAgent"].ToString();
					textBoxBOL.Text = dataRow["BOLNumber"].ToString();
					if (dataRow["Weight"] != DBNull.Value)
					{
						textBoxWeight.Text = decimal.Parse(dataRow["Weight"].ToString()).ToString(Format.QuantityFormat);
					}
					else
					{
						textBoxWeight.Text = "0.00";
					}
					if (dataRow["Value"] != DBNull.Value)
					{
						textBoxValue.Text = decimal.Parse(dataRow["Value"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxWeight.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if (dataRow["ETA"] != DBNull.Value)
					{
						dateTimePickerETA.Value = DateTime.Parse(dataRow["ETA"].ToString());
						dateTimePickerETA.Checked = true;
					}
					else
					{
						dateTimePickerETA.Checked = false;
					}
					if (dataRow["ATD"] != DBNull.Value)
					{
						dateTimePickerATD.Value = DateTime.Parse(dataRow["ATD"].ToString());
						dateTimePickerATD.Checked = true;
					}
					else
					{
						dateTimePickerATD.Checked = false;
					}
					comboBoxDestPort.SelectedID = dataRow["Port"].ToString();
					comboBoxSourcePort.SelectedID = dataRow["LoadingPort"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxShippingMethod.SelectedID = dataRow["ShippingMethodID"].ToString();
					comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
					comboBoxContainerSize.Text = dataRow["ContainerSizeID"].ToString();
					PurchaseOrderStatus purchaseOrderStatus = PurchaseOrderStatus.Open;
					if (dataRow["Status"] != DBNull.Value)
					{
						purchaseOrderStatus = (PurchaseOrderStatus)int.Parse(dataRow["Status"].ToString());
					}
					bool flag = false;
					if (dataRow.Table.Columns.Contains("ISPLReceived") && !dataRow["ISPLReceived"].IsDBNullOrEmpty())
					{
						flag = bool.Parse(dataRow["ISPLReceived"].ToString());
					}
					if (flag && purchaseOrderStatus == PurchaseOrderStatus.Open)
					{
						purchaseOrderStatus = PurchaseOrderStatus.Received;
					}
					docStatusLabel.DisplayStatus = purchaseOrderStatus.ToString();
					if (flag)
					{
						docStatusLabel.DocumentNumber = dataRow["GRNVoucherID"].ToString();
						docStatusLabel.Tag = dataRow["GRNSysDocID"].ToString();
					}
					else
					{
						docStatusLabel.DocumentNumber = "";
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("PO_Shipment_Detail") && currentData.POShipmentDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["PO_Shipment_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							if (row["UnitQuantity"] != DBNull.Value)
							{
								dataRow3["Shipped Qty"] = row["UnitQuantity"];
							}
							else
							{
								dataRow3["Shipped Qty"] = row["Quantity"];
							}
							dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
							dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
							dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
							dataRow3["SourceDocType"] = row["SourceDocType"];
							dataRow3["IsSourcedRow"] = row["IsSourcedRow"];
							dataRow3["Price"] = row["UnitPrice"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Unit"] = row["UnitID"];
							decimal result = default(decimal);
							decimal.TryParse(dataRow3["Shipped Qty"].ToString(), out result);
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
						}
						AdjustGridColumnSettings();
						if (dataRow["IsVoid"] != DBNull.Value)
						{
							IsVoid = bool.Parse(dataRow["IsVoid"].ToString());
						}
						else
						{
							IsVoid = false;
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
						if (currentData.Tables.Contains("Purchase_Receipt"))
						{
							labelProcessedDocs.Visible = true;
							checkedListBoxProcessedDocs.Visible = true;
							foreach (DataRow row4 in currentData.Tables["Purchase_Receipt"].Rows)
							{
								NameValue item2 = new NameValue(row4["VoucherID"].ToString(), row4["SysDocID"].ToString());
								checkedListBoxProcessedDocs.Items.Add(item2);
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
				bool flag2 = Factory.POShipmentSystem.CreatePOShipment(currentData, !isNewRecord);
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
			formManager.ShowApprovalPanel(approvalTaskID, "PO_Shipment", "VoucherID");
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("PO_Shipment", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:116).");
				return false;
			}
			if (!Factory.SystemDocumentSystem.HasuserAccess(comboBoxSysDoc.SelectedID, Global.DefaultLocationID) && !Global.IsUserAdmin && !AllowEditTransDiffLocation)
			{
				ErrorHelper.WarningMessage("You dont have permission to edit (SecurityRoleID:117).");
				return false;
			}
			if (!IsNewRecord)
			{
				DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(currentData, SysDocTypes.PackingList, Global.CurrentUser, includeApproveUser: false);
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxVendor.SelectedID == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				decimal d = default(decimal);
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					UltraGridRow ultraGridRow = dataGridItems.Rows[i];
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
						i--;
					}
					else if (ultraGridRow.Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
						ultraGridRow.Activate();
						return false;
					}
					if (ultraGridRow.Cells["Shipped Qty"].Value != null && ultraGridRow.Cells["Shipped Qty"].Value.ToString() != "")
					{
						d += decimal.Parse(ultraGridRow.Cells["Shipped Qty"].Value.ToString());
					}
				}
				if (d == 0m)
				{
					ErrorHelper.InformationMessage("You cannot save a packing list without any item. Please enter at least one item with Shipped Quantity.");
					return false;
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one row of item.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("PO_Shipment", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (row.Cells["Shipped Qty"].Value != null && row.Cells["Shipped Qty"].Value.ToString() != "")
				{
					result += decimal.Parse(row.Cells["Shipped Qty"].Value.ToString());
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
				comboBoxVendor.Enabled = true;
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxvendorRefNo.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				textBoxVendorName.Clear();
				sourceSysDocID = "";
				sourceVoucherID = "";
				comboBoxContainerSize.Clear();
				comboBoxVendor.Clear();
				comboBoxVendor.FilterString = "";
				docStatusLabel.Visible = false;
				dataGridItems.AllowAddNew = false;
				textBoxBOL.Clear();
				textBoxClearingAgent.Clear();
				textBoxContainerNumber.Clear();
				textBoxShipper.Clear();
				textBoxWeight.Text = "0";
				comboBoxDestPort.Clear();
				comboBoxSourcePort.Clear();
				textBoxValue.Text = 0.ToString(Format.TotalAmountFormat);
				dateTimePickerETA.Value = DateTime.Now;
				dateTimePickerETA.Checked = false;
				dateTimePickerATD.Value = DateTime.Now;
				dateTimePickerATD.Checked = false;
				comboBoxShippingMethod.Clear();
				checkedListBoxSelectedDocs.Items.Clear();
				checkedListBoxProcessedDocs.Items.Clear();
				labelSelectedDocs.Visible = false;
				checkedListBoxSelectedDocs.Visible = false;
				labelProcessedDocs.Visible = false;
				checkedListBoxProcessedDocs.Visible = false;
				comboBoxTransporter.Clear();
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				comboBoxVendor.Focus();
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
				if (!allowEdit)
				{
					ErrorHelper.InformationMessage(Application.ProductName, "You cannot delete this transfer transaction because it is already accepted or rejected.", "Document is in use.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				return Factory.POShipmentSystem.DeletePOShipment(SystemDocID, textBoxVoucherNumber.Text);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.ErrorMessage(ex.Message);
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
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
			string nextID = DatabaseHelper.GetNextID("PO_Shipment", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("PO_Shipment", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("PO_Shipment", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("PO_Shipment", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("PO_Shipment", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.PackingList);
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
				return Factory.POShipmentSystem.VoidPOShipment(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (CompanyException ex)
			{
				if (ex.Number == 1037)
				{
					ErrorHelper.ErrorMessage(ex.Message);
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
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.PackingList);
		}

		private void LoadVendorBillingAddress()
		{
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

		private void transferToPOShipmentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (isNewRecord)
				{
					DataSet pOsForPackingList = Factory.PurchaseOrderSystem.GetPOsForPackingList(comboBoxVendor.SelectedID, isImport: true);
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.ValidateSelection += form_ValidateSelection;
					selectDocumentDialog.HiddenColumns.Add("ParentVendorID");
					selectDocumentDialog.IsMultiSelect = true;
					selectDocumentDialog.DataSource = pOsForPackingList;
					selectDocumentDialog.Text = "Select Purchase Orders";
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						string text = "";
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							string text2 = sourceSysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
							string text3 = sourceVoucherID = selectedRow.Cells["Number"].Value.ToString();
							DataSet pOsItemsToShip = Factory.PurchaseOrderSystem.GetPOsItemsToShip(text2, text3);
							DataRow dataRow = pOsItemsToShip.Tables["Purchase_Order_Detail"].Rows[0];
							comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
							DataSet lastPOShipment = Factory.POShipmentSystem.GetLastPOShipment(text2, text3);
							if (lastPOShipment.Tables["PO_Shipment"].Rows.Count > 0)
							{
								DataRow dataRow2 = lastPOShipment.Tables["PO_Shipment"].Rows[0];
								if (!string.IsNullOrEmpty(dataRow2["ShippingMethodID"].ToString()))
								{
									comboBoxShippingMethod.SelectedID = dataRow2["ShippingMethodID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["Port"].ToString()))
								{
									comboBoxDestPort.SelectedID = dataRow2["Port"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["LoadingPort"].ToString()))
								{
									comboBoxSourcePort.SelectedID = dataRow2["LoadingPort"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["Shipper"].ToString()))
								{
									textBoxShipper.Text = dataRow2["Shipper"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["ClearingAgent"].ToString()))
								{
									textBoxClearingAgent.Text = dataRow2["ClearingAgent"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["Weight"].ToString()))
								{
									textBoxWeight.Text = dataRow2["Weight"].ToString();
								}
								if (textBoxContainerNumber.Text == "" && !string.IsNullOrEmpty(dataRow2["ContainerNumber"].ToString()))
								{
									textBoxContainerNumber.Text = dataRow2["ContainerNumber"].ToString();
								}
								if (textBoxBOL.Text == "" && !string.IsNullOrEmpty(dataRow2["BOLNumber"].ToString()))
								{
									textBoxBOL.Text = dataRow2["BOLNumber"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow2["TransporterID"].ToString()))
								{
									comboBoxTransporter.SelectedID = dataRow2["TransporterID"].ToString();
								}
								if (dataRow2["ETA"] != DBNull.Value)
								{
									dateTimePickerETA.Checked = true;
									dateTimePickerETA.Value = DateTime.Parse(dataRow2["ETA"].ToString());
								}
								if (dataRow2["ATD"] != DBNull.Value)
								{
									dateTimePickerATD.Checked = true;
									dateTimePickerATD.Value = DateTime.Parse(dataRow2["ATD"].ToString());
								}
							}
							else
							{
								DataRow dataRow3 = Factory.PurchaseOrderSystem.GetPurchaseOrderByID(text2, text3).PurchaseOrderTable.Rows[0];
								if (!string.IsNullOrEmpty(dataRow3["ShippingMethodID"].ToString()))
								{
									comboBoxShippingMethod.SelectedID = dataRow3["ShippingMethodID"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow3["PortDestination"].ToString()))
								{
									comboBoxDestPort.SelectedID = dataRow3["PortDestination"].ToString();
								}
								if (!string.IsNullOrEmpty(dataRow3["PortLoading"].ToString()))
								{
									comboBoxSourcePort.SelectedID = dataRow3["PortLoading"].ToString();
								}
								if (dataRow3["ETA"] != DBNull.Value)
								{
									dateTimePickerETA.Checked = true;
									dateTimePickerETA.Value = DateTime.Parse(dataRow3["ETA"].ToString());
								}
								if (dataRow3["ETD"] != DBNull.Value)
								{
									dateTimePickerATD.Checked = true;
									dateTimePickerATD.Value = DateTime.Parse(dataRow3["ETD"].ToString());
								}
							}
							DataTable dataTable = dataGridItems.DataSource as DataTable;
							ArrayList arrayList = new ArrayList();
							foreach (DataRow row in pOsItemsToShip.Tables["Purchase_Order_Detail"].Rows)
							{
								decimal result = default(decimal);
								decimal result2 = default(decimal);
								decimal num = default(decimal);
								text = row["VendorID"].ToString();
								string text4 = "";
								if (row["ParentVendorID"] != DBNull.Value)
								{
									text4 = row["ParentVendorID"].ToString();
								}
								comboBoxVendor.SelectedID = text;
								if (!arrayList.Contains(text))
								{
									arrayList.Add(text);
								}
								if (text4 != "" && !arrayList.Contains(text4))
								{
									arrayList.Add(text4);
								}
								DataRow dataRow5 = dataTable.NewRow();
								dataRow5["Item Code"] = row["ProductID"];
								dataRow5["SourceSysDocID"] = text2;
								dataRow5["SourceVoucherID"] = text3;
								dataRow5["SourceRowIndex"] = row["RowIndex"];
								dataRow5["SourceDocType"] = ItemSourceTypes.PurchaseOrder;
								dataRow5["IsSourcedRow"] = true;
								decimal d = (row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString());
								decimal.TryParse(row["QuantityShipped"].ToString(), out result);
								decimal.TryParse(row["QuantityReceived"].ToString(), out result2);
								num = d - result - result2;
								dataRow5["Order Qty"] = num;
								dataRow5["Description"] = row["Description"];
								dataRow5["Unit"] = row["UnitID"];
								dataRow5["Price"] = row["UnitPrice"];
								if (!(num <= 0m))
								{
									dataRow5.EndEdit();
									dataTable.Rows.Add(dataRow5);
								}
							}
							if (dataGridItems.Rows.Count == 0)
							{
								ErrorHelper.InformationMessage("All items in selected POs are already shipped. No PO has undelivered items.");
							}
						}
						if (CompanyPreferences.ImportPurchaseFlow == PurchaseFlows.POThenGRNThenInvoice)
						{
							if (!CompanyPreferences.AllowImportGRNPackingListAddNew)
							{
								dataGridItems.AllowAddNew = false;
							}
							else
							{
								dataGridItems.AllowAddNew = true;
							}
							AdjustGridColumnSettings();
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void AdjustGridColumnSettings()
		{
			bool flag = CompanyPreferences.AllowImportGRNPackingListAddNew;
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
				row.Cells["SourceVoucherID"].Activation = Activation.Disabled;
				row.Cells["SourceVoucherID"].Appearance.BackColorDisabled = Color.WhiteSmoke;
				row.Cells["SourceVoucherID"].Appearance.ForeColorDisabled = Color.Black;
			}
			if (flag)
			{
				dataGridItems.AllowAddNew = true;
			}
			else
			{
				dataGridItems.AllowAddNew = false;
			}
			dataGridItems.ShowDeleteMenu = flag;
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			SelectDocumentDialog selectDocumentDialog = sender as SelectDocumentDialog;
			if (selectDocumentDialog != null && selectDocumentDialog.CanClose && selectDocumentDialog.SelectedRows != null)
			{
				string a = "";
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					if (a == "")
					{
						a = selectedRow.Cells["Vendor"].Value.ToString();
					}
					if (selectedRow.Cells["ParentVendorID"].Value != null && selectedRow.Cells["ParentVendorID"].Value.ToString() != "")
					{
						a = selectedRow.Cells["ParentVendorID"].Value.ToString();
					}
					if (a != selectedRow.Cells["Vendor"].Value.ToString() && a != selectedRow.Cells["ParentVendorID"].Value.ToString())
					{
						ErrorHelper.InformationMessage("You cannot select purchase orders from multiple vendors in one shipment.");
						selectDocumentDialog.CanClose = false;
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
					DataSet pOShipmentToPrint = Factory.POShipmentSystem.GetPOShipmentToPrint(selectedID, text);
					if (pOShipmentToPrint == null || pOShipmentToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(pOShipmentToPrint, selectedID, "PO Shipment", SysDocTypes.PackingList, isPrint, showPrintDialog);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.PackingList);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 36.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.PackingList);
					currentData = (dataSet as POShipmentData);
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

		private void toolStripMenuItemCloseOrder_Click(object sender, EventArgs e)
		{
			try
			{
				UpdatePOStatusDialog updatePOStatusDialog = new UpdatePOStatusDialog();
				updatePOStatusDialog.SysDocumentType = SysDocTypes.PackingList;
				updatePOStatusDialog.SetDocument(SysDocTypes.PackingList, comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
				if (updatePOStatusDialog.ShowDialog(this) == DialogResult.OK)
				{
					object fieldValue = Factory.DatabaseSystem.GetFieldValue("PO_Shipment", "Status", "SysDocID", comboBoxSysDoc.SelectedID, "VoucherID", textBoxVoucherNumber.Text);
					if (fieldValue != null)
					{
						docStatusLabel.DisplayStatus = ((PurchaseOrderStatus)int.Parse(fieldValue.ToString())).ToString();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonSelectDocument_Click(object sender, EventArgs e)
		{
			transferToPOShipmentToolStripMenuItem_Click(sender, e);
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.PurchasePackingListFormObj);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransporter(comboBoxTransporter.SelectedID);
		}

		private void ultraFormattedLinkLabel7_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditContainerSize(comboBoxContainerSize.SelectedID);
		}

		private void ultraFormattedLinkLabel8_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPort(comboBoxDestPort.SelectedID);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditPort(comboBoxSourcePort.SelectedID);
		}

		private void buttonSelectBol_Click(object sender, EventArgs e)
		{
			DataSet bOLForPackingList = Factory.BillOfLadingSystem.GetBOLForPackingList(comboBoxVendor.SelectedID);
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.HiddenColumns.Add("Doc ID");
			selectDocumentDialog.HiddenColumns.Add("DOC Number");
			selectDocumentDialog.HiddenColumns.Add("ClearingAgent");
			selectDocumentDialog.HiddenColumns.Add("ETA");
			selectDocumentDialog.HiddenColumns.Add("ATD");
			selectDocumentDialog.HiddenColumns.Add("Shipper");
			selectDocumentDialog.HiddenColumns.Add("ShippingMethodID");
			selectDocumentDialog.IsMultiSelect = false;
			selectDocumentDialog.DataSource = bOLForPackingList;
			selectDocumentDialog.Text = "Select BOL";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					sourceSysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
					sourceVoucherID = selectedRow.Cells["DOC Number"].Value.ToString();
					if (!string.IsNullOrEmpty(selectedRow.Cells["ShippingMethodID"].Value.ToString()))
					{
						comboBoxShippingMethod.SelectedID = selectedRow.Cells["ShippingMethodID"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["Port"].Value.ToString()))
					{
						comboBoxDestPort.SelectedID = selectedRow.Cells["Port"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["LoadingPort"].Value.ToString()))
					{
						comboBoxSourcePort.SelectedID = selectedRow.Cells["LoadingPort"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["Shipper"].Value.ToString()))
					{
						textBoxShipper.Text = selectedRow.Cells["Shipper"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["ClearingAgent"].Value.ToString()))
					{
						textBoxClearingAgent.Text = selectedRow.Cells["ClearingAgent"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["Weight"].Value.ToString()))
					{
						textBoxWeight.Text = selectedRow.Cells["Weight"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["TransporterID"].Value.ToString()))
					{
						comboBoxTransporter.SelectedID = selectedRow.Cells["TransporterID"].Value.ToString();
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["ETA"].Value.ToString()))
					{
						dateTimePickerETA.Checked = true;
						dateTimePickerETA.Value = DateTime.Parse(selectedRow.Cells["ETA"].Value.ToString());
					}
					if (!string.IsNullOrEmpty(selectedRow.Cells["ATD"].Value.ToString()))
					{
						dateTimePickerATD.Checked = true;
						dateTimePickerATD.Value = DateTime.Parse(selectedRow.Cells["ATD"].Value.ToString());
					}
					textBoxBOL.Text = selectedRow.Cells["BOLNumber"].Value.ToString();
					if (comboBoxVendor.SelectedID == "")
					{
						comboBoxVendor.SelectedID = selectedRow.Cells["VendorID"].Value.ToString();
					}
					textBoxContainerNumber.Text = selectedRow.Cells["Container Number"].Value.ToString();
					comboBoxContainerSize.SelectedID = selectedRow.Cells["Container Size"].Value.ToString();
				}
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

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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

		private void checkedListBoxProcessedDocs_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (checkedListBoxProcessedDocs.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxProcessedDocs.SelectedItem as NameValue;
				new FormHelper().EditTransaction(nameValue.ID, nameValue.Name);
			}
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditShippingMethod(comboBoxShippingMethod.SelectedID);
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.PurchasePackingListForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItemCloseOrder = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			transferToPOShipmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			label4 = new System.Windows.Forms.Label();
			textBoxvendorRefNo = new System.Windows.Forms.TextBox();
			buttonSelectBol = new Micromind.UISupport.XPButton();
			ultraFormattedLinkLabel8 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel7 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			buttonSelectDocument = new Micromind.UISupport.XPButton();
			label12 = new System.Windows.Forms.Label();
			dateTimePickerATD = new Micromind.UISupport.MMSDateTimePicker(components);
			label11 = new System.Windows.Forms.Label();
			comboBoxSourcePort = new Micromind.DataControls.PortComboBox();
			comboBoxContainerSize = new Micromind.DataControls.ContainerSizeComboBox();
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			textBoxValue = new Micromind.UISupport.AmountTextBox();
			label16 = new System.Windows.Forms.Label();
			textBoxWeight = new Micromind.UISupport.QuantityTextBox();
			label14 = new System.Windows.Forms.Label();
			textBoxClearingAgent = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			textBoxBOL = new System.Windows.Forms.TextBox();
			dateTimePickerETA = new Micromind.UISupport.MMSDateTimePicker(components);
			comboBoxDestPort = new Micromind.DataControls.PortComboBox();
			label15 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxContainerNumber = new System.Windows.Forms.TextBox();
			comboBoxShippingMethod = new Micromind.DataControls.ShippingMethodsComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			textBoxShipper = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			purchaseStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			labelSelectedDocs = new System.Windows.Forms.Label();
			checkedListBoxSelectedDocs = new System.Windows.Forms.ListBox();
			labelProcessedDocs = new System.Windows.Forms.Label();
			checkedListBoxProcessedDocs = new System.Windows.Forms.ListBox();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSourcePort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContainerSize).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
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
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator3,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonInformation,
				toolStripDropDownButton1,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(864, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7]
			{
				duplicateToolStripMenuItem,
				saveAsDraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				toolStripSeparator6,
				toolStripMenuItemCloseOrder,
				toolStripSeparator4,
				transferToPOShipmentToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			duplicateToolStripMenuItem.Text = "Duplicate";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			saveAsDraftToolStripMenuItem.Name = "saveAsDraftToolStripMenuItem";
			saveAsDraftToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			saveAsDraftToolStripMenuItem.Text = "Save as Draft";
			saveAsDraftToolStripMenuItem.Click += new System.EventHandler(saveAsDraftToolStripMenuItem_Click);
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadDraftToolStripMenuItem.Click += new System.EventHandler(loadDraftToolStripMenuItem_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(200, 6);
			toolStripMenuItemCloseOrder.Name = "toolStripMenuItemCloseOrder";
			toolStripMenuItemCloseOrder.Size = new System.Drawing.Size(203, 22);
			toolStripMenuItemCloseOrder.Text = "Change Order Status...";
			toolStripMenuItemCloseOrder.Click += new System.EventHandler(toolStripMenuItemCloseOrder_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(200, 6);
			transferToPOShipmentToolStripMenuItem.Name = "transferToPOShipmentToolStripMenuItem";
			transferToPOShipmentToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
			transferToPOShipmentToolStripMenuItem.Text = "Select Purchase Orders...";
			transferToPOShipmentToolStripMenuItem.Click += new System.EventHandler(transferToPOShipmentToolStripMenuItem_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 529);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(864, 40);
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
			linePanelDown.Size = new System.Drawing.Size(864, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(754, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(554, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(138, 20);
			dateTimePickerDate.TabIndex = 5;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(140, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(472, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(554, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(138, 20);
			textBoxRef1.TabIndex = 6;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 443);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(327, 76);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 442);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance.FontData.BoldAsString = "True";
			appearance.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(212, 4);
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
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(textBoxvendorRefNo);
			panelDetails.Controls.Add(buttonSelectBol);
			panelDetails.Controls.Add(ultraFormattedLinkLabel8);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel7);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(buttonSelectDocument);
			panelDetails.Controls.Add(label12);
			panelDetails.Controls.Add(dateTimePickerATD);
			panelDetails.Controls.Add(label11);
			panelDetails.Controls.Add(comboBoxSourcePort);
			panelDetails.Controls.Add(comboBoxContainerSize);
			panelDetails.Controls.Add(comboBoxTransporter);
			panelDetails.Controls.Add(textBoxValue);
			panelDetails.Controls.Add(label16);
			panelDetails.Controls.Add(textBoxWeight);
			panelDetails.Controls.Add(label14);
			panelDetails.Controls.Add(textBoxClearingAgent);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(label13);
			panelDetails.Controls.Add(textBoxBOL);
			panelDetails.Controls.Add(dateTimePickerETA);
			panelDetails.Controls.Add(comboBoxDestPort);
			panelDetails.Controls.Add(label15);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxContainerNumber);
			panelDetails.Controls.Add(comboBoxShippingMethod);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxVendorName);
			panelDetails.Controls.Add(textBoxShipper);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(0, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(849, 137);
			panelDetails.TabIndex = 0;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(629, 94);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(74, 13);
			label4.TabIndex = 200;
			label4.Text = "Vendor Ref #:";
			textBoxvendorRefNo.Location = new System.Drawing.Point(703, 91);
			textBoxvendorRefNo.MaxLength = 40;
			textBoxvendorRefNo.Name = "textBoxvendorRefNo";
			textBoxvendorRefNo.Size = new System.Drawing.Size(138, 20);
			textBoxvendorRefNo.TabIndex = 17;
			buttonSelectBol.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectBol.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectBol.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectBol.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectBol.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectBol.Location = new System.Drawing.Point(437, 89);
			buttonSelectBol.Name = "buttonSelectBol";
			buttonSelectBol.Size = new System.Drawing.Size(29, 22);
			buttonSelectBol.TabIndex = 198;
			buttonSelectBol.Text = "...";
			buttonSelectBol.UseVisualStyleBackColor = false;
			buttonSelectBol.Click += new System.EventHandler(buttonSelectBol_Click);
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel8.Appearance = appearance3;
			ultraFormattedLinkLabel8.AutoSize = true;
			ultraFormattedLinkLabel8.Location = new System.Drawing.Point(538, 71);
			ultraFormattedLinkLabel8.Name = "ultraFormattedLinkLabel8";
			ultraFormattedLinkLabel8.Size = new System.Drawing.Size(53, 15);
			ultraFormattedLinkLabel8.TabIndex = 193;
			ultraFormattedLinkLabel8.TabStop = true;
			ultraFormattedLinkLabel8.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel8.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel8.Value = "Dest.Port:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel8.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel8.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel8_LinkClicked);
			appearance5.FontData.BoldAsString = "False";
			appearance5.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance5;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(370, 70);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel6.TabIndex = 197;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "Source Port:";
			appearance6.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance6;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance7.FontData.BoldAsString = "False";
			appearance7.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel7.Appearance = appearance7;
			ultraFormattedLinkLabel7.AutoSize = true;
			ultraFormattedLinkLabel7.Location = new System.Drawing.Point(211, 114);
			ultraFormattedLinkLabel7.Name = "ultraFormattedLinkLabel7";
			ultraFormattedLinkLabel7.Size = new System.Drawing.Size(78, 15);
			ultraFormattedLinkLabel7.TabIndex = 196;
			ultraFormattedLinkLabel7.TabStop = true;
			ultraFormattedLinkLabel7.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel7.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel7.Value = "Container Size:";
			appearance8.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel7.VisitedLinkAppearance = appearance8;
			ultraFormattedLinkLabel7.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel7_LinkClicked);
			appearance9.FontData.BoldAsString = "False";
			appearance9.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance9;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(13, 115);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel1.TabIndex = 192;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Transporter:";
			appearance10.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance10;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			appearance11.FontData.BoldAsString = "False";
			appearance11.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance11;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(214, 48);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(90, 15);
			ultraFormattedLinkLabel3.TabIndex = 179;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Shipping Method:";
			appearance12.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance12;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			buttonSelectDocument.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocument.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocument.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocument.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocument.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocument.Location = new System.Drawing.Point(437, 22);
			buttonSelectDocument.Name = "buttonSelectDocument";
			buttonSelectDocument.Size = new System.Drawing.Size(29, 22);
			buttonSelectDocument.TabIndex = 4;
			buttonSelectDocument.Text = "...";
			buttonSelectDocument.UseVisualStyleBackColor = false;
			buttonSelectDocument.Click += new System.EventHandler(buttonSelectDocument_Click);
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(213, 71);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(31, 13);
			label12.TabIndex = 165;
			label12.Text = "ETA:";
			dateTimePickerATD.Checked = false;
			dateTimePickerATD.CustomFormat = " ";
			dateTimePickerATD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerATD.Location = new System.Drawing.Point(99, 68);
			dateTimePickerATD.Name = "dateTimePickerATD";
			dateTimePickerATD.ShowCheckBox = true;
			dateTimePickerATD.Size = new System.Drawing.Size(109, 20);
			dateTimePickerATD.TabIndex = 10;
			dateTimePickerATD.Value = new System.DateTime(0L);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(12, 71);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(32, 13);
			label11.TabIndex = 164;
			label11.Text = "ATD:";
			comboBoxSourcePort.Assigned = false;
			comboBoxSourcePort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSourcePort.CustomReportFieldName = "";
			comboBoxSourcePort.CustomReportKey = "";
			comboBoxSourcePort.CustomReportValueType = 1;
			comboBoxSourcePort.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSourcePort.DisplayLayout.Appearance = appearance13;
			comboBoxSourcePort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSourcePort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSourcePort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxSourcePort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSourcePort.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxSourcePort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSourcePort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSourcePort.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSourcePort.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxSourcePort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSourcePort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSourcePort.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxSourcePort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSourcePort.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSourcePort.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxSourcePort.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxSourcePort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSourcePort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxSourcePort.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxSourcePort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSourcePort.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxSourcePort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxSourcePort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxSourcePort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxSourcePort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSourcePort.Editable = true;
			comboBoxSourcePort.FilterString = "";
			comboBoxSourcePort.HasAllAccount = false;
			comboBoxSourcePort.HasCustom = false;
			comboBoxSourcePort.IsDataLoaded = false;
			comboBoxSourcePort.Location = new System.Drawing.Point(437, 68);
			comboBoxSourcePort.MaxDropDownItems = 12;
			comboBoxSourcePort.Name = "comboBoxSourcePort";
			comboBoxSourcePort.ShowInactiveItems = false;
			comboBoxSourcePort.ShowQuickAdd = true;
			comboBoxSourcePort.Size = new System.Drawing.Size(95, 20);
			comboBoxSourcePort.TabIndex = 12;
			comboBoxSourcePort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxContainerSize.Assigned = false;
			comboBoxContainerSize.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxContainerSize.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxContainerSize.CustomReportFieldName = "";
			comboBoxContainerSize.CustomReportKey = "";
			comboBoxContainerSize.CustomReportValueType = 1;
			comboBoxContainerSize.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxContainerSize.DisplayLayout.Appearance = appearance25;
			comboBoxContainerSize.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxContainerSize.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContainerSize.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxContainerSize.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxContainerSize.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxContainerSize.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxContainerSize.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxContainerSize.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxContainerSize.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxContainerSize.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxContainerSize.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxContainerSize.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxContainerSize.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxContainerSize.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxContainerSize.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxContainerSize.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxContainerSize.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxContainerSize.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxContainerSize.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxContainerSize.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxContainerSize.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxContainerSize.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxContainerSize.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxContainerSize.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxContainerSize.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxContainerSize.Editable = true;
			comboBoxContainerSize.FilterString = "";
			comboBoxContainerSize.HasAllAccount = false;
			comboBoxContainerSize.HasCustom = false;
			comboBoxContainerSize.IsDataLoaded = false;
			comboBoxContainerSize.Location = new System.Drawing.Point(293, 112);
			comboBoxContainerSize.MaxDropDownItems = 12;
			comboBoxContainerSize.Name = "comboBoxContainerSize";
			comboBoxContainerSize.ShowInactiveItems = false;
			comboBoxContainerSize.ShowQuickAdd = true;
			comboBoxContainerSize.Size = new System.Drawing.Size(100, 20);
			comboBoxContainerSize.TabIndex = 19;
			comboBoxContainerSize.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxTransporter.Assigned = false;
			comboBoxTransporter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransporter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransporter.CustomReportFieldName = "";
			comboBoxTransporter.CustomReportKey = "";
			comboBoxTransporter.CustomReportValueType = 1;
			comboBoxTransporter.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTransporter.DisplayLayout.Appearance = appearance37;
			comboBoxTransporter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTransporter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxTransporter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxTransporter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTransporter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTransporter.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTransporter.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTransporter.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxTransporter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTransporter.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxTransporter.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxTransporter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTransporter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxTransporter.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxTransporter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTransporter.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			comboBoxTransporter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTransporter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTransporter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTransporter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransporter.Editable = true;
			comboBoxTransporter.FilterString = "";
			comboBoxTransporter.HasAllAccount = false;
			comboBoxTransporter.HasCustom = false;
			comboBoxTransporter.IsDataLoaded = false;
			comboBoxTransporter.Location = new System.Drawing.Point(99, 112);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(109, 20);
			comboBoxTransporter.TabIndex = 18;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxValue.AllowDecimal = true;
			textBoxValue.CustomReportFieldName = "";
			textBoxValue.CustomReportKey = "";
			textBoxValue.CustomReportValueType = 1;
			textBoxValue.IsComboTextBox = false;
			textBoxValue.IsModified = false;
			textBoxValue.Location = new System.Drawing.Point(519, 91);
			textBoxValue.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxValue.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxValue.Name = "textBoxValue";
			textBoxValue.NullText = "0";
			textBoxValue.Size = new System.Drawing.Size(104, 20);
			textBoxValue.TabIndex = 16;
			textBoxValue.Text = "0.00";
			textBoxValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxValue.Value = new decimal(new int[4]
			{
				0,
				0,
				0,
				131072
			});
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(472, 48);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(46, 13);
			label16.TabIndex = 150;
			label16.Text = "Shipper:";
			textBoxWeight.CustomReportFieldName = "";
			textBoxWeight.CustomReportKey = "";
			textBoxWeight.CustomReportValueType = 1;
			textBoxWeight.IsComboTextBox = false;
			textBoxWeight.IsModified = false;
			textBoxWeight.Location = new System.Drawing.Point(99, 91);
			textBoxWeight.MaxLength = 10;
			textBoxWeight.Name = "textBoxWeight";
			textBoxWeight.Size = new System.Drawing.Size(109, 20);
			textBoxWeight.TabIndex = 14;
			textBoxWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(399, 116);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(79, 13);
			label14.TabIndex = 148;
			label14.Text = "Clearing Agent:";
			textBoxClearingAgent.Location = new System.Drawing.Point(484, 113);
			textBoxClearingAgent.MaxLength = 20;
			textBoxClearingAgent.Name = "textBoxClearingAgent";
			textBoxClearingAgent.Size = new System.Drawing.Size(174, 20);
			textBoxClearingAgent.TabIndex = 20;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(474, 94);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(37, 13);
			label5.TabIndex = 146;
			label5.Text = "Value:";
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(212, 93);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(48, 13);
			label13.TabIndex = 146;
			label13.Text = "BOL No:";
			textBoxBOL.Location = new System.Drawing.Point(268, 91);
			textBoxBOL.MaxLength = 20;
			textBoxBOL.Name = "textBoxBOL";
			textBoxBOL.Size = new System.Drawing.Size(167, 20);
			textBoxBOL.TabIndex = 15;
			dateTimePickerETA.Checked = false;
			dateTimePickerETA.CustomFormat = " ";
			dateTimePickerETA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			dateTimePickerETA.Location = new System.Drawing.Point(257, 68);
			dateTimePickerETA.Name = "dateTimePickerETA";
			dateTimePickerETA.ShowCheckBox = true;
			dateTimePickerETA.Size = new System.Drawing.Size(109, 20);
			dateTimePickerETA.TabIndex = 11;
			dateTimePickerETA.Value = new System.DateTime(0L);
			comboBoxDestPort.Assigned = false;
			comboBoxDestPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDestPort.CustomReportFieldName = "";
			comboBoxDestPort.CustomReportKey = "";
			comboBoxDestPort.CustomReportValueType = 1;
			comboBoxDestPort.DescriptionTextBox = null;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDestPort.DisplayLayout.Appearance = appearance49;
			comboBoxDestPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDestPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			comboBoxDestPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDestPort.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			comboBoxDestPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDestPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDestPort.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDestPort.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			comboBoxDestPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDestPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDestPort.DisplayLayout.Override.CellAppearance = appearance56;
			comboBoxDestPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDestPort.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDestPort.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			comboBoxDestPort.DisplayLayout.Override.HeaderAppearance = appearance58;
			comboBoxDestPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDestPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			comboBoxDestPort.DisplayLayout.Override.RowAppearance = appearance59;
			comboBoxDestPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDestPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			comboBoxDestPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDestPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDestPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDestPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDestPort.Editable = true;
			comboBoxDestPort.FilterString = "";
			comboBoxDestPort.HasAllAccount = false;
			comboBoxDestPort.HasCustom = false;
			comboBoxDestPort.IsDataLoaded = false;
			comboBoxDestPort.Location = new System.Drawing.Point(600, 68);
			comboBoxDestPort.MaxDropDownItems = 12;
			comboBoxDestPort.Name = "comboBoxDestPort";
			comboBoxDestPort.ShowInactiveItems = false;
			comboBoxDestPort.ShowQuickAdd = true;
			comboBoxDestPort.Size = new System.Drawing.Size(92, 20);
			comboBoxDestPort.TabIndex = 13;
			comboBoxDestPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(10, 93);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(44, 13);
			label15.TabIndex = 142;
			label15.Text = "Weight:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(179, 69);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(31, 13);
			label9.TabIndex = 142;
			label9.Text = "ETA:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(10, 47);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(72, 13);
			label2.TabIndex = 142;
			label2.Text = "Container No:";
			textBoxContainerNumber.Location = new System.Drawing.Point(99, 45);
			textBoxContainerNumber.MaxLength = 15;
			textBoxContainerNumber.Name = "textBoxContainerNumber";
			textBoxContainerNumber.Size = new System.Drawing.Size(109, 20);
			textBoxContainerNumber.TabIndex = 7;
			comboBoxShippingMethod.Assigned = false;
			comboBoxShippingMethod.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxShippingMethod.CustomReportFieldName = "";
			comboBoxShippingMethod.CustomReportKey = "";
			comboBoxShippingMethod.CustomReportValueType = 1;
			comboBoxShippingMethod.DescriptionTextBox = null;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxShippingMethod.DisplayLayout.Appearance = appearance61;
			comboBoxShippingMethod.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxShippingMethod.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxShippingMethod.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			comboBoxShippingMethod.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxShippingMethod.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxShippingMethod.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxShippingMethod.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxShippingMethod.DisplayLayout.Override.CellAppearance = appearance68;
			comboBoxShippingMethod.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxShippingMethod.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxShippingMethod.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			comboBoxShippingMethod.DisplayLayout.Override.HeaderAppearance = appearance70;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxShippingMethod.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			comboBoxShippingMethod.DisplayLayout.Override.RowAppearance = appearance71;
			comboBoxShippingMethod.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxShippingMethod.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			comboBoxShippingMethod.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxShippingMethod.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxShippingMethod.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxShippingMethod.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxShippingMethod.Editable = true;
			comboBoxShippingMethod.FilterString = "";
			comboBoxShippingMethod.HasAllAccount = false;
			comboBoxShippingMethod.HasCustom = false;
			comboBoxShippingMethod.IsDataLoaded = false;
			comboBoxShippingMethod.Location = new System.Drawing.Point(308, 45);
			comboBoxShippingMethod.MaxDropDownItems = 12;
			comboBoxShippingMethod.Name = "comboBoxShippingMethod";
			comboBoxShippingMethod.ShowInactiveItems = false;
			comboBoxShippingMethod.ShowQuickAdd = true;
			comboBoxShippingMethod.Size = new System.Drawing.Size(158, 20);
			comboBoxShippingMethod.TabIndex = 8;
			comboBoxShippingMethod.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance73.FontData.BoldAsString = "True";
			appearance73.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance73;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 24);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 129;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance74.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance74;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance75;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance76.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance76.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance76;
			appearance77.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance77;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance78.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance78.BackColor2 = System.Drawing.SystemColors.Control;
			appearance78.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance78.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance78;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance79;
			appearance80.BackColor = System.Drawing.SystemColors.Highlight;
			appearance80.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance80;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance81;
			appearance82.BorderColor = System.Drawing.Color.Silver;
			appearance82.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance82;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance83.BackColor = System.Drawing.SystemColors.Control;
			appearance83.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance83.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance83.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance83.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance83;
			appearance84.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance84;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance85;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance86;
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
			comboBoxVendor.TabIndex = 2;
			comboBoxVendor.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance87.FontData.BoldAsString = "True";
			appearance87.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance87;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(11, 3);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance88.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance88;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
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
			mmLabel1.Location = new System.Drawing.Point(472, 3);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			textBoxVendorName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxVendorName.Location = new System.Drawing.Point(211, 23);
			textBoxVendorName.MaxLength = 64;
			textBoxVendorName.Name = "textBoxVendorName";
			textBoxVendorName.ReadOnly = true;
			textBoxVendorName.Size = new System.Drawing.Size(224, 20);
			textBoxVendorName.TabIndex = 3;
			textBoxVendorName.TabStop = false;
			textBoxShipper.Location = new System.Drawing.Point(554, 45);
			textBoxShipper.MaxLength = 15;
			textBoxShipper.Name = "textBoxShipper";
			textBoxShipper.Size = new System.Drawing.Size(138, 20);
			textBoxShipper.TabIndex = 9;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(13, 378);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(836, 58);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
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
			docStatusLabel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			docStatusLabel.BackColor = System.Drawing.Color.Transparent;
			docStatusLabel.DocumentNumber = "";
			docStatusLabel.LinkEnabled = true;
			docStatusLabel.Location = new System.Drawing.Point(719, 454);
			docStatusLabel.Margin = new System.Windows.Forms.Padding(4);
			docStatusLabel.Name = "docStatusLabel";
			docStatusLabel.ShowDocNumber = true;
			docStatusLabel.Size = new System.Drawing.Size(131, 65);
			docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
			docStatusLabel.TabIndex = 121;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(48, 232);
			productPhotoViewer.Margin = new System.Windows.Forms.Padding(4);
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance101;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance102;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance104.BackColor2 = System.Drawing.SystemColors.Control;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance105;
			appearance106.BackColor = System.Drawing.SystemColors.Highlight;
			appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance106;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance107;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance108;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance109.BackColor = System.Drawing.SystemColors.Control;
			appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance109;
			appearance110.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance110;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance111;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 167);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(837, 270);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance113;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance114;
			appearance115.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance115;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance116.BackColor2 = System.Drawing.SystemColors.Control;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance116.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance116;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance117;
			appearance118.BackColor = System.Drawing.SystemColors.Highlight;
			appearance118.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance118;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance119;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			appearance120.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance120;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance121.BackColor = System.Drawing.SystemColors.Control;
			appearance121.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance121.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance121;
			appearance122.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance122;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance123;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance124;
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
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(380, 443);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(109, 13);
			labelSelectedDocs.TabIndex = 169;
			labelSelectedDocs.Text = "Selected Documents:";
			labelSelectedDocs.Visible = false;
			checkedListBoxSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxSelectedDocs.FormattingEnabled = true;
			checkedListBoxSelectedDocs.Location = new System.Drawing.Point(380, 461);
			checkedListBoxSelectedDocs.Name = "checkedListBoxSelectedDocs";
			checkedListBoxSelectedDocs.Size = new System.Drawing.Size(142, 56);
			checkedListBoxSelectedDocs.TabIndex = 170;
			checkedListBoxSelectedDocs.Visible = false;
			checkedListBoxSelectedDocs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(checkedListBoxSelectedDocs_MouseDoubleClick);
			labelProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelProcessedDocs.AutoSize = true;
			labelProcessedDocs.Location = new System.Drawing.Point(528, 441);
			labelProcessedDocs.Name = "labelProcessedDocs";
			labelProcessedDocs.Size = new System.Drawing.Size(117, 13);
			labelProcessedDocs.TabIndex = 167;
			labelProcessedDocs.Text = "Processed Documents:";
			labelProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxProcessedDocs.FormattingEnabled = true;
			checkedListBoxProcessedDocs.Location = new System.Drawing.Point(528, 460);
			checkedListBoxProcessedDocs.Name = "checkedListBoxProcessedDocs";
			checkedListBoxProcessedDocs.Size = new System.Drawing.Size(145, 56);
			checkedListBoxProcessedDocs.TabIndex = 168;
			checkedListBoxProcessedDocs.Visible = false;
			checkedListBoxProcessedDocs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(checkedListBoxProcessedDocs_MouseDoubleClick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(864, 569);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxSelectedDocs);
			base.Controls.Add(labelProcessedDocs);
			base.Controls.Add(checkedListBoxProcessedDocs);
			base.Controls.Add(docStatusLabel);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(panelDetails);
			base.Controls.Add(labelVoided);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(648, 395);
			base.Name = "PurchasePackingListForm";
			Text = "Purchase Packing List";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSourcePort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxContainerSize).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDestPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxShippingMethod).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
