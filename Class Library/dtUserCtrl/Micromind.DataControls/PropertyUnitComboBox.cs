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
	public class PropertyUnitComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private string PropertyID = "";

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool activeOnly;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private bool IsVirtualOnly;

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

		public bool ShowActiveOnly
		{
			get
			{
				return activeOnly;
			}
			set
			{
				activeOnly = value;
			}
		}

		public PropertyUnitComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.PropertyUnit;
			TABLENAME_FIELD = "Property_Unit";
			ID_FIELD = "PropertyUnitID";
			NAME_FIELD = "PropertyUnitName";
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
			return CombosData.GetPropertyUnitList(isReferesh);
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

		public void FilterByVirtual(bool isVirtualOnly)
		{
			IsVirtualOnly = isVirtualOnly;
			LoadData(isReferesh: false);
		}

		public override void FillData(DataSet data)
		{
			if (!Factory.IsDBConnected)
			{
				return;
			}
			_ = data.Tables[0];
			if (IsVirtualOnly)
			{
				DataRow[] array = data.Tables[0].Select("IsVirtual = false");
				DataSet dataSet = new DataSet();
				if (array.Length != 0)
				{
					dataSet.Merge(array);
				}
				else
				{
					dataSet = data.Clone();
				}
				data = dataSet;
			}
			if (ShowActiveOnly)
			{
				DataRow[] rows = data.Tables[0].Select("UnitStatus = 1");
				DataSet dataSet2 = new DataSet();
				dataSet2.Merge(rows);
				data = dataSet2;
			}
			if (PropertyID != "")
			{
				DataRow[] array2 = data.Tables[0].Select("PropertyID ='" + PropertyID + "'");
				DataSet dataSet3 = new DataSet();
				if (array2.Length != 0)
				{
					dataSet3.Merge(array2);
				}
				else
				{
					dataSet3 = data.Clone();
				}
				data = dataSet3;
			}
			base.FillData(data);
		}

		public void FilterByPropertyId(string propertyID)
		{
			try
			{
				if (propertyID != "")
				{
					PropertyID = propertyID;
					LoadData(isReferesh: false);
				}
				else
				{
					PropertyID = "";
					LoadData(isReferesh: false);
				}
			}
			catch
			{
			}
		}
	}
}
