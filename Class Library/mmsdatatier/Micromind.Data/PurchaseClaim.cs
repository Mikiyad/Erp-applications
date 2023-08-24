using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseClaim : StoreObject
	{
		private const string PURCHASECLAIM_TABLE = "Purchase_Claim";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string CLAIMAMOUNT_PARM = "@ClaimAmount";

		private const string CLAIMDETAILS_PARM = "@ClaimDetails";

		private const string CREDITNOTENO_PARM = "@CreditNoteNo";

		private const string CRNOTEAMOUNT_PARM = "@CRNoteAmount";

		private const string CLAIMSTATUS_PARM = "@ClaimStatus";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public PurchaseClaim(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseClaimText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Claim", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ClaimAmount", "@ClaimAmount"), new FieldValue("ClaimDetails", "@ClaimDetails"), new FieldValue("CreditNoteNo", "@CreditNoteNo"), new FieldValue("CRNoteAmount", "@CRNoteAmount"), new FieldValue("ClaimStatus", "@ClaimStatus"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("IsVoid", "@IsVoid"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Claim", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseClaimCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseClaimText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseClaimText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ClaimAmount", SqlDbType.Decimal);
			parameters.Add("@ClaimDetails", SqlDbType.NVarChar);
			parameters.Add("@CreditNoteNo", SqlDbType.NVarChar);
			parameters.Add("@CRNoteAmount", SqlDbType.Decimal);
			parameters.Add("@ClaimStatus", SqlDbType.TinyInt);
			parameters.Add("@IsVoid", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ClaimAmount"].SourceColumn = "ClaimAmount";
			parameters["@ClaimDetails"].SourceColumn = "ClaimDetails";
			parameters["@CreditNoteNo"].SourceColumn = "CreditNoteNo";
			parameters["@CRNoteAmount"].SourceColumn = "CRNoteAmount";
			parameters["@ClaimStatus"].SourceColumn = "ClaimStatus";
			parameters["@IsVoid"].SourceColumn = "IsVoid";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
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

		private bool ValidateData(PurchaseClaimData journalData)
		{
			return true;
		}

		public bool VoidPurchaseClaim(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Purchase_Claim SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Purchase Claim", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePurchaseClaim(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "DELETE FROM Purchase_Claim WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Claim", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool InsertUpdatePurchaseClaim(PurchaseClaimData purchaseClaimData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseClaimCommand = GetInsertUpdatePurchaseClaimCommand(isUpdate);
			try
			{
				DataRow dataRow = purchaseClaimData.PurchaseClaimTable.Rows[0];
				_ = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Claim", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				insertUpdatePurchaseClaimCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseClaimData, "Purchase_Claim", insertUpdatePurchaseClaimCommand)) : (flag & Insert(purchaseClaimData, "Purchase_Claim", insertUpdatePurchaseClaimCommand)));
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Claim", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Claim";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Claim", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseClaim, sysDocID, text, "Purchase_Claim", sqlTransaction);
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

		public PurchaseClaimData GetPurchaseClaimByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseClaimData purchaseClaimData = new PurchaseClaimData();
				string textCommand = "SELECT PC.*,PR.ContainerNumber,PR.TransactionDate as [GRNDate],PR.Reference,PR.Reference2,PR.POSysDocID,PR.POVoucherID,PR.InvoiceSysDocID,PR.InvoiceVoucherID,PR.SourceSysDocID AS [PackDocID],PR.SourceVoucherID AS  [PackVoucherID],PR.ContainerSizeID,PR.VendorID,PR.BuyerID,B.FullName [BuyerName] FROM Purchase_Claim  PC \r\n                   INNER JOIN Purchase_Receipt PR ON PC.SourceSysDocID=PR.SySDocID AND PC.SourceVoucherID=PR.VoucherID  \r\n                    LEFT JOIN Buyer B ON  B.BuyerID=PR.BuyerID\r\n                                WHERE PC.SysDocID = '" + sysDocID + "' AND PC.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseClaimData, "Purchase_Claim", textCommand);
				return purchaseClaimData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseClaimToPrint(string sysDocID, string voucherID)
		{
			return GetPurchaseClaimToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseClaimToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT PC.*,V.Vendorname,PR.ContainerNumber,PR.TransactionDate as [GRNDate],PR.Reference,\r\n                                PR.Reference2,PR.POSysDocID,PR.POVoucherID,PR.InvoiceSysDocID,PR.InvoiceVoucherID,\r\n                                PR.SourceSysDocID AS [PackDocID],PR.SourceVoucherID AS  [PackVoucherID],PR.ContainerSizeID,PR.VendorID,\r\n                                PR.BuyerID,B.FullName [BuyerName],CASE WHEN PC.Claimstatus='1' THEN 'Open' ELSE 'Closed' END AS Staus  \r\n                                FROM Purchase_Claim  PC  INNER JOIN Purchase_Receipt PR ON PC.SourceSysDocID=PR.SySDocID AND PC.SourceVoucherID=PR.VoucherID  \r\n                                LEFT JOIN Buyer B ON  B.BuyerID=PR.BuyerID INNER JOIN Vendor V ON V.VendorID=PR.VendorID\r\n                                                                WHERE PC.SysDocID = '" + sysDocID + "' AND PC.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Purchase_Claim", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Claim"].Rows.Count == 0)
				{
					return null;
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseClaimToPrintTR(string sysDocID, string voucherID)
		{
			return GetPurchaseClaimToPrintTR(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseClaimToPrintTR(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT ISNULL(PR.AmountFC,PR.Amount) Amount,BF.PrintTemplateName as [TemplateName],V.VendorName,VA.Address1,VA.Address2,VA.Address3,V.CountryID,PR.TransactionDate,\r\n                            ( SELECT BankAccountNumber FROM Account WHERE Accountid IN (BF.CurrentAccountID)) AS [Account Number], \r\n                            (SELECT CountryName FROM Country WHERE CountryID IN (V.CountryID)) AS [Vendor Country],\r\n                            VA.AddressPrintFormat,V.BankName AS [Vendor Bank],V.BankBranch AS [Vendor Bank Address],V.BankAccountNumber AS [Vendor Account],PR.Reference,\r\n                            PT1.TermName AS PayeeTerm,V.CurrencyID AS PayeeCurrencyID,'' as AmountInWords  \r\n                            FROM Payment_Request PR LEFt OUTER JOIN Account ON PR.PayFromID=Account.AccountID \r\n                            LEFt OUTER JOIN Chequebook ON PR.PayFromID=Chequebook.ChequebookID\r\n                            LEFt OUTER JOIN Bank_Facility BF ON PR.PayFromID=BF.FacilityID  \r\n                            INNER JOIN Vendor V ON V.VendorID  = PR.PayeeID\r\n                            LEFT JOIN Vendor_Address VA ON V.VendorID=VA.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT1 ON PT1.PaymentTermID = V.PaymentTermID\r\n                            LEFT OUTER JOIN Purchase_Order PO ON PO.SysDocID = POSysDocID AND PO.VoucherID = POVoucherID\r\n                            LEFT OUTER JOIN Payment_Term POTerm ON POTerm.PaymentTermID = PO.TermID\r\n                             WHERE PR.SysDocID = '" + sysDocID + "' AND PR.VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Purchase_Claim", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Claim"].Rows.Count == 0)
				{
					return null;
				}
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
			string text3 = "SELECT  PC.SysDocID,PC.VoucherID, PC.SourceVoucherID AS [GRN],PC.TransactionDate as [Date],V.VendorName[Supplier],PR.ContainerNumber[Container No.],PC.ClaimAmount[ClaimAmount]\r\n                            , CASE PC.ClaimStatus WHEN  '1' THEN 'Open' ELSE 'Closed' END AS ClaimStatus FROM Purchase_Claim PC   INNER JOIN Purchase_Receipt PR ON PC.SourceSysDocID=PR.SySDocID AND PC.SourceVoucherID=PR.VoucherID\r\n\t\t                     LEFT JOIN Vendor V ON V.VendorID=PR.VendorID  ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE PC.TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(PC.IsVoid,'False')='False'";
			}
			text3 += " ORDER BY PC.SysDocID,PC.VoucherID";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Claim", sqlCommand);
			return dataSet;
		}

		public DataSet GetOpenPurchaseClaims(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				SqlCommand sqlCommand = new SqlCommand("SELECT PC.SysDocID [Doc ID],PC.VoucherID [Number],PC.SourceVoucherID as GRN,PC.TransactionDate as Date FROM Purchase_claim PC\r\n                    LEFT JOIN Purchase_Receipt PR ON PR.SysDocID=PC.SourceSysdocid AND PR.VoucherID=PC.SourceVoucherID\r\n                    WHERE PC.ClaimStatus=1 AND PR.VendorID ='" + vendorID + "'");
				FillDataSet(dataSet, "Purchase_Claim", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Order_Detail SOD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
