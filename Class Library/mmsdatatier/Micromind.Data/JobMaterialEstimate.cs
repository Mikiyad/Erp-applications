using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobMaterialEstimate : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string VARIANCE_PARM = "@Variance";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBMATERIALESTIMATE_TABLE = "Job_Material_Estimate";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string AMOUNT_PARM = "@Amount";

		private const string REQUIREDON_PARM = "@RequiredOn";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string JOBMATERIALESTIMATEDETAIL_TABLE = "Job_Material_Estimate_Detail";

		public JobMaterialEstimate(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobMaterialEstimateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Material_Estimate", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("JobID", "@JobID"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Material_Estimate", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaterialEstimateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaterialEstimateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaterialEstimateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
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

		private string GetInsertUpdateJobMaterialEstimateDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Material_Estimate_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Amount", "@Amount"), new FieldValue("RequiredOn", "@RequiredOn"), new FieldValue("UnitID", "@UnitID"), new FieldValue("Variance", "@Variance"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaterialEstimateDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaterialEstimateDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaterialEstimateDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Real);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@RequiredOn", SqlDbType.DateTime);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@Variance", SqlDbType.Real);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@RequiredOn"].SourceColumn = "RequiredOn";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@Variance"].SourceColumn = "Variance";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobMaterialEstimateData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobMaterialEstimate(JobMaterialEstimateData jobMaterialEstimateData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobMaterialEstimateCommand = GetInsertUpdateJobMaterialEstimateCommand(isUpdate);
			try
			{
				DataRow dataRow = jobMaterialEstimateData.JobMaterialEstimateTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Material_Estimate", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobMaterialEstimateData.JobMaterialEstimateDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobMaterialEstimateDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobMaterialEstimateCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobMaterialEstimateData, "Job_Material_Estimate", insertUpdateJobMaterialEstimateCommand)) : (flag & Insert(jobMaterialEstimateData, "Job_Material_Estimate", insertUpdateJobMaterialEstimateCommand)));
				insertUpdateJobMaterialEstimateCommand = GetInsertUpdateJobMaterialEstimateDetailsCommand(isUpdate: false);
				insertUpdateJobMaterialEstimateCommand.Transaction = sqlTransaction;
				if (jobMaterialEstimateData.JobMaterialEstimateDetailTable.Rows.Count > 0)
				{
					flag &= Insert(jobMaterialEstimateData, "Job_Material_Estimate_Detail", insertUpdateJobMaterialEstimateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Material_Estimate", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Material Estimate";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Material_Estimate", "VoucherID", sqlTransaction);
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

		private GLData CreateJobMaterialEstimateGLData(JobMaterialEstimateData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.JobMaterialEstimateTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.JobMaterialEstimate;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Material Estimate";
			dataRow2["Note"] = dataRow["Description"];
			dataRow2.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow2);
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Hashtable hashtable2 = new Hashtable();
			ArrayList arrayList2 = new ArrayList();
			decimal num = default(decimal);
			string text = "";
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.JobMaterialEstimateDetailTable.Rows)
			{
				decimal num2 = default(decimal);
				decimal num3 = decimal.Parse(row["Quantity"].ToString());
				decimal d2 = decimal.Parse(row["Cost"].ToString());
				string productID = row["ProductID"].ToString();
				string idFieldValue = row["JobID"].ToString();
				string voucherNumber = row["VoucherID"].ToString();
				string docID = row["SysDocID"].ToString();
				int rowIndex = int.Parse(row["RowIndex"].ToString());
				text = new Products(base.DBConfig).GetProductAccountIDByLocation(productID, locationID, Products.ProductAccounts.AssetAccount, sqlTransaction);
				string text2 = "";
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Job", "WIPAccountID", "JobID", idFieldValue, sqlTransaction);
				if (fieldValue != null)
				{
					text2 = fieldValue.ToString();
				}
				if (num3 > 0m)
				{
					num2 += num3 * d2;
				}
				else
				{
					num2 += -1m * Math.Abs(new Products(base.DBConfig).GetProductCOGS(productID, locationID, voucherNumber, docID, rowIndex, num3, sqlTransaction));
				}
				d += num2;
				if (hashtable.ContainsKey(text))
				{
					num = decimal.Parse(hashtable[text].ToString());
					num += Math.Round(num2, currencyDecimalPoints);
					hashtable[text] = num;
				}
				else
				{
					hashtable.Add(text, Math.Round(num2, currencyDecimalPoints));
					arrayList.Add(text);
				}
				if (hashtable2.ContainsKey(text2))
				{
					num = decimal.Parse(hashtable2[text2].ToString());
					num += Math.Round(num2, currencyDecimalPoints);
					hashtable2[text] = num;
				}
				else
				{
					hashtable2.Add(text2, Math.Round(num2, currencyDecimalPoints));
					arrayList2.Add(text2);
				}
			}
			d = Math.Round(d, currencyDecimalPoints);
			DataRow dataRow3 = gLData.JournalDetailsTable.NewRow();
			if (d != 0m)
			{
				for (int i = 0; i < hashtable.Count; i++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					text = arrayList[i].ToString();
					num = decimal.Parse(hashtable[text].ToString());
					dataRow3["Debit"] = DBNull.Value;
					dataRow3["Credit"] = Math.Abs(num);
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
				for (int j = 0; j < hashtable2.Count; j++)
				{
					dataRow3 = gLData.JournalDetailsTable.NewRow();
					dataRow3.BeginEdit();
					string text3 = arrayList2[j].ToString();
					num = decimal.Parse(hashtable2[text3].ToString());
					dataRow3["Debit"] = Math.Abs(num);
					dataRow3["Credit"] = DBNull.Value;
					dataRow3["JournalID"] = 0;
					dataRow3["AccountID"] = text3;
					dataRow3["Description"] = dataRow["Description"];
					dataRow3["Reference"] = dataRow["Reference"];
					dataRow3.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow3);
				}
			}
			return gLData;
		}

		public JobMaterialEstimateData GetJobMaterialEstimateByID(string sysDocID, string voucherID)
		{
			try
			{
				JobMaterialEstimateData jobMaterialEstimateData = new JobMaterialEstimateData();
				string textCommand = "SELECT * FROM Job_Material_Estimate WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialEstimateData, "Job_Material_Estimate", textCommand);
				if (jobMaterialEstimateData == null || jobMaterialEstimateData.Tables.Count == 0 || jobMaterialEstimateData.Tables["Job_Material_Estimate"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,P.ItemType, PL.Quantity AS 'OnHand', MR.LocationID\r\n                        FROM Job_Material_Estimate MR INNER JOIN \r\n                        Job_Material_Estimate_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                        LEFT OUTER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        LEFT OUTER JOIN Product_Location PL ON P.ProductID = PL.ProductID AND MR.LocationID = PL.LocationID \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialEstimateData, "Job_Material_Estimate_Detail", textCommand);
				return jobMaterialEstimateData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialEstimateAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date], Description, Reference, \r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location FROM Job_Material_Estimate JMR";
				FillDataSet(dataSet, "Job_Material_Estimate", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobMaterialEstimateDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Job_Material_Estimate_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobMaterialEstimate(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobMaterialEstimate(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobMaterialEstimateDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Material_Estimate WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Material Estimate", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobMaterialEstimateToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT * FROM Job_Material_Estimate WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Material_Estimate", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Material_Estimate"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT * FROM Job_Material_Estimate_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Material_Estimate_Detail", cmdText);
				dataSet.Relations.Add("JobMaterialDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Estimate"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Estimate"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Estimate_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Estimate_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialEstimateList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JobID,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Material_Estimate JI                          \r\n                            WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Material_Estimate", str);
			return dataSet;
		}

		public JobMaterialEstimateData GetJobMaterialEstimateByJobIDCostCategoryID(string sysDocID, string voucherID, string jobID, string costCategoryID)
		{
			try
			{
				JobMaterialEstimateData jobMaterialEstimateData = new JobMaterialEstimateData();
				string textCommand = "SELECT * FROM Job_Material_Estimate WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialEstimateData, "Job_Material_Estimate", textCommand);
				if (jobMaterialEstimateData == null || jobMaterialEstimateData.Tables.Count == 0 || jobMaterialEstimateData.Tables["Job_Material_Estimate"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,P.ItemType, PL.Quantity AS 'OnHand', MR.LocationID,MR.JobID\r\n                        FROM Job_Material_Estimate MR INNER JOIN \r\n                        Job_Material_Estimate_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                        LEFT OUTER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        LEFT OUTER JOIN Product_Location PL ON P.ProductID = PL.ProductID AND MR.LocationID = PL.LocationID \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' AND MR.JobID='" + jobID + "' AND TD.CostCategoryID='" + costCategoryID + "' ";
				FillDataSet(jobMaterialEstimateData, "Job_Material_Estimate_Detail", textCommand);
				return jobMaterialEstimateData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialEstimationList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = " SELECT DISTINCT JME.SysDocID[Doc ID],JME.VoucherID[Number],JME.JobID,J.JobName,J.CustomerID,C.CustomerName,\r\n\t\t\t\t                                JC.CostCategoryID,JC.CostCategoryName\r\n                                FROM Job_Material_Estimate JME\r\n                                INNER JOIN Job_Material_Estimate_Detail JED ON JME.VoucherID = JED.VoucherID\r\n                                INNER JOIN Job J ON J.JobID = JME.JobID\r\n                                INNER JOIN Job_Cost_Category JC ON JED.CostCategoryID = JC.CostCategoryID\r\n                                INNER JOIN Customer C ON C.CustomerID = J.CustomerID WHERE J.Status IN (0,1)\r\n                                ORDER BY JME.VoucherID ";
				FillDataSet(dataSet, "Job_Estimation", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public decimal GetJobMaterialEstimationQty(string jobID)
		{
			new JobMaterialEstimateData();
			string exp = "SELECT SUM(ISNULL(TD.Quantity,TD.UnitQuantity)* UnitPrice) AS Amount\r\n                        FROM Job_Material_Estimate MR INNER JOIN \r\n                        Job_Material_Estimate_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                        LEFT OUTER JOIN Product P ON P.ProductID = TD.ProductID\r\n                        LEFT OUTER JOIN Product_Location PL ON P.ProductID = PL.ProductID AND MR.LocationID = PL.LocationID\r\n                        WHERE MR.JobID='" + jobID + "' ";
			object obj = ExecuteScalar(exp);
			if (obj == null || obj.ToString() == "")
			{
				return 0m;
			}
			return decimal.Parse(obj.ToString());
		}
	}
}
