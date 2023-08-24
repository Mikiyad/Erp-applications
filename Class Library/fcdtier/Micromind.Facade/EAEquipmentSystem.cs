using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EAEquipmentSystem : MarshalByRefObject, IEAEquipmentSystem, IDisposable
	{
		private Config config;

		public EAEquipmentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipment(EAEquipmentData data)
		{
			return new EAEquipment(config).InsertEquipment(data);
		}

		public bool UpdateEquipment(EAEquipmentData data)
		{
			return UpdateEquipment(data, checkConcurrency: false);
		}

		public bool UpdateEquipment(EAEquipmentData data, bool checkConcurrency)
		{
			return new EAEquipment(config).UpdateEquipment(data);
		}

		public EAEquipmentData GetEquipment()
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipment();
			}
		}

		public bool DeleteEquipment(string groupID)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.DeleteEquipment(groupID);
			}
		}

		public EAEquipmentData GetEquipmentByID(string id)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentByID(id);
			}
		}

		public DataSet GetEquipmentByCategoryID(string id)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentByCategoryID(id);
			}
		}

		public DataSet GetEquipmentByFields(params string[] columns)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentByFields(columns);
			}
		}

		public DataSet GetEquipmentByFields(string[] ids, params string[] columns)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentByFields(ids, columns);
			}
		}

		public DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEquipmentList()
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentList();
			}
		}

		public DataSet GetEquipmentComboList()
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentComboList();
			}
		}

		public DataSet GetEquipmentReport(string fromEquipment, string toEquipment, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentReport(fromEquipment, toEquipment, fromClass, toClass, fromGroup, toGroup, showInactive);
			}
		}

		public DataSet GetEquipmentListReport(string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, bool showInactive)
		{
			using (EAEquipment eAEquipment = new EAEquipment(config))
			{
				return eAEquipment.GetEquipmentListReport(fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob, showInactive);
			}
		}

		public DataSet GetEquipmentFlowReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string sysDocID, string VoucherID)
		{
			return new EAEquipment(config).GetEquipmentFlowReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob, sysDocID, VoucherID);
		}
	}
}
