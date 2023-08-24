using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyCancelSystem
	{
		bool CreatePropertyCancel(PropertyCancelData propertyCancelData, bool isUpdate);

		PropertyCancelData GetPropertyCancelByID(string sysDocID, string voucherID);

		DataSet GetPropertyCancelToPrint(string sysDocID, string[] voucherID);

		DataSet GetPropertyCancelToPrint(string sysDocID, string voucherID);

		bool DeletePropertyCancel(string sysDocID, string voucherID, string propertyUnitID, string sourceSysDocID, string sourceVoucherID);

		DataSet GetPropertyCancelList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate);

		DataSet GetList(DateTime from, DateTime to, bool val, bool val2, int AgreeType);

		DataSet GetPropertyCancelDetailList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate);

		DataSet GetPropertyCancelDetail(string sysDocID, string voucherID);

		DataSet GetPropertypaymentdetails(string sysDocID, string voucherID);
	}
}
