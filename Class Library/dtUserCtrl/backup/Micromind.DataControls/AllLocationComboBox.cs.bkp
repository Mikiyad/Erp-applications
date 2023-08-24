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
	public class AllLocationComboBox : MultiColumnComboBox
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

		public bool ShowDefaultLocationOnly
		{
			get;
			set;
		}

		public AllLocationComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Location;
			TABLENAME_FIELD = "Location";
			ID_FIELD = "LocationID";
			NAME_FIELD = "LocationName";
			if (Global.ConStatus == ConnectionStatus.Connected)
			{
				if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.ChangeInventoryLocation))
				{
					ShowDefaultLocationOnly = true;
				}
				else
				{
					ShowDefaultLocationOnly = false;
				}
			}
			else
			{
				ShowDefaultLocationOnly = false;
			}
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
			return CombosData.GetLocationList(isReferesh);
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
				DataSet dataSet2 = dataSet.Clone();
				if (showWarehouseOnly)
				{
					DataRow[] rows = dataSet.Tables[0].Select("ISNULL(IsWarehouse,'False')='True'");
					dataSet2.Merge(rows);
				}
				else if (showPOSOnly)
				{
					DataRow[] rows2 = dataSet.Tables[0].Select("ISNULL(ISPOSLocation,'False')='True'");
					dataSet2.Merge(rows2);
				}
				else
				{
					if (showNormalLocations)
					{
						DataRow[] rows3 = dataSet.Tables[0].Select("ISNULL(IsConsignOutLocation,'False')='False' AND ISNULL(IsConsignInLocation,'False') = 'False'");
						dataSet2.Merge(rows3);
					}
					if (showConsignOutLocations)
					{
						DataRow[] rows4 = dataSet.Tables[0].Select("IsConsignOutLocation='True'");
						dataSet2.Merge(rows4);
					}
					if (showConsignOutLocations)
					{
						DataRow[] rows5 = dataSet.Tables[0].Select("IsConsignInLocation='True'");
						dataSet2.Merge(rows5);
					}
				}
				if (ShowDefaultLocationOnly)
				{
					DataRow[] rows6 = dataSet.Tables[0].Select("Code = '" + Security.DefaultInventoryLocationID + "'");
					dataSet2.Clear();
					dataSet2.Merge(rows6);
				}
				dataSet = dataSet2;
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
