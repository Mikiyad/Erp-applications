using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyServiceSystem
	{
		bool CreatePropertyService(PropertyServiceData PropertyServiceData);

		bool UpdatePropertyService(PropertyServiceData PropertyServiceData);

		bool CreatePropertyServiceAssign(PropertyServiceData PropertyServiceAssignData);

		bool UpdatePropertyServiceAssign(PropertyServiceData PropertyServiceAssignData);

		PropertyServiceData GetPropertyService();

		bool DeletePropertyService(string ID);

		bool DeletePropertyServiceAssign(string ID);

		PropertyServiceData GetPropertyServiceByID(string id);

		DataSet GetPropertyServiceByFields(params string[] columns);

		DataSet GetPropertyServiceByFields(string[] ids, params string[] columns);

		DataSet GetPropertyServiceByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPropertyServiceList(bool includeClosedTasks);

		DataSet GetPropertyServiceComboList();

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID);

		DataSet GetPropertyServiceAssignList(DateTime from, DateTime to, bool showVoid, string SysDocID);

		DataSet GetServiceRequestList(string SysDocID);

		DataSet GetPropertyServiceRequestToPrint(string sysDocID, string voucherID);

		DataSet GetPropertyServiceAssignToPrint(string sysDocID, string voucherID);

		PropertyServiceData GetPropertyServiceAssignByID(string id);

		DataSet GetTenantByUnit(string unitID, DateTime reportingdate);
	}
}
