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
	public class ServiceProviderComboBox : MultiColumnComboBox
	{
		private PartnerComboData vendor;

		private bool showQuickAdd = true;

		private string filterSysDocID = "";

		private Container components;

		private bool hasAll;

		private bool hasCustom;

		public PayeeTaxOptions TaxOption
		{
			get
			{
				if (base.SelectedRow == null)
				{
					return PayeeTaxOptions.NonTaxable;
				}
				if (!base.IsDataLoaded)
				{
					LoadData();
				}
				object selectedCellValue = GetSelectedCellValue("TaxOption");
				byte result = 2;
				byte.TryParse(selectedCellValue.ToString(), out result);
				return (PayeeTaxOptions)result;
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

		public string FilterSysDocID
		{
			get
			{
				return filterSysDocID;
			}
			set
			{
				filterSysDocID = value;
			}
		}

		public bool ShowConsignmentOnly
		{
			get;
			set;
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

		public PartnerComboData SelectedItem => new PartnerComboData();

		public string DefaultCurrencyID => GetSelectedCellValue("CurrencyID").ToString();

		public string DefaultPaymentTerm => GetSelectedCellValue("PaymentTermID").ToString();

		public string DefaultPaymentMethod => GetSelectedCellValue("PaymentMethodID").ToString();

		public string DefaultShippingMethod => GetSelectedCellValue("ShippingMethodID").ToString();

		public string DefaultBuyer => GetSelectedCellValue("BuyerID").ToString();

		public string DefaultPrimaryAddress => GetSelectedCellValue("PrimaryAddressID").ToString();

		public decimal ConsignComPercent
		{
			get
			{
				object selectedCellValue = GetSelectedCellValue("ConsignComPercent");
				if (selectedCellValue == null || selectedCellValue.ToString() == "")
				{
					return 0m;
				}
				return decimal.Parse(selectedCellValue.ToString());
			}
		}

		public string DefaultTaxGroupID => GetSelectedCellValue("TaxGroupID").ToString();

		public ServiceProviderComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.Vendor;
			TABLENAME_FIELD = "Vendor";
			ID_FIELD = "VendorID";
			NAME_FIELD = "VendorName";
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
			base.Name = "ServiceProviderComboBox";
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
				return CombosData.GetServiceProviderList(isReferesh);
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

		public override void FillData(DataSet data)
		{
			if (Factory.IsDBConnected)
			{
				try
				{
					_ = data.Tables[0];
					if (ShowConsignmentOnly)
					{
						DataRow[] rows = data.Tables[0].Select("AllowConsignment = 'True'");
						DataSet dataSet = new DataSet();
						dataSet.Merge(rows);
						data = dataSet;
					}
					if (filterSysDocID != "")
					{
						string text = "";
						DataSet entityLinks = Factory.SystemDocumentSystem.GetEntityLinks(filterSysDocID, SysDocEntityTypes.SupplierClass);
						if (entityLinks != null && entityLinks.Tables["System_Doc_Entity_Link"].Rows.Count > 0)
						{
							foreach (DataRow row in entityLinks.Tables["System_Doc_Entity_Link"].Rows)
							{
								if (text != "")
								{
									text += ",";
								}
								text = text + "'" + row["EntityID"].ToString() + "'";
							}
						}
						if (text != "")
						{
							DataRow[] rows2 = data.Tables[0].Select("VendorClassID IN (" + text + ")");
							DataSet dataSet2 = new DataSet();
							dataSet2.Merge(rows2);
							data = dataSet2;
						}
					}
					base.FillData(data);
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
