using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesManTargetSystem
	{
		bool CreateSalesManTarget(SalesManTargetData salesmantargetData, bool isUpdate);

		SalesManTargetData GetSalesManTargetByID(string sysDocID, string voucherID);

		bool DeleteSalesManTarget(string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetSalesManTargetToPrint(string sysDocID, string VoucherID);
	}
}
