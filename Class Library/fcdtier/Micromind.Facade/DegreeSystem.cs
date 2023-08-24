using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DegreeSystem : MarshalByRefObject, IDegreeSystem, IDisposable
	{
		private Config config;

		public DegreeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDegree(DegreeData data)
		{
			return new Degree(config).InsertDegree(data);
		}

		public bool UpdateDegree(DegreeData data)
		{
			return UpdateDegree(data, checkConcurrency: false);
		}

		public bool UpdateDegree(DegreeData data, bool checkConcurrency)
		{
			return new Degree(config).UpdateDegree(data);
		}

		public DegreeData GetDegree()
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegree();
			}
		}

		public bool DeleteDegree(string groupID)
		{
			using (Degree degree = new Degree(config))
			{
				return degree.DeleteDegree(groupID);
			}
		}

		public DegreeData GetDegreeByID(string id)
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeByID(id);
			}
		}

		public DataSet GetDegreeByFields(params string[] columns)
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeByFields(columns);
			}
		}

		public DataSet GetDegreeByFields(string[] ids, params string[] columns)
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeByFields(ids, columns);
			}
		}

		public DataSet GetDegreeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDegreeList()
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeList();
			}
		}

		public DataSet GetDegreeComboList()
		{
			using (Degree degree = new Degree(config))
			{
				return degree.GetDegreeComboList();
			}
		}
	}
}
