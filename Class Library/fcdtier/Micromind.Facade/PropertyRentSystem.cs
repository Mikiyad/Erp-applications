using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyRentSystem : MarshalByRefObject, IPropertyRentSystem, IDisposable
	{
		private Config config;

		public PropertyRentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyRent(PropertyRentData data, bool isUpdate)
		{
			return new PropertyRent(config).InsertUpdatePropertyRent(data, isUpdate);
		}

		public PropertyRentData GetPropertyRentByID(string sysDocID, string voucherID)
		{
			return new PropertyRent(config).GetPropertyRentByID(sysDocID, voucherID);
		}

		public bool DeletePropertyRent(string sysDocID, string voucherID)
		{
			return new PropertyRent(config).DeletePropertyRent(sysDocID, voucherID);
		}

		public DataSet GetPropertyRentToPrint(string sysDocID, string[] voucherID)
		{
			return new PropertyRent(config).GetPropertyRentToPrint(sysDocID, voucherID);
		}

		public DataSet GetPropertyRentToPrint(string sysDocID, string voucherID)
		{
			return new PropertyRent(config).GetPropertyRentToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPropertyRentList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate)
		{
			return new PropertyRent(config).GetPropertyRentList(sysDocID, customerID, fromDate, endDate);
		}

		public DataSet GetList(DateTime fromDate, DateTime endDate, bool val1, bool val2, int AgreeType)
		{
			return new PropertyRent(config).GetList(fromDate, endDate, val1, val2, AgreeType);
		}

		public DataSet GetList()
		{
			return new PropertyRent(config).GetList();
		}

		public DataSet GetPropertyRentDetailList(string sysDocID, string customerID, DateTime fromDate, DateTime endDate)
		{
			return new PropertyRent(config).GetPropertyRentDetailList(sysDocID, customerID, fromDate, endDate);
		}

		public DataSet GetPropertyRentDetail(string sysDocID, string VoucherID)
		{
			return new PropertyRent(config).GetPropertyRentDetail(sysDocID, VoucherID);
		}

		public DataSet GetPropertyRentActiveDetail(DateTime fromDate, DateTime endDate)
		{
			return new PropertyRent(config).GetPropertyRentActiveDetail(fromDate, endDate);
		}
	}
}
