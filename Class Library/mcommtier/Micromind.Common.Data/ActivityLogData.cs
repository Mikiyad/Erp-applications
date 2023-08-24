using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ActivityLogData : DataSet
	{
		public const string ACTIVITYLOGS_TABLE = "[Activity Logs]";

		public const string ACTIVITYLOGID_FIELD = "ActivityLogID";

		public const string USERID_FIELD = "UserID";

		public const string ENTITYID_FIELD = "EntityID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string MACHINEID_FIELD = "MachineID";

		public const string LOGDATE_FIELD = "LogDate";

		public const string AMOUNT_FIELD = "Amount";

		public const string PAYEE_FIELD = "Payee";

		public const string ACTIVITYTYPE_FIELD = "ActivityType";

		public const string REFERENCEID_FIELD = "ReferenceID";

		public const string TRANSACTIONTYPE_FIELD = "TransactionType";

		public const string DATACOMBOTYPE_FIELD = "DataComboType";

		public const string DESCRIPTION_FIELD = "Description";

		public const string COGSUPDATELOG_TABLE = "COGS_Update_LOG";

		public const string BATCHREFERENCE_FIELD = "BatchReference";

		public const string BATCHDATE_FIELD = "BatchDate";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string OLDCOST_FIELD = "OldCost";

		public const string NEWCOST_FIELD = "NewCost";

		public const string OLDCOGS_FIELD = "OldCOGS";

		public const string NEWCOGS_FIELD = "NewCOGS";

		public const string TOTALDIFF_FIELD = "TotalDiff";

		public const string COGSACCOUNTID_FIELD = "COGSAccountID";

		public const string ASSETACCOUNTID_FIELD = "AssetAccountID";

		public const string LOGID_FIELD = "LogID";

		public const string DOCVERSION_TABLE = "Doc_Version";

		public const string VERSIONID_FIELD = "VersionID";

		public const string SCREENTYPE_FIELD = "ScreenType";

		public const string SCREENID_FIELD = "ScreenID";

		public const string DOCDATA_FIELD = "DocData";

		public const string DOCNUMBER_FIELD = "DocNumber";

		public DataTable ActivityLogTable => base.Tables["[Activity Logs]"];

		public DataTable DocVersionTable => base.Tables["Doc_Version"];

		public ActivityLogData()
		{
			BuildDataTables();
		}

		public ActivityLogData(bool isCOGSLog, bool isDocVersion)
		{
			if (isCOGSLog)
			{
				BuildCOGSUpdateLogDataTables();
			}
			else if (isDocVersion)
			{
				BuildDocVersionDataTables();
			}
		}

		public ActivityLogData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public void AddCOGSUpdateLogRow(string reference, DateTime batchDate, string sysDocID, string voucherID, string productID, int rowIndex, decimal oldCost, decimal newCost, decimal oldCOGS, decimal newCOGS, decimal totalDiff, string cogsAccountID, string assetAccountID)
		{
			DataRow dataRow = base.Tables["COGS_Update_LOG"].NewRow();
			dataRow["BatchReference"] = reference;
			dataRow["BatchDate"] = batchDate;
			dataRow["SysDocID"] = sysDocID;
			dataRow["VoucherID"] = voucherID;
			dataRow["ProductID"] = productID;
			dataRow["RowIndex"] = rowIndex;
			dataRow["OldCost"] = oldCost;
			dataRow["NewCost"] = newCost;
			dataRow["OldCOGS"] = oldCOGS;
			dataRow["NewCOGS"] = newCOGS;
			dataRow["TotalDiff"] = totalDiff;
			dataRow["COGSAccountID"] = cogsAccountID;
			dataRow["AssetAccountID"] = assetAccountID;
			dataRow.EndEdit();
			base.Tables["COGS_Update_LOG"].Rows.Add(dataRow);
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Activity Logs]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ActivityLogID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("UserID", typeof(string));
			columns.Add("MachineID", typeof(string));
			columns.Add("EntityID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("LogDate", typeof(DateTime));
			columns.Add("ActivityType", typeof(byte));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Payee", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("ReferenceID", typeof(int));
			columns.Add("TransactionType", typeof(byte));
			columns.Add("DataComboType", typeof(byte));
			base.Tables.Add(dataTable);
		}

		private void BuildDocVersionDataTables()
		{
			DataTable dataTable = new DataTable("Doc_Version");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VersionID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("ScreenType", typeof(byte));
			columns.Add("ScreenID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("DocNumber", typeof(string));
			columns.Add("DocData", typeof(byte[]));
			columns.Add("UserID", typeof(string));
			columns.Add("MachineID", typeof(string));
			columns.Add("LogDate", typeof(DateTime));
			base.Tables.Add(dataTable);
		}

		private void BuildCOGSUpdateLogDataTables()
		{
			DataTable dataTable = new DataTable("COGS_Update_LOG");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LogID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("BatchReference", typeof(string));
			columns.Add("BatchDate", typeof(DateTime));
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("OldCost", typeof(decimal));
			columns.Add("NewCost", typeof(decimal));
			columns.Add("OldCOGS", typeof(decimal));
			columns.Add("NewCOGS", typeof(decimal));
			columns.Add("TotalDiff", typeof(decimal));
			columns.Add("COGSAccountID", typeof(string));
			columns.Add("AssetAccountID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
