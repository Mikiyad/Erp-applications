using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class ShippingMethodSystem : MarshalByRefObject, IShippingMethodSystem, IDisposable
	{
		private Config config;

		public ShippingMethodSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateShippingMethod(string companyName, string phone, string contact)
		{
			ShippingMethodData shippingMethodData = new ShippingMethodData();
			DataTable dataTable = shippingMethodData.Tables["Shipping_Method"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["ShippingMethodName"] = companyName.Trim();
			dataRow["Phone"] = phone;
			dataRow["ContactName"] = contact;
			dataTable.Rows.Add(dataRow);
			return CreateShippingMethod(shippingMethodData);
		}

		public bool CreateShippingMethod(string companyName)
		{
			ShippingMethodData shippingMethodData = new ShippingMethodData();
			DataTable dataTable = shippingMethodData.Tables["Shipping_Method"];
			DataRow dataRow = dataTable.NewRow();
			dataRow["ShippingMethodName"] = companyName.Trim();
			dataTable.Rows.Add(dataRow);
			return CreateShippingMethod(shippingMethodData);
		}

		public bool CreateShippingMethod(ShippingMethodData ShippingMethodData)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.InsertShippingMethod(ShippingMethodData);
			}
		}

		public bool UpdateShippingMethod(ShippingMethodData ShippingMethodData)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.UpdateShiper(ShippingMethodData);
			}
		}

		public ShippingMethodData GetShippingMethods()
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethods();
			}
		}

		public bool DeleteShippingMethod(string ShippingMethodID)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.DeleteShippingMethod(ShippingMethodID);
			}
		}

		public ShippingMethodData GetShippingMethodByID(string ShippingMethodID)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodByID(ShippingMethodID);
			}
		}

		public bool ExistShippingMethod(string companyName)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.ExistShippingMethod(companyName);
			}
		}

		public DataSet GetShippingMethodsByFields(params string[] columns)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodsByFields(columns);
			}
		}

		public DataSet GetShippingMethodsByFields(int[] ShippingMethodsID, params string[] columns)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodsByFields(ShippingMethodsID, columns);
			}
		}

		public DataSet GetShippingMethodsByFields(bool isInactive, int[] ShippingMethodsID, params string[] columns)
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodsByFields(isInactive, ShippingMethodsID, columns);
			}
		}

		public bool CreateUpdateShippingMethodBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Shipping_Method"), "ShippingMethods Data must exist.", 0);
			ShippingMethods shippingMethods = new ShippingMethods(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Shipping_Method"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Shipping_Method"].Columns.Contains("ShippingMethodName"))
					{
						text2 = row["ShippingMethodName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						ShippingMethodData shippingMethodData = new ShippingMethodData();
						DataRow dataRow2 = shippingMethodData.ShippingMethodTable.NewRow();
						bool flag2 = true;
						bool flag3 = true;
						foreach (DataColumn column in listData.Tables["Shipping_Method"].Columns)
						{
							try
							{
								if (shippingMethodData.ShippingMethodTable.Columns.Contains(column.ColumnName))
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
						shippingMethodData.ShippingMethodTable.Rows.Add(dataRow2);
						if (shippingMethods.ExistShippingMethod(text2))
						{
							flag = true;
							text = shippingMethods.GetShippingMethodIDByName(text2);
							shippingMethodData.ShippingMethodTable.AcceptChanges();
							dataRow2["ShippingMethodID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateShippingMethod(shippingMethodData);
								}
							}
							else if (flag2)
							{
								UpdateShippingMethod(shippingMethodData);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						shippingMethodData.Dispose();
						shippingMethodData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				shippingMethods.Dispose();
				shippingMethods = null;
				if (stringBuilder.Length > 0)
				{
					ApplicationLog.WriteImportLogError(stringBuilder);
				}
				stringBuilder = null;
			}
			return true;
		}

		public DataSet GetShippingMethodsList()
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodsList();
			}
		}

		public DataSet GetShippingMethodsComboList()
		{
			using (ShippingMethods shippingMethods = new ShippingMethods(config))
			{
				return shippingMethods.GetShippingMethodsComboList();
			}
		}
	}
}
