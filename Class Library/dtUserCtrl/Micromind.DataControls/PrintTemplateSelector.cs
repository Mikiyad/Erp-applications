using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class PrintTemplateSelector : UserControl
	{
		private string formID = "";

		private string defaultTemplate = "";

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Label label1;

		private Button buttonTemplate;

		private TextBox textBoxTemplate;

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

		public string SelectedTemplate => textBoxTemplate.Text;

		public string DefaultTemplate
		{
			get
			{
				return defaultTemplate;
			}
			set
			{
				defaultTemplate = value;
			}
		}

		public string FormID
		{
			get
			{
				return formID;
			}
			set
			{
				formID = value;
			}
		}

		public PrintTemplateSelector()
		{
			InitializeComponent();
		}

		private void BankSelector_Load(object sender, EventArgs e)
		{
		}

		private void buttonTemplate_Click(object sender, EventArgs e)
		{
			if (!Security.IsAllowedSecurityRole(GeneralSecurityRoles.AllowDesignPrintTemplate) || Global.isUserAdmin)
			{
				SelectPrintTemplateDialog selectPrintTemplateDialog = new SelectPrintTemplateDialog();
				selectPrintTemplateDialog.SelectedTemplate = DefaultTemplate;
				selectPrintTemplateDialog.FormID = FormID;
				PrintTemplateMap.PrintTemplateFilterByFormID = FormID;
				_ = PrintTemplateMap.PrintTemplateMapData;
				selectPrintTemplateDialog.IsMultiSelect = false;
				selectPrintTemplateDialog.DataSource = PrintTemplateMap.PrintTemplateMapData;
				if (selectPrintTemplateDialog.ShowDialog(this) == DialogResult.OK)
				{
					textBoxTemplate.Text = selectPrintTemplateDialog.SelectedRow.Cells["Name"].Value.ToString();
				}
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
			label1 = new System.Windows.Forms.Label();
			buttonTemplate = new System.Windows.Forms.Button();
			textBoxTemplate = new System.Windows.Forms.TextBox();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(3, 7);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(57, 13);
			label1.TabIndex = 83;
			label1.Text = "Template :";
			buttonTemplate.Location = new System.Drawing.Point(326, 3);
			buttonTemplate.Name = "buttonTemplate";
			buttonTemplate.Size = new System.Drawing.Size(26, 21);
			buttonTemplate.TabIndex = 81;
			buttonTemplate.Text = "...";
			buttonTemplate.UseVisualStyleBackColor = true;
			buttonTemplate.Click += new System.EventHandler(buttonTemplate_Click);
			textBoxTemplate.Location = new System.Drawing.Point(65, 3);
			textBoxTemplate.Name = "textBoxTemplate";
			textBoxTemplate.ReadOnly = true;
			textBoxTemplate.Size = new System.Drawing.Size(260, 20);
			textBoxTemplate.TabIndex = 82;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(label1);
			base.Controls.Add(buttonTemplate);
			base.Controls.Add(textBoxTemplate);
			base.Name = "PrintTemplateSelector";
			base.Size = new System.Drawing.Size(366, 28);
			base.Load += new System.EventHandler(BankSelector_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
