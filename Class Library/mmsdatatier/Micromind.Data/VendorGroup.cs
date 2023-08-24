using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class VendorGroup : StoreObject
	{
		private const string VENDORGROUPID_PARM = "@GroupID";

		private const string VENDORGROUPNAME_PARM = "@GroupName";

		public const string NOTE_PARM = "@Note";

		public const string INACTIVE_PARM = "@Inactive";

		public const string VENDORGROUP_TABLE = "Vendor_Group";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public VendorGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor_Group", new FieldValue("GroupID", "@GroupID", isUpdateConditionField: true), new FieldValue("GroupName", "@GroupName"), new FieldValue("Inactive", "@Inactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vendor_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@GroupID", SqlDbType.NVarChar);
			parameters.Add("@GroupName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@GroupID"].SourceColumn = "GroupID";
			parameters["@GroupName"].SourceColumn = "GroupName";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		public bool InsertVendorGroup(VendorGroupData accountVendorGroupData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountVendorGroupData, "Vendor_Group", insertUpdateCommand);
				string text = accountVendorGroupData.VendorGroupTable.Rows[0]["GroupID"].ToString();
				AddActivityLog("Vendor Group", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vendor_Group", "GroupID", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateVendorGroup(VendorGroupData accountVendorGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountVendorGroupData, "Vendor_Group", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountVendorGroupData.VendorGroupTable.Rows[0]["GroupID"];
				UpdateTableRowByID("Vendor_Group", "GroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountVendorGroupData.VendorGroupTable.Rows[0]["GroupName"].ToString();
				AddActivityLog("Vendor Group", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vendor_Group", "GroupID", obj, sqlTransaction, isInsert: false);
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

		public VendorGroupData GetVendorGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor_Group");
			VendorGroupData vendorGroupData = new VendorGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorGroupData, "Vendor_Group", sqlBuilder);
			return vendorGroupData;
		}

		public bool DeleteVendorGroup(string vendorGroupID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vendor_Group WHERE GroupID = '" + vendorGroupID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vendor Group", vendorGroupID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VendorGroupData GetVendorGroupByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "GroupID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Vendor_Group";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VendorGroupData vendorGroupData = new VendorGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorGroupData, "Vendor_Group", sqlBuilder);
			return vendorGroupData;
		}

		public DataSet GetVendorGroupByFields(params string[] columns)
		{
			return GetVendorGroupByFields(null, isInactive: true, columns);
		}

		public DataSet GetVendorGroupByFields(string[] vendorGroupID, params string[] columns)
		{
			return GetVendorGroupByFields(vendorGroupID, isInactive: true, columns);
		}

		public DataSet GetVendorGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor_Group");
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
				commandHelper.TableName = "Vendor_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Vendor_Group", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVendorGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Group Code],GroupName [Group Name],Note,Inactive\r\n                           FROM Vendor_Group ";
			FillDataSet(dataSet, "Vendor_Group", textCommand);
			return dataSet;
		}

		public DataSet GetVendorGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT GroupID [Code],GroupName [Name]\r\n                           FROM Vendor_Group ORDER BY GroupID,GroupName";
			FillDataSet(dataSet, "Vendor_Group", textCommand);
			return dataSet;
		}
	}
}
