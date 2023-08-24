using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CustomerInsuranceClaim : StoreObject
	{
		public const string VOUCHERID_PARM = "@VoucherID";

		public const string SYSDOCID_PARM = "@SysDocID";

		public const string TRANSACTIONDATE_PARM = "@TransactionDate";

		public const string CUSTOMERID_PARM = "@CustomerID";

		public const string INSPROVIDERID_PARM = "@InsProviderID";

		public const string INSAPPLICATIONDATE_PARM = "@InsApplicationDate";

		public const string INSPAYABLEAMOUNT_PARM = "@InsPayableAmount";

		public const string INSEFFECTIVEDATE_PARM = "@InsEffectiveDate";

		public const string INSREMARKS_PARM = "@InsRemarks";

		public const string INSPOLICYNUMBER_PARM = "@InsPolicyNumber";

		public const string INSAPPROVEDAMOUNT_PARM = "@InsApprovedAmount";

		public const string INSURANCEID_PARM = "@InsuranceID";

		public const string INSEXPIRYDATE_PARM = "@InsExpiryDate";

		public const string CLAIMAMOUNT_PARM = "@ClaimAmount";

		public const string PAIDAMOUNT_PARM = "@PaidAmount";

		public const string REASON_PARM = "@Reason";

		public const string CUSTOMERINSREMARKS_PARM = "@CustomerInsRemarks";

		public const string PAIDDATE_PARM = "@PaidDate";

		public const string CUSTOMERINSSTATUS_PARM = "@CustomerInsStatus";

		public const string CREATEDBY_PARM = "@CreatedBy";

		public const string DATECREATED_PARM = "@DateCreated";

		public const string UPDATEDBY_PARM = "@UpdatedBy";

		public const string DATEUPDATED_PARM = "@DateUpdated";

		public const string CUSTOMERINSURANCECLAIM_TABLE = "Customer_Insurance_Claim";

		public CustomerInsuranceClaim(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateCustomerInsuranceClaimText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customer_Insurance_Claim", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("InsProviderID", "@InsProviderID"), new FieldValue("InsApplicationDate", "@InsApplicationDate"), new FieldValue("InsPayableAmount", "@InsPayableAmount"), new FieldValue("InsEffectiveDate", "@InsEffectiveDate"), new FieldValue("InsRemarks", "@InsRemarks"), new FieldValue("InsPolicyNumber", "@InsPolicyNumber"), new FieldValue("InsApprovedAmount", "@InsApprovedAmount"), new FieldValue("InsuranceID", "@InsuranceID"), new FieldValue("InsExpiryDate", "@InsExpiryDate"), new FieldValue("ClaimAmount", "@ClaimAmount"), new FieldValue("PaidAmount", "@PaidAmount"), new FieldValue("Reason", "@Reason"), new FieldValue("CustomerInsRemarks", "@CustomerInsRemarks"), new FieldValue("PaidDate", "@PaidDate"), new FieldValue("CustomerInsStatus", "@CustomerInsStatus"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Customer_Insurance_Claim", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCustomerInsuranceClaimCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateCustomerInsuranceClaimText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateCustomerInsuranceClaimText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@InsProviderID", SqlDbType.NVarChar);
			parameters.Add("@InsApplicationDate", SqlDbType.DateTime);
			parameters.Add("@InsPayableAmount", SqlDbType.Money);
			parameters.Add("@InsEffectiveDate", SqlDbType.DateTime);
			parameters.Add("@InsRemarks", SqlDbType.NVarChar);
			parameters.Add("@InsPolicyNumber", SqlDbType.NVarChar);
			parameters.Add("@InsApprovedAmount", SqlDbType.Money);
			parameters.Add("@InsuranceID", SqlDbType.NVarChar);
			parameters.Add("@InsExpiryDate", SqlDbType.DateTime);
			parameters.Add("@ClaimAmount", SqlDbType.Money);
			parameters.Add("@PaidAmount", SqlDbType.Money);
			parameters.Add("@Reason", SqlDbType.NVarChar);
			parameters.Add("@CustomerInsRemarks", SqlDbType.NVarChar);
			parameters.Add("@PaidDate", SqlDbType.DateTime);
			parameters.Add("@CustomerInsStatus", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@InsProviderID"].SourceColumn = "InsProviderID";
			parameters["@InsApplicationDate"].SourceColumn = "InsApplicationDate";
			parameters["@InsPayableAmount"].SourceColumn = "InsPayableAmount";
			parameters["@InsEffectiveDate"].SourceColumn = "InsEffectiveDate";
			parameters["@InsRemarks"].SourceColumn = "InsRemarks";
			parameters["@InsPolicyNumber"].SourceColumn = "InsPolicyNumber";
			parameters["@InsApprovedAmount"].SourceColumn = "InsApprovedAmount";
			parameters["@InsuranceID"].SourceColumn = "InsuranceID";
			parameters["@InsExpiryDate"].SourceColumn = "InsExpiryDate";
			parameters["@ClaimAmount"].SourceColumn = "ClaimAmount";
			parameters["@PaidAmount"].SourceColumn = "PaidAmount";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@CustomerInsRemarks"].SourceColumn = "CustomerInsRemarks";
			parameters["@PaidDate"].SourceColumn = "PaidDate";
			parameters["@CustomerInsStatus"].SourceColumn = "CustomerInsStatus";
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

		private bool ValidateData(CustomerInsuranceClaimData CustomerInsuranceClaimData)
		{
			return true;
		}

		public bool InsertUpdateCustomerInsuranceClaim(CustomerInsuranceClaimData customerInsuranceClaimData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCustomerInsuranceClaimCommand = GetInsertUpdateCustomerInsuranceClaimCommand(isUpdate);
			try
			{
				DataRow dataRow = customerInsuranceClaimData.CustomerInsuranceClaimTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Customer_Insurance_Claim", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateCustomerInsuranceClaimCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(customerInsuranceClaimData, "Customer_Insurance_Claim", insertUpdateCustomerInsuranceClaimCommand)) : (flag & Insert(customerInsuranceClaimData, "Customer_Insurance_Claim", insertUpdateCustomerInsuranceClaimCommand)));
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Customer_Insurance_Claim", "SysDocID", text2, "VoucherID", text, sqlTransaction, !isUpdate);
				string entityName = "CustomerInsuranceClaim";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, text2, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Customer_Insurance_Claim", "VoucherID", sqlTransaction);
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

		public CustomerInsuranceClaimData GetCustomerInsuranceClaimByID(string sysDocID, string voucherID)
		{
			try
			{
				CustomerInsuranceClaimData customerInsuranceClaimData = new CustomerInsuranceClaimData();
				string textCommand = "SELECT * FROM Customer_Insurance_Claim WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(customerInsuranceClaimData, "Customer_Insurance_Claim", textCommand);
				if (customerInsuranceClaimData == null || customerInsuranceClaimData.Tables.Count == 0 || customerInsuranceClaimData.Tables["Customer_Insurance_Claim"].Rows.Count == 0)
				{
					return null;
				}
				return customerInsuranceClaimData;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteCustomerInsuranceClaim(string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Customer_Insurance_Claim WHERE VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("CustomerInsuranceClaim", voucherID, activityType, sqlTransaction);
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

		public DataSet GetList(DateTime from, DateTime to, string status)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT SysDocID as [Doc ID], VoucherID AS [Doc Number], C.CustomerID+ '-'+ C.CustomerName as Customer,IP.InsuranceProviderName [Insurance Provider], CI.InsPolicyNumber AS [Insurance No.], CI.InsApplicationDate AS [Applied Date],\r\n                            CI.InsEffectiveDate As [Effective Date],CI.InsApprovedAmount AS [Insurance Amount],CI.ClaimAmount AS [Claim Amount],CI.InsPayableAmount AS [Payable Amount],CI.PaidAmount AS [Paid Amount],CI.PaidDate [Paid Date],CI.CustomerInsStatus [Status]   FROM Customer_Insurance_Claim CI INNER JOIN Customer C ON CI.CustomerID=C.CustomerID \r\n                             LEFT JOIN Insurance_Provider IP ON IP.InsuranceProviderID = CI.InsProviderID WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND CI.DateCreated Between '" + text + "' AND '" + text2 + "'";
			}
			if (status != "")
			{
				text3 = text3 + " AND CI.CustomerInsStatus='" + status + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Customer_Insurance_Claim", sqlCommand);
			return dataSet;
		}

		public DataSet GetCustomerInsuranceClaimToPrint(string sysDocID, string voucherID)
		{
			return GetCustomerInsuranceClaimToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetCustomerInsuranceClaimToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = " SELECT CIC.*,C.CustomerName AS CustomerName,IP.InsuranceProviderName AS [Insurance Provider],\r\n                                FD.NextFollowupDate,FD.ThisFollowupDate AS FollowupDate,FD.ThisFollowupByID AS FollowupBy,FD.NextFollowupByID AS NextFollowupBy,\r\n                                FD.ThisFollowupStatusID AS Status,FD.Remark\t\t\t\t\r\n                                FROM Customer_Insurance_Claim CIC \r\n                                    LEFT JOIN Customer C ON C.CustomerID=CIC.CustomerID\r\n                                    LEFT JOIN Insurance_Provider IP ON IP.InsuranceProviderID = CIC.InsProviderID\r\n                                    LEFT JOIN Lead_Followup_Details FD ON  CIC.SysDocID = FD.SourceSysDocID AND CIC.VoucherID = FD.SourceVoucherID\r\n                                     WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ") ";
				FillDataSet(dataSet, "Customer_Insurance_Claim", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
