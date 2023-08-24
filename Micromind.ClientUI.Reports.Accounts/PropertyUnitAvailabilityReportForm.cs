using DevExpress.XtraReports.UI;
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

namespace Micromind.ClientUI.Reports.Accounts
{
	public class PropertyUnitAvailabilityReportForm : Form, IForm
	{
		private ScreenAccessRight screenRight;

		private IContainer components;

		private DateControl dateControl1;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private JobSelector jobCostSelector;

		private PropertySelector propertySelector1;

		private PropertyUnitSelector propertyUnitSelector1;

		private GroupBox groupBox2;

		private GroupBox groupBox3;

		private GroupBox groupBox1;

		private ComboBox cmbListBy;

		private Label label1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsPropertyRental;

		public int ScreenID => 7002;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PropertyUnitAvailabilityReportForm()
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
			try
			{
				ReportHelper reportHelper = new ReportHelper();
				XtraReport xtraReport = null;
				DataSet data = Factory.PropertySystem.GetPropertyUnitAvailabilityReport(dateControl1.FromDate, dateControl1.ToDate, propertyUnitSelector1.FromUnit, propertyUnitSelector1.ToUnit, propertySelector1.FromProperty, propertySelector1.ToProperty, cmbListBy.Text);
				xtraReport = reportHelper.GetReport("Property Unit Availability");
				string reportFilter = "From:" + dateControl1.FromDate.ToShortDateString() + "  To:" + dateControl1.ToDate.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				if (xtraReport == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "Please make sure you have access to reports path and the files are not corrupted.", "'Property Unit Availability.repx'");
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.PropertyUnitAvailabilityReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			propertyUnitSelector1 = new Micromind.DataControls.PropertyUnitSelector();
			propertySelector1 = new Micromind.DataControls.PropertySelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBox2 = new System.Windows.Forms.GroupBox();
			groupBox3 = new System.Windows.Forms.GroupBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			cmbListBy = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			panelButtons.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(269, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 307);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(479, 40);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(479, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(369, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			propertyUnitSelector1.BackColor = System.Drawing.Color.Transparent;
			propertyUnitSelector1.CustomReportFieldName = "";
			propertyUnitSelector1.CustomReportKey = "";
			propertyUnitSelector1.CustomReportValueType = 1;
			propertyUnitSelector1.Location = new System.Drawing.Point(6, 10);
			propertyUnitSelector1.Name = "propertyUnitSelector1";
			propertyUnitSelector1.Size = new System.Drawing.Size(428, 57);
			propertyUnitSelector1.TabIndex = 15;
			propertySelector1.BackColor = System.Drawing.Color.Transparent;
			propertySelector1.CustomReportFieldName = "";
			propertySelector1.CustomReportKey = "";
			propertySelector1.CustomReportValueType = 1;
			propertySelector1.Location = new System.Drawing.Point(3, 15);
			propertySelector1.Name = "propertySelector1";
			propertySelector1.Size = new System.Drawing.Size(428, 73);
			propertySelector1.TabIndex = 14;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(13, 245);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(447, 59);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 29, 23, 59, 59, 59);
			groupBox2.Controls.Add(propertySelector1);
			groupBox2.Location = new System.Drawing.Point(12, 13);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(453, 90);
			groupBox2.TabIndex = 17;
			groupBox2.TabStop = false;
			groupBox2.Text = "Property";
			groupBox3.Controls.Add(propertyUnitSelector1);
			groupBox3.Location = new System.Drawing.Point(12, 113);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new System.Drawing.Size(453, 64);
			groupBox3.TabIndex = 18;
			groupBox3.TabStop = false;
			groupBox3.Text = "Property Unit";
			groupBox1.Controls.Add(cmbListBy);
			groupBox1.Controls.Add(label1);
			groupBox1.Location = new System.Drawing.Point(12, 184);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(453, 53);
			groupBox1.TabIndex = 19;
			groupBox1.TabStop = false;
			groupBox1.Text = "Based On";
			cmbListBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbListBy.FormattingEnabled = true;
			cmbListBy.Items.AddRange(new object[3]
			{
				"All",
				"Vacant",
				"Occupied"
			});
			cmbListBy.Location = new System.Drawing.Point(91, 19);
			cmbListBy.Name = "cmbListBy";
			cmbListBy.Size = new System.Drawing.Size(181, 21);
			cmbListBy.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(14, 22);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(44, 13);
			label1.TabIndex = 0;
			label1.Text = "List By :";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(479, 347);
			base.Controls.Add(groupBox1);
			base.Controls.Add(groupBox3);
			base.Controls.Add(groupBox2);
			base.Controls.Add(panelButtons);
			base.Controls.Add(dateControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PropertyUnitAvailabilityReportForm";
			Text = "Property Unit Availability";
			panelButtons.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox3.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			ResumeLayout(false);
		}
	}
}
