using DevExpress.XtraReports.UI;
using Infragistics.Win.Misc;
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
	public class PDCIssuedReportForm : Form, IForm
	{
		private string selectedText = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private MMSDateTimePicker dateTimePickerDate;

		private Label label1;

		private BankSelector bankSelector;

		private CheckBox checkBoxCancelled;

		private CheckBox checkBoxBounced;

		private Label label2;

		private CheckBox checkBoxCleared;

		private DateControl dateControl1;

		private Label label3;

		private VendorSelector vendorSelector;

		private RadioButton radioButtonVendor;

		private RadioButton radioButtonChequeDate;

		private RadioButton radioButtonBank;

		private GroupBox groupBoxGroup;

		private UltraGroupBox ultraGroupBox2;

		private LocationSelector locationSelector;

		private Label label4;

		private DateControl dateControl2;

		private RadioButton radioButtonIssuedDate;

		private ComboBox comboBox1;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7003;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PDCIssuedReportForm()
		{
			InitializeComponent();
		}

		private void Test()
		{
			GetAllControls(this);
		}

		private List<Control> GetAllControls(Control container, List<Control> list)
		{
			foreach (Control control in container.Controls)
			{
				new List<string>();
				if (control is MultiColumnComboBox)
				{
					list.Add(control);
				}
				if (control is MultiColumnComboBox)
				{
					string text = control.AccessibilityObject.ToString();
					string[] separator = new string[1]
					{
						"ControlAccessibleObject: Owner = Micromind.DataControls."
					};
					string[] array = text.Split(separator, StringSplitOptions.None);
					if (array[1].GetTypeCode().ToString() != DataComboType.None.ToString())
					{
						Control[] array2 = base.Controls.Find(array[1], searchAllChildren: true);
						if (array2 != null && array2.Length != 0)
						{
							array2[0].Text = "Found!";
						}
					}
				}
				if (control.Controls.Count > 0)
				{
					list = GetAllControls(control, list);
				}
			}
			return list;
		}

		private List<Control> GetAllControls(Control container)
		{
			return GetAllControls(container, new List<Control>());
		}

		private void findControlsOfType(Type type, Control.ControlCollection formControls, ref List<Control> controls)
		{
			foreach (Control formControl in formControls)
			{
				if (formControl.GetType() == type)
				{
					controls.Add(formControl);
				}
				if (formControl.Controls.Count > 0)
				{
					findControlsOfType(type, formControl.Controls, ref controls);
				}
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				dateTimePickerDate.Value = dateControl1.ToDate;
				DateTime value = dateTimePickerDate.Value;
				value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
				string text = "";
				if (selectedText == "Vendor")
				{
					text = "1";
				}
				else if (selectedText == "ChequeDate")
				{
					text = "2";
				}
				else if (selectedText == "Bank")
				{
					text = "3";
				}
				else if (selectedText == "IssuedDate")
				{
					text = "4";
				}
				else if (text == "")
				{
					text = "1";
				}
				DataSet data = Factory.IssuedChequeSystem.GetIssuedChequeAsOfDate(dateControl2.FromDate, dateControl2.ToDate, dateControl1.FromDate, dateControl1.ToDate, bankSelector.FromBank.Trim(), bankSelector.ToBank.Trim(), vendorSelector.FromVendor, vendorSelector.ToVendor, vendorSelector.FromClass, vendorSelector.ToClass, vendorSelector.FromGroup, vendorSelector.ToGroup, locationSelector.FromLocation, locationSelector.ToLocation, checkBoxCleared.Checked, checkBoxBounced.Checked, checkBoxCancelled.Checked, text, vendorSelector.MultipleVendors);
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As Of : " + dateTimePickerDate.Value.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				XtraReport report = reportHelper.GetReport("PDC Issued");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'PDC Issued.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
				}
				else
				{
					report.DataSource = data;
					reportHelper.ShowReport(report);
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

		private void PDCIssuedReportForm_Load(object sender, EventArgs e)
		{
			try
			{
				dateTimePickerDate.Value = dateTimePickerDate.MaxDate;
				DatePeriods datePeriods = DatePeriods.ThisMonthToDate;
				int result = 0;
				int.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.DefaultDate.ToString(), DatePeriods.ThisMonthToDate).ToString(), out result);
				datePeriods = (DatePeriods)result;
				dateControl1.SelectedPeriod = datePeriods;
				dateControl2.SelectedPeriod = DatePeriods.AllDates;
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

		private void radioButton_CheckedChanged(object sender, EventArgs e)
		{
			if (((RadioButton)sender).Checked)
			{
				selectedText = ((RadioButton)sender).Text;
			}
			if (selectedText == "Vendor")
			{
				radioButtonBank.Checked = false;
				radioButtonChequeDate.Checked = false;
				radioButtonIssuedDate.Checked = false;
			}
			if (selectedText == "Bank")
			{
				radioButtonVendor.Checked = false;
				radioButtonChequeDate.Checked = false;
				radioButtonIssuedDate.Checked = false;
			}
			if (selectedText == "ChequeDate")
			{
				radioButtonVendor.Checked = false;
				radioButtonBank.Checked = false;
				radioButtonIssuedDate.Checked = false;
			}
			if (selectedText == "IssuedDate")
			{
				radioButtonVendor.Checked = false;
				radioButtonBank.Checked = false;
				radioButtonChequeDate.Checked = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.PDCIssuedReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			label1 = new System.Windows.Forms.Label();
			checkBoxCancelled = new System.Windows.Forms.CheckBox();
			checkBoxBounced = new System.Windows.Forms.CheckBox();
			label2 = new System.Windows.Forms.Label();
			checkBoxCleared = new System.Windows.Forms.CheckBox();
			label3 = new System.Windows.Forms.Label();
			radioButtonVendor = new System.Windows.Forms.RadioButton();
			radioButtonChequeDate = new System.Windows.Forms.RadioButton();
			radioButtonBank = new System.Windows.Forms.RadioButton();
			groupBoxGroup = new System.Windows.Forms.GroupBox();
			radioButtonIssuedDate = new System.Windows.Forms.RadioButton();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			label4 = new System.Windows.Forms.Label();
			dateControl2 = new Micromind.DataControls.DateControl();
			vendorSelector = new Micromind.DataControls.VendorSelector();
			dateControl1 = new Micromind.DataControls.DateControl();
			bankSelector = new Micromind.DataControls.BankSelector();
			comboBox1 = new System.Windows.Forms.ComboBox();
			panelButtons.SuspendLayout();
			groupBoxGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).BeginInit();
			ultraGroupBox2.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(271, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 519);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(481, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(481, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(371, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 443);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 5;
			label1.Text = "As of:";
			label1.Visible = false;
			checkBoxCancelled.AutoSize = true;
			checkBoxCancelled.Location = new System.Drawing.Point(392, 442);
			checkBoxCancelled.Name = "checkBoxCancelled";
			checkBoxCancelled.Size = new System.Drawing.Size(73, 17);
			checkBoxCancelled.TabIndex = 15;
			checkBoxCancelled.Text = "Cancelled";
			checkBoxCancelled.UseVisualStyleBackColor = true;
			checkBoxBounced.AutoSize = true;
			checkBoxBounced.Location = new System.Drawing.Point(319, 442);
			checkBoxBounced.Name = "checkBoxBounced";
			checkBoxBounced.Size = new System.Drawing.Size(69, 17);
			checkBoxBounced.TabIndex = 14;
			checkBoxBounced.Text = "Bounced";
			checkBoxBounced.UseVisualStyleBackColor = true;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(200, 442);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(45, 13);
			label2.TabIndex = 13;
			label2.Text = "Include:";
			checkBoxCleared.AutoSize = true;
			checkBoxCleared.Location = new System.Drawing.Point(251, 441);
			checkBoxCleared.Name = "checkBoxCleared";
			checkBoxCleared.Size = new System.Drawing.Size(62, 17);
			checkBoxCleared.TabIndex = 12;
			checkBoxCleared.Text = "Cleared";
			checkBoxCleared.UseVisualStyleBackColor = true;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(12, 283);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(67, 13);
			label3.TabIndex = 17;
			label3.Text = "Issued Date:";
			radioButtonVendor.AutoSize = true;
			radioButtonVendor.Checked = true;
			radioButtonVendor.Location = new System.Drawing.Point(88, 15);
			radioButtonVendor.Name = "radioButtonVendor";
			radioButtonVendor.Size = new System.Drawing.Size(59, 17);
			radioButtonVendor.TabIndex = 23;
			radioButtonVendor.TabStop = true;
			radioButtonVendor.Text = "Vendor";
			radioButtonVendor.UseVisualStyleBackColor = true;
			radioButtonVendor.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			radioButtonChequeDate.AutoSize = true;
			radioButtonChequeDate.Location = new System.Drawing.Point(159, 16);
			radioButtonChequeDate.Name = "radioButtonChequeDate";
			radioButtonChequeDate.Size = new System.Drawing.Size(85, 17);
			radioButtonChequeDate.TabIndex = 24;
			radioButtonChequeDate.Text = "ChequeDate";
			radioButtonChequeDate.UseVisualStyleBackColor = true;
			radioButtonChequeDate.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			radioButtonBank.AutoSize = true;
			radioButtonBank.Location = new System.Drawing.Point(259, 16);
			radioButtonBank.Name = "radioButtonBank";
			radioButtonBank.Size = new System.Drawing.Size(50, 17);
			radioButtonBank.TabIndex = 25;
			radioButtonBank.TabStop = true;
			radioButtonBank.Text = "Bank";
			radioButtonBank.UseVisualStyleBackColor = true;
			radioButtonBank.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			groupBoxGroup.Controls.Add(radioButtonIssuedDate);
			groupBoxGroup.Controls.Add(radioButtonChequeDate);
			groupBoxGroup.Controls.Add(radioButtonVendor);
			groupBoxGroup.Controls.Add(radioButtonBank);
			groupBoxGroup.Location = new System.Drawing.Point(12, 391);
			groupBoxGroup.Name = "groupBoxGroup";
			groupBoxGroup.Size = new System.Drawing.Size(436, 41);
			groupBoxGroup.TabIndex = 26;
			groupBoxGroup.TabStop = false;
			groupBoxGroup.Text = "GroupBy";
			radioButtonIssuedDate.AutoSize = true;
			radioButtonIssuedDate.Location = new System.Drawing.Point(333, 16);
			radioButtonIssuedDate.Name = "radioButtonIssuedDate";
			radioButtonIssuedDate.Size = new System.Drawing.Size(79, 17);
			radioButtonIssuedDate.TabIndex = 25;
			radioButtonIssuedDate.Text = "IssuedDate";
			radioButtonIssuedDate.UseVisualStyleBackColor = true;
			radioButtonIssuedDate.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(59, 440);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(113, 20);
			dateTimePickerDate.TabIndex = 0;
			dateTimePickerDate.Tag = "As Of Date";
			dateTimePickerDate.Value = new System.DateTime(2015, 1, 18, 17, 59, 0, 655);
			dateTimePickerDate.Visible = false;
			ultraGroupBox2.Controls.Add(locationSelector);
			ultraGroupBox2.Location = new System.Drawing.Point(12, 193);
			ultraGroupBox2.Name = "ultraGroupBox2";
			ultraGroupBox2.Size = new System.Drawing.Size(455, 81);
			ultraGroupBox2.TabIndex = 27;
			ultraGroupBox2.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(6, 18);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(12, 341);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(73, 13);
			label4.TabIndex = 29;
			label4.Text = "Cheque Date:";
			dateControl2.CustomReportFieldName = "";
			dateControl2.CustomReportKey = "";
			dateControl2.CustomReportValueType = 1;
			dateControl2.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl2.Location = new System.Drawing.Point(109, 340);
			dateControl2.Name = "dateControl2";
			dateControl2.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl2.Size = new System.Drawing.Size(331, 52);
			dateControl2.TabIndex = 28;
			dateControl2.Tag = "Cheque Date";
			dateControl2.ToDate = new System.DateTime(2017, 10, 29, 23, 59, 59, 59);
			vendorSelector.BackColor = System.Drawing.Color.Transparent;
			vendorSelector.CustomReportFieldName = "";
			vendorSelector.CustomReportKey = "";
			vendorSelector.CustomReportValueType = 1;
			vendorSelector.Location = new System.Drawing.Point(12, 67);
			vendorSelector.Name = "vendorSelector";
			vendorSelector.Size = new System.Drawing.Size(449, 124);
			vendorSelector.TabIndex = 18;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(109, 280);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(331, 52);
			dateControl1.TabIndex = 16;
			dateControl1.Tag = "Issued Date";
			dateControl1.ToDate = new System.DateTime(2017, 10, 29, 23, 59, 59, 59);
			bankSelector.BackColor = System.Drawing.Color.Transparent;
			bankSelector.CustomReportFieldName = "";
			bankSelector.CustomReportKey = "";
			bankSelector.CustomReportValueType = 1;
			bankSelector.Location = new System.Drawing.Point(12, 17);
			bankSelector.Name = "bankSelector";
			bankSelector.Size = new System.Drawing.Size(449, 48);
			bankSelector.TabIndex = 8;
			comboBox1.FormattingEnabled = true;
			comboBox1.Location = new System.Drawing.Point(251, 486);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new System.Drawing.Size(121, 21);
			comboBox1.TabIndex = 30;
			comboBox1.Visible = false;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(481, 559);
			base.Controls.Add(comboBox1);
			base.Controls.Add(label4);
			base.Controls.Add(dateControl2);
			base.Controls.Add(ultraGroupBox2);
			base.Controls.Add(vendorSelector);
			base.Controls.Add(label3);
			base.Controls.Add(dateControl1);
			base.Controls.Add(checkBoxCancelled);
			base.Controls.Add(checkBoxBounced);
			base.Controls.Add(label2);
			base.Controls.Add(checkBoxCleared);
			base.Controls.Add(bankSelector);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(panelButtons);
			base.Controls.Add(groupBoxGroup);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PDCIssuedReportForm";
			Text = "PDC Issued Report";
			base.Load += new System.EventHandler(PDCIssuedReportForm_Load);
			panelButtons.ResumeLayout(false);
			groupBoxGroup.ResumeLayout(false);
			groupBoxGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox2).EndInit();
			ultraGroupBox2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
