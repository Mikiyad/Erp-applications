using DevExpress.XtraEditors;
using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinToolTip;
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ItemTransactionForm : Form, IForm
	{
		private bool allowEdit = true;

		private ItemSourceTypes sourceDocType;

		private bool allowChangeCustomer;

		private string clUserID = "";

		private ItemTransactionData currentData;

		private const string TABLENAME_CONST = "Item_Transaction";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentCustomerAddressID = "";

		private DataTable invoiceDNoteTable;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private bool loadItemDescFromPriceList;

		private DataSet priceListData;

		private bool useJobCosting;

		private bool showLotDetail;

		private bool LoadItemFeatures;

		private bool ExclueZeroQtyInDN = CompanyPreferences.ExcludeZeroQtyInDN;

		private string partyID;

		private string locationID;

		private string sysDocID = "";

		private int sysDocType;

		private bool supressInventoryMessage;

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool isVoid;

		private bool isDiscountPercent;

		public string currentkey = "";

		private IContainer components;

		private ToolStrip toolStrip1;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton ButtonClose;

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

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem selectInvoiceToolStripMenuItem;

		private ToolStripMenuItem createFromSalesOrderToolStripMenuItem;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator7;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonBalance;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripMenuItem createFromSalesQuoteToolStripMenuItem;

		private UltraToolTipManager ultraToolTipManager1;

		private ToolStripButton toolStripButtonInfo;

		private MMLabel mmLabel1;

		private DateTimePicker dateTimePickerDate;

		private UltraGroupBox ultraGroupBox1;

		private Label label2;

		private XPButton buttonSelectItem;

		private TextBox textBoxItem;

		private Label label1;

		private TextBox textBoxQuantity;

		private ProductPhotoViewer productPhotoViewer;

		private Label labelVoided;

		private DataEntryGrid dataGridItems;

		private CostCategoryComboBox ComboBoxitemcostCategory;

		private JobComboBox ComboBoxitemJob;

		private LocationComboBox comboBoxGridLocation;

		private ProductComboBox comboBoxGridItem;

		private XPButton buttonLocation;

		private TextBox textBoxLocation;

		private ContainerStstusComboBox comboBoxPartyType;

		private Label label9;

		private Label label8;

		private Label label7;

		private Label label6;

		private TextBox textBoxSysDocName;

		private XPButton buttonSelectDocID;

		private TextBox textBoxSysDocID;

		private XPButton buttonSelectParty;

		private TextBox textBoxCustomerName;

		private TextBox textBoxVoucherNumber;

		private XPButton buttonEnter;

		private Label label4;

		private Panel panelxml;

		private SimpleButton simpleButtonFirst;

		private SimpleButton simpleButtonLast;

		private SimpleButton simpleButtonNext;

		private SimpleButton simpleButtonPrevious;

		private SimpleButton simpleButtonFind;

		private SimpleButton simpleButtonSynchronize;

		private XPButton ButtonDraft;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 2007;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

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
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
					if (!IsVoid)
					{
						buttonVoid.Enabled = true;
					}
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
				toolStripButtonAttach.Enabled = !value;
				toolStripButtonDistribution.Enabled = !value;
				toolStripButtonInformation.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = true;
					buttonVoid.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = true;
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

		public ItemTransactionForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			dataGridItems.AfterCellActivate += dataGridItems_AfterCellActivate;
			dataGridItems.GotFocus += dataGridItems_GotFocus;
			base.FormClosing += Form_FormClosing;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridItems.BeforeExitEditMode += dataGridItems_BeforeExitEditMode;
		}

		private void dataGridItems_InitializeRow(object sender, InitializeRowEventArgs e)
		{
			e.Row.Appearance.FontData.SizeInPoints = 12f;
		}

		private void comboBoxShippingAddressID_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxBillingAddress_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void docStatusLabel_LinkClicked(object sender, EventArgs e)
		{
			FormHelper formHelper = new FormHelper();
			Control control = sender as Control;
			if (control != null)
			{
				formHelper.EditTransaction(TransactionListType.SalesInvoice, control.Tag.ToString(), control.Text);
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
			}
			else
			{
				if (CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenDNThenInvoice || activeRow == null || !activeRow.DataChanged)
				{
					return;
				}
				ItemTypes itemTypes = ItemTypes.Inventory;
				if (activeRow.Cells["ItemType"].Value.ToString() != "")
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(activeRow.Cells["ItemType"].Value.ToString()));
				}
				if (CompanyPreferences.NegativeQuantityAction == 1 && itemTypes != ItemTypes.ConsignmentItem)
				{
					return;
				}
				string text = "";
				string voucherID = "";
				if (!IsNewRecord)
				{
					text = textBoxSysDocID.Text;
					voucherID = textBoxVoucherNumber.Text;
				}
				if (Factory.ProductSystem.IsSufficientQuantityOnhand(activeRow.Cells["Item Code"].Value.ToString(), activeRow.Cells["Unit"].Value.ToString(), activeRow.Cells["Location"].Value.ToString(), "Item_Transaction_Detail", text, voucherID, decimal.Parse(activeRow.Cells["Quantity"].Value.ToString())) || supressInventoryMessage)
				{
					return;
				}
				if (itemTypes == ItemTypes.ConsignmentItem)
				{
					ErrorHelper.WarningMessage("You do not have sufficient quantity in this location.");
					e.Cancel = true;
				}
				else if (CompanyPreferences.NegativeQuantityAction == 2)
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
				}
			}
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
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

		private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadCustomerPriceList();
		}

		private void LoadCustomerPriceList()
		{
			try
			{
				if (!isDataLoading)
				{
					priceListData = Factory.PriceListSystem.GetActivePriceListByCustomerID(textBoxCustomerName.Text);
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
			}
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridItems.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Item Code")
					{
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
					else if (e.Cell.Column.Key == "Location" && e.Cell.Row.Cells["Quantity"].Tag != null)
					{
						e.Cell.Row.Cells["Quantity"].Tag = null;
						e.Cell.Row.Cells["Quantity"].Value = 0;
						e.Cell.Row.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
					}
				}
				goto end_IL_0000;
				IL_0096:
				if (comboBoxGridItem.SelectedRow != null)
				{
					_ = (comboBoxGridItem.SelectedID == "");
				}
				dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
				dataGridItems.ActiveRow.Cells["Attribute1"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute1"].Value.ToString();
				dataGridItems.ActiveRow.Cells["Attribute2"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute2"].Value.ToString();
				dataGridItems.ActiveRow.Cells["Attribute3"].Value = comboBoxGridItem.SelectedRow.Cells["Attribute3"].Value.ToString();
				dataGridItems.ActiveRow.Cells["MatrixParentID"].Value = comboBoxGridItem.SelectedRow.Cells["MatrixParentID"].Value.ToString();
				dataGridItems.ActiveRow.Cells["Quantity"].Value = 1;
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
						dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[checked(dataGridItems.ActiveRow.Index - 1)].Cells["Location"].Value;
					}
				}
				end_IL_0000:;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotal();
		}

		private void dataGridItems_CellChange(object sender, CellEventArgs e)
		{
			_ = e.Cell.Activated;
		}

		private void dataGridItems_BeforeCellActivate(object sender, CancelableCellEventArgs e)
		{
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
				if (!isNewRecord)
				{
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
					currentData = new ItemTransactionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ItemTransactionTable.Rows[0] : currentData.ItemTransactionTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = textBoxSysDocID.Text;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["PartyType"] = comboBoxPartyType.Text;
				dataRow["PartyID"] = partyID;
				dataRow["SysDocType"] = sysDocType;
				dataRow["LocationID"] = locationID;
				dataRow["Note"] = textBoxNote.Text;
				if (clUserID != "")
				{
					dataRow["CLUserID"] = clUserID;
				}
				else
				{
					dataRow["CLUserID"] = DBNull.Value;
				}
				dataRow["SourceDocType"] = sourceDocType;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ItemTransactionTable.Rows.Add(dataRow);
				}
				foreach (UltraGridColumn column in dataGridItems.DisplayLayout.Bands[0].Columns)
				{
					if (!currentData.ItemTransactionDetailTable.Columns.Contains(column.Key))
					{
						currentData.ItemTransactionDetailTable.Columns.Add(column.Key, column.DataType);
					}
				}
				currentData.ItemTransactionDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.ItemTransactionDetailTable.NewRow();
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
					dataRow2["Attribute1"] = row.Cells["Attribute1"].Value.ToString();
					dataRow2["Attribute2"] = row.Cells["Attribute2"].Value.ToString();
					dataRow2["Attribute3"] = row.Cells["Attribute3"].Value.ToString();
					dataRow2["MatrixParentID"] = row.Cells["MatrixParentID"].Value.ToString();
					dataRow2["LocationID"] = locationID;
					if (row.Cells["RowSourceType"].Value.ToString() != "")
					{
						dataRow2["RowSource"] = row.Cells["RowSourceType"].Value.ToString();
					}
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
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
					dataRow2.EndEdit();
					currentData.ItemTransactionDetailTable.Rows.Add(dataRow2);
					_ = locationID;
					string text = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Quantity"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
						{
							if (row2["LocationID"].ToString() != locationID || row2["ProductID"].ToString() != text)
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
							dataRow4["Reference2"] = row2["Reference2"];
							dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
							dataRow4["SoldQty"] = row2["SoldQty"];
							dataRow4["Cost"] = row2["Cost"];
							dataRow4["SysDocID"] = textBoxSysDocID.Text;
							dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow4["UnitPrice"] = 0;
							dataRow4["RowIndex"] = row.Index;
							currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow4);
						}
					}
				}
				_ = sourceDocType;
				_ = 6;
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
				dataTable.Columns.Add("Job");
				dataTable.Columns.Add("CostCategory");
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("DefaultDescription");
				dataTable.Columns.Add("Attribute1");
				dataTable.Columns.Add("Attribute2");
				dataTable.Columns.Add("Attribute3");
				dataTable.Columns.Add("MatrixParentID");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("RowIndex", typeof(int));
				dataTable.Columns.Add("SourceSysDocID");
				dataTable.Columns.Add("SourceVoucherID");
				dataTable.Columns.Add("SourceRowIndex", typeof(int));
				dataTable.Columns.Add("RowSourceType", typeof(int));
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Price", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].Hidden = true;
				bool flag4 = ultraGridColumn3.Hidden = flag2;
				bool hidden = ultraGridColumn2.Hidden = flag4;
				ultraGridColumn.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute1"];
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute2"];
				UltraGridColumn ultraGridColumn6 = dataGridItems.DisplayLayout.Bands[0].Columns["Attribute3"];
				Activation activation2 = dataGridItems.DisplayLayout.Bands[0].Columns["MatrixParentID"].CellActivation = Activation.Disabled;
				Activation activation4 = ultraGridColumn6.CellActivation = activation2;
				Activation activation7 = ultraGridColumn4.CellActivation = (ultraGridColumn5.CellActivation = activation4);
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
				dataGridItems.DisplayLayout.Bands[0].Columns["DefaultDescription"].Hidden = true;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Formula = "[Quantity]*[Price]";
				UltraGridColumn ultraGridColumn7 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true);
				ultraGridColumn7.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				AdjustGridColumnSettings();
				dataGridItems.SetupUI();
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
			dataGridItems.DisplayLayout.Bands[0].Columns["Job"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["CostCategory"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["Amount"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Hidden = true;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Hidden = false;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellActivation = Activation.AllowEdit;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellActivation = Activation.Disabled;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].TabIndex = 1;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].TabStop = false;
			dataGridItems.DisplayLayout.Bands[0].Columns["RowIndex"].Hidden = true;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.ItemTransactionSystem.GetItemTransactionByID(textBoxSysDocID.Text, voucherID);
					if (currentData != null)
					{
						IsNewRecord = false;
						FillData();
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
					string text = "";
					string text2 = "";
					DataRow dataRow = currentData.Tables["Item_Transaction"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxSysDocID.Text = dataRow["SysDocID"].ToString();
					sysDocType = int.Parse(dataRow["SysDocType"].ToString());
					if (dataRow["SysDocID"].ToString() != "" || dataRow["SysDocID"] != DBNull.Value)
					{
						text2 = Factory.DatabaseSystem.GetFieldValue("System_Document", "DocName", "SysDocID", dataRow["SysDocID"].ToString()).ToString();
						if (textBoxSysDocName.Text == "")
						{
							textBoxSysDocName.Text = text2;
						}
					}
					comboBoxPartyType.SelectedItem = dataRow["PartyType"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					if (dataRow["LocationID"].ToString() != "" || dataRow["LocationID"] != DBNull.Value)
					{
						text = Factory.DatabaseSystem.GetFieldValue("Location", "LocationName", "LocationID", dataRow["LocationID"].ToString()).ToString();
						if (textBoxLocation.Text == "")
						{
							textBoxLocation.Text = text;
						}
					}
					if (comboBoxPartyType.SelectedItem.ToString() == "V" && dataRow["PartyID"] != DBNull.Value)
					{
						string text3 = Factory.DatabaseSystem.GetFieldValue("Vendor", "VendorName", "VendorID", dataRow["PartyID"].ToString()).ToString();
						if (text3 != "")
						{
							textBoxCustomerName.Text = text3;
						}
					}
					else if (comboBoxPartyType.SelectedItem.ToString() == "C" && dataRow["PartyID"] != DBNull.Value)
					{
						string text4 = Factory.DatabaseSystem.GetFieldValue("Customer", "CustomerName", "CustomerID", dataRow["PartyID"].ToString()).ToString();
						if (text4 != "")
						{
							textBoxCustomerName.Text = text4;
						}
					}
					bool result = false;
					bool.TryParse(dataRow["IsInvoiced"].ToString(), out result);
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Item_Transaction_Detail") && currentData.ItemTransactionDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Item_Transaction_Detail"].Rows)
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
							dataRow3["Description"] = row["Description"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["Price"] = row["UnitPrice"];
							dataRow3["SourceSysDocID"] = row["SourceSysDocID"];
							dataRow3["SourceVoucherID"] = row["SourceVoucherID"];
							dataRow3["SourceRowIndex"] = row["SourceRowIndex"];
							dataRow3["Job"] = row["JobID"];
							dataRow3["CostCategory"] = row["CostCategoryID"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
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
							row2.Cells["Item Code"].Value.ToString();
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
						if (currentData.Tables.Contains("SourceTableDetails"))
						{
							foreach (DataRow row3 in currentData.Tables["SourceTableDetails"].Rows)
							{
								new NameValue(row3["SourceVoucherID"].ToString(), row3["SourceSysDocID"].ToString());
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
				bool flag2 = Factory.ItemTransactionSystem.CreateItemTransaction(currentData, !isNewRecord);
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
					bool flag3 = false;
					if (false)
					{
						if (flag3)
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

		private bool ValidateData()
		{
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
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(textBoxSysDocID.Text, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || textBoxSysDocID.Text == "")
				{
					ErrorHelper.InformationMessage(UIMessages.EnterRequiredFields);
					return false;
				}
				for (int i = 0; i < dataGridItems.Rows.Count; i++)
				{
					if (!dataGridItems.HasRowAnyValue(dataGridItems.Rows[i]))
					{
						dataGridItems.Rows[i].Delete(displayPrompt: false);
					}
					else if (dataGridItems.Rows[i].Cells["Item Code"].Value.ToString() == "")
					{
						ErrorHelper.InformationMessage("Please select an item.");
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
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Item_Transaction", "VoucherID", textBoxSysDocID.Text, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage(UIMessages.DocumentNumberInUse);
					textBoxVoucherNumber.Focus();
					return false;
				}
				if (CompanyPreferences.OverCLAction != 1)
				{
					if (!IsNewRecord)
					{
						_ = textBoxSysDocID.Text;
						_ = textBoxVoucherNumber.Text;
					}
					decimal num2 = default(decimal);
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						decimal num3 = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						num3 = decimal.Parse(row2.Cells["Amount"].Value.ToString());
						if (num3 == 0m)
						{
							decimal.TryParse(row2.Cells["Quantity"].Value.ToString(), out result2);
							decimal.TryParse(row2.Cells["Cost"].Value.ToString(), out result3);
							num3 = result2 * result3;
						}
						num2 += Math.Round(num3, Global.CurDecimalPoints);
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
				clUserID = "";
				dateTimePickerDate.Value = DateTime.Now;
				textBoxVoucherNumber.Clear();
				comboBoxGridItem.Clear();
				textBoxCustomerName.Clear();
				textBoxLocation.Clear();
				textBoxItem.Clear();
				textBoxQuantity.Clear();
				textBoxSysDocID.Clear();
				textBoxSysDocName.Clear();
				comboBoxPartyType.Clear();
				textBoxCustomerName.Clear();
				isDiscountPercent = false;
				isDiscountPercent = false;
				clUserID = "";
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				if (dataTable.Columns.Contains("Invoiced"))
				{
					dataTable.Columns.Remove("Invoiced");
					dataTable.Columns.Remove("Shipped");
				}
				dataTable.Rows.Clear();
				IsNewRecord = true;
				IsVoid = false;
				formManager.ResetDirty();
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
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(textBoxSysDocID.Text, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				return Factory.DeliveryNoteSystem.DeleteDeliveryNote(textBoxSysDocID.Text, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Item_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", textBoxSysDocID.Text);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Item_Transaction", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", textBoxSysDocID.Text);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Item_Transaction", "VoucherID", "SysDocID", textBoxSysDocID.Text);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Item_Transaction", "VoucherID", "SysDocID", textBoxSysDocID.Text);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Item_Transaction", "VoucherID", textBoxSysDocID.Text, toolStripTextBoxFind.Text);
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
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					base.AcceptButton = buttonEnter;
					textBoxItem.Focus();
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
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
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
				return Factory.SystemDocumentSystem.GetNextDocumentNumber(textBoxSysDocID.Text);
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
				if (!IsNewRecord && !Factory.DeliveryNoteSystem.AllowModify(textBoxSysDocID.Text, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Some items in this transaction has been already invoiced. You are not able to modify.");
					return false;
				}
				bool flag = Factory.DeliveryNoteSystem.VoidDeliveryNote(textBoxSysDocID.Text, textBoxVoucherNumber.Text, isVoid);
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
			new FormHelper().EditSysDoc(textBoxSysDocID.Text, SysDocTypes.DeliveryNote);
		}

		private void LoadCustomerBillingAddress()
		{
		}

		private void buttonNextBillTo_Click(object sender, EventArgs e)
		{
		}

		private void buttonPrevBillto_Click(object sender, EventArgs e)
		{
		}

		private void CalculateTotal()
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

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel1_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
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
				}
				else if (CanClose())
				{
					LoadData(text);
					IsNewRecord = true;
					textBoxVoucherNumber.Text = GetNextVoucherNumber();
				}
			}
		}

		private void toolStripDropDownButton1_DropDownOpening(object sender, EventArgs e)
		{
			duplicateToolStripMenuItem.Enabled = !IsNewRecord;
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
					string text = textBoxSysDocID.Text;
					string text2 = textBoxVoucherNumber.Text;
					DataSet deliveryNoteToPrint = Factory.DeliveryNoteSystem.GetDeliveryNoteToPrint(text, text2, showLotDetail, ExclueZeroQtyInDN);
					if (deliveryNoteToPrint == null || deliveryNoteToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string text3 = "";
						if (!string.IsNullOrEmpty(text3))
						{
							PrintHelper.PrintDocument(deliveryNoteToPrint, text, text3, SysDocTypes.DeliveryNote, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(deliveryNoteToPrint, text, "Delivery Note", SysDocTypes.DeliveryNote, isPrint, showPrintDialog);
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
			textBoxSysDocID.Text = sysDocID;
			LoadData(voucherID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ItemTransactionListFormObj);
		}

		private void linkLabelVehicle_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					DocManagementForm docManagementForm = new DocManagementForm();
					docManagementForm.EntitySysDocID = textBoxSysDocID.Text;
					docManagementForm.EntityID = textBoxVoucherNumber.Text.Trim();
					docManagementForm.EntityName = textBoxSysDocID.Text;
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
			journalDistibutionDialog.SysDocID = textBoxSysDocID.Text;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, textBoxSysDocID.Text, this);
			}
		}

		private void toolStripButtonBalance_Click(object sender, EventArgs e)
		{
		}

		private void checkedListBoxOrders_MouseDoubleClick(object sender, MouseEventArgs e)
		{
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

		public void xmlcheck()
		{
		}

		private void buttonSelectDocID_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet itemTransactionSystemDoc = Factory.SystemDocumentSystem.GetItemTransactionSystemDoc();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = itemTransactionSystemDoc;
			selectDocumentDialog.Text = "Select Document ID";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				sysDocID = selectDocumentDialog.SelectedRow.Cells["SysDocID"].Value.ToString();
				sysDocType = int.Parse(selectDocumentDialog.SelectedRow.Cells["SysDocType"].Value.ToString());
				string text = selectDocumentDialog.SelectedRow.Cells["DocName"].Value.ToString();
				textBoxSysDocID.Text = sysDocID;
				textBoxSysDocName.Text = text;
				string text2 = "";
				text2 = Factory.ItemTransactionSystem.GetNextDocNumber(sysDocID);
				if (text2 == "")
				{
					textBoxVoucherNumber.Text = "0000001";
				}
				else
				{
					textBoxVoucherNumber.Text = text2;
				}
				if (sysDocType == 24 || sysDocType == 26)
				{
					comboBoxPartyType.SelectedItem = "C";
					comboBoxPartyType.Enabled = false;
				}
				else if (sysDocType == 32)
				{
					comboBoxPartyType.SelectedItem = "V";
					comboBoxPartyType.Enabled = false;
				}
				else
				{
					comboBoxPartyType.Clear();
					comboBoxPartyType.Enabled = true;
				}
			}
		}

		private void buttonSelectParty_Click(object sender, EventArgs e)
		{
			string a = comboBoxPartyType.SelectedItem.ToString();
			if (a == "")
			{
				ErrorHelper.InformationMessage("Please select The Part type.");
			}
			else if (a == "V")
			{
				DataSet vendorSelectionList = Factory.VendorSystem.GetVendorSelectionList();
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = vendorSelectionList;
				selectDocumentDialog.Text = "Select Party";
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					partyID = selectDocumentDialog.SelectedRow.Cells["Code"].Value.ToString();
					textBoxCustomerName.Text = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
				}
			}
			else if (a == "C")
			{
				DataSet transactionCustomerList = Factory.CustomerSystem.GetTransactionCustomerList();
				SelectDocumentDialog selectDocumentDialog2 = new SelectDocumentDialog();
				selectDocumentDialog2.DataSource = transactionCustomerList;
				selectDocumentDialog2.Text = "Select Party";
				if (selectDocumentDialog2.ShowDialog(this) == DialogResult.OK)
				{
					partyID = selectDocumentDialog2.SelectedRow.Cells["Customer Code"].Value.ToString();
					textBoxCustomerName.Text = selectDocumentDialog2.SelectedRow.Cells["Name"].Value.ToString();
				}
			}
		}

		private void buttonEnter_Click(object sender, EventArgs e)
		{
			if (textBoxItem.Text.Trim() == "")
			{
				textBoxItem.Focus();
				return;
			}
			string text = textBoxItem.Text;
			ScanItem(text);
		}

		private void ScanItem(string itemCode)
		{
			DataRow[] array = null;
			DataRow dataRow = null;
			Factory.ProductSystem.POSGetProductData(itemCode);
			if (dataRow != null)
			{
				array = new DataRow[1]
				{
					dataRow
				};
			}
			if (array == null || array.Length == 0)
			{
				ErrorHelper.InformationMessage("Item not found.");
				textBoxItem.Clear();
				textBoxItem.Focus();
				return;
			}
			DataRow dataRow2 = array[0];
			DataTable obj = dataGridItems.DataSource as DataTable;
			checked
			{
				if (dataRow2["ItemType"].ToString() != "")
				{
					_ = (byte)int.Parse(dataRow2["ItemType"].ToString());
				}
				DataRow dataRow3 = obj.Rows.Add();
				dataRow3["Item Code"] = dataRow2["ProductID"];
				dataRow3["Description"] = dataRow2["Description"];
				int result = 1;
				int.TryParse(textBoxQuantity.Text, out result);
				if (result != 0)
				{
					dataRow3["Quantity"] = result;
				}
				else
				{
					dataRow3["Quantity"] = 1;
				}
				decimal num = decimal.Parse(dataRow3["Quantity"].ToString());
				dataRow3["Quantity"] = num;
				dataGridItems.ActiveRow = dataGridItems.Rows[dataGridItems.Rows.Count - 1];
				base.AcceptButton = buttonEnter;
				textBoxItem.Clear();
				textBoxItem.Focus();
			}
		}

		private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
		}

		private void buttonLocation_Click(object sender, EventArgs e)
		{
			DataSet locationComboList = Factory.LocationSystem.GetLocationComboList();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.Text = "Select Location";
			selectDocumentDialog.HiddenColumns.Add("IsConsignOutLocation");
			selectDocumentDialog.HiddenColumns.Add("IsConsignInLocation");
			selectDocumentDialog.HiddenColumns.Add("ISPOSLocation");
			selectDocumentDialog.HiddenColumns.Add("IsWarehouse");
			selectDocumentDialog.DataSource = locationComboList;
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				textBoxLocation.Text = selectDocumentDialog.SelectedRow.Cells["name"].Value.ToString();
				locationID = selectDocumentDialog.SelectedRow.Cells["Code"].Value.ToString();
			}
		}

		private void simpleButtonNext_Click(object sender, EventArgs e)
		{
			new DataSet();
			string executablePath = Application.ExecutablePath;
			string[] fileNamesWithoutFileExtensions = GetFileNamesWithoutFileExtensions(new DirectoryInfo(executablePath.Substring(0, executablePath.LastIndexOf('\\')) + "\\XmlTransactions"));
			string text2 = CurrentKey = ArrayCheckNext(fileNamesWithoutFileExtensions);
			DataSet transData = null;
			if (text2 != null)
			{
				transData = LoadTransactionDraft(text2, SysDocTypes.SalesInvoice);
			}
			FillTransactionData(transData);
		}

		private void buttonSelectItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet productsForItemTransaction = Factory.ProductSystem.GetProductsForItemTransaction();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = productsForItemTransaction;
			selectDocumentDialog.Text = "Select Item";
			decimal num = default(decimal);
			if (textBoxQuantity.Text != "")
			{
				num = decimal.Parse(textBoxQuantity.Text);
			}
			if (num <= 0m || num.ToString() == "")
			{
				num = 1m;
			}
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				DataTable obj = dataGridItems.DataSource as DataTable;
				DataRow dataRow = obj.NewRow();
				dataRow["Item Code"] = selectDocumentDialog.SelectedRow.Cells["Code"].Value.ToString();
				textBoxItem.Text = selectDocumentDialog.SelectedRow.Cells["Code"].Value.ToString();
				dataRow["Quantity"] = num;
				dataRow["Description"] = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
				dataRow["Unit"] = selectDocumentDialog.SelectedRow.Cells["UnitID"].Value.ToString();
				obj.Rows.Add(dataRow);
			}
			textBoxItem.Clear();
			textBoxQuantity.Clear();
		}

		private void simpleButtonSynchronize_Click(object sender, EventArgs e)
		{
		}

		private bool CheckDBConnection()
		{
			bool num = Factory.DatabaseSystem.CanConnect();
			if (!num)
			{
				simpleButtonSynchronize.Enabled = false;
				toolStrip1.Enabled = false;
				panelxml.Enabled = true;
				ButtonClose.Enabled = false;
				buttonDelete.Enabled = false;
				buttonSave.Enabled = false;
				buttonVoid.Enabled = false;
				buttonNew.Enabled = false;
				return num;
			}
			simpleButtonSynchronize.Enabled = true;
			toolStrip1.Enabled = true;
			ButtonClose.Enabled = true;
			buttonDelete.Enabled = true;
			buttonSave.Enabled = true;
			buttonVoid.Enabled = true;
			buttonNew.Enabled = true;
			DataSet transactionCustomerList = Factory.CustomerSystem.GetTransactionCustomerList();
			DataSet vendorSelectionList = Factory.VendorSystem.GetVendorSelectionList();
			DataSet productsForItemTransaction = Factory.ProductSystem.GetProductsForItemTransaction();
			DataSet productsForItemTransaction2 = Factory.ProductSystem.GetProductsForItemTransaction();
			DataSet itemTransactionSystemDoc = Factory.SystemDocumentSystem.GetItemTransactionSystemDoc();
			DataSet locationComboList = Factory.LocationSystem.GetLocationComboList();
			DataSet maxVoucherID = Factory.ItemTransactionSystem.GetMaxVoucherID();
			Global.CompanySettings.SaveItemTransactionDraft(transactionCustomerList, "CustomerData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(vendorSelectionList, "VendorData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(productsForItemTransaction, "ProductXmlData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(productsForItemTransaction2, "ProductComboData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(itemTransactionSystemDoc, "ItemDocData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(locationComboList, "LocationData", SysDocTypes.None);
			Global.CompanySettings.SaveItemTransactionDraft(maxVoucherID, "MaxVoucherData", SysDocTypes.None);
			return num;
		}

		private bool SaveDraft()
		{
			try
			{
				SysDocTypes sysDocTypes = (SysDocTypes)sysDocType;
				if (GetData())
				{
					return Global.CompanySettings.SaveItemTransactionDraft(currentData, textBoxSysDocID.Text + "-" + textBoxVoucherNumber.Text, sysDocTypes);
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private DataSet ToDataSet(string[] input)
		{
			DataSet dataSet = new DataSet("Saved Drafts");
			DataTable dataTable = dataSet.Tables.Add();
			dataTable.Columns.Add();
			Array.ForEach(input, delegate(string c)
			{
				dataTable.Rows.Add()[0] = c;
			});
			return dataSet;
		}

		private void simpleButtonFind_Click(object sender, EventArgs e)
		{
			LoadDraft(SysDocTypes.DeliveryNote);
		}

		private void ButtonDraft_Click(object sender, EventArgs e)
		{
		}

		public static string[] GetFileNamesWithoutFileExtensions(DirectoryInfo di)
		{
			FileInfo[] files = di.GetFiles();
			List<string> list = new List<string>();
			for (int i = 0; i < files.Length; i = checked(i + 1))
			{
				list.Add(Path.GetFileNameWithoutExtension(files[i].FullName));
			}
			return Enumerable.ToArray(list);
		}

		private bool LoadDraft(SysDocTypes sysDocType)
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				string executablePath = Application.ExecutablePath;
				string str = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
				text = ((sysDocType != SysDocTypes.None) ? (str + "\\XmlTransactions") : (str + "\\XmlCards"));
				string[] fileNamesWithoutFileExtensions = GetFileNamesWithoutFileExtensions(new DirectoryInfo(text));
				dataSet = ToDataSet(fileNamesWithoutFileExtensions);
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = dataSet;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = CurrentKey = selectDocumentDialog.SelectedRow.Cells[0].Value.ToString();
					DataSet transData = LoadTransactionDraft(key, SysDocTypes.SalesInvoice);
					FillTransactionData(transData);
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void simpleButtonLast_Click(object sender, EventArgs e)
		{
			SaveDraft();
			ClearForm();
		}

		public DataSet LoadTransactionDraft(string key, SysDocTypes sysDocType)
		{
			string str = key + ".xml";
			string text = "";
			string executablePath = Application.ExecutablePath;
			string str2 = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
			text = ((sysDocType != SysDocTypes.None) ? (str2 + "\\XmlTransactions") : (str2 + "\\XmlCards"));
			new FileInfo(text);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			str2 = text + "\\" + str;
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(str2);
			if (File.Exists(str2))
			{
				_ = -1;
			}
			return dataSet;
		}

		public DataSet DeleteTransactionDraft(string key, SysDocTypes sysDocType)
		{
			string str = key + ".xml";
			string text = "";
			string executablePath = Application.ExecutablePath;
			string str2 = executablePath.Substring(0, executablePath.LastIndexOf('\\'));
			text = ((sysDocType != SysDocTypes.None) ? (str2 + "\\XmlTransactions") : (str2 + "\\XmlCards"));
			new FileInfo(text);
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			str2 = text + "\\" + str;
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(str2);
			if (File.Exists(str2) && sysDocType != SysDocTypes.None)
			{
				File.Delete(str2);
			}
			return dataSet;
		}

		private void simpleButtonFirst_Click(object sender, EventArgs e)
		{
			new DataSet();
			string executablePath = Application.ExecutablePath;
			string[] fileNamesWithoutFileExtensions = GetFileNamesWithoutFileExtensions(new DirectoryInfo(executablePath.Substring(0, executablePath.LastIndexOf('\\')) + "\\XmlTransactions"));
			int lowerBound = fileNamesWithoutFileExtensions.GetLowerBound(0);
			string text = fileNamesWithoutFileExtensions.ElementAtOrDefault(lowerBound);
			DataSet transData = null;
			if (text != null)
			{
				transData = LoadTransactionDraft(text, SysDocTypes.SalesInvoice);
			}
			FillTransactionData(transData);
		}

		private void simpleButtonPrevious_Click(object sender, EventArgs e)
		{
			new DataSet();
			string executablePath = Application.ExecutablePath;
			string[] fileNamesWithoutFileExtensions = GetFileNamesWithoutFileExtensions(new DirectoryInfo(executablePath.Substring(0, executablePath.LastIndexOf('\\')) + "\\XmlTransactions"));
			string text = ArrayCheckPrev(fileNamesWithoutFileExtensions);
			DataSet transData = null;
			if (text != null)
			{
				transData = LoadTransactionDraft(text, SysDocTypes.SalesInvoice);
			}
			FillTransactionData(transData);
		}

		public string ArrayCheckPrev(string[] translist)
		{
			List<string> list = translist.ToList();
			int num = list.IndexOf(CurrentKey);
			if (num == -1)
			{
				return null;
			}
			string result = null;
			if (num > 0)
			{
				result = list[checked(num - 1)];
			}
			return result;
		}

		public string ArrayCheckNext(string[] translist)
		{
			List<string> list = translist.ToList();
			int num = list.IndexOf(CurrentKey);
			if (num == -1)
			{
				return null;
			}
			string result = null;
			checked
			{
				if (num < list.Count - 1)
				{
					result = list[num + 1];
				}
				return result;
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
			}
		}

		public string ArrayCheckFirst(string[] translist)
		{
			List<string> list = translist.ToList();
			int num = list.IndexOf(CurrentKey);
			if (num == -1)
			{
				return null;
			}
			string result = null;
			if (num < checked(list.Count - 1))
			{
				result = list[0];
			}
			return result;
		}

		private void FillTransactionData(DataSet transData)
		{
			try
			{
				isDataLoading = true;
				if (transData != null && transData.Tables.Count != 0 && transData.Tables[0].Rows.Count != 0)
				{
					string text = "";
					string text2 = "";
					DataRow dataRow = transData.Tables["Item_Transaction"].Rows[0];
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxSysDocID.Text = dataRow["SysDocID"].ToString();
					sysDocType = int.Parse(dataRow["SysDocType"].ToString());
					if (dataRow["SysDocID"].ToString() != "" || dataRow["SysDocID"] != DBNull.Value)
					{
						text2 = LoadTransactionDraft("ItemDocData", SysDocTypes.None).Tables[0].Select("SysDocID Like '%" + dataRow["SysDocID"].ToString() + "%'")[0][1].ToString();
						if (textBoxSysDocName.Text == "")
						{
							textBoxSysDocName.Text = text2;
						}
					}
					comboBoxPartyType.SelectedItem = dataRow["PartyType"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					if (dataRow["LocationID"].ToString() != "" || dataRow["LocationID"] != DBNull.Value)
					{
						text = LoadTransactionDraft("LocationData", SysDocTypes.None).Tables[0].Select("Code Like '%" + dataRow["LocationID"].ToString() + "%'")[0][1].ToString();
						if (textBoxLocation.Text == "")
						{
							textBoxLocation.Text = text;
						}
					}
					if (comboBoxPartyType.SelectedItem.ToString() == "V" && dataRow["PartyID"] != DBNull.Value)
					{
						string text3 = "";
						text3 = LoadTransactionDraft("VendorData", SysDocTypes.None).Tables[0].Select("Code Like '%" + dataRow["PartyID"].ToString() + "%'")[0][1].ToString();
						if (text3 != "")
						{
							textBoxCustomerName.Text = text3;
						}
					}
					else if (comboBoxPartyType.SelectedItem.ToString() == "C" && dataRow["PartyID"] != DBNull.Value)
					{
						string text4 = "";
						text4 = LoadTransactionDraft("CustomerData", SysDocTypes.None).Tables[0].Select("[Customer Code] Like '%" + dataRow["PartyID"].ToString() + "%'")[0][1].ToString();
						if (text4 != "")
						{
							textBoxCustomerName.Text = text4;
						}
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (transData.Tables.Contains("Item_Transaction_Detail") && transData.Tables[1].Rows.Count != 0)
					{
						foreach (DataRow row in transData.Tables["Item_Transaction_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3["Quantity"] = row["Quantity"];
							dataRow3["Description"] = row["Description"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["RowIndex"] = row["RowIndex"];
							dataRow3["Price"] = row["UnitPrice"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
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
							row2.Cells["Item Code"].Value.ToString();
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array = transData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
								if (array.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
						}
						CalculateTotal();
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				isDataLoading = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ItemTransactionForm));
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonBalance = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			selectInvoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			createFromSalesQuoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInfo = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			ButtonDraft = new Micromind.UISupport.XPButton();
			buttonVoid = new Micromind.UISupport.XPButton();
			buttonDelete = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			ButtonClose = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			textBoxNote = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			panelDetails = new System.Windows.Forms.Panel();
			simpleButtonSynchronize = new DevExpress.XtraEditors.SimpleButton();
			label4 = new System.Windows.Forms.Label();
			buttonLocation = new Micromind.UISupport.XPButton();
			textBoxLocation = new System.Windows.Forms.TextBox();
			comboBoxPartyType = new Micromind.DataControls.ContainerStstusComboBox();
			label9 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			textBoxSysDocName = new System.Windows.Forms.TextBox();
			buttonSelectDocID = new Micromind.UISupport.XPButton();
			textBoxSysDocID = new System.Windows.Forms.TextBox();
			buttonSelectParty = new Micromind.UISupport.XPButton();
			textBoxCustomerName = new System.Windows.Forms.TextBox();
			textBoxVoucherNumber = new System.Windows.Forms.TextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager(components);
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			buttonEnter = new Micromind.UISupport.XPButton();
			label2 = new System.Windows.Forms.Label();
			buttonSelectItem = new Micromind.UISupport.XPButton();
			textBoxItem = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxQuantity = new System.Windows.Forms.TextBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			labelVoided = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ComboBoxitemcostCategory = new Micromind.DataControls.CostCategoryComboBox();
			ComboBoxitemJob = new Micromind.DataControls.JobComboBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			panelxml = new System.Windows.Forms.Panel();
			simpleButtonFind = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonLast = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonNext = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonPrevious = new DevExpress.XtraEditors.SimpleButton();
			simpleButtonFirst = new DevExpress.XtraEditors.SimpleButton();
			formManager = new Micromind.DataControls.FormManager();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			panelDetails.SuspendLayout();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			panelxml.SuspendLayout();
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
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator7,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonBalance,
				toolStripButtonInformation,
				toolStripDropDownButton1,
				toolStripButtonInfo
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(931, 31);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);
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
			toolStripButtonBalance.Visible = false;
			toolStripButtonBalance.Click += new System.EventHandler(toolStripButtonBalance_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				duplicateToolStripMenuItem,
				toolStripSeparator4,
				selectInvoiceToolStripMenuItem,
				createFromSalesOrderToolStripMenuItem,
				createFromSalesQuoteToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.Visible = false;
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(215, 6);
			selectInvoiceToolStripMenuItem.Name = "selectInvoiceToolStripMenuItem";
			selectInvoiceToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			selectInvoiceToolStripMenuItem.Text = "Create From Sales Invoice...";
			createFromSalesOrderToolStripMenuItem.Name = "createFromSalesOrderToolStripMenuItem";
			createFromSalesOrderToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			createFromSalesOrderToolStripMenuItem.Text = "Create from Sales Order...";
			createFromSalesQuoteToolStripMenuItem.Name = "createFromSalesQuoteToolStripMenuItem";
			createFromSalesQuoteToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
			createFromSalesQuoteToolStripMenuItem.Text = "Create from Sales Quote...";
			createFromSalesQuoteToolStripMenuItem.Visible = false;
			toolStripButtonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInfo.Image = Micromind.ClientUI.Properties.Resources.Alert;
			toolStripButtonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInfo.Name = "toolStripButtonInfo";
			toolStripButtonInfo.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInfo.Visible = false;
			toolStripButtonInfo.MouseLeave += new System.EventHandler(toolStripButtonInfo_MouseLeave);
			panelButtons.Controls.Add(ButtonDraft);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(ButtonClose);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 605);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(931, 40);
			panelButtons.TabIndex = 16;
			ButtonDraft.AdjustImageLocation = new System.Drawing.Point(0, 0);
			ButtonDraft.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			ButtonDraft.BackColor = System.Drawing.Color.DarkGray;
			ButtonDraft.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			ButtonDraft.BtnStyle = Micromind.UISupport.XPStyle.Default;
			ButtonDraft.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			ButtonDraft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			ButtonDraft.Location = new System.Drawing.Point(577, 4);
			ButtonDraft.Name = "ButtonDraft";
			ButtonDraft.Size = new System.Drawing.Size(96, 33);
			ButtonDraft.TabIndex = 21;
			ButtonDraft.Text = "&Draft";
			ButtonDraft.UseVisualStyleBackColor = false;
			ButtonDraft.Visible = false;
			ButtonDraft.Click += new System.EventHandler(ButtonDraft_Click);
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(210, 4);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(96, 33);
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
			buttonDelete.Location = new System.Drawing.Point(307, 4);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 33);
			buttonDelete.TabIndex = 19;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(111, 4);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 33);
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
			linePanelDown.Size = new System.Drawing.Size(931, 1);
			linePanelDown.TabIndex = 0;
			linePanelDown.TabStop = false;
			ButtonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
			ButtonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			ButtonClose.BackColor = System.Drawing.Color.DarkGray;
			ButtonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			ButtonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
			ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			ButtonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			ButtonClose.Location = new System.Drawing.Point(821, 4);
			ButtonClose.Name = "ButtonClose";
			ButtonClose.Size = new System.Drawing.Size(96, 33);
			ButtonClose.TabIndex = 20;
			ButtonClose.Text = "&Close";
			ButtonClose.UseVisualStyleBackColor = false;
			ButtonClose.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 4);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 33);
			buttonSave.TabIndex = 16;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraCalcManager1.ContainingControl = this;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxNote.Location = new System.Drawing.Point(49, 500);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(365, 92);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 510);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 3;
			label3.Text = "Note:";
			panelDetails.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			panelDetails.Controls.Add(simpleButtonSynchronize);
			panelDetails.Controls.Add(label4);
			panelDetails.Controls.Add(buttonLocation);
			panelDetails.Controls.Add(textBoxLocation);
			panelDetails.Controls.Add(comboBoxPartyType);
			panelDetails.Controls.Add(label9);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(label7);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(textBoxSysDocName);
			panelDetails.Controls.Add(buttonSelectDocID);
			panelDetails.Controls.Add(textBoxSysDocID);
			panelDetails.Controls.Add(buttonSelectParty);
			panelDetails.Controls.Add(textBoxCustomerName);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(1, 37);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(884, 147);
			panelDetails.TabIndex = 0;
			simpleButtonSynchronize.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			simpleButtonSynchronize.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonSynchronize.Appearance.Options.UseFont = true;
			simpleButtonSynchronize.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.Refresh;
			simpleButtonSynchronize.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonSynchronize.Location = new System.Drawing.Point(830, 5);
			simpleButtonSynchronize.Name = "simpleButtonSynchronize";
			simpleButtonSynchronize.Size = new System.Drawing.Size(50, 40);
			simpleButtonSynchronize.TabIndex = 250;
			simpleButtonSynchronize.Text = "Synchronize";
			simpleButtonSynchronize.Visible = false;
			simpleButtonSynchronize.Click += new System.EventHandler(simpleButtonSynchronize_Click);
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(11, 83);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(51, 13);
			label4.TabIndex = 259;
			label4.Text = "Location:";
			buttonLocation.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonLocation.BackColor = System.Drawing.Color.DarkGray;
			buttonLocation.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonLocation.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonLocation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonLocation.Location = new System.Drawing.Point(297, 72);
			buttonLocation.Name = "buttonLocation";
			buttonLocation.Size = new System.Drawing.Size(38, 33);
			buttonLocation.TabIndex = 7;
			buttonLocation.Text = "...";
			buttonLocation.UseVisualStyleBackColor = false;
			buttonLocation.Click += new System.EventHandler(buttonLocation_Click);
			textBoxLocation.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxLocation.Location = new System.Drawing.Point(84, 73);
			textBoxLocation.MaxLength = 64;
			textBoxLocation.Multiline = true;
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(212, 30);
			textBoxLocation.TabIndex = 6;
			textBoxLocation.TabStop = false;
			comboBoxPartyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxPartyType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			comboBoxPartyType.FormattingEnabled = true;
			comboBoxPartyType.Items.AddRange(new object[3]
			{
				"",
				"V",
				"C"
			});
			comboBoxPartyType.Location = new System.Drawing.Point(84, 107);
			comboBoxPartyType.Name = "comboBoxPartyType";
			comboBoxPartyType.SelectedID = 0;
			comboBoxPartyType.Size = new System.Drawing.Size(73, 28);
			comboBoxPartyType.TabIndex = 8;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 115);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(34, 13);
			label9.TabIndex = 254;
			label9.Text = "Party:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(11, 9);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(67, 13);
			label8.TabIndex = 253;
			label8.Text = "Voucher No:";
			label7.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(267, 38);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(61, 13);
			label7.TabIndex = 252;
			label7.Text = "Doc Name:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(11, 47);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(44, 13);
			label6.TabIndex = 251;
			label6.Text = "Doc ID:";
			textBoxSysDocName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSysDocName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxSysDocName.Location = new System.Drawing.Point(334, 39);
			textBoxSysDocName.MaxLength = 64;
			textBoxSysDocName.Multiline = true;
			textBoxSysDocName.Name = "textBoxSysDocName";
			textBoxSysDocName.ReadOnly = true;
			textBoxSysDocName.Size = new System.Drawing.Size(240, 30);
			textBoxSysDocName.TabIndex = 5;
			textBoxSysDocName.TabStop = false;
			buttonSelectDocID.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectDocID.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectDocID.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectDocID.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectDocID.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectDocID.Location = new System.Drawing.Point(218, 37);
			buttonSelectDocID.Name = "buttonSelectDocID";
			buttonSelectDocID.Size = new System.Drawing.Size(38, 33);
			buttonSelectDocID.TabIndex = 4;
			buttonSelectDocID.Text = "...";
			buttonSelectDocID.UseVisualStyleBackColor = false;
			buttonSelectDocID.Click += new System.EventHandler(buttonSelectDocID_Click);
			textBoxSysDocID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxSysDocID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxSysDocID.Location = new System.Drawing.Point(84, 39);
			textBoxSysDocID.MaxLength = 64;
			textBoxSysDocID.Multiline = true;
			textBoxSysDocID.Name = "textBoxSysDocID";
			textBoxSysDocID.ReadOnly = true;
			textBoxSysDocID.Size = new System.Drawing.Size(132, 30);
			textBoxSysDocID.TabIndex = 3;
			textBoxSysDocID.TabStop = false;
			buttonSelectParty.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectParty.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectParty.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectParty.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectParty.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectParty.Location = new System.Drawing.Point(576, 105);
			buttonSelectParty.Name = "buttonSelectParty";
			buttonSelectParty.Size = new System.Drawing.Size(38, 33);
			buttonSelectParty.TabIndex = 10;
			buttonSelectParty.Text = "...";
			buttonSelectParty.UseVisualStyleBackColor = false;
			buttonSelectParty.Click += new System.EventHandler(buttonSelectParty_Click);
			textBoxCustomerName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxCustomerName.Location = new System.Drawing.Point(160, 106);
			textBoxCustomerName.MaxLength = 64;
			textBoxCustomerName.Multiline = true;
			textBoxCustomerName.Name = "textBoxCustomerName";
			textBoxCustomerName.ReadOnly = true;
			textBoxCustomerName.Size = new System.Drawing.Size(414, 30);
			textBoxCustomerName.TabIndex = 9;
			textBoxCustomerName.TabStop = false;
			textBoxVoucherNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxVoucherNumber.Location = new System.Drawing.Point(84, 5);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Multiline = true;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(132, 30);
			textBoxVoucherNumber.TabIndex = 1;
			mmLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(564, 9);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 194;
			mmLabel1.Text = "Date:";
			dateTimePickerDate.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			dateTimePickerDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(606, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 26);
			dateTimePickerDate.TabIndex = 2;
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
			ultraToolTipManager1.ContainingControl = this;
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraGroupBox1.Controls.Add(buttonEnter);
			ultraGroupBox1.Controls.Add(label2);
			ultraGroupBox1.Controls.Add(buttonSelectItem);
			ultraGroupBox1.Controls.Add(textBoxItem);
			ultraGroupBox1.Controls.Add(label1);
			ultraGroupBox1.Controls.Add(textBoxQuantity);
			ultraGroupBox1.Controls.Add(productPhotoViewer);
			ultraGroupBox1.Controls.Add(labelVoided);
			ultraGroupBox1.Controls.Add(dataGridItems);
			ultraGroupBox1.Controls.Add(ComboBoxitemcostCategory);
			ultraGroupBox1.Controls.Add(ComboBoxitemJob);
			ultraGroupBox1.Controls.Add(comboBoxGridItem);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 189);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(874, 300);
			ultraGroupBox1.TabIndex = 1;
			ultraGroupBox1.Text = "Add Item Details";
			buttonEnter.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonEnter.BackColor = System.Drawing.Color.DarkGray;
			buttonEnter.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonEnter.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonEnter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonEnter.Location = new System.Drawing.Point(603, 17);
			buttonEnter.Name = "buttonEnter";
			buttonEnter.Size = new System.Drawing.Size(95, 33);
			buttonEnter.TabIndex = 247;
			buttonEnter.Text = "Enter";
			buttonEnter.UseVisualStyleBackColor = false;
			buttonEnter.Click += new System.EventHandler(buttonEnter_Click);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(163, 24);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(30, 13);
			label2.TabIndex = 246;
			label2.Text = "Item:";
			buttonSelectItem.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSelectItem.BackColor = System.Drawing.Color.DarkGray;
			buttonSelectItem.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSelectItem.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSelectItem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSelectItem.Location = new System.Drawing.Point(563, 17);
			buttonSelectItem.Name = "buttonSelectItem";
			buttonSelectItem.Size = new System.Drawing.Size(38, 33);
			buttonSelectItem.TabIndex = 2;
			buttonSelectItem.Text = "...";
			buttonSelectItem.UseVisualStyleBackColor = false;
			buttonSelectItem.Click += new System.EventHandler(buttonSelectItem_Click);
			textBoxItem.BackColor = System.Drawing.SystemColors.Window;
			textBoxItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxItem.Location = new System.Drawing.Point(196, 19);
			textBoxItem.MaxLength = 64;
			textBoxItem.Multiline = true;
			textBoxItem.Name = "textBoxItem";
			textBoxItem.Size = new System.Drawing.Size(366, 30);
			textBoxItem.TabIndex = 1;
			textBoxItem.TabStop = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(17, 25);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(49, 13);
			label1.TabIndex = 243;
			label1.Text = "Quantity:";
			textBoxQuantity.BackColor = System.Drawing.SystemColors.Window;
			textBoxQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			textBoxQuantity.Location = new System.Drawing.Point(72, 20);
			textBoxQuantity.MaxLength = 64;
			textBoxQuantity.Multiline = true;
			textBoxQuantity.Name = "textBoxQuantity";
			textBoxQuantity.Size = new System.Drawing.Size(85, 30);
			textBoxQuantity.TabIndex = 0;
			textBoxQuantity.TabStop = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(38, 102);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 4;
			productPhotoViewer.Visible = false;
			labelVoided.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			labelVoided.BackColor = System.Drawing.Color.White;
			labelVoided.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelVoided.ForeColor = System.Drawing.Color.DarkRed;
			labelVoided.Location = new System.Drawing.Point(6, 214);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(862, 49);
			labelVoided.TabIndex = 158;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(6, 64);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(862, 230);
			dataGridItems.TabIndex = 3;
			dataGridItems.Text = "dataEntryGrid1";
			dataGridItems.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(dataGridItems_InitializeRow);
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
			ComboBoxitemcostCategory.Location = new System.Drawing.Point(510, 143);
			ComboBoxitemcostCategory.MaxDropDownItems = 12;
			ComboBoxitemcostCategory.Name = "ComboBoxitemcostCategory";
			ComboBoxitemcostCategory.ShowInactiveItems = false;
			ComboBoxitemcostCategory.ShowQuickAdd = true;
			ComboBoxitemcostCategory.Size = new System.Drawing.Size(127, 20);
			ComboBoxitemcostCategory.TabIndex = 6;
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
			ComboBoxitemJob.Location = new System.Drawing.Point(337, 143);
			ComboBoxitemJob.MaxDropDownItems = 12;
			ComboBoxitemJob.Name = "ComboBoxitemJob";
			ComboBoxitemJob.ShowInactiveItems = false;
			ComboBoxitemJob.ShowQuickAdd = true;
			ComboBoxitemJob.Size = new System.Drawing.Size(100, 20);
			ComboBoxitemJob.TabIndex = 5;
			ComboBoxitemJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxitemJob.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance13;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
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
			comboBoxGridItem.Location = new System.Drawing.Point(666, 64);
			comboBoxGridItem.MaxDropDownItems = 12;
			comboBoxGridItem.Name = "comboBoxGridItem";
			comboBoxGridItem.Show3PLItems = true;
			comboBoxGridItem.ShowInactiveItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 7;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			panelxml.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			panelxml.Controls.Add(simpleButtonFind);
			panelxml.Controls.Add(simpleButtonLast);
			panelxml.Controls.Add(simpleButtonNext);
			panelxml.Controls.Add(simpleButtonPrevious);
			panelxml.Controls.Add(simpleButtonFirst);
			panelxml.Location = new System.Drawing.Point(448, 521);
			panelxml.Name = "panelxml";
			panelxml.Size = new System.Drawing.Size(436, 43);
			panelxml.TabIndex = 17;
			panelxml.Visible = false;
			simpleButtonFind.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFind.Appearance.Options.UseFont = true;
			simpleButtonFind.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.find;
			simpleButtonFind.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFind.Location = new System.Drawing.Point(107, 3);
			simpleButtonFind.Name = "simpleButtonFind";
			simpleButtonFind.Size = new System.Drawing.Size(60, 40);
			simpleButtonFind.TabIndex = 249;
			simpleButtonFind.Click += new System.EventHandler(simpleButtonFind_Click);
			simpleButtonLast.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonLast.Appearance.Options.UseFont = true;
			simpleButtonLast.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.last;
			simpleButtonLast.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonLast.Location = new System.Drawing.Point(219, 3);
			simpleButtonLast.Name = "simpleButtonLast";
			simpleButtonLast.Size = new System.Drawing.Size(50, 40);
			simpleButtonLast.TabIndex = 6;
			simpleButtonLast.Click += new System.EventHandler(simpleButtonLast_Click);
			simpleButtonNext.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonNext.Appearance.Options.UseFont = true;
			simpleButtonNext.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.next;
			simpleButtonNext.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonNext.Location = new System.Drawing.Point(168, 3);
			simpleButtonNext.Name = "simpleButtonNext";
			simpleButtonNext.Size = new System.Drawing.Size(50, 40);
			simpleButtonNext.TabIndex = 5;
			simpleButtonNext.Click += new System.EventHandler(simpleButtonNext_Click);
			simpleButtonPrevious.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonPrevious.Appearance.Options.UseFont = true;
			simpleButtonPrevious.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.prev;
			simpleButtonPrevious.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonPrevious.Location = new System.Drawing.Point(55, 3);
			simpleButtonPrevious.Name = "simpleButtonPrevious";
			simpleButtonPrevious.Size = new System.Drawing.Size(50, 40);
			simpleButtonPrevious.TabIndex = 4;
			simpleButtonPrevious.Click += new System.EventHandler(simpleButtonPrevious_Click);
			simpleButtonFirst.Appearance.Font = new System.Drawing.Font("Tahoma", 14f);
			simpleButtonFirst.Appearance.Options.UseFont = true;
			simpleButtonFirst.ImageOptions.Image = Micromind.ClientUI.Properties.Resources.first;
			simpleButtonFirst.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
			simpleButtonFirst.Location = new System.Drawing.Point(3, 3);
			simpleButtonFirst.Name = "simpleButtonFirst";
			simpleButtonFirst.Size = new System.Drawing.Size(50, 40);
			simpleButtonFirst.TabIndex = 3;
			simpleButtonFirst.Click += new System.EventHandler(simpleButtonFirst_Click);
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
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(931, 645);
			base.Controls.Add(panelxml);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "ItemTransactionForm";
			Text = "Item Transaction";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemcostCategory).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxitemJob).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			panelxml.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
