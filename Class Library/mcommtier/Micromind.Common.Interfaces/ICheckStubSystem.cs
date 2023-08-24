using Micromind.Common.Data;
using System;

namespace Micromind.Common.Interfaces
{
	public interface ICheckStubSystem
	{
		int CreateCheckStub(CheckStubData checkStubData);

		bool UpdateCheckStub(CheckStubData checkStubData);

		bool UpdateCheckStub(CheckStubData checkStubData, bool checkConcurrency);

		CheckStubData GetCheckStubs();

		bool DeleteCheckStub(int checkStubID);

		CheckStubData GetCheckStubsByFields(params string[] columns);

		CheckStubData GetCheckStubsByFields(int[] checkStubsID, params string[] columns);

		CheckStubData GetCheckStubsByID(int[] checkStubID);

		CheckStubData GetCheckStubsByPayee(int[] payeeID);

		CheckStubData GetCheckStubsByPayee(int[] payeeID, DateTime from, DateTime to);
	}
}
