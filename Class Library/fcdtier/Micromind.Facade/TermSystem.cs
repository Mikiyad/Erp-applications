using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Text;

namespace Micromind.Facade
{
	public sealed class TermSystem : MarshalByRefObject, ITermSystem, IDisposable
	{
		private Config config;

		public TermSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTerm(PaymentTermData termData)
		{
			return new Terms(config).InsertUpdateTerm(termData, isUpdate: false);
		}

		public bool UpdateTerm(PaymentTermData termData)
		{
			return new Terms(config).InsertUpdateTerm(termData, isUpdate: true);
		}

		public PaymentTermData GetTermByID(string termID)
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetTermByID(termID);
			}
		}

		public PaymentTermData GetTerms()
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetTerms();
			}
		}

		public PaymentTermData GetTermsByFields(params string[] columns)
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetTermsByFields(columns);
			}
		}

		public PaymentTermData GetTermsByFields(int[] termsID, params string[] columns)
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetTermsByFields(termsID, columns);
			}
		}

		public bool DeleteTerm(string termID)
		{
			using (Terms terms = new Terms(config))
			{
				return terms.DeleteTerm(termID);
			}
		}

		public bool ExistTermName(string termName)
		{
			using (Terms terms = new Terms(config))
			{
				return terms.ExistTermName(termName);
			}
		}

		public DataSet GetPaymentTermsList()
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetPaymentTermsList();
			}
		}

		public DataSet GetPaymentTermsComboList()
		{
			using (Terms terms = new Terms(config))
			{
				return terms.GetPaymentTermsComboList();
			}
		}

		public bool CreateUpdateTermsBatch(DataSet listData, bool checkConcurrency)
		{
			ApplicationAssert.CheckCondition(listData != null, "listData Parameter cannot be null.", 0);
			ApplicationAssert.CheckCondition(listData.Tables.Contains("Payment_Term"), "Terms Data must exist.", 0);
			Terms terms = new Terms(config);
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				foreach (DataRow row in listData.Tables["Payment_Term"].Rows)
				{
					bool flag = false;
					string text = "-1";
					string text2 = string.Empty;
					if (listData.Tables["Payment_Term"].Columns.Contains("TermName"))
					{
						text2 = row["TermName"].ToString();
					}
					if (!(text2 == string.Empty))
					{
						bool flag2 = true;
						bool flag3 = true;
						PaymentTermData paymentTermData = new PaymentTermData();
						DataRow dataRow2 = paymentTermData.TermTable.NewRow();
						foreach (DataColumn column in listData.Tables["Payment_Term"].Columns)
						{
							try
							{
								if (paymentTermData.TermTable.Columns.Contains(column.ColumnName))
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
						paymentTermData.TermTable.Rows.Add(dataRow2);
						if (terms.ExistTermName(text2))
						{
							flag = true;
							text = terms.GetTermIDByName(text2);
							paymentTermData.TermTable.AcceptChanges();
							dataRow2["PaymentTermID"] = text;
						}
						try
						{
							if (!flag)
							{
								if (flag3)
								{
									CreateTerm(paymentTermData);
								}
							}
							else if (flag2)
							{
								UpdateTerm(paymentTermData);
							}
						}
						catch (Exception ex)
						{
							stringBuilder.Append(ex.Message).Append("\n");
						}
						paymentTermData.Dispose();
						paymentTermData = null;
					}
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				terms.Dispose();
				terms = null;
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
