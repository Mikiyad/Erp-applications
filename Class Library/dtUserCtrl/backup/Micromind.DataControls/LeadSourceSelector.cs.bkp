using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class LeadSourceSelector : UserControl
	{
		private IContainer components;

		private Label labelTo;

		private RadioButton radioButtonAll;

		private RadioButton radioButtonRange;

		private Label label3;

		private RadioButton radioButtonSingle;

		private LeadSourceComboBox comboBoxLeadSourceFrom;

		private LeadSourceComboBox comboBoxLeadSourceTo;

		private LeadSourceComboBox comboBoxSingleLeadSource;

		public string FromSource
		{
			get
			{
				if (radioButtonRange.Checked)
				{
					return comboBoxLeadSourceFrom.SelectedID;
				}
				return "";
			}
		}

		public string ToSource
		{
			get
			{
				if (radioButtonRange.Checked)
				{
					return comboBoxLeadSourceTo.SelectedID;
				}
				return "";
			}
		}

		public LeadSourceSelector()
		{
			InitializeComponent();
		}

		private void LeadSelector_Load(object sender, EventArgs e)
		{
		}

		private void EnableDisableControls()
		{
			comboBoxSingleLeadSource.Enabled = radioButtonSingle.Checked;
			LeadSourceComboBox leadSourceComboBox = comboBoxLeadSourceFrom;
			bool enabled = comboBoxLeadSourceTo.Enabled = radioButtonRange.Checked;
			leadSourceComboBox.Enabled = enabled;
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
			labelTo = new System.Windows.Forms.Label();
			radioButtonAll = new System.Windows.Forms.RadioButton();
			radioButtonRange = new System.Windows.Forms.RadioButton();
			label3 = new System.Windows.Forms.Label();
			radioButtonSingle = new System.Windows.Forms.RadioButton();
			comboBoxLeadSourceTo = new Micromind.DataControls.LeadSourceComboBox();
			comboBoxLeadSourceFrom = new Micromind.DataControls.LeadSourceComboBox();
			comboBoxSingleLeadSource = new Micromind.DataControls.LeadSourceComboBox();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSourceTo).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSourceFrom).BeginInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLeadSource).BeginInit();
			SuspendLayout();
			labelTo.AutoSize = true;
			labelTo.Location = new System.Drawing.Point(265, 29);
			labelTo.Name = "labelTo";
			labelTo.Size = new System.Drawing.Size(23, 13);
			labelTo.TabIndex = 6;
			labelTo.Text = "To:";
			radioButtonAll.AutoSize = true;
			radioButtonAll.Checked = true;
			radioButtonAll.Location = new System.Drawing.Point(6, 5);
			radioButtonAll.Name = "radioButtonAll";
			radioButtonAll.Size = new System.Drawing.Size(78, 17);
			radioButtonAll.TabIndex = 0;
			radioButtonAll.TabStop = true;
			radioButtonAll.Text = "All Sources";
			radioButtonAll.UseVisualStyleBackColor = true;
			radioButtonAll.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			radioButtonRange.AutoSize = true;
			radioButtonRange.Location = new System.Drawing.Point(6, 28);
			radioButtonRange.Name = "radioButtonRange";
			radioButtonRange.Size = new System.Drawing.Size(60, 17);
			radioButtonRange.TabIndex = 3;
			radioButtonRange.Text = "Range:";
			radioButtonRange.UseVisualStyleBackColor = true;
			radioButtonRange.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(116, 30);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(33, 13);
			label3.TabIndex = 4;
			label3.Text = "From:";
			radioButtonSingle.AutoSize = true;
			radioButtonSingle.Location = new System.Drawing.Point(100, 5);
			radioButtonSingle.Name = "radioButtonSingle";
			radioButtonSingle.Size = new System.Drawing.Size(57, 17);
			radioButtonSingle.TabIndex = 1;
			radioButtonSingle.Text = "Single:";
			radioButtonSingle.UseVisualStyleBackColor = true;
			radioButtonSingle.CheckedChanged += new System.EventHandler(radioButtons_CheckedChanged);
			comboBoxLeadSourceTo.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadSourceTo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadSourceTo.DescriptionTextBox = null;
			comboBoxLeadSourceTo.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadSourceTo.Editable = true;
			comboBoxLeadSourceTo.Enabled = false;
			comboBoxLeadSourceTo.FilterString = "";
			comboBoxLeadSourceTo.HasAll = false;
			comboBoxLeadSourceTo.HasCustom = false;
			comboBoxLeadSourceTo.Location = new System.Drawing.Point(294, 27);
			comboBoxLeadSourceTo.MaxDropDownItems = 12;
			comboBoxLeadSourceTo.Name = "comboBoxLeadSourceTo";
			comboBoxLeadSourceTo.ShowInactiveItems = false;
			comboBoxLeadSourceTo.ShowQuickAdd = true;
			comboBoxLeadSourceTo.Size = new System.Drawing.Size(103, 20);
			comboBoxLeadSourceTo.TabIndex = 14;
			comboBoxLeadSourceTo.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxLeadSourceFrom.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxLeadSourceFrom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxLeadSourceFrom.DescriptionTextBox = null;
			comboBoxLeadSourceFrom.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxLeadSourceFrom.Editable = true;
			comboBoxLeadSourceFrom.Enabled = false;
			comboBoxLeadSourceFrom.FilterString = "";
			comboBoxLeadSourceFrom.HasAll = false;
			comboBoxLeadSourceFrom.HasCustom = false;
			comboBoxLeadSourceFrom.Location = new System.Drawing.Point(161, 27);
			comboBoxLeadSourceFrom.MaxDropDownItems = 12;
			comboBoxLeadSourceFrom.Name = "comboBoxLeadSourceFrom";
			comboBoxLeadSourceFrom.ShowInactiveItems = false;
			comboBoxLeadSourceFrom.ShowQuickAdd = true;
			comboBoxLeadSourceFrom.Size = new System.Drawing.Size(103, 20);
			comboBoxLeadSourceFrom.TabIndex = 13;
			comboBoxLeadSourceFrom.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxSingleLeadSource.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append;
			comboBoxSingleLeadSource.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxSingleLeadSource.DescriptionTextBox = null;
			comboBoxSingleLeadSource.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxSingleLeadSource.Editable = true;
			comboBoxSingleLeadSource.Enabled = false;
			comboBoxSingleLeadSource.FilterString = "";
			comboBoxSingleLeadSource.HasAll = false;
			comboBoxSingleLeadSource.HasCustom = false;
			comboBoxSingleLeadSource.Location = new System.Drawing.Point(161, 4);
			comboBoxSingleLeadSource.MaxDropDownItems = 12;
			comboBoxSingleLeadSource.Name = "comboBoxSingleLeadSource";
			comboBoxSingleLeadSource.ShowInactiveItems = false;
			comboBoxSingleLeadSource.ShowQuickAdd = true;
			comboBoxSingleLeadSource.Size = new System.Drawing.Size(236, 20);
			comboBoxSingleLeadSource.TabIndex = 15;
			comboBoxSingleLeadSource.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.Transparent;
			base.Controls.Add(comboBoxSingleLeadSource);
			base.Controls.Add(comboBoxLeadSourceTo);
			base.Controls.Add(comboBoxLeadSourceFrom);
			base.Controls.Add(radioButtonSingle);
			base.Controls.Add(radioButtonRange);
			base.Controls.Add(radioButtonAll);
			base.Controls.Add(label3);
			base.Controls.Add(labelTo);
			base.Name = "LeadSourceSelector";
			base.Size = new System.Drawing.Size(414, 53);
			base.Load += new System.EventHandler(LeadSelector_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSourceTo).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxLeadSourceFrom).EndInit();
			((System.ComponentModel.ISupportInitialize)comboBoxSingleLeadSource).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
