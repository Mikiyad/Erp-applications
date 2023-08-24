using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ContainerTrackings : StoreObject
	{
		private const string CONTAINERTRACKINGTABLE_PARM = "Container_Tracking";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string STATUS_PARM = "@Status";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string ISVOID_PARM = "@IsVoid";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string PORT_PARM = "@Port";

		private const string LOADINGPORT_PARM = "@LoadingPort";

		private const string ETA_PARM = "@ETA";

		private const string ATD_PARM = "@ATD";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string SHIPPER_PARM = "@Shipper";

		private const string WEIGHT_PARM = "@Weight";

		private const string ISRECEIVED_PARM = "@IsReceived";

		private const string TRUCKNUMBER_PARM = "@TruckNumber";

		private const string TRANSPORTERID_PARM = "@TransporterID";

		private const string CONTAINERSTATUS_PARM = "@ContainerStatus";

		private const string REMARKS_PARM = "@Remarks";

		private const string ACTIVITYDONEBY_PARM = "@ActivityDoneBy";

		private const string ACTIVITYTIME_PARM = "@DeliveryTime";

		private const string CONTAINERSIZEID_PARM = "@ContainerSizeID";

		private const string DELIVERYFREETIME_PARM = "@FreeTimeTODeliver";

		private const string DELIVERYFREEDATE_PARM = "@DeliveryDate";

		private const string TRANSPORTCOMPANY_PARM = "@TransportCompany";

		private const string DRIVERID_PARM = "@DriverID";

		private const string ISBL_PARM = "@IsBL";

		private const string ISINVOICE_PARM = "@IsInvoice";

		private const string ISPL_PARM = "@IsPL";

		private const string ISHEALTHCERTIFICATE_PARM = "@IsHealthCertficate";

		private const string ISCERTIFICATEOFORIGIN_PARM = "@IsCertificateOfOrigin";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public ContainerTrackings(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Container_Tracking", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("VendorID", "@VendorID"), new FieldValue("FreeTimeTODeliver", "@FreeTimeTODeliver"), new FieldValue("DeliveryDate", "@DeliveryDate"), new FieldValue("TransportCompany", "@TransportCompany"), new FieldValue("DriverID", "@DriverID"), new FieldValue("TruckNumber", "@TruckNumber"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("Reference", "@Reference"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("DestinationPort", "@Port"), new FieldValue("LoadingPort", "@LoadingPort"), new FieldValue("ETA", "@ETA"), new FieldValue("ATD", "@ATD"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("Weight", "@Weight"), new FieldValue("IsBL", "@IsBL"), new FieldValue("IsInvoice", "@IsInvoice"), new FieldValue("IsPL", "@IsPL"), new FieldValue("IsHealthCertficate", "@IsHealthCertficate"), new FieldValue("IsCertificateOfOrigin", "@IsCertificateOfOrigin"), new FieldValue("TransporterID", "@TransporterID"), new FieldValue("ContainerSizeID", "@ContainerSizeID"), new FieldValue("Container_Status", "@ContainerStatus"), new FieldValue("ActivityDoneBy", "@ActivityDoneBy"), new FieldValue("DeliveryTime", "@DeliveryTime"), new FieldValue("Remarks", "@Remarks"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Container_Tracking", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@LoadingPort", SqlDbType.NVarChar);
			parameters.Add("@ETA", SqlDbType.DateTime);
			parameters.Add("@ATD", SqlDbType.DateTime);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@Shipper", SqlDbType.NVarChar);
			parameters.Add("@FreeTimeTODeliver", SqlDbType.DateTime);
			parameters.Add("@DeliveryDate", SqlDbType.DateTime);
			parameters.Add("@TruckNumber", SqlDbType.NVarChar);
			parameters.Add("@TransportCompany", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@IsBL", SqlDbType.Bit);
			parameters.Add("@IsInvoice", SqlDbType.Bit);
			parameters.Add("@IsPL", SqlDbType.Bit);
			parameters.Add("@IsHealthCertficate", SqlDbType.Bit);
			parameters.Add("@IsCertificateOfOrigin", SqlDbType.Bit);
			parameters.Add("@Weight", SqlDbType.Real);
			parameters.Add("@TransporterID", SqlDbType.NVarChar);
			parameters.Add("@ContainerSizeID", SqlDbType.NVarChar);
			parameters.Add("@ContainerStatus", SqlDbType.Int);
			parameters.Add("@ActivityDoneBy", SqlDbType.NVarChar);
			parameters.Add("@DeliveryTime", SqlDbType.Time);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@FreeTimeTODeliver"].SourceColumn = "FreeTimeTODeliver";
			parameters["@DeliveryDate"].SourceColumn = "DeliveryDate";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@Port"].SourceColumn = "DestinationPort";
			parameters["@LoadingPort"].SourceColumn = "LoadingPort";
			parameters["@ETA"].SourceColumn = "ETA";
			parameters["@ATD"].SourceColumn = "ATD";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@Shipper"].SourceColumn = "Shipper";
			parameters["@TransportCompany"].SourceColumn = "TransportCompany";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@IsBL"].SourceColumn = "IsBL";
			parameters["@IsInvoice"].SourceColumn = "IsInvoice";
			parameters["@IsPL"].SourceColumn = "IsPL";
			parameters["@IsHealthCertficate"].SourceColumn = "IsHealthCertficate";
			parameters["@IsCertificateOfOrigin"].SourceColumn = "IsCertificateOfOrigin";
			parameters["@Weight"].SourceColumn = "Weight";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@TruckNumber"].SourceColumn = "TruckNumber";
			parameters["@TransporterID"].SourceColumn = "TransporterID";
			parameters["@ContainerStatus"].SourceColumn = "Container_Status";
			parameters["@ActivityDoneBy"].SourceColumn = "ActivityDoneBy";
			parameters["@DeliveryTime"].SourceColumn = "DeliveryTime";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@ContainerSizeID"].SourceColumn = "ContainerSizeID";
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

		public bool InsertUpdateContainerTracking(ContainerTrackingData accountContainerTrackingData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				DataRow dataRow = accountContainerTrackingData.ContainerTrackingTable.Rows[0];
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(accountContainerTrackingData, "Container_Tracking", insertUpdateCommand) : Update(accountContainerTrackingData, "Container_Tracking", insertUpdateCommand));
				string entiyID = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				if (!flag)
				{
					return flag;
				}
				flag &= UpdateTableRowInsertUpdateInfo("Container_Tracking", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Container Tracking";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, entiyID, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, entiyID, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (isUpdate)
				{
					return flag;
				}
				flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Container_Tracking", "VoucherID", sqlTransaction);
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

		public ContainerTrackingData GetContainerTracking()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Container_Tracking");
			ContainerTrackingData containerTrackingData = new ContainerTrackingData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(containerTrackingData, "Container_Tracking", sqlBuilder);
			return containerTrackingData;
		}

		public bool DeleteContainerTracking(string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "DELETE FROM Container_Tracking WHERE VoucherID = '" + voucherID + "'";
				flag &= Delete(commandText, trans);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ContainerTracking", voucherID, ActivityTypes.Delete, null);
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

		public ContainerTrackingData GetContainerTrackingByID(string id)
		{
			ContainerTrackingData containerTrackingData = new ContainerTrackingData();
			string textCommand = "SELECT E.* FROM Container_Tracking E\r\n                            WHERE VoucherID='" + id + "'";
			FillDataSet(containerTrackingData, "Container_Tracking", textCommand);
			return containerTrackingData;
		}

		public DataSet GetContainerTrackingByFields(params string[] columns)
		{
			return GetContainerTrackingByFields(null, isInactive: true, columns);
		}

		public DataSet GetContainerTrackingByFields(string[] ContainerTrackingID, params string[] columns)
		{
			return GetContainerTrackingByFields(ContainerTrackingID, isInactive: true, columns);
		}

		public DataSet GetContainerTrackingByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Container_Tracking");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (ids != null && ids.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "VoucherID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Container_Tracking";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Container_Tracking", sqlBuilder);
			return dataSet;
		}

		public DataSet GetContainerTrackingList(DateTime from, DateTime to, bool showVoid)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = StoreConfiguration.ToSqlDateTimeString(from);
				string text2 = StoreConfiguration.ToSqlDateTimeString(to);
				string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],ContainerNumber [Container No],\r\n                            Case INV.Container_Status When 1 Then 'Shipped' when 2 then 'Arrived' when 3 then 'DO release' when 4 then 'Customs Checking'\r\n                            when 5 then 'Shipment Removal' when 6 then 'Shipment Delivered' when 7 then 'Off Loaded' when 8 then 'Returning' when 9 then 'Returned' when 10 then 'Cancelled' ELSE 'NA' End AS [Status]\r\n                             FROM   Container_Tracking INV\r\n                            INNER JOIN Vendor ON VENDOR.VendorID=INV.VendorID  ";
				if (from != DateTime.MinValue)
				{
					text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
				}
				if (!showVoid)
				{
					text3 += " AND ISNULL(IsVoid,'False')='False' ";
				}
				FillDataSet(dataSet, "Container_Tracking", text3);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerTrackingComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContainerTrackingID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],\r\n                            ISNULL(IsTerminated,'False') AS IsTerminated\r\n                           FROM ContainerTracking ORDER BY ContainerTrackingID,FirstName";
			FillDataSet(dataSet, "Container_Tracking", textCommand);
			return dataSet;
		}

		public DataSet GetContainerTrackingFilterComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContainerTrackingID [Code],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Name],\r\n                            ISNULL(IsTerminated,'False') AS IsTerminated,ReportToID\r\n                           FROM ContainerTracking ORDER BY ContainerTrackingID,FirstName";
			FillDataSet(dataSet, "Container_Tracking", textCommand);
			return dataSet;
		}

		public DataSet GetEventContainerTrackingList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ContainerTrackingID [Doc ID],ISNULL(FirstName,'')+ ' ' + ISNULL(MiddleName,'') + ' ' + ISNULL(LastName,'') [Number]                           \r\n                           FROM Container_Tracking ORDER BY ContainerTrackingID,FirstName";
			FillDataSet(dataSet, "Container_Tracking", textCommand);
			return dataSet;
		}

		public DataSet GetContainerTrackingToPrint(string sysDocID, string[] voucherID)
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
				SqlCommand sqlCommand = new SqlCommand("SELECT   SI.ContainerNumber,\r\n                        Case SI.Container_Status When 1 Then 'Shipped' when 2 then 'Original Documents Received' when 3 then 'Arrived' when 4 then 'DO release' when 5 then 'Customs Checking'\r\n                        when 6 then 'Shipment Removal' when 7 then 'Shipment Delivered' when 8 then 'Off Loaded' when 9 then 'Returning' when 10 then 'Returned' \r\n\t\t\t\t\t\twhen 11 then 'Cancelled' End AS [Status] \r\n                        ,SI.VendorID,SI.TransactionDate ,Vendor.Vendorname,ShippingMethodName, SI.ATD, SI.BOLNumber, SI.ETA, SI.DeliveryDate, SI.DeliveryTime,CZ.ContainerSizeName,  SI.TransporterID, SI.DestinationPort,SI.LoadingPort, SI.ContainerSizeID, SI.DriverID, DriverName, SI.Trucknumber, SI.Remarks,SI.Shipper, SI.Weight, T.TransporterName,\r\n\t\t\t\t\t\tCase SI.IsBL when 1 then 'Yes' Else 'No' END AS BL,CASE SI.IsInvoice when 1 then 'Yes' Else 'No' END AS Invoice ,Case SI.IsCertificateOfOrigin  when 1 then 'Yes' Else 'No' END AS [Certificate Of Origin] ,Case SI.IsHealthCertficate  when 1 then 'Yes' Else 'No' END AS [Health Certificate],case SI.IsPL  when 1 then 'Yes' Else 'No' END AS [Packing List]\r\n                        FROM  Container_Tracking SI LEFT JOIN Vendor ON SI.VendorID=Vendor.VendorID \r\n\t\t\t\t\t\tLEFT JOIN Driver D ON SI.DriverID=D.DriverID \r\n\t\t\t\t\t\tLEFT JOIN ContainerSize CZ ON SI.ContainerSizeID=CZ.ContainerSizeID\r\n\t\t\t\t\t\tLEFT JOIN Transporter T ON SI.TransporterID=T.TransporterID\r\n                        LEFT  JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID \r\n                                WHERE SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")");
				FillDataSet(dataSet, "Container_Tracking", sqlCommand);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerTrackingReport(string containerNumber)
		{
			DataSet dataSet = new DataSet();
			string str = "";
			str += "SELECT   SI.ContainerNumber,\r\n                        Case SI.Container_Status When 1 Then 'Shipped' when 2 then 'Original Documents Received' when 3 then 'Arrived' when 4 then 'DO release' when 5 then 'Customs Checking'\r\n                        when 6 then 'Shipment Removal' when 7 then 'Shipment Delivered' when 8 then 'Off Loaded' when 9 then 'Returning' when 10 then 'Returned' \r\n\t\t\t\t\t\twhen 11 then 'Cancelled' End AS [Status] \r\n                        ,SI.VendorID,SI.TransactionDate ,Vendor.Vendorname,ShippingMethodName, SI.ATD, SI.BOLNumber, SI.ETA, SI.DeliveryDate, SI.DeliveryTime,CZ.ContainerSizeName,  SI.TransporterID, SI.DestinationPort,SI.LoadingPort, SI.ContainerSizeID, SI.DriverID, DriverName, SI.Trucknumber, SI.Remarks,SI.Shipper, SI.Weight, T.TransporterName,\r\n\t\t\t\t\t\tCase SI.IsBL when 1 then 'Yes' Else 'No' END AS BL,CASE SI.IsInvoice when 1 then 'Yes' Else 'No' END AS Invoice ,Case SI.IsCertificateOfOrigin  when 1 then 'Yes' Else 'No' END AS [Certificate Of Origin] ,Case SI.IsHealthCertficate  when 1 then 'Yes' Else 'No' END AS [Health Certificate],case SI.IsPL  when 1 then 'Yes' Else 'No' END AS [Packing List]\r\n                        FROM  Container_Tracking SI LEFT JOIN Vendor ON SI.VendorID=Vendor.VendorID \r\n\t\t\t\t\t\tLEFT JOIN Driver D ON SI.DriverID=D.DriverID \r\n\t\t\t\t\t\tLEFT JOIN ContainerSize CZ ON SI.ContainerSizeID=CZ.ContainerSizeID\r\n\t\t\t\t\t\tLEFT JOIN Transporter T ON SI.TransporterID=T.TransporterID\r\n                        LEFT  JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID where 1=1 ";
			if (containerNumber != "")
			{
				str = str + "AND ContainerNumber= '" + containerNumber + "'";
			}
			FillDataSet(dataSet, "Container_Tracking", str);
			return dataSet;
		}

		public DataSet GetContainerDetailsNew(string containerNumber)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT top 1 INV.*,Vendor.VendorID, Vendor.VendorName,CT.Container_Status, CT.*\r\n                            FROM   PO_Shipment INV\r\n                             Inner JOIN Vendor ON VENDOR.VendorID=INV.VendorID \r\n\t\t\t\t\t\t\t LEFT JOIN Container_Tracking CT ON INV.ContainerNumber=CT.ContainerNumber\r\n\t\t\t\t\t\t\t WHERE ISNULL(INV.IsVoid,'False')='False'";
				if (containerNumber != "")
				{
					text = text + " AND INV.ContainerNumber='" + containerNumber + "' order by CAST(Container_Status AS INT) desc";
				}
				FillDataSet(dataSet, "Container_Tracking", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetContainerDetailsOnStatus(string containerNumber, int status)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT  CT.Container_Status, CT.* From\r\n                             Container_Tracking CT \r\n\t\t\t\t\t\t\t WHERE ISNULL(CT.IsVoid,'False')='False'";
				if (containerNumber != "")
				{
					text += " AND CT.ContainerNumber='" + containerNumber + "' and CT.Container_Status=" + status;
				}
				FillDataSet(dataSet, "Container_Tracking", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public ContainerTrackingData GetContainerDetailsOnStatusChange(string containerNumber)
		{
			try
			{
				ContainerTrackingData containerTrackingData = new ContainerTrackingData();
				string text = "SELECT CT.Container_Status, CT.DriverID, CT.TruckNumber, CT.TransporterID, CT.DeliveryDate, CT.DeliveryTime, CT.FreeTimeTODeliver\r\n                            FROM   Container_Tracking CT\r\n                              \r\n\t\t\t\t\t\t\t WHERE ISNULL(CT.IsVoid,'False')='False'";
				if (containerNumber != "")
				{
					text = text + " AND CT.ContainerNumber='" + containerNumber + "'and CT.Container_Status=6";
				}
				FillDataSet(containerTrackingData, "ContainerTracking", text);
				return containerTrackingData;
			}
			catch
			{
				throw;
			}
		}

		public bool VoidTracking(string sysDocID, string voucherID, bool isVoid)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				ContainerTrackingData dataSet = new ContainerTrackingData();
				string textCommand = "SELECT * FROM Container_Tracking\r\n                              WHERE SysDocID = '" + sysDocID + "' AND SysDocID = '" + voucherID + "'";
				FillDataSet(dataSet, "Container_Tracking", textCommand);
				textCommand = "UPDATE Container_Tracking SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Container Tracking", voucherID, sysDocID, activityType, sqlTransaction);
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
	}
}
