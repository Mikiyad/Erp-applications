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
	public class ExpenseCodeComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private ExpenseCodeTypes expenseCodeType = ExpenseCodeTypes.LandingCost;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		[DefaultValue(ExpenseCodeTypes.LandingCost)]
		public ExpenseCodeTypes ExpenseCodeType
		{
			get
			{
				return expenseCodeType;
			}
			set
			{
				expenseCodeType = value;
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

		public string ExpenseRate => GetSelectedCellValue("ExpenseRate").ToString();

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

		public string TaxGroupID
		{
			get
			{
				if (!base.IsDataLoaded)
				{
					LoadData();
				}
				return GetSelectedCellValue("TaxGroupID").ToString();
			}
		}

		public ItemTaxOptions TaxOption
		{
			get
			{
				if (!base.IsDataLoaded)
				{
					LoadData();
				}
				object selectedCellValue = GetSelectedCellValue("TaxOption");
				if (selectedCellValue.IsNullOrEmpty())
				{
					return ItemTaxOptions.NonTaxable;
				}
				return (ItemTaxOptions)byte.Parse(selectedCellValue.ToString());
			}
		}

		public ExpenseCodeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.ExpenseCode;
			TABLENAME_FIELD = "Expense_Code";
			ID_FIELD = "ExpenseID";
			NAME_FIELD = "ExpenseName";
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
			return CombosData.GetExpenseCodeList(isReferesh);
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
					DataTable dataTable = dataSet.Tables[0];
					int num = (int)expenseCodeType;
					DataRow[] rows = dataTable.Select("ExpenseType = " + num.ToString());
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(rows);
					dataSet = dataSet2;
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
