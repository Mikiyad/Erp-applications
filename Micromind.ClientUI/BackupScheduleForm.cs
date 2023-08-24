using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI
{
	public class BackupScheduleForm : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private GroupBox groupBoxOccurs;

		private RadioButton radioButtonDaily;

		private RadioButton radioButtonWeekly;

		private RadioButton radioButtonMonthly;

		private GroupBox groupBoxSchedule;

		private MMLabel labelDayWeekEvery;

		private CheckBox checkBoxMonday;

		private CheckBox checkBoxTuesday;

		private CheckBox checkBoxWednesday;

		private CheckBox checkBoxThursday;

		private CheckBox checkBoxFriday;

		private Panel panelEveryMonth;

		private MMLabel label4;

		private MMLabel label5;

		private flatDatePicker flatDatePickerStartDate;

		private MMLabel label6;

		private Panel panelWeekDays;

		private CheckBox checkBoxEndDate;

		private DateTimePicker dateTimePickerStartTime;

		private flatDatePicker flatDatePickerEndDate;

		private XPButton buttonCancel;

		private XPButton buttonOK;

		private NumericUpDown numericUpDownMonth;

		private NumericUpDown numericUpDownDays;

		private MMLabel label1;

		private MMTextBox textBoxName;

		private CheckBox checkBoxSunday;

		private CheckBox checkBoxSaturday;

		private MMLabel labelEveryDay;

		private GroupBox groupBox1;

		private Container components;

		internal string ScheduleName
		{
			get
			{
				return textBoxName.Text;
			}
			set
			{
				textBoxName.Text = value;
			}
		}

		internal DateTime StartDate
		{
			get
			{
				return flatDatePickerStartDate.Value;
			}
			set
			{
				flatDatePickerStartDate.Value = value;
			}
		}

		internal DateTime StartTime
		{
			get
			{
				return dateTimePickerStartTime.Value;
			}
			set
			{
				dateTimePickerStartTime.Value = value;
			}
		}

		internal DateTime EndDate
		{
			get
			{
				return flatDatePickerEndDate.Value;
			}
			set
			{
				flatDatePickerEndDate.Value = value;
			}
		}

		internal bool HasEndDate
		{
			get
			{
				return checkBoxEndDate.Checked;
			}
			set
			{
				checkBoxEndDate.Checked = value;
			}
		}

		internal int FrequencyType
		{
			get
			{
				if (radioButtonDaily.Checked)
				{
					return 4;
				}
				if (radioButtonWeekly.Checked)
				{
					return 8;
				}
				if (radioButtonMonthly.Checked)
				{
					return 16;
				}
				return 1;
			}
		}

		internal int FrequencyInterval
		{
			get
			{
				if (radioButtonDaily.Checked)
				{
					return (int)numericUpDownDays.Value;
				}
				checked
				{
					if (radioButtonWeekly.Checked)
					{
						int num = 0;
						if (checkBoxSunday.Checked)
						{
							num++;
						}
						if (checkBoxMonday.Checked)
						{
							num += 2;
						}
						if (checkBoxTuesday.Checked)
						{
							num += 4;
						}
						if (checkBoxWednesday.Checked)
						{
							num += 8;
						}
						if (checkBoxThursday.Checked)
						{
							num += 16;
						}
						if (checkBoxFriday.Checked)
						{
							num += 32;
						}
						if (checkBoxSaturday.Checked)
						{
							num += 64;
						}
						return num;
					}
					if (radioButtonMonthly.Checked)
					{
						return (int)numericUpDownDays.Value;
					}
					return 0;
				}
			}
		}

		internal int FrequencyRecurrenceFactor
		{
			get
			{
				if (radioButtonDaily.Checked)
				{
					return 0;
				}
				if (radioButtonWeekly.Checked)
				{
					return (int)numericUpDownDays.Value;
				}
				if (radioButtonMonthly.Checked)
				{
					return (int)numericUpDownMonth.Value;
				}
				return 0;
			}
		}

		public BackupScheduleForm()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
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
			groupBoxOccurs = new System.Windows.Forms.GroupBox();
			radioButtonMonthly = new System.Windows.Forms.RadioButton();
			radioButtonWeekly = new System.Windows.Forms.RadioButton();
			radioButtonDaily = new System.Windows.Forms.RadioButton();
			groupBoxSchedule = new System.Windows.Forms.GroupBox();
			panelEveryMonth = new System.Windows.Forms.Panel();
			label4 = new Micromind.UISupport.MMLabel();
			numericUpDownMonth = new System.Windows.Forms.NumericUpDown();
			panelWeekDays = new System.Windows.Forms.Panel();
			checkBoxSunday = new System.Windows.Forms.CheckBox();
			checkBoxSaturday = new System.Windows.Forms.CheckBox();
			checkBoxFriday = new System.Windows.Forms.CheckBox();
			checkBoxThursday = new System.Windows.Forms.CheckBox();
			checkBoxWednesday = new System.Windows.Forms.CheckBox();
			checkBoxTuesday = new System.Windows.Forms.CheckBox();
			checkBoxMonday = new System.Windows.Forms.CheckBox();
			labelDayWeekEvery = new Micromind.UISupport.MMLabel();
			numericUpDownDays = new System.Windows.Forms.NumericUpDown();
			labelEveryDay = new Micromind.UISupport.MMLabel();
			label5 = new Micromind.UISupport.MMLabel();
			checkBoxEndDate = new System.Windows.Forms.CheckBox();
			flatDatePickerStartDate = new Micromind.UISupport.flatDatePicker();
			label6 = new Micromind.UISupport.MMLabel();
			dateTimePickerStartTime = new System.Windows.Forms.DateTimePicker();
			flatDatePickerEndDate = new Micromind.UISupport.flatDatePicker();
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonOK = new Micromind.UISupport.XPButton();
			label1 = new Micromind.UISupport.MMLabel();
			textBoxName = new Micromind.UISupport.MMTextBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBoxOccurs.SuspendLayout();
			groupBoxSchedule.SuspendLayout();
			panelEveryMonth.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownMonth).BeginInit();
			panelWeekDays.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)numericUpDownDays).BeginInit();
			groupBox1.SuspendLayout();
			SuspendLayout();
			groupBoxOccurs.Controls.Add(radioButtonMonthly);
			groupBoxOccurs.Controls.Add(radioButtonWeekly);
			groupBoxOccurs.Controls.Add(radioButtonDaily);
			groupBoxOccurs.Location = new System.Drawing.Point(5, 81);
			groupBoxOccurs.Name = "groupBoxOccurs";
			groupBoxOccurs.Size = new System.Drawing.Size(88, 128);
			groupBoxOccurs.TabIndex = 6;
			groupBoxOccurs.TabStop = false;
			groupBoxOccurs.Text = "Occ&urs";
			radioButtonMonthly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonMonthly.Location = new System.Drawing.Point(13, 72);
			radioButtonMonthly.Name = "radioButtonMonthly";
			radioButtonMonthly.Size = new System.Drawing.Size(64, 16);
			radioButtonMonthly.TabIndex = 2;
			radioButtonMonthly.Text = "Monthly";
			radioButtonMonthly.CheckedChanged += new System.EventHandler(radioButtonMonthly_CheckedChanged);
			radioButtonWeekly.Checked = true;
			radioButtonWeekly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonWeekly.Location = new System.Drawing.Point(12, 49);
			radioButtonWeekly.Name = "radioButtonWeekly";
			radioButtonWeekly.Size = new System.Drawing.Size(64, 16);
			radioButtonWeekly.TabIndex = 1;
			radioButtonWeekly.TabStop = true;
			radioButtonWeekly.Text = "Weekly";
			radioButtonWeekly.CheckedChanged += new System.EventHandler(radioButtonWeekly_CheckedChanged);
			radioButtonDaily.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			radioButtonDaily.Location = new System.Drawing.Point(13, 25);
			radioButtonDaily.Name = "radioButtonDaily";
			radioButtonDaily.Size = new System.Drawing.Size(64, 16);
			radioButtonDaily.TabIndex = 0;
			radioButtonDaily.Text = "Daily";
			radioButtonDaily.CheckedChanged += new System.EventHandler(radioButtonDaily_CheckedChanged);
			groupBoxSchedule.Controls.Add(panelEveryMonth);
			groupBoxSchedule.Controls.Add(panelWeekDays);
			groupBoxSchedule.Controls.Add(labelDayWeekEvery);
			groupBoxSchedule.Controls.Add(numericUpDownDays);
			groupBoxSchedule.Controls.Add(labelEveryDay);
			groupBoxSchedule.Location = new System.Drawing.Point(101, 81);
			groupBoxSchedule.Name = "groupBoxSchedule";
			groupBoxSchedule.Size = new System.Drawing.Size(336, 128);
			groupBoxSchedule.TabIndex = 7;
			groupBoxSchedule.TabStop = false;
			groupBoxSchedule.Text = "Schedule";
			panelEveryMonth.Controls.Add(label4);
			panelEveryMonth.Controls.Add(numericUpDownMonth);
			panelEveryMonth.Location = new System.Drawing.Point(184, 22);
			panelEveryMonth.Name = "panelEveryMonth";
			panelEveryMonth.Size = new System.Drawing.Size(128, 24);
			panelEveryMonth.TabIndex = 5;
			label4.AutoSize = true;
			label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label4.Location = new System.Drawing.Point(72, 4);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(49, 16);
			label4.TabIndex = 4;
			label4.Text = "Month(s)";
			numericUpDownMonth.Location = new System.Drawing.Point(4, 2);
			numericUpDownMonth.Name = "numericUpDownMonth";
			numericUpDownMonth.Size = new System.Drawing.Size(56, 20);
			numericUpDownMonth.TabIndex = 0;
			numericUpDownMonth.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			panelWeekDays.Controls.Add(checkBoxSunday);
			panelWeekDays.Controls.Add(checkBoxSaturday);
			panelWeekDays.Controls.Add(checkBoxFriday);
			panelWeekDays.Controls.Add(checkBoxThursday);
			panelWeekDays.Controls.Add(checkBoxWednesday);
			panelWeekDays.Controls.Add(checkBoxTuesday);
			panelWeekDays.Controls.Add(checkBoxMonday);
			panelWeekDays.Location = new System.Drawing.Point(8, 56);
			panelWeekDays.Name = "panelWeekDays";
			panelWeekDays.Size = new System.Drawing.Size(320, 64);
			panelWeekDays.TabIndex = 0;
			checkBoxSunday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxSunday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxSunday.Location = new System.Drawing.Point(150, 34);
			checkBoxSunday.Name = "checkBoxSunday";
			checkBoxSunday.Size = new System.Drawing.Size(78, 16);
			checkBoxSunday.TabIndex = 6;
			checkBoxSunday.Text = "Sunday";
			checkBoxSaturday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxSaturday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxSaturday.Location = new System.Drawing.Point(74, 34);
			checkBoxSaturday.Name = "checkBoxSaturday";
			checkBoxSaturday.Size = new System.Drawing.Size(72, 16);
			checkBoxSaturday.TabIndex = 5;
			checkBoxSaturday.Text = "Saturday";
			checkBoxFriday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxFriday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxFriday.Location = new System.Drawing.Point(10, 34);
			checkBoxFriday.Name = "checkBoxFriday";
			checkBoxFriday.Size = new System.Drawing.Size(64, 16);
			checkBoxFriday.TabIndex = 4;
			checkBoxFriday.Text = "Friday";
			checkBoxThursday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxThursday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxThursday.Location = new System.Drawing.Point(239, 12);
			checkBoxThursday.Name = "checkBoxThursday";
			checkBoxThursday.Size = new System.Drawing.Size(71, 16);
			checkBoxThursday.TabIndex = 3;
			checkBoxThursday.Text = "Thursday";
			checkBoxWednesday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxWednesday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxWednesday.Location = new System.Drawing.Point(150, 11);
			checkBoxWednesday.Name = "checkBoxWednesday";
			checkBoxWednesday.Size = new System.Drawing.Size(82, 16);
			checkBoxWednesday.TabIndex = 2;
			checkBoxWednesday.Text = "Wednesday";
			checkBoxTuesday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxTuesday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxTuesday.Location = new System.Drawing.Point(75, 11);
			checkBoxTuesday.Name = "checkBoxTuesday";
			checkBoxTuesday.Size = new System.Drawing.Size(64, 16);
			checkBoxTuesday.TabIndex = 1;
			checkBoxTuesday.Text = "Tuesday";
			checkBoxMonday.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxMonday.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxMonday.Location = new System.Drawing.Point(10, 11);
			checkBoxMonday.Name = "checkBoxMonday";
			checkBoxMonday.Size = new System.Drawing.Size(64, 16);
			checkBoxMonday.TabIndex = 0;
			checkBoxMonday.Text = "Monday";
			labelDayWeekEvery.AutoSize = true;
			labelDayWeekEvery.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelDayWeekEvery.Location = new System.Drawing.Point(119, 26);
			labelDayWeekEvery.Name = "labelDayWeekEvery";
			labelDayWeekEvery.Size = new System.Drawing.Size(38, 16);
			labelDayWeekEvery.TabIndex = 3;
			labelDayWeekEvery.Text = "Day(s)";
			numericUpDownDays.Location = new System.Drawing.Point(56, 24);
			numericUpDownDays.Name = "numericUpDownDays";
			numericUpDownDays.Size = new System.Drawing.Size(56, 20);
			numericUpDownDays.TabIndex = 0;
			numericUpDownDays.Value = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			labelEveryDay.AutoSize = true;
			labelEveryDay.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelEveryDay.Location = new System.Drawing.Point(11, 24);
			labelEveryDay.Name = "labelEveryDay";
			labelEveryDay.Size = new System.Drawing.Size(33, 16);
			labelEveryDay.TabIndex = 1;
			labelEveryDay.Text = "E&very";
			label5.AutoSize = true;
			label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label5.Location = new System.Drawing.Point(9, 48);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(58, 16);
			label5.TabIndex = 2;
			label5.Text = "Start &Date:";
			checkBoxEndDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxEndDate.Location = new System.Drawing.Point(232, 48);
			checkBoxEndDate.Name = "checkBoxEndDate";
			checkBoxEndDate.Size = new System.Drawing.Size(76, 24);
			checkBoxEndDate.TabIndex = 4;
			checkBoxEndDate.Text = "End Date:";
			checkBoxEndDate.CheckedChanged += new System.EventHandler(checkBoxEndDate_CheckedChanged);
			flatDatePickerStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			flatDatePickerStartDate.Location = new System.Drawing.Point(95, 48);
			flatDatePickerStartDate.Name = "flatDatePickerStartDate";
			flatDatePickerStartDate.Size = new System.Drawing.Size(128, 20);
			flatDatePickerStartDate.TabIndex = 3;
			flatDatePickerStartDate.Value = new System.DateTime(2005, 9, 13, 16, 52, 7, 0);
			label6.AutoSize = true;
			label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label6.Location = new System.Drawing.Point(12, 24);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(83, 16);
			label6.TabIndex = 0;
			label6.Text = "&Occurs once at:";
			dateTimePickerStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerStartTime.Location = new System.Drawing.Point(109, 22);
			dateTimePickerStartTime.Name = "dateTimePickerStartTime";
			dateTimePickerStartTime.ShowUpDown = true;
			dateTimePickerStartTime.Size = new System.Drawing.Size(131, 20);
			dateTimePickerStartTime.TabIndex = 1;
			flatDatePickerEndDate.Enabled = false;
			flatDatePickerEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			flatDatePickerEndDate.Location = new System.Drawing.Point(312, 51);
			flatDatePickerEndDate.Name = "flatDatePickerEndDate";
			flatDatePickerEndDate.Size = new System.Drawing.Size(128, 20);
			flatDatePickerEndDate.TabIndex = 5;
			flatDatePickerEndDate.Value = new System.DateTime(2005, 9, 13, 16, 52, 7, 0);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(360, 280);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(80, 24);
			buttonCancel.TabIndex = 9;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(272, 280);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(80, 24);
			buttonOK.TabIndex = 8;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			label1.AutoSize = true;
			label1.BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(8, 8);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(88, 16);
			label1.TabIndex = 0;
			label1.Text = "Schedule &Name:";
			textBoxName.AcceptsReturn = false;
			textBoxName.AcceptsTab = false;
			textBoxName.AutoSize = true;
			textBoxName.BackColor = System.Drawing.Color.White;
			textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxName.HideSelection = true;
			textBoxName.IsComboTextBox = false;
			textBoxName.Lines = new string[1]
			{
				"Schedule 1"
			};
			textBoxName.Location = new System.Drawing.Point(96, 8);
			textBoxName.MaxLength = 25;
			textBoxName.Multiline = false;
			textBoxName.Name = "textBoxName";
			textBoxName.PasswordChar = '\0';
			textBoxName.ReadOnly = false;
			textBoxName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxName.Size = new System.Drawing.Size(352, 20);
			textBoxName.TabIndex = 1;
			textBoxName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxName.WordWrap = true;
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(dateTimePickerStartTime);
			groupBox1.Location = new System.Drawing.Point(8, 216);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(432, 56);
			groupBox1.TabIndex = 8;
			groupBox1.TabStop = false;
			groupBox1.Text = "Daily Frequency";
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(456, 311);
			base.Controls.Add(groupBox1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(flatDatePickerEndDate);
			base.Controls.Add(flatDatePickerStartDate);
			base.Controls.Add(checkBoxEndDate);
			base.Controls.Add(label5);
			base.Controls.Add(groupBoxSchedule);
			base.Controls.Add(groupBoxOccurs);
			base.Controls.Add(textBoxName);
			base.Controls.Add(label1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "BackupScheduleForm";
			Text = "Schedule Database Backup";
			base.Closing += new System.ComponentModel.CancelEventHandler(BackupScheduleForm_Closing);
			base.Load += new System.EventHandler(BackupScheduleForm_Load);
			groupBoxOccurs.ResumeLayout(false);
			groupBoxSchedule.ResumeLayout(false);
			panelEveryMonth.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)numericUpDownMonth).EndInit();
			panelWeekDays.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)numericUpDownDays).EndInit();
			groupBox1.ResumeLayout(false);
			ResumeLayout(false);
		}

		private void flatDatePicker1_Load(object sender, EventArgs e)
		{
		}

		private void checkBoxEndDate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxEndDate.Checked)
			{
				flatDatePickerEndDate.Enabled = true;
			}
			else
			{
				flatDatePickerEndDate.Enabled = false;
			}
		}

		private void ShowHideScheduleControls()
		{
			if (radioButtonDaily.Checked)
			{
				numericUpDownDays.Visible = true;
				labelDayWeekEvery.Text = SR.GetString("00150");
				Panel panel = panelEveryMonth;
				bool visible = panelWeekDays.Visible = false;
				panel.Visible = visible;
				groupBoxSchedule.Text = SR.GetString("00149");
				labelEveryDay.Text = SR.GetString("00142");
			}
			else if (radioButtonWeekly.Checked)
			{
				numericUpDownDays.Visible = true;
				labelDayWeekEvery.Text = SR.GetString("00148");
				panelEveryMonth.Visible = false;
				panelWeekDays.Visible = true;
				groupBoxSchedule.Text = SR.GetString("00147");
				labelEveryDay.Text = SR.GetString("00142");
			}
			else if (radioButtonMonthly.Checked)
			{
				numericUpDownDays.Visible = true;
				labelDayWeekEvery.Text = SR.GetString("00146");
				panelEveryMonth.Visible = true;
				panelWeekDays.Visible = false;
				labelEveryDay.Text = SR.GetString("00145");
				groupBoxSchedule.Text = SR.GetString("00144");
			}
			else
			{
				numericUpDownDays.Visible = false;
				labelDayWeekEvery.Text = "";
				panelEveryMonth.Visible = false;
				panelWeekDays.Visible = false;
				groupBoxSchedule.Text = SR.GetString("00143");
				labelEveryDay.Text = SR.GetString("00142");
			}
		}

		private void radioButtonDaily_CheckedChanged(object sender, EventArgs e)
		{
			ShowHideScheduleControls();
		}

		private void radioButtonWeekly_CheckedChanged(object sender, EventArgs e)
		{
			ShowHideScheduleControls();
		}

		private void radioButtonMonthly_CheckedChanged(object sender, EventArgs e)
		{
			ShowHideScheduleControls();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void BackupScheduleForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				Global.GlobalSettings.LoadFormProperties(this);
				ShowHideScheduleControls();
				switch (DateTime.Today.DayOfWeek)
				{
				case DayOfWeek.Sunday:
					checkBoxSunday.Checked = true;
					break;
				case DayOfWeek.Monday:
					checkBoxMonday.Checked = true;
					break;
				case DayOfWeek.Tuesday:
					checkBoxTuesday.Checked = true;
					break;
				case DayOfWeek.Wednesday:
					checkBoxWednesday.Checked = true;
					break;
				case DayOfWeek.Thursday:
					checkBoxThursday.Checked = true;
					break;
				case DayOfWeek.Friday:
					checkBoxFriday.Checked = true;
					break;
				case DayOfWeek.Saturday:
					checkBoxSaturday.Checked = true;
					break;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void BackupScheduleForm_Closing(object sender, CancelEventArgs e)
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}
	}
}
