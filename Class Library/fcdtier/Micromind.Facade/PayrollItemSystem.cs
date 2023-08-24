using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class PayrollItemSystem : MarshalByRefObject, IPayrollItemSystem, IDisposable
	{
		private Config config;

		public PayrollItemSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePayrollItem(PayrollItemData data)
		{
			return new PayrollItem(config).InsertPayrollItem(data);
		}

		public bool UpdatePayrollItem(PayrollItemData data)
		{
			return UpdatePayrollItem(data, checkConcurrency: false);
		}

		public bool UpdatePayrollItem(PayrollItemData data, bool checkConcurrency)
		{
			return new PayrollItem(config).UpdatePayrollItem(data);
		}

		public PayrollItemData GetPayrollItem()
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItem();
			}
		}

		public bool DeletePayrollItem(string groupID)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.DeletePayrollItem(groupID);
			}
		}

		public PayrollItemData GetPayrollItemByID(string id)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemByID(id);
			}
		}

		public DataSet GetPayrollItemByFields(params string[] columns)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemByFields(columns);
			}
		}

		public DataSet GetPayrollItemByFields(string[] ids, params string[] columns)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemByFields(ids, columns);
			}
		}

		public DataSet GetPayrollItemByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPayrollItemList()
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemList();
			}
		}

		public DataSet GetPayrollDeductionList()
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollDeductionList();
			}
		}

		public DataSet GetPayrollItemListforWeb()
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemListforWeb();
			}
		}

		public DataSet GetPayrollItemComboList()
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollItemComboList();
			}
		}

		public DataSet GetPayrollProfileReport(string fromPayrollItem, string toPayrollItem, bool showInactive)
		{
			using (PayrollItem payrollItem = new PayrollItem(config))
			{
				return payrollItem.GetPayrollProfileReport(fromPayrollItem, toPayrollItem, showInactive);
			}
		}
	}
}
