using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ApprovalTransactionTypesComboBox : ComboBox
	{
		private ApprovalTypes approvalType = ApprovalTypes.Approval;

		private Container components;

		public ApprovalTypes ApprovalType
		{
			get
			{
				return approvalType;
			}
			set
			{
				approvalType = value;
			}
		}

		public SysDocTypes SelectedID
		{
			get
			{
				ComboData comboData = base.SelectedItem as ComboData;
				if (comboData != null)
				{
					return (SysDocTypes)int.Parse(comboData.ID);
				}
				return SysDocTypes.None;
			}
			set
			{
				foreach (object item in base.Items)
				{
					ComboData comboData = item as ComboData;
					if (comboData != null)
					{
						string iD = comboData.ID;
						int num = (int)value;
						if (iD == num.ToString())
						{
							base.SelectedItem = item;
							break;
						}
					}
				}
			}
		}

		public ApprovalTransactionTypesComboBox()
		{
			InitializeComponent();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			new System.Resources.ResourceManager(typeof(Micromind.DataControls.AccountTypesComboBox));
			SuspendLayout();
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public void LoadData()
		{
			base.Items.Clear();
			base.Items.Add(new ComboData("Purchase Order", 31.ToString()));
			base.Items.Add(new ComboData("Purchase Quote", 30.ToString()));
			base.Items.Add(new ComboData("Purchase Invoice", 33.ToString()));
			base.Items.Add(new ComboData("Import Purchase Invoice", 39.ToString()));
			base.Items.Add(new ComboData("Packing List", 36.ToString()));
			base.Items.Add(new ComboData("Cash Purchase", 34.ToString()));
			base.Items.Add(new ComboData("Import Purchase Order", 38.ToString()));
			base.Items.Add(new ComboData("Purchase Order NI", 115.ToString()));
			base.Items.Add(new ComboData("Purchase Invoice NI", 116.ToString()));
			base.Items.Add(new ComboData("Goods Received Note", 32.ToString()));
			base.Items.Add(new ComboData("Import Goods Received Note", 50.ToString()));
			base.Items.Add(new ComboData("Import Proforma Invoice", 63.ToString()));
			base.Items.Add(new ComboData("Cash Payment Voucher", 4.ToString()));
			base.Items.Add(new ComboData("Cash Expense", 8.ToString()));
			base.Items.Add(new ComboData("Cash Receipt", 3.ToString()));
			base.Items.Add(new ComboData("TT Payment Voucher", 64.ToString()));
			base.Items.Add(new ComboData("Cheque Payment Voucher", 5.ToString()));
			base.Items.Add(new ComboData("Sales Enquiry", 118.ToString()));
			base.Items.Add(new ComboData("Sales Order", 23.ToString()));
			base.Items.Add(new ComboData("Export Sales Order", 52.ToString()));
			base.Items.Add(new ComboData("Sales Quote", 22.ToString()));
			base.Items.Add(new ComboData("Sales Invoice", 25.ToString()));
			base.Items.Add(new ComboData("Delivery Note", 24.ToString()));
			base.Items.Add(new ComboData("Export Delivery Note", 53.ToString()));
			base.Items.Add(new ComboData("Salary Sheet", 43.ToString()));
			base.Items.Add(new ComboData("Overtime Entry", 85.ToString()));
			base.Items.Add(new ComboData("General Journal Entry", 1.ToString()));
			base.Items.Add(new ComboData("Fixed Asset Transfer ", 59.ToString()));
			base.Items.Add(new ComboData("Job Material Requisition ", 77.ToString()));
			base.Items.Add(new ComboData("Employee General Activity ", 220.ToString()));
			base.Items.Add(new ComboData("Employee Appraisal ", 221.ToString()));
			base.Items.Add(new ComboData("Inventory Adjustment", 18.ToString()));
			base.Items.Add(new ComboData("Inventory NoneSale", 87.ToString()));
			base.Items.Add(new ComboData("Inventory Repacking", 89.ToString()));
			base.Items.Add(new ComboData("CL Voucher", 217.ToString()));
			base.Items.Add(new ComboData("Credit Limit Review", 219.ToString()));
			base.Items.Add(new ComboData("Purchase Prepayment Invoice", 244.ToString()));
			base.Items.Add(new ComboData("SubContract Order", 215.ToString()));
			base.Items.Add(new ComboData("SubContract Invoice", 218.ToString()));
			base.Items.Add(new ComboData("Service Call Track", 120.ToString()));
			base.Items.Add(new ComboData("Estimation", 110.ToString()));
			base.Items.Add(new ComboData("TimeSheet Entry", 76.ToString()));
			base.Items.Add(new ComboData("Project Expense Entry", 73.ToString()));
			base.Items.Add(new ComboData("Inventory Issue", 71.ToString()));
			base.Items.Add(new ComboData("Inventory Return", 72.ToString()));
			base.Items.Add(new ComboData("Project Billing", 74.ToString()));
			base.Items.Add(new ComboData("Project Closing", 81.ToString()));
			base.Items.Add(new ComboData("Service Maintenance Entry", 211.ToString()));
			base.Items.Add(new ComboData("Salary Payment-Bank", 99.ToString()));
			base.Items.Add(new ComboData("Salary Payment-Cash", 69.ToString()));
			base.Items.Add(new ComboData("Salary Payment-Cheque", 70.ToString()));
			base.Items.Add(new ComboData("Project Salary Expense Allocation", 108.ToString()));
			base.Items.Add(new ComboData("Cash Receipt-MultiplePayee", 66.ToString()));
			base.Items.Add(new ComboData("Cheque Receipt", 2.ToString()));
			base.Items.Add(new ComboData("Fund Transfer", 6.ToString()));
			base.Items.Add(new ComboData("Bank Transfer Receipt", 65.ToString()));
			base.Items.Add(new ComboData("LPO Receipt", 94.ToString()));
			base.Items.Add(new ComboData("TR Entry", 79.ToString()));
			base.Items.Add(new ComboData("TR Payment", 80.ToString()));
			base.Items.Add(new ComboData("TR Application", 248.ToString()));
			base.Items.Add(new ComboData("Cheque Clearance", 14.ToString()));
			base.Items.Add(new ComboData("Cheque Security", 17.ToString()));
			base.Items.Add(new ComboData("Cheque SendTo Bank", 83.ToString()));
			base.Items.Add(new ComboData("Cheque Deposit", 7.ToString()));
			base.Items.Add(new ComboData("Cheque Discount", 84.ToString()));
			base.Items.Add(new ComboData("Cheque Return", 12.ToString()));
			base.Items.Add(new ComboData("Cheque Receieved Cancellation", 13.ToString()));
			if (approvalType == ApprovalTypes.Verification)
			{
				base.Items.Add(new ComboData("Export Sales Invoice", 51.ToString()));
				base.Items.Add(new ComboData("Cash Sales Return", 28.ToString()));
				base.Items.Add(new ComboData("Credit Sales Return", 27.ToString()));
				base.Items.Add(new ComboData("Arrival Report", 98.ToString()));
				base.Items.Add(new ComboData("Consign In", 55.ToString()));
				base.Items.Add(new ComboData("Consign In Settlement", 56.ToString()));
				base.Items.Add(new ComboData("Consign Out", 47.ToString()));
				base.Items.Add(new ComboData("Consign Out Settlement", 48.ToString()));
				base.Items.Add(new ComboData("Credit Note", 11.ToString()));
				base.Items.Add(new ComboData("Debit Note", 10.ToString()));
				base.Items.Add(new ComboData("Cheque Bounce", 16.ToString()));
				base.Items.Add(new ComboData("Cheque Cancel", 15.ToString()));
				base.Items.Add(new ComboData("Payment Request Screen", 100.ToString()));
				base.Items.Add(new ComboData("Damage Entry", 87.ToString()));
			}
		}

		public void Clear()
		{
			if (base.Items.Count > 0)
			{
				SelectedIndex = 0;
			}
			else
			{
				SelectedIndex = -1;
			}
		}
	}
}
