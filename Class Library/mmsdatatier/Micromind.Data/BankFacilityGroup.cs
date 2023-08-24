using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BankFacilityGroup : StoreObject
	{
		private const string BANKFACILITYGROUPID_PARM = "@BankFacilityGroupID";

		private const string BANKFACILITYGROUPNAME_PARM = "@BankFacilityGroupName";

		public const string NOTE_PARM = "@Note";

		public const string BANKFACILITYGROUP_TABLE = "Bank_Facility_Group";

		public const string BANKID_PARM = "@BankID";

		public const string TOTALLIMIT_PARM = "@TotalLimit";

		public const string STARTDATE_PARM = "@StartDate";

		public const string ENDDATE_PARM = "@EndDate";

		public const string RENEWDATE_PARM = "@RenewDate";

		public const string EXPIRYDATE_PARM = "@ExpiryDate";

		public const string STATUS_PARM = "@Status";

		public const string AMOUNTLIMIT_PARM = "@LimitAmount";

		public const string CONTACTNAME_PARM = "@ContactName";

		public const string CONTACTNUMBER_PARM = "@ContactNumber";

		public const string CONTACTID_PARM = "@ContactID";

		public const string JOBTITLE_PARM = "@JobTitle";

		public const string ROWINDEX_PARM = "@RowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public BankFacilityGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Group", new FieldValue("GroupID", "@BankFacilityGroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@BankFacilityGroupName"), new FieldValue("BankID", "@BankID"), new FieldValue("TotalLimit", "@TotalLimit"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("RenewDate", "@RenewDate"), new FieldValue("ExpiryDate", "@ExpiryDate"), new FieldValue("Status", "@Status"), new FieldValue("ContactName", "@ContactName"), new FieldValue("ContactNumber", "@ContactNumber"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BankFacilityGroupID", SqlDbType.NVarChar);
			parameters.Add("@BankFacilityGroupName", SqlDbType.NVarChar);
			parameters.Add("@BankID", SqlDbType.NVarChar);
			parameters.Add("@TotalLimit", SqlDbType.Money);
			parameters.Add("@ContactName", SqlDbType.NVarChar);
			parameters.Add("@ContactNumber", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@RenewDate", SqlDbType.DateTime);
			parameters.Add("@ExpiryDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BankFacilityGroupID"].SourceColumn = "GroupID";
			parameters["@BankFacilityGroupName"].SourceColumn = "GroupName";
			parameters["@BankID"].SourceColumn = "BankID";
			parameters["@TotalLimit"].SourceColumn = "TotalLimit";
			parameters["@ContactName"].SourceColumn = "ContactName";
			parameters["@ContactNumber"].SourceColumn = "ContactNumber";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@RenewDate"].SourceColumn = "RenewDate";
			parameters["@ExpiryDate"].SourceColumn = "ExpiryDate";
			parameters["@Status"].SourceColumn = "Status";
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

		private string GetInsertUpdateBankFacilityGroupContactText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Bank_Facility_Group_Contacts", new FieldValue("GroupID", "@BankFacilityGroupID", isUpdateConditionField: true), new FieldValue("ContactID", "@ContactID", isUpdateConditionField: true), new FieldValue("JobTitle", "@JobTitle"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBankFacilityGroupContactCommand(bool isUpdate)
		{
			SqlCommand sqlCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetInsertUpdateBankFacilityGroupContactText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetInsertUpdateBankFacilityGroupContactText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@BankFacilityGroupID", SqlDbType.NVarChar);
			parameters.Add("@ContactID", SqlDbType.NVarChar);
			parameters.Add("@JobTitle", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BankFacilityGroupID"].SourceColumn = "GroupID";
			parameters["@ContactID"].SourceColumn = "ContactID";
			parameters["@JobTitle"].SourceColumn = "JobTitle";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Note"].SourceColumn = "Note";
			return sqlCommand;
		}

		public bool InsertBankFacilityGroup(BankFacilityGroupData accountBankFacilityGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountBankFacilityGroupData, "Bank_Facility_Group", insertUpdateCommand);
				if (accountBankFacilityGroupData.BankFacilityGroupContactTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateBankFacilityGroupContactCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountBankFacilityGroupData, "Bank_Facility_Group_Contacts", insertUpdateCommand);
				}
				string text = accountBankFacilityGroupData.BankFacilityGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("BankFacilityGroup", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank_Facility_Group", "GroupID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateBankFacilityGroup(BankFacilityGroupData accountBankFacilityGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountBankFacilityGroupData, "Bank_Facility_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountBankFacilityGroupData.BankFacilityGroupTable.Rows[0]["GroupID"];
				flag &= UpdateTableRowByID("Bank_Facility_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				flag &= DeleteBankFacilityGroupContacts(sqlTransaction, obj.ToString());
				if (accountBankFacilityGroupData.BankFacilityGroupContactTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateBankFacilityGroupContactCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(accountBankFacilityGroupData, "Bank_Facility_Group_Contacts", insertUpdateCommand);
				}
				string entiyID = accountBankFacilityGroupData.BankFacilityGroupTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("BankFacilityGroup", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Bank_Facility_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		private bool DeleteBankFacilityGroupContacts(SqlTransaction sqlTransaction, string groupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Bank_Facility_Group_Contacts WHERE GroupID = '" + groupID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("BankFacilityGroup", groupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BankFacilityGroupData GetBankFacilityGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bank_Facility_Group");
			BankFacilityGroupData bankFacilityGroupData = new BankFacilityGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankFacilityGroupData, "Bank_Facility_Group", sqlBuilder);
			return bankFacilityGroupData;
		}

		public bool DeleteBankFacilityGroup(string bankFacilityGroupID)
		{
			bool flag = true;
			using (SqlTransaction trans = base.DBConfig.StartNewTransaction())
			{
				try
				{
					string commandText = "DELETE FROM Bank_Facility_Group WHERE GroupID = '" + bankFacilityGroupID + "'";
					flag = Delete(commandText, trans);
					if (!flag)
					{
						return flag;
					}
					AddActivityLog("BankFacilityGroup", bankFacilityGroupID, ActivityTypes.Delete, null);
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
		}

		public BankFacilityGroupData GetBankFacilityGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Bank_Facility_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			BankFacilityGroupData bankFacilityGroupData = new BankFacilityGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bankFacilityGroupData, "Bank_Facility_Group", sqlBuilder);
			string textCommand = "SELECT GC.GroupID, GC.ContactID, GC.JobTitle, C.FirstName,C.LastName,GC.Note,C.Country,C.City,C.Phone1,C.Phone2,C.Email1\r\n                        FROM Bank_Facility_Group_Contacts AS GC INNER JOIN Contact C ON C.ContactID = GC.ContactID \r\n                            WHERE GroupID='" + id + "' ORDER BY RowIndex";
			FillDataSet(bankFacilityGroupData, "Bank_Facility_Group_Contacts", textCommand);
			return bankFacilityGroupData;
		}

		public DataSet GetBankFacilityGroupByFields(params string[] columns)
		{
			return GetBankFacilityGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetBankFacilityGroupByFields(string[] bankFacilityGroupID, params string[] columns)
		{
			return GetBankFacilityGroupByFields(bankFacilityGroupID, isInactive: true, columns);
		}

		public DataSet GetBankFacilityGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Bank_Facility_Group");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "GroupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Bank_Facility_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Bank_Facility_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBankFacilityGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Group Code], GroupName [Group Name], Note\r\n                           FROM Bank_Facility_Group ";
			FillDataSet(dataSet, "Bank_Facility_Group", textCommand);
			return dataSet;
		}

		public DataSet GetBankFacilityGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Bank_Facility_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Bank_Facility_Group", textCommand);
			return dataSet;
		}
	}
}
