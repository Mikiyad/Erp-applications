using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BinSystem : MarshalByRefObject, IBinSystem, IDisposable
	{
		private Config config;

		public BinSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBin(BinData data)
		{
			return new Bin(config).InsertBin(data);
		}

		public bool UpdateBin(BinData data)
		{
			return UpdateBin(data, checkConcurrency: false);
		}

		public bool UpdateBin(BinData data, bool checkConcurrency)
		{
			return new Bin(config).UpdateBin(data);
		}

		public BinData GetBin()
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetBin();
			}
		}

		public bool DeleteBin(string groupID)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.DeleteBin(groupID);
			}
		}

		public BinData GetBinByID(string id)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetBinByID(id);
			}
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetJobTaskByFields(columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, params string[] columns)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetJobTaskByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetJobTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBinList(bool isInactive)
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetBinList(isInactive);
			}
		}

		public DataSet GetBinComboList()
		{
			using (Bin bin = new Bin(config))
			{
				return bin.GetBinComboList();
			}
		}
	}
}
