using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductData : DataSet
	{
		public const string PRODUCT_TABLE = "Product";

		public const string LOCATIONPRODUCT_TABLE = "Location_Product";

		public const string PRODUCTUNIT_TABLE = "Product_Unit";

		public const string PRODUCTBOM_TABLE = "Product_BOM";

		public const string PRODUCTPARENT_TABLE = "Product_Parent";

		public const string PRODUCTPARENTID_FIELD = "ProductParentID";

		public const string MATRIXPARENTID_FIELD = "MatrixParentID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string DESCRIPTION2_FIELD = "Description2";

		public const string DESCRIPTION3_FIELD = "Description3";

		public const string UPC_FIELD = "UPC";

		public const string ISPRICEEMBEDDED_FIELD = "IsPriceEmbedded";

		public const string CLASSID_FIELD = "ClassID";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string VENDORREF_FIELD = "VendorRef";

		public const string UNITPRICE1_FIELD = "UnitPrice1";

		public const string UNITPRICE2_FIELD = "UnitPrice2";

		public const string UNITPRICE3_FIELD = "UnitPrice3";

		public const string MINPRICE_FIELD = "MinPrice";

		public const string EXCLUDEFROMCATALOGE_FIELD = "ExcludeFromCatalogue";

		public const string ATTRIBUTE1_FIELD = "Attribute1";

		public const string ATTRIBUTE2_FIELD = "Attribute2";

		public const string ATTRIBUTE3_FIELD = "Attribute3";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string STANDARDCOST_FIELD = "StandardCost";

		public const string AVERAGECOST_FIELD = "AverageCost";

		public const string LASTCOST_FIELD = "LastCost";

		public const string COSTMETHOD_FIELD = "CostMethod";

		public const string QUANTITY_FIELD = "Quantity";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string QUANTITYPERUNIT_FIELD = "QuantityPerUnit";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string ISHOLDSALE_FIELD = "IsHoldSale";

		public const string WEIGHT_FIELD = "Weight";

		public const string PHOTO_FIELD = "Photo";

		public const string UNITID_FIELD = "UnitID";

		public const string REORDERLEVEL_FIELD = "ReorderLevel";

		public const string DEFAULTLOCATIONID_FIELD = "DefaultLocationID";

		public const string BOMID_FIELD = "BOMID";

		public const string EXPENSECODE_FIELD = "ExpenseCode";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXIDNUMBER_FIELD = "TaxIDNumber";

		public const string COGSACCOUNT_FIELD = "COGSAccount";

		public const string ASSETACCOUNT_FIELD = "AssetAccount";

		public const string INCOMEACCOUNT_FIELD = "IncomeAccount";

		public const string PREFERREDVENDOR_FIELD = "PreferredVendor";

		public const string ISTRACKLOT_FIELD = "IsTrackLot";

		public const string ISTRACKSERIAL_FIELD = "IsTrackSerial";

		public const string MATERIALID_FIELD = "MaterialID";

		public const string FINISHINGID_FIELD = "FinishingID";

		public const string COLORID_FIELD = "ColorID";

		public const string GRADEID_FIELD = "GradeID";

		public const string STANDARDID_FIELD = "StandardID";

		public const string PTYPE1_FIELD = "PType1";

		public const string PTYPE2_FIELD = "PType2";

		public const string PTYPE3_FIELD = "PType3";

		public const string PTYPE4_FIELD = "PType4";

		public const string PTYPE5_FIELD = "PType5";

		public const string PTYPE6_FIELD = "PType6";

		public const string PTYPE7_FIELD = "PType7";

		public const string PTYPE8_FIELD = "PType8";

		public const string STYLEID_FIELD = "StyleID";

		public const string ATTRIBUTE_FIELD = "Attribute";

		public const string SIZE_FIELD = "Size";

		public const string BRANDID_FIELD = "BrandID";

		public const string MANUFACTURERID_FIELD = "ManufacturerID";

		public const string ORIGIN_FIELD = "Origin";

		public const string WARRANTYPERIOD_FIELD = "WarrantyPeriod";

		public const string RACKBIN_FIELD = "RackBin";

		public const string NOTE_FIELD = "Note";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ISMAINUNIT_FIELD = "IsMainUnit";

		public const string FACTOR_FIELD = "Factor";

		public const string BOMPRODUCTID_FIELD = "BOMProductID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string COST_FIELD = "Cost";

		public const string W3PLRENTPRICE_FIELD = "W3PLRentPrice";

		public const string PRODUCTLOTRECEIVINGDETAIL_TABLE = "Product_Lot_Receiving_Detail";

		public const string PRODUCTLOTISSUEDETAIL_TABLE = "Product_Lot_Issue_Detail";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string SOURCELOTNUMBER_FIELD = "SourceLotNumber";

		public const string BINID_FIELD = "BinID";

		public const string RACKID_FIELD = "RackID";

		public const string LOTQTY_FIELD = "LotQty";

		public const string SOLDQTY_FIELD = "SoldQty";

		public const string PRODUCTIONDATE_FIELD = "ProductionDate";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REFERENCE_FIELD = "Reference";

		public const string RECEIPTDATE_FIELD = "ReceiptDate";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTPARTSDETAIL_TABLE = "Product_Parts_Detail";

		public const string PRODUCTSUBSTITUTEDETAIL_TABLE = "Product_Substitute_Detail";

		public const string PRODUCTAPPLIEDMODELSDETAIL_TABLE = "Product_AppliedModels_Detail";

		public const string SPECIFICATION_FIELD = "Specification";

		public const string VEHICLEMAKEID_FIELD = "VehicleMakeID";

		public const string VEHICLETYPEID_FIELD = "VehicleTypeID";

		public const string VEHICLEMODELID_FIELD = "VehicleModelID";

		public const string PARTSMAKETYPEID_FIELD = "PartsMakeTypeID";

		public const string PARTSTYPEID_FIELD = "PartsTypeID";

		public const string PARTSFAMILYID_FIELD = "PartsFamilyID";

		public const string PARTSCHASISNO_FIELD = "PartsChasisNo";

		public const string PARTSMODEL_FIELD = "PartsModel";

		public const string PARTSENGINENO_FIELD = "PartsEngineNo";

		public const string OEMCODE_FIELD = "OEMCode";

		public const string PRODUCCTPRICELISTDETAIL_TABLE = "Product_PriceList_Detail";

		public const string UNITPRICE1GRID_FIELD = "UnitPrice1";

		public const string UNITPRICE2GRID_FIELD = "UnitPrice2";

		public const string UNITPRICE3GRID_FIELD = "UnitPrice3";

		public const string MINPRICEGRID_FIELD = "MinPrice";

		public const string LOCATIONIDGRID_FIELD = "LocationID";

		public const string SUBSTITUTEPRODUCTID_FIELD = "SubstituteProductID";

		public const string SUBPRODUCTDESCRIPTION_FIELD = "SubProductDescription";

		public const string REMARKS_FIELD = "Remarks";

		public DataTable ProductTable => base.Tables["Product"];

		public DataTable UnitTable => base.Tables["Product_Unit"];

		public DataTable BOMTable => base.Tables["Product_BOM"];

		public DataTable ProductPartsDetail => base.Tables["Product_Parts_Detail"];

		public DataTable ProductSubstituteDetail => base.Tables["Product_Substitute_Detail"];

		public DataTable ProductAppliedModelsDetail => base.Tables["Product_AppliedModels_Detail"];

		public DataTable ProductPriceListDetail => base.Tables["Product_PriceList_Detail"];

		public ProductData()
		{
			BuildDataTables();
		}

		public ProductData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ProductID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string)).AllowDBNull = false;
			columns.Add("Description2", typeof(string));
			columns.Add("Description3", typeof(string));
			columns.Add("UPC", typeof(string));
			columns.Add("IsPriceEmbedded", typeof(bool));
			columns.Add("ClassID", typeof(string));
			columns.Add("VendorRef", typeof(string));
			columns.Add("MatrixParentID", typeof(string));
			columns.Add("ItemType", typeof(byte));
			columns.Add("UnitPrice1", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPrice2", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPrice3", typeof(decimal)).DefaultValue = 0;
			columns.Add("MinPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("StandardCost", typeof(decimal)).DefaultValue = 0;
			columns.Add("LastCost", typeof(decimal)).DefaultValue = 0;
			columns.Add("CostMethod", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("AverageCost", typeof(decimal)).DefaultValue = 0;
			columns.Add("W3PLRentPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("CategoryID", typeof(string));
			columns.Add("Attribute1", typeof(string));
			columns.Add("Attribute2", typeof(string));
			columns.Add("Attribute3", typeof(string));
			columns.Add("ExcludeFromCatalogue", typeof(bool));
			columns.Add("QuantityPerUnit", typeof(float)).DefaultValue = 1;
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("ReorderLevel", typeof(float)).DefaultValue = 0;
			columns.Add("UnitID", typeof(string));
			columns.Add("Photo", typeof(byte[]));
			columns.Add("DefaultLocationID", typeof(string));
			columns.Add("COGSAccount", typeof(string));
			columns.Add("BOMID", typeof(string));
			columns.Add("AssetAccount", typeof(string));
			columns.Add("ExpenseCode", typeof(string));
			columns.Add("IncomeAccount", typeof(string));
			columns.Add("Weight", typeof(string));
			columns.Add("StyleID", typeof(string));
			columns.Add("IsTrackLot", typeof(bool));
			columns.Add("IsTrackSerial", typeof(bool));
			columns.Add("Attribute", typeof(string));
			columns.Add("Size", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = 0;
			columns.Add("IsHoldSale", typeof(bool)).DefaultValue = 0;
			columns.Add("PreferredVendor", typeof(string));
			columns.Add("BrandID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("ManufacturerID", typeof(string));
			columns.Add("Origin", typeof(string));
			columns.Add("WarrantyPeriod", typeof(string));
			columns.Add("RackBin", typeof(string));
			columns.Add("Note", typeof(string)).AllowDBNull = true;
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("MaterialID", typeof(string));
			columns.Add("FinishingID", typeof(string));
			columns.Add("ColorID", typeof(string));
			columns.Add("GradeID", typeof(string));
			columns.Add("StandardID", typeof(string));
			columns.Add("PType1", typeof(string));
			columns.Add("PType2", typeof(string));
			columns.Add("PType3", typeof(string));
			columns.Add("PType4", typeof(string));
			columns.Add("PType5", typeof(string));
			columns.Add("PType6", typeof(string));
			columns.Add("PType7", typeof(string));
			columns.Add("PType8", typeof(string));
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxIDNumber", typeof(string));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_Unit");
			columns = dataTable.Columns;
			dataColumn = columns.Add("ProductID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("UnitID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("FactorType", typeof(string)).AllowDBNull = false;
			columns.Add("Factor", typeof(float)).AllowDBNull = false;
			columns.Add("IsMainUnit", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_Parts_Detail");
			columns = dataTable.Columns;
			columns.Add("ProductID", typeof(string));
			columns.Add("Specification", typeof(string));
			columns.Add("VehicleMakeID", typeof(string));
			columns.Add("VehicleTypeID", typeof(string));
			columns.Add("VehicleModelID", typeof(string));
			columns.Add("PartsMakeTypeID", typeof(string));
			columns.Add("PartsTypeID", typeof(string));
			columns.Add("PartsFamilyID", typeof(string));
			columns.Add("PartsChasisNo", typeof(string));
			columns.Add("PartsModel", typeof(string));
			columns.Add("PartsEngineNo", typeof(string));
			columns.Add("OEMCode", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_Substitute_Detail");
			columns = dataTable.Columns;
			columns.Add("ProductID", typeof(string));
			columns.Add("SubstituteProductID", typeof(string));
			columns.Add("SubProductDescription", typeof(string));
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_AppliedModels_Detail");
			columns = dataTable.Columns;
			columns.Add("ProductID", typeof(string));
			columns.Add("Specification", typeof(string));
			columns.Add("VehicleMakeID", typeof(string));
			columns.Add("VehicleTypeID", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_PriceList_Detail");
			columns = dataTable.Columns;
			columns.Add("ProductID", typeof(string));
			columns.Add("UnitPrice1", typeof(string));
			columns.Add("UnitPrice2", typeof(string));
			columns.Add("UnitPrice3", typeof(string));
			columns.Add("MinPrice", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("UnitID", typeof(string));
			base.Tables.Add(dataTable);
		}

		public static string GetItemTypeName(ItemTypes itemType)
		{
			if (itemType == ItemTypes.Service)
			{
				return "Service";
			}
			return itemType.ToString();
		}
	}
}
