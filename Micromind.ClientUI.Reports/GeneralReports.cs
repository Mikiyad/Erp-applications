using DevExpress.XtraReports.UI;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using System.Data;

namespace Micromind.ClientUI.Reports
{
	public class GeneralReports
	{
		public void ShowAccountListReport()
		{
			DataSet data = Factory.CompanyAccountSystem.GetAccountListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Accounts";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Chart of Accounts");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "'Chart of Accounts.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowOpenSalesOrderListReport()
		{
			DataSet data = Factory.SalesOrderSystem.GetOpenOrderListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Open Sales Orders";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Open SO");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Open SO.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowOpenPurchaseOrderListReport()
		{
			DataSet data = Factory.PurchaseOrderSystem.GetOpenOrderListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Open Purchase Orders";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Open PO");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Open SO.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowBillOfLadingListReport()
		{
			DataSet data = Factory.BillOfLadingSystem.GetBillOfLadingListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "Bill Of Lading List";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Bill Of Lading Report");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Bill Of Lading Report.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowOpenPurchaseOrderNonInvListReport()
		{
			DataSet data = Factory.PurchaseOrderNISystem.GetOpenOrderListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Open Purchase Orders Non Inventory";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Open PO NI");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Open PO NI.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowOpenPurchaseOrderNonInvSubContractListReport()
		{
			DataSet data = Factory.ProjectSubContractSystem.GetOpenOrderListReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Open Purchase Orders SubContract";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("SubContract Order");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'SubContract Order.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public void ShowOpenShipmentsListReport()
		{
			DataSet data = Factory.POShipmentSystem.GetOpenShipmentsReport();
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "All Open Shipments";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			XtraReport report = reportHelper.GetReport("Open Shipments");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Open Shipments.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}
	}
}
