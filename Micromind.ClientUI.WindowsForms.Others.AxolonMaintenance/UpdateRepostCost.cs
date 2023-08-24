using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Reports.CustomDashboards;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others.AxolonMaintenance
{
	public class UpdateRepostCost : Form
	{
		private DataSet data = new DataSet();

		private int SuccessCount;

		private int FailedCount;

		private IContainer components;

		private Label label1;

		private Label label2;

		private Label label3;

		private Label labelTotal;

		private Label labelFailed;

		private Label labelSuccess;

		private DataEntryGrid ultraGridQueryData;

		private Label labelProcess;

		private Label label5;

		private BackgroundWorker _worker;

		private GroupBox groupBox1;

		private XPButton buttonClear;

		private XPButton buttonNewQuery;

		private XPButton buttonUpdate;

		private Line line1;

		public UpdateRepostCost()
		{
			InitializeComponent();
			SetupGridUI();
		}

		private void InitWorker()
		{
			if (_worker != null)
			{
				_worker.Dispose();
			}
			_worker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			_worker.DoWork += DoWork;
			_worker.RunWorkerCompleted += RunWorkerCompleted;
			_worker.RunWorkerAsync();
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			InitWorker();
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
			Clear();
		}

		private void DoWork(object sender, DoWorkEventArgs e)
		{
			if (_worker.CancellationPending)
			{
				e.Cancel = true;
				return;
			}
			base.UseWaitCursor = true;
			DataTable dataTable = ultraGridQueryData.DataSource as DataTable;
			checked
			{
				try
				{
					bool flag = false;
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						DataRow detailsRow = dataTable.Rows[i];
						Invoke((MethodInvoker)delegate
						{
							Thread.Sleep(2);
							labelProcess.Text = detailsRow["VoucherID"].ToString();
						});
						try
						{
							SysDocTypes sysDocTypes = unchecked((SysDocTypes)int.Parse(detailsRow["SysDocType"].ToString()));
							switch (sysDocTypes)
							{
							case SysDocTypes.TransitTransferOut:
							case SysDocTypes.TransitTransferIn:
							case SysDocTypes.ReturnTransitTransfer:
							case SysDocTypes.DirectInventoryTransfer:
								flag = Factory.InventoryTransferSystem.ReCostTransferTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString(), sysDocTypes);
								break;
							case SysDocTypes.ConsignOut:
								flag = Factory.ConsignOutSystem.ReCostTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString());
								break;
							case SysDocTypes.ConsignOutSettlement:
								flag = Factory.ConsignOutSettlementSystem.ReCostTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString());
								break;
							case SysDocTypes.SalesInvoice:
							case SysDocTypes.SalesReceipt:
							case SysDocTypes.ExportSalesInvoice:
								Factory.DatabaseSystem.GetFieldValue("Journal", "JournalID", "SysDocID", detailsRow["SysDocID"], "VoucherID", detailsRow["VoucherID"]).ToString();
								flag = Factory.SalesInvoiceSystem.ReCostTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString());
								break;
							case SysDocTypes.SalesPOS:
								flag = Factory.SalesPOSSystem.ReCostTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString());
								break;
							case SysDocTypes.DeliveryNote:
								flag = Factory.DeliveryNoteSystem.ReCostTransaction(detailsRow["SysDocID"].ToString(), detailsRow["VoucherID"].ToString());
								break;
							default:
								throw new CompanyException("Document Type is not implemented:" + sysDocTypes.ToString());
							}
							ultraGridQueryData.Rows[i].Cells["Status"].Value = flag;
							if (flag)
							{
								SuccessCount++;
							}
							else
							{
								FailedCount++;
							}
						}
						catch (Exception ex)
						{
							new Thread(CloseIt).Start();
							MessageBox.Show(ex.ToString());
							FailedCount++;
						}
						Invoke((MethodInvoker)delegate
						{
							Thread.Sleep(2);
							labelSuccess.Text = SuccessCount.ToString();
							labelFailed.Text = FailedCount.ToString();
						});
					}
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
			}
		}

		public void CloseIt()
		{
			Thread.Sleep(2000);
			Interaction.AppActivate(Process.GetCurrentProcess().Id);
			SendKeys.SendWait(" ");
		}

		private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			ErrorHelper.InformationMessage("Recost Process Completed");
			base.UseWaitCursor = false;
		}

		private void Clear()
		{
			labelSuccess.Text = "0";
			labelFailed.Text = "0";
			labelTotal.Text = "0";
			SuccessCount = 0;
			FailedCount = 0;
			labelProcess.Text = "";
			ultraGridQueryData.DataSource = null;
			SetupGridUI();
		}

		private void buttonClear_Click_1(object sender, EventArgs e)
		{
			Clear();
		}

		private void buttonNewQuery_Click(object sender, EventArgs e)
		{
			Clear();
			AddTableDialog addTableDialog = new AddTableDialog();
			if (addTableDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					ultraGridQueryData.DataSource = addTableDialog.dataSetValue.Tables[0];
					labelTotal.Text = addTableDialog.dataSetValue.Tables[0].Rows.Count.ToString();
					ultraGridQueryData.SetupUI();
				}
				catch
				{
				}
			}
		}

		private void SetupGridUI()
		{
			ultraGridQueryData.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocType");
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Status");
			ultraGridQueryData.DataSource = dataTable;
			ultraGridQueryData.SetupUI();
		}

		private void UpdateSalesCost_FormClosing(object sender, FormClosingEventArgs e)
		{
			_worker.WorkerSupportsCancellation = true;
			_worker.CancelAsync();
			_worker.Dispose();
			_worker = null;
		}

		private void UpdateCost()
		{
			checked
			{
				try
				{
					DataTable dataTable = ultraGridQueryData.DataSource as DataTable;
					bool flag = false;
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						DataRow dataRow = dataTable.Rows[i];
						labelProcess.Text = dataRow["VoucherID"].ToString();
						SysDocTypes sysDocTypes = (SysDocTypes)dataRow["SysDocType"];
						if (sysDocTypes == SysDocTypes.TransitTransferOut || sysDocTypes == SysDocTypes.TransitTransferIn || sysDocTypes == SysDocTypes.ReturnTransitTransfer || sysDocTypes == SysDocTypes.DirectInventoryTransfer)
						{
							flag = Factory.InventoryTransferSystem.ReCostTransferTransaction(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), SysDocTypes.TransitTransferOut);
						}
						else
						{
							string s = Factory.DatabaseSystem.GetFieldValue("Journal", "JournalID", "SysDocID", dataRow["SysDocID"], "VoucherID", dataRow["VoucherID"]).ToString();
							flag = Factory.JournalSystem.MergeJournalDetailsForCOGSUpdate(int.Parse(s), mergeSysWithNormal: true);
							flag = Factory.SalesInvoiceSystem.ReCostTransaction(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString());
						}
						try
						{
							ultraGridQueryData.Rows[i].Cells["Status"].Value = flag;
						}
						catch
						{
						}
						if (flag)
						{
							SuccessCount++;
							labelSuccess.Text = SuccessCount.ToString();
						}
						else
						{
							FailedCount++;
							labelFailed.Text = FailedCount.ToString();
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Others.AxolonMaintenance.UpdateRepostCost));
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			labelTotal = new System.Windows.Forms.Label();
			labelFailed = new System.Windows.Forms.Label();
			labelSuccess = new System.Windows.Forms.Label();
			ultraGridQueryData = new Micromind.DataControls.DataEntryGrid();
			labelProcess = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			_worker = new System.ComponentModel.BackgroundWorker();
			groupBox1 = new System.Windows.Forms.GroupBox();
			buttonClear = new Micromind.UISupport.XPButton();
			buttonNewQuery = new Micromind.UISupport.XPButton();
			buttonUpdate = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			((System.ComponentModel.ISupportInitialize)ultraGridQueryData).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(6, 38);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 13);
			label1.TabIndex = 18;
			label1.Text = "Success :";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(6, 57);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(41, 13);
			label2.TabIndex = 19;
			label2.Text = "Failed :";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(6, 76);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(37, 13);
			label3.TabIndex = 20;
			label3.Text = "Total :";
			labelTotal.AutoSize = true;
			labelTotal.Location = new System.Drawing.Point(95, 77);
			labelTotal.Name = "labelTotal";
			labelTotal.Size = new System.Drawing.Size(13, 13);
			labelTotal.TabIndex = 23;
			labelTotal.Text = "0";
			labelFailed.AutoSize = true;
			labelFailed.Location = new System.Drawing.Point(95, 57);
			labelFailed.Name = "labelFailed";
			labelFailed.Size = new System.Drawing.Size(13, 13);
			labelFailed.TabIndex = 22;
			labelFailed.Text = "0";
			labelSuccess.AutoSize = true;
			labelSuccess.Location = new System.Drawing.Point(95, 39);
			labelSuccess.Name = "labelSuccess";
			labelSuccess.Size = new System.Drawing.Size(13, 13);
			labelSuccess.TabIndex = 21;
			labelSuccess.Text = "0";
			ultraGridQueryData.AllowAddNew = false;
			ultraGridQueryData.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ultraGridQueryData.DisplayLayout.Appearance = appearance;
			ultraGridQueryData.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ultraGridQueryData.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridQueryData.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridQueryData.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ultraGridQueryData.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ultraGridQueryData.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ultraGridQueryData.DisplayLayout.MaxColScrollRegions = 1;
			ultraGridQueryData.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridQueryData.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ultraGridQueryData.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ultraGridQueryData.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			ultraGridQueryData.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ultraGridQueryData.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ultraGridQueryData.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ultraGridQueryData.DisplayLayout.Override.CellAppearance = appearance8;
			ultraGridQueryData.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ultraGridQueryData.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ultraGridQueryData.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ultraGridQueryData.DisplayLayout.Override.HeaderAppearance = appearance10;
			ultraGridQueryData.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ultraGridQueryData.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ultraGridQueryData.DisplayLayout.Override.RowAppearance = appearance11;
			ultraGridQueryData.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ultraGridQueryData.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ultraGridQueryData.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ultraGridQueryData.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ultraGridQueryData.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			ultraGridQueryData.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ultraGridQueryData.ExitEditModeOnLeave = false;
			ultraGridQueryData.IncludeLotItems = false;
			ultraGridQueryData.LoadLayoutFailed = false;
			ultraGridQueryData.Location = new System.Drawing.Point(15, 19);
			ultraGridQueryData.MinimumSize = new System.Drawing.Size(622, 154);
			ultraGridQueryData.Name = "ultraGridQueryData";
			ultraGridQueryData.ShowClearMenu = true;
			ultraGridQueryData.ShowDeleteMenu = true;
			ultraGridQueryData.ShowInsertMenu = true;
			ultraGridQueryData.ShowMoveRowsMenu = true;
			ultraGridQueryData.Size = new System.Drawing.Size(694, 330);
			ultraGridQueryData.TabIndex = 25;
			ultraGridQueryData.Text = "dataEntryGrid1";
			labelProcess.AutoSize = true;
			labelProcess.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelProcess.Location = new System.Drawing.Point(95, 20);
			labelProcess.Name = "labelProcess";
			labelProcess.Size = new System.Drawing.Size(13, 13);
			labelProcess.TabIndex = 29;
			labelProcess.Text = "0";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(6, 19);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(88, 13);
			label5.TabIndex = 28;
			label5.Text = "Current Process :";
			groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(labelProcess);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(label2);
			groupBox1.Controls.Add(labelTotal);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(labelFailed);
			groupBox1.Controls.Add(labelSuccess);
			groupBox1.Location = new System.Drawing.Point(16, 351);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(247, 101);
			groupBox1.TabIndex = 31;
			groupBox1.TabStop = false;
			groupBox1.Text = "Status";
			buttonClear.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonClear.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClear.BackColor = System.Drawing.Color.DarkGray;
			buttonClear.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonClear.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonClear.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonClear.Location = new System.Drawing.Point(611, 471);
			buttonClear.Name = "buttonClear";
			buttonClear.Size = new System.Drawing.Size(96, 24);
			buttonClear.TabIndex = 32;
			buttonClear.Text = "&Clear";
			buttonClear.UseVisualStyleBackColor = false;
			buttonClear.Click += new System.EventHandler(buttonClear_Click_1);
			buttonNewQuery.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNewQuery.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonNewQuery.BackColor = System.Drawing.Color.DarkGray;
			buttonNewQuery.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNewQuery.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNewQuery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNewQuery.Location = new System.Drawing.Point(407, 471);
			buttonNewQuery.Name = "buttonNewQuery";
			buttonNewQuery.Size = new System.Drawing.Size(96, 24);
			buttonNewQuery.TabIndex = 33;
			buttonNewQuery.Text = "New Query";
			buttonNewQuery.UseVisualStyleBackColor = false;
			buttonNewQuery.Click += new System.EventHandler(buttonNewQuery_Click);
			buttonUpdate.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonUpdate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonUpdate.BackColor = System.Drawing.Color.DarkGray;
			buttonUpdate.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonUpdate.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonUpdate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonUpdate.Location = new System.Drawing.Point(509, 471);
			buttonUpdate.Name = "buttonUpdate";
			buttonUpdate.Size = new System.Drawing.Size(96, 24);
			buttonUpdate.TabIndex = 34;
			buttonUpdate.Text = "Update";
			buttonUpdate.UseVisualStyleBackColor = false;
			buttonUpdate.Click += new System.EventHandler(buttonUpdate_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 462);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(787, 1);
			line1.TabIndex = 35;
			line1.TabStop = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(723, 500);
			base.Controls.Add(line1);
			base.Controls.Add(buttonUpdate);
			base.Controls.Add(buttonNewQuery);
			base.Controls.Add(buttonClear);
			base.Controls.Add(groupBox1);
			base.Controls.Add(ultraGridQueryData);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "UpdateRepostCost";
			Text = "Update Repost Cost";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(UpdateSalesCost_FormClosing);
			((System.ComponentModel.ISupportInitialize)ultraGridQueryData).EndInit();
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
