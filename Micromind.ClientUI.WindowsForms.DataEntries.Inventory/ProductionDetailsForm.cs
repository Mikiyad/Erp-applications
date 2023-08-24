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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductionDetailsForm : Form, IForm
	{
		private ProductionData currentData;

		private const string TABLENAME_CONST = "Mfg_Production";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool loadItemDescFromPriceList = CompanyPreferences.LoadItemDescFromPriceList;

		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private string sourceSysDocID = "";

		private string sourceVoucherID = "";

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool canAccessCost;

		private DataSet priceListData;

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

		private DateTimePicker dateTimePickerDate;

		private TextBox textBoxVoucherNumber;

		private Label label1;

		private MMLabel mmLabel1;

		private TextBox textBoxRef;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private UltraGroupBox ultraGroupBox1;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ProductComboBox comboBoxGridItem;

		private LocationComboBox comboBoxLocation;

		private UltraLabel ultraLabel1;

		private ProductUnitComboBox comboBoxGridProductUnit;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabelRoute;

		private TextBox textBoxRouteName;

		private BOMComboBox bomComboBox2;

		private BOMComboBox comboBoxBOM;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageItems;

		private Label label5;

		private UltraTabPageControl tabPageExpense;

		private ExpenseCodeComboBox comboBoxGridExpenseCode;

		private CurrencyComboBox comboBoxGridCurrency;

		private DataEntryGrid dataGridExpense;

		private DataEntryGrid dataGridItems;

		private Label label323;

		private SysDocComboBox comboBoxSysDoc;

		private Label labelTotalCost;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonDistribution;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripButton toolStripButtonOpenList;

		private Label label8;

		private DateTimePicker dateTimePickerWorkCompDate;

		private ToolStripDropDownButton toolStripButton1;

		private ToolStripMenuItem saveADraftToolStripMenuItem;

		private ToolStripMenuItem loadDraftToolStripMenuItem;

		private ToolStripMenuItem loadFromWOToolStripMenuItem;

		private LocationComboBox comboBoxGridLocation;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel6;

		private ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ProductUnitComboBox comboBoxGridProductUnit1;

		private ToolStripButton toolStripButtonAttach;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem loadFromMaterialToolStripMenuItem;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel4;

		private customersFlatComboBox comboBoxJobOrder;

		private TextBox textBoxNote;

		private Label label2;

		private RouteComboBox routeComboBox;

		private UltraTabPageControl ultraTabPageControl1;

		private DataEntryGrid dataGridResource;

		private UltraTabPageControl ultraTabPageControl2;

		private DataEntryGrid dataGridRawMaterials;

		private ProductComboBox rawMaterialsProductComboBox;

		private PositionComboBox comboBoxGridPosition;

		private EmployeeComboBox comboBoxGridEmployee;

		private ProductUnitComboBox productUnitComboBox;

		private TextBox textBoxJobOrderName;

		private Label label3;

		private TextBox textBoxRef1;

		private TextBox textBoxBOMName;

		private LocationComboBox itemLocationComboBox;

		private LocationComboBox rawMaterialsLocationComboBox;

		private ContextMenuStrip contextMenuStrip3;

		private ToolStripMenuItem toolStripMenuItem1;

		private ToolStripMenuItem toolStripMenuItem2;

		private ToolStripMenuItem toolStripMenuItem3;

		private ToolStripMenuItem toolStripMenuItem4;

		private ToolStripSeparator toolStripSeparator6;

		private ToolStripMenuItem toolStripMenuItem5;

		private RouteComboBox routeGridComboBox;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4002;

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
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					bool enabled = buttonVoid.Enabled = false;
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
					comboBoxSysDoc.Enabled = true;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					bool enabled = buttonVoid.Enabled = true;
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
					comboBoxSysDoc.Enabled = false;
				}
				toolStripButtonPrint.Enabled = !value;
				toolStripButtonPreview.Enabled = !value;
				toolStripButtonAttach.Enabled = !value;
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
					dataGridItems.Enabled = !value;
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

		public string SourceSysDocID
		{
			get
			{
				return sourceSysDocID;
			}
			set
			{
				sourceSysDocID = value;
			}
		}

		public string SourceVoucherID
		{
			get
			{
				return sourceVoucherID;
			}
			set
			{
				sourceVoucherID = value;
			}
		}

		public ProductionDetailsForm()
		{
			InitializeComponent();
			AddEvents();
			checked
			{
				int num;
				for (num = 0; num < contextMenuStrip1.Items.Count; num++)
				{
					dataGridItems.DropDownMenu.Items.Add(contextMenuStrip1.Items[num]);
					num--;
				}
				dataGridItems.AllowCustomizeHeaders = true;
				comboBoxGridExpenseCode.ExpenseCodeType = ExpenseCodeTypes.Manufacturing;
			}
		}

		private void AddEvents()
		{
			base.Load += BuildAssemblyForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.CellChange += dataGridItems_CellChange;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			base.KeyDown += SalesOrderForm_KeyDown;
			base.FormClosing += Form_FormClosing;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridExpense.BeforeRowDeactivate += dataGridExpense_BeforeRowDeactivate;
			dataGridExpense.BeforeCellDeactivate += dataGridExpense_BeforeCellDeactivate;
			dataGridExpense.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			dataGridExpense.CellDataError += dataGridExpense_CellDataError;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxGridProductUnit.SelectedIndexChanged += comboBoxGridProductUnit_SelectedIndexChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			dataGridResource.AfterCellUpdate += dataGridResource_AfterCellUpdate;
			dataGridRawMaterials.AfterCellUpdate += dataGridRawMaterials_AfterCellUpdate;
			dataGridRawMaterials.HeaderClicked += dataGridRawMaterials_HeaderClicked;
			dataGridRawMaterials.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			comboBoxBOM.SelectedIndexChanged += comboBoxBOM_SelectedIndexChanged;
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

		private void dataGridExpense_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridExpense.ActiveCell.Column.Key == "Expense Code")
			{
				ErrorHelper.InformationMessage("Please select a valid item.");
				e.RaiseErrorEvent = false;
			}
		}

		private void dataGridRawMaterials_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridRawMaterials.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "Item Code")
			{
				dataGridRawMaterials.ActiveRow.Cells["Description"].Value = rawMaterialsProductComboBox.SelectedName;
				dataGridRawMaterials.ActiveRow.Cells["Qty"].Value = 0;
				dataGridRawMaterials.ActiveRow.Cells["Location"].Value = comboBoxLocation.SelectedID;
				dataGridRawMaterials.ActiveRow.Cells["Unit"].ValueList = rawMaterialsProductComboBox.GetProductUnitsValueList(rawMaterialsProductComboBox.SelectedID);
				dataGridRawMaterials.ActiveRow.Cells["Unit"].Value = rawMaterialsProductComboBox.SelectedUnitID;
				if (rawMaterialsProductComboBox.SelectedID != "")
				{
					Factory.ProductSystem.GetProductByID(rawMaterialsProductComboBox.SelectedID);
					DateTime date = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 11, 59, 59);
					DataSet productQuantityAndCostAsOfDate = Factory.ProductSystem.GetProductQuantityAndCostAsOfDate(rawMaterialsProductComboBox.SelectedID, comboBoxLocation.SelectedID, date);
					DataRow dataRow = null;
					if (productQuantityAndCostAsOfDate != null && productQuantityAndCostAsOfDate.Tables.Count > 0 && productQuantityAndCostAsOfDate.Tables[0].Rows.Count > 0)
					{
						dataRow = productQuantityAndCostAsOfDate.Tables[0].Rows[0];
					}
					if (dataRow != null)
					{
						dataGridRawMaterials.ActiveRow.Cells["Cost"].Value = dataRow["AverageCost"].ToString();
					}
					else
					{
						dataGridRawMaterials.ActiveRow.Cells["Cost"].Value = 0;
					}
				}
			}
			if (e.Cell.Column.Key == "Qty" || e.Cell.Column.Key == "Cost")
			{
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataGridRawMaterials.ActiveRow.Cells["Qty"].Value.ToString(), out result);
				decimal.TryParse(dataGridRawMaterials.ActiveRow.Cells["Cost"].Value.ToString(), out result2);
				dataGridRawMaterials.ActiveRow.Cells["Total"].Value = result * result2;
			}
			if (e.Cell.Column.Key == "Unit")
			{
				string text = dataGridRawMaterials.ActiveRow.Cells["Unit"].Value.ToString();
				string text2 = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
				if (text2 != "" && text != "")
				{
					DataSet productUnitDetails = Factory.ProductSystem.GetProductUnitDetails(text2, text, comboBoxSysDoc.LocationID);
					if (productUnitDetails != null && productUnitDetails.Tables.Count > 0 && productUnitDetails.Tables[0].Rows.Count > 0 && productUnitDetails.Tables[1].Rows.Count == 0)
					{
						DataRow dataRow2 = productUnitDetails.Tables[0].Rows[0];
						string a = dataRow2["FactorType"].ToString();
						decimal num = decimal.Parse(dataRow2["Factor"].ToString());
						decimal num2 = decimal.Parse(dataGridRawMaterials.ActiveRow.Cells["Cost"].Value.ToString());
						if (a == "M")
						{
							num2 /= num;
						}
						else
						{
							num2 *= num;
						}
						dataGridRawMaterials.ActiveRow.Cells["Cost"].Value = num2;
					}
				}
			}
			CalculateTotalCost();
		}

		private void dataGridResource_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridResource.ActiveRow != null && dataGridResource.ActiveRow != null)
				{
					string key = e.Cell.Column.Key;
					if (key == "PositionID" && dataGridResource.ActiveCell != null && dataGridResource.ActiveCell.Column.Key == "PositionID" && dataGridResource.ActiveRow.Cells["PositionID"].Value.ToString() != "")
					{
						string text = dataGridResource.ActiveRow.Cells["PositionID"].Value.ToString();
						if (text != "")
						{
							comboBoxGridEmployee.Filter(text);
						}
						else
						{
							comboBoxGridEmployee.Filter("");
						}
					}
					if (key == "EmpNo" && dataGridResource.ActiveCell != null && dataGridResource.ActiveCell.Column.Key == "EmpNo")
					{
						dataGridResource.ActiveRow.Cells["EmpName"].Value = comboBoxGridEmployee.SelectedName;
						dataGridResource.ActiveRow.Cells["Hour"].Value = 1;
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
			CalculateTotalQuantity();
		}

		private void SalesOrderForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.P && !IsNewRecord)
			{
				Print(isPrint: true, showPrintDialog: true, saveChanges: true);
			}
			if (LoadItemFeatures && e.KeyCode == Keys.F3)
			{
				ComboSearchDialogNew comboSearchDialogNew = new ComboSearchDialogNew();
				comboSearchDialogNew.IsMultiSelect = false;
				if (ultraTabControl1.SelectedTab.Index == 0)
				{
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
				}
				else if (ultraTabControl1.SelectedTab.Index == 1)
				{
					_ = dataGridRawMaterials.ActiveRow;
					DataSet dataSet2 = new DataSet();
					dataSet2 = (comboSearchDialogNew.DataSource = Factory.ProductSystem.GetProducts());
					if (dataGridRawMaterials.ActiveCell == null)
					{
						comboSearchDialogNew.SelectedItem = "";
					}
					else
					{
						rawMaterialsProductComboBox.SelectedID = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
						comboSearchDialogNew.SelectedItem = rawMaterialsProductComboBox.SelectedID;
					}
				}
				comboSearchDialogNew.ShowDialog();
				_ = 1;
			}
			if (e.KeyCode != Keys.F6)
			{
				return;
			}
			if (ultraTabControl1.SelectedTab.Index == 0)
			{
				if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					ProductQuantityForm productQuantityForm = new ProductQuantityForm();
					productQuantityForm.LoadData(productID);
					productQuantityForm.ShowDialog();
				}
			}
			else if (ultraTabControl1.SelectedTab.Index == 1 && dataGridRawMaterials.ActiveRow != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID2 = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
				ProductQuantityForm productQuantityForm2 = new ProductQuantityForm();
				productQuantityForm2.LoadData(productID2);
				productQuantityForm2.ShowDialog();
			}
		}

		private void comboBoxBOM_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxLocation.SelectedID != "")
			{
				foreach (UltraGridRow item in dataGridItems.Rows.Where((UltraGridRow x) => x.Cells["Location"].Value.ToString() == ""))
				{
					item.Cells["Location"].Value = comboBoxLocation.SelectedID;
				}
				foreach (UltraGridRow item2 in dataGridRawMaterials.Rows.Where((UltraGridRow x) => x.Cells["Location"].Value.ToString() == ""))
				{
					item2.Cells["Location"].Value = comboBoxLocation.SelectedID;
				}
			}
		}

		private void CalculateTotalQuantity()
		{
		}

		private void CalculateTotalCost()
		{
			try
			{
				decimal d = default(decimal);
				decimal num = default(decimal);
				decimal d2 = default(decimal);
				foreach (UltraGridRow row in dataGridExpense.Rows)
				{
					decimal result = default(decimal);
					if (row.Cells["Amount"].Value != null && !(row.Cells["Amount"].Value.ToString() == ""))
					{
						decimal.TryParse(row.Cells["Amount"].Value.ToString(), out result);
						d += result;
					}
				}
				foreach (UltraGridRow row2 in dataGridRawMaterials.Rows)
				{
					decimal result2 = default(decimal);
					if (row2.Cells["Total"].Value != null && !(row2.Cells["Total"].Value.ToString() == ""))
					{
						decimal.TryParse(row2.Cells["Total"].Value.ToString(), out result2);
						result2 = Math.Round(result2, Global.CurDecimalPoints);
						d2 += result2;
					}
				}
				num = d + d2;
				labelTotalCost.Text = num.ToString(Format.TotalAmountFormat);
				SetTotalCost();
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void comboBoxGridProductUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxGridItem_VisibleChanged(object sender, EventArgs e)
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

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "Item Code")
			{
				dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
				dataGridItems.ActiveRow.Cells["Qty Build"].Value = 0;
				dataGridItems.ActiveRow.Cells["Location"].Value = comboBoxLocation.SelectedID;
				dataGridItems.ActiveRow.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(comboBoxGridItem.SelectedID);
				dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
			}
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Qty Build"].Value.ToString(), out result);
			decimal.TryParse(dataGridItems.ActiveRow.Cells["Cost"].Value.ToString(), out result2);
			if (e.Cell.Column.Key == "Cost")
			{
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Qty Build"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Cost"].Value.ToString(), out result2);
				dataGridItems.ActiveRow.Cells["Total"].Value = result * result2;
			}
			if (e.Cell.Column.Key == "Qty Build")
			{
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Qty Build"].Value.ToString(), out result);
				decimal.TryParse(dataGridItems.ActiveRow.Cells["Cost"].Value.ToString(), out result2);
				dataGridItems.ActiveRow.Cells["Total"].Value = result * result2;
			}
			if (e.Cell.Column.Key == "Unit")
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
						decimal num2 = decimal.Parse(dataGridItems.ActiveRow.Cells["Cost"].Value.ToString());
						if (a == "M")
						{
							num2 /= num;
						}
						else
						{
							num2 *= num;
						}
						dataGridItems.ActiveRow.Cells["Cost"].Value = num2;
					}
				}
			}
			if (e.Cell.Column.Key == "Cost Allocation")
			{
				dataGridItems.ActiveRow.Cells["Total"].Value = 0;
				dataGridItems.ActiveRow.Cells["Cost"].Value = 0;
			}
			CalculateTotalCost();
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip2.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void dataGridRawMaterials_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridRawMaterials.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip3.Show(dataGridRawMaterials, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void dataGridItems_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotalCost();
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
			if (activeRow == null)
			{
				return;
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (!row.IsActiveRow && row.Cells["Item Code"].Value == dataGridItems.ActiveRow.Cells["Item Code"].Value)
				{
					ErrorHelper.InformationMessage("This item is already in the list.");
					e.Cancel = true;
					return;
				}
			}
			if (activeRow.Cells["Item Code"].Value.ToString() == "")
			{
				ErrorHelper.InformationMessage("Please select an item.");
				e.Cancel = true;
				activeRow.Cells["Item Code"].Activate();
				return;
			}
			if (activeRow.Cells["Qty Build"].Value.ToString() == "")
			{
				activeRow.Cells["Qty"].Value = 0;
			}
			labelTotalCost.Text = GetTransactionBalance().ToString(Format.TotalAmountFormat);
		}

		private void dataGrid_BeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e)
		{
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Qty Build")
			{
				e.RaiseErrorEvent = false;
				ErrorHelper.InformationMessage("Invalid quantity. Please enter a numeric value.");
			}
		}

		private void dataGridExpense_AfterCellUpdate(object sender, CellEventArgs e)
		{
			try
			{
				if (dataGridExpense.ActiveRow != null)
				{
					if (e.Cell.Column.Key == "Expense Code")
					{
						dataGridExpense.ActiveRow.Cells["Description"].Value = comboBoxGridExpenseCode.SelectedName;
						decimal d = dataGridRawMaterials.Rows.Sum((UltraGridRow x) => decimal.Parse(x.Cells["Qty"].Value.ToString()));
						dataGridExpense.ActiveRow.Cells["Amount"].Value = d * decimal.Parse(comboBoxGridExpenseCode.ExpenseRate);
					}
					CalculateTotalCost();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridExpense_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridExpense.ActiveRow;
			if (activeRow != null && activeRow.Cells["Amount"].Value.ToString() == "")
			{
				activeRow.Cells["Amount"].Value = 0;
			}
		}

		private void dataGridExpense_BeforeCellDeactivate(object sender, CancelEventArgs e)
		{
			if (dataGridExpense.ActiveCell.Column.Key.ToString() == "Amount")
			{
				decimal result = default(decimal);
				decimal.TryParse(dataGridExpense.ActiveCell.Value.ToString(), out result);
				result = Math.Round(result, Global.CurDecimalPoints);
				dataGridExpense.ActiveCell.Value = result;
				CalculateTotalCost();
			}
		}

		private void dataGridExpense_AfterRowsDeleted(object sender, EventArgs e)
		{
			CalculateTotalCost();
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new ProductionData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.ProductionTable.Rows[0] : currentData.ProductionTable.NewRow();
				dataRow["TransactionDate"] = dateTimePickerDate.Value;
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["RouteID"] = routeComboBox.SelectedID;
				dataRow["Reference"] = textBoxRef.Text;
				dataRow["Reference1"] = textBoxRef1.Text;
				dataRow["BOMID"] = comboBoxBOM.SelectedID;
				dataRow["JobOrderID"] = comboBoxJobOrder.SelectedID;
				dataRow["Note"] = textBoxNote.Text;
				dataRow["Total"] = decimal.Parse(labelTotalCost.Text);
				if (dateTimePickerWorkCompDate.Value > DateTime.Today)
				{
					dataRow["WorkCompDate"] = dateTimePickerWorkCompDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerWorkCompDate.Value;
					dataRow["WorkCompDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.ProductionTable.Rows.Add(dataRow);
				}
				currentData.ProductionDetailTable.Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.ProductionDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow2["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Qty Build"].Value != null && row.Cells["Qty Build"].Value.ToString() != "")
					{
						dataRow2["QuantityBuild"] = row.Cells["Qty Build"].Value.ToString();
					}
					if (row.Cells["Cost"].Value != null && row.Cells["Cost"].Value.ToString() != "")
					{
						dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
					}
					if (row.Cells["Cost Allocation"].Value != null && row.Cells["Cost Allocation"].Value.ToString() != "")
					{
						dataRow2["CostAllocation"] = row.Cells["Cost Allocation"].Value.ToString();
					}
					else
					{
						dataRow2["CostAllocation"] = DBNull.Value;
					}
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2["Total"] = decimal.Parse(row.Cells["Total"].Value.ToString());
					dataRow2["NextRoute"] = row.Cells["Next Route"].Value.ToString();
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					if (row.Cells["Location"].Value != null && row.Cells["Location"].Value.ToString() != "")
					{
						dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					}
					dataRow2.EndEdit();
					currentData.ProductionDetailTable.Rows.Add(dataRow2);
				}
				currentData.ProductionExpenseTable.Rows.Clear();
				foreach (UltraGridRow row2 in dataGridExpense.Rows)
				{
					DataRow dataRow3 = currentData.ProductionExpenseTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow3["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow3["ExpenseID"] = row2.Cells["Expense Code"].Value.ToString();
					dataRow3["Description"] = row2.Cells["Description"].Value.ToString();
					dataRow3["Reference"] = row2.Cells["Reference"].Value.ToString();
					dataRow3["Amount"] = row2.Cells["Amount"].Value.ToString();
					dataRow3["RowIndex"] = row2.Index;
					dataRow3.EndEdit();
					currentData.ProductionExpenseTable.Rows.Add(dataRow3);
				}
				currentData.ProductionRawMaterialTable.Rows.Clear();
				foreach (UltraGridRow row3 in dataGridRawMaterials.Rows)
				{
					DataRow dataRow4 = currentData.ProductionRawMaterialTable.NewRow();
					dataRow4.BeginEdit();
					dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow4["ProductID"] = row3.Cells["Item Code"].Value.ToString();
					dataRow4["Description"] = row3.Cells["Description"].Value.ToString();
					dataRow4["Reference"] = row3.Cells["Reference"].Value.ToString();
					dataRow4["Quantity"] = row3.Cells["Qty"].Value.ToString();
					dataRow4["UnitPrice"] = row3.Cells["Cost"].Value.ToString();
					dataRow4["UnitID"] = row3.Cells["Unit"].Value.ToString();
					dataRow4["LocationID"] = row3.Cells["Location"].Value.ToString();
					dataRow4["Total"] = row3.Cells["Total"].Value.ToString();
					dataRow4["RowIndex"] = row3.Index;
					dataRow4.EndEdit();
					currentData.ProductionRawMaterialTable.Rows.Add(dataRow4);
				}
				currentData.ProductionResourceTable.Rows.Clear();
				foreach (UltraGridRow row4 in dataGridResource.Rows)
				{
					DataRow dataRow5 = currentData.ProductionResourceTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["SysDocID"] = comboBoxSysDoc.SelectedID;
					dataRow5["VoucherID"] = textBoxVoucherNumber.Text;
					dataRow5["PositionID"] = row4.Cells["PositionID"].Value.ToString();
					dataRow5["EmployeeID"] = row4.Cells["EmpNo"].Value.ToString();
					dataRow5["EmployeeName"] = row4.Cells["EmpName"].Value.ToString();
					dataRow5["Remarks"] = row4.Cells["Remarks"].Value.ToString();
					dataRow5["Hour"] = row4.Cells["Hour"].Value.ToString();
					dataRow5["RowIndex"] = row4.Index;
					dataRow5.EndEdit();
					currentData.ProductionResourceTable.Rows.Add(dataRow5);
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
				dataTable.Columns.Add("Qty Build", typeof(decimal));
				dataTable.Columns.Add("Cost Allocation", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Total", typeof(decimal));
				dataTable.Columns.Add("Next Route");
				dataGridItems.DataSource = dataTable;
				dataGridItems.LoadLayout();
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost Allocation"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Hidden = !canAccessCost;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Hidden = !canAccessCost;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost Allocation"].Header.Caption = "Cost Allocation";
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = itemLocationComboBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].Format = "0.####";
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].MinValue = -99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost Allocation"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost Allocation"].Format = "#,##0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Format = Format.GridAmountFormat;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].MinValue = new decimal(-999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].MaxValue = new decimal(999999999999L);
				dataGridItems.DisplayLayout.Bands[0].Columns["Next Route"].ValueList = routeGridComboBox;
				dataGridItems.DisplayLayout.Bands[0].Columns["Next Route"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Next Route"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Next Route"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Next Route"].MaxLength = 30;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				if (!dataGridItems.DisplayLayout.Bands[0].Summaries.Contains("TotalQty"))
				{
					dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Qty Build"], SummaryPosition.UseSummaryPositionColumn);
					dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
					dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
					dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = Format.GridAmountFormat;
				}
				if (canAccessCost)
				{
					dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Value", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Total"], SummaryPosition.UseSummaryPositionColumn);
					dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].Appearance.BackColor = Color.White;
					dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].Appearance.TextHAlign = HAlign.Right;
					dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].DisplayFormat = Format.GridAmountFormat;
				}
				ultraGroupBox1.Visible = canAccessCost;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupExpenseGrid()
		{
			try
			{
				dataGridExpense.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Expense Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Amount", typeof(decimal));
				dataGridExpense.DataSource = dataTable;
				dataGridExpense.LoadLayout();
				dataGridExpense.DisplayLayout.Bands[0].Columns["Reference"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].MaxLength = 64;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].ValueList = comboBoxGridExpenseCode;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 20;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MinValue = 0;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].MaxValue = new decimal(999999999999L);
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Format = Format.GridAmountFormat;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 15;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Width = 50;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Reference"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 50;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				if (canAccessCost)
				{
					dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("Value", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
					dataGridExpense.DisplayLayout.Bands[0].Summaries["Value"].Appearance.BackColor = Color.White;
					dataGridExpense.DisplayLayout.Bands[0].Summaries["Value"].Appearance.TextHAlign = HAlign.Right;
					dataGridExpense.DisplayLayout.Bands[0].Summaries["Value"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridExpense.DisplayLayout.Bands[0].Summaries["Value"].DisplayFormat = Format.GridAmountFormat;
				}
			}
			catch (Exception e)
			{
				dataGridExpense.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupRawMaterialsGrid()
		{
			try
			{
				dataGridRawMaterials.SetupUI();
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns.ClearUnbound();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("Item Code");
				dataTable.Columns.Add("Description");
				dataTable.Columns.Add("Reference");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Qty", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Total", typeof(decimal));
				dataGridRawMaterials.DataSource = dataTable;
				dataGridRawMaterials.LoadLayout();
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Description"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Unit"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = rawMaterialsProductComboBox;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].MaxLength = 20;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].Format = "0.####";
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].MinValue = -99999999m;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"].MaxValue = 99999999m;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Unit"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].ValueList = rawMaterialsLocationComboBox;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].MinValue = 0;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].MaxValue = new decimal(999999999999L);
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].Format = Format.GridAmountFormat;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].MinValue = 0;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].MaxValue = new decimal(999999999999L);
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].Format = Format.GridAmountFormat;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].Hidden = !canAccessCost;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].Hidden = !canAccessCost;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Reference"].MaxLength = 255;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].Width = 55;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Reference"].Width = 90;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].Width = 50;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].Width = 50;
				if (!dataGridRawMaterials.DisplayLayout.Bands[0].Summaries.Contains("TotalQty"))
				{
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Qty"], SummaryPosition.UseSummaryPositionColumn);
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = Format.GridAmountFormat;
				}
				if (canAccessCost)
				{
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries.Add("Value", SummaryType.Sum, dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"], SummaryPosition.UseSummaryPositionColumn);
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["Value"].Appearance.BackColor = Color.White;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["Value"].Appearance.TextHAlign = HAlign.Right;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["Value"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
					dataGridRawMaterials.DisplayLayout.Bands[0].Summaries["Value"].DisplayFormat = Format.GridAmountFormat;
				}
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].CellActivation = Activation.NoEdit;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.NoEdit;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridRawMaterials.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
			}
			catch (Exception e)
			{
				dataGridRawMaterials.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void SetupResourceGrid()
		{
			dataGridResource.SetupUI();
			dataGridResource.DisplayLayout.Bands[0].Columns.ClearUnbound();
			dataGridResource.DisplayLayout.Bands[0].Summaries.Clear();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("PositionID");
			dataTable.Columns.Add("EmpNo");
			dataTable.Columns.Add("EmpName");
			dataTable.Columns.Add("Hour", typeof(decimal));
			dataTable.Columns.Add("Remarks");
			dataGridResource.DataSource = dataTable;
			dataGridResource.LoadLayout();
			dataGridResource.DisplayLayout.Bands[0].Columns["PositionID"].CellActivation = Activation.AllowEdit;
			dataGridResource.DisplayLayout.Bands[0].Columns["PositionID"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridResource.DisplayLayout.Bands[0].Columns["PositionID"].MaxLength = 255;
			dataGridResource.DisplayLayout.Bands[0].Columns["PositionID"].Header.Caption = "Position";
			dataGridResource.DisplayLayout.Bands[0].Columns["PositionID"].ValueList = comboBoxGridPosition;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].CharacterCasing = CharacterCasing.Upper;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].MaxLength = 15;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].ValueList = comboBoxGridEmployee;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpNo"].Header.Caption = "Employee No";
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].CellActivation = Activation.Disabled;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].CellAppearance.ForeColorDisabled = Color.Black;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].MaxLength = 255;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].Header.Caption = "Employee Name";
			dataGridResource.DisplayLayout.Bands[0].Columns["Hour"].MaxLength = 20;
			dataGridResource.DisplayLayout.Bands[0].Columns["Hour"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridResource.DisplayLayout.Bands[0].Columns["Hour"].MinValue = 0;
			dataGridResource.DisplayLayout.Bands[0].Columns["Hour"].MaxValue = 99999999m;
			dataGridResource.DisplayLayout.Bands[0].Columns["Remarks"].Width = checked(20 * dataGridResource.Width) / 100;
			dataGridResource.DisplayLayout.Bands[0].Columns["EmpName"].MaxLength = 255;
			dataGridResource.DisplayLayout.Bands[0].Columns["Remarks"].MaxLength = 255;
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
			dataGridItems.Focus();
		}

		public void LoadData(string voucherID)
		{
			try
			{
				if (!base.IsDisposed && !(voucherID.Trim() == "") && CanClose())
				{
					currentData = Factory.ProductionSystem.GetProductionByID(SystemDocID, voucherID);
					if (currentData != null)
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
						formManager.IsForcedDirty = true;
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
					DataRow dataRow = currentData.Tables["Mfg_Production"].Rows[0];
					comboBoxSysDoc.SelectedID = dataRow["SysDocID"].ToString();
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef.Text = dataRow["Reference"].ToString();
					textBoxRef1.Text = dataRow["Reference1"].ToString();
					comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
					routeComboBox.SelectedID = dataRow["RouteID"].ToString();
					textBoxNote.Text = dataRow["Note"].ToString();
					comboBoxBOM.SelectedID = dataRow["BOMID"].ToString();
					comboBoxJobOrder.SelectedID = dataRow["JobOrderID"].ToString();
					if (dataRow["WorkCompDate"].ToString() != "")
					{
						dateTimePickerWorkCompDate.Value = DateTime.Parse(dataRow["WorkCompDate"].ToString());
					}
					labelTotalCost.Text = decimal.Parse(dataRow["Total"].ToString()).ToString(Format.TotalAmountFormat);
					DataTable dataTable = dataGridRawMaterials.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in currentData.Tables["Mfg_Production_Raw_Material"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3.BeginEdit();
						dataRow3["Item Code"] = row["ProductID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Reference"] = row["Reference"];
						if (row["UnitQuantity"] != DBNull.Value)
						{
							dataRow3["Qty"] = row["UnitQuantity"];
						}
						else
						{
							dataRow3["Qty"] = row["Quantity"];
						}
						dataRow3["Cost"] = row["UnitPrice"];
						dataRow3["Unit"] = row["UnitID"];
						dataRow3["Location"] = row["LocationID"];
						dataRow3["Total"] = row["Total"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					DataTable dataTable2 = dataGridItems.DataSource as DataTable;
					dataTable2.Rows.Clear();
					if (currentData.Tables.Contains("Mfg_Production_Detail") && currentData.ProductionDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row2 in currentData.Tables["Mfg_Production_Detail"].Rows)
						{
							DataRow dataRow5 = dataTable2.NewRow();
							dataRow5["Item Code"] = row2["ProductID"];
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								dataRow5["Qty Build"] = row2["UnitQuantity"];
							}
							else
							{
								dataRow5["Qty Build"] = row2["QuantityBuild"];
							}
							dataRow5["Cost"] = row2["Cost"];
							dataRow5["Cost Allocation"] = row2["CostAllocation"];
							dataRow5["Description"] = row2["Description"];
							dataRow5["Total"] = row2["Total"];
							dataRow5["Unit"] = row2["UnitID"];
							dataRow5["Location"] = row2["LocationID"];
							dataRow5["Next Route"] = row2["NextRoute"];
							dataRow5.EndEdit();
							dataTable2.Rows.Add(dataRow5);
						}
						dataTable2.AcceptChanges();
						dataGridItems.EndUpdate();
						DataTable dataTable3 = dataGridExpense.DataSource as DataTable;
						dataTable3.Rows.Clear();
						foreach (DataRow row3 in currentData.Tables["Mfg_Production_Expense"].Rows)
						{
							DataRow dataRow7 = dataTable3.NewRow();
							dataRow7["Expense Code"] = row3["ExpenseID"];
							dataRow7["Description"] = row3["Description"];
							dataRow7["Reference"] = row3["Reference"];
							dataRow7["Amount"] = row3["Amount"];
							dataRow7.EndEdit();
							dataTable3.Rows.Add(dataRow7);
						}
						DataTable dataTable4 = dataGridResource.DataSource as DataTable;
						dataTable4.Rows.Clear();
						foreach (DataRow row4 in currentData.Tables["Mfg_Production_Resource"].Rows)
						{
							DataRow dataRow9 = dataTable4.NewRow();
							dataRow9["PositionID"] = row4["PositionID"];
							dataRow9["EmpNo"] = row4["EmployeeID"];
							dataRow9["EmpName"] = row4["EmployeeName"];
							dataRow9["Hour"] = row4["Hour"];
							dataRow9["Remarks"] = row4["Remarks"];
							dataRow9.EndEdit();
							dataTable4.Rows.Add(dataRow9);
						}
						foreach (UltraGridRow row5 in dataGridItems.Rows)
						{
							string productID = row5.Cells["Item Code"].Value.ToString();
							row5.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
						}
						foreach (UltraGridRow row6 in dataGridRawMaterials.Rows)
						{
							string productID2 = row6.Cells["Item Code"].Value.ToString();
							row6.Cells["Unit"].ValueList = rawMaterialsProductComboBox.GetProductUnitsValueList(productID2);
						}
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
				bool flag = Factory.ProductionSystem.CreateProduction(currentData, !isNewRecord);
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
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Mfg_Production", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (isNewRecord && dateTimePickerDate.Value > DateTime.Today && !Security.IsAllowedSecurityRole(GeneralSecurityRoles.FutureDatedTransaction))
				{
					ErrorHelper.WarningMessage("You are not allowed to enter future-dated transactions.");
					return false;
				}
				if (DateTime.Parse(dateTimePickerWorkCompDate.Value.ToShortDateString()) < DateTime.Parse(dateTimePickerDate.Value.ToShortDateString()))
				{
					ErrorHelper.WarningMessage("Work completion date should not be back date.");
					return false;
				}
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxLocation.SelectedID == "")
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
					ErrorHelper.InformationMessage("There should be at least one adjustment row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Mfg_Production", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (row.GetCellValue("Total") != null && row.GetCellValue("Total").ToString() != "")
				{
					result += decimal.Parse(row.Cells["Total"].Value.ToString());
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
				textBoxRef.Clear();
				textBoxRef1.Clear();
				textBoxVoucherNumber.Text = GetNextVoucherNumber();
				labelTotalCost.Text = 0.ToString(Format.TotalAmountFormat);
				comboBoxGridItem.Clear();
				itemLocationComboBox.Clear();
				rawMaterialsLocationComboBox.Clear();
				routeComboBox.Clear();
				comboBoxLocation.Clear();
				comboBoxBOM.Clear();
				comboBoxJobOrder.Clear();
				dateTimePickerDate.Value = DateTime.Now;
				dateTimePickerWorkCompDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				(dataGridExpense.DataSource as DataTable).Rows.Clear();
				(dataGridRawMaterials.DataSource as DataTable).Rows.Clear();
				(dataGridResource.DataSource as DataTable).Rows.Clear();
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
				return Factory.ProductionSystem.DeleteProduction(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Mfg_Production", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Mfg_Production", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Mfg_Production", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Mfg_Production", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Mfg_Production", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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

		private void BuildAssemblyForm_Load(object sender, EventArgs e)
		{
			try
			{
				dataGridItems.SetupUI();
				SetupGrid();
				dataGridExpense.SetupUI();
				SetupExpenseGrid();
				SetupRawMaterialsGrid();
				SetupResourceGrid();
				ultraTabControl1.Tabs[2].VisibleIndex = 3;
				comboBoxSysDoc.FilterByType(SysDocTypes.Production);
				dateTimePickerDate.Value = DateTime.Now;
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
				dataGridExpense.LoadLayoutFailed = true;
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
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
				return;
			}
			canAccessCost = true;
			dataGridItems.DataSource = null;
			SetupGrid();
			SetupRawMaterialsGrid();
		}

		private void dataGridItems_AfterCellActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				_ = dataGridItems.ActiveCell;
			}
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
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.Production);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.Production);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 264.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.Production);
					currentData = (dataSet as ProductionData);
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
				DataSet productionToPrint = Factory.ProductionSystem.GetProductionToPrint(selectedID, text);
				if (productionToPrint == null || productionToPrint.Tables.Count == 0)
				{
					ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
				}
				else
				{
					PrintHelper.PrintDocument(productionToPrint, selectedID, "Production", SysDocTypes.Production, isPrint, showPrintDialog);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
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
			new FormHelper().EditRoute(routeComboBox.SelectedID);
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

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductionListFormObj);
		}

		private void ultraFormattedLinkLabel6_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditBOM(comboBoxBOM.SelectedID);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
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

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void ultraFormattedLinkLabel4_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
		}

		private void loadFromMaterialToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet jobMaterialEstimationList = Factory.JobMaterialEstimateSystem.GetJobMaterialEstimationList();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = jobMaterialEstimationList;
			selectDocumentDialog.Text = "Select Material Estimation";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				ClearForm();
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string sysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
					string voucherID = selectedRow.Cells["Number"].Value.ToString();
					string jobID = selectedRow.Cells["JobID"].Value.ToString();
					string costCategoryID = selectedRow.Cells["CostCategoryID"].Value.ToString();
					JobMaterialEstimateData jobMaterialEstimateByJobIDCostCategoryID = Factory.JobMaterialEstimateSystem.GetJobMaterialEstimateByJobIDCostCategoryID(sysDocID, voucherID, jobID, costCategoryID);
					DataRow dataRow = jobMaterialEstimateByJobIDCostCategoryID.JobMaterialEstimateTable.Rows[0];
					textBoxRef.Text = dataRow["VoucherID"].ToString();
					comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					foreach (DataRow row in jobMaterialEstimateByJobIDCostCategoryID.Tables["Job_Material_Estimate_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Item Code"] = row["ProductID"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Unit"] = row["UnitID"];
						dataRow3["Qty"] = row["Quantity"];
						if (comboBoxLocation.SelectedID != "")
						{
							DateTime date = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 11, 59, 59);
							DataSet productQuantityAndCostAsOfDate = Factory.ProductSystem.GetProductQuantityAndCostAsOfDate(dataRow3["Item Code"].ToString(), comboBoxLocation.SelectedID, date);
							DataRow dataRow4 = null;
							if (productQuantityAndCostAsOfDate != null && productQuantityAndCostAsOfDate.Tables.Count > 0 && productQuantityAndCostAsOfDate.Tables[0].Rows.Count > 0)
							{
								dataRow4 = productQuantityAndCostAsOfDate.Tables[0].Rows[0];
							}
							if (dataRow4 != null)
							{
								dataRow3["Onhand"] = dataRow4["Quantity"].ToString();
								dataRow3["Cost"] = dataRow4["AverageCost"].ToString();
							}
							else
							{
								dataRow3["Onhand"] = 0;
								dataRow3["Cost"] = 0;
							}
						}
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataGridItems.AllowAddNew = true;
				}
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (dataGridRawMaterials.ActiveRow != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			checked
			{
				if (dataGridRawMaterials.ActiveRow != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString() != "")
				{
					string productID = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
					productPhotoViewer.ShowImage(productID, dataGridRawMaterials.Left + 20, dataGridRawMaterials.Top + 20);
				}
			}
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			if (dataGridRawMaterials.ActiveRow != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value != null && dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridRawMaterials.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void loadFromWOToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsNewRecord)
			{
				ErrorHelper.InformationMessage("Please start a new transaction first.");
				return;
			}
			DataSet workOrderAll = Factory.WorkOrderSystem.GetWorkOrderAll();
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = workOrderAll;
			selectDocumentDialog.Text = "Select Work Order";
			if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
			{
				_ = (selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString() != "");
				ClearForm();
				string sysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				string voucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				SourceSysDocID = selectDocumentDialog.SelectedRow.Cells["Doc ID"].Value.ToString();
				SourceVoucherID = selectDocumentDialog.SelectedRow.Cells["Number"].Value.ToString();
				WorkOrderData workOrderByID = Factory.WorkOrderSystem.GetWorkOrderByID(sysDocID, voucherID);
				DataRow dataRow = workOrderByID.WorkOrderTable.Rows[0];
				textBoxRef.Text = dataRow["Reference"].ToString();
				comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
				textBoxNote.Text = dataRow["Description"].ToString();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				if (workOrderByID.Tables.Contains("Mfg_Work_Order_Detail") && workOrderByID.Tables["Mfg_Work_Order_Detail"].Rows.Count != 0)
				{
					dataGridItems.BeginUpdate();
					foreach (DataRow row in workOrderByID.Tables["Mfg_Work_Order_Detail"].Rows)
					{
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["Item Code"] = row["ProductID"];
						dataRow3["Qty"] = row["Quantity"];
						dataRow3["Quantity"] = row["Quantity"];
						dataRow3["Description"] = row["Description"];
						dataRow3["Unit"] = row["UnitID"];
						dataRow3.EndEdit();
						dataTable.Rows.Add(dataRow3);
					}
					dataTable.AcceptChanges();
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						if (decimal.Parse(row2.Cells["Qty"].Value.ToString()) < 0m)
						{
							row2.Cells["Cost"].Activation = Activation.Disabled;
						}
						else
						{
							row2.Cells["Cost"].Activation = Activation.AllowEdit;
						}
					}
					DataTable dataTable2 = dataGridExpense.DataSource as DataTable;
					dataTable2.Rows.Clear();
					foreach (DataRow row3 in workOrderByID.Tables["Mfg_Work_Order_Expense"].Rows)
					{
						DataRow dataRow5 = dataTable2.NewRow();
						dataRow5["Expense Code"] = row3["ExpenseID"];
						dataRow5["Description"] = row3["Description"];
						dataRow5["Reference"] = row3["Reference"];
						dataRow5["Amount"] = row3["Amount"];
						dataRow5.EndEdit();
						dataTable2.Rows.Add(dataRow5);
					}
					dataGridItems.EndUpdate();
					CalculateTotalCost();
				}
			}
		}

		private void SetTotalCost()
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal.TryParse(labelTotalCost.Text, out result);
			IEnumerable<UltraGridRow> enumerable = dataGridItems.Rows.Where((UltraGridRow x) => x.Cells["Cost Allocation"].Value.ToString() != "0" && x.Cells["Qty Build"].Value.ToString() != "0" && x.Cells["Qty Build"].Value.ToString() != "");
			decimal d = enumerable.Sum((UltraGridRow x) => decimal.Parse(x.Cells["Qty Build"].Value.ToString()));
			foreach (UltraGridRow item in enumerable)
			{
				decimal.TryParse(item.Cells["Qty Build"].Value.ToString(), out result2);
				decimal num = result2 / d * result;
				item.Cells["Total"].Value = num;
				item.Cells["Cost"].Value = num / result2;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.ProductionDetailsForm));
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxGridProductUnit1 = new Micromind.DataControls.ProductUnitComboBox();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			label5 = new System.Windows.Forms.Label();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridRawMaterials = new Micromind.DataControls.DataEntryGrid();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridResource = new Micromind.DataControls.DataEntryGrid();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
			label323 = new System.Windows.Forms.Label();
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
			saveADraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadDraftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadFromWOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			loadFromMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
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
			textBoxRef = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel2 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelTotalCost = new System.Windows.Forms.Label();
			ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
			panelDetails = new System.Windows.Forms.Panel();
			routeGridComboBox = new Micromind.DataControls.RouteComboBox();
			label3 = new System.Windows.Forms.Label();
			textBoxRef1 = new System.Windows.Forms.TextBox();
			rawMaterialsLocationComboBox = new Micromind.DataControls.LocationComboBox();
			textBoxBOMName = new System.Windows.Forms.TextBox();
			textBoxJobOrderName = new System.Windows.Forms.TextBox();
			routeComboBox = new Micromind.DataControls.RouteComboBox();
			textBoxRouteName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel4 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxJobOrder = new Micromind.DataControls.customersFlatComboBox();
			ultraFormattedLinkLabel6 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			label8 = new System.Windows.Forms.Label();
			dateTimePickerWorkCompDate = new System.Windows.Forms.DateTimePicker();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			comboBoxBOM = new Micromind.DataControls.BOMComboBox();
			ultraFormattedLinkLabelRoute = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			productUnitComboBox = new Micromind.DataControls.ProductUnitComboBox();
			comboBoxGridEmployee = new Micromind.DataControls.EmployeeComboBox();
			comboBoxGridPosition = new Micromind.DataControls.PositionComboBox();
			rawMaterialsProductComboBox = new Micromind.DataControls.ProductComboBox();
			contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(components);
			printListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			ultraGridPrintDocument1 = new Infragistics.Win.UltraWinGrid.UltraGridPrintDocument(components);
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(components);
			availableQuantityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			salesStatisticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			itemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			textBoxNote = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			itemLocationComboBox = new Micromind.DataControls.LocationComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			bomComboBox2 = new Micromind.DataControls.BOMComboBox();
			contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(components);
			toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
			tabPageItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit1).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			ultraTabPageControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridRawMaterials).BeginInit();
			ultraTabPageControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridResource).BeginInit();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)routeGridComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)rawMaterialsLocationComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)routeComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJobOrder).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)productUnitComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPosition).BeginInit();
			((System.ComponentModel.ISupportInitialize)rawMaterialsProductComboBox).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			contextMenuStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)itemLocationComboBox).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).BeginInit();
			contextMenuStrip3.SuspendLayout();
			SuspendLayout();
			tabPageItems.Controls.Add(comboBoxGridProductUnit1);
			tabPageItems.Controls.Add(productPhotoViewer);
			tabPageItems.Controls.Add(comboBoxGridLocation);
			tabPageItems.Controls.Add(comboBoxGridProductUnit);
			tabPageItems.Controls.Add(comboBoxGridItem);
			tabPageItems.Controls.Add(label5);
			tabPageItems.Controls.Add(dataGridItems);
			tabPageItems.Location = new System.Drawing.Point(1, 23);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(708, 243);
			comboBoxGridProductUnit1.Assigned = false;
			comboBoxGridProductUnit1.CalcManager = ultraCalcManager1;
			comboBoxGridProductUnit1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit1.CustomReportFieldName = "";
			comboBoxGridProductUnit1.CustomReportKey = "";
			comboBoxGridProductUnit1.CustomReportValueType = 1;
			comboBoxGridProductUnit1.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit1.DisplayLayout.Appearance = appearance;
			comboBoxGridProductUnit1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit1.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridProductUnit1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit1.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridProductUnit1.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit1.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit1.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit1.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridProductUnit1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit1.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit1.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridProductUnit1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit1.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit1.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridProductUnit1.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridProductUnit1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit1.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridProductUnit1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit1.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxGridProductUnit1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit1.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit1.Editable = true;
			comboBoxGridProductUnit1.FilterString = "";
			comboBoxGridProductUnit1.IsDataLoaded = false;
			comboBoxGridProductUnit1.Location = new System.Drawing.Point(465, 111);
			comboBoxGridProductUnit1.MaxDropDownItems = 12;
			comboBoxGridProductUnit1.Name = "comboBoxGridProductUnit1";
			comboBoxGridProductUnit1.Size = new System.Drawing.Size(101, 20);
			comboBoxGridProductUnit1.TabIndex = 139;
			comboBoxGridProductUnit1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit1.Visible = false;
			ultraCalcManager1.ContainingControl = this;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(61, 33);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 138;
			productPhotoViewer.Visible = false;
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance13;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance20;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance23;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(321, 111);
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
			comboBoxGridLocation.Size = new System.Drawing.Size(66, 20);
			comboBoxGridLocation.TabIndex = 137;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
			comboBoxGridProductUnit.AlwaysInEditMode = true;
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CalcManager = ultraCalcManager1;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance25;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance32;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance34;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance35;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			comboBoxGridProductUnit.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridProductUnit.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridProductUnit.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridProductUnit.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridProductUnit.Editable = true;
			comboBoxGridProductUnit.FilterString = "";
			comboBoxGridProductUnit.IsDataLoaded = false;
			comboBoxGridProductUnit.Location = new System.Drawing.Point(546, -27);
			comboBoxGridProductUnit.MaxDropDownItems = 12;
			comboBoxGridProductUnit.Name = "comboBoxGridProductUnit";
			comboBoxGridProductUnit.Size = new System.Drawing.Size(101, 20);
			comboBoxGridProductUnit.TabIndex = 120;
			comboBoxGridProductUnit.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridProductUnit.Visible = false;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
			comboBoxGridItem.Assigned = false;
			comboBoxGridItem.CalcManager = ultraCalcManager1;
			comboBoxGridItem.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridItem.CustomReportFieldName = "";
			comboBoxGridItem.CustomReportKey = "";
			comboBoxGridItem.CustomReportValueType = 1;
			comboBoxGridItem.DescriptionTextBox = null;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridItem.DisplayLayout.Appearance = appearance37;
			comboBoxGridItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			comboBoxGridItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridItem.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			comboBoxGridItem.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridItem.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridItem.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridItem.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridItem.DisplayLayout.Override.CellAppearance = appearance44;
			comboBoxGridItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridItem.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridItem.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			comboBoxGridItem.DisplayLayout.Override.HeaderAppearance = appearance46;
			comboBoxGridItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridItem.DisplayLayout.Override.RowAppearance = appearance47;
			comboBoxGridItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridItem.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
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
			comboBoxGridItem.Location = new System.Drawing.Point(546, 49);
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
			label5.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label5.BackColor = System.Drawing.Color.White;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.ForeColor = System.Drawing.Color.DarkRed;
			label5.Location = new System.Drawing.Point(5, 139);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(700, 59);
			label5.TabIndex = 117;
			label5.Text = "VOIDED";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			label5.Visible = false;
			dataGridItems.AllowAddNew = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance49;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(2, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(704, 243);
			dataGridItems.TabIndex = 136;
			dataGridItems.Text = "dataEntryGrid1";
			ultraTabPageControl2.Controls.Add(dataGridRawMaterials);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(708, 243);
			dataGridRawMaterials.AllowAddNew = true;
			dataGridRawMaterials.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridRawMaterials.CalcManager = ultraCalcManager1;
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridRawMaterials.DisplayLayout.Appearance = appearance61;
			dataGridRawMaterials.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridRawMaterials.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			dataGridRawMaterials.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridRawMaterials.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			dataGridRawMaterials.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridRawMaterials.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			dataGridRawMaterials.DisplayLayout.MaxColScrollRegions = 1;
			dataGridRawMaterials.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridRawMaterials.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridRawMaterials.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			dataGridRawMaterials.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridRawMaterials.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridRawMaterials.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			dataGridRawMaterials.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridRawMaterials.DisplayLayout.Override.CellAppearance = appearance68;
			dataGridRawMaterials.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridRawMaterials.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			dataGridRawMaterials.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			dataGridRawMaterials.DisplayLayout.Override.HeaderAppearance = appearance70;
			dataGridRawMaterials.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridRawMaterials.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			dataGridRawMaterials.DisplayLayout.Override.RowAppearance = appearance71;
			dataGridRawMaterials.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridRawMaterials.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			dataGridRawMaterials.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridRawMaterials.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridRawMaterials.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridRawMaterials.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridRawMaterials.ExitEditModeOnLeave = false;
			dataGridRawMaterials.IncludeLotItems = false;
			dataGridRawMaterials.LoadLayoutFailed = false;
			dataGridRawMaterials.Location = new System.Drawing.Point(2, 3);
			dataGridRawMaterials.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridRawMaterials.Name = "dataGridRawMaterials";
			dataGridRawMaterials.ShowClearMenu = true;
			dataGridRawMaterials.ShowDeleteMenu = true;
			dataGridRawMaterials.ShowInsertMenu = true;
			dataGridRawMaterials.ShowMoveRowsMenu = true;
			dataGridRawMaterials.Size = new System.Drawing.Size(704, 243);
			dataGridRawMaterials.TabIndex = 137;
			dataGridRawMaterials.Text = "dataEntryGrid1";
			ultraTabPageControl1.Controls.Add(dataGridResource);
			ultraTabPageControl1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(708, 243);
			dataGridResource.AllowAddNew = true;
			dataGridResource.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridResource.CalcManager = ultraCalcManager1;
			appearance73.BackColor = System.Drawing.SystemColors.Window;
			appearance73.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridResource.DisplayLayout.Appearance = appearance73;
			dataGridResource.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridResource.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance74.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance74.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance74.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance74.BorderColor = System.Drawing.SystemColors.Window;
			dataGridResource.DisplayLayout.GroupByBox.Appearance = appearance74;
			appearance75.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridResource.DisplayLayout.GroupByBox.BandLabelAppearance = appearance75;
			dataGridResource.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance76.BackColor2 = System.Drawing.SystemColors.Control;
			appearance76.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance76.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridResource.DisplayLayout.GroupByBox.PromptAppearance = appearance76;
			dataGridResource.DisplayLayout.MaxColScrollRegions = 1;
			dataGridResource.DisplayLayout.MaxRowScrollRegions = 1;
			appearance77.BackColor = System.Drawing.SystemColors.Window;
			appearance77.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridResource.DisplayLayout.Override.ActiveCellAppearance = appearance77;
			appearance78.BackColor = System.Drawing.SystemColors.Highlight;
			appearance78.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridResource.DisplayLayout.Override.ActiveRowAppearance = appearance78;
			dataGridResource.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridResource.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridResource.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance79.BackColor = System.Drawing.SystemColors.Window;
			dataGridResource.DisplayLayout.Override.CardAreaAppearance = appearance79;
			appearance80.BorderColor = System.Drawing.Color.Silver;
			appearance80.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridResource.DisplayLayout.Override.CellAppearance = appearance80;
			dataGridResource.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridResource.DisplayLayout.Override.CellPadding = 0;
			appearance81.BackColor = System.Drawing.SystemColors.Control;
			appearance81.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance81.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance81.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance81.BorderColor = System.Drawing.SystemColors.Window;
			dataGridResource.DisplayLayout.Override.GroupByRowAppearance = appearance81;
			appearance82.TextHAlignAsString = "Left";
			dataGridResource.DisplayLayout.Override.HeaderAppearance = appearance82;
			dataGridResource.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridResource.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance83.BackColor = System.Drawing.SystemColors.Window;
			appearance83.BorderColor = System.Drawing.Color.Silver;
			dataGridResource.DisplayLayout.Override.RowAppearance = appearance83;
			dataGridResource.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridResource.DisplayLayout.Override.TemplateAddRowAppearance = appearance84;
			dataGridResource.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridResource.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridResource.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridResource.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridResource.ExitEditModeOnLeave = false;
			dataGridResource.IncludeLotItems = false;
			dataGridResource.LoadLayoutFailed = false;
			dataGridResource.Location = new System.Drawing.Point(2, 3);
			dataGridResource.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridResource.Name = "dataGridResource";
			dataGridResource.ShowClearMenu = true;
			dataGridResource.ShowDeleteMenu = true;
			dataGridResource.ShowInsertMenu = true;
			dataGridResource.ShowMoveRowsMenu = true;
			dataGridResource.Size = new System.Drawing.Size(704, 243);
			dataGridResource.TabIndex = 137;
			dataGridResource.Text = "dataEntryGrid1";
			tabPageExpense.Controls.Add(dataGridExpense);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(708, 243);
			dataGridExpense.AllowAddNew = true;
			dataGridExpense.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridExpense.CalcManager = ultraCalcManager1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance85;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance86.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance86.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance86.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance86.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance86;
			appearance87.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance87;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance88.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance88.BackColor2 = System.Drawing.SystemColors.Control;
			appearance88.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance88.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance88;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance89.BackColor = System.Drawing.SystemColors.Window;
			appearance89.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance89;
			appearance90.BackColor = System.Drawing.SystemColors.Highlight;
			appearance90.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance90;
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance91;
			appearance92.BorderColor = System.Drawing.Color.Silver;
			appearance92.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance92;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance93.BackColor = System.Drawing.SystemColors.Control;
			appearance93.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance93.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance93.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance93.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance93;
			appearance94.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance94;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance95.BackColor = System.Drawing.SystemColors.Window;
			appearance95.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance95;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance96;
			dataGridExpense.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridExpense.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridExpense.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridExpense.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridExpense.ExitEditModeOnLeave = false;
			dataGridExpense.IncludeLotItems = false;
			dataGridExpense.LoadLayoutFailed = false;
			dataGridExpense.Location = new System.Drawing.Point(2, 3);
			dataGridExpense.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridExpense.Name = "dataGridExpense";
			dataGridExpense.ShowClearMenu = true;
			dataGridExpense.ShowDeleteMenu = true;
			dataGridExpense.ShowInsertMenu = true;
			dataGridExpense.ShowMoveRowsMenu = true;
			dataGridExpense.Size = new System.Drawing.Size(704, 243);
			dataGridExpense.TabIndex = 135;
			dataGridExpense.Text = "dataEntryGrid1";
			label323.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			label323.Location = new System.Drawing.Point(4, 2);
			label323.Name = "label323";
			label323.Size = new System.Drawing.Size(563, 17);
			label323.TabIndex = 138;
			label323.Text = "Total Cost:";
			label323.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripSeparator5,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButton1,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(739, 31);
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
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			toolStripButtonDistribution.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonDistribution.Image = Micromind.ClientUI.Properties.Resources.jvdistribution;
			toolStripButtonDistribution.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonDistribution.Name = "toolStripButtonDistribution";
			toolStripButtonDistribution.Size = new System.Drawing.Size(28, 28);
			toolStripButtonDistribution.Text = "Journal Distribution Summary";
			toolStripButtonDistribution.Click += new System.EventHandler(toolStripButtonDistribution_Click);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4]
			{
				saveADraftToolStripMenuItem,
				loadDraftToolStripMenuItem,
				loadFromWOToolStripMenuItem,
				loadFromMaterialToolStripMenuItem
			});
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(60, 28);
			toolStripButton1.Text = "Actions";
			saveADraftToolStripMenuItem.Name = "saveADraftToolStripMenuItem";
			saveADraftToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
			saveADraftToolStripMenuItem.Text = "Save as Draft";
			loadDraftToolStripMenuItem.Name = "loadDraftToolStripMenuItem";
			loadDraftToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
			loadDraftToolStripMenuItem.Text = "Load Draft...";
			loadFromWOToolStripMenuItem.Name = "loadFromWOToolStripMenuItem";
			loadFromWOToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
			loadFromWOToolStripMenuItem.Text = "Load From Work Order";
			loadFromWOToolStripMenuItem.Click += new System.EventHandler(loadFromWOToolStripMenuItem_Click);
			loadFromMaterialToolStripMenuItem.Name = "loadFromMaterialToolStripMenuItem";
			loadFromMaterialToolStripMenuItem.Size = new System.Drawing.Size(234, 22);
			loadFromMaterialToolStripMenuItem.Text = "Load from Material Estimation";
			loadFromMaterialToolStripMenuItem.Click += new System.EventHandler(loadFromMaterialToolStripMenuItem_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Controls.Add(buttonVoid);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 548);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(739, 40);
			panelButtons.TabIndex = 3;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 2;
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
			linePanelDown.Size = new System.Drawing.Size(739, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(628, 8);
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
			dateTimePickerDate.Location = new System.Drawing.Point(493, 5);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(330, 5);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(119, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(225, 53);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef.Location = new System.Drawing.Point(285, 49);
			textBoxRef.MaxLength = 64;
			textBoxRef.Name = "textBoxRef";
			textBoxRef.Size = new System.Drawing.Size(142, 20);
			textBoxRef.TabIndex = 6;
			appearance97.FontData.BoldAsString = "True";
			appearance97.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance97;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(223, 9);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance98.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance98;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			ultraGroupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance99.BackColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance99.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			ultraGroupBox1.Appearance = appearance99;
			ultraGroupBox1.BorderStyle = Infragistics.Win.Misc.GroupBoxBorderStyle.RectangularSolid;
			ultraGroupBox1.Controls.Add(labelTotalCost);
			ultraGroupBox1.Controls.Add(label323);
			ultraGroupBox1.Controls.Add(ultraLabel1);
			ultraGroupBox1.HeaderBorderStyle = Infragistics.Win.UIElementBorderStyle.None;
			ultraGroupBox1.Location = new System.Drawing.Point(11, 439);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(711, 21);
			ultraGroupBox1.TabIndex = 17;
			ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.VisualStudio2005;
			labelTotalCost.Dock = System.Windows.Forms.DockStyle.Right;
			labelTotalCost.Location = new System.Drawing.Point(565, 2);
			labelTotalCost.Name = "labelTotalCost";
			labelTotalCost.Size = new System.Drawing.Size(144, 17);
			labelTotalCost.TabIndex = 139;
			labelTotalCost.Text = "0.00";
			labelTotalCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			ultraLabel1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance100.BorderColor = System.Drawing.Color.FromArgb(122, 150, 223);
			appearance100.FontData.BoldAsString = "True";
			appearance100.FontData.Name = "Tahoma";
			appearance100.ForeColor = System.Drawing.Color.FromArgb(194, 216, 252);
			appearance100.TextHAlignAsString = "Right";
			appearance100.TextVAlignAsString = "Middle";
			ultraLabel1.Appearance = appearance100;
			ultraLabel1.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
			ultraLabel1.Location = new System.Drawing.Point(2, 2);
			ultraLabel1.Name = "ultraLabel1";
			ultraLabel1.Size = new System.Drawing.Size(566, 16);
			ultraLabel1.TabIndex = 112;
			ultraLabel1.Text = "Balance:";
			panelDetails.Controls.Add(routeGridComboBox);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(rawMaterialsLocationComboBox);
			panelDetails.Controls.Add(textBoxBOMName);
			panelDetails.Controls.Add(textBoxJobOrderName);
			panelDetails.Controls.Add(routeComboBox);
			panelDetails.Controls.Add(ultraFormattedLinkLabel4);
			panelDetails.Controls.Add(comboBoxJobOrder);
			panelDetails.Controls.Add(ultraFormattedLinkLabel6);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(label8);
			panelDetails.Controls.Add(dateTimePickerWorkCompDate);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(comboBoxBOM);
			panelDetails.Controls.Add(ultraFormattedLinkLabelRoute);
			panelDetails.Controls.Add(comboBoxLocation);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(textBoxRouteName);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Controls.Add(productUnitComboBox);
			panelDetails.Controls.Add(comboBoxGridEmployee);
			panelDetails.Controls.Add(comboBoxGridPosition);
			panelDetails.Controls.Add(rawMaterialsProductComboBox);
			panelDetails.Location = new System.Drawing.Point(11, 32);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 130);
			panelDetails.TabIndex = 0;
			routeGridComboBox.Assigned = false;
			routeGridComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			routeGridComboBox.CalcManager = ultraCalcManager1;
			routeGridComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			routeGridComboBox.CustomReportFieldName = "";
			routeGridComboBox.CustomReportKey = "";
			routeGridComboBox.CustomReportValueType = 1;
			routeGridComboBox.DescriptionTextBox = null;
			appearance101.BackColor = System.Drawing.SystemColors.Window;
			appearance101.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			routeGridComboBox.DisplayLayout.Appearance = appearance101;
			routeGridComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			routeGridComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance102.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance102.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance102.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance102.BorderColor = System.Drawing.SystemColors.Window;
			routeGridComboBox.DisplayLayout.GroupByBox.Appearance = appearance102;
			appearance103.ForeColor = System.Drawing.SystemColors.GrayText;
			routeGridComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance103;
			routeGridComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance104.BackColor2 = System.Drawing.SystemColors.Control;
			appearance104.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance104.ForeColor = System.Drawing.SystemColors.GrayText;
			routeGridComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance104;
			routeGridComboBox.DisplayLayout.MaxColScrollRegions = 1;
			routeGridComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.ForeColor = System.Drawing.SystemColors.ControlText;
			routeGridComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance105;
			appearance106.BackColor = System.Drawing.SystemColors.Highlight;
			appearance106.ForeColor = System.Drawing.SystemColors.HighlightText;
			routeGridComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance106;
			routeGridComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			routeGridComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance107.BackColor = System.Drawing.SystemColors.Window;
			routeGridComboBox.DisplayLayout.Override.CardAreaAppearance = appearance107;
			appearance108.BorderColor = System.Drawing.Color.Silver;
			appearance108.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			routeGridComboBox.DisplayLayout.Override.CellAppearance = appearance108;
			routeGridComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			routeGridComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance109.BackColor = System.Drawing.SystemColors.Control;
			appearance109.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance109.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance109.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance109.BorderColor = System.Drawing.SystemColors.Window;
			routeGridComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance109;
			appearance110.TextHAlignAsString = "Left";
			routeGridComboBox.DisplayLayout.Override.HeaderAppearance = appearance110;
			routeGridComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			routeGridComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			appearance111.BorderColor = System.Drawing.Color.Silver;
			routeGridComboBox.DisplayLayout.Override.RowAppearance = appearance111;
			routeGridComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance112.BackColor = System.Drawing.SystemColors.ControlLight;
			routeGridComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance112;
			routeGridComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			routeGridComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			routeGridComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			routeGridComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			routeGridComboBox.Editable = true;
			routeGridComboBox.FilterString = "";
			routeGridComboBox.HasAllAccount = false;
			routeGridComboBox.HasCustom = false;
			routeGridComboBox.IsDataLoaded = false;
			routeGridComboBox.Location = new System.Drawing.Point(430, 110);
			routeGridComboBox.MaxDropDownItems = 12;
			routeGridComboBox.Name = "routeGridComboBox";
			routeGridComboBox.ShowInactiveItems = false;
			routeGridComboBox.ShowQuickAdd = true;
			routeGridComboBox.Size = new System.Drawing.Size(100, 20);
			routeGridComboBox.TabIndex = 21;
			routeGridComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			routeGridComboBox.Visible = false;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(427, 53);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(66, 13);
			label3.TabIndex = 173;
			label3.Text = "Reference1:";
			textBoxRef1.Location = new System.Drawing.Point(493, 49);
			textBoxRef1.MaxLength = 64;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(142, 20);
			textBoxRef1.TabIndex = 7;
			rawMaterialsLocationComboBox.Assigned = false;
			rawMaterialsLocationComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			rawMaterialsLocationComboBox.CalcManager = ultraCalcManager1;
			rawMaterialsLocationComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			rawMaterialsLocationComboBox.CustomReportFieldName = "";
			rawMaterialsLocationComboBox.CustomReportKey = "";
			rawMaterialsLocationComboBox.CustomReportValueType = 1;
			rawMaterialsLocationComboBox.DescriptionTextBox = null;
			appearance113.BackColor = System.Drawing.SystemColors.Window;
			appearance113.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			rawMaterialsLocationComboBox.DisplayLayout.Appearance = appearance113;
			rawMaterialsLocationComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			rawMaterialsLocationComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance114.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance114.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance114.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance114.BorderColor = System.Drawing.SystemColors.Window;
			rawMaterialsLocationComboBox.DisplayLayout.GroupByBox.Appearance = appearance114;
			appearance115.ForeColor = System.Drawing.SystemColors.GrayText;
			rawMaterialsLocationComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance115;
			rawMaterialsLocationComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance116.BackColor2 = System.Drawing.SystemColors.Control;
			appearance116.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance116.ForeColor = System.Drawing.SystemColors.GrayText;
			rawMaterialsLocationComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance116;
			rawMaterialsLocationComboBox.DisplayLayout.MaxColScrollRegions = 1;
			rawMaterialsLocationComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance117.BackColor = System.Drawing.SystemColors.Window;
			appearance117.ForeColor = System.Drawing.SystemColors.ControlText;
			rawMaterialsLocationComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance117;
			appearance118.BackColor = System.Drawing.SystemColors.Highlight;
			appearance118.ForeColor = System.Drawing.SystemColors.HighlightText;
			rawMaterialsLocationComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance118;
			rawMaterialsLocationComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			rawMaterialsLocationComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance119.BackColor = System.Drawing.SystemColors.Window;
			rawMaterialsLocationComboBox.DisplayLayout.Override.CardAreaAppearance = appearance119;
			appearance120.BorderColor = System.Drawing.Color.Silver;
			appearance120.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			rawMaterialsLocationComboBox.DisplayLayout.Override.CellAppearance = appearance120;
			rawMaterialsLocationComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			rawMaterialsLocationComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance121.BackColor = System.Drawing.SystemColors.Control;
			appearance121.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance121.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance121.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance121.BorderColor = System.Drawing.SystemColors.Window;
			rawMaterialsLocationComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance121;
			appearance122.TextHAlignAsString = "Left";
			rawMaterialsLocationComboBox.DisplayLayout.Override.HeaderAppearance = appearance122;
			rawMaterialsLocationComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			rawMaterialsLocationComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance123.BackColor = System.Drawing.SystemColors.Window;
			appearance123.BorderColor = System.Drawing.Color.Silver;
			rawMaterialsLocationComboBox.DisplayLayout.Override.RowAppearance = appearance123;
			rawMaterialsLocationComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance124.BackColor = System.Drawing.SystemColors.ControlLight;
			rawMaterialsLocationComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance124;
			rawMaterialsLocationComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			rawMaterialsLocationComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			rawMaterialsLocationComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			rawMaterialsLocationComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			rawMaterialsLocationComboBox.Editable = true;
			rawMaterialsLocationComboBox.FilterString = "";
			rawMaterialsLocationComboBox.HasAllAccount = false;
			rawMaterialsLocationComboBox.HasCustom = false;
			rawMaterialsLocationComboBox.IsDataLoaded = false;
			rawMaterialsLocationComboBox.Location = new System.Drawing.Point(339, 107);
			rawMaterialsLocationComboBox.MaxDropDownItems = 12;
			rawMaterialsLocationComboBox.Name = "rawMaterialsLocationComboBox";
			rawMaterialsLocationComboBox.ShowAll = false;
			rawMaterialsLocationComboBox.ShowConsignIn = false;
			rawMaterialsLocationComboBox.ShowConsignOut = false;
			rawMaterialsLocationComboBox.ShowDefaultLocationOnly = false;
			rawMaterialsLocationComboBox.ShowInactiveItems = false;
			rawMaterialsLocationComboBox.ShowNormalLocations = true;
			rawMaterialsLocationComboBox.ShowPOSOnly = false;
			rawMaterialsLocationComboBox.ShowQuickAdd = true;
			rawMaterialsLocationComboBox.ShowWarehouseOnly = false;
			rawMaterialsLocationComboBox.Size = new System.Drawing.Size(100, 20);
			rawMaterialsLocationComboBox.TabIndex = 21;
			rawMaterialsLocationComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			rawMaterialsLocationComboBox.Visible = false;
			textBoxBOMName.Location = new System.Drawing.Point(225, 71);
			textBoxBOMName.MaxLength = 255;
			textBoxBOMName.Name = "textBoxBOMName";
			textBoxBOMName.ReadOnly = true;
			textBoxBOMName.Size = new System.Drawing.Size(410, 20);
			textBoxBOMName.TabIndex = 171;
			textBoxBOMName.TabStop = false;
			textBoxJobOrderName.Location = new System.Drawing.Point(490, 70);
			textBoxJobOrderName.MaxLength = 255;
			textBoxJobOrderName.Name = "textBoxJobOrderName";
			textBoxJobOrderName.ReadOnly = true;
			textBoxJobOrderName.Size = new System.Drawing.Size(126, 20);
			textBoxJobOrderName.TabIndex = 170;
			textBoxJobOrderName.TabStop = false;
			textBoxJobOrderName.Visible = false;
			routeComboBox.Assigned = false;
			routeComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			routeComboBox.CalcManager = ultraCalcManager1;
			routeComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			routeComboBox.CustomReportFieldName = "";
			routeComboBox.CustomReportKey = "";
			routeComboBox.CustomReportValueType = 1;
			routeComboBox.DescriptionTextBox = textBoxRouteName;
			appearance125.BackColor = System.Drawing.SystemColors.Window;
			appearance125.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			routeComboBox.DisplayLayout.Appearance = appearance125;
			routeComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			routeComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance126.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance126.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance126.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance126.BorderColor = System.Drawing.SystemColors.Window;
			routeComboBox.DisplayLayout.GroupByBox.Appearance = appearance126;
			appearance127.ForeColor = System.Drawing.SystemColors.GrayText;
			routeComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance127;
			routeComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance128.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance128.BackColor2 = System.Drawing.SystemColors.Control;
			appearance128.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance128.ForeColor = System.Drawing.SystemColors.GrayText;
			routeComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance128;
			routeComboBox.DisplayLayout.MaxColScrollRegions = 1;
			routeComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance129.BackColor = System.Drawing.SystemColors.Window;
			appearance129.ForeColor = System.Drawing.SystemColors.ControlText;
			routeComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance129;
			appearance130.BackColor = System.Drawing.SystemColors.Highlight;
			appearance130.ForeColor = System.Drawing.SystemColors.HighlightText;
			routeComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance130;
			routeComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			routeComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance131.BackColor = System.Drawing.SystemColors.Window;
			routeComboBox.DisplayLayout.Override.CardAreaAppearance = appearance131;
			appearance132.BorderColor = System.Drawing.Color.Silver;
			appearance132.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			routeComboBox.DisplayLayout.Override.CellAppearance = appearance132;
			routeComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			routeComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance133.BackColor = System.Drawing.SystemColors.Control;
			appearance133.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance133.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance133.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance133.BorderColor = System.Drawing.SystemColors.Window;
			routeComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance133;
			appearance134.TextHAlignAsString = "Left";
			routeComboBox.DisplayLayout.Override.HeaderAppearance = appearance134;
			routeComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			routeComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance135.BackColor = System.Drawing.SystemColors.Window;
			appearance135.BorderColor = System.Drawing.Color.Silver;
			routeComboBox.DisplayLayout.Override.RowAppearance = appearance135;
			routeComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance136.BackColor = System.Drawing.SystemColors.ControlLight;
			routeComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance136;
			routeComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			routeComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			routeComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			routeComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			routeComboBox.Editable = true;
			routeComboBox.FilterString = "";
			routeComboBox.HasAllAccount = false;
			routeComboBox.HasCustom = false;
			routeComboBox.IsDataLoaded = false;
			routeComboBox.Location = new System.Drawing.Point(93, 27);
			routeComboBox.MaxDropDownItems = 12;
			routeComboBox.Name = "routeComboBox";
			routeComboBox.ShowInactiveItems = false;
			routeComboBox.ShowQuickAdd = true;
			routeComboBox.Size = new System.Drawing.Size(128, 20);
			routeComboBox.TabIndex = 3;
			routeComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRouteName.Location = new System.Drawing.Point(225, 27);
			textBoxRouteName.MaxLength = 255;
			textBoxRouteName.Name = "textBoxRouteName";
			textBoxRouteName.ReadOnly = true;
			textBoxRouteName.Size = new System.Drawing.Size(410, 20);
			textBoxRouteName.TabIndex = 4;
			textBoxRouteName.TabStop = false;
			appearance137.FontData.BoldAsString = "True";
			appearance137.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel4.Appearance = appearance137;
			ultraFormattedLinkLabel4.AutoSize = true;
			ultraFormattedLinkLabel4.Location = new System.Drawing.Point(415, 92);
			ultraFormattedLinkLabel4.Name = "ultraFormattedLinkLabel4";
			ultraFormattedLinkLabel4.Size = new System.Drawing.Size(62, 15);
			ultraFormattedLinkLabel4.TabIndex = 162;
			ultraFormattedLinkLabel4.TabStop = true;
			ultraFormattedLinkLabel4.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel4.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel4.Value = "Job Order:";
			ultraFormattedLinkLabel4.Visible = false;
			appearance138.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel4.VisitedLinkAppearance = appearance138;
			ultraFormattedLinkLabel4.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel4_LinkClicked);
			comboBoxJobOrder.Assigned = false;
			comboBoxJobOrder.CalcManager = ultraCalcManager1;
			comboBoxJobOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJobOrder.CustomReportFieldName = "";
			comboBoxJobOrder.CustomReportKey = "";
			comboBoxJobOrder.CustomReportValueType = 1;
			comboBoxJobOrder.DescriptionTextBox = textBoxJobOrderName;
			appearance139.BackColor = System.Drawing.SystemColors.Window;
			appearance139.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxJobOrder.DisplayLayout.Appearance = appearance139;
			comboBoxJobOrder.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxJobOrder.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance140.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance140.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance140.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance140.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJobOrder.DisplayLayout.GroupByBox.Appearance = appearance140;
			appearance141.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJobOrder.DisplayLayout.GroupByBox.BandLabelAppearance = appearance141;
			comboBoxJobOrder.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance142.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance142.BackColor2 = System.Drawing.SystemColors.Control;
			appearance142.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance142.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJobOrder.DisplayLayout.GroupByBox.PromptAppearance = appearance142;
			comboBoxJobOrder.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxJobOrder.DisplayLayout.MaxRowScrollRegions = 1;
			appearance143.BackColor = System.Drawing.SystemColors.Window;
			appearance143.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxJobOrder.DisplayLayout.Override.ActiveCellAppearance = appearance143;
			appearance144.BackColor = System.Drawing.SystemColors.Highlight;
			appearance144.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxJobOrder.DisplayLayout.Override.ActiveRowAppearance = appearance144;
			comboBoxJobOrder.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxJobOrder.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance145.BackColor = System.Drawing.SystemColors.Window;
			comboBoxJobOrder.DisplayLayout.Override.CardAreaAppearance = appearance145;
			appearance146.BorderColor = System.Drawing.Color.Silver;
			appearance146.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxJobOrder.DisplayLayout.Override.CellAppearance = appearance146;
			comboBoxJobOrder.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxJobOrder.DisplayLayout.Override.CellPadding = 0;
			appearance147.BackColor = System.Drawing.SystemColors.Control;
			appearance147.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance147.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance147.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance147.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJobOrder.DisplayLayout.Override.GroupByRowAppearance = appearance147;
			appearance148.TextHAlignAsString = "Left";
			comboBoxJobOrder.DisplayLayout.Override.HeaderAppearance = appearance148;
			comboBoxJobOrder.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxJobOrder.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance149.BackColor = System.Drawing.SystemColors.Window;
			appearance149.BorderColor = System.Drawing.Color.Silver;
			comboBoxJobOrder.DisplayLayout.Override.RowAppearance = appearance149;
			comboBoxJobOrder.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance150.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxJobOrder.DisplayLayout.Override.TemplateAddRowAppearance = appearance150;
			comboBoxJobOrder.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxJobOrder.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxJobOrder.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxJobOrder.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJobOrder.Editable = true;
			comboBoxJobOrder.FilterString = "";
			comboBoxJobOrder.FilterSysDocID = "";
			comboBoxJobOrder.HasAll = false;
			comboBoxJobOrder.HasCustom = false;
			comboBoxJobOrder.IsDataLoaded = false;
			comboBoxJobOrder.Location = new System.Drawing.Point(490, 88);
			comboBoxJobOrder.MaxDropDownItems = 12;
			comboBoxJobOrder.Name = "comboBoxJobOrder";
			comboBoxJobOrder.ShowConsignmentOnly = false;
			comboBoxJobOrder.ShowInactive = false;
			comboBoxJobOrder.ShowLPOCustomersOnly = false;
			comboBoxJobOrder.ShowPROCustomersOnly = false;
			comboBoxJobOrder.ShowQuickAdd = true;
			comboBoxJobOrder.Size = new System.Drawing.Size(128, 20);
			comboBoxJobOrder.TabIndex = 8;
			comboBoxJobOrder.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxJobOrder.Visible = false;
			appearance151.FontData.BoldAsString = "False";
			appearance151.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel6.Appearance = appearance151;
			ultraFormattedLinkLabel6.AutoSize = true;
			ultraFormattedLinkLabel6.Location = new System.Drawing.Point(3, 74);
			ultraFormattedLinkLabel6.Name = "ultraFormattedLinkLabel6";
			ultraFormattedLinkLabel6.Size = new System.Drawing.Size(31, 15);
			ultraFormattedLinkLabel6.TabIndex = 161;
			ultraFormattedLinkLabel6.TabStop = true;
			ultraFormattedLinkLabel6.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel6.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel6.Value = "BOM:";
			appearance152.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel6.VisitedLinkAppearance = appearance152;
			ultraFormattedLinkLabel6.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel6_LinkClicked);
			appearance153.FontData.BoldAsString = "True";
			appearance153.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance153;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(3, 52);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel3.TabIndex = 160;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Location:";
			appearance154.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance154;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(3, 96);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(88, 13);
			label8.TabIndex = 159;
			label8.Text = "Completion Date:";
			dateTimePickerWorkCompDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerWorkCompDate.Location = new System.Drawing.Point(94, 93);
			dateTimePickerWorkCompDate.Name = "dateTimePickerWorkCompDate";
			dateTimePickerWorkCompDate.Size = new System.Drawing.Size(127, 20);
			dateTimePickerWorkCompDate.TabIndex = 9;
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
			comboBoxSysDoc.Location = new System.Drawing.Point(93, 5);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(128, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBOM.Assigned = false;
			comboBoxBOM.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBOM.CalcManager = ultraCalcManager1;
			comboBoxBOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBOM.CustomReportFieldName = "";
			comboBoxBOM.CustomReportKey = "";
			comboBoxBOM.CustomReportValueType = 1;
			comboBoxBOM.DescriptionTextBox = textBoxBOMName;
			comboBoxBOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBOM.Editable = true;
			comboBoxBOM.FilterString = "";
			comboBoxBOM.HasAllAccount = false;
			comboBoxBOM.HasCustom = false;
			comboBoxBOM.IsDataLoaded = false;
			comboBoxBOM.Location = new System.Drawing.Point(93, 71);
			comboBoxBOM.MaxDropDownItems = 12;
			comboBoxBOM.Name = "comboBoxBOM";
			comboBoxBOM.ShowInactiveItems = false;
			comboBoxBOM.ShowQuickAdd = true;
			comboBoxBOM.Size = new System.Drawing.Size(128, 20);
			comboBoxBOM.TabIndex = 8;
			comboBoxBOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance155.FontData.Name = "Tahoma";
			ultraFormattedLinkLabelRoute.Appearance = appearance155;
			ultraFormattedLinkLabelRoute.AutoSize = true;
			ultraFormattedLinkLabelRoute.Location = new System.Drawing.Point(3, 29);
			ultraFormattedLinkLabelRoute.Name = "ultraFormattedLinkLabelRoute";
			ultraFormattedLinkLabelRoute.Size = new System.Drawing.Size(37, 15);
			ultraFormattedLinkLabelRoute.TabIndex = 124;
			ultraFormattedLinkLabelRoute.TabStop = true;
			ultraFormattedLinkLabelRoute.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabelRoute.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabelRoute.Value = "Route:";
			appearance156.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabelRoute.VisitedLinkAppearance = appearance156;
			ultraFormattedLinkLabelRoute.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = null;
			appearance157.BackColor = System.Drawing.SystemColors.Window;
			appearance157.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance157;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance158.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance158.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance158.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance158.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance158;
			appearance159.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance159;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance160.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance160.BackColor2 = System.Drawing.SystemColors.Control;
			appearance160.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance160.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance160;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance161.BackColor = System.Drawing.SystemColors.Window;
			appearance161.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance161;
			appearance162.BackColor = System.Drawing.SystemColors.Highlight;
			appearance162.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance162;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance163.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance163;
			appearance164.BorderColor = System.Drawing.Color.Silver;
			appearance164.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance164;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance165.BackColor = System.Drawing.SystemColors.Control;
			appearance165.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance165.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance165.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance165.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance165;
			appearance166.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance166;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance167.BackColor = System.Drawing.SystemColors.Window;
			appearance167.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance167;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance168.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance168;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(93, 49);
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
			comboBoxLocation.Size = new System.Drawing.Size(128, 20);
			comboBoxLocation.TabIndex = 5;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			appearance169.FontData.BoldAsString = "True";
			appearance169.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance169;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(3, 8);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance170.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance170;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(450, 9);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(38, 13);
			mmLabel1.TabIndex = 2;
			mmLabel1.Text = "Date:";
			productUnitComboBox.Assigned = false;
			productUnitComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			productUnitComboBox.CalcManager = ultraCalcManager1;
			productUnitComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			productUnitComboBox.CustomReportFieldName = "";
			productUnitComboBox.CustomReportKey = "";
			productUnitComboBox.CustomReportValueType = 1;
			productUnitComboBox.DescriptionTextBox = null;
			appearance171.BackColor = System.Drawing.SystemColors.Window;
			appearance171.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			productUnitComboBox.DisplayLayout.Appearance = appearance171;
			productUnitComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			productUnitComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance172.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance172.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance172.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance172.BorderColor = System.Drawing.SystemColors.Window;
			productUnitComboBox.DisplayLayout.GroupByBox.Appearance = appearance172;
			appearance173.ForeColor = System.Drawing.SystemColors.GrayText;
			productUnitComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance173;
			productUnitComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance174.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance174.BackColor2 = System.Drawing.SystemColors.Control;
			appearance174.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance174.ForeColor = System.Drawing.SystemColors.GrayText;
			productUnitComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance174;
			productUnitComboBox.DisplayLayout.MaxColScrollRegions = 1;
			productUnitComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance175.BackColor = System.Drawing.SystemColors.Window;
			appearance175.ForeColor = System.Drawing.SystemColors.ControlText;
			productUnitComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance175;
			appearance176.BackColor = System.Drawing.SystemColors.Highlight;
			appearance176.ForeColor = System.Drawing.SystemColors.HighlightText;
			productUnitComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance176;
			productUnitComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			productUnitComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance177.BackColor = System.Drawing.SystemColors.Window;
			productUnitComboBox.DisplayLayout.Override.CardAreaAppearance = appearance177;
			appearance178.BorderColor = System.Drawing.Color.Silver;
			appearance178.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			productUnitComboBox.DisplayLayout.Override.CellAppearance = appearance178;
			productUnitComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			productUnitComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance179.BackColor = System.Drawing.SystemColors.Control;
			appearance179.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance179.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance179.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance179.BorderColor = System.Drawing.SystemColors.Window;
			productUnitComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance179;
			appearance180.TextHAlignAsString = "Left";
			productUnitComboBox.DisplayLayout.Override.HeaderAppearance = appearance180;
			productUnitComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			productUnitComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance181.BackColor = System.Drawing.SystemColors.Window;
			appearance181.BorderColor = System.Drawing.Color.Silver;
			productUnitComboBox.DisplayLayout.Override.RowAppearance = appearance181;
			productUnitComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance182.BackColor = System.Drawing.SystemColors.ControlLight;
			productUnitComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance182;
			productUnitComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			productUnitComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			productUnitComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			productUnitComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			productUnitComboBox.Editable = true;
			productUnitComboBox.FilterString = "";
			productUnitComboBox.IsDataLoaded = false;
			productUnitComboBox.Location = new System.Drawing.Point(461, 107);
			productUnitComboBox.MaxDropDownItems = 12;
			productUnitComboBox.Name = "productUnitComboBox";
			productUnitComboBox.Size = new System.Drawing.Size(100, 20);
			productUnitComboBox.TabIndex = 169;
			productUnitComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			productUnitComboBox.Visible = false;
			comboBoxGridEmployee.Assigned = false;
			comboBoxGridEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridEmployee.CalcManager = ultraCalcManager1;
			comboBoxGridEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridEmployee.CustomReportFieldName = "";
			comboBoxGridEmployee.CustomReportKey = "";
			comboBoxGridEmployee.CustomReportValueType = 1;
			comboBoxGridEmployee.DescriptionTextBox = null;
			appearance183.BackColor = System.Drawing.SystemColors.Window;
			appearance183.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridEmployee.DisplayLayout.Appearance = appearance183;
			comboBoxGridEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance184.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance184.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance184.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance184.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.Appearance = appearance184;
			appearance185.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance185;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance186.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance186.BackColor2 = System.Drawing.SystemColors.Control;
			appearance186.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance186.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance186;
			comboBoxGridEmployee.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance187.BackColor = System.Drawing.SystemColors.Window;
			appearance187.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance187;
			appearance188.BackColor = System.Drawing.SystemColors.Highlight;
			appearance188.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance188;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance189.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.CardAreaAppearance = appearance189;
			appearance190.BorderColor = System.Drawing.Color.Silver;
			appearance190.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridEmployee.DisplayLayout.Override.CellAppearance = appearance190;
			comboBoxGridEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance191.BackColor = System.Drawing.SystemColors.Control;
			appearance191.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance191.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance191.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance191.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance191;
			appearance192.TextHAlignAsString = "Left";
			comboBoxGridEmployee.DisplayLayout.Override.HeaderAppearance = appearance192;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance193.BackColor = System.Drawing.SystemColors.Window;
			appearance193.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridEmployee.DisplayLayout.Override.RowAppearance = appearance193;
			comboBoxGridEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance194.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance194;
			comboBoxGridEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridEmployee.Editable = true;
			comboBoxGridEmployee.FilterString = "";
			comboBoxGridEmployee.HasAllAccount = false;
			comboBoxGridEmployee.HasCustom = false;
			comboBoxGridEmployee.IsDataLoaded = false;
			comboBoxGridEmployee.Location = new System.Drawing.Point(495, 107);
			comboBoxGridEmployee.MaxDropDownItems = 12;
			comboBoxGridEmployee.Name = "comboBoxGridEmployee";
			comboBoxGridEmployee.ShowInactiveItems = false;
			comboBoxGridEmployee.ShowQuickAdd = true;
			comboBoxGridEmployee.ShowTerminatedEmployees = true;
			comboBoxGridEmployee.Size = new System.Drawing.Size(100, 20);
			comboBoxGridEmployee.TabIndex = 168;
			comboBoxGridEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridEmployee.Visible = false;
			comboBoxGridPosition.Assigned = false;
			comboBoxGridPosition.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxGridPosition.CalcManager = ultraCalcManager1;
			comboBoxGridPosition.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridPosition.CustomReportFieldName = "";
			comboBoxGridPosition.CustomReportKey = "";
			comboBoxGridPosition.CustomReportValueType = 1;
			comboBoxGridPosition.DescriptionTextBox = null;
			appearance195.BackColor = System.Drawing.SystemColors.Window;
			appearance195.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridPosition.DisplayLayout.Appearance = appearance195;
			comboBoxGridPosition.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridPosition.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance196.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance196.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance196.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance196.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPosition.DisplayLayout.GroupByBox.Appearance = appearance196;
			appearance197.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPosition.DisplayLayout.GroupByBox.BandLabelAppearance = appearance197;
			comboBoxGridPosition.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance198.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance198.BackColor2 = System.Drawing.SystemColors.Control;
			appearance198.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance198.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridPosition.DisplayLayout.GroupByBox.PromptAppearance = appearance198;
			comboBoxGridPosition.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridPosition.DisplayLayout.MaxRowScrollRegions = 1;
			appearance199.BackColor = System.Drawing.SystemColors.Window;
			appearance199.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridPosition.DisplayLayout.Override.ActiveCellAppearance = appearance199;
			appearance200.BackColor = System.Drawing.SystemColors.Highlight;
			appearance200.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridPosition.DisplayLayout.Override.ActiveRowAppearance = appearance200;
			comboBoxGridPosition.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridPosition.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance201.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridPosition.DisplayLayout.Override.CardAreaAppearance = appearance201;
			appearance202.BorderColor = System.Drawing.Color.Silver;
			appearance202.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridPosition.DisplayLayout.Override.CellAppearance = appearance202;
			comboBoxGridPosition.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridPosition.DisplayLayout.Override.CellPadding = 0;
			appearance203.BackColor = System.Drawing.SystemColors.Control;
			appearance203.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance203.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance203.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance203.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridPosition.DisplayLayout.Override.GroupByRowAppearance = appearance203;
			appearance204.TextHAlignAsString = "Left";
			comboBoxGridPosition.DisplayLayout.Override.HeaderAppearance = appearance204;
			comboBoxGridPosition.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridPosition.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance205.BackColor = System.Drawing.SystemColors.Window;
			appearance205.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridPosition.DisplayLayout.Override.RowAppearance = appearance205;
			comboBoxGridPosition.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance206.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridPosition.DisplayLayout.Override.TemplateAddRowAppearance = appearance206;
			comboBoxGridPosition.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridPosition.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridPosition.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridPosition.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridPosition.Editable = true;
			comboBoxGridPosition.FilterString = "";
			comboBoxGridPosition.HasAllAccount = false;
			comboBoxGridPosition.HasCustom = false;
			comboBoxGridPosition.IsDataLoaded = false;
			comboBoxGridPosition.Location = new System.Drawing.Point(523, 107);
			comboBoxGridPosition.MaxDropDownItems = 12;
			comboBoxGridPosition.Name = "comboBoxGridPosition";
			comboBoxGridPosition.ShowInactiveItems = false;
			comboBoxGridPosition.ShowQuickAdd = true;
			comboBoxGridPosition.Size = new System.Drawing.Size(100, 20);
			comboBoxGridPosition.TabIndex = 167;
			comboBoxGridPosition.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridPosition.Visible = false;
			rawMaterialsProductComboBox.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			rawMaterialsProductComboBox.Assigned = false;
			rawMaterialsProductComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			rawMaterialsProductComboBox.CalcManager = ultraCalcManager1;
			rawMaterialsProductComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			rawMaterialsProductComboBox.CustomReportFieldName = "";
			rawMaterialsProductComboBox.CustomReportKey = "";
			rawMaterialsProductComboBox.CustomReportValueType = 1;
			rawMaterialsProductComboBox.DescriptionTextBox = null;
			appearance207.BackColor = System.Drawing.SystemColors.Window;
			appearance207.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			rawMaterialsProductComboBox.DisplayLayout.Appearance = appearance207;
			rawMaterialsProductComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			rawMaterialsProductComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance208.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance208.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance208.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance208.BorderColor = System.Drawing.SystemColors.Window;
			rawMaterialsProductComboBox.DisplayLayout.GroupByBox.Appearance = appearance208;
			appearance209.ForeColor = System.Drawing.SystemColors.GrayText;
			rawMaterialsProductComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance209;
			rawMaterialsProductComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance210.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance210.BackColor2 = System.Drawing.SystemColors.Control;
			appearance210.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance210.ForeColor = System.Drawing.SystemColors.GrayText;
			rawMaterialsProductComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance210;
			rawMaterialsProductComboBox.DisplayLayout.MaxColScrollRegions = 1;
			rawMaterialsProductComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance211.BackColor = System.Drawing.SystemColors.Window;
			appearance211.ForeColor = System.Drawing.SystemColors.ControlText;
			rawMaterialsProductComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance211;
			appearance212.BackColor = System.Drawing.SystemColors.Highlight;
			appearance212.ForeColor = System.Drawing.SystemColors.HighlightText;
			rawMaterialsProductComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance212;
			rawMaterialsProductComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			rawMaterialsProductComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance213.BackColor = System.Drawing.SystemColors.Window;
			rawMaterialsProductComboBox.DisplayLayout.Override.CardAreaAppearance = appearance213;
			appearance214.BorderColor = System.Drawing.Color.Silver;
			appearance214.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			rawMaterialsProductComboBox.DisplayLayout.Override.CellAppearance = appearance214;
			rawMaterialsProductComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			rawMaterialsProductComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance215.BackColor = System.Drawing.SystemColors.Control;
			appearance215.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance215.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance215.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance215.BorderColor = System.Drawing.SystemColors.Window;
			rawMaterialsProductComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance215;
			appearance216.TextHAlignAsString = "Left";
			rawMaterialsProductComboBox.DisplayLayout.Override.HeaderAppearance = appearance216;
			rawMaterialsProductComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			rawMaterialsProductComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance217.BackColor = System.Drawing.SystemColors.Window;
			appearance217.BorderColor = System.Drawing.Color.Silver;
			rawMaterialsProductComboBox.DisplayLayout.Override.RowAppearance = appearance217;
			rawMaterialsProductComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance218.BackColor = System.Drawing.SystemColors.ControlLight;
			rawMaterialsProductComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance218;
			rawMaterialsProductComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			rawMaterialsProductComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			rawMaterialsProductComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			rawMaterialsProductComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			rawMaterialsProductComboBox.Editable = true;
			rawMaterialsProductComboBox.FilterCustomerID = "";
			rawMaterialsProductComboBox.FilterString = "";
			rawMaterialsProductComboBox.FilterSysDocID = "";
			rawMaterialsProductComboBox.HasAllAccount = false;
			rawMaterialsProductComboBox.HasCustom = false;
			rawMaterialsProductComboBox.IsDataLoaded = false;
			rawMaterialsProductComboBox.Location = new System.Drawing.Point(552, 107);
			rawMaterialsProductComboBox.MaxDropDownItems = 12;
			rawMaterialsProductComboBox.Name = "rawMaterialsProductComboBox";
			rawMaterialsProductComboBox.Show3PLItems = true;
			rawMaterialsProductComboBox.ShowInactiveItems = false;
			rawMaterialsProductComboBox.ShowOnlyLotItems = false;
			rawMaterialsProductComboBox.ShowQuickAdd = true;
			rawMaterialsProductComboBox.Size = new System.Drawing.Size(100, 20);
			rawMaterialsProductComboBox.TabIndex = 20;
			rawMaterialsProductComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			rawMaterialsProductComboBox.Visible = false;
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
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Location = new System.Drawing.Point(12, 169);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(712, 269);
			ultraTabControl1.TabIndex = 1;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "Items";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "Raw Materials";
			ultraTab3.TabPage = ultraTabPageControl1;
			ultraTab3.Text = "Resources";
			ultraTab4.TabPage = tabPageExpense;
			ultraTab4.Text = "Expenses";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[4]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(708, 243);
			contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator4,
				removeRowToolStripMenuItem
			});
			contextMenuStrip2.Name = "contextMenuStrip1";
			contextMenuStrip2.Size = new System.Drawing.Size(181, 120);
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
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			textBoxNote.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxNote.Location = new System.Drawing.Point(53, 475);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxNote.Size = new System.Drawing.Size(337, 67);
			textBoxNote.TabIndex = 2;
			label2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(14, 477);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 13);
			label2.TabIndex = 18;
			label2.Text = "Note:";
			itemLocationComboBox.Assigned = false;
			itemLocationComboBox.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			itemLocationComboBox.CalcManager = ultraCalcManager1;
			itemLocationComboBox.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			itemLocationComboBox.CustomReportFieldName = "";
			itemLocationComboBox.CustomReportKey = "";
			itemLocationComboBox.CustomReportValueType = 1;
			itemLocationComboBox.DescriptionTextBox = null;
			appearance219.BackColor = System.Drawing.SystemColors.Window;
			appearance219.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			itemLocationComboBox.DisplayLayout.Appearance = appearance219;
			itemLocationComboBox.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			itemLocationComboBox.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance220.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance220.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance220.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance220.BorderColor = System.Drawing.SystemColors.Window;
			itemLocationComboBox.DisplayLayout.GroupByBox.Appearance = appearance220;
			appearance221.ForeColor = System.Drawing.SystemColors.GrayText;
			itemLocationComboBox.DisplayLayout.GroupByBox.BandLabelAppearance = appearance221;
			itemLocationComboBox.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance222.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance222.BackColor2 = System.Drawing.SystemColors.Control;
			appearance222.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance222.ForeColor = System.Drawing.SystemColors.GrayText;
			itemLocationComboBox.DisplayLayout.GroupByBox.PromptAppearance = appearance222;
			itemLocationComboBox.DisplayLayout.MaxColScrollRegions = 1;
			itemLocationComboBox.DisplayLayout.MaxRowScrollRegions = 1;
			appearance223.BackColor = System.Drawing.SystemColors.Window;
			appearance223.ForeColor = System.Drawing.SystemColors.ControlText;
			itemLocationComboBox.DisplayLayout.Override.ActiveCellAppearance = appearance223;
			appearance224.BackColor = System.Drawing.SystemColors.Highlight;
			appearance224.ForeColor = System.Drawing.SystemColors.HighlightText;
			itemLocationComboBox.DisplayLayout.Override.ActiveRowAppearance = appearance224;
			itemLocationComboBox.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			itemLocationComboBox.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance225.BackColor = System.Drawing.SystemColors.Window;
			itemLocationComboBox.DisplayLayout.Override.CardAreaAppearance = appearance225;
			appearance226.BorderColor = System.Drawing.Color.Silver;
			appearance226.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			itemLocationComboBox.DisplayLayout.Override.CellAppearance = appearance226;
			itemLocationComboBox.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			itemLocationComboBox.DisplayLayout.Override.CellPadding = 0;
			appearance227.BackColor = System.Drawing.SystemColors.Control;
			appearance227.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance227.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance227.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance227.BorderColor = System.Drawing.SystemColors.Window;
			itemLocationComboBox.DisplayLayout.Override.GroupByRowAppearance = appearance227;
			appearance228.TextHAlignAsString = "Left";
			itemLocationComboBox.DisplayLayout.Override.HeaderAppearance = appearance228;
			itemLocationComboBox.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			itemLocationComboBox.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance229.BackColor = System.Drawing.SystemColors.Window;
			appearance229.BorderColor = System.Drawing.Color.Silver;
			itemLocationComboBox.DisplayLayout.Override.RowAppearance = appearance229;
			itemLocationComboBox.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance230.BackColor = System.Drawing.SystemColors.ControlLight;
			itemLocationComboBox.DisplayLayout.Override.TemplateAddRowAppearance = appearance230;
			itemLocationComboBox.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			itemLocationComboBox.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			itemLocationComboBox.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			itemLocationComboBox.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			itemLocationComboBox.Editable = true;
			itemLocationComboBox.FilterString = "";
			itemLocationComboBox.HasAllAccount = false;
			itemLocationComboBox.HasCustom = false;
			itemLocationComboBox.IsDataLoaded = false;
			itemLocationComboBox.Location = new System.Drawing.Point(586, 167);
			itemLocationComboBox.MaxDropDownItems = 12;
			itemLocationComboBox.Name = "itemLocationComboBox";
			itemLocationComboBox.ShowAll = false;
			itemLocationComboBox.ShowConsignIn = false;
			itemLocationComboBox.ShowConsignOut = false;
			itemLocationComboBox.ShowDefaultLocationOnly = false;
			itemLocationComboBox.ShowInactiveItems = false;
			itemLocationComboBox.ShowNormalLocations = true;
			itemLocationComboBox.ShowPOSOnly = false;
			itemLocationComboBox.ShowQuickAdd = true;
			itemLocationComboBox.ShowWarehouseOnly = false;
			itemLocationComboBox.Size = new System.Drawing.Size(100, 20);
			itemLocationComboBox.TabIndex = 20;
			itemLocationComboBox.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			itemLocationComboBox.Visible = false;
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
			appearance231.BackColor = System.Drawing.SystemColors.Window;
			appearance231.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance231;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance232.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance232.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance232.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance232.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance232;
			appearance233.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance233;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance234.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance234.BackColor2 = System.Drawing.SystemColors.Control;
			appearance234.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance234.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance234;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance235.BackColor = System.Drawing.SystemColors.Window;
			appearance235.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance235;
			appearance236.BackColor = System.Drawing.SystemColors.Highlight;
			appearance236.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance236;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance237.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance237;
			appearance238.BorderColor = System.Drawing.Color.Silver;
			appearance238.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance238;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance239.BackColor = System.Drawing.SystemColors.Control;
			appearance239.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance239.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance239.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance239.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance239;
			appearance240.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance240;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance241.BackColor = System.Drawing.SystemColors.Window;
			appearance241.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance241;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance242.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance242;
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
			appearance243.BackColor = System.Drawing.SystemColors.Window;
			appearance243.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance243;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance244.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance244.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance244.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance244.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance244;
			appearance245.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance245;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance246.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance246.BackColor2 = System.Drawing.SystemColors.Control;
			appearance246.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance246.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance246;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance247.BackColor = System.Drawing.SystemColors.Window;
			appearance247.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance247;
			appearance248.BackColor = System.Drawing.SystemColors.Highlight;
			appearance248.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance248;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance249.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance249;
			appearance250.BorderColor = System.Drawing.Color.Silver;
			appearance250.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance250;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance251.BackColor = System.Drawing.SystemColors.Control;
			appearance251.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance251.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance251.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance251.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance251;
			appearance252.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance252;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance253.BackColor = System.Drawing.SystemColors.Window;
			appearance253.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance253;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance254.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance254;
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
			contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				toolStripMenuItem1,
				toolStripMenuItem2,
				toolStripMenuItem3,
				toolStripMenuItem4,
				toolStripSeparator6,
				toolStripMenuItem5
			});
			contextMenuStrip3.Name = "contextMenuStrip1";
			contextMenuStrip3.Size = new System.Drawing.Size(181, 120);
			toolStripMenuItem1.Name = "toolStripMenuItem1";
			toolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem1.Text = "Available Quantity...";
			toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
			toolStripMenuItem2.Name = "toolStripMenuItem2";
			toolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem2.Text = "Sales Statistics...";
			toolStripMenuItem3.Name = "toolStripMenuItem3";
			toolStripMenuItem3.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem3.Text = "Item Photo...";
			toolStripMenuItem3.Click += new System.EventHandler(toolStripMenuItem3_Click);
			toolStripMenuItem4.Name = "toolStripMenuItem4";
			toolStripMenuItem4.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem4.Text = "Item Details...";
			toolStripMenuItem4.Click += new System.EventHandler(toolStripMenuItem4_Click);
			toolStripSeparator6.Name = "toolStripSeparator6";
			toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
			toolStripMenuItem5.Image = Micromind.ClientUI.Properties.Resources.Delete;
			toolStripMenuItem5.Name = "toolStripMenuItem5";
			toolStripMenuItem5.Size = new System.Drawing.Size(180, 22);
			toolStripMenuItem5.Text = "Remove Row";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(739, 588);
			base.Controls.Add(itemLocationComboBox);
			base.Controls.Add(panelDetails);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(label2);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(ultraGroupBox1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "ProductionDetailsForm";
			Text = "Production";
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit1).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ultraTabPageControl2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridRawMaterials).EndInit();
			ultraTabPageControl1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridResource).EndInit();
			tabPageExpense.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)routeGridComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)rawMaterialsLocationComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)routeComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxJobOrder).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)productUnitComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridEmployee).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridPosition).EndInit();
			((System.ComponentModel.ISupportInitialize)rawMaterialsProductComboBox).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			contextMenuStrip2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)itemLocationComboBox).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).EndInit();
			contextMenuStrip3.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
