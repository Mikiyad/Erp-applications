using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class InsuranceProviderData : DataSet
	{
		public const string INSURANCEPROVIDERID_FIELD = "InsuranceProviderID";

		public const string INSURANCEPROVIDERNAME_FIELD = "InsuranceProviderName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string ISMEDICAL_FIELD = "IsMedical";

		public const string DESCRIPTION_FIELD = "Description";

		public const string INSURANCEPROVIDER_TABLE = "Insurance_Provider";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable InsuranceProviderTable => base.Tables["Insurance_Provider"];

		public InsuranceProviderData()
		{
			BuildDataTables();
		}

		public InsuranceProviderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Insurance_Provider");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("InsuranceProviderID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("InsuranceProviderName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsMedical", typeof(bool)).DefaultValue = false;
			columns.Add("Description", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
