using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PositionSystem : MarshalByRefObject, IPositionSystem, IDisposable
	{
		private Config config;

		public PositionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePosition(PositionData data)
		{
			return new Position(config).InsertPosition(data);
		}

		public bool UpdatePosition(PositionData data)
		{
			return UpdatePosition(data, checkConcurrency: false);
		}

		public bool UpdatePosition(PositionData data, bool checkConcurrency)
		{
			return new Position(config).UpdatePosition(data);
		}

		public PositionData GetPosition()
		{
			using (Position position = new Position(config))
			{
				return position.GetPosition();
			}
		}

		public bool DeletePosition(string groupID)
		{
			using (Position position = new Position(config))
			{
				return position.DeletePosition(groupID);
			}
		}

		public PositionData GetPositionByID(string id)
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionByID(id);
			}
		}

		public DataSet GetPositionByFields(params string[] columns)
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionByFields(columns);
			}
		}

		public DataSet GetPositionByFields(string[] ids, params string[] columns)
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionByFields(ids, columns);
			}
		}

		public DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPositionList()
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionList();
			}
		}

		public DataSet GetPositionComboList()
		{
			using (Position position = new Position(config))
			{
				return position.GetPositionComboList();
			}
		}
	}
}
