using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IMaterialReservationSystem
	{
		bool CreateMaterialReservation(MaterialReservationData MaterialReservationData, bool isUpdate);

		MaterialReservationData GetMaterialReservationByID(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetMaterialReservationToPrint(string sysDocID, string[] voucherID);

		DataSet GetMaterialReservationToPrint(string sysDocID, string voucherID);

		bool DeleteMaterialReservation(string sysDocID, string voucherID);
	}
}
