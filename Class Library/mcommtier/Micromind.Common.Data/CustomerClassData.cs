using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerClassData : DataSet
	{
		public const string CUSTOMERCLASS_TABLE = "Customer_Class";

		public const string CUSTOMERCLASSID_FIELD = "ClassID";

		public const string CUSTOMERCLASSNAME_FIELD = "ClassName";

		public const string ARACCOUNTID_FIELD = "ARAccountID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string HASPOSACCESS_FIELD = "HasPOSAccess";

		public const string ISLPO_FIELD = "IsLPO";

		public const string ISPRO_FIELD = "IsPRO";

		public const string NOTE_FIELD = "Note";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CustomerClassTable => base.Tables["Customer_Class"];

		public CustomerClassData()
		{
			BuildDataTables();
		}

		public CustomerClassData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Customer_Class");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ClassID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ClassName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("HasPOSAccess", typeof(bool));
			columns.Add("IsLPO", typeof(bool));
			columns.Add("IsPRO", typeof(bool));
			columns.Add("ARAccountID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
