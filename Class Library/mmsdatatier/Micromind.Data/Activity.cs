using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Activity : StoreObject
	{
		public const string CRMACTIVITY_TABLE = "Activity";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CRMACTIVITYNAME_PARM = "@ActivityName";

		private const string CRMACTIVITYTYPE_PARM = "@ActivityType";

		private const string REASONID_PARM = "@ReasonID";

		private const string RELATEDID_PARM = "@RelatedID";

		private const string RELATEDTYPE_PARM = "@RelatedType";

		private const string CONTACTID_PARM = "@ContactID";

		private const string OWNERID_PARM = "@OwnerID";

		private const string DATETIME_PARM = "@ActivityDateTime";

		public const string NOTE_PARM = "@Note";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Activity(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Activity", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("ReasonID", "@ReasonID"), new FieldValue("ActivityName", "@ActivityName"), new FieldValue("ActivityType", "@ActivityType"), new FieldValue("ActivityDateTime", "@ActivityDateTime"), new FieldValue("RelatedID", "@RelatedID"), new FieldValue("RelatedType", "@RelatedType"), new FieldValue("ContactID", "@ContactID"), new FieldValue("OwnerID", "@OwnerID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Activity", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ReasonID", SqlDbType.NVarChar);
			parameters.Add("@ActivityName", SqlDbType.NVarChar);
			parameters.Add("@ActivityType", SqlDbType.TinyInt);
			parameters.Add("@RelatedID", SqlDbType.NVarChar);
			parameters.Add("@RelatedType", SqlDbType.TinyInt);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@OwnerID", SqlDbType.NVarChar);
			parameters.Add("@ActivityDateTime", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ReasonID"].SourceColumn = "ReasonID";
			parameters["@ActivityName"].SourceColumn = "ActivityName";
			parameters["@ActivityType"].SourceColumn = "ActivityType";
			parameters["@RelatedID"].SourceColumn = "RelatedID";
			parameters["@RelatedType"].SourceColumn = "RelatedType";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@OwnerID"].SourceColumn = "OwnerID";
			parameters["@ActivityDateTime"].SourceColumn = "ActivityDateTime";
			parameters["@Note"].SourceColumn = "Note";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateCRMActivity(CRMActivityData crmactivityData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(crmactivityData, "Activity", insertUpdateCommand) : Update(crmactivityData, "Activity", insertUpdateCommand));
				string text = crmactivityData.CRMActivityTable.Rows[0]["SysDocID"].ToString();
				string text2 = crmactivityData.CRMActivityTable.Rows[0]["VoucherID"].ToString();
				if (isUpdate)
				{
					AddActivityLog("CRM Activity", text2, text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("CRM Activity", text2, text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Activity", "SysDocID", text, "VoucherID", text2, sqlTransaction, !isUpdate);
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(text, text2, "Activity", "VoucherID", sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public CRMActivityData GetCRMActivity()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Activity");
			CRMActivityData cRMActivityData = new CRMActivityData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(cRMActivityData, "Activity", sqlBuilder);
			return cRMActivityData;
		}

		public bool DeleteCRMActivity(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Activity WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CRMActivity", voucherID, sysDocID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public CRMActivityData GetCRMActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				CRMActivityData cRMActivityData = new CRMActivityData();
				string textCommand = "SELECT * FROM Activity  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(cRMActivityData, "Activity", textCommand);
				return cRMActivityData;
			}
			catch
			{
				throw;
			}
		}

		public CRMActivityData GetCustomerActivityByID(string sysDocID, string voucherID)
		{
			try
			{
				CRMActivityData cRMActivityData = new CRMActivityData();
				string textCommand = "SELECT * FROM Activity  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' AND RelatedType = ' " + 2 + "'";
				FillDataSet(cRMActivityData, "Activity", textCommand);
				return cRMActivityData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCRMActivityListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			return GetCRMActivityList(leadType, leadID, from, to);
		}

		public DataSet GetCRMActivityList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               ActivityName [Activity Name], \r\n                        (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead' \tWHEN 2 THEN 'Customer' WHEN 3 THEN 'Opportunity'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName WHEN 3 THEN OP.OpportunityName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],Con.FirstName + ' ' + Con.LastName AS Contact,\r\n                                CASE ActivityType WHEN 0 THEN 'Call' \r\n                                WHEN 1 THEN 'Email' WHEN 2 THEN 'Task'  WHEN 3 THEN 'Fax' \r\n                                WHEN 4 THEN 'Letter' WHEN 5 THEN 'Appointment' WHEN 6 THEN 'Compaign' \r\n                                WHEN 7 THEN 'Service' WHEN 1000 THEN 'Custom' END AS [Activity Type],ACT.OwnerID [Performed By],  ActivityDateTime [Date], ACT.Note\r\n                           FROM Activity ACT LEFT OUTER JOIN Lead ON Lead.LeadID = Act.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Act.RelatedID\r\n                            LEFT OUTER JOIN Opportunity OP ON OP.OpportunityID = Act.RelatedID\r\n                            LEFT OUTER JOIN Contact CON ON CON.ContactID = ACT.ContactID\r\n                                --Checking user access right for the doc id\r\n                                WHERE (ACT.SysDocID NOT IN (SELECT SysDocID FROM System_Doc_Entity_Link)\r\n\t\t\t\t\t\t\t\tOR Act.SysDocID IN (SELECT SysDocID FROM System_Doc_Entity_Link SDL WHERE SDL.EntityType = 5 AND SDL.EntityID = '" + base.DBConfig.UserID + "'))";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActivityDateTime Between '" + text + "' AND '" + text2 + "' ";
			}
			if (leadID != "")
			{
				text3 = text3 + " AND Act.RelatedType = '" + (int)leadType + "' AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Activity", text3);
			return dataSet;
		}

		public DataSet GetCustomerActivityList(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT ACT.SysDocID [Doc ID], ACT.VoucherID [Number],\r\n                               ActivityName [Activity Name], \r\n                        (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN 'Lead' \tWHEN 2 THEN 'Customer' WHEN 3 THEN 'Opportunity'\r\n\t\t\t\t\t\tELSE '' END) AS [Account Type], (CASE ACT.RelatedType\r\n\t\t\t\t\t\tWHEN 1 THEN Lead.LeadName\r\n\t\t\t\t\t\tWHEN 2 THEN CUS.CustomerName WHEN 3 THEN OP.OpportunityName\r\n\t\t\t\t\t\tELSE '' END) AS [Account Name],Con.FirstName + ' ' + Con.LastName AS Contact,\r\n                                CASE ActivityType WHEN 0 THEN 'Call' \r\n                                WHEN 1 THEN 'Email' WHEN 2 THEN 'Task'  WHEN 3 THEN 'Fax' \r\n                                WHEN 4 THEN 'Letter' WHEN 5 THEN 'Appointment' WHEN 6 THEN 'Compaign' \r\n                                WHEN 7 THEN 'Service' WHEN 1000 THEN 'Custom' END AS [Activity Type],ACT.OwnerID [Performed By],  ActivityDateTime [Date], ACT.Note\r\n                           FROM Activity ACT LEFT OUTER JOIN Lead ON Lead.LeadID = Act.RelatedID\r\n                            LEFT OUTER JOIN Customer CUS ON CUS.CustomerID = Act.RelatedID\r\n                            LEFT OUTER JOIN Opportunity OP ON OP.OpportunityID = Act.RelatedID\r\n                            LEFT OUTER JOIN Contact CON ON CON.ContactID = ACT.ContactID\r\n                                --Checking user access right for the doc id\r\n                                WHERE (ACT.SysDocID NOT IN (SELECT SysDocID FROM System_Doc_Entity_Link)\r\n\t\t\t\t\t\t\t\tOR Act.SysDocID IN (SELECT SysDocID FROM System_Doc_Entity_Link SDL WHERE SDL.EntityType = 5 AND SDL.EntityID = '" + base.DBConfig.UserID + "'))";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND ActivityDateTime Between '" + text + "' AND '" + text2 + "'AND Act.RelatedType = '" + (int)leadType + "'";
			}
			if (leadID != "")
			{
				text3 = text3 + "AND Act.RelatedID = '" + leadID + "'";
			}
			FillDataSet(dataSet, "Activity", text3);
			return dataSet;
		}

		public DataSet GetCRMActivityComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ActivityID [Code], ActivityName [Name]\r\n                           FROM Activity ORDER BY ActivityID, ActivityName";
			FillDataSet(dataSet, "Activity", textCommand);
			return dataSet;
		}
	}
}
