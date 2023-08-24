using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ReleaseTypeSystem : MarshalByRefObject, IReleaseTypeSystem, IDisposable
	{
		private Config config;

		public ReleaseTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateReleaseType(ReleaseTypeData data)
		{
			return new ReleaseTypes(config).InsertReleaseType(data);
		}

		public bool UpdateReleaseType(ReleaseTypeData data)
		{
			return UpdateReleaseType(data, checkConcurrency: false);
		}

		public bool UpdateReleaseType(ReleaseTypeData data, bool checkConcurrency)
		{
			return new ReleaseTypes(config).UpdateReleaseType(data);
		}

		public ReleaseTypeData GetReleaseType()
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseType();
			}
		}

		public bool DeleteReleaseType(string groupID)
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.DeleteReleaseType(groupID);
			}
		}

		public ReleaseTypeData GetReleaseTypeByID(string id)
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeByID(id);
			}
		}

		public DataSet GetReleaseTypeByFields(params string[] columns)
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeByFields(columns);
			}
		}

		public DataSet GetReleaseTypeByFields(string[] ids, params string[] columns)
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeByFields(ids, columns);
			}
		}

		public DataSet GetReleaseTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetReleaseTypeList()
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeList();
			}
		}

		public DataSet GetReleaseTypeComboList()
		{
			using (ReleaseTypes releaseTypes = new ReleaseTypes(config))
			{
				return releaseTypes.GetReleaseTypeComboList();
			}
		}
	}
}
