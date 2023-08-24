using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BouncedCheckSystem : MarshalByRefObject, IBouncedCheckSystem, IDisposable
	{
		private Config config;

		public BouncedCheckSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool InsertBouncedCheck(BouncedCheckData bouncedCheckData)
		{
			return new BouncedChecks(config).InsertBouncedCheck(bouncedCheckData, null);
		}

		public bool UpdateBouncedCheck(BouncedCheckData bouncedCheckData)
		{
			return new BouncedChecks(config).UpdateBouncedCheck(bouncedCheckData);
		}

		public DataSet GetBouncedChecks()
		{
			using (BouncedChecks bouncedChecks = new BouncedChecks(config))
			{
				return bouncedChecks.GetBouncedChecks();
			}
		}

		public DataSet GetBouncedChecks(int partnerID)
		{
			using (BouncedChecks bouncedChecks = new BouncedChecks(config))
			{
				return bouncedChecks.GetBouncedChecks(partnerID);
			}
		}

		public DataSet GetBouncedChecks(DateTime from, DateTime to)
		{
			using (BouncedChecks bouncedChecks = new BouncedChecks(config))
			{
				return bouncedChecks.GetBouncedChecks(from, to);
			}
		}

		public DataSet GetBouncedChecks(int partnerID, DateTime from, DateTime to)
		{
			using (BouncedChecks bouncedChecks = new BouncedChecks(config))
			{
				return bouncedChecks.GetBouncedChecks(partnerID, from, to);
			}
		}

		public DataSet GetBouncedChecks(int[] partnersID, DateTime from, DateTime to)
		{
			using (BouncedChecks bouncedChecks = new BouncedChecks(config))
			{
				return bouncedChecks.GetBouncedChecks(partnersID, from, to);
			}
		}
	}
}
