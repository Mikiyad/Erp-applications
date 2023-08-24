using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PhysicalStockEntry : StoreObject
	{
		private const string DOCNUMBER_PARM = "@DocNumber";

		private const string TRANSACTIONDATE_PARM = "@PurchaseDate";

		private const string REFERENCE_PARM = "@Reference";

		private const string NOTE_PARM = "@Note";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string PHYSICALSTOCKENTRY_TABLE = "Physical_Stock_Entry";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public const string ITEMID_PARM = "@ItemID";

		public const string DESCRIPTION_PARM = "@ItemDescription";

		private const string ITEMUNIT_PARM = "@Unit";

		private const string QUANTITY_PARM = "@Quantity";

		private const string LOCATION_PARM = "@LocationID";

		private const string ROWINDEX_PARM = "@RowIndex";

		public const string PHYSICALSTOCKENTRYDETAIL_TABLE = "Physical_Entry_Stock_Detail";

		public PhysicalStockEntry(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePhysicalStockEntryText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Physical_Stock_Entry", new FieldValue("DocNumber", "@DocNumber", isUpdateConditionField: true), new FieldValue("PurchaseDate", "@PurchaseDate", isUpdateConditionField: true), new FieldValue("Reference", "@Reference"), new FieldValue("Note", "@Note"), new FieldValue("LocationID", "@LocationID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Physical_Stock_Entry", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePhysicalStockEntryCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePhysicalStockEntryText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePhysicalStockEntryText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@DocNumber", SqlDbType.NVarChar);
			parameters.Add("@PurchaseDate", SqlDbType.DateTime);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters["@DocNumber"].SourceColumn = "DocNumber";
			parameters["@PurchaseDate"].SourceColumn = "PurchaseDate";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LocationID"].SourceColumn = "LocationID";
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

		private string GetInsertUpdatePhysicalStockEntryDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Physical_Stock_Entry_Detail", new FieldValue("DocNumber", "@DocNumber"), new FieldValue("ItemID", "@ItemID"), new FieldValue("ItemDescription", "@ItemDescription"), new FieldValue("Unit", "@Unit"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Reference", "@Reference"), new FieldValue("LocationID", "@LocationID"), new FieldValue("Qty", "@Quantity"), new FieldValue("PurchaseDate", "@PurchaseDate"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePhysicalStockEntryDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePhysicalStockEntryDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePhysicalStockEntryDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@DocNumber", SqlDbType.NVarChar);
			parameters.Add("@ItemID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@ItemDescription", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@Unit", SqlDbType.NVarChar);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@PurchaseDate", SqlDbType.DateTime);
			parameters["@DocNumber"].SourceColumn = "DocNumber";
			parameters["@ItemID"].SourceColumn = "ItemID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Unit"].SourceColumn = "Unit";
			parameters["@ItemDescription"].SourceColumn = "ItemDescription";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@Quantity"].SourceColumn = "Qty";
			parameters["@PurchaseDate"].SourceColumn = "PurchaseDate";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(PhysicalStockEntryData journalData)
		{
			return true;
		}

		public string GetNextDocNumber()
		{
			DataSet dataSet = new DataSet();
			string text = "";
			int num = 1;
			string text2 = "";
			string textCommand = "SELECT MAX(DocNumber) AS LastNumber FROM Physical_Stock_Entry";
			FillDataSet(dataSet, "System_Document", textCommand);
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
			{
				text2 = dataSet.Tables[0].Rows[0]["LastNumber"].ToString();
			}
			for (int i = 0; i < text2.Length && !char.IsNumber(text2[i]); i++)
			{
				text += text2[i].ToString();
			}
			if (text2 != "")
			{
				num = int.Parse(text2.Substring(text.Length)) + 1;
				int num2 = text2.Length - text.Length;
				string text3 = "";
				for (int j = 0; j < num2; j++)
				{
					text3 += "0";
				}
				return text + num.ToString(text3);
			}
			return text + num.ToString("00000000");
		}

		public bool InsertUpdatePhysicalStockEntry(PhysicalStockEntryData physicalStockEntryData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdatePhysicalStockEntryCommand = GetInsertUpdatePhysicalStockEntryCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdatePhysicalStockEntryCommand.Transaction = (insertUpdatePhysicalStockEntryCommand.Transaction = base.DBConfig.StartNewTransaction()));
				string text = physicalStockEntryData.PhysicalEStockEntryTable.Rows[0]["DocNumber"].ToString();
				flag = (isUpdate ? (flag & Update(physicalStockEntryData, "Physical_Stock_Entry", insertUpdatePhysicalStockEntryCommand)) : (flag & Insert(physicalStockEntryData, "Physical_Stock_Entry", insertUpdatePhysicalStockEntryCommand)));
				insertUpdatePhysicalStockEntryCommand = GetInsertUpdatePhysicalStockEntryDetailsCommand(isUpdate: false);
				insertUpdatePhysicalStockEntryCommand.Transaction = sqlTransaction;
				if (isUpdate)
				{
					flag &= DeletePhysicalStockEntryDetails(text, sqlTransaction);
				}
				if (physicalStockEntryData.Tables["Physical_Stock_Entry_Detail"].Rows.Count > 0)
				{
					flag &= Insert(physicalStockEntryData, "Physical_Stock_Entry_Detail", insertUpdatePhysicalStockEntryCommand);
				}
				AddActivityLog("Physical Stock Entry", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Physical_Stock_Entry", "DocNumber", text, sqlTransaction, isInsert: true);
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

		internal bool DeletePhysicalStockEntryDetails(string DocNumber, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Physical_Stock_Entry_Detail WHERE DocNumber = '" + DocNumber + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		private bool VoidPhysicalStockEntry(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			return false;
		}

		public bool DeletePhysicalStockEntry(string DocNumber)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Physical_Stock_Entry WHERE DocNumber = '" + DocNumber + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("Physical Stock Entry", DocNumber, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public PhysicalStockEntryData GetPhysicalStockEntryByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "DocNumber";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Physical_Stock_Entry";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			PhysicalStockEntryData physicalStockEntryData = new PhysicalStockEntryData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(physicalStockEntryData, "Physical_Stock_Entry", sqlBuilder);
			return physicalStockEntryData;
		}
	}
}
