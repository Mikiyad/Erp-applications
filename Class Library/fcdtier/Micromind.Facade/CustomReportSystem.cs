using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class CustomReportSystem : MarshalByRefObject, ICustomReportSystem, IDisposable
	{
		private Config config;

		public CustomReportSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCustomReport(CustomReportData customReportData)
		{
			if (new CustomReports(config).InsertUpdateCustomReport(customReportData, isUpdate: false))
			{
				return true;
			}
			return false;
		}

		public bool UpdateCustomReport(CustomReportData customReportData)
		{
			return UpdateCustomReport(customReportData, checkConcurrency: true);
		}

		public bool UpdateCustomReport(CustomReportData customReportData, bool checkConcurrency)
		{
			return new CustomReports(config).InsertUpdateCustomReport(customReportData, isUpdate: true);
		}

		public CustomReportData GetCustomReports()
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReports();
			}
		}

		public bool DeleteCustomReport(string customReportID)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.DeleteCustomReport(customReportID);
			}
		}

		public DataSet GetCustomReportsByFields(params string[] columns)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportsByFields(columns);
			}
		}

		public DataSet GetCustomReportsByFields(string[] customReportsID, params string[] columns)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportsByFields(customReportsID, columns);
			}
		}

		public DataSet GetCustomReportsByFields(string[] customReportsID, bool isInactive, params string[] columns)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportsByFields(customReportsID, isInactive, columns);
			}
		}

		public CustomReportData GetCustomReportByID(string customReportID)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportByID(customReportID);
			}
		}

		public bool ExistCustomReport(string customReportName)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.ExistCustomReport(customReportName);
			}
		}

		public bool CreateUpdateCustomReportBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Custom_Report"), "CustomReport Data must exist.", 0);
			CustomReports customReports = new CustomReports(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Custom_Report"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Custom_Report"].Columns.Contains("CustomReportName"))
					{
						text2 = row["CustomReportName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						CustomReportData customReportData = new CustomReportData();
						DataRow dataRow2 = customReportData.CustomReportTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Custom_Report"].Columns)
						{
							try
							{
								if (customReportData.CustomReportTable.Columns.Contains(column.ColumnName))
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
						customReportData.CustomReportTable.Rows.Add(dataRow2);
						if (customReports.ExistCustomReport(text2))
						{
							flag = true;
							text = customReports.GetCustomReportIDByName(text2);
							customReportData.CustomReportTable.AcceptChanges();
							dataRow2["CustomReportID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateCustomReport(customReportData);
								}
							}
							else if (flag2)
							{
								UpdateCustomReport(customReportData, checkConcurrency);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						customReportData.Dispose();
						customReportData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				customReports.Dispose();
				customReports = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}

		public DataSet GetCustomReportComboList()
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportComboList();
			}
		}

		public DataSet GetCustomReportList()
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportList();
			}
		}

		public DataSet GetTableSchema(string query)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetTableSchema(query);
			}
		}

		public DataSet GetCustomData(string reportID)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomData(reportID);
			}
		}

		public DataSet GetCustomReportData(string reportID, string[] parameters, string[] parameterValues)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.GetCustomReportData(reportID, parameters, parameterValues);
			}
		}

		public bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight)
		{
			using (CustomReports customReports = new CustomReports(config))
			{
				return customReports.SaveLayout(reportID, layout, formWidth, formHeight);
			}
		}
	}
}
