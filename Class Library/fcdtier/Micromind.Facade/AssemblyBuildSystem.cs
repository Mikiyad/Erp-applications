using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class AssemblyBuildSystem : MarshalByRefObject, IAssemblyBuildSystem, IDisposable
	{
		private Config config;

		public AssemblyBuildSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAssemblyBuild(AssemblyBuildData data, bool isUpdate)
		{
			return new AssemblyBuild(config).InsertUpdateAssemblyBuild(data, isUpdate);
		}

		public AssemblyBuildData GetAssemblyBuildByID(string sysDocID, string voucherID)
		{
			return new AssemblyBuild(config).GetAssemblyBuildByID(sysDocID, voucherID);
		}

		public bool DeleteAssemblyBuild(string sysDocID, string voucherID)
		{
			return new AssemblyBuild(config).DeleteAssemblyBuild(sysDocID, voucherID);
		}

		public DataSet GetAssemblyBuildToPrint(string sysDocID, string[] voucherID)
		{
			return new AssemblyBuild(config).GetAssemblyBuildToPrint(sysDocID, voucherID);
		}

		public DataSet GetAssemblyBuildToPrint(string sysDocID, string voucherID)
		{
			return new AssemblyBuild(config).GetAssemblyBuildToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetAssemblyBuildReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromJob, string toJob, string fromCostCategory, string toCostCategory, DateTime asOfDate, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new AssemblyBuild(config).GetAssemblyBuildReport(fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromJob, toJob, fromCostCategory, toCostCategory, asOfDate, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new AssemblyBuild(config).GetAssemblyBuildList(fromDate, toDate, Isvoid: true);
		}

		public DataSet GetFactoryQty(string itemCode)
		{
			return new AssemblyBuild(config).GetFactoryQty(itemCode);
		}
	}
}
