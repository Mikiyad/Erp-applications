using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class FreightCharge : StoreObject
	{
		private const string FREIGHTCHARGE_TABLE = "Freight_Charge";

		private const string FREIGHTCHARGEDETAIL_TABLE = "Freight_Charge_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string VALIDDATEFROM_PARM = "@ValidDateFrom";

		private const string VALIDDATETO_PARM = "@ValidDateTo";

		private const string ISVOID_PARM = "@IsVoid";

		private const string DISCOUNT_PARM = "@Discount";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TOTAL_PARM = "@Total";

		private const string INACTIVE_PARM = "@Inactive";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string SOURCEPORTID_PARM = "@SourcePortID";

		private const string DESTINATIONPORTID_PARM = "@DestinationPortID";

		private const string SHIPPINGCOMPANYID_PARM = "@ShippingCompanyID";

		private const string FREEDAYS_PARM = "@FreeDays";

		private const string TRANSITDAYS_PARM = "@TransitDays";

		private const string AMOUNT_PARM = "@Amount";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string TYPEID_PARM = "@TypeID";

		private const string REMARKS_PARM = "@Remarks";

		public FreightCharge(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateFreightChargeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Freight_Charge", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("Status", "@Status"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Discount", "@Discount"), new FieldValue("Total", "@Total"), new FieldValue("Reference", "@Reference"), new FieldValue("ValidDateFrom", "@ValidDateFrom"), new FieldValue("ValidDateTo", "@ValidDateTo"), new FieldValue("Note", "@Note"), new FieldValue("Inactive", "@Inactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Freight_Charge", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFreightChargeCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFreightChargeText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFreightChargeText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@ValidDateFrom", SqlDbType.DateTime);
			parameters.Add("@ValidDateTo", SqlDbType.DateTime);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@Inactive", SqlDbType.Bit);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@ValidDateFrom"].SourceColumn = "ValidDateFrom";
			parameters["@ValidDateTo"].SourceColumn = "ValidDateTo";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@Inactive"].SourceColumn = "Inactive";
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

		private string GetInsertUpdateFreightChargeDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Freight_Charge_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("SourcePortID", "@SourcePortID"), new FieldValue("DestinationPortID", "@DestinationPortID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("ShippingCompanyID", "@ShippingCompanyID"), new FieldValue("FreeDays", "@FreeDays"), new FieldValue("TransitDays", "@TransitDays"), new FieldValue("TypeID", "@TypeID"), new FieldValue("Amount", "@Amount"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("Remarks", "@Remarks"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateFreightChargeDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateFreightChargeDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateFreightChargeDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@SourcePortID", SqlDbType.NVarChar);
			parameters.Add("@DestinationPortID", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@ShippingCompanyID", SqlDbType.NVarChar);
			parameters.Add("@FreeDays", SqlDbType.Real);
			parameters.Add("@TransitDays", SqlDbType.Real);
			parameters.Add("@TypeID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@SourcePortID"].SourceColumn = "SourcePortID";
			parameters["@DestinationPortID"].SourceColumn = "DestinationPortID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@ShippingCompanyID"].SourceColumn = "ShippingCompanyID";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
			parameters["@FreeDays"].SourceColumn = "FreeDays";
			parameters["@TransitDays"].SourceColumn = "TransitDays";
			parameters["@TypeID"].SourceColumn = "TypeID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@Remarks"].SourceColumn = "Remarks";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(FreightChargeData journalData)
		{
			return true;
		}

		public bool InsertUpdateFreightCharge(FreightChargeData FreightChargeData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateFreightChargeCommand = GetInsertUpdateFreightChargeCommand(isUpdate);
			try
			{
				DataRow dataRow = FreightChargeData.FreightChargeTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Freight_Charge", "VoucherID", dataRow["SysDocID"].ToString(), text, sqlTransaction))
				{
					base.DBConfig.EndTransaction(result: false);
					throw new CompanyException("Document number already exist.", 1046);
				}
				new ArrayList();
				foreach (DataRow row in FreightChargeData.FreightChargeDetailTable.Rows)
				{
					row["SysDocID"] = dataRow["SysDocID"];
					row["VoucherID"] = dataRow["VoucherID"];
				}
				insertUpdateFreightChargeCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(FreightChargeData, "Freight_Charge", insertUpdateFreightChargeCommand)) : (flag & Insert(FreightChargeData, "Freight_Charge", insertUpdateFreightChargeCommand)));
				insertUpdateFreightChargeCommand = GetInsertUpdateFreightChargeDetailsCommand(isUpdate: false);
				insertUpdateFreightChargeCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeleteFreightChargeDetailsRows(sysDocID, text, sqlTransaction);
				}
				flag &= Insert(FreightChargeData, "Freight_Charge_Detail", insertUpdateFreightChargeCommand);
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Freight_Charge", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, isInsert: true);
				string entityName = "Freight Charge";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Freight_Charge", "VoucherID", sqlTransaction);
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

		public FreightChargeData GetFreightChargeByID(string sysDocID, string voucherID)
		{
			try
			{
				FreightChargeData freightChargeData = new FreightChargeData();
				string textCommand = "SELECT * FROM Freight_Charge WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(freightChargeData, "Freight_Charge", textCommand);
				if (freightChargeData == null || freightChargeData.Tables.Count == 0 || freightChargeData.Tables["Freight_Charge"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*\r\n                        FROM Freight_Charge_Detail TD \r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(freightChargeData, "Freight_Charge_Detail", textCommand);
				return freightChargeData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFreightChargeByCustomerID(string customerID)
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
				FillDataSet(dataSet, "Freight_Charge", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetActiveFreightChargeByCustomerID(string customerID)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(DateTime.Now);
				DataSet dataSet = new DataSet();
				string text2 = "SELECT PLD.*, ISNULL(UnitPrice, 0) UnitPrice,  PL.CustomerID, PL.ValidDateFrom, PL.ValidDateTo, PL.TransactionDate\r\n                                FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID\r\n                                WHERE PL.CustomerID = '" + customerID + "' AND ISNULL(PL.Inactive, 0) <> 1 AND '" + text + "' BETWEEN ValidDateFrom AND ValidDateTo\r\n                                  AND PL.VoucherID IN (SELECT  VoucherID FROM Price_List WHERE (CustomerID = '" + customerID + "'  ))";
				FillDataSet(dataSet, "PriceLevel", text2);
				if (dataSet.Tables["PriceLevel"].Rows.Count <= 0)
				{
					text2 = text2 + "OR CustomerID = (SELECT ParentCustomerID FROM Customer WHERE CustomerID = '" + customerID + "' ) AND  ISNULL(Inactive, 0) <> 1 ORDER BY TransactioNDate DESC";
					FillDataSet(dataSet, "PriceLevel", text2);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteFreightChargeDetailsRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Freight_Charge_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidFreightCharge(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string exp = "UPDATE Freight_Charge SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Freight Charge", voucherID, sysDocID, activityType, sqlTransaction);
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

		public bool DeleteFreightCharge(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag &= DeleteFreightChargeDetailsRows(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Freight_Charge WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Freight Charge", voucherID, sysDocID, activityType, sqlTransaction);
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

		public DataSet GetFreightChargeToPrint(string sysDocID, string voucherID)
		{
			return GetFreightChargeToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetFreightChargeToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "select * from Freight_Charge\r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Freight_Charge", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Freight_Charge"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "select FCD.*, (select PortName from  Port where Port.PortID=FCD.SourcePortID) AS [Source Port], \r\n                    (select PortName from Port where Port.PortID=FCD.DestinationPortID) AS [Destination Port], \r\n                        FCD.ShippingCompanyID+'-'+GL.GenericListName AS [Shipping Company], ContainerSizeName\r\n                        from Freight_Charge_Detail FCD LEFT JOIN Generic_List GL ON FCD.ShippingCompanyID=GL.GenericListID\r\n                        LEFT JOIN ContainerSize CS ON FCD.ContainerSizeID=CS.ContainerSizeID\r\n                        WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Freight_Charge_Detail", cmdText);
				dataSet.Relations.Add("FreightCharge", new DataColumn[2]
				{
					dataSet.Tables["Freight_Charge"].Columns["SysDocID"],
					dataSet.Tables["Freight_Charge"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Freight_Charge_Detail"].Columns["SysDocID"],
					dataSet.Tables["Freight_Charge_Detail"].Columns["VoucherID"]
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
			string text3 = "select FC.SysDocID as [Doc ID],FC.VoucherID [Doc Number],TransactionDate, FCD.SourcePortID AS [Source Port], FCD.DestinationPortID AS [Destination Port], FCD.ShippingCompanyID+'-'+ GenericListName AS [Shipping Company] \r\n                            from Freight_Charge FC LEFT JOIN Freight_Charge_Detail FCD ON FC.SysDocID=FCD.SysDocID and FC.VoucherID=FCD.VoucherID\r\n                            LEFT JOIN Generic_List  GL On FCD.ShippingCompanyID=GL.GenericListID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Freight_Charge", sqlCommand);
			return dataSet;
		}

		public DataSet GetFreightChargeAll()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SysDocID [Doc ID], VoucherID [Doc Number], TransactionDate AS [Date]\r\n                                 FROM Freight_Charge PL";
				FillDataSet(dataSet, "Freight_Charge", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFreightChargeReport(DateTime fromDate, DateTime toDate, string fromPort, string toPort, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs)
		{
			DataSet dataSet = new DataSet();
			string text = CommonLib.ToSqlDateTimeString(fromDate);
			string text2 = CommonLib.ToSqlDateTimeString(toDate);
			string text3 = "select * from Freight_Charge FC";
			text3 = text3 + " WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			text3 += " AND ISNULL(FC.IsVoid,'False')='False'  ";
			if (!showInactive)
			{
				text3 += " AND ISNULL(FC.Inactive,'False') = 'False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Freight_Charge", sqlCommand);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Freight_Charge"].Rows.Count == 0)
			{
				return null;
			}
			text3 = "select FCD.*,\r\n                     (select PortName from  Port where Port.PortID=FCD.SourcePortID) AS [Source Port], \r\n                    (select PortName from Port where Port.PortID=FCD.DestinationPortID) AS [Destination Port], \r\n                    FCD.VendorID+'-'+Vendor.VendorName AS [Supplier], ContainerSizeName\r\n                    from Freight_Charge_Detail FCD LEFT JOIN Vendor ON FCD.VendorID=Vendor.VendorID\r\n                    LEFT JOIN ContainerSize CS ON FCD.ContainerSizeID=CS.ContainerSizeID where 1=1\r\n                       ";
			if (vendorIDs != "")
			{
				text3 = text3 + " AND CustomerID IN(" + vendorIDs + ")";
			}
			if (fromVendor != "")
			{
				text3 = text3 + " AND FCD.VendorID>='" + fromVendor + "'";
			}
			if (toVendor != "")
			{
				text3 = text3 + " AND FCD.VendorID<='" + toVendor + "'";
			}
			if (fromClass != "")
			{
				text3 = text3 + " AND FCD.VendorClassID>='" + fromClass + "'";
			}
			if (toClass != "")
			{
				text3 = text3 + " AND FCD.VendorClassID<='" + toClass + "'";
			}
			if (fromGroup != "")
			{
				text3 = text3 + " AND FCD.VendorGroupID>='" + fromGroup + "'";
			}
			if (toGroup != "")
			{
				text3 = text3 + " AND FCD.VendorGroupID<='" + toGroup + "'";
			}
			if (fromPort != "")
			{
				text3 = text3 + " AND FCD.SourcePortID='" + fromPort + "'";
			}
			if (toPort != "")
			{
				text3 = text3 + " AND FCD.DestinationPortID='" + toPort + "'";
			}
			text3 += " ORDER BY RowIndex";
			FillDataSet(dataSet, "Freight_Charge_Detail", text3);
			dataSet.Relations.Add("FreightCharge", new DataColumn[2]
			{
				dataSet.Tables["Freight_Charge"].Columns["SysDocID"],
				dataSet.Tables["Freight_Charge"].Columns["VoucherID"]
			}, new DataColumn[2]
			{
				dataSet.Tables["Freight_Charge_Detail"].Columns["SysDocID"],
				dataSet.Tables["Freight_Charge_Detail"].Columns["VoucherID"]
			}, createConstraints: false);
			return dataSet;
		}
	}
}
