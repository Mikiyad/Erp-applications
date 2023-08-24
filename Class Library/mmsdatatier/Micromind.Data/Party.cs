using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class Party : StoreObject
	{
		public const string PARTYID_PARM = "@PartyID";

		public const string PARTYNAME_PARM = "@PartyName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string ACCOUNTID_PARM = "@AccountID";

		public const string ENTITYTYPE_PARM = "@EntityType";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public Party(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Vendor_Link", new FieldValue("PartyID", "@PartyID", isUpdateConditionField: true), new FieldValue("PartyName", "@PartyName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Vendor_Link", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@PartyName", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@PartyName"].SourceColumn = "PartyName";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetInsertUpdateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Vendor_Link_Detail", new FieldValue("PartyID", "@PartyID"), new FieldValue("AccountID", "@AccountID"), new FieldValue("EntityType", "@EntityType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@PartyID", SqlDbType.NVarChar);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@EntityType", SqlDbType.Int);
			parameters["@PartyID"].SourceColumn = "PartyID";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@EntityType"].SourceColumn = "EntityType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateParty(PartyData partyData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = partyData.CustomerVendorLinkTable.Rows[0];
				sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["PartyID"].ToString();
				insertUpdateCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(partyData, "Customer_Vendor_Link", insertUpdateCommand)) : (flag & Insert(partyData, "Customer_Vendor_Link", insertUpdateCommand)));
				foreach (DataRow row in partyData.CustomerVendorLinkDetailTable.Rows)
				{
					row["PartyID"] = dataRow["PartyID"];
				}
				if (isUpdate)
				{
					flag &= DeleteCustomerVendorLinkDetailRows(text, sqlTransaction);
				}
				if (partyData.CustomerVendorLinkDetailTable.Rows.Count > 0)
				{
					insertUpdateCommand = GetInsertUpdateDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Insert(partyData, "Customer_Vendor_Link_Detail", insertUpdateCommand);
				}
				if (isUpdate)
				{
					AddActivityLog("Party", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("Party", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("Customer_Vendor_Link", "PartyID", text, sqlTransaction, !isUpdate);
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

		internal bool DeleteCustomerVendorLinkDetailRows(string partyCode, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Customer_Vendor_Link_Detail WHERE PartyID = '" + partyCode + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Party", partyCode, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PartyData GetParty()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Vendor_Link");
			PartyData partyData = new PartyData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(partyData, "Customer_Vendor_Link", sqlBuilder);
			return partyData;
		}

		public bool DeleteParty(string partyID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Customer_Vendor_Link WHERE PartyID = '" + partyID + "'";
				flag = Delete(commandText, trans);
				commandText = "DELETE FROM Customer_Vendor_Link_Detail WHERE PartyID = '" + partyID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Party", partyID, ActivityTypes.Delete, null);
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

		public PartyData GetPartyByID(string id)
		{
			PartyData partyData = new PartyData();
			try
			{
				string textCommand = "SELECT *\r\n                           FROM Customer_Vendor_Link WHERE PartyID='" + id + "'";
				FillDataSet(partyData, "Customer_Vendor_Link", textCommand);
				if (partyData == null || partyData.Tables.Count == 0 || partyData.Tables["Customer_Vendor_Link"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT CVL.*, CASE EntityType WHEN 1 THEN (SELECT CustomerName FROM Customer WHERE CustomerID = CVL.AccountID)\r\n                                    WHEN 2 THEN (SELECT VendorName FROM Vendor WHERE VendorID = CVL.AccountID) END AS AccountName\r\n                                FROM Customer_Vendor_Link_Detail CVL WHERE PartyID='" + id + "'";
				FillDataSet(partyData, "Customer_Vendor_Link_Detail", textCommand);
				return partyData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPartyByFields(params string[] columns)
		{
			return GetPartyByFields(null, isInactive: true, columns);
		}

		public DataSet GetPartyByFields(string[] areaID, params string[] columns)
		{
			return GetPartyByFields(areaID, isInactive: true, columns);
		}

		public DataSet GetPartyByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Customer_Vendor_Link");
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
				commandHelper.FieldName = "PartyID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Customer_Vendor_Link";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Customer_Vendor_Link", sqlBuilder);
			return dataSet;
		}

		public DataSet GetPartyList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PartyID [Party Code], PartyName [Party Name], Note, Inactive\r\n                           FROM Customer_Vendor_Link ";
			FillDataSet(dataSet, "Customer_Vendor_Link", textCommand);
			return dataSet;
		}

		public DataSet GetPartyComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT PartyID [Code],PartyName [Name]\r\n                           FROM Party ORDER BY PartyID,PartyName";
			FillDataSet(dataSet, "Customer_Vendor_Link", textCommand);
			return dataSet;
		}
	}
}
