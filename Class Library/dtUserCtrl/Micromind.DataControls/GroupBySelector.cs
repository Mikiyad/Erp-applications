using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class GroupBySelector : UserControl, ICustomReportControl
	{
		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		private IContainer components;

		private Button buttonMultiple;

		private RadioButton radioButtonGroupBy;

		private TextBox textBoxGroupBy;

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

		public GroupBySelector()
		{
			InitializeComponent();
		}

		private void GroupBySelector_Load(object sender, EventArgs e)
		{
		}

		public string GetParameterValue()
		{
			return "";
		}

		private void EnableDisableControls()
		{
		}

		private void radioButtons_CheckedChanged(object sender, EventArgs e)
		{
			EnableDisableControls();
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
			buttonMultiple = new System.Windows.Forms.Button();
			radioButtonGroupBy = new System.Windows.Forms.RadioButton();
			textBoxGroupBy = new System.Windows.Forms.TextBox();
			SuspendLayout();
			buttonMultiple.Location = new System.Drawing.Point(361, 1);
			buttonMultiple.Name = "buttonMultiple";
			buttonMultiple.Size = new System.Drawing.Size(48, 23);
			buttonMultiple.TabIndex = 58;
			buttonMultiple.Text = "...";
			buttonMultiple.UseVisualStyleBackColor = true;
			radioButtonGroupBy.AutoSize = true;
			radioButtonGroupBy.Location = new System.Drawing.Point(1, 4);
			radioButtonGroupBy.Name = "radioButtonGroupBy";
			radioButtonGroupBy.Size = new System.Drawing.Size(69, 17);
			radioButtonGroupBy.TabIndex = 57;
			radioButtonGroupBy.Text = "Group By";
			radioButtonGroupBy.UseVisualStyleBackColor = true;
			textBoxGroupBy.Location = new System.Drawing.Point(76, 3);
			textBoxGroupBy.Name = "textBoxGroupBy";
			textBoxGroupBy.ReadOnly = true;
			textBoxGroupBy.Size = new System.Drawing.Size(281, 20);
			textBoxGroupBy.TabIndex = 56;
			textBoxGroupBy.Visible = false;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(buttonMultiple);
			base.Controls.Add(radioButtonGroupBy);
			base.Controls.Add(textBoxGroupBy);
			base.Name = "GroupBySelector";
			base.Size = new System.Drawing.Size(430, 48);
			base.Load += new System.EventHandler(GroupBySelector_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
