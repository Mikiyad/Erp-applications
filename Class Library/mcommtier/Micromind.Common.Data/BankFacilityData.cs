using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankFacilityData : DataSet
	{
		public const string BANKFACILITY_TABLE = "Bank_Facility";

		public const string BANKFACILITYID_FIELD = "FacilityID";

		public const string BANKFACILITYNAME_FIELD = "FacilityName";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string FACILITYGROUP_FIELD = "GroupID";

		public const string FACILITYTYPE_FIELD = "FacilityType";

		public const string ARACCOUNT_FIELD = "PayableAccountID";

		public const string CURRENTACCOUNT_FIELD = "CurrentAccountID";

		public const string BANKCHARGEACCOUNTID_FIELD = "BankChargeAccountID";

		public const string BANKINTERESTACCOUNTID_FIELD = "BankInterestAccountID";

		public const string STATUS_FIELD = "Status";

		public const string ALIASNAME_FIELD = "Alias";

		public const string AMOUNTLIMIT_FIELD = "LimitAmount";

		public const string PRINTTEMPLATENAME_FIELD = "PrintTemplateName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BankFacilityTable => base.Tables["Bank_Facility"];

		public BankFacilityData()
		{
			BuildDataTables();
		}

		public BankFacilityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bank_Facility");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("FacilityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FacilityName", typeof(string)).AllowDBNull = false;
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("GroupID", typeof(string));
			columns.Add("FacilityType", typeof(byte));
			columns.Add("PayableAccountID", typeof(string));
			columns.Add("CurrentAccountID", typeof(string));
			columns.Add("BankChargeAccountID", typeof(string));
			columns.Add("BankInterestAccountID", typeof(string));
			columns.Add("Alias", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("LimitAmount", typeof(decimal));
			columns.Add();
			columns.Add("PrintTemplateName", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
