using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VehicleDocumentSystem : MarshalByRefObject, IVehicleDocumentSystem, IDisposable
	{
		private Config config;

		public VehicleDocumentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVehicleDocument(VehicleDocumentData data)
		{
			return new VehicleDocuments(config).InsertVehicleDocument(data);
		}

		public bool UpdateVehicleDocument(VehicleDocumentData data)
		{
			return UpdateVehicleDocument(data, checkConcurrency: false);
		}

		public bool UpdateVehicleDocument(VehicleDocumentData data, bool checkConcurrency)
		{
			return new VehicleDocuments(config).UpdateVehicleDocument(data);
		}

		public VehicleDocumentData GetVehicleDocument()
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocument();
			}
		}

		public bool DeleteVehicleDocument(string groupID)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.DeleteVehicleDocument(groupID);
			}
		}

		public VehicleDocumentData GetVehicleDocumentByID(string id)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentByID(id);
			}
		}

		public VehicleDocumentData GetVehicleDocumentsByVehicleID(string employeeID)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentsByVehicleID(employeeID);
			}
		}

		public DataSet GetVehicleDocumentByFields(params string[] columns)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentByFields(columns);
			}
		}

		public DataSet GetVehicleDocumentByFields(string[] ids, params string[] columns)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentByFields(ids, columns);
			}
		}

		public DataSet GetVehicleDocumentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetVehicleDocumentList()
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentList();
			}
		}

		public DataSet GetVehicleDocumentComboList()
		{
			using (VehicleDocuments vehicleDocuments = new VehicleDocuments(config))
			{
				return vehicleDocuments.GetVehicleDocumentComboList();
			}
		}
	}
}
