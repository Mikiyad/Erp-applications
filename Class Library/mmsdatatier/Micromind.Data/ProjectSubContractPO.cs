using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ProjectSubContractPO : StoreObject
	{
		private const string PROJECTSUBCONTRACTPO_TABLE = "Project_Subcontract_PO";

		private const string PROJECTSUBCONTRACTPODETAIL_TABLE = "Project_Subcontract_PO_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STARTDATE_PARM = "@StartDate";

		private const string ENDDATE_PARM = "@EndDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE_PARM2 = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TOTAL_PARM = "@Total";

		private const string DUEDATE_PARM = "@DueDate";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string PORTLOADING_PARM = "@PortLoading";

		private const string PORTDESTINATION_PARM = "@PortDestination";

		private const string ETA_PARM = "@ETA";

		private const string ETD_PARM = "@ETD";

		private const string INCOID_PARM = "@INCOID";

		private const string BOLNO_PARM = "@BOLNo";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISSOURCEDROW_PARM = "@IsSourcedRow";

		private const string REMARKS_PARM = "@Remarks";

		public ProjectSubContractPO(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateProjectSubContractPOText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Project_Subcontract_PO", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("StartDate", "@StartDate"), new FieldValue("EndDate", "@EndDate"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("PortLoading", "@PortLoading"), new FieldValue("PortDestination", "@PortDestination"), new FieldValue("ETA", "@ETA"), new FieldValue("ETD", "@ETD"), new FieldValue("INCOID", "@INCOID"), new FieldValue("BOLNo", "@BOLNo"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("Note", "@Note"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Project_Subcontract_PO", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProjectSubContractPOCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProjectSubContractPOText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProjectSubContractPOText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@StartDate", SqlDbType.DateTime);
			parameters.Add("@EndDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PortLoading", SqlDbType.NVarChar);
			parameters.Add("@PortDestination", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ETD", SqlDbType.DateTime);
			parameters.Add("@INCOID", SqlDbType.NVarChar);
			parameters.Add("@BOLNo", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@StartDate"].SourceColumn = "StartDate";
			parameters["@EndDate"].SourceColumn = "EndDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PortLoading"].SourceColumn = "PortLoading";
			parameters["@PortDestination"].SourceColumn = "PortDestination";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ETD"].SourceColumn = "ETD";
			parameters["@INCOID"].SourceColumn = "INCOID";
			parameters["@BOLNo"].SourceColumn = "BOLNo";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
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

		private string GetInsertUpdateProjectSubContractPODetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Project_Subcontract_PO_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("JobID", "@JobID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateProjectSubContractPODetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateProjectSubContractPODetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateProjectSubContractPODetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsSourcedRow", SqlDbType.Bit);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@IsSourcedRow"].SourceColumn = "IsSourcedRow";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ProjectSubcontractPOData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Project_SubContract_PI_Detail POD\r\n                                WHERE OrderSysDocID='" + sysDocID + "' AND OrderVoucherID='" + voucherNumber + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateProjectSubContractPO(ProjectSubcontractPOData ProjectSubContractData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateProjectSubContractPOCommand = GetInsertUpdateProjectSubContractPOCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = ProjectSubContractData.ProjectSubcontractPOTable.Rows[0];
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text2 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (isUpdate && !CanUpdate(sysDocID, text2, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				decimal num = default(decimal);
				foreach (DataRow row in ProjectSubContractData.ProjectSubcontractPODetailTable.Rows)
				{
					decimal num2 = default(decimal);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result);
					decimal.TryParse(row["UnitPrice"].ToString(), out result2);
					num2 = Math.Round(result * result2, currencyDecimalPoints);
					num += num2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Project_Subcontract_PO", "VoucherID", dataRow["SysDocID"].ToString(), text2, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in ProjectSubContractData.ProjectSubcontractPODetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text3)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text4 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text4;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text4 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
				}
				insertUpdateProjectSubContractPOCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(ProjectSubContractData, "Project_Subcontract_PO", insertUpdateProjectSubContractPOCommand)) : (flag & Insert(ProjectSubContractData, "Project_Subcontract_PO", insertUpdateProjectSubContractPOCommand)));
				insertUpdateProjectSubContractPOCommand = GetInsertUpdateProjectSubContractPODetailsCommand(isUpdate: false);
				insertUpdateProjectSubContractPOCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteProjectSubContractPODetailsRows(sysDocID, text2, sqlTransaction);
				}
				if (ProjectSubContractData.Tables["Project_Subcontract_PO_Detail"].Rows.Count > 0)
				{
					flag &= Insert(ProjectSubContractData, "Project_Subcontract_PO_Detail", insertUpdateProjectSubContractPOCommand);
				}
				foreach (DataRow row3 in ProjectSubContractData.ProjectSubcontractPODetailTable.Rows)
				{
					text = row3["ProductID"].ToString();
					float num5 = float.Parse(row3["Quantity"].ToString());
					float quantity = new Products(base.DBConfig).GetOrderedQuantity(text, sqlTransaction) + num5;
					flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text, quantity, sqlTransaction);
				}
				if (ProjectSubContractData.Tables.Contains("Tax_Detail") && ProjectSubContractData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(ProjectSubContractData, sysDocID, text2, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Project_Subcontract_PO", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Project SubContract PO";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text2, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Project_Subcontract_PO", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ProjectSubContractPO, sysDocID, text2, "Project_Subcontract_PO", sqlTransaction);
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

		public DataSet GetPOPaymentSummary(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.CurrencyID, PO.TermID,PO.Total,PO.ETA, ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount                                           \r\n                                FROM Project_Subcontract_PO PO  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Project_Subcontract_PO", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public ProjectSubcontractPOData GetProjectSubContractPOByID(string sysDocID, string voucherID)
		{
			try
			{
				ProjectSubcontractPOData projectSubcontractPOData = new ProjectSubcontractPOData();
				string textCommand = "SELECT *, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n                                CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Project_SubContract_PO PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail POS\r\n                                WHERE  POS.SourceSysDocID = '" + sysDocID + "' AND   POS.SourceVoucherID = '" + voucherID + "') T  ON T.SourceSysDocID = PO.SysDocID AND T.SourceVoucherID = PO.VoucherID \r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO", textCommand);
				if (projectSubcontractPOData == null || projectSubcontractPOData.Tables.Count == 0 || projectSubcontractPOData.Tables["Project_Subcontract_PO"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "select P.IsTrackLot,PI.UnitID,PI.JobID,PI.QuantityReceived, P.IsTrackSerial, PO.ProductID,P.Description,PO.RowIndex as PORowIndex,PO.Remarks,\r\n                    (Select SUM(CurrentValue) from Project_SubContract_PI_Detail PI2    where PI2.OrderSysDocID=PO.SysDocID and PI2.OrderVoucherID =PO.VoucherID and PI2.OrderRowIndex=Po.RowIndex GROUP BY OrderRowIndex ) as invoiced\r\n                        ,(PO.Quantity*PO.UnitPrice) AS OriginalValue,(Select sUM(CurrentPercent)  from Project_SubContract_PI_Detail PI2   \r\n\t\t\t\t\t\t where PI2.OrderSysDocID=PO.SysDocID and PI2.OrderVoucherID =PO.VoucherID and PI2.OrderRowIndex=Po.RowIndex GROUP BY OrderRowIndex) AS PercentCompleted,PO.TaxGroupID,PO.TaxOption, PO.TaxAmount\r\n\t\t\t\t\t\t  From  Project_Subcontract_PO_Detail PO left join Project_SubContract_PI_Detail PI on PO.SysDocID=PI.OrderSysDocID and PO.VoucherID =PI.OrderVoucherID\r\n                        INNER JOIN Product P ON PO.ProductID=P.ProductID WHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "' group by PO.TaxGroupID,PO.TaxOption, PO.TaxAmount, PO.ProductID,PO.SysDocID,PO.VoucherID,PO.UnitPrice,P.Description,PO.Remarks, P.IsTrackLot, P.IsTrackSerial,PI.UnitID,PI.JobID,PI.QuantityReceived,PO.RowIndex , PO.Quantity";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Tax_Detail", textCommand);
				return projectSubcontractPOData;
			}
			catch
			{
				throw;
			}
		}

		public ProjectSubcontractPOData GetProjectSubContractOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				ProjectSubcontractPOData projectSubcontractPOData = new ProjectSubcontractPOData();
				string textCommand = "SELECT *, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n                                CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Project_SubContract_PO PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail POS\r\n                                WHERE  POS.SourceSysDocID = '" + sysDocID + "' AND   POS.SourceVoucherID = '" + voucherID + "') T  ON T.SourceSysDocID = PO.SysDocID AND T.SourceVoucherID = PO.VoucherID \r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO", textCommand);
				if (projectSubcontractPOData == null || projectSubcontractPOData.Tables.Count == 0 || projectSubcontractPOData.Tables["Project_Subcontract_PO"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, Product.IsTrackLot, Product.IsTrackSerial\r\n                        FROM Project_SubContract_PO_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO_Detail", textCommand);
				return projectSubcontractPOData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProjectSubContractPODetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT PO.TransactionDate,PO.VoucherID,V.VendorName,J.JobName,POD.UnitPrice,POD.ProductID,POD.UnitID,POD.Description,POD.Quantity,\r\n                                POD.QuantityReceived,POD.UnitPrice  FROM Project_Subcontract_PO PO LEFT JOIN Project_Subcontract_PO_Detail POD ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                LEFT JOIN JOB J ON J.JobID=POD.JobID LEFT JOIN Vendor V ON V.VendorID=PO.VendorID LEFT JOIN Product P ON POD.ProductID=P.ProductID\r\n                                LEFT JOIN Product ON POD.ProductID=Product.ProductID ";
				text3 = text3 + "WHERE PO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PO.IsVoid,'False')='False' ";
				if (jobID != "")
				{
					text3 = text3 + " AND POD.JobID='" + jobID + "'";
				}
				if (vendorID != "")
				{
					text3 = text3 + " AND PO.VendorID= '" + vendorID + "'";
				}
				if (fromItem != "")
				{
					text3 = text3 + " AND POD.ProductID BETWEEN '" + fromItem + "' AND '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE ClassID BETWEEN '" + fromClass + "' AND '" + toClass + "') ";
				}
				if (fromCategory != "")
				{
					text3 = text3 + " AND POD.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID BETWEEN '" + fromCategory + "' AND '" + toCategory + "') ";
				}
				FillDataSet(dataSet, "Project_Subcontract_PO", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsForPackingList(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.SysDocID [Doc ID],PO.VoucherID [Number],V.ParentVendorID, TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor] FROM Project_Subcontract_PO PO\r\n                             INNER JOIN Vendor V ON PO.VendorID=V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  AND Status = 1 AND ISNULL(IsImport,'False')='" + isImport.ToString() + "'";
				if (vendorID != "")
				{
					text = text + " AND (PO.VendorID='" + vendorID + "' OR V.ParentVendorID = '" + vendorID + "')";
				}
				FillDataSet(dataSet, "Project_Subcontract_PO", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOsItemsToShip(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT PO.VendorID,Ven.ParentVendorID, POD.SysDocID [Doc ID], POD.VoucherID AS [PO Number],ProductID,Description,POD.UnitID,RowIndex,UnitQuantity,\r\n                                ISNULL(UnitQuantity,Quantity) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped,\r\n                                ISNULL(SubunitPrice,UnitPrice) AS UnitPrice,ISNULL(QuantityReceived,0) AS QuantityReceived, PO.ShippingMethodID, PO.ETA, PO.PortDestination \r\n                                FROM Project_Subcontract_PO_Detail POD INNER JOIN Project_Subcontract_PO PO ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                INNER JOIN Vendor VEN ON VEN.VendorID=PO.VendorID\r\n                              \r\n                             WHERE POD.SysDocID='" + sysDocID + "' AND POD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Project_Subcontract_PO_Detail", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrdersSummary(string vendorID, bool isImport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string str = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Project_Subcontract_PO SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=0";
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				str = str + " AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "'";
				FillDataSet(dataSet, "Project_Subcontract_PO", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenPOComboData(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Project_Subcontract_PO PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1 ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Project_Subcontract_PO", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPOListForPayment(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],ETA as [ETA],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Project_Subcontract_PO PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status IN (1,2,3) ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Project_Subcontract_PO", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetProjectSubContractPOAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order_NonInv SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				FillDataSet(dataSet, "Project_Subcontract_PO", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, string productID, float quantity, SqlTransaction sqlTransaction)
		{
			try
			{
				return UpdateRowShippedQuantity(sysDocID, voucherID, rowIndex, productID, quantity, validateQuantity: true, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, string productID, float quantity, bool validateQuantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			float result3 = 0f;
			try
			{
				string textCommand = "SELECT ISNULL(Quantity,0) AS Quantity,UnitQuantity,ISNULL(QuantityShipped,0) AS QuantityShipped,ISNULL(QuantityReceived,0) AS QuantityReceived FROM Project_Subcontract_PO_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (validateQuantity && dataSet != null && dataSet.Tables[0].Rows.Count > 0)
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
					float.TryParse(dataRow["QuantityShipped"].ToString(), out result2);
					float.TryParse(dataRow["QuantityReceived"].ToString(), out result3);
					if (result < result2 + result3 + quantity)
					{
						throw new CompanyException("Shipped quantity cannot be greater than ordered quantity.", 1013);
					}
				}
				result2 += quantity;
				textCommand = "UPDATE Project_Subcontract_PO_Detail SET QuantityShipped = " + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND ProductID='" + productID + "' AND RowIndex=" + rowIndex.ToString();
				return (byte)(1 & ((ExecuteNonQuery(textCommand, sqlTransaction) > 0) ? 1 : 0)) != 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateOrderStatus(string sysDocID, string voucherID, int rowIndex, int status, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			bool flag = true;
			try
			{
				string textCommand = "SELECT status FROM Project_Subcontract_PO WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' ";
				FillDataSet(dataSet, "Project_Subcontract_PO", textCommand);
				if (dataSet == null)
				{
					return flag;
				}
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					textCommand = "UPDATE Project_Subcontract_PO SET Status=" + status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
					return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				}
				return flag;
			}
			catch
			{
				return false;
			}
		}

		internal bool CloseShippedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Project_Subcontract_PO_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Project_Subcontract_PO_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) + ISNULL(QuantityShipped,0))  FROM Purchase_Order_NonInv_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null)
				{
					return true;
				}
				bool flag = false;
				if (int.Parse(obj.ToString()) > 0)
				{
					flag = true;
				}
				int num = 1;
				if (flag)
				{
					num = 2;
				}
				exp = "UPDATE Project_Subcontract_PO SET Status = " + num + ", IsShipped = '" + flag.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) >= 0;
			}
			catch
			{
				throw;
			}
		}

		public bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status)
		{
			try
			{
				string exp = "UPDATE Project_Subcontract_PO SET Status= " + (int)status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseReceivedOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Project_Subcontract_PO_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Project_Subcontract_PO_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0))  FROM Project_Subcontract_PO_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Project_Subcontract_PO SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool ReOpenOrder(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Project_Subcontract_PO_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Project_Subcontract_PO_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) ) FROM Project_Subcontract_PO_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Project_Subcontract_PO SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeleteProjectSubContractPODetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "";
				ProjectSubcontractPOData projectSubcontractPOData = new ProjectSubcontractPOData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Project_Subcontract_PO_Detail SOD INNER JOIN Project_Subcontract_PO SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(projectSubcontractPOData.ProjectSubcontractPODetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in projectSubcontractPOData.ProjectSubcontractPODetailTable.Rows)
					{
						text = row["ProductID"].ToString();
						float num = float.Parse(row["Quantity"].ToString());
						float num2 = new Products(base.DBConfig).GetOrderedQuantity(text, sqlTransaction) - num;
						if (num2 < 0f)
						{
							num2 = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text, num2, sqlTransaction);
					}
					string text2 = "";
					string text3 = "";
					foreach (DataRow row2 in projectSubcontractPOData.ProjectSubcontractPODetailTable.Rows)
					{
						text = row2["ProductID"].ToString();
						text2 = row2["SourceVoucherID"].ToString();
						text3 = row2["SourceSysDocID"].ToString();
						int result2 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row2["SourceRowIndex"].ToString(), out result2);
							float result3 = 0f;
							if (row2["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row2["UnitQuantity"].ToString(), out result3);
							}
							else
							{
								float.TryParse(row2["Quantity"].ToString(), out result3);
							}
							flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text3, text2, result2, -1f * result3, sqlTransaction);
						}
					}
				}
				textCommand = "DELETE FROM Project_Subcontract_PO_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidProjectSubContractPO(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				ProjectSubcontractPOData projectSubcontractPOData = new ProjectSubcontractPOData();
				string textCommand = "SELECT * FROM Project_Subcontract_PO_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO_Detail", textCommand, sqlTransaction);
				string text = "";
				string text2 = "";
				foreach (DataRow row in projectSubcontractPOData.ProjectSubcontractPODetailTable.Rows)
				{
					string productID = row["ProductID"].ToString();
					float result = 0f;
					if (row["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(row["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(row["Quantity"].ToString(), out result);
					}
					float num = new Products(base.DBConfig).GetOrderedQuantity(productID, sqlTransaction) - result;
					if (num < 0f)
					{
						num = 0f;
					}
					flag &= new Products(base.DBConfig).UpdateOrderedQuantity(productID, num, sqlTransaction);
					int result2 = 0;
					text = row["SourceVoucherID"].ToString();
					text2 = row["SourceSysDocID"].ToString();
					int.TryParse(row["SourceRowIndex"].ToString(), out result2);
					if (!(text == "") && !(text2 == ""))
					{
						flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text2, text, result2, -1f * result, sqlTransaction);
					}
				}
				textCommand = "UPDATE Project_Subcontract_PO SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Project SubContract", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteProjectSubContractPO(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to change or delete. Some of the items in this order has been already received.", 1037);
				}
				flag &= DeleteProjectSubContractPODetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Project_Subcontract_PO WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Project SubContract", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenOrderListReport()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT SysDocID,VoucherID,SO.VendorID,VendorName,TransactionDate,SO.BuyerID,Total\r\n                            FROM Project_Subcontract_PO SO INNER JOIN Vendor ON SO.VendorID=Vendor.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "ProjectSubContractOpenOrders", textCommand);
			return dataSet;
		}

		public DataSet GetProjectSubContractPOToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT DISTINCT SI.*,V.VendorName,B.FullName,B.Phone1 AS BPhone,B.Mobile AS BMobile,V.TaxIDNumber as VTaxIDNo,\r\n                                J.JobName AS Project,J.SiteLocationAddress AS [Site Address],\r\n                                VA.AddressPrintFormat AS VendorAddress,VA.Phone1,VA.Fax,VA.Mobile,VA.ContactName,PT.TermName,SM.ShippingMethodName,\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                TermName,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                                ISNULL(TaxAmount ,0) AS Tax,Total AS Total, ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax\r\n                                FROM  Project_Subcontract_PO SI INNER JOIN Vendor V ON SI.VendorID=V.VendorID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                                LEFT OUTER JOIN Job J ON J.JobID=SI.JobID\r\n                                 WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Project_Subcontract_PO", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Project_Subcontract_PO"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT  SysDocID,VoucherID,POD.ProductID,PC.CategoryID,PC.CategoryName,POD.Description,ISNULL(POD.Quantity,0) AS Quantity,\r\n                        P.UnitID AS [P Unit],P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,POD.UnitPrice AS UnitPrice,C.CountryName,POD.Remarks,\r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,POD.Quantity)*Factor ELSE ISNULL(UnitQuantity,POD.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=POD.UnitID AND PU.ProductID=POD.ProductID ) AS Weight,\r\n                        ISNULL(UnitQuantity,POD.Quantity)*ISNULL(POD.UnitPrice,0) AS Total,POD.UnitID, P.BrandID, POD.RowIndex + 1 RowIndex,POD.TaxAmount, POD.TaxGroupID,TG.TaxGroupName\r\n                        FROM   Project_Subcontract_PO_Detail POD\r\n                        INNER JOIN Product P ON P.ProductID = POD.ProductID\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                        LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                         LEFT JOIn Tax_Group TG ON POD.TaxGroupID=TG.TaxGroupID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Project_Subcontract_PO_Detail", cmdText);
				string str = "";
				if (dataSet.Tables["Project_Subcontract_PO"].Rows.Count > 0)
				{
					str = dataSet.Tables["Project_Subcontract_PO"].Rows[0]["BOLNo"].ToString();
				}
				cmdText = "SELECT  DISTINCT  PL.*,V.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName\r\n                        FROM  PO_Shipment PL INNER JOIN Vendor V ON PL.VendorID=V.VendorID   \r\n                        LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=PL.VendorID AND VA.AddressID='PRIMARY'     \r\n                        LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=PL.ShippingMethodID  \r\n                        WHERE PL.BOLNumber='" + str + "' AND ISNULL(PL.BOLNumber,'') <>''";
				FillDataSet(dataSet, "PO_Shipment", cmdText);
				dataSet.Relations.Add("ProjectSubContractPO", new DataColumn[2]
				{
					dataSet.Tables["Project_Subcontract_PO"].Columns["SysDocID"],
					dataSet.Tables["Project_Subcontract_PO"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Project_Subcontract_PO_Detail"].Columns["SysDocID"],
					dataSet.Tables["Project_Subcontract_PO_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Project_Subcontract_PO"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Project_Subcontract_PO"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					row["TotalInWords"] = NumToWord.GetNumInWords(result - result2);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],J.JobID [Job],J.JobName [Job Name],TransactionDate [Order Date],INV.Status,\r\n                        CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],INV.Reference,\r\n                        INV.CurrencyID Currency\r\n                        , Currency.ExchangeRate as Rate,CASE WHEN Currency.ExchangeRate!=1 THEN\r\n                        ( CASE WHEN Currency.RateType='M' THEN ROUND((INV.Total*Currency.ExchangeRate),2) ELSE ROUND((INV.Total/Currency.ExchangeRate),2) END ) ELSE INV.Total END AS Amount,\r\n                        CASE WHEN Currency.ExchangeRate!=1 THEN INV.Total Else NULL END AS AmountFC\r\n            \r\n                        FROM         Project_Subcontract_PO INV\r\n                        INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID\r\n                        LEFT OUTER JOIN Currency ON Currency.CurrencyID=INV.CurrencyID\r\n                         LEFT JOIN Job J ON J.JobID=INV.JobID WHERE 1=1 ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Project_Subcontract_PO", sqlCommand);
			return dataSet;
		}

		public DataSet XGetPendingApprovalList(string approvalID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],Reference,INV.CurrencyID Currency,Total [Amount]\r\n                            FROM         Purchase_Order_NonInv INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ");
			FillDataSet(dataSet, "Project_Subcontract_PO", sqlCommand);
			return dataSet;
		}

		public ProjectSubcontractPOData GetProjectSubContractPIByID(string sysDocID, string voucherID)
		{
			try
			{
				ProjectSubcontractPOData projectSubcontractPOData = new ProjectSubcontractPOData();
				string textCommand = "SELECT *, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n                                CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Project_SubContract_PO PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,SourceSysDocID,SourceVoucherID FROM PO_Shipment_Detail POS\r\n                                WHERE  POS.SourceSysDocID = '" + sysDocID + "' AND   POS.SourceVoucherID = '" + voucherID + "') T  ON T.SourceSysDocID = PO.SysDocID AND T.SourceVoucherID = PO.VoucherID \r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO", textCommand);
				if (projectSubcontractPOData == null || projectSubcontractPOData.Tables.Count == 0 || projectSubcontractPOData.Tables["Project_Subcontract_PO"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.IsTrackLot,Product.IsTrackSerial,Product.Weight, Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n\t\t\t\t\t\tFROM Project_SubContract_PO_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Project_Subcontract_PO_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_GRN\r\n\t\t\t\t\t\tWHERE InvoiceNumber='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Invoice_GRN", textCommand);
				textCommand = "SELECT * FROM Purchase_Invoice_NonInv_Expense\r\n\t\t\t\t\t\tWHERE InvoiceVoucherID='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Purchase_Invoice_NonInv_Expense", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(projectSubcontractPOData, "Product_Lot_Receiving_Detail", textCommand);
				return projectSubcontractPOData;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReceivedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			bool flag = true;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Project_Subcontract_PO_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					float.TryParse(dataRow["QuantityReceived"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Project_Subcontract_PO_Detail SET QuantityReceived=" + decimal.Parse(result2.ToString()) + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				return false;
			}
		}
	}
}
