using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.Customers
{
	public class SalesOrderDetailReport : Form, IForm
	{
		private bool useJobCosting = CompanyPreferences.UseJobCosting;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Button buttonClose;

		private JobComboBox comboBoxJob;

		private Label labeljob;

		private TextBox textBoxJobName;

		private UltraGroupBox ultraGroupBox1;

		private ProductSelector productSelector;

		private UltraGroupBox ultraGroupBox4;

		private LocationSelector locationSelector;

		private Label label2;

		private customersFlatComboBox comboBoxCustomer;

		private DateControl dateControl1;

		private CheckBox checkBoxOpenOrders;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsSales;

		public int ScreenID => 7029;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public SalesOrderDetailReport()
		{
			InitializeComponent();
			base.Load += Form_Load;
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				labeljob.Visible = useJobCosting;
				comboBoxJob.Visible = useJobCosting;
				textBoxJobName.Visible = useJobCosting;
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
			try
			{
				ReportHelper reportHelper = new ReportHelper();
				XtraReport xtraReport = null;
				DataSet data = Factory.SalesOrderSystem.GetSalesOrderDetailReport(dateControl1.FromDate, dateControl1.ToDate, comboBoxJob.SelectedID, comboBoxCustomer.SelectedID, productSelector.FromItem, productSelector.ToItem, productSelector.FromClass, productSelector.ToClass, productSelector.FromCategory, productSelector.ToCategory, productSelector.FromBrand, productSelector.ToBrand, locationSelector.FromLocation, locationSelector.ToLocation, checkBoxOpenOrders.Checked, productSelector.FromManufacturer, productSelector.ToManufacturer, productSelector.FromStyle, productSelector.ToStyle, productSelector.FromOrigin, productSelector.ToOrigin);
				xtraReport = reportHelper.GetReport("SO Detail");
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'SO Detail.repx'");
				}
				else
				{
					xtraReport.DataSource = data;
					reportHelper.ShowReport(xtraReport);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void buttonClose_Click(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Customers.SalesOrderDetailReport));
			buttonOK = new System.Windows.Forms.Button();
			buttonClose = new System.Windows.Forms.Button();
			textBoxJobName = new System.Windows.Forms.TextBox();
			labeljob = new System.Windows.Forms.Label();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			productSelector = new Micromind.DataControls.ProductSelector();
			comboBoxJob = new Micromind.DataControls.JobComboBox();
			ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			label2 = new System.Windows.Forms.Label();
			comboBoxCustomer = new Micromind.DataControls.customersFlatComboBox();
			dateControl1 = new Micromind.DataControls.DateControl();
			checkBoxOpenOrders = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxJob).BeginInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).BeginInit();
			ultraGroupBox4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).BeginInit();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(332, 389);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(448, 389);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 5;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			textBoxJobName.Location = new System.Drawing.Point(295, 39);
			textBoxJobName.Name = "textBoxJobName";
			textBoxJobName.ReadOnly = true;
			textBoxJobName.Size = new System.Drawing.Size(259, 20);
			textBoxJobName.TabIndex = 10;
			labeljob.AutoSize = true;
			labeljob.Location = new System.Drawing.Point(246, 15);
			labeljob.Name = "labeljob";
			labeljob.Size = new System.Drawing.Size(46, 13);
			labeljob.TabIndex = 7;
			labeljob.Text = "Project :";
			ultraGroupBox1.Controls.Add(productSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(9, 57);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(425, 204);
			ultraGroupBox1.TabIndex = 11;
			ultraGroupBox1.Text = "Items";
			productSelector.BackColor = System.Drawing.Color.Transparent;
			productSelector.CustomReportFieldName = "";
			productSelector.CustomReportKey = "";
			productSelector.CustomReportValueType = 1;
			productSelector.Location = new System.Drawing.Point(4, 17);
			productSelector.Name = "productSelector";
			productSelector.ShowOnlyAssemlbyItems = false;
			productSelector.Size = new System.Drawing.Size(415, 187);
			productSelector.TabIndex = 0;
			comboBoxJob.Assigned = false;
			comboBoxJob.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxJob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxJob.CustomReportFieldName = "";
			comboBoxJob.CustomReportKey = "";
			comboBoxJob.CustomReportValueType = 1;
			comboBoxJob.DescriptionTextBox = textBoxJobName;
			comboBoxJob.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxJob.Editable = true;
			comboBoxJob.FilterString = "";
			comboBoxJob.HasAllAccount = false;
			comboBoxJob.HasCustom = false;
			comboBoxJob.IsDataLoaded = false;
			comboBoxJob.Location = new System.Drawing.Point(295, 12);
			comboBoxJob.MaxDropDownItems = 12;
			comboBoxJob.Name = "comboBoxJob";
			comboBoxJob.ShowInactiveItems = false;
			comboBoxJob.ShowQuickAdd = true;
			comboBoxJob.Size = new System.Drawing.Size(163, 20);
			comboBoxJob.TabIndex = 6;
			comboBoxJob.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraGroupBox4.Controls.Add(locationSelector);
			ultraGroupBox4.Location = new System.Drawing.Point(33, 431);
			ultraGroupBox4.Name = "ultraGroupBox4";
			ultraGroupBox4.Size = new System.Drawing.Size(425, 76);
			ultraGroupBox4.TabIndex = 169;
			ultraGroupBox4.Text = "Locations";
			ultraGroupBox4.Visible = false;
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(436, 55);
			locationSelector.TabIndex = 0;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(-1, 15);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(54, 13);
			label2.TabIndex = 9;
			label2.Text = "Customer:";
			comboBoxCustomer.Assigned = false;
			comboBoxCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxCustomer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxCustomer.CustomReportFieldName = "";
			comboBoxCustomer.CustomReportKey = "";
			comboBoxCustomer.CustomReportValueType = 1;
			comboBoxCustomer.DescriptionTextBox = null;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxCustomer.DisplayLayout.Appearance = appearance;
			comboBoxCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxCustomer.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxCustomer.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxCustomer.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxCustomer.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxCustomer.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxCustomer.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxCustomer.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxCustomer.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxCustomer.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxCustomer.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxCustomer.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxCustomer.Editable = true;
			comboBoxCustomer.FilterString = "";
			comboBoxCustomer.FilterSysDocID = "";
			comboBoxCustomer.HasAll = false;
			comboBoxCustomer.HasCustom = false;
			comboBoxCustomer.IsDataLoaded = false;
			comboBoxCustomer.Location = new System.Drawing.Point(50, 12);
			comboBoxCustomer.MaxDropDownItems = 12;
			comboBoxCustomer.Name = "comboBoxCustomer";
			comboBoxCustomer.ShowConsignmentOnly = false;
			comboBoxCustomer.ShowInactive = false;
			comboBoxCustomer.ShowLPOCustomersOnly = false;
			comboBoxCustomer.ShowPROCustomersOnly = false;
			comboBoxCustomer.ShowQuickAdd = true;
			comboBoxCustomer.Size = new System.Drawing.Size(190, 20);
			comboBoxCustomer.TabIndex = 170;
			comboBoxCustomer.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2015, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 267);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(276, 54);
			dateControl1.TabIndex = 171;
			dateControl1.ToDate = new System.DateTime(2015, 11, 22, 23, 59, 59, 59);
			checkBoxOpenOrders.AutoSize = true;
			checkBoxOpenOrders.Location = new System.Drawing.Point(50, 337);
			checkBoxOpenOrders.Name = "checkBoxOpenOrders";
			checkBoxOpenOrders.Size = new System.Drawing.Size(86, 17);
			checkBoxOpenOrders.TabIndex = 172;
			checkBoxOpenOrders.Text = "Open Orders";
			checkBoxOpenOrders.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(559, 432);
			base.Controls.Add(checkBoxOpenOrders);
			base.Controls.Add(dateControl1);
			base.Controls.Add(comboBoxCustomer);
			base.Controls.Add(ultraGroupBox4);
			base.Controls.Add(textBoxJobName);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(label2);
			base.Controls.Add(labeljob);
			base.Controls.Add(comboBoxJob);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SalesOrderDetailReport";
			Text = "Sales Order Detail ";
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxJob).EndInit();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox4).EndInit();
			ultraGroupBox4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)comboBoxCustomer).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
