using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyUnitData : DataSet
	{
		public const string PROPERTYUNITID_FIELD = "PropertyUnitID";

		public const string PROPERTYUNITNAME_FIELD = "PropertyUnitName";

		public const string SHORTNAME_FIELD = "ShortName";

		public const string UNITSTATUS_FIELD = "UnitStatus";

		public const string AVAILABLEFROM_FIELD = "AvailableFrom";

		public const string AVAILABLETO_FIELD = "AvailableTo";

		public const string PROPERTYID_FIELD = "PropertyID";

		public const string PARENTUNITID_FIELD = "ParentUnitID";

		public const string UNITTYPEID_FIELD = "UnitTypeID";

		public const string NOBEDROOMS_FIELD = "NoBedRooms";

		public const string NOBATHROOMS_FIELD = "NoBathRooms";

		public const string TOTALROOMS_FIELD = "TotalRooms";

		public const string AREA_FIELD = "Area";

		public const string NOOFPARKING_FIELD = "NoofParking";

		public const string VIEWTYPEID_FIELD = "ViewTypeID";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string PROPERTYTYPE_FIELD = "PropertyType";

		public const string RENTALINCOME_FIELD = "RentalIncome";

		public const string ELECTRICITYPREMISENUMBER_FIELD = "ElectricityPremiseNumber";

		public const string MUNICIPALITYFILENUMBER_FIELD = "MunicipalityFileNumber";

		public const string ELECTRICITYFILENUMBER_FIELD = "ElectricityFileNumber";

		public const string MUNICIPALITYPERMITNUMBER_FIELD = "MunicipalityPermitNumber";

		public const string ELECTRICITYPERMITNUMBER_FIELD = "ElectricityPermitNumber";

		public const string KITCHENTYPEID_FIELD = "KitchenTypeID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PROPERTYUNIT_TABLE = "Property_Unit";

		public DataTable PropertyUnitTable => base.Tables["Property_Unit"];

		public PropertyUnitData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public PropertyUnitData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Unit");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PropertyUnitID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PropertyUnitName", typeof(string)).AllowDBNull = false;
			columns.Add("ShortName", typeof(string));
			columns.Add("UnitStatus", typeof(string));
			columns.Add("AvailableFrom", typeof(DateTime));
			columns.Add("AvailableTo", typeof(DateTime));
			columns.Add("PropertyID", typeof(string));
			columns.Add("ParentUnitID", typeof(string));
			columns.Add("UnitTypeID", typeof(string));
			columns.Add("NoBedRooms", typeof(short));
			columns.Add("NoBathRooms", typeof(short));
			columns.Add("TotalRooms", typeof(short));
			columns.Add("Area", typeof(decimal));
			columns.Add("NoofParking", typeof(short));
			columns.Add("ViewTypeID", typeof(string));
			columns.Add("KitchenTypeID", typeof(string));
			columns.Add("RentalIncome", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("Status", typeof(bool)).DefaultValue = false;
			columns.Add("PropertyType", typeof(short));
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("ElectricityPremiseNumber", typeof(string));
			columns.Add("MunicipalityFileNumber", typeof(string));
			columns.Add("MunicipalityPermitNumber", typeof(string));
			columns.Add("ElectricityPermitNumber", typeof(string));
			columns.Add("ElectricityFileNumber", typeof(string));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
		}
	}
}
