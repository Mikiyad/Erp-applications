using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBouncedCheckSystem
	{
		bool InsertBouncedCheck(BouncedCheckData bouncedCheckData);

		bool UpdateBouncedCheck(BouncedCheckData bouncedCheckData);

		DataSet GetBouncedChecks();

		DataSet GetBouncedChecks(int partnerID);

		DataSet GetBouncedChecks(DateTime from, DateTime to);

		DataSet GetBouncedChecks(int partnerID, DateTime from, DateTime to);

		DataSet GetBouncedChecks(int[] partnersID, DateTime from, DateTime to);
	}
}
