using Micromind.ClientLibraries;
using Micromind.Common;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others
{
	public class UpdateCOGSForm : Form
	{
		private DataSet data = new DataSet();

		private int nextIndex;

		private bool isPaused;

		private bool isStopped;

		private IContainer components;

		private DateTimePicker dateTimePickerDate;

		private Label label1;

		private Button buttonUpdate;

		private RadioButton radioButtonSelected;

		private RadioButton radioButtonAllItems;

		private TextBox textBoxFromItem;

		private TextBox textBoxToItem;

		private Label label2;

		private ProgressBar progressBar1;

		private Button button1;

		private CheckBox checkBoxPause;

		private RichTextBox textBoxLog;

		private Label label3;

		private TextBox textBoxCount;

		private Label label4;

		private RadioButton radioButtonSystemDecide;

		private DateTimePicker dateTimePickerEndDate;

		private Label label5;

		public UpdateCOGSForm()
		{
			InitializeComponent();
			textBoxCount.Enabled = true;
		}

		private void UpdateCOGSForm_Load(object sender, EventArgs e)
		{
		}

		private void AddLog(string msg)
		{
			textBoxLog.AppendText("\n" + msg);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			bool flag = true;
			checked
			{
				try
				{
					nextIndex = 0;
					isStopped = false;
					isPaused = false;
					checkBoxPause.Checked = false;
					progressBar1.Value = 0;
					textBoxLog.Clear();
					textBoxLog.AppendText("\nThis process may take several minutes or hours depend on number of transactions. It is recommended to run this process when the system is idle.");
					if (MessageBox.Show("Are you sure you want to continue?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					{
						string text = textBoxFromItem.Text;
						string text2 = textBoxToItem.Text;
						DateTime now = DateTime.Now;
						textBoxLog.AppendText("\nUpdating cost process started...");
						AddLog("Calculating rows to be updated...");
						object obj = Factory.DatabaseSystem.PerformExecuteScalar("SELECT TOP 1 StartDate FROM FiscalYear WHERE Status = 1 ORDER BY StartDate");
						DateTime date = DateTime.Parse("1-1-1990");
						if (!obj.IsDBNullOrEmpty())
						{
							date = DateTime.Parse(obj.ToString());
						}
						DateTime date2 = new DateTime(dateTimePickerEndDate.Value.Year, dateTimePickerEndDate.Value.Month, dateTimePickerEndDate.Value.Day, 23, 59, 59);
						string text3 = StoreConfiguration.ToSqlDateTimeString(date);
						string text4 = StoreConfiguration.ToSqlDateTimeString(date2);
						string text5 = "select  * from  (\r\n                                    SELECT IT.*,  \r\n                                    ROW_NUMBER() OVER( PARTITION BY IT.Productid ORDER BY transactiondate,TransactionID ) AS Row\r\n                                    from Inventory_Transactions IT INNER JOIN Product P ON IT.ProductID = P.ProductID WHERE P.ItemType  IN (1,7) AND  TransactionDate> '" + text3 + "' ";
						if (radioButtonSystemDecide.Checked)
						{
							text5 = text5 + " AND IT.ProductID IN  ( SELECT DISTINCT IT.ProductID FROM (\r\n                                 SELECT TransactionID,SysdocID,VoucherID,TransactionType Y,IT.ProductID,SysDocType,ISNONCostedGRN as G,TransactionDate,Quantity,UnitPrice,\r\n                                 AverageCost,AssetValue, \r\n                                ROW_NUMBER()  OVER ( Partition BY IT.ProductID Order by TransactionDate, TransactionID ) RowNumber,\r\n                                SUM(Quantity - ISNULL(ReturnedQuantity,0))  OVER ( Partition BY IT.ProductID Order by TransactionDate, TransactionID ROWS between unbounded preceding and current ROW) AS TotalQuantity,\r\n                                SUM(AssetValue)  OVER ( Partition BY IT.ProductID Order by TransactionDate, TransactionID ROWS between unbounded preceding and current ROW ) AS TotalValue\r\n                                 FROM Inventory_Transactions IT WHERE TransactionDate < '" + text4 + "') AS IT INNER JOIN Product P ON P.ProductID = IT.ProductID\r\n                                 WHERE totalquantity >= 0  AND (SysDocType <> 20 OR (SysDocType = 20 AND TransactionDate > '10-1-2015')) --AND assetvalue <> 0  --Old Stock Transfer system\r\n                                   AND IT.quantity <> 0  AND SysDocType<>95  AND TransactionDate > '" + text3 + "'  AND ABS(ROUND(totalvalue- TotalQuantity * ISNULL(it.AverageCost,0),2)) > ISNULL(P.IgnoreCostDiffAmount,0.1) ) ";
						}
						else if (text != "")
						{
							text5 = text5 + " AND IT.ProductID BETWEEN '" + text + "' AND '" + text2 + "' ";
						}
						text5 += ") s where row = 1   ";
						text5 += "UNION \r\n                            SELECT IT.*,0 AS ROW FROM Inventory_Transactions IT \r\n                            INNER JOIN Product P ON P.ProductID = IT.ProductID WHERE P.CostMethod = 2\r\n                            AND ISNULL(IsRecost,0) = 1  ";
						if (text != "")
						{
							text5 = text5 + " AND IT.ProductID BETWEEN '" + text + "' AND '" + text2 + "' ";
						}
						text5 += " ORDER BY ProductID";
						data = Factory.SmartListSystem.GetReportByQuery(text5, DateTime.MinValue, DateTime.MaxValue);
						if (data == null || data.Tables[0].Rows.Count == 0)
						{
							AddLog("No rows found to update.");
							AddLog("Process completed.");
						}
						else
						{
							AddLog(data.Tables[0].Rows.Count + " items found to update...Click YES to start updating...");
							if (ErrorHelper.QuestionMessageYesNo("Update", data.Tables[0].Rows.Count.ToString(), "Items?") == DialogResult.Yes)
							{
								progressBar1.Maximum = data.Tables[0].Rows.Count;
								AddLog("Process started...");
								buttonUpdate.Enabled = false;
								int num = int.Parse(textBoxCount.Text);
								for (int i = 0; i < num; i++)
								{
									nextIndex = 0;
									progressBar1.Value = 0;
									flag = ProcessUpdate();
									if (!flag)
									{
										break;
									}
									AddLog("******************** Round: " + (i + 1).ToString() + " Finished Successfully! **********************");
									if (radioButtonSystemDecide.Checked)
									{
										data = Factory.SmartListSystem.GetReportByQuery(text5, DateTime.MinValue, DateTime.MaxValue);
									}
								}
								buttonUpdate.Enabled = true;
							}
							else
							{
								AddLog("Process stopped by user.");
							}
							TimeSpan timeSpan = DateTime.Now.Subtract(now);
							if (flag && !isStopped && !isPaused)
							{
								AddLog("Process completed successfully in " + timeSpan.Hours + " Hours , " + timeSpan.Minutes + " Minutes , " + timeSpan.Seconds + " Seconds");
								ErrorHelper.InformationMessage("Tranaction completed successfully.", timeSpan.Hours + " Hours " + timeSpan.Minutes + " Minutes " + timeSpan.Seconds + " Seconds");
							}
						}
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
					buttonUpdate.Enabled = true;
				}
			}
		}

		private bool ProcessUpdate()
		{
			bool flag = true;
			checked
			{
				try
				{
					string text = "";
					string str = "";
					bool flag2 = false;
					for (int i = nextIndex; i < data.Tables[0].Rows.Count; i++)
					{
						string text2 = "";
						DateTime now = DateTime.Now;
						try
						{
							if (isPaused)
							{
								AddLog("Process paused by user.");
								return flag;
							}
							if (isStopped)
							{
								AddLog("Process stopped by user.");
								return flag;
							}
							nextIndex = i;
							text2 = data.Tables[0].Rows[i]["ProductID"].ToString();
							AddLog("Updating Item: " + text2);
							flag = Factory.ProductSystem.UpdatePreviousTransactionsCOGS(dateTimePickerDate.Value, text2, text2);
						}
						catch (Exception ex)
						{
							flag2 = true;
							text = text + "Error in item: " + text2 + " - Message:" + ex.Message + "\n";
							str = ex.Message;
							flag = false;
						}
						finally
						{
							TimeSpan timeSpan = DateTime.Now.Subtract(now);
							if (flag)
							{
								AddLog("Updating item finished successfully - Duration: " + timeSpan.Minutes + " Min , " + timeSpan.Seconds + " Second");
							}
							else
							{
								AddLog("Error in updating. " + str);
							}
							if (progressBar1.Value + 1 < progressBar1.Maximum)
							{
								progressBar1.Value++;
							}
							Application.DoEvents();
							textBoxLog.ScrollToCaret();
						}
					}
					if (flag && !flag2)
					{
						AddLog("Process completed successfully.");
					}
					else
					{
						AddLog("Process completed with errors.");
						ErrorHelper.ProcessError(new Exception(), text);
					}
					return flag;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return false;
				}
			}
		}

		private void radioButtonAllItems_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButtonAllItems.Checked)
			{
				textBoxFromItem.Clear();
				textBoxToItem.Clear();
				TextBox textBox = textBoxToItem;
				bool enabled = textBoxFromItem.Enabled = false;
				textBox.Enabled = enabled;
			}
		}

		private void radioButtonSelected_CheckedChanged(object sender, EventArgs e)
		{
			TextBox textBox = textBoxToItem;
			bool enabled = textBoxFromItem.Enabled = radioButtonSelected.Checked;
			textBox.Enabled = enabled;
		}

		private void checkBoxPause_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxPause.Checked)
			{
				isPaused = false;
				ProcessUpdate();
			}
			else
			{
				isPaused = true;
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			isStopped = true;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Others.UpdateCOGSForm));
			dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
			label1 = new System.Windows.Forms.Label();
			buttonUpdate = new System.Windows.Forms.Button();
			radioButtonSelected = new System.Windows.Forms.RadioButton();
			radioButtonAllItems = new System.Windows.Forms.RadioButton();
			textBoxFromItem = new System.Windows.Forms.TextBox();
			textBoxToItem = new System.Windows.Forms.TextBox();
			label2 = new System.Windows.Forms.Label();
			progressBar1 = new System.Windows.Forms.ProgressBar();
			button1 = new System.Windows.Forms.Button();
			checkBoxPause = new System.Windows.Forms.CheckBox();
			textBoxLog = new System.Windows.Forms.RichTextBox();
			label3 = new System.Windows.Forms.Label();
			textBoxCount = new System.Windows.Forms.TextBox();
			label4 = new System.Windows.Forms.Label();
			radioButtonSystemDecide = new System.Windows.Forms.RadioButton();
			dateTimePickerEndDate = new System.Windows.Forms.DateTimePicker();
			label5 = new System.Windows.Forms.Label();
			SuspendLayout();
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(77, 12);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(130, 20);
			dateTimePickerDate.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(58, 13);
			label1.TabIndex = 1;
			label1.Text = "Start Date:";
			buttonUpdate.Location = new System.Drawing.Point(540, 150);
			buttonUpdate.Name = "buttonUpdate";
			buttonUpdate.Size = new System.Drawing.Size(107, 23);
			buttonUpdate.TabIndex = 10;
			buttonUpdate.Text = "Update";
			buttonUpdate.UseVisualStyleBackColor = true;
			buttonUpdate.Click += new System.EventHandler(button1_Click);
			radioButtonSelected.AutoSize = true;
			radioButtonSelected.Location = new System.Drawing.Point(114, 50);
			radioButtonSelected.Name = "radioButtonSelected";
			radioButtonSelected.Size = new System.Drawing.Size(79, 17);
			radioButtonSelected.TabIndex = 3;
			radioButtonSelected.Text = "Items From:";
			radioButtonSelected.UseVisualStyleBackColor = true;
			radioButtonSelected.CheckedChanged += new System.EventHandler(radioButtonSelected_CheckedChanged);
			radioButtonAllItems.AutoSize = true;
			radioButtonAllItems.Checked = true;
			radioButtonAllItems.Location = new System.Drawing.Point(15, 50);
			radioButtonAllItems.Name = "radioButtonAllItems";
			radioButtonAllItems.Size = new System.Drawing.Size(64, 17);
			radioButtonAllItems.TabIndex = 2;
			radioButtonAllItems.TabStop = true;
			radioButtonAllItems.Text = "All Items";
			radioButtonAllItems.UseVisualStyleBackColor = true;
			radioButtonAllItems.CheckedChanged += new System.EventHandler(radioButtonAllItems_CheckedChanged);
			textBoxFromItem.Enabled = false;
			textBoxFromItem.Location = new System.Drawing.Point(199, 50);
			textBoxFromItem.Name = "textBoxFromItem";
			textBoxFromItem.Size = new System.Drawing.Size(255, 20);
			textBoxFromItem.TabIndex = 4;
			textBoxToItem.Enabled = false;
			textBoxToItem.Location = new System.Drawing.Point(199, 74);
			textBoxToItem.Name = "textBoxToItem";
			textBoxToItem.Size = new System.Drawing.Size(255, 20);
			textBoxToItem.TabIndex = 5;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(157, 74);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 1;
			label2.Text = "To:";
			progressBar1.Location = new System.Drawing.Point(15, 111);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(632, 23);
			progressBar1.TabIndex = 6;
			button1.Location = new System.Drawing.Point(129, 149);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(107, 23);
			button1.TabIndex = 9;
			button1.Text = "Stop";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click_1);
			checkBoxPause.Appearance = System.Windows.Forms.Appearance.Button;
			checkBoxPause.Location = new System.Drawing.Point(15, 149);
			checkBoxPause.Name = "checkBoxPause";
			checkBoxPause.Size = new System.Drawing.Size(104, 24);
			checkBoxPause.TabIndex = 8;
			checkBoxPause.Text = "| |    Pause";
			checkBoxPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			checkBoxPause.UseVisualStyleBackColor = true;
			checkBoxPause.CheckedChanged += new System.EventHandler(checkBoxPause_CheckedChanged);
			textBoxLog.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxLog.BackColor = System.Drawing.Color.Black;
			textBoxLog.ForeColor = System.Drawing.Color.LimeGreen;
			textBoxLog.Location = new System.Drawing.Point(15, 179);
			textBoxLog.Name = "textBoxLog";
			textBoxLog.ReadOnly = true;
			textBoxLog.Size = new System.Drawing.Size(632, 285);
			textBoxLog.TabIndex = 11;
			textBoxLog.Text = "";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, -21);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(28, 13);
			label3.TabIndex = 10;
			label3.Text = "Log:";
			textBoxCount.Enabled = false;
			textBoxCount.Location = new System.Drawing.Point(567, 74);
			textBoxCount.Name = "textBoxCount";
			textBoxCount.Size = new System.Drawing.Size(70, 20);
			textBoxCount.TabIndex = 7;
			textBoxCount.Text = "1";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(491, 77);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(38, 13);
			label4.TabIndex = 13;
			label4.Text = "Count:";
			radioButtonSystemDecide.AutoSize = true;
			radioButtonSystemDecide.Location = new System.Drawing.Point(15, 77);
			radioButtonSystemDecide.Name = "radioButtonSystemDecide";
			radioButtonSystemDecide.Size = new System.Drawing.Size(110, 17);
			radioButtonSystemDecide.TabIndex = 6;
			radioButtonSystemDecide.Text = "Decide by System";
			radioButtonSystemDecide.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
			radioButtonSystemDecide.UseVisualStyleBackColor = true;
			radioButtonSystemDecide.CheckedChanged += new System.EventHandler(radioButtonAllItems_CheckedChanged);
			dateTimePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerEndDate.Location = new System.Drawing.Point(291, 12);
			dateTimePickerEndDate.Name = "dateTimePickerEndDate";
			dateTimePickerEndDate.Size = new System.Drawing.Size(130, 20);
			dateTimePickerEndDate.TabIndex = 1;
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(213, 14);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(55, 13);
			label5.TabIndex = 14;
			label5.Text = "End Date:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(659, 476);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(textBoxCount);
			base.Controls.Add(textBoxLog);
			base.Controls.Add(label3);
			base.Controls.Add(checkBoxPause);
			base.Controls.Add(button1);
			base.Controls.Add(progressBar1);
			base.Controls.Add(textBoxToItem);
			base.Controls.Add(textBoxFromItem);
			base.Controls.Add(radioButtonSystemDecide);
			base.Controls.Add(radioButtonAllItems);
			base.Controls.Add(radioButtonSelected);
			base.Controls.Add(buttonUpdate);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerEndDate);
			base.Controls.Add(dateTimePickerDate);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpdateCOGSForm";
			Text = "Update COGS";
			base.Load += new System.EventHandler(UpdateCOGSForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
