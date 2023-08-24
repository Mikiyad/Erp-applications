using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProvisionTypeSystem : MarshalByRefObject, IProvisionTypeSystem, IDisposable
	{
		private Config config;

		public ProvisionTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProvisionType(EmployeeProvisionTypeData data)
		{
			return new ProvisionType(config).InsertProvisionType(data);
		}

		public bool UpdateProvisionType(EmployeeProvisionTypeData data)
		{
			return UpdateProvisionType(data, checkConcurrency: false);
		}

		public bool UpdateProvisionType(EmployeeProvisionTypeData data, bool checkConcurrency)
		{
			return new ProvisionType(config).UpdateProvisionType(data);
		}

		public EmployeeProvisionTypeData GetProvisionType()
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionType();
			}
		}

		public bool DeleteProvisionType(string groupID)
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.DeleteProvisionType(groupID);
			}
		}

		public EmployeeProvisionTypeData GetProvisionTypeByID(string id)
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeByID(id);
			}
		}

		public DataSet GetProvisionTypeByFields(params string[] columns)
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeByFields(columns);
			}
		}

		public DataSet GetProvisionTypeByFields(string[] ids, params string[] columns)
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeByFields(ids, columns);
			}
		}

		public DataSet GetProvisionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetProvisionTypeList()
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeList();
			}
		}

		public DataSet GetProvisionTypeComboList()
		{
			using (ProvisionType provisionType = new ProvisionType(config))
			{
				return provisionType.GetProvisionTypeComboList();
			}
		}
	}
}
