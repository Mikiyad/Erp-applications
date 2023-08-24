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
	public class PropertyIncomeCodeComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private PropertyIncomeCodeTypes incomeCodeType = PropertyIncomeCodeTypes.Rent;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		[DefaultValue(PropertyIncomeCodeTypes.Rent)]
		public PropertyIncomeCodeTypes IncomeCodeType
		{
			get
			{
				return incomeCodeType;
			}
			set
			{
				incomeCodeType = value;
			}
		}

		public string TaxGroupID => GetSelectedCellValue("TaxGroupID").ToString();

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

		public string AccountID => GetSelectedCellValue("AccountID").ToString();

		public string IncomeRate => GetSelectedCellValue("IncomeRate").ToString();

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

		public PropertyIncomeCodeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.PropertyIncomeCode;
			TABLENAME_FIELD = "PropertyIncome_Code";
			ID_FIELD = "IncomeID";
			NAME_FIELD = "IncomeName";
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
			return CombosData.GetPropertyIncomeCodeList(isReferesh);
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
				if (dataSet != null)
				{
					_ = dataSet.Tables.Count;
					_ = 0;
				}
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
	}
}
