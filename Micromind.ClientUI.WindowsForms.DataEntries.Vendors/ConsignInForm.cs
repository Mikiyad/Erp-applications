using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
	public class ConsignInForm : Form, IForm
	{
		private bool allowEdit = true;

		private bool hasUsedLots;

		private ConsignInData currentData;

		private const string TABLENAME_CONST = "Consign_In";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private string currentVendorAddressID = "";

		private bool isDataLoading;

		private ScreenAccessRight screenRight;

		private bool AllowEditTransaction;

		private bool AllowEditTransDiffLocation;

		private bool isVoid;

		private bool isDiscountPercent;

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

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ToolStripDropDownButton toolStripDropDownButton1;

		private ToolStripMenuItem duplicateToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private ToolStripButton toolStripButtonPrint;

		private ToolStripButton toolStripButtonPreview;

		private ToolStripSeparator toolStripSeparator5;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private vendorsFlatComboBox comboBoxVendor;

		private Label label2;

		private TextBox textBoxPONumber;

		private ToolStripButton toolStripButtonDistribution;

		private TextBox textBoxTransporterName;

		private TransporterComboBox comboBoxTransporter;

		private Label label5;

		private TextBox textBoxContainerNo;

		private Label label6;

		private PortComboBox comboBoxArrivalPort;

		private Label label8;

		private TextBox textBoxBLNo;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonOpenList;

		private ListBox checkedListBoxsettled;

		private Label labelSelectedDocs;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripButton toolStripButtonAttach;

		private DocStatusLabel docStatusLabel;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2007;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		private string LotNumber
		{
			get;
			set;
		}

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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
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

		public ConsignInForm()
		{
			InitializeComponent();
			AddEvents();
			dataGridItems.AllowCustomizeHeaders = true;
			comboBoxVendor.DescriptionTextBox = textBoxVendorName;
			comboBoxVendor.ShowConsignmentOnly = true;
		}

		private void AddEvents()
		{
			base.Load += JournalLeavesForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeRowUpdate += dataGridItems_BeforeRowUpdate;
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
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLot(e.Cell);
			}
		}

		private bool AllocateQuantityToLot(UltraGridCell cell)
		{
			try
			{
				ReceiveLotSelectionForm receiveLotSelectionForm = new ReceiveLotSelectionForm();
				receiveLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				receiveLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
				receiveLotSelectionForm.LocationID = dataGridItems.ActiveRow.Cells["Location"].Value.ToString();
				receiveLotSelectionForm.RowQuantity = float.Parse(cell.Text);
				receiveLotSelectionForm.LotNumber = LotNumber;
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
				LotNumber = receiveLotSelectionForm.LotNumber;
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

		private void dataGridItems_BeforeRowUpdate(object sender, CancelableRowEventArgs e)
		{
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (activeRow != null && activeRow.Cells["Item Code"].Value != null && !(activeRow.Cells["Item Code"].Value.ToString() == "") && CompanyPreferences.LocalSalesFlow == SalesFlows.SOThenDNThenInvoice && activeRow != null && activeRow.DataChanged && CompanyPreferences.NegativeQuantityAction != 1 && !IsNewRecord)
			{
				_ = comboBoxSysDoc.SelectedID;
				_ = textBoxVoucherNumber.Text;
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

		private void comboBoxVendor_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxVendorName.Text = comboBoxVendor.SelectedName;
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
			checked
			{
				try
				{
					if (dataGridItems.ActiveRow != null)
					{
						if (!(e.Cell.Column.Key == "Item Code"))
						{
							goto IL_0247;
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
					IL_0247:
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
					goto end_IL_0000;
					IL_0096:
					dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
					dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
					dataGridItems.ActiveRow.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.False;
					dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
					dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
					dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
					if ((dataGridItems.ActiveRow.Cells["Location"].Value == null || dataGridItems.ActiveRow.Cells["Location"].Value.ToString() == "") && dataGridItems.ActiveRow.Index > 0)
					{
						dataGridItems.ActiveRow.Cells["Location"].Value = dataGridItems.Rows[dataGridItems.ActiveRow.Index - 1].Cells["Location"].Value;
					}
					goto IL_0247;
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
			CalculateTotal();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
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
			if (dataGridItems.ActiveRow != null)
			{
				if (dataGridItems.ActiveCell.Column.Key == "Item Code" && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() == "")
				{
					dataGridItems.ActiveRow.Cells["Description"].Value = "";
				}
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
				else if (activeRow.Cells["Item Code"].Value.ToString() != "" && activeRow.Cells["Location"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select a location.");
					e.Cancel = true;
					activeRow.Cells["Location"].Activate();
				}
				else if (activeRow.Cells["Quantity"].Value.ToString() == "")
				{
					activeRow.Cells["Quantity"].Value = 0;
				}
			}
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
			if (e.Cell.Column.Key == "Item Code" && dataGridItems.ExistCellValue("Item Code", e.NewValue.ToString()) >= 0)
			{
				ErrorHelper.WarningMessage("This item is already added.");
				e.Cancel = true;
			}
			else if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLot(e.Cell))
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
					currentData = new ConsignInData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ConsignInTable.Rows[0] : currentData.ConsignInTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["PONumber"] = textBoxPONumber.Text;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["VendorID"] = comboBoxVendor.SelectedID;
				dataRow["TransporterID"] = comboBoxTransporter.SelectedID;
				dataRow["ArrivalPort"] = comboBoxArrivalPort.SelectedID;
				dataRow["ContainerNo"] = textBoxContainerNo.Text;
				dataRow["BLNo"] = textBoxBLNo.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ConsignInTable.Rows.Add(dataRow);
				}
				currentData.ConsignInDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.ConsignInDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					if (hasUsedLots && row.Cells["Item Code"].Activation == Activation.AllowEdit)
					{
						dataRow2["IsNewRow"] = true;
					}
					else
					{
						dataRow2["IsNewRow"] = false;
					}
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					if (row.Cells["TargetPrice"].Value != null && row.Cells["TargetPrice"].Value.ToString() != "")
					{
						dataRow2["TargetPrice"] = row.Cells["TargetPrice"].Value.ToString();
					}
					else
					{
						dataRow2["TargetPrice"] = DBNull.Value;
					}
					dataRow2["RowIndex"] = row.Index;
					dataRow2.EndEdit();
					currentData.ConsignInDetailTable.Rows.Add(dataRow2);
					if (row.Cells["Quantity"].Tag != null)
					{
						foreach (DataRow row2 in (row.Cells["Quantity"].Tag as DataTable).Rows)
						{
							DataRow dataRow4 = currentData.Tables["Product_Lot_Receiving_Detail"].NewRow();
							dataRow4["ProductID"] = row2["ProductID"];
							dataRow4["LocationID"] = row2["LocationID"];
							dataRow4["LotNumber"] = row2["LotNumber"];
							dataRow4["BinID"] = row2["BinID"];
							dataRow4["ProductionDate"] = row2["ProductionDate"];
							dataRow4["ExpiryDate"] = row2["ExpiryDate"];
							dataRow4["LotQty"] = row2["LotQty"];
							dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
							dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
							dataRow4["RowIndex"] = row.Index;
							currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow4);
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
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("TargetPrice", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].Hidden = true;
				dataGridItems.LoadLayout();
				AdjustGridColumnSettings();
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void AdjustGridColumnSettings()
		{
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
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
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
				dataGridItems.DisplayLayout.Bands[0].Columns["SISysDocID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SIVoucherID"].Hidden = true;
				dataGridItems.DisplayLayout.Bands[0].Columns["SIRowIndex"].Hidden = true;
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					row.Cells["Item Code"].Activation = Activation.Disabled;
					row.Cells["Item Code"].Appearance.BackColorDisabled = Color.WhiteSmoke;
					row.Cells["Item Code"].Appearance.ForeColorDisabled = Color.Black;
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
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].MaxLength = 20;
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].Format = "#,0.00##";
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].MinValue = 0;
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].MaxValue = new decimal(999999999999L);
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Width = checked(10 * dataGridItems.Width) / 100;
			dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Width = checked(10 * dataGridItems.Width) / 100;
			if (dataGridItems.DisplayLayout.Bands[0].Summaries.Exists("TotalQty"))
			{
				dataGridItems.DisplayLayout.Bands[0].Summaries.Remove(dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"]);
			}
			dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
			dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
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
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
			dataGridItems.DisplayLayout.Bands[0].Columns["TargetPrice"].Header.Appearance.Cursor = Cursors.Hand;
			dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CellActivation = Activation.Disabled;
				comboBoxGridLocation.ReadOnly = true;
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
					currentData = Factory.ConsignInSystem.GetConsignInByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["Consign_In"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					textBoxPONumber.Text = dataRow["PONumber"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxVendor.SelectedID = dataRow["VendorID"].ToString();
					comboBoxTransporter.SelectedID = dataRow["TransporterID"].ToString();
					comboBoxArrivalPort.SelectedID = dataRow["ArrivalPort"].ToString();
					textBoxContainerNo.Text = dataRow["ContainerNo"].ToString();
					textBoxBLNo.Text = dataRow["BLNo"].ToString();
					hasUsedLots = Factory.ProductSystem.DocumentHasUsedLots(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text);
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Consign_In_Detail") && currentData.ConsignInDetailTable.Rows.Count != 0)
					{
						foreach (DataRow row in currentData.Tables["Consign_In_Detail"].Rows)
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
							dataRow3["Location"] = row["LocationID"];
							if (row["TargetPrice"] != DBNull.Value)
							{
								dataRow3["TargetPrice"] = decimal.Parse(row["TargetPrice"].ToString()).ToString(Format.UnitPriceFormat);
							}
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
							DataRow[] array = currentData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
							if (array.Length != 0)
							{
								DataSet dataSet = new DataSet();
								dataSet.Merge(array);
								DataTable tag = dataSet.Tables[0];
								row2.Cells["Quantity"].Tag = tag;
								row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
							}
							if (hasUsedLots)
							{
								UltraGridCell ultraGridCell = row2.Cells["Item Code"];
								UltraGridCell ultraGridCell2 = row2.Cells["Description"];
								UltraGridCell ultraGridCell3 = row2.Cells["Location"];
								Activation activation2 = row2.Cells["Unit"].Activation = Activation.NoEdit;
								Activation activation4 = ultraGridCell3.Activation = activation2;
								Activation activation7 = ultraGridCell.Activation = (ultraGridCell2.Activation = activation4);
								Infragistics.Win.Appearance appearance = row2.Cells["Item Code"].Appearance;
								Infragistics.Win.Appearance appearance2 = row2.Cells["Description"].Appearance;
								Infragistics.Win.Appearance appearance3 = row2.Cells["Location"].Appearance;
								Color color = row2.Cells["Unit"].Appearance.BackColor = Color.WhiteSmoke;
								Color color3 = appearance3.BackColor = color;
								Color color6 = appearance.BackColor = (appearance2.BackColor = color3);
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
						int num = 1;
						if (dataRow["Status"] != DBNull.Value)
						{
							num = int.Parse(dataRow["Status"].ToString());
						}
						switch (num)
						{
						case 2:
							docStatusLabel.Visible = true;
							docStatusLabel.DisplayStatus = "Settled";
							break;
						case 3:
							docStatusLabel.Visible = true;
							docStatusLabel.DisplayStatus = "Closed";
							break;
						default:
							docStatusLabel.Visible = false;
							break;
						}
						if (hasUsedLots)
						{
							dateTimePickerDate.Enabled = false;
							dataGridItems.ContextMenuStrip.Enabled = false;
							dataGridItems.ShowDeleteMenu = false;
							dataGridItems.ShowInsertMenu = false;
							dataGridItems.ShowMoveRowsMenu = false;
						}
						else
						{
							dateTimePickerDate.Enabled = true;
							dataGridItems.ContextMenuStrip.Enabled = true;
							DataEntryGrid dataEntryGrid = dataGridItems;
							DataEntryGrid dataEntryGrid2 = dataGridItems;
							bool flag2 = dataGridItems.ShowMoveRowsMenu = true;
							bool showDeleteMenu = dataEntryGrid2.ShowInsertMenu = flag2;
							dataEntryGrid.ShowDeleteMenu = showDeleteMenu;
						}
						checkedListBoxsettled.Items.Clear();
						if (currentData.Tables.Contains("ConsignIn_Settlement"))
						{
							labelSelectedDocs.Visible = true;
							checkedListBoxsettled.Visible = true;
							foreach (DataRow row3 in currentData.Tables["ConsignIn_Settlement"].Rows)
							{
								NameValue item = new NameValue(row3["VoucherID"].ToString(), row3["SysDocID"].ToString());
								checkedListBoxsettled.Items.Add(item);
							}
						}
						CalculateTotal();
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
				bool flag = Factory.ConsignInSystem.CreateConsignIn(currentData, !isNewRecord);
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
				if (ex.Number == 1053)
				{
					ErrorHelper.WarningMessage(ex.Message);
				}
				else if (ex.Number == 1054)
				{
					ErrorHelper.WarningMessage(ex.Message);
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
			if (!isNewRecord && !hasUsedLots && Factory.ProductSystem.DocumentHasUsedLots(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
			{
				ErrorHelper.WarningMessage("Some items from this consignment are already issued. You need to reload the transaction before updating.");
				return false;
			}
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Consign_In", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (!isNewRecord && !Factory.ConsignInSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Document is in use by another transaction or some items are already settled or returned. Unable to modify.");
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
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Consign_In", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				textBoxNote.Clear();
				textBoxRef1.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				comboBoxGridItem.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxVendor.Clear();
				textBoxPONumber.Clear();
				comboBoxTransporter.Clear();
				comboBoxArrivalPort.Clear();
				textBoxContainerNo.Clear();
				textBoxBLNo.Clear();
				checkedListBoxsettled.Items.Clear();
				isDiscountPercent = false;
				isDiscountPercent = false;
				DataTable obj = dataGridItems.DataSource as DataTable;
				hasUsedLots = false;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				obj.Rows.Clear();
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
				if (Factory.ProductSystem.DocumentHasUsedLots(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("This transaction cannot be deleted because some items are refered by other transactions.");
					return false;
				}
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DeleteRecord) == DialogResult.No)
				{
					return false;
				}
				if (!Factory.ConsignInSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.ErrorMessage("Document is in use by another transaction. Unable to modify.");
					return false;
				}
				return Factory.ConsignInSystem.DeleteConsignIn(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Consign_In", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Consign_In", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Consign_In", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Consign_In", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Consign_In", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				dataGridItems.SetupUI();
				SetupGrid();
				comboBoxSysDoc.FilterByType(SysDocTypes.ConsignIn);
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
			if (ErrorHelper.QuestionMessageYesNo(UIMessages.WantToVoid) != DialogResult.No && Void(isVoid: true))
			{
				IsVoid = true;
			}
		}

		private bool Void(bool isVoid)
		{
			try
			{
				if (!isNewRecord && !Factory.ConsignInSystem.AllowModify(comboBoxSysDoc.SelectedID, textBoxVoucherNumber.Text))
				{
					ErrorHelper.WarningMessage("Document is in use by another transaction. Unable to modify.");
					return false;
				}
				return Factory.ConsignInSystem.VoidConsignIn(SystemDocID, textBoxVoucherNumber.Text, isVoid);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.ConsignIn);
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
							a = item.Cells["Vendor"].Value.ToString();
						}
						else if (a != item.Cells["Vendor"].Value.ToString())
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
		}

		private void Print()
		{
		}

		private void Print(bool isPrint, bool showPrintDialog, bool saveChanges)
		{
			try
			{
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet consignInToPrint = Factory.ConsignInSystem.GetConsignInToPrint(selectedID, text);
					if (consignInToPrint == null || consignInToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						string printTemplateName = comboBoxSysDoc.GetPrintTemplateName();
						if (!string.IsNullOrEmpty(printTemplateName))
						{
							PrintHelper.PrintDocument(consignInToPrint, selectedID, printTemplateName, SysDocTypes.ConsignIn, isPrint, showPrintDialog);
						}
						else
						{
							PrintHelper.PrintDocument(consignInToPrint, selectedID, "Consign-In Detail", SysDocTypes.ConsignIn, isPrint, showPrintDialog);
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

		private void ultraFormattedLinkLabel4_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditVendor(comboBoxVendor.SelectedID);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel1_LinkClicked_1(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditTransporter(comboBoxTransporter.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ConsignInListFormObj);
		}

		private void checkedListBoxsettled_DoubleClick(object sender, EventArgs e)
		{
			if (checkedListBoxsettled.SelectedItem.ToString() != "")
			{
				NameValue nameValue = checkedListBoxsettled.SelectedItem as NameValue;
				new FormHelper().EditTransaction(TransactionListType.ConsignInSettlement, nameValue.ID, nameValue.Name);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind.PerformClick();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Vendors.ConsignInForm));
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
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			duplicateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label8 = new System.Windows.Forms.Label();
			textBoxBLNo = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			comboBoxArrivalPort = new Micromind.DataControls.PortComboBox();
			label5 = new System.Windows.Forms.Label();
			textBoxContainerNo = new System.Windows.Forms.TextBox();
			textBoxTransporterName = new System.Windows.Forms.TextBox();
			comboBoxTransporter = new Micromind.DataControls.TransporterComboBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxVendor = new Micromind.DataControls.vendorsFlatComboBox();
			label2 = new System.Windows.Forms.Label();
			textBoxPONumber = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			textBoxVendorName = new System.Windows.Forms.TextBox();
			labelVoided = new System.Windows.Forms.Label();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			formManager = new Micromind.DataControls.FormManager();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			checkedListBoxsettled = new System.Windows.Forms.ListBox();
			labelSelectedDocs = new System.Windows.Forms.Label();
			docStatusLabel = new Micromind.DataControls.OtherControls.DocStatusLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			SuspendLayout();
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
				toolStripSeparator6,
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator5,
				toolStripButtonDistribution,
				toolStripButtonInformation,
				toolStripDropDownButton1
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
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(6, 31);
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
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				duplicateToolStripMenuItem
			});
			toolStripDropDownButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownButton1.Image");
			toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripDropDownButton1.Name = "toolStripDropDownButton1";
			toolStripDropDownButton1.Size = new System.Drawing.Size(60, 28);
			toolStripDropDownButton1.Text = "Actions";
			toolStripDropDownButton1.DropDownOpening += new System.EventHandler(toolStripDropDownButton1_DropDownOpening);
			duplicateToolStripMenuItem.Name = "duplicateToolStripMenuItem";
			duplicateToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			duplicateToolStripMenuItem.Text = "Copy";
			duplicateToolStripMenuItem.Click += new System.EventHandler(duplicateToolStripMenuItem_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 541);
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
			dateTimePickerDate.Location = new System.Drawing.Point(551, 1);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(107, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(326, 1);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(109, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(469, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(551, 23);
			textBoxRef1.MaxLength = 20;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(107, 20);
			textBoxRef1.TabIndex = 5;
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(49, 470);
			textBoxNote.MaxLength = 4000;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(381, 58);
			textBoxNote.TabIndex = 2;
			label3.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(10, 469);
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
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(textBoxBLNo);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(comboBoxArrivalPort);
			panelDetails.Controls.Add(label5);
			panelDetails.Controls.Add(textBoxContainerNo);
			panelDetails.Controls.Add(textBoxTransporterName);
			panelDetails.Controls.Add(comboBoxTransporter);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxVendor);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxPONumber);
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
			panelDetails.Size = new System.Drawing.Size(749, 97);
			panelDetails.TabIndex = 0;
			appearance3.FontData.BoldAsString = "False";
			appearance3.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance3;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(11, 49);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(65, 15);
			ultraFormattedLinkLabel1.TabIndex = 158;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Transporter:";
			appearance4.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance4;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked_1);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(469, 70);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(48, 13);
			label8.TabIndex = 157;
			label8.Text = "BOL No:";
			textBoxBLNo.Location = new System.Drawing.Point(551, 67);
			textBoxBLNo.MaxLength = 15;
			textBoxBLNo.Name = "textBoxBLNo";
			textBoxBLNo.Size = new System.Drawing.Size(142, 20);
			textBoxBLNo.TabIndex = 11;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(10, 70);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(61, 13);
			label6.TabIndex = 153;
			label6.Text = "Arrival Port:";
			comboBoxArrivalPort.Assigned = false;
			comboBoxArrivalPort.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxArrivalPort.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxArrivalPort.CustomReportFieldName = "";
			comboBoxArrivalPort.CustomReportKey = "";
			comboBoxArrivalPort.CustomReportValueType = 1;
			comboBoxArrivalPort.DescriptionTextBox = null;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxArrivalPort.DisplayLayout.Appearance = appearance5;
			comboBoxArrivalPort.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxArrivalPort.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance6.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance6.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.Appearance = appearance6;
			appearance7.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BandLabelAppearance = appearance7;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance8.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance8.BackColor2 = System.Drawing.SystemColors.Control;
			appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance8.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxArrivalPort.DisplayLayout.GroupByBox.PromptAppearance = appearance8;
			comboBoxArrivalPort.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxArrivalPort.DisplayLayout.MaxRowScrollRegions = 1;
			appearance9.BackColor = System.Drawing.SystemColors.Window;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveCellAppearance = appearance9;
			appearance10.BackColor = System.Drawing.SystemColors.Highlight;
			appearance10.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxArrivalPort.DisplayLayout.Override.ActiveRowAppearance = appearance10;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxArrivalPort.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.CardAreaAppearance = appearance11;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			appearance12.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxArrivalPort.DisplayLayout.Override.CellAppearance = appearance12;
			comboBoxArrivalPort.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxArrivalPort.DisplayLayout.Override.CellPadding = 0;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance13.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance13.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxArrivalPort.DisplayLayout.Override.GroupByRowAppearance = appearance13;
			appearance14.TextHAlignAsString = "Left";
			comboBoxArrivalPort.DisplayLayout.Override.HeaderAppearance = appearance14;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxArrivalPort.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.BorderColor = System.Drawing.Color.Silver;
			comboBoxArrivalPort.DisplayLayout.Override.RowAppearance = appearance15;
			comboBoxArrivalPort.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxArrivalPort.DisplayLayout.Override.TemplateAddRowAppearance = appearance16;
			comboBoxArrivalPort.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxArrivalPort.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxArrivalPort.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxArrivalPort.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxArrivalPort.Editable = true;
			comboBoxArrivalPort.FilterString = "";
			comboBoxArrivalPort.HasAllAccount = false;
			comboBoxArrivalPort.HasCustom = false;
			comboBoxArrivalPort.IsDataLoaded = false;
			comboBoxArrivalPort.Location = new System.Drawing.Point(99, 66);
			comboBoxArrivalPort.MaxDropDownItems = 12;
			comboBoxArrivalPort.Name = "comboBoxArrivalPort";
			comboBoxArrivalPort.ShowInactiveItems = false;
			comboBoxArrivalPort.ShowQuickAdd = true;
			comboBoxArrivalPort.Size = new System.Drawing.Size(109, 20);
			comboBoxArrivalPort.TabIndex = 9;
			comboBoxArrivalPort.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(214, 70);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 13);
			label5.TabIndex = 151;
			label5.Text = "Container No:";
			textBoxContainerNo.Location = new System.Drawing.Point(291, 67);
			textBoxContainerNo.MaxLength = 15;
			textBoxContainerNo.Name = "textBoxContainerNo";
			textBoxContainerNo.Size = new System.Drawing.Size(144, 20);
			textBoxContainerNo.TabIndex = 10;
			textBoxTransporterName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxTransporterName.Location = new System.Drawing.Point(210, 45);
			textBoxTransporterName.MaxLength = 64;
			textBoxTransporterName.Name = "textBoxTransporterName";
			textBoxTransporterName.ReadOnly = true;
			textBoxTransporterName.Size = new System.Drawing.Size(225, 20);
			textBoxTransporterName.TabIndex = 7;
			textBoxTransporterName.TabStop = false;
			comboBoxTransporter.Assigned = false;
			comboBoxTransporter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxTransporter.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxTransporter.CustomReportFieldName = "";
			comboBoxTransporter.CustomReportKey = "";
			comboBoxTransporter.CustomReportValueType = 1;
			comboBoxTransporter.DescriptionTextBox = textBoxTransporterName;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxTransporter.DisplayLayout.Appearance = appearance17;
			comboBoxTransporter.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxTransporter.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance18.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance18.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.GroupByBox.Appearance = appearance18;
			appearance19.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.BandLabelAppearance = appearance19;
			comboBoxTransporter.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance20.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance20.BackColor2 = System.Drawing.SystemColors.Control;
			appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance20.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxTransporter.DisplayLayout.GroupByBox.PromptAppearance = appearance20;
			comboBoxTransporter.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxTransporter.DisplayLayout.MaxRowScrollRegions = 1;
			appearance21.BackColor = System.Drawing.SystemColors.Window;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxTransporter.DisplayLayout.Override.ActiveCellAppearance = appearance21;
			appearance22.BackColor = System.Drawing.SystemColors.Highlight;
			appearance22.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxTransporter.DisplayLayout.Override.ActiveRowAppearance = appearance22;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxTransporter.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.CardAreaAppearance = appearance23;
			appearance24.BorderColor = System.Drawing.Color.Silver;
			appearance24.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxTransporter.DisplayLayout.Override.CellAppearance = appearance24;
			comboBoxTransporter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxTransporter.DisplayLayout.Override.CellPadding = 0;
			appearance25.BackColor = System.Drawing.SystemColors.Control;
			appearance25.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance25.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance25.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance25.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxTransporter.DisplayLayout.Override.GroupByRowAppearance = appearance25;
			appearance26.TextHAlignAsString = "Left";
			comboBoxTransporter.DisplayLayout.Override.HeaderAppearance = appearance26;
			comboBoxTransporter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxTransporter.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance27.BackColor = System.Drawing.SystemColors.Window;
			appearance27.BorderColor = System.Drawing.Color.Silver;
			comboBoxTransporter.DisplayLayout.Override.RowAppearance = appearance27;
			comboBoxTransporter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxTransporter.DisplayLayout.Override.TemplateAddRowAppearance = appearance28;
			comboBoxTransporter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxTransporter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxTransporter.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxTransporter.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxTransporter.Editable = true;
			comboBoxTransporter.FilterString = "";
			comboBoxTransporter.HasAllAccount = false;
			comboBoxTransporter.HasCustom = false;
			comboBoxTransporter.IsDataLoaded = false;
			comboBoxTransporter.Location = new System.Drawing.Point(99, 45);
			comboBoxTransporter.MaxDropDownItems = 12;
			comboBoxTransporter.Name = "comboBoxTransporter";
			comboBoxTransporter.ShowInactiveItems = false;
			comboBoxTransporter.ShowQuickAdd = true;
			comboBoxTransporter.Size = new System.Drawing.Size(109, 20);
			comboBoxTransporter.TabIndex = 6;
			comboBoxTransporter.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance29.FontData.BoldAsString = "True";
			appearance29.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance29;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(11, 27);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(64, 15);
			ultraFormattedLinkLabel4.TabIndex = 146;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Vendor ID:";
			appearance30.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance30;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked_1);
			comboBoxVendor.Assigned = false;
			comboBoxVendor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxVendor.CustomReportFieldName = "";
			comboBoxVendor.CustomReportKey = "";
			comboBoxVendor.CustomReportValueType = 1;
			comboBoxVendor.DescriptionTextBox = null;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			appearance31.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxVendor.DisplayLayout.Appearance = appearance31;
			comboBoxVendor.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxVendor.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance32.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance32.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance32.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance32.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.GroupByBox.Appearance = appearance32;
			appearance33.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.BandLabelAppearance = appearance33;
			comboBoxVendor.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance34.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance34.BackColor2 = System.Drawing.SystemColors.Control;
			appearance34.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance34.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxVendor.DisplayLayout.GroupByBox.PromptAppearance = appearance34;
			comboBoxVendor.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxVendor.DisplayLayout.MaxRowScrollRegions = 1;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxVendor.DisplayLayout.Override.ActiveCellAppearance = appearance35;
			appearance36.BackColor = System.Drawing.SystemColors.Highlight;
			appearance36.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxVendor.DisplayLayout.Override.ActiveRowAppearance = appearance36;
			comboBoxVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxVendor.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.CardAreaAppearance = appearance37;
			appearance38.BorderColor = System.Drawing.Color.Silver;
			appearance38.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxVendor.DisplayLayout.Override.CellAppearance = appearance38;
			comboBoxVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxVendor.DisplayLayout.Override.CellPadding = 0;
			appearance39.BackColor = System.Drawing.SystemColors.Control;
			appearance39.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance39.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance39.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxVendor.DisplayLayout.Override.GroupByRowAppearance = appearance39;
			appearance40.TextHAlignAsString = "Left";
			comboBoxVendor.DisplayLayout.Override.HeaderAppearance = appearance40;
			comboBoxVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxVendor.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.BorderColor = System.Drawing.Color.Silver;
			comboBoxVendor.DisplayLayout.Override.RowAppearance = appearance41;
			comboBoxVendor.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance42.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxVendor.DisplayLayout.Override.TemplateAddRowAppearance = appearance42;
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
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(469, 48);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(77, 13);
			label2.TabIndex = 143;
			label2.Text = "Vendor DOC#:";
			textBoxPONumber.Location = new System.Drawing.Point(551, 45);
			textBoxPONumber.MaxLength = 15;
			textBoxPONumber.Name = "textBoxPONumber";
			textBoxPONumber.Size = new System.Drawing.Size(142, 20);
			textBoxPONumber.TabIndex = 8;
			appearance43.FontData.BoldAsString = "True";
			appearance43.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance43;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(12, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 2;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance44.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance44;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			comboBoxSysDoc.Assigned = false;
			comboBoxSysDoc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSysDoc.CustomReportFieldName = "";
			comboBoxSysDoc.CustomReportKey = "";
			comboBoxSysDoc.CustomReportValueType = 1;
			comboBoxSysDoc.DescriptionTextBox = null;
			appearance45.BackColor = System.Drawing.SystemColors.Window;
			appearance45.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxSysDoc.DisplayLayout.Appearance = appearance45;
			comboBoxSysDoc.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxSysDoc.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance46.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance46.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance46.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance46.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.GroupByBox.Appearance = appearance46;
			appearance47.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BandLabelAppearance = appearance47;
			comboBoxSysDoc.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance48.BackColor2 = System.Drawing.SystemColors.Control;
			appearance48.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance48.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxSysDoc.DisplayLayout.GroupByBox.PromptAppearance = appearance48;
			comboBoxSysDoc.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxSysDoc.DisplayLayout.MaxRowScrollRegions = 1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveCellAppearance = appearance49;
			appearance50.BackColor = System.Drawing.SystemColors.Highlight;
			appearance50.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxSysDoc.DisplayLayout.Override.ActiveRowAppearance = appearance50;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxSysDoc.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance51.BackColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.CardAreaAppearance = appearance51;
			appearance52.BorderColor = System.Drawing.Color.Silver;
			appearance52.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxSysDoc.DisplayLayout.Override.CellAppearance = appearance52;
			comboBoxSysDoc.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxSysDoc.DisplayLayout.Override.CellPadding = 0;
			appearance53.BackColor = System.Drawing.SystemColors.Control;
			appearance53.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance53.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance53.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance53.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxSysDoc.DisplayLayout.Override.GroupByRowAppearance = appearance53;
			appearance54.TextHAlignAsString = "Left";
			comboBoxSysDoc.DisplayLayout.Override.HeaderAppearance = appearance54;
			comboBoxSysDoc.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxSysDoc.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			appearance55.BorderColor = System.Drawing.Color.Silver;
			comboBoxSysDoc.DisplayLayout.Override.RowAppearance = appearance55;
			comboBoxSysDoc.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxSysDoc.DisplayLayout.Override.TemplateAddRowAppearance = appearance56;
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
			mmLabel1.Location = new System.Drawing.Point(469, 3);
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
			labelVoided.Location = new System.Drawing.Point(13, 415);
			labelVoided.Name = "labelVoided";
			labelVoided.Size = new System.Drawing.Size(750, 49);
			labelVoided.TabIndex = 117;
			labelVoided.Text = "VOIDED";
			labelVoided.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			labelVoided.Visible = false;
			contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem
			});
			contextMenuStrip1.Name = "contextMenuStrip1";
			contextMenuStrip1.Size = new System.Drawing.Size(181, 92);
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
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance57;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance58.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance58.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance58.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance58.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance58;
			appearance59.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance59;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance60.BackColor2 = System.Drawing.SystemColors.Control;
			appearance60.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance60.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance60;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance61;
			appearance62.BackColor = System.Drawing.SystemColors.Highlight;
			appearance62.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance62;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance63;
			appearance64.BorderColor = System.Drawing.Color.Silver;
			appearance64.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance64;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance65.BackColor = System.Drawing.SystemColors.Control;
			appearance65.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance65.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance65.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance65.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance65;
			appearance66.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance66;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			appearance67.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance67;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance68;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(654, 194);
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
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance69;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance70.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance70.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance70.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance70.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance70;
			appearance71.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance71;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance72.BackColor2 = System.Drawing.SystemColors.Control;
			appearance72.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance72.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance72;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance73;
			appearance74.BackColor = System.Drawing.SystemColors.Highlight;
			appearance74.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance74;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance75;
			appearance76.BorderColor = System.Drawing.Color.Silver;
			appearance76.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance76;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance77.BackColor = System.Drawing.SystemColors.Control;
			appearance77.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance77.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance77.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance77.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance77;
			appearance78.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance78;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			appearance79.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance79;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance80.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance80;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(12, 134);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(752, 331);
			dataGridItems.TabIndex = 1;
			dataGridItems.Text = "dataEntryGrid1";
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance81;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
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
			comboBoxGridItem.ShowOnlyConsignmentItems = true;
			comboBoxGridItem.ShowOnlyLotItems = false;
			comboBoxGridItem.ShowQuickAdd = true;
			comboBoxGridItem.Size = new System.Drawing.Size(74, 20);
			comboBoxGridItem.TabIndex = 118;
			comboBoxGridItem.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridItem.Visible = false;
			checkedListBoxsettled.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxsettled.FormattingEnabled = true;
			checkedListBoxsettled.Location = new System.Drawing.Point(436, 484);
			checkedListBoxsettled.Name = "checkedListBoxsettled";
			checkedListBoxsettled.Size = new System.Drawing.Size(124, 43);
			checkedListBoxsettled.TabIndex = 128;
			checkedListBoxsettled.Visible = false;
			checkedListBoxsettled.DoubleClick += new System.EventHandler(checkedListBoxsettled_DoubleClick);
			labelSelectedDocs.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			labelSelectedDocs.AutoSize = true;
			labelSelectedDocs.Location = new System.Drawing.Point(433, 468);
			labelSelectedDocs.Name = "labelSelectedDocs";
			labelSelectedDocs.Size = new System.Drawing.Size(62, 13);
			labelSelectedDocs.TabIndex = 129;
			labelSelectedDocs.Text = "Settlements";
			labelSelectedDocs.Visible = false;
			docStatusLabel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			docStatusLabel.BackColor = System.Drawing.Color.Transparent;
			docStatusLabel.DocumentNumber = "";
			docStatusLabel.LinkEnabled = true;
			docStatusLabel.Location = new System.Drawing.Point(589, 484);
			docStatusLabel.Name = "docStatusLabel";
			docStatusLabel.ShowDocNumber = false;
			docStatusLabel.Size = new System.Drawing.Size(131, 44);
			docStatusLabel.StatusColor = Micromind.DataControls.OtherControls.DocStatusLabel.StatusColors.Red;
			docStatusLabel.TabIndex = 130;
			docStatusLabel.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(778, 581);
			base.Controls.Add(docStatusLabel);
			base.Controls.Add(labelSelectedDocs);
			base.Controls.Add(checkedListBoxsettled);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(productPhotoViewer);
			base.Controls.Add(labelVoided);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxGridItem);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label3);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 396);
			base.Name = "ConsignInForm";
			Text = "Receive Consignment In";
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxArrivalPort).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxTransporter).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxVendor).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
