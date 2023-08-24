using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class CLVouchers : StoreObject
	{
		private const string CLVOUCHER_TABLE = "CL_Voucher";

		private const string CLVOUCHERDETAIL_TABLE = "CL_Voucher_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string VOUCHERDATE_PARM = "@VoucherDate";

		private const string NOTE_PARM = "@Note";

		private const string REFERENCE_PARM = "@Reference";

		private const string VALIDFROM_PARM = "@ValidFrom";

		private const string VALIDTO_PARM = "@ValidTo";

		private const string REASON_PARM = "@Reason";

		private const string AMOUNT_PARM = "@Amount";

		private const string APPROVEDBY_PARM = "@ApprovedBy";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CHEQUEID_PARM = "@ChequeID";

		public CLVouchers(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateCLVoucherText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("CL_Voucher", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VoucherDate", "@VoucherDate"), new FieldValue("Reference", "@Reference"), new FieldValue("ValidFrom", "@ValidFrom"), new FieldValue("ValidTo", "@ValidTo"), new FieldValue("Reason", "@Reason"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("IsVoid", "@IsVoid"), new FieldValue("ApprovedBy", "@ApprovedBy"), new FieldValue("Amount", "@Amount"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("CL_Voucher", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCLVoucherCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateCLVoucherText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateCLVoucherText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VoucherDate", SqlDbType.DateTime);
			parameters.Add("@Reason", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ApprovedBy", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@ValidFrom", SqlDbType.DateTime);
			parameters.Add("@ValidTo", SqlDbType.DateTime);
			parameters.Add("@IsVoid", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VoucherDate"].SourceColumn = "VoucherDate";
			parameters["@Reason"].SourceColumn = "Reason";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ApprovedBy"].SourceColumn = "ApprovedBy";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@ValidFrom"].SourceColumn = "ValidFrom";
			parameters["@ValidTo"].SourceColumn = "ValidTo";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
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

		private bool ValidateData(CLVoucherData data)
		{
			return true;
		}

		public bool InsertUpdateCLVoucher(CLVoucherData CLVoucherData, bool isInactive, bool isHold, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCLVoucherCommand = GetInsertUpdateCLVoucherCommand(isUpdate);
			try
			{
				DataRow dataRow = CLVoucherData.CLVOUCHERTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				dataRow["Reference"].ToString();
				string text2 = dataRow["CustomerID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("CL_Voucher", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdateCLVoucherCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(CLVoucherData, "CL_Voucher", insertUpdateCLVoucherCommand)) : (flag & Insert(CLVoucherData, "CL_Voucher", insertUpdateCLVoucherCommand)));
				if (flag)
				{
					string exp = "UPDATE Customer SET IsInactive = '" + isInactive.ToString() + "' , IsHold= '" + isHold.ToString() + "'  WHERE CustomerID='" + text2 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("CL_Voucher", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Credit Limit Voucher";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "CL_Voucher", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CLVoucher, sysDocID, text, "CL_Voucher", sqlTransaction);
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

		public CLVoucherData GetCLVoucherByID(string sysDocID, string voucherID)
		{
			try
			{
				CLVoucherData cLVoucherData = new CLVoucherData();
				string textCommand = "SELECT CLV.*, c.IsInactive, c.IsHold FROM CL_Voucher  CLV INNER JOIN Customer C ON CLV.CustomerID=C.CustomerID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(cLVoucherData, "CL_Voucher", textCommand);
				return cLVoucherData;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidCLVoucher(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE CL_Voucher SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Credit Limit Voucher", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteCLVoucher(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM CL_Voucher WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Credit Limit Voucher", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetCLVoucherToPrint(string sysDocID, string voucherID)
		{
			return GetCLVoucherToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetCLVoucherToPrint(string sysDocID, string[] voucherID)
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
				string textCommand = "SELECT CS.*, AccountName FROM CL_Voucher CS INNER JOIN Account A ON CS.BankAccountID = A.AccountID WHERE VoucherID=" + text + " AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "CL_Voucher", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT    SysDocID [Doc ID],VoucherID [Doc Number], VoucherDate [Date],CS.CustomerID + ' | '+ CUS.CustomerName AS Customer,\r\nValidFrom [Valid From], ValidTo [Valid To],Amount        \r\n                            FROM         CL_Voucher CS INNER JOIN Customer CUS ON CUS.CustomerID = CS.CustomerID   ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE VoucherDate Between '" + text + "' AND '" + text2 + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "CL_Voucher", sqlCommand);
			return dataSet;
		}
	}
}
