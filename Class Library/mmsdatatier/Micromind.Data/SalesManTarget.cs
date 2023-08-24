using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class SalesManTarget : StoreObject
	{
		public const string SYSDOCID_PARM = "@SysDocID";

		public const string VOUCHERID_PARM = "@VoucherID";

		public const string FROMDATE_PARM = "@FromDate";

		public const string TODATE_PARM = "@ToDate";

		public const string MONTH_PARM = "@Month";

		public const string TARGETTYPE_PARM = "@TargetType";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public const string SALESMANGROUP_PARM = "@SalesManGroupID";

		public const string SALESMAN_PARM = "@SalesManID";

		public const string PRODUCTCLASS_PARM = "@ProductClassID";

		public const string PRODUCTCATEGORY_PARM = "@ProductCategoryID";

		public const string PRODUCTID_PARM = "@ProductID";

		public const string AMOUNT_PARM = "@Amount";

		public const string COMMISSIONPERCENT_PARM = "@CommissionPercent";

		public const string ROWINDEX_PARM = "@RowIndex";

		public const string SALESMANTARGET_TABLE = "Sales_Man_Target";

		public const string SALESMANTARGETDETAIL_TABLE = "Sales_Man_Target_Detail";

		public SalesManTarget(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesManTargetText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Man_Target", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("FromDate", "@FromDate"), new FieldValue("ToDate", "@ToDate"), new FieldValue("Month", "@Month"), new FieldValue("TargetType", "@TargetType"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Man_Target", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesManTargetCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesManTargetText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesManTargetText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@FromDate", SqlDbType.DateTime);
			parameters.Add("@ToDate", SqlDbType.DateTime);
			parameters.Add("@Month", SqlDbType.Int);
			parameters.Add("@TargetType", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@FromDate"].SourceColumn = "FromDate";
			parameters["@ToDate"].SourceColumn = "ToDate";
			parameters["@Month"].SourceColumn = "Month";
			parameters["@TargetType"].SourceColumn = "TargetType";
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

		private string GetInsertUpdateSalesManTargetDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Man_Target_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SalesManGroupID", "@SalesManGroupID"), new FieldValue("SalesManID", "@SalesManID"), new FieldValue("ProductClassID", "@ProductClassID"), new FieldValue("ProductCategoryID", "@ProductCategoryID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Amount", "@Amount"), new FieldValue("CommissionPercent", "@CommissionPercent"), new FieldValue("RowIndex", "@RowIndex"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesManTargetDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesManTargetDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesManTargetDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@SalesManGroupID", SqlDbType.NVarChar);
			parameters.Add("@SalesManID", SqlDbType.NVarChar);
			parameters.Add("@ProductClassID", SqlDbType.NVarChar);
			parameters.Add("@ProductCategoryID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@CommissionPercent", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SalesManGroupID"].SourceColumn = "SalesManGroupID";
			parameters["@SalesManID"].SourceColumn = "SalesManID";
			parameters["@ProductClassID"].SourceColumn = "ProductClassID";
			parameters["@ProductCategoryID"].SourceColumn = "ProductCategoryID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@CommissionPercent"].SourceColumn = "CommissionPercent";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesManTargetData salesmantargetData)
		{
			return true;
		}

		public bool InsertUpdateSalesManTarget(SalesManTargetData salesmanTargetData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateSalesManTargetCommand = GetInsertUpdateSalesManTargetCommand(isUpdate);
			try
			{
				DataRow dataRow = salesmanTargetData.SalesManTargetTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Man_Target", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in salesmanTargetData.SalesManTargetDetailTable.Rows)
				{
					row["VoucherID"] = dataRow["VoucherID"];
					row["SysDocID"] = dataRow["SysDocID"];
				}
				if (isUpdate)
				{
					flag &= DeleteSalesManTargetDetailsRows(text, sqlTransaction);
				}
				insertUpdateSalesManTargetCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesmanTargetData, "Sales_Man_Target", insertUpdateSalesManTargetCommand)) : (flag & Insert(salesmanTargetData, "Sales_Man_Target", insertUpdateSalesManTargetCommand)));
				insertUpdateSalesManTargetCommand = GetInsertUpdateSalesManTargetDetailsCommand(isUpdate: false);
				insertUpdateSalesManTargetCommand.Transaction = sqlTransaction;
				if (salesmanTargetData.SalesManTargetDetailTable.Rows.Count > 0)
				{
					flag &= Insert(salesmanTargetData, "Sales_Man_Target_Detail", insertUpdateSalesManTargetCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Man_Target", "SysDocID", text2, "VoucherID", text, sqlTransaction, !isUpdate);
				string entityName = "SalesManTarget";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Man_Target", "VoucherID", sqlTransaction);
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

		public SalesManTargetData GetSalesManTargetByID(string sysDocID, string voucherID)
		{
			try
			{
				SalesManTargetData salesManTargetData = new SalesManTargetData();
				string textCommand = "SELECT * FROM Sales_Man_Target WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(salesManTargetData, "Sales_Man_Target", textCommand);
				if (salesManTargetData == null || salesManTargetData.Tables.Count == 0 || salesManTargetData.Tables["Sales_Man_Target"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT *\r\n                        FROM Sales_Man_Target_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(salesManTargetData, "Sales_Man_Target_Detail", textCommand);
				return salesManTargetData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesManTargetDetailsRows(string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Sales_Man_Target_Detail WHERE VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSalesManTarget(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteSalesManTargetDetailsRows(voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Man_Target WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("SalesManTarget", voucherID, activityType, sqlTransaction);
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

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID,VoucherID,FromDate,ToDate,Month FROM Sales_Man_Target ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Sales_Man_Target", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalesManTargetToPrint(string sysDocID, string voucherID)
		{
			return GetSalesManTargetToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetSalesManTargetToPrint(string sysDocID, string[] voucherID)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT  SMT.* FROM Sales_Man_Target SMT  WHERE VoucherID IN (" + text + ") AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Sales_Man_Target", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Man_Target"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = " SELECT SMT.*,P.Description AS ProductDescription,PC.CategoryName AS ProductCategoryName,\r\n\t\t\t\t                PClass.ClassName AS ProductClassName,SP.FullName AS SalesPersonName,\r\n\t\t\t\t                    SPG.GroupName AS SalesPersonGroupName\r\n                                    FROM Sales_Man_Target_Detail SMT \r\n                                    LEFT JOIN Product P ON P.ProductID=SMT.ProductID \r\n                                    LEFT JOIN Product_Category PC ON PC.CategoryID = SMT.ProductCategoryID \r\n                                    LEFT JOIN Product_Class PClass ON PClass.ClassID = SMT.ProductClassID\r\n                                    LEFT JOIN Salesperson SP ON SP.SalespersonID = SMT.SalesManID\r\n                                    LEFT JOIN Salesperson_Group SPG ON SPG.GroupID = SMT.SalesManGroupID\r\n                                     WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                                              ORDER BY RowIndex";
				FillDataSet(dataSet, "Sales_Man_Target_Detail", textCommand);
				dataSet.Relations.Add("Sales_Man_Target_Detail", new DataColumn[2]
				{
					dataSet.Tables["Sales_Man_Target"].Columns["SysDocID"],
					dataSet.Tables["Sales_Man_Target"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Man_Target_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Man_Target_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
