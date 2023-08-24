using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CheckListSystem : MarshalByRefObject, ICheckListSystem, IDisposable
	{
		private Config config;

		public CheckListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCheckList(CheckListData data)
		{
			return new CheckList(config).InsertUpdateCheckList(data, isUpdate: false);
		}

		public bool UpdateCheckList(CheckListData data)
		{
			return UpdateCheckList(data, checkConcurrency: false);
		}

		public bool UpdateCheckList(CheckListData data, bool checkConcurrency)
		{
			return new CheckList(config).InsertUpdateCheckList(data, isUpdate: true);
		}

		public CheckListData GetCheckList()
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckList();
			}
		}

		public bool DeleteCheckList(CheckListTypes checkListType, string checkListID)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.DeleteCheckList(checkListType, checkListID);
			}
		}

		public CheckListData GetCheckListByID(CheckListTypes checkListType, string id)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListByID(checkListType, id);
			}
		}

		public DataSet GetCheckListByFields(params string[] columns)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListByFields(columns);
			}
		}

		public DataSet GetCheckListByFields(string[] ids, params string[] columns)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListByFields(ids, columns);
			}
		}

		public DataSet GetCheckListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCheckListList()
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListList();
			}
		}

		public DataSet GetCheckListComboList(CheckListTypes checkListType)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListComboList(checkListType);
			}
		}

		public bool ApproveTask(int taskID, string tableName, string idColumnName)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.ApproveRejectTask(taskID, isCompleted: true, tableName, idColumnName);
			}
		}

		public bool RejectTask(int taskID, string tableName, string idColumnName)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.ApproveRejectTask(taskID, isCompleted: false, tableName, idColumnName);
			}
		}

		public DataSet GetUserCheckListsWithPendingTasks(CheckListTypes checkListType)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetUserCheckListsWithPendingTasks(checkListType);
			}
		}

		public DataSet GetUserPendingCheckListTasks(CheckListTypes checkListType, string checkListID)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetUserPendingCheckListTasks(checkListType, checkListID);
			}
		}

		public byte GetCheckListTaskStatusByID(int taskID)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetCheckListTaskStatusByID(taskID);
			}
		}

		public bool CloseTask(string taskID, string remarks)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.CloseTask(taskID, remarks);
			}
		}

		public DataSet GetTransactionCheckListDetail(SysDocTypes sysDocType, string sysDocID, string voucherID)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetDocumentCheckListDetail(1, (int)sysDocType, voucherID, sysDocID);
			}
		}

		public DataSet GetCardCheckListDetail(DataComboType cardType, string cardID)
		{
			using (CheckList checkList = new CheckList(config))
			{
				return checkList.GetDocumentCheckListDetail(2, (int)cardType, cardID, "");
			}
		}

		public DoubleString GetTableName(int objectType, int objectID)
		{
			switch (objectType)
			{
			case 1:
				switch (objectID)
				{
				case 31:
					return new DoubleString("Purchase_Order", "VoucherID");
				case 30:
					return new DoubleString("Purchase_Quote", "VoucherID");
				case 4:
				case 5:
				case 64:
					return new DoubleString("GL_Transaction", "VoucherID");
				case 23:
				case 52:
					return new DoubleString("Sales_Order", "VoucherID");
				case 22:
					return new DoubleString("Sales_Quote", "VoucherID");
				case 43:
					return new DoubleString("SalarySheet", "VoucherID");
				case 85:
					return new DoubleString("OverTimeEntry", "VoucherID");
				default:
					throw new Exception("type not implemented");
				}
			case 2:
				switch (objectID)
				{
				case 1:
					return new DoubleString("Customer", "CustomerID");
				case 2:
					return new DoubleString("Vendor", "VendorID");
				case 18:
					return new DoubleString("Employee", "EmployeeID");
				case 95:
					return new DoubleString("Job", "JobID");
				case 4:
					return new DoubleString("Account", "AccountID");
				case 23:
					return new DoubleString("Product", "ProductID");
				}
				break;
			}
			throw new Exception("Type not implemented.");
		}
	}
}
