using Infragistics.Win;
using Infragistics.Win.FormattedLinkLabel;
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class InventoryDismantleForm : Form, IForm
	{
		private InventoryDismantleData currentData;

		private const string TABLENAME_CONST = "Inventory_Dismantle";

		private const string IDFIELD_CONST = "VoucherID";

		private bool isNewRecord = true;

		private bool showItemdetail = CompanyPreferences.ShowItemdetail;

		private bool userViewStaus;

		private bool LoadItemFeatures = CompanyPreferences.LoadItemFeatures;

		private bool ActivatePartsDetails = CompanyPreferences.ActivatePartsDetails;

		private decimal percentage;

		private decimal oldQty;

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

		private TextBox textBoxRef1;

		private TextBox textBoxNote;

		private Label label3;

		private XPButton buttonDelete;

		private XPButton buttonNew;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel2;

		private XPButton buttonVoid;

		private Panel panelDetails;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel5;

		private ProductComboBox comboBoxGridItem;

		private ProductUnitComboBox comboBoxGridProductUnit;

		private UltraCalcManager ultraCalcManager1;

		private ToolStripButton toolStripButtonPreview;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem printListToolStripMenuItem;

		private UltraGridPrintDocument ultraGridPrintDocument1;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel1;

		private TextBox textBoxAssemblyName;

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

		private ProductComboBox comboBoxDismantle;

		private DataEntryGrid dataGridItems;

		private Label label6;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonDistribution;

		private LocationComboBox comboBoxGridLocation;

		private LocationComboBox comboBoxLocation;

		private TextBox textBoxRepackName;

		private SysDocComboBox comboBoxSysDoc;

		private UltraFormattedLinkLabel ultraFormattedLinkLabel3;

		private ToolStripButton toolStripButtonOpenList;

		private NumberTextBox textBoxCost;

		private ToolStripButton toolStripButtonInformation;

		private ToolStripSeparator toolStripSeparator4;

		private ToolStripButton toolStripButtonAttach;

		private ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem availableQuantityToolStripMenuItem;

		private ToolStripMenuItem salesStatisticsToolStripMenuItem;

		private ToolStripMenuItem itemPicToolStripMenuItem;

		private ToolStripMenuItem itemDetailsToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator5;

		private ToolStripMenuItem removeRowToolStripMenuItem;

		private ProductPhotoViewer productPhotoViewer;

		private ToolStripButton toolStripButtonApproval;

		private ToolStripLabel toolStripLabelApproval;

		private ToolStripSeparator toolStripSeparatorApproval;

		private NumberTextBox textBoxQuantity;

		private Label label2;

		private ToolStripButton toolStripButtonExcelImport;

		private NumberTextBox textBoxUnitCost;

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
				bool enabled;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					XPButton xPButton = buttonDelete;
					enabled = (buttonVoid.Enabled = false);
					xPButton.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					XPButton xPButton2 = buttonDelete;
					enabled = (buttonVoid.Enabled = true);
					xPButton2.Enabled = enabled;
					textBoxVoucherNumber.ReadOnly = true;
				}
				ToolStripButton toolStripButton = toolStripButtonPrint;
				enabled = (toolStripButtonPreview.Enabled = !isNewRecord);
				toolStripButton.Enabled = enabled;
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

		public InventoryDismantleForm()
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
				comboBoxGridLocation.ShowWarehouseOnly = true;
				comboBoxDismantle.ShowOnlyInventoryItems = true;
				comboBoxGridExpenseCode.ExpenseCodeType = ExpenseCodeTypes.Manufacturing;
				comboBoxDismantle.DescriptionTextBox = textBoxAssemblyName;
			}
		}

		private void AddEvents()
		{
			base.Load += BuildAssemblyForm_Load;
			dataGridItems.CellDataError += dataGrid_CellDataError;
			dataGridItems.BeforeCellUpdate += dataGrid_BeforeCellUpdate;
			dataGridItems.AfterRowActivate += dataGridItems_AfterRowActivate;
			dataGridItems.BeforeRowDeactivate += dataGrid_BeforeRowDeactivate;
			dataGridItems.BeforeCellDeactivate += dataGrid_BeforeCellDeactivate;
			dataGridItems.BeforeCellActivate += dataGridItems_BeforeCellActivate;
			dataGridItems.AfterCellUpdate += dataGridItems_AfterCellUpdate;
			dataGridItems.AfterRowsDeleted += dataGridItems_AfterRowsDeleted;
			dataGridItems.HeaderClicked += dataGridItems_HeaderClicked;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
			dataGridExpense.AfterCellUpdate += dataGridExpense_AfterCellUpdate;
			dataGridExpense.BeforeRowDeactivate += dataGridExpense_BeforeRowDeactivate;
			dataGridExpense.BeforeCellDeactivate += dataGridExpense_BeforeCellDeactivate;
			dataGridExpense.AfterRowsDeleted += dataGridExpense_AfterRowsDeleted;
			dataGridExpense.CellDataError += dataGridExpense_CellDataError;
			comboBoxGridItem.SelectedIndexChanged += comboBoxGridItem_SelectedIndexChanged;
			comboBoxSysDoc.SelectedIndexChanged += comboBoxSysDoc_SelectedIndexChanged;
			comboBoxGridProductUnit.SelectedIndexChanged += comboBoxGridProductUnit_SelectedIndexChanged;
			dateTimePickerDate.ValueChanged += dateTimePickerDate_ValueChanged;
			comboBoxLocation.SelectedIndexChanged += comboBoxLocation_SelectedIndexChanged;
			comboBoxDismantle.SelectedIndexChanged += comboBoxRepacking_SelectedIndexChanged;
			textBoxQuantity.Validating += textBoxQuantity_Validating;
			textBoxQuantity.MouseClick += textBoxQuantity_MouseClicked;
			textBoxCost.Validated += textBoxCost_Validated;
			textBoxQuantity.Leave += textBoxQuantity_Leave;
			textBoxQuantity.TextChanged += textBoxQuantity_TextChanged;
			base.KeyDown += SalesOrderForm_KeyDown;
			dataGridItems.ClickCell += dataGridItems_ClickCell;
		}

		private void comboBoxRepacking_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(comboBoxDismantle.SelectedID))
			{
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Product", "LastCost", "ProductID", comboBoxDismantle.SelectedID);
				decimal result = default(decimal);
				decimal.TryParse(fieldValue.ToString(), out result);
				textBoxUnitCost.Text = result.ToString(Format.TotalAmountFormat);
				textBoxCost.Text = result.ToString(Format.TotalAmountFormat);
			}
			textBoxQuantity.Text = "1";
			textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Regular);
			textBoxQuantity.Tag = null;
		}

		private void textBoxQuantity_Validating(object sender, CancelEventArgs e)
		{
			decimal result = default(decimal);
			decimal.TryParse(textBoxQuantity.Text, out result);
			if (textBoxQuantity.Text != "" && result != 0m && result != oldQty)
			{
				if (!AllocateQuantityToLot(textBoxQuantity.Text))
				{
					e.Cancel = true;
					return;
				}
			}
			else if (result == 0m)
			{
				textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Regular);
				textBoxQuantity.Tag = null;
			}
			oldQty = result;
		}

		private void dataGridItems_ClickCell(object sender, ClickCellEventArgs e)
		{
			if (e.Cell.Appearance.FontData.Underline == DefaultableBoolean.True && e.Cell.Column.Key == "Quantity")
			{
				AllocateQuantityToLotDismantle(e.Cell);
			}
			UltraGridRow activeRow = dataGridItems.ActiveRow;
			if (dataGridItems.Rows.Count > 0 && dataGridItems.ActiveRow.Cells["Item code"].Value != null)
			{
				activeRow.Cells["Item code"].Value.ToString();
			}
		}

		private bool AllocateQuantityToLotDismantle(UltraGridCell cell)
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
			checked
			{
				if (dataGridItems.ActiveRow.Cells["ItemType"].Value != null && !string.IsNullOrWhiteSpace(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString()))
				{
					_ = (byte)int.Parse(dataGridItems.ActiveRow.Cells["ItemType"].Value.ToString());
				}
				if (result)
				{
					if (comboBoxLocation.SelectedID == "")
					{
						ErrorHelper.InformationMessage("Please select a location first.");
						return false;
					}
					ReceiveLotSelectionForm receiveLotSelectionForm = new ReceiveLotSelectionForm();
					receiveLotSelectionForm.ProductID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
					receiveLotSelectionForm.ProductDescription = dataGridItems.ActiveRow.Cells["Description"].Value.ToString();
					receiveLotSelectionForm.LocationID = comboBoxLocation.SelectedID;
					receiveLotSelectionForm.RowQuantity = float.Parse(dataGridItems.ActiveCell.Text);
					if (!isNewRecord)
					{
						receiveLotSelectionForm.SysDocID = comboBoxSysDoc.SelectedID;
						receiveLotSelectionForm.VoucherID = textBoxVoucherNumber.Text;
					}
					if (comboBoxLocation.SelectedID != "")
					{
						receiveLotSelectionForm.ProductLotTable = (cell.Tag as DataTable);
					}
					if (receiveLotSelectionForm.ShowDialog() != DialogResult.OK)
					{
						return false;
					}
					cell.Tag = receiveLotSelectionForm.ProductLotTable;
					cell.Appearance.FontData.Underline = DefaultableBoolean.True;
				}
				return true;
			}
		}

		private bool AllocateQuantityToLot(string strQuantity)
		{
			bool result = false;
			bool result2 = false;
			ItemTypes itemTypes = ItemTypes.Inventory;
			if (comboBoxDismantle.GetSelectedCellValue("IsTrackLot") != null)
			{
				bool.TryParse(comboBoxDismantle.GetSelectedCellValue("IsTrackLot").ToString(), out result);
			}
			if (comboBoxDismantle.GetSelectedCellValue("IsTrackSerial") != null)
			{
				bool.TryParse(comboBoxDismantle.GetSelectedCellValue("IsTrackSerial").ToString(), out result2);
			}
			if (comboBoxDismantle.GetSelectedCellValue("ItemType") != null && !string.IsNullOrWhiteSpace(comboBoxDismantle.GetSelectedCellValue("ItemType").ToString()))
			{
				itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxDismantle.GetSelectedCellValue("ItemType").ToString()));
			}
			if (result || itemTypes == ItemTypes.ConsignmentItem)
			{
				if (comboBoxLocation.SelectedID == "")
				{
					ErrorHelper.InformationMessage("Please select a location first.");
					return false;
				}
				IssueLotSelectionForm issueLotSelectionForm = new IssueLotSelectionForm();
				issueLotSelectionForm.ProductID = comboBoxDismantle.SelectedID;
				issueLotSelectionForm.ProductDescription = textBoxAssemblyName.Text;
				issueLotSelectionForm.LocationID = comboBoxLocation.SelectedID;
				issueLotSelectionForm.RowQuantity = float.Parse(textBoxQuantity.Text);
				if (!isNewRecord)
				{
					issueLotSelectionForm.SysDocID = comboBoxSysDoc.SelectedID;
					issueLotSelectionForm.VoucherID = textBoxVoucherNumber.Text;
				}
				if (textBoxQuantity.Tag != null)
				{
					issueLotSelectionForm.ProductLotTable = (DataTable)textBoxQuantity.Tag;
				}
				if (issueLotSelectionForm.ShowDialog() != DialogResult.OK)
				{
					return false;
				}
				textBoxQuantity.Tag = issueLotSelectionForm.ProductLotTable;
				Font font = new Font(textBoxQuantity.Font, FontStyle.Underline);
				textBoxQuantity.Font = font;
			}
			return true;
		}

		private void dataGridExpense_CellDataError(object sender, CellDataErrorEventArgs e)
		{
			if (dataGridExpense.ActiveCell.Column.Key == "Expense Code")
			{
				ErrorHelper.InformationMessage("Please select a valid item.");
				e.RaiseErrorEvent = false;
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
					comboBoxGridItem.SelectedID = dataGridItems.ActiveCell.Text;
					comboSearchDialogNew.SelectedItem = comboBoxGridItem.SelectedID;
				}
				comboSearchDialogNew.SelectedProvider = "";
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

		private void textBoxQuantity_Leave(object sender, EventArgs e)
		{
		}

		private void textBoxQuantity_TextChanged(object sender, EventArgs e)
		{
			decimal result = default(decimal);
			decimal result2 = default(decimal);
			decimal.TryParse(textBoxQuantity.Text, out result);
			decimal.TryParse(textBoxUnitCost.Text, out result2);
			textBoxCost.Text = (result * result2).ToString(Format.GridAmountFormat);
		}

		private void textBoxCost_Validated(object sender, EventArgs e)
		{
			checked
			{
				for (int i = 0; i <= dataGridItems.Rows.Count - 1; i++)
				{
					decimal num = default(decimal);
					if (!string.IsNullOrEmpty(textBoxCost.Text))
					{
						num = decimal.Parse(textBoxCost.Text);
					}
					if (dataGridItems.Rows[i].Cells["Percent"].Value.ToString() != "" && num > 0m)
					{
						decimal d = decimal.Parse(dataGridItems.Rows[i].Cells["Percent"].Value.ToString());
						decimal d2 = decimal.Parse(dataGridItems.Rows[i].Cells["Cost"].Value.ToString());
						if (d > 0m)
						{
							d2 = d * num / 100m;
							dataGridItems.Rows[i].Cells["Cost"].Value = d2;
						}
						else if (d2 > 0m)
						{
							d = d2 * 100m / num;
							dataGridItems.Rows[i].Cells["Percent"].Value = d;
						}
						else
						{
							dataGridItems.Rows[i].Cells["Cost"].Value = 0;
							dataGridItems.Rows[i].Cells["Percent"].Value = 0;
						}
					}
				}
			}
		}

		private void textBoxQuantity_MouseClicked(object sender, EventArgs e)
		{
			if (textBoxQuantity.Text != "")
			{
				decimal result = default(decimal);
				decimal.TryParse(textBoxQuantity.Text, out result);
				if (result > 0m && textBoxQuantity.Font.Underline && !AllocateQuantityToLot(textBoxQuantity.Text))
				{
					return;
				}
			}
			CalculateTotalQuantity();
		}

		private void textBoxQuantity_Enter(object sender, EventArgs e)
		{
		}

		private void textBoxQuantity_TabStopChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxBOM_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				BOMData bOMByID = Factory.BOMSystem.GetBOMByID(comboBoxBOM.SelectedID);
				(dataGridExpense.DataSource as DataTable).Clear();
				DataTable dataTable = dataGridItems.DataSource as DataTable;
				dataTable.Rows.Clear();
				new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 23, 59, 59);
				int num = (textBoxQuantity.Text.Trim() != "") ? int.Parse(textBoxQuantity.Text) : 0;
				foreach (DataRow row in bOMByID.Tables["BOM_Detail"].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Item Code"] = row["ProductID"];
					dataRow2["Description"] = row["Description"];
					dataRow2["ItemType"] = row["ItemType"];
					dataRow2["Unit"] = row["UnitID"];
					dataRow2["Onhand"] = 0;
					int num2 = 0;
					decimal num3 = default(decimal);
					if (row["Quantity"] != null && row["Quantity"].ToString() != "")
					{
						num2 = int.Parse(row["Quantity"].ToString());
					}
					if (row["Cost"] != null && row["Cost"].ToString() != "")
					{
						num3 = int.Parse(row["Cost"].ToString());
					}
					dataRow2["Quantity"] = checked(num2 * num);
					dataRow2["Cost"] = num3.ToString();
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				FillProductQuantityAndCost();
				CalculateTotalCost();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void comboBoxAssembly_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (comboBoxDismantle.SelectedRow != null && comboBoxDismantle.SelectedRow.Cells.Exists("BOMID"))
				{
					comboBoxBOM.SelectedID = comboBoxDismantle.SelectedRow.Cells["BOMID"].Value.ToString();
				}
				else
				{
					comboBoxBOM.Clear();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dateTimePickerDate_ValueChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxLocation_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void FillProductQuantityAndCost()
		{
			try
			{
				decimal d = default(decimal);
				decimal num = default(decimal);
				if (dataGridItems.Rows.Count != 0)
				{
					ArrayList arrayList = new ArrayList();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["Item Code"].Value != null && !(row.Cells["Item Code"].Value.ToString() == ""))
						{
							string value = row.GetCellValue("Item Code").ToString();
							arrayList.Add(value);
						}
					}
					new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 23, 59, 59);
					DataSet dataSet = null;
					DataTable dataTable = null;
					if (dataSet != null && dataSet.Tables.Count > 0)
					{
						dataTable = dataSet.Tables[0];
					}
					foreach (UltraGridRow row2 in dataGridItems.Rows)
					{
						if (row2.Cells["Item Code"].Value != null && !(row2.Cells["Item Code"].Value.ToString() == ""))
						{
							string str = row2.Cells["Item Code"].Value.ToString();
							decimal result = default(decimal);
							decimal.TryParse(row2.GetCellValue("Quantity").ToString(), out result);
							DataRow dataRow = null;
							DataRow[] array = null;
							if (dataTable != null)
							{
								array = dataTable.Select("ProductID = '" + str + "'");
							}
							if (array != null && array.Length != 0)
							{
								dataRow = array[0];
							}
							if (dataRow != null)
							{
								if (dataRow["Quantity"] != DBNull.Value)
								{
									row2.Cells["Onhand"].Value = dataRow["Quantity"].ToString();
								}
								else
								{
									row2.Cells["Onhand"].Value = 0;
								}
								if (result < 0m)
								{
									if (dataRow["AverageCost"] != DBNull.Value)
									{
										row2.Cells["Cost"].Value = dataRow["AverageCost"].ToString();
									}
									else
									{
										row2.Cells["Cost"].Value = 0;
									}
								}
							}
							else
							{
								row2.Cells["Onhand"].Value = 0;
								if (result < 0m)
								{
									row2.Cells["Cost"].Value = 0;
								}
							}
							if (row2.Cells["Quantity"].Value != null && row2.Cells["Quantity"].Value.ToString() != "")
							{
								d = decimal.Parse(row2.Cells["Quantity"].Value.ToString());
							}
							num = decimal.Parse(row2.Cells["Cost"].Value.ToString());
							row2.Cells["Total"].Value = (d * num).ToString();
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void CalculateTotalQuantity()
		{
		}

		private void CalculateTotalCost()
		{
			try
			{
				decimal result = default(decimal);
				decimal num = default(decimal);
				decimal num2 = dataGridExpense.Rows.Where((UltraGridRow x) => x.Cells["Amount"].Value.ToString() != "").Sum((UltraGridRow x) => decimal.Parse(x.Cells["Amount"].Value.ToString()));
				decimal.TryParse(textBoxCost.Text, out result);
				result += num2;
				IEnumerable<UltraGridRow> enumerable = dataGridItems.Rows.Where((UltraGridRow x) => x.Cells["Percent"].Value.ToString() != "");
				if (enumerable.Count() > 0)
				{
					foreach (UltraGridRow item in enumerable)
					{
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal.TryParse(item.Cells["Percent"].Value.ToString(), out result2);
						decimal.TryParse(item.Cells["Quantity"].Value.ToString(), out result3);
						num = Math.Round(result * result2 / 100m, Global.CurDecimalPoints);
						item.Cells["Total"].Value = num;
						if (result3 != 0m)
						{
							item.Cells["Cost"].Value = (num / result3).ToString(Format.GridAmountFormat);
							item.Cells["Total"].Value = num;
						}
						else
						{
							item.Cells["Total"].Value = 0;
							item.Cells["Cost"].Value = 0;
						}
					}
				}
				else
				{
					enumerable = dataGridItems.Rows.Where((UltraGridRow x) => x.Cells["Percent"].Value.ToString() == "");
					int value = enumerable.Count();
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						decimal result4 = default(decimal);
						decimal.TryParse(row.Cells["Quantity"].Value.ToString(), out result4);
						num = Math.Round(result / (decimal)value, Global.CurDecimalPoints);
						row.Cells["Total"].Value = num;
						if (result4 != 0m)
						{
							row.Cells["Cost"].Value = (num / result4).ToString(Format.GridAmountFormat);
							row.Cells["Total"].Value = num;
						}
						else
						{
							row.Cells["Total"].Value = 0;
							row.Cells["Cost"].Value = 0;
						}
					}
				}
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
			comboBoxGridItem.FilterSysDocID = comboBoxSysDoc.SelectedID;
		}

		private void dataGridItems_AfterCellUpdate(object sender, CellEventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			if (e.Cell.Column.Key == "Item Code")
			{
				ItemTypes itemTypes = ItemTypes.None;
				if (comboBoxGridItem.SelectedRow != null && !(comboBoxGridItem.SelectedID == "") && comboBoxGridItem.SelectedRow.Cells["ItemType"].Value != null)
				{
					itemTypes = (ItemTypes)checked((byte)int.Parse(comboBoxGridItem.SelectedRow.Cells["ItemType"].Value.ToString()));
				}
				if (itemTypes == ItemTypes.Matrix)
				{
					MatrixSelectionForm matrixSelectionForm = new MatrixSelectionForm();
					matrixSelectionForm.LoadMatrixData(comboBoxGridItem.SelectedID, comboBoxGridItem.SelectedName);
					dataGridItems.ActiveRow.Delete(displayPrompt: false);
					if (matrixSelectionForm.ShowDialog(this) == DialogResult.OK)
					{
						foreach (DataRow row in matrixSelectionForm.SelectedItems.Tables[0].Rows)
						{
							UltraGridRow ultraGridRow = dataGridItems.DisplayLayout.Bands[0].AddNew();
							ultraGridRow.Cells["Item Code"].Value = row["ProductID"].ToString();
							ultraGridRow.Cells["Description"].Value = row["Description"].ToString();
							ultraGridRow.Cells["Quantity"].Value = row["Quantity"].ToString();
							dataGridItems.ActiveRow.Cells["Quantity"].Tag = null;
							ultraGridRow.Update();
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal.TryParse(ultraGridRow.Cells["Quantity"].Value.ToString(), out result);
							decimal.TryParse(ultraGridRow.Cells["Onhand"].Value.ToString(), out result2);
							if (result < 0m)
							{
								ultraGridRow.Cells["Cost"].Activation = Activation.Disabled;
							}
							else
							{
								ultraGridRow.Cells["Cost"].Activation = Activation.AllowEdit;
							}
						}
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							_ = row2;
						}
					}
					return;
				}
				dataGridItems.ActiveRow.Cells["Description"].Value = comboBoxGridItem.SelectedName;
				dataGridItems.ActiveRow.Cells["Unit"].Value = comboBoxGridItem.SelectedUnitID;
				if (dataGridItems.ActiveRow.Cells["Item Code"].ToString() != "")
				{
					dataGridItems.ActiveRow.Cells["ItemType"].Value = (int)comboBoxGridItem.SelectedItemType;
				}
				else
				{
					dataGridItems.ActiveRow.Cells["ItemType"].Value = 1;
				}
				dataGridItems.ActiveRow.Cells["Location"].Value = comboBoxLocation.SelectedID;
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
				decimal d = default(decimal);
				checked
				{
					if (isNewRecord)
					{
						for (int num = dataGridItems.Rows.Count - 1; num >= 0; num--)
						{
							if (dataGridItems.Rows[num].Cells["Percent"].Value.ToString() != "")
							{
								string s = dataGridItems.Rows[num].Cells["Percent"].Value.ToString();
								d += decimal.Parse(s);
							}
						}
						dataGridItems.ActiveRow.Cells["Percent"].Value = 100m - d;
					}
					try
					{
						DateTime date = new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, 11, 59, 59);
						DataSet productQuantityAndCostAsOfDate = Factory.ProductSystem.GetProductQuantityAndCostAsOfDate(comboBoxGridItem.SelectedID, dataGridItems.ActiveRow.Cells["Location"].Value.ToString(), date);
						DataRow dataRow2 = null;
						if (productQuantityAndCostAsOfDate != null && productQuantityAndCostAsOfDate.Tables.Count > 0 && productQuantityAndCostAsOfDate.Tables[0].Rows.Count > 0)
						{
							dataRow2 = productQuantityAndCostAsOfDate.Tables[0].Rows[0];
						}
						if (dataRow2 != null)
						{
							dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
						}
						else
						{
							dataGridItems.ActiveRow.Cells["Onhand"].Value = 0;
							dataGridItems.ActiveRow.Cells["Quantity"].Value = 0;
							dataGridItems.ActiveRow.Cells["Cost"].Value = 0;
							dataGridItems.ActiveRow.Cells["Total"].Value = 0.ToString(Format.TotalAmountFormat);
						}
					}
					catch (Exception e2)
					{
						ErrorHelper.ProcessError(e2);
					}
				}
			}
			else if (e.Cell.Column.Key == "Quantity")
			{
				CalculateTotalCost();
			}
			else if (isNewRecord && e.Cell.Column.Key == "Onhand" && e.Cell.Row.Cells["Quantity"].Value != null && e.Cell.Row.Cells["Quantity"].Value.ToString() != "")
			{
				float result3 = 0f;
				float result4 = 0f;
				float.TryParse(e.Cell.Row.Cells["Quantity"].Text.ToString(), out result4);
				float.TryParse(e.Cell.Row.Cells["Onhand"].Value.ToString(), out result3);
				CalculateTotalCost();
			}
			if (!(e.Cell.Column.Key == "Percent"))
			{
				return;
			}
			decimal d2 = default(decimal);
			if (dataGridItems.ActiveRow.Index >= 0)
			{
				for (int i = 0; i < dataGridItems.Rows.Count; i = checked(i + 1))
				{
					if (dataGridItems.Rows[i].Cells["Percent"].Value.ToString() != "")
					{
						d2 += decimal.Parse(dataGridItems.Rows[i].Cells["Percent"].Value.ToString());
					}
					if (d2 > 100m)
					{
						ErrorHelper.InformationMessage("Invalid Percent");
						e.Cell.Value = percentage;
					}
				}
			}
			CalculateTotalCost();
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
			if (activeRow != null)
			{
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					if (!row.IsActiveRow && row.Cells["Item Code"].Value == dataGridItems.ActiveRow.Cells["Item Code"].Value)
					{
						ErrorHelper.InformationMessage("This item is already in the list.");
						e.Cancel = true;
						return;
					}
				}
				if (activeRow.Cells["Item Code"].Value.ToString() == "" && activeRow.Cells["Onhand"].Value.ToString() != "")
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
			if (e.Cell != null && e.Cell.Column.Key == "Item Code")
			{
				string selectedID = comboBoxGridItem.SelectedID;
				if (selectedID != "" && selectedID == comboBoxDismantle.SelectedID)
				{
					ErrorHelper.InformationMessage("This item is same as the Dismantled item and cannot be selected.");
					e.Cancel = true;
					e.Cell.Activate();
					return;
				}
			}
			if (e.Cell.Column.Key == "Quantity" && dataGridItems.ActiveCell != null && dataGridItems.ActiveCell.Column.Key == "Quantity" && !AllocateQuantityToLotDismantle(e.Cell))
			{
				e.Cancel = true;
			}
			if (e.Cell != null && e.Cell.Column.Key == "Percentage")
			{
				decimal.TryParse(e.Cell.Value.ToString(), out percentage);
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
			else if (dataGridItems.ActiveCell.Column.Key.ToString() == "Quantity")
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
					}
					if (e.Cell.Column.Key == "Amount")
					{
						CalculateTotalCost();
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void dataGridItems_HeaderClicked(object sender, EventArgs e)
		{
			UltraGridColumn ultraGridColumn = sender as UltraGridColumn;
			if (dataGridItems.ActiveRow != null && ultraGridColumn != null && ultraGridColumn.Key == "Item Code")
			{
				contextMenuStrip2.Show(dataGridItems, new Point(0, 20), ToolStripDropDownDirection.BelowRight);
			}
		}

		private void dataGridExpense_BeforeRowDeactivate(object sender, CancelEventArgs e)
		{
			UltraGridRow activeRow = dataGridExpense.ActiveRow;
			if (activeRow != null && string.IsNullOrEmpty(activeRow.Cells["Amount"].Value.ToString()))
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
			}
		}

		private void dataGridExpense_AfterRowsDeleted(object sender, EventArgs e)
		{
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new InventoryDismantleData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.InventoryDismantleTable.Rows[0] : currentData.InventoryDismantleTable.NewRow();
				if (dateTimePickerDate.Value > DateTime.Today)
				{
					dataRow["TransactionDate"] = dateTimePickerDate.Value;
				}
				else
				{
					DateTime value = dateTimePickerDate.Value;
					dataRow["TransactionDate"] = new DateTime(value.Year, value.Month, value.Day, 11, 59, 59);
				}
				dataRow["SysDocID"] = comboBoxSysDoc.SelectedID;
				dataRow["VoucherID"] = textBoxVoucherNumber.Text;
				dataRow["CompanyID"] = Global.CompanyID;
				dataRow["DivisionID"] = comboBoxSysDoc.DivisionID;
				dataRow["LocationID"] = comboBoxLocation.SelectedID;
				dataRow["DimantledProductID"] = comboBoxDismantle.SelectedID;
				dataRow["Reference"] = textBoxRef1.Text;
				dataRow["Description"] = textBoxNote.Text;
				dataRow["QuantityDismantled"] = textBoxQuantity.Text;
				dataRow["UnitCost"] = textBoxCost.Text;
				dataRow.EndEdit();
				if (IsNewRecord)
				{
					currentData.InventoryDismantleTable.Rows.Add(dataRow);
				}
				currentData.InventoryDismantleDetailTable.Rows.Clear();
				currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
				currentData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
				foreach (UltraGridRow row in dataGridItems.Rows)
				{
					DataRow dataRow2 = currentData.InventoryDismantleDetailTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["ProductID"] = row.Cells["Item Code"].Value.ToString();
					if (row.Cells["Quantity"].Value != null && row.Cells["Quantity"].Value.ToString() != "")
					{
						dataRow2["Quantity"] = row.Cells["Quantity"].Value.ToString();
					}
					if (row.Cells["Cost"].Value != null && row.Cells["Cost"].Value.ToString() != "")
					{
						dataRow2["Cost"] = row.Cells["Cost"].Value.ToString();
					}
					if (row.Cells["Total"].Value != null && row.Cells["Total"].Value.ToString() != "")
					{
						dataRow2["Total"] = row.Cells["Total"].Value.ToString();
					}
					if (row.Cells["Percent"].Value != null && row.Cells["Percent"].Value.ToString() != "")
					{
						dataRow2["Cost_Percent"] = row.Cells["Percent"].Value.ToString();
					}
					dataRow2["Description"] = row.Cells["Description"].Value.ToString();
					dataRow2["LocationID"] = row.Cells["Location"].Value.ToString();
					dataRow2["RowIndex"] = row.Index;
					dataRow2["FactorType"] = DBNull.Value;
					if (row.Cells["Unit"].Value != null && row.Cells["Unit"].Value.ToString() != "")
					{
						dataRow2["UnitID"] = row.Cells["Unit"].Value.ToString();
					}
					dataRow2.EndEdit();
					currentData.InventoryDismantleDetailTable.Rows.Add(dataRow2);
					if (row.Cells["Quantity"].Tag != null)
					{
						DataTable dataTable = row.Cells["Quantity"].Tag as DataTable;
						if (dataTable != null && dataTable.Rows.Count > 0)
						{
							foreach (DataRow row2 in dataTable.Rows)
							{
								_ = comboBoxLocation.SelectedID;
								row.Cells["Item Code"].Value.ToString();
								DataRow dataRow4 = currentData.Tables["Product_Lot_Receiving_Detail"].NewRow();
								dataRow4["ProductID"] = row2["ProductID"];
								dataRow4["LocationID"] = row2["LocationID"];
								dataRow4["LotNumber"] = row2["LotNumber"];
								dataRow4["Reference"] = row2["Reference"];
								dataRow4["SourceLotNumber"] = row2["SourceLotNumber"];
								dataRow4["SoldQty"] = row2["SoldQty"];
								dataRow4["SysDocID"] = comboBoxSysDoc.SelectedID;
								dataRow4["VoucherID"] = textBoxVoucherNumber.Text;
								dataRow4["BinID"] = row2["BinID"];
								dataRow4["Reference2"] = row2["Reference2"];
								dataRow4["LotQty"] = row2["LotQty"];
								dataRow4["ProductionDate"] = row2["ProductionDate"];
								dataRow4["ExpiryDate"] = row2["ExpiryDate"];
								dataRow4["ReceiptDate"] = row2["ReceiptDate"];
								dataRow4["RowIndex"] = row.Index;
								currentData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow4);
							}
						}
					}
				}
				if (textBoxQuantity.Tag != null)
				{
					string selectedID = comboBoxLocation.SelectedID;
					foreach (DataRow row3 in (textBoxQuantity.Tag as DataTable).Rows)
					{
						string text = row3["ProductID"].ToString();
						if (row3["LocationID"].ToString() != selectedID || row3["ProductID"].ToString() != text)
						{
							ErrorHelper.WarningMessage("Location or Product code mismatch with selected lots for item code:", "'" + text + "'", "Please reallocate the lots for this item.");
							return false;
						}
						DataRow dataRow6 = currentData.Tables["Product_Lot_Issue_Detail"].NewRow();
						dataRow6["ProductID"] = row3["ProductID"];
						dataRow6["LocationID"] = selectedID;
						dataRow6["LotNumber"] = row3["LotNumber"];
						dataRow6["Reference"] = row3["Reference"];
						dataRow6["SourceLotNumber"] = row3["SourceLotNumber"];
						dataRow6["BinID"] = row3["BinID"];
						dataRow6["Reference2"] = row3["Reference2"];
						dataRow6["SoldQty"] = row3["SoldQty"];
						dataRow6["SysDocID"] = comboBoxSysDoc.SelectedID;
						dataRow6["VoucherID"] = textBoxVoucherNumber.Text;
						dataRow6["UnitPrice"] = 0;
						dataRow6["RowIndex"] = -1;
						currentData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow6);
					}
				}
				currentData.InventoryDismantleExpenseTable.Rows.Clear();
				foreach (UltraGridRow row4 in dataGridExpense.Rows)
				{
					DataRow dataRow7 = currentData.InventoryDismantleExpenseTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["ExpenseID"] = row4.Cells["Expense Code"].Value.ToString();
					dataRow7["Description"] = row4.Cells["Description"].Value.ToString();
					dataRow7["Reference"] = row4.Cells["Reference"].Value.ToString();
					dataRow7["Amount"] = row4.Cells["Amount"].Value.ToString();
					dataRow7.EndEdit();
					currentData.InventoryDismantleExpenseTable.Rows.Add(dataRow7);
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
				dataTable.Columns.Add("ItemType");
				dataTable.Columns.Add("Location");
				dataTable.Columns.Add("IsTrackLot");
				dataTable.Columns.Add("IsTrackSerial");
				dataTable.Columns.Add("Unit");
				dataTable.Columns.Add("Quantity", typeof(decimal));
				dataTable.Columns.Add("Onhand", typeof(decimal));
				dataTable.Columns.Add("Percent", typeof(decimal));
				dataTable.Columns.Add("Cost", typeof(decimal));
				dataTable.Columns.Add("Total", typeof(decimal));
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].MaxLength = 64;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].ValueList = comboBoxGridItem;
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.ForeColor = Color.FromArgb(0, 0, 255);
				dataGridItems.DisplayLayout.Bands[0].Columns["Item Code"].Header.Appearance.Cursor = Cursors.Hand;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Unit"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].ValueList = comboBoxGridLocation;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].InvalidValueBehavior = InvalidValueBehavior.RetainValueAndFocus;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].CharacterCasing = CharacterCasing.Upper;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].MaxLength = 15;
				dataGridItems.DisplayLayout.Bands[0].Columns["Location"].DefaultCellValue = Security.DefaultInventoryLocationID;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Percent"].MaxLength = 20;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percent"].MinValue = 0;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percent"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percent"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Percent"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].MaxLength = 255;
				dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].Format = "n";
				dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].CellAppearance.ForeColorDisabled = Color.Black;
				UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				UltraGridColumn ultraGridColumn2 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"];
				bool flag2 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].Hidden = true;
				bool hidden = ultraGridColumn2.Hidden = flag2;
				ultraGridColumn.Hidden = hidden;
				UltraGridColumn ultraGridColumn3 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackLot"];
				UltraGridColumn ultraGridColumn4 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"];
				ExcludeFromColumnChooser excludeFromColumnChooser2 = dataGridItems.DisplayLayout.Bands[0].Columns["IsTrackSerial"].ExcludeFromColumnChooser = ExcludeFromColumnChooser.True;
				ExcludeFromColumnChooser excludeFromColumnChooser5 = ultraGridColumn3.ExcludeFromColumnChooser = (ultraGridColumn4.ExcludeFromColumnChooser = excludeFromColumnChooser2);
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].MaxValue = 99999999m;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Format = "#,##0.00##";
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].Header.Caption = "Amount";
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Cost"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellActivation = Activation.Disabled;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.BackColorDisabled = Color.WhiteSmoke;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].CellAppearance.ForeColorDisabled = Color.Black;
				dataGridItems.DisplayLayout.Bands[0].Columns["Total"].Format = "#,##0.00##";
				UltraGridColumn ultraGridColumn5 = dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"];
				hidden = (dataGridItems.DisplayLayout.Bands[0].Columns["Onhand"].Hidden = true);
				ultraGridColumn5.Hidden = hidden;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridItems.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
				ValueList valueList = new ValueList();
				valueList.ValueListItems.Add(0, "None");
				valueList.ValueListItems.Add(1, "Inventory");
				valueList.ValueListItems.Add(2, "Non-Inventory");
				valueList.ValueListItems.Add(3, "Service");
				valueList.ValueListItems.Add(4, "Discount");
				valueList.ValueListItems.Add(5, "Consignment");
				valueList.ValueListItems.Add(6, "Matrix");
				valueList.ValueListItems.Add(7, "Assembly");
				dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ValueList = valueList;
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("TotalQty", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Quantity"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["TotalQty"].DisplayFormat = "{0:n}";
				dataGridItems.DisplayLayout.Bands[0].Summaries.Add("Value", SummaryType.Sum, dataGridItems.DisplayLayout.Bands[0].Columns["Total"], SummaryPosition.UseSummaryPositionColumn);
				dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].Appearance.BackColor = Color.White;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].Appearance.TextHAlign = HAlign.Right;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
				dataGridItems.DisplayLayout.Bands[0].Summaries["Value"].DisplayFormat = "{0:n}";
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
				dataGridExpense.DisplayLayout.Bands[0].Columns["Expense Code"].Width = 50;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Reference"].Width = 100;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"].Width = 50;
				dataGridExpense.DisplayLayout.Bands[0].Columns["Description"].Width = checked(40 * dataGridItems.Width) / 100;
				dataGridExpense.DisplayLayout.Bands[0].Summaries.Add("TotalAmount", SummaryType.Sum, dataGridExpense.DisplayLayout.Bands[0].Columns["Amount"], SummaryPosition.UseSummaryPositionColumn);
			}
			catch
			{
			}
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
					currentData = Factory.InventoryDismantleSystem.GetInventoryDismantleByID(SystemDocID, voucherID);
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
					DataRow dataRow = currentData.Tables["Inventory_Dismantle"].Rows[0];
					comboBoxSysDoc.DivisionID = dataRow["DivisionID"].ToString();
					dateTimePickerDate.Value = DateTime.Parse(dataRow["TransactionDate"].ToString());
					textBoxVoucherNumber.Text = dataRow["VoucherID"].ToString();
					textBoxRef1.Text = dataRow["Reference"].ToString();
					comboBoxLocation.SelectedID = dataRow["LocationID"].ToString();
					comboBoxDismantle.SelectedID = dataRow["DimantledProductID"].ToString();
					textBoxCost.Text = decimal.Parse(dataRow["UnitCost"].ToString()).ToString(Format.GridAmountFormat);
					textBoxNote.Text = dataRow["Description"].ToString();
					textBoxQuantity.Text = decimal.Parse(dataRow["QuantityDismantled"].ToString()).ToString(Format.QuantityFormat);
					if (dataRow["QuantityDismantled"] != DBNull.Value)
					{
						oldQty = decimal.Parse(dataRow["QuantityDismantled"].ToString());
					}
					DataTable dataTable = dataGridItems.DataSource as DataTable;
					dataTable.Rows.Clear();
					if (currentData.Tables.Contains("Inventory_Dismantle_Detail") && currentData.InventoryDismantleDetailTable.Rows.Count != 0)
					{
						dataGridItems.BeginUpdate();
						foreach (DataRow row in currentData.Tables["Inventory_Dismantle_Detail"].Rows)
						{
							DataRow dataRow3 = dataTable.NewRow();
							dataRow3["Item Code"] = row["ProductID"];
							dataRow3["Quantity"] = row["Quantity"];
							dataRow3["Cost"] = row["Cost"];
							if (!string.IsNullOrEmpty(row["Cost_Percent"].ToString()))
							{
								dataRow3["Percent"] = row["Cost_Percent"];
							}
							dataRow3["Description"] = row["Description"];
							dataRow3["Location"] = row["LocationID"];
							dataRow3["ItemType"] = row["FactorType"];
							dataRow3["Unit"] = row["UnitID"];
							dataRow3["IsTrackLot"] = row["IsTrackLot"];
							dataRow3["IsTrackSerial"] = row["IsTrackSerial"];
							dataRow3.EndEdit();
							dataTable.Rows.Add(dataRow3);
						}
						dataTable.AcceptChanges();
						foreach (UltraGridRow row2 in dataGridItems.Rows)
						{
							if (row2.Cells["IsTrackLot"].Value != DBNull.Value && bool.Parse(row2.Cells["IsTrackLot"].Value.ToString()))
							{
								DataRow[] array = currentData.Tables["Product_Lot_Receiving_Detail"].Select("ProductID = '" + row2.Cells["Item Code"].Value.ToString() + "' AND RowIndex = " + row2.Index);
								if (array.Length != 0)
								{
									DataSet dataSet = new DataSet();
									dataSet.Merge(array);
									DataTable tag = dataSet.Tables[0];
									row2.Cells["Quantity"].Tag = tag;
									row2.Cells["Quantity"].Appearance.FontData.Underline = DefaultableBoolean.True;
								}
							}
							string productID = row2.Cells["Item Code"].Value.ToString();
							row2.Cells["Unit"].ValueList = comboBoxGridItem.GetProductUnitsValueList(productID);
						}
						if (comboBoxDismantle.SelectedID != "")
						{
							DataRow[] array2 = currentData.Tables["Product_Lot_Issue_Detail"].Select("ProductID = '" + comboBoxDismantle.SelectedID + "' AND RowIndex = -1 ");
							if (array2.Length != 0)
							{
								DataSet dataSet2 = new DataSet();
								dataSet2.Merge(array2);
								textBoxQuantity.Tag = dataSet2.Tables["Product_Lot_Issue_Detail"];
								textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Underline);
							}
							else
							{
								textBoxQuantity.Tag = null;
								textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Regular);
							}
						}
						else
						{
							textBoxQuantity.Tag = null;
							textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Regular);
						}
						DataTable dataTable2 = dataGridExpense.DataSource as DataTable;
						dataTable2.Rows.Clear();
						foreach (DataRow row3 in currentData.Tables["Inventory_Dismantle_Expense"].Rows)
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
						FillProductQuantityAndCost();
						CalculateTotalCost();
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
			dataGridExpense.PerformAction(UltraGridAction.ExitEditMode);
			dataGridItems.PerformAction(UltraGridAction.ExitEditMode);
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
			bool flag = false;
			try
			{
				bool flag2 = Factory.InventoryDismantleSystem.CreateInventoryDismantle(currentData, !isNewRecord);
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
			formManager.ShowApprovalPanel(approvalTaskID, "Inventory_Dismantle", "VoucherID");
		}

		private bool ValidateData()
		{
			if (!IsNewRecord && !Global.IsUserAdmin && !AllowEditTransaction && Global.CurrentUser != Factory.SystemDocumentSystem.GetTransUserID("Inventory_Dismantle", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (textBoxVoucherNumber.Text.Trim() == "" || comboBoxSysDoc.SelectedID == "" || comboBoxDismantle.SelectedID == "" || comboBoxLocation.SelectedID == "" || textBoxQuantity.Text == "")
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
						ErrorHelper.InformationMessage("Please select a location for all items.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
					if (dataGridItems.Rows[i].Cells["Quantity"].Value == null || string.IsNullOrWhiteSpace(dataGridItems.Rows[i].Cells["Quantity"].Value.ToString()))
					{
						ErrorHelper.InformationMessage("Please enter quantity for all items.");
						dataGridItems.Rows[i].Activate();
						return false;
					}
				}
				if (dataGridItems.Rows.Count == 0)
				{
					ErrorHelper.InformationMessage("There should be at least one adjustment row.");
					return false;
				}
				if (formManager.IsFieldDirty(textBoxVoucherNumber) && Factory.SystemDocumentSystem.ExistDocumentNumber("Inventory_Dismantle", "VoucherID", SystemDocID, textBoxVoucherNumber.Text))
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
				if (row.GetCellValue("Quantity") != null && row.GetCellValue("Quantity").ToString() != "")
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
				textBoxUnitCost.Clear();
				textBoxQuantity.Text = "0";
				textBoxCost.Clear();
				comboBoxGridItem.Clear();
				comboBoxDismantle.Clear();
				comboBoxLocation.Clear();
				comboBoxBOM.Clear();
				textBoxQuantity.Font = new Font(textBoxQuantity.Font, FontStyle.Regular);
				dateTimePickerDate.Value = DateTime.Now;
				comboBoxSysDoc.SetDefaultID(Security.DefaultTransactionLocationID);
				(dataGridItems.DataSource as DataTable).Rows.Clear();
				(dataGridExpense.DataSource as DataTable).Rows.Clear();
				IsVoid = false;
				formManager.ResetDirty();
				textBoxQuantity.Tag = null;
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
				return Factory.InventoryDismantleSystem.DeleteInventoryDismantle(SystemDocID, textBoxVoucherNumber.Text);
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
			string nextID = DatabaseHelper.GetNextID("Inventory_Dismantle", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(nextID == ""))
			{
				LoadData(nextID);
			}
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			string previousID = DatabaseHelper.GetPreviousID("Inventory_Dismantle", "VoucherID", textBoxVoucherNumber.Text, "SysDocID", SystemDocID);
			if (!(previousID == ""))
			{
				LoadData(previousID);
			}
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			string lastID = DatabaseHelper.GetLastID("Inventory_Dismantle", "VoucherID", "SysDocID", SystemDocID);
			if (!(lastID == ""))
			{
				LoadData(lastID);
			}
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			string firstID = DatabaseHelper.GetFirstID("Inventory_Dismantle", "VoucherID", "SysDocID", SystemDocID);
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
					string text = Factory.DatabaseSystem.FindDocumentByNumber("Inventory_Dismantle", "VoucherID", SystemDocID, toolStripTextBoxFind.Text);
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
				comboBoxSysDoc.FilterByType(SysDocTypes.InventoryDismantle);
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
			if (!Security.IsAllowedSecurityUser(GeneralSecurityRoles.ChangeInventoryLocation))
			{
				comboBoxLocation.ShowDefaultLocationOnly = true;
			}
			else
			{
				comboBoxLocation.ShowDefaultLocationOnly = false;
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

		private void ultraFormattedLinkLabel5_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditSysDoc(comboBoxSysDoc.SelectedID, SysDocTypes.InventoryDismantle);
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
						return Global.CompanySettings.SaveTransactionDraft(currentData, enterNameDialog.EnteredName, SysDocTypes.InventoryDismantle);
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
				DataSet settingsList = Factory.SettingSystem.GetSettingsList("", 232.ToString());
				SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
				selectDocumentDialog.DataSource = settingsList;
				selectDocumentDialog.Text = "Select Draft";
				selectDocumentDialog.IsMultiSelect = false;
				if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
				{
					string key = selectDocumentDialog.SelectedRow.Cells["Name"].Value.ToString();
					DataSet dataSet = Global.CompanySettings.LoadTransactionDraft(key, SysDocTypes.InventoryDismantle);
					currentData = (dataSet as InventoryDismantleData);
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
				if (!(IsDirty && saveChanges) || (ErrorHelper.QuestionMessage(MessageBoxButtons.YesNo, "You must save the document before printing.", "Do you want to save?") == DialogResult.Yes && SaveData(clearAfter: false)))
				{
					string selectedID = comboBoxSysDoc.SelectedID;
					string text = textBoxVoucherNumber.Text;
					DataSet inventoryDismantleToPrint = Factory.InventoryDismantleSystem.GetInventoryDismantleToPrint(selectedID, text);
					if (inventoryDismantleToPrint == null || inventoryDismantleToPrint.Tables.Count == 0)
					{
						ErrorHelper.ErrorMessage("Cannot print the document.", "Document not found.");
					}
					else
					{
						PrintHelper.PrintDocument(inventoryDismantleToPrint, selectedID, "Inventory Dismantle", SysDocTypes.InventoryDismantle, isPrint, showPrintDialog);
					}
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
			return "Inventory Repacking - Products List";
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
			new FormHelper().EditProduct(comboBoxDismantle.SelectedID);
		}

		private void toolStripButtonDistribution_Click(object sender, EventArgs e)
		{
			JournalDistibutionDialog journalDistibutionDialog = new JournalDistibutionDialog();
			journalDistibutionDialog.VoucherID = textBoxVoucherNumber.Text;
			journalDistibutionDialog.SysDocID = comboBoxSysDoc.SelectedID;
			journalDistibutionDialog.ShowDialog(this);
		}

		private void ultraFormattedLinkLabel3_LinkClicked(object sender, Infragistics.Win.FormattedLinkLabel.LinkClickedEventArgs e)
		{
			new FormHelper().EditLocation(comboBoxLocation.SelectedID);
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.InventoryDismantleListFormObj);
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

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
			if (!isNewRecord)
			{
				FormHelper.ShowDocumentInfo(textBoxVoucherNumber.Text, comboBoxSysDoc.SelectedID, this);
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

		private void availableQuantityToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string productID = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				FormActivator.ProductQuantityFormObj.LoadData(productID);
				FormActivator.BringFormToFront(FormActivator.ProductQuantityFormObj);
			}
		}

		private void salesStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void itemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null && dataGridItems.ActiveRow.Cells["Item Code"].Value != null && dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString() != "")
			{
				string id = dataGridItems.ActiveRow.Cells["Item Code"].Value.ToString();
				new FormHelper().EditItem(id);
			}
		}

		private void removeRowToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow != null)
			{
				dataGridItems.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void toolStripTextBoxFind_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == Convert.ToChar(Keys.Return))
			{
				toolStripButtonFind_Click(sender, e);
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
								CalculateTotalCost();
							}
							ItemTaxOptions itemTaxOptions = (ItemTaxOptions)byte.Parse(Factory.DatabaseSystem.GetFieldValue("Product", "TaxOption", "ProductID", text).ToString() ?? null);
							row.Cells["TaxOption"].Value = itemTaxOptions;
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
							row2.Cells["Amount"].Value = (d2 * d3).ToString(Format.TotalAmountFormat);
							if (row2.Cells["Amount"].Value.ToString() != "")
							{
								CalculateTotalCost();
							}
						}
					}
				}
			}
			CalculateTotalCost();
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Inventory.InventoryDismantleForm));
			tabPageItems = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			comboBoxGridProductUnit = new Micromind.DataControls.ProductUnitComboBox();
			ultraCalcManager1 = new Infragistics.Win.UltraWinCalcManager.UltraCalcManager(components);
			comboBoxGridItem = new Micromind.DataControls.ProductComboBox();
			label5 = new System.Windows.Forms.Label();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			tabPageExpense = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			dataGridExpense = new Micromind.DataControls.DataEntryGrid();
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
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPreview = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonDistribution = new System.Windows.Forms.ToolStripButton();
			toolStripButtonExcelImport = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
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
			textBoxQuantity = new Micromind.UISupport.NumberTextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxCost = new Micromind.UISupport.NumberTextBox();
			ultraFormattedLinkLabel3 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			comboBoxSysDoc = new Micromind.DataControls.SysDocComboBox();
			textBoxRepackName = new System.Windows.Forms.TextBox();
			comboBoxLocation = new Micromind.DataControls.LocationComboBox();
			label6 = new System.Windows.Forms.Label();
			comboBoxDismantle = new Micromind.DataControls.ProductComboBox();
			textBoxAssemblyName = new System.Windows.Forms.TextBox();
			ultraFormattedLinkLabel1 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			ultraFormattedLinkLabel5 = new Infragistics.Win.FormattedLinkLabel.UltraFormattedLinkLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
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
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			removeRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			comboBoxGridLocation = new Micromind.DataControls.LocationComboBox();
			formManager = new Micromind.DataControls.FormManager();
			comboBoxBOM = new Micromind.DataControls.BOMComboBox();
			comboBoxGridExpenseCode = new Micromind.DataControls.ExpenseCodeComboBox();
			comboBoxGridCurrency = new Micromind.DataControls.CurrencyComboBox();
			bomComboBox2 = new Micromind.DataControls.BOMComboBox();
			textBoxUnitCost = new Micromind.UISupport.NumberTextBox();
			tabPageItems.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			tabPageExpense.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridExpense).BeginInit();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			panelDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDismantle).BeginInit();
			contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			contextMenuStrip2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).BeginInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).BeginInit();
			SuspendLayout();
			tabPageItems.Controls.Add(comboBoxGridProductUnit);
			tabPageItems.Controls.Add(comboBoxGridItem);
			tabPageItems.Controls.Add(label5);
			tabPageItems.Controls.Add(productPhotoViewer);
			tabPageItems.Controls.Add(dataGridItems);
			tabPageItems.Location = new System.Drawing.Point(1, 23);
			tabPageItems.Name = "tabPageItems";
			tabPageItems.Size = new System.Drawing.Size(708, 284);
			comboBoxGridProductUnit.AlwaysInEditMode = true;
			comboBoxGridProductUnit.Assigned = false;
			comboBoxGridProductUnit.CalcManager = ultraCalcManager1;
			comboBoxGridProductUnit.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridProductUnit.CustomReportFieldName = "";
			comboBoxGridProductUnit.CustomReportKey = "";
			comboBoxGridProductUnit.CustomReportValueType = 1;
			comboBoxGridProductUnit.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridProductUnit.DisplayLayout.Appearance = appearance;
			comboBoxGridProductUnit.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridProductUnit.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridProductUnit.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxGridProductUnit.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridProductUnit.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridProductUnit.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridProductUnit.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridProductUnit.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxGridProductUnit.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridProductUnit.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridProductUnit.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridProductUnit.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridProductUnit.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxGridProductUnit.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridProductUnit.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
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
			ultraCalcManager1.ContainingControl = this;
			comboBoxGridItem.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxGridItem.AlwaysInEditMode = true;
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
			label5.Location = new System.Drawing.Point(14, 152);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(700, 59);
			label5.TabIndex = 117;
			label5.Text = "VOIDED";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			label5.Visible = false;
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(261, 61);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 162);
			productPhotoViewer.TabIndex = 137;
			productPhotoViewer.Visible = false;
			dataGridItems.AllowAddNew = true;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridItems.CalcManager = ultraCalcManager1;
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance25;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.IncludeLotItems = false;
			dataGridItems.LoadLayoutFailed = false;
			dataGridItems.Location = new System.Drawing.Point(3, 3);
			dataGridItems.MinimumSize = new System.Drawing.Size(622, 154);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowClearMenu = true;
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.ShowMoveRowsMenu = true;
			dataGridItems.Size = new System.Drawing.Size(704, 281);
			dataGridItems.TabIndex = 136;
			dataGridItems.Text = "dataEntryGrid1";
			tabPageExpense.Controls.Add(dataGridExpense);
			tabPageExpense.Location = new System.Drawing.Point(-10000, -10000);
			tabPageExpense.Name = "tabPageExpense";
			tabPageExpense.Size = new System.Drawing.Size(708, 284);
			dataGridExpense.AllowAddNew = true;
			dataGridExpense.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridExpense.CalcManager = ultraCalcManager1;
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridExpense.DisplayLayout.Appearance = appearance37;
			dataGridExpense.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridExpense.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridExpense.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridExpense.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridExpense.DisplayLayout.MaxColScrollRegions = 1;
			dataGridExpense.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridExpense.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridExpense.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridExpense.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
			dataGridExpense.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridExpense.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridExpense.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridExpense.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridExpense.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridExpense.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridExpense.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridExpense.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridExpense.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridExpense.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridExpense.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridExpense.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
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
			dataGridExpense.Size = new System.Drawing.Size(704, 254);
			dataGridExpense.TabIndex = 135;
			dataGridExpense.Text = "dataEntryGrid1";
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[20]
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
				toolStripButtonAttach,
				toolStripSeparator2,
				toolStripButtonPrint,
				toolStripButtonPreview,
				toolStripSeparator3,
				toolStripButtonDistribution,
				toolStripButtonExcelImport,
				toolStripButtonInformation,
				toolStripButtonApproval,
				toolStripLabelApproval,
				toolStripSeparatorApproval
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
			toolStripTextBoxFind.KeyPress += new System.Windows.Forms.KeyPressEventHandler(toolStripTextBoxFind_KeyPress);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(58, 28);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(91, 28);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
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
			toolStripButtonExcelImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonExcelImport.Image = Micromind.ClientUI.Properties.Resources.excelimport;
			toolStripButtonExcelImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonExcelImport.Name = "toolStripButtonExcelImport";
			toolStripButtonExcelImport.Size = new System.Drawing.Size(28, 28);
			toolStripButtonExcelImport.Text = "Import from Excel";
			toolStripButtonExcelImport.Visible = false;
			toolStripButtonExcelImport.Click += new System.EventHandler(toolStripButtonExcelImport_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 28);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
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
			panelButtons.Location = new System.Drawing.Point(0, 474);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(739, 40);
			panelButtons.TabIndex = 12;
			buttonVoid.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonVoid.BackColor = System.Drawing.Color.DarkGray;
			buttonVoid.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonVoid.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonVoid.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonVoid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonVoid.Location = new System.Drawing.Point(328, 8);
			buttonVoid.Name = "buttonVoid";
			buttonVoid.Size = new System.Drawing.Size(42, 24);
			buttonVoid.TabIndex = 4;
			buttonVoid.Text = "&Void";
			buttonVoid.UseVisualStyleBackColor = false;
			buttonVoid.Visible = false;
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
			xpButton1.TabIndex = 5;
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
			dateTimePickerDate.Location = new System.Drawing.Point(495, 3);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(142, 20);
			dateTimePickerDate.TabIndex = 2;
			textBoxVoucherNumber.Location = new System.Drawing.Point(307, 3);
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new System.Drawing.Size(118, 20);
			textBoxVoucherNumber.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(343, 71);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(60, 13);
			label1.TabIndex = 20;
			label1.Text = "Reference:";
			textBoxRef1.Location = new System.Drawing.Point(408, 68);
			textBoxRef1.MaxLength = 30;
			textBoxRef1.Name = "textBoxRef1";
			textBoxRef1.Size = new System.Drawing.Size(136, 20);
			textBoxRef1.TabIndex = 9;
			textBoxNote.Location = new System.Drawing.Point(93, 91);
			textBoxNote.MaxLength = 255;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(544, 20);
			textBoxNote.TabIndex = 10;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(15, 94);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 20;
			label3.Text = "Note:";
			appearance49.FontData.BoldAsString = "True";
			appearance49.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel2.Appearance = appearance49;
			ultraFormattedLinkLabel2.AutoSize = true;
			ultraFormattedLinkLabel2.Location = new System.Drawing.Point(198, 5);
			ultraFormattedLinkLabel2.Name = "ultraFormattedLinkLabel2";
			ultraFormattedLinkLabel2.Size = new System.Drawing.Size(101, 15);
			ultraFormattedLinkLabel2.TabIndex = 2;
			ultraFormattedLinkLabel2.TabStop = true;
			ultraFormattedLinkLabel2.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel2.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel2.Value = "Voucher Number:";
			appearance50.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel2.VisitedLinkAppearance = appearance50;
			ultraFormattedLinkLabel2.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel2_LinkClicked);
			panelDetails.Controls.Add(textBoxUnitCost);
			panelDetails.Controls.Add(textBoxQuantity);
			panelDetails.Controls.Add(label2);
			panelDetails.Controls.Add(textBoxCost);
			panelDetails.Controls.Add(ultraFormattedLinkLabel3);
			panelDetails.Controls.Add(comboBoxSysDoc);
			panelDetails.Controls.Add(textBoxRepackName);
			panelDetails.Controls.Add(comboBoxLocation);
			panelDetails.Controls.Add(label6);
			panelDetails.Controls.Add(comboBoxDismantle);
			panelDetails.Controls.Add(ultraFormattedLinkLabel1);
			panelDetails.Controls.Add(ultraFormattedLinkLabel5);
			panelDetails.Controls.Add(ultraFormattedLinkLabel2);
			panelDetails.Controls.Add(mmLabel1);
			panelDetails.Controls.Add(label3);
			panelDetails.Controls.Add(textBoxAssemblyName);
			panelDetails.Controls.Add(textBoxNote);
			panelDetails.Controls.Add(label1);
			panelDetails.Controls.Add(textBoxRef1);
			panelDetails.Controls.Add(textBoxVoucherNumber);
			panelDetails.Controls.Add(dateTimePickerDate);
			panelDetails.Location = new System.Drawing.Point(12, 31);
			panelDetails.Name = "panelDetails";
			panelDetails.Size = new System.Drawing.Size(655, 124);
			panelDetails.TabIndex = 0;
			textBoxQuantity.AllowDecimal = true;
			textBoxQuantity.BackColor = System.Drawing.Color.White;
			textBoxQuantity.CustomReportFieldName = "";
			textBoxQuantity.CustomReportKey = "";
			textBoxQuantity.CustomReportValueType = 1;
			textBoxQuantity.IsComboTextBox = false;
			textBoxQuantity.IsModified = false;
			textBoxQuantity.Location = new System.Drawing.Point(262, 69);
			textBoxQuantity.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxQuantity.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxQuantity.Name = "textBoxQuantity";
			textBoxQuantity.NullText = "0";
			textBoxQuantity.Size = new System.Drawing.Size(83, 20);
			textBoxQuantity.TabIndex = 134;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(184, 71);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(59, 13);
			label2.TabIndex = 135;
			label2.Text = "Qty Built:";
			textBoxCost.AllowDecimal = true;
			textBoxCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCost.CustomReportFieldName = "";
			textBoxCost.CustomReportKey = "";
			textBoxCost.CustomReportValueType = 1;
			textBoxCost.IsComboTextBox = false;
			textBoxCost.IsModified = false;
			textBoxCost.Location = new System.Drawing.Point(93, 69);
			textBoxCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxCost.Name = "textBoxCost";
			textBoxCost.NullText = "0";
			textBoxCost.ReadOnly = true;
			textBoxCost.Size = new System.Drawing.Size(83, 20);
			textBoxCost.TabIndex = 7;
			appearance51.FontData.BoldAsString = "True";
			appearance51.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel3.Appearance = appearance51;
			ultraFormattedLinkLabel3.AutoSize = true;
			ultraFormattedLinkLabel3.Location = new System.Drawing.Point(17, 49);
			ultraFormattedLinkLabel3.Name = "ultraFormattedLinkLabel3";
			ultraFormattedLinkLabel3.Size = new System.Drawing.Size(56, 15);
			ultraFormattedLinkLabel3.TabIndex = 133;
			ultraFormattedLinkLabel3.TabStop = true;
			ultraFormattedLinkLabel3.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel3.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel3.Value = "Location:";
			appearance52.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel3.VisitedLinkAppearance = appearance52;
			ultraFormattedLinkLabel3.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel3_LinkClicked);
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
			comboBoxSysDoc.Location = new System.Drawing.Point(93, 4);
			comboBoxSysDoc.MaxDropDownItems = 12;
			comboBoxSysDoc.Name = "comboBoxSysDoc";
			comboBoxSysDoc.ShowAll = false;
			comboBoxSysDoc.ShowInactiveItems = false;
			comboBoxSysDoc.ShowQuickAdd = true;
			comboBoxSysDoc.Size = new System.Drawing.Size(100, 20);
			comboBoxSysDoc.TabIndex = 0;
			comboBoxSysDoc.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxRepackName.Location = new System.Drawing.Point(253, 47);
			textBoxRepackName.MaxLength = 255;
			textBoxRepackName.Name = "textBoxRepackName";
			textBoxRepackName.ReadOnly = true;
			textBoxRepackName.Size = new System.Drawing.Size(384, 20);
			textBoxRepackName.TabIndex = 6;
			textBoxRepackName.TabStop = false;
			comboBoxLocation.AlwaysInEditMode = true;
			comboBoxLocation.Assigned = false;
			comboBoxLocation.CalcManager = ultraCalcManager1;
			comboBoxLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLocation.CustomReportFieldName = "";
			comboBoxLocation.CustomReportKey = "";
			comboBoxLocation.CustomReportValueType = 1;
			comboBoxLocation.DescriptionTextBox = textBoxRepackName;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxLocation.DisplayLayout.Appearance = appearance53;
			comboBoxLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance54.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance54.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance54.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance54.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.GroupByBox.Appearance = appearance54;
			appearance55.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance55;
			comboBoxLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance56.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance56.BackColor2 = System.Drawing.SystemColors.Control;
			appearance56.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance56.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance56;
			comboBoxLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance57.BackColor = System.Drawing.SystemColors.Window;
			appearance57.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxLocation.DisplayLayout.Override.ActiveCellAppearance = appearance57;
			appearance58.BackColor = System.Drawing.SystemColors.Highlight;
			appearance58.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxLocation.DisplayLayout.Override.ActiveRowAppearance = appearance58;
			comboBoxLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.CardAreaAppearance = appearance59;
			appearance60.BorderColor = System.Drawing.Color.Silver;
			appearance60.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxLocation.DisplayLayout.Override.CellAppearance = appearance60;
			comboBoxLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxLocation.DisplayLayout.Override.CellPadding = 0;
			appearance61.BackColor = System.Drawing.SystemColors.Control;
			appearance61.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance61.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance61.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance61.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxLocation.DisplayLayout.Override.GroupByRowAppearance = appearance61;
			appearance62.TextHAlignAsString = "Left";
			comboBoxLocation.DisplayLayout.Override.HeaderAppearance = appearance62;
			comboBoxLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance63.BackColor = System.Drawing.SystemColors.Window;
			appearance63.BorderColor = System.Drawing.Color.Silver;
			comboBoxLocation.DisplayLayout.Override.RowAppearance = appearance63;
			comboBoxLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance64;
			comboBoxLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLocation.Editable = true;
			comboBoxLocation.FilterString = "";
			comboBoxLocation.HasAllAccount = false;
			comboBoxLocation.HasCustom = false;
			comboBoxLocation.IsDataLoaded = false;
			comboBoxLocation.Location = new System.Drawing.Point(93, 47);
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
			comboBoxLocation.Size = new System.Drawing.Size(158, 20);
			comboBoxLocation.TabIndex = 5;
			comboBoxLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(15, 71);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(36, 13);
			label6.TabIndex = 130;
			label6.Text = "Cost:";
			comboBoxDismantle.AllowedItemTypes = new Micromind.Common.Data.ItemTypes[0];
			comboBoxDismantle.Assigned = false;
			comboBoxDismantle.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxDismantle.CalcManager = ultraCalcManager1;
			comboBoxDismantle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDismantle.CustomReportFieldName = "";
			comboBoxDismantle.CustomReportKey = "";
			comboBoxDismantle.CustomReportValueType = 1;
			comboBoxDismantle.DescriptionTextBox = textBoxAssemblyName;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDismantle.DisplayLayout.Appearance = appearance65;
			comboBoxDismantle.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDismantle.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance66.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance66.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance66.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance66.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDismantle.DisplayLayout.GroupByBox.Appearance = appearance66;
			appearance67.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDismantle.DisplayLayout.GroupByBox.BandLabelAppearance = appearance67;
			comboBoxDismantle.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance68.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance68.BackColor2 = System.Drawing.SystemColors.Control;
			appearance68.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance68.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDismantle.DisplayLayout.GroupByBox.PromptAppearance = appearance68;
			comboBoxDismantle.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDismantle.DisplayLayout.MaxRowScrollRegions = 1;
			appearance69.BackColor = System.Drawing.SystemColors.Window;
			appearance69.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDismantle.DisplayLayout.Override.ActiveCellAppearance = appearance69;
			appearance70.BackColor = System.Drawing.SystemColors.Highlight;
			appearance70.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDismantle.DisplayLayout.Override.ActiveRowAppearance = appearance70;
			comboBoxDismantle.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDismantle.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDismantle.DisplayLayout.Override.CardAreaAppearance = appearance71;
			appearance72.BorderColor = System.Drawing.Color.Silver;
			appearance72.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDismantle.DisplayLayout.Override.CellAppearance = appearance72;
			comboBoxDismantle.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDismantle.DisplayLayout.Override.CellPadding = 0;
			appearance73.BackColor = System.Drawing.SystemColors.Control;
			appearance73.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance73.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance73.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance73.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDismantle.DisplayLayout.Override.GroupByRowAppearance = appearance73;
			appearance74.TextHAlignAsString = "Left";
			comboBoxDismantle.DisplayLayout.Override.HeaderAppearance = appearance74;
			comboBoxDismantle.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDismantle.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance75.BackColor = System.Drawing.SystemColors.Window;
			appearance75.BorderColor = System.Drawing.Color.Silver;
			comboBoxDismantle.DisplayLayout.Override.RowAppearance = appearance75;
			comboBoxDismantle.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance76.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDismantle.DisplayLayout.Override.TemplateAddRowAppearance = appearance76;
			comboBoxDismantle.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDismantle.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDismantle.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDismantle.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDismantle.Editable = true;
			comboBoxDismantle.FilterCustomerID = "";
			comboBoxDismantle.FilterString = "";
			comboBoxDismantle.FilterSysDocID = "";
			comboBoxDismantle.HasAllAccount = false;
			comboBoxDismantle.HasCustom = false;
			comboBoxDismantle.IsDataLoaded = false;
			comboBoxDismantle.Location = new System.Drawing.Point(93, 26);
			comboBoxDismantle.MaxDropDownItems = 12;
			comboBoxDismantle.Name = "comboBoxDismantle";
			comboBoxDismantle.Show3PLItems = true;
			comboBoxDismantle.ShowInactiveItems = false;
			comboBoxDismantle.ShowOnlyLotItems = false;
			comboBoxDismantle.ShowQuickAdd = true;
			comboBoxDismantle.Size = new System.Drawing.Size(158, 20);
			comboBoxDismantle.TabIndex = 3;
			comboBoxDismantle.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			textBoxAssemblyName.Location = new System.Drawing.Point(253, 26);
			textBoxAssemblyName.MaxLength = 255;
			textBoxAssemblyName.Name = "textBoxAssemblyName";
			textBoxAssemblyName.ReadOnly = true;
			textBoxAssemblyName.Size = new System.Drawing.Size(384, 20);
			textBoxAssemblyName.TabIndex = 4;
			textBoxAssemblyName.TabStop = false;
			appearance77.FontData.BoldAsString = "True";
			appearance77.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel1.Appearance = appearance77;
			ultraFormattedLinkLabel1.AutoSize = true;
			ultraFormattedLinkLabel1.Location = new System.Drawing.Point(17, 27);
			ultraFormattedLinkLabel1.Name = "ultraFormattedLinkLabel1";
			ultraFormattedLinkLabel1.Size = new System.Drawing.Size(52, 15);
			ultraFormattedLinkLabel1.TabIndex = 124;
			ultraFormattedLinkLabel1.TabStop = true;
			ultraFormattedLinkLabel1.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel1.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel1.Value = "Product:";
			appearance78.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel1.VisitedLinkAppearance = appearance78;
			ultraFormattedLinkLabel1.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel1_LinkClicked);
			appearance79.FontData.BoldAsString = "True";
			appearance79.FontData.Name = "Tahoma";
			ultraFormattedLinkLabel5.Appearance = appearance79;
			ultraFormattedLinkLabel5.AutoSize = true;
			ultraFormattedLinkLabel5.Location = new System.Drawing.Point(18, 5);
			ultraFormattedLinkLabel5.Name = "ultraFormattedLinkLabel5";
			ultraFormattedLinkLabel5.Size = new System.Drawing.Size(45, 15);
			ultraFormattedLinkLabel5.TabIndex = 0;
			ultraFormattedLinkLabel5.TabStop = true;
			ultraFormattedLinkLabel5.TreatValueAs = Infragistics.Win.FormattedLinkLabel.TreatValueAs.URL;
			ultraFormattedLinkLabel5.UnderlineLinks = Infragistics.Win.FormattedLinkLabel.UnderlineLink.WhenHovered;
			ultraFormattedLinkLabel5.Value = "Doc ID:";
			appearance80.ForeColor = System.Drawing.Color.Blue;
			ultraFormattedLinkLabel5.VisitedLinkAppearance = appearance80;
			ultraFormattedLinkLabel5.LinkClicked += new Infragistics.Win.FormattedLinkLabel.LinkClickedEventHandler(ultraFormattedLinkLabel5_LinkClicked);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(452, 5);
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
			ultraTabControl1.Controls.Add(tabPageItems);
			ultraTabControl1.Controls.Add(tabPageExpense);
			ultraTabControl1.Location = new System.Drawing.Point(12, 158);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(712, 310);
			ultraTabControl1.TabIndex = 11;
			ultraTab.TabPage = tabPageItems;
			ultraTab.Text = "Items";
			ultraTab2.TabPage = tabPageExpense;
			ultraTab2.Text = "Expenses";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(708, 284);
			contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[6]
			{
				availableQuantityToolStripMenuItem,
				salesStatisticsToolStripMenuItem,
				itemPicToolStripMenuItem,
				itemDetailsToolStripMenuItem,
				toolStripSeparator5,
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
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
			removeRowToolStripMenuItem.Image = Micromind.ClientUI.Properties.Resources.Delete;
			removeRowToolStripMenuItem.Name = "removeRowToolStripMenuItem";
			removeRowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			removeRowToolStripMenuItem.Text = "Remove Row";
			removeRowToolStripMenuItem.Click += new System.EventHandler(removeRowToolStripMenuItem_Click);
			comboBoxGridLocation.AlwaysInEditMode = true;
			comboBoxGridLocation.Assigned = false;
			comboBoxGridLocation.CalcManager = ultraCalcManager1;
			comboBoxGridLocation.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridLocation.CustomReportFieldName = "";
			comboBoxGridLocation.CustomReportKey = "";
			comboBoxGridLocation.CustomReportValueType = 1;
			comboBoxGridLocation.DescriptionTextBox = null;
			appearance81.BackColor = System.Drawing.SystemColors.Window;
			appearance81.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridLocation.DisplayLayout.Appearance = appearance81;
			comboBoxGridLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance82.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance82.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance82.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance82.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.GroupByBox.Appearance = appearance82;
			appearance83.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance83;
			comboBoxGridLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance84.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance84.BackColor2 = System.Drawing.SystemColors.Control;
			appearance84.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance84.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance84;
			comboBoxGridLocation.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance85.BackColor = System.Drawing.SystemColors.Window;
			appearance85.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveCellAppearance = appearance85;
			appearance86.BackColor = System.Drawing.SystemColors.Highlight;
			appearance86.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridLocation.DisplayLayout.Override.ActiveRowAppearance = appearance86;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance87.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.CardAreaAppearance = appearance87;
			appearance88.BorderColor = System.Drawing.Color.Silver;
			appearance88.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridLocation.DisplayLayout.Override.CellAppearance = appearance88;
			comboBoxGridLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridLocation.DisplayLayout.Override.CellPadding = 0;
			appearance89.BackColor = System.Drawing.SystemColors.Control;
			appearance89.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance89.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance89.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance89.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridLocation.DisplayLayout.Override.GroupByRowAppearance = appearance89;
			appearance90.TextHAlignAsString = "Left";
			comboBoxGridLocation.DisplayLayout.Override.HeaderAppearance = appearance90;
			comboBoxGridLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance91.BackColor = System.Drawing.SystemColors.Window;
			appearance91.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridLocation.DisplayLayout.Override.RowAppearance = appearance91;
			comboBoxGridLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance92.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance92;
			comboBoxGridLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxGridLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxGridLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxGridLocation.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxGridLocation.Editable = true;
			comboBoxGridLocation.FilterString = "";
			comboBoxGridLocation.HasAllAccount = false;
			comboBoxGridLocation.HasCustom = false;
			comboBoxGridLocation.IsDataLoaded = false;
			comboBoxGridLocation.Location = new System.Drawing.Point(673, 57);
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
			comboBoxGridLocation.TabIndex = 123;
			comboBoxGridLocation.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxGridLocation.Visible = false;
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
			comboBoxBOM.Assigned = false;
			comboBoxBOM.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxBOM.CalcManager = ultraCalcManager1;
			comboBoxBOM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxBOM.CustomReportFieldName = "";
			comboBoxBOM.CustomReportKey = "";
			comboBoxBOM.CustomReportValueType = 1;
			comboBoxBOM.DescriptionTextBox = null;
			comboBoxBOM.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxBOM.Editable = true;
			comboBoxBOM.FilterString = "";
			comboBoxBOM.HasAllAccount = false;
			comboBoxBOM.HasCustom = false;
			comboBoxBOM.IsDataLoaded = false;
			comboBoxBOM.Location = new System.Drawing.Point(673, 31);
			comboBoxBOM.MaxDropDownItems = 12;
			comboBoxBOM.Name = "comboBoxBOM";
			comboBoxBOM.ShowInactiveItems = false;
			comboBoxBOM.ShowQuickAdd = true;
			comboBoxBOM.Size = new System.Drawing.Size(66, 20);
			comboBoxBOM.TabIndex = 6;
			comboBoxBOM.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxBOM.Visible = false;
			comboBoxGridExpenseCode.AlwaysInEditMode = true;
			comboBoxGridExpenseCode.Assigned = false;
			comboBoxGridExpenseCode.CalcManager = ultraCalcManager1;
			comboBoxGridExpenseCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxGridExpenseCode.CustomReportFieldName = "";
			comboBoxGridExpenseCode.CustomReportKey = "";
			comboBoxGridExpenseCode.CustomReportValueType = 1;
			comboBoxGridExpenseCode.DescriptionTextBox = null;
			appearance93.BackColor = System.Drawing.SystemColors.Window;
			appearance93.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridExpenseCode.DisplayLayout.Appearance = appearance93;
			comboBoxGridExpenseCode.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridExpenseCode.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance94.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance94.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance94.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance94.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.Appearance = appearance94;
			appearance95.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BandLabelAppearance = appearance95;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance96.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance96.BackColor2 = System.Drawing.SystemColors.Control;
			appearance96.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance96.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridExpenseCode.DisplayLayout.GroupByBox.PromptAppearance = appearance96;
			comboBoxGridExpenseCode.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridExpenseCode.DisplayLayout.MaxRowScrollRegions = 1;
			appearance97.BackColor = System.Drawing.SystemColors.Window;
			appearance97.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveCellAppearance = appearance97;
			appearance98.BackColor = System.Drawing.SystemColors.Highlight;
			appearance98.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridExpenseCode.DisplayLayout.Override.ActiveRowAppearance = appearance98;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridExpenseCode.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance99.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.CardAreaAppearance = appearance99;
			appearance100.BorderColor = System.Drawing.Color.Silver;
			appearance100.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellAppearance = appearance100;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridExpenseCode.DisplayLayout.Override.CellPadding = 0;
			appearance101.BackColor = System.Drawing.SystemColors.Control;
			appearance101.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance101.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance101.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance101.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridExpenseCode.DisplayLayout.Override.GroupByRowAppearance = appearance101;
			appearance102.TextHAlignAsString = "Left";
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderAppearance = appearance102;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridExpenseCode.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance103.BackColor = System.Drawing.SystemColors.Window;
			appearance103.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowAppearance = appearance103;
			comboBoxGridExpenseCode.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance104.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridExpenseCode.DisplayLayout.Override.TemplateAddRowAppearance = appearance104;
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
			appearance105.BackColor = System.Drawing.SystemColors.Window;
			appearance105.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxGridCurrency.DisplayLayout.Appearance = appearance105;
			comboBoxGridCurrency.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxGridCurrency.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance106.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance106.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance106.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance106.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.Appearance = appearance106;
			appearance107.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BandLabelAppearance = appearance107;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance108.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance108.BackColor2 = System.Drawing.SystemColors.Control;
			appearance108.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance108.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxGridCurrency.DisplayLayout.GroupByBox.PromptAppearance = appearance108;
			comboBoxGridCurrency.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxGridCurrency.DisplayLayout.MaxRowScrollRegions = 1;
			appearance109.BackColor = System.Drawing.SystemColors.Window;
			appearance109.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveCellAppearance = appearance109;
			appearance110.BackColor = System.Drawing.SystemColors.Highlight;
			appearance110.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxGridCurrency.DisplayLayout.Override.ActiveRowAppearance = appearance110;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxGridCurrency.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance111.BackColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.CardAreaAppearance = appearance111;
			appearance112.BorderColor = System.Drawing.Color.Silver;
			appearance112.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxGridCurrency.DisplayLayout.Override.CellAppearance = appearance112;
			comboBoxGridCurrency.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxGridCurrency.DisplayLayout.Override.CellPadding = 0;
			appearance113.BackColor = System.Drawing.SystemColors.Control;
			appearance113.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance113.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance113.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance113.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxGridCurrency.DisplayLayout.Override.GroupByRowAppearance = appearance113;
			appearance114.TextHAlignAsString = "Left";
			comboBoxGridCurrency.DisplayLayout.Override.HeaderAppearance = appearance114;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxGridCurrency.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance115.BackColor = System.Drawing.SystemColors.Window;
			appearance115.BorderColor = System.Drawing.Color.Silver;
			comboBoxGridCurrency.DisplayLayout.Override.RowAppearance = appearance115;
			comboBoxGridCurrency.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance116.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxGridCurrency.DisplayLayout.Override.TemplateAddRowAppearance = appearance116;
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
			textBoxUnitCost.AllowDecimal = true;
			textBoxUnitCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnitCost.CustomReportFieldName = "";
			textBoxUnitCost.CustomReportKey = "";
			textBoxUnitCost.CustomReportValueType = 1;
			textBoxUnitCost.IsComboTextBox = false;
			textBoxUnitCost.IsModified = false;
			textBoxUnitCost.Location = new System.Drawing.Point(547, 68);
			textBoxUnitCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxUnitCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxUnitCost.Name = "textBoxUnitCost";
			textBoxUnitCost.NullText = "0";
			textBoxUnitCost.ReadOnly = true;
			textBoxUnitCost.Size = new System.Drawing.Size(83, 20);
			textBoxUnitCost.TabIndex = 136;
			textBoxUnitCost.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(739, 514);
			base.Controls.Add(comboBoxGridLocation);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelDetails);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(comboBoxBOM);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(649, 366);
			base.Name = "InventoryDismantleForm";
			Text = "Inventory Dismantle";
			tabPageItems.ResumeLayout(false);
			tabPageItems.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxGridProductUnit).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraCalcManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridItem).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			tabPageExpense.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridExpense).EndInit();
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			panelDetails.ResumeLayout(false);
			panelDetails.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxSysDoc).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxDismantle).EndInit();
			contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			contextMenuStrip2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxGridLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxBOM).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridExpenseCode).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxGridCurrency).EndInit();
			((System.ComponentModel.ISupportInitialize)bomComboBox2).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
