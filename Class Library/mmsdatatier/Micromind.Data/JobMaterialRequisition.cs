using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobMaterialRequisition : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE_PARM2 = "@Reference2";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REQUESTEDBY_PARM = "@RequestedBy";

		private const string REQONDDATE_PARM = "@ReqonDate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string JOBMATERIALREQUISITION_TABLE = "Job_Material_Requisition";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string COST_PARM = "@Cost";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string FACTOR_PARM = "@Factor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ONHAND_PARM = "@OnHand";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string ISSUED_PARM = "@Issued";

		private const string REMARKS_PARM = "@Remarks";

		private const string JOBMATERIALREQUISITIONDETAIL_TABLE = "Job_Material_Requisition_Detail";

		public JobMaterialRequisition(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobMaterialRequisitionText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Material_Requisition", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Description", "@Description"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ReqonDate", "@ReqonDate"), new FieldValue("RequestedBy", "@RequestedBy"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Material_Requisition", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaterialRequisitionCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaterialRequisitionText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaterialRequisitionText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@RequestedBy", SqlDbType.NVarChar);
			parameters.Add("@ReqonDate", SqlDbType.DateTime);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@RequestedBy"].SourceColumn = "RequestedBy";
			parameters["@ReqonDate"].SourceColumn = "ReqonDate";
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

		private string GetInsertUpdateJobMaterialRequisitionDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Material_Requisition_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Issued", "@Issued"), new FieldValue("Remarks", "@Remarks"), new FieldValue("OnHand", "@OnHand"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("UnitID", "@UnitID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobMaterialRequisitionDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobMaterialRequisitionDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobMaterialRequisitionDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@Factor", SqlDbType.Real);
			parameters.Add("@FactorType", SqlDbType.Char);
			parameters.Add("@Issued", SqlDbType.Real);
			parameters.Add("@OnHand", SqlDbType.Real);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@Factor"].SourceColumn = "Factor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@Issued"].SourceColumn = "Issued";
			parameters["@OnHand"].SourceColumn = "OnHand";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobMaterialRequisitionData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobMaterialRequisition(JobMaterialRequisitionData jobMaterialRequisitionData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateJobMaterialRequisitionCommand = GetInsertUpdateJobMaterialRequisitionCommand(isUpdate);
			try
			{
				DataRow dataRow = jobMaterialRequisitionData.JobMaterialRequisitionTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Material_Requisition", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobMaterialRequisitionData.JobMaterialRequisitionDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (isUpdate)
				{
					flag &= DeleteJobMaterialRequisitionDetailsRows(sysDocID, text, sqlTransaction);
				}
				insertUpdateJobMaterialRequisitionCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobMaterialRequisitionData, "Job_Material_Requisition", insertUpdateJobMaterialRequisitionCommand)) : (flag & Insert(jobMaterialRequisitionData, "Job_Material_Requisition", insertUpdateJobMaterialRequisitionCommand)));
				insertUpdateJobMaterialRequisitionCommand = GetInsertUpdateJobMaterialRequisitionDetailsCommand(isUpdate: false);
				insertUpdateJobMaterialRequisitionCommand.Transaction = sqlTransaction;
				if (jobMaterialRequisitionData.JobMaterialRequisitionDetailTable.Rows.Count > 0)
				{
					flag &= Insert(jobMaterialRequisitionData, "Job_Material_Requisition_Detail", insertUpdateJobMaterialRequisitionCommand);
				}
				if (flag)
				{
					flag &= UpdateTableRowInsertUpdateInfo("Job_Material_Requisition", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
					string entityName = "Job Material Requisition";
					flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
					if (!isUpdate)
					{
						flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Material_Requisition", "VoucherID", sqlTransaction);
					}
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobMaterialRequisition, sysDocID, text, "Job_Material_Requisition", sqlTransaction);
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

		private GLData CreateJobMaterialRequisitionGLData(JobMaterialRequisitionData transactionData, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.JobMaterialRequisitionTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			string locationID = dataRow["LocationID"].ToString();
			DataRow dataRow2 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.JobMaterialRequisition;
			dataRow2["JournalID"] = 0;
			dataRow2["JournalDate"] = dataRow["TransactionDate"];
			dataRow2["SysDocID"] = dataRow["SysDocID"];
			dataRow2["SysDocType"] = (byte)sysDocTypes;
			dataRow2["VoucherID"] = dataRow["VoucherID"];
			dataRow2["Reference"] = dataRow["Reference"];
			dataRow2["Narration"] = "Project Material Requisition";
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
			foreach (DataRow row in transactionData.JobMaterialRequisitionDetailTable.Rows)
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

		public JobMaterialRequisitionData GetJobMaterialRequisitionByID(string sysDocID, string voucherID)
		{
			try
			{
				JobMaterialRequisitionData jobMaterialRequisitionData = new JobMaterialRequisitionData();
				string textCommand = "SELECT * FROM Job_Material_Requisition WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialRequisitionData, "Job_Material_Requisition", textCommand);
				if (jobMaterialRequisitionData == null || jobMaterialRequisitionData.Tables.Count == 0 || jobMaterialRequisitionData.Tables["Job_Material_Requisition"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,CAST(ISNULL(Issued,0)AS real) as IssuedQty,P.ItemType AS ItemTypeVal,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,P.LastCost, PL.Quantity AS 'OnHand', MR.LocationID,\r\n                        (SELECT CAST(ISNULL(SUM(Quantity),0)AS real) from Inventory_Transactions IT WHERE IT.ProductID=TD.ProductID AND IT.locationid=MR.LocationID ) AS Stock,ISNull(P.TaxGroupID,PC.TaxGroupID) AS TaxGroupID,ISNull(P.TaxOption,PC.TaxOption) AS TaxOption\r\n                        FROM Job_Material_Requisition MR INNER JOIN \r\n                        Job_Material_Requisition_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                        LEFT JOIN Product P ON P.ProductID = TD.ProductID\r\n                        LEFT JOIN Product_Location PL ON P.ProductID = PL.ProductID AND MR.LocationID = PL.LocationID \r\n                        LEFT OUTER JOIN Product_Class PC ON PC.ClassID = P.ClassID\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY RowIndex";
				FillDataSet(jobMaterialRequisitionData, "Job_Material_Requisition_Detail", textCommand);
				textCommand = "SELECT * FROM Purchase_Order_Detail PR  INNER JOIN Job_Material_Requisition PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialRequisitionData, "Purchase_Order_Detail", textCommand);
				textCommand = "SELECT top 1 * FROM Job_Inventory_Issue_Detail PR  INNER JOIN Job_Material_Requisition PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialRequisitionData, "Job_Inventory_Issue_Detail", textCommand);
				textCommand = "SELECT Distinct  PR.SysDocID, PR.VoucherID FROM Purchase_Quote_Detail PR  INNER JOIN Job_Material_Requisition PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(jobMaterialRequisitionData, "Purchase_Quote_Detail", textCommand);
				return jobMaterialRequisitionData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetRequisitionList(string sysDocID, DateTime from, DateTime to)
		{
			CommonLib.ToSqlDateTimeString(from);
			CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date] FROM Job_Material_Requisition CON  ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + "  WHERE  SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Job_Material_Requisition", str);
			return dataSet;
		}

		public DataSet GetJobMaterialRequisitionAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT JMR.SysDocID [Doc ID],JMR.VoucherID [Number], TransactionDate AS [Date], JMR.Description, JMR.Reference, JMRD.jobID [Project Code],J.JobName[Project Name],\r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location\r\n\t\t\t\t\t\t\t\t FROM Job_Material_Requisition JMR LEFT JOIn Job_Material_Requisition_Detail JMRD ON JMR.SysDocID=JMRD.SysDocID and JMR.VoucherID=JMRD.VoucherID \r\n\t\t\t\t\t\t\t\t LEFT JOIN Job J ON JMRD.JobID=J.JobID WHERE JMR.Status='1'";
				FillDataSet(dataSet, "Job_Material_Requisition", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialRequisition(int mrFlag)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT DISTINCT JMR.SysDocID [Doc ID],JMR.VoucherID [Number], TransactionDate AS [Date], JMR.Description, JMR.Reference, JMRD.jobID [Project Code],J.JobName[Project Name],\r\n                                (SELECT LocationName FROM Location L WHERE L.LocationID =  JMR.LocationID) AS Location\r\n\t\t\t\t\t\t\t\t FROM Job_Material_Requisition JMR LEFT JOIn Job_Material_Requisition_Detail JMRD ON JMR.SysDocID=JMRD.SysDocID and JMR.VoucherID=JMRD.VoucherID \r\n\t\t\t\t\t\t\t\t LEFT JOIN Job J ON JMRD.JobID=J.JobID WHERE JMR.Status='1' and  MRFlag=" + mrFlag;
				FillDataSet(dataSet, "Job_Material_Requisition", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobMaterialRequisitionDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				string commandText = "DELETE FROM Job_Material_Requisition_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidJobMaterialRequisition(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeleteJobMaterialRequisition(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobMaterialRequisitionDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Job_Material_Requisition WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Job Material Requisition", voucherID, sysDocID, ActivityTypes.Delete, null);
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

		public DataSet GetJobMaterialRequisitionToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT JM.*,CASE JM.ApprovalStatus when 1 THEN 'PENDING' WHEN 2 THEN 'WAITING' WHEN 3 THEN 'REJECTED' WHEN 10 THEN 'APPROVED' ELSE 'NA' END AS [APPROVAL STATUS] ,\r\n                                (SELECT TOP 1 ApproverID FROM Approval_Task AT WHERE AT.DocumentSysDocID=SysDocID AND AT.DocumentCode=VoucherID ORDER BY AT.DateApproved DESC) AS [Approved By], (SELECT TOP 1 JobID FROM Job_Material_Requisition_Detail JMD WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID)  AS [JobID],\r\n                                (SELECT TOP 1 JobName FROM Job_Material_Requisition_Detail JMD INNER JOIN Job J ON JMD.JobID=J.JobID WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID )  AS [Job Name],\r\n                                (SELECT TOP 1 CustomerName FROM Job_Material_Requisition_Detail JMD INNER JOIN Job J ON JMD.JobID=J.JobID INNER JOIN Customer C ON C.CustomerID=J.CustomerID WHERE JMD.SysDocID=JM.SysDocID AND JMD.VoucherID=JM.VoucherID )  AS [CustomerID] FROM Job_Material_Requisition JM WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Material_Requisition", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Material_Requisition"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SysDocID, VoucherID, ProductID, JobID, CostCategoryID, Description, Quantity, UnitQuantity, UnitID, Factor,  FactorType, Cost, Amount, RowIndex + 1 RowIndex, IsBillable, IsBilled, BilledAmount, ItemType, \r\n                        SUM(Quantity * COST) AS TotalAmount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount, OnHand, Remarks FROM Job_Material_Requisition_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") \r\n                        GROUP BY SysDocID,VoucherID,JobID, CostCategoryID,ProductID,Description,UnitID,Quantity,Cost,Factor, Amount, UnitQuantity,FactorType,IsBilled, IsBillable, BilledAmount,RowIndex , ItemType, OnHand, Remarks\r\n                        ORDER BY RowIndex";
				FillDataSet(dataSet, "Job_Material_Requisition_Detail", cmdText);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowIssuedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,Issued FROM Job_Material_Requisition_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["Issued"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Job_Material_Requisition_Detail SET Issued=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseIssuedDoc(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Job_Material_Requisition_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Job_Material_Requisition_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(Issued,0))  FROM Job_Material_Requisition_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Job_Material_Requisition SET Status= '2' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, int status)
		{
			try
			{
				string exp = "UPDATE Job_Material_Requisition SET Status= " + status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobMaterialRequisitionList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT DISTINCT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID], JID.JobID,J.JobName, JI.LocationID, JI.RequestedBy,  JI.TransactionDate AS [Date]  ,JI.Reference,JI.Reference2,JI.ReqonDate,\r\n                        JI.Description,\r\n                        CASE WHEN JI.Status<>1 THEN 'Closed' ELSE  'Open' END AS [Status] \r\n                        FROM Job_Material_Requisition JI INNER JOIN Job_Material_Requisition_Detail JID ON JI.SysDocID=JID.SysDocID and JI.VoucherID=JID.VoucherID LEFT JOIN Job J ON JID.JobID=J.JobID\r\n                        WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Material_Requisition", str);
			return dataSet;
		}

		public DataSet AllowDelete(string sysDocID, string voucherNumber)
		{
			string textCommand = "SELECT * FROM Purchase_Order_Detail  WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "PODetails", textCommand);
			textCommand = "SELECT * FROM Job_Inventory_Issue_Detail  WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			FillDataSet(dataSet, "IssueDetails", textCommand);
			textCommand = "SELECT * FROM Purchase_Quote_Detail  WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			FillDataSet(dataSet, "PQDetails", textCommand);
			return dataSet;
		}

		internal bool UpdateRowOrderedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float num = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity FROM Job_Material_Requisition_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
				}
				num += quantity;
				textCommand = "UPDATE Job_Material_Requisition_Detail SET =" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetMateralRequisitionFlowReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromLocation, string toLocation, string toJob, string fromJob, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				CommonLib.ToSqlDateTimeString(from);
				CommonLib.ToSqlDateTimeString(to);
				DataSet dataSet = new DataSet();
				string text = "SELECT * FROM  Job_Material_Requisition JR WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				if (fromLocation != "")
				{
					text = text + " AND LocationID >= '" + fromLocation + "' ";
				}
				if (toLocation != "")
				{
					text = text + " AND LocationID <= '" + toLocation + "' ";
				}
				FillDataSet(dataSet, "Job_Material_Requisition", text);
				DataSet dataSet2 = new DataSet();
				text = " SELECT TD.SysDocID, TD.VoucherID, TD.ProductID, TD.JobID, CostCategoryID, TD.Description, TD.Quantity, UnitQuantity, TD.UnitID, Factor,  FactorType, Cost, Amount, RowIndex + 1 RowIndex, IsBillable, IsBilled, BilledAmount, TD.ItemType, \r\n                        SUM(TD.Quantity * COST) AS TotalAmount, SUM(ISNULL(UnitQuantity,0)* Cost) AS UnitAmount,CAST(ISNULL(Issued,0)AS real) as IssuedQty,P.ItemType AS ItemTypeVal,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,P.LastCost, PL.Quantity AS 'OnHand', MR.LocationID,JobName,\r\n                        (SELECT CAST(ISNULL(SUM(Quantity),0)AS real) from Inventory_Transactions IT WHERE IT.ProductID=TD.ProductID AND IT.locationid=MR.LocationID ) AS Stock\r\n                            \r\n                        FROM Job_Material_Requisition MR INNER JOIN \r\n                        Job_Material_Requisition_Detail TD ON MR.SysDocID = TD.SysDocID AND MR.VoucherID = TD.VoucherID\r\n                        LEFT JOIN Product P ON P.ProductID = TD.ProductID\r\n\t\t\t\t\t\tLEFT JOIN Job J ON TD.JobID=J.JobID\r\n                        LEFT JOIN Product_Location PL ON P.ProductID = PL.ProductID AND MR.LocationID = PL.LocationID\r\n                                    \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ";
				if (fromItem != "")
				{
					text = text + " AND TD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND TD.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND MR.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text = text + " AND TD.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				text += "  GROUP BY TD.SysDocID, TD.VoucherID,TD.JobID, CostCategoryID,TD.ProductID,TD.Description,TD.UnitID,TD.Quantity,Cost,Factor, Amount, UnitQuantity,FactorType,IsBilled, IsBillable, BilledAmount,RowIndex ,TD.ItemType,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,P.LastCost, PL.Quantity , MR.LocationID,JobName, TD.Issued, P.ItemType, MR.TransactionDate";
				text += " ORDER BY TransactionDate   ";
				FillDataSet(dataSet2, "Job_Material_Requisition_Detail", text);
				dataSet.Merge(dataSet2);
				dataSet.Relations.Add("Materail_Detail", new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Requisition"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Requisition"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Requisition_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Requisition_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				DataSet dataSet3 = new DataSet();
				text = "SELECT PR.SysDocID, PR.VoucherID, PR.Description, pr.JobID, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID,PD.BuyerID, PD.CostCategoryID, PD.CurrencyID, PD.VendorID, Pd.VendorID+ '- '+VendorName AS Vendor, JobName                           \r\n                        FROM Purchase_Order_Detail PR \r\n                         INNER JOIN Purchase_order PD ON PR.SysDocID=PD.SysDocID and PD.VoucherID=PR.VoucherID\r\n                         INNER JOIN Vendor ON Pd.VendorID=Vendor.VendorID\r\n\t\t\t\t\t\t LEFT JOIN Job J ON PR.JobID=J.JobID\r\n                         INNER JOIN Job_Material_Requisition PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n                                LEFT JOIN Product P ON P.ProductID = PR.ProductID                              \r\n\t\t\t\t\t\t  WHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				if (fromItem != "")
				{
					text = text + " AND PR.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromVendor != "")
				{
					text = text + " AND PD.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					text = text + " AND PD.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					text = text + " AND PD.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PD.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text = text + " AND PR.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				text += " group by  PR.VoucherID,PR.SysDocID, PR.VoucherID, PR.Description, pr.JobID, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID,PD.BuyerID, PD.CostCategoryID, PD.CurrencyID, PD.VendorID, Pd.VendorID, Vendor.VendorName, JobName";
				FillDataSet(dataSet3, "Material_PO", text);
				dataSet.Merge(dataSet3);
				dataSet.Relations.Add("MaterialPOREL", new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Requisition"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Requisition"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Material_PO"].Columns["SourceSysDocID"],
					dataSet.Tables["Material_PO"].Columns["SourceVoucherID"]
				}, createConstraints: false);
				DataSet dataSet4 = new DataSet();
				text = "SELECT   PR.SysDocID, PR.VoucherID, PR.Description, pr.JobID, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID, JobName                       \r\n                        FROM Job_Inventory_Issue_Detail PR  \r\n                        INNER JOIN Job_Material_Requisition PO ON \r\n                        PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n                        LEFT JOIN Job J on PR.JobID=j.JobID\r\n                            LEFT JOIN Product P ON P.ProductID = PR.ProductID                                  \r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				if (fromItem != "")
				{
					text = text + " AND PR.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PO.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				if (fromJob != "")
				{
					text = text + " AND PR.JobID BETWEEN '" + fromJob + "' AND '" + toJob + "' ";
				}
				text += " group by  PR.SysDocID, PR.VoucherID, PR.Description, pr.JobID, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID, JobName";
				FillDataSet(dataSet4, "Material_JobInventory", text);
				dataSet.Merge(dataSet4);
				dataSet.Relations.Add("MaterialJobInventoryREL", new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Requisition"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Requisition"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Material_JobInventory"].Columns["SourceSysDocID"],
					dataSet.Tables["Material_JobInventory"].Columns["SourceVoucherID"]
				}, createConstraints: false);
				DataSet dataSet5 = new DataSet();
				text = "SELECT  PR.SysDocID, PR.VoucherID, PR.Description, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID,PD.BuyerID, PD.CurrencyID, PD.VendorID, PD.VendorID+ '- '+VendorName AS Vendor                      \r\n                        FROM Purchase_Quote_Detail PR \r\n                        INNER JOIN Purchase_Quote PD ON PR.SysDocID=PD.SysDocID and PR.VoucherID=PD.VoucherID\r\n                         INNER JOIN Job_Material_Requisition PO ON PO.SysDocID=PR.SourceSysDocID AND PO.VoucherID=PR.SourceVoucherID\r\n                         INNER JOIN Vendor ON Pd.VendorID=Vendor.VendorID                                 \r\n\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				if (fromItem != "")
				{
					text = text + " AND PR.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromItemClass != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromItemClass + "' AND '" + toItemClass + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromItemCategory != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromItemCategory + "' AND '" + toItemCategory + "') ";
				}
				if (fromManufacturer != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					text = text + " AND PR.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromVendor != "")
				{
					text = text + " AND PD.VendorID BETWEEN '" + fromVendor + "' AND '" + toVendor + "' ";
				}
				if (fromVendorClass != "")
				{
					text = text + " AND PD.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorClassID BETWEEN '" + fromVendorClass + "' AND '" + toVendorClass + "') ";
				}
				if (fromVendorGroup != "")
				{
					text = text + " AND PD.VendorID IN (SELECT VendorID FROM Vendor WHERE VendorGroupID BETWEEN '" + fromVendorGroup + "' AND '" + toVendorGroup + "') ";
				}
				if (fromLocation != "")
				{
					text = text + " AND PO.LocationID BETWEEN '" + fromLocation + "' AND '" + toLocation + "' ";
				}
				text += " group by  PR.SysDocID, PR.VoucherID, PR.Description, PR.ProductID, pr.Quantity, pr.UnitQuantity, PR.RowIndex, PR.SourceSysDocID, PR.SourceRowIndex, PR.SourceSysDocID, PR.SourceVoucherID,PD.BuyerID, PD.CurrencyID, PD.VendorID, PD.VendorID, VendorName ";
				FillDataSet(dataSet5, "Material_Quote", text);
				dataSet.Merge(dataSet5);
				dataSet.Relations.Add("MaterialQuoteREL", new DataColumn[2]
				{
					dataSet.Tables["Job_Material_Requisition"].Columns["SysDocID"],
					dataSet.Tables["Job_Material_Requisition"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Material_Quote"].Columns["SourceSysDocID"],
					dataSet.Tables["Material_Quote"].Columns["SourceVoucherID"]
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
