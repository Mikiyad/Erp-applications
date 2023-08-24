using Micromind.Common.Data;
using System.Data;

namespace Micromind.ClientLibraries
{
	public static class TaxHelper
	{
		private static DataSet taxData;

		public static bool RefreshTaxData;

		private static DataSet TaxData
		{
			get
			{
				if (taxData == null || RefreshTaxData)
				{
					RefreshTaxData = false;
					taxData = Factory.TaxGroupSystem.GetTaxGroupDetailList();
					return taxData;
				}
				return taxData;
			}
		}

		public static TaxTransactionData CreateTaxDetailData(PayeeTaxOptions payeeTaxOption, string payeeTaxGroupID)
		{
			return CreateTaxDetailData(payeeTaxOption, payeeTaxGroupID, ItemTaxOptions.BasedOnCustomer, "");
		}

		public static TaxTransactionData CreateTaxDetailData(PayeeTaxOptions payeeTaxOption, string payeeTaxGroupID, ItemTaxOptions itemTaxOption, string itemTaxGroupID)
		{
			TaxTransactionData taxTransactionData = new TaxTransactionData();
			if (!CompanyPreferences.IsTax)
			{
				return taxTransactionData;
			}
			if (payeeTaxOption == PayeeTaxOptions.NonTaxable || itemTaxOption == ItemTaxOptions.NonTaxable)
			{
				return taxTransactionData;
			}
			DataSet dataSet = TaxData;
			checked
			{
				if (itemTaxOption == ItemTaxOptions.BasedOnCustomer)
				{
					DataRow[] array = dataSet.Tables[0].Select("TaxGroupID = '" + payeeTaxGroupID + "'");
					for (int i = 0; i < array.Length; i++)
					{
						DataRow dataRow = taxTransactionData.TaxDetailTable.NewRow();
						dataRow["TaxGroupID"] = payeeTaxGroupID;
						dataRow["TaxItemID"] = array[i]["TaxCode"];
						dataRow["TaxRate"] = array[i]["TaxRate"];
						dataRow["CalculationMethod"] = array[i]["CalculationMethod"];
						dataRow["TaxItemName"] = array[i]["TaxItemName"];
						taxTransactionData.TaxDetailTable.Rows.Add(dataRow);
					}
				}
				else
				{
					DataRow[] array2 = dataSet.Tables[0].Select("TaxGroupID = '" + payeeTaxGroupID + "'");
					DataRow[] array3 = dataSet.Tables[0].Select("TaxGroupID = '" + itemTaxGroupID + "'");
					for (int j = 0; j < array2.Length; j++)
					{
						for (int k = 0; k < array3.Length; k++)
						{
							if (array2[j]["TaxCode"] == array3[k]["TaxCode"])
							{
								DataRow dataRow2 = taxTransactionData.TaxDetailTable.NewRow();
								dataRow2["TaxGroupID"] = payeeTaxGroupID;
								dataRow2["TaxItemID"] = array2[j]["TaxCode"];
								dataRow2["CalculationMethod"] = array2[j]["CalculationMethod"];
								dataRow2["TaxRate"] = array2[j]["TaxRate"];
								dataRow2["TaxItemName"] = array2[j]["TaxItemName"];
								taxTransactionData.TaxDetailTable.Rows.Add(dataRow2);
							}
						}
					}
				}
				return taxTransactionData;
			}
		}

		public static void CreateTaxRows(DataSet data, TaxTransactionData taxData, TaxDetailLevel level, string sysDocID, string voucherID, int rowIndex, string currencyID, decimal currencyRate)
		{
			DataTable dataTable = data.Tables["Tax_Detail"];
			DataTable dataTable2 = taxData.Tables["Tax_Detail"];
			int num = 0;
			foreach (DataRow row in dataTable2.Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["SysDocID"] = sysDocID;
				dataRow2["VoucherID"] = voucherID;
				dataRow2["TaxGroupID"] = row["TaxGroupID"];
				dataRow2["TaxItemID"] = row["TaxItemID"];
				dataRow2["TaxLevel"] = level;
				dataRow2["TaxRate"] = row["TaxRate"];
				dataRow2["CalculationMethod"] = row["CalculationMethod"];
				dataRow2["TaxAmount"] = row["TaxAmount"];
				dataRow2["CurrencyID"] = currencyID;
				dataRow2["CurrencyRate"] = currencyRate;
				dataRow2["RowIndex"] = rowIndex;
				dataRow2["OrderIndex"] = num;
				dataTable.Rows.Add(dataRow2);
				num = checked(num + 1);
			}
		}
	}
}
