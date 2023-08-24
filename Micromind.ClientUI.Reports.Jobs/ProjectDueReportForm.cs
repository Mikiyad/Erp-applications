using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Jobs
{
	public class ProjectDueReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private GroupBox groupBoxJob;

		private JobSelector jobCostSelector;

		private JobSelector jobSelector;

		private DateControl dateControl1;

		private UltraGroupBox ultraGroupBox1;

		private CustomerSelector customerSelector;

		private JobTypeComboBox comboBoxJobType;

		private MMLabel mmLabel5;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProjectDueReportForm()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			DataSet data = Factory.JobSystem.GetProjectDueReport(jobSelector.FromJob, jobSelector.ToJob, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, dateControl1.FromDate, dateControl1.ToDate, comboBoxJobType.SelectedID, customerSelector.MultipleCustomers);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:  To:";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Project Due Billing");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Project Due Billing.repx'");
				return;
			}
			report.DataSource = data;
			reportHelper.ShowReport(report);
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void dateControl1_Load(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Jobs.ProjectDueReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			groupBoxJob = new System.Windows.Forms.GroupBox();
			jobSelector = new Micromind.DataControls.JobSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			comboBoxJobType = new Micromind.DataControls.JobTypeComboBox();
			mmLabel5 = new Micromind.UISupport.MMLabel();
			panelButtons.SuspendLayout();
			groupBoxJob.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJobType).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(296, 8);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(96, 24);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonOK);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 352);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(506, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(506, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(396, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			groupBoxJob.Controls.Add(jobSelector);
			groupBoxJob.Location = new System.Drawing.Point(14, 21);
			groupBoxJob.Name = "groupBoxJob";
			groupBoxJob.Size = new System.Drawing.Size(449, 69);
			groupBoxJob.TabIndex = 6;
			groupBoxJob.TabStop = false;
			groupBoxJob.Text = "Jobs";
			jobSelector.BackColor = System.Drawing.Color.Transparent;
			jobSelector.CustomReportFieldName = "";
			jobSelector.CustomReportKey = "";
			jobSelector.CustomReportValueType = 1;
			jobSelector.Location = new System.Drawing.Point(6, 17);
			jobSelector.Name = "jobSelector";
			jobSelector.Size = new System.Drawing.Size(429, 46);
			jobSelector.TabIndex = 0;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(193, 287);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(281, 56);
			dateControl1.TabIndex = 7;
			dateControl1.ToDate = new System.DateTime(2017, 10, 26, 23, 59, 59, 59);
			dateControl1.Load += new System.EventHandler(dateControl1_Load);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(14, 97);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(449, 185);
			ultraGroupBox1.TabIndex = 8;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 16);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(436, 166);
			customerSelector.TabIndex = 0;
			comboBoxJobType.Assigned = false;
			comboBoxJobType.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJobType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJobType.CustomReportFieldName = "";
			comboBoxJobType.CustomReportKey = "";
			comboBoxJobType.CustomReportValueType = 1;
			comboBoxJobType.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxJobType.DisplayLayout.Appearance = appearance;
			comboBoxJobType.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxJobType.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJobType.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJobType.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxJobType.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxJobType.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxJobType.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxJobType.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxJobType.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxJobType.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxJobType.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxJobType.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxJobType.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxJobType.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxJobType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxJobType.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxJobType.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxJobType.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxJobType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxJobType.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxJobType.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxJobType.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxJobType.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxJobType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxJobType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxJobType.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxJobType.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJobType.Editable = true;
			comboBoxJobType.FilterString = "";
			comboBoxJobType.HasAllAccount = false;
			comboBoxJobType.HasCustom = false;
			comboBoxJobType.IsDataLoaded = false;
			comboBoxJobType.Location = new System.Drawing.Point(57, 292);
			comboBoxJobType.MaxDropDownItems = 12;
			comboBoxJobType.Name = "comboBoxJobType";
			comboBoxJobType.ShowInactiveItems = false;
			comboBoxJobType.ShowQuickAdd = true;
			comboBoxJobType.Size = new System.Drawing.Size(128, 20);
			comboBoxJobType.TabIndex = 9;
			comboBoxJobType.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			mmLabel5.AutoSize = true;
			mmLabel5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel5.IsFieldHeader = false;
			mmLabel5.IsRequired = false;
			mmLabel5.Location = new System.Drawing.Point(17, 295);
			mmLabel5.Name = "mmLabel5";
			mmLabel5.PenWidth = 1f;
			mmLabel5.ShowBorder = false;
			mmLabel5.Size = new System.Drawing.Size(34, 13);
			mmLabel5.TabIndex = 50;
			mmLabel5.Text = "Type:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(506, 392);
			base.Controls.Add(mmLabel5);
			base.Controls.Add(comboBoxJobType);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(dateControl1);
			base.Controls.Add(groupBoxJob);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProjectDueReportForm";
			Text = "Project Due for Billing";
			panelButtons.ResumeLayout(false);
			groupBoxJob.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxJobType).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
