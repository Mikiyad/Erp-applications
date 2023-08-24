using Micromind.Common.Data;
using System;
using System.Data;
using System.IO;

namespace Micromind.Common.Interfaces
{
	public interface IPartnerSystem
	{
		string PartnerPicDirectory
		{
			get;
			set;
		}

		bool UpdatePartner(PartnerData partnerData);

		bool UpdatePartner(PartnerData partnerData, CustomFieldData customFieldData);

		int CreateVendor(string name, string phone, string email);

		int CreateCustomer(string name, string phone, string email);

		int CreatePartner(PartnerData partnerData);

		int CreatePartner(PartnerData partnerData, string closingPassword);

		PartnerData GetPartnerByID(object partnerID);

		int CreatePartner(string name, PartnerType partnerType);

		int CreatePartner(PartnerData partnerData, CustomFieldData customFieldData, string closingPassword);

		int CreatePartner(PartnerData partnerData, CustomFieldData customFieldData);

		int CreateCustomer(PartnerData partnerData, CustomerInvoiceData customerInvoiceData);

		int CreateCustomer(PartnerData partnerData, CustomerInvoiceData customerInvoiceData, string closingPassword);

		int CreateCustomer(PartnerData partnerData, CustomerInvoiceData customerInvoiceData, CustomFieldData customFieldData, string closingPassword);

		int CreateCustomer(PartnerData partnerData, CustomerInvoiceData customerInvoiceData, CustomFieldData customFieldData);

		int CreateVendor(PartnerData partnerData, PurchaseInvoiceData purchaseInvoiceData);

		int CreateVendor(PartnerData partnerData, PurchaseInvoiceData purchaseInvoiceData, string closingPassword);

		int CreateVendor(PartnerData partnerData, PurchaseInvoiceData purchaseInvoiceData, CustomFieldData customFieldData, string closingPassword);

		int CreateVendor(PartnerData partnerData, PurchaseInvoiceData purchaseInvoiceData, CustomFieldData customFieldData);

		bool DeletePartner(object partnerID);

		DataSet GetCompanies();

		DataSet GetPartnersByFields(PartnerType partnerType, params string[] columns);

		DataSet GetPartnersByFields(bool isInactive, PartnerType partnerType, params string[] columns);

		bool ActivatePartner(object partnerID, bool activate);

		bool ExistPartnerName(string partnerName);

		DataSet GetPartnerPhone(PartnerType partnerType);

		DataSet GetPartnerContact(PartnerType partnerType);

		DataSet GetPartners(PartnerType partnerType);

		string GetPartnerName(int id);

		DataSet GetPartnersName(PartnerType partnerType);

		bool ChangeDefaultShipping(int partnerID, int shippingID);

		bool ChangeDefaultBilling(int partnerID, int billingID);

		decimal GetOpeningBalance(int partnerID, PartnerType partnerType);

		bool UpdateOpeningBalance(decimal initialBalance, int partnerID, DateTime date, PartnerType partnerType);

		bool UpdateOpeningBalance(decimal initialBalance, int partnerID, DateTime date, PartnerType partnerType, string closingPassword);

		bool UpdateCustomerOpeningBalance(CustomerInvoiceData data, int partnerID);

		bool UpdateCustomerOpeningBalance(CustomerInvoiceData data, string closingPassword, int partnerID);

		bool UpdateVendorOpeningBalance(PurchaseInvoiceData data, int partnerID);

		bool UpdateVendorOpeningBalance(PurchaseInvoiceData data, string closingPassword, int partnerID);

		DataSet GetPartnersByFields(params string[] fields);

		DataSet GetPartnerByFields(int partnerID, params string[] fields);

		DataSet GetPartnersByFields(int[] partnersID, PartnerType partnerType, params string[] columns);

		DataSet GetPartnersByFields(bool isInactive, int[] partnersID, PartnerType partnerType, params string[] columns);

		DataSet GetPartnersAddressByFields(int[] partnersID, PartnerType partnerType, params string[] columns);

		Stream GetPartnerImage(int id);

		Stream GetPartnerThumbnailImage(int id, int width, int height);

		bool SavePartnerImage(int id, string imageName, Stream image);

		DataSet GetPartnersAddressByFields(PartnerType partnerType, params string[] columns);

		bool CreateUpdatePartnerBatch(DataSet partnerData, bool checkConcurrency, bool allowPartnerTypeChange);

		int GetTerms(int partnerID);

		int GetPriceLevelID(int partnerID);

		short GetPreferredPaymentMethod(int partnerID);
	}
}
