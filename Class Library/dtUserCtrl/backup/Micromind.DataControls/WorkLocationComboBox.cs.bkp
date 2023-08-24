using Infragistics.Win.UltraWinGrid;
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
	public class WorkLocationComboBox : MultiColumnComboBox
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

		public WorkLocationComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.WorkLocation;
			TABLENAME_FIELD = "Work_Location";
			ID_FIELD = "WorkLocationID";
			NAME_FIELD = "WorkLocationName";
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
			return CombosData.GetWorkLocationList(isReferesh);
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
				if (base.DropDownStyle == UltraComboStyle.DropDownList)
				{
					foreach (DataRow row in dataSet.Tables[0].Rows)
					{
						if (row[0].ToString() == "")
						{
							dataSet.Tables[0].Rows.Remove(row);
							break;
						}
					}
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
