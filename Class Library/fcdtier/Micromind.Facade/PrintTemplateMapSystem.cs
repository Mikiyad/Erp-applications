using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PrintTemplateMapSystem : MarshalByRefObject, IPrintTemplateMapSystem, IDisposable
	{
		private Config config;

		public PrintTemplateMapSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePrintTemplateMap(PrintTemplateMapData data)
		{
			return new PrintTemplateMap(config).InsertPrintTemplateMap(data);
		}

		public bool UpdatePrintTemplateMap(PrintTemplateMapData data)
		{
			return UpdatePrintTemplateMap(data, checkConcurrency: false);
		}

		public bool UpdatePrintTemplateMap(PrintTemplateMapData data, bool checkConcurrency)
		{
			return new PrintTemplateMap(config).UpdatePrintTemplateMap(data);
		}

		public PrintTemplateMapData GetPrintTemplateMap()
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMap();
			}
		}

		public bool DeletePrintTemplateMap(string groupID)
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.DeletePrintTemplateMap(groupID);
			}
		}

		public PrintTemplateMapData GetPrintTemplateMapByID(string id)
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapByID(id);
			}
		}

		public DataSet GetPrintTemplateMapByFields(params string[] columns)
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapByFields(columns);
			}
		}

		public DataSet GetPrintTemplateMapByFields(string[] ids, params string[] columns)
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapByFields(ids, columns);
			}
		}

		public DataSet GetPrintTemplateMapByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPrintTemplateMapList()
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapList();
			}
		}

		public DataSet GetPrintTemplateMapComboList()
		{
			using (PrintTemplateMap printTemplateMap = new PrintTemplateMap(config))
			{
				return printTemplateMap.GetPrintTemplateMapComboList();
			}
		}
	}
}
