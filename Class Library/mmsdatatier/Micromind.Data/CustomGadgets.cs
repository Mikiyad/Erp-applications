using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Data.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Micromind.Data
{
	public sealed class CustomGadgets : StoreObject
	{
		private const string CUSTOMGADGETID_PARM = "@CustomGadgetID";

		private const string CUSTOMGADGETNAME_PARM = "@CustomGadgetName";

		private const string ISINACTIVE_PARM = "@IsInactive";

		private const string GADGETDATA_PARM = "@GadgetData";

		private const string DATETIMESTAMP_PARM = "@DateTimeStamp";

		private const string GADGETSTYLE_PARM = "@GadgetStyle";

		private const string CHARTVALUECOLUMN_PARM = "@ChartVAlueColumn";

		private const string CHARTARGCOLUMN_PARM = "@ChartArgColumn";

		private const string CHARTTYPE_PARM = "@ChartType";

		private const string CHARTPALLET_PARM = "@ChartPallet";

		private const string CATEGORYID_PARM = "@CategoryID";

		private const string FILTEROPTION_PARM = "@FilterOption";

		private const string COLOREACH_PARM = "@ColorEach";

		private const string COLORPALETTENAME_PARM = "@ColorPaletteName";

		private const string AUTOREFRESH_PARM = "@AutoRefresh";

		private const string REFRESHINTERVAL_PARM = "@RefreshInterval";

		private const string DRILLACTION_PARM = "@DrillAction";

		private const string DRILLCARDTYPEID_PARM = "@DrillCardTypeID";

		private const string DRILLCARDIDFIELD_PARM = "@DrillCardIDField";

		private const string DRILLTRANSACTIONSYSDOCIDFIELD_PARM = "@DrillTransactionSysDocIDField";

		private const string DRILLTRANSACTIONVOUCHERIDFIELD_PARM = "@DrillTransactionVoucherIDField";

		private const string DRILLPARM1_PARM = "@DrillParm1";

		private const string DRILLPARM2_PARM = "@DrillParm2";

		private const string DRILLPARM3_PARM = "@DrillParm3";

		private const string DRILLPARM4_PARM = "@DrillParm4";

		private const string DRILLSUBREPORTID_PARM = "@DrillSubReportID";

		private const string ISSUBREPORT_PARM = "@IsSubReport";

		private const string ISPREVIEW_PARM = "@IsPreview";

		private const string GSHOWGAUGETEXT_PARM = "@GShowGaugeText";

		private const string GSHOWNEEDLE_PARM = "@GShowNeedle";

		private const string SHOWLEGEND_PARM = "@ShowLegend";

		private const string AXISXTITLE_PARM = "@AxisXTitle";

		private const string AXISYTITLE_PARM = "@AxisYTitle";

		private const string AXISYTEXTPATTERN_PARM = "@AxisYTextPattern";

		private const string ISROTATED_PARM = "@IsRotated";

		private const string AXISYVISIBLE_PARM = "@AxisYVisible";

		private const string LEGENDTEXTPATTERN_PARM = "@LegendTextPattern";

		private const string TOPNOPTION_PARM = "@TopNOption";

		private const string TOPNCOUNT_PARM = "@TopNCount";

		private const string TOPNOTHERSTEXT_PARM = "@TopNOthersText";

		private const string SHOWTOPNOTHER_PARM = "@ShowTopNOther";

		private const string SERIESID_PARM = "@SeriesID";

		private const string DISPLAYNAME_PARM = "@DisplayName";

		private const string LABELVISIBLE_PARM = "@LabelVisible";

		private const string LABELPOSITION_PARM = "@LabelPosition";

		private const string COLOR_PARM = "@Color";

		private const string LABELTEXTPATTERN_PARM = "@LabelTextPattern";

		private const string DESCRIPTION_PARM = "@Description";

		private const string INDVALUECOLUMN_PARM = "@IndValueColumn";

		private const string INDTEXTVALUECOLUMN_PARM = "@IndTextValueColumn";

		private const string SHOWNAME_PARM = "@ShowName";

		private const string SHOWINDICATOR_PARM = "@ShowIndicator";

		private const string TEXTCOLOR_PARM = "@TextColor";

		public const string CUSTOMREPORTID_PARM = "@CustomReportID";

		public const string CUSTOMREPORTTYPE_PARM = "@CustomReportType";

		public const string RANGEID_PARM = "@RangeID";

		public const string STARTVALUE_PARM = "@StartValue";

		public const string ENDVALUE_PARM = "@EndValue";

		public const string RANGECOLOR_PARM = "@RangeColor";

		private const string FIELDID_PARM = "@FieldID";

		private const string LISTHIDDENFIELDS_TABLE = "ListHiddenFields";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		public bool CheckConcurrency = true;

		public CustomGadgets(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Custom_Gadget", new FieldValue("CustomGadgetID", "@CustomGadgetID", isUpdateConditionField: true), new FieldValue("CustomGadgetName", "@CustomGadgetName"), new FieldValue("GadgetData", "@GadgetData"), new FieldValue("GadgetStyle", "@GadgetStyle"), new FieldValue("FilterOption", "@FilterOption"), new FieldValue("CategoryID", "@CategoryID"), new FieldValue("ChartVAlueColumn", "@ChartVAlueColumn"), new FieldValue("ChartType", "@ChartType"), new FieldValue("ChartPallet", "@ChartPallet"), new FieldValue("ColorEach", "@ColorEach"), new FieldValue("ColorPaletteName", "@ColorPaletteName"), new FieldValue("ChartArgColumn", "@ChartArgColumn"), new FieldValue("AutoRefresh", "@AutoRefresh"), new FieldValue("RefreshInterval", "@RefreshInterval"), new FieldValue("DrillParm1", "@DrillParm1"), new FieldValue("DrillParm2", "@DrillParm2"), new FieldValue("DrillParm3", "@DrillParm3"), new FieldValue("DrillParm4", "@DrillParm4"), new FieldValue("DrillSubReportID", "@DrillSubReportID"), new FieldValue("IsSubReport", "@IsSubReport"), new FieldValue("IsPreview", "@IsPreview"), new FieldValue("ShowLegend", "@ShowLegend"), new FieldValue("AxisXTitle", "@AxisXTitle"), new FieldValue("AxisYTitle", "@AxisYTitle"), new FieldValue("AxisYTextPattern", "@AxisYTextPattern"), new FieldValue("IsRotated", "@IsRotated"), new FieldValue("AxisYVisible", "@AxisYVisible"), new FieldValue("LegendTextPattern", "@LegendTextPattern"), new FieldValue("TopNOption", "@TopNOption"), new FieldValue("TopNCount", "@TopNCount"), new FieldValue("TopNOthersText", "@TopNOthersText"), new FieldValue("ShowTopNOther", "@ShowTopNOther"), new FieldValue("GShowGaugeText", "@GShowGaugeText"), new FieldValue("GShowNeedle", "@GShowNeedle"), new FieldValue("IndValueColumn", "@IndValueColumn"), new FieldValue("IndTextValueColumn", "@IndTextValueColumn"), new FieldValue("ShowName", "@ShowName"), new FieldValue("ShowIndicator", "@ShowIndicator"), new FieldValue("TextColor", "@TextColor"), new FieldValue("DisplayName", "@DisplayName"), new FieldValue("Description", "@Description"), new FieldValue("DrillAction", "@DrillAction"), new FieldValue("DrillCardTypeID", "@DrillCardTypeID"), new FieldValue("DrillCardIDField", "@DrillCardIDField"), new FieldValue("DrillTransactionSysDocIDField", "@DrillTransactionSysDocIDField"), new FieldValue("DrillTransactionVoucherIDField", "@DrillTransactionVoucherIDField"), new FieldValue("IsInactive", "@IsInactive"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Custom_Gadget", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@CustomGadgetID", SqlDbType.NVarChar);
			parameters.Add("@CustomGadgetName", SqlDbType.NVarChar);
			parameters.Add("@CategoryID", SqlDbType.NVarChar);
			parameters.Add("@GadgetData", SqlDbType.Image);
			parameters.Add("@FilterOption", SqlDbType.TinyInt);
			parameters.Add("@GadgetStyle", SqlDbType.TinyInt);
			parameters.Add("@ChartType", SqlDbType.TinyInt);
			parameters.Add("@ChartPallet", SqlDbType.TinyInt);
			parameters.Add("@ColorEach", SqlDbType.Bit);
			parameters.Add("@ColorPaletteName", SqlDbType.NVarChar);
			parameters.Add("@ChartVAlueColumn", SqlDbType.NVarChar);
			parameters.Add("@ChartArgColumn", SqlDbType.NVarChar);
			parameters.Add("@IsInactive", SqlDbType.Bit);
			parameters.Add("@AutoRefresh", SqlDbType.Bit);
			parameters.Add("@RefreshInterval", SqlDbType.SmallInt);
			parameters.Add("@DrillAction", SqlDbType.TinyInt);
			parameters.Add("@DrillCardTypeID", SqlDbType.Int);
			parameters.Add("@DrillCardIDField", SqlDbType.NVarChar);
			parameters.Add("@DrillTransactionSysDocIDField", SqlDbType.NVarChar);
			parameters.Add("@DrillTransactionVoucherIDField", SqlDbType.NVarChar);
			parameters.Add("@IsPreview", SqlDbType.Bit);
			parameters.Add("@DrillParm1", SqlDbType.NVarChar);
			parameters.Add("@DrillParm2", SqlDbType.NVarChar);
			parameters.Add("@DrillParm3", SqlDbType.NVarChar);
			parameters.Add("@DrillParm4", SqlDbType.NVarChar);
			parameters.Add("@IsSubReport", SqlDbType.Bit);
			parameters.Add("@DrillSubReportID", SqlDbType.Int);
			parameters.Add("@LegendTextPattern", SqlDbType.NVarChar);
			parameters.Add("@TopNOption", SqlDbType.Bit);
			parameters.Add("@ShowTopNOther", SqlDbType.Bit);
			parameters.Add("@TopNCount", SqlDbType.Int);
			parameters.Add("@TopNOthersText", SqlDbType.NVarChar);
			parameters.Add("@GShowGaugeText", SqlDbType.Bit);
			parameters.Add("@GShowNeedle", SqlDbType.Bit);
			parameters.Add("@IndValueColumn", SqlDbType.NVarChar);
			parameters.Add("@IndTextValueColumn", SqlDbType.NVarChar);
			parameters.Add("@ShowName", SqlDbType.Bit);
			parameters.Add("@ShowIndicator", SqlDbType.Bit);
			parameters.Add("@TextColor", SqlDbType.Int);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@ShowLegend", SqlDbType.Bit);
			parameters.Add("@AxisXTitle", SqlDbType.NVarChar);
			parameters.Add("@AxisYTitle", SqlDbType.NVarChar);
			parameters.Add("@AxisYTextPattern", SqlDbType.NVarChar);
			parameters.Add("@IsRotated", SqlDbType.Bit);
			parameters.Add("@AxisYVisible", SqlDbType.Bit);
			parameters["@CustomGadgetID"].SourceColumn = "CustomGadgetID";
			parameters["@CustomGadgetName"].SourceColumn = "CustomGadgetName";
			parameters["@CategoryID"].SourceColumn = "CategoryID";
			parameters["@FilterOption"].SourceColumn = "FilterOption";
			parameters["@GadgetData"].SourceColumn = "GadgetData";
			parameters["@GadgetStyle"].SourceColumn = "GadgetStyle";
			parameters["@ChartType"].SourceColumn = "ChartType";
			parameters["@ChartPallet"].SourceColumn = "ChartPallet";
			parameters["@ColorEach"].SourceColumn = "ColorEach";
			parameters["@ColorPaletteName"].SourceColumn = "ColorPaletteName";
			parameters["@ChartVAlueColumn"].SourceColumn = "ChartVAlueColumn";
			parameters["@ChartArgColumn"].SourceColumn = "ChartArgColumn";
			parameters["@IsInactive"].SourceColumn = "IsInactive";
			parameters["@GShowGaugeText"].SourceColumn = "GShowGaugeText";
			parameters["@GShowNeedle"].SourceColumn = "GShowNeedle";
			parameters["@AutoRefresh"].SourceColumn = "AutoRefresh";
			parameters["@RefreshInterval"].SourceColumn = "RefreshInterval";
			parameters["@DrillParm1"].SourceColumn = "DrillParm1";
			parameters["@DrillParm2"].SourceColumn = "DrillParm2";
			parameters["@DrillParm3"].SourceColumn = "DrillParm3";
			parameters["@DrillParm4"].SourceColumn = "DrillParm4";
			parameters["@IsSubReport"].SourceColumn = "IsSubReport";
			parameters["@DrillSubReportID"].SourceColumn = "DrillSubReportID";
			parameters["@IsPreview"].SourceColumn = "IsPreview";
			parameters["@DrillAction"].SourceColumn = "DrillAction";
			parameters["@DrillCardTypeID"].SourceColumn = "DrillCardTypeID";
			parameters["@DrillCardIDField"].SourceColumn = "DrillCardIDField";
			parameters["@DrillTransactionSysDocIDField"].SourceColumn = "DrillTransactionSysDocIDField";
			parameters["@DrillTransactionVoucherIDField"].SourceColumn = "DrillTransactionVoucherIDField";
			parameters["@LegendTextPattern"].SourceColumn = "LegendTextPattern";
			parameters["@TopNOption"].SourceColumn = "TopNOption";
			parameters["@TopNCount"].SourceColumn = "TopNCount";
			parameters["@TopNOthersText"].SourceColumn = "TopNOthersText";
			parameters["@ShowTopNOther"].SourceColumn = "ShowTopNOther";
			parameters["@IndValueColumn"].SourceColumn = "IndValueColumn";
			parameters["@IndTextValueColumn"].SourceColumn = "IndTextValueColumn";
			parameters["@ShowName"].SourceColumn = "ShowName";
			parameters["@ShowIndicator"].SourceColumn = "ShowIndicator";
			parameters["@TextColor"].SourceColumn = "TextColor";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@ShowLegend"].SourceColumn = "ShowLegend";
			parameters["@AxisXTitle"].SourceColumn = "AxisXTitle";
			parameters["@AxisYTitle"].SourceColumn = "AxisYTitle";
			parameters["@AxisYTextPattern"].SourceColumn = "AxisYTextPattern";
			parameters["@IsRotated"].SourceColumn = "IsRotated";
			parameters["@AxisYVisible"].SourceColumn = "AxisYVisible";
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

		private string GetInsertUpdateChartSeriesText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Chart_Series", new FieldValue("CustomGadgetID", "@CustomGadgetID", isUpdateConditionField: true), new FieldValue("SeriesID", "@SeriesID"), new FieldValue("DisplayName", "@DisplayName"), new FieldValue("ChartVAlueColumn", "@ChartVAlueColumn"), new FieldValue("LabelVisible", "@LabelVisible"), new FieldValue("LabelPosition", "@LabelPosition"), new FieldValue("Color", "@Color"), new FieldValue("LabelTextPattern", "@LabelTextPattern"), new FieldValue("ChartType", "@ChartType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateChartSeriesCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateChartSeriesText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@CustomGadgetID", SqlDbType.NVarChar);
			parameters.Add("@SeriesID", SqlDbType.NVarChar);
			parameters.Add("@DisplayName", SqlDbType.NVarChar);
			parameters.Add("@ChartType", SqlDbType.TinyInt);
			parameters.Add("@ChartVAlueColumn", SqlDbType.NVarChar);
			parameters.Add("@LabelVisible", SqlDbType.Bit);
			parameters.Add("@LabelPosition", SqlDbType.NVarChar);
			parameters.Add("@Color", SqlDbType.Int);
			parameters.Add("@LabelTextPattern", SqlDbType.NVarChar);
			parameters["@CustomGadgetID"].SourceColumn = "CustomGadgetID";
			parameters["@SeriesID"].SourceColumn = "SeriesID";
			parameters["@DisplayName"].SourceColumn = "DisplayName";
			parameters["@ChartType"].SourceColumn = "ChartType";
			parameters["@ChartVAlueColumn"].SourceColumn = "ChartVAlueColumn";
			parameters["@LabelVisible"].SourceColumn = "LabelVisible";
			parameters["@LabelPosition"].SourceColumn = "LabelPosition";
			parameters["@Color"].SourceColumn = "Color";
			parameters["@LabelTextPattern"].SourceColumn = "LabelTextPattern";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateGaugeRangeText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Gauge_Range", new FieldValue("CustomReportID", "@CustomReportID"), new FieldValue("CustomReportType", "@CustomReportType"), new FieldValue("RangeID", "@RangeID"), new FieldValue("StartValue", "@StartValue"), new FieldValue("EndValue", "@EndValue"), new FieldValue("RangeColor", "@RangeColor"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateGaugeRangeCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateGaugeRangeText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@CustomReportID", SqlDbType.NVarChar);
			parameters.Add("@CustomReportType", SqlDbType.TinyInt);
			parameters.Add("@RangeID", SqlDbType.NVarChar);
			parameters.Add("@StartValue", SqlDbType.Decimal);
			parameters.Add("@EndValue", SqlDbType.Decimal);
			parameters.Add("@RangeColor", SqlDbType.Int);
			parameters["@CustomReportID"].SourceColumn = "CustomReportID";
			parameters["@CustomReportType"].SourceColumn = "CustomReportType";
			parameters["@RangeID"].SourceColumn = "RangeID";
			parameters["@StartValue"].SourceColumn = "StartValue";
			parameters["@EndValue"].SourceColumn = "EndValue";
			parameters["@RangeColor"].SourceColumn = "RangeColor";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateListHiddenColumnsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("ListHiddenFields", new FieldValue("CustomReportID", "@CustomReportID"), new FieldValue("CustomReportType", "@CustomReportType"), new FieldValue("FieldID", "@FieldID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateListHiddenColumnsCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateListHiddenColumnsText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@CustomReportID", SqlDbType.NVarChar);
			parameters.Add("@CustomReportType", SqlDbType.TinyInt);
			parameters.Add("@FieldID", SqlDbType.NVarChar);
			parameters["@CustomReportID"].SourceColumn = "CustomReportID";
			parameters["@CustomReportType"].SourceColumn = "CustomReportType";
			parameters["@FieldID"].SourceColumn = "FieldID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool InsertUpdateCustomGadget(CustomGadgetData customGadgetData, bool isUpdate)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = ((!isUpdate) ? Insert(customGadgetData, "Custom_Gadget", insertUpdateCommand) : Update(customGadgetData, "Custom_Gadget", insertUpdateCommand));
				string text = customGadgetData.CustomGadgetTable.Rows[0]["CustomGadgetID"].ToString();
				flag &= DeleteChartSeriesRows(text, sqlTransaction);
				flag &= DeleteGaugeRangeRows(text, sqlTransaction);
				flag &= DeleteListHiddenColumnRows(text, sqlTransaction);
				insertUpdateCommand = GetInsertUpdateChartSeriesCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (customGadgetData.ChartSeriesTable.Rows.Count > 0)
				{
					flag &= Insert(customGadgetData, "Chart_Series", insertUpdateCommand);
				}
				insertUpdateCommand = GetInsertUpdateGaugeRangeCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (customGadgetData.GaugeRangeTable.Rows.Count > 0)
				{
					flag &= Insert(customGadgetData, "Gauge_Range", insertUpdateCommand);
				}
				insertUpdateCommand = GetInsertUpdateListHiddenColumnsCommand(isUpdate: false);
				insertUpdateCommand.Transaction = sqlTransaction;
				if (customGadgetData.ListHiddenFieldsTable.Rows.Count > 0)
				{
					flag &= Insert(customGadgetData, "ListHiddenFields", insertUpdateCommand);
				}
				if (!isUpdate)
				{
					AddActivityLog("CustomGadget", text, ActivityTypes.Add, sqlTransaction);
					UpdateTableRowInsertUpdateInfo("Custom_Gadget", "CustomGadgetID", text, sqlTransaction, isInsert: true);
					return flag;
				}
				AddActivityLog("CustomGadget", text, ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Custom_Gadget", "CustomGadgetID", text, sqlTransaction, isInsert: false);
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

		internal bool DeleteChartSeriesRows(string customGadgetID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Chart_Series WHERE CustomGadgetID = '" + customGadgetID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteGaugeRangeRows(string customGadgetID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Gauge_Range WHERE CustomReportID = '" + customGadgetID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteListHiddenColumnRows(string customGadgetID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM ListHiddenFields WHERE CustomReportID = '" + customGadgetID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCustomGadgetsByFields(params string[] columns)
		{
			return GetCustomGadgetsByFields(null, isInactive: true, columns);
		}

		public DataSet GetCustomGadgetsByFields(string[] customGadgetsID, params string[] columns)
		{
			return GetCustomGadgetsByFields(customGadgetsID, isInactive: true, columns);
		}

		public DataSet GetCustomGadgetsByFields(string[] customGadgetsID, bool isInactive, params string[] columns)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Custom_Gadget");
			foreach (string text in columns)
			{
				ApplicationAssert.CheckCondition(text.IndexOf(".") >= 0, "A table name with the column name must be specified. eg. table.column.", 0);
				string tableName = text.Substring(0, text.IndexOf("."));
				string columnName = text.Substring(text.IndexOf(".") + 1);
				sqlBuilder.AddColumn(tableName, columnName);
			}
			if (customGadgetsID != null && customGadgetsID.Length != 0)
			{
				CommandHelper commandHelper = new CommandHelper();
				commandHelper.FieldName = "CustomGadgetID";
				commandHelper.FieldValue = customGadgetsID;
				commandHelper.SqlFieldType = SqlDbType.NVarChar;
				commandHelper.TableName = "Custom_Gadget";
				sqlBuilder.AddCommandHelper(commandHelper);
			}
			if (!isInactive)
			{
				CommandHelper commandHelper2 = new CommandHelper();
				commandHelper2.FieldName = "IsInactive";
				commandHelper2.FieldValue = 0;
				commandHelper2.SqlFieldType = SqlDbType.Bit;
				commandHelper2.TableName = "Custom_Gadget";
				sqlBuilder.AddCommandHelper(commandHelper2);
			}
			DataSet dataSet = new DataSet();
			dataSet.EnforceConstraints = false;
			sqlBuilder.UseDistinct = false;
			FillDataSet(dataSet, "Custom_Gadget", sqlBuilder);
			return dataSet;
		}

		public CustomGadgetData GetCustomGadgets()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			CommandHelper commandHelper = null;
			commandHelper = new CommandHelper();
			commandHelper.TableName = "Custom_Gadget";
			sqlBuilder.AddCommandHelper(commandHelper);
			sqlBuilder.IsComparing = false;
			sqlBuilder.AddOrderByColumn("Custom_Gadget", "CustomGadgetName");
			CustomGadgetData customGadgetData = new CustomGadgetData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(customGadgetData, "Custom_Gadget", sqlBuilder);
			string textCommand = "SELECT * FROM Chart_Series";
			FillDataSet(customGadgetData, "Chart_Series", textCommand);
			if (customGadgetData.CustomGadgetTable.Rows.Count > 0 && customGadgetData.ChartSeriesTable.Rows.Count > 0)
			{
				customGadgetData.Relations.Add("Rel", customGadgetData.CustomGadgetTable.Columns["CustomGadgetID"], customGadgetData.ChartSeriesTable.Columns["CustomGadgetID"]);
			}
			return customGadgetData;
		}

		public CustomGadgetData GetCustomGadgetByID(string customGadgetID)
		{
			CustomGadgetData customGadgetData = new CustomGadgetData();
			string textCommand = "SELECT *, \r\n\t\t\t\t\t\t\t(SELECT Count(P2.CustomGadgetID)\r\n\t\t\t\t\t\t\tFROM Custom_Gadget   P2 WHERE P2.Photo IS Not NULL AND P2.CustomGadgetID=P1.CustomGadgetID)  AS HasPhoto  from Custom_Gadget P1 where CustomGadgetID='" + customGadgetID + "'";
			FillDataSet(customGadgetData, "Custom_Gadget", textCommand);
			textCommand = "SELECT * FROM Chart_Series WHERE CustomGadgetID = '" + customGadgetID + "'";
			FillDataSet(customGadgetData, "Chart_Series", textCommand);
			textCommand = "SELECT * FROM Gauge_Range WHERE CustomReportID = '" + customGadgetID + "'";
			FillDataSet(customGadgetData, "Gauge_Range", textCommand);
			textCommand = "SELECT * FROM ListHiddenFields WHERE CustomReportID = '" + customGadgetID + "'";
			FillDataSet(customGadgetData, "ListHiddenFields", textCommand);
			return customGadgetData;
		}

		public bool DeleteCustomGadget(string customGadgetID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				flag = DeleteChartSeriesRows(customGadgetID, sqlTransaction);
				if (flag)
				{
					flag &= DeleteGaugeRangeRows(customGadgetID, sqlTransaction);
				}
				if (flag)
				{
					flag &= DeleteListHiddenColumnRows(customGadgetID, sqlTransaction);
				}
				if (flag)
				{
					flag &= DeleteTableRowByID("Custom_Gadget", "CustomGadgetID", customGadgetID, sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				AddActivityLog("CustomGadget", customGadgetID, ActivityTypes.Delete, null);
				return flag;
			}
			catch
			{
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public string GetCustomGadgetIDByName(string name)
		{
			try
			{
				object obj = ExecuteSelectScalar("Custom_Gadget", "CustomGadgetName", name, "CustomGadgetID");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "-1";
		}

		public string GetCustomGadgetNameByID(object customGadgetID)
		{
			try
			{
				object obj = ExecuteSelectScalar("Custom_Gadget", "CustomGadgetID", customGadgetID, "CustomGadgetName");
				if (obj != null && obj != DBNull.Value)
				{
					return obj.ToString();
				}
			}
			catch
			{
				throw;
			}
			return "";
		}

		public bool ExistCustomGadget(string customGadgetName)
		{
			try
			{
				return IsTableFieldValueExist("Custom_Gadget", "CustomGadgetName", customGadgetName);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetCustomGadgetComboList()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT CustomGadgetID [Code],CustomGadgetName [Name],CategoryID\r\n\t\t\t\t\t\t   FROM Custom_Gadget ORDER BY CustomGadgetID,CustomGadgetName";
			FillDataSet(dataSet, "Buyer", textCommand);
			return dataSet;
		}

		public DataSet GetCustomGadgetList()
		{
			string textCommand = "SELECT CustomGadgetID [CustomGadget Code],CustomGadgetName [CustomGadget Name],ContactName AS [Contact Name],Phone,Fax,IsInactive AS [Inactive]\r\n\t\t\t\t\t\t   FROM Custom_Gadget ORDER BY CustomGadgetID,CustomGadgetName";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Account_Group", textCommand);
			return dataSet;
		}

		public DataSet GetCustomGadgetData(string reportID, string[] parameters, string[] parameterValues)
		{
			try
			{
				DataHelper dataHelper = new DataHelper(base.DBConfig);
				CustomGadget customGadget = (CustomGadget)CommonLib.DeserializeFromStream((byte[])GetCustomGadgetByID(reportID).Tables[0].Rows[0]["GadgetData"]);
				DataSet dataSet = new DataSet();
				foreach (CustomGadgetTable table in customGadget.Tables)
				{
					string text = table.query;
					for (int i = 0; i < parameters.Length; i++)
					{
						text = text.Replace(parameters[i], parameterValues[i]);
					}
					text = dataHelper.ReplaceSystemParameters(text);
					if (!SQLHelper.ValidateQuerySecurity(text))
					{
						throw new CompanyException("report query does not allow one or more keywords used in this query.");
					}
					FillDataSet(dataSet, table.tableName, text);
				}
				foreach (GadgetRelation relation in customGadget.Relations)
				{
					DataColumn[] array = new DataColumn[relation.ParentColumns.Length];
					DataColumn[] array2 = new DataColumn[relation.ChildColumns.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = dataSet.Tables[relation.ParentTableName].Columns[relation.ParentColumns[j]];
						array2[j] = dataSet.Tables[relation.ChildTableName].Columns[relation.ChildColumns[j]];
					}
					dataSet.Relations.Add(relation.RelationName, array, array2, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				if (layout == null)
				{
					string exp = "Update Custom_Gadget SET Layout = NULL,FormWidth=" + formWidth + ",FormHeight =" + formHeight + " WHERE CustomGadgetID = '" + reportID + "'";
					result = (ExecuteNonQuery(exp) > 0);
				}
				else
				{
					string exp = "Update Custom_Gadget SET Layout=@Layout, FormWidth=" + formWidth + ",FormHeight =" + formHeight + " WHERE CustomGadgetID = '" + reportID + "'";
					SqlCommand sqlCommand = new SqlCommand(exp);
					sqlCommand.Parameters.AddWithValue("@Layout", layout);
					sqlCommand.Transaction = transaction;
					result = (ExecuteNonQuery(sqlCommand) > 0);
				}
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

		public DataSet GetTableSchema(string query)
		{
			string text = query.ToLower();
			if (text.Contains("delete ") || text.Contains("alter ") || text.Contains("update ") || text.Contains("insert "))
			{
				throw new CompanyException("Delete, Alter,Insert, Update are not allowed in the query.");
			}
			DataSet dataSet = new DataSet();
			int num = 0;
			int num2 = 0;
			ArrayList arrayList = new ArrayList();
			while (num >= 0)
			{
				num = query.IndexOf("@", num2, StringComparison.CurrentCultureIgnoreCase);
				if (num == -1)
				{
					break;
				}
				num2 = query.IndexOf(" ", num);
				if (num2 == -1)
				{
					num2 = query.Length;
				}
				arrayList.Add(query.Substring(num, num2 - num));
			}
			foreach (string item in arrayList)
			{
				query = query.Replace(item, " 1=1 ");
			}
			query = query.Replace("SELECT", "SELECT TOP 1 ");
			FillDataSet(dataSet, "Table", query);
			dataSet.Tables[0].Rows.Clear();
			return dataSet;
		}

		public bool AddGadgetPhoto(string productID, byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Custom_Gadget SET Photo=@Photo WHERE CustomGadgetID='" + productID + "'");
				sqlCommand.Parameters.AddWithValue("@Photo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public bool RemoveGadgetPhoto(string gadgetID)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Custom_Gadget SET Photo= Null WHERE CustomGadgetID='" + gadgetID + "'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
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

		public MemoryStream GetgadgetThumbnailImage(string gadgetID)
		{
			string exp = "SELECT Photo\r\n\t\t\t\t\t\t   FROM Custom_Gadget WHERE CustomGadgetID='" + gadgetID + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				byte[] array = (byte[])obj;
				MemoryStream memoryStream = new MemoryStream();
				memoryStream.Write(array, 0, array.Length);
				return memoryStream;
			}
			return null;
		}
	}
}
