using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataCaches.Libraries;
using System;
using System.Data;
using System.Text;

namespace Micromind.DataCaches.Others
{
	public sealed class PrintTemplates
	{
		private static PrintTemplateData printTemplates;

		private static object syncRoot;

		private static DateTime dateTimeStamp;

		public static event Delegates.RefereshHandler Refereshed;

		private PrintTemplates()
		{
		}

		internal static void Reset()
		{
			dateTimeStamp = DateTime.MinValue;
			if (printTemplates != null)
			{
				printTemplates.Dispose();
				printTemplates = null;
			}
		}

		public static DataRow[] GetPrintTemplates(PrintTemplateTypes[] types, bool isReferesh)
		{
			if (Micromind.ClientLibraries.Global.ConStatus == ConnectionStatus.Connected)
			{
				if ((printTemplates == null) | isReferesh)
				{
					lock (syncRoot)
					{
						if ((printTemplates == null) | isReferesh)
						{
							if (printTemplates != null && isReferesh)
							{
								DateTime tableLastDateTimeStamp = Factory.DatabaseSystem.GetTableLastDateTimeStamp("[Print Templates]");
								if (tableLastDateTimeStamp > dateTimeStamp)
								{
									SetData();
									dateTimeStamp = tableLastDateTimeStamp;
								}
							}
							else
							{
								SetData();
							}
						}
					}
				}
			}
			else if (printTemplates == null)
			{
				throw new DBNotConnectedException();
			}
			return GetPrintTemplatesByType(types);
		}

		private static DataRow[] GetPrintTemplatesByType(PrintTemplateTypes[] types)
		{
			if (printTemplates == null)
			{
				return null;
			}
			if (types == null || types.Length == 0)
			{
				return printTemplates.PrintTemplatesTable.Select("", "Name");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("DocumentType").Append(" IN(");
			for (int i = 0; i < types.Length; i++)
			{
				stringBuilder.Append((byte)types[i]).Append(",");
			}
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
			stringBuilder.Append(") ");
			return printTemplates.PrintTemplatesTable.Select(stringBuilder.ToString(), "Name");
		}

		public static void ForceReferesh()
		{
			SetData();
		}

		private static void SetData()
		{
			if (printTemplates != null)
			{
				printTemplates.Dispose();
				printTemplates = null;
			}
			printTemplates = Factory.PrintTemplateSystem.GetPrintTemplates(new PrintTemplateTypes[0], readOnly: true);
			OnRefereshed();
		}

		private static void OnRefereshed()
		{
			if (PrintTemplates.Refereshed != null)
			{
				PrintTemplates.Refereshed();
			}
		}

		static PrintTemplates()
		{
			PrintTemplates.Refereshed = null;
			printTemplates = null;
			syncRoot = new object();
			dateTimeStamp = DateTime.MinValue;
		}
	}
}
