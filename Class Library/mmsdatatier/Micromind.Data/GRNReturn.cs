using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class GRNReturn : StoreObject
	{
		private const string GRNRETURN_TABLE = "GRN_Return";

		private const string GRNRETURNDETAIL_TABLE = "GRN_Return_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string ISCASH_PARM = "@IsCash";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYER_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string VENDORADDRESS_PARM = "@VendorAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		public GRNReturn(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateGRNReturnText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("GRN_Return", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("VendorAddress", "@VendorAddress"), new FieldValue("Status", "@Status"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("GRN_Return", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGRNReturnCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGRNReturnText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGRNReturnText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@VendorAddress", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@VendorAddress"].SourceColumn = "VendorAddress";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
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

		private string GetInsertUpdateGRNReturnDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("GRN_Return_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGRNReturnDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateGRNReturnDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateGRNReturnDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Amount", "@Amount"), new FieldValue("PaymentMethodType", "@PaymentMethodType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(GRNReturnData journalData)
		{
			return true;
		}

		public bool InsertUpdateGRNReturn(GRNReturnData grnReturnData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateGRNReturnCommand = GetInsertUpdateGRNReturnCommand(isUpdate);
			string text = "";
			try
			{
				DataRow dataRow = grnReturnData.GRNReturnTable.Rows[0];
				string text2 = "";
				string text3 = "";
				string text4 = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text5 = dataRow["VoucherID"].ToString();
				string text6 = dataRow["SysDocID"].ToString();
				flag &= ValidateDate(grnReturnData, isUpdate, sqlTransaction);
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (itemSourceTypes != 0)
				{
					DataRow dataRow2 = grnReturnData.GRNReturnDetailTable.Rows[0];
					text3 = dataRow2["SourceVoucherID"].ToString();
					text2 = dataRow2["SourceSysDocID"].ToString();
					text4 = "SELECT SUM(ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0)) ";
					if (isUpdate)
					{
						text4 = text4 + " + ISNULL((SELECT SUM(Quantity) FROM GRN_Return_Detail GRND WHERE GRND.SysDocID = '" + text6 + "' AND GRND.VoucherID = '" + text5 + "') ,0)";
					}
					text4 = text4 + "        AS TotalReceived\r\n                                FROM Purchase_Receipt_Detail PRD WHERE SysDocID = '" + text2 + "' AND VoucherID = '" + text3 + "'";
					object obj = ExecuteScalar(text4, sqlTransaction);
					float num = 0f;
					if (obj != null && obj.ToString() != "")
					{
						num = float.Parse(obj.ToString());
					}
					float num2 = 0f;
					foreach (DataRow row in grnReturnData.GRNReturnDetailTable.Rows)
					{
						num2 = ((row["UnitQuantity"] == DBNull.Value) ? (num2 + float.Parse(row["Quantity"].ToString())) : (num2 + float.Parse(row["UnitQuantity"].ToString())));
					}
					if (num2 > num)
					{
						throw new CompanyException("Total returned quantity cannot be more than received quantity.");
					}
				}
				if (!isUpdate)
				{
					if (new SystemDocuments(base.DBConfig).ExistDocumentNumber("GRN_Return", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
					{
						throw new CompanyException("Document number already exist.", 1046);
					}
				}
				else if (!AllowModify(text6, text5, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modifed because it is refered by other transactions.");
				}
				text4 = "SELECT SUM(ISNULL(UnitQuantity,Quantity) - ISNULL(QuantityReturned,0)) + \r\n                                ISNULL((SELECT SUM(Quantity) FROM GRN_Return_Detail GRND WHERE GRND.SysDocID = '' AND GRND.VoucherID = ''),0) \r\n                                AS TotalReceived\r\n                                FROM Purchase_Receipt_Detail PRD WHERE SysDocID = '' AND VoucherID = ''";
				foreach (DataRow row2 in grnReturnData.GRNReturnDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text = row2["ProductID"].ToString();
					string text7 = "";
					object obj = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text, sqlTransaction);
					if (obj != null)
					{
						text7 = obj.ToString();
					}
					if (text7 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text7)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text8 = obj2["FactorType"].ToString();
						float num4 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text8;
						row2["UnitQuantity"] = row2["Quantity"];
						num4 = ((!(text8 == "M")) ? float.Parse(Math.Round(num4 * num3, 5).ToString()) : float.Parse(Math.Round(num4 / num3, 5).ToString()));
						row2["Quantity"] = num4;
					}
				}
				insertUpdateGRNReturnCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(grnReturnData, "GRN_Return", insertUpdateGRNReturnCommand)) : (flag & Insert(grnReturnData, "GRN_Return", insertUpdateGRNReturnCommand)));
				insertUpdateGRNReturnCommand = GetInsertUpdateGRNReturnDetailsCommand(isUpdate: false);
				insertUpdateGRNReturnCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteGRNReturnDetailsRows(text6, text5, isDeletingTransaction: false, sqlTransaction);
				}
				if (grnReturnData.Tables["GRN_Return_Detail"].Rows.Count > 0)
				{
					flag &= Insert(grnReturnData, "GRN_Return_Detail", insertUpdateGRNReturnCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row3 in grnReturnData.GRNReturnDetailTable.Rows)
				{
					DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["SysDocID"] = row3["SysDocID"];
					dataRow6["VoucherID"] = row3["VoucherID"];
					if (row3["LocationID"].ToString() == "")
					{
						throw new Exception("Location cannot be empty.");
					}
					dataRow6["LocationID"] = row3["LocationID"];
					dataRow6["ProductID"] = row3["ProductID"];
					dataRow6["Quantity"] = -1m * decimal.Parse(row3["Quantity"].ToString());
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["SysDocType"] = (byte)95;
					dataRow6["TransactionDate"] = dataRow["TransactionDate"];
					dataRow6["TransactionType"] = (byte)4;
					dataRow6["UnitPrice"] = 0;
					dataRow6["RowIndex"] = row3["RowIndex"];
					dataRow6["SpecificationID"] = row3["SpecificationID"];
					dataRow6["StyleID"] = row3["StyleID"];
					dataRow6["PayeeType"] = "V";
					dataRow6["PayeeID"] = dataRow["VendorID"];
					dataRow6["RefSysDocID"] = row3["SourceSysDocID"];
					dataRow6["RefVoucherID"] = row3["SourceVoucherID"];
					dataRow6["RefRowIndex"] = row3["SourceRowIndex"];
					dataRow6["CompanyID"] = dataRow["CompanyID"];
					dataRow6["DivisionID"] = dataRow["DivisionID"];
					dataRow6.EndEdit();
					inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
				}
				inventoryTransactionData.Merge(grnReturnData.Tables["Product_Lot_Issue_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(grnReturnData, isUpdate: false, sqlTransaction);
				flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				if (itemSourceTypes == ItemSourceTypes.GRN)
				{
					new PurchaseReceipt(base.DBConfig).UpdateGRNReturnedQuantity(text2, text3, sqlTransaction);
				}
				text4 = " UPDATE Inventory_Transactions SET  \r\n                                ReturnedQuantity=Quantity, RefTransactionID = (SELECT TransactionID FROM Inventory_Transactions IT2 WHERE IT2.SysDocID = IT.RefSysDocID AND IT2.VoucherID = IT.RefVoucherID AND IT2.RowIndex = IT.RefRowIndex)\r\n                                FROM Inventory_Transactions IT\r\n                                 WHERE IT.SysDocID = '" + text6 + "' AND IT.VoucherID = '" + text5 + "'";
				flag &= (new InventoryTransaction(base.DBConfig).ExecuteNonQuery(text4, sqlTransaction) >= 0);
				flag &= UpdateInventoryTransactionRowID(text6, text5, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("GRN_Return", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "GRN Return";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "GRN_Return", "VoucherID", sqlTransaction);
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

		private bool ValidateDate(GRNReturnData grnReturnData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(DateTime.Parse(DateTime.Parse(grnReturnData.GRNReturnTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString()));
				if (grnReturnData.GRNReturnDetailTable.Rows.Count <= 0)
				{
					return false;
				}
				DataRow dataRow = grnReturnData.GRNReturnDetailTable.Rows[0];
				string text2 = dataRow["SourceSysDocID"].ToString();
				string text3 = dataRow["SourceVoucherID"].ToString();
				string exp = "Select Case When CONVERT(date,TransactionDate) > CONVERT(date,'" + text + "') Then 'True' Else 'False' End From Purchase_Receipt Where SysDocID='" + text2 + "' AND VoucherID='" + text3 + "'";
				if (bool.Parse(ExecuteScalar(exp, sqlTransaction).ToString()))
				{
					throw new CompanyException("Selected GRN date is greater than return date.");
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) \r\n                                    FROM GRN_Return_Detail SID  \r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		public GRNReturnData GetGRNReturnByID(string sysDocID, string voucherID)
		{
			return GetGRNReturnByID(sysDocID, voucherID, null);
		}

		internal GRNReturnData GetGRNReturnByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				GRNReturnData gRNReturnData = new GRNReturnData();
				string text = "SELECT * FROM GRN_Return WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				new SqlCommand(text);
				FillDataSet(gRNReturnData, "GRN_Return", text, sqlTransaction);
				if (gRNReturnData == null || gRNReturnData.Tables.Count == 0 || gRNReturnData.Tables["GRN_Return"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT TD.*,ISNULL(PR.UnitQuantity,PR.Quantity) - ISNULL(PR.QuantityReturned,0) + ISNULL(TD.UnitQuantity,TD.Quantity) AS QuantityReceived ,ItemType,Product.Description,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,IsTrackLot,IsTrackSerial\r\n                        FROM GRN_Return_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tLEFT OUTER JOIN Purchase_Receipt_Detail PR ON PR.SysDocID = TD.SourceSysDocID AND PR.VoucherID = TD.SourceVoucherID AND PR.RowIndex = TD.SourceRowIndex\r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(gRNReturnData, "GRN_Return_Detail", text, sqlTransaction);
				DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
				if (gRNReturnData.Tables.Contains("Product_Lot_Issue_Detail"))
				{
					gRNReturnData.Tables.Remove("Product_Lot_Issue_Detail");
				}
				gRNReturnData.Merge(transactionIssuesProductLots, preserveChanges: false);
				return gRNReturnData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetGRNsToReturn(string vendorID, DateTime fromDate, DateTime toDate)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
			string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
			string text3 = "  SELECT DISTINCT   GRN.SysDocID [Doc ID], GRN.VoucherID [Number], TransactionDate AS [Date], GRN.VendorID + '-' + VEN.VendorName AS [Vendor]\r\n                            FROM Purchase_Receipt GRN INNER JOIN Purchase_Receipt_Detail GRND ON GRN.SysDocID=GRND.SysDocID \r\n\t\t\t\t\t\t\tAND GRN.VoucherID=GRND.VoucherID\r\n                            INNER JOIN Vendor VEN ON VEN.VendorID = GRN.VendorID\r\n                            WHERE ISNULL(ISNULL(UnitQuantity,Quantity),0) - ISNULL(GRND.QuantityReturned, 0) > 0  AND ISNULL(GRN.IsInvoiced,'False') = 'False' AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "'\r\n                             AND ISNULL(IsVoid,'False')='False' ";
			if (vendorID != "")
			{
				text3 = text3 + " AND GRN.VendorID='" + vendorID + "' ";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Receipt", sqlCommand);
			return dataSet;
		}

		internal bool DeleteGRNReturnDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				GRNReturnData gRNReturnData = new GRNReturnData();
				string textCommand = "SELECT SOD.*,ISNULL(SO.IsCash,'False') AS [IsCash],ISNULL(ISVOID,'False') AS IsVoid FROM GRN_Return_Detail SOD INNER JOIN GRN_Return SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(gRNReturnData, "GRN_Return_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(gRNReturnData.GRNReturnDetailTable.Rows[0]["IsCash"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(gRNReturnData.GRNReturnDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				if (!result2)
				{
					flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(95, sysDocID, voucherID, isDeletingTransaction, sqlTransaction);
				}
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM GRN_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePaymentRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Invoice_Payment WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool AllowModify(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				bool result = false;
				string exp = "SELECT TOP 1 IsInvoiced FROM GRN_Return_Detail GR INNER JOIN Purchase_Receipt PR ON PR.SysDocID = GR.SourceSysDocID AND PR.VoucherID = GR.SourceVoucherID\r\n                                    WHERE GR.SysDocID = '" + sysDocID + "' AND GR.VoucherID = '" + voucherID + "'";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj != null)
				{
					bool.TryParse(obj.ToString(), out result);
				}
				if (result)
				{
					throw new CompanyException("This transaction cannot be modifed because it is in refered by other transactions.");
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidGRNReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return VoidGRNReturn(sysDocID, voucherID, isVoid, null);
		}

		public bool VoidGRNReturn(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				GRNReturnData gRNReturnData = new GRNReturnData();
				if (!AllowModify(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("This transaction cannot be modifed because it is refered by other transactions.");
				}
				flag &= new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(95, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				string exp = "UPDATE GRN_Return SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				exp = "SELECT TOP 1 SourceSysDocID, SourceVoucherID FROM GRN_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(gRNReturnData, "GRN", exp, sqlTransaction);
				if (gRNReturnData != null && gRNReturnData.Tables["GRN"].Rows.Count > 0)
				{
					string sysDocID2 = gRNReturnData.Tables["GRN"].Rows[0]["SourceSysDocID"].ToString();
					string voucherID2 = gRNReturnData.Tables["GRN"].Rows[0]["SourceVoucherID"].ToString();
					flag &= new PurchaseReceipt(base.DBConfig).UpdateGRNReturnedQuantity(sysDocID2, voucherID2, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("GRN Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteGRNReturn(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("GRN_Return", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidGRNReturn(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteGRNReturnDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM GRN_Return WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				DataSet dataSet = new DataSet();
				text = "SELECT TOP 1 SourceSysDocID, SourceVoucherID FROM GRN_Return_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(dataSet, "GRN", text, sqlTransaction);
				if (dataSet != null && dataSet.Tables["GRN"].Rows.Count > 0)
				{
					string sysDocID2 = dataSet.Tables["GRN"].Rows[0]["SourceSysDocID"].ToString();
					string voucherID2 = dataSet.Tables["GRN"].Rows[0]["SourceVoucherID"].ToString();
					flag &= new PurchaseReceipt(base.DBConfig).UpdateGRNReturnedQuantity(sysDocID2, voucherID2, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("GRN Return", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetGRNReturnToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT DISTINCT SI.*,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName , VendorName,Vendor.TaxIDNumber as VTaxIDNo                    \r\n                                FROM  GRN_Return SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                                LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "GRN_Return", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["GRN_Return"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PRD.ProductID,PRD.Description,ISNULL(UnitQuantity,PRD.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n                        ISNULL(UnitPriceFC,PRD.UnitPrice) AS UnitPrice,\r\n                        ISNULL(UnitQuantity,PRD.Quantity)*ISNULL(UnitPriceFC,PRD.UnitPrice) AS Total,PRD.UnitID,LocationID\r\n                        FROM   GRN_Return_Detail PRD\r\n                        INNER JOIN Product P ON P.ProductID = PRD.ProductID\r\n                    \r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "GRN_Return_Detail", cmdText);
				dataSet.Relations.Add("GRNReturn", new DataColumn[2]
				{
					dataSet.Tables["GRN_Return"].Columns["SysDocID"],
					dataSet.Tables["GRN_Return"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["GRN_Return_Detail"].Columns["SysDocID"],
					dataSet.Tables["GRN_Return_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
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
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],TransactionDate [Return Date],Reference AS Ref1, Reference2 AS Ref2,\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type]\r\n                            FROM         GRN_Return INV\r\n                            Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "GRN_Return", sqlCommand);
			return dataSet;
		}
	}
}
