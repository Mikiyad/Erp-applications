using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PropertyServiceSystem : MarshalByRefObject, IPropertyServiceSystem, IDisposable
	{
		private Config config;

		public PropertyServiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePropertyService(PropertyServiceData data)
		{
			return new PropertyService(config).InsertPropertyService(data);
		}

		public bool UpdatePropertyService(PropertyServiceData data)
		{
			return UpdatePropertyService(data, checkConcurrency: false);
		}

		public bool CreatePropertyServiceAssign(PropertyServiceData data)
		{
			return new PropertyService(config).InsertPropertyServiceAssign(data);
		}

		public bool UpdatePropertyServiceAssign(PropertyServiceData data)
		{
			return UpdatePropertyServiceAssign(data, checkConcurrency: false);
		}

		public bool UpdatePropertyService(PropertyServiceData data, bool checkConcurrency)
		{
			return new PropertyService(config).UpdatePropertyService(data);
		}

		public bool UpdatePropertyServiceAssign(PropertyServiceData data, bool checkConcurrency)
		{
			return new PropertyService(config).UpdatePropertyServiceAssign(data);
		}

		public PropertyServiceData GetPropertyService()
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyService();
			}
		}

		public bool DeletePropertyService(string voucherID)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.DeletePropertyServiceRequest(voucherID);
			}
		}

		public bool DeletePropertyServiceAssign(string voucherID)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.DeletePropertyServiceAssign(voucherID);
			}
		}

		public PropertyServiceData GetPropertyServiceByID(string id)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceByID(id);
			}
		}

		public PropertyServiceData GetPropertyServiceAssignByID(string id)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceAssignByID(id);
			}
		}

		public DataSet GetPropertyServiceByFields(params string[] columns)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceByFields(columns);
			}
		}

		public DataSet GetPropertyServiceByFields(string[] ids, params string[] columns)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceByFields(ids, columns);
			}
		}

		public DataSet GetPropertyServiceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPropertyServiceList(bool includeClosedTasks)
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceList(includeClosedTasks);
			}
		}

		public DataSet GetPropertyServiceComboList()
		{
			using (PropertyService propertyService = new PropertyService(config))
			{
				return propertyService.GetPropertyServiceComboList();
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			return new PropertyService(config).GetList(from, to, showVoid, SysDocID);
		}

		public DataSet GetPropertyServiceAssignList(DateTime from, DateTime to, bool showVoid, string SysDocID)
		{
			return new PropertyService(config).GetPropertyServiceAssignList(from, to, showVoid, SysDocID);
		}

		public DataSet GetServiceRequestList(string SysDocID)
		{
			return new PropertyService(config).GetServiceRequestList(SysDocID);
		}

		public DataSet GetPropertyServiceRequestToPrint(string sysDocID, string voucherID)
		{
			return new PropertyService(config).GetPropertyServiceRequestToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPropertyServiceAssignToPrint(string sysDocID, string voucherID)
		{
			return new PropertyService(config).GetPropertyServiceAssignToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetTenantByUnit(string unitID, DateTime reportingdate)
		{
			return new PropertyService(config).GetTenantByUnit(unitID, reportingdate);
		}
	}
}
