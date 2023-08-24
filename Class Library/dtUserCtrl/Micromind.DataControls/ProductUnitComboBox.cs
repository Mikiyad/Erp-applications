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
	public class ProductUnitComboBox : MultiColumnComboBox
	{
		private string filterProductID = "3f36^$##_js";

		private ToolTip toolTip = new ToolTip();

		private Container components;

		public ProductUnitComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Unit;
			TABLENAME_FIELD = "Product_Unit";
			ID_FIELD = "ProductID";
			NAME_FIELD = "UnitID";
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

		public void ApplyFilter(string productID)
		{
			try
			{
				if (productID != "")
				{
					base.FilterString = " ProductID = '" + productID + "'";
				}
				else
				{
					base.FilterString = "";
				}
			}
			catch
			{
			}
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetProductUnitList(isReferesh);
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
				FillData(dataSet);
				if (filterProductID != "")
				{
					ApplyFilter(filterProductID);
				}
				base.IsDataLoaded = true;
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
	}
}
