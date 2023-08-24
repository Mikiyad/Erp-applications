using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IQualityClaimSystem
	{
		bool CreateQualityClaim(QualityClaimData inventoryAdjustmentData, bool isUpdate);

		QualityClaimData GetQualityClaimByID(string sysDocID, string voucherID);

		bool DeleteQualityClaim(string sysDocID, string voucherID);

		DataSet GetQualityClaimToPrint(string sysDocID, string voucherID);

		DataSet GetQualityClaimToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, bool showClose);

		bool CanUpdate(string sysDocID, string voucherNumber);

		DataSet GetQualityClaimAll();

		DataSet GetOpenQualityClaims(string vendorID);
	}
}
