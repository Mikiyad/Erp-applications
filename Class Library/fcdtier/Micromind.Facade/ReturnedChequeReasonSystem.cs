using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ReturnedChequeReasonSystem : MarshalByRefObject, IReturnedChequeReasonSystem, IDisposable
	{
		private Config config;

		public ReturnedChequeReasonSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateReturnedChequeReason(ReturnedChequeReasonData data)
		{
			return new ReturnedChequeReason(config).InsertReturnedChequeReason(data);
		}

		public bool UpdateReturnedChequeReason(ReturnedChequeReasonData data)
		{
			return UpdateReturnedChequeReason(data, checkConcurrency: false);
		}

		public bool UpdateReturnedChequeReason(ReturnedChequeReasonData data, bool checkConcurrency)
		{
			return new ReturnedChequeReason(config).UpdateReturnedChequeReason(data);
		}

		public ReturnedChequeReasonData GetReturnedChequeReason()
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReason();
			}
		}

		public bool DeleteReturnedChequeReason(string groupID)
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.DeleteReturnedChequeReason(groupID);
			}
		}

		public ReturnedChequeReasonData GetReturnedChequeReasonByID(string id)
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonByID(id);
			}
		}

		public DataSet GetReturnedChequeReasonByFields(params string[] columns)
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonByFields(columns);
			}
		}

		public DataSet GetReturnedChequeReasonByFields(string[] ids, params string[] columns)
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonByFields(ids, columns);
			}
		}

		public DataSet GetReturnedChequeReasonByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetReturnedChequeReasonList()
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonList();
			}
		}

		public DataSet GetReturnedChequeReasonComboList()
		{
			using (ReturnedChequeReason returnedChequeReason = new ReturnedChequeReason(config))
			{
				return returnedChequeReason.GetReturnedChequeReasonComboList();
			}
		}
	}
}
