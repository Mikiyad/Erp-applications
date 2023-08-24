using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class JobEstimation : StoreObject
	{
		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string MATERIALOHP_PARM = "@MaterialOHP";

		private const string LABOUROHP_PARM = "@LabourOHP";

		private const string OTHEROHP_PARM = "@OtherOHP";

		private const string JOBID_PARM = "@JobID";

		private const string NOTE_PARM = "@Note";

		public const string LABELC1_PARM = "@LabelC1";

		public const string LABELC2_PARM = "@LabelC2";

		public const string LABELC3_PARM = "@LabelC3";

		public const string LABELC4_PARM = "@LabelC4";

		public const string LABELC5_PARM = "@LabelC5";

		public const string LABELC6_PARM = "@LabelC6";

		private const string REFERENCE_PARM = "@Reference";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string LASTREVISEDDATE_PARM = "@LastRevisedDate";

		private const string JOB_ESTIMATION_TABLE = "Job_Estimation";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string PHASEID_PARM = "@PhaseID";

		private const string BOQID_PARM = "@BOQID";

		private const string BOQQUANTITY_PARM = "@BOQQuantity";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string COST_PARM = "@Cost";

		private const string ACTUALCOST_PARM = "@ActualCost";

		private const string LABOURCOST_PARM = "@LabourCost";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string MATERIALTOTAL_PARM = "@MaterialTotal";

		private const string LABOURTOTAL_PARM = "@LabourTotal";

		private const string MATERIALOH_PARM = "@MaterialOH";

		private const string LABOUROH_PARM = "@LabourOH";

		private const string OTHEROH_PARM = "@OtherOH";

		private const string NETTOTAL_PARM = "@NetTotal";

		private const string COSTMARKUP_PARM = "@CostMarkUp";

		private const string ATTRIBUTEC1_PARM = "@AttributeC1";

		private const string ATTRIBUTEC2_PARM = "@AttributeC2";

		private const string ATTRIBUTEC3_PARM = "@AttributeC3";

		private const string ATTRIBUTEC4_PARM = "@AttributeC4";

		private const string ATTRIBUTEC5_PARM = "@AttributeC5";

		private const string ATTRIBUTEC6_PARM = "@AttributeC6";

		private const string REMARKS_PARM = "@Remarks";

		private const string TOTAL_PARM = "@Total";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITCOST_PARM = "@UnitCost";

		private const string UNITLABOURCOST_PARM = "@UnitLabourCost";

		private const string ROWRELATION_PARM = "@RowRelation";

		private const string JOBESTIMATIONDETAIL_TABLE = "Job_Estimation_Detail";

		public JobEstimation(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateJobEstimationText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Estimation", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("MaterialOHP", "@MaterialOHP"), new FieldValue("LabourOHP", "@LabourOHP"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("OtherOHP", "@OtherOHP"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("LastRevisedDate", "@LastRevisedDate"), new FieldValue("LabelC1", "@LabelC1"), new FieldValue("LabelC2", "@LabelC2"), new FieldValue("LabelC3", "@LabelC3"), new FieldValue("LabelC4", "@LabelC4"), new FieldValue("LabelC5", "@LabelC5"), new FieldValue("LabelC6", "@LabelC6"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Job_Estimation", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobEstimationCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobEstimationText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobEstimationText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@MaterialOHP", SqlDbType.Decimal);
			parameters.Add("@LabourOHP", SqlDbType.Decimal);
			parameters.Add("@OtherOHP", SqlDbType.Decimal);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@LastRevisedDate", SqlDbType.DateTime);
			parameters.Add("@LabelC1", SqlDbType.NVarChar);
			parameters.Add("@LabelC2", SqlDbType.NVarChar);
			parameters.Add("@LabelC3", SqlDbType.NVarChar);
			parameters.Add("@LabelC4", SqlDbType.NVarChar);
			parameters.Add("@LabelC5", SqlDbType.NVarChar);
			parameters.Add("@LabelC6", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@MaterialOHP"].SourceColumn = "MaterialOHP";
			parameters["@LabourOHP"].SourceColumn = "LabourOHP";
			parameters["@OtherOHP"].SourceColumn = "OtherOHP";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@LastRevisedDate"].SourceColumn = "LastRevisedDate";
			parameters["@LabelC1"].SourceColumn = "LabelC1";
			parameters["@LabelC2"].SourceColumn = "LabelC2";
			parameters["@LabelC3"].SourceColumn = "LabelC3";
			parameters["@LabelC4"].SourceColumn = "LabelC4";
			parameters["@LabelC5"].SourceColumn = "LabelC5";
			parameters["@LabelC6"].SourceColumn = "LabelC6";
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

		private string GetInsertUpdateJobEstimationDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Estimation_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("PhaseID", "@PhaseID"), new FieldValue("BOQID", "@BOQID"), new FieldValue("BOQQuantity", "@BOQQuantity"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("AttributeC1", "@AttributeC1"), new FieldValue("AttributeC2", "@AttributeC2"), new FieldValue("AttributeC3", "@AttributeC3"), new FieldValue("AttributeC4", "@AttributeC4"), new FieldValue("AttributeC5", "@AttributeC5"), new FieldValue("AttributeC6", "@AttributeC6"), new FieldValue("MaterialOHP", "@MaterialOHP"), new FieldValue("LabourOHP", "@LabourOHP"), new FieldValue("OtherOHP", "@OtherOHP"), new FieldValue("CostMarkUp", "@CostMarkUp"), new FieldValue("RowRelation", "@RowRelation"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Total", "@Total"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobEstimationDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobEstimationDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobEstimationDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@PhaseID", SqlDbType.NVarChar);
			parameters.Add("@BOQID", SqlDbType.NVarChar);
			parameters.Add("@BOQQuantity", SqlDbType.Decimal);
			parameters.Add("@AttributeC1", SqlDbType.Decimal);
			parameters.Add("@AttributeC2", SqlDbType.Decimal);
			parameters.Add("@AttributeC3", SqlDbType.Decimal);
			parameters.Add("@AttributeC4", SqlDbType.Decimal);
			parameters.Add("@AttributeC5", SqlDbType.Decimal);
			parameters.Add("@AttributeC6", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@MaterialOHP", SqlDbType.Decimal);
			parameters.Add("@LabourOHP", SqlDbType.Decimal);
			parameters.Add("@OtherOHP", SqlDbType.Decimal);
			parameters.Add("@CostMarkUp", SqlDbType.Decimal);
			parameters.Add("@RowRelation", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@PhaseID"].SourceColumn = "PhaseID";
			parameters["@BOQID"].SourceColumn = "BOQID";
			parameters["@BOQQuantity"].SourceColumn = "BOQQuantity";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@AttributeC1"].SourceColumn = "AttributeC1";
			parameters["@AttributeC2"].SourceColumn = "AttributeC2";
			parameters["@AttributeC3"].SourceColumn = "AttributeC3";
			parameters["@AttributeC4"].SourceColumn = "AttributeC4";
			parameters["@AttributeC5"].SourceColumn = "AttributeC5";
			parameters["@AttributeC6"].SourceColumn = "AttributeC6";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@RowRelation"].SourceColumn = "RowRelation";
			parameters["@MaterialOHP"].SourceColumn = "MaterialOHP";
			parameters["@LabourOHP"].SourceColumn = "LabourOHP";
			parameters["@OtherOHP"].SourceColumn = "OtherOHP";
			parameters["@CostMarkUp"].SourceColumn = "CostMarkUp";
			parameters["@Total"].SourceColumn = "Total";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateJobEstimationItemDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Job_Estimation_Detail_Item", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("BOQID", "@BOQID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("Description", "@Description"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Cost", "@Cost"), new FieldValue("ActualCost", "@ActualCost"), new FieldValue("LabourCost", "@LabourCost"), new FieldValue("MaterialTotal", "@MaterialTotal"), new FieldValue("LabourTotal", "@LabourTotal"), new FieldValue("MaterialOH", "@MaterialOH"), new FieldValue("LabourOH", "@LabourOH"), new FieldValue("OtherOH", "@OtherOH"), new FieldValue("NetTotal", "@NetTotal"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitCost", "@UnitCost"), new FieldValue("UnitLabourCost", "@UnitLabourCost"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("RowRelation", "@RowRelation"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateJobEstimationItemDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateJobEstimationItemDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateJobEstimationItemDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@BOQID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Cost", SqlDbType.Money);
			parameters.Add("@ActualCost", SqlDbType.Money);
			parameters.Add("@LabourCost", SqlDbType.Decimal);
			parameters.Add("@MaterialTotal", SqlDbType.Decimal);
			parameters.Add("@LabourTotal", SqlDbType.Decimal);
			parameters.Add("@MaterialOH", SqlDbType.Decimal);
			parameters.Add("@LabourOH", SqlDbType.Decimal);
			parameters.Add("@OtherOH", SqlDbType.Decimal);
			parameters.Add("@NetTotal", SqlDbType.Decimal);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Decimal);
			parameters.Add("@UnitCost", SqlDbType.Decimal);
			parameters.Add("@UnitLabourCost", SqlDbType.Decimal);
			parameters.Add("@RowRelation", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@BOQID"].SourceColumn = "BOQID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@ActualCost"].SourceColumn = "ActualCost";
			parameters["@LabourCost"].SourceColumn = "LabourCost";
			parameters["@MaterialTotal"].SourceColumn = "MaterialTotal";
			parameters["@LabourTotal"].SourceColumn = "LabourTotal";
			parameters["@MaterialOH"].SourceColumn = "MaterialOH";
			parameters["@LabourOH"].SourceColumn = "LabourOH";
			parameters["@OtherOH"].SourceColumn = "OtherOH";
			parameters["@NetTotal"].SourceColumn = "NetTotal";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitCost"].SourceColumn = "UnitCost";
			parameters["@UnitLabourCost"].SourceColumn = "UnitLabourCost";
			parameters["@RowRelation"].SourceColumn = "RowRelation";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(JobEstimationData journalData)
		{
			return true;
		}

		public bool InsertUpdateJobEstimation(JobEstimationData jobEstimationData, bool isUpdate, bool IsRevised)
		{
			bool flag = true;
			SqlCommand insertUpdateJobEstimationCommand = GetInsertUpdateJobEstimationCommand(isUpdate);
			try
			{
				DataRow dataRow = jobEstimationData.JobEstimationTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Job_Estimation", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in jobEstimationData.JobEstimationDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				if (IsRevised)
				{
					DateTime dateTime = DateTime.Now;
					if (jobEstimationData.Tables["Job_Estimation"].Rows[0]["PreviousRevisedDate"].ToString() != "")
					{
						dateTime = DateTime.Parse(jobEstimationData.Tables["Job_Estimation"].Rows[0]["PreviousRevisedDate"].ToString());
					}
					int nextRevsionNo = GetNextRevsionNo(text2, text);
					string text3 = "";
					text3 = "INSERT INTO Job_Estimation_History SELECT SysDocID,VoucherID," + nextRevsionNo + ",TransactionDate,MaterialOHP,LabourOHP,OtherOHP,JobID,CostCategoryID,Reference,Note,LabelC1,LabelC2,LabelC3,LabelC4,LabelC5,LabelC6,\r\n                            '" + dateTime + "',DateCreated,DateUpdated,CreatedBy,UpdatedBy FROM Job_Estimation \r\n                                    WHERE VoucherID='" + text + "' AND SysDocID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text3, sqlTransaction) >= 0);
					text3 = "INSERT INTO Job_Estimation_Detail_History SELECT SysDocID,VoucherID," + nextRevsionNo + ",CostCategoryID,PhaseID,BOQID,BOQQuantity,RowIndex,AttributeC1,AttributeC2,AttributeC3,AttributeC4,AttributeC5,AttributeC6,Remarks,Total,RowRelation\r\n                             FROM Job_Estimation_Detail \r\n                                    WHERE VoucherID='" + text + "' AND SysDocID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text3, sqlTransaction) >= 0);
					text3 = "INSERT INTO Job_Estimation_Detail_Item_History SELECT SysDocID,VoucherID," + nextRevsionNo + ",BOQID,ProductID,Description,UnitQuantity,UnitCost,UnitLabourCost,Quantity,Cost,LabourCost,MaterialTotal,LabourTotal,MaterialOH,LabourOH,OtherOH,\r\n                           NetTotal,RowIndex,UnitID,RowRelation,Remarks FROM Job_Estimation_Detail_Item \r\n                                    WHERE VoucherID='" + text + "' AND SysDocID='" + text2 + "'";
					flag &= (ExecuteNonQuery(text3, sqlTransaction) >= 0);
				}
				if (isUpdate)
				{
					flag &= DeleteJobEstimationDetailsRows(text2, text, isDeletingTransaction: false, sqlTransaction);
				}
				insertUpdateJobEstimationCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(jobEstimationData, "Job_Estimation", insertUpdateJobEstimationCommand)) : (flag & Insert(jobEstimationData, "Job_Estimation", insertUpdateJobEstimationCommand)));
				insertUpdateJobEstimationCommand = GetInsertUpdateJobEstimationDetailsCommand(isUpdate: false);
				insertUpdateJobEstimationCommand.Transaction = sqlTransaction;
				if (jobEstimationData.Tables["Job_Estimation_Detail"].Rows.Count > 0)
				{
					flag &= Insert(jobEstimationData, "Job_Estimation_Detail", insertUpdateJobEstimationCommand);
				}
				insertUpdateJobEstimationCommand = GetInsertUpdateJobEstimationItemDetailsCommand(isUpdate: false);
				insertUpdateJobEstimationCommand.Transaction = sqlTransaction;
				if (jobEstimationData.Tables["Job_Estimation_Detail_Item"].Rows.Count > 0)
				{
					flag &= Insert(jobEstimationData, "Job_Estimation_Detail_Item", insertUpdateJobEstimationCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Job_Estimation", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Job Estimation";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Job_Estimation", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.JobEstimation, text2, text, "Job_Estimation", sqlTransaction);
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

		public JobEstimationData GetJobEstimationByID(string sysDocID, string voucherID)
		{
			try
			{
				JobEstimationData jobEstimationData = new JobEstimationData();
				string textCommand = "SELECT * FROM Job_Estimation WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobEstimationData, "Job_Estimation", textCommand);
				if (jobEstimationData == null || jobEstimationData.Tables.Count == 0 || jobEstimationData.Tables["Job_Estimation"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Estimation_Detail TD \r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobEstimationData, "Job_Estimation_Detail", textCommand);
				textCommand = "SELECT TD.*,P.ItemType,jbd.Quantity AS ActualQty ,jbd.Cost AS ActualCost,jbd.LabourCost AS ActualLabCost,\r\n                        (SELECT BOQQuantity  FROM Job_Estimation_Detail WHERE VoucherID=TD.VoucherID AND SysDocID=TD.SysDocID AND BOQID=TD.BOQID AND RowRelation=TD.RowRelation) AS ActualBOQQuantity\r\n                        FROM Job_Estimation_Detail_Item TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n\t\t\t\t\t\tLEFT JOIN Job_BOM_Detail jbd ON jbd.JobBOMID=TD.BOQID AND  jbd.ProductID = TD.ProductID                       \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowRelation";
				FillDataSet(jobEstimationData, "Job_Estimation_Detail_Item", textCommand);
				return jobEstimationData;
			}
			catch
			{
				throw;
			}
		}

		public JobEstimationData GetJobEstimationHistoryByID(string sysDocID, string voucherID, int RevisedNo)
		{
			try
			{
				JobEstimationData jobEstimationData = new JobEstimationData();
				string textCommand = "SELECT * FROM Job_Estimation_History WHERE RevisionNo=" + RevisedNo + " AND VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobEstimationData, "Job_Estimation", textCommand);
				if (jobEstimationData == null || jobEstimationData.Tables.Count == 0 || jobEstimationData.Tables["Job_Estimation"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Job_Estimation_Detail_History TD \r\n                        WHERE TD.RevisionNo=" + RevisedNo + " AND VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(jobEstimationData, "Job_Estimation_Detail", textCommand);
				textCommand = "SELECT TD.*,P.ItemType,jbd.Quantity AS ActualQty ,jbd.Cost AS ActualCost,jbd.LabourCost AS ActualLabCost,'0' AS ActualBOQQuantity\r\n                        --,(SELECT BOQQuantity  FROM Job_Estimation_Detail_History WHERE VoucherID=TD.VoucherID AND SysDocID=TD.SysDocID AND BOQID=TD.BOQID AND RowRelation=TD.RowRelation) AS ActualBOQQuantity\r\n                        FROM Job_Estimation_Detail_Item_History TD INNER JOIN Product P ON P.ProductID = TD.ProductID\r\n\t\t\t\t\t\tLEFT JOIN Job_BOM_Detail jbd ON jbd.JobBOMID=TD.BOQID AND  jbd.ProductID = TD.ProductID                       \r\n                        WHERE TD.RevisionNo=" + RevisedNo + " AND TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowRelation";
				FillDataSet(jobEstimationData, "Job_Estimation_Detail_Item", textCommand);
				return jobEstimationData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteJobEstimationDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Job_Estimation_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, sqlTransaction);
				commandText = "DELETE FROM Job_Estimation_Detail_Item WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidJobEstimation(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Job_Estimation", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null)
				{
					_ = (fieldValue.ToString() != "");
				}
				text = "UPDATE Job_Estimation SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Job Estimation", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteJobEstimation(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteJobEstimationDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				text = "DELETE FROM Job_Estimation WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Job Estimation", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetJobEstimationToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT IA.*,J.JobName,J.CustomerID,C.CustomerName FROM Job_Estimation IA\r\n                                INNER JOIN Job J ON J.JobID=IA.JobID\r\n                                INNER JOIN Customer C ON C.CustomerID=J.CustomerID\r\n                                WHERE SysDocID = \r\n                                 '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Estimation", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Estimation"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JED.*,JEDI.*,JF.FeeName,JB.BOMName,JC.CostCategoryName FROM Job_Estimation_Detail JED\r\n                        LEFT JOIN Job_Fee JF ON JED.PhaseID=JF.FeeID\r\n                        INNER JOIN Job_BOM JB ON JED.BOQID=JB.JobBOMID\r\n                        LEFT JOIN Job_Cost_Category JC ON JED.CostCategoryID=JC.CostCategoryID\r\n                        INNER JOIN Job_Estimation_Detail_Item JEDI ON JED.SysDocID=JEDI.SysDocID AND JED.VoucherID=JEDI.VoucherID AND JED.BOQID=JEDI.BOQID AND JED.RowRelation=JEDI.RowRelation\r\n                        WHERE JED.SysDocID='" + sysDocID + "' AND JED.VoucherID IN  (" + text + ")                        \r\n                        ORDER BY JED.RowIndex,JED.BOQID";
				FillDataSet(dataSet, "Job_Estimation_Detail", cmdText);
				dataSet.Relations.Add("JobEstDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Estimation"].Columns["SysDocID"],
					dataSet.Tables["Job_Estimation"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Estimation_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Estimation_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetJobEstimationList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			DataSet dataSet = new DataSet();
			string str = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID],JI.JobID,J.JobName,JI.CostCategoryID, JI.Reference,JI.TransactionDate AS [Date]   \r\n                            FROM Job_Estimation JI   LEFT JOIN Job J ON JI.JobID=J.JobID WHERE  JI.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!Isvoid)
			{
				str += " AND ISNULL(IsVoid,'False')='False' ";
			}
			str += " ORDER BY JI.TransactionDate, JI.VoucherID ";
			FillDataSet(dataSet, "Job_Estimation", str);
			return dataSet;
		}

		public DataSet GetLoadRevisionCombo(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT JI.SysDocID [Doc ID], JI.VoucherID [VoucherID],JI.JobID,JI.TransactionDate AS [Date],JI.RevisionNo,CONVERT(INT,JI.RevisionNo) as RevisionNoInt   \r\n                            FROM Job_Estimation_History JI  WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "' ORDER BY JI.RevisionNo desc";
			FillDataSet(dataSet, "Job_Estimation", textCommand);
			return dataSet;
		}

		public int GetNextRevsionNo(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			int result = 0;
			string textCommand = "SELECT TOP 1 JI.RevisionNo AS RevisionNo  \r\n                            FROM Job_Estimation_History JI  WHERE JI.SysDocID='" + sysDocID + "' AND JI.VoucherID = '" + voucherID + "' ORDER BY RevisionNo DESC";
			FillDataSet(dataSet, "Job_Estimation_History", textCommand);
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				int.TryParse(dataSet.Tables[0].Rows[0]["RevisionNo"].ToString(), out result);
				return result + 1;
			}
			return 1;
		}

		public DataSet GetJobEstimationRevToPrint(string sysDocID, string voucherID, int RevisedNo)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string cmdText = "SELECT IA.*,J.JobName,J.CustomerID,C.CustomerName FROM Job_Estimation_History IA\r\n                                INNER JOIN Job J ON J.JobID=IA.JobID\r\n                                INNER JOIN Customer C ON C.CustomerID=J.CustomerID\r\n                                WHERE RevisionNo=" + RevisedNo + " AND SysDocID = \r\n                                 '" + sysDocID + "' AND VoucherID ='" + voucherID + "'";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Job_Estimation", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Job_Estimation"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT JED.*,JEDI.*,JF.FeeName,JB.BOMName,JC.CostCategoryName FROM Job_Estimation_Detail_History JED\r\n                        LEFT JOIN Job_Fee JF ON JED.PhaseID=JF.FeeID\r\n                        INNER JOIN Job_BOM JB ON JED.BOQID=JB.JobBOMID\r\n                        LEFT JOIN Job_Cost_Category JC ON JED.CostCategoryID=JC.CostCategoryID\r\n                        INNER JOIN Job_Estimation_Detail_Item_History JEDI ON JED.SysDocID=JEDI.SysDocID AND JED.VoucherID=JEDI.VoucherID AND JED.BOQID=JEDI.BOQID AND JED.RowRelation=JEDI.RowRelation\r\n                        WHERE JED.RevisionNo=" + RevisedNo + " AND JED.SysDocID='" + sysDocID + "' AND JED.VoucherID ='" + voucherID + "'                        \r\n                        ORDER BY JED.RowIndex,JED.BOQID";
				FillDataSet(dataSet, "Job_Estimation_Detail", cmdText);
				dataSet.Relations.Add("JobEstDetail", new DataColumn[2]
				{
					dataSet.Tables["Job_Estimation"].Columns["SysDocID"],
					dataSet.Tables["Job_Estimation"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Job_Estimation_Detail"].Columns["SysDocID"],
					dataSet.Tables["Job_Estimation_Detail"].Columns["VoucherID"]
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
