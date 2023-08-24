using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ContainerSizeSystem : MarshalByRefObject, IContainerSizeSystem, IDisposable
	{
		private Config config;

		public ContainerSizeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateContainerSize(ContainerSizeData data)
		{
			return new ContainerSize(config).InsertContainerSize(data);
		}

		public bool UpdateContainerSize(ContainerSizeData data)
		{
			return UpdateContainerSize(data, checkConcurrency: false);
		}

		public bool UpdateContainerSize(ContainerSizeData data, bool checkConcurrency)
		{
			return new ContainerSize(config).UpdateContainerSize(data);
		}

		public ContainerSizeData GetContainerSize()
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSize();
			}
		}

		public bool DeleteContainerSize(string groupID)
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.DeleteContainerSize(groupID);
			}
		}

		public ContainerSizeData GetContainerSizeByID(string id)
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeByID(id);
			}
		}

		public DataSet GetContainerSizeByFields(params string[] columns)
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeByFields(columns);
			}
		}

		public DataSet GetContainerSizeByFields(string[] ids, params string[] columns)
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeByFields(ids, columns);
			}
		}

		public DataSet GetContainerSizeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetContainerSizeList()
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeList();
			}
		}

		public DataSet GetContainerSizeComboList()
		{
			using (ContainerSize containerSize = new ContainerSize(config))
			{
				return containerSize.GetContainerSizeComboList();
			}
		}
	}
}
