using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SmartListSystem : MarshalByRefObject, ISmartListSystem, IDisposable
	{
		private Config config;

		public SmartListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSmartList(SmartListData data)
		{
			return new SmartList(config).InsertSmartList(data);
		}

		public bool UpdateSmartList(SmartListData data)
		{
			return UpdateSmartList(data, checkConcurrency: false);
		}

		public bool UpdateSmartList(SmartListData data, bool checkConcurrency)
		{
			return new SmartList(config).UpdateSmartList(data);
		}

		public SmartListData GetSmartList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartList();
			}
		}

		public bool DeleteSmartList(string groupID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.DeleteSmartList(groupID);
			}
		}

		public bool CreateCategory(string categoryName, int parentID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.CreateCategory(categoryName, parentID);
			}
		}

		public int CreateCategory(string categoryName, string parentID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.CreateCategory(categoryName, parentID);
			}
		}

		public SmartListData GetSmartListByID(string id)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListByID(id);
			}
		}

		public DataSet GetSmartListByFields(params string[] columns)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListByFields(columns);
			}
		}

		public DataSet GetSmartListByFields(string[] ids, params string[] columns)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListByFields(ids, columns);
			}
		}

		public DataSet GetSmartListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSubSmartListComboList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSubSmartListComboList();
			}
		}

		public DataSet GetCategoryList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetCategoryList();
			}
		}

		public DataSet GetCategoryListByID(int Id)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetCategoryListById(Id);
			}
		}

		public DataSet GetSmartListList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListList();
			}
		}

		public DataSet GetSmartListComboList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListComboList();
			}
		}

		public DataSet GetReportByQuery(string query, DateTime fromDate, DateTime toDate)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetReportByQuery(query, fromDate, toDate);
			}
		}

		public bool UpdateCategory(string categoryName, int parentID, int categoryID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.UpdateCategory(categoryName, parentID, categoryID);
			}
		}

		public bool UpdateCategory(string categoryName, int parentID, int categoryID, int rowIndex)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.UpdateCategory(categoryName, parentID, categoryID, rowIndex);
			}
		}

		public bool RenameReport(string reportName, int reportID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.RenameReport(reportName, reportID);
			}
		}

		public bool DeleteCategory(string categoryID)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.DeleteCategory(categoryID);
			}
		}

		public DataSet GetSmartListCategoryComboList()
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListCategoryComboList();
			}
		}

		public byte[] GetSmartListData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.GetSmartListData(reportID, fromDate, toDate, parameters, parameterValues);
			}
		}

		public bool UpdateSmartListIndex(string categoryID, int index)
		{
			using (SmartList smartList = new SmartList(config))
			{
				return smartList.UpdateSmartListIndex(categoryID, index);
			}
		}
	}
}
