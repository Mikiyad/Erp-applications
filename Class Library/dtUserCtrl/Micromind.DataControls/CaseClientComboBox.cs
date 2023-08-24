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
	public class CaseClientComboBox : MultiColumnComboBox
	{
		private PartnerComboData customer;

		private string filterSysDocID = "";

		private string filterID = "";

		private bool showQuickAdd = true;

		private Container components;

		private bool hasAll;

		private bool hasCustom;

		private bool isDefendant;

		private bool isPlantiff;

		private bool includeInactive;

		private bool isPRO;

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

		public string FilterSysDocID
		{
			get
			{
				return filterSysDocID;
			}
			set
			{
				filterSysDocID = value;
				if (isDataLoaded)
				{
					LoadData(isReferesh: false);
				}
			}
		}

		public bool IsCustomerOnHold
		{
			get
			{
				if (SelectedID == "")
				{
					return false;
				}
				object selectedCellValue = GetSelectedCellValue("IsHold");
				if (selectedCellValue != null && selectedCellValue.ToString() != "")
				{
					return bool.Parse(selectedCellValue.ToString());
				}
				return false;
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

		public string DefaultCurrencyID => GetSelectedCellValue("CurrencyID").ToString();

		public string DefaultPaymentTerm => GetSelectedCellValue("PaymentTermID").ToString();

		public string DefaultPaymentMethod => GetSelectedCellValue("PaymentMethodID").ToString();

		public string DefaultSalesPersonID => GetSelectedCellValue("SalesPersonID").ToString();

		public string DefaultShippingMethod => GetSelectedCellValue("ShippingMethodID").ToString();

		public string DefaultShipToAddress => GetSelectedCellValue("ShipToAddressID").ToString();

		public string DefaultBillToAddress => GetSelectedCellValue("BillToAddressID").ToString();

		public string Balance => GetSelectedCellValue("Balance").ToString();

		public bool ShowDefendant
		{
			get
			{
				return isDefendant;
			}
			set
			{
				isDefendant = value;
			}
		}

		public bool ShowPlantiff
		{
			get
			{
				return isPlantiff;
			}
			set
			{
				isPlantiff = value;
			}
		}

		public bool ShowInactive
		{
			get
			{
				return includeInactive;
			}
			set
			{
				includeInactive = value;
			}
		}

		public bool ShowPROCustomersOnly
		{
			get
			{
				return isPRO;
			}
			set
			{
				isPRO = value;
			}
		}

		public bool HasParent
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return false;
				}
				if (base.SelectedRow.Cells["ParentCustomerID"].Value == null || base.SelectedRow.Cells["ParentCustomerID"].Value.ToString() == "")
				{
					return false;
				}
				return true;
			}
		}

		public new bool HasChildren
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return false;
				}
				if (Convert.ToInt32(base.SelectedRow.Cells["ChildCustomers"].Value.ToString()) <= 0)
				{
					return false;
				}
				return true;
			}
		}

		public CaseClientComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.CaseClient;
			TABLENAME_FIELD = "Case_Client";
			ID_FIELD = "CaseClientID";
			NAME_FIELD = "CaseClientName";
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
			base.Name = "caseClientComboBox";
			base.Size = new System.Drawing.Size(256, 0);
			ResumeLayout(false);
		}

		private void QuickAdd()
		{
		}

		protected DataSet GetData(bool isReferesh)
		{
			try
			{
				return CombosData.getCaseClientList(isReferesh);
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
				if (ShowPlantiff && ShowDefendant)
				{
					DataRow[] rows = dataSet.Tables[0].Select("IsPlantiff='true ' AND  IsDefendant = 'true '");
					DataSet dataSet2 = new DataSet();
					dataSet2.Merge(rows);
					dataSet = dataSet2;
				}
				else if (ShowDefendant)
				{
					DataRow[] rows2 = dataSet.Tables[0].Select("IsDefendant='true   '");
					DataSet dataSet3 = new DataSet();
					dataSet3.Merge(rows2);
					dataSet = dataSet3;
				}
				else if (ShowPlantiff)
				{
					DataRow[] rows3 = dataSet.Tables[0].Select("IsPlantiff='true   '");
					DataSet dataSet4 = new DataSet();
					dataSet4.Merge(rows3);
					dataSet = dataSet4;
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

		public override void FillData(DataSet data)
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					if (data != null && data.Tables.Count != 0)
					{
						_ = data.Tables[0];
						base.FillData(data);
					}
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

		public override void GetComboRowByID(string id)
		{
			try
			{
				DataSet data = (DataSet)(base.DataSource = Factory.CustomerSystem.GetCustomerComboList(id));
				FillData(data);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}
	}
}
