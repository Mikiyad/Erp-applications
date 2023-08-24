using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IContainerTrackingSystem
	{
		bool CreateContainerTracking(ContainerTrackingData areaData, bool isUpdate);

		ContainerTrackingData GetContainerTracking();

		bool DeleteContainerTracking(string ID);

		ContainerTrackingData GetContainerTrackingByID(string id);

		DataSet GetContainerTrackingByFields(params string[] columns);

		DataSet GetContainerTrackingByFields(string[] ids, params string[] columns);

		DataSet GetContainerTrackingByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetContainerTrackingList(DateTime from, DateTime to, bool showVoid);

		DataSet GetContainerTrackingToPrint(string sysDocID, string[] voucherID);

		DataSet GetContainerTrackingToPrint(string sysDocID, string voucherID);

		bool VoidTracking(string sysDocID, string voucherID, bool isVoid);

		DataSet GetContainerTrackingComboList();

		DataSet GetContainerTrackingFilterComboList();

		DataSet GetContainerTrackingReport(string containerNumber);

		DataSet GetContainerDetailsNew(string containerNumber);

		DataSet GetContainerDetailsOnStatus(string containerNumber, int status);

		ContainerTrackingData GetContainerDetailsOnStatusChange(string containerNumber);
	}
}
