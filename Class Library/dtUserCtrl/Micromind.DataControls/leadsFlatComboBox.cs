using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataCaches;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace Micromind.DataControls
{
	public class leadsFlatComboBox : MultiColumnComboBox
	{
		private PartnerComboData lead;

		private bool showQuickAdd = true;

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

		public bool HasAll
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

		public string DefaultCurrencyID => GetSelectedCellValue("CurrencyID").ToString();

		public string DefaultPaymentTerm => GetSelectedCellValue("PaymentTermID").ToString();

		public string DefaultSalesPersonID => GetSelectedCellValue("SalesPersonID").ToString();

		public leadsFlatComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Lead;
			TABLENAME_FIELD = "Lead";
			ID_FIELD = "LeadID";
			NAME_FIELD = "LeadName";
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
			base.Name = "leadsFlatComboBox";
			base.Size = new System.Drawing.Size(256, 0);
			ResumeLayout(false);
		}

		protected DataSet GetData(bool isReferesh)
		{
			try
			{
				return CombosData.GetLeadsList(isReferesh);
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return null;
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
