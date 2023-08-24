using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.DataControls.Libraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class DateBetween : UserControl, ICustomReportControlPlus, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private string crKey1 = "";

		private byte crValueType = 1;

		private DateTime dtStartDate = new DateTime(DateTime.Now.Year, 1, 1);

		private DateTime dtEndDate = new DateTime(DateTime.Now.Year, 12, 31);

		private IContainer components;

		private DateTimePicker dateTimePickerFrom;

		private Label label1;

		private ComboBox comboBoxDateRange;

		private Panel panelCustom;

		private DateTimePicker dateTimePickerTo;

		private Label label2;

		private Label label3;

		public string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public string CustomReportKey1
		{
			get
			{
				return crKey1;
			}
			set
			{
				crKey1 = value;
			}
		}

		public byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
			}
		}

		public bool IsAllDates => comboBoxDateRange.Text == "All Dates";

		public DatePeriods SelectedPeriod
		{
			get
			{
				if (comboBoxDateRange.SelectedItem == null)
				{
					return DatePeriods.ThisMonthToDate;
				}
				return (DatePeriods)int.Parse((comboBoxDateRange.SelectedItem as ComboData).ID);
			}
			set
			{
				foreach (ComboData item in comboBoxDateRange.Items)
				{
					if (int.Parse(item.ID) == (int)value)
					{
						comboBoxDateRange.SelectedItem = item;
						break;
					}
				}
			}
		}

		public DateTime FromDate
		{
			get
			{
				return HelperLib.CreateStartDate(dateTimePickerFrom.Value);
			}
			set
			{
				dateTimePickerFrom.Value = value;
			}
		}

		public DateTime ToDate
		{
			get
			{
				return HelperLib.CreateEndDate(dateTimePickerTo.Value);
			}
			set
			{
				dateTimePickerTo.Value = value;
			}
		}

		public DateBetween()
		{
			InitializeComponent();
			comboBoxDateRange.SelectedIndexChanged += comboBoxDateRange_SelectedIndexChanged;
		}

		public string GetParameterValue()
		{
			if (crValueType == 1)
			{
				return "'" + StoreConfiguration.ToSqlDateTimeString(FromDate) + "'";
			}
			if (crFieldName == "")
			{
				return " ''='' ";
			}
			return crFieldName + " BETWEEN '" + StoreConfiguration.ToSqlDateTimeString(FromDate) + "' AND '" + StoreConfiguration.ToSqlDateTimeString(ToDate) + "' ";
		}

		public string GetParameterValue1()
		{
			if (crValueType == 1)
			{
				return "'" + StoreConfiguration.ToSqlDateTimeString(ToDate) + "'";
			}
			return " ''='' ";
		}

		private void comboBoxDateRange_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				panelCustom.Enabled = false;
				dateTimePickerFrom.Enabled = true;
				if (SelectedPeriod == DatePeriods.Custom)
				{
					panelCustom.Enabled = true;
				}
				else
				{
					DateRangeStruct dateRange = HelperLib.GetDateRange(SelectedPeriod);
					if (dateRange.From < dateTimePickerFrom.MinDate)
					{
						dateTimePickerFrom.Value = dateTimePickerFrom.MinDate;
					}
					else
					{
						dateTimePickerFrom.Value = dateRange.From;
					}
					if (dateRange.To > dateTimePickerTo.MaxDate)
					{
						dateTimePickerTo.Value = dateTimePickerTo.MaxDate;
					}
					else
					{
						dateTimePickerTo.Value = dateRange.To;
					}
				}
				if (SelectedPeriod == DatePeriods.DateEqualTo)
				{
					panelCustom.Enabled = true;
					dateTimePickerFrom.Enabled = false;
				}
				if (SelectedPeriod == DatePeriods.AllDates)
				{
					dateTimePickerFrom.Value = Factory.CompanyInformationSystem.GetInitialFiscalYearDate();
					dateTimePickerTo.Value = Factory.CompanyInformationSystem.GetLastFiscalYearDate();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private int GetLastDayOfMonth(int year, int month)
		{
			return new DateTime(year, month, 1).AddMonths(1).AddDays(-1.0).Day;
		}

		public void SelectThisMonth()
		{
			SelectedPeriod = DatePeriods.ThisMonthToDate;
		}

		public void SelectToCustom()
		{
			SelectedPeriod = DatePeriods.Custom;
		}

		private void DateControl_Load(object sender, EventArgs e)
		{
			try
			{
				LoadData();
				DatePeriods datePeriods = DatePeriods.ThisMonthToDate;
				int result = 0;
				if (Factory.IsConnected)
				{
					int.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.DefaultDate.ToString(), DatePeriods.ThisMonthToDate).ToString(), out result);
					datePeriods = (SelectedPeriod = (DatePeriods)result);
					DatabaseHelper.GetFirstID("FiscalYear", "FiscalYearID");
					DatabaseHelper.GetLastID("FiscalYear", "FiscalYearID");
					DateTime now = DateTime.Now;
					dtStartDate = DateTime.Parse(Factory.DatabaseSystem.GetFieldValue("FiscalYear", "StartDate", "FiscalYearID", now.Year).ToString());
					dtEndDate = DateTime.Parse(Factory.DatabaseSystem.GetFieldValue("FiscalYear", "EndDate", "FiscalYearID", now.Year).ToString());
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadData()
		{
			comboBoxDateRange.Items.Clear();
			comboBoxDateRange.Items.AddRange(HelperLib.GetDateRangeComboList(includeCustom: true).ToArray());
		}

		private void dateTimePickerTo_ValueChanged(object sender, EventArgs e)
		{
			if (SelectedPeriod == DatePeriods.DateEqualTo)
			{
				dateTimePickerFrom.Value = dateTimePickerTo.Value;
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
			panelCustom = new System.Windows.Forms.Panel();
			label1 = new System.Windows.Forms.Label();
			dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
			dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			comboBoxDateRange = new System.Windows.Forms.ComboBox();
			panelCustom.SuspendLayout();
			SuspendLayout();
			panelCustom.Controls.Add(label1);
			panelCustom.Controls.Add(dateTimePickerFrom);
			panelCustom.Controls.Add(dateTimePickerTo);
			panelCustom.Controls.Add(label2);
			panelCustom.Enabled = false;
			panelCustom.Location = new System.Drawing.Point(3, 24);
			panelCustom.Name = "panelCustom";
			panelCustom.Size = new System.Drawing.Size(272, 23);
			panelCustom.TabIndex = 2;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(2, 3);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(33, 13);
			label1.TabIndex = 0;
			label1.Text = "From:";
			dateTimePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerFrom.Location = new System.Drawing.Point(38, 1);
			dateTimePickerFrom.Name = "dateTimePickerFrom";
			dateTimePickerFrom.Size = new System.Drawing.Size(101, 20);
			dateTimePickerFrom.TabIndex = 1;
			dateTimePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerTo.Location = new System.Drawing.Point(167, 1);
			dateTimePickerTo.Name = "dateTimePickerTo";
			dateTimePickerTo.Size = new System.Drawing.Size(101, 20);
			dateTimePickerTo.TabIndex = 3;
			dateTimePickerTo.ValueChanged += new System.EventHandler(dateTimePickerTo_ValueChanged);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(139, 5);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(23, 13);
			label2.TabIndex = 2;
			label2.Text = "To:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 5);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 0;
			label3.Text = "Date:";
			comboBoxDateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDateRange.FormattingEnabled = true;
			comboBoxDateRange.Location = new System.Drawing.Point(41, 2);
			comboBoxDateRange.Name = "comboBoxDateRange";
			comboBoxDateRange.Size = new System.Drawing.Size(230, 21);
			comboBoxDateRange.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(panelCustom);
			base.Controls.Add(label3);
			base.Controls.Add(comboBoxDateRange);
			base.Name = "DateControl";
			base.Size = new System.Drawing.Size(275, 50);
			base.Load += new System.EventHandler(DateControl_Load);
			panelCustom.ResumeLayout(false);
			panelCustom.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
