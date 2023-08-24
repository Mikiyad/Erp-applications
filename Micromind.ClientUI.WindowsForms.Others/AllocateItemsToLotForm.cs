using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others
{
	public class AllocateItemsToLotForm : Form
	{
		private DataSet data = new DataSet();

		private int nextIndex;

		private bool isPaused;

		private bool isStopped;

		private IContainer components;

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

		public AllocateItemsToLotForm()
		{
			InitializeComponent();
		}

		private void AllocateItemsToLotForm_Load(object sender, EventArgs e)
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
					textBoxLog.AppendText("\nThis process may take several minutes depend on number of items that are not allocated. It is recommended to run this process when the system is idle.");
					if (MessageBox.Show("Are you sure you want to continue?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
					{
						string text = textBoxFromItem.Text;
						string text2 = textBoxToItem.Text;
						DateTime now = DateTime.Now;
						textBoxLog.AppendText("\nAllocation process started...");
						string text3 = "SELECT DISTINCT ProductID FROM Unallocated_Lot_Items WHERE 1=1 ";
						if (radioButtonSelected.Checked && text != "")
						{
							text3 = text3 + " AND ProductID BETWEEN '" + text + "' AND '" + text2 + "' ";
						}
						text3 += "   ORDER BY ProductID";
						data = Factory.SmartListSystem.GetReportByQuery(text3, DateTime.Today, DateTime.Today);
						if (data == null || data.Tables[0].Rows.Count == 0)
						{
							AddLog("No rows found to update.");
							AddLog("Process completed.");
						}
						else
						{
							AddLog(data.Tables[0].Rows.Count + " items found to allocate...Click YES to start allocation...");
							if (ErrorHelper.QuestionMessageYesNo("Allocate " + data.Tables[0].Rows.Count + " Items ?") == DialogResult.Yes)
							{
								progressBar1.Maximum = data.Tables[0].Rows.Count;
								AddLog("Process started...");
								buttonUpdate.Enabled = false;
								int num = 1;
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

		private void DeleteUnallocateableRows()
		{
			AddLog("\n\nDeleting unallocated lot rows which are not matched:\n");
		}

		private bool ProcessUpdate()
		{
			bool flag = true;
			checked
			{
				try
				{
					for (int i = nextIndex; i < data.Tables[0].Rows.Count; i++)
					{
						DateTime now = DateTime.Now;
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
						string text = data.Tables[0].Rows[i]["ProductID"].ToString();
						AddLog("Allocating Item: " + text);
						flag = Factory.ProductSystem.AllocateItemsToLot(new string[1]
						{
							text
						});
						TimeSpan timeSpan = DateTime.Now.Subtract(now);
						if (flag)
						{
							AddLog("Allocating item finished successfully - Duration: " + timeSpan.Minutes + " Min , " + timeSpan.Seconds + " Second");
						}
						progressBar1.Value++;
						Application.DoEvents();
						textBoxLog.ScrollToCaret();
					}
					if (flag)
					{
						AddLog("Process completed successfully.");
					}
					else
					{
						AddLog("Process completed with errors.");
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

		private void buttonViewUnmatched_Click(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Others.AllocateItemsToLotForm));
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
			SuspendLayout();
			buttonUpdate.Location = new System.Drawing.Point(537, 100);
			buttonUpdate.Name = "buttonUpdate";
			buttonUpdate.Size = new System.Drawing.Size(107, 23);
			buttonUpdate.TabIndex = 10;
			buttonUpdate.Text = "Allocate";
			buttonUpdate.UseVisualStyleBackColor = true;
			buttonUpdate.Click += new System.EventHandler(button1_Click);
			radioButtonSelected.AutoSize = true;
			radioButtonSelected.Location = new System.Drawing.Point(159, 12);
			radioButtonSelected.Name = "radioButtonSelected";
			radioButtonSelected.Size = new System.Drawing.Size(79, 17);
			radioButtonSelected.TabIndex = 3;
			radioButtonSelected.Text = "Items From:";
			radioButtonSelected.UseVisualStyleBackColor = true;
			radioButtonSelected.CheckedChanged += new System.EventHandler(radioButtonSelected_CheckedChanged);
			radioButtonAllItems.AutoSize = true;
			radioButtonAllItems.Checked = true;
			radioButtonAllItems.Location = new System.Drawing.Point(15, 12);
			radioButtonAllItems.Name = "radioButtonAllItems";
			radioButtonAllItems.Size = new System.Drawing.Size(64, 17);
			radioButtonAllItems.TabIndex = 2;
			radioButtonAllItems.TabStop = true;
			radioButtonAllItems.Text = "All Items";
			radioButtonAllItems.UseVisualStyleBackColor = true;
			radioButtonAllItems.CheckedChanged += new System.EventHandler(radioButtonAllItems_CheckedChanged);
			textBoxFromItem.Enabled = false;
			textBoxFromItem.Location = new System.Drawing.Point(244, 12);
			textBoxFromItem.Name = "textBoxFromItem";
			textBoxFromItem.Size = new System.Drawing.Size(255, 20);
			textBoxFromItem.TabIndex = 4;
			textBoxToItem.Enabled = false;
			textBoxToItem.Location = new System.Drawing.Point(244, 36);
			textBoxToItem.Name = "textBoxToItem";
			textBoxToItem.Size = new System.Drawing.Size(255, 20);
			textBoxToItem.TabIndex = 5;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(199, 39);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 1;
			label2.Text = "To:";
			progressBar1.Location = new System.Drawing.Point(12, 71);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(632, 23);
			progressBar1.TabIndex = 6;
			button1.Location = new System.Drawing.Point(126, 99);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(107, 23);
			button1.TabIndex = 9;
			button1.Text = "Stop";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click_1);
			checkBoxPause.Appearance = System.Windows.Forms.Appearance.Button;
			checkBoxPause.Location = new System.Drawing.Point(12, 99);
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
			textBoxLog.Location = new System.Drawing.Point(12, 129);
			textBoxLog.Name = "textBoxLog";
			textBoxLog.ReadOnly = true;
			textBoxLog.Size = new System.Drawing.Size(635, 304);
			textBoxLog.TabIndex = 11;
			textBoxLog.Text = "";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, -21);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(28, 13);
			label3.TabIndex = 10;
			label3.Text = "Log:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(662, 445);
			base.Controls.Add(textBoxLog);
			base.Controls.Add(label3);
			base.Controls.Add(checkBoxPause);
			base.Controls.Add(button1);
			base.Controls.Add(progressBar1);
			base.Controls.Add(textBoxToItem);
			base.Controls.Add(textBoxFromItem);
			base.Controls.Add(radioButtonAllItems);
			base.Controls.Add(radioButtonSelected);
			base.Controls.Add(buttonUpdate);
			base.Controls.Add(label2);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AllocateItemsToLotForm";
			Text = "Allocate Items to Lots";
			base.Load += new System.EventHandler(AllocateItemsToLotForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
