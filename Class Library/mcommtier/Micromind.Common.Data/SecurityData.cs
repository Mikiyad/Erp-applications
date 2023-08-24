using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SecurityData : DataSet
	{
		public const string USERS_TABLE = "Users";

		public const string LOGINNAME_FIELD = "";

		public const string MENUSECURITY_TABLE = "Menu_Security";

		public const string CUSTOMREPORTSECURITY_TABLE = "Custom_Report_Security";

		public const string MENUID_FIELD = "MenuID";

		public const string SUBMENUID_FIELD = "SubMenuID";

		public const string DROPDOWNID_FIELD = "DropDownID";

		public const string ENABLE_FIELD = "Enable";

		public const string VISIBLE_FIELD = "Visible";

		public const string USERID_FIELD = "UserID";

		public const string GROUPID_FIELD = "GroupID";

		public const string REPORTTYPE_FIELD = "ReportType";

		public const string TABSSECURITY_TABLE = "Tabs_Security";

		public const string TABID_FIELD = "TabID";

		public const string LINKGROUPID_FIELD = "LinkGroupID";

		public const string LINKID_FIELD = "LinkID";

		public const string SCREENSECURITY_TABLE = "Screen_Security";

		public const string SCREENID_FIELD = "ScreenID";

		public const string VIEW_FIELD = "ViewRight";

		public const string NEW_FIELD = "NewRight";

		public const string EDIT_FIELD = "EditRight";

		public const string DELETE_FIELD = "DeleteRight";

		public const string GENERALSECURITY_TABLE = "General_Security";

		public const string SECURITYROLEID_FIELD = "SecurityRoleID";

		public const string ISALLOWED_FIELD = "IsAllowed";

		public const string INTVAL_FIELD = "intVal";

		public const string CARDSECURITY_TABLE = "Card_Security";

		public const string CARDID_FIELD = "CardID";

		public const string CONDITIONALQUERY_FIELD = "ConditionalQuery";

		public const string OTHERFILTERQUERY_FIELD = "OtherFilterQuery";

		public const string FILTERCONTROL_FIELD = "FilterControl";

		public const string FILTERFROM_FIELD = "FilterFrom";

		public const string FILTERTO_FIELD = "FilterTo";

		public const string ISMANUAL_FIELD = "IsManual";

		public DataTable MenuSecurityTable => base.Tables["Menu_Security"];

		public DataTable CustomReportSecurityTable => base.Tables["Custom_Report_Security"];

		public DataTable TabSecurityTable => base.Tables["Tabs_Security"];

		public DataTable ScreenSecurityTable => base.Tables["Screen_Security"];

		public DataTable GeneralSecurityTable => base.Tables["General_Security"];

		public DataTable CardSecurityTable => base.Tables["Card_Security"];

		public SecurityData()
		{
			BuildDataTables();
		}

		public SecurityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Menu_Security");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("MenuID", typeof(string));
			columns.Add("SubMenuID", typeof(string));
			columns.Add("DropDownID", typeof(string));
			columns.Add("Enable", typeof(bool));
			columns.Add("Visible", typeof(bool));
			columns.Add("UserID", typeof(string));
			columns.Add("GroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Custom_Report_Security");
			DataColumnCollection columns2 = dataTable.Columns;
			columns2.Add("MenuID", typeof(string));
			columns2.Add("SubMenuID", typeof(string));
			columns2.Add("DropDownID", typeof(string));
			columns2.Add("Enable", typeof(bool));
			columns2.Add("Visible", typeof(bool));
			columns2.Add("UserID", typeof(string));
			columns2.Add("GroupID", typeof(string));
			columns2.Add("ReportType", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Tabs_Security");
			DataColumnCollection columns3 = dataTable.Columns;
			columns3.Add("TabID", typeof(string));
			columns3.Add("LinkGroupID", typeof(string));
			columns3.Add("LinkID", typeof(string));
			columns3.Add("Visible", typeof(bool));
			columns3.Add("UserID", typeof(string));
			columns3.Add("GroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Screen_Security");
			DataColumnCollection columns4 = dataTable.Columns;
			columns4.Add("ScreenID", typeof(string));
			columns4.Add("ViewRight", typeof(bool));
			columns4.Add("NewRight", typeof(bool));
			columns4.Add("EditRight", typeof(bool));
			columns4.Add("DeleteRight", typeof(bool));
			columns4.Add("UserID", typeof(string));
			columns4.Add("GroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("General_Security");
			DataColumnCollection columns5 = dataTable.Columns;
			columns5.Add("SecurityRoleID", typeof(short));
			columns5.Add("IsAllowed", typeof(bool));
			columns5.Add("UserID", typeof(string));
			columns5.Add("GroupID", typeof(string));
			columns5.Add("intVal", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Card_Security");
			DataColumnCollection columns6 = dataTable.Columns;
			columns6.Add("CardID", typeof(short));
			columns6.Add("ConditionalQuery", typeof(string));
			columns6.Add("OtherFilterQuery", typeof(string));
			columns6.Add("FilterControl", typeof(string));
			columns6.Add("FilterFrom", typeof(string));
			columns6.Add("FilterTo", typeof(string));
			columns6.Add("IsManual", typeof(bool));
			columns6.Add("UserID", typeof(string));
			columns6.Add("GroupID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
