using Infragistics.Documents.Excel;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.ExcelExport;
using Micromind.ClientLibraries;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	public class DataExportHelper
	{
		private string reportHeader = "";

		private int totalGridColumns;

		public void BeginExport(object sender, BeginExportEventArgs e)
		{
			IWorksheetCellFormat worksheetCellFormat = e.Workbook.CreateNewWorksheetCellFormat();
			worksheetCellFormat.Font.Bold = ExcelDefaultableBoolean.True;
			worksheetCellFormat.Font.Height = 400;
			checked
			{
				e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex + 2].Value = reportHeader;
				e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex + 2].CellFormat.SetFormatting(worksheetCellFormat);
				e.CurrentWorksheet.MergedCellsRegions.Add(e.CurrentRowIndex, e.CurrentColumnIndex, e.CurrentRowIndex, totalGridColumns);
				e.CurrentRowIndex++;
			}
		}

		public void ExportToExcel(UltraGrid grid)
		{
			UltraGridExcelExporter ultraGridExcelExporter = new UltraGridExcelExporter();
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = "xls";
			saveFileDialog.AddExtension = true;
			saveFileDialog.Filter = "Microsoft Excel|*.xls";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog.FileName;
				ultraGridExcelExporter.Export(grid, fileName);
				ErrorHelper.InformationMessage("Successfully exported to Excel.");
			}
		}

		public void ExportToExcel(UltraGrid grid, string reportHeader)
		{
			UltraGridExcelExporter ultraGridExcelExporter = new UltraGridExcelExporter();
			ultraGridExcelExporter.BeginExport += BeginExport;
			this.reportHeader = reportHeader;
			totalGridColumns = grid.DisplayLayout.Bands[0].Columns.Count;
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = "xls";
			saveFileDialog.AddExtension = true;
			saveFileDialog.Filter = "Microsoft Excel|*.xls";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string fileName = saveFileDialog.FileName;
				ultraGridExcelExporter.Export(grid, fileName);
				ErrorHelper.InformationMessage("Successfully exported to Excel.");
			}
		}
	}
}
