using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class VendorClass : StoreObject
	{
		private const string VENDORCLASSID_PARM = "@ClassID";

		private const string VENDORCLASSNAME_PARM = "@ClassName";

		public const string NOTE_PARM = "@Note";

		private const string APACCOUNTID_PARM = "@APAccountID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		public const string TAXOPTION_PARM = "@TaxOption";

		public const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string VENDORCLASS_TABLE = "Vendor_Class";

		public VendorClass(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor_Class", new FieldValue("ClassID", "@ClassID", isUpdateConditionField: true), new FieldValue("ClassName", "@ClassName"), new FieldValue("APAccountID", "@APAccountID"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Vendor_Class", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@ClassID", SqlDbType.NVarChar);
			parameters.Add("@ClassName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@APAccountID", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters["@ClassID"].SourceColumn = "ClassID";
			parameters["@ClassName"].SourceColumn = "ClassName";
			parameters["@APAccountID"].SourceColumn = "APAccountID";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
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

		public bool InsertVendorClass(VendorClassData accountVendorClassData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountVendorClassData, "Vendor_Class", insertUpdateCommand);
				string text = accountVendorClassData.VendorClassTable.Rows[0]["ClassID"].ToString();
				AddActivityLog("Vendor Class", text, "", ActivityTypes.Add, "", null, SysDocTypes.None, DataComboType.VendorClass, null, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vendor_Class", "ClassID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateVendorClass(VendorClassData accountVendorClassData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountVendorClassData, "Vendor_Class", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountVendorClassData.VendorClassTable.Rows[0]["ClassID"];
				UpdateTableRowByID("Vendor_Class", "ClassID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				accountVendorClassData.VendorClassTable.Rows[0]["ClassName"].ToString();
				AddActivityLog("Vendor Class", obj.ToString(), "", ActivityTypes.Update, "", null, SysDocTypes.None, DataComboType.VendorClass, null, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Vendor_Class", "ClassID", obj, sqlTransaction, isInsert: false);
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

		public VendorClassData GetVendorClass()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor_Class");
			VendorClassData vendorClassData = new VendorClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorClassData, "Vendor_Class", sqlBuilder);
			return vendorClassData;
		}

		public bool DeleteVendorClass(string vendorClassID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vendor_Class WHERE ClassID = '" + vendorClassID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Vendor Class", vendorClassID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public VendorClassData GetVendorClassByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "ClassID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Vendor_Class";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			VendorClassData vendorClassData = new VendorClassData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(vendorClassData, "Vendor_Class", sqlBuilder);
			return vendorClassData;
		}

		public DataSet GetVendorClassByFields(params string[] columns)
		{
			return GetVendorClassByFields(null, isInactive: true, columns);
		}

		public DataSet GetVendorClassByFields(string[] vendorClassID, params string[] columns)
		{
			return GetVendorClassByFields(vendorClassID, isInactive: true, columns);
		}

		public DataSet GetVendorClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Vendor_Class");
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
				commandHelper.FieldName = "ClassID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Vendor_Class";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Vendor_Class", sqlBuilder);
			return dataSet;
		}

		public DataSet GetVendorClassList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Name],Note,IsInactive [Inactive]\r\n                           FROM Vendor_Class  ";
			FillDataSet(dataSet, "Vendor_Class", textCommand);
			return dataSet;
		}

		public DataSet GetVendorClassComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ClassID [Code],ClassName [Name]\r\n                           FROM Vendor_Class \r\n                            WHERE IsInactive<>1  ORDER BY ClassID,ClassName";
			FillDataSet(dataSet, "Vendor_Class", textCommand);
			return dataSet;
		}
	}
}
