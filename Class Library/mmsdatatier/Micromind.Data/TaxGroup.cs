using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class TaxGroup : StoreObject
	{
		private const string TAXGROUPCODE_PARM = "@TaxGroupCode";

		private const string TAXGROUPNAME_PARM = "@TaxGroupName";

		private const string TAXGROUP_TABLE = "Tax_Group";

		private const string NOTE_PARM = "@Note";

		private const string INACTIVE_PARM = "@Inactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string TAXGROUPDETAIL_TABLE = "Tax_Group_Detail";

		public const string TAXCODE_PARM = "@TaxCode";

		public const string DESCRIPTION_PARM = "@Description";

		public const string ROWINDEX_PARM = "@RowIndex";

		public TaxGroup(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tax_Group", new FieldValue("TaxGroupID", "@TaxGroupCode", isUpdateConditionField: true), new FieldValue("Note", "@Note"), new FieldValue("Inactive", "@Inactive"), new FieldValue("TaxGroupName", "@TaxGroupName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tax_Group", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@TaxGroupCode", SqlDbType.NVarChar);
			parameters.Add("@TaxGroupName", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@TaxGroupCode"].SourceColumn = "TaxGroupID";
			parameters["@TaxGroupName"].SourceColumn = "TaxGroupName";
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

		private string GetInsertUpdateTaxGroupDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Tax_Group_Detail", new FieldValue("TaxGroupID", "@TaxGroupCode"), new FieldValue("TaxCode", "@TaxCode"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Tax_Group_Detail", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateTaxDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateTaxGroupDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateTaxGroupDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@TaxGroupCode", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@TaxCode", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@TaxGroupCode"].SourceColumn = "TaxGroupID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@TaxCode"].SourceColumn = "TaxCode";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
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

		public bool InsertTaxGroup(TaxGroupData accountTaxGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Insert(accountTaxGroupData, "Tax_Group", insertUpdateCommand);
				string text = accountTaxGroupData.TaxGroupDetailTable.Rows[0]["TaxGroupID"].ToString();
				accountTaxGroupData.TaxGroupDetailTable.Rows[0]["TaxCode"].ToString();
				flag &= DeleteTaxGroupDetailsRows(text, sqlTransaction);
				foreach (DataRow row in accountTaxGroupData.TaxGroupDetailTable.Rows)
				{
					row["TaxGroupID"] = text;
				}
				if (flag)
				{
					insertUpdateCommand = GetInsertUpdateTaxDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					Insert(accountTaxGroupData, "Tax_Group_Detail", insertUpdateCommand);
				}
				string text2 = accountTaxGroupData.TaxTable.Rows[0]["TaxGroupID"].ToString();
				AddActivityLog("Tax Group", text2, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tax_Group", "TaxGroupID", text2, sqlTransaction, isInsert: true);
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

		public bool UpdateTaxGroup(TaxGroupData accountTaxGroupData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountTaxGroupData, "Tax_Group", insertUpdateCommand);
				string text = accountTaxGroupData.TaxGroupDetailTable.Rows[0]["TaxGroupID"].ToString();
				accountTaxGroupData.TaxGroupDetailTable.Rows[0]["TaxCode"].ToString();
				flag &= DeleteTaxGroupDetailsRows(text, sqlTransaction);
				foreach (DataRow row in accountTaxGroupData.TaxGroupDetailTable.Rows)
				{
					row["TaxGroupID"] = text;
				}
				if (flag)
				{
					insertUpdateCommand = GetInsertUpdateTaxDetailsCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					Insert(accountTaxGroupData, "Tax_Group_Detail", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = accountTaxGroupData.TaxTable.Rows[0]["TaxGroupID"];
				UpdateTableRowByID("Tax_Group", "TaxGroupID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountTaxGroupData.TaxTable.Rows[0]["TaxGroupID"].ToString();
				AddActivityLog("Tax", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Tax_Group", "TaxGroupID", obj, sqlTransaction, isInsert: false);
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

		public TaxGroupData GetTaxGroup()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tax_Group");
			TaxGroupData taxGroupData = new TaxGroupData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(taxGroupData, "Tax_Group", sqlBuilder);
			return taxGroupData;
		}

		public bool DeleteTaxGroup(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Tax_Group WHERE TaxGroupID = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Tax Group", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteTaxGroupDetailsRows(string GroupID, SqlTransaction sqlTransaction)
		{
			bool result = true;
			try
			{
				string exp = "select count(*) from  Tax_Group_Detail WHERE TaxGroupID = '" + GroupID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return result;
				}
				if (obj.ToString() != "")
				{
					exp = "DELETE FROM Tax_Group_Detail WHERE TaxGroupID = '" + GroupID + "'";
					return Delete(exp, sqlTransaction);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public TaxGroupData GetTaxGroupByID(string id)
		{
			try
			{
				TaxGroupData taxGroupData = new TaxGroupData();
				string text = "SELECT INV.* FROM Tax_Group INV  WHERE INV.TaxGroupID='" + id + "'";
				new SqlCommand(text);
				FillDataSet(taxGroupData, "Tax_Group", text);
				if (taxGroupData == null || taxGroupData.Tables.Count == 0 || taxGroupData.Tables["Tax_Group"].Rows.Count == 0)
				{
					return null;
				}
				text = "select * from Tax_Group_Detail TGD\r\n                        WHERE TGD.TaxGroupID='" + id + "' ORDER BY TGD.RowIndex ";
				FillDataSet(taxGroupData, "Tax_Group_Detail", text);
				return taxGroupData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaxByFields(params string[] columns)
		{
			return GetTaxByFields(null, isInactive: true, columns);
		}

		public DataSet GetTaxByFields(string[] degreeID, params string[] columns)
		{
			return GetTaxByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetTaxByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Tax_Group");
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
				commandHelper.FieldName = "TaxGroupID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Tax_Group";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Tax_Group", sqlBuilder);
			return dataSet;
		}

		public string GetExpenseAccountID(string expenseID, SqlTransaction sqlTransaction)
		{
			new DataSet();
			string exp = "SELECT AccountID  \r\n                           FROM Expense_Code WHERE ExpenseID = '" + expenseID + "'";
			return ExecuteScalar(exp, sqlTransaction).ToString();
		}

		public DataSet GetTaxGroupList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaxGroupID [Code],TaxGroupName [Name],ISNULL((SELECT SUM(TaxRate) FROM Tax T INNER JOIN Tax_Group_Detail TGD\r\n                            ON TGD.TaxCode = T.TaxCode WHERE TGD.TaxGroupID =  tg.TaxGroupID),0) AS TaxRate\r\n                            FROM Tax_Group TG ";
			FillDataSet(dataSet, "Tax_Group", textCommand);
			return dataSet;
		}

		public DataSet GetTaxClassList(string ID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT  TaxCode [TAX Code],\r\n                        TaxPercent [Tax Percent], RowIndex\r\n                         FROM Tax_ProductClass_Detail WHERE ClassId='" + ID + "' UNION select TaxID,  NUll, NULL from Tax where TaxID NOT IN (select TaxID from Tax_ProductClass_Detail WHERE ClassId='" + ID + "') ";
			FillDataSet(dataSet, "Tax_Group", textCommand);
			return dataSet;
		}

		public DataSet GetTaxGroupDetailList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TGD.TaxCode,TGD.TaxGroupID,TGD.RowIndex,T.TaxName AS TaxItemName,TaxType,ISNULL(CalculationMethod,1) AS CalculationMethod,TaxRate FROM Tax_Group_Detail TGD\r\n                                INNER JOIN Tax T ON T.TaxCode = TGD.TaxCode ";
			FillDataSet(dataSet, "Tax_Group_Detail", textCommand);
			return dataSet;
		}

		public DataSet GetTaxGroupComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TaxGroupID [Code],TaxGroupName [Name],ISNULL((SELECT SUM(TaxRate) FROM Tax T INNER JOIN Tax_Group_Detail TGD\r\n                            ON TGD.TaxCode = T.TaxCode WHERE TGD.TaxGroupID =  tg.TaxGroupID),0) AS TaxRate\r\n                            FROM Tax_Group TG";
			FillDataSet(dataSet, "Tax_Group", textCommand);
			return dataSet;
		}

		public DataSet GetTaxDatabasedonproductID(string id, string BasedonID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "select TGD.*,P.ProductID,T.TaxRate as TaxPercent from Tax_Group_Detail TGD INNER JOIN Product P ON P.TaxGroupID=TGD.TaxGroupID \r\n                        LEFT JOIN Tax T ON TGD.TaxCode=T.TaxCode                      \r\n                        WHERE TGD.TaxGroupID='" + id + "' AND  P.ProductID='" + BasedonID + "'  ORDER BY TGD.RowIndex ";
				FillDataSet(dataSet, "TaxProductData", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaxDatabasedonCustomerID(string id, string BasedonID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "select TGD.*,C.CustomerID,T.TaxRate as TaxPercent from Tax_Group_Detail TGD \r\n                        INNER JOIN CUSTOMER C ON C.TaxGroupID=TGD.TaxGroupID  LEFT JOIN Tax T ON TGD.TaxCode=T.TaxCode \r\n                        WHERE T.TaxCode IN ( SELECT TGD2.TaxCode FROM Tax_Group_Detail TGD2 WHERE TGD2.TaxGroupID='" + id + "') AND C.CustomerID='" + BasedonID + "'  ORDER BY TGD.RowIndex ";
				FillDataSet(dataSet, "TaxCustomerData", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaxDatabasedonvendorID(string id, string BasedonID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "select TGD.*,V.VendorID,T.TaxRate as TaxPercent from Tax_Group_Detail TGD LEFT JOIN Vendor V ON V.TaxGroupID=TGD.TaxGroupID LEFT JOIN Tax T ON TGD.TaxCode=T.TaxCode \r\n                        WHERE T.TaxCode IN ( SELECT TGD2.TaxCode FROM Tax_Group_Detail TGD2 WHERE TGD2.TaxGroupID='" + id + "') AND  V.VendorID='" + BasedonID + "' ORDER BY TGD.RowIndex ";
				FillDataSet(dataSet, "TaxVendorData", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetTaxDatabasedonGroupID(string id)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "select TGD.*,TaxRate as TaxPercent from Tax_Group_Detail TGD LEFT JOIN Tax T ON TGD.TaxCode=T.TaxCode\r\n                        WHERE TGD.TaxGroupID ='" + id + "'";
				FillDataSet(dataSet, "TaxGroupData", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
