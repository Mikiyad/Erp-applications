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
	public class AdjustmentTypeComboBox : MultiColumnComboBox
	{
		public enum AdjustmentTypes : byte
		{
			None,
			NonSale
		}

		private new bool isDataLoaded;

		private bool showQuickAdd = true;

		private ToolTip toolTip = new ToolTip();

		private Container components;

		private bool hasCustom;

		private bool hasAll;

		private bool showInactiveItems;

		private bool showNonSaleItems;

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

		public bool ShowNonSaleItems
		{
			get
			{
				return showNonSaleItems;
			}
			set
			{
				showNonSaleItems = value;
			}
		}

		public AdjustmentTypeComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.AdjustmentType;
			TABLENAME_FIELD = "Adjustment_Type";
			ID_FIELD = "TypeID";
			NAME_FIELD = "TypeName";
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
			return CombosData.GetAdjustmentTypeList(isReferesh);
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
				if (dataSet != null && dataSet.Tables.Count > 0 && showNonSaleItems)
				{
					DataRow[] rows = dataSet.Tables[0].Select("IsNonSale = " + 1.ToString());
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
