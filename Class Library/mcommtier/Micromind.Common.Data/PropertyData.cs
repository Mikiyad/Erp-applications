using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyData : DataSet
	{
		public const string PROPERTYID_FIELD = "PropertyID";

		public const string PROPERTYNAME_FIELD = "PropertyName";

		public const string SHORTNAME_FIELD = "ShortName";

		public const string OFFERTYPEID_FIELD = "OfferTypeID";

		public const string FIXEDASSETID_FIELD = "FixedAssetID";

		public const string PROPERTYCLASSID_FIELD = "PropertyClassID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string CITYID_FIELD = "CityID";

		public const string AREAID_FIELD = "AreaID";

		public const string ADDRESS1_FIELD = "Address1";

		public const string ADDRESS2_FIELD = "Address2";

		public const string YEARBUILT_FIELD = "YearBuilt";

		public const string BUILTBY_FIELD = "Builtby";

		public const string LANDAREA_FIELD = "LandArea";

		public const string BUILDAREA_FIELD = "BuildArea";

		public const string OWNERNAME_FIELD = "OwnerName";

		public const string REGISTERNUMBER_FIELD = "RegisterNumber";

		public const string RENTINCOMEACCOUNTID_FIELD = "RentIncomeAccountID";

		public const string PREPAIDRENTINCOMEACCOUNTID_FIELD = "PrepaidRentIncomeAccountID";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string ISPERIODICINVOICE_FIELD = "IsPeriodicInvoice";

		public const string ELECTRICITYREGNNUMBER_FIELD = "ElectricityRegnNumber";

		public const string TELECOMREGNNUMBER_FIELD = "TelecomRegnNumber";

		public const string MUNICIPALITYREGNNUMBER_FIELD = "MunicipalityRegnNumber";

		public const string ELECTRICITYPREMISENUMBER_FIELD = "ElectricityPremiseNumber";

		public const string ELECTRICITYCONTRACTNUMBER_FIELD = "ElectricityContractNumber";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PROPERTY_TABLE = "Property";

		public const string PROPERTYUNIT_TABLE = "Property_Unit";

		public const string PROPERTYFACILITY_TABLE = "Property_Facility";

		public const string PROPERTYOWNERDETAIL_TABLE = "Property_Owner_Detail";

		public const string FACILITYID_FIELD = "FacilityID";

		public const string TYPE_FIELD = "Type";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXIDNUMBER_FIELD = "TaxIDNumber";

		public const string PROPERTYOWNERID_FIELD = "PropertyOwnerID";

		public const string OWNERSHIPPERCENT_FIELD = "OwnerShipPercent";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ROWINDEX_FIELD = "RowIndex";

		public DataTable PropertyTable => base.Tables["Property"];

		public DataTable PropertyUnitTable => base.Tables["Property_Unit"];

		public DataTable PropertyFacilityTable => base.Tables["Property_Facility"];

		public DataTable PropertyOwnerDetailTable => base.Tables["Property_Owner_Detail"];

		public PropertyData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public PropertyData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PropertyID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PropertyName", typeof(string)).AllowDBNull = false;
			columns.Add("ShortName", typeof(string));
			columns.Add("OfferTypeID", typeof(string));
			columns.Add("FixedAssetID", typeof(string));
			columns.Add("PropertyClassID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("CityID", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("Address1", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("YearBuilt", typeof(short));
			columns.Add("Builtby", typeof(string));
			columns.Add("LandArea", typeof(decimal)).DefaultValue = 0;
			columns.Add("BuildArea", typeof(decimal)).DefaultValue = 0;
			columns.Add("OwnerName", typeof(string));
			columns.Add("RegisterNumber", typeof(string));
			columns.Add("RentIncomeAccountID", typeof(string));
			columns.Add("PrepaidRentIncomeAccountID", typeof(string));
			columns.Add("ElectricityRegnNumber", typeof(string));
			columns.Add("TelecomRegnNumber", typeof(string));
			columns.Add("MunicipalityRegnNumber", typeof(string));
			columns.Add("ElectricityPremiseNumber", typeof(string));
			columns.Add("ElectricityContractNumber", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("Status", typeof(bool)).DefaultValue = false;
			columns.Add("IsPeriodicInvoice", typeof(bool)).DefaultValue = false;
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxIDNumber", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Facility");
			columns = dataTable.Columns;
			columns.Add("PropertyID", typeof(string));
			columns.Add("FacilityID", typeof(string));
			columns.Add("Type", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Owner_Detail");
			columns = dataTable.Columns;
			columns.Add("PropertyID", typeof(string));
			columns.Add("PropertyOwnerID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
