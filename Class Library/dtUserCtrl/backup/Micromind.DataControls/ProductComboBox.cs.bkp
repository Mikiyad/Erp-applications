using Infragistics.Win;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataCaches;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductComboBox : MultiColumnComboBox
	{
		private string filterSysDocID = "";

		private string filterCustomerID = "";

		private bool showConsignmentItems = true;

		private bool showOnlyConsignmentItems;

		private bool showOnly3PLItems;

		private bool show3PLItems = true;

		private bool showOnlyAssemblyItems;

		private bool showOnlyInventoryItems;

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private bool showOnlyMatrixAddableItems;

		private bool showOnlyLotItems;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private string filteredCategoryID = "";

		private ItemTypes[] allowedTypes = new ItemTypes[0];

		public bool HasCustom
		{
			get
			{
				return hasCustom;
			}
			set
			{
				hasCustom = value;
			}
		}

		public bool HasAllAccount
		{
			get
			{
				return hasAll;
			}
			set
			{
				hasAll = value;
			}
		}

		[DefaultValue(true)]
		public bool ShowConsignmentItems
		{
			get
			{
				return showConsignmentItems;
			}
			set
			{
				showConsignmentItems = value;
			}
		}

		[DefaultValue(false)]
		public bool ShowOnlyMatrixAddableItems
		{
			get
			{
				return showOnlyMatrixAddableItems;
			}
			set
			{
				showOnlyMatrixAddableItems = value;
			}
		}

		[DefaultValue(false)]
		public bool ShowOnly3PLItems
		{
			get
			{
				return showOnly3PLItems;
			}
			set
			{
				showOnly3PLItems = value;
			}
		}

		[DefaultValue(false)]
		public bool Show3PLItems
		{
			get
			{
				return show3PLItems;
			}
			set
			{
				show3PLItems = value;
			}
		}

		[DefaultValue(false)]
		public bool ShowOnlyConsignmentItems
		{
			get
			{
				return showOnlyConsignmentItems;
			}
			set
			{
				showOnlyConsignmentItems = value;
			}
		}

		[DefaultValue(false)]
		public bool ShowOnlyAssemblyItems
		{
			get
			{
				return showOnlyAssemblyItems;
			}
			set
			{
				showOnlyAssemblyItems = value;
			}
		}

		[DefaultValue(false)]
		public bool ShowOnlyInventoryItems
		{
			get
			{
				return showOnlyInventoryItems;
			}
			set
			{
				showOnlyInventoryItems = value;
			}
		}

		public bool ShowOnlyLotItems
		{
			get
			{
				return showOnlyLotItems;
			}
			set
			{
				showOnlyLotItems = value;
			}
		}

		public new bool ShowQuickAdd
		{
			get
			{
				return showQuickAdd;
			}
			set
			{
				showQuickAdd = value;
			}
		}

		public bool ShowInactiveItems
		{
			get
			{
				return showInactiveItems;
			}
			set
			{
				showInactiveItems = value;
			}
		}

		public string FilterSysDocID
		{
			get
			{
				return filterSysDocID;
			}
			set
			{
				filterSysDocID = value;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
		}

		public string FilterCustomerID
		{
			get
			{
				return filterCustomerID;
			}
			set
			{
				filterCustomerID = value;
			}
		}

		public string SelectedUnitID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells["UnitID"].Value.ToString();
			}
		}

		public string SelectedProductID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells["Code"].Value.ToString();
			}
		}

		public decimal SelectedAverageCost
		{
			get
			{
				if (base.SelectedRow == null || base.SelectedRow.Cells["AverageCost"].Value == null || base.SelectedRow.Cells["AverageCost"].Value.ToString() == "")
				{
					return 0m;
				}
				return decimal.Parse(base.SelectedRow.Cells["AverageCost"].Value.ToString());
			}
		}

		public string SelectedTaxGroupID
		{
			get
			{
				if (base.SelectedRow == null || base.SelectedRow.Cells["TaxGroupID"].Value == null || base.SelectedRow.Cells["TaxGroupID"].Value.ToString() == "")
				{
					return "";
				}
				return base.SelectedRow.Cells["TaxGroupID"].Value.ToString();
			}
		}

		public ItemTypes SelectedItemType
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return ItemTypes.Inventory;
				}
				if (base.SelectedRow.Cells["ItemType"].Value == null || base.SelectedRow.Cells["ItemType"].Value.ToString() == "")
				{
					return ItemTypes.None;
				}
				return (ItemTypes)int.Parse(base.SelectedRow.Cells["ItemType"].Value.ToString());
			}
		}

		public ItemTypes[] AllowedItemTypes
		{
			get
			{
				return allowedTypes;
			}
			set
			{
				allowedTypes = value;
			}
		}

		public string TaxGroupID
		{
			get
			{
				object selectedCellValue = GetSelectedCellValue("TaxGroupID");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public ItemTaxOptions TaxOption
		{
			get
			{
				object selectedCellValue = GetSelectedCellValue("TaxOption");
				if (selectedCellValue.IsNullOrEmpty())
				{
					return ItemTaxOptions.BasedOnCustomer;
				}
				return (ItemTaxOptions)byte.Parse(selectedCellValue.ToString());
			}
		}

		public ProductComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Product;
			TABLENAME_FIELD = "Product";
			ID_FIELD = "ProductID";
			NAME_FIELD = "Description";
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
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		private void QuickAdd()
		{
		}

		public void FilterByCategory(string categoryID)
		{
			filteredCategoryID = categoryID;
			LoadData(isReferesh: false);
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetProductList(isReferesh);
		}

		public override void LoadData()
		{
			LoadData(isReferesh: false);
		}

		public void LoadData(bool isReferesh)
		{
			DataSet dataSet = null;
			try
			{
				dataSet = GetData(isReferesh);
				new DataSet();
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					if (showOnlyConsignmentItems)
					{
						DataRow[] rows = dataSet.Tables[0].Select("ItemType = " + 5.ToString());
						DataSet dataSet2 = new DataSet();
						dataSet2.Merge(rows);
						dataSet = dataSet2;
					}
					else if (showOnly3PLItems)
					{
						DataRow[] rows2 = dataSet.Tables[0].Select("ItemType = " + 9.ToString());
						DataSet dataSet3 = new DataSet();
						dataSet3.Merge(rows2);
						dataSet = dataSet3;
					}
					else if (showOnlyAssemblyItems)
					{
						DataRow[] rows3 = dataSet.Tables[0].Select("ItemType = " + 7.ToString());
						DataSet dataSet4 = new DataSet();
						dataSet4.Merge(rows3);
						dataSet = dataSet4;
					}
					else if (showOnlyInventoryItems)
					{
						DataRow[] rows4 = dataSet.Tables[0].Select("ItemType = " + 1.ToString());
						DataSet dataSet5 = new DataSet();
						dataSet5.Merge(rows4);
						dataSet = dataSet5;
					}
					else if (showOnlyMatrixAddableItems)
					{
						DataRow[] rows5 = dataSet.Tables[0].Select("ItemType = " + 1.ToString() + " AND MatrixParentID IS NULL ");
						DataSet dataSet6 = new DataSet();
						dataSet6.Merge(rows5);
						dataSet = dataSet6;
					}
					else if (showOnlyLotItems)
					{
						DataRow[] rows6 = dataSet.Tables[0].Select("IsTrackLot = '" + showOnlyLotItems.ToString() + "'");
						DataSet dataSet7 = new DataSet();
						dataSet7.Merge(rows6);
						dataSet = dataSet7;
					}
					if (dataSet.Tables.Count > 0)
					{
						if (!showConsignmentItems)
						{
							DataRow[] rows7 = dataSet.Tables[0].Select("ItemType <> " + 5.ToString());
							DataSet dataSet8 = new DataSet();
							dataSet8.Merge(rows7);
							dataSet = dataSet8;
						}
						if (!show3PLItems)
						{
							DataRow[] rows8 = dataSet.Tables[0].Select("ItemType <> " + 9.ToString());
							DataSet dataSet9 = new DataSet();
							dataSet9.Merge(rows8);
							dataSet = dataSet9;
						}
					}
				}
				if (allowedTypes.Length != 0)
				{
					string text = "";
					for (int i = 0; i < allowedTypes.Length; i++)
					{
						if (text != "")
						{
							text += ",";
						}
						text += (int)allowedTypes[i];
					}
					DataRow[] rows9 = dataSet.Tables[0].Select("ItemType IN (" + text + ")");
					DataSet dataSet10 = new DataSet();
					dataSet10.Merge(rows9);
					dataSet = dataSet10;
				}
				if (filterSysDocID != "")
				{
					string text2 = "";
					DataSet entityLinks = Factory.SystemDocumentSystem.GetEntityLinks(filterSysDocID, SysDocEntityTypes.ProductClass);
					if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
					{
						foreach (DataRow row in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
						{
							if (text2 != "")
							{
								text2 += ",";
							}
							text2 = text2 + "'" + row["EntityID"].ToString() + "'";
						}
					}
					if (text2 != "")
					{
						DataRow[] rows10 = dataSet.Tables[0].Select("ClassID IN (" + text2 + ")");
						DataSet dataSet11 = new DataSet();
						dataSet11.Merge(rows10);
						dataSet = dataSet11;
					}
				}
				if (FilterCustomerID != "")
				{
					string text3 = "";
					foreach (DataRow row2 in Factory.PriceListSystem.GetActivePriceListByCustomerID(FilterCustomerID).Tables[0].Rows)
					{
						if (text3 != "")
						{
							text3 += ",";
						}
						text3 = text3 + "'" + row2["ProductID"].ToString() + "'";
					}
					DataRow[] rows11 = dataSet.Tables[0].Select("Code IN (" + text3 + ")");
					DataSet dataSet12 = new DataSet();
					dataSet12.Merge(rows11);
					dataSet = dataSet12;
				}
				FillData(dataSet);
				if (base.DisplayLayout.Bands[0].Columns.Exists("ItemType"))
				{
					base.DisplayLayout.Bands[0].Columns["ItemType"].Hidden = false;
					ValueList valueList = new ValueList();
					valueList.ValueListItems.Add(0, "None");
					valueList.ValueListItems.Add(1, "Inventory");
					valueList.ValueListItems.Add(2, "Non-Inventory");
					valueList.ValueListItems.Add(3, "Service");
					valueList.ValueListItems.Add(4, "Discount");
					valueList.ValueListItems.Add(5, "Consignment");
					valueList.ValueListItems.Add(6, "Matrix");
					valueList.ValueListItems.Add(7, "Assembly");
					valueList.ValueListItems.Add(8, "Project Fee");
					base.DisplayLayout.Bands[0].Columns["ItemType"].ValueList = valueList;
					base.DisplayLayout.Bands[0].Columns["ItemType"].Width = 90;
				}
				if (CompanyPreferences.ShowItemUnitInCombo && base.DisplayLayout.Bands[0].Columns.Exists("UnitID"))
				{
					base.DisplayLayout.Bands[0].Columns["UnitID"].Hidden = false;
				}
				if (CompanyPreferences.ShowItemQuantityInCombo && base.DisplayLayout.Bands[0].Columns.Count > 0)
				{
					base.DisplayLayout.Bands[0].Columns["Quantity"].Hidden = false;
					base.DisplayLayout.Bands[0].Columns["Quantity"].Width = 80;
					base.DisplayLayout.Bands[0].Columns["Quantity"].CellAppearance.TextHAlign = HAlign.Right;
					base.DisplayLayout.Bands[0].Columns["Quantity"].Format = "#,##0.####";
				}
				if (CompanyPreferences.ShowItemCostInCombo)
				{
					base.DisplayLayout.Bands[0].Columns["AverageCost"].Hidden = false;
				}
				if (CompanyPreferences.ShowItemUPCInCombo)
				{
					base.DisplayLayout.Bands[0].Columns["UPC"].Hidden = false;
				}
				base.IsDataLoaded = true;
				if (dataSet != null)
				{
					if ((int)dataSet.Tables[0].Compute("AVG([CodeLength])", "") <= 20)
					{
						base.DisplayLayout.Bands[0].Columns["Code"].Width = 165;
					}
					else
					{
						base.DisplayLayout.Bands[0].Columns["Code"].Width = 200;
					}
					if ((int)dataSet.Tables[0].Compute("AVG([NameLength])", "") < 60)
					{
						base.DisplayLayout.Bands[0].Columns["Name"].Width = 350;
					}
					else
					{
						base.DisplayLayout.Bands[0].Columns["Name"].Width = 460;
					}
					base.DisplayLayout.Bands[0].Columns["UnitID"].Width = 50;
					base.DisplayLayout.Bands[0].Columns["UPC"].Width = 150;
					base.DisplayLayout.Bands[0].Columns["ItemType"].Width = 80;
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public ValueList GetProductUnitsValueList(string productID)
		{
			if (productID == "")
			{
				return new ValueList();
			}
			ValueList valueList = new ValueList();
			DataSet data = GetData(isReferesh: false);
			DataRow[] array = data.Tables["Product"].Select("Code = '" + productID + "'");
			if (array.Length != 0)
			{
				valueList.ValueListItems.Add(array[0]["UnitID"].ToString(), "");
			}
			string text = "";
			if (array.Length != 0)
			{
				text = array[0]["UnitID"].ToString();
				array = data.Tables["Product_Unit"].Select("ProductID = '" + productID + "' AND Code <> '" + text + "'");
			}
			for (int i = 0; i < array.Length; i++)
			{
				valueList.ValueListItems.Add(array[i]["Code"].ToString(), array[i]["Name"].ToString());
			}
			return valueList;
		}

		public ValueList GetProductTaxValueList(string productID)
		{
			if (productID == "")
			{
				return new ValueList();
			}
			ValueList valueList = new ValueList();
			DataSet data = GetData(isReferesh: false);
			DataRow[] array = data.Tables["Product"].Select("Code = '" + productID + "'");
			array = data.Tables["Tax_ProductClass_Detail"].Select("ProductID = '" + productID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				valueList.ValueListItems.Add(array[i]["TaxPercent"].ToString(), array[i]["TaxID"].ToString());
			}
			return valueList;
		}

		public string GetProductTaxValueWholeList(string productID)
		{
			string result = "";
			if (productID == "")
			{
				return result;
			}
			ValueList valueList = new ValueList();
			DataSet data = GetData(isReferesh: false);
			DataRow[] array = data.Tables["Product"].Select("Code = '" + productID + "'");
			array = data.Tables["Tax_ProductClass_Detail"].Select("ProductID = '" + productID + "'");
			for (int i = 0; i < array.Length; i++)
			{
				valueList.ValueListItems.Add(array[i]["TaxPercent"].ToString(), array[i]["TaxID"].ToString());
			}
			return result;
		}
	}
}
