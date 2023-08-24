using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class SetReminderForm : DialogBoxBaseForm
	{
		private DateTime date = DateTime.Now;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private Micromind.UISupport.Line line1;

		private Label label1;

		private Micromind.UISupport.flatDatePicker dateTimePickerDate;

		private Label label2;

		private DateTimePicker dateTimePickerTime;

		private Container components;

		private bool isFirst = true;

		public DateTime ReminderDate
		{
			get
			{
				return new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day, dateTimePickerTime.Value.Hour, dateTimePickerTime.Value.Minute, dateTimePickerTime.Value.Second);
			}
			set
			{
				DateTime dateTime3 = dateTimePickerDate.Value = (dateTimePickerTime.Value = value);
				date = value;
			}
		}

		public SetReminderForm()
		{
			InitializeComponent();
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
			buttonOK = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			label1 = new System.Windows.Forms.Label();
			dateTimePickerDate = new Micromind.UISupport.flatDatePicker();
			label2 = new System.Windows.Forms.Label();
			dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
			SuspendLayout();
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.Silver;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.Location = new System.Drawing.Point(152, 64);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(64, 20);
			buttonOK.TabIndex = 13;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.Silver;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(224, 64);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(72, 20);
			buttonCancel.TabIndex = 12;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 56);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(288, 1);
			line1.TabIndex = 14;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(8, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(31, 16);
			label1.TabIndex = 15;
			label1.Text = "Date:";
			dateTimePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			dateTimePickerDate.Location = new System.Drawing.Point(48, 12);
			dateTimePickerDate.Name = "dateTimePickerDate";
			dateTimePickerDate.Size = new System.Drawing.Size(96, 20);
			dateTimePickerDate.TabIndex = 16;
			dateTimePickerDate.Value = new System.DateTime(2005, 3, 20, 17, 45, 29, 0);
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(152, 14);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(33, 16);
			label2.TabIndex = 17;
			label2.Text = "Time:";
			dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerTime.Location = new System.Drawing.Point(192, 12);
			dateTimePickerTime.Name = "dateTimePickerTime";
			dateTimePickerTime.ShowUpDown = true;
			dateTimePickerTime.Size = new System.Drawing.Size(104, 20);
			dateTimePickerTime.TabIndex = 18;
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(306, 87);
			base.Controls.Add(dateTimePickerTime);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(dateTimePickerDate);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SetReminderForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Set Reminder";
			base.Load += new System.EventHandler(SetReminderForm_Load);
			base.Activated += new System.EventHandler(SetReminderForm_Activated);
			ResumeLayout(false);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void SetReminderForm_Load(object sender, EventArgs e)
		{
			InitDialog();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void SetReminderForm_Activated(object sender, EventArgs e)
		{
			if (isFirst)
			{
				ReminderDate = date;
				isFirst = false;
			}
		}
	}
}
