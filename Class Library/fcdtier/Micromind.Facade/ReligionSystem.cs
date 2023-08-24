using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ReligionSystem : MarshalByRefObject, IReligionSystem, IDisposable
	{
		private Config config;

		public ReligionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateReligion(ReligionData data)
		{
			return new Religion(config).InsertReligion(data);
		}

		public bool UpdateReligion(ReligionData data)
		{
			return UpdateReligion(data, checkConcurrency: false);
		}

		public bool UpdateReligion(ReligionData data, bool checkConcurrency)
		{
			return new Religion(config).UpdateReligion(data);
		}

		public ReligionData GetReligion()
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligion();
			}
		}

		public bool DeleteReligion(string groupID)
		{
			using (Religion religion = new Religion(config))
			{
				return religion.DeleteReligion(groupID);
			}
		}

		public ReligionData GetReligionByID(string id)
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionByID(id);
			}
		}

		public DataSet GetReligionByFields(params string[] columns)
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionByFields(columns);
			}
		}

		public DataSet GetReligionByFields(string[] ids, params string[] columns)
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionByFields(ids, columns);
			}
		}

		public DataSet GetReligionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetReligionList()
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionList();
			}
		}

		public DataSet GetReligionComboList()
		{
			using (Religion religion = new Religion(config))
			{
				return religion.GetReligionComboList();
			}
		}
	}
}
