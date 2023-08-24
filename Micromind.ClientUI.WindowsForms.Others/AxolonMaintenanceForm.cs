using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.ClientUI.WindowsForms.Others.AxolonMaintenance;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others
{
	public class AxolonMaintenanceForm : Form
	{
		private string str1 = "This function will update the SourceLotNumber column for all items which does not have a source lot assigned to them.\n The function will not do action if any value in sourcelot column already.";

		private DateControl dateControlObj;

		private TransactionTypeComboBox transtypeControlObj;

		private SysDocComboBox sysDocComboObj;

		private TextBox textBoxVoucherNumber;

		private CheckBox checkBoxNone;

		private Form dateFormObj;

		private SysDocTypes doctype = SysDocTypes.None;

		public DateTime fromDate = DateTime.Today;

		public DateTime toDate = DateTime.Today;

		private string tableName = "";

		private bool isStop;

		private IContainer components;

		private MenuStrip menuStrip1;

		private ToolStripMenuItem inventoryToolStripMenuItem;

		private ToolStripMenuItem updateAverageCostToolStripMenuItem;

		private ToolStripMenuItem updateSourceLotNumberProductLotTableToolStripMenuItem;

		private Label label1;

		private ToolStripMenuItem updateProductLotCostToolStripMenuItem;

		private ToolStripMenuItem updateProductLotSalesCostToolStripMenuItem;

		private RichTextBox textBoxLog;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem mergeJournalDetailsForCOGSUpdatesToolStripMenuItem;

		private ProgressBar progressBar1;

		private Label label2;

		private BackgroundWorker backgroundWorker1;

		private Button buttonStop;

		private ToolStripMenuItem mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem;

		private ToolStripMenuItem deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem;

		private ToolStripMenuItem updateSalesCostToolStripMenuItem;

		public string SysDocID
		{
			get
			{
				return sysDocComboObj.SelectedID;
			}
			set
			{
				sysDocComboObj.SelectedID = value;
			}
		}

		public string VoucherIDs
		{
			get
			{
				return textBoxVoucherNumber.Text;
			}
			set
			{
				textBoxVoucherNumber.Text = value;
			}
		}

		public AxolonMaintenanceForm()
		{
			InitializeComponent();
		}

		private void buttonUpdateAvgCost_Click(object sender, EventArgs e)
		{
			new UpdateCOGSForm().ShowDialog();
		}

		private void buttonUpdateSourceLots_Click(object sender, EventArgs e)
		{
		}

		private void updateAverageCostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new UpdateCOGSForm().Show();
		}

		private void updateSourceLotNumberProductLotTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				if (MessageBox.Show(str1 + "\n\nAre you sure you want to update the SourceLotNumbers?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					textBoxLog.AppendText("\nStarting function: Updating SourceLotNumber...");
					textBoxLog.AppendText("\n Updating Delivery Note Returns...");
					text = "UPDATE PL SET SourceLotNumber = \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                            PL6.SourceLotNumber END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PLS.LotNo)\r\n\r\n                            FROM Product_Lot PL \r\n                                INNER JOIN Delivery_Return_Detail DRD ON DRD.SysDocID = PL.DocID AND DRD.VoucherID = PL.ReceiptNumber AND DRD.RowIndex = PL.RowIndex\r\n\t\t\t\t\t\t\t\tINNER JOIN Delivery_Return DR ON DRD.SysDocID = DR.SysDocID AND DRD.VoucherID = DR.VoucherID  \r\n                                INNER JOIN Product_Lot_Sales PLS ON PLS.DocID = DR.DNoteSysDocID AND PLS.InvoiceNumber = DR.DNoteVoucherID AND PLS.ItemCode = DRD.ProductID AND PLS.RowIndex = DRD.DNRowIndex\r\n\t\t\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n\t\t\t\t\t\t\t\t         WHERE  P.ItemType IN (1,7) AND ISNULL(P.IsTrackLot,'False') = 'False'";
					int num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows for Delivery Returns Updated in Product_Lot table Successfully.");
					textBoxLog.AppendText("\n\n Updating Sales Returns...");
					text = "UPDATE PL SET SourceLotNumber =  \r\n\r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                            PL6.SourceLotNumber END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PLS.LotNo)\r\n\r\n                             FROM Product_Lot PL INNER JOIN  Sales_Return_Detail DRD ON DRD.SysDocID = PL.DocID AND DRD.VoucherID = PL.ReceiptNumber  AND DRD.RowIndex = PL.RowIndex\r\n\t\t\t\t\t\t\t\tINNER JOIN Sales_Invoice_Detail SID ON SID.SysDocID = DRD.SourceSysDocID AND SID.VoucherID = DRD.SourceVoucherID AND SID.RowIndex = DRD.SourceRowIndex\r\n                                INNER JOIN Product_Lot_Sales PLS ON PLS.DocID = SID.OrderSysDocID AND PLS.InvoiceNumber = SID.OrderVoucherID AND PLS.ItemCode = SID.ProductID AND PLS.RowIndex = SID.OrderRowIndex\r\n\t\t\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n                                  WHERE   P.ItemType IN (1,7) AND ISNULL(P.IsTrackLot,'False') = 'False' ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows for Sales Returns Updated in Product_Lot table Successfully.");
					textBoxLog.AppendText("\n\n Updating Inventory Transfers Acceptance...");
					text = " UPDATE PL SET SourceLotNumber = \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                            PL6.SourceLotNumber END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PLS.LotNo)\r\n\r\n\r\n                            FROM Product_Lot PL \r\n                            INNER JOIN Inventory_Transfer TR ON TR.AcceptSysDocID = PL.DocID AND TR.AcceptVoucherID = PL.ReceiptNumber  \r\n                            INNER JOIN Inventory_Transfer_Detail TRD ON TRD.SysDocID = TR.SysDocID AND TRD.VoucherID = TR.VoucherID AND PL.RowIndex = TRD.RowIndex\r\n                            INNER JOIN Product_Lot_Sales PLS ON PLS.DocID = TRD.SysDocID AND PLS.InvoiceNumber = TRD.VoucherID AND PLS.ItemCode = TRD.ProductID AND PLS.RowIndex = TRD.RowIndex\r\n\t\t\t\t\t\t \tINNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n\r\n\t\t\t\t\t\t\t\tWHERE    P.ItemType IN (1,7) AND ISNULL(P.IsTrackLot,'False') = 'False'   ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows for Transfer Acceptance Updated in Product_Lot table Successfully.");
					textBoxLog.AppendText("\n\n Updating Inventory Transfers Rejection Acceptance...");
					text = " UPDATE PL SET SourceLotNumber = \r\n                                 (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                                (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                                (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                                (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                                (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                                PL6.SourceLotNumber END \r\n                                FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                                FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                                FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                                FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                                FROM Product_LOT PL2 WHERE PL2.LotNumber = PLS.LotNo)\r\n \r\n                                 FROM Product_Lot PL \r\n                                INNER JOIN Inventory_Transfer TR ON TR.RejectAcceptSysDocID = PL.DocID AND TR.RejectAcceptVoucherID = PL.ReceiptNumber  \r\n                                INNER JOIN Inventory_Transfer_Detail TRD ON TRD.SysDocID = TR.SysDocID AND TRD.VoucherID = TR.VoucherID AND PL.RowIndex = TRD.RowIndex\r\n                                INNER JOIN Product_Lot_Sales PLS ON PLS.DocID = TRD.SysDocID AND PLS.InvoiceNumber = TRD.VoucherID AND PLS.ItemCode = TRD.ProductID AND PLS.RowIndex = TRD.RowIndex\r\n\t\t\t\t\t\t\t    INNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n\r\n\t\t\t\t\t\t\t\tWHERE  P.ItemType IN (1,7) AND ISNULL(P.IsTrackLot,'False') = 'False'  ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows for Transfer Rejection Acceptance Updated in Product_Lot table Successfully.");
					textBoxLog.AppendText("\n\n Updating Product_Lot Cost...");
					text = "  UPDATE  PL   SET PL.Cost = PL.AvgCost FROM Product_Lot PL INNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n                                WHERE Cost = 0 AND P.ItemType IN (1,7) AND ISNULL(IsTrackLot,'False') = 'False' AND PL.Cost <> 0  ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows updated Successfully.");
					textBoxLog.AppendText("\n\n Updating Product_Lot_Sale Cost...");
					text = "  UPDATE  PLS   SET PLS.Cost = PL.Cost FROM Product_Lot_Sales PLS INNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo\r\n                                INNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n                                WHERE PL.Cost <> PLS.Cost AND P.ItemType IN (1,7) AND ISNULL(IsTrackLot,'False') = 'False'   ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows updated Successfully.");
					textBoxLog.AppendText("\n\n Updating Source Lot Numbers to base source lot...");
					text = " UPDATE PL SET SourceLotNumber = \r\n                            (SELECT CASE WHEN PL2.SourceLotNumber IS NULL THEN PL2.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL3.SourceLotNumber IS NULL THEN PL3.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL4.SourceLotNumber IS NULL THEN PL4.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL5.SourceLotNumber IS NULL THEN PL5.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL6.SourceLotNumber IS NULL THEN PL6.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL7.SourceLotNumber IS NULL THEN PL7.LotNumber ELSE \r\n                            (SELECT CASE WHEN PL8.SourceLotNumber IS NULL THEN PL8.LotNumber ELSE \r\n                            PL8.SourceLotNumber END \r\n                            FROM Product_LOT PL8 WHERE PL8.LotNumber = PL7.SourceLotNumber) END \r\n                            FROM Product_LOT PL7 WHERE PL7.LotNumber = PL6.SourceLotNumber) END \r\n                            FROM Product_LOT PL6 WHERE PL6.LotNumber = PL5.SourceLotNumber) END \r\n                            FROM Product_LOT PL5 WHERE PL5.LotNumber = PL4.SourceLotNumber) END \r\n                            FROM Product_LOT PL4 WHERE PL4.LotNumber = PL3.SourceLotNumber) END \r\n                            FROM Product_LOT PL3 WHERE PL3.LotNumber = PL2.SourceLotNumber) END \r\n                            FROM Product_LOT PL2 WHERE PL2.LotNumber = PL.SourceLotNumber)\r\n\r\n                            FROM Product_Lot PL \r\n\t\t\t\t\t\t \tINNER JOIN Product P ON P.ProductID = PL.ItemCode\r\n\t\t\t\t\t\t\t\tWHERE     PL.SourceLotNumber IS NOT NULL  ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows updated Successfully.");
					textBoxLog.AppendText("\n\n Updating Product_Lot_Sale Cost...");
					text = " UPDATE Product_LOT SET SourceLotNumber = NULL  where SourceLotNumber = LotNumber  ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows updated Successfully.");
					textBoxLog.AppendText("\n\n Updating Zero Quantity Valuation to Zero...");
					text = " UPDATE  Inventory_Transactions Set AssetValue = 0 WHERE  quantity = 0 and assetvalue <> 0 ";
					num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n" + num.ToString() + " Rows updated Successfully.");
					textBoxLog.AppendText("\n..>>");
					textBoxLog.AppendText("\n..>>");
					textBoxLog.AppendText("\n..>>");
					textBoxLog.AppendText("\n All Rows Updated Successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void updateProductLotCostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				ErrorHelper.InformationMessage("This process will update the cost field in Product_Lot table from Inventory transaction table.");
				if (MessageBox.Show(str1 + "\n\nAre you sure you want to update the Cost in Product_Lot Table?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					textBoxLog.AppendText("\nStarting function: Updating Product_Lot...");
					text = " UPDATE PL SET PL.Cost = CASE WHEN IT.Quantity <> 0 THEN AssetValue/Quantity ELSE 0 END, PL.AvgCost = IT.AverageCost\r\n                                FROM Product_Lot PL INNER JOIN Inventory_Transactions IT ON IT.SysDocID = PL.DocID AND IT.VoucherID = PL.ReceiptNumber AND IT.RowIndex = PL.RowIndex ";
					int num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n " + num.ToString() + " Rows Updated Successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void updateProductLotSalesCostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				ErrorHelper.InformationMessage("This process will update the cost field in Product_Lot_Sales table from Product_Lot table based on lot number.");
				if (MessageBox.Show(str1 + "\n\nAre you sure you want to update the Cost in Product_Lot_Sale Table?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					textBoxLog.AppendText("\nStarting function: Updating Product_Lot_Sale...");
					text = " UPDATE PLS SET Cost = PL.Cost FROM Product_Lot_Sales PLS INNER JOIN Product_Lot PL ON PL.LotNumber = PLS.LotNo ";
					int num = Factory.DatabaseSystem.PerformExecuteNonQuery(text);
					textBoxLog.AppendText("\n " + num.ToString() + " Rows Updated Successfully.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void mergeJournalDetailsForCOGSUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					GetDate();
					textBoxLog.Clear();
					isStop = false;
					progressBar1.Value = 0;
					string query = "select DISTINCT JournalID, (SELECT COUNT (*) FROM Journal_Details WHERE JournalID = JD.JournalID  AND Reference = 'SYS_COGS')\r\n                               FROM Journal_Details JD where journalid IN (SELECT JD2.JournalID FROM Journal_Details JD2 INNER JOIN Journal J ON J.JournalID = JD2.JournalID WHERE jd2.JournalID = JD.JournalID AND JournalDate >= '1-1-2018'  AND JD2.Reference = 'SYS_COGS' GROUP BY JD2.JournalID HAVING COUNT(*)>3) ORDER BY JournalID";
					DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(query, fromDate, toDate);
					if (reportByQuery != null && reportByQuery.Tables[0].Rows.Count > 0)
					{
						textBoxLog.AppendText("\nUpdating journals....\nCollecting information...");
						textBoxLog.AppendText(reportByQuery.Tables[0].Rows.Count.ToString() + " journals found which require update. Waiting for confirmation...");
						if (ErrorHelper.QuestionMessageYesNo(reportByQuery.Tables[0].Rows.Count.ToString(), "journal rows found which require updating.", "Do you want to continue?") == DialogResult.Yes)
						{
							progressBar1.Maximum = int.Parse(reportByQuery.Tables[0].Rows.Count.ToString());
							foreach (DataRow row in reportByQuery.Tables[0].Rows)
							{
								if (isStop)
								{
									textBoxLog.AppendText("\nOperation stopped by user.\n\n\n\n\n\n");
									break;
								}
								int num = int.Parse(row["JournalID"].ToString());
								textBoxLog.AppendText("\nUpdating Journal ID:" + num + "...");
								if (!Factory.JournalSystem.MergeJournalDetailsForCOGSUpdate(num, mergeSysWithNormal: false))
								{
									textBoxLog.AppendText("\nAn Error Occured in the operation.\nOperation stoped because of an error for journal ID:" + num);
									break;
								}
								textBoxLog.AppendText("\nJournal ID: " + num + " updated successfully.");
								progressBar1.Value++;
								Application.DoEvents();
							}
						}
						else
						{
							textBoxLog.AppendText("\nOperation cancelled by user.");
						}
					}
					else
					{
						textBoxLog.AppendText("\nNo Journal found with problem to be updated.");
						textBoxLog.AppendText("\nOperation completed");
						ErrorHelper.InformationMessage("No journal found with problem to be updated.");
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
					textBoxLog.AppendText("An error occured in the operation.\nOperation stopped.");
				}
			}
		}

		private void buttonStop_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to stop the operation?") == DialogResult.Yes)
			{
				isStop = true;
			}
		}

		private void mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					GetDate();
					textBoxLog.Clear();
					isStop = false;
					progressBar1.Value = 0;
					string value = "";
					if (doctype != SysDocTypes.None)
					{
						int num = (int)Enum.Parse(typeof(SysDocTypes), doctype.ToString());
						value = "AND DocType= '" + num + "'";
					}
					if (!string.IsNullOrEmpty(SysDocID))
					{
						value = "AND SysDocID = '" + SysDocID + "'";
					}
					if (!string.IsNullOrEmpty(VoucherIDs))
					{
						value = " AND VoucherID IN('" + VoucherIDs + "')";
					}
					string str = "SELECT * FROM \r\n                                (select DISTINCT JournalID, (SELECT COUNT (*) FROM Journal_Details WHERE JournalID = JD.JournalID ) AS Num\r\n                               FROM Journal_Details JD where journalid IN (SELECT JournalID FROM Journal_Details WHERE JournalID = JD.JournalID AND JDDate BETWEEN '@FromDate' AND '@EndDate'";
					if (!string.IsNullOrEmpty(value))
					{
						str += value;
					}
					str += "GROUP BY JournalID \r\n\t\t\t\t\t\t\t   HAVING COUNT(*)>2 AND (SELECT COUNT(*) FROM Journal_Details JD2 WHERE JD2.journalID = JD.JournalID AND Reference = 'SYS_COGS'  ) > 0\r\n\t\t\t\t\t\t\t   ) ) AS JD \r\n                                WHERE (SELECT TOP 1 COUNT(JD3.AccountID) C FROM Journal_Details JD3 WHERE JD3.JournalID = JD.JournalID AND JD3.JDDate  BETWEEN '@FromDate' AND '@EndDate'";
					if (!string.IsNullOrEmpty(value))
					{
						str += value;
					}
					str += "GROUP BY AccountID ORDER BY C DESC)>1\t\t\t\t\t\t\t   \r\n\t\t\t\t\t\t\t   ORDER BY JournalID ";
					DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(str, fromDate, toDate);
					if (reportByQuery != null && reportByQuery.Tables[0].Rows.Count > 0)
					{
						textBoxLog.AppendText("\nUpdating journals....\nCollecting information...");
						textBoxLog.AppendText(reportByQuery.Tables[0].Rows.Count.ToString() + " journals found which require update. Waiting for confirmation...");
						if (ErrorHelper.QuestionMessageYesNo(reportByQuery.Tables[0].Rows.Count.ToString(), "journal rows found which require updating.", "Do you want to continue?") == DialogResult.Yes)
						{
							progressBar1.Maximum = int.Parse(reportByQuery.Tables[0].Rows.Count.ToString());
							foreach (DataRow row in reportByQuery.Tables[0].Rows)
							{
								if (isStop)
								{
									textBoxLog.AppendText("\nOperation stopped by user.\n\n\n\n\n\n");
									break;
								}
								int num2 = int.Parse(row["JournalID"].ToString());
								textBoxLog.AppendText("\nUpdating Journal ID:" + num2 + "...");
								if (!Factory.JournalSystem.MergeJournalDetailsForCOGSUpdate(num2, mergeSysWithNormal: true))
								{
									textBoxLog.AppendText("\nAn Error Occured in the operation.\nOperation stoped because of an error for journal ID:" + num2);
									break;
								}
								textBoxLog.AppendText("\nJournal ID: " + num2 + " updated successfully.");
								progressBar1.Value++;
								Application.DoEvents();
								textBoxLog.ScrollToCaret();
							}
						}
						else
						{
							textBoxLog.AppendText("\nOperation cancelled by user.");
						}
					}
					else
					{
						textBoxLog.AppendText("\nNo Journal found with problem to be updated.");
						textBoxLog.AppendText("\nOperation completed");
						ErrorHelper.InformationMessage("No journal found with problem to be updated.");
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
					textBoxLog.AppendText("An error occured in the operation.\nOperation stopped.");
				}
			}
		}

		private void deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("This process will delete the rows from unallocated_product_lot table which are already allocated.", "Are you sure you want to continue?") == DialogResult.Yes)
				{
					string exp = "  DELETE FROM Unallocated_Lot_Items WHERE ID IN (\r\n                                SELECT  ID\r\n                                 FROM (\r\n                                select UL.ID, UL.sysdocid,ul.voucherid,ul.productid,ul.rowindex,ul.locationid,ul.quantity, \r\n                                (select sum(PLS.SoldQty)  FROM Product_Lot_Sales PLS WHERE   PLS.docid = ul.sysdocid and pls.InvoiceNumber = ul.voucherid and \r\n                                PLS.itemcode = UL.ProductID and pls.rowindex= ul.rowindex) AS PLSQty,\r\n                                (select -1 * sum(IT.Quantity)  FROM Inventory_Transactions IT WHERE    \r\n                                 quantity<0 AND UL.SysDocID = it.SysDocID and UL.VoucherID = IT.voucherID and UL.rowindex = IT.RowIndex and ul.productid = it.productid) AS ITQty\r\n\r\n                                FROM xUnallocated_Lot_Items UL ) AS T\r\n                                  where isnull(itqty,0)-ISNULL(plsqty,0)-quantity <0 )";
					int num = Factory.DatabaseSystem.PerformExecuteNonQuery(exp);
					textBoxLog.AppendText(num.ToString() + " rows deleted from unallocated_product_lot.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void updateSalesCostToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new UpdateRepostCost().ShowDialog();
		}

		private void comboBoxTransactionType_SelectedIndexChanged(object sender, EventArgs e)
		{
			TransactionTypeComboBox transactionTypeComboBox = (TransactionTypeComboBox)dateFormObj.Controls.Find("transtypeControl1", searchAllChildren: false)[0];
			doctype = (SysDocTypes)byte.Parse(transactionTypeComboBox.SelectedID);
			((SysDocComboBox)dateFormObj.Controls.Find("sysDocComboControl1", searchAllChildren: false)[0]).FilterByType(doctype);
		}

		private void buttonGRN_Click(object sender, EventArgs e)
		{
			if (sysDocComboObj.SelectedID != "")
			{
				SysDocID = sysDocComboObj.SelectedID;
				VoucherIDs = textBoxVoucherNumber.Text;
				int objectID = (int)Enum.Parse(value: Factory.SystemDocumentSystem.GetBarCodeSystemDocumentType(SysDocID).ToString(), enumType: typeof(SysDocTypes));
				DoubleString doubleString = Factory.ApprovalSystem.GetTableName(1, objectID);
				tableName = doubleString.FirstString;
				try
				{
					SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
					selectDocumentDialog.AllowDateFilter = true;
					selectDocumentDialog.RequireDataRefresh += form_RequireDataRefresh;
					selectDocumentDialog.Text = "Select Value";
					selectDocumentDialog.IsMultiSelect = true;
					selectDocumentDialog.AllowDateFilter = true;
					new List<string>();
					List<string> list = new List<string>();
					if (selectDocumentDialog.ShowDialog(this) == DialogResult.OK)
					{
						foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
						{
							SysDocID = selectedRow.Cells["Doc ID"].Value.ToString();
							if (selectedRow.Cells.Exists("Doc Number"))
							{
								VoucherIDs = selectedRow.Cells["Doc Number"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("VoucherID"))
							{
								VoucherIDs = selectedRow.Cells["VoucherID"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("Number"))
							{
								VoucherIDs = selectedRow.Cells["Number"].Text.ToString();
							}
							else if (selectedRow.Cells.Exists("Batch Number"))
							{
								VoucherIDs = selectedRow.Cells["Batch Number"].Text.ToString();
							}
							list.Add(VoucherIDs);
						}
						textBoxVoucherNumber.Text = string.Join(",", list);
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		private void form_RequireDataRefresh(object sender, DateRangeStruct e)
		{
			SelectDocumentDialog obj = sender as SelectDocumentDialog;
			DataSet dataSet = new DataSet();
			dataSet = (obj.DataSource = Factory.PurchaseReceiptSystem.GetVoucherNumbersFromTransaction(tableName, SysDocID, "", e.From, e.To));
		}

		private void GetDate()
		{
			transtypeControlObj = new TransactionTypeComboBox();
			transtypeControlObj.Location = new Point(6, 95);
			transtypeControlObj.Name = "transtypeControl1";
			transtypeControlObj.Size = new Size(200, 30);
			transtypeControlObj.TabIndex = 0;
			transtypeControlObj.SelectedIndexChanged += comboBoxTransactionType_SelectedIndexChanged;
			checkBoxNone = new CheckBox();
			checkBoxNone.Name = "none";
			checkBoxNone.Text = "None";
			checkBoxNone.Location = new Point(190, 142);
			checkBoxNone.CheckedChanged += checkChanged;
			checkBoxNone.Checked = false;
			sysDocComboObj = new SysDocComboBox();
			sysDocComboObj.Location = new Point(6, 120);
			sysDocComboObj.Name = "sysDocComboControl1";
			sysDocComboObj.Size = new Size(200, 30);
			sysDocComboObj.TabIndex = 0;
			textBoxVoucherNumber = new TextBox();
			textBoxVoucherNumber.Location = new Point(6, 141);
			textBoxVoucherNumber.MaxLength = 15;
			textBoxVoucherNumber.Name = "textBoxVoucherNumber";
			textBoxVoucherNumber.Size = new Size(132, 20);
			textBoxVoucherNumber.TabIndex = 2;
			textBoxVoucherNumber.ReadOnly = true;
			XPButton xPButton = new XPButton();
			xPButton.AdjustImageLocation = new Point(0, 0);
			xPButton.BackColor = Color.DarkGray;
			xPButton.BtnShape = BtnShape.Rectangle;
			xPButton.BtnStyle = XPStyle.Default;
			xPButton.ImageAlign = ContentAlignment.MiddleLeft;
			xPButton.ImeMode = ImeMode.NoControl;
			xPButton.Location = new Point(140, 140);
			xPButton.Name = "buttonGRN";
			xPButton.Size = new Size(34, 24);
			xPButton.TabIndex = 167;
			xPButton.Text = "...";
			xPButton.UseVisualStyleBackColor = false;
			xPButton.Click += buttonGRN_Click;
			dateControlObj = new DateControl();
			dateControlObj.FromDate = new DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControlObj.Location = new Point(6, 15);
			dateControlObj.Name = "dateControl1";
			dateControlObj.SelectedPeriod = DatePeriods.ThisMonthToDate;
			dateControlObj.Size = new Size(445, 60);
			dateControlObj.TabIndex = 0;
			dateControlObj.ToDate = new DateTime(2017, 10, 17, 23, 59, 59, 59);
			GroupBox groupBox = new GroupBox();
			groupBox.Controls.Add(dateControlObj);
			groupBox.Location = new Point(2, 12);
			groupBox.Name = "groupBoxCC";
			groupBox.Size = new Size(453, 80);
			groupBox.TabIndex = 10;
			groupBox.TabStop = false;
			groupBox.Text = "DatePicker";
			Button button = new Button();
			button.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			button.Location = new Point(353, 120);
			button.Name = "buttonOK";
			button.Size = new Size(102, 24);
			button.TabIndex = 1;
			button.Text = "&Process";
			button.UseVisualStyleBackColor = true;
			button.Click += buttonOK_Click;
			dateFormObj = new Form();
			dateFormObj.Size = new Size(481, 210);
			dateFormObj.Controls.Add(button);
			dateFormObj.Controls.Add(groupBox);
			dateFormObj.Controls.Add(transtypeControlObj);
			dateFormObj.Controls.Add(sysDocComboObj);
			dateFormObj.Controls.Add(textBoxVoucherNumber);
			dateFormObj.Controls.Add(xPButton);
			dateFormObj.Controls.Add(checkBoxNone);
			dateFormObj.ShowDialog();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			fromDate = dateControlObj.FromDate;
			toDate = dateControlObj.ToDate;
			dateFormObj.Close();
		}

		private void checkChanged(object sender, EventArgs e)
		{
			sysDocComboObj.Clear();
			transtypeControlObj.Clear();
			textBoxVoucherNumber.Clear();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Others.AxolonMaintenanceForm));
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			inventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			updateAverageCostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			updateSourceLotNumberProductLotTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			updateProductLotCostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			updateProductLotSalesCostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			updateSalesCostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			mergeJournalDetailsForCOGSUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			label1 = new System.Windows.Forms.Label();
			textBoxLog = new System.Windows.Forms.RichTextBox();
			progressBar1 = new System.Windows.Forms.ProgressBar();
			label2 = new System.Windows.Forms.Label();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			buttonStop = new System.Windows.Forms.Button();
			menuStrip1.SuspendLayout();
			SuspendLayout();
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1]
			{
				inventoryToolStripMenuItem
			});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(1080, 24);
			menuStrip1.TabIndex = 4;
			menuStrip1.Text = "menuStrip1";
			inventoryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9]
			{
				updateAverageCostToolStripMenuItem,
				updateSourceLotNumberProductLotTableToolStripMenuItem,
				updateProductLotCostToolStripMenuItem,
				updateProductLotSalesCostToolStripMenuItem,
				updateSalesCostToolStripMenuItem,
				toolStripSeparator1,
				mergeJournalDetailsForCOGSUpdatesToolStripMenuItem,
				mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem,
				deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem
			});
			inventoryToolStripMenuItem.Name = "inventoryToolStripMenuItem";
			inventoryToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			inventoryToolStripMenuItem.Text = "Inventory";
			updateAverageCostToolStripMenuItem.Name = "updateAverageCostToolStripMenuItem";
			updateAverageCostToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			updateAverageCostToolStripMenuItem.Text = "Update Average Cost...";
			updateAverageCostToolStripMenuItem.ToolTipText = resources.GetString("updateAverageCostToolStripMenuItem.ToolTipText");
			updateAverageCostToolStripMenuItem.Click += new System.EventHandler(updateAverageCostToolStripMenuItem_Click);
			updateSourceLotNumberProductLotTableToolStripMenuItem.Name = "updateSourceLotNumberProductLotTableToolStripMenuItem";
			updateSourceLotNumberProductLotTableToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			updateSourceLotNumberProductLotTableToolStripMenuItem.Text = "Update SourceLotNumber (Product_Lot Table)";
			updateSourceLotNumberProductLotTableToolStripMenuItem.ToolTipText = "This function will update the Source Lot Number for all items which are not set to track lot and does not have a source lot assigned to them.";
			updateSourceLotNumberProductLotTableToolStripMenuItem.Click += new System.EventHandler(updateSourceLotNumberProductLotTableToolStripMenuItem_Click);
			updateProductLotCostToolStripMenuItem.Name = "updateProductLotCostToolStripMenuItem";
			updateProductLotCostToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			updateProductLotCostToolStripMenuItem.Text = "Update Product Lot Cost ";
			updateProductLotCostToolStripMenuItem.Click += new System.EventHandler(updateProductLotCostToolStripMenuItem_Click);
			updateProductLotSalesCostToolStripMenuItem.Name = "updateProductLotSalesCostToolStripMenuItem";
			updateProductLotSalesCostToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			updateProductLotSalesCostToolStripMenuItem.Text = "Update Product_Lot_Sales Cost";
			updateProductLotSalesCostToolStripMenuItem.Click += new System.EventHandler(updateProductLotSalesCostToolStripMenuItem_Click);
			updateSalesCostToolStripMenuItem.Name = "updateSalesCostToolStripMenuItem";
			updateSalesCostToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			updateSalesCostToolStripMenuItem.Text = "Update Sales Cost";
			updateSalesCostToolStripMenuItem.Click += new System.EventHandler(updateSalesCostToolStripMenuItem_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(368, 6);
			mergeJournalDetailsForCOGSUpdatesToolStripMenuItem.Name = "mergeJournalDetailsForCOGSUpdatesToolStripMenuItem";
			mergeJournalDetailsForCOGSUpdatesToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			mergeJournalDetailsForCOGSUpdatesToolStripMenuItem.Text = "Merge Journal Details for COGS Updates";
			mergeJournalDetailsForCOGSUpdatesToolStripMenuItem.Click += new System.EventHandler(mergeJournalDetailsForCOGSUpdatesToolStripMenuItem_Click);
			mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem.Name = "mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem";
			mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem.Text = "Merge Journal Details for COGS - Complete Merge";
			mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem.Click += new System.EventHandler(mergeJournalDetailsForCOGSCompleteMergeToolStripMenuItem_Click);
			deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem.Name = "deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem";
			deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem.Size = new System.Drawing.Size(371, 22);
			deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem.Text = "Delete Unallocated Product Lot Which Allocated Already";
			deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem.Click += new System.EventHandler(deleteUnallocatedProductLotWhichAllocatedAlreadyToolStripMenuItem_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 190);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(28, 13);
			label1.TabIndex = 5;
			label1.Text = "Log:";
			textBoxLog.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxLog.BackColor = System.Drawing.Color.Black;
			textBoxLog.ForeColor = System.Drawing.Color.LimeGreen;
			textBoxLog.Location = new System.Drawing.Point(12, 206);
			textBoxLog.Name = "textBoxLog";
			textBoxLog.ReadOnly = true;
			textBoxLog.Size = new System.Drawing.Size(1056, 385);
			textBoxLog.TabIndex = 6;
			textBoxLog.Text = "";
			progressBar1.Location = new System.Drawing.Point(78, 146);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(509, 23);
			progressBar1.TabIndex = 7;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 151);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(51, 13);
			label2.TabIndex = 8;
			label2.Text = "Progress:";
			buttonStop.Location = new System.Drawing.Point(614, 148);
			buttonStop.Name = "buttonStop";
			buttonStop.Size = new System.Drawing.Size(111, 23);
			buttonStop.TabIndex = 9;
			buttonStop.Text = "Stop";
			buttonStop.UseVisualStyleBackColor = true;
			buttonStop.Click += new System.EventHandler(buttonStop_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1080, 603);
			base.Controls.Add(buttonStop);
			base.Controls.Add(label2);
			base.Controls.Add(progressBar1);
			base.Controls.Add(textBoxLog);
			base.Controls.Add(label1);
			base.Controls.Add(menuStrip1);
			base.MainMenuStrip = menuStrip1;
			base.Name = "AxolonMaintenanceForm";
			Text = "Axolon Maintenance";
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
