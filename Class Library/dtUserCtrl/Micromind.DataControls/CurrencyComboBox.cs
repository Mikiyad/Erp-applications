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
	public class CurrencyComboBox : MultiColumnComboBox
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

		public decimal SelectedRate
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return 1m;
				}
				if (base.SelectedRow.Cells["ExchangeRate"].Value.ToString() != "")
				{
					return decimal.Parse(base.SelectedRow.Cells["ExchangeRate"].Value.ToString());
				}
				return 1m;
			}
		}

		public string SelectedRateType
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return "M";
				}
				if (!base.SelectedRow.Cells.Exists("ExchangeRate"))
				{
					LoadData();
				}
				if (base.SelectedRow.Cells["ExchangeRate"].Value.ToString() != "")
				{
					return base.SelectedRow.Cells["RateType"].Value.ToString();
				}
				return "M";
			}
		}

		public CurrencyComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Currency;
			TABLENAME_FIELD = "Currency";
			ID_FIELD = "CurrencyID";
			NAME_FIELD = "CurrencyName";
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
			return CombosData.GetCurrencyList(isReferesh);
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
