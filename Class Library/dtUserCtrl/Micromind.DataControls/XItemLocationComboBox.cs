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
	public class XItemLocationComboBox : MultiColumnComboBox
	{
		private bool useAccountNumbers;

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

		public XItemLocationComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Location;
			TABLENAME_FIELD = "Location";
			ID_FIELD = "LocationID";
			NAME_FIELD = "LocationName";
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
				FillData(dataSet);
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