using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class PriceLevelSystem : MarshalByRefObject, IPriceLevelSystem, IDisposable
	{
		private Config config;

		public PriceLevelSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePriceLevel(string levelName)
		{
			PriceLevelData priceLevelData = new PriceLevelData();
			DataTable dataTable = priceLevelData.Tables["Price_Level"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["PriceLevelName"] = levelName.Trim();
			dataTable.Rows.Add(dataRow);
			return CreatePriceLevel(priceLevelData);
		}

		public bool CreatePriceLevel(PriceLevelData priceLevelData)
		{
			return new PriceLevels(config).InsertPriceLevel(priceLevelData);
		}

		public bool UpdatePriceLevel(PriceLevelData priceLevelData)
		{
			return new PriceLevels(config).UpdatePriceLevel(priceLevelData);
		}

		public PriceLevelData GetPriceLevelByID(string priceLevelID)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelByID(priceLevelID);
			}
		}

		public PriceLevelData GetPriceLevels()
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevels();
			}
		}

		public bool DeletePriceLevel(string priceLevelID)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.DeletePriceLevel(priceLevelID);
			}
		}

		public DataSet GetPriceLevelsByFields(params string[] columns)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelsByFields(columns);
			}
		}

		public DataSet GetPriceLevelsByFields(int[] levelID, params string[] columns)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelsByFields(levelID, columns);
			}
		}

		public DataSet GetPriceLevelsByFields(bool isInactive, int[] levelID, params string[] columns)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelsByFields(isInactive, levelID, columns);
			}
		}

		public bool ExistPriceLevel(string levelName)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.ExistPriceLevel(levelName);
			}
		}

		public string GetPriceLevelIDByName(string levelName)
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelIDByName(levelName);
			}
		}

		public DataSet GetPriceLevelList()
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelList();
			}
		}

		public DataSet GetPriceLevelComboList()
		{
			using (PriceLevels priceLevels = new PriceLevels(config))
			{
				return priceLevels.GetPriceLevelComboList();
			}
		}

		public bool CreateUpdatePriceLevelBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Price_Level"), "PriceLevel Data must exist.", 0);
			PriceLevels priceLevels = new PriceLevels(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Price_Level"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Price_Level"].Columns.Contains("PriceLevelName"))
					{
						text2 = row["PriceLevelName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						PriceLevelData priceLevelData = new PriceLevelData();
						DataRow dataRow2 = priceLevelData.PriceLevelTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Price_Level"].Columns)
						{
							try
							{
								if (priceLevelData.PriceLevelTable.Columns.Contains(column.ColumnName))
								{
									dataRow2[column.ColumnName] = row[column.ColumnName];
								}
								else if (column.ColumnName.ToLower() == "OverwriteExistingRecord".ToLower())
								{
									flag2 = bool.Parse(row[column.ColumnName].ToString());
								}
								else if (column.ColumnName.ToLower() == "CreateNewRecord".ToLower())
								{
									flag3 = bool.Parse(row[column.ColumnName].ToString());
								}
							}
							catch
							{
							}
						}
						priceLevelData.PriceLevelTable.Rows.Add(dataRow2);
						if (priceLevels.ExistPriceLevel(text2))
						{
							flag = true;
							text = priceLevels.GetPriceLevelIDByName(text2);
							priceLevelData.PriceLevelTable.AcceptChanges();
							dataRow2["PriceLevelID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreatePriceLevel(priceLevelData);
								}
							}
							else if (flag2)
							{
								UpdatePriceLevel(priceLevelData);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						priceLevelData.Dispose();
						priceLevelData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				priceLevels.Dispose();
				priceLevels = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}
	}
}
