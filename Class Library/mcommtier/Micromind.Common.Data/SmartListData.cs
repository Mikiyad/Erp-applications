using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SmartListData : DataSet
	{
		public const string SMARTLIST_TABLE = "SmartList";

		public const string SMARTLISTCATEGORY_TABLE = "SmartList_Category";

		public const string SMARTLISTID_FIELD = "SmartListID";

		public const string SMARTLISTNAME_FIELD = "SmartListName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string QUERY_FIELD = "Query";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string REPORTDATA_FIELD = "ReportData";

		public const string DRILLACTION_FIELD = "DrillAction";

		public const string DRILLCARDTYPEID_FIELD = "DrillCardTypeID";

		public const string DRILLCARDIDFIELD_FIELD = "DrillCardIDField";

		public const string DRILLTRANSACTIONSYSDOCIDFIELD_FIELD = "DrillTransactionSysDocIDField";

		public const string DRILLTRANSACTIONVOUCHERIDFIELD_FIELD = "DrillTransactionVoucherIDField";

		public const string ISSUBREPORT_FIELD = "IsSubReport";

		public const string ISPREVIEW_FIELD = "IsPreview";

		public const string NOTE_FIELD = "Note";

		public const string DISPLAYNOTE_FIELD = "DisplayNote";

		public const string ISHIDEDATEFILTER_FIELD = "IsHideDateFilter";

		public const string ISSETDATEEQUALTO_FIELD = "IsSetDateEqualTo";

		public const string DRILLPARM1_FIELD = "DrillParm1";

		public const string DRILLPARM2_FIELD = "DrillParm2";

		public const string DRILLPARM3_FIELD = "DrillParm3";

		public const string DRILLPARM4_FIELD = "DrillParm4";

		public const string DRILLSUBREPORTID_FIELD = "DrillSubReportID";

		public const string CATEGORYNAME_FIELD = "CategoryName";

		public const string PARENTID_FIELD = "ParentID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable SmartListTable => base.Tables["SmartList"];

		public SmartListData()
		{
			BuildDataTables();
		}

		public SmartListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("SmartList");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SmartListID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SmartListName", typeof(string)).AllowDBNull = false;
			columns.Add("Description", typeof(string));
			columns.Add("Query", typeof(string));
			columns.Add("ReportData", typeof(byte[]));
			columns.Add("CategoryID", typeof(string));
			columns.Add("ParentID", typeof(string));
			columns.Add("DrillAction", typeof(byte));
			columns.Add("DrillCardTypeID", typeof(short));
			columns.Add("DrillCardIDField", typeof(string));
			columns.Add("DrillTransactionSysDocIDField", typeof(string));
			columns.Add("DrillTransactionVoucherIDField", typeof(string));
			columns.Add("IsPreview", typeof(bool));
			columns.Add("DrillSubReportID", typeof(int));
			columns.Add("DrillParm1", typeof(string));
			columns.Add("DrillParm2", typeof(string));
			columns.Add("DrillParm3", typeof(string));
			columns.Add("DrillParm4", typeof(string));
			columns.Add("IsSubReport", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("DisplayNote", typeof(string));
			columns.Add("IsHideDateFilter", typeof(bool));
			columns.Add("IsSetDateEqualTo", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
