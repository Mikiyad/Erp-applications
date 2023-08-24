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
	public class BankFacilityComboBox : MultiColumnComboBox
	{
		private bool showQuickAdd = true;

		private System.Windows.Forms.ToolTip toolTip = new System.Windows.Forms.ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private BankFacilityTypes filterFacilityType;

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

		[Browsable(false)]
		public decimal AvailableLimit
		{
			get
			{
				try
				{
					string selectedID = SelectedID;
					if (selectedID == "")
					{
						return 0m;
					}
					return Factory.BankFacilitySystem.GetBankFacilityAvailableLimit(selectedID);
				}
				catch
				{
					throw;
				}
			}
		}

		public string PayableAccountID
		{
			get
			{
				object selectedCellValue = GetSelectedCellValue("PayableAccountID");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public decimal LimitAmount
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("LimitAmount");
				if (selectedCellValue != null && selectedCellValue.ToString() != "")
				{
					return decimal.Parse(selectedCellValue.ToString());
				}
				return 0m;
			}
		}

		public string CurrentAccountID
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("CurrentAccountID");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public string CurrentAccountName
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("CurrentAccountName");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public string BankChargeAccountID
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("BankChargeAccountID");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public string BankInterestAccountID
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("BankInterestAccountID");
				if (selectedCellValue == null)
				{
					return "";
				}
				return selectedCellValue.ToString();
			}
		}

		public int TenorDays
		{
			get
			{
				if (!isDataLoaded)
				{
					ForceLoadData();
				}
				object selectedCellValue = GetSelectedCellValue("TenorDays");
				if (selectedCellValue.IsNullOrEmpty())
				{
					return 0;
				}
				return int.Parse(selectedCellValue.ToString());
			}
		}

		public BankFacilityTypes FilterFacilityType
		{
			get
			{
				return filterFacilityType;
			}
			set
			{
				filterFacilityType = value;
			}
		}

		public BankFacilityComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Area;
			TABLENAME_FIELD = "Bank_Facility";
			ID_FIELD = "FacilityID";
			NAME_FIELD = "FacilityName";
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
			return CombosData.GetBankFacilityList(isReferesh);
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
				if (FilterFacilityType != 0)
				{
					DataRow[] rows = dataSet.Tables[0].Select("FacilityType = " + ((int)FilterFacilityType).ToString());
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(rows);
					dataSet = dataSet2;
				}
				FillData(dataSet);
				if (base.DisplayLayout.Bands[0].Columns.Exists("FacilityType"))
				{
					base.DisplayLayout.Bands[0].Columns["FacilityType"].Hidden = false;
					base.DisplayLayout.Bands[0].Columns["FacilityType"].Width = 110;
					if (base.DisplayLayout.Bands[0].Columns["FacilityType"].ValueList == null)
					{
						ValueList valueList = new ValueList();
						valueList.ValueListItems.Add(0, "None");
						valueList.ValueListItems.Add(1, "LC");
						valueList.ValueListItems.Add(2, "TR");
						valueList.ValueListItems.Add(3, "Cheque Discounting");
						valueList.ValueListItems.Add(4, "Bill Discounting");
						valueList.ValueListItems.Add(5, "Over Draft");
						valueList.ValueListItems.Add(6, "Term Loan");
						valueList.ValueListItems.Add(7, "Fixed Loan");
						base.DisplayLayout.Bands[0].Columns["FacilityType"].ValueList = valueList;
					}
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
