using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class FixedAssetData : DataSet
	{
		public const string ASSET_TABLE = "FixedAsset";

		public const string ASSETID_FIELD = "AssetID";

		public const string ASSETNAME_FIELD = "AssetName";

		public const string AQUESITIONDATE_FIELD = "AquesitionDate";

		public const string AQUESITIONCOST_FIELD = "AquesitionCost";

		public const string ASSETCLASSID_FIELD = "AssetClassID";

		public const string ASSETGROUPID_FIELD = "AssetGroupID";

		public const string LIFE_FIELD = "Life";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYDIVISIONID_FIELD = "CompanyDivisionID";

		public const string DEPARTMENTID_FIELD = "DepartmentID";

		public const string ASSETLOCATIONID_FIELD = "AssetLocationID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string ORIGINALVALUE_FIELD = "OriginalValue";

		public const string SALVAGEVALUE_FIELD = "SalvageValue";

		public const string INVOICENUMBER_FIELD = "InvoiceNumber";

		public const string PURCHASEDATE_FIELD = "PurchaseDate";

		public const string BOOKVALUE_FIELD = "BookValue";

		public const string SUPPLIERNAME_FIELD = "SupplierName";

		public const string PURCHASEREMARKS_FIELD = "PurchaseRemarks";

		public const string DEPMETHOD_FIELD = "DepMethod";

		public const string OPENINGDEPAMOUNT_FIELD = "OpeningDepAmount";

		public const string DEPAMOUNT_FIELD = "DepAmount";

		public const string DEPPERCENT_FIELD = "DepPercent";

		public const string DEPFREQUENCY_FIELD = "DepFrequency";

		public const string NEXTDEPDATE_FIELD = "NextDepDate";

		public const string DEPSTARTDATE_FIELD = "DepStartDate";

		public const string ACCUMDEP_FIELD = "AccumDep";

		public const string LASTDEPAMOUNT_FIELD = "LastDepAmount";

		public const string LASTDEPDATE_FIELD = "LastDepDate";

		public const string SERIALNUMBER_FIELD = "SerialNumber";

		public const string BARCODENUMBER_FIELD = "BarcodeNumber";

		public const string MODELNUMBER_FIELD = "ModelNumber";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string FIXEDASSETDEPSCHEDULE_TABLE = "FixedAsset_Dep_Schedule";

		public const string SCHEDULEDATE_FIELD = "ScheduleDate";

		public const string ISRECORDED_FIELD = "IsRecorded";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERNUMBER_FIELD = "VoucherNumber";

		public const string PERIODFROM_FIELD = "PeriodFrom";

		public const string PERIODTO_FIELD = "PeriodTo";

		public const string MONTH_FIELD = "Month";

		public const string YEAR_FIELD = "Year";

		public DataTable AssetTable => base.Tables["FixedAsset"];

		public DataTable FixedAssetDepSchedule => base.Tables["FixedAsset_Dep_Schedule"];

		public FixedAssetData()
		{
			BuildDataTables();
		}

		public FixedAssetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("FixedAsset");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AssetID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AssetName", typeof(string)).AllowDBNull = false;
			columns.Add("AquesitionDate", typeof(DateTime));
			columns.Add("AssetClassID", typeof(string));
			columns.Add("AssetGroupID", typeof(string));
			columns.Add("Life", typeof(short));
			columns.Add("AquesitionCost", typeof(decimal));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyDivisionID", typeof(string));
			columns.Add("DepartmentID", typeof(string));
			columns.Add("AssetLocationID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("BookValue", typeof(decimal));
			columns.Add("OriginalValue", typeof(decimal));
			columns.Add("SalvageValue", typeof(decimal));
			columns.Add("InvoiceNumber", typeof(string));
			columns.Add("PurchaseDate", typeof(DateTime));
			columns.Add("DepStartDate", typeof(DateTime));
			columns.Add("SupplierName", typeof(string));
			columns.Add("PurchaseRemarks", typeof(string));
			columns.Add("DepMethod", typeof(byte));
			columns.Add("OpeningDepAmount", typeof(decimal));
			columns.Add("DepPercent", typeof(decimal));
			columns.Add("DepFrequency", typeof(byte));
			columns.Add("AccumDep", typeof(decimal));
			columns.Add("LastDepAmount", typeof(decimal));
			columns.Add("LastDepDate", typeof(DateTime));
			columns.Add("SerialNumber", typeof(string));
			columns.Add("BarcodeNumber", typeof(string));
			columns.Add("ModelNumber", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("FixedAsset_Dep_Schedule");
			columns = dataTable.Columns;
			columns.Add("DepAmount", typeof(decimal));
			columns.Add("AssetID", typeof(string));
			columns.Add("IsRecorded", typeof(bool));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ScheduleDate", typeof(DateTime));
			columns.Add("PeriodFrom", typeof(DateTime));
			columns.Add("PeriodTo", typeof(DateTime));
			columns.Add("Month", typeof(byte));
			columns.Add("Year", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
