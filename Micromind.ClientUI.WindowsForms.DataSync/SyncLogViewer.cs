using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataSync
{
	public class SyncLogViewer : Form
	{
		public DataSet dsSyncView;

		private DataSet data = new DataSet();

		private int SuccessCount;

		private int FailedCount;

		private IContainer components;

		private DataEntryGrid datagridLogView;

		private BackgroundWorker _worker;

		private Line line1;

		private TextBox textBoxProcessed;

		private TextBox textBoxSuccess;

		private TextBox textBoxFailed;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMLabel mmLabel3;

		private Button button1;

		public DataSet SyncData
		{
			get
			{
				return (DataSet)datagridLogView.DataSource;
			}
			set
			{
				datagridLogView.DataSource = value;
			}
		}

		public SyncLogViewer()
		{
			InitializeComponent();
			SetupGridUI();
			base.Load += SyncLogViewer_Load;
		}

		private void InitWorker()
		{
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
		}

		private void buttonClear_Click(object sender, EventArgs e)
		{
		}

		private void SyncLogViewer_Load(object sender, EventArgs e)
		{
			try
			{
				if (SyncData != null)
				{
					datagridLogView.DataSource = SyncData;
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("SysDocID"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["SysDocID"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("SyncActvityID"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["SyncActvityID"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("SyncActivityType"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["SyncActivityType"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("UserID"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["UserID"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("MachineID"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["MachineID"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("ActivityDataView"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["ActivityDataView"].Hidden = true;
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("SyncLogDate"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["SyncLogDate"].Format = "dd/MM/yyyy HH:mm:ss";
					}
					if (SyncData.Tables["Sync_Activity"].Columns.Contains("Description"))
					{
						datagridLogView.DisplayLayout.Bands[0].Columns["Description"].Width = checked(60 * datagridLogView.Width) / 100;
						datagridLogView.DisplayLayout.Bands[0].Columns["Description"].CellMultiLine = DefaultableBoolean.True;
					}
					datagridLogView.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
					datagridLogView.DisplayLayout.Bands[0].Columns["EntityID"].Header.Caption = "TransactionNumber";
					if (SyncData.Tables["Sync_Summary"].Columns.Contains("Totalprocessed"))
					{
						textBoxProcessed.Text = SyncData.Tables["Sync_Summary"].Rows[0]["Totalprocessed"].ToString();
						textBoxFailed.Text = SyncData.Tables["Sync_Summary"].Rows[0]["Failed"].ToString();
						textBoxSuccess.Text = SyncData.Tables["Sync_Summary"].Rows[0]["Success"].ToString();
					}
				}
			}
			catch (Exception e2)
			{
				datagridLogView.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonClear_Click_1(object sender, EventArgs e)
		{
		}

		private void buttonNewQuery_Click(object sender, EventArgs e)
		{
		}

		private void SetupGridUI()
		{
			datagridLogView.DisplayLayout.Bands[0].Columns.ClearUnbound();
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("SysDocType");
			dataTable.Columns.Add("SysDocID");
			dataTable.Columns.Add("VoucherID");
			dataTable.Columns.Add("Status");
			datagridLogView.DataSource = dataTable;
			datagridLogView.SetupUI();
		}

		private void SyncLogViewer_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			new DataExportHelper().ExportToExcel(datagridLogView, "Syn Log" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString());
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataSync.SyncLogViewer));
			_worker = new System.ComponentModel.BackgroundWorker();
			line1 = new Micromind.UISupport.Line();
			datagridLogView = new Micromind.DataControls.DataEntryGrid();
			textBoxProcessed = new System.Windows.Forms.TextBox();
			textBoxSuccess = new System.Windows.Forms.TextBox();
			textBoxFailed = new System.Windows.Forms.TextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)datagridLogView).BeginInit();
			SuspendLayout();
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 446);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(787, 1);
			line1.TabIndex = 35;
			line1.TabStop = false;
			datagridLogView.AllowAddNew = false;
			datagridLogView.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			datagridLogView.DisplayLayout.Appearance = appearance;
			datagridLogView.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			datagridLogView.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			datagridLogView.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			datagridLogView.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			datagridLogView.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			datagridLogView.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			datagridLogView.DisplayLayout.MaxColScrollRegions = 1;
			datagridLogView.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			datagridLogView.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			datagridLogView.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			datagridLogView.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			datagridLogView.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			datagridLogView.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			datagridLogView.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			datagridLogView.DisplayLayout.Override.CellAppearance = appearance8;
			datagridLogView.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			datagridLogView.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			datagridLogView.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			datagridLogView.DisplayLayout.Override.HeaderAppearance = appearance10;
			datagridLogView.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			datagridLogView.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			datagridLogView.DisplayLayout.Override.RowAppearance = appearance11;
			datagridLogView.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			datagridLogView.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			datagridLogView.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			datagridLogView.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			datagridLogView.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			datagridLogView.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			datagridLogView.ExitEditModeOnLeave = false;
			datagridLogView.IncludeLotItems = false;
			datagridLogView.LoadLayoutFailed = false;
			datagridLogView.Location = new System.Drawing.Point(15, 152);
			datagridLogView.MinimumSize = new System.Drawing.Size(622, 154);
			datagridLogView.Name = "datagridLogView";
			datagridLogView.ShowClearMenu = true;
			datagridLogView.ShowDeleteMenu = true;
			datagridLogView.ShowInsertMenu = true;
			datagridLogView.ShowMoveRowsMenu = true;
			datagridLogView.Size = new System.Drawing.Size(694, 288);
			datagridLogView.TabIndex = 25;
			datagridLogView.Text = "dataEntryGrid1";
			textBoxProcessed.Location = new System.Drawing.Point(112, 35);
			textBoxProcessed.Name = "textBoxProcessed";
			textBoxProcessed.Size = new System.Drawing.Size(134, 20);
			textBoxProcessed.TabIndex = 36;
			textBoxSuccess.Location = new System.Drawing.Point(112, 61);
			textBoxSuccess.Name = "textBoxSuccess";
			textBoxSuccess.Size = new System.Drawing.Size(134, 20);
			textBoxSuccess.TabIndex = 37;
			textBoxFailed.Location = new System.Drawing.Point(112, 87);
			textBoxFailed.Name = "textBoxFailed";
			textBoxFailed.Size = new System.Drawing.Size(134, 20);
			textBoxFailed.TabIndex = 38;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(21, 38);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(57, 13);
			mmLabel1.TabIndex = 39;
			mmLabel1.Text = "Processed";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(21, 68);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(48, 13);
			mmLabel2.TabIndex = 40;
			mmLabel2.Text = "Success";
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(21, 93);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(35, 13);
			mmLabel3.TabIndex = 41;
			mmLabel3.Text = "Failed";
			button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button1.Location = new System.Drawing.Point(24, 453);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 42;
			button1.Text = "Export Excel";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(723, 484);
			base.Controls.Add(button1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(textBoxFailed);
			base.Controls.Add(textBoxSuccess);
			base.Controls.Add(textBoxProcessed);
			base.Controls.Add(line1);
			base.Controls.Add(datagridLogView);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "SyncLogViewer";
			Text = "Log Viewer";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(SyncLogViewer_FormClosing);
			((System.ComponentModel.ISupportInitialize)datagridLogView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
