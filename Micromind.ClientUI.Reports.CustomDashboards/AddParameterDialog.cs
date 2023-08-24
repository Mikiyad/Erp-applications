using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class AddParameterDialog : Form
	{
		private GadgetParameter gadgetParameter;

		private IContainer components;

		private TextBox textBoxParameterName;

		private Label label1;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private ComboBox comboBoxDataType;

		private Label label2;

		public GadgetParameter GadgetParameter => gadgetParameter;

		public string ParameterName
		{
			get
			{
				return textBoxParameterName.Text;
			}
			set
			{
				textBoxParameterName.Text = value;
			}
		}

		public int DataType
		{
			get
			{
				return checked(comboBoxDataType.SelectedIndex + 1);
			}
			set
			{
				comboBoxDataType.SelectedIndex = checked(value - 1);
			}
		}

		public AddParameterDialog()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxParameterName.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter a name for the parameter.");
				return;
			}
			if (textBoxParameterName.Text[0] != '@')
			{
				ErrorHelper.InformationMessage("Parameter name must start with @ character.");
				return;
			}
			gadgetParameter = new GadgetParameter();
			gadgetParameter.ParameterName = textBoxParameterName.Text;
			gadgetParameter.ParameterType = checked(comboBoxDataType.SelectedIndex + 1);
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public void EditParameter(GadgetParameter parameter)
		{
			if (parameter != null)
			{
				textBoxParameterName.Text = parameter.ParameterName;
				comboBoxDataType.SelectedIndex = checked(parameter.ParameterType - 1);
			}
		}

		private void AddParameterDialog_Load(object sender, EventArgs e)
		{
			comboBoxDataType.SelectedIndex = 0;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomDashboards.AddParameterDialog));
			textBoxParameterName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			comboBoxDataType = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			SuspendLayout();
			textBoxParameterName.Location = new System.Drawing.Point(101, 23);
			textBoxParameterName.Name = "textBoxParameterName";
			textBoxParameterName.Size = new System.Drawing.Size(252, 20);
			textBoxParameterName.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(89, 13);
			label1.TabIndex = 1;
			label1.Text = "Parameter Name:";
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(273, 132);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(173, 132);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(94, 29);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 127);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(475, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDataType.FormattingEnabled = true;
			comboBoxDataType.Items.AddRange(new object[3]
			{
				"String",
				"DateTime",
				"Numeric"
			});
			comboBoxDataType.Location = new System.Drawing.Point(101, 49);
			comboBoxDataType.Name = "comboBoxDataType";
			comboBoxDataType.Size = new System.Drawing.Size(117, 21);
			comboBoxDataType.TabIndex = 1;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 52);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(60, 13);
			label2.TabIndex = 1;
			label2.Text = "Data Type:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(381, 167);
			base.Controls.Add(comboBoxDataType);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxParameterName);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "AddParameterDialog";
			Text = "Add Parameter";
			base.Load += new System.EventHandler(AddParameterDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
