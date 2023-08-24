using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseOrderNI : StoreObject
	{
		private const string PURCHASEORDER_TABLE = "Purchase_Order_NonInv";

		private const string PURCHASEORDERDETAIL_TABLE = "Purchase_Order_NonInv_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

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

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string PORTLOADING_PARM = "@PortLoading";

		private const string PORTDESTINATION_PARM = "@PortDestination";

		private const string ETA_PARM = "@ETA";

		private const string ETD_PARM = "@ETD";

		private const string ACTUALREQDATE_PARM = "@ActualReqDate";

		private const string INCOID_PARM = "@INCOID";

		private const string BOLNO_PARM = "@BOLNo";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

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

		public PurchaseOrderNI(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseOrderText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Order_NonInv", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("Discount", "@Discount"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Total", "@Total"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("PortLoading", "@PortLoading"), new FieldValue("PortDestination", "@PortDestination"), new FieldValue("ETA", "@ETA"), new FieldValue("ETD", "@ETD"), new FieldValue("ActualReqDate", "@ActualReqDate"), new FieldValue("INCOID", "@INCOID"), new FieldValue("BOLNo", "@BOLNo"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Note", "@Note"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Order_NonInv", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseOrderCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseOrderText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseOrderText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@PortLoading", SqlDbType.NVarChar);
			parameters.Add("@PortDestination", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ETD", SqlDbType.DateTime);
			parameters.Add("@ActualReqDate", SqlDbType.DateTime);
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
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
			parameters["@PortLoading"].SourceColumn = "PortLoading";
			parameters["@PortDestination"].SourceColumn = "PortDestination";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ETD"].SourceColumn = "ETD";
			parameters["@ActualReqDate"].SourceColumn = "ActualReqDate";
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
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
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

		private string GetInsertUpdatePurchaseOrderDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Order_NonInv_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("Remarks", "@Remarks"), new FieldValue("JobID", "@JobID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseOrderDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseOrderDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseOrderDetailsText(isUpdate: false), base.DBConfig.Connection);
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
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
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
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PurchaseOrderNIData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Purchase_Order_NonInv_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityReceived,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdatePurchaseOrder(PurchaseOrderNIData purchaseOrderData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePurchaseOrderCommand = GetInsertUpdatePurchaseOrderCommand(isUpdate);
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = purchaseOrderData.PurchaseOrderTable.Rows[0];
				string text = "";
				string text2 = "";
				string text3 = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (isUpdate && !CanUpdate(sysDocID, text4, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				decimal num = default(decimal);
				foreach (DataRow row in purchaseOrderData.PurchaseOrderDetailTable.Rows)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Order_NonInv", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row2 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text3 = row2["ProductID"].ToString();
					string text5 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text5 = fieldValue.ToString();
					}
					if (text5 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text5)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text3, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text6 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text6;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text6 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
				}
				insertUpdatePurchaseOrderCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseOrderData, "Purchase_Order_NonInv", insertUpdatePurchaseOrderCommand)) : (flag & Insert(purchaseOrderData, "Purchase_Order_NonInv", insertUpdatePurchaseOrderCommand)));
				insertUpdatePurchaseOrderCommand = GetInsertUpdatePurchaseOrderDetailsCommand(isUpdate: false);
				insertUpdatePurchaseOrderCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePurchaseOrderDetailsRows(sysDocID, text4, sqlTransaction);
				}
				if (purchaseOrderData.Tables["Purchase_Order_NonInv_Detail"].Rows.Count > 0)
				{
					flag &= Insert(purchaseOrderData, "Purchase_Order_NonInv_Detail", insertUpdatePurchaseOrderCommand);
				}
				foreach (DataRow row3 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
				{
					text3 = row3["ProductID"].ToString();
					float num5 = float.Parse(row3["Quantity"].ToString());
					float quantity = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) + num5;
					flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text3, quantity, sqlTransaction);
				}
				if (itemSourceTypes == ItemSourceTypes.PurchaseQuote)
				{
					foreach (DataRow row4 in purchaseOrderData.PurchaseOrderDetailTable.Rows)
					{
						text3 = row4["ProductID"].ToString();
						object obj4 = row4["SourceDocType"].ToString();
						if (!(obj4.ToString() == ""))
						{
							itemSourceTypes = (ItemSourceTypes)byte.Parse(obj4.ToString());
							if (itemSourceTypes == ItemSourceTypes.PurchaseQuote)
							{
								text = row4["SourceVoucherID"].ToString();
								text2 = row4["SourceSysDocID"].ToString();
								int result3 = 0;
								if (!(text == "") && !(text2 == ""))
								{
									int.TryParse(row4["SourceRowIndex"].ToString(), out result3);
									float result4 = 0f;
									if (row4["UnitQuantity"] != DBNull.Value)
									{
										float.TryParse(row4["UnitQuantity"].ToString(), out result4);
									}
									else
									{
										float.TryParse(row4["Quantity"].ToString(), out result4);
									}
									flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text2, text, result3, result4, sqlTransaction);
								}
							}
						}
					}
					if (text != "")
					{
						flag &= new PurchaseQuote(base.DBConfig).CloseOrderedQuote(text2, text, sqlTransaction);
					}
				}
				if (purchaseOrderData.Tables.Contains("Tax_Detail") && purchaseOrderData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseOrderData, sysDocID, text4, isUpdate, sqlTransaction);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Order_NonInv", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Order Non Inv";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Order_NonInv", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseOrderNI, sysDocID, text4, "Purchase_Order_NonInv", sqlTransaction);
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
				string textCommand = "SELECT PO.CurrencyID, PO.TermID,PO.Total,PO.ETA, ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount                                           \r\n                                FROM Purchase_Order_NonInv PO  WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Purchase_Order_NonInv", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseOrderNIData GetPurchaseOrderByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseOrderNIData purchaseOrderNIData = new PurchaseOrderNIData();
				string textCommand = "SELECT *,PT.TermName, ISNULL((SELECT SUM(Amount) FROM Payment_Request WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS PaidAmount ,\r\n            ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) FROM Purchase_Prepayment_Invoice PPI WHERE PPI.SourceSysDocID = PO.SysDocID AND PPI.SourceVoucherID = PO.VoucherID),0)\r\n             AS PrePaidAmount,\r\n                                            CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,T.SysDocID AS PKSysDocID,T.VoucherID AS PKVoucherID\r\n                                FROM Purchase_Order_NonInv PO LEFT OUTER JOIN (SELECT TOP 1 SysDocID,VoucherID,OrderSysDocID,OrderVoucherID FROM Purchase_Invoice_NonInv_Detail POS\r\n                                WHERE  POS.OrderSysDocID = '" + sysDocID + "' AND   POS.OrderVoucherID = '" + voucherID + "') T  ON T.OrderSysDocID = PO.SysDocID AND T.OrderVoucherID = PO.VoucherID \r\n                                LEFT OUTER JOIN Payment_Term PT ON PT.PaymentTermID = PO.TermID\r\n\t\t\t\t\t\t\t\tWHERE PO.VoucherID='" + voucherID + "' AND PO.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv", textCommand);
				if (purchaseOrderNIData == null || purchaseOrderNIData.Tables.Count == 0 || purchaseOrderNIData.Tables["Purchase_Order_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, Product.IsTrackLot, Product.IsTrackSerial\r\n                        FROM Purchase_Order_NonInv_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderNIData, "Tax_Detail", textCommand);
				return purchaseOrderNIData;
			}
			catch
			{
				throw;
			}
		}

		public PurchaseOrderNIData GetPurchaseOrderServiceItemByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseOrderNIData purchaseOrderNIData = new PurchaseOrderNIData();
				string textCommand = "SELECT top 1 *,V.VendorName,CASE WHEN (SELECT COUNT(*) FROM PO_Shipment_Detail PR \r\n                                                                WHERE PO.SysDocID = PR.SourceSysDocID AND PO.VoucherID = PR.SourceVoucherID)>0 THEN 'True' ELSE 'False' END  AS IsPOShipped ,PO.SysDocID AS PKSysDocID,PO.VoucherID AS PKVoucherID, PO.ClearingAgent, \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tPO.ContainerNumber, PO.Port, PO.LoadingPort, PO.ATD, PO.ETA, PO.Note, PO.TransporterID , PO.BuyerID, PO.ShippingMethodID, PO.Shipper\r\n                                FROM Purchase_Order_NonInv PON LEFT JOIN Vendor V ON V.VendorID=PON.VendorID  \r\n\t\t\t\t\t\t\t\tLEFT JOIN Buyer ON PON.BuyerID=Buyer.BuyerID\r\n\t\t\t\t\t\t\t\tLEFT JOIN  PO_Shipment PO ON PO.BOLNumber=PON.BOLNo\r\n\t\t\t\t\t\t\t\tWHERE PON.VoucherID='" + voucherID + "' AND PON.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv", textCommand);
				if (purchaseOrderNIData == null || purchaseOrderNIData.Tables.Count == 0 || purchaseOrderNIData.Tables["Purchase_Order_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*, Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID, PO.Total,TD.UnitPrice,(TD.Quantity*TD.UnitPrice) as Amount,\r\n                        Product.IsTrackLot, Product.IsTrackSerial,Product.ExpenseCode [Expense Code],\r\n                        ex.Description[Expense Description] FROM Purchase_Order_NonInv_Detail TD \r\n\t\t\t\t\t\tLEFT JOIN Purchase_Order_NonInv PO On TD.SysDocID=po.SysDocID and TD.VoucherID=po.VoucherID\r\n\t\t\t\t\t\tINNER JOIN Product ON TD.ProductID=Product.ProductID LEFT JOIN Expense_Code EX ON Product.ExpenseCode = EX.ExpenseID \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv_Detail", textCommand);
				return purchaseOrderNIData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseOrderDetailReport(DateTime from, DateTime to, string jobID, string vendorID, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = CommonLib.ToSqlDateTimeString(from);
				string text2 = CommonLib.ToSqlDateTimeString(to);
				string text3 = "SELECT DISTINCT PO.TransactionDate,PO.VoucherID,V.VendorName,J.JobName,POD.UnitPrice,POD.ProductID,POD.UnitID,POD.Description,POD.Quantity,\r\n                                POD.QuantityReceived,POD.UnitPrice  FROM Purchase_Order_NonInv PO LEFT JOIN Purchase_Order_NonInv_Detail POD ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                LEFT JOIN JOB J ON J.JobID=POD.JobID LEFT JOIN Vendor V ON V.VendorID=PO.VendorID LEFT JOIN Product P ON POD.ProductID=P.ProductID\r\n                                LEFT JOIN Product ON POD.ProductID=Product.ProductID ";
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
				FillDataSet(dataSet, "Purchase_Order_NonInv", text3);
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
				string text = "SELECT PO.SysDocID [Doc ID],PO.VoucherID [Number],V.ParentVendorID, TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor] FROM Purchase_Order_NonInv PO\r\n                             INNER JOIN Vendor V ON PO.VendorID=V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsShipped,'False')='False'  AND Status = 1 AND ISNULL(IsImport,'False')='" + isImport.ToString() + "'";
				if (vendorID != "")
				{
					text = text + " AND (PO.VendorID='" + vendorID + "' OR V.ParentVendorID = '" + vendorID + "')";
				}
				FillDataSet(dataSet, "Purchase_Order_NonInv", text);
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
				string textCommand = "SELECT PO.VendorID,Ven.ParentVendorID, POD.SysDocID [Doc ID], POD.VoucherID AS [PO Number],ProductID,Description,POD.UnitID,RowIndex,UnitQuantity,\r\n                                ISNULL(UnitQuantity,Quantity) AS Quantity,ISNULL(QuantityShipped,0) AS QuantityShipped,\r\n                                ISNULL(SubunitPrice,UnitPrice) AS UnitPrice,ISNULL(QuantityReceived,0) AS QuantityReceived, PO.ShippingMethodID, PO.ETA, PO.PortDestination \r\n                                FROM Purchase_Order_NonInv_Detail POD INNER JOIN Purchase_Order_NonInv PO ON PO.SysDocID=POD.SysDocID AND PO.VoucherID=POD.VoucherID\r\n                                INNER JOIN Vendor VEN ON VEN.VendorID=PO.VendorID\r\n                              \r\n                             WHERE POD.SysDocID='" + sysDocID + "' AND POD.VoucherID='" + voucherID + "'";
				FillDataSet(dataSet, "Purchase_Order_NonInv_Detail", textCommand);
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
				string str = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],SO.Reference,J.JobName,\r\n                                Total AS Amount \r\n                                 FROM Purchase_Order_NonInv SO\r\n                                INNER JOIN Vendor C ON SO.VendorID=C.VendorID\r\n                                LEFT OUTER JOIN Job J On J.JobID=SO.JobID WHERE ISNULL(IsVoid,'False')='False' AND SO.Status=1 ";
				if (vendorID != "")
				{
					str = str + " AND SO.VendorID='" + vendorID + "'";
				}
				str = str + " AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "'";
				FillDataSet(dataSet, "Purchase_Order_NonInv", str);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenOrderServiceItemSummary()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order_NonInv SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False'";
				FillDataSet(dataSet, "Purchase_Order_NonInv", textCommand);
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
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order_NonInv PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1 ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Order_NonInv", text);
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
				string text = "SELECT PO.VoucherID  ,PO.SysDocID  , TransactionDate AS [Date],PO.VendorID + '-' + V.VendorName AS [Vendor],ETA as [ETA],Reference,Total AS Amount,\r\n                                ISNULL((SELECT SUM(Amount) FROM Payment_Request PR WHERE POSysDocID = PO.SysDocID AND POVoucherID = PO.VoucherID),0) AS Paid\r\n                                 FROM Purchase_Order_NonInv PO\r\n                                     INNER JOIN Vendor V ON PO.VendorID= V.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status IN (1,2,3) ";
				if (vendorID != "")
				{
					text = text + " AND PO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Order_NonInv", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseOrderAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor],Reference,Total AS Amount  FROM Purchase_Order_NonInv SO\r\n                             INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				FillDataSet(dataSet, "Purchase_Order_NonInv", textCommand);
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
				string textCommand = "SELECT ISNULL(Quantity,0) AS Quantity,UnitQuantity,ISNULL(QuantityShipped,0) AS QuantityShipped,ISNULL(QuantityReceived,0) AS QuantityReceived FROM Purchase_Order_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Purchase_Order_NonInv_Detail SET QuantityShipped = " + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND ProductID='" + productID + "' AND RowIndex=" + rowIndex.ToString();
				return (byte)(1 & ((ExecuteNonQuery(textCommand, sqlTransaction) > 0) ? 1 : 0)) != 0;
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Order_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Purchase_Order_NonInv_Detail SET QuantityReceived=" + decimal.Parse(result2.ToString()) + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_NonInv_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_NonInv_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) + ISNULL(QuantityShipped,0))  FROM Purchase_Order_NonInv_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
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
				exp = "UPDATE Purchase_Order_NonInv SET Status = " + num + ", IsShipped = '" + flag.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string exp = "UPDATE Purchase_Order_NonInv SET Status= " + (int)status + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_NonInv_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_NonInv_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0))  FROM Purchase_Order_NonInv_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order_NonInv SET Status= 2 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string exp = "SELECT COUNT(RowIndex)FROM Purchase_Order_NonInv_Detail POD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                 AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Purchase_Order_NonInv_Detail POD2 WHERE POD.SysDocID=POD2.SysDocID AND POD.VoucherID=POD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityReceived,0) ) FROM Purchase_Order_NonInv_Detail POD3 WHERE POD.SysDocID=POD3.SysDocID AND POD.VoucherID=POD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Purchase_Order_NonInv SET Status= 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool DeletePurchaseOrderDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string text = "";
				PurchaseOrderNIData purchaseOrderNIData = new PurchaseOrderNIData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Purchase_Order_NonInv_Detail SOD INNER JOIN Purchase_Order_NonInv SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseOrderNIData.PurchaseOrderDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in purchaseOrderNIData.PurchaseOrderDetailTable.Rows)
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
					foreach (DataRow row2 in purchaseOrderNIData.PurchaseOrderDetailTable.Rows)
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
							if (int.Parse(new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", text3, sqlTransaction).ToString()) == 30)
							{
								flag &= new PurchaseQuote(base.DBConfig).UpdateRowOrderedQuantity(text3, text2, result2, -1f * result3, sqlTransaction);
							}
						}
					}
				}
				textCommand = "DELETE FROM Purchase_Order_NonInv_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (!CanUpdate(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this order has been already received.", 1037);
				}
				PurchaseOrderNIData purchaseOrderNIData = new PurchaseOrderNIData();
				string textCommand = "SELECT * FROM Purchase_Order_NonInv_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseOrderNIData, "Purchase_Order_NonInv_Detail", textCommand, sqlTransaction);
				string text = "";
				string text2 = "";
				foreach (DataRow row in purchaseOrderNIData.PurchaseOrderDetailTable.Rows)
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
				textCommand = "UPDATE Purchase_Order_NonInv SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Purchase Order", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePurchaseOrder(string sysDocID, string voucherID)
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
				flag &= DeletePurchaseOrderDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Order_NonInv WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Order", voucherID, sysDocID, activityType, sqlTransaction);
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
			string textCommand = "SELECT SysDocID,VoucherID,SO.VendorID,VendorName,TransactionDate,SO.BuyerID,Total\r\n                            FROM Purchase_Order_NonInv SO INNER JOIN Vendor ON SO.VendorID=Vendor.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
			FillDataSet(dataSet, "Orders", textCommand);
			return dataSet;
		}

		public DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT DISTINCT SI.*,V.VendorName,B.FullName,B.Phone1 AS BPhone,B.Mobile AS BMobile,\r\n                                (SELECT COUNT(PL.BOLNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'') AS [BOL_PL],\r\n                                (SELECT COUNT(PL.ContainerNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNo) AS [BOL_Con],\r\n                                (SELECT SUM(PLD.Quantity) FROM PO_Shipment_Detail PLD INNER JOIN PO_Shipment PL ON PLD.SysDocID=PL.SysDocID \r\n                                AND PLD.VoucherID=PL.VoucherID WHERE PL.BOLNumber=SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'')AS [No. Box],\r\n                                (SELECT SUBSTRING((SELECT DISTINCT ',' + PC.CategoryName \r\n                                FROM PO_Shipment_Detail PLD INNER JOIN \r\n                                PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                                INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                                LEFT OUTER JOIN Product_Category PC ON P.CategoryID=PC.CategoryID\r\n                                WHERE PL.BOLNumber= SI.BOLNo FOR XML PATH('')),2,20000) )AS  Category,\r\n                                (SELECT SUBSTRING((SELECT DISTINCT ',' + C.CountryName \r\n                                FROM PO_Shipment_Detail PLD INNER JOIN \r\n                                PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                                INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                                LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                                WHERE PL.BOLNumber= SI.BOLNo FOR XML PATH('')),2,20000) )AS Orgin,\r\n                                (SELECT  TOP 1  PL.VendorID +' - '+V.VendorName FROM  PO_Shipment PL INNER JOIN Vendor V ON \r\n                                PL.VendorID=V.VendorID WHERE PL.BOLNumber= SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'')AS [Supplier],\r\n                                (SELECT TOP 1 J.JobName FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID WHERE SysDocID = \r\n                              '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS Project,\r\n                              (SELECT TOP 1 J.SiteLocationAddress FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID WHERE SysDocID = \r\n                              '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS [Site Address],\r\n                              VA.AddressPrintFormat AS VendorAddress,VA.Phone1,VA.Fax,VA.Mobile,VA.ContactName,PT.TermName,SM.ShippingMethodName,\r\n                            ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            TermName,ISNULL(Total,0) - ISNULL(Discount,0) AS GrandTotal,\r\n                            ISNULL(TaxAmount ,0) AS Tax,Total AS Total,\r\n                            (SELECT TOP 1 J.JobID FROM Job J LEFT JOIN Purchase_Order_NonInv_Detail PRD ON J.JobID=PRD.JobID \r\n                             WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")) AS JobID\r\n                              FROM  Purchase_Order_NonInv SI INNER JOIN Vendor V ON SI.VendorID=V.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Order_NonInv", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Order_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT  SysDocID,VoucherID,POD.ProductID,PC.CategoryID,PC.CategoryName,POD.Description,ISNULL(UnitQuantity,POD.Quantity) AS Quantity,\r\n                        P.UnitID AS [P Unit],P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,POD.UnitPrice AS UnitPrice,C.CountryName,POD.Remarks,\r\n                        (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,POD.Quantity)*Factor ELSE ISNULL(UnitQuantity,POD.Quantity)/Factor END FROM Product_Unit PU WHERE PU.UnitID=POD.UnitID AND PU.ProductID=POD.ProductID ) AS Weight,\r\n                        ISNULL(UnitQuantity,POD.Quantity)*ISNULL(POD.UnitPrice,0) AS Total,POD.UnitID, P.BrandID, POD.RowIndex + 1 RowIndex\r\n                        FROM   Purchase_Order_NonInv_Detail POD\r\n                        INNER JOIN Product P ON P.ProductID = POD.ProductID\r\n                        LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                        LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Purchase_Order_NonInv_Detail", cmdText);
				string str = "";
				if (dataSet.Tables["Purchase_Order_NonInv"].Rows.Count > 0)
				{
					str = dataSet.Tables["Purchase_Order_NonInv"].Rows[0]["BOLNo"].ToString();
				}
				cmdText = "SELECT  DISTINCT  PL.*,V.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName\r\n                        FROM  PO_Shipment PL INNER JOIN Vendor V ON PL.VendorID=V.VendorID   \r\n                        LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=PL.VendorID AND VA.AddressID='PRIMARY'     \r\n                        LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=PL.ShippingMethodID  \r\n                        WHERE PL.BOLNumber='" + str + "' AND ISNULL(PL.BOLNumber,'') <>''";
				FillDataSet(dataSet, "PO_Shipment", cmdText);
				dataSet.Relations.Add("PurchaseOrderNI", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Order_NonInv"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Order_NonInv"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Order_NonInv_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Order_NonInv_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Order_NonInv"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Order_NonInv"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["Tax"].ToString(), out result3);
					row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result - result2 + result3);
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
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],INV.BOLNo [BOL Number],TransactionDate [Order Date],INV.Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],INV.Reference AS Ref1,INV.CurrencyID Currency,Total - ISNULL(Discount,0) AS [Amount],INV.Reference2 AS Ref2,J.JobID,J.JobName, ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge'  END) ,(CASE Vendor.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END))AS TAXOPTION,INV.TaxAmount\r\n                            FROM         Purchase_Order_NonInv INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 AND ISNULL(IsImport,'False') = '" + isImport.ToString() + "' ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Order_NonInv", sqlCommand);
			return dataSet;
		}

		public DataSet XGetPendingApprovalList(string approvalID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Order Date],Status,\r\n                            CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Local' END AS [Type],INV.BuyerID [Buyer],Reference,INV.CurrencyID Currency,Total [Amount]\r\n                            FROM         Purchase_Order_NonInv INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID WHERE 1=1 ");
			FillDataSet(dataSet, "Purchase_Order_NonInv", sqlCommand);
			return dataSet;
		}

		public decimal GetPODueAmount(string sysDocID, string voucherID)
		{
			try
			{
				DateTime dateTime = DateTime.MinValue;
				decimal d = default(decimal);
				DataSet dataSet = new DataSet();
				DataSet dataSet2 = new DataSet();
				string textCommand = "SELECT PaymentTermID, ISNULL(isInstallments,'False') AS Ignore ,NetDays,100 AS Percentage,TermType FROM Payment_Term PT INNER JOIN Purchase_Order_NonInv PO \r\n                                    ON PO.TermID = PT.PaymentTermID WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'\r\n                                    UNION\r\n                                    SELECT PaymentTermID, 'False' AS Ignore ,Days AS NetDays,Percentage,TermType FROM Payment_Term_Installment PTI INNER JOIN Purchase_Order_NonInv PO \r\n                                    ON PO.TermID = PTI.PaymentTermID  WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "Term", textCommand);
				decimal d2 = default(decimal);
				textCommand = "SELECT PO.TransactionDate PODate, Total POTotal,BL.TransactionDate BLDate,ATD,BL.ETA FROM Purchase_Order_NonInv PO \r\n                            LEFT OUTER JOIN Bill_Of_Lading BL ON PO.SysDocID = BL.SourceSysDocID AND PO.VoucherID = BL.SourceVoucherID\r\n                            WHERE PO.SysDocID = '" + sysDocID + "' AND PO.VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet2, "PO", textCommand);
				DataRow[] array = dataSet.Tables["Term"].Select("TermType = " + 3);
				if (array.Length != 0)
				{
					if (dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0)
					{
						DataRow dataRow = dataSet2.Tables["PO"].Rows[0];
						d2 = decimal.Parse(dataRow["POTotal"].ToString());
						dateTime = DateTime.Parse(dataRow["PODate"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj in array2)
					{
						int num = int.Parse(obj["NetDays"].ToString());
						decimal d3 = decimal.Parse(obj["Percentage"].ToString());
						if (dateTime.AddDays(num) < DateTime.Today.EndOfDay())
						{
							d += d2 * d3 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 8);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["BLDate"] != DBNull.Value)
				{
					DataRow dataRow2 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime2 = DateTime.MinValue;
					_ = DateTime.MinValue;
					_ = DateTime.MinValue;
					if (!dataRow2["BLDate"].IsDBNullOrEmpty())
					{
						dateTime2 = DateTime.Parse(dataRow2["BLDate"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj2 in array2)
					{
						int num2 = int.Parse(obj2["NetDays"].ToString());
						decimal d4 = decimal.Parse(obj2["Percentage"].ToString());
						if (dateTime2.AddDays(num2) < DateTime.Today.EndOfDay())
						{
							d += d2 * d4 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 4);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ATD"] != DBNull.Value)
				{
					DataRow dataRow3 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime3 = DateTime.MinValue;
					if (!dataRow3["ATD"].IsDBNullOrEmpty())
					{
						dateTime3 = DateTime.Parse(dataRow3["ATD"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj3 in array2)
					{
						int num3 = int.Parse(obj3["NetDays"].ToString());
						decimal d5 = decimal.Parse(obj3["Percentage"].ToString());
						if (dateTime3.AddDays(num3) < DateTime.Today.EndOfDay())
						{
							d += d2 * d5 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 6);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ETA"] != DBNull.Value)
				{
					DataRow dataRow4 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime4 = DateTime.MinValue;
					if (!dataRow4["ETA"].IsDBNullOrEmpty())
					{
						dateTime4 = DateTime.Parse(dataRow4["ETA"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj4 in array2)
					{
						int num4 = int.Parse(obj4["NetDays"].ToString());
						decimal d6 = decimal.Parse(obj4["Percentage"].ToString());
						if (dateTime4.AddDays(-1 * num4) < DateTime.Today.EndOfDay())
						{
							d += d2 * d6 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 7);
				if (array.Length != 0 && dataSet2 != null && dataSet2.Tables["PO"].Rows.Count > 0 && dataSet2.Tables["PO"].Rows[0]["ETA"] != DBNull.Value)
				{
					DataRow dataRow5 = dataSet2.Tables["PO"].Rows[0];
					DateTime dateTime5 = DateTime.MinValue;
					if (!dataRow5["ETA"].IsDBNullOrEmpty())
					{
						dateTime5 = DateTime.Parse(dataRow5["ETA"].ToString());
					}
					DataRow[] array2 = array;
					foreach (DataRow obj5 in array2)
					{
						int num5 = int.Parse(obj5["NetDays"].ToString());
						decimal d7 = decimal.Parse(obj5["Percentage"].ToString());
						if (dateTime5.AddDays(num5) < DateTime.Today.EndOfDay())
						{
							d += d2 * d7 / 100m;
						}
					}
				}
				array = dataSet.Tables["Term"].Select("TermType = " + 9);
				if (array.Length != 0)
				{
					textCommand = "SELECT TOP 1 GRNDate FROM (\r\n                                 SELECT PR.TransactionDate AS GRNDate FROM Purchase_Receipt  PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n                                 INNER JOIN PO_Shipment_Detail POSD ON POSD.SysDocID = PRD.PKSysDocID AND POSD.VoucherID = PRD.PKVoucherID\r\n                                 WHERE POSD.SourceSysDocID = '" + sysDocID + "' AND POSD.SourceVoucherID = '" + voucherID + "'\r\n                                 UNION \r\n                                 SELECT PR.TransactionDate AS GRNDate FROM Purchase_Receipt  PR INNER JOIN Purchase_Receipt_Detail PRD ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n                                 WHERE PR.POSysDocID = '" + sysDocID + "' AND PR.POVoucherID = '" + voucherID + "') GRN";
					object obj6 = ExecuteScalar(textCommand);
					if (!obj6.IsNullOrEmpty())
					{
						DateTime dateTime6 = DateTime.Parse(obj6.ToString());
						DataRow[] array2 = array;
						foreach (DataRow obj7 in array2)
						{
							int num6 = int.Parse(obj7["NetDays"].ToString());
							decimal d8 = decimal.Parse(obj7["Percentage"].ToString());
							if (dateTime6.AddDays(num6) < DateTime.Today.EndOfDay())
							{
								d += d2 * d8 / 100m;
							}
						}
					}
				}
				textCommand = "SELECT ISNULL(SUM(ISNULL(AmountFC,Amount)),0) AS Amount FROM Purchase_Prepayment_Invoice PPI WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherID + "'";
				decimal d9 = default(decimal);
				object obj8 = ExecuteScalar(textCommand);
				if (!obj8.IsNullOrEmpty())
				{
					d9 = decimal.Parse(obj8.ToString());
				}
				return d - d9;
			}
			catch
			{
				throw;
			}
		}
	}
}
