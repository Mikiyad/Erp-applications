using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalespersonGroupSystem : MarshalByRefObject, ISalespersonGroupSystem, IDisposable
	{
		private Config config;

		public SalespersonGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalespersonGroup(SalespersonGroupData data)
		{
			return new SalespersonGroup(config).InsertSalespersonGroup(data);
		}

		public bool UpdateSalespersonGroup(SalespersonGroupData data)
		{
			return UpdateSalespersonGroup(data, checkConcurrency: false);
		}

		public bool UpdateSalespersonGroup(SalespersonGroupData data, bool checkConcurrency)
		{
			return new SalespersonGroup(config).UpdateSalespersonGroup(data);
		}

		public SalespersonGroupData GetSalespersonGroup()
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroup();
			}
		}

		public bool DeleteSalespersonGroup(string groupID)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.DeleteSalespersonGroup(groupID);
			}
		}

		public SalespersonGroupData GetSalespersonGroupByID(string id)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupByID(id);
			}
		}

		public DataSet GetSalespersonGroupByFields(params string[] columns)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupByFields(columns);
			}
		}

		public DataSet GetSalespersonGroupByFields(string[] ids, params string[] columns)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupByFields(ids, columns);
			}
		}

		public DataSet GetSalespersonGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSalespersonGroupList()
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupList();
			}
		}

		public DataSet GetSalespersonAssignedGroupsList(string customerID)
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonAssignedGroupsList(customerID);
			}
		}

		public DataSet GetSalespersonGroupComboList()
		{
			using (SalespersonGroup salespersonGroup = new SalespersonGroup(config))
			{
				return salespersonGroup.GetSalespersonGroupComboList();
			}
		}
	}
}
