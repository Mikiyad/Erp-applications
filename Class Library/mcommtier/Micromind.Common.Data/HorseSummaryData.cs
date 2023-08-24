using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class HorseSummaryData : DataSet
	{
		public const string HORSEID_FIELD = "HorseCode";

		public const string NAME_FIELD = "HorseName";

		public const string TYPE_FIELD = "HorseType";

		public const string REGISTERNUMBER_FIELD = "RegisterNumber";

		public const string MICROCHIPNUMBER_FIELD = "MicroChipNumber";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string BRAND_FIELD = "Brand";

		public const string BREED_FIELD = "Breed";

		public const string BIRTHDATE_FIELD = "DateOfBirth";

		public const string COLOR_FIELD = "Colour";

		public const string SEX_FIELD = "Sex";

		public const string SIRE_FIELD = "Sire";

		public const string OWNERSHIPTYPEID_FIELD = "OwnershipTypeID";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string DAM_FIELD = "Dam";

		public const string SIREOFDAM_FIELD = "SireOfDam";

		public const string COUNTRYOFORIGIN_FIELD = "CountryOfOrigin";

		public const string CURRENTOWNERSHIP_FIELD = "CurrentOwnerShip";

		public const string PREVIOUSOWNERSHIP_FIELD = "PreviousOwnership";

		public const string OWNERSHIPCHANGEDDATE_FIELD = "OwnerShipChangedDate";

		public const string BREEDER_FIELD = "Breeder";

		public const string LOCATION_FIELD = "LocationID";

		public const string TRAINER_FIELD = "RiderID";

		public const string CARETAKER_FIELD = "CareTaker";

		public const string PASSPORTISSUEDATE_FIELD = "PassportIssueDate";

		public const string PASSPORTEXPIRYDATE_FIELD = "PassportExpiryDate";

		public const string REVALIDATED_FIELD = "RevalidationDate";

		public const string IMPORTEDFROM_FIELD = "ImportedFrom";

		public const string IMPORTEDDATE_FIELD = "ImportedDate";

		public const string PASTPERFORMANCE_FIELD = "PastPerformance";

		public const string EXPORTEDTO_FIELD = "ExportedTo";

		public const string RECEIVEDFROM_FIELD = "ReceivedFrom";

		public const string RECEIVEDDATE_FIELD = "ReceivedDate";

		public const string TRANSFERREDTO_FIELD = "TransferredTo";

		public const string TRANSFERREDDATE_FIELD = "TransferredDate";

		public const string SOLDAT_FIELD = "SoldAt";

		public const string SOLDDATE_FIELD = "SoldDate";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string SEXCHANGEDFROM_FIELD = "SexChangedFrom";

		public const string SEXCHANGEDDATE_FIELD = "SexChangedDate";

		public const string TRANSFEROFOWNERSHIPGIVENON_FIELD = "OwnerShipTransferDate";

		public const string DEADON_FIELD = "DeadDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string HORSESUMMARY_TABLE = "Horse_Summary";

		public DataTable HorseSummaryTable => base.Tables["Horse_Summary"];

		public HorseSummaryData()
		{
			BuildDataTables();
		}

		public HorseSummaryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Horse_Summary");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("HorseCode", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("HorseName", typeof(string));
			columns.Add("HorseType", typeof(string));
			columns.Add("RegisterNumber", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			columns.Add("MicroChipNumber", typeof(string));
			columns.Add("Brand", typeof(string));
			columns.Add("Breed", typeof(string));
			columns.Add("DateOfBirth", typeof(DateTime));
			columns.Add("Colour", typeof(string));
			columns.Add("Sex", typeof(string));
			columns.Add("OwnershipTypeID", typeof(string));
			columns.Add("CategoryID", typeof(string));
			columns.Add("Sire", typeof(string));
			columns.Add("Dam", typeof(string));
			columns.Add("SireOfDam", typeof(string));
			columns.Add("CountryOfOrigin", typeof(string));
			columns.Add("CurrentOwnerShip", typeof(string));
			columns.Add("PreviousOwnership", typeof(string));
			columns.Add("OwnerShipChangedDate", typeof(DateTime));
			columns.Add("Breeder", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("RiderID", typeof(string));
			columns.Add("CareTaker", typeof(string));
			columns.Add("PassportIssueDate", typeof(DateTime));
			columns.Add("PassportExpiryDate", typeof(DateTime));
			columns.Add("RevalidationDate", typeof(DateTime));
			columns.Add("ImportedFrom", typeof(string));
			columns.Add("ImportedDate", typeof(DateTime));
			columns.Add("PastPerformance", typeof(string));
			columns.Add("ExportedTo", typeof(string));
			columns.Add("ReceivedFrom", typeof(string));
			columns.Add("ReceivedDate", typeof(DateTime));
			columns.Add("TransferredTo", typeof(string));
			columns.Add("TransferredDate", typeof(DateTime));
			columns.Add("SoldAt", typeof(string));
			columns.Add("SoldDate", typeof(DateTime));
			columns.Add("SexChangedFrom", typeof(string));
			columns.Add("SexChangedDate", typeof(DateTime));
			columns.Add("OwnerShipTransferDate", typeof(DateTime));
			columns.Add("DeadDate", typeof(DateTime));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
