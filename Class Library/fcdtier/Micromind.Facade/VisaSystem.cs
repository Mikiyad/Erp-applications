using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VisaSystem : MarshalByRefObject, IVisaSystem, IDisposable
	{
		private Config config;

		public VisaSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVisa(VisaData data)
		{
			return new Visa(config).InsertVisa(data);
		}

		public bool UpdateVisa(VisaData data)
		{
			return UpdateVisa(data, checkConcurrency: false);
		}

		public bool UpdateVisa(VisaData data, bool checkConcurrency)
		{
			return new Visa(config).UpdateVisa(data);
		}

		public VisaData GetVisa()
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisa();
			}
		}

		public bool DeleteVisa(string groupID)
		{
			using (Visa visa = new Visa(config))
			{
				return visa.DeleteVisa(groupID);
			}
		}

		public VisaData GetVisaByID(string id)
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaByID(id);
			}
		}

		public DataSet GetVisaByFields(params string[] columns)
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaByFields(columns);
			}
		}

		public DataSet GetVisaByFields(string[] ids, params string[] columns)
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaByFields(ids, columns);
			}
		}

		public DataSet GetVisaByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVisaList()
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaList();
			}
		}

		public DataSet GetVisaComboList()
		{
			using (Visa visa = new Visa(config))
			{
				return visa.GetVisaComboList();
			}
		}
	}
}
