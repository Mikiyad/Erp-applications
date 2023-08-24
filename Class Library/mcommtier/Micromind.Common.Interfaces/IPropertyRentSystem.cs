using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyRentSystem
	{
		bool CreatePropertyRent(PropertyRentData propertyRentData, bool isUpdate);

		PropertyRentData GetPropertyRentByID(string sysDocID, string voucherID);

		DataSet GetPropertyRentToPrint(string sysDocID, string[] voucherID);

		DataSet GetPropertyRentToPrint(string sysDocID, string voucherID);

		bool DeletePropertyRent(string sysDocID, string voucherID);

		DataSet GetPropertyRentList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate);

		DataSet GetList(DateTime from, DateTime to, bool val, bool val2, int AgreeType);

		DataSet GetPropertyRentDetailList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate);

		DataSet GetPropertyRentDetail(string sysDocID, string voucherID);

		DataSet GetPropertyRentActiveDetail(DateTime from, DateTime to);

		DataSet GetList();
	}
}
