using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries
{
	public class SelectDashboardTemplateDialog : Form
	{
		private string userID = "";

		private string groupID = "";

		private DataSet data;

		private bool result;

		private bool isLoad;

		private string templateName = "";

		private IContainer components;

		private Button buttonCancel;

		private Label label1;

		private Button buttonOK;

		private Line linePanelDown;

		private ComboBox comboBoxTemplate;

		public string UserID
		{
			get
			{
				return userID;
			}
			set
			{
				userID = value;
			}
		}

		public string GroupID
		{
			get
			{
				return groupID;
			}
			set
			{
				groupID = value;
			}
		}

		public string TemplateName
		{
			get
			{
				return templateName;
			}
			set
			{
				templateName = value;
			}
		}

		public bool IsLoad
		{
			get
			{
				return isLoad;
			}
			set
			{
				isLoad = value;
			}
		}

		public SelectDashboardTemplateDialog()
		{
			InitializeComponent();
			base.StartPosition = FormStartPosition.CenterParent;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (!isLoad)
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to update this dashboard?") != DialogResult.Yes || ErrorHelper.WarningMessageOkCancel("Updating DashBoard cannot be reverted.", "Do you still want to continue?") == DialogResult.Cancel)
				{
					return;
				}
				if (!string.IsNullOrEmpty(GroupID))
				{
					data = Factory.UserGroupSystem.GetUsersByGroup(GroupID);
					if (data == null || data.Tables.Count == 0 || data.Tables[0].Rows.Count == 0)
					{
						return;
					}
					foreach (DataRow row in data.Tables[0].Rows)
					{
						string text = row["UserID"].ToString();
						result = Factory.DashboardSystem.DeleteDashboard("dashLayoutHome", text);
						if (result)
						{
							result &= Factory.DashboardSystem.UpdateDashboardWithTemplate(templateName, text);
						}
					}
				}
				else
				{
					DataSet dashboardByID = Factory.DashboardSystem.GetDashboardByID("dashLayoutHome", userID);
					if (dashboardByID != null && dashboardByID.Tables.Count > 0 && dashboardByID.Tables[0].Rows.Count > 0)
					{
						result = Factory.DashboardSystem.DeleteDashboard("dashLayoutHome", UserID);
					}
					if (result)
					{
						result &= Factory.DashboardSystem.UpdateDashboardWithTemplate(templateName, UserID);
					}
				}
				if (result)
				{
					Close();
				}
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
		}

		private void EnterNameDialog_Activated(object sender, EventArgs e)
		{
		}

		private void comboBoxTemplate_SelectedIndexChanged(object sender, EventArgs e)
		{
			templateName = comboBoxTemplate.Text;
		}

		private void SelectDashboardTemplateDialog_Load(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			dataSet = Factory.DashboardSystem.GetAvailableDashboardTemplates();
			if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[1].Rows.Count > 0)
			{
				comboBoxTemplate.DataSource = dataSet.Tables[1];
				comboBoxTemplate.DisplayMember = dataSet.Tables[1].Columns[0].ToString();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.SelectDashboardTemplateDialog));
			buttonCancel = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			linePanelDown = new Micromind.UISupport.Line();
			comboBoxTemplate = new System.Windows.Forms.ComboBox();
			SuspendLayout();
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(279, 117);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(88, 25);
			buttonCancel.TabIndex = 0;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 51);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(38, 13);
			label1.TabIndex = 2;
			label1.Text = "Name:";
			buttonOK.Location = new System.Drawing.Point(186, 117);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 25);
			buttonOK.TabIndex = 0;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(button2_Click);
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(-41, 110);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(505, 1);
			linePanelDown.TabIndex = 15;
			linePanelDown.TabStop = false;
			comboBoxTemplate.FormattingEnabled = true;
			comboBoxTemplate.Location = new System.Drawing.Point(56, 48);
			comboBoxTemplate.Name = "comboBoxTemplate";
			comboBoxTemplate.Size = new System.Drawing.Size(307, 21);
			comboBoxTemplate.TabIndex = 16;
			comboBoxTemplate.SelectedIndexChanged += new System.EventHandler(comboBoxTemplate_SelectedIndexChanged);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(375, 146);
			base.Controls.Add(comboBoxTemplate);
			base.Controls.Add(linePanelDown);
			base.Controls.Add(label1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SelectDashboardTemplateDialog";
			Text = "Select Template";
			base.Activated += new System.EventHandler(EnterNameDialog_Activated);
			base.Load += new System.EventHandler(SelectDashboardTemplateDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
