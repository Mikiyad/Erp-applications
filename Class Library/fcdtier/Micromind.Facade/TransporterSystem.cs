using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TransporterSystem : MarshalByRefObject, ITransporterSystem, IDisposable
	{
		private Config config;

		public TransporterSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTransporter(TransporterData data)
		{
			return new Transporter(config).InsertTransporter(data);
		}

		public bool UpdateTransporter(TransporterData data)
		{
			return UpdateTransporter(data, checkConcurrency: false);
		}

		public bool UpdateTransporter(TransporterData data, bool checkConcurrency)
		{
			return new Transporter(config).UpdateTransporter(data);
		}

		public TransporterData GetTransporter()
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporter();
			}
		}

		public bool DeleteTransporter(string groupID)
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.DeleteTransporter(groupID);
			}
		}

		public TransporterData GetTransporterByID(string id)
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterByID(id);
			}
		}

		public DataSet GetTransporterByFields(params string[] columns)
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterByFields(columns);
			}
		}

		public DataSet GetTransporterByFields(string[] ids, params string[] columns)
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterByFields(ids, columns);
			}
		}

		public DataSet GetTransporterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTransporterList()
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterList();
			}
		}

		public DataSet GetTransporterComboList()
		{
			using (Transporter transporter = new Transporter(config))
			{
				return transporter.GetTransporterComboList();
			}
		}
	}
}
