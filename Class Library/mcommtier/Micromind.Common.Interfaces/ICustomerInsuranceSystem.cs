using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomerInsuranceSystem
	{
		bool CreateCustomerInsurance(CustomerInsuranceData CustomerInsuranceData, bool isUpdate);

		CustomerInsuranceData GetCustomerInsurance();

		CustomerInsuranceData GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetList(string id);

		bool DeleteCustomerInsurance(string ID);

		CustomerInsuranceData GetCustomerInsuranceByID(string id);

		CustomerInsuranceData GetCustomerInsuranceIndividuals(string Customerid);

		string GetNextDocumentNumber(string sysDocID);

		DataSet GetCustomerInsuranceByFields(params string[] columns);

		DataSet GetCustomerInsuranceByFields(string[] ids, params string[] columns);

		DataSet GetCustomerInsuranceByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCustomerInsuranceList();

		DataSet GetCustomerInsuranceComboList();
	}
}
