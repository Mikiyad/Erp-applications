using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class ArrivalReportTemplate : StoreObject
	{
		private const string TEMPLATEID_PARM = "@TemplateID";

		private const string TEMPLATENAME_PARM = "@ArrivalReportTemplateName";

		private const string ISSUE1NAME_PARM = "@Issue1Name";

		private const string ISSUE2NAME_PARM = "@Issue2Name";

		private const string ISSUE3NAME_PARM = "@Issue3Name";

		private const string ISSUE4NAME_PARM = "@Issue4Name";

		private const string ISSUE1LOSSPERCENT_PARM = "@Issue1LossPercent";

		private const string ISSUE2LOSSPERCENT_PARM = "@Issue2LossPercent";

		private const string ISSUE3LOSSPERCENT_PARM = "@Issue3LossPercent";

		private const string ISSUE4LOSSPERCENT_PARM = "@Issue4LossPercent";

		private const string ATRNUM1NAME_PARM = "@AtrNum1Name";

		private const string ATRNUM2NAME_PARM = "@AtrNum2Name";

		private const string ATRNUM3NAME_PARM = "@AtrNum3Name";

		private const string ATRNUM4NAME_PARM = "@AtrNum4Name";

		private const string ATRTEXT1NAME_PARM = "@AtrText1Name";

		private const string ATRTEXT2NAME_PARM = "@AtrText2Name";

		private const string ATRTEXT3NAME_PARM = "@AtrText3Name";

		private const string ATRTEXT4NAME_PARM = "@AtrText4Name";

		private const string PRINTTEMPLATENAME_PARM = "@PrintTemplateName";

		private const string INACTIVE_PARM = "@IsInactive";

		private const string ISBRIX_PARM = "@IsBrix";

		private const string ISPRESSURE_PARM = "@IsPressure";

		private const string ISGROWER_PARM = "@IsGrower";

		private const string ISDATECODE_PARM = "@IsDateCode";

		private const string ISPALLETID_PARM = "@IsPalletID";

		private const string ISTEMPERATURE_PARM = "@IsTemperature";

		private const string ARRIVALREPORTTEMPLATE_TABLE = "ArrivalReportTemplate";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public ArrivalReportTemplate(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Arrival_Report_Template", new FieldValue("TemplateID", "@TemplateID", isUpdateConditionField: true), new FieldValue("TemplateName", "@ArrivalReportTemplateName"), new FieldValue("Issue1Name", "@Issue1Name"), new FieldValue("Issue2Name", "@Issue2Name"), new FieldValue("Issue3Name", "@Issue3Name"), new FieldValue("Issue4Name", "@Issue4Name"), new FieldValue("Issue1LossPercent", "@Issue1LossPercent"), new FieldValue("Issue2LossPercent", "@Issue2LossPercent"), new FieldValue("Issue3LossPercent", "@Issue3LossPercent"), new FieldValue("Issue4LossPercent", "@Issue4LossPercent"), new FieldValue("AtrNum1Name", "@AtrNum1Name"), new FieldValue("AtrNum2Name", "@AtrNum2Name"), new FieldValue("AtrNum3Name", "@AtrNum3Name"), new FieldValue("AtrNum4Name", "@AtrNum4Name"), new FieldValue("AtrText1Name", "@AtrText1Name"), new FieldValue("AtrText2Name", "@AtrText2Name"), new FieldValue("AtrText3Name", "@AtrText3Name"), new FieldValue("AtrText4Name", "@AtrText4Name"), new FieldValue("PrintTemplateName", "@PrintTemplateName"), new FieldValue("IsBrix", "@IsBrix"), new FieldValue("IsPressure", "@IsPressure"), new FieldValue("IsGrower", "@IsGrower"), new FieldValue("IsDateCode", "@IsDateCode"), new FieldValue("IsPalletID", "@IsPalletID"), new FieldValue("IsTemperature", "@IsTemperature"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Arrival_Report_Template", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
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
			parameters.Add("@TemplateID", SqlDbType.NVarChar);
			parameters.Add("@ArrivalReportTemplateName", SqlDbType.NVarChar);
			parameters.Add("@Issue1Name", SqlDbType.NVarChar);
			parameters.Add("@Issue2Name", SqlDbType.NVarChar);
			parameters.Add("@Issue3Name", SqlDbType.NVarChar);
			parameters.Add("@Issue4Name", SqlDbType.NVarChar);
			parameters.Add("@Issue1LossPercent", SqlDbType.Decimal);
			parameters.Add("@Issue2LossPercent", SqlDbType.Decimal);
			parameters.Add("@Issue3LossPercent", SqlDbType.Decimal);
			parameters.Add("@Issue4LossPercent", SqlDbType.Decimal);
			parameters.Add("@AtrNum1Name", SqlDbType.NVarChar);
			parameters.Add("@AtrNum2Name", SqlDbType.NVarChar);
			parameters.Add("@AtrNum3Name", SqlDbType.NVarChar);
			parameters.Add("@AtrNum4Name", SqlDbType.NVarChar);
			parameters.Add("@AtrText1Name", SqlDbType.NVarChar);
			parameters.Add("@AtrText2Name", SqlDbType.NVarChar);
			parameters.Add("@AtrText3Name", SqlDbType.NVarChar);
			parameters.Add("@AtrText4Name", SqlDbType.NVarChar);
			parameters.Add("@IsBrix", SqlDbType.Bit);
			parameters.Add("@IsPressure", SqlDbType.Bit);
			parameters.Add("@IsGrower", SqlDbType.Bit);
			parameters.Add("@IsDateCode", SqlDbType.Bit);
			parameters.Add("@IsPalletID", SqlDbType.Bit);
			parameters.Add("@IsTemperature", SqlDbType.Bit);
			parameters.Add("@PrintTemplateName", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters["@TemplateID"].SourceColumn = "TemplateID";
			parameters["@ArrivalReportTemplateName"].SourceColumn = "TemplateName";
			parameters["@Issue1Name"].SourceColumn = "Issue1Name";
			parameters["@Issue2Name"].SourceColumn = "Issue2Name";
			parameters["@Issue3Name"].SourceColumn = "Issue3Name";
			parameters["@Issue4Name"].SourceColumn = "Issue4Name";
			parameters["@Issue1LossPercent"].SourceColumn = "Issue1LossPercent";
			parameters["@Issue2LossPercent"].SourceColumn = "Issue2LossPercent";
			parameters["@Issue3LossPercent"].SourceColumn = "Issue3LossPercent";
			parameters["@Issue4LossPercent"].SourceColumn = "Issue4LossPercent";
			parameters["@AtrNum1Name"].SourceColumn = "AtrNum1Name";
			parameters["@AtrNum2Name"].SourceColumn = "AtrNum2Name";
			parameters["@AtrNum3Name"].SourceColumn = "AtrNum3Name";
			parameters["@AtrNum4Name"].SourceColumn = "AtrNum4Name";
			parameters["@AtrText1Name"].SourceColumn = "AtrText1Name";
			parameters["@AtrText2Name"].SourceColumn = "AtrText2Name";
			parameters["@AtrText3Name"].SourceColumn = "AtrText3Name";
			parameters["@AtrText4Name"].SourceColumn = "AtrText4Name";
			parameters["@IsBrix"].SourceColumn = "IsBrix";
			parameters["@IsPressure"].SourceColumn = "IsPressure";
			parameters["@IsGrower"].SourceColumn = "IsGrower";
			parameters["@IsDateCode"].SourceColumn = "IsDateCode";
			parameters["@IsPalletID"].SourceColumn = "IsPalletID";
			parameters["@IsTemperature"].SourceColumn = "IsTemperature";
			parameters["@PrintTemplateName"].SourceColumn = "PrintTemplateName";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
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

		public bool InsertArrivalReportTemplate(ArrivalReportTemplateData accountArrivalReportTemplateData)
		{
			bool result = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				result = Insert(accountArrivalReportTemplateData, "Arrival_Report_Template", insertUpdateCommand);
				string text = accountArrivalReportTemplateData.ArrivalReportTemplateTable.Rows[0]["TemplateID"].ToString();
				AddActivityLog("ArrivalReportTemplate", text, ActivityTypes.Add, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Arrival_Report_Template", "TemplateID", text, sqlTransaction, isInsert: true);
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

		public bool UpdateArrivalReportTemplate(ArrivalReportTemplateData accountArrivalReportTemplateData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(accountArrivalReportTemplateData, "Arrival_Report_Template", insertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = accountArrivalReportTemplateData.ArrivalReportTemplateTable.Rows[0]["TemplateID"];
				UpdateTableRowByID("Arrival_Report_Template", "TemplateID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				string entiyID = accountArrivalReportTemplateData.ArrivalReportTemplateTable.Rows[0]["TemplateName"].ToString();
				AddActivityLog("ArrivalReportTemplate", entiyID, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Arrival_Report_Template", "TemplateID", obj, sqlTransaction, isInsert: false);
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

		public ArrivalReportTemplateData GetArrivalReportTemplate()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Arrival_Report_Template");
			ArrivalReportTemplateData arrivalReportTemplateData = new ArrivalReportTemplateData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(arrivalReportTemplateData, "Arrival_Report_Template", sqlBuilder);
			return arrivalReportTemplateData;
		}

		public bool DeleteArrivalReportTemplate(string arrivalReportTemplateID)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Arrival_Report_Template WHERE TemplateID = '" + arrivalReportTemplateID + "'";
				flag = Delete(commandText, null);
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("ArrivalReportTemplate", arrivalReportTemplateID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public ArrivalReportTemplateData GetArrivalReportTemplateByID(string id)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.FieldName = "TemplateID";
			commandHelper.SqlFieldType = SqlDbType.NVarChar;
			commandHelper.FieldValue = id;
			commandHelper.TableName = "Arrival_Report_Template";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = true;
			ArrivalReportTemplateData arrivalReportTemplateData = new ArrivalReportTemplateData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(arrivalReportTemplateData, "Arrival_Report_Template", sqlBuilder);
			return arrivalReportTemplateData;
		}

		public DataSet GetArrivalReportTemplateByFields(params string[] columns)
		{
			return GetArrivalReportTemplateByFields(null, isInactive: true, columns);
		}

		public DataSet GetArrivalReportTemplateByFields(string[] arrivalReportTemplateID, params string[] columns)
		{
			return GetArrivalReportTemplateByFields(arrivalReportTemplateID, isInactive: true, columns);
		}

		public DataSet GetArrivalReportTemplateByFields(string[] ids, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Arrival_Report_Template");
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
				commandHelper.FieldName = "TemplateID";
				commandHelper.FieldValue = ids;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Arrival_Report_Template";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Arrival_Report_Template", sqlBuilder);
			return dataSet;
		}

		public DataSet GetArrivalReportTemplateList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TemplateID [Template Code],TemplateName [Template Name],IsInactive\r\n                           FROM Arrival_Report_Template ";
			FillDataSet(dataSet, "Arrival_Report_Template", textCommand);
			return dataSet;
		}

		public DataSet GetArrivalReportTemplateComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT TemplateID [Code], TemplateName [Name]\r\n                           FROM Arrival_Report_Template ORDER BY TemplateID,TemplateName";
			FillDataSet(dataSet, "Arrival_Report_Template", textCommand);
			return dataSet;
		}
	}
}
