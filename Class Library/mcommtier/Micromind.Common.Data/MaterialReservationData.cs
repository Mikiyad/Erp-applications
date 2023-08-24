using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class MaterialReservationData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string VALIDDATEFROM_FIELD = "ValidDateFrom";

		public const string VALIDDATETO_FIELD = "ValidDateTo";

		public const string STATUS_FIELD = "Status";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ISVOID_FIELD = "IsVoid";

		public const string MATERIALRESERVATION_TABLE = "Material_Reservation";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string FACTOR_FIELD = "Factor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string MATERIALRESERVATIONDETAIL_TABLE = "Material_Reservation_Detail";

		public DataTable MaterialReservationTable => base.Tables["Material_Reservation"];

		public DataTable MaterialReservationDetailsTable => base.Tables["Material_Reservation_Detail"];

		public MaterialReservationData()
		{
			BuildDataTables();
		}

		public MaterialReservationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Material_Reservation");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("ValidDateFrom", typeof(string));
			columns.Add("ValidDateTo", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Material_Reservation_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("Cost", typeof(decimal));
			columns.Add("Factor", typeof(float));
			columns.Add("FactorType", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
