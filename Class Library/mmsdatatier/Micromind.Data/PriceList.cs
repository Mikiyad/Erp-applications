using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PriceList : StoreObject
	{
		private const string PRICELIST_TABLE = "Price_List";

		private const string PRICELISTDETAIL_TABLE = "Price_List_Detail";

		public const string VENDORPRICELIST_TABLE = "Vendor_Price_List";

		public const string VENDORPRICELISTDETAIL_TABLE = "Vendor_Price_List_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string VENDORID_PARM = "@VendorID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string VALIDDATEFROM_PARM = "@ValidDateFrom";

		private const string VALIDDATETO_PARM = "@ValidDateTo";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TOTAL_PARM = "@Total";

		private const string INACTIVE_PARM = "@Inactive";

		private const string ISAPPLICABLETOCHILD_PARM = "@ApplicableToChild";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string CUSTOMERPRODUCTID_PARM = "@CustomerProductID";

		private const string VENDORPRODUCTID_PARM = "@VendorProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string REMARKS_PARM = "@Remarks";

		public PriceList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePriceListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Price_List", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ValidDateFrom", "@ValidDateFrom"), new FieldValue("ValidDateTo", "@ValidDateTo"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("Status", "@Status"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"), new FieldValue("ApplicableToChild", "@ApplicableToChild"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Price_List", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePriceListCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePriceListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePriceListText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ValidDateFrom", SqlDbType.DateTime);
			parameters.Add("@ValidDateTo", SqlDbType.DateTime);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@ApplicableToChild", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ValidDateFrom"].SourceColumn = "ValidDateFrom";
			parameters["@ValidDateTo"].SourceColumn = "ValidDateTo";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@ApplicableToChild"].SourceColumn = "ApplicableToChild";
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

		private string GetInsertUpdatePriceListDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Price_List_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("CustomerProductID", "@CustomerProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePriceListDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePriceListDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePriceListDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@CustomerProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@CustomerProductID"].SourceColumn = "CustomerProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateVendorPriceListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor_Price_List", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ValidDateFrom", "@ValidDateFrom"), new FieldValue("ValidDateTo", "@ValidDateTo"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("Status", "@Status"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"), new FieldValue("ApplicableToChild", "@ApplicableToChild"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Price_List", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVendorPriceListCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateVendorPriceListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateVendorPriceListText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ValidDateFrom", SqlDbType.DateTime);
			parameters.Add("@ValidDateTo", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters.Add("@ApplicableToChild", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ValidDateFrom"].SourceColumn = "ValidDateFrom";
			parameters["@ValidDateTo"].SourceColumn = "ValidDateTo";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
			parameters["@ApplicableToChild"].SourceColumn = "ApplicableToChild";
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

		private string GetInsertUpdateVendorPriceListDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Vendor_Price_List_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("VendorProductID", "@VendorProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("SubunitPrice", "@SubunitPrice"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateVendorPriceListDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateVendorPriceListDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateVendorPriceListDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@VendorProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@VendorProductID"].SourceColumn = "VendorProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PriceListData journalData)
		{
			return true;
		}

		public bool InsertUpdatePriceList(PriceListData priceListData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePriceListCommand = GetInsertUpdatePriceListCommand(isUpdate);
			try
			{
				DataRow dataRow = priceListData.PriceListTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Price_List", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row in priceListData.PriceListDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["ProductID"].ToString();
					if (!arrayList.Contains(text2))
					{
						arrayList.Add(text2);
					}
				}
				string text3 = "";
				string text4 = "";
				foreach (string item in arrayList)
				{
					if (text4 != "")
					{
						text4 += ",";
					}
					text4 = text4 + "('" + item + "')";
				}
				text3 = "SELECT * FROM\r\n                                      (values " + text4 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT ProductID FROM Product";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3, sqlTransaction);
				if (dataSet != null && dataSet.Tables["Product"].Rows.Count > 0)
				{
					string text5 = "";
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						if (text5 != "")
						{
							text5 += ",";
						}
						text5 = text5 + "'" + row2[0].ToString() + "'";
					}
					throw new CompanyException("One or more item codes does not exist in item list.\nItems:" + text5, 1055);
				}
				insertUpdatePriceListCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(priceListData, "Price_List", insertUpdatePriceListCommand)) : (flag & Insert(priceListData, "Price_List", insertUpdatePriceListCommand)));
				insertUpdatePriceListCommand = GetInsertUpdatePriceListDetailsCommand(isUpdate: false);
				insertUpdatePriceListCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePriceListDetailsRows(sysDocID, text, sqlTransaction);
				}
				flag &= Insert(priceListData, "Price_List_Detail", insertUpdatePriceListCommand);
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Price_List", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Price List";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Price_List", "VoucherID", sqlTransaction);
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

		public bool InsertUpdateVendorPriceList(PriceListData priceListData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateVendorPriceListCommand = GetInsertUpdateVendorPriceListCommand(isUpdate);
			try
			{
				DataRow dataRow = priceListData.VendorPriceListTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Price_List", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				ArrayList arrayList = new ArrayList();
				foreach (DataRow row in priceListData.VendorPriceListDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["ProductID"].ToString();
					if (!arrayList.Contains(text2))
					{
						arrayList.Add(text2);
					}
				}
				string text3 = "";
				string text4 = "";
				foreach (string item in arrayList)
				{
					if (text4 != "")
					{
						text4 += ",";
					}
					text4 = text4 + "('" + item + "')";
				}
				text3 = "SELECT * FROM\r\n                                      (values " + text4 + ") as T(ID)\r\n                                    EXCEPT\r\n                                    SELECT ProductID FROM Product";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Product", text3, sqlTransaction);
				if (dataSet != null && dataSet.Tables["Product"].Rows.Count > 0)
				{
					string text5 = "";
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						if (text5 != "")
						{
							text5 += ",";
						}
						text5 = text5 + "'" + row2[0].ToString() + "'";
					}
					throw new CompanyException("One or more item codes does not exist in item list.\nItems:" + text5, 1055);
				}
				insertUpdateVendorPriceListCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(priceListData, "Vendor_Price_List", insertUpdateVendorPriceListCommand)) : (flag & Insert(priceListData, "Vendor_Price_List", insertUpdateVendorPriceListCommand)));
				insertUpdateVendorPriceListCommand = GetInsertUpdateVendorPriceListDetailsCommand(isUpdate: false);
				insertUpdateVendorPriceListCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteVendorPriceListDetailsRows(sysDocID, text, sqlTransaction);
				}
				flag &= Insert(priceListData, "Vendor_Price_List_Detail", insertUpdateVendorPriceListCommand);
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Vendor_Price_List", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Price List";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Vendor_Price_List", "VoucherID", sqlTransaction);
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

		public PriceListData GetPriceListByID(string sysDocID, string voucherID)
		{
			try
			{
				PriceListData priceListData = new PriceListData();
				string textCommand = "SELECT * FROM Price_List WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(priceListData, "Price_List", textCommand);
				if (priceListData == null || priceListData.Tables.Count == 0 || priceListData.Tables["Price_List"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Price_List_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(priceListData, "Price_List_Detail", textCommand);
				return priceListData;
			}
			catch
			{
				throw;
			}
		}

		public PriceListData GetVendorPriceListByID(string sysDocID, string voucherID)
		{
			try
			{
				PriceListData priceListData = new PriceListData();
				string textCommand = "SELECT * FROM Vendor_Price_List WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(priceListData, "Vendor_Price_List", textCommand);
				if (priceListData == null || priceListData.Tables.Count == 0 || priceListData.Tables["Vendor_Price_List"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n                        FROM Vendor_Price_List_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(priceListData, "Vendor_Price_List_Detail", textCommand);
				return priceListData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPriceListByCustomerID(string customerID)
		{
			try
			{
				_ = string.Empty;
				DataSet dataSet = new DataSet();
				string text = "SELECT SysDocID [Doc ID], VoucherID [Number],TransactionDate AS [Date], \r\n                              PL.CustomerID + '-' + C.CustomerName AS [Customer] FROM Price_List PL INNER JOIN Customer C ON PL.CustomerID = C.CustomerID WHERE 1=1";
				if (customerID != "")
				{
					text = text + " AND PL.CustomerID='" + customerID + "' ";
				}
				FillDataSet(dataSet, "Price_List", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPriceListByVendorID(string vendorID)
		{
			try
			{
				_ = string.Empty;
				DataSet dataSet = new DataSet();
				string text = "SELECT SysDocID [Doc ID], VoucherID [Number],TransactionDate AS [Date], \r\n                              PL.VendorID + '-' + V.VendorName AS [Vendor] FROM Vendor_Price_List PL INNER JOIN Vendor V ON PL.VendorID = V.VendorID WHERE 1=1";
				if (vendorID != "")
				{
					text = text + " AND PL.VendorID='" + vendorID + "' ";
				}
				FillDataSet(dataSet, "Vendor_Price_List", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetActivePriceListByCustomerID(string customerID)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
				DataSet dataSet = new DataSet();
				string text2 = "SELECT PLD.*, ISNULL(UnitPrice, 0) UnitPrice,  PL.CustomerID, PL.ValidDateFrom, PL.ValidDateTo, PL.TransactionDate\r\n                                FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID\r\n                                WHERE PL.CustomerID = '" + customerID + "' AND ISNULL(PL.Inactive, 0) <> 1 AND '" + text + "' BETWEEN ValidDateFrom AND ValidDateTo\r\n                                  AND PL.VoucherID IN (SELECT  VoucherID FROM Price_List WHERE (CustomerID = '" + customerID + "'  ))";
				FillDataSet(dataSet, "PriceLevel", text2);
				if (dataSet.Tables["PriceLevel"].Rows.Count <= 0)
				{
					text2 = text2 + "OR CustomerID = (SELECT ParentCustomerID FROM Customer WHERE CustomerID = '" + customerID + "' AND ISNULL(ApplicableToChild, 0) = 1  ) AND  ISNULL(Inactive, 0) <> 1 ORDER BY TransactioNDate DESC";
					FillDataSet(dataSet, "PriceLevel", text2);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePriceListDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Price_List_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteVendorPriceListDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Vendor_Price_List_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPriceList(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Sales_Quote SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Sales Quote", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool VoidVendorPriceList(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Vendor_Price_List SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Vendor PriceLIst", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeletePriceList(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePriceListDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Price_List WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Price List", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteVendorPriceList(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeletePriceListDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Vendor_Price_List WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Vendor Price List", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetPriceListToPrint(string sysDocID, string voucherID)
		{
			return GetPriceListToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetVendorPriceListToPrint(string sysDocID, string voucherID)
		{
			return GetVendorPriceListToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPriceListToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT SysDocID,VoucherID,PL.CustomerID,CustomerName,TransactionDate,\r\n                                PL.SalesPersonID,ValidDateTo, ValidDateFrom, IsVoid,Reference,Discount AS Discount, PL.Note\r\n                                FROM  Price_List PL INNER JOIN Customer ON PL.CustomerID=Customer.CustomerID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Price_List", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Price_List"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SysDocID, VoucherID, PLD.ProductID, PLD.Description, P.Attribute1, P.Attribute2, P.Attribute3, P.MatrixParentID,\r\n                        PLD.UnitPrice AS UnitPrice, PLD.UnitID\r\n                        FROM   Price_List_Detail PLD\r\n                        INNER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Price_List_Detail", cmdText);
				dataSet.Relations.Add("PriceList", new DataColumn[2]
				{
					dataSet.Tables["Price_List"].Columns["SysDocID"],
					dataSet.Tables["Price_List"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Price_List_Detail"].Columns["SysDocID"],
					dataSet.Tables["Price_List_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetVendorPriceListToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT SysDocID,VoucherID,PL.CustomerID,CustomerName,TransactionDate,\r\n                                PL.SalesPersonID,ValidDateTo, ValidDateFrom, IsVoid,Reference,Discount AS Discount, PL.Note\r\n                                FROM  Vendor_Price_List PL INNER JOIN Customer ON PL.CustomerID=Customer.CustomerID\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Price_List", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Price_List"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT SysDocID, VoucherID, PLD.ProductID, PLD.Description, P.Attribute1, P.Attribute2, P.Attribute3, P.MatrixParentID,\r\n                        PLD.UnitPrice AS UnitPrice, PLD.UnitID\r\n                        FROM   Vendor_Price_List_Detail PLD\r\n                        INNER JOIN Product P ON P.ProductID = PLD.ProductID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Price_List_Detail", cmdText);
				dataSet.Relations.Add("PriceList", new DataColumn[2]
				{
					dataSet.Tables["Price_List"].Columns["SysDocID"],
					dataSet.Tables["Price_List"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Price_List_Detail"].Columns["SysDocID"],
					dataSet.Tables["Price_List_Detail"].Columns["VoucherID"]
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
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Quote Date],\r\n                            INV.SalespersonID [Salesperson],Total [Amount]\r\n                            FROM  Price_List INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Price_List", sqlCommand);
			return dataSet;
		}

		public DataSet GetVendorPriceList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],TransactionDate [Quote Date],\r\n                            INV.SalespersonID [Salesperson],Total [Amount]\r\n                            FROM  Vendor_Price_List INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Vendor_Price_List", sqlCommand);
			return dataSet;
		}

		public DataSet GetPriceListAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Doc Number], TransactionDate AS [Date],\r\n                                (SELECT CustomerName FROM Customer C WHERE C.CustomerID =  PL.CustomerID) AS Customer FROM Price_List PL";
				FillDataSet(dataSet, "Price_List", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
