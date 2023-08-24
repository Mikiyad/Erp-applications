using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ExportPackingList : StoreObject
	{
		private const string EXPORTPACKINGLIST_TABLE = "Export_PackingList";

		private const string EXPORTPACKINGLISTDETAIL_TABLE = "Export_PackingList_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string STATUS_PARM = "@Status";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string ISVOID_PARM = "@IsVoid";

		private const string ISSHIPMENT = "@IsShipment";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string PORT_PARM = "@Port";

		private const string ETA_PARM = "@ETA";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string SHIPPER_PARM = "@Shipper";

		private const string CLEARINGAGENT_PARM = "@ClearingAgent";

		private const string WEIGHT_PARM = "@Weight";

		private const string VALUE_PARM = "@Value";

		private const string PACKINGLISTTAG_PARM = "@PackingListTag";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITID_PARM = "@UnitID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string SOURCEVOUCHERID_PARM = "@SourceVoucherID";

		private const string SOURCESYSDOCID_PARM = "@SourceSysDocID";

		private const string SOURCEROWINDEX_PARM = "@SourceRowIndex";

		private const string ISSOURCEDROW_PARM = "@IsSourcedRow";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string QUANTITYRECEIVED_PARM = "@QuantityReceived";

		private const string LICENSE_PARM = "@License";

		private const string BALANCE_PARM = "@Balance";

		private const string TERMS_PARM = "@Terms";

		private const string TOTALPACKAGES_PARM = "@TotalPackages";

		private const string COUNTRYOFORIGIN_PARM = "@CountryofOrigin";

		private const string BOX_PARM = "@Box";

		private const string NETWEIGHT_PARM = "@NetWeight";

		private const string GROSSWEIGHT_PARM = "@GrossWeight";

		private const string LENGTH_PARM = "@Length";

		private const string WIDTH_PARM = "@Width";

		private const string HEIGHT_PARM = "@Height";

		private const string CUBICMEASURE_PARM = "@CubicMeasure";

		private const string REMARKS_PARM = "@Remarks";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		public ExportPackingList(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateExportPackingListText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Export_PackingList", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Port", "@Port"), new FieldValue("ETA", "@ETA"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("Weight", "@Weight"), new FieldValue("Value", "@Value"), new FieldValue("PackingListTag", "@PackingListTag"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("Note", "@Note"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("License", "@License"), new FieldValue("Balance", "@Balance"), new FieldValue("Terms", "@Terms"), new FieldValue("TotalPackages", "@TotalPackages"), new FieldValue("CountryofOrigin", "@CountryofOrigin"), new FieldValue("Box", "@Box"), new FieldValue("NetWeight", "@NetWeight"), new FieldValue("GrossWeight", "@GrossWeight"), new FieldValue("Length", "@Length"), new FieldValue("Width", "@Width"), new FieldValue("Height", "@Height"), new FieldValue("IsShipment", "@IsShipment"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("CubicMeasure", "@CubicMeasure"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Export_PackingList", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExportPackingListCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExportPackingListText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExportPackingListText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@Shipper", SqlDbType.NVarChar);
			parameters.Add("@ClearingAgent", SqlDbType.NVarChar);
			parameters.Add("@Weight", SqlDbType.Real);
			parameters.Add("@Value", SqlDbType.Money);
			parameters.Add("@PackingListTag", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@IsShipment", SqlDbType.Bit);
			parameters.Add("@License", SqlDbType.NVarChar);
			parameters.Add("@Balance", SqlDbType.NVarChar);
			parameters.Add("@Terms", SqlDbType.NVarChar);
			parameters.Add("@TotalPackages", SqlDbType.Int);
			parameters.Add("@CountryofOrigin", SqlDbType.NVarChar);
			parameters.Add("@Box", SqlDbType.NVarChar);
			parameters.Add("@NetWeight", SqlDbType.Decimal);
			parameters.Add("@GrossWeight", SqlDbType.Decimal);
			parameters.Add("@Length", SqlDbType.Decimal);
			parameters.Add("@Width", SqlDbType.Decimal);
			parameters.Add("@Height", SqlDbType.Decimal);
			parameters.Add("@CubicMeasure", SqlDbType.Decimal);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@Shipper"].SourceColumn = "Shipper";
			parameters["@ClearingAgent"].SourceColumn = "ClearingAgent";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Value"].SourceColumn = "Value";
			parameters["@PackingListTag"].SourceColumn = "PackingListTag";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@IsShipment"].SourceColumn = "IsShipment";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@License"].SourceColumn = "License";
			parameters["@Balance"].SourceColumn = "Balance";
			parameters["@Terms"].SourceColumn = "Terms";
			parameters["@TotalPackages"].SourceColumn = "TotalPackages";
			parameters["@CountryofOrigin"].SourceColumn = "CountryofOrigin";
			parameters["@Box"].SourceColumn = "Box";
			parameters["@NetWeight"].SourceColumn = "NetWeight";
			parameters["@GrossWeight"].SourceColumn = "GrossWeight";
			parameters["@Length"].SourceColumn = "Length";
			parameters["@Width"].SourceColumn = "Width";
			parameters["@Height"].SourceColumn = "Height";
			parameters["@CubicMeasure"].SourceColumn = "CubicMeasure";
			parameters["@IsShipment"].SourceColumn = "IsShipment";
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

		private string GetInsertUpdateExportPackingListDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Export_PackingList_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("SourceVoucherID", "@SourceVoucherID"), new FieldValue("SourceSysDocID", "@SourceSysDocID"), new FieldValue("SourceRowIndex", "@SourceRowIndex"), new FieldValue("IsSourcedRow", "@IsSourcedRow"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("QuantityReceived", "@QuantityReceived"), new FieldValue("Remarks", "@Remarks"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("FactorType", "@FactorType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExportPackingListDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExportPackingListDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExportPackingListDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SourceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@SourceRowIndex", SqlDbType.Int);
			parameters.Add("@IsSourcedRow", SqlDbType.Int);
			parameters.Add("@SourceDocType", SqlDbType.Int);
			parameters.Add("@QuantityReceived", SqlDbType.Real);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SourceVoucherID"].SourceColumn = "SourceVoucherID";
			parameters["@SourceSysDocID"].SourceColumn = "SourceSysDocID";
			parameters["@SourceRowIndex"].SourceColumn = "SourceRowIndex";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@IsSourcedRow"].SourceColumn = "IsSourcedRow";
			parameters["@QuantityReceived"].SourceColumn = "QuantityReceived";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(ExportPackingListData journalData)
		{
			return true;
		}

		public bool CanUpdate(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Export_PackingList_Detail POD\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityReceived,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		public bool InsertUpdateExportPackingList(ExportPackingListData poShipmentData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateExportPackingListCommand = GetInsertUpdateExportPackingListCommand(isUpdate);
			try
			{
				DataRow dataRow = poShipmentData.ExportPackingListTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (isUpdate && !CanUpdate(sysDocID, text, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items has been already received.", 1037);
				}
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Export_PackingList", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				foreach (DataRow row in poShipmentData.ExportPackingListDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
					string text2 = row["ProductID"].ToString();
					string text3 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text2, sqlTransaction);
					if (fieldValue != null)
					{
						text3 = fieldValue.ToString();
					}
					if (text3 != "" && row["UnitID"] != DBNull.Value && row["UnitID"].ToString() != text3)
					{
						DataRow obj = new Products(base.DBConfig).GetProductUnitRow(text2, row["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text2 + "\nUnit:" + row["UnitID"].ToString());
						float num = float.Parse(obj["Factor"].ToString());
						string text4 = obj["FactorType"].ToString();
						float num2 = float.Parse(row["Quantity"].ToString());
						row["UnitFactor"] = num;
						row["FactorType"] = text4;
						row["UnitQuantity"] = row["Quantity"];
						num2 = ((!(text4 == "M")) ? float.Parse(Math.Round(num2 * num, 5).ToString()) : float.Parse(Math.Round(num2 / num, 5).ToString()));
						row["Quantity"] = num2;
					}
				}
				insertUpdateExportPackingListCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(poShipmentData, "Export_PackingList", insertUpdateExportPackingListCommand)) : (flag & Insert(poShipmentData, "Export_PackingList", insertUpdateExportPackingListCommand)));
				insertUpdateExportPackingListCommand = GetInsertUpdateExportPackingListDetailsCommand(isUpdate: false);
				insertUpdateExportPackingListCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteExportPackingListDetailsRows(sysDocID, text, sqlTransaction);
				}
				if (poShipmentData.Tables["Export_PackingList_Detail"].Rows.Count > 0)
				{
					flag &= Insert(poShipmentData, "Export_PackingList_Detail", insertUpdateExportPackingListCommand);
				}
				foreach (DataRow row2 in poShipmentData.ExportPackingListDetailTable.Rows)
				{
					string text5 = row2["SourceSysDocID"].ToString();
					string text6 = row2["SourceVoucherID"].ToString();
					string exp = "Select ISNULL(Sum(QUANTITY),0) From Export_PackingList_Detail Where SourceSysDocID='" + text5 + "' AND SourceVoucherID='" + text6 + "'";
					object obj3 = ExecuteScalar(exp, sqlTransaction);
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal.TryParse(obj3.ToString(), out result);
					exp = "Select ISNULL(Sum(QUANTITY),0) From Delivery_Note_Detail Where SysDocID='" + text5 + "' AND VoucherID='" + text6 + "'";
					decimal.TryParse(ExecuteScalar(exp, sqlTransaction).ToString(), out result2);
					if (result2 - result == 0m)
					{
						exp = "UPDATE  Delivery_Note SET IsShipped = 'True'\r\n                                FROM Delivery_Note  WHERE SysDocID = '" + text5 + "' AND VoucherID = '" + text6 + "'";
						flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
					}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Export_PackingList", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Export Pick List";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Export_PackingList", "VoucherID", sqlTransaction);
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

		public ExportPackingListData GetExportPackingListByID(string sysDocID, string voucherID)
		{
			try
			{
				ExportPackingListData exportPackingListData = new ExportPackingListData();
				string textCommand = "SELECT * FROM Export_PackingList WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(exportPackingListData, "Export_PackingList", textCommand);
				if (exportPackingListData == null || exportPackingListData.Tables.Count == 0 || exportPackingListData.Tables["Export_PackingList"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Isnull(DND.Quantity,0) as OrderQuantity,Isnull(DND.UnitQuantity,0) as OrderUnitQuantity,(select ISNULL(Sum(Quantity),0) as ShippedQuantity from Export_PackingList_Detail EPD\r\n                         WHERE EPD.SourceSysdocID=DND.SysDocID and EPD.SourceVoucherID=DND.VoucherID AND TD.ProductID=EPD.ProductID GROUP BY ProductID ) AS ShippedQuantity \r\n                        FROM Export_PackingList_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n                        LEFT JOIN Delivery_Note_Detail DND ON DND.SysDocID=TD.SourceSysdocID AND DND.VoucherID=TD.SourceVoucherID AND DND.ProductID=TD.ProductID\r\n                       \r\n                        WHERE TD.VoucherID='" + voucherID + "' AND TD.SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(exportPackingListData, "Export_PackingList_Detail", textCommand);
				return exportPackingListData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetOpenShipmentsSummary(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],ContainerNumber [Container No], SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Export_PackingList SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND Status=1";
				if (customerID != "")
				{
					text = text + " AND (SO.CustomerID='" + customerID + "' OR ParentCustomerID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "Export_PackingList", text);
				return dataSet;
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
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Export_PackingList_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
					if (result < result2 + quantity)
					{
						throw new CompanyException("Received quantity cannot be greater than packing list quantity.", 1036);
					}
				}
				result2 += quantity;
				textCommand = "UPDATE Export_PackingList_Detail SET QuantityReceived=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool CloseReceivedShipment(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Export_PackingList_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Export_PackingList SET Status= 2,IsReceived='True' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteExportPackingListDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				ExportPackingListData exportPackingListData = new ExportPackingListData();
				string textCommand = "SELECT SOD.*,ISVOID FROM Export_PackingList_Detail SOD INNER JOIN Export_PackingList SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(exportPackingListData, "Export_PackingList_Detail", textCommand, sqlTransaction);
				if (exportPackingListData.ExportPackingListDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(exportPackingListData.ExportPackingListDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				if (!result)
				{
					foreach (DataRow row in exportPackingListData.ExportPackingListDetailTable.Rows)
					{
						row["ProductID"].ToString();
						row["SourceVoucherID"].ToString();
						row["SourceSysDocID"].ToString();
						row["SourceRowIndex"].ToString();
						float.Parse(row["Quantity"].ToString());
					}
				}
				textCommand = "DELETE FROM Export_PackingList_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidExportPackingList(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE  DN SET IsShipped = 'False'\r\n                                FROM Delivery_Note DN INNER JOIN Export_PackingList_Detail EPD ON DN.SysDocID = EPD.SourceSysDocID AND EPD.SourceVoucherID = DN.VoucherID\r\n                                WHERE EPD.SysDocID = '" + sysDocID + "' AND EPD.VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				ExportPackingListData exportPackingListData = new ExportPackingListData();
				exp = "SELECT * FROM Export_PackingList_Detail\r\n                              WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				FillDataSet(exportPackingListData, "Export_PackingList_Detail", exp, sqlTransaction);
				exp = "SELECT DISTINCT SourceSysDocID,SourceVoucherID FROM Export_PackingList_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Sales_Order", exp, sqlTransaction);
				foreach (DataRow row in exportPackingListData.ExportPackingListDetailTable.Rows)
				{
					row["ProductID"].ToString();
					row["SourceVoucherID"].ToString();
					row["SourceSysDocID"].ToString();
					row["SourceRowIndex"].ToString();
					float.Parse(row["Quantity"].ToString());
				}
				if (dataSet != null && dataSet.Tables.Count > 0)
				{
					foreach (DataRow row2 in dataSet.Tables[0].Rows)
					{
						string sysDocID2 = row2["SourceSysDocID"].ToString();
						string voucherID2 = row2["SourceVoucherID"].ToString();
						flag &= new SalesOrder(base.DBConfig).CloseShippedOrder(sysDocID2, voucherID2, sqlTransaction);
					}
				}
				exp = "UPDATE Export_PackingList SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Packing List", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteExportPackingList(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				text = "UPDATE  DN SET IsShipped = 'False'\r\n                                FROM Delivery_Note DN INNER JOIN Export_PackingList_Detail EPD ON DN.SysDocID = EPD.SourceSysDocID AND EPD.SourceVoucherID = DN.VoucherID\r\n                                WHERE EPD.SysDocID = '" + sysDocID + "' AND EPD.VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				text = "SELECT DISTINCT SourceSysDocID,SourceVoucherID FROM Export_PackingList_Detail WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Sales_Order", text, sqlTransaction);
				flag &= DeleteExportPackingListDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Export_PackingList WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("PO Shipment", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetOpenShipmentsReport()
		{
			try
			{
				string text = "";
				DataSet dataSet = new DataSet();
				text = "SELECT POS.CustomerID + ' - ' + CustomerName AS Customer,ContainerNumber ,PortName,PONUmber,ETA,\r\n                            ShippingMethodName,Shipper,POS.Note \r\n                            FROM Export_PackingList POS\r\n                            INNER JOIN Customer V ON POS.CustomerID=V.CustomerID\r\n                            Left OUTER JOIN Port ON POS.Port=Port.PortID\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=POS.ShippingMethodID\r\n                            WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsReceived,0)= 0 ORDER BY POS.CustomerID,ETA";
				FillDataSet(dataSet, "Shipments", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetExportPackingListToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SI.CustomerID,CustomerName,TransactionDate,\r\n                                    VA.AddressPrintFormat AS CustomerAddress,ShippingMethodName,   SD.LocationID SYSLOCID, LocationName AS SYSLocationName,\r\n\t\t\t\t\t\t\t\t\t    ETA,ClearingAgent,Shipper,BOLNumber,ContainerNumber,Weight,[Value],Port,\r\n                                        IsVoid,Reference, PONumber,SI.Note,SI.License,SI.Balance,SI.Terms,SI.TotalPackages,SI.CountryofOrigin,SI.Box,\r\n                                        SI.NetWeight,SI.GrossWeight,SI.Length,SI.Width,SI.Height,SI.CubicMeasure,SI.PackingListTag           \r\n                                        ,V.VehicleName,V.VehicleID,D.Drivername,D.Note AS [Driver No.],SI.VehicleID AS OutVehicle,\r\n                                SUBSTRING((SELECT  DISTINCT  ' ' + convert(varchar,  ( DN.PODate) , 106) + ', ' FROM Export_PackingList_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.SourceSysDocID=DND.SysDocID AND SND.SourceVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Delivery_Note DN ON DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID FOR XML PATH('')),2,20000) AS [DNPODate],\r\n                                SUBSTRING((SELECT  DISTINCT  ' ' +  DN.PONumber + ', ' FROM Export_PackingList_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.SourceSysDocID=DND.SysDocID AND SND.SourceVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Delivery_Note DN ON DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID FOR XML PATH('')),2,20000) AS [DNPONumber],\r\n                                SUBSTRING((SELECT  DISTINCT  ' ' +  DN.VoucherID + ', ' FROM Export_PackingList_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.SourceSysDocID=DND.SysDocID AND SND.SourceVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Delivery_Note DN ON DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID FOR XML PATH('')),2,20000) AS [DNNumber],\r\n                                SUBSTRING((SELECT  DISTINCT  ' ' +  DN.Note + ', ' FROM Export_PackingList_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.SourceSysDocID=DND.SysDocID AND SND.SourceVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Delivery_Note DN ON DND.SysDocID=DN.SysDocID AND DND.VoucherID=DN.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID FOR XML PATH('')),2,20000) AS [DNNote]\r\n                                        FROM  Export_PackingList SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID   \r\n                                        LEFT OUTER JOIN Customer_Address VA ON VA.CustomerID=SI.CustomerID AND VA.AddressID='PRIMARY'     \r\n                                     LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID      \r\n                                      LEFT JOIN System_Document SD ON SD.SysDocID=SI.SysDocID\r\n\t\t\t\t\t\t\t        LEFT JOIN Location ON SD.LocationID=Location.LocationID  \r\n                                    LEFT OUTER JOIN Driver D ON D.DriverID=SI.DriverID\r\n                                    LEFT OUTER JOIN Vehicle V ON V.VehicleID=SI.VehicleID                           \r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Export_PackingList", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Export_PackingList"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     EPD.SysDocID,EPD.VoucherID,EPD.SourceVoucherID,EPD.ProductID,EPD.Description,ISNULL(EPD.UnitQuantity,EPD.Quantity) AS Quantity,\r\n                        EPD.UnitPrice AS UnitPrice,\r\n                        ISNULL(EPD.UnitQuantity,EPD.Quantity)*ISNULL(EPD.UnitPrice,0) AS Total,EPD.UnitID,C.CountryName [Origin],PS.StyleName [Style],EPD.SpecificationID, SpecificationName, EPD.Remarks,P.Weight,DND.Remarks AS [DN Remarks],\r\n                        P.UnitID AS MainUnit,\r\n                        (SELECT TOP 1  PU.Factor from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS Factor,\r\n                        (SELECT TOP 1  PU.UnitID from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS SubUnit, DND.LocationID\r\n                        FROM   Export_PackingList_Detail EPD\r\n                        LEFT OUTER JOIn Delivery_Note_Detail DND ON DND.SysDocID=EPD.SourceSysDocID AND DND.VoucherID=EPD.SourceVoucherID\r\n                        LEFT JOIN Product P ON P.ProductID=EPD.ProductID\r\n                        LEFT JOIN Product_Style PS ON PS.StyleID=EPD.StyleID\r\n                        LEFT JOIN Country C ON C.CountryID=P.Origin\r\n                        LEFT OUTER JOIN Product_Specification PSS ON PSS.SpecificationID=EPD.SpecificationID\r\n\r\n                        WHERE EPD.SysDocID='" + sysDocID + "' AND EPD.VoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Export_PackingList_Detail", cmdText);
				dataSet.Relations.Add("ExportPackingList", new DataColumn[2]
				{
					dataSet.Tables["Export_PackingList"].Columns["SysDocID"],
					dataSet.Tables["Export_PackingList"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Export_PackingList_Detail"].Columns["SysDocID"],
					dataSet.Tables["Export_PackingList_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, bool isShipment)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT     ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],\r\n                            CustomerName [Customer Name],TransactionDate [Invoice Date], Reference\r\n                            FROM         Export_PackingList INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			text3 = text3 + " AND ISNULL(IsShipment, 'False') = '" + isShipment.ToString() + "'";
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Export_PackingList", sqlCommand);
			return dataSet;
		}

		public DataSet GetPackingListsForInvoice(string customerID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT PL.SysDocID [Doc ID],PL.VoucherID [Number], TransactionDate AS [Date],PL.CustomerID + '-' + CUS.CustomerName AS Customer \r\n                                FROM Export_PackingList PL\r\n                                INNER JOIN Customer CUS ON PL.CustomerID=CUS.CustomerID  WHERE ISNULL(IsVoid,'False')='False' AND ISNULL(IsInvoiced,'False')='False'  AND ISNULL(CUS.AllowConsignment,'False')='True'";
				if (customerID != "")
				{
					text = text + " AND (PL.CustomerID = '" + customerID + "')";
				}
				FillDataSet(dataSet, "Export_PackingList", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}
	}
}
