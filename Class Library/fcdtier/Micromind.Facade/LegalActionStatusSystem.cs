using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LegalActionStatusSystem : MarshalByRefObject, ILegalActionStatusSystem, IDisposable
	{
		private Config config;

		public LegalActionStatusSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLegalActionStatus(LegalActionStatusData data)
		{
			return new LegalActionStatus(config).InsertLegalActionStatus(data);
		}

		public bool UpdateLegalActionStatus(LegalActionStatusData data)
		{
			return UpdateLegalActionStatus(data, checkConcurrency: false);
		}

		public bool UpdateLegalActionStatus(LegalActionStatusData data, bool checkConcurrency)
		{
			return new LegalActionStatus(config).UpdateLegalActionStatus(data);
		}

		public LegalActionStatusData GetLegalActionStatus()
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatus();
			}
		}

		public bool DeleteLegalActionStatus(string groupID)
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.DeleteLegalActionStatus(groupID);
			}
		}

		public LegalActionStatusData GetLegalActionStatusByID(string id)
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusByID(id);
			}
		}

		public DataSet GetLegalActionStatusByFields(params string[] columns)
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusByFields(columns);
			}
		}

		public DataSet GetLegalActionStatusByFields(string[] ids, params string[] columns)
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusByFields(ids, columns);
			}
		}

		public DataSet GetLegalActionStatusByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetLegalActionStatusList()
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusList();
			}
		}

		public DataSet GetLegalActionStatusComboList()
		{
			using (LegalActionStatus legalActionStatus = new LegalActionStatus(config))
			{
				return legalActionStatus.GetLegalActionStatusComboList();
			}
		}
	}
}
