using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Printing;
using Micromind.Common.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public static class PrintHelper
	{
		private static Dictionary<string, string> DocsPrintTemplates = new Dictionary<string, string>();

		private static UltraPrintPreviewDialog previewDialog = new UltraPrintPreviewDialog();

		private static string printTemplatePath = "";

		private static string serverPath = "";

		public static string PrintTemplatePath
		{
			get
			{
				if (serverPath == "")
				{
					serverPath = Factory.DatabaseSystem.GetServerPath();
				}
				CompanyInformationData companyInformation = Global.CompanyInformation;
				int num = 1;
				string text = "\\Print Templates";
				if (companyInformation.CompanyInformationTable.Rows[0]["TemplatePathLocation"] != DBNull.Value)
				{
					num = int.Parse(companyInformation.CompanyInformationTable.Rows[0]["TemplatePathLocation"].ToString());
				}
				if (companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"] != DBNull.Value)
				{
					text = companyInformation.CompanyInformationTable.Rows[0]["TemplatePathFolder"].ToString();
				}
				string text2 = "";
				text2 = ((num != 1) ? serverPath : Path.GetDirectoryName(Application.ExecutablePath));
				if (text != "")
				{
					return text2 + text;
				}
				return text2 + "\\Print Templates";
			}
		}

		public static void Reset()
		{
			DocsPrintTemplates = new Dictionary<string, string>();
		}

		public static void PreviewDocument(PrintDocument doc, string documentName)
		{
			doc.DocumentName = documentName;
			previewDialog.Document = doc;
			previewDialog.ShowDialog();
		}

		public static void PreviewDocument(UltraPrintDocument doc, string documentName)
		{
			doc.DocumentName = documentName;
			previewDialog.Document = doc;
			previewDialog.ShowDialog();
		}

		public static void PreviewDocument(UltraGridPrintDocument doc, string documentName)
		{
			doc.DocumentName = documentName;
			doc.Header.TextCenter = documentName;
			doc.Header.TextLeft = "Printed by: " + Global.CurrentUser + "\nPrint Date: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
			previewDialog.Document = doc;
			previewDialog.ShowDialog();
		}

		public static void PrintDocument(DataSet data, string templateName, SysDocTypes docType)
		{
			PrintDocument(data, "", templateName, docType, isPrint: false, showPrintDialog: false);
		}

		public static Image GetTransactionPreviewImage(DataSet data, string sysDocID, string templateName, SysDocTypes docType)
		{
			XtraReport printDocument = GetPrintDocument(data, templateName, sysDocID, docType);
			ImageExportOptions options = new ImageExportOptions();
			MemoryStream stream = new MemoryStream();
			printDocument.ExportToImage(stream, options);
			return Image.FromStream(stream);
		}

		public static Image GetTransactionPreviewImage(XtraReport printDocument)
		{
			ImageExportOptions options = new ImageExportOptions();
			MemoryStream stream = new MemoryStream();
			printDocument.ExportToImage(stream, options);
			return Image.FromStream(stream);
		}

		public static MemoryStream GetTransactionPreviewPDF(XtraReport printDocument)
		{
			PdfExportOptions options = new PdfExportOptions();
			MemoryStream memoryStream = new MemoryStream();
			printDocument.ExportToPdf(memoryStream, options);
			return memoryStream;
		}

		public static MemoryStream GetTransactionPreviewPDF(DataSet data, string sysDocID, string templateName, SysDocTypes docType)
		{
			XtraReport printDocument = GetPrintDocument(data, templateName, sysDocID, docType);
			PdfExportOptions options = new PdfExportOptions();
			MemoryStream memoryStream = new MemoryStream();
			printDocument.ExportToPdf(memoryStream, options);
			return memoryStream;
		}

		public static XtraReport GetPrintDocument(DataSet data, string defaultTemplateName, string sysDocID, SysDocTypes docType)
		{
			try
			{
				new ReportHelper();
				AddGeneralPrintData(ref data, docType);
				XtraReport printTemplate = GetPrintTemplate(defaultTemplateName);
				if (printTemplate == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file. Please make sure you have access to print template path and the files are not corrupted.", "'" + defaultTemplateName + ".repx'");
					return null;
				}
				if (printTemplate != null)
				{
					if (docType == SysDocTypes.PaymentRequest)
					{
						printTemplate.DefaultPrinterSettingsUsing.UsePaperKind = false;
					}
					else
					{
						printTemplate.DefaultPrinterSettingsUsing.UsePaperKind = true;
					}
					if (sysDocID != "")
					{
						if (DocsPrintTemplates.ContainsKey(sysDocID))
						{
							defaultTemplateName = DocsPrintTemplates[sysDocID];
						}
						else
						{
							object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "PrintTemplateName", "SysDocID", sysDocID);
							if (fieldValue != null && fieldValue.ToString() != "")
							{
								defaultTemplateName = fieldValue.ToString();
								DocsPrintTemplates.Add(sysDocID, defaultTemplateName);
							}
							if (fieldValue == "" || fieldValue.ToString() == "")
							{
								DocsPrintTemplates.Add(sysDocID, defaultTemplateName);
							}
						}
					}
				}
				printTemplate.DataSource = data;
				return printTemplate;
			}
			catch
			{
				throw;
			}
		}

		public static void PrintDocument(DataSet data, string sysDocID, string defaultTemplateName, SysDocTypes docType, bool isPrint, bool showPrintDialog)
		{
			new ReportHelper();
			AddGeneralPrintData(ref data, docType);
			XtraReport printTemplate = GetPrintTemplate(defaultTemplateName);
			if (printTemplate == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file. Please make sure you have access to print template path and the files are not corrupted.", "'" + defaultTemplateName + ".repx'");
				return;
			}
			printTemplate.ShowPrintStatusDialog = false;
			printTemplate.PrintingSystem.ExportOptions.SetOptionsVisibility(new ExportOptionKind[5]
			{
				ExportOptionKind.PdfDocumentApplication,
				ExportOptionKind.PdfDocumentAuthor,
				ExportOptionKind.PdfDocumentKeywords,
				ExportOptionKind.PdfDocumentSubject,
				ExportOptionKind.PdfDocumentTitle
			}, visible: false);
			if (printTemplate != null)
			{
				if (docType == SysDocTypes.PaymentRequest)
				{
					printTemplate.DefaultPrinterSettingsUsing.UsePaperKind = false;
				}
				else
				{
					printTemplate.DefaultPrinterSettingsUsing.UsePaperKind = true;
				}
				if (sysDocID != "")
				{
					if (DocsPrintTemplates.ContainsKey(sysDocID))
					{
						defaultTemplateName = DocsPrintTemplates[sysDocID];
					}
					else
					{
						object fieldValue = Factory.DatabaseSystem.GetFieldValue("System_Document", "PrintTemplateName", "SysDocID", sysDocID);
						if (fieldValue != null && fieldValue.ToString() != "")
						{
							defaultTemplateName = fieldValue.ToString();
							DocsPrintTemplates.Add(sysDocID, defaultTemplateName);
						}
					}
				}
			}
			printTemplate.DataSource = data;
			if (!isPrint)
			{
				PrintPreviewForm printPreviewForm = new PrintPreviewForm();
				printPreviewForm.Document = printTemplate;
				if (showPrintDialog)
				{
					printPreviewForm.ShowDialog();
				}
				else
				{
					printPreviewForm.Show();
				}
			}
			else if (showPrintDialog)
			{
				printTemplate.PrintDialog();
			}
			else
			{
				printTemplate.Print();
			}
		}

		public static void PrintCheque(DataSet data, string templateName, bool isPrint, bool showPrintDialog)
		{
			new ReportHelper();
			string text = PrintTemplatePath + "\\Cheques\\" + templateName;
			XtraReport xtraReport = new XtraReport();
			xtraReport.LoadLayout(text);
			xtraReport.Tag = text;
			xtraReport.ShowDesignerHints = true;
			if (xtraReport == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the cheuqe template file. Please make sure you have access to print template path and the file is not corrupted.");
				return;
			}
			xtraReport.DataSource = data;
			if (!isPrint)
			{
				PrintPreviewForm printPreviewForm = new PrintPreviewForm();
				printPreviewForm.Document = xtraReport;
				printPreviewForm.ShowDialog();
			}
			else if (showPrintDialog)
			{
				xtraReport.PrintDialog();
			}
			else
			{
				xtraReport.Print();
			}
		}

		public static void AddGeneralPrintData(ref DataSet data, SysDocTypes docType)
		{
			if (data == null || data.Tables.Contains("DocInfo"))
			{
				return;
			}
			data.Tables.Add("DocInfo");
			if (data.Tables.Count <= 0)
			{
				return;
			}
			data.Tables["DocInfo"].Columns.Add("CompanyName");
			data.Tables["DocInfo"].Columns.Add("UserID");
			data.Tables["DocInfo"].Columns.Add("UserName");
			data.Tables["DocInfo"].Columns.Add("Logo", typeof(Image));
			data.Tables["DocInfo"].Columns.Add("IsPrintOnLetterHead");
			data.Tables["DocInfo"].Columns.Add("ApprovedStatus");
			data.Tables["DocInfo"].Columns.Add("TRN");
			string text = "";
			DataSet entityApprovalStatus = Factory.EntityDocSystem.GetEntityApprovalStatus(data, docType, Global.CurrentUser, includeApproveUser: true);
			if (entityApprovalStatus.Tables.Count > 0)
			{
				DataRow[] source = entityApprovalStatus.Tables[0].Select("ApproverID <> '123abc'");
				DataRow dataRow = source.FirstOrDefault((DataRow s) => s["ApproverID"].ToString() != "123abc");
				if (dataRow == null)
				{
					dataRow = source.FirstOrDefault((DataRow s) => s["ApproverID"].ToString() != "123abc");
				}
				if (dataRow != null)
				{
					text = dataRow["APPROVAL_STATUS"].ToString();
				}
			}
			object fieldValue = Factory.DatabaseSystem.GetFieldValue("Users", "UserName", "UserID", Global.CurrentUser);
			object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Company", "Notes", "CompanyID", "1");
			data.Tables["DocInfo"].Rows.Add(Global.CompanyName, Global.CurrentUser, fieldValue.ToString(), Global.CompanyLogo, UserPreferences.PrintDocumentOnLetterHead, text, fieldValue2.ToString().Trim());
		}

		public static XtraReport GetPrintTemplate(string fileName)
		{
			try
			{
				XtraReport xtraReport = new XtraReport();
				xtraReport.LoadLayout(PrintTemplatePath + "\\Documents\\" + fileName + ".repx");
				xtraReport.Tag = PrintTemplatePath + "\\Documents\\" + fileName + ".repx";
				return xtraReport;
			}
			catch
			{
				return null;
			}
		}
	}
}
