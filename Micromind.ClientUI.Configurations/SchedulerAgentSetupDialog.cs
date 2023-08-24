using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class SchedulerAgentSetupDialog : Form
	{
		private IContainer components;

		private Label label2;

		private Label label3;

		private Button buttonUpdate;

		private Label label1;

		private TextBox textBoxUserName;

		private TextBox textBoxPassword;

		private CheckBox checkBoxDisable;

		private NumericUpDown numericUpDownInterval;

		private Label label4;

		private Label label5;

		private DateTimePicker dateTimePickerMaintenanceTime;

		private Label label6;

		private Button buttonClose;

		private Line line1;

		private Label label7;

		private Label label8;

		private NumericUpDown numericUpDownEmailInterval;

		private ServiceController serviceController;

		private TextBox textBoxStatus;

		private Label label9;

		private Button buttonServiceStatus;

		public SchedulerAgentSetupDialog()
		{
			InitializeComponent();
		}

		private void ReadVersions()
		{
		}

		private void SchedulerAgentSetupDialog_Load(object sender, EventArgs e)
		{
			try
			{
				LoadData();
				serviceController.MachineName = Global.CurrentServerName.Substring(0, Global.CurrentServerName.IndexOf("\\"));
				try
				{
					textBoxStatus.Text = serviceController.Status.ToString();
				}
				catch
				{
					textBoxStatus.Text = "-";
					buttonServiceStatus.Enabled = false;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void LoadData()
		{
			try
			{
				DataSet schedulerAgentInfo = Factory.CompanyInformationSystem.GetSchedulerAgentInfo();
				if (schedulerAgentInfo != null)
				{
					DataRow dataRow = schedulerAgentInfo.Tables[0].Rows[0];
					textBoxUserName.Text = dataRow["UserName"].ToString();
					textBoxPassword.Text = dataRow["Password"].ToString();
					numericUpDownInterval.Value = decimal.Parse(dataRow["TaskInterval"].ToString());
					numericUpDownEmailInterval.Value = decimal.Parse(dataRow["EmailInterval"].ToString());
					dateTimePickerMaintenanceTime.Value = DateTime.Parse(dataRow["MaintenanceTime"].ToString());
					checkBoxDisable.Checked = !bool.Parse(dataRow["IsActive"].ToString());
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void buttonUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				if (Factory.CompanyInformationSystem.ConfigureSchedulerAgent(textBoxUserName.Text, textBoxPassword.Text, numericUpDownInterval.Value, numericUpDownEmailInterval.Value, dateTimePickerMaintenanceTime.Value, !checkBoxDisable.Checked))
				{
					ErrorHelper.InformationMessage("Agent configured successfully. You must restart Axolon Scheduler Agent for the changes to take effect.");
					base.DialogResult = DialogResult.None;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonServiceStatus_Click(object sender, EventArgs e)
		{
			try
			{
				if (serviceController.Status == ServiceControllerStatus.Running)
				{
					serviceController.Stop();
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
					textBoxStatus.Text = "Stopped";
					buttonServiceStatus.Text = "Start";
				}
				else
				{
					serviceController.Start();
					serviceController.WaitForStatus(ServiceControllerStatus.Running);
					textBoxStatus.Text = "Started";
					buttonServiceStatus.Text = "Stop";
				}
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage(ex.Message, ex.InnerException.Message);
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
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			buttonUpdate = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			textBoxUserName = new System.Windows.Forms.TextBox();
			textBoxPassword = new System.Windows.Forms.TextBox();
			checkBoxDisable = new System.Windows.Forms.CheckBox();
			numericUpDownInterval = new System.Windows.Forms.NumericUpDown();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			dateTimePickerMaintenanceTime = new System.Windows.Forms.DateTimePicker();
			label6 = new System.Windows.Forms.Label();
			buttonClose = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			numericUpDownEmailInterval = new System.Windows.Forms.NumericUpDown();
			serviceController = new System.ServiceProcess.ServiceController();
			textBoxStatus = new System.Windows.Forms.TextBox();
			label9 = new System.Windows.Forms.Label();
			buttonServiceStatus = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)numericUpDownInterval).BeginInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownEmailInterval).BeginInit();
			SuspendLayout();
			label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(449, 27);
			label2.TabIndex = 1;
			label2.Text = "Enter Axolon user username and password which will be used to run scheduled tasks:";
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label3.Location = new System.Drawing.Point(12, 46);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(59, 13);
			label3.TabIndex = 2;
			label3.Text = "Username:";
			buttonUpdate.Location = new System.Drawing.Point(251, 269);
			buttonUpdate.Name = "buttonUpdate";
			buttonUpdate.Size = new System.Drawing.Size(105, 28);
			buttonUpdate.TabIndex = 6;
			buttonUpdate.Text = "Configure";
			buttonUpdate.UseVisualStyleBackColor = true;
			buttonUpdate.Click += new System.EventHandler(buttonUpdate_Click);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 69);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 13);
			label1.TabIndex = 2;
			label1.Text = "Password:";
			textBoxUserName.Location = new System.Drawing.Point(77, 44);
			textBoxUserName.Name = "textBoxUserName";
			textBoxUserName.Size = new System.Drawing.Size(191, 20);
			textBoxUserName.TabIndex = 0;
			textBoxPassword.Location = new System.Drawing.Point(77, 69);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(191, 20);
			textBoxPassword.TabIndex = 1;
			textBoxPassword.UseSystemPasswordChar = true;
			checkBoxDisable.AutoSize = true;
			checkBoxDisable.Location = new System.Drawing.Point(15, 190);
			checkBoxDisable.Name = "checkBoxDisable";
			checkBoxDisable.Size = new System.Drawing.Size(190, 17);
			checkBoxDisable.TabIndex = 5;
			checkBoxDisable.Text = "Disable scheduler for this company";
			checkBoxDisable.UseVisualStyleBackColor = true;
			numericUpDownInterval.Location = new System.Drawing.Point(181, 107);
			numericUpDownInterval.Name = "numericUpDownInterval";
			numericUpDownInterval.Size = new System.Drawing.Size(69, 20);
			numericUpDownInterval.TabIndex = 2;
			numericUpDownInterval.Value = new decimal(new int[4]
			{
				60,
				0,
				0,
				0
			});
			label4.AutoSize = true;
			label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label4.Location = new System.Drawing.Point(12, 109);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(163, 13);
			label4.TabIndex = 21;
			label4.Text = "Check for scheduled tasks every";
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(256, 111);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(44, 13);
			label5.TabIndex = 21;
			label5.Text = "minutes";
			dateTimePickerMaintenanceTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			dateTimePickerMaintenanceTime.Location = new System.Drawing.Point(230, 160);
			dateTimePickerMaintenanceTime.Name = "dateTimePickerMaintenanceTime";
			dateTimePickerMaintenanceTime.ShowUpDown = true;
			dateTimePickerMaintenanceTime.Size = new System.Drawing.Size(110, 20);
			dateTimePickerMaintenanceTime.TabIndex = 4;
			dateTimePickerMaintenanceTime.Value = new System.DateTime(2016, 5, 24, 0, 0, 0, 0);
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(12, 163);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(212, 13);
			label6.TabIndex = 24;
			label6.Text = "Perform system maintenance tasks daily at";
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(359, 269);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(105, 28);
			buttonClose.TabIndex = 7;
			buttonClose.Text = "Cancel";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(1, 262);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(476, 1);
			line1.TabIndex = 22;
			line1.TabStop = false;
			label7.AutoSize = true;
			label7.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label7.Location = new System.Drawing.Point(257, 137);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(44, 13);
			label7.TabIndex = 27;
			label7.Text = "minutes";
			label8.AutoSize = true;
			label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label8.Location = new System.Drawing.Point(13, 135);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(147, 13);
			label8.TabIndex = 26;
			label8.Text = "Process sending emails every";
			numericUpDownEmailInterval.Location = new System.Drawing.Point(182, 133);
			numericUpDownEmailInterval.Name = "numericUpDownEmailInterval";
			numericUpDownEmailInterval.Size = new System.Drawing.Size(69, 20);
			numericUpDownEmailInterval.TabIndex = 3;
			numericUpDownEmailInterval.Value = new decimal(new int[4]
			{
				10,
				0,
				0,
				0
			});
			serviceController.ServiceName = "Axolon Scheduler Agent";
			textBoxStatus.Location = new System.Drawing.Point(98, 236);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(100, 20);
			textBoxStatus.TabIndex = 28;
			label9.AutoSize = true;
			label9.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label9.Location = new System.Drawing.Point(12, 239);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(80, 13);
			label9.TabIndex = 29;
			label9.Text = "Service Status:";
			buttonServiceStatus.Location = new System.Drawing.Point(204, 235);
			buttonServiceStatus.Name = "buttonServiceStatus";
			buttonServiceStatus.Size = new System.Drawing.Size(64, 23);
			buttonServiceStatus.TabIndex = 30;
			buttonServiceStatus.Text = "Stop";
			buttonServiceStatus.UseVisualStyleBackColor = true;
			buttonServiceStatus.Click += new System.EventHandler(buttonServiceStatus_Click);
			base.AcceptButton = buttonUpdate;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(476, 302);
			base.Controls.Add(buttonServiceStatus);
			base.Controls.Add(label9);
			base.Controls.Add(textBoxStatus);
			base.Controls.Add(label7);
			base.Controls.Add(label8);
			base.Controls.Add(numericUpDownEmailInterval);
			base.Controls.Add(label6);
			base.Controls.Add(dateTimePickerMaintenanceTime);
			base.Controls.Add(line1);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(numericUpDownInterval);
			base.Controls.Add(checkBoxDisable);
			base.Controls.Add(textBoxPassword);
			base.Controls.Add(textBoxUserName);
			base.Controls.Add(buttonClose);
			base.Controls.Add(buttonUpdate);
			base.Controls.Add(label1);
			base.Controls.Add(label3);
			base.Controls.Add(label2);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SchedulerAgentSetupDialog";
			Text = "Scheduler Agent Setup";
			base.Load += new System.EventHandler(SchedulerAgentSetupDialog_Load);
			((System.ComponentModel.ISupportInitialize)numericUpDownInterval).EndInit();
			((System.ComponentModel.ISupportInitialize)numericUpDownEmailInterval).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
