using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DisciplineActionTypeSystem : MarshalByRefObject, IDisciplineActionTypeSystem, IDisposable
	{
		private Config config;

		public DisciplineActionTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateActionType(DisciplineActionTypeData data)
		{
			return new DisciplineActionType(config).InsertActionType(data);
		}

		public bool UpdateActionType(DisciplineActionTypeData data)
		{
			return UpdateActionType(data, checkConcurrency: false);
		}

		public bool UpdateActionType(DisciplineActionTypeData data, bool checkConcurrency)
		{
			return new DisciplineActionType(config).UpdateActionType(data);
		}

		public DisciplineActionTypeData GetActionType()
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionType();
			}
		}

		public bool DeleteActionType(string groupID)
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.DeleteActionType(groupID);
			}
		}

		public DisciplineActionTypeData GetActionTypeByID(string id)
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeByID(id);
			}
		}

		public DataSet GetActionTypeByFields(params string[] columns)
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeByFields(columns);
			}
		}

		public DataSet GetActionTypeByFields(string[] ids, params string[] columns)
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeByFields(ids, columns);
			}
		}

		public DataSet GetActionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetActionTypeList()
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeList();
			}
		}

		public DataSet GetActionTypeComboList()
		{
			using (DisciplineActionType disciplineActionType = new DisciplineActionType(config))
			{
				return disciplineActionType.GetActionTypeComboList();
			}
		}
	}
}
