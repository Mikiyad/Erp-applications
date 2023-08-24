using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAssemblyBuildSystem
	{
		bool CreateAssemblyBuild(AssemblyBuildData assemblyBuildData, bool isUpdate);

		AssemblyBuildData GetAssemblyBuildByID(string sysDocID, string voucherID);

		DataSet GetAssemblyBuildToPrint(string sysDocID, string[] voucherID);

		DataSet GetAssemblyBuildToPrint(string sysDocID, string voucherID);

		bool DeleteAssemblyBuild(string sysDocID, string voucherID);

		DataSet GetAssemblyBuildReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromJob, string toJob, string fromCostCategory, string toCostCategory, DateTime asOfDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetFactoryQty(string itemCode);
	}
}
