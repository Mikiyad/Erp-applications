using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class MaterialReservationSystem : MarshalByRefObject, IMaterialReservationSystem, IDisposable
	{
		private Config config;

		public MaterialReservationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateMaterialReservation(MaterialReservationData data, bool isUpdate)
		{
			return new MaterialReservation(config).InsertUpdateMaterialReservation(data, isUpdate);
		}

		public MaterialReservationData GetMaterialReservationByID(string sysDocID, string voucherID)
		{
			return new MaterialReservation(config).GetMaterialReservationByID(sysDocID, voucherID);
		}

		public bool DeleteMaterialReservation(string sysDocID, string voucherID)
		{
			return new MaterialReservation(config).DeleteMaterialReservation(sysDocID, voucherID);
		}

		public DataSet GetMaterialReservationToPrint(string sysDocID, string[] voucherID)
		{
			return new MaterialReservation(config).GetMaterialReservationToPrint(sysDocID, voucherID);
		}

		public DataSet GetMaterialReservationToPrint(string sysDocID, string voucherID)
		{
			return new MaterialReservation(config).GetMaterialReservationToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new MaterialReservation(config).GetList(fromDate, toDate, showVoid: true);
		}
	}
}
