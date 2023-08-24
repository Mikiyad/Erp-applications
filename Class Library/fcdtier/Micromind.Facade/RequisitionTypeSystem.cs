using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RequisitionTypeSystem : MarshalByRefObject, IRequisitionTypeSystem, IDisposable
	{
		private Config config;

		public RequisitionTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRequisitionType(RequisitionTypeData data)
		{
			return new RequisitionType(config).InsertRequisitionType(data);
		}

		public bool UpdateRequisitionType(RequisitionTypeData data)
		{
			return UpdateRequisitionType(data, checkConcurrency: false);
		}

		public bool UpdateRequisitionType(RequisitionTypeData data, bool checkConcurrency)
		{
			return new RequisitionType(config).UpdateRequisitionType(data);
		}

		public RequisitionTypeData GetRequisitionType()
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionType();
			}
		}

		public bool DeleteRequisitionType(string groupID)
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.DeleteRequisitionType(groupID);
			}
		}

		public RequisitionTypeData GetRequisitionTypeByID(string id)
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeByID(id);
			}
		}

		public DataSet GetRequisitionTypeByFields(params string[] columns)
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeByFields(columns);
			}
		}

		public DataSet GetRequisitionTypeByFields(string[] ids, params string[] columns)
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeByFields(ids, columns);
			}
		}

		public DataSet GetRequisitionTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetRequisitionTypeList()
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeList();
			}
		}

		public DataSet GetRequisitionTypeComboList()
		{
			using (RequisitionType requisitionType = new RequisitionType(config))
			{
				return requisitionType.GetRequisitionTypeComboList();
			}
		}
	}
}
