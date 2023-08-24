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
	public class CustomerLeadsComboBox : MultiColumnComboBox
	{
		private PartnerComboData customer;

		private string filterSysDocID = "";

		private bool showQuickAdd = true;

		private Container components;

		private bool hasAll;

		private bool hasCustom;

		private bool isLPO;

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

		public string EntityType => GetSelectedCellValue("EntityType").ToString();

		public string DefaultTaxGroupID => GetSelectedCellValue("TaxGroupID").ToString();

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

		public bool ShowConsignmentOnly
		{
			get;
			set;
		}

		public bool ShowLPOCustomersOnly
		{
			get
			{
				return isLPO;
			}
			set
			{
				isLPO = value;
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

		public CustomerLeadsComboBox()
		{
			InitializeComponent();
			base.ComboType = DataComboType.CustomerandLead;
			TABLENAME_FIELD = "Customer";
			ID_FIELD = "CustomerID";
			NAME_FIELD = "CustomerName";
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
			base.Name = "CustomerLeadsComboBox";
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
				return CombosData.GetCustomerLeadsList(isReferesh);
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
					if (data != null && data.Tables.Count != 0)
					{
						_ = data.Tables[0];
						if (ShowConsignmentOnly)
						{
							DataRow[] rows = data.Tables[0].Select("AllowConsignment = 'True'");
							DataSet dataSet = new DataSet();
							dataSet.Merge(rows);
							data = dataSet;
						}
						if (ShowLPOCustomersOnly)
						{
							DataRow[] rows2 = data.Tables[0].Select("IsLPO = 'True'");
							DataSet dataSet2 = new DataSet();
							dataSet2.Merge(rows2);
							data = dataSet2;
						}
						if (ShowPROCustomersOnly)
						{
							DataRow[] rows3 = data.Tables[0].Select("IsPRO = 'True'");
							DataSet dataSet3 = new DataSet();
							dataSet3.Merge(rows3);
							data = dataSet3;
						}
						if (filterSysDocID != "")
						{
							string text = "";
							DataSet entityLinks = Factory.SystemDocumentSystem.GetEntityLinks(filterSysDocID, SysDocEntityTypes.CustomerClass);
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
								DataRow[] rows4 = data.Tables[0].Select("CustomerClassID IN (" + text + ")");
								DataSet dataSet4 = new DataSet();
								dataSet4.Merge(rows4);
								data = dataSet4;
							}
						}
						base.FillData(data);
						if (base.DisplayLayout.Bands[0].Columns.Exists("EntityType"))
						{
							base.DisplayLayout.Bands[0].Columns["EntityType"].Hidden = false;
							base.DisplayLayout.Bands[0].Columns["EntityType"].Width = 150;
						}
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
	}
}
