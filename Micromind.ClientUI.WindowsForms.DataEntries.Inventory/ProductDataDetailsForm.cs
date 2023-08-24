using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Inventory
{
	public class ProductDataDetailsForm : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private bool loadItemDetails = CompanyPreferences.LoadItemFeatures;

		private string selectedItem = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private TextBox textBoxSearch;

		private Label label1;

		private MMSListGrid dataGridItems;

		private Panel panelItemdetails;

		private ProductPhotoViewer productPhotoViewer;

		private Panel panel1;

		private Label label2;

		private Label label3;

		private Label label4;

		private Label label5;

		private Label label6;

		private Label label7;

		private Label label8;

		private Label labelCost;

		private Label labelStock;

		private Label labelCountry;

		private Label labelManufacturer;

		private Label labelBrand;

		private Label labelCategory;

		private Label labelUnit;

		private DataEntryGrid dataGridPurchase;

		private DataEntryGrid dataGridSales;

		private GroupBox groupBox2;

		private GroupBox groupBox1;

		private Label label37;

		private DataEntryGrid dataGridMultiLocation;

		private Label labelLastCost;

		private NumberTextBox textBoxLastCost;

		private MMLabel labelMinPrice;

		private MMLabel labelSpecialPrice;

		private MMLabel labelWholesalePrice;

		private MMLabel labelStandardPrice;

		private UnitPriceTextBox textBoxMinimumPrice;

		private UnitPriceTextBox textBoxSpecialPrice;

		private UnitPriceTextBox textBoxWholesalePrice;

		private UnitPriceTextBox textBoxStandardPrice;

		private Label labelUnitPrice;

		private NumberTextBox textBoxCost;

		private Label labelFactor;

		private TextBox textBoxUnitFactor;

		public bool IsMultiSelect
		{
			get
			{
				return isMultiSelect;
			}
			set
			{
				isMultiSelect = value;
				if (value && dataGridItems.DataSource != null)
				{
					dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].DataType = typeof(bool);
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellActivation = Activation.AllowEdit;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
					dataGridItems.DisplayLayout.Bands[0].Columns["C"].CellClickAction = CellClickAction.Edit;
				}
			}
		}

		public string SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				selectedItem = value;
			}
		}

		public UltraGridRow SelectedRow
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				if (dataGridItems.ActiveRow != null)
				{
					return dataGridItems.ActiveRow;
				}
				return null;
			}
		}

		public List<UltraGridRow> SelectedRows
		{
			get
			{
				if (base.DialogResult == DialogResult.Cancel)
				{
					return null;
				}
				List<UltraGridRow> list = new List<UltraGridRow>();
				if (IsMultiSelect)
				{
					foreach (UltraGridRow row in dataGridItems.Rows)
					{
						if (row.Cells["C"].Value.ToString() == "True")
						{
							list.Add(row);
						}
					}
				}
				if (list.Count == 0 && dataGridItems.ActiveRow != null)
				{
					list.Add(dataGridItems.ActiveRow);
				}
				return list;
			}
		}

		public UltraGrid Grid => dataGridItems;

		public DataSet DataSource
		{
			get
			{
				return (DataSet)dataGridItems.DataSource;
			}
			set
			{
				if (value != null)
				{
					if (value != null && value.Tables.Count > 0 && !value.Tables[0].Columns.Contains("SearchColumn"))
					{
						DataTable dataTable = value.Tables[0];
						dataTable.Columns.Add("SearchColumn");
						string text = "";
						foreach (DataRow row in dataTable.Rows)
						{
							text = "";
							foreach (DataColumn column in dataTable.Columns)
							{
								if (!(column.ColumnName == "SearchColumn"))
								{
									text = text + row[column].ToString() + " ";
								}
							}
							row["SearchColumn"] = text;
						}
					}
					dataGridItems.DataSource = value;
					dataGridItems.DisplayLayout.Bands[0].Columns["SearchColumn"].Hidden = true;
					dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
					foreach (UltraGridColumn column2 in dataGridItems.DisplayLayout.Bands[0].Columns)
					{
						if (column2.Key.ToLower() != "name" && column2.Key.ToLower() != "code" && column2.Key.ToLower() != "description")
						{
							column2.Hidden = true;
						}
					}
					if (dataGridItems.DisplayLayout.Bands[0].Columns.Exists("ItemType"))
					{
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = false;
						ValueList valueList = new ValueList();
						valueList.ValueListItems.Add(0, "None");
						valueList.ValueListItems.Add(1, "Inventory");
						valueList.ValueListItems.Add(2, "Non-Inventory");
						valueList.ValueListItems.Add(3, "Service");
						valueList.ValueListItems.Add(4, "Discount");
						valueList.ValueListItems.Add(5, "Consignment");
						valueList.ValueListItems.Add(6, "Matrix");
						valueList.ValueListItems.Add(7, "Assembly");
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Header.Caption = "Item Type";
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].ValueList = valueList;
						dataGridItems.DisplayLayout.Bands[0].Columns["ItemType"].Width = 100;
					}
					if (IsMultiSelect && !dataGridItems.DisplayLayout.Bands[0].Columns.Exists("C"))
					{
						UltraGridColumn ultraGridColumn = dataGridItems.DisplayLayout.Bands[0].Columns.Insert(0, "C");
						ultraGridColumn.DataType = typeof(bool);
						ultraGridColumn.CellActivation = Activation.AllowEdit;
						ultraGridColumn.CellClickAction = CellClickAction.Edit;
						ultraGridColumn.CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
						ultraGridColumn.Width = 18;
						ultraGridColumn.MinWidth = 18;
						ultraGridColumn.MaxWidth = 18;
						ultraGridColumn.LockedWidth = true;
						ultraGridColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
					}
				}
			}
		}

		public event EventHandler ValidateSelection;

		public ProductDataDetailsForm()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			productPhotoViewer.EnlargeRequested += productPhotoViewer_EnlargeRequested;
			if (!loadItemDetails)
			{
				panelItemdetails.Visible = false;
				base.Size = new Size(522, 400);
			}
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
		{
			checked
			{
				if (e.KeyCode == Keys.Down)
				{
					if (dataGridItems.ActiveRow != null)
					{
						int visibleIndex = dataGridItems.ActiveRow.VisibleIndex;
						if (visibleIndex < dataGridItems.Rows.VisibleRowCount)
						{
							dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex + 1);
						}
						e.SuppressKeyPress = true;
					}
				}
				else if (e.KeyCode == Keys.Up && dataGridItems.ActiveRow != null)
				{
					int visibleIndex2 = dataGridItems.ActiveRow.VisibleIndex;
					if (visibleIndex2 > 0)
					{
						dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(visibleIndex2 - 1);
					}
					e.SuppressKeyPress = true;
				}
			}
		}

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			dataGridItems.ApplyUIDesign();
			dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			LoadGridData();
			dataGridSales.SetupUI();
			dataGridPurchase.SetupUI();
			dataGridMultiLocation.SetupUI();
			SetupSalesGrid();
			SetupPurchaseGrid();
			SetupMultilocationGrid();
			textBoxSearch.Text = SelectedItem.Trim();
			textBoxSearch.Focus();
		}

		public string GetSelectedCode(string codeColumnName)
		{
			if (SelectedRow != null)
			{
				return SelectedRow.Cells[codeColumnName].Value.ToString();
			}
			return "";
		}

		private void textBoxSearch_TextChanged(object sender, EventArgs e)
		{
			if (DataSource != null && dataGridItems.DisplayLayout.Bands[0].Columns.Exists("SearchColumn"))
			{
				dataGridItems.BeginUpdate();
				if (dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Count == 0)
				{
					dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions.Add(new FilterCondition());
				}
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].ComparisionOperator = FilterComparisionOperator.Contains;
				dataGridItems.DisplayLayout.Bands[0].ColumnFilters["SearchColumn"].FilterConditions[0].CompareValue = textBoxSearch.Text;
				dataGridItems.EndUpdate();
				if (dataGridItems.Rows.VisibleRowCount > 0)
				{
					dataGridItems.ActiveRow = dataGridItems.Rows.GetRowAtVisibleIndex(0);
				}
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			CanClose = true;
			if (CanClose)
			{
				Close();
			}
		}

		private void dataGridItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			if (!isMultiSelect || dataGridItems.Rows.Count <= 1)
			{
				SelectItem();
			}
		}

		private void SelectItem()
		{
			CanClose = true;
			if (dataGridItems.Rows.VisibleRowCount == 0 || dataGridItems.ActiveRow == null)
			{
				ErrorHelper.InformationMessage("Please select an item first.");
				return;
			}
			if (this.ValidateSelection != null)
			{
				this.ValidateSelection(this, null);
			}
			if (CanClose && dataGridItems.ActiveRow != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			SelectItem();
		}

		private void SetupGrid()
		{
		}

		private void ClearForm()
		{
			labelUnit.Text = "";
			labelCategory.Text = "";
			labelBrand.Text = "";
			labelManufacturer.Text = "";
			labelCountry.Text = "";
			labelStock.Text = "";
			labelCost.Text = "";
			textBoxUnitFactor.Text = "";
			TextBox textBox = textBoxUnitFactor;
			bool visible = labelFactor.Visible = false;
			textBox.Visible = visible;
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			if (dataGridItems.ActiveRow == null)
			{
				return;
			}
			ClearForm();
			if (dataGridItems.ActiveRow.Cells["Code"].Value == null || !(dataGridItems.ActiveRow.Cells["Code"].Value.ToString() != "") || !loadItemDetails)
			{
				return;
			}
			string text = dataGridItems.ActiveRow.Cells["Code"].Value.ToString();
			checked
			{
				productPhotoViewer.ShowImage(text, dataGridItems.Left + 220, dataGridItems.Top - 30);
				DataSet dataSet = new DataSet();
				dataSet = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(text));
				if (dataSet.Tables[0].Rows.Count <= 0)
				{
					return;
				}
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				labelUnit.Text = dataRow["UnitName"].ToString();
				labelCategory.Text = dataRow["CategoryName"].ToString();
				labelBrand.Text = dataRow["BrandName"].ToString();
				labelManufacturer.Text = dataRow["ManufacturerName"].ToString();
				labelCountry.Text = dataRow["CountryName"].ToString();
				labelStock.Text = dataRow["Quantity"].ToString();
				textBoxCost.Text = decimal.Parse(dataRow["StandardCost"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxStandardPrice.Text = decimal.Parse(dataRow["UnitPrice1"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxWholesalePrice.Text = decimal.Parse(dataRow["UnitPrice2"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxSpecialPrice.Text = decimal.Parse(dataRow["UnitPrice3"].ToString()).ToString(Format.TotalAmountFormat);
				textBoxMinimumPrice.Text = decimal.Parse(dataRow["MinPrice"].ToString()).ToString(Format.TotalAmountFormat);
				if (!string.IsNullOrEmpty(dataRow["LAST COST"].ToString()))
				{
					textBoxLastCost.Text = decimal.Parse(dataRow["LAST COST"].ToString()).ToString(Format.TotalAmountFormat);
				}
				else
				{
					textBoxLastCost.Text = 0.0.ToString();
				}
				labelCost.Text = dataRow["AverageCost"].ToString();
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
				{
					UnitPriceTextBox unitPriceTextBox = textBoxStandardPrice;
					bool visible = labelStandardPrice.Visible = false;
					unitPriceTextBox.Visible = visible;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice2))
				{
					UnitPriceTextBox unitPriceTextBox2 = textBoxWholesalePrice;
					bool visible = labelWholesalePrice.Visible = false;
					unitPriceTextBox2.Visible = visible;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice3))
				{
					UnitPriceTextBox unitPriceTextBox3 = textBoxSpecialPrice;
					bool visible = labelSpecialPrice.Visible = false;
					unitPriceTextBox3.Visible = visible;
				}
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
				{
					UnitPriceTextBox unitPriceTextBox4 = textBoxMinimumPrice;
					bool visible = labelMinPrice.Visible = false;
					unitPriceTextBox4.Visible = visible;
				}
				if (!string.IsNullOrEmpty(labelUnit.Text))
				{
					DataSet productUnits = Factory.ProductSystem.GetProductUnits(text);
					string text2 = "";
					string text3 = "";
					string text4 = "";
					if (productUnits != null)
					{
						foreach (DataRow row in productUnits.Tables[0].Rows)
						{
							if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
							{
								TextBox textBox = textBoxUnitFactor;
								textBox.Text = textBox.Text + ";" + Environment.NewLine;
							}
							text2 = row["Factor"].ToString();
							text3 = row["FactorType"].ToString();
							text4 = row["UnitID"].ToString();
							if (text3 == "M")
							{
								TextBox textBox2 = textBoxUnitFactor;
								textBox2.Text = textBox2.Text + "1 " + labelUnit.Text + " = " + text2 + " " + text4;
							}
							else
							{
								TextBox textBox2 = textBoxUnitFactor;
								textBox2.Text = textBox2.Text + "1 " + text4 + " = " + text2 + " " + labelUnit.Text;
							}
							if (!string.IsNullOrEmpty(textBoxUnitFactor.Text))
							{
								labelFactor.Visible = true;
								textBoxUnitFactor.Visible = true;
							}
						}
					}
				}
				(dataGridSales.DataSource as DataTable).Rows.Clear();
				(dataGridPurchase.DataSource as DataTable).Rows.Clear();
				DisplayPurchaseDetails(text);
				DisplaySalesDetails(text);
				DisplayProductUnderLocation(text);
			}
		}

		private void productPhotoViewer_EnlargeRequested(object sender, EventArgs e)
		{
		}

		private void SetupSalesGrid()
		{
			dataGridSales.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Date");
			dataTable.Columns.Add("Customer");
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			dataGridSales.DataSource = dataTable;
			dataGridSales.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
			dataGridSales.DisplayLayout.Bands[0].Columns[0].Width = checked(20 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[1].Width = checked(20 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[2].Width = checked(30 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[3].Width = checked(50 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[4].Width = checked(20 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[5].Width = checked(20 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[6].Width = checked(20 * dataGridSales.Width) / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[7].Width = checked(20 * dataGridSales.Width) / 100;
		}

		private void SetupPurchaseGrid()
		{
			dataGridPurchase.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Date");
			dataTable.Columns.Add("Supplier");
			dataTable.Columns.Add("Unit");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataTable.Columns.Add("Price", typeof(double));
			dataTable.Columns.Add("Amount", typeof(double));
			dataGridPurchase.DataSource = dataTable;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[4].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[5].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[6].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[7].CellActivation = Activation.NoEdit;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[0].Width = checked(20 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[1].Width = checked(20 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[2].Width = checked(30 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[3].Width = checked(50 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[4].Width = checked(20 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[5].Width = checked(20 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[6].Width = checked(20 * dataGridPurchase.Width) / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[7].Width = checked(20 * dataGridPurchase.Width) / 100;
		}

		private void SetupMultilocationGrid()
		{
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Station Name");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataGridMultiLocation.DataSource = dataTable;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[0].Width = checked(50 * dataGridMultiLocation.Width) / 100;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[1].Width = checked(50 * dataGridMultiLocation.Width) / 100;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MaxLength = 20;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].Format = "0.####";
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MinValue = -99999999m;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns["Quantity"].MaxValue = 99999999m;
		}

		private void DisplaySalesDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail("", SelectedID);
			new DataSet();
			DataTable dataTable = dataGridSales.DataSource as DataTable;
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Customer"] = row["CustomerName"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayPurchaseDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail("", SelectedID);
			new DataSet();
			DataTable dataTable = dataGridPurchase.DataSource as DataTable;
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["SysDocID"] = row["SysDocID"];
					dataRow2["VoucherID"] = row["VoucherID"];
					dataRow2["Date"] = row["Date"];
					dataRow2["Supplier"] = row["VendorName"];
					dataRow2["Quantity"] = row["Quantity"];
					dataRow2["Price"] = row["UnitPrice"];
					dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayProductUnderLocation(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.ProductSystem.GetProductAvailability(SelectedID, "");
			new DataSet();
			DataTable dataTable = dataGridMultiLocation.DataSource as DataTable;
			dataTable.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[0]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Station Name"] = row["Location Name"];
					dataRow2["Quantity"] = row["Onhand"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void LoadGridData()
		{
			DataSet dataSet = new DataSet();
			dataSet = (DataSource = CombosData.GetProductList(refresh: false));
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
			panelButtons = new System.Windows.Forms.Panel();
			labelLastCost = new System.Windows.Forms.Label();
			textBoxLastCost = new Micromind.UISupport.NumberTextBox();
			labelMinPrice = new Micromind.UISupport.MMLabel();
			labelSpecialPrice = new Micromind.UISupport.MMLabel();
			labelWholesalePrice = new Micromind.UISupport.MMLabel();
			labelStandardPrice = new Micromind.UISupport.MMLabel();
			textBoxMinimumPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxSpecialPrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxWholesalePrice = new Micromind.UISupport.UnitPriceTextBox();
			textBoxStandardPrice = new Micromind.UISupport.UnitPriceTextBox();
			labelUnitPrice = new System.Windows.Forms.Label();
			textBoxCost = new Micromind.UISupport.NumberTextBox();
			label37 = new System.Windows.Forms.Label();
			dataGridMultiLocation = new Micromind.DataControls.DataEntryGrid();
			groupBox2 = new System.Windows.Forms.GroupBox();
			dataGridSales = new Micromind.DataControls.DataEntryGrid();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			groupBox1 = new System.Windows.Forms.GroupBox();
			dataGridPurchase = new Micromind.DataControls.DataEntryGrid();
			textBoxSearch = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			panelItemdetails = new System.Windows.Forms.Panel();
			panel1 = new System.Windows.Forms.Panel();
			labelFactor = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			labelCost = new System.Windows.Forms.Label();
			labelStock = new System.Windows.Forms.Label();
			labelCountry = new System.Windows.Forms.Label();
			labelManufacturer = new System.Windows.Forms.Label();
			labelBrand = new System.Windows.Forms.Label();
			labelCategory = new System.Windows.Forms.Label();
			labelUnit = new System.Windows.Forms.Label();
			productPhotoViewer = new Micromind.DataControls.ProductPhotoViewer();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			textBoxUnitFactor = new System.Windows.Forms.TextBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).BeginInit();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridSales).BeginInit();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).BeginInit();
			panelItemdetails.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(labelLastCost);
			panelButtons.Controls.Add(textBoxLastCost);
			panelButtons.Controls.Add(labelMinPrice);
			panelButtons.Controls.Add(labelSpecialPrice);
			panelButtons.Controls.Add(labelWholesalePrice);
			panelButtons.Controls.Add(labelStandardPrice);
			panelButtons.Controls.Add(textBoxMinimumPrice);
			panelButtons.Controls.Add(textBoxSpecialPrice);
			panelButtons.Controls.Add(textBoxWholesalePrice);
			panelButtons.Controls.Add(textBoxStandardPrice);
			panelButtons.Controls.Add(labelUnitPrice);
			panelButtons.Controls.Add(textBoxCost);
			panelButtons.Controls.Add(label37);
			panelButtons.Controls.Add(dataGridMultiLocation);
			panelButtons.Controls.Add(groupBox2);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(groupBox1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 42);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(927, 518);
			panelButtons.TabIndex = 3;
			labelLastCost.AutoSize = true;
			labelLastCost.Location = new System.Drawing.Point(236, 237);
			labelLastCost.Name = "labelLastCost";
			labelLastCost.Size = new System.Drawing.Size(102, 13);
			labelLastCost.TabIndex = 234;
			labelLastCost.Text = "Last Purchase Cost:";
			textBoxLastCost.AllowDecimal = true;
			textBoxLastCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastCost.CustomReportFieldName = "";
			textBoxLastCost.CustomReportKey = "";
			textBoxLastCost.CustomReportValueType = 1;
			textBoxLastCost.IsComboTextBox = false;
			textBoxLastCost.IsModified = false;
			textBoxLastCost.Location = new System.Drawing.Point(344, 237);
			textBoxLastCost.MaxLength = 15;
			textBoxLastCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxLastCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxLastCost.Name = "textBoxLastCost";
			textBoxLastCost.NullText = "0";
			textBoxLastCost.ReadOnly = true;
			textBoxLastCost.Size = new System.Drawing.Size(120, 20);
			textBoxLastCost.TabIndex = 233;
			textBoxLastCost.Text = "0.00";
			textBoxLastCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelMinPrice.AutoSize = true;
			labelMinPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelMinPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelMinPrice.IsFieldHeader = false;
			labelMinPrice.IsRequired = false;
			labelMinPrice.Location = new System.Drawing.Point(22, 326);
			labelMinPrice.Name = "labelMinPrice";
			labelMinPrice.PenWidth = 1f;
			labelMinPrice.ShowBorder = false;
			labelMinPrice.Size = new System.Drawing.Size(78, 13);
			labelMinPrice.TabIndex = 225;
			labelMinPrice.Text = "Minimum Price:";
			labelSpecialPrice.AutoSize = true;
			labelSpecialPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSpecialPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelSpecialPrice.IsFieldHeader = false;
			labelSpecialPrice.IsRequired = false;
			labelSpecialPrice.Location = new System.Drawing.Point(22, 304);
			labelSpecialPrice.Name = "labelSpecialPrice";
			labelSpecialPrice.PenWidth = 1f;
			labelSpecialPrice.ShowBorder = false;
			labelSpecialPrice.Size = new System.Drawing.Size(65, 13);
			labelSpecialPrice.TabIndex = 226;
			labelSpecialPrice.Text = "Unit Price 3:";
			labelWholesalePrice.AutoSize = true;
			labelWholesalePrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelWholesalePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelWholesalePrice.IsFieldHeader = false;
			labelWholesalePrice.IsRequired = false;
			labelWholesalePrice.Location = new System.Drawing.Point(22, 282);
			labelWholesalePrice.Name = "labelWholesalePrice";
			labelWholesalePrice.PenWidth = 1f;
			labelWholesalePrice.ShowBorder = false;
			labelWholesalePrice.Size = new System.Drawing.Size(65, 13);
			labelWholesalePrice.TabIndex = 227;
			labelWholesalePrice.Text = "Unit Price 2:";
			labelStandardPrice.AutoSize = true;
			labelStandardPrice.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelStandardPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelStandardPrice.IsFieldHeader = false;
			labelStandardPrice.IsRequired = false;
			labelStandardPrice.Location = new System.Drawing.Point(22, 260);
			labelStandardPrice.Name = "labelStandardPrice";
			labelStandardPrice.PenWidth = 1f;
			labelStandardPrice.ShowBorder = false;
			labelStandardPrice.Size = new System.Drawing.Size(65, 13);
			labelStandardPrice.TabIndex = 228;
			labelStandardPrice.Text = "Unit Price 1:";
			textBoxMinimumPrice.CustomReportFieldName = "";
			textBoxMinimumPrice.CustomReportKey = "";
			textBoxMinimumPrice.CustomReportValueType = 1;
			textBoxMinimumPrice.IsComboTextBox = false;
			textBoxMinimumPrice.IsModified = false;
			textBoxMinimumPrice.Location = new System.Drawing.Point(114, 323);
			textBoxMinimumPrice.MaxLength = 10;
			textBoxMinimumPrice.Name = "textBoxMinimumPrice";
			textBoxMinimumPrice.Size = new System.Drawing.Size(120, 20);
			textBoxMinimumPrice.TabIndex = 232;
			textBoxMinimumPrice.Text = "0.00";
			textBoxMinimumPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxSpecialPrice.CustomReportFieldName = "";
			textBoxSpecialPrice.CustomReportKey = "";
			textBoxSpecialPrice.CustomReportValueType = 1;
			textBoxSpecialPrice.IsComboTextBox = false;
			textBoxSpecialPrice.IsModified = false;
			textBoxSpecialPrice.Location = new System.Drawing.Point(114, 301);
			textBoxSpecialPrice.MaxLength = 10;
			textBoxSpecialPrice.Name = "textBoxSpecialPrice";
			textBoxSpecialPrice.Size = new System.Drawing.Size(120, 20);
			textBoxSpecialPrice.TabIndex = 231;
			textBoxSpecialPrice.Text = "0.00";
			textBoxSpecialPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxWholesalePrice.CustomReportFieldName = "";
			textBoxWholesalePrice.CustomReportKey = "";
			textBoxWholesalePrice.CustomReportValueType = 1;
			textBoxWholesalePrice.IsComboTextBox = false;
			textBoxWholesalePrice.IsModified = false;
			textBoxWholesalePrice.Location = new System.Drawing.Point(114, 279);
			textBoxWholesalePrice.MaxLength = 10;
			textBoxWholesalePrice.Name = "textBoxWholesalePrice";
			textBoxWholesalePrice.Size = new System.Drawing.Size(120, 20);
			textBoxWholesalePrice.TabIndex = 230;
			textBoxWholesalePrice.Text = "0.00";
			textBoxWholesalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxStandardPrice.CustomReportFieldName = "";
			textBoxStandardPrice.CustomReportKey = "";
			textBoxStandardPrice.CustomReportValueType = 1;
			textBoxStandardPrice.IsComboTextBox = false;
			textBoxStandardPrice.IsModified = false;
			textBoxStandardPrice.Location = new System.Drawing.Point(114, 257);
			textBoxStandardPrice.MaxLength = 10;
			textBoxStandardPrice.Name = "textBoxStandardPrice";
			textBoxStandardPrice.Size = new System.Drawing.Size(120, 20);
			textBoxStandardPrice.TabIndex = 229;
			textBoxStandardPrice.Text = "0.00";
			textBoxStandardPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelUnitPrice.AutoSize = true;
			labelUnitPrice.Location = new System.Drawing.Point(22, 237);
			labelUnitPrice.Name = "labelUnitPrice";
			labelUnitPrice.Size = new System.Drawing.Size(31, 13);
			labelUnitPrice.TabIndex = 224;
			labelUnitPrice.Text = "Cost:";
			textBoxCost.AllowDecimal = true;
			textBoxCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCost.CustomReportFieldName = "";
			textBoxCost.CustomReportKey = "";
			textBoxCost.CustomReportValueType = 1;
			textBoxCost.IsComboTextBox = false;
			textBoxCost.IsModified = false;
			textBoxCost.Location = new System.Drawing.Point(114, 234);
			textBoxCost.MaxLength = 15;
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
			textBoxCost.Size = new System.Drawing.Size(120, 20);
			textBoxCost.TabIndex = 223;
			textBoxCost.Text = "0.00";
			textBoxCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label37.Location = new System.Drawing.Point(702, 243);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(105, 13);
			label37.TabIndex = 212;
			label37.Text = "MULTI LOCATION";
			dataGridMultiLocation.AllowAddNew = false;
			dataGridMultiLocation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridMultiLocation.DisplayLayout.Appearance = appearance;
			dataGridMultiLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridMultiLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridMultiLocation.DisplayLayout.MaxColScrollRegions = 1;
			dataGridMultiLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridMultiLocation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridMultiLocation.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridMultiLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridMultiLocation.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridMultiLocation.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridMultiLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridMultiLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridMultiLocation.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridMultiLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridMultiLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridMultiLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridMultiLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridMultiLocation.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridMultiLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridMultiLocation.ExitEditModeOnLeave = false;
			dataGridMultiLocation.IncludeLotItems = false;
			dataGridMultiLocation.LoadLayoutFailed = false;
			dataGridMultiLocation.Location = new System.Drawing.Point(587, 260);
			dataGridMultiLocation.Name = "dataGridMultiLocation";
			dataGridMultiLocation.ShowClearMenu = true;
			dataGridMultiLocation.ShowDeleteMenu = true;
			dataGridMultiLocation.ShowInsertMenu = true;
			dataGridMultiLocation.ShowMoveRowsMenu = true;
			dataGridMultiLocation.Size = new System.Drawing.Size(328, 101);
			dataGridMultiLocation.TabIndex = 211;
			dataGridMultiLocation.Text = "dataEntryGrid3";
			groupBox2.Controls.Add(dataGridSales);
			groupBox2.Location = new System.Drawing.Point(508, 366);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(411, 111);
			groupBox2.TabIndex = 124;
			groupBox2.TabStop = false;
			groupBox2.Text = "Sale Details";
			dataGridSales.AllowAddNew = false;
			dataGridSales.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSales.DisplayLayout.Appearance = appearance13;
			dataGridSales.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSales.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridSales.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridSales.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSales.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSales.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSales.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridSales.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSales.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSales.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSales.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridSales.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSales.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridSales.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridSales.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSales.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridSales.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridSales.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSales.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridSales.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSales.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSales.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridSales.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSales.ExitEditModeOnLeave = false;
			dataGridSales.IncludeLotItems = false;
			dataGridSales.LoadLayoutFailed = false;
			dataGridSales.Location = new System.Drawing.Point(4, 13);
			dataGridSales.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridSales.Name = "dataGridSales";
			dataGridSales.ShowClearMenu = true;
			dataGridSales.ShowDeleteMenu = true;
			dataGridSales.ShowInsertMenu = true;
			dataGridSales.ShowMoveRowsMenu = true;
			dataGridSales.Size = new System.Drawing.Size(403, 93);
			dataGridSales.TabIndex = 123;
			dataGridSales.Text = "dataEntryGrid1";
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(712, 486);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(927, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(817, 486);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			groupBox1.Controls.Add(dataGridPurchase);
			groupBox1.Location = new System.Drawing.Point(15, 366);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(427, 111);
			groupBox1.TabIndex = 123;
			groupBox1.TabStop = false;
			groupBox1.Text = "Purchase Details";
			dataGridPurchase.AllowAddNew = false;
			dataGridPurchase.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPurchase.DisplayLayout.Appearance = appearance25;
			dataGridPurchase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPurchase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridPurchase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridPurchase.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPurchase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPurchase.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPurchase.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridPurchase.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPurchase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPurchase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPurchase.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridPurchase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPurchase.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridPurchase.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridPurchase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPurchase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridPurchase.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridPurchase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPurchase.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridPurchase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPurchase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPurchase.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridPurchase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPurchase.ExitEditModeOnLeave = false;
			dataGridPurchase.IncludeLotItems = false;
			dataGridPurchase.LoadLayoutFailed = false;
			dataGridPurchase.Location = new System.Drawing.Point(4, 14);
			dataGridPurchase.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridPurchase.Name = "dataGridPurchase";
			dataGridPurchase.ShowClearMenu = true;
			dataGridPurchase.ShowDeleteMenu = true;
			dataGridPurchase.ShowInsertMenu = true;
			dataGridPurchase.ShowMoveRowsMenu = true;
			dataGridPurchase.Size = new System.Drawing.Size(419, 91);
			dataGridPurchase.TabIndex = 3;
			dataGridPurchase.Text = "dataEntryGrid1";
			textBoxSearch.Location = new System.Drawing.Point(48, 13);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(394, 20);
			textBoxSearch.TabIndex = 4;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 5;
			label1.Text = "Find:";
			panelItemdetails.Controls.Add(panel1);
			panelItemdetails.Controls.Add(productPhotoViewer);
			panelItemdetails.Location = new System.Drawing.Point(502, 44);
			panelItemdetails.Name = "panelItemdetails";
			panelItemdetails.Size = new System.Drawing.Size(422, 238);
			panelItemdetails.TabIndex = 7;
			panel1.Controls.Add(textBoxUnitFactor);
			panel1.Controls.Add(labelFactor);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label3);
			panel1.Controls.Add(label4);
			panel1.Controls.Add(label5);
			panel1.Controls.Add(label6);
			panel1.Controls.Add(label7);
			panel1.Controls.Add(label8);
			panel1.Controls.Add(labelCost);
			panel1.Controls.Add(labelStock);
			panel1.Controls.Add(labelCountry);
			panel1.Controls.Add(labelManufacturer);
			panel1.Controls.Add(labelBrand);
			panel1.Controls.Add(labelCategory);
			panel1.Controls.Add(labelUnit);
			panel1.Location = new System.Drawing.Point(6, 3);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(221, 232);
			panel1.TabIndex = 122;
			labelFactor.AutoSize = true;
			labelFactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			labelFactor.Location = new System.Drawing.Point(6, 195);
			labelFactor.Name = "labelFactor";
			labelFactor.Size = new System.Drawing.Size(78, 13);
			labelFactor.TabIndex = 236;
			labelFactor.Text = "Unit Factor :";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(6, 172);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(40, 13);
			label2.TabIndex = 15;
			label2.Text = "Cost :";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(6, 149);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(93, 13);
			label3.TabIndex = 14;
			label3.Text = "Stock In Hand:";
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(6, 124);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(58, 13);
			label4.TabIndex = 13;
			label4.Text = "Country :";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(6, 98);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(90, 13);
			label5.TabIndex = 12;
			label5.Text = "Manufacturer :";
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(6, 72);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(48, 13);
			label6.TabIndex = 11;
			label6.Text = "Brand :";
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(6, 48);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(65, 13);
			label7.TabIndex = 10;
			label7.Text = "Category :";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(6, 21);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(38, 13);
			label8.TabIndex = 9;
			label8.Text = "Unit :";
			labelCost.AutoSize = true;
			labelCost.Location = new System.Drawing.Point(72, 172);
			labelCost.Name = "labelCost";
			labelCost.Size = new System.Drawing.Size(10, 13);
			labelCost.TabIndex = 7;
			labelCost.Text = "t";
			labelStock.AutoSize = true;
			labelStock.Location = new System.Drawing.Point(99, 149);
			labelStock.Name = "labelStock";
			labelStock.Size = new System.Drawing.Size(10, 13);
			labelStock.TabIndex = 6;
			labelStock.Text = "t";
			labelCountry.AutoSize = true;
			labelCountry.Location = new System.Drawing.Point(72, 124);
			labelCountry.Name = "labelCountry";
			labelCountry.Size = new System.Drawing.Size(10, 13);
			labelCountry.TabIndex = 5;
			labelCountry.Text = "t";
			labelManufacturer.AutoSize = true;
			labelManufacturer.Location = new System.Drawing.Point(97, 99);
			labelManufacturer.Name = "labelManufacturer";
			labelManufacturer.Size = new System.Drawing.Size(10, 13);
			labelManufacturer.TabIndex = 4;
			labelManufacturer.Text = "t";
			labelBrand.AutoSize = true;
			labelBrand.Location = new System.Drawing.Point(72, 72);
			labelBrand.Name = "labelBrand";
			labelBrand.Size = new System.Drawing.Size(10, 13);
			labelBrand.TabIndex = 3;
			labelBrand.Text = "t";
			labelCategory.AutoSize = true;
			labelCategory.Location = new System.Drawing.Point(72, 48);
			labelCategory.Name = "labelCategory";
			labelCategory.Size = new System.Drawing.Size(10, 13);
			labelCategory.TabIndex = 2;
			labelCategory.Text = "t";
			labelUnit.AutoSize = true;
			labelUnit.Location = new System.Drawing.Point(72, 21);
			labelUnit.Name = "labelUnit";
			labelUnit.Size = new System.Drawing.Size(10, 13);
			labelUnit.TabIndex = 1;
			labelUnit.Text = "t";
			productPhotoViewer.BackColor = System.Drawing.Color.White;
			productPhotoViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			productPhotoViewer.Location = new System.Drawing.Point(233, 3);
			productPhotoViewer.MaximumSize = new System.Drawing.Size(186, 250);
			productPhotoViewer.MinimumSize = new System.Drawing.Size(186, 162);
			productPhotoViewer.Name = "productPhotoViewer";
			productPhotoViewer.Size = new System.Drawing.Size(186, 207);
			productPhotoViewer.TabIndex = 121;
			productPhotoViewer.Visible = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance37;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(10, 54);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(486, 203);
			dataGridItems.TabIndex = 6;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.AfterRowActivate += new System.EventHandler(dataGridItems_AfterRowActivate);
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			textBoxUnitFactor.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxUnitFactor.Location = new System.Drawing.Point(85, 191);
			textBoxUnitFactor.Multiline = true;
			textBoxUnitFactor.Name = "textBoxUnitFactor";
			textBoxUnitFactor.ReadOnly = true;
			textBoxUnitFactor.Size = new System.Drawing.Size(129, 38);
			textBoxUnitFactor.TabIndex = 237;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(927, 560);
			base.Controls.Add(panelItemdetails);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(panelButtons);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ProductDataDetailsForm";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Product Data ";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).EndInit();
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridSales).EndInit();
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).EndInit();
			panelItemdetails.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
