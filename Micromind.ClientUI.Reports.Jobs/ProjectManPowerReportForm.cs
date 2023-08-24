using DevExpress.XtraReports.UI;
using Infragistics.Win;
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
	public class ProjectManPowerReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private JobSelector jobCostSelector;

		private DateControl dateControl1;

		private JobComboBox ComboBoxJob;

		private EmployeeComboBox ComboBoxEmployee;

		private Label label1;

		private Label label2;

		private MMTextBox textBoxJob;

		private MMTextBox textBoxEmployee;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public ProjectManPowerReportForm()
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
			DataSet data = Factory.JobSystem.GetProjectManPowerReport(ComboBoxJob.SelectedID, ComboBoxEmployee.SelectedID, dateControl1.FromDate, dateControl1.ToDate);
			ReportHelper reportHelper = new ReportHelper();
			string reportFilter = "From:  To:";
			reportHelper.AddGeneralReportData(ref data, reportFilter);
			reportHelper.AddFilterData(ref data, GetAllFormControls(this));
			XtraReport report = reportHelper.GetReport("Project Man Power");
			if (report == null)
			{
				ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Project Man Power.repx'");
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Jobs.ProjectManPowerReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			dateControl1 = new Micromind.DataControls.DateControl();
			ComboBoxJob = new Micromind.DataControls.JobComboBox();
			ComboBoxEmployee = new Micromind.DataControls.EmployeeComboBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxJob = new Micromind.UISupport.MMTextBox();
			textBoxEmployee = new Micromind.UISupport.MMTextBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ComboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxEmployee).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(289, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 129);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(499, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(499, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(389, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 69);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(303, 50);
			dateControl1.TabIndex = 7;
			dateControl1.ToDate = new System.DateTime(2016, 3, 12, 23, 59, 59, 59);
			ComboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxJob.CustomReportFieldName = "";
			ComboBoxJob.CustomReportKey = "";
			ComboBoxJob.CustomReportValueType = 1;
			ComboBoxJob.DescriptionTextBox = textBoxJob;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxJob.DisplayLayout.Appearance = appearance;
			ComboBoxJob.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxJob.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxJob.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxJob.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			ComboBoxJob.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxJob.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			ComboBoxJob.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxJob.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxJob.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxJob.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			ComboBoxJob.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxJob.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxJob.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxJob.DisplayLayout.Override.CellAppearance = appearance8;
			ComboBoxJob.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxJob.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxJob.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			ComboBoxJob.DisplayLayout.Override.HeaderAppearance = appearance10;
			ComboBoxJob.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxJob.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			ComboBoxJob.DisplayLayout.Override.RowAppearance = appearance11;
			ComboBoxJob.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxJob.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			ComboBoxJob.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxJob.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxJob.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxJob.Editable = true;
			ComboBoxJob.FilterString = "";
			ComboBoxJob.HasAllAccount = false;
			ComboBoxJob.HasCustom = false;
			ComboBoxJob.IsDataLoaded = false;
			ComboBoxJob.Location = new System.Drawing.Point(80, 11);
			ComboBoxJob.MaxDropDownItems = 12;
			ComboBoxJob.Name = "ComboBoxJob";
			ComboBoxJob.ShowInactiveItems = false;
			ComboBoxJob.ShowQuickAdd = true;
			ComboBoxJob.Size = new System.Drawing.Size(94, 20);
			ComboBoxJob.TabIndex = 8;
			ComboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ComboBoxEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			ComboBoxEmployee.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			ComboBoxEmployee.CustomReportFieldName = "";
			ComboBoxEmployee.CustomReportKey = "";
			ComboBoxEmployee.CustomReportValueType = 1;
			ComboBoxEmployee.DescriptionTextBox = textBoxEmployee;
			appearance13.BackColor = System.Drawing.SystemColors.Window;
			appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			ComboBoxEmployee.DisplayLayout.Appearance = appearance13;
			ComboBoxEmployee.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			ComboBoxEmployee.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance14.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxEmployee.DisplayLayout.GroupByBox.Appearance = appearance14;
			appearance15.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxEmployee.DisplayLayout.GroupByBox.BandLabelAppearance = appearance15;
			ComboBoxEmployee.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance16.BackColor2 = System.Drawing.SystemColors.Control;
			appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance16.ForeColor = System.Drawing.SystemColors.GrayText;
			ComboBoxEmployee.DisplayLayout.GroupByBox.PromptAppearance = appearance16;
			ComboBoxEmployee.DisplayLayout.MaxColScrollRegions = 1;
			ComboBoxEmployee.DisplayLayout.MaxRowScrollRegions = 1;
			appearance17.BackColor = System.Drawing.SystemColors.Window;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			ComboBoxEmployee.DisplayLayout.Override.ActiveCellAppearance = appearance17;
			appearance18.BackColor = System.Drawing.SystemColors.Highlight;
			appearance18.ForeColor = System.Drawing.SystemColors.HighlightText;
			ComboBoxEmployee.DisplayLayout.Override.ActiveRowAppearance = appearance18;
			ComboBoxEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			ComboBoxEmployee.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			ComboBoxEmployee.DisplayLayout.Override.CardAreaAppearance = appearance19;
			appearance20.BorderColor = System.Drawing.Color.Silver;
			appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			ComboBoxEmployee.DisplayLayout.Override.CellAppearance = appearance20;
			ComboBoxEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			ComboBoxEmployee.DisplayLayout.Override.CellPadding = 0;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance21.BorderColor = System.Drawing.SystemColors.Window;
			ComboBoxEmployee.DisplayLayout.Override.GroupByRowAppearance = appearance21;
			appearance22.TextHAlignAsString = "Left";
			ComboBoxEmployee.DisplayLayout.Override.HeaderAppearance = appearance22;
			ComboBoxEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			ComboBoxEmployee.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance23.BackColor = System.Drawing.SystemColors.Window;
			appearance23.BorderColor = System.Drawing.Color.Silver;
			ComboBoxEmployee.DisplayLayout.Override.RowAppearance = appearance23;
			ComboBoxEmployee.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance24.BackColor = System.Drawing.SystemColors.ControlLight;
			ComboBoxEmployee.DisplayLayout.Override.TemplateAddRowAppearance = appearance24;
			ComboBoxEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			ComboBoxEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			ComboBoxEmployee.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			ComboBoxEmployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			ComboBoxEmployee.Editable = true;
			ComboBoxEmployee.FilterString = "";
			ComboBoxEmployee.HasAllAccount = false;
			ComboBoxEmployee.HasCustom = false;
			ComboBoxEmployee.IsDataLoaded = false;
			ComboBoxEmployee.Location = new System.Drawing.Point(80, 37);
			ComboBoxEmployee.MaxDropDownItems = 12;
			ComboBoxEmployee.Name = "ComboBoxEmployee";
			ComboBoxEmployee.ShowInactiveItems = false;
			ComboBoxEmployee.ShowQuickAdd = true;
			ComboBoxEmployee.ShowTerminatedEmployees = true;
			ComboBoxEmployee.Size = new System.Drawing.Size(94, 20);
			ComboBoxEmployee.TabIndex = 9;
			ComboBoxEmployee.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(24, 15);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(24, 13);
			label1.TabIndex = 10;
			label1.Text = "Job";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(21, 42);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(53, 13);
			label2.TabIndex = 11;
			label2.Text = "Employee";
			textBoxJob.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxJob.CustomReportFieldName = "";
			textBoxJob.CustomReportKey = "";
			textBoxJob.CustomReportValueType = 1;
			textBoxJob.IsComboTextBox = false;
			textBoxJob.Location = new System.Drawing.Point(180, 12);
			textBoxJob.Name = "textBoxJob";
			textBoxJob.ReadOnly = true;
			textBoxJob.Size = new System.Drawing.Size(302, 20);
			textBoxJob.TabIndex = 12;
			textBoxEmployee.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployee.CustomReportFieldName = "";
			textBoxEmployee.CustomReportKey = "";
			textBoxEmployee.CustomReportValueType = 1;
			textBoxEmployee.IsComboTextBox = false;
			textBoxEmployee.Location = new System.Drawing.Point(180, 38);
			textBoxEmployee.Name = "textBoxEmployee";
			textBoxEmployee.ReadOnly = true;
			textBoxEmployee.Size = new System.Drawing.Size(302, 20);
			textBoxEmployee.TabIndex = 13;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(499, 169);
			base.Controls.Add(textBoxEmployee);
			base.Controls.Add(textBoxJob);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(ComboBoxEmployee);
			base.Controls.Add(ComboBoxJob);
			base.Controls.Add(dateControl1);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "ProjectManPowerReportForm";
			Text = "Project Man Power";
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ComboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)ComboBoxEmployee).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
