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
	public class CustomerAddressComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private string filterID = "";

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

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

		public new bool IsDataLoaded
		{
			get
			{
				return isDataLoaded;
			}
			set
			{
				isDataLoaded = value;
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

		public CustomerAddressComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.CustomerAddress;
			TABLENAME_FIELD = "Customer_Address";
			ID_FIELD = "AddressID";
			NAME_FIELD = "ContactName";
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

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetCustomerAddressList(isReferesh);
		}

		public void Filter(string customerID)
		{
			try
			{
				filterID = customerID;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
			catch
			{
			}
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
				if (filterID != "")
				{
					DataRow[] rows = dataSet.Tables[0].Select("CustomerID='" + filterID + "'");
					DataSet dataSet2 = dataSet.Clone();
					dataSet2.Tables[0].Rows.Clear();
					dataSet2.Merge(rows);
					FillData(dataSet2);
					base.DisplayLayout.Bands[0].Columns["AddressName"].Hidden = false;
				}
				else
				{
					DataSet dataSet3 = dataSet.Clone();
					DataRow dataRow = dataSet3.Tables[0].NewRow();
					dataRow["Code"] = "PRIMARY";
					dataSet3.Tables[0].Rows.Add(dataRow);
					FillData(dataSet3);
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
	}
}
