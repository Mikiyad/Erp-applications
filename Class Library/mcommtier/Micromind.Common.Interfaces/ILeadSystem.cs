using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILeadSystem
	{
		bool CreateLead(LeadData leadData);

		bool UpdateLead(LeadData leadData);

		LeadData GetLead();

		bool DeleteLead(string ID);

		LeadData GetLeadByID(string id);

		DataSet GetLeadByFields(params string[] columns);

		DataSet GetLeadByFields(string[] ids, params string[] columns);

		DataSet GetLeadByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetLeadList(bool inactive);

		DataSet GetLeadComboList();

		DataSet GetLeadSourceComboList();

		DataSet GetLeadDocumentAddress(string leadID, string addressField);

		DataSet GetLeadListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive);

		DataSet GetLeadPrimaryContactListReport(string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, bool showInactive);

		DataSet GetLeadProfileReport(string fromLead, string toLead, bool showInactive);

		DataSet GetLeadBySourceReport(string fromSource, string toSource, string fromArea, string toArea, bool showInactive);

		DataSet GetLeadActivityReport(DateTime from, DateTime to, string fromLead, string toLead, string fromClass, string toClass, string fromGroup, string toGroup);

		bool SetFlag(string leadID, byte flagID);
	}
}
