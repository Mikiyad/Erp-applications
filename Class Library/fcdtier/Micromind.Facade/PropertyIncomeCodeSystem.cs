using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyIncomeCodeSystem : MarshalByRefObject, IPropertyIncomeCodeSystem, IDisposable
	{
		private Config config;

		public PropertyIncomeCodeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyIncomeCode(PropertyIncomeCodeData data)
		{
			return new PropertyIncomeCode(config).InsertPropertyIncomeCode(data);
		}

		public bool UpdatePropertyIncomeCode(PropertyIncomeCodeData data)
		{
			return UpdatePropertyIncomeCode(data, checkConcurrency: false);
		}

		public bool UpdatePropertyIncomeCode(PropertyIncomeCodeData data, bool checkConcurrency)
		{
			return new PropertyIncomeCode(config).UpdatePropertyIncomeCode(data);
		}

		public PropertyIncomeCodeData GetPropertyIncomeCode()
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCode();
			}
		}

		public bool DeletePropertyIncomeCode(string groupID)
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.DeletePropertyIncomeCode(groupID);
			}
		}

		public PropertyIncomeCodeData GetPropertyIncomeCodeByID(string id)
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeByID(id);
			}
		}

		public DataSet GetPropertyIncomeCodeByFields(params string[] columns)
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeByFields(columns);
			}
		}

		public DataSet GetPropertyIncomeCodeByFields(string[] ids, params string[] columns)
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeByFields(ids, columns);
			}
		}

		public DataSet GetPropertyIncomeCodeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyIncomeCodeList()
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeList();
			}
		}

		public DataSet GetPropertyIncomeCodeComboList()
		{
			using (PropertyIncomeCode propertyIncomeCode = new PropertyIncomeCode(config))
			{
				return propertyIncomeCode.GetPropertyIncomeCodeComboList();
			}
		}
	}
}
