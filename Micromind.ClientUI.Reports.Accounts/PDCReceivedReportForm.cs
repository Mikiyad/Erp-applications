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
	public class PDCReceivedReportForm : Form, IForm
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

		private CheckBox checkBoxCleared;

		private CustomerSelector customerSelector;

		private BankSelector bankSelector;

		private Label label2;

		private CheckBox checkBoxBounced;

		private CheckBox checkBoxCancelled;

		private Label label3;

		private DateControl dateControl1;

		private GroupBox groupBoxGroup;

		private RadioButton radioButtonChequeDate;

		private RadioButton radioButtonCustomer;

		private RadioButton radioButtonBank;

		private CheckBox checkBoxDiscounted;

		private CheckBox checkBoxSenttoBank;

		private UltraGroupBox ultraGroupBox1;

		private LocationSelector locationSelector;

		private Label label4;

		private DateControl dateControl2;

		private RadioButton radioButtonReceiptDate;

		public ScreenAreas ScreenArea => ScreenAreas.ReportsAccounts;

		public int ScreenID => 7003;

		public ScreenTypes ScreenType => ScreenTypes.Report;

		public PDCReceivedReportForm()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				dateTimePickerDate.Value = dateControl1.ToDate;
				DateTime value = dateTimePickerDate.Value;
				value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
				string text = "";
				if (selectedText == "Customer")
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
				else if (selectedText == "ReceiptDate")
				{
					text = "4";
				}
				else if (text == "")
				{
					text = "1";
				}
				DataSet data = Factory.ReceivedChequeSystem.GetReceivedChequeAsOfDate(dateControl2.FromDate, dateControl2.ToDate, dateControl1.FromDate, dateControl1.ToDate, bankSelector.FromBank, bankSelector.ToBank, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, locationSelector.FromLocation, locationSelector.ToLocation, checkBoxCleared.Checked, checkBoxBounced.Checked, checkBoxCancelled.Checked, checkBoxDiscounted.Checked, checkBoxSenttoBank.Checked, text, customerSelector.MultipleCustomers);
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "As Of : " + dateTimePickerDate.Value.ToShortDateString();
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				XtraReport report = reportHelper.GetReport("PDC Received");
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'PDC Received.repx'", "Please make sure you have access to reports path and the files are not corrupted.");
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
			if (selectedText == "Customer")
			{
				radioButtonBank.Checked = false;
				radioButtonChequeDate.Checked = false;
				radioButtonReceiptDate.Checked = false;
			}
			if (selectedText == "Bank")
			{
				radioButtonCustomer.Checked = false;
				radioButtonChequeDate.Checked = false;
				radioButtonReceiptDate.Checked = false;
			}
			if (selectedText == "ChequeDate")
			{
				radioButtonCustomer.Checked = false;
				radioButtonBank.Checked = false;
				radioButtonReceiptDate.Checked = false;
			}
			if (selectedText == "ReceiptDate")
			{
				radioButtonCustomer.Checked = false;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.Accounts.PDCReceivedReportForm));
			buttonOK = new System.Windows.Forms.Button();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			dateTimePickerDate = new Micromind.UISupport.MMSDateTimePicker(components);
			label1 = new System.Windows.Forms.Label();
			checkBoxCleared = new System.Windows.Forms.CheckBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			bankSelector = new Micromind.DataControls.BankSelector();
			label2 = new System.Windows.Forms.Label();
			checkBoxBounced = new System.Windows.Forms.CheckBox();
			checkBoxCancelled = new System.Windows.Forms.CheckBox();
			label3 = new System.Windows.Forms.Label();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBoxGroup = new System.Windows.Forms.GroupBox();
			radioButtonReceiptDate = new System.Windows.Forms.RadioButton();
			radioButtonBank = new System.Windows.Forms.RadioButton();
			radioButtonChequeDate = new System.Windows.Forms.RadioButton();
			radioButtonCustomer = new System.Windows.Forms.RadioButton();
			checkBoxDiscounted = new System.Windows.Forms.CheckBox();
			checkBoxSenttoBank = new System.Windows.Forms.CheckBox();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			locationSelector = new Micromind.DataControls.LocationSelector();
			label4 = new System.Windows.Forms.Label();
			dateControl2 = new Micromind.DataControls.DateControl();
			panelButtons.SuspendLayout();
			groupBoxGroup.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(262, 8);
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
			panelButtons.Location = new System.Drawing.Point(0, 535);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(472, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(472, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(362, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(59, 486);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(113, 20);
			dateTimePickerDate.TabIndex = 0;
			dateTimePickerDate.Value = new System.DateTime(2015, 1, 18, 17, 59, 0, 655);
			dateTimePickerDate.Visible = false;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(21, 489);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(34, 13);
			label1.TabIndex = 5;
			label1.Text = "As of:";
			label1.Visible = false;
			checkBoxCleared.AutoSize = true;
			checkBoxCleared.Location = new System.Drawing.Point(229, 488);
			checkBoxCleared.Name = "checkBoxCleared";
			checkBoxCleared.Size = new System.Drawing.Size(62, 17);
			checkBoxCleared.TabIndex = 2;
			checkBoxCleared.Text = "Cleared";
			checkBoxCleared.UseVisualStyleBackColor = true;
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(12, 71);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(428, 156);
			customerSelector.TabIndex = 7;
			bankSelector.BackColor = System.Drawing.Color.Transparent;
			bankSelector.CustomReportFieldName = "";
			bankSelector.CustomReportKey = "";
			bankSelector.CustomReportValueType = 1;
			bankSelector.Location = new System.Drawing.Point(12, 17);
			bankSelector.Name = "bankSelector";
			bankSelector.Size = new System.Drawing.Size(430, 48);
			bankSelector.TabIndex = 8;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(178, 489);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(45, 13);
			label2.TabIndex = 9;
			label2.Text = "Include:";
			checkBoxBounced.AutoSize = true;
			checkBoxBounced.Checked = true;
			checkBoxBounced.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxBounced.Location = new System.Drawing.Point(306, 488);
			checkBoxBounced.Name = "checkBoxBounced";
			checkBoxBounced.Size = new System.Drawing.Size(69, 17);
			checkBoxBounced.TabIndex = 10;
			checkBoxBounced.Text = "Bounced";
			checkBoxBounced.UseVisualStyleBackColor = true;
			checkBoxCancelled.AutoSize = true;
			checkBoxCancelled.Location = new System.Drawing.Point(383, 488);
			checkBoxCancelled.Name = "checkBoxCancelled";
			checkBoxCancelled.Size = new System.Drawing.Size(73, 17);
			checkBoxCancelled.TabIndex = 11;
			checkBoxCancelled.Text = "Cancelled";
			checkBoxCancelled.UseVisualStyleBackColor = true;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(21, 320);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(73, 13);
			label3.TabIndex = 19;
			label3.Text = "Receipt Date:";
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(107, 320);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(331, 52);
			dateControl1.TabIndex = 18;
			dateControl1.ToDate = new System.DateTime(2017, 11, 19, 23, 59, 59, 59);
			groupBoxGroup.Controls.Add(radioButtonReceiptDate);
			groupBoxGroup.Controls.Add(radioButtonBank);
			groupBoxGroup.Controls.Add(radioButtonChequeDate);
			groupBoxGroup.Controls.Add(radioButtonCustomer);
			groupBoxGroup.Location = new System.Drawing.Point(33, 438);
			groupBoxGroup.Name = "groupBoxGroup";
			groupBoxGroup.Size = new System.Drawing.Size(405, 41);
			groupBoxGroup.TabIndex = 27;
			groupBoxGroup.TabStop = false;
			groupBoxGroup.Text = "GroupBy";
			radioButtonReceiptDate.AutoSize = true;
			radioButtonReceiptDate.Location = new System.Drawing.Point(310, 16);
			radioButtonReceiptDate.Name = "radioButtonReceiptDate";
			radioButtonReceiptDate.Size = new System.Drawing.Size(85, 17);
			radioButtonReceiptDate.TabIndex = 27;
			radioButtonReceiptDate.Text = "ReceiptDate";
			radioButtonReceiptDate.UseVisualStyleBackColor = true;
			radioButtonReceiptDate.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			radioButtonBank.AutoSize = true;
			radioButtonBank.Location = new System.Drawing.Point(250, 16);
			radioButtonBank.Name = "radioButtonBank";
			radioButtonBank.Size = new System.Drawing.Size(50, 17);
			radioButtonBank.TabIndex = 26;
			radioButtonBank.Text = "Bank";
			radioButtonBank.UseVisualStyleBackColor = true;
			radioButtonBank.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			radioButtonChequeDate.AutoSize = true;
			radioButtonChequeDate.Location = new System.Drawing.Point(159, 16);
			radioButtonChequeDate.Name = "radioButtonChequeDate";
			radioButtonChequeDate.Size = new System.Drawing.Size(85, 17);
			radioButtonChequeDate.TabIndex = 24;
			radioButtonChequeDate.Text = "ChequeDate";
			radioButtonChequeDate.UseVisualStyleBackColor = true;
			radioButtonChequeDate.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			radioButtonCustomer.AutoSize = true;
			radioButtonCustomer.Checked = true;
			radioButtonCustomer.Location = new System.Drawing.Point(88, 15);
			radioButtonCustomer.Name = "radioButtonCustomer";
			radioButtonCustomer.Size = new System.Drawing.Size(69, 17);
			radioButtonCustomer.TabIndex = 23;
			radioButtonCustomer.TabStop = true;
			radioButtonCustomer.Text = "Customer";
			radioButtonCustomer.UseVisualStyleBackColor = true;
			radioButtonCustomer.CheckedChanged += new System.EventHandler(radioButton_CheckedChanged);
			checkBoxDiscounted.AutoSize = true;
			checkBoxDiscounted.Checked = true;
			checkBoxDiscounted.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxDiscounted.Location = new System.Drawing.Point(229, 512);
			checkBoxDiscounted.Name = "checkBoxDiscounted";
			checkBoxDiscounted.Size = new System.Drawing.Size(80, 17);
			checkBoxDiscounted.TabIndex = 28;
			checkBoxDiscounted.Text = "Discounted";
			checkBoxDiscounted.UseVisualStyleBackColor = true;
			checkBoxSenttoBank.AutoSize = true;
			checkBoxSenttoBank.Checked = true;
			checkBoxSenttoBank.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxSenttoBank.Location = new System.Drawing.Point(308, 512);
			checkBoxSenttoBank.Name = "checkBoxSenttoBank";
			checkBoxSenttoBank.Size = new System.Drawing.Size(88, 17);
			checkBoxSenttoBank.TabIndex = 29;
			checkBoxSenttoBank.Text = "Sent to Bank";
			checkBoxSenttoBank.UseVisualStyleBackColor = true;
			ultraGroupBox1.Controls.Add(locationSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(9, 233);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(429, 81);
			ultraGroupBox1.TabIndex = 31;
			ultraGroupBox1.Text = "Location";
			locationSelector.BackColor = System.Drawing.Color.Transparent;
			locationSelector.CustomReportFieldName = "";
			locationSelector.CustomReportKey = "";
			locationSelector.CustomReportValueType = 1;
			locationSelector.Location = new System.Drawing.Point(7, 19);
			locationSelector.Name = "locationSelector";
			locationSelector.Size = new System.Drawing.Size(412, 59);
			locationSelector.TabIndex = 7;
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(21, 378);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(73, 13);
			label4.TabIndex = 33;
			label4.Text = "Cheque Date:";
			dateControl2.CustomReportFieldName = "";
			dateControl2.CustomReportKey = "";
			dateControl2.CustomReportValueType = 1;
			dateControl2.FromDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
			dateControl2.Location = new System.Drawing.Point(111, 378);
			dateControl2.Name = "dateControl2";
			dateControl2.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl2.Size = new System.Drawing.Size(331, 52);
			dateControl2.TabIndex = 32;
			dateControl2.ToDate = new System.DateTime(2017, 11, 19, 23, 59, 59, 59);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(472, 575);
			base.Controls.Add(label4);
			base.Controls.Add(dateControl2);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(checkBoxSenttoBank);
			base.Controls.Add(checkBoxDiscounted);
			base.Controls.Add(groupBoxGroup);
			base.Controls.Add(label3);
			base.Controls.Add(dateControl1);
			base.Controls.Add(checkBoxCancelled);
			base.Controls.Add(checkBoxBounced);
			base.Controls.Add(label2);
			base.Controls.Add(bankSelector);
			base.Controls.Add(customerSelector);
			base.Controls.Add(checkBoxCleared);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "PDCReceivedReportForm";
			Text = "PDC Received Report";
			base.Load += new System.EventHandler(PDCIssuedReportForm_Load);
			panelButtons.ResumeLayout(false);
			groupBoxGroup.ResumeLayout(false);
			groupBoxGroup.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
