using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CLVoucherSystem : MarshalByRefObject, ICLVoucherSystem, IDisposable
	{
		private Config config;

		public CLVoucherSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCLVoucher(CLVoucherData data, bool isInactive, bool isHold, bool isUpdate)
		{
			return new CLVouchers(config).InsertUpdateCLVoucher(data, isInactive, isHold, isUpdate);
		}

		public CLVoucherData GetCLVoucherByID(string sysDocID, string voucherID)
		{
			return new CLVouchers(config).GetCLVoucherByID(sysDocID, voucherID);
		}

		public bool DeleteCLVoucher(string sysDocID, string voucherID)
		{
			return new CLVouchers(config).DeleteCLVoucher(sysDocID, voucherID);
		}

		public DataSet GetCLVoucherToPrint(string sysDocID, string voucherID)
		{
			return new CLVouchers(config).GetCLVoucherToPrint(sysDocID, voucherID);
		}

		public DataSet GetCLVoucherToPrint(string sysDocID, string[] voucherID)
		{
			return new CLVouchers(config).GetCLVoucherToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new CLVouchers(config).GetList(from, to, showVoid);
		}

		public bool VoidCLVoucher(string sysDocID, string voucherID, bool isVoid)
		{
			return new CLVouchers(config).VoidCLVoucher(sysDocID, voucherID, isVoid);
		}
	}
}
