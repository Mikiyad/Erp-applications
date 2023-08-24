using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ChequebookSystem : MarshalByRefObject, IChequebookSystem, IDisposable
	{
		private Config config;

		public ChequebookSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateChequebook(ChequebookData data)
		{
			return new Chequebook(config).InsertChequebook(data);
		}

		public bool UpdateChequebook(ChequebookData data)
		{
			return UpdateChequebook(data, checkConcurrency: false);
		}

		public bool UpdateChequebook(ChequebookData data, bool checkConcurrency)
		{
			return new Chequebook(config).UpdateChequebook(data);
		}

		public ChequebookData GetChequebook()
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebook();
			}
		}

		public bool DeleteChequebook(string groupID)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.DeleteChequebook(groupID);
			}
		}

		public ChequebookData GetChequebookByID(string id)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookByID(id);
			}
		}

		public DataSet GetChequebookByFields(params string[] columns)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookByFields(columns);
			}
		}

		public DataSet GetChequebookByFields(string[] ids, params string[] columns)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookByFields(ids, columns);
			}
		}

		public DataSet GetChequebookByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetChequebookList()
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookList();
			}
		}

		public DataSet GetChequebookComboList()
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookComboList();
			}
		}

		public int ExistChequeNumber(string chequebookID, int from, int to)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.ExistChequeNumber(chequebookID, from, to);
			}
		}

		public bool RegisterCheque(string chequebookID, int from, int to)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.RegisterCheque(chequebookID, from, to);
			}
		}

		public int GetLastChequeNumber(string chequebookID)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetLastChequeNumber(chequebookID);
			}
		}

		public int GetNextChequeNumber(string chequebookID)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetNextChequeNumber(chequebookID);
			}
		}

		public decimal GetChequebookBalance(string chequebookID, bool includeOD)
		{
			using (Chequebook chequebook = new Chequebook(config))
			{
				return chequebook.GetChequebookBalance(chequebookID, includeOD);
			}
		}
	}
}
