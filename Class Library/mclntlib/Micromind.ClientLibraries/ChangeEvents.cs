using Micromind.Common.Data;
using System;

namespace Micromind.ClientLibraries
{
	public sealed class ChangeEvents
	{
		public static event EventHandler ItemChanged;

		public static event EventHandler AccountChanged;

		public static event EventHandler PartnerChanged;

		public static event EventHandler BankChanged;

		public static event EventHandler DepartmentChanged;

		public static event EventHandler EmployeeChanged;

		public static event EventHandler PayCodeChanged;

		public static event EventHandler BrandChanged;

		public static event EventHandler CategoryChanged;

		public static event EventHandler ColorChanged;

		public static event EventHandler ContainerChanged;

		public static event EventHandler ManufacturerChanged;

		public static event EventHandler ModelChanged;

		public static event EventHandler JobChanged;

		public static event EventHandler JobTypeChanged;

		public static event EventHandler LCChanged;

		public static event EventHandler CurrencyChanged;

		public static event EventHandler PaymentMethodChanged;

		public static event EventHandler PriceLevelChanged;

		public static event EventHandler ShipperChanged;

		public static event EventHandler TermChanged;

		public static event EventHandler UnitChanged;

		public static event EventHandler StoreChanged;

		public static event EventHandler CustomFieldGroupChanged;

		public static event EventHandler EntityTypeChanged;

		public static event EventHandler FixedAssetChanged;

		public static event EventHandler FixedAssetGroupChanged;

		public static event EventHandler FixedAssetLocationChanged;

		public static event EventHandler FixedAssetClassChanged;

		private ChangeEvents()
		{
		}

		public static void OnItemChanged(object obj, int itemID, ChangeTypes type)
		{
			if (ChangeEvents.ItemChanged != null)
			{
				ChangeEvents.ItemChanged(obj, new ChangeEventArg(itemID, type));
			}
		}

		public static void OnAccountChanged(object obj, int accountID, ChangeTypes changeType)
		{
			if (ChangeEvents.AccountChanged != null)
			{
				ChangeEvents.AccountChanged(obj, new ChangeEventArg(accountID, changeType));
			}
		}

		public static void OnPartnerChanged(object obj, int partnerID, PartnerType partnerType, ChangeTypes changeType)
		{
			if (ChangeEvents.PartnerChanged != null)
			{
				ChangeEvents.PartnerChanged(obj, new ChangeEventArg(partnerID, partnerType, changeType));
			}
		}

		public static void OnBankChanged(object obj, int bankID, ChangeTypes changeType)
		{
			if (ChangeEvents.BankChanged != null)
			{
				ChangeEvents.BankChanged(obj, new ChangeEventArg(bankID, changeType));
			}
		}

		public static void OnDepartmentChanged(object obj, int departmentID, ChangeTypes changeType)
		{
			if (ChangeEvents.DepartmentChanged != null)
			{
				ChangeEvents.DepartmentChanged(obj, new ChangeEventArg(departmentID, changeType));
			}
		}

		public static void OnCustomFieldGroupChanged(object obj, int groupID, ChangeTypes changeType)
		{
			if (ChangeEvents.CustomFieldGroupChanged != null)
			{
				ChangeEvents.CustomFieldGroupChanged(obj, new ChangeEventArg(groupID, changeType));
			}
		}

		public static void OnEmployeeChanged(object obj, int employeeID, ChangeTypes changeType)
		{
			if (ChangeEvents.EmployeeChanged != null)
			{
				ChangeEvents.EmployeeChanged(obj, new ChangeEventArg(employeeID, changeType));
			}
		}

		public static void OnPayCodeChanged(object obj, int payCodeID, ChangeTypes changeType)
		{
			if (ChangeEvents.PayCodeChanged != null)
			{
				ChangeEvents.PayCodeChanged(obj, new ChangeEventArg(payCodeID, changeType));
			}
		}

		public static void OnBrandChanged(object obj, int brandID, ChangeTypes changeType)
		{
			if (ChangeEvents.BrandChanged != null)
			{
				ChangeEvents.BrandChanged(obj, new ChangeEventArg(brandID, changeType));
			}
		}

		public static void OnCategoryChanged(object obj, int categoryID, ChangeTypes changeType)
		{
			if (ChangeEvents.CategoryChanged != null)
			{
				ChangeEvents.CategoryChanged(obj, new ChangeEventArg(categoryID, changeType));
			}
		}

		public static void OnColorChanged(object obj, int colorID, ChangeTypes changeType)
		{
			if (ChangeEvents.ColorChanged != null)
			{
				ChangeEvents.ColorChanged(obj, new ChangeEventArg(colorID, changeType));
			}
		}

		public static void OnContainerChanged(object obj, int containerID, ChangeTypes changeType)
		{
			if (ChangeEvents.ContainerChanged != null)
			{
				ChangeEvents.ContainerChanged(obj, new ChangeEventArg(containerID, changeType));
			}
		}

		public static void OnManufacturerChanged(object obj, int manufacturerID, ChangeTypes changeType)
		{
			if (ChangeEvents.ManufacturerChanged != null)
			{
				ChangeEvents.ManufacturerChanged(obj, new ChangeEventArg(manufacturerID, changeType));
			}
		}

		public static void OnModelChanged(object obj, int modelID, ChangeTypes changeType)
		{
			if (ChangeEvents.ModelChanged != null)
			{
				ChangeEvents.ModelChanged(obj, new ChangeEventArg(modelID, changeType));
			}
		}

		public static void OnJobChanged(object obj, int jobID, ChangeTypes changeType)
		{
			if (ChangeEvents.JobChanged != null)
			{
				ChangeEvents.JobChanged(obj, new ChangeEventArg(jobID, changeType));
			}
		}

		public static void OnJobTypeChanged(object obj, int jobTypeID, ChangeTypes changeType)
		{
			if (ChangeEvents.JobTypeChanged != null)
			{
				ChangeEvents.JobTypeChanged(obj, new ChangeEventArg(jobTypeID, changeType));
			}
		}

		public static void OnLCChanged(object obj, int lcID, ChangeTypes changeType)
		{
			if (ChangeEvents.LCChanged != null)
			{
				ChangeEvents.LCChanged(obj, new ChangeEventArg(lcID, changeType));
			}
		}

		public static void OnCurrencyChanged(object obj, int currencyID, ChangeTypes changeType)
		{
			if (ChangeEvents.CurrencyChanged != null)
			{
				ChangeEvents.CurrencyChanged(obj, new ChangeEventArg(currencyID, changeType));
			}
		}

		public static void OnPaymentMethodChanged(object obj, int paymentMethodID, ChangeTypes changeType)
		{
			if (ChangeEvents.PaymentMethodChanged != null)
			{
				ChangeEvents.PaymentMethodChanged(obj, new ChangeEventArg(paymentMethodID, changeType));
			}
		}

		public static void OnPriceLevelChanged(object obj, int priceLevelID, ChangeTypes changeType)
		{
			if (ChangeEvents.PriceLevelChanged != null)
			{
				ChangeEvents.PriceLevelChanged(obj, new ChangeEventArg(priceLevelID, changeType));
			}
		}

		public static void OnShipperChanged(object obj, int shipperID, ChangeTypes changeType)
		{
			if (ChangeEvents.ShipperChanged != null)
			{
				ChangeEvents.ShipperChanged(obj, new ChangeEventArg(shipperID, changeType));
			}
		}

		public static void OnTermChanged(object obj, int termID, ChangeTypes changeType)
		{
			if (ChangeEvents.TermChanged != null)
			{
				ChangeEvents.TermChanged(obj, new ChangeEventArg(termID, changeType));
			}
		}

		public static void OnUnitChanged(object obj, int unitID, ChangeTypes changeType)
		{
			if (ChangeEvents.UnitChanged != null)
			{
				ChangeEvents.UnitChanged(obj, new ChangeEventArg(unitID, changeType));
			}
		}

		public static void OnStoreChanged(object obj, int storeID, ChangeTypes changeType)
		{
			if (ChangeEvents.StoreChanged != null)
			{
				ChangeEvents.StoreChanged(obj, new ChangeEventArg(storeID, changeType));
			}
		}

		public static void OnEntityTypeChanged(object obj, int entityTypeID, ChangeTypes changeType)
		{
			if (ChangeEvents.EntityTypeChanged != null)
			{
				ChangeEvents.EntityTypeChanged(obj, new ChangeEventArg(entityTypeID, changeType));
			}
		}

		public static void OnFixedAssetChanged(object obj, int fixedAssetID, ChangeTypes changeType)
		{
			if (ChangeEvents.FixedAssetChanged != null)
			{
				ChangeEvents.FixedAssetChanged(obj, new ChangeEventArg(fixedAssetID, changeType));
			}
		}

		public static void OnFixedAssetGroupChanged(object obj, int fixedAssetID, ChangeTypes changeType)
		{
			if (ChangeEvents.FixedAssetGroupChanged != null)
			{
				ChangeEvents.FixedAssetGroupChanged(obj, new ChangeEventArg(fixedAssetID, changeType));
			}
		}

		public static void OnFixedAssetLocationChanged(object obj, int fixedAssetID, ChangeTypes changeType)
		{
			if (ChangeEvents.FixedAssetLocationChanged != null)
			{
				ChangeEvents.FixedAssetLocationChanged(obj, new ChangeEventArg(fixedAssetID, changeType));
			}
		}

		public static void OnFixedAssetClassChanged(object obj, int fixedAssetTypeID, ChangeTypes changeType)
		{
			if (ChangeEvents.FixedAssetClassChanged != null)
			{
				ChangeEvents.FixedAssetClassChanged(obj, new ChangeEventArg(fixedAssetTypeID, changeType));
			}
		}
	}
}
