using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyCancelSystem : MarshalByRefObject, IPropertyCancelSystem, IDisposable
	{
		private Config config;

		public PropertyCancelSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyCancel(PropertyCancelData data, bool isUpdate)
		{
			return new PropertyCancel(config).InsertUpdatePropertyCancel(data, isUpdate);
		}

		public PropertyCancelData GetPropertyCancelByID(string sysDocID, string voucherID)
		{
			return new PropertyCancel(config).GetPropertyCancelByID(sysDocID, voucherID);
		}

		public bool DeletePropertyCancel(string sysDocID, string voucherID, string propertyUnitID, string sourceSysDocID, string sourceVoucherID)
		{
			return new PropertyCancel(config).DeletePropertyCancel(sysDocID, voucherID, propertyUnitID, sourceSysDocID, sourceVoucherID);
		}

		public DataSet GetPropertyCancelToPrint(string sysDocID, string[] voucherID)
		{
			return new PropertyCancel(config).GetPropertyCancelToPrint(sysDocID, voucherID);
		}

		public DataSet GetPropertyCancelToPrint(string sysDocID, string voucherID)
		{
			return new PropertyCancel(config).GetPropertyCancelToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPropertyCancelList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate)
		{
			return new PropertyCancel(config).GetPropertyCancelList(sysDocID, customerID, fromDate, endDate);
		}

		public DataSet GetList(DateTime fromDate, DateTime endDate, bool val1, bool val2, int AgreeType)
		{
			return new PropertyCancel(config).GetList(fromDate, endDate, val1, val2, AgreeType);
		}

		public DataSet GetPropertyCancelDetailList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate)
		{
			return new PropertyCancel(config).GetPropertyCancelDetailList(sysDocID, customerID, fromDate, endDate);
		}

		public DataSet GetPropertyCancelDetail(string sysDocID, string VoucherID)
		{
			return new PropertyCancel(config).GetPropertyCancelDetail(sysDocID, VoucherID);
		}

		public DataSet GetPropertypaymentdetails(string sysDocID, string VoucherID)
		{
			return new PropertyCancel(config).GetPropertypaymentdetails(sysDocID, VoucherID);
		}
	}
}
