using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class BOMs : StoreObject
	{
		private const string BOMID_PARM = "@BOMID";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string BOMNAME_PARM = "@BOMName";

		private const string AMOUNT_PARM = "@Amount";

		private const string PRICEPERCENT_PARM = "@PricePercent";

		private const string NOTE_PARM = "@Note";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string QUANTITY_PARM = "@Quantity";

		public BOMs(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("BOM", new FieldValue("BOMID", "@BOMID", isUpdateConditionField: true), new FieldValue("BOMName", "@BOMName"), new FieldValue("Amount", "@Amount"), new FieldValue("PricePercent", "@PricePercent"), new FieldValue("IsInactive", "@IsInactive"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("BOM", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@BOMID", SqlDbType.NVarChar);
			parameters.Add("@BOMName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PricePercent", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@BOMID"].SourceColumn = "BOMID";
			parameters["@BOMName"].SourceColumn = "BOMName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PricePercent"].SourceColumn = "PricePercent";
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

		private string GetInsertUpdateBOMDetailText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("BOM_Detail", new FieldValue("BOMID", "@BOMID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Cost", "@Cost"), new FieldValue("Quantity", "@Quantity"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateBOMDetailCommand(bool isUpdate)
		{
			SqlCommand sqlCommand = null;
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				sqlCommand = new SqlCommand(GetInsertUpdateBOMDetailText(isUpdate: true), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			else
			{
				sqlCommand = new SqlCommand(GetInsertUpdateBOMDetailText(isUpdate: false), base.DBConfig.Connection);
				sqlCommand.CommandType = CommandType.Text;
				parameters = sqlCommand.Parameters;
			}
			parameters.Add("@BOMID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters["@BOMID"].SourceColumn = "BOMID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Quantity"].SourceColumn = "Quantity";
			return sqlCommand;
		}

		public bool InsertUpdateBOM(BOMData accountBOMData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountBOMData, "BOM", insertUpdateCommand) : Update(accountBOMData, "BOM", insertUpdateCommand));
				string text = accountBOMData.BOMTable.Rows[0]["BOMID"].ToString();
				if (flag)
				{
					if (isUpdate)
					{
						DeleteBOMDetail(sqlTransaction, text.ToString());
					}
					insertUpdateCommand = GetInsertUpdateBOMDetailCommand(isUpdate: false);
					insertUpdateCommand.Transaction = sqlTransaction;
					if (accountBOMData.BOMDetailTable.Rows.Count > 0)
					{
						flag &= Insert(accountBOMData, "BOM_Detail", insertUpdateCommand);
					}
				}
				if (isUpdate)
				{
					AddActivityLog("BOM", text, ActivityTypes.Update, sqlTransaction);
				}
				else
				{
					AddActivityLog("BOM", text, ActivityTypes.Add, sqlTransaction);
				}
				UpdateTableRowInsertUpdateInfo("BOM", "BOMID", text, sqlTransaction, !isUpdate);
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

		private bool DeleteBOMDetail(SqlTransaction sqlTransaction, string bomID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM BOM_Detail WHERE BOMID = '" + bomID + "'";
				flag = Delete(commandText, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("BOM Detail", bomID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public BOMData GetBOM()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("BOM");
			BOMData bOMData = new BOMData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(bOMData, "BOM", sqlBuilder);
			return bOMData;
		}

		public bool DeleteBOM(string bomID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM BOM_Detail  WHERE BOMID = '" + bomID + "'";
				flag &= Delete(commandText, trans);
				commandText = "DELETE FROM BOM WHERE BOMID = '" + bomID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("BOM", bomID, ActivityTypes.Delete, null);
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

		public BOMData GetBOMByID(string id)
		{
			string textCommand = "SELECT * FROM BOM WHERE BOMID = '" + id + "'";
			BOMData bOMData = new BOMData();
			FillDataSet(bOMData, "BOM", textCommand);
			if (bOMData == null || bOMData.Tables.Count == 0 || bOMData.Tables[0].Rows.Count == 0)
			{
				return bOMData;
			}
			textCommand = "SELECT  BOMD.*,P.ItemType\r\n                        FROM BOM_Detail AS BOMD INNER JOIN BOM ON BOMD.BOMID = BOM.BOMID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = BOMD.ProductID\r\n                            WHERE BOMD.BOMID='" + id + "' ORDER BY RowIndex";
			FillDataSet(bOMData, "BOM_Detail", textCommand);
			return bOMData;
		}

		public BOMData GetBOMItemsByID(string id)
		{
			string textCommand = "SELECT * FROM BOM WHERE BOMID = '" + id + "'";
			BOMData bOMData = new BOMData();
			FillDataSet(bOMData, "BOM", textCommand);
			if (bOMData == null || bOMData.Tables.Count == 0 || bOMData.Tables[0].Rows.Count == 0)
			{
				return bOMData;
			}
			textCommand = "SELECT  BOMD.*,P.ItemType\r\n                        FROM BOM_Detail AS BOMD INNER JOIN BOM ON BOMD.BOMID = BOM.BOMID\r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = BOMD.ProductID\r\n                            WHERE BOMD.BOMID='" + id + "' ORDER BY RowIndex";
			FillDataSet(bOMData, "BOM_Detail", textCommand);
			return bOMData;
		}

		public DataSet GetBOMByFields(params string[] columns)
		{
			return GetBOMByFields(null, isInactive: true, columns);
		}

		public DataSet GetBOMByFields(string[] bomID, params string[] columns)
		{
			return GetBOMByFields(bomID, isInactive: true, columns);
		}

		public DataSet GetBOMByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("BOM");
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
				commandHelper.FieldName = "BOMID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "BOM";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.NVarChar;
				commandHelper2.TableName = "BOM";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "BOM", sqlBuilder);
			return dataSet;
		}

		public DataSet GetBOMList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT * FROM BOM  ";
			if (!showInactive)
			{
				text += " WHERE ISNULL(IsInactive,'False')='False'";
			}
			FillDataSet(dataSet, "BOM", text);
			return dataSet;
		}

		public DataSet GetBOMComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BOMID [Code],BOMName [Name]\r\n                            FROM BOM\r\n                            WHERE ISINACTIVE<>1 ORDER BY BOMID,BOMName";
			FillDataSet(dataSet, "BOM", textCommand);
			return dataSet;
		}

		public DataSet GetPackageComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT BOMID [Code],BOMName [Name],Amount [Amount]\r\n                            FROM BOM\r\n                            WHERE ISINACTIVE<>1 AND Amount<>0 ORDER BY BOMID,BOMName";
			FillDataSet(dataSet, "BOM", textCommand);
			return dataSet;
		}

		public DataSet GetPackageList(bool showInactive)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT * FROM BOM WHERE Amount<>0 ";
			if (!showInactive)
			{
				text += " AND ISNULL(IsInactive,'False')='False' ";
			}
			FillDataSet(dataSet, "BOM", text);
			return dataSet;
		}

		public BOMData GetPackageByID(string id)
		{
			string textCommand = "SELECT * FROM BOM WHERE BOMID = '" + id + "'";
			BOMData bOMData = new BOMData();
			FillDataSet(bOMData, "BOM", textCommand);
			if (bOMData == null || bOMData.Tables.Count == 0 || bOMData.Tables[0].Rows.Count == 0)
			{
				return bOMData;
			}
			textCommand = "SELECT  BOMD.*\r\n                        FROM BOM_Detail AS BOMD INNER JOIN BOM ON BOMD.BOMID = BOM.BOMID\r\n\t\t\t\t\t\tINNER JOIN Product_Category PC ON PC.CategoryID = BOMD.ProductID\r\n                            WHERE BOMD.BOMID='" + id + "' ORDER BY RowIndex";
			FillDataSet(bOMData, "BOM_Detail", textCommand);
			return bOMData;
		}
	}
}
