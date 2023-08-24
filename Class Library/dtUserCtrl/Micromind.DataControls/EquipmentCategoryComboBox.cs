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
	public class EquipmentCategoryComboBox : MultiColumnComboBox
	{
		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private bool showAllLocations;

		private bool showConsignOutLocations;

		private bool showConsignInLocations;

		private bool showNormalLocations = true;

		private bool showWarehouseOnly;

		private bool showPOSOnly;

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

		public bool ShowConsignOut
		{
			get
			{
				return showConsignOutLocations;
			}
			set
			{
				showConsignOutLocations = value;
			}
		}

		public bool ShowConsignIn
		{
			get
			{
				return showConsignInLocations;
			}
			set
			{
				showConsignInLocations = value;
			}
		}

		public bool ShowWarehouseOnly
		{
			get
			{
				return showWarehouseOnly;
			}
			set
			{
				showWarehouseOnly = value;
			}
		}

		public bool ShowPOSOnly
		{
			get
			{
				return showPOSOnly;
			}
			set
			{
				showPOSOnly = value;
			}
		}

		public bool ShowNormalLocations
		{
			get
			{
				return showNormalLocations;
			}
			set
			{
				showNormalLocations = value;
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

		public bool ShowAll
		{
			get
			{
				return showAllLocations;
			}
			set
			{
				showAllLocations = value;
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

		public EquipmentCategoryComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.EquipmentCategory;
			TABLENAME_FIELD = "EA_Equipment_Category";
			ID_FIELD = "EquipmentCategoryID";
			NAME_FIELD = "EquipmentCategoryName";
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
			return CombosData.GetEquipmentCategoryList(isReferesh);
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
	}
}
