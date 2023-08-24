using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Micromind.Data
{
	public sealed class DataSync : StoreObject
	{
		private const string DATASYNCID_PARM = "@DataSyncID";

		private const string DATASYNCNAME_PARM = "@DataSyncName";

		private const string DATASYNC_TABLE = "DataSync";

		private const string TYPE_PARM = "@Type";

		private const string DATABASENAME_PARM = "@DatabaseName";

		private const string SERVERID_PARM = "@ServerID";

		private const string USERID_PARM = "@UserID";

		private const string PASSWORD_PARM = "@Password";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string SYNCTYPE_PARM = "@SyncType";

		private const string RECORDTYPE_PARM = "@RecordType";

		private const string NAME_PARM = "@Name";

		private const string DESCRIPTION_PARM = "@Description";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string DEFAULTSYSDOCID_PARM = "@DefaultSysDocID";

		private const string DEFAULTREGISTERID_PARM = "@DefaultRegisterID";

		private const string LASTSYNCTIME_PARM = "@LastSyncTime";

		private const string SYNCINTERVAL_PARM = "@SyncInterval";

		private const string STATUS_PARM = "@Status";

		private DataSet conncData = new DataSet();

		public DataSync(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateDataSyncText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Data_Sync", new FieldValue("Code", "@DataSyncID", isUpdateConditionField: true), new FieldValue("Name", "@DataSyncName"), new FieldValue("Type", "@Type"), new FieldValue("DatabaseName", "@DatabaseName"), new FieldValue("ServerID", "@ServerID"), new FieldValue("UserID", "@UserID"), new FieldValue("Password", "@Password"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Data_Sync", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private string GetInsertUpdateDataSyncDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Data_Sync_Detail", new FieldValue("Code", "@DataSyncID"), new FieldValue("SyncType", "@SyncType"), new FieldValue("RecordType", "@RecordType"), new FieldValue("Name", "@Name"), new FieldValue("Description", "@Description"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("DefaultSysDocID", "@DefaultSysDocID"), new FieldValue("DefaultRegisterID", "@DefaultRegisterID"), new FieldValue("LastSyncTime", "@LastSyncTime"), new FieldValue("SyncInterval", "@SyncInterval"), new FieldValue("Status", "@Status"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateDataSyncCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDataSyncText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDataSyncText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@DataSyncID", SqlDbType.NVarChar);
			parameters.Add("@DataSyncName", SqlDbType.NVarChar);
			parameters.Add("@Type", SqlDbType.SmallInt);
			parameters.Add("@DatabaseName", SqlDbType.NVarChar);
			parameters.Add("@ServerID", SqlDbType.NVarChar);
			parameters.Add("@UserID", SqlDbType.NVarChar);
			parameters.Add("@Password", SqlDbType.NVarChar);
			parameters["@DataSyncID"].SourceColumn = "Code";
			parameters["@DataSyncName"].SourceColumn = "Name";
			parameters["@Type"].SourceColumn = "Type";
			parameters["@DatabaseName"].SourceColumn = "DatabaseName";
			parameters["@ServerID"].SourceColumn = "ServerID";
			parameters["@UserID"].SourceColumn = "UserID";
			parameters["@Password"].SourceColumn = "Password";
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

		private SqlCommand GetInsertUpdateDataSyncDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateDataSyncDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateDataSyncDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@DataSyncID", SqlDbType.NVarChar);
			parameters.Add("@SyncType", SqlDbType.SmallInt);
			parameters.Add("@RecordType", SqlDbType.SmallInt);
			parameters.Add("@Name", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@DefaultSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DefaultRegisterID", SqlDbType.NVarChar);
			parameters.Add("@LastSyncTime", SqlDbType.DateTime);
			parameters.Add("@SyncInterval", SqlDbType.Int);
			parameters.Add("@Status", SqlDbType.Bit);
			parameters["@DataSyncID"].SourceColumn = "Code";
			parameters["@SyncType"].SourceColumn = "SyncType";
			parameters["@RecordType"].SourceColumn = "RecordType";
			parameters["@Name"].SourceColumn = "Name";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@DefaultSysDocID"].SourceColumn = "DefaultSysDocID";
			parameters["@DefaultRegisterID"].SourceColumn = "DefaultRegisterID";
			parameters["@LastSyncTime"].SourceColumn = "LastSyncTime";
			parameters["@SyncInterval"].SourceColumn = "SyncInterval";
			parameters["@Status"].SourceColumn = "Status";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateDataSync(DataSyncData dataSyncData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateDataSyncCommand = GetInsertUpdateDataSyncCommand(isUpdate);
			try
			{
				string text = dataSyncData.Tables["Data_Sync"].Rows[0]["Code"].ToString();
				SqlTransaction sqlTransaction2 = insertUpdateDataSyncCommand.Transaction = base.DBConfig.StartNewTransaction();
				flag = (isUpdate ? (flag & Update(dataSyncData, "Data_Sync", insertUpdateDataSyncCommand)) : (flag & Insert(dataSyncData, "Data_Sync", insertUpdateDataSyncCommand)));
				insertUpdateDataSyncCommand = GetInsertUpdateDataSyncDetailsCommand(isUpdate: false);
				insertUpdateDataSyncCommand.Transaction = sqlTransaction2;
				if (isUpdate)
				{
					flag &= DeleteDataSyncDetailsRows(text, sqlTransaction2);
				}
				if (dataSyncData.Tables["Data_Sync_Detail"].Rows.Count > 0)
				{
					flag &= Insert(dataSyncData, "Data_Sync_Detail", insertUpdateDataSyncCommand);
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				if (!isUpdate)
				{
					AddActivityLog("Data Sync", text, ActivityTypes.Add, sqlTransaction2);
				}
				else
				{
					AddActivityLog("Data Sync", text, ActivityTypes.Update, sqlTransaction2);
				}
				UpdateTableRowInsertUpdateInfo("Data_Sync", "Code", text, sqlTransaction2, !isUpdate);
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

		internal bool DeleteDataSyncDetailsRows(string dataSyncID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Data_Sync_Detail WHERE Code = '" + dataSyncID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private string GetCustomerInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Customers", new FieldValue("OrgCode", "@OrgCode", isUpdateConditionField: true), new FieldValue("BilltoCode", "@BilltoCode"), new FieldValue("BillToName", "@BillToName"), new FieldValue("BillToName_A", "@BillToName_A"), new FieldValue("ShipToCode", "@ShipToCode"), new FieldValue("ShipToName", "@ShipToName"), new FieldValue("ShipToName_A", "@ShipToName_A"), new FieldValue("CustomerType", "@CustomerType"), new FieldValue("PaymentTermDays", "@PaymentTermDays"), new FieldValue("CreditLimit", "@CreditLimit"), new FieldValue("InActive", "@InActive"), new FieldValue("Address", "@Address"), new FieldValue("Address_A", "@Address_A"), new FieldValue("Phone", "@Phone"), new FieldValue("Mobile", "@Mobile"), new FieldValue("Latitude", "@Latitude"), new FieldValue("Longitude", "@Longitude"), new FieldValue("ChannelCode", "@ChannelCode"), new FieldValue("ChannelName", "@ChannelName"), new FieldValue("SubChannelCode", "@SubChannelCode"), new FieldValue("SubChannelName", "@SubChannelName"), new FieldValue("GroupCode", "@GroupCode"), new FieldValue("GroupName", "@GroupName"), new FieldValue("Taxable", "@Taxable"), new FieldValue("TRNO", "@TRNO"), new FieldValue("Cust_Ref_No", "@Cust_Ref_No"), new FieldValue("IsProcessed", "@IsProcessed"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetCustomerInsertUpdateCommand(bool isUpdate, string code, SqlConnection synConnection)
		{
			if (!isUpdate)
			{
				DataSet dataSyncConn = GetDataSyncConn(code);
				string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
				string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
				string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
				string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
				synConnection = new SqlConnection("Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";Pooling=false;");
				insertCommand = new SqlCommand(GetCustomerInsertUpdateText(isUpdate: false), synConnection);
				insertCommand.CommandType = CommandType.Text;
				_ = insertCommand.Parameters;
				synConnection.Open();
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private SqlCommand GetItemInsertUpdateCommand(bool isUpdate, string code, SqlConnection synConnection)
		{
			if (!isUpdate)
			{
				DataSet dataSyncConn = GetDataSyncConn(code);
				string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
				string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
				string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
				string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
				synConnection = new SqlConnection("Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";Pooling=false;");
				insertCommand = new SqlCommand(GetItemInsertUpdateText(isUpdate: false), synConnection);
				insertCommand.CommandType = CommandType.Text;
				_ = insertCommand.Parameters;
				synConnection.Open();
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetItemInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Items", new FieldValue("OrgCode", "@OrgCode", isUpdateConditionField: true), new FieldValue("ItemCode", "@ItemCode"), new FieldValue("ItemName", "@ItemName"), new FieldValue("ItemName_A", "@ItemName_A"), new FieldValue("ItemUOM", "@ItemUOM"), new FieldValue("ConversionFactor", "@ConversionFactor"), new FieldValue("ItemCategoryCode", "@ItemCategoryCode"), new FieldValue("ItemCategoryName", "@ItemCategoryName"), new FieldValue("ItemCategoryName_A", "@ItemCategoryName_A"), new FieldValue("ItemDivisionCode", "@ItemDivisionCode"), new FieldValue("ItemDivisionName", "@ItemDivisionName"), new FieldValue("ItemDivisionName_A", "@ItemDivisionName_A"), new FieldValue("ItemBrandCode", "@ItemBrandCode"), new FieldValue("ItemBrandName", "@ItemBrandName"), new FieldValue("ItemBrandName_A", "@ItemBrandName_A"), new FieldValue("PackBarcode", "@PackBarcode"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("InActive", "@InActive"), new FieldValue("IsProcessed", "@IsProcessed"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		public bool InsertDataSync(DataSyncData accountDataSyncData)
		{
			bool result = true;
			SqlCommand sqlCommand = null;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (sqlCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDataSyncData, "Data_Sync", sqlCommand);
				string text = accountDataSyncData.DataSyncTable.Rows[0]["Code"].ToString();
				AddActivityLog("DataSync", text, "", ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Data_Sync", "Code", text, sqlTransaction, isInsert: true);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool UpdateDataSync(DataSyncData accountDataSyncData)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (sqlCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountDataSyncData, "Data_Sync", sqlCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountDataSyncData.DataSyncTable.Rows[0]["Code"];
				UpdateTableRowByID("Data_Sync", "Code", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountDataSyncData.DataSyncTable.Rows[0]["Name"].ToString();
				AddActivityLog("DataSync", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Data_Sync", "Code", obj, sqlTransaction, isInsert: false);
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

		public DataSet InsertCustomerDataSync(DataTable dtupdateCustomer, string code)
		{
			DataSet dataSet = new DataSet();
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				dataSet = BuildSyncActivityDataTables(dataSet);
				dataSet = BuildSyncSummaryDataTables(dataSet);
				dataSet = insertSynRow(dataSet, SyncActivityTypes.Start, "", "", DateTime.Now, "Customer Sync Process Begins");
				num = dtupdateCustomer.Rows.Count;
				for (int i = 0; i < dtupdateCustomer.Rows.Count; i++)
				{
					try
					{
						DeleteField(code, "1", dtupdateCustomer.Rows[i]["CustomerID"].ToString(), dtupdateCustomer.Rows[i]["ShipToCode"].ToString());
						SqlConnection sqlConnection = new SqlConnection();
						SqlCommand customerInsertUpdateCommand = GetCustomerInsertUpdateCommand(isUpdate: false, code, sqlConnection);
						customerInsertUpdateCommand.Parameters.Add("@OrgCode", SqlDbType.NVarChar).Value = "ARTIN";
						customerInsertUpdateCommand.Parameters.Add("@BilltoCode", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["CustomerID"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@BillToName", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["CustomerName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@BillToName_A", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["CustomerName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@ShipToCode", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["ShipToCode"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@ShipToName", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["ShipToName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@ShipToName_A", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["ShipToName"].ToString();
						string s = " 1";
						int result = 0;
						if (dtupdateCustomer.Rows[i]["CreditLimitType"].ToString() == "2")
						{
							s = "1";
						}
						else if (dtupdateCustomer.Rows[i]["CreditLimitType"].ToString() == "3")
						{
							s = "2";
						}
						else if (dtupdateCustomer.Rows[i]["CreditLimitType"].ToString() == "1")
						{
							s = "2";
						}
						base.DBConfig.StartNewTransaction();
						int.TryParse(dtupdateCustomer.Rows[i]["NetDays"].ToString(), out result);
						customerInsertUpdateCommand.Parameters.Add("@CustomerType", SqlDbType.Int).Value = int.Parse(s);
						customerInsertUpdateCommand.Parameters.Add("@PaymentTermDays", SqlDbType.Int).Value = result;
						decimal result2 = default(decimal);
						decimal.TryParse(dtupdateCustomer.Rows[i]["CreditAmount"].ToString(), out result2);
						customerInsertUpdateCommand.Parameters.Add("@CreditLimit", SqlDbType.Decimal).Value = result2;
						customerInsertUpdateCommand.Parameters.Add("@InActive", SqlDbType.Bit).Value = dtupdateCustomer.Rows[i]["IsInactive"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Address", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["Address"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Address_A", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["Address_A"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["Phone1"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["Mobile"].ToString();
						string text = "";
						string text2 = "";
						text = dtupdateCustomer.Rows[i]["Latitude"].ToString();
						text2 = dtupdateCustomer.Rows[i]["Longitude"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Latitude", SqlDbType.NVarChar).Value = text;
						customerInsertUpdateCommand.Parameters.Add("@Longitude", SqlDbType.NVarChar).Value = text2;
						customerInsertUpdateCommand.Parameters.Add("@ChannelCode", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["CustomerClassID"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@ChannelName", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["ClassName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@SubChannelCode", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["CustomerGroupID"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@SubChannelName", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["GroupName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@GroupCode", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["PriceLevelID"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@GroupName", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["PriceLevelName"].ToString();
						bool flag = true;
						if (dtupdateCustomer.Rows[0]["TaxOption"].ToString() == "2")
						{
							flag = false;
						}
						customerInsertUpdateCommand.Parameters.Add("@Taxable", SqlDbType.Bit).Value = flag;
						customerInsertUpdateCommand.Parameters.Add("@TRNO", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["TaxIDNumber"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@Cust_Ref_No", SqlDbType.NVarChar).Value = dtupdateCustomer.Rows[i]["LegalName"].ToString();
						customerInsertUpdateCommand.Parameters.Add("@IsProcessed", SqlDbType.Bit).Value = false;
						bool num4 = customerInsertUpdateCommand.ExecuteNonQuery() > 0;
						sqlConnection.Close();
						base.DBConfig.EndTransaction(result: true);
						if (num4)
						{
							dataSet = insertSynRow(dataSet, SyncActivityTypes.None, dtupdateCustomer.Rows[i]["CustomerID"].ToString(), "", DateTime.Now, dtupdateCustomer.Rows[i]["CustomerID"].ToString() + " " + dtupdateCustomer.Rows[i]["ShipToCode"].ToString() + " --saved successfully.");
							num2++;
						}
					}
					catch (Exception ex)
					{
						dataSet = insertSynRow(dataSet, SyncActivityTypes.Bug, dtupdateCustomer.Rows[i]["CustomerID"].ToString(), "", DateTime.Now, dtupdateCustomer.Rows[i]["CustomerID"].ToString() + " --" + ex.Message);
						num3++;
					}
				}
				dataSet = insertSynRow(dataSet, SyncActivityTypes.Stop, "", "", DateTime.Now, "Customer Sync Process Stops");
				dataSet = insertSyncSumary(dataSet, num, num2, num3);
				return dataSet;
			}
			catch (Exception ex2)
			{
				return insertSynRow(dataSet, SyncActivityTypes.Stop, "", "", DateTime.Now, ex2.Message);
			}
		}

		public DataSet InsertItemDataSync(DataTable dtupdateItem, string code)
		{
			DataSet dataSet = new DataSet();
			try
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				dataSet = BuildSyncActivityDataTables(dataSet);
				dataSet = BuildSyncSummaryDataTables(dataSet);
				dataSet = insertSynRow(dataSet, SyncActivityTypes.Start, "", "", DateTime.Now, "Item Sync Process Begins");
				num = dtupdateItem.Rows.Count;
				for (int i = 0; i < dtupdateItem.Rows.Count; i++)
				{
					try
					{
						DeleteField(code, "25", dtupdateItem.Rows[i]["ProductID"].ToString(), dtupdateItem.Rows[i]["UnitID"].ToString());
						SqlConnection sqlConnection = new SqlConnection();
						SqlCommand itemInsertUpdateCommand = GetItemInsertUpdateCommand(isUpdate: false, code, sqlConnection);
						SqlTransaction sqlTransaction = null;
						itemInsertUpdateCommand.Parameters.Add("@OrgCode", SqlDbType.NVarChar).Value = "ARTIN";
						itemInsertUpdateCommand.Parameters.Add("@ItemCode", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["ProductID"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemName", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["Description"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemName_A", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["Description"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemUOM", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["UnitID"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ConversionFactor", SqlDbType.Decimal).Value = dtupdateItem.Rows[i]["ConvertionFactor"].ToString();
						decimal num4 = default(decimal);
						sqlTransaction = base.DBConfig.StartNewTransaction();
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Tax_Group_Detail", "TaxCode", "TaxGroupID", dtupdateItem.Rows[i]["TaxGroupID"].ToString(), sqlTransaction);
						object obj = null;
						if (fieldValue != null)
						{
							obj = new Databases(base.DBConfig).GetFieldValue("Tax", "TaxRate", "TaxCode", fieldValue.ToString(), sqlTransaction);
						}
						if (obj != null)
						{
							num4 = decimal.Parse(obj.ToString());
						}
						itemInsertUpdateCommand.Parameters.Add("@TaxPercentage", SqlDbType.Decimal).Value = num4;
						itemInsertUpdateCommand.Parameters.Add("@ItemDivisionCode", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["ClassID"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemDivisionName", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["ClassName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemDivisionName_A", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["ClassName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemCategoryCode", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["CategoryID"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemCategoryName", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["CategoryName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemCategoryName_A", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["CategoryName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemBrandCode", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["BrandID"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemBrandName", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["BrandName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@ItemBrandName_A", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["BrandName"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@PackBarCode", SqlDbType.NVarChar).Value = dtupdateItem.Rows[i]["UPC"].ToString();
						itemInsertUpdateCommand.Parameters.Add("@SubChannelCode", SqlDbType.NVarChar).Value = DBNull.Value;
						itemInsertUpdateCommand.Parameters.Add("@InActive", SqlDbType.Bit).Value = bool.Parse(dtupdateItem.Rows[i]["IsInactive"].ToString());
						itemInsertUpdateCommand.Parameters.Add("@IsProcessed", SqlDbType.Bit).Value = false;
						bool num5 = itemInsertUpdateCommand.ExecuteNonQuery() > 0;
						sqlConnection.Close();
						if (num5)
						{
							dataSet = insertSynRow(dataSet, SyncActivityTypes.None, dtupdateItem.Rows[i]["ProductID"].ToString(), "", DateTime.Now, dtupdateItem.Rows[i]["ProductID"].ToString() + " --" + dtupdateItem.Rows[i]["UnitID"].ToString() + " saved successfully.");
							num2++;
						}
					}
					catch (Exception ex)
					{
						dataSet = insertSynRow(dataSet, SyncActivityTypes.Bug, dtupdateItem.Rows[i]["ProductID"].ToString(), "", DateTime.Now, dtupdateItem.Rows[i]["ProductID"].ToString() + " --" + dtupdateItem.Rows[i]["UnitID"].ToString() + "--" + ex.Message);
						num3++;
					}
				}
				dataSet = insertSynRow(dataSet, SyncActivityTypes.Stop, "", "", DateTime.Now, "Item Sync Process Stops");
				dataSet = insertSyncSumary(dataSet, num, num2, num3);
				dtupdateItem.Rows[0]["ProductID"].ToString();
				return dataSet;
			}
			catch (Exception ex2)
			{
				return insertSynRow(dataSet, SyncActivityTypes.Stop, "", "", DateTime.Now, ex2.Message);
			}
		}

		public DataSyncData GetDataSync()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Data_Sync");
			DataSyncData dataSyncData = new DataSyncData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSyncData, "Data_Sync", sqlBuilder);
			return dataSyncData;
		}

		public bool DeleteDataSync(string degreeID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Data_Sync WHERE Code = '" + degreeID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("DataSync", degreeID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetDataSyncBySales(string docType, string code)
		{
			try
			{
				DataSet result = new DataSet();
				if (docType == "26")
				{
					docType = "1";
					result = RunSalesSyncron(docType, code);
				}
				else if (docType == "28")
				{
					docType = "2";
					result = RunSalesReturnSyncron(docType, code);
				}
				return result;
			}
			catch (SqlException)
			{
				throw;
			}
		}

		public DataSyncData GetDataSyncByID(string id)
		{
			try
			{
				DataSyncData dataSyncData = new DataSyncData();
				string textCommand = "SELECT * FROM Data_Sync WHERE Code='" + id + "'";
				FillDataSet(dataSyncData, "Data_Sync", textCommand);
				if (dataSyncData == null || dataSyncData.Tables.Count == 0 || dataSyncData.Tables["Data_Sync"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT distinct RecordType,* FROM   Data_Sync_Detail\r\n\t\t\t\t\t\tWHERE Code='" + id + "' order by Rowindex asc";
				FillDataSet(dataSyncData, "Data_Sync_Detail", textCommand);
				return dataSyncData;
			}
			catch
			{
				throw;
			}
		}

		public DataSet RunSalesReturnSyncron(string docType, string code)
		{
			try
			{
				DataSet syncTables = new DataSet();
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				syncTables = BuildSyncActivityDataTables(syncTables);
				syncTables = BuildSyncSummaryDataTables(syncTables);
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Start, "", "", DateTime.Now, "Sales Return Sync Process Begins");
				DataSet dataSet = PullSalesData(docType, code);
				DataTable dataTable = dataSet.Tables[0];
				num = dataTable.Rows.Count;
				foreach (DataRow row in dataTable.Rows)
				{
					string text = row["TransactionNumber"].ToString();
					string text2 = row["BillToCode"].ToString();
					DateTime dateTime = DateTime.Parse(row["TransactionDate"].ToString());
					string value = row["SalesmanCode"].ToString();
					decimal num4 = decimal.Parse(row["InvoiceDiscount"].ToString());
					decimal d = default(decimal);
					decimal num5 = decimal.Parse(row["InvoiceTax"].ToString());
					decimal num6 = decimal.Parse(row["InvoiceAmount"].ToString());
					string value2 = row["Currency"].ToString();
					row["PONumber"].ToString();
					string value3 = "VANSALES";
					SqlTransaction sqlTransaction = null;
					sqlTransaction = base.DBConfig.StartNewTransaction();
					new SqlCommand().Transaction = sqlTransaction;
					string text3 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "28", sqlTransaction).ToString();
					if (new Databases(base.DBConfig).GetFieldValue("Customer", "CustomerID", "CustomerID", text2, sqlTransaction) == null)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text2 + " Customer not Available . Transaction No :-" + text);
						num3++;
					}
					else
					{
						object fieldValue = new Databases(base.DBConfig).GetFieldValue("Customer", "BillToAddressID", "CustomerID", text2, sqlTransaction);
						new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text3, sqlTransaction).ToString();
						string text4 = "";
						text4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultRegisterID", "RecordType", "28", sqlTransaction).ToString();
						string value4 = new Databases(base.DBConfig).GetFieldValue("Customer", "TaxGroupID", "CustomerID", text2, sqlTransaction).ToString();
						base.DBConfig.EndTransaction(result: true);
						row["RefTransactionNumber"].ToString();
						string value5 = "1";
						string value6 = null;
						SalesReturnData salesReturnData = null;
						salesReturnData = new SalesReturnData();
						DataRow dataRow2 = salesReturnData.SalesReturnTable.NewRow();
						dataRow2["TransactionDate"] = dateTime;
						dataRow2["SysDocID"] = text3;
						dataRow2["VoucherID"] = text;
						dataRow2["CompanyID"] = value5;
						dataRow2["DivisionID"] = value6;
						dataRow2["ShippingAddressID"] = fieldValue;
						dataRow2["IsCash"] = true;
						dataRow2["CustomerID"] = text2;
						dataRow2["RegisterID"] = text4;
						dataRow2["SalespersonID"] = value;
						dataRow2["CurrencyID"] = value2;
						dataRow2["CurrencyRate"] = 1;
						dataRow2["PayeeTaxGroupID"] = value4;
						dataRow2["Discount"] = num4;
						dataRow2["TaxAmount"] = num5;
						dataRow2["Total"] = num6;
						dataRow2.EndEdit();
						salesReturnData.SalesReturnTable.Rows.Add(dataRow2);
						salesReturnData.SalesReturnDetailTable.Rows.Clear();
						salesReturnData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
						DataView defaultView = dataSet.Tables[1].DefaultView;
						defaultView.RowFilter = "TransactionNumber='" + text + "'";
						DataTable dataTable2 = defaultView.ToTable();
						foreach (DataRow row2 in dataTable2.Rows)
						{
							DataRow dataRow4 = salesReturnData.SalesReturnDetailTable.NewRow();
							dataRow4.BeginEdit();
							string text6 = (string)(dataRow4["ProductID"] = row2["ItemCode"].ToString());
							dataRow4["Quantity"] = row2["Quantity"];
							dataRow4["UnitID"] = row2["UOM"];
							dataRow4["LocationID"] = value3;
							dataRow4["UnitPrice"] = row2["Price"];
							dataRow4["Description"] = new Databases(base.DBConfig).GetFieldValue("Product", "Description", "ProductID", text6, sqlTransaction).ToString();
							dataRow4["TaxAmount"] = row2["LineTax"];
							sqlTransaction = base.DBConfig.StartNewTransaction();
							new SqlCommand().Transaction = sqlTransaction;
							string value7 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxOption", "ProductID", text6, sqlTransaction).ToString();
							string value8 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxGroupID", "ProductID", text6, sqlTransaction).ToString();
							base.DBConfig.EndTransaction(result: true);
							dataRow4["TaxOption"] = value7;
							dataRow4["TaxGroupID"] = value8;
							dataRow4["Amount"] = row2["LineAmount"];
							int num7 = int.Parse(row2["LineNumber"].ToString());
							dataRow4["RowIndex"] = num7 - 1;
							dataRow4.EndEdit();
							salesReturnData.SalesReturnDetailTable.Rows.Add(dataRow4);
							DataView defaultView2 = dataSet.Tables[1].DefaultView;
							defaultView2.RowFilter = "TransactionNumber='" + text + "' AND ItemCode='" + text6 + "' AND LineNumber='" + num7.ToString() + "'";
							foreach (DataRow row3 in defaultView2.ToTable().Rows)
							{
								DataRow dataRow6 = salesReturnData.Tables["Product_Lot_Receiving_Detail"].NewRow();
								dataRow6["ProductID"] = row3["ItemCode"];
								dataRow6["LocationID"] = value3;
								string text7 = row3["BatchNumber"].ToString();
								sqlTransaction = base.DBConfig.StartNewTransaction();
								new SqlCommand().Transaction = sqlTransaction;
								object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Product_Lot", "LotNumber", "ItemCode", text6, "Reference", text7, sqlTransaction);
								if (fieldValue2 != null)
								{
									int num8 = int.Parse(fieldValue2.ToString());
									base.DBConfig.EndTransaction(result: true);
									dataRow6["LotNumber"] = text7;
									dataRow6["Reference"] = text7;
									dataRow6["SourceLotNumber"] = num8;
									dataRow6["BinID"] = DBNull.Value;
									dataRow6["Reference2"] = DBNull.Value;
									dataRow6["LotQty"] = row3["Quantity"].ToString();
									dataRow6["SysDocID"] = text3;
									dataRow6["VoucherID"] = text;
									dataRow6["RowIndex"] = num7 - 1;
									salesReturnData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow6);
									continue;
								}
								syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, "Lot not availble for Item " + text6 + "with Reference " + text7 + " Transaction No :-" + text);
								num3++;
								goto IL_0d2b;
							}
						}
						salesReturnData.PaymentTable.Rows.Clear();
						dataRow2 = salesReturnData.PaymentTable.NewRow();
						dataRow2["SysDocID"] = text3;
						dataRow2["VoucherID"] = text;
						dataRow2["RegisterID"] = text4;
						dataRow2["TransactionDate"] = dateTime;
						dataRow2["Amount"] = num6 - num4 + d;
						dataRow2["PaymentMethodType"] = PaymentMethodTypes.Cash;
						salesReturnData.PaymentTable.Rows.Add(dataRow2);
						salesReturnData.TaxDetailsTable.Rows.Clear();
						string value9 = "";
						foreach (DataRow row4 in dataTable2.Rows)
						{
							DataRow dataRow8 = salesReturnData.TaxDetailsTable.NewRow();
							dataRow8["SysDocID"] = text3;
							dataRow8["VoucherID"] = text;
							sqlTransaction = base.DBConfig.StartNewTransaction();
							new SqlCommand().Transaction = sqlTransaction;
							string text8 = "";
							string text9 = "";
							decimal num9 = default(decimal);
							text8 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxGroupID", "ProductID", row4["ItemCode"].ToString(), sqlTransaction).ToString();
							if (!string.IsNullOrEmpty(text8))
							{
								text9 = new Databases(base.DBConfig).GetFieldValue("Tax_Group_Detail", "TaxCode", "TaxGroupID", text8, sqlTransaction).ToString();
							}
							if (!string.IsNullOrEmpty(text9))
							{
								num9 = decimal.Parse(new Databases(base.DBConfig).GetFieldValue("Tax", "TaxRate", "TaxCode", text9, sqlTransaction).ToString());
							}
							base.DBConfig.EndTransaction(result: true);
							dataRow8["TaxGroupID"] = text8;
							dataRow8["TaxItemID"] = text9;
							value9 = text9;
							dataRow8["TaxLevel"] = 2;
							dataRow8["TaxRate"] = num9;
							dataRow8["CalculationMethod"] = (byte)1;
							dataRow8["TaxAmount"] = row4["LineTax"];
							dataRow8["CurrencyID"] = value2;
							dataRow8["CurrencyRate"] = 1;
							dataRow8["RowIndex"] = int.Parse(row4["LineNumber"].ToString()) - 1;
							dataRow8["OrderIndex"] = DBNull.Value;
							salesReturnData.TaxDetailsTable.Rows.Add(dataRow8);
						}
						DataRow dataRow9 = salesReturnData.TaxDetailsTable.NewRow();
						dataRow9["SysDocID"] = text3;
						dataRow9["VoucherID"] = text;
						dataRow9["TaxGroupID"] = value4;
						dataRow9["TaxItemID"] = value9;
						dataRow9["TaxLevel"] = 1;
						dataRow9["TaxRate"] = DBNull.Value;
						dataRow9["CalculationMethod"] = (byte)1;
						dataRow9["TaxAmount"] = num5;
						dataRow9["CurrencyID"] = value2;
						dataRow9["CurrencyRate"] = 1;
						dataRow9["RowIndex"] = -1;
						dataRow9["OrderIndex"] = DBNull.Value;
						salesReturnData.TaxDetailsTable.Rows.Add(dataRow9);
						if (string.IsNullOrEmpty(num5.ToString()))
						{
							salesReturnData.TaxDetailsTable.Clear();
						}
						if (string.IsNullOrEmpty(num5.ToString()))
						{
							salesReturnData.TaxDetailsTable.Clear();
						}
						try
						{
							new SalesReturn(base.DBConfig).InsertUpdateSalesReturn(salesReturnData, isUpdate: false);
							UpdateSyncStatus(text3, text, "28", code);
							syncTables = insertSynRow(syncTables, SyncActivityTypes.None, text, "", DateTime.Now, text + " --saved successfully.");
							num2++;
						}
						catch (Exception ex)
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text + " --" + ex.Message);
							num3++;
						}
					}
					IL_0d2b:;
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Stop, "", "", DateTime.Now, "Sales Return Sync Process Stops");
				syncTables = insertSyncSumary(syncTables, num, num2, num3);
				SqlTransaction sqlTransaction2 = null;
				sqlTransaction2 = base.DBConfig.StartNewTransaction();
				MemoryStream memoryStream = SerializeToStream(syncTables);
				insertSyncActvity(memoryStream.ToArray(), SyncActivityTypes.Bug, sqlTransaction2);
				base.DBConfig.EndTransaction(result: true);
				return syncTables;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataSet RunSalesSyncron(string docType, string code)
		{
			try
			{
				DataSet syncTables = new DataSet();
				int totalprocessed = 0;
				int num = 0;
				int num2 = 0;
				syncTables = BuildSyncActivityDataTables(syncTables);
				syncTables = BuildSyncSummaryDataTables(syncTables);
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Start, "", "", DateTime.Now, "Sales Sync Process Begins");
				DataSet dataSet = PullSalesData(docType, code);
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					totalprocessed = dataTable.Rows.Count;
				}
				foreach (DataRow row in dataTable.Rows)
				{
					string text = row["TransactionNumber"].ToString();
					string text2 = row["BillToCode"].ToString();
					DateTime dateTime = DateTime.Parse(row["TransactionDate"].ToString());
					string text3 = row["SalesmanCode"].ToString();
					decimal num3 = decimal.Parse(row["InvoiceDiscount"].ToString());
					decimal d = default(decimal);
					decimal num4 = decimal.Parse(row["InvoiceTax"].ToString());
					decimal num5 = decimal.Parse(row["InvoiceAmount"].ToString());
					string value = row["Currency"].ToString();
					row["PONumber"].ToString();
					string value2 = "VANSALES";
					string checkFieldValue = row["ShipToCode"].ToString();
					SqlTransaction sqlTransaction = null;
					sqlTransaction = base.DBConfig.StartNewTransaction();
					new SqlCommand().Transaction = sqlTransaction;
					string text4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "26", sqlTransaction).ToString();
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Customer", "CustomerID", "CustomerID", text2, sqlTransaction);
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Salesperson", "SalespersonID", "SalespersonID", text3, sqlTransaction);
					if (fieldValue == null)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text2 + " Customer not Available . Transaction No :-" + text);
						num2++;
					}
					else if (fieldValue2 == null)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text3 + " Salesman not Available . Transaction No :-" + text);
						num2++;
					}
					else
					{
						new Databases(base.DBConfig).GetFieldValue("System_Document", "LocationID", "SysDocID", text4, sqlTransaction).ToString();
						string text5 = "";
						text5 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultRegisterID", "RecordType", "26", sqlTransaction).ToString();
						string value3 = new Databases(base.DBConfig).GetFieldValue("Customer", "TaxGroupID", "CustomerID", text2, sqlTransaction).ToString();
						object fieldValue3 = new Databases(base.DBConfig).GetFieldValue("Customer_Address", "AddressID", "CustomerID", text2, "AddressID", checkFieldValue, sqlTransaction);
						new Databases(base.DBConfig).GetFieldValue("Customer", "BillToAddressID", "CustomerID", text2, sqlTransaction);
						if (fieldValue3 == null)
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, fieldValue3 + " ShipToCode not Available for " + text2 + " Transaction No :-" + text);
							num2++;
						}
						else
						{
							base.DBConfig.EndTransaction(result: true);
							int num6 = 5;
							row["RefTransactionNumber"].ToString();
							string value4 = "1";
							string value5 = null;
							SalesInvoiceData salesInvoiceData = null;
							if (salesInvoiceData == null)
							{
								salesInvoiceData = new SalesInvoiceData();
							}
							salesInvoiceData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
							DataRow dataRow2 = salesInvoiceData.SalesInvoiceTable.NewRow();
							dataRow2["SysDocID"] = text4;
							dataRow2["VoucherID"] = text;
							dataRow2["CustomerID"] = text2.Trim();
							dataRow2["IsCash"] = true;
							dataRow2["TransactionDate"] = dateTime;
							dataRow2["SalespersonID"] = text3;
							dataRow2["CurrencyID"] = value;
							dataRow2["CurrencyRate"] = 1;
							dataRow2["Discount"] = num3;
							dataRow2["Total"] = num5;
							dataRow2["TaxAmount"] = num4;
							dataRow2["RegisterID"] = text5;
							dataRow2["CompanyID"] = value4;
							dataRow2["DivisionID"] = value5;
							dataRow2["SalesFlow"] = SalesFlows.DirectInvoice;
							dataRow2["SourceDocType"] = ItemSourceTypes.None;
							dataRow2["PayeeTaxGroupID"] = value3;
							dataRow2["PaymentMethodType"] = num6;
							dataRow2["BillingAddressID"] = fieldValue3;
							dataRow2["ShippingAddressID"] = fieldValue3;
							dataRow2.EndEdit();
							salesInvoiceData.SalesInvoiceTable.Rows.Add(dataRow2);
							DataView defaultView = dataSet.Tables[1].DefaultView;
							defaultView.RowFilter = "TransactionNumber='" + text + "'";
							DataTable dataTable2 = defaultView.ToTable();
							foreach (DataRow row2 in dataTable2.Rows)
							{
								DataRow dataRow4 = salesInvoiceData.SalesInvoiceDetailTable.NewRow();
								dataRow4.BeginEdit();
								dataRow4["VoucherID"] = text;
								dataRow4["SysDocID"] = text4;
								string text7 = (string)(dataRow4["ProductID"] = row2["ItemCode"].ToString());
								if (new Databases(base.DBConfig).GetFieldValue("Product", "ProductID", "ProductID", text7, sqlTransaction) == null)
								{
									syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text7 + " ProductID not Available for Transaction No :-" + text);
									num2++;
								}
								else
								{
									decimal num7 = default(decimal);
									decimal num8 = default(decimal);
									dataRow4["Description"] = new Databases(base.DBConfig).GetFieldValue("Product", "Description", "ProductID", text7, sqlTransaction).ToString();
									dataRow4["Quantity"] = row2["Quantity"];
									num7 = decimal.Parse(row2["Quantity"].ToString());
									dataRow4["UnitID"] = row2["UOM"];
									dataRow4["UnitFactor"] = DBNull.Value;
									dataRow4["UnitPrice"] = row2["Price"];
									dataRow4["Discount"] = row2["LineDiscount"];
									dataRow4["RowIndex"] = int.Parse(row2["LineNumber"].ToString()) - 1;
									dataRow4["LocationID"] = value2;
									dataRow4["TaxAmount"] = row2["LineTax"];
									sqlTransaction = base.DBConfig.StartNewTransaction();
									new SqlCommand().Transaction = sqlTransaction;
									string value6 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxOption", "ProductID", text7, sqlTransaction).ToString();
									string value7 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxGroupID", "ProductID", text7, sqlTransaction).ToString();
									base.DBConfig.EndTransaction(result: true);
									if (!(decimal.Parse(row2["LineTax"].ToString()) > 0m) || !string.IsNullOrEmpty(value7))
									{
										dataRow4["TaxOption"] = value6;
										dataRow4["TaxGroupID"] = value7;
										dataRow4["Amount"] = row2["LineAmount"];
										int num9 = int.Parse(row2["LineNumber"].ToString());
										dataRow4.EndEdit();
										salesInvoiceData.SalesInvoiceDetailTable.Rows.Add(dataRow4);
										DataView defaultView2 = dataSet.Tables[1].DefaultView;
										defaultView2.RowFilter = "TransactionNumber='" + text + "' AND ItemCode='" + text7 + "' AND LineNumber='" + num9.ToString() + "'";
										num9--;
										foreach (DataRow row3 in defaultView2.ToTable().Rows)
										{
											DataRow dataRow6 = salesInvoiceData.Tables["Product_Lot_Issue_Detail"].NewRow();
											dataRow6["ProductID"] = row3["ItemCode"];
											dataRow6["LocationID"] = value2;
											string text8 = row3["BatchNumber"].ToString();
											if (string.IsNullOrEmpty(text8))
											{
												syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, "Batch not availble for Item " + text7 + " Transaction No :-" + text);
												num2++;
											}
											else
											{
												sqlTransaction = base.DBConfig.StartNewTransaction();
												new SqlCommand().Transaction = sqlTransaction;
												DataSet dataSet2 = new DataSet();
												string textCommand = "Select LotNumber,SoldQty,ItemCode,ReceiptDate,ReceiptNumber,LocationID,Cost,AvgCost, (LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0) AS LotQty,BinID,Reference2 FROM product_lot\r\n\t\t\t\t\t\t\t\tWHERE ItemCode IN ('" + text7 + "') and  \r\n\t\t\t\t\t\t\t\t(LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0)>0 and ISNULL(IsDeleted,'False') = 'False' and Reference='" + text8 + "'\r\n\t\t\t\t\t\t\t\tOrder BY ReceiptDate,ReceiptNumber,LotQty DESC ";
												FillDataSet(dataSet2, "Product_Lot", textCommand, sqlTransaction);
												base.DBConfig.EndTransaction(result: true);
												object obj = null;
												if (dataSet2.Tables[0].Rows.Count > 0)
												{
													obj = dataSet2.Tables[0].Rows[0]["LotNumber"];
													num8 = decimal.Parse(dataSet2.Tables[0].Rows[0]["LotQty"].ToString());
												}
												if (obj == null)
												{
													syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, "Lot Number not availble with batch " + text8 + " for Item " + text7 + " Transaction No :-" + text);
													num2++;
												}
												else
												{
													dataRow6["LotNumber"] = int.Parse(obj.ToString());
													dataRow6["Reference"] = text8;
													dataRow6["SourceLotNumber"] = DBNull.Value;
													dataRow6["BinID"] = DBNull.Value;
													dataRow6["Reference2"] = DBNull.Value;
													dataRow6["SoldQty"] = row3["Quantity"].ToString();
													if (num8 < num7)
													{
														dataRow6["SoldQty"] = num8;
													}
													dataRow6["Cost"] = 0.0;
													dataRow6["SysDocID"] = text4;
													dataRow6["VoucherID"] = text;
													dataRow6["UnitPrice"] = row3["Price"];
													dataRow6["RowIndex"] = num9;
													salesInvoiceData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow6);
													int num10 = 1;
													if (!(num7 > num8))
													{
														continue;
													}
													decimal d2 = num8;
													while (true)
													{
														obj = null;
														decimal num11 = num7 - d2;
														dataRow6 = salesInvoiceData.Tables["Product_Lot_Issue_Detail"].NewRow();
														if (dataSet2.Tables[0].Rows.Count > num10)
														{
															obj = dataSet2.Tables[0].Rows[num10]["LotNumber"];
															num8 = decimal.Parse(dataSet2.Tables[0].Rows[num10]["LotQty"].ToString());
														}
														if (obj == null)
														{
															syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, "Lot Number not availble with batch " + text8 + " for Item " + text7 + " Transaction No :-" + text);
															num2++;
															break;
														}
														dataRow6["ProductID"] = row3["ItemCode"];
														dataRow6["LocationID"] = value2;
														dataRow6["LotNumber"] = int.Parse(obj.ToString());
														dataRow6["Reference"] = text8;
														dataRow6["SourceLotNumber"] = DBNull.Value;
														dataRow6["BinID"] = DBNull.Value;
														dataRow6["Reference2"] = DBNull.Value;
														if (num11 < num8)
														{
															dataRow6["SoldQty"] = num11;
															d2 += num11;
														}
														else
														{
															dataRow6["SoldQty"] = num8;
															d2 += num8;
														}
														dataRow6["Cost"] = 0.0;
														dataRow6["SysDocID"] = text4;
														dataRow6["VoucherID"] = text;
														dataRow6["UnitPrice"] = row3["Price"];
														dataRow6["RowIndex"] = num9;
														salesInvoiceData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow6);
														if (num7 > d2)
														{
															num10++;
															continue;
														}
														goto IL_0e40;
													}
												}
											}
											goto IL_1825;
											IL_0e40:;
										}
										continue;
									}
									syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text7 + " Tax Group not Assigned . Transaction No :-" + text);
									num2++;
								}
								goto IL_1825;
							}
							foreach (DataRow row4 in salesInvoiceData.Tables["Product_Lot_Issue_Detail"].Rows)
							{
								row4["SysDocID"] = text4;
								row4["VoucherID"] = text;
								string text9 = row4["ProductID"].ToString();
								DataRow[] array = salesInvoiceData.SalesInvoiceDetailTable.Select("ProductID= '" + text9 + "' AND RowIndex = '" + row4["RowIndex"].ToString() + "'");
								string text10 = "";
								DataRow[] array2 = array;
								for (int i = 0; i < array2.Length; i++)
								{
									text10 = array2[i]["UnitID"].ToString();
								}
								string text11 = "";
								object fieldValue4 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text9, sqlTransaction);
								if (fieldValue4 != null)
								{
									text11 = fieldValue4.ToString();
								}
								if (text11 != "" && !string.IsNullOrEmpty(text10) && text10 != text11)
								{
									DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text9, text10) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text9 + "\nUnit:" + row4["UnitID"].ToString());
									float num12 = float.Parse(obj2["Factor"].ToString());
									string a = obj2["FactorType"].ToString();
									float num13 = float.Parse(row4["SoldQty"].ToString());
									num13 = ((!(a == "M")) ? float.Parse(Math.Round(num13 * num12, 5).ToString()) : float.Parse(Math.Round(num13 / num12, 5).ToString()));
									row4["SoldQty"] = num13;
								}
							}
							foreach (DataRow row5 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
							{
								row5["SysDocID"] = text4;
								row5["VoucherID"] = text;
								string text12 = row5["ProductID"].ToString();
								string text13 = "";
								object fieldValue5 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text12, sqlTransaction);
								if (fieldValue5 != null)
								{
									text13 = fieldValue5.ToString();
								}
								if (text13 != "" && row5["UnitID"] != DBNull.Value && row5["UnitID"].ToString() != text13)
								{
									DataRow obj3 = new Products(base.DBConfig).GetProductUnitRow(text12, row5["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text12 + "\nUnit:" + row5["UnitID"].ToString());
									float num14 = float.Parse(obj3["Factor"].ToString());
									string a2 = obj3["FactorType"].ToString();
									float num15 = float.Parse(row5["Quantity"].ToString());
									float num16 = float.Parse(row5["FOCQuantity"].ToString());
									decimal num17 = decimal.Parse(row5["UnitPrice"].ToString());
									if (a2 == "M")
									{
										num15 = float.Parse(Math.Round(num15 / num14, 5).ToString());
										num16 = float.Parse(Math.Round(num16 / num14, 5).ToString());
										num17 *= decimal.Parse(num14.ToString());
									}
									else
									{
										num15 = float.Parse(Math.Round(num15 * num14, 5).ToString());
										num16 = float.Parse(Math.Round(num16 * num14, 5).ToString());
										num17 /= decimal.Parse(num14.ToString());
									}
									string text15 = (string)(row5["Remarks"] = row5["Quantity"].ToString() + "    " + row5["UnitID"]);
									row5["Quantity"] = num15;
									row5["FOCQuantity"] = num16;
									row5["UnitID"] = text13;
									row5["UnitPrice"] = num17;
								}
							}
							salesInvoiceData.PaymentTable.Rows.Clear();
							dataRow2 = salesInvoiceData.PaymentTable.NewRow();
							dataRow2["SysDocID"] = text4;
							dataRow2["VoucherID"] = text;
							dataRow2["RegisterID"] = text5;
							dataRow2["TransactionDate"] = dateTime;
							dataRow2["Amount"] = num5 - num3 + d + num4;
							dataRow2["PaymentMethodType"] = (byte)num6;
							salesInvoiceData.PaymentTable.Rows.Add(dataRow2);
							salesInvoiceData.TaxDetailsTable.Rows.Clear();
							string value8 = "";
							foreach (DataRow row6 in dataTable2.Rows)
							{
								DataRow dataRow10 = salesInvoiceData.TaxDetailsTable.NewRow();
								dataRow10["SysDocID"] = text4;
								dataRow10["VoucherID"] = text;
								sqlTransaction = base.DBConfig.StartNewTransaction();
								new SqlCommand().Transaction = sqlTransaction;
								string text16 = "";
								string text17 = "";
								decimal num18 = default(decimal);
								text16 = new Databases(base.DBConfig).GetFieldValue("Product", "TaxGroupID", "ProductID", row6["ItemCode"].ToString(), sqlTransaction).ToString();
								if (!string.IsNullOrEmpty(text16))
								{
									text17 = new Databases(base.DBConfig).GetFieldValue("Tax_Group_Detail", "TaxCode", "TaxGroupID", text16, sqlTransaction).ToString();
								}
								if (!string.IsNullOrEmpty(text17))
								{
									num18 = decimal.Parse(new Databases(base.DBConfig).GetFieldValue("Tax", "TaxRate", "TaxCode", text17, sqlTransaction).ToString());
								}
								base.DBConfig.EndTransaction(result: true);
								dataRow10["TaxGroupID"] = text16;
								dataRow10["TaxItemID"] = text17;
								value8 = text17;
								dataRow10["TaxLevel"] = 2;
								dataRow10["TaxRate"] = num18;
								dataRow10["CalculationMethod"] = (byte)1;
								dataRow10["TaxAmount"] = row6["LineTax"];
								dataRow10["CurrencyID"] = value;
								dataRow10["CurrencyRate"] = 1;
								int num19 = int.Parse(row6["LineNumber"].ToString());
								dataRow10["RowIndex"] = num19 - 1;
								dataRow10["OrderIndex"] = num19 - 1;
								salesInvoiceData.TaxDetailsTable.Rows.Add(dataRow10);
							}
							DataRow dataRow11 = salesInvoiceData.TaxDetailsTable.NewRow();
							dataRow11["SysDocID"] = text4;
							dataRow11["VoucherID"] = text;
							dataRow11["TaxGroupID"] = value3;
							dataRow11["TaxItemID"] = value8;
							dataRow11["TaxLevel"] = 1;
							dataRow11["TaxRate"] = DBNull.Value;
							dataRow11["CalculationMethod"] = (byte)1;
							dataRow11["TaxAmount"] = num4;
							dataRow11["CurrencyID"] = value;
							dataRow11["CurrencyRate"] = 1;
							dataRow11["RowIndex"] = -1;
							dataRow11["OrderIndex"] = DBNull.Value;
							salesInvoiceData.TaxDetailsTable.Rows.Add(dataRow11);
							if (string.IsNullOrEmpty(num4.ToString()))
							{
								salesInvoiceData.TaxDetailsTable.Clear();
							}
							new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Invoice", "VoucherID", text4, text, sqlTransaction);
							try
							{
								if (new SalesInvoice(base.DBConfig).InsertUpdateSalesInvoice(salesInvoiceData, isUpdate: false, TempSave: false))
								{
									UpdateSyncStatus(text4, text, "26", code);
								}
								syncTables = insertSynRow(syncTables, SyncActivityTypes.None, text, "", DateTime.Now, text + " --saved successfully.");
								num++;
							}
							catch (Exception ex)
							{
								syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text + " --" + ex.Message);
								num2++;
							}
						}
					}
					IL_1825:;
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Stop, "", "", DateTime.Now, "Sales Sync Process Stops");
				syncTables = insertSyncSumary(syncTables, totalprocessed, num, num2);
				SqlTransaction sqlTransaction2 = null;
				sqlTransaction2 = base.DBConfig.StartNewTransaction();
				MemoryStream memoryStream = SerializeToStream(syncTables);
				insertSyncActvity(memoryStream.ToArray(), SyncActivityTypes.Bug, sqlTransaction2);
				base.DBConfig.EndTransaction(result: true);
				return syncTables;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataSet RunNewCustomerSyncron(string code)
		{
			try
			{
				DataSet syncTables = new DataSet();
				int totalprocessed = 0;
				int num = 0;
				int num2 = 0;
				syncTables = BuildSyncActivityDataTables(syncTables);
				syncTables = BuildSyncSummaryDataTables(syncTables);
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Start, "", "", DateTime.Now, "New Customer Sync Process Begins");
				DataTable dataTable = PullNewCustomerData(code).Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					totalprocessed = dataTable.Rows.Count;
				}
				foreach (DataRow row in dataTable.Rows)
				{
					CustomerData customerData = new CustomerData();
					DataRow dataRow2 = customerData.CustomerTable.NewRow();
					dataRow2.BeginEdit();
					string text = (string)(dataRow2["CustomerID"] = GetNextCustomerID(row["CustomerName"].ToString()[0].ToString()));
					dataRow2["CustomerName"] = row["CustomerName"].ToString();
					dataRow2["ShortName"] = "";
					dataRow2["LegalName"] = row["CustomerCode"].ToString();
					dataRow2["PaymentTermID"] = "P-10002";
					int result = 1;
					int.TryParse(row["CustomerType"].ToString(), out result);
					decimal result2 = default(decimal);
					decimal.TryParse(row["CreditLimit"].ToString(), out result2);
					switch (result)
					{
					case 0:
						dataRow2["CreditLimitType"] = "";
						break;
					case 1:
						dataRow2["CreditLimitType"] = CreditLimitTypes.NoCredit;
						break;
					case 2:
						dataRow2["CreditLimitType"] = CreditLimitTypes.CreditAmount;
						dataRow2["CreditAmount"] = result2;
						break;
					}
					bool result3 = false;
					bool.TryParse(row["Taxable"].ToString(), out result3);
					if (result3)
					{
						dataRow2["TaxOption"] = (byte)1;
						dataRow2["TaxGroupID"] = "TAX01";
					}
					else
					{
						dataRow2["TaxOption"] = (byte)2;
					}
					if (result3)
					{
						dataRow2["TaxGroupID"] = new CompanyOption(base.DBConfig).GetCompanyOptionValue(CompanyOptionsEnum.DefaultTaxGroup, "");
					}
					else
					{
						dataRow2["TaxGroupID"] = DBNull.Value;
					}
					dataRow2["TaxIDNumber"] = row["TRNO"].ToString();
					dataRow2.EndEdit();
					customerData.CustomerTable.Rows.Add(dataRow2);
					dataRow2 = customerData.CustomerAddressTable.NewRow();
					dataRow2.BeginEdit();
					dataRow2["CustomerID"] = text;
					dataRow2["AddressID"] = "PRIMARY";
					dataRow2["AddressName"] = row["CustomerName"].ToString();
					dataRow2["Address1"] = row["Address"].ToString();
					dataRow2["Phone1"] = row["Phone"].ToString();
					dataRow2["Latitude"] = row["GPSlattitude"].ToString();
					dataRow2["Longitude"] = row["GPSLongitude"].ToString();
					dataRow2.EndEdit();
					customerData.CustomerAddressTable.Rows.Add(dataRow2);
					customerData.Tables.Add("UDF");
					if (new Databases(base.DBConfig).ExistFieldValue("Customer", "CustomerID", text))
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text + " Document number already exist.");
					}
					else
					{
						try
						{
							if (new Customers(base.DBConfig).InsertUpdateCustomer(customerData, isUpdate: false))
							{
								UpdateSyncStatus("", row["CustomerCode"].ToString(), "101", code);
							}
							syncTables = insertSynRow(syncTables, SyncActivityTypes.None, text, "", DateTime.Now, row["CustomerCode"].ToString() + " converted to " + text + " PRIMARY -- saved successfully.");
							num++;
						}
						catch (Exception ex)
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text, "", DateTime.Now, text + " PRIMARY  -- " + ex.Message);
							num2++;
						}
					}
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Stop, "", "", DateTime.Now, "New Customer Sync Process Stops");
				syncTables = insertSyncSumary(syncTables, totalprocessed, num, num2);
				SqlTransaction sqlTransaction = null;
				sqlTransaction = base.DBConfig.StartNewTransaction();
				MemoryStream memoryStream = SerializeToStream(syncTables);
				insertSyncActvity(memoryStream.ToArray(), SyncActivityTypes.Bug, sqlTransaction);
				base.DBConfig.EndTransaction(result: true);
				return syncTables;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private MemoryStream SerializeToStream(object data)
		{
			MemoryStream memoryStream = new MemoryStream();
			((IFormatter)new BinaryFormatter()).Serialize((Stream)memoryStream, data);
			return memoryStream;
		}

		public DataSet PullSalesData(string docType, string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "select * from SalesInvoiceHeader where ISNULL(TransactionNumber,'')<>'' and  ISNULL(IsProcessed,0)=0 AND DocumentType=" + int.Parse(docType) + " order by TransactionNumber asc";
			string cmdText2 = "select * from SalesInvoiceDetails  order by TransactionNumber asc";
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			SqlCommand selectCommand2 = new SqlCommand(cmdText2, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			new SqlDataAdapter(selectCommand).Fill(dataSet, "Sales_Invoice");
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand2);
			sqlDataAdapter.Fill(dataSet, "Sales_Invoice_Detail");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		public DataSet PullNewCustomerData(string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "select * from NewCustomer where ISNULL(IsProcessed,0)=0 order by CustomerCode asc";
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
			sqlDataAdapter.Fill(dataSet, "NewCustomer");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		private bool FillSynDataSet(DataSet dataSet, string srcTable, string textCommand, string connString)
		{
			bool result = true;
			SqlConnection sqlConnection = new SqlConnection(connString);
			try
			{
				new SqlCommand
				{
					CommandText = textCommand,
					CommandType = CommandType.Text
				};
				sqlConnection.Open();
				dsCommand.Fill(dataSet, srcTable);
				return result;
			}
			catch
			{
				throw;
			}
			finally
			{
				sqlConnection.Close();
			}
		}

		public DataSet GetDataSyncByFields(params string[] columns)
		{
			return GetDataSyncByFields(null, isInactive: true, columns);
		}

		public DataSet GetDataSyncByFields(string[] degreeID, params string[] columns)
		{
			return GetDataSyncByFields(degreeID, isInactive: true, columns);
		}

		public DataSet GetDataSyncByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Data_Sync");
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
				commandHelper.FieldName = "Code";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Data_Sync";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Data_Sync", sqlBuilder);
			return dataSet;
		}

		public DataSet GetDataSyncList()
		{
			try
			{
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT Code,Name FROM Data_Sync ";
				FillDataSet(dataSet, "Data_Sync", textCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Data_Sync"].Rows.Count == 0)
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

		public DataSet GetDataSyncComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT DataSyncID [Code],DataSyncName [Name]\r\n                           FROM DataSync ORDER BY DataSyncID,DataSyncName";
			FillDataSet(dataSet, "Data_Sync", textCommand);
			return dataSet;
		}

		public bool CreateCustomerSync(DataSet accountDataSyncData)
		{
			bool result = true;
			SqlCommand sqlCommand = null;
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (sqlCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountDataSyncData, "Data_Sync", sqlCommand);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		private static string CreateSyncConnectionString(DataSet serverDetails)
		{
			SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
			if (!string.IsNullOrWhiteSpace(serverDetails.Tables[0].Rows[0][0].ToString()))
			{
				sqlConnectionStringBuilder.DataSource = "";
				if (!string.IsNullOrWhiteSpace(serverDetails.Tables[0].Rows[0][0].ToString()))
				{
					sqlConnectionStringBuilder.InitialCatalog = "";
				}
				sqlConnectionStringBuilder.IntegratedSecurity = false;
				if (!sqlConnectionStringBuilder.IntegratedSecurity)
				{
					sqlConnectionStringBuilder.UserID = "sa";
					sqlConnectionStringBuilder.Password = "sa1";
				}
			}
			return sqlConnectionStringBuilder.ConnectionString;
		}

		public bool UpdateSyncStatus(string PaymentNumber, string InvoiceNumber, string RecordType, string code)
		{
			bool result = false;
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "";
			switch (RecordType)
			{
			case "26":
			case "28":
				cmdText = " UPDATE SalesInvoiceHeader  SET IsProcessed = 1 Where TransactionNumber='" + InvoiceNumber + "'";
				break;
			case "3":
			case "2":
				cmdText = " UPDATE Collections  SET IsProcessed = 1 Where PaymentNumber='" + PaymentNumber + "'";
				break;
			case "20":
			case "19":
				cmdText = " UPDATE StockTransferHeader  SET IsProcessed = 1 Where TransactionNumber='" + InvoiceNumber + "'";
				break;
			case "101":
				cmdText = " UPDATE NewCustomer  SET IsProcessed = 1 Where CustomerCode='" + InvoiceNumber + "'";
				break;
			}
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			try
			{
				sqlConnection.Open();
				sqlCommand.ExecuteNonQuery();
				result = true;
				sqlConnection.Close();
				return result;
			}
			catch (SqlException ex)
			{
				MessageBox.Show(ex.Message);
				return result;
			}
		}

		public bool DeleteField(string code, string recType, string Field1, string Field2)
		{
			bool result = false;
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "";
			if (recType == "25")
			{
				cmdText = "DELETE FROM Items WHERE ItemCode = '" + Field1 + "' AND ItemUOM='" + Field2 + "'";
			}
			else if (recType == "1")
			{
				cmdText = "DELETE FROM  Customers WHERE BilltoCode = '" + Field1 + "'AND ShipToCode='" + Field2 + "'";
			}
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
			try
			{
				sqlConnection.Open();
				result = (sqlCommand.ExecuteNonQuery() > 0);
				sqlConnection.Close();
				return result;
			}
			catch (SqlException)
			{
				return result;
			}
		}

		public DataSet GetConnected(string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			SqlConnection sqlConnection = new SqlConnection("Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";");
			try
			{
				sqlConnection.Open();
				dataSyncConn.Tables[0].Columns.Add("Isconnected", typeof(bool));
				dataSyncConn.Tables[0].Rows[0]["Isconnected"] = true;
				sqlConnection.Close();
				return dataSyncConn;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public bool insertSyncActvity(byte[] activityData, SyncActivityTypes transaction, SqlTransaction sqlTransaction)
		{
			bool result = false;
			string value = "";
			SyncActivityTypes syncActivityTypes = SyncActivityTypes.None;
			SqlCommand sqlCommand = new SqlCommand("INSERT INTO [Sync_Activity](SyncActivityType,EntityID,SysDocID,SyncLogDate,UserID,MachineID,Description,ActivityDataView) VALUES\r\n                (@SyncActivityType,@EntityID,@SysDocID,@SyncLogDate,@UserID,@MachineID,@Description,@ActivityDataView)");
			sqlCommand.Parameters.AddWithValue("@SyncActivityType", syncActivityTypes);
			sqlCommand.Parameters.AddWithValue("@EntityID", "");
			sqlCommand.Parameters.AddWithValue("@SysDocID", "");
			sqlCommand.Parameters.AddWithValue("@SyncLogDate", DateTime.Now);
			sqlCommand.Parameters.AddWithValue("@UserID", base.DBConfig.AccessUser);
			sqlCommand.Parameters.AddWithValue("@MachineID", base.DBConfig.ClientMachineName);
			sqlCommand.Parameters.AddWithValue("@Description", value);
			sqlCommand.Parameters.AddWithValue("@ActivityDataView", activityData);
			sqlCommand.Transaction = sqlTransaction;
			if (ExecuteNonQuery(sqlCommand) > 0)
			{
				result = true;
			}
			return result;
		}

		private DataSet BuildSyncActivityDataTables(DataSet SyncTables)
		{
			DataTable dataTable = new DataTable("Sync_Activity");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("SyncActivityType", typeof(short));
			columns.Add("EntityID", typeof(string)).DefaultValue = "";
			columns.Add("SysDocID", typeof(string)).DefaultValue = "";
			columns.Add("SyncLogDate", typeof(DateTime));
			columns.Add("Status");
			columns.Add("UserID", typeof(string)).DefaultValue = "";
			columns.Add("MachineID", typeof(string)).DefaultValue = "";
			columns.Add("Description", typeof(string)).DefaultValue = "";
			columns.Add("ActivityDataView", typeof(string)).DefaultValue = "";
			SyncTables.Tables.Add(dataTable);
			return SyncTables;
		}

		private DataSet BuildSyncSummaryDataTables(DataSet SyncTables)
		{
			DataTable dataTable = new DataTable("Sync_Summary");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("Totalprocessed", typeof(long));
			columns.Add("Success", typeof(long));
			columns.Add("Failed", typeof(long));
			SyncTables.Tables.Add(dataTable);
			return SyncTables;
		}

		public DataSet insertSynRow(DataSet dsSync, SyncActivityTypes syncActivity, string EntityID, string SysDocID, DateTime RunTime, string ErrLog)
		{
			DataRow dataRow = dsSync.Tables["Sync_Activity"].NewRow();
			int num = (int)(SyncActivityTypes)Enum.Parse(typeof(SyncActivityTypes), syncActivity.ToString());
			dataRow["SyncActivityType"] = num;
			dataRow["EntityID"] = EntityID;
			dataRow["SysDocID"] = SysDocID;
			dataRow["SyncLogDate"] = RunTime;
			if (ErrLog != null && syncActivity == SyncActivityTypes.Bug)
			{
				dataRow["Status"] = "Failed";
			}
			else if (ErrLog != null && syncActivity == SyncActivityTypes.None)
			{
				dataRow["Status"] = "Success";
			}
			dataRow["Description"] = ErrLog;
			dataRow.EndEdit();
			dsSync.Tables["Sync_Activity"].Rows.Add(dataRow);
			return dsSync;
		}

		public DataSet insertSyncSumary(DataSet dsSync, int Totalprocessed, int Success, int Failed)
		{
			DataRow dataRow = dsSync.Tables["Sync_Summary"].NewRow();
			dataRow["Totalprocessed"] = Totalprocessed;
			dataRow["Success"] = Success;
			dataRow["Failed"] = Failed;
			dsSync.Tables["Sync_Summary"].Rows.Add(dataRow);
			return dsSync;
		}

		public DataSet GetDataSyncConn(string code)
		{
			string textCommand = "SELECT DatabaseName,ServerID,UserID,Password\r\n                           FROM Data_Sync where Code='" + code + "'";
			if (conncData.Tables.Count == 0)
			{
				FillDataSet(conncData, "Data_Sync", textCommand);
			}
			else if (conncData.Tables[0].Rows.Count == 0)
			{
				FillDataSet(conncData, "Data_Sync", textCommand);
			}
			return conncData;
		}

		public DataSet PullCollectionData(string paymentType, string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "";
			string cmdText2 = "";
			if (paymentType == "3")
			{
				cmdText = "select SUM(PaidAmount)[TotalPaid],BillToCode,ShipToCode,PaymentNumber,PaymentDate,EmployeeCode,Currency from Collections\r\n                    where ISNULL(IsProcessed,0)= 0 and paymentType IN(1) \r\n                    GROUP BY BillToCode,PaymentNumber,PaymentDate,ShipToCode,EmployeeCode,Currency";
				cmdText2 = "select * from Collections where ISNULL(IsProcessed,0)=0 and paymentType IN(1)  order by PaymentDate asc";
			}
			else if (paymentType == "2")
			{
				cmdText = "select SUM(PaidAmount)[TotalPaid],BillToCode,ShipToCode,PaymentNumber,PaymentDate,EmployeeCode,Currency,2 as [PaymentType],ChequeNumber,ChequeType,BankCode,ChequeDate from Collections\r\n                    where ISNULL(IsProcessed,0)= 0 and paymentType IN(2,3)\r\n                    GROUP BY BillToCode,PaymentNumber,PaymentDate,ShipToCode,EmployeeCode,Currency,ChequeNumber,ChequeType,BankCode,ChequeDate";
				cmdText2 = "select * from Collections where ISNULL(IsProcessed,0)=0 and paymentType IN (2,3) order by PaymentDate asc";
			}
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			new SqlDataAdapter(selectCommand).Fill(dataSet, "PaymentSummary");
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(cmdText2, sqlConnection));
			sqlDataAdapter.Fill(dataSet, "CollectionDetails");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		public DataSet RunCollectionsSync(string pType, string code)
		{
			try
			{
				DataSet syncTables = new DataSet();
				int totalprocessed = 0;
				int num = 0;
				int num2 = 0;
				syncTables = BuildSyncActivityDataTables(syncTables);
				syncTables = BuildSyncSummaryDataTables(syncTables);
				string text = "";
				if (pType == "3")
				{
					text = "Cash";
				}
				else if (pType == "2")
				{
					text = "Cheque";
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Start, "", "", DateTime.Now, "Collection " + text + "Sync  Process Begins");
				DataSet dataSet = PullCollectionData(pType, code);
				DataTable dataTable = new DataTable();
				if (dataSet.Tables[0].Rows.Count > 0)
				{
					dataTable = dataSet.Tables[0];
					totalprocessed = dataTable.Rows.Count;
				}
				foreach (DataRow row in dataTable.Rows)
				{
					string text2 = row["BillToCode"].ToString();
					string value = row["ShipToCode"].ToString();
					string text3 = row["PaymentNumber"].ToString();
					string invoiceNumber = "";
					DateTime dateTime = DateTime.Parse(row["PaymentDate"].ToString());
					row["EmployeeCode"].ToString();
					decimal num3 = decimal.Parse(row["TotalPaid"].ToString());
					string a = row["Currency"].ToString();
					a = ((!(a == "1.000000000")) ? "AED" : "AED");
					DateTime result = new DateTime(0L);
					string value2 = "";
					string value3 = "";
					if (pType == "2")
					{
						int.Parse(row["PaymentType"].ToString());
						value2 = row["ChequeNumber"].ToString();
						row["ChequeType"].ToString();
						value3 = row["BankCode"].ToString();
						DateTime.TryParse(row["ChequeDate"].ToString(), out result);
					}
					string text4 = "";
					string value4 = "";
					bool flag = false;
					SqlTransaction sqlTransaction = null;
					sqlTransaction = base.DBConfig.StartNewTransaction();
					new SqlCommand().Transaction = sqlTransaction;
					TransactionData transactionData = null;
					if (transactionData == null)
					{
						transactionData = new TransactionData();
					}
					DataRow dataRow2 = transactionData.TransactionTable.NewRow();
					dataRow2["TransactionID"] = 0;
					dataRow2["Description"] = null;
					dataRow2["CostCenterID"] = null;
					dataRow2["TransactionDate"] = dateTime;
					bool result2 = false;
					bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Customer", "IsParentPosting", "CustomerID", text2, sqlTransaction).ToString(), out result2);
					if (result2)
					{
						text2 = new Databases(base.DBConfig).GetFieldValue("Customer", "ParentCustomerID", "CustomerID", text2, sqlTransaction).ToString();
					}
					if (pType == "3")
					{
						text4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "3", sqlTransaction).ToString();
						value4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultRegisterID", "RecordType", "3", sqlTransaction).ToString();
						flag = false;
						dataRow2["SysDocType"] = (byte)3;
					}
					else if (pType == "2")
					{
						text4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "2", sqlTransaction).ToString();
						value4 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultRegisterID", "RecordType", "2", sqlTransaction).ToString();
						flag = true;
						dataRow2["SysDocType"] = (byte)2;
					}
					dataRow2["SysDocID"] = text4;
					dataRow2["VoucherID"] = text3;
					dataRow2["Reference"] = value;
					dataRow2["RegisterID"] = value4;
					dataRow2["DivisionID"] = null;
					dataRow2["CompanyID"] = 1;
					dataRow2["PayeeType"] = "C";
					dataRow2["PayeeID"] = text2;
					dataRow2["CurrencyID"] = a;
					dataRow2["CurrencyRate"] = 1;
					dataRow2["Amount"] = num3;
					dataRow2.EndEdit();
					transactionData.TransactionTable.Rows.Add(dataRow2);
					transactionData.TransactionDetailsTable.Rows.Clear();
					DataRow dataRow3 = transactionData.TransactionDetailsTable.NewRow();
					dataRow3.BeginEdit();
					if (!flag)
					{
						dataRow3["PaymentMethodType"] = (byte)1;
					}
					else
					{
						dataRow3["PaymentMethodType"] = (byte)2;
						dataRow3["BankID"] = value3;
						dataRow3["Description"] = "";
						dataRow3["CheckDate"] = result;
						dataRow3["CheckNumber"] = value2;
					}
					dataRow3["Description"] = text3;
					dataRow3["Amount"] = num3;
					dataRow3["RowIndex"] = 0;
					dataRow3.EndEdit();
					transactionData.TransactionDetailsTable.Rows.Add(dataRow3);
					try
					{
						if (new Transactions(base.DBConfig).InsertUpdateTransaction(transactionData, isUpdate: false))
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.None, text3, "", DateTime.Now, text3 + " -- collections " + text + " saved successfully.");
						}
					}
					catch (Exception ex)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text3, "", DateTime.Now, text3 + " --" + ex.Message);
						num2++;
						continue;
					}
					DataSet dataSet2 = null;
					DataSet dataSet3 = null;
					dataSet2 = new ARJournal(base.DBConfig).GetARPaymentToAllocate(text4, text3, text2, -1, flag);
					dataSet3 = new ARJournal(base.DBConfig).GetUnallocatedInvoices(text2);
					ARJournalData aRJournalData = null;
					aRJournalData = new ARJournalData();
					DataRow dataRow4 = null;
					string text5 = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "26", sqlTransaction).ToString();
					DataRow[] array = dataSet.Tables[1].Select("PaymentNumber = '" + text3 + "'");
					DataTable dataTable2 = array.CopyToDataTable();
					string text6 = "";
					if (array.Length != 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							text6 = array[i]["InvoiceNumber"].ToString();
							if (text6.Contains("SLCS"))
							{
								text6 = text6.Replace("SLCS", "");
							}
							DataRow[] array2 = dataSet3.Tables[0].Select("SysDocID = '" + text5 + "' AND VoucherID ='" + text6 + "'");
							if (array2.Length != 0)
							{
								string value5 = "";
								decimal num4 = default(decimal);
								DataRow[] array3 = array2;
								int num5 = 0;
								if (num5 < array3.Length)
								{
									DataRow obj = array3[num5];
									value5 = obj["JournalID"].ToString();
									num4 = decimal.Parse(obj["OriginalAmount"].ToString());
								}
								decimal num6 = default(decimal);
								array3 = dataTable2.Select("PaymentNumber = '" + text3 + "' AND InvoiceNumber ='" + text6 + "'");
								num5 = 0;
								if (num5 < array3.Length)
								{
									num6 = decimal.Parse(array3[num5]["PaidAmount"].ToString());
									if (num4 != num6)
									{
										num6 = num4;
									}
								}
								_ = dataSet2.Tables[0].Rows[0];
								dataRow4 = aRJournalData.Tables["AR_Payment_Allocation"].NewRow();
								dataRow4["InvoiceSysDocID"] = text5;
								dataRow4["InvoiceVoucherID"] = text6;
								dataRow4["PaymentSysDocID"] = text4;
								dataRow4["PaymentVoucherID"] = text3;
								dataRow4["CurrencyID"] = a;
								dataRow4["AllocationDate"] = dateTime;
								dataRow4["CustomerID"] = text2;
								dataRow4["ARJournalID"] = value5;
								dataRow4["PaymentAmount"] = num6;
								dataRow4["UnAllocatedAmount"] = num3 - num6;
								num3 -= num6;
								int num7 = int.Parse(new Databases(base.DBConfig).GetFieldValue("ARJournal", "ARID", "VoucherID", text3, "SysDocID", text4, sqlTransaction).ToString());
								dataRow4["PaymentARID"] = num7;
								dataRow4.EndEdit();
								aRJournalData.Tables["AR_Payment_Allocation"].Rows.Add(dataRow4);
							}
						}
						try
						{
							if (new ARJournal(base.DBConfig).InsertPaymentAllocation(aRJournalData))
							{
								syncTables = insertSynRow(syncTables, SyncActivityTypes.None, "", "", DateTime.Now, text3 + " allocated with invoice saved successfully.");
							}
							UpdateSyncStatus(text3, invoiceNumber, pType, code);
							num++;
						}
						catch (Exception ex2)
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, "", "", DateTime.Now, text3 + " allocated with  invoice --" + ex2.Message);
							num2++;
						}
					}
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Stop, "", "", DateTime.Now, "Collection " + text + "Sync  Process stops");
				syncTables = insertSyncSumary(syncTables, totalprocessed, num, num2);
				SqlTransaction sqlTransaction2 = null;
				sqlTransaction2 = base.DBConfig.StartNewTransaction();
				MemoryStream memoryStream = SerializeToStream(syncTables);
				insertSyncActvity(memoryStream.ToArray(), SyncActivityTypes.Bug, sqlTransaction2);
				base.DBConfig.EndTransaction(result: true);
				return syncTables;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataSet RunStockTransferSync(string transferType, string code)
		{
			DataSet result = new DataSet();
			if (transferType == "19")
			{
				result = RunStockTransferLoadSync(transferType, code);
			}
			else if (transferType == "20")
			{
				result = RunStockTransferLoadSync(transferType, code);
			}
			return result;
		}

		public bool UpdateLastSyncTime(string recType, string code)
		{
			bool result = false;
			SqlTransaction sqlTransaction = null;
			sqlTransaction = base.DBConfig.StartNewTransaction();
			new SqlCommand().Transaction = sqlTransaction;
			SqlCommand sqlCommand = new SqlCommand("UPDATE Data_Sync_Detail SET LastSyncTime=@LastSyncTime WHERE RecordType='" + recType + "' AND  Code='" + code + "' ");
			sqlCommand.Parameters.AddWithValue("@LastSyncTime", DateTime.Now);
			sqlCommand.Transaction = sqlTransaction;
			if (ExecuteNonQuery(sqlCommand) > 0)
			{
				result = true;
			}
			base.DBConfig.EndTransaction(result: true);
			return result;
		}

		public DataSet GetSyncCustomerList(string code)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(C.DateUpdated,C.DateCreated) as LastDoneTime,\r\n                        C.CustomerID,CustomerName,CreditAmount,C.IsInactive,CA.ADDRESSID [ShipToCode],CA.AddressName [ShipToName],CA.AddressName \r\n                        [ShipToName_A],CA.AddressPrintFormat[Address],CA.AddressPrintFormat[Address_A],CA.Phone1,CA.Mobile\r\n                        ,CA.Latitude,CA.Longitude,C.CustomerGroupID,CG.GroupName,CG.GroupName,TaxIDNumber,CreditLimitType,C.PaymentTermID,PT.NetDays,CreditAmount,C.TaxOption,C.CustomerClassID,CC.ClassName,PL.PriceLevelID,PL.PriceLevelName,C.LegalName \r\n                        FROM Customer C LEFT JOIN Customer_Address CA ON C.CustomerID=CA.CustomerID \t\r\n                        LEFT JOIN Customer_Group CG ON C.CustomerGroupID=CG.GroupID\r\n                        LEFT JOIN Payment_Term PT ON C.PaymentTermID=PT.PaymentTermID\r\n\t\t\t\t\t    LEFT JOIN Customer_Class CC ON C.CustomerClassID=CC.ClassID\t\r\n\t\t\t\t\t    LEFT OUTER JOIN ( SELECT\r\nrow_number() over (partition by ECD.ENTITYID order by ECD.ENTITYID) as rn\r\n , ECD.EntityID AS BB, ECD.CATEGORYID as PriceLevelID, EC.CategoryName as PriceLevelName,ECD.DateUpdated FROM Entity_Category_Detail ECD \r\nLEFT JOIN ENTITY_CATEGORY EC ON EC.EntityType  = ECD.EntityType AND EC.CategoryID = ECD.CategoryID  )  PL \r\nON PL.BB = C.CustomerID AND PL.rn=1 \r\n                        WHERE ISNULL(C.DateUpdated,C.DateCreated)> (select LastSyncTime from Data_Sync_Detail where RecordType =1 AND Code='" + code + "')\r\n                        OR  ISNULL(CA.DateUpdated,CA.DateCreated) > (select LastSyncTime from Data_Sync_Detail where RecordType =1 AND Code='" + code + "')\r\n                        OR  PL.DateUpdated > (select LastSyncTime from Data_Sync_Detail where RecordType =1 AND Code='" + code + "') \r\n                        ORDER BY C.CustomerID";
			FillDataSet(dataSet, "Customer", textCommand);
			return dataSet;
		}

		public DataSet GetSyncItemList(string code)
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT ISNULL(p.DateUpdated,p.DateCreated) as LastDoneTime,P.ProductID,P.Description,P.UnitID AS [MU], P.UnitID as UnitID,P.CategoryID,PC.CategoryName,P.BrandID,\r\n                            PB.BrandName,P.UPC,P.TaxGroupID,P.IsInactive,P.ClassID,PCS.ClassName\r\n                            ,1 as MainUnit,1 as 'Factor','M' as 'FactorType',1 as ConvertionFactor\r\n                            FROM Product P                            \r\n                            LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                            LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID \r\n                            LEFT JOIN Product_Class PCS ON PCS.ClassID=P.ClassID                             \r\n                            WHERE P.ItemType=1  AND ISNULL(P.DateUpdated,P.DateCreated)> (select LastSyncTime from Data_Sync_Detail where RecordType =25 AND Code='" + code + "')         \r\n                            union all\t\t\t\t\t\t\t\r\n                            SELECT ISNULL(p.DateUpdated,p.DateCreated) as LastDoneTime, P.ProductID,P.Description,P.UnitID AS [MU],PU.UnitID,P.CategoryID,PC.CategoryName\r\n                            ,P.BrandID,PB.BrandName,P.UPC,P.TaxGroupID,P.IsInactive,P.ClassID,PCS.ClassName,PU.IsMainUnit,PU.Factor,PU.FactorType\r\n                            ,case when PU.FactorType='D' then Factor ELSE 1/Factor END AS ConvertionFactor\r\n                            FROM Product P right  JOIN Product_Unit PU ON P.ProductID=PU.ProductID\r\n                            LEFT JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                            LEFT JOIN Product_Brand PB ON PB.BrandID=P.BrandID \r\n                            LEFT JOIN Product_Class PCS ON PCS.ClassID=P.ClassID                           \r\n                            WHERE P.ItemType=1  AND ISNULL(P.DateUpdated,P.DateCreated)> (select LastSyncTime from Data_Sync_Detail where RecordType =25 AND Code='" + code + "')                       \r\n                            ORDER BY P.ProductID";
			FillDataSet(dataSet, "Product", textCommand);
			return dataSet;
		}

		public DataSet RunSyncCustomers(string code)
		{
			DataSet syncCustomerList = GetSyncCustomerList(code);
			new DataSet();
			return InsertCustomerDataSync(syncCustomerList.Tables[0], code);
		}

		public DataSet RunSyncItems(string code)
		{
			DataSet syncItemList = GetSyncItemList(code);
			new DataSet();
			return InsertItemDataSync(syncItemList.Tables[0], code);
		}

		public static string ToDelimString(Hashtable input, string delimiter)
		{
			return string.Join(delimiter, (from string name in input.Keys
				select Convert.ToString(input[name].ToString())).ToArray());
		}

		public DataSet RunStockTransferLoadSync(string transferType, string code)
		{
			try
			{
				DataSet syncTables = new DataSet();
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				syncTables = BuildSyncActivityDataTables(syncTables);
				syncTables = BuildSyncSummaryDataTables(syncTables);
				string text = "";
				text = ((!(transferType == "19")) ? "OffLoad" : "Load");
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Start, "", "", DateTime.Now, "Stock Transfer- " + text + " Process Begins");
				DataSet dataSet = PullStockTransferData(transferType, code);
				DataTable dataTable = dataSet.Tables[0];
				num = dataTable.Rows.Count;
				foreach (DataRow row in dataTable.Rows)
				{
					string text2 = row["TransactionNumber"].ToString();
					string text3 = "";
					string text4 = "";
					float num4 = 1f;
					float num5 = 1f;
					if (transferType == "19")
					{
						text3 = row["VehiceCode"].ToString();
						text4 = row["WarehouseCode"].ToString();
					}
					else if (transferType == "20")
					{
						text3 = row["WarehouseCode"].ToString();
						text4 = row["VehiceCode"].ToString();
					}
					SqlTransaction sqlTransaction = null;
					sqlTransaction = base.DBConfig.StartNewTransaction();
					new SqlCommand().Transaction = sqlTransaction;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text4, sqlTransaction);
					object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Location", "InventoryAccountID", "LocationID", text3, sqlTransaction);
					if (fieldValue == null)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text2, "", DateTime.Now, text4 + " Location not Available . Transaction No :-" + text2);
						num3++;
					}
					else if (fieldValue2 == null)
					{
						syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text2, "", DateTime.Now, text3 + " Location not Available . Transaction No :-" + text2);
						num3++;
					}
					else
					{
						int.Parse(row["TransactionType"].ToString());
						DateTime dateTime = DateTime.Parse(row["TransactionDate"].ToString());
						DataView defaultView = dataSet.Tables[1].DefaultView;
						defaultView.RowFilter = "TransactionNumber='" + text2 + "'";
						DataTable dataTable2 = defaultView.ToTable();
						InventoryTransferData inventoryTransferData = null;
						string value = "";
						if (transferType == "19")
						{
							value = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "19", sqlTransaction).ToString();
						}
						else if (transferType == "20")
						{
							value = new Databases(base.DBConfig).GetFieldValue("Data_Sync_Detail", "DefaultSysDocID", "RecordType", "20", sqlTransaction).ToString();
						}
						if (inventoryTransferData == null)
						{
							inventoryTransferData = new InventoryTransferData();
						}
						inventoryTransferData.InventoryTransferDetailsTable.Rows.Clear();
						inventoryTransferData.Tables["Product_Lot_Issue_Detail"].Rows.Clear();
						inventoryTransferData.Tables["Product_Lot_Receiving_Detail"].Rows.Clear();
						DataRow dataRow2 = inventoryTransferData.InventoryTransferTable.NewRow();
						dataRow2["TransactionDate"] = dateTime;
						dataRow2["SysDocID"] = value;
						dataRow2["VoucherID"] = text2;
						dataRow2["LocationFromID"] = text4;
						dataRow2["LocationToID"] = text3;
						dataRow2.EndEdit();
						inventoryTransferData.InventoryTransferTable.Rows.Add(dataRow2);
						Hashtable hashtable = new Hashtable();
						foreach (DataRow row2 in dataTable2.Rows)
						{
							int num6 = int.Parse(row2["LineNumber"].ToString());
							string text5 = row2["ItemCode"].ToString();
							string text6 = row2["UOM"].ToString();
							decimal num7 = decimal.Parse(row2["Quantity"].ToString());
							num5 = float.Parse(row2["Quantity"].ToString());
							row2["BatchNumber"].ToString();
							DateTime dateTime2 = DateTime.Parse(row2["ExpiryDate"].ToString());
							num4 = 1f;
							float num8 = 1f;
							string text7 = "";
							object fieldValue3 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text5, sqlTransaction);
							string text8 = "";
							if (fieldValue3 != null)
							{
								text8 = fieldValue3.ToString();
							}
							if (text8 != "" && !string.IsNullOrEmpty(text6) && text6 != text8)
							{
								DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text5, text6) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text5 + "\nUnit:" + text6);
								num8 = float.Parse(obj2["Factor"].ToString());
								text7 = obj2["FactorType"].ToString();
								if (text7 == "M")
								{
									num4 = float.Parse(Math.Round(num4 / num8, 5).ToString());
									num5 = float.Parse(Math.Round(num5 / num8, 5).ToString());
								}
								else if (text7 == "D")
								{
									num4 = float.Parse(Math.Round(num4 * num8, 5).ToString());
									num5 = float.Parse(Math.Round(num5 * num8, 5).ToString());
								}
							}
							DataRow dataRow3 = inventoryTransferData.InventoryTransferDetailsTable.NewRow();
							dataRow3.BeginEdit();
							dataRow3["ProductID"] = text5;
							dataRow3["Quantity"] = num7;
							dataRow3["UnitID"] = text6;
							dataRow3["RowIndex"] = num6 - 1;
							dataRow3.EndEdit();
							inventoryTransferData.InventoryTransferDetailsTable.Rows.Add(dataRow3);
							DataView defaultView2 = dataSet.Tables[1].DefaultView;
							defaultView2.RowFilter = "TransactionNumber='" + text2 + "' AND ItemCode='" + text5 + "' AND LineNumber='" + num6.ToString() + "'";
							DataTable dataTable3 = defaultView2.ToTable();
							int num9 = 0;
							float num10 = 0f;
							bool flag = false;
							foreach (DataRow row3 in dataTable3.Rows)
							{
								num10 = num5;
								string text9 = row3["BatchNumber"].ToString();
								string.IsNullOrEmpty(text9);
								sqlTransaction = base.DBConfig.StartNewTransaction();
								new SqlCommand().Transaction = sqlTransaction;
								base.DBConfig.EndTransaction(result: true);
								string text10 = "";
								_ = hashtable.Count;
								_ = 1;
								if (hashtable.Count > 0)
								{
									text10 = ToDelimString(hashtable, "','");
								}
								DataSet dataSet2 = new DataSet();
								string textCommand = "Select LotNumber,SoldQty,ItemCode,ReceiptDate,ReceiptNumber,LocationID,Cost,AvgCost, (LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0) AS LotQty,BinID,Reference2 FROM product_lot\r\n\t\t\t\t\t\t\t\tWHERE ItemCode IN ('" + text5 + "') and  \r\n\t\t\t\t\t\t\t\t(LotQty - ISNULL(ReturnedQty,0)) - ISNULL(SoldQty,0)>0 and ISNULL(IsDeleted,'False') = 'False' and Reference='" + text9 + "' and LocationID='" + text4 + "' and LotNumber NOT IN('" + text10 + "')\r\n\t\t\t\t\t\t\t\tOrder BY ReceiptDate,ReceiptNumber,LotQty DESC ";
								FillDataSet(dataSet2, "Product_Lot", textCommand, sqlTransaction);
								base.DBConfig.EndTransaction(result: true);
								while (true)
								{
									object obj3 = null;
									float num11 = 0f;
									flag = false;
									if (dataSet2.Tables[0].Rows.Count > num9)
									{
										obj3 = dataSet2.Tables[0].Rows[num9]["LotNumber"];
										num11 = float.Parse(dataSet2.Tables[0].Rows[num9]["LotQty"].ToString());
									}
									if (obj3 == null)
									{
										syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text2, "", DateTime.Now, "Lot Number not availble with batch " + text9 + " for Item " + text5 + " Transaction No :-" + text2);
										num3++;
										break;
									}
									DataRow dataRow5 = inventoryTransferData.Tables["Product_Lot_Issue_Detail"].NewRow();
									DataRow dataRow6 = inventoryTransferData.Tables["Product_Lot_Receiving_Detail"].NewRow();
									dataRow5["ProductID"] = text5;
									dataRow5["LocationID"] = text4;
									dataRow5["LotNumber"] = obj3;
									dataRow5["Reference"] = text9;
									dataRow5["SourceLotNumber"] = DBNull.Value;
									dataRow5["BinID"] = DBNull.Value;
									dataRow5["Reference2"] = DBNull.Value;
									float num12 = 0f;
									if (num11 < num10)
									{
										num12 = num11;
										num10 -= num11;
										num9++;
										flag = true;
										hashtable.Add(obj3.ToString() + "FinishedLot", obj3.ToString());
									}
									else
									{
										num12 = num10;
									}
									dataRow5["SoldQty"] = num12;
									dataRow5["Cost"] = 0.0;
									dataRow5["LocationID"] = text4;
									dataRow5["SysDocID"] = value;
									dataRow5["VoucherID"] = text2;
									dataRow5["UnitPrice"] = 0.0;
									dataRow5["RowIndex"] = num6 - 1;
									inventoryTransferData.Tables["Product_Lot_Issue_Detail"].Rows.Add(dataRow5);
									dataRow6 = inventoryTransferData.Tables["Product_Lot_Receiving_Detail"].NewRow();
									dataRow6["ProductID"] = row3["ItemCode"].ToString();
									dataRow6["LocationID"] = text3;
									dataRow6["LotNumber"] = text9;
									dataRow6["SourceLotNumber"] = obj3;
									dataRow6["BinID"] = DBNull.Value;
									dataRow6["Reference2"] = DBNull.Value;
									dataRow6["LotQty"] = num12;
									dataRow6["SoldQty"] = 0.0;
									dataRow6["SysDocID"] = value;
									dataRow6["VoucherID"] = text2;
									dataRow6["RowIndex"] = num6 - 1;
									dataRow6["ExpiryDate"] = dateTime2;
									inventoryTransferData.Tables["Product_Lot_Receiving_Detail"].Rows.Add(dataRow6);
									if (flag)
									{
										continue;
									}
									goto IL_0aff;
								}
								goto IL_1203;
								IL_0aff:;
							}
						}
						foreach (DataRow row4 in inventoryTransferData.Tables["Product_Lot_Issue_Detail"].Rows)
						{
							row4["SysDocID"] = value;
							row4["VoucherID"] = text2;
							string text11 = row4["ProductID"].ToString();
							DataRow[] array = inventoryTransferData.InventoryTransferDetailsTable.Select("ProductID= '" + text11 + "' AND RowIndex = '" + row4["RowIndex"].ToString() + "'");
							string text12 = "";
							DataRow[] array2 = array;
							for (int i = 0; i < array2.Length; i++)
							{
								text12 = array2[i]["UnitID"].ToString();
							}
							string text13 = "";
							object fieldValue4 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text11, sqlTransaction);
							if (fieldValue4 != null)
							{
								text13 = fieldValue4.ToString();
							}
							if (text13 != "" && !string.IsNullOrEmpty(text12) && text12 != text13)
							{
								DataRow obj4 = new Products(base.DBConfig).GetProductUnitRow(text11, text12) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text11 + "\nUnit:" + row4["UnitID"].ToString());
								float num13 = float.Parse(obj4["Factor"].ToString());
								string a = obj4["FactorType"].ToString();
								float num14 = float.Parse(row4["SoldQty"].ToString());
								if (a == "M")
								{
									num14 = float.Parse(Math.Round(num14 / num13, 5).ToString());
								}
								else
								{
									num14 = float.Parse(Math.Round(num14 * num13, 5).ToString());
								}
							}
						}
						foreach (DataRow row5 in inventoryTransferData.Tables["Product_Lot_Receiving_Detail"].Rows)
						{
							row5["SysDocID"] = value;
							row5["VoucherID"] = text2;
							string text14 = row5["ProductID"].ToString();
							DataRow[] array3 = inventoryTransferData.InventoryTransferDetailsTable.Select("ProductID= '" + text14 + "' AND RowIndex = '" + row5["RowIndex"].ToString() + "'");
							string text15 = "";
							DataRow[] array2 = array3;
							for (int i = 0; i < array2.Length; i++)
							{
								text15 = array2[i]["UnitID"].ToString();
							}
							string text16 = "";
							object fieldValue5 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text14, sqlTransaction);
							if (fieldValue5 != null)
							{
								text16 = fieldValue5.ToString();
							}
							if (text16 != "" && !string.IsNullOrEmpty(text15) && text15 != text16)
							{
								DataRow obj5 = new Products(base.DBConfig).GetProductUnitRow(text14, text15) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text14 + "\nUnit:" + row5["UnitID"].ToString());
								float num15 = float.Parse(obj5["Factor"].ToString());
								string a2 = obj5["FactorType"].ToString();
								float num16 = float.Parse(row5["LotQty"].ToString());
								if (a2 == "M")
								{
									num16 = float.Parse(Math.Round(num16 / num15, 5).ToString());
								}
								else
								{
									num16 = float.Parse(Math.Round(num16 * num15, 5).ToString());
								}
							}
						}
						foreach (DataRow row6 in inventoryTransferData.InventoryTransferDetailsTable.Rows)
						{
							row6["SysDocID"] = value;
							row6["VoucherID"] = text2;
							string text17 = row6["ProductID"].ToString();
							string text18 = "";
							object fieldValue6 = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text17, sqlTransaction);
							if (fieldValue6 != null)
							{
								text18 = fieldValue6.ToString();
							}
							if (text18 != "" && row6["UnitID"] != DBNull.Value && row6["UnitID"].ToString() != text18)
							{
								DataRow obj6 = new Products(base.DBConfig).GetProductUnitRow(text17, row6["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text17 + "\nUnit:" + row6["UnitID"].ToString());
								float num17 = float.Parse(obj6["Factor"].ToString());
								string a3 = obj6["FactorType"].ToString();
								float num18 = float.Parse(row6["Quantity"].ToString());
								num18 = ((!(a3 == "M")) ? float.Parse(Math.Round(num18 * num17, 5).ToString()) : float.Parse(Math.Round(num18 / num17, 5).ToString()));
								row6["AcceptedQuantity"] = num18;
								row6["Quantity"] = num18;
								row6["UnitID"] = text18;
							}
						}
						try
						{
							if (new InventoryTransfer(base.DBConfig).InsertUpdateDirectInventoryTransfer(inventoryTransferData, isUpdate: false))
							{
								UpdateSyncStatus("", text2, transferType, code);
							}
							syncTables = insertSynRow(syncTables, SyncActivityTypes.None, text2, "", DateTime.Now, text2 + " --saved successfully.");
							num2++;
						}
						catch (Exception ex)
						{
							syncTables = insertSynRow(syncTables, SyncActivityTypes.Bug, text2, "", DateTime.Now, text2 + " --" + ex.Message);
							num3++;
						}
					}
					IL_1203:;
				}
				syncTables = insertSynRow(syncTables, SyncActivityTypes.Stop, "", "", DateTime.Now, "Stock Transfer- " + text + "  Process Stops");
				syncTables = insertSyncSumary(syncTables, num, num2, num3);
				SqlTransaction sqlTransaction2 = null;
				sqlTransaction2 = base.DBConfig.StartNewTransaction();
				MemoryStream memoryStream = SerializeToStream(syncTables);
				insertSyncActvity(memoryStream.ToArray(), SyncActivityTypes.Bug, sqlTransaction2);
				base.DBConfig.EndTransaction(result: true);
				return syncTables;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public DataSet PullStockTransferData(string transferType, string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			int num = 0;
			if (transferType == "19")
			{
				num = 1;
			}
			if (transferType == "20")
			{
				num = 2;
			}
			string cmdText = "select * from StockTransferHeader where ISNULL(IsProcessed,0)=0 and TransactionType=" + num + " order by TransactionDate asc";
			string cmdText2 = "select * from  StockTransferDetails std inner join StockTransferHeader sth on sth.TransactionNumber=std.TransactionNumber where  sth.TransactionType=" + num + "  order by sth.TransactionNumber asc";
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			SqlCommand selectCommand2 = new SqlCommand(cmdText2, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			new SqlDataAdapter(selectCommand).Fill(dataSet, "StockTransfer");
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand2);
			sqlDataAdapter.Fill(dataSet, "StockTransferDetail");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		public DataSet PullStgCustomers(string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "select * from Customers where ISNULL(IsProcessed,0)=0 ";
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
			sqlDataAdapter.Fill(dataSet, "customers");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		public DataSet PullStgItems(string code)
		{
			DataSet dataSyncConn = GetDataSyncConn(code);
			string text = dataSyncConn.Tables[0].Rows[0]["ServerID"].ToString();
			string text2 = dataSyncConn.Tables[0].Rows[0]["DatabaseName"].ToString();
			string text3 = dataSyncConn.Tables[0].Rows[0]["UserID"].ToString();
			string text4 = dataSyncConn.Tables[0].Rows[0]["Password"].ToString();
			string connectionString = "Data Source=" + text + ";Initial Catalog=" + text2 + ";User ID=" + text3 + ";Password=" + text4 + ";";
			string cmdText = "select * from Items where ISNULL(IsProcessed,0)=0 ";
			SqlConnection sqlConnection = new SqlConnection(connectionString);
			SqlCommand selectCommand = new SqlCommand(cmdText, sqlConnection);
			sqlConnection.Open();
			DataSet dataSet = new DataSet();
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommand);
			sqlDataAdapter.Fill(dataSet, "Items");
			sqlConnection.Close();
			sqlDataAdapter.Dispose();
			return dataSet;
		}

		private string GetNextCustomerID(string id)
		{
			string exp = "SELECT TOP 10 CustomerID AS Code FROM Customer where CustomerID like '" + id + "%' ORDER BY DateCreated DESC";
			object obj = ExecuteScalar(exp);
			if (!obj.IsDBNullOrEmpty())
			{
				id = obj.ToString();
			}
			do
			{
				string numberPrefix = CommonLib.GetNumberPrefix(id);
				if (numberPrefix.Length == id.Length)
				{
					id += "00001";
					continue;
				}
				long result = 0L;
				long.TryParse(id.Substring(numberPrefix.Length, id.Length - numberPrefix.Length), out result);
				long num = 0L;
				if (result >= 0)
				{
					int num2 = id.Length - numberPrefix.Length;
					string text = "";
					for (int i = 0; i < num2; i++)
					{
						text += "0";
					}
					num = ++result;
					id = numberPrefix + num.ToString(text);
				}
				else
				{
					id = numberPrefix + num.ToString("000000");
				}
			}
			while (new Databases(base.DBConfig).ExistFieldValue("Customer", "CustomerID", id));
			return id;
		}

		public DataSet DataSyncList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT Code,Name, Case When Type=1 Then 'INCUBE' Else '' End as [Type],DatabaseName,ServerID,UserID  \r\n                           FROM Data_Sync ";
			FillDataSet(dataSet, "Patient_Doc_Type", textCommand);
			return dataSet;
		}
	}
}
