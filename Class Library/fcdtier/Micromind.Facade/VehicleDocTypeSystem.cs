using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VehicleDocTypeSystem : MarshalByRefObject, IVehicleDocTypeSystem, IDisposable
	{
		private Config config;

		public VehicleDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVehicleDocType(VehicleDocTypeData data)
		{
			return new VehicleDocType(config).InsertVehicleDocType(data);
		}

		public bool UpdateVehicleDocType(VehicleDocTypeData data)
		{
			return UpdateVehicleDocType(data, checkConcurrency: false);
		}

		public bool UpdateVehicleDocType(VehicleDocTypeData data, bool checkConcurrency)
		{
			return new VehicleDocType(config).UpdateVehicleDocType(data);
		}

		public VehicleDocTypeData GetVehicleDocType()
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocType();
			}
		}

		public bool DeleteVehicleDocType(string groupID)
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.DeleteVehicleDocType(groupID);
			}
		}

		public VehicleDocTypeData GetVehicleDocTypeByID(string id)
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeByID(id);
			}
		}

		public DataSet GetVehicleDocTypeByFields(params string[] columns)
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeByFields(columns);
			}
		}

		public DataSet GetVehicleDocTypeByFields(string[] ids, params string[] columns)
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetVehicleDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVehicleDocTypeList()
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeList();
			}
		}

		public DataSet GetVehicleDocTypeComboList()
		{
			using (VehicleDocType vehicleDocType = new VehicleDocType(config))
			{
				return vehicleDocType.GetVehicleDocTypeComboList();
			}
		}
	}
}
