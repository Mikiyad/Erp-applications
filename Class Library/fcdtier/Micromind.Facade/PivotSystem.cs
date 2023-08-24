using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PivotSystem : MarshalByRefObject, IPivotSystem, IDisposable
	{
		private Config config;

		public PivotSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool InsertUpdatePivot(PivotData data, bool isUpdate)
		{
			return new Pivot(config).InsertUpdatePivot(data, isUpdate);
		}

		public PivotData GetPivot()
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivot();
			}
		}

		public bool DeletePivot(string groupID)
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.DeletePivot(groupID);
			}
		}

		public PivotData GetPivotByID(string id)
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotByID(id);
			}
		}

		public DataSet GetPivotByFields(params string[] columns)
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotByFields(columns);
			}
		}

		public DataSet GetPivotByFields(string[] ids, params string[] columns)
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotByFields(ids, columns);
			}
		}

		public DataSet GetPivotByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPivotList()
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotList();
			}
		}

		public DataSet GetPivotComboList()
		{
			using (Pivot pivot = new Pivot(config))
			{
				return pivot.GetPivotComboList();
			}
		}
	}
}
