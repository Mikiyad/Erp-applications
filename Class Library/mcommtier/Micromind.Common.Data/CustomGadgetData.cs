using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomGadgetData : DataSet
	{
		public const string CUSTOMGADGETID_FIELD = "CustomGadgetID";

		public const string CUSTOMGADGETNAME_FIELD = "CustomGadgetName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string GADGETDATA_FIELD = "GadgetData";

		public const string CUSTOMGADGET_TABLE = "Custom_Gadget";

		public const string GADGETSTYLE_FIELD = "GadgetStyle";

		public const string CHARTTYPE_FIELD = "ChartType";

		public const string CHARTPALLET_FIELD = "ChartPallet";

		public const string COLOREACH_FIELD = "ColorEach";

		public const string COLORPALETTENAME_FIELD = "ColorPaletteName";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string FILTEROPTION_FIELD = "FilterOption";

		public const string DRILLACTION_FIELD = "DrillAction";

		public const string DRILLCARDTYPEID_FIELD = "DrillCardTypeID";

		public const string DRILLCARDIDFIELD_FIELD = "DrillCardIDField";

		public const string DRILLTRANSACTIONSYSDOCIDFIELD_FIELD = "DrillTransactionSysDocIDField";

		public const string DRILLTRANSACTIONVOUCHERIDFIELD_FIELD = "DrillTransactionVoucherIDField";

		public const string ISPREVIEW_FIELD = "IsPreview";

		public const string ISSUBREPORT_FIELD = "IsSubReport";

		public const string AUTOREFRESH_FIELD = "AutoRefresh";

		public const string REFRESHINTERVAL_FIELD = "RefreshInterval";

		public const string DRILLPARM1_FIELD = "DrillParm1";

		public const string DRILLPARM2_FIELD = "DrillParm2";

		public const string DRILLPARM3_FIELD = "DrillParm3";

		public const string DRILLPARM4_FIELD = "DrillParm4";

		public const string DRILLSUBREPORTID_FIELD = "DrillSubReportID";

		public const string CHARTSERIES_TABLE = "Chart_Series";

		public const string SERIESID_FIELD = "SeriesID";

		public const string DISPLAYNAME_FIELD = "DisplayName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string SHOWLEGEND_FIELD = "ShowLegend";

		public const string AXISXTITLE_FIELD = "AxisXTitle";

		public const string AXISYTITLE_FIELD = "AxisYTitle";

		public const string AXISYTEXTPATTERN_FIELD = "AxisYTextPattern";

		public const string ISROTATED_FIELD = "IsRotated";

		public const string AXISYVISIBLE_FIELD = "AxisYVisible";

		public const string TOPNOPTION_FIELD = "TopNOption";

		public const string TOPNCOUNT_FIELD = "TopNCount";

		public const string TOPNOTHERSTEXT_FIELD = "TopNOthersText";

		public const string SHOWTOPNOTHER_FIELD = "ShowTopNOther";

		public const string LEGENDTEXTPATTERN_FIELD = "LegendTextPattern";

		public const string GSHOWGAUGETEXT_FIELD = "GShowGaugeText";

		public const string GSHOWNEEDLE_FIELD = "GShowNeedle";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CHARTVALUECOLUMN_FIELD = "ChartVAlueColumn";

		public const string CHARTARGCOLUMN_FIELD = "ChartArgColumn";

		public const string LABELVISIBLE_FIELD = "LabelVisible";

		public const string LABELPOSITION_FIELD = "LabelPosition";

		public const string COLOR_FIELD = "Color";

		public const string LABELTEXTPATTERN_FIELD = "LabelTextPattern";

		public const string INDVALUECOLUMN_FIELD = "IndValueColumn";

		public const string INDTEXTVALUECOLUMN_FIELD = "IndTextValueColumn";

		public const string SHOWNAME_FIELD = "ShowName";

		public const string SHOWINDICATOR_FIELD = "ShowIndicator";

		public const string TEXTCOLOR_FIELD = "TextColor";

		public const string CUSTOMREPORTID_FIELD = "CustomReportID";

		public const string CUSTOMREPORTTYPE_FIELD = "CustomReportType";

		public const string RANGEID_FIELD = "RangeID";

		public const string GAUGERANGE_TABLE = "Gauge_Range";

		public const string STARTVALUE_FIELD = "StartValue";

		public const string ENDVALUE_FIELD = "EndValue";

		public const string RANGECOLOR_FIELD = "RangeColor";

		public const string FIELDID_FIELD = "FieldID";

		public const string LISTHIDDENFIELDS_TABLE = "ListHiddenFields";

		public DataTable CustomGadgetTable => base.Tables["Custom_Gadget"];

		public DataTable ChartSeriesTable => base.Tables["Chart_Series"];

		public DataTable GaugeRangeTable => base.Tables["Gauge_Range"];

		public DataTable ListHiddenFieldsTable => base.Tables["ListHiddenFields"];

		public CustomGadgetData()
		{
			BuildDataTables();
		}

		public CustomGadgetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Custom_Gadget");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CustomGadgetID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CustomGadgetName", typeof(string));
			columns.Add("ChartVAlueColumn", typeof(string));
			columns.Add("ChartArgColumn", typeof(string));
			columns.Add("CategoryID", typeof(string));
			columns.Add("FilterOption", typeof(byte));
			columns.Add("GadgetStyle", typeof(byte));
			columns.Add("ChartType", typeof(byte));
			columns.Add("ColorEach", typeof(bool));
			columns.Add("DrillAction", typeof(byte));
			columns.Add("DrillCardTypeID", typeof(short));
			columns.Add("DrillCardIDField", typeof(string));
			columns.Add("DrillTransactionSysDocIDField", typeof(string));
			columns.Add("DrillTransactionVoucherIDField", typeof(string));
			columns.Add("IsPreview", typeof(bool));
			columns.Add("AutoRefresh", typeof(bool));
			columns.Add("RefreshInterval", typeof(short));
			columns.Add("DrillSubReportID", typeof(int));
			columns.Add("DrillParm1", typeof(string));
			columns.Add("DrillParm2", typeof(string));
			columns.Add("DrillParm3", typeof(string));
			columns.Add("DrillParm4", typeof(string));
			columns.Add("IsSubReport", typeof(bool));
			columns.Add("TopNOption", typeof(bool));
			columns.Add("TopNCount", typeof(byte));
			columns.Add("ShowTopNOther", typeof(bool));
			columns.Add("TopNOthersText", typeof(string));
			columns.Add("LegendTextPattern", typeof(string));
			columns.Add("ShowLegend", typeof(bool));
			columns.Add("IsRotated", typeof(bool));
			columns.Add("AxisYVisible", typeof(bool));
			columns.Add("AxisXTitle", typeof(string));
			columns.Add("AxisYTitle", typeof(string));
			columns.Add("AxisYTextPattern", typeof(string));
			columns.Add("IndValueColumn", typeof(string));
			columns.Add("IndTextValueColumn", typeof(string));
			columns.Add("ShowName", typeof(bool));
			columns.Add("ShowIndicator", typeof(bool));
			columns.Add("TextColor", typeof(int));
			columns.Add("DisplayName", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("GShowGaugeText", typeof(bool));
			columns.Add("GShowNeedle", typeof(bool));
			columns.Add("ColorPaletteName", typeof(string));
			columns.Add("ChartPallet", typeof(byte));
			columns.Add("GadgetData", typeof(byte[]));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Chart_Series");
			columns = dataTable.Columns;
			columns.Add("CustomGadgetID", typeof(string));
			columns.Add("SeriesID", typeof(string));
			columns.Add("DisplayName", typeof(string));
			columns.Add("ChartVAlueColumn", typeof(string));
			columns.Add("ChartType", typeof(short));
			columns.Add("LabelVisible", typeof(bool));
			columns.Add("LabelPosition", typeof(string));
			columns.Add("Color", typeof(int));
			columns.Add("LabelTextPattern", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Gauge_Range");
			columns = dataTable.Columns;
			columns.Add("CustomReportID", typeof(string));
			columns.Add("CustomReportType", typeof(short));
			columns.Add("RangeID", typeof(string));
			columns.Add("StartValue", typeof(decimal));
			columns.Add("EndValue", typeof(decimal));
			columns.Add("RangeColor", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("ListHiddenFields");
			columns = dataTable.Columns;
			columns.Add("CustomReportID", typeof(string));
			columns.Add("CustomReportType", typeof(short));
			columns.Add("FieldID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
