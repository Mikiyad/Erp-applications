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
	public class PriceLevelComboBox : MultiColumnComboBox
	{
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

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string SelectedID
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "";
				}
				return base.SelectedRow.Cells[0].Value.ToString();
			}
			set
			{
				if (value != "" && !base.IsDataLoaded)
				{
					LoadData();
				}
				if (value == "")
				{
					lastSelectedID = "";
					base.SelectedIndex = -1;
				}
				else
				{
					base.SelectedIndex = int.Parse(value) + 1;
				}
				if (!base.IsUpdating)
				{
					RaiseSelectedIndexChange();
				}
			}
		}

		public PriceLevelComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.PriceLevel;
			TABLENAME_FIELD = "Price_Level";
			ID_FIELD = "PriceLevelID";
			NAME_FIELD = "PriceLevelName";
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
			return CombosData.GetPriceLevelList(isReferesh);
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
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					base.DisplayLayout.Bands[0].Columns[0].Hidden = true;
				}
				base.DisplayMember = "Name";
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
