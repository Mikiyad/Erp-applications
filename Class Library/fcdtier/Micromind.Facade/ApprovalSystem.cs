using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ApprovalSystem : MarshalByRefObject, IApprovalSystem, IDisposable
	{
		private Config config;

		public ApprovalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateApproval(ApprovalData data)
		{
			return new Approval(config).InsertUpdateApproval(data, isUpdate: false);
		}

		public bool UpdateApproval(ApprovalData data)
		{
			return UpdateApproval(data, checkConcurrency: false);
		}

		public bool UpdateApproval(ApprovalData data, bool checkConcurrency)
		{
			return new Approval(config).InsertUpdateApproval(data, isUpdate: true);
		}

		public ApprovalData GetApproval()
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApproval();
			}
		}

		public bool DeleteApproval(ApprovalTypes approvalType, string approvalID)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.DeleteApproval(approvalType, approvalID);
			}
		}

		public ApprovalData GetApprovalByID(ApprovalTypes approvalType, string id)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalByID(approvalType, id);
			}
		}

		public DataSet GetApprovalByFields(params string[] columns)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalByFields(columns);
			}
		}

		public DataSet GetApprovalByFields(string[] ids, params string[] columns)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalByFields(ids, columns);
			}
		}

		public DataSet GetApprovalByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetApprovalList()
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalList();
			}
		}

		public DataSet GetVerificationList()
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetVerificationList();
			}
		}

		public DataSet GetApprovalComboList(ApprovalTypes approvalType)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalComboList(approvalType);
			}
		}

		public bool ApproveTask(int taskID, string tableName, string idColumnName)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.ApproveRejectTask(taskID, isApproved: true, tableName, idColumnName);
			}
		}

		public bool ApproveTaskVerification(int taskID, string tableName, string idColumnName)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.ApproveRejectTaskVerification(taskID, isApproved: true, tableName, idColumnName);
			}
		}

		public bool RejectTask(int taskID, string tableName, string idColumnName)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.ApproveRejectTask(taskID, isApproved: false, tableName, idColumnName);
			}
		}

		public DataSet GetUserApprovalsWithPendingTasks(ApprovalTypes approvalType)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetUserApprovalsWithPendingTasks(approvalType);
			}
		}

		public DataSet GetUserPendingApprovalTasks(ApprovalTypes approvalType, string approvalID)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetUserPendingApprovalTasks(approvalType, approvalID);
			}
		}

		public byte GetApprovalTaskStatusByID(int taskID)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetApprovalTaskStatusByID(taskID);
			}
		}

		public DataSet GetTransactionApprovalDetail(SysDocTypes sysDocType, string sysDocID, string voucherID)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetDocumentApprovalDetail(1, (int)sysDocType, voucherID, sysDocID);
			}
		}

		public DataSet GetCardApprovalDetail(DataComboType cardType, string cardID)
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetDocumentApprovalDetail(2, (int)cardType, cardID, "");
			}
		}

		public int GetUserPendingTasksCount()
		{
			using (Approval approval = new Approval(config))
			{
				return approval.GetUserPendingTasksCount();
			}
		}

		public DoubleString GetTableName(int objectType, int objectID)
		{
			switch (objectType)
			{
			case 1:
			{
				SysDocTypes sysDocTypes = (SysDocTypes)objectID;
				switch (sysDocTypes)
				{
				case SysDocTypes.PurchaseOrder:
				case SysDocTypes.ImportPurchaseOrder:
					return new DoubleString("Purchase_Order", "VoucherID");
				case SysDocTypes.PurchaseQuote:
				case SysDocTypes.ProformaInvoice:
					return new DoubleString("Purchase_Quote", "VoucherID");
				case SysDocTypes.PurchaseOrderNI:
					return new DoubleString("Purchase_Order_NonInv", "VoucherID");
				case SysDocTypes.PurchaseCostEntry:
					return new DoubleString("Purchase_Cost_Entry", "VoucherID");
				case SysDocTypes.PurchaseInvoice:
					return new DoubleString("Purchase_Invoice", "VoucherID");
				case SysDocTypes.ImportPurchaseInvoice:
					return new DoubleString("Purchase_Invoice", "VoucherID");
				case SysDocTypes.CashPurchase:
					return new DoubleString("Purchase_Invoice", "VoucherID");
				case SysDocTypes.PurchaseInvoiceNI:
					return new DoubleString("Purchase_Invoice_NonInv", "VoucherID");
				case SysDocTypes.GoodsReceivedNote:
				case SysDocTypes.ImportGoodsReceivedNote:
					return new DoubleString("Purchase_Receipt", "VoucherID");
				case SysDocTypes.PackingList:
					return new DoubleString("PO_Shipment", "VoucherID");
				case SysDocTypes.SalesInvoice:
				case SysDocTypes.SalesReceipt:
				case SysDocTypes.ExportSalesInvoice:
					return new DoubleString("Sales_Invoice", "VoucherID");
				case SysDocTypes.ConsignIn:
					return new DoubleString("Consign_In", "VoucherID");
				case SysDocTypes.ConsignInReturn:
					return new DoubleString("ConsignIn_Return", "VoucherID");
				case SysDocTypes.ConsignInSettlement:
					return new DoubleString("ConsignIn_Settlement", "VoucherID");
				case SysDocTypes.ConsignOut:
					return new DoubleString("Consign_Out", "VoucherID");
				case SysDocTypes.ConsignOutReturn:
					return new DoubleString("ConsignOut_Return", "VoucherID");
				case SysDocTypes.ConsignOutSettlement:
					return new DoubleString("ConsignOut_Settlement", "VoucherID");
				case SysDocTypes.DeliveryNote:
				case SysDocTypes.ExportDeliveryNote:
					return new DoubleString("Delivery_Note", "VoucherID");
				case SysDocTypes.ChequeReceipt:
				case SysDocTypes.CashPayment:
				case SysDocTypes.ChequePayment:
				case SysDocTypes.FundTransfer:
				case SysDocTypes.CashExpense:
				case SysDocTypes.TTPayment:
				case SysDocTypes.TTReceipt:
				case SysDocTypes.CashReceiptMultiple:
					return new DoubleString("GL_Transaction", "VoucherID");
				case SysDocTypes.CashReceipt:
					return new DoubleString("GL_Transaction", "VoucherID");
				case SysDocTypes.SalesEnquiry:
					return new DoubleString("Sales_Enquiry", "VoucherID");
				case SysDocTypes.SalesOrder:
				case SysDocTypes.ExportSalesOrder:
					return new DoubleString("Sales_Order", "VoucherID");
				case SysDocTypes.SalesQuote:
					return new DoubleString("Sales_Quote", "VoucherID");
				case SysDocTypes.SalarySheet:
					return new DoubleString("SalarySheet", "VoucherID");
				case SysDocTypes.OverTimeEntry:
					return new DoubleString("OverTimeEntry", "VoucherID");
				case SysDocTypes.GJournal:
					return new DoubleString("Journal", "VoucherID");
				case SysDocTypes.CreditSalesReturn:
					return new DoubleString("Sales_Return", "VoucherID");
				case SysDocTypes.FixedAssetTransfer:
					return new DoubleString("FixedAsset_Transfer", "VoucherID");
				case SysDocTypes.JobMaterialRequisition:
					return new DoubleString("Job_Material_Requisition", "VoucherID");
				case SysDocTypes.EmployeeAppraisal:
					return new DoubleString("Employee_Appraisal", "VoucherID");
				case SysDocTypes.EmployeeGeneralActivity:
					return new DoubleString("Employee_GeneralActivity", "VoucherID");
				case SysDocTypes.CLVoucher:
					return new DoubleString("CL_Voucher", "VoucherID");
				case SysDocTypes.CreditLimitReview:
					return new DoubleString("Credit_Limit_Review", "VoucherID");
				case SysDocTypes.PurchasePrepaymentInvoice:
					return new DoubleString("Purchase_PrePayment_Invoice", "VoucherID");
				case SysDocTypes.InventoryNoneSale:
					return new DoubleString("Inventory_Damage", "VoucherID");
				case SysDocTypes.InventoryAdjustment:
					return new DoubleString("Inventory_Adjustment", "VoucherID");
				case SysDocTypes.InventoryRepacking:
					return new DoubleString("Inventory_Repacking", "VoucherID");
				case SysDocTypes.TransitTransferIn:
					return new DoubleString("Inventory_Transfer", "VoucherID");
				case SysDocTypes.ReturnTransitTransfer:
					return new DoubleString("Inventory_Transfer", "VoucherID");
				case SysDocTypes.TransitTransferOut:
					return new DoubleString("Inventory_Transfer", "VoucherID");
				case SysDocTypes.DirectInventoryTransfer:
					return new DoubleString("Inventory_Transfer", "VoucherID");
				case SysDocTypes.ProjectSubContractPO:
					return new DoubleString("Project_Subcontract_PO", "VoucherID");
				case SysDocTypes.ProjectSubContractPI:
					return new DoubleString("Project_SubContract_PI", "VoucherID");
				case SysDocTypes.ServiceCallTrack:
					return new DoubleString("Service_CallTrack", "VoucherID");
				case SysDocTypes.JobEstimation:
					return new DoubleString("Job_Estimation", "VoucherID");
				case SysDocTypes.JobTimesheet:
					return new DoubleString("Job_Timesheet", "VoucherID");
				case SysDocTypes.JobExpenseIssue:
					return new DoubleString("Job_Expense_Issue", "VoucherID");
				case SysDocTypes.JobInventoryIssue:
					return new DoubleString("Job_Inventory_Issue", "VoucherID");
				case SysDocTypes.JobInventoryReturn:
					return new DoubleString("Job_Inventory_Return", "VoucherID");
				case SysDocTypes.JobInvoice:
					return new DoubleString("Job_Invoice", "VoucherID");
				case SysDocTypes.JobClosing:
					return new DoubleString("Job", "VoucherID");
				case SysDocTypes.JobMaintenanceServiceEntry:
					return new DoubleString("Job_Maintenance_Service", "VoucherID");
				case SysDocTypes.SalaryPaymentCash:
				case SysDocTypes.SalaryPaymentCheque:
				case SysDocTypes.SalaryPaymentBank:
					return new DoubleString("Payroll_Transaction", "VoucherID");
				case SysDocTypes.ProjectExpenseAllocation:
					return new DoubleString("Project_Expense_Allocation", "VoucherID");
				case SysDocTypes.LPOReceipt:
					return new DoubleString("LPO_Receipt", "VoucherID");
				case SysDocTypes.TR:
					return new DoubleString("Bank_Facility_Transaction", "VoucherID");
				case SysDocTypes.TRPayment:
					return new DoubleString("Bank_Facility_Payment", "VoucherID");
				case SysDocTypes.TRApplication:
					return new DoubleString("TR_Application", "VoucherID");
				case SysDocTypes.SendChequesToBank:
					return new DoubleString("Cheque_Send", "VoucherID");
				case SysDocTypes.ChequeDeposit:
				case SysDocTypes.ReturnedCheque:
				case SysDocTypes.ReceivedChequeCancellation:
				case SysDocTypes.IssuedChequeClearance:
					return new DoubleString("GL_Transaction", "VoucherID");
				case SysDocTypes.ChequeDiscount:
					return new DoubleString("Cheque_Discount", "VoucherID");
				default:
					throw new Exception("Document type is not implemented in approval function 'GetTableName'. Type:" + sysDocTypes.ToString());
				}
			}
			case 2:
			{
				DataComboType dataComboType = (DataComboType)objectID;
				switch (dataComboType)
				{
				case DataComboType.Customer:
					return new DoubleString("Customer", "CustomerID");
				case DataComboType.Vendor:
					return new DoubleString("Vendor", "VendorID");
				case DataComboType.Employee:
					return new DoubleString("Employee", "EmployeeID");
				case DataComboType.Job:
					return new DoubleString("Job", "JobID");
				case DataComboType.Accounts:
					return new DoubleString("Account", "AccountID");
				case DataComboType.Product:
					return new DoubleString("Product", "ProductID");
				case DataComboType.JobBOM:
					return new DoubleString("Job_BOM", "JobBOMID");
				default:
					throw new Exception("Card type is not implemented in approval function 'GetTableName'. Type:" + dataComboType.ToString());
				}
			}
			default:
				throw new Exception("Object type is not implemented in approval function 'GetTableName'.");
			}
		}
	}
}
