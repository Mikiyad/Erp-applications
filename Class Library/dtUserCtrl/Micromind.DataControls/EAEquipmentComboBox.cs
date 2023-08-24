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
	public class EAEquipmentComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

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

		public EAEquipmentComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.EAEquipment;
			TABLENAME_FIELD = "EA_Equipment";
			ID_FIELD = "EquipmentID";
			NAME_FIELD = "Description";
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
			return CombosData.GetEAEquipmentList(isReferesh);
		}

		public void Filter(string categoryID)
		{
			try
			{
				if (categoryID != "")
				{
					filterID = categoryID;
					LoadData(isReferesh: false);
				}
				else
				{
					filterID = "";
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
			dataSet = GetData(isReferesh);
			if (filterID != "")
			{
				DataRow[] rows = dataSet.Tables[0].Select("EquipmentCategoryID='" + filterID + "'");
				DataSet dataSet2 = dataSet.Clone();
				dataSet2.Tables[0].Rows.Clear();
				dataSet2.Merge(rows);
				FillData(dataSet2);
			}
			else
			{
				FillData(dataSet);
				base.IsDataLoaded = true;
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
}
