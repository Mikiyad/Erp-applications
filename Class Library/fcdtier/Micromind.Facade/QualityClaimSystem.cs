using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class QualityClaimSystem : MarshalByRefObject, IQualityClaimSystem, IDisposable
	{
		private Config config;

		public QualityClaimSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateQualityClaim(QualityClaimData data, bool isUpdate)
		{
			return new QualityClaim(config).InsertUpdateQualityClaim(data, isUpdate);
		}

		public QualityClaimData GetQualityClaimByID(string sysDocID, string voucherID)
		{
			return new QualityClaim(config).GetQualityClaimByID(sysDocID, voucherID);
		}

		public bool DeleteQualityClaim(string sysDocID, string voucherID)
		{
			return new QualityClaim(config).DeleteQualityClaim(sysDocID, voucherID);
		}

		public DataSet GetQualityClaimAll()
		{
			return new QualityClaim(config).GetQualityClaimAll();
		}

		public DataSet GetQualityClaimToPrint(string sysDocID, string[] voucherID)
		{
			return new QualityClaim(config).GetQualityClaimToPrint(sysDocID, voucherID);
		}

		public DataSet GetQualityClaimToPrint(string sysDocID, string voucherID)
		{
			return new QualityClaim(config).GetQualityClaimToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid, bool showClose)
		{
			return new QualityClaim(config).GetList(from, to, isImport, showVoid, showClose);
		}

		public bool CanUpdate(string sysDocID, string voucherNumber)
		{
			return new QualityClaim(config).CanUpdate(sysDocID, voucherNumber, null);
		}

		public DataSet GetOpenQualityClaims(string vendorID)
		{
			return new QualityClaim(config).GetOpenQualityClaims(vendorID);
		}
	}
}
