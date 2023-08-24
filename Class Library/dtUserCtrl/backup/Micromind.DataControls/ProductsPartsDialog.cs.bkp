using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductsPartsDialog : Form
	{
		public bool CanClose = true;

		private bool isMultiSelect;

		private bool loadItemDetails = CompanyPreferences.LoadItemFeatures;

		private DataSet currentData;

		private bool canAccessCost = true;

		private bool loadAll;

		private string productID = "";

		private ScreenAccessRight screenRight;

		private bool AllowEditCard;

		private string selectedItem = "";

		private string selectedProvider = "";

		private string selectedVendor = "";

		private IContainer components;

		private Panel panelButtons;

		private XPButton buttonOK;

		private Micromind.UISupport.Line linePanelDown;

		private XPButton buttonCancel;

		private GroupBox groupBox2;

		private DataEntryGrid dataGridSales;

		private Label label27;

		private TextBox textBoxSpecification;

		private Label label26;

		private TextBox textBoxProductName;

		private Label label25;

		private TextBox textBoxOwnRefNo;

		private Label label24;

		private UltraGroupBox ultraGroupBox1;

		private DataEntryGrid dataGridMultiLocation;

		private DataEntryGrid dataGridAppliedModels;

		private DataEntryGrid dataGridSubstituteParts;

		private UltraGroupBox ultraGroupBox2;

		private Label label22;

		private TextBox textBoxEngineNumber;

		private Label label21;

		private TextBox textBoxModelNumber;

		private Label label20;

		private TextBox textBoxChaseNo;

		private Label label19;

		private TextBox textBoxPartsFamily;

		private Label label18;

		private TextBox textBoxOEMCode;

		private Label label17;

		private TextBox textBoxPartsType;

		private Label labelUnitPrice;

		private NumberTextBox textBoxUnitPrice;

		private Label labelMinUnitPrice;

		private NumberTextBox textBoxMinUnitPrice;

		private Label label14;

		private NumberTextBox textBoxOnHand;

		private Label label13;

		private NumberTextBox textBoxPackQty;

		private Label label12;

		private TextBox textBoxUnit;

		private Label label11;

		private TextBox textBoxStockType;

		private Label label10;

		private NumberTextBox textBoxReorderLevel;

		private Label label9;

		private NumberTextBox textBoxReorderQty;

		private Label label8;

		private TextBox textBoxPacketDetails;

		private Label label7;

		private TextBox textBoxSupRefNo;

		private Label label6;

		private TextBox textBoxVehicleMake;

		private Label label5;

		private TextBox textBoxVehicleType;

		private Label label4;

		private TextBox textBoxModel1;

		private Label label3;

		private TextBox textBoxMakeType;

		private Label label2;

		private TextBox textBoxLocation;

		private Label label1;

		private TextBox textBoxProductBrand;

		private GroupBox groupBox1;

		private DataEntryGrid dataGridPurchase;

		private MMSListGrid dataGridItems;

		private Label label23;

		private TextBox textBoxSearch;

		private TextBox textBoxProduct;

		private Label label37;

		private Label label36;

		private Label label35;

		private Label label28;

		private TextBox textBoxOrigin;

		private XPButton xpButton1;

		private XPButton buttonLoadAll;

		private Label labelLastCost;

		private NumberTextBox textBoxLastCost;

		private Label label15;

		private NumberTextBox textBoxAverageCost;

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

		public string SelectedProvider
		{
			get
			{
				return selectedProvider;
			}
			set
			{
				selectedProvider = value;
			}
		}

		public string SelectedVendor
		{
			get
			{
				return selectedVendor;
			}
			set
			{
				selectedVendor = value;
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
				if (dataGridItems.DataSource == null || dataGridItems.DataSource.ToString() == "")
				{
					DataSet dataSet = new DataSet();
					return DataSource = Factory.ProductSystem.GetProducts();
				}
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

		public ProductsPartsDialog()
		{
			InitializeComponent();
			base.Load += SelectDocumentDialog_Load;
			textBoxSearch.KeyDown += textBoxSearch_KeyDown;
			base.Activated += SelectDocumentDialog_Activated;
			SetSecurity();
			if (!loadItemDetails)
			{
				base.Size = new Size(525, 584);
			}
		}

		private void SetSecurity()
		{
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
		}

		private void SelectDocumentDialog_Activated(object sender, EventArgs e)
		{
			textBoxSearch.Focus();
		}

		private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
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

		private void SelectDocumentDialog_Load(object sender, EventArgs e)
		{
			dataGridItems.ApplyUIDesign();
			dataGridItems.DisplayLayout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessCost))
			{
				canAccessCost = false;
			}
			else
			{
				canAccessCost = true;
			}
			dataGridItems.DataSource = DataSource;
			dataGridSales.SetupUI();
			dataGridPurchase.SetupUI();
			dataGridSubstituteParts.SetupUI();
			dataGridAppliedModels.SetupUI();
			dataGridMultiLocation.SetupUI();
			SetupSalesGrid();
			SetupPurchaseGrid();
			SetupSubstitutePartsGrid();
			SetupAppliedModelsGrid();
			SetupMultilocationGrid();
			if (SelectedItem != "")
			{
				textBoxSearch.Text = SelectedItem.Trim();
				textBoxProduct.Text = SelectedItem.Trim();
			}
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
				loadAll = false;
				dataGridItems.DataSource = DataSource;
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

		private void dataGridSubstituteItems_DoubleClickRow(object sender, DoubleClickRowEventArgs e)
		{
			try
			{
				if (dataGridSubstituteParts.ActiveRow != null)
				{
					productID = dataGridSubstituteParts.ActiveRow.Cells["Part No"].Value.ToString();
					if (!string.IsNullOrEmpty(productID))
					{
						textBoxSearch.Text = productID;
						DisplayProductsPartsDetails(productID);
						DisplayPurchaseDetails(productID);
						DisplaySalesDetails(productID);
						DisplayProductUnderLocation(productID);
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			_ = CanClose;
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
			textBoxProduct.Clear();
			textBoxProductBrand.Clear();
			textBoxModel1.Clear();
			textBoxOwnRefNo.Clear();
			textBoxOrigin.Clear();
			textBoxPacketDetails.Clear();
			textBoxPartsFamily.Clear();
			textBoxChaseNo.Clear();
			textBoxLocation.Clear();
			textBoxEngineNumber.Clear();
			textBoxMakeType.Clear();
			textBoxModel1.Clear();
			textBoxOnHand.Clear();
			textBoxPackQty.Clear();
			textBoxPartsType.Clear();
			textBoxProductName.Clear();
			textBoxSupRefNo.Clear();
			textBoxStockType.Clear();
			textBoxMinUnitPrice.Clear();
			textBoxUnitPrice.Clear();
			textBoxReorderLevel.Clear();
		}

		private void dataGridItems_AfterRowActivate(object sender, EventArgs e)
		{
			if (selectedItem == null || selectedItem == "")
			{
				if (dataGridItems.ActiveRow == null)
				{
					return;
				}
				ClearForm();
				if (dataGridItems.ActiveRow.Cells["Code"].Value != null && dataGridItems.ActiveRow.Cells["Code"].Value.ToString() != "" && loadItemDetails)
				{
					productID = dataGridItems.ActiveRow.Cells["Code"].Value.ToString();
					DataSet dataSet = new DataSet();
					dataSet = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(productID));
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						_ = dataSet.Tables[0].Rows[0];
					}
					(dataGridSales.DataSource as DataTable).Rows.Clear();
					(dataGridPurchase.DataSource as DataTable).Rows.Clear();
					(dataGridSubstituteParts.DataSource as DataTable).Rows.Clear();
					(dataGridAppliedModels.DataSource as DataTable).Rows.Clear();
					DisplayPurchaseDetails(productID);
					DisplaySalesDetails(productID);
					DisplayProductsPartsDetails(productID);
					DisplayProductUnderLocation(productID);
				}
			}
			else
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
				string selectedID = dataGridItems.ActiveRow.Cells["Code"].Value.ToString();
				DataSet dataSet2 = new DataSet();
				dataSet2 = CommonLib.DecompressDataSet(Factory.ProductSystem.GetItemFeatures(selectedID));
				if (dataSet2.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet2.Tables[0].Rows[0];
					textBoxProductBrand.Text = dataRow["BrandName"].ToString();
					textBoxModel1.Text = "";
					textBoxOwnRefNo.Text = "";
					textBoxOrigin.Text = dataRow["CountryName"].ToString();
					textBoxPacketDetails.Text = "";
					textBoxPartsFamily.Text = "";
					textBoxChaseNo.Text = "";
					textBoxLocation.Text = "";
					textBoxEngineNumber.Text = "";
					textBoxMakeType.Text = "";
					textBoxModel1.Text = "";
					textBoxOnHand.Text = dataRow["Quantity"].ToString();
					textBoxPackQty.Text = "";
					textBoxPartsType.Text = "";
					textBoxProductName.Text = dataRow["Description"].ToString();
					textBoxSupRefNo.Text = dataRow["VendorRef"].ToString();
					textBoxStockType.Text = dataRow["Item  Type"].ToString();
					NumberTextBox numberTextBox = textBoxMinUnitPrice;
					NumberTextBox numberTextBox2 = textBoxUnitPrice;
					Label label = labelMinUnitPrice;
					bool flag2 = labelUnitPrice.Visible = true;
					bool flag4 = label.Visible = flag2;
					bool visible = numberTextBox2.Visible = flag4;
					numberTextBox.Visible = visible;
					textBoxMinUnitPrice.Text = dataRow["MinPrice"].ToString();
					textBoxUnitPrice.Text = dataRow["UnitPrice1"].ToString();
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemPrice1))
					{
						NumberTextBox numberTextBox3 = textBoxUnitPrice;
						visible = (labelUnitPrice.Visible = false);
						numberTextBox3.Visible = visible;
					}
					if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AccessItemMinPrice))
					{
						NumberTextBox numberTextBox4 = textBoxMinUnitPrice;
						visible = (labelMinUnitPrice.Visible = false);
						numberTextBox4.Visible = visible;
					}
					textBoxReorderLevel.Text = dataRow["ReorderLevel"].ToString();
					if (!string.IsNullOrEmpty(dataRow["LAST COST"].ToString()))
					{
						textBoxLastCost.Text = decimal.Parse(dataRow["LAST COST"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxLastCost.Text = 0.0.ToString();
					}
					if (!string.IsNullOrEmpty(dataRow["AverageCost"].ToString()))
					{
						textBoxAverageCost.Text = decimal.Parse(dataRow["AverageCost"].ToString()).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxAverageCost.Text = 0.0.ToString();
					}
				}
				(dataGridSales.DataSource as DataTable).Rows.Clear();
				(dataGridPurchase.DataSource as DataTable).Rows.Clear();
				DisplayPurchaseDetails(selectedID);
				DisplaySalesDetails(selectedID);
				DisplayProductsPartsDetails(selectedID);
				DisplayProductUnderLocation(selectedID);
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
			dataGridSales.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[3].Width = 50 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[4].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[5].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[6].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns[7].Width = 20 * dataGridSales.Width / 100;
			dataGridSales.DisplayLayout.Bands[0].Columns["Amount"].Header.Caption = "Total";
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
			dataGridPurchase.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[3].Width = 50 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[4].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[5].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[6].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns[7].Width = 20 * dataGridPurchase.Width / 100;
			dataGridPurchase.DisplayLayout.Bands[0].Columns["Amount"].Header.Caption = "Total";
			UltraGridColumn ultraGridColumn = dataGridPurchase.DisplayLayout.Bands[0].Columns["Amount"];
			bool hidden = dataGridPurchase.DisplayLayout.Bands[0].Columns["Price"].Hidden = !canAccessCost;
			ultraGridColumn.Hidden = hidden;
		}

		private void SetupSubstitutePartsGrid()
		{
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Part No");
			dataTable.Columns.Add("Part Name");
			dataTable.Columns.Add("Selling Price");
			dataTable.Columns.Add("Quantity", typeof(double));
			dataGridSubstituteParts.DataSource = dataTable;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[3].CellActivation = Activation.NoEdit;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridSubstituteParts.Width / 100;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridSubstituteParts.Width / 100;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridSubstituteParts.Width / 100;
			dataGridSubstituteParts.DisplayLayout.Bands[0].Columns[3].Width = 50 * dataGridSubstituteParts.Width / 100;
		}

		private void SetupAppliedModelsGrid()
		{
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("Make");
			dataTable.Columns.Add("Type");
			dataTable.Columns.Add("Remarks");
			dataGridAppliedModels.DataSource = dataTable;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[0].CellActivation = Activation.NoEdit;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[1].CellActivation = Activation.NoEdit;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[2].CellActivation = Activation.NoEdit;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[0].Width = 20 * dataGridAppliedModels.Width / 100;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[1].Width = 20 * dataGridAppliedModels.Width / 100;
			dataGridAppliedModels.DisplayLayout.Bands[0].Columns[2].Width = 30 * dataGridAppliedModels.Width / 100;
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
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[0].Width = 50 * dataGridMultiLocation.Width / 100;
			dataGridMultiLocation.DisplayLayout.Bands[0].Columns[1].Width = 50 * dataGridMultiLocation.Width / 100;
		}

		private void DisplayProductsPartsDetails(string SelectedID)
		{
			DataSet dataSet = null;
			textBoxProduct.Text = SelectedID;
			if (!string.IsNullOrEmpty(SelectedID))
			{
				textBoxProductName.Text = Factory.DatabaseSystem.GetFieldValue("Product", "Description", "ProductID", SelectedID).ToString();
			}
			dataSet = Factory.ProductSystem.GetProductPartsDetail(SelectedID);
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					return;
				}
				DataRow dataRow = dataSet.Tables[0].Rows[0];
				textBoxSpecification.Text = dataRow["Specification"].ToString();
				textBoxModel1.Text = dataRow["Vehicle Model"].ToString();
				textBoxModelNumber.Text = dataRow["Vehicle Model"].ToString();
				textBoxOwnRefNo.Text = "";
				textBoxPacketDetails.Text = "";
				textBoxPartsFamily.Text = dataRow["Parts Family"].ToString();
				textBoxChaseNo.Text = dataRow["PartsChasisNo"].ToString();
				textBoxLocation.Text = "";
				textBoxEngineNumber.Text = dataRow["PartsEngineNo"].ToString();
				textBoxMakeType.Text = dataRow["Parts Make"].ToString();
				textBoxModel1.Text = dataRow["Vehicle Model"].ToString();
				textBoxPartsType.Text = dataRow["Parts Type"].ToString();
				textBoxPackQty.Text = "";
				textBoxOEMCode.Text = dataRow["OEMCode"].ToString();
				textBoxVehicleType.Text = dataRow["VehicleTypeID"].ToString();
				textBoxVehicleMake.Text = dataRow["VehicleMakeID"].ToString();
			}
			new DataSet();
			DataTable dataTable = dataGridAppliedModels.DataSource as DataTable;
			DataTable dataTable2 = dataGridSubstituteParts.DataSource as DataTable;
			dataTable.Rows.Clear();
			dataTable2.Rows.Clear();
			if (dataSet != null && dataSet.Tables.Count > 0)
			{
				DataView dataView = new DataView(dataSet.Tables[1]);
				new DataTable();
				foreach (DataRow row in dataView.ToTable().Rows)
				{
					DataRow dataRow3 = dataTable.NewRow();
					dataRow3["Make"] = row["Make_Applied"];
					dataRow3["Type"] = row["VehicleTypeID"];
					dataRow3["Remarks"] = row["Remarks"];
					dataRow3.EndEdit();
					dataTable.Rows.Add(dataRow3);
				}
				dataTable.AcceptChanges();
				DataView dataView2 = new DataView(dataSet.Tables[2]);
				new DataTable();
				foreach (DataRow row2 in dataView2.ToTable().Rows)
				{
					DataRow dataRow5 = dataTable2.NewRow();
					dataRow5["Part No"] = row2["SubstituteProductID"];
					dataRow5["Part Name"] = row2["SubProductDescription"];
					dataRow5["Selling Price"] = row2["UnitPrice"];
					dataRow5["Quantity"] = 0.0;
					dataRow5.EndEdit();
					dataTable2.Rows.Add(dataRow5);
				}
				dataTable2.AcceptChanges();
			}
		}

		private void DisplaySalesDetails(string SelectedID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail("", SelectedID, loadAll);
			new DataSet();
			DataTable dataTable = dataGridSales.DataSource as DataTable;
			dataTable.Rows.Clear();
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

		private void DisplaySalesDetailsCustomer(string CustomerID, string productID)
		{
			DataSet dataSet = null;
			dataSet = Factory.CustomerSystem.GetInventorySalesItemDetail("", productID, loadAll);
			new DataSet();
			DataTable dataTable = dataGridSales.DataSource as DataTable;
			dataTable.Rows.Clear();
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
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail("", SelectedID, loadAll);
			new DataSet();
			DataTable dataTable = dataGridPurchase.DataSource as DataTable;
			dataTable.Rows.Clear();
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
					if (canAccessCost)
					{
						dataRow2["Price"] = row["UnitPrice"];
						dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					}
					else
					{
						UltraGridColumn ultraGridColumn = dataGridPurchase.DisplayLayout.Bands[0].Columns["Amount"];
						bool hidden = dataGridPurchase.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
						ultraGridColumn.Hidden = hidden;
					}
					dataRow2["Unit"] = row["UnitID"];
					dataRow2.EndEdit();
					dataTable.Rows.Add(dataRow2);
				}
				dataTable.AcceptChanges();
			}
		}

		private void DisplayPurchaseDetails(string vendorID, string productID)
		{
			DataSet dataSet = null;
			dataSet = Factory.VendorSystem.GetInventoryPurchaseItemDetail(vendorID, productID, loadAll);
			new DataSet();
			DataTable dataTable = dataGridPurchase.DataSource as DataTable;
			dataTable.Rows.Clear();
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
					if (canAccessCost)
					{
						dataRow2["Price"] = row["UnitPrice"];
						dataRow2["Amount"] = (double.Parse(row["UnitPrice"].ToString()) * double.Parse(row["Quantity"].ToString())).ToString();
					}
					else
					{
						UltraGridColumn ultraGridColumn = dataGridPurchase.DisplayLayout.Bands[0].Columns["Amount"];
						bool hidden = dataGridPurchase.DisplayLayout.Bands[0].Columns["Price"].Hidden = true;
						ultraGridColumn.Hidden = hidden;
					}
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

		private void comboBoxCustomers_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxVendors_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (SelectedItem != "")
			{
				DisplayPurchaseDetails(dataGridItems.ActiveRow.Cells["Code"].Value.ToString());
			}
		}

		private void ComboSearchDialogNew_Activated(object sender, EventArgs e)
		{
			DisplayPurchaseDetails(selectedItem);
			DisplaySalesDetails(selectedItem);
			DisplayProductsPartsDetails(selectedItem);
		}

		private void ComboSearchDialogNew_Load(object sender, EventArgs e)
		{
			DisplayPurchaseDetails(selectedItem);
			DisplaySalesDetails(selectedItem);
			DisplayProductsPartsDetails(selectedItem);
			DisplayProductUnderLocation(selectedItem);
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
		}

		private void buttonLoadAll_Click(object sender, EventArgs e)
		{
			loadAll = true;
			DisplayPurchaseDetails("", dataGridItems.ActiveRow.Cells["Code"].Value.ToString());
			DisplaySalesDetails(dataGridItems.ActiveRow.Cells["Code"].Value.ToString());
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
			panelButtons = new System.Windows.Forms.Panel();
			buttonLoadAll = new Micromind.UISupport.XPButton();
			textBoxProduct = new System.Windows.Forms.TextBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			dataGridSales = new Micromind.DataControls.DataEntryGrid();
			label27 = new System.Windows.Forms.Label();
			textBoxSpecification = new System.Windows.Forms.TextBox();
			label26 = new System.Windows.Forms.Label();
			textBoxProductName = new System.Windows.Forms.TextBox();
			label25 = new System.Windows.Forms.Label();
			textBoxOwnRefNo = new System.Windows.Forms.TextBox();
			label24 = new System.Windows.Forms.Label();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			labelLastCost = new System.Windows.Forms.Label();
			textBoxLastCost = new Micromind.UISupport.NumberTextBox();
			label28 = new System.Windows.Forms.Label();
			textBoxOrigin = new System.Windows.Forms.TextBox();
			label37 = new System.Windows.Forms.Label();
			label36 = new System.Windows.Forms.Label();
			label35 = new System.Windows.Forms.Label();
			dataGridMultiLocation = new Micromind.DataControls.DataEntryGrid();
			dataGridAppliedModels = new Micromind.DataControls.DataEntryGrid();
			dataGridSubstituteParts = new Micromind.DataControls.DataEntryGrid();
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			label22 = new System.Windows.Forms.Label();
			textBoxEngineNumber = new System.Windows.Forms.TextBox();
			label21 = new System.Windows.Forms.Label();
			textBoxModelNumber = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			textBoxChaseNo = new System.Windows.Forms.TextBox();
			label19 = new System.Windows.Forms.Label();
			textBoxPartsFamily = new System.Windows.Forms.TextBox();
			label18 = new System.Windows.Forms.Label();
			textBoxOEMCode = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			textBoxPartsType = new System.Windows.Forms.TextBox();
			labelUnitPrice = new System.Windows.Forms.Label();
			textBoxUnitPrice = new Micromind.UISupport.NumberTextBox();
			labelMinUnitPrice = new System.Windows.Forms.Label();
			textBoxMinUnitPrice = new Micromind.UISupport.NumberTextBox();
			label14 = new System.Windows.Forms.Label();
			textBoxOnHand = new Micromind.UISupport.NumberTextBox();
			label13 = new System.Windows.Forms.Label();
			textBoxPackQty = new Micromind.UISupport.NumberTextBox();
			label12 = new System.Windows.Forms.Label();
			textBoxUnit = new System.Windows.Forms.TextBox();
			label11 = new System.Windows.Forms.Label();
			textBoxStockType = new System.Windows.Forms.TextBox();
			label10 = new System.Windows.Forms.Label();
			textBoxReorderLevel = new Micromind.UISupport.NumberTextBox();
			label9 = new System.Windows.Forms.Label();
			textBoxReorderQty = new Micromind.UISupport.NumberTextBox();
			label8 = new System.Windows.Forms.Label();
			textBoxPacketDetails = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBoxSupRefNo = new System.Windows.Forms.TextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxVehicleMake = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			textBoxVehicleType = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			textBoxModel1 = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxMakeType = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			textBoxLocation = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxProductBrand = new System.Windows.Forms.TextBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			dataGridPurchase = new Micromind.DataControls.DataEntryGrid();
			buttonOK = new Micromind.UISupport.XPButton();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			label23 = new System.Windows.Forms.Label();
			textBoxSearch = new System.Windows.Forms.TextBox();
			dataGridItems = new Micromind.DataControls.MMSListGrid();
			xpButton1 = new Micromind.UISupport.XPButton();
			label15 = new System.Windows.Forms.Label();
			textBoxAverageCost = new Micromind.UISupport.NumberTextBox();
			panelButtons.SuspendLayout();
			groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridSales).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridAppliedModels).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridSubstituteParts).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).BeginInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			panelButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panelButtons.Controls.Add(groupBox1);
			panelButtons.Controls.Add(buttonLoadAll);
			panelButtons.Controls.Add(textBoxProduct);
			panelButtons.Controls.Add(groupBox2);
			panelButtons.Controls.Add(label27);
			panelButtons.Controls.Add(textBoxSpecification);
			panelButtons.Controls.Add(label26);
			panelButtons.Controls.Add(textBoxProductName);
			panelButtons.Controls.Add(label25);
			panelButtons.Controls.Add(textBoxOwnRefNo);
			panelButtons.Controls.Add(label24);
			panelButtons.Controls.Add(ultraGroupBox1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 170);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(927, 546);
			panelButtons.TabIndex = 3;
			buttonLoadAll.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonLoadAll.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonLoadAll.BackColor = System.Drawing.Color.DarkGray;
			buttonLoadAll.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonLoadAll.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonLoadAll.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonLoadAll.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonLoadAll.Location = new System.Drawing.Point(20, 519);
			buttonLoadAll.Name = "buttonLoadAll";
			buttonLoadAll.Size = new System.Drawing.Size(96, 24);
			buttonLoadAll.TabIndex = 158;
			buttonLoadAll.Text = "Load All";
			buttonLoadAll.UseVisualStyleBackColor = false;
			buttonLoadAll.Click += new System.EventHandler(buttonLoadAll_Click);
			textBoxProduct.Location = new System.Drawing.Point(115, 22);
			textBoxProduct.MaxLength = 20;
			textBoxProduct.Name = "textBoxProduct";
			textBoxProduct.ReadOnly = true;
			textBoxProduct.Size = new System.Drawing.Size(163, 20);
			textBoxProduct.TabIndex = 157;
			groupBox2.Controls.Add(dataGridSales);
			groupBox2.Location = new System.Drawing.Point(19, 253);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(439, 260);
			groupBox2.TabIndex = 147;
			groupBox2.TabStop = false;
			groupBox2.Text = "Sale Details";
			dataGridSales.AllowAddNew = false;
			dataGridSales.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSales.DisplayLayout.Appearance = appearance;
			dataGridSales.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSales.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridSales.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSales.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridSales.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSales.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSales.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSales.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridSales.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSales.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSales.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSales.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridSales.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSales.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSales.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridSales.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridSales.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSales.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridSales.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridSales.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSales.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridSales.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSales.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSales.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridSales.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSales.ExitEditModeOnLeave = false;
			dataGridSales.IncludeLotItems = false;
			dataGridSales.LoadLayoutFailed = false;
			dataGridSales.Location = new System.Drawing.Point(4, 14);
			dataGridSales.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridSales.Name = "dataGridSales";
			dataGridSales.ShowClearMenu = true;
			dataGridSales.ShowDeleteMenu = true;
			dataGridSales.ShowInsertMenu = true;
			dataGridSales.ShowMoveRowsMenu = true;
			dataGridSales.Size = new System.Drawing.Size(429, 240);
			dataGridSales.TabIndex = 123;
			dataGridSales.Text = "dataEntryGrid1";
			label27.AutoSize = true;
			label27.Location = new System.Drawing.Point(30, 77);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(71, 13);
			label27.TabIndex = 155;
			label27.Text = "Specification:";
			textBoxSpecification.Location = new System.Drawing.Point(114, 74);
			textBoxSpecification.MaxLength = 20;
			textBoxSpecification.Multiline = true;
			textBoxSpecification.Name = "textBoxSpecification";
			textBoxSpecification.ReadOnly = true;
			textBoxSpecification.Size = new System.Drawing.Size(387, 28);
			textBoxSpecification.TabIndex = 156;
			label26.AutoSize = true;
			label26.Location = new System.Drawing.Point(30, 51);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(60, 13);
			label26.TabIndex = 153;
			label26.Text = "Part Name:";
			textBoxProductName.Location = new System.Drawing.Point(114, 48);
			textBoxProductName.MaxLength = 20;
			textBoxProductName.Name = "textBoxProductName";
			textBoxProductName.ReadOnly = true;
			textBoxProductName.Size = new System.Drawing.Size(387, 20);
			textBoxProductName.TabIndex = 154;
			label25.AutoSize = true;
			label25.Location = new System.Drawing.Point(284, 25);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(69, 13);
			label25.TabIndex = 151;
			label25.Text = "Own Ref No:";
			textBoxOwnRefNo.Location = new System.Drawing.Point(368, 22);
			textBoxOwnRefNo.MaxLength = 20;
			textBoxOwnRefNo.Name = "textBoxOwnRefNo";
			textBoxOwnRefNo.ReadOnly = true;
			textBoxOwnRefNo.Size = new System.Drawing.Size(133, 20);
			textBoxOwnRefNo.TabIndex = 152;
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(30, 22);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(46, 13);
			label24.TabIndex = 149;
			label24.Text = "Part No:";
			ultraGroupBox1.Controls.Add(label15);
			ultraGroupBox1.Controls.Add(textBoxAverageCost);
			ultraGroupBox1.Controls.Add(labelLastCost);
			ultraGroupBox1.Controls.Add(textBoxLastCost);
			ultraGroupBox1.Controls.Add(label28);
			ultraGroupBox1.Controls.Add(textBoxOrigin);
			ultraGroupBox1.Controls.Add(label37);
			ultraGroupBox1.Controls.Add(label36);
			ultraGroupBox1.Controls.Add(label35);
			ultraGroupBox1.Controls.Add(dataGridMultiLocation);
			ultraGroupBox1.Controls.Add(dataGridAppliedModels);
			ultraGroupBox1.Controls.Add(dataGridSubstituteParts);
			ultraGroupBox1.Controls.Add(ultraGroupBox2);
			ultraGroupBox1.Controls.Add(labelUnitPrice);
			ultraGroupBox1.Controls.Add(textBoxUnitPrice);
			ultraGroupBox1.Controls.Add(labelMinUnitPrice);
			ultraGroupBox1.Controls.Add(textBoxMinUnitPrice);
			ultraGroupBox1.Controls.Add(label14);
			ultraGroupBox1.Controls.Add(textBoxOnHand);
			ultraGroupBox1.Controls.Add(label13);
			ultraGroupBox1.Controls.Add(textBoxPackQty);
			ultraGroupBox1.Controls.Add(label12);
			ultraGroupBox1.Controls.Add(textBoxUnit);
			ultraGroupBox1.Controls.Add(label11);
			ultraGroupBox1.Controls.Add(textBoxStockType);
			ultraGroupBox1.Controls.Add(label10);
			ultraGroupBox1.Controls.Add(textBoxReorderLevel);
			ultraGroupBox1.Controls.Add(label9);
			ultraGroupBox1.Controls.Add(textBoxReorderQty);
			ultraGroupBox1.Controls.Add(label8);
			ultraGroupBox1.Controls.Add(textBoxPacketDetails);
			ultraGroupBox1.Controls.Add(label7);
			ultraGroupBox1.Controls.Add(textBoxSupRefNo);
			ultraGroupBox1.Controls.Add(label6);
			ultraGroupBox1.Controls.Add(textBoxVehicleMake);
			ultraGroupBox1.Controls.Add(label5);
			ultraGroupBox1.Controls.Add(textBoxVehicleType);
			ultraGroupBox1.Controls.Add(label4);
			ultraGroupBox1.Controls.Add(textBoxModel1);
			ultraGroupBox1.Controls.Add(label3);
			ultraGroupBox1.Controls.Add(textBoxMakeType);
			ultraGroupBox1.Controls.Add(label2);
			ultraGroupBox1.Controls.Add(textBoxLocation);
			ultraGroupBox1.Controls.Add(label1);
			ultraGroupBox1.Controls.Add(textBoxProductBrand);
			ultraGroupBox1.Location = new System.Drawing.Point(11, 109);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(900, 241);
			ultraGroupBox1.TabIndex = 148;
			labelLastCost.AutoSize = true;
			labelLastCost.Location = new System.Drawing.Point(243, 125);
			labelLastCost.Name = "labelLastCost";
			labelLastCost.Size = new System.Drawing.Size(102, 13);
			labelLastCost.TabIndex = 224;
			labelLastCost.Text = "Last Purchase Cost:";
			textBoxLastCost.AllowDecimal = true;
			textBoxLastCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLastCost.CustomReportFieldName = "";
			textBoxLastCost.CustomReportKey = "";
			textBoxLastCost.CustomReportValueType = 1;
			textBoxLastCost.IsComboTextBox = false;
			textBoxLastCost.IsModified = false;
			textBoxLastCost.Location = new System.Drawing.Point(347, 120);
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
			textBoxLastCost.Size = new System.Drawing.Size(81, 20);
			textBoxLastCost.TabIndex = 223;
			textBoxLastCost.Text = "0.00";
			textBoxLastCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label28.AutoSize = true;
			label28.Location = new System.Drawing.Point(243, 36);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(37, 13);
			label28.TabIndex = 209;
			label28.Text = "Origin:";
			textBoxOrigin.Location = new System.Drawing.Point(322, 32);
			textBoxOrigin.MaxLength = 20;
			textBoxOrigin.Name = "textBoxOrigin";
			textBoxOrigin.ReadOnly = true;
			textBoxOrigin.Size = new System.Drawing.Size(106, 20);
			textBoxOrigin.TabIndex = 210;
			label37.AutoSize = true;
			label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label37.Location = new System.Drawing.Point(687, 143);
			label37.Name = "label37";
			label37.Size = new System.Drawing.Size(105, 13);
			label37.TabIndex = 208;
			label37.Text = "MULTI LOCATION";
			label37.Visible = false;
			label36.AutoSize = true;
			label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label36.Location = new System.Drawing.Point(437, 143);
			label36.Name = "label36";
			label36.Size = new System.Drawing.Size(114, 13);
			label36.TabIndex = 207;
			label36.Text = "APPLIED MODELS";
			label36.Visible = false;
			label35.AutoSize = true;
			label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 7f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label35.Location = new System.Drawing.Point(100, 143);
			label35.Name = "label35";
			label35.Size = new System.Drawing.Size(119, 13);
			label35.TabIndex = 206;
			label35.Text = "SUBSTITUTE PARTS";
			label35.Visible = false;
			dataGridMultiLocation.AllowAddNew = false;
			dataGridMultiLocation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridMultiLocation.DisplayLayout.Appearance = appearance13;
			dataGridMultiLocation.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridMultiLocation.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			dataGridMultiLocation.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridMultiLocation.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			dataGridMultiLocation.DisplayLayout.MaxColScrollRegions = 1;
			dataGridMultiLocation.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridMultiLocation.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			dataGridMultiLocation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridMultiLocation.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridMultiLocation.DisplayLayout.Override.CellAppearance = appearance20;
			dataGridMultiLocation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridMultiLocation.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			dataGridMultiLocation.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			dataGridMultiLocation.DisplayLayout.Override.HeaderAppearance = appearance22;
			dataGridMultiLocation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridMultiLocation.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			dataGridMultiLocation.DisplayLayout.Override.RowAppearance = appearance23;
			dataGridMultiLocation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridMultiLocation.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			dataGridMultiLocation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridMultiLocation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridMultiLocation.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridMultiLocation.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridMultiLocation.ExitEditModeOnLeave = false;
			dataGridMultiLocation.IncludeLotItems = false;
			dataGridMultiLocation.LoadLayoutFailed = false;
			dataGridMultiLocation.Location = new System.Drawing.Point(651, 159);
			dataGridMultiLocation.Name = "dataGridMultiLocation";
			dataGridMultiLocation.ShowClearMenu = true;
			dataGridMultiLocation.ShowDeleteMenu = true;
			dataGridMultiLocation.ShowInsertMenu = true;
			dataGridMultiLocation.ShowMoveRowsMenu = true;
			dataGridMultiLocation.Size = new System.Drawing.Size(236, 76);
			dataGridMultiLocation.TabIndex = 205;
			dataGridMultiLocation.Text = "dataEntryGrid3";
			dataGridMultiLocation.Visible = false;
			dataGridAppliedModels.AllowAddNew = false;
			dataGridAppliedModels.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance25.BackColor = System.Drawing.SystemColors.Window;
			appearance25.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridAppliedModels.DisplayLayout.Appearance = appearance25;
			dataGridAppliedModels.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridAppliedModels.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance26.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance26.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance26.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance26.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAppliedModels.DisplayLayout.GroupByBox.Appearance = appearance26;
			appearance27.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAppliedModels.DisplayLayout.GroupByBox.BandLabelAppearance = appearance27;
			dataGridAppliedModels.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance28.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance28.BackColor2 = System.Drawing.SystemColors.Control;
			appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance28.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridAppliedModels.DisplayLayout.GroupByBox.PromptAppearance = appearance28;
			dataGridAppliedModels.DisplayLayout.MaxColScrollRegions = 1;
			dataGridAppliedModels.DisplayLayout.MaxRowScrollRegions = 1;
			appearance29.BackColor = System.Drawing.SystemColors.Window;
			appearance29.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridAppliedModels.DisplayLayout.Override.ActiveCellAppearance = appearance29;
			appearance30.BackColor = System.Drawing.SystemColors.Highlight;
			appearance30.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridAppliedModels.DisplayLayout.Override.ActiveRowAppearance = appearance30;
			dataGridAppliedModels.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridAppliedModels.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridAppliedModels.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance31.BackColor = System.Drawing.SystemColors.Window;
			dataGridAppliedModels.DisplayLayout.Override.CardAreaAppearance = appearance31;
			appearance32.BorderColor = System.Drawing.Color.Silver;
			appearance32.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridAppliedModels.DisplayLayout.Override.CellAppearance = appearance32;
			dataGridAppliedModels.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridAppliedModels.DisplayLayout.Override.CellPadding = 0;
			appearance33.BackColor = System.Drawing.SystemColors.Control;
			appearance33.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance33.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance33.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance33.BorderColor = System.Drawing.SystemColors.Window;
			dataGridAppliedModels.DisplayLayout.Override.GroupByRowAppearance = appearance33;
			appearance34.TextHAlignAsString = "Left";
			dataGridAppliedModels.DisplayLayout.Override.HeaderAppearance = appearance34;
			dataGridAppliedModels.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridAppliedModels.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance35.BackColor = System.Drawing.SystemColors.Window;
			appearance35.BorderColor = System.Drawing.Color.Silver;
			dataGridAppliedModels.DisplayLayout.Override.RowAppearance = appearance35;
			dataGridAppliedModels.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance36.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridAppliedModels.DisplayLayout.Override.TemplateAddRowAppearance = appearance36;
			dataGridAppliedModels.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridAppliedModels.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridAppliedModels.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridAppliedModels.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridAppliedModels.ExitEditModeOnLeave = false;
			dataGridAppliedModels.IncludeLotItems = false;
			dataGridAppliedModels.LoadLayoutFailed = false;
			dataGridAppliedModels.Location = new System.Drawing.Point(364, 159);
			dataGridAppliedModels.Name = "dataGridAppliedModels";
			dataGridAppliedModels.ShowClearMenu = true;
			dataGridAppliedModels.ShowDeleteMenu = true;
			dataGridAppliedModels.ShowInsertMenu = true;
			dataGridAppliedModels.ShowMoveRowsMenu = true;
			dataGridAppliedModels.Size = new System.Drawing.Size(281, 76);
			dataGridAppliedModels.TabIndex = 204;
			dataGridAppliedModels.Text = "dataEntryGrid2";
			dataGridAppliedModels.Visible = false;
			dataGridSubstituteParts.AllowAddNew = false;
			dataGridSubstituteParts.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance37.BackColor = System.Drawing.SystemColors.Window;
			appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridSubstituteParts.DisplayLayout.Appearance = appearance37;
			dataGridSubstituteParts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridSubstituteParts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance38.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteParts.DisplayLayout.GroupByBox.Appearance = appearance38;
			appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSubstituteParts.DisplayLayout.GroupByBox.BandLabelAppearance = appearance39;
			dataGridSubstituteParts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance40.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance40.BackColor2 = System.Drawing.SystemColors.Control;
			appearance40.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridSubstituteParts.DisplayLayout.GroupByBox.PromptAppearance = appearance40;
			dataGridSubstituteParts.DisplayLayout.MaxColScrollRegions = 1;
			dataGridSubstituteParts.DisplayLayout.MaxRowScrollRegions = 1;
			appearance41.BackColor = System.Drawing.SystemColors.Window;
			appearance41.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridSubstituteParts.DisplayLayout.Override.ActiveCellAppearance = appearance41;
			appearance42.BackColor = System.Drawing.SystemColors.Highlight;
			appearance42.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridSubstituteParts.DisplayLayout.Override.ActiveRowAppearance = appearance42;
			dataGridSubstituteParts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridSubstituteParts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridSubstituteParts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance43.BackColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteParts.DisplayLayout.Override.CardAreaAppearance = appearance43;
			appearance44.BorderColor = System.Drawing.Color.Silver;
			appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridSubstituteParts.DisplayLayout.Override.CellAppearance = appearance44;
			dataGridSubstituteParts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridSubstituteParts.DisplayLayout.Override.CellPadding = 0;
			appearance45.BackColor = System.Drawing.SystemColors.Control;
			appearance45.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance45.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance45.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance45.BorderColor = System.Drawing.SystemColors.Window;
			dataGridSubstituteParts.DisplayLayout.Override.GroupByRowAppearance = appearance45;
			appearance46.TextHAlignAsString = "Left";
			dataGridSubstituteParts.DisplayLayout.Override.HeaderAppearance = appearance46;
			dataGridSubstituteParts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridSubstituteParts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance47.BackColor = System.Drawing.SystemColors.Window;
			appearance47.BorderColor = System.Drawing.Color.Silver;
			dataGridSubstituteParts.DisplayLayout.Override.RowAppearance = appearance47;
			dataGridSubstituteParts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance48.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridSubstituteParts.DisplayLayout.Override.TemplateAddRowAppearance = appearance48;
			dataGridSubstituteParts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridSubstituteParts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridSubstituteParts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridSubstituteParts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridSubstituteParts.ExitEditModeOnLeave = false;
			dataGridSubstituteParts.IncludeLotItems = false;
			dataGridSubstituteParts.LoadLayoutFailed = false;
			dataGridSubstituteParts.Location = new System.Drawing.Point(8, 159);
			dataGridSubstituteParts.MinimumSize = new System.Drawing.Size(350, 50);
			dataGridSubstituteParts.Name = "dataGridSubstituteParts";
			dataGridSubstituteParts.ShowClearMenu = true;
			dataGridSubstituteParts.ShowDeleteMenu = true;
			dataGridSubstituteParts.ShowInsertMenu = true;
			dataGridSubstituteParts.ShowMoveRowsMenu = true;
			dataGridSubstituteParts.Size = new System.Drawing.Size(350, 76);
			dataGridSubstituteParts.TabIndex = 203;
			dataGridSubstituteParts.Text = "dataEntryGrid1";
			dataGridSubstituteParts.Visible = false;
			dataGridSubstituteParts.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridSubstituteItems_DoubleClickRow);
			ultraGroupBox2.Controls.Add(label22);
			ultraGroupBox2.Controls.Add(textBoxEngineNumber);
			ultraGroupBox2.Controls.Add(label21);
			ultraGroupBox2.Controls.Add(textBoxModelNumber);
			ultraGroupBox2.Controls.Add(label20);
			ultraGroupBox2.Controls.Add(textBoxChaseNo);
			ultraGroupBox2.Controls.Add(label19);
			ultraGroupBox2.Controls.Add(textBoxPartsFamily);
			ultraGroupBox2.Controls.Add(label18);
			ultraGroupBox2.Controls.Add(textBoxOEMCode);
			ultraGroupBox2.Controls.Add(label17);
			ultraGroupBox2.Controls.Add(textBoxPartsType);
			ultraGroupBox2.Location = new System.Drawing.Point(706, 5);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(187, 135);
			ultraGroupBox2.TabIndex = 202;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(6, 114);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(60, 13);
			label22.TabIndex = 27;
			label22.Text = "Engine No:";
			textBoxEngineNumber.Location = new System.Drawing.Point(90, 111);
			textBoxEngineNumber.MaxLength = 20;
			textBoxEngineNumber.Name = "textBoxEngineNumber";
			textBoxEngineNumber.ReadOnly = true;
			textBoxEngineNumber.Size = new System.Drawing.Size(91, 20);
			textBoxEngineNumber.TabIndex = 28;
			label21.AutoSize = true;
			label21.Location = new System.Drawing.Point(6, 93);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(39, 13);
			label21.TabIndex = 25;
			label21.Text = "Model:";
			textBoxModelNumber.Location = new System.Drawing.Point(90, 90);
			textBoxModelNumber.MaxLength = 20;
			textBoxModelNumber.Name = "textBoxModelNumber";
			textBoxModelNumber.ReadOnly = true;
			textBoxModelNumber.Size = new System.Drawing.Size(91, 20);
			textBoxModelNumber.TabIndex = 26;
			label20.AutoSize = true;
			label20.Location = new System.Drawing.Point(6, 72);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(57, 13);
			label20.TabIndex = 23;
			label20.Text = "Chase No:";
			textBoxChaseNo.Location = new System.Drawing.Point(90, 69);
			textBoxChaseNo.MaxLength = 20;
			textBoxChaseNo.Name = "textBoxChaseNo";
			textBoxChaseNo.ReadOnly = true;
			textBoxChaseNo.Size = new System.Drawing.Size(91, 20);
			textBoxChaseNo.TabIndex = 24;
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(6, 51);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(66, 13);
			label19.TabIndex = 21;
			label19.Text = "Parts Family:";
			textBoxPartsFamily.Location = new System.Drawing.Point(90, 48);
			textBoxPartsFamily.MaxLength = 20;
			textBoxPartsFamily.Name = "textBoxPartsFamily";
			textBoxPartsFamily.ReadOnly = true;
			textBoxPartsFamily.Size = new System.Drawing.Size(91, 20);
			textBoxPartsFamily.TabIndex = 22;
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(6, 30);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(62, 13);
			label18.TabIndex = 19;
			label18.Text = "OEM Code:";
			textBoxOEMCode.Location = new System.Drawing.Point(90, 27);
			textBoxOEMCode.MaxLength = 20;
			textBoxOEMCode.Name = "textBoxOEMCode";
			textBoxOEMCode.ReadOnly = true;
			textBoxOEMCode.Size = new System.Drawing.Size(91, 20);
			textBoxOEMCode.TabIndex = 20;
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(6, 9);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(61, 13);
			label17.TabIndex = 17;
			label17.Text = "Parts Type:";
			textBoxPartsType.Location = new System.Drawing.Point(90, 6);
			textBoxPartsType.MaxLength = 20;
			textBoxPartsType.Name = "textBoxPartsType";
			textBoxPartsType.ReadOnly = true;
			textBoxPartsType.Size = new System.Drawing.Size(91, 20);
			textBoxPartsType.TabIndex = 18;
			labelUnitPrice.AutoSize = true;
			labelUnitPrice.Location = new System.Drawing.Point(243, 80);
			labelUnitPrice.Name = "labelUnitPrice";
			labelUnitPrice.Size = new System.Drawing.Size(56, 13);
			labelUnitPrice.TabIndex = 201;
			labelUnitPrice.Text = "Unit Price:";
			textBoxUnitPrice.AllowDecimal = true;
			textBoxUnitPrice.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUnitPrice.CustomReportFieldName = "";
			textBoxUnitPrice.CustomReportKey = "";
			textBoxUnitPrice.CustomReportValueType = 1;
			textBoxUnitPrice.IsComboTextBox = false;
			textBoxUnitPrice.IsModified = false;
			textBoxUnitPrice.Location = new System.Drawing.Point(322, 76);
			textBoxUnitPrice.MaxLength = 15;
			textBoxUnitPrice.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxUnitPrice.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxUnitPrice.Name = "textBoxUnitPrice";
			textBoxUnitPrice.NullText = "0";
			textBoxUnitPrice.ReadOnly = true;
			textBoxUnitPrice.Size = new System.Drawing.Size(106, 20);
			textBoxUnitPrice.TabIndex = 200;
			textBoxUnitPrice.Text = "0.00";
			textBoxUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelMinUnitPrice.AutoSize = true;
			labelMinUnitPrice.Location = new System.Drawing.Point(243, 58);
			labelMinUnitPrice.Name = "labelMinUnitPrice";
			labelMinUnitPrice.Size = new System.Drawing.Size(76, 13);
			labelMinUnitPrice.TabIndex = 199;
			labelMinUnitPrice.Text = "Min Unit Price:";
			textBoxMinUnitPrice.AllowDecimal = true;
			textBoxMinUnitPrice.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxMinUnitPrice.CustomReportFieldName = "";
			textBoxMinUnitPrice.CustomReportKey = "";
			textBoxMinUnitPrice.CustomReportValueType = 1;
			textBoxMinUnitPrice.IsComboTextBox = false;
			textBoxMinUnitPrice.IsModified = false;
			textBoxMinUnitPrice.Location = new System.Drawing.Point(322, 54);
			textBoxMinUnitPrice.MaxLength = 15;
			textBoxMinUnitPrice.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxMinUnitPrice.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxMinUnitPrice.Name = "textBoxMinUnitPrice";
			textBoxMinUnitPrice.NullText = "0";
			textBoxMinUnitPrice.ReadOnly = true;
			textBoxMinUnitPrice.Size = new System.Drawing.Size(106, 20);
			textBoxMinUnitPrice.TabIndex = 198;
			textBoxMinUnitPrice.Text = "0.00";
			textBoxMinUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label14.AutoSize = true;
			label14.Location = new System.Drawing.Point(447, 123);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(72, 13);
			label14.TabIndex = 197;
			label14.Text = "Qty On Hand:";
			textBoxOnHand.AllowDecimal = true;
			textBoxOnHand.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxOnHand.CustomReportFieldName = "";
			textBoxOnHand.CustomReportKey = "";
			textBoxOnHand.CustomReportValueType = 1;
			textBoxOnHand.IsComboTextBox = false;
			textBoxOnHand.IsModified = false;
			textBoxOnHand.Location = new System.Drawing.Point(531, 119);
			textBoxOnHand.MaxLength = 15;
			textBoxOnHand.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxOnHand.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxOnHand.Name = "textBoxOnHand";
			textBoxOnHand.NullText = "0";
			textBoxOnHand.ReadOnly = true;
			textBoxOnHand.Size = new System.Drawing.Size(94, 20);
			textBoxOnHand.TabIndex = 196;
			textBoxOnHand.Text = "0.00";
			textBoxOnHand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(602, 101);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(54, 13);
			label13.TabIndex = 195;
			label13.Text = "Pack Qty:";
			textBoxPackQty.AllowDecimal = true;
			textBoxPackQty.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPackQty.CustomReportFieldName = "";
			textBoxPackQty.CustomReportKey = "";
			textBoxPackQty.CustomReportValueType = 1;
			textBoxPackQty.IsComboTextBox = false;
			textBoxPackQty.IsModified = false;
			textBoxPackQty.Location = new System.Drawing.Point(657, 97);
			textBoxPackQty.MaxLength = 15;
			textBoxPackQty.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxPackQty.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxPackQty.Name = "textBoxPackQty";
			textBoxPackQty.NullText = "0";
			textBoxPackQty.ReadOnly = true;
			textBoxPackQty.Size = new System.Drawing.Size(43, 20);
			textBoxPackQty.TabIndex = 194;
			textBoxPackQty.Text = "0.00";
			textBoxPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(447, 101);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(29, 13);
			label12.TabIndex = 192;
			label12.Text = "Unit:";
			textBoxUnit.Location = new System.Drawing.Point(531, 97);
			textBoxUnit.MaxLength = 20;
			textBoxUnit.Name = "textBoxUnit";
			textBoxUnit.ReadOnly = true;
			textBoxUnit.Size = new System.Drawing.Size(69, 20);
			textBoxUnit.TabIndex = 193;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(447, 80);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(65, 13);
			label11.TabIndex = 190;
			label11.Text = "Stock Type:";
			textBoxStockType.Location = new System.Drawing.Point(531, 76);
			textBoxStockType.MaxLength = 20;
			textBoxStockType.Name = "textBoxStockType";
			textBoxStockType.ReadOnly = true;
			textBoxStockType.Size = new System.Drawing.Size(169, 20);
			textBoxStockType.TabIndex = 191;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(447, 57);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(82, 13);
			label10.TabIndex = 189;
			label10.Text = "Re Order Level:";
			textBoxReorderLevel.AllowDecimal = true;
			textBoxReorderLevel.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxReorderLevel.CustomReportFieldName = "";
			textBoxReorderLevel.CustomReportKey = "";
			textBoxReorderLevel.CustomReportValueType = 1;
			textBoxReorderLevel.IsComboTextBox = false;
			textBoxReorderLevel.IsModified = false;
			textBoxReorderLevel.Location = new System.Drawing.Point(531, 53);
			textBoxReorderLevel.MaxLength = 15;
			textBoxReorderLevel.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxReorderLevel.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxReorderLevel.Name = "textBoxReorderLevel";
			textBoxReorderLevel.NullText = "0";
			textBoxReorderLevel.ReadOnly = true;
			textBoxReorderLevel.Size = new System.Drawing.Size(36, 20);
			textBoxReorderLevel.TabIndex = 188;
			textBoxReorderLevel.Text = "0.00";
			textBoxReorderLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(586, 56);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(72, 13);
			label9.TabIndex = 187;
			label9.Text = "Re Order Qty:";
			textBoxReorderQty.AllowDecimal = true;
			textBoxReorderQty.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxReorderQty.CustomReportFieldName = "";
			textBoxReorderQty.CustomReportKey = "";
			textBoxReorderQty.CustomReportValueType = 1;
			textBoxReorderQty.IsComboTextBox = false;
			textBoxReorderQty.IsModified = false;
			textBoxReorderQty.Location = new System.Drawing.Point(664, 53);
			textBoxReorderQty.MaxLength = 15;
			textBoxReorderQty.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxReorderQty.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxReorderQty.Name = "textBoxReorderQty";
			textBoxReorderQty.NullText = "0";
			textBoxReorderQty.ReadOnly = true;
			textBoxReorderQty.Size = new System.Drawing.Size(36, 20);
			textBoxReorderQty.TabIndex = 186;
			textBoxReorderQty.Text = "0.00";
			textBoxReorderQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(447, 35);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(79, 13);
			label8.TabIndex = 184;
			label8.Text = "Packet Details:";
			textBoxPacketDetails.Location = new System.Drawing.Point(531, 31);
			textBoxPacketDetails.MaxLength = 20;
			textBoxPacketDetails.Name = "textBoxPacketDetails";
			textBoxPacketDetails.ReadOnly = true;
			textBoxPacketDetails.Size = new System.Drawing.Size(169, 20);
			textBoxPacketDetails.TabIndex = 185;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(243, 14);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(66, 13);
			label7.TabIndex = 182;
			label7.Text = "Sup.Ref.No:";
			textBoxSupRefNo.Location = new System.Drawing.Point(322, 10);
			textBoxSupRefNo.MaxLength = 20;
			textBoxSupRefNo.Name = "textBoxSupRefNo";
			textBoxSupRefNo.ReadOnly = true;
			textBoxSupRefNo.Size = new System.Drawing.Size(106, 20);
			textBoxSupRefNo.TabIndex = 183;
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(20, 33);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(75, 13);
			label6.TabIndex = 180;
			label6.Text = "Vehicle Make:";
			textBoxVehicleMake.Location = new System.Drawing.Point(104, 30);
			textBoxVehicleMake.MaxLength = 20;
			textBoxVehicleMake.Name = "textBoxVehicleMake";
			textBoxVehicleMake.ReadOnly = true;
			textBoxVehicleMake.Size = new System.Drawing.Size(133, 20);
			textBoxVehicleMake.TabIndex = 181;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(20, 55);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(72, 13);
			label5.TabIndex = 178;
			label5.Text = "Vehicle Type:";
			textBoxVehicleType.Location = new System.Drawing.Point(104, 52);
			textBoxVehicleType.MaxLength = 20;
			textBoxVehicleType.Name = "textBoxVehicleType";
			textBoxVehicleType.ReadOnly = true;
			textBoxVehicleType.Size = new System.Drawing.Size(133, 20);
			textBoxVehicleType.TabIndex = 179;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(20, 77);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(39, 13);
			label4.TabIndex = 176;
			label4.Text = "Model:";
			textBoxModel1.Location = new System.Drawing.Point(104, 74);
			textBoxModel1.MaxLength = 20;
			textBoxModel1.Name = "textBoxModel1";
			textBoxModel1.ReadOnly = true;
			textBoxModel1.Size = new System.Drawing.Size(133, 20);
			textBoxModel1.TabIndex = 177;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(20, 99);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(64, 13);
			label3.TabIndex = 174;
			label3.Text = "Make Type:";
			textBoxMakeType.Location = new System.Drawing.Point(104, 96);
			textBoxMakeType.MaxLength = 20;
			textBoxMakeType.Name = "textBoxMakeType";
			textBoxMakeType.ReadOnly = true;
			textBoxMakeType.Size = new System.Drawing.Size(133, 20);
			textBoxMakeType.TabIndex = 175;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(20, 121);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(51, 13);
			label2.TabIndex = 172;
			label2.Text = "Location:";
			textBoxLocation.Location = new System.Drawing.Point(104, 118);
			textBoxLocation.MaxLength = 20;
			textBoxLocation.Name = "textBoxLocation";
			textBoxLocation.ReadOnly = true;
			textBoxLocation.Size = new System.Drawing.Size(133, 20);
			textBoxLocation.TabIndex = 173;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(20, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(78, 13);
			label1.TabIndex = 170;
			label1.Text = "Product Brand:";
			textBoxProductBrand.Location = new System.Drawing.Point(104, 8);
			textBoxProductBrand.MaxLength = 20;
			textBoxProductBrand.Name = "textBoxProductBrand";
			textBoxProductBrand.ReadOnly = true;
			textBoxProductBrand.Size = new System.Drawing.Size(133, 20);
			textBoxProductBrand.TabIndex = 171;
			groupBox1.Controls.Add(dataGridPurchase);
			groupBox1.Location = new System.Drawing.Point(465, 252);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(440, 261);
			groupBox1.TabIndex = 146;
			groupBox1.TabStop = false;
			groupBox1.Text = "Purchase Details";
			dataGridPurchase.AllowAddNew = false;
			dataGridPurchase.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance49.BackColor = System.Drawing.SystemColors.Window;
			appearance49.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridPurchase.DisplayLayout.Appearance = appearance49;
			dataGridPurchase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridPurchase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance50.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance50.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance50.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance50.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.GroupByBox.Appearance = appearance50;
			appearance51.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance51;
			dataGridPurchase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance52.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance52.BackColor2 = System.Drawing.SystemColors.Control;
			appearance52.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance52.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridPurchase.DisplayLayout.GroupByBox.PromptAppearance = appearance52;
			dataGridPurchase.DisplayLayout.MaxColScrollRegions = 1;
			dataGridPurchase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance53.BackColor = System.Drawing.SystemColors.Window;
			appearance53.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridPurchase.DisplayLayout.Override.ActiveCellAppearance = appearance53;
			appearance54.BackColor = System.Drawing.SystemColors.Highlight;
			appearance54.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridPurchase.DisplayLayout.Override.ActiveRowAppearance = appearance54;
			dataGridPurchase.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridPurchase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridPurchase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance55.BackColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.CardAreaAppearance = appearance55;
			appearance56.BorderColor = System.Drawing.Color.Silver;
			appearance56.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridPurchase.DisplayLayout.Override.CellAppearance = appearance56;
			dataGridPurchase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridPurchase.DisplayLayout.Override.CellPadding = 0;
			appearance57.BackColor = System.Drawing.SystemColors.Control;
			appearance57.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance57.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance57.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance57.BorderColor = System.Drawing.SystemColors.Window;
			dataGridPurchase.DisplayLayout.Override.GroupByRowAppearance = appearance57;
			appearance58.TextHAlignAsString = "Left";
			dataGridPurchase.DisplayLayout.Override.HeaderAppearance = appearance58;
			dataGridPurchase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridPurchase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance59.BackColor = System.Drawing.SystemColors.Window;
			appearance59.BorderColor = System.Drawing.Color.Silver;
			dataGridPurchase.DisplayLayout.Override.RowAppearance = appearance59;
			dataGridPurchase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance60.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridPurchase.DisplayLayout.Override.TemplateAddRowAppearance = appearance60;
			dataGridPurchase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridPurchase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridPurchase.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridPurchase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridPurchase.ExitEditModeOnLeave = false;
			dataGridPurchase.IncludeLotItems = false;
			dataGridPurchase.LoadLayoutFailed = false;
			dataGridPurchase.Location = new System.Drawing.Point(4, 14);
			dataGridPurchase.Name = "dataGridPurchase";
			dataGridPurchase.ShowClearMenu = true;
			dataGridPurchase.ShowDeleteMenu = true;
			dataGridPurchase.ShowInsertMenu = true;
			dataGridPurchase.ShowMoveRowsMenu = true;
			dataGridPurchase.Size = new System.Drawing.Size(430, 241);
			dataGridPurchase.TabIndex = 3;
			dataGridPurchase.Text = "dataEntryGrid1";
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(712, 514);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Visible = false;
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
			buttonCancel.Location = new System.Drawing.Point(817, 514);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Visible = false;
			label23.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			label23.AutoSize = true;
			label23.Location = new System.Drawing.Point(6, 15);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(30, 13);
			label23.TabIndex = 9;
			label23.Text = "Find:";
			textBoxSearch.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			textBoxSearch.Location = new System.Drawing.Point(42, 12);
			textBoxSearch.Name = "textBoxSearch";
			textBoxSearch.Size = new System.Drawing.Size(445, 20);
			textBoxSearch.TabIndex = 8;
			textBoxSearch.TextChanged += new System.EventHandler(textBoxSearch_TextChanged);
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			appearance61.BackColor = System.Drawing.SystemColors.Window;
			appearance61.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance61;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance62.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance62.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance62.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance62.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance62;
			appearance63.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance63;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance64.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance64.BackColor2 = System.Drawing.SystemColors.Control;
			appearance64.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance64.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance64;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance65.BackColor = System.Drawing.SystemColors.Window;
			appearance65.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance65;
			appearance66.BackColor = System.Drawing.SystemColors.Highlight;
			appearance66.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance66;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance67.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance67;
			appearance68.BorderColor = System.Drawing.Color.Silver;
			appearance68.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance68;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance69.BackColor = System.Drawing.SystemColors.Control;
			appearance69.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance69.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance69.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance69.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance69;
			appearance70.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance70;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance71.BackColor = System.Drawing.SystemColors.Window;
			appearance71.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance71;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance72.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance72;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.Location = new System.Drawing.Point(1, 42);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowMinusInRed = true;
			dataGridItems.Size = new System.Drawing.Size(635, 122);
			dataGridItems.TabIndex = 7;
			dataGridItems.Text = "mmsListGrid1";
			dataGridItems.AfterRowActivate += new System.EventHandler(dataGridItems_AfterRowActivate);
			dataGridItems.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(dataGridItems_DoubleClickRow);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(668, 69);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 10;
			xpButton1.Text = "&Show";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Visible = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(243, 102);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(74, 13);
			label15.TabIndex = 226;
			label15.Text = "Average Cost:";
			textBoxAverageCost.AllowDecimal = true;
			textBoxAverageCost.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxAverageCost.CustomReportFieldName = "";
			textBoxAverageCost.CustomReportKey = "";
			textBoxAverageCost.CustomReportValueType = 1;
			textBoxAverageCost.IsComboTextBox = false;
			textBoxAverageCost.IsModified = false;
			textBoxAverageCost.Location = new System.Drawing.Point(322, 98);
			textBoxAverageCost.MaxLength = 15;
			textBoxAverageCost.MaxValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				0
			});
			textBoxAverageCost.MinValue = new decimal(new int[4]
			{
				-1,
				-1,
				-1,
				-2147483648
			});
			textBoxAverageCost.Name = "textBoxAverageCost";
			textBoxAverageCost.NullText = "0";
			textBoxAverageCost.ReadOnly = true;
			textBoxAverageCost.Size = new System.Drawing.Size(106, 20);
			textBoxAverageCost.TabIndex = 225;
			textBoxAverageCost.Text = "0.00";
			textBoxAverageCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(927, 716);
			base.Controls.Add(xpButton1);
			base.Controls.Add(label23);
			base.Controls.Add(textBoxSearch);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(465, 215);
			base.Name = "ProductsPartsDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Select Item-Product Parts";
			panelButtons.ResumeLayout(false);
			panelButtons.PerformLayout();
			groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridSales).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ultraGroupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dataGridMultiLocation).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridAppliedModels).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridSubstituteParts).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ultraGroupBox2.PerformLayout();
			groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridPurchase).EndInit();
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
