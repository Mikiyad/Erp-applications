using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ProductUnitDetailComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

		private bool isKeyPressed;

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

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

		public string FilterProductID
		{
			get;
			set;
		}

		public ProductUnitDetailComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Accounts;
			TABLENAME_FIELD = "Product_Unit";
			ID_FIELD = "UnitID";
			NAME_FIELD = "UnitName";
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

		private void textBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData != Keys.F3 && e.KeyData == Keys.F5)
			{
				LoadData(isReferesh: true);
			}
		}

		private void OnControl_LostFocus(object sender, EventArgs e)
		{
			QuickAdd();
		}

		private void flatComboBox_Load(object sender, EventArgs e)
		{
		}

		private CompanyAccountTypes GetAccountType()
		{
			return (base.Parent as IComboBoxAccount)?.GetAccountType() ?? CompanyAccountTypes.Bank;
		}

		private void QuickAdd()
		{
			if (!Security.HasComboAccessRight(ScreenAreas.Accounts, suppressMessage: true))
			{
				showQuickAdd = false;
				return;
			}
			string a = Text = Text.Trim();
			_ = (a == "");
		}

		private DataSet GetData(bool isReferesh)
		{
			return CombosData.GetProductUnitDetailList(refresh: false);
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

		public override void FillData(DataSet data)
		{
			try
			{
				if (Factory.IsDBConnected)
				{
					DataRow[] array = data.Tables[0].Select("ProductID = '" + FilterProductID + "'");
					DataSet dataSet = data.Clone();
					if (array.Length != 0)
					{
						dataSet.Merge(array);
					}
					data = dataSet;
					base.FillData(data);
					base.DisplayLayout.Bands[0].Columns["ProductID"].Hidden = true;
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

		public void ApplyFilter(string productID)
		{
			FilterProductID = productID;
			LoadData(isReferesh: false);
		}
	}
}
