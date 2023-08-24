using DevExpress.XtraCharts;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class ChartSerieSettingDialog : Form
	{
		public UltraGridRow GridRow;

		private IContainer components;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private CheckBox checkBoxShowLabel;

		private ComboBox comboBoxLabelPosition;

		private Label label2;

		private MMTextBox textBoxLabelPattern;

		private MMLabel mmLabel10;

		public ChartSerieSettingDialog()
		{
			InitializeComponent();
			base.DialogResult = DialogResult.None;
			base.Load += ChartSerieSettingDialog_Load;
		}

		private void ChartSerieSettingDialog_Load(object sender, EventArgs e)
		{
			try
			{
				checkBoxShowLabel.Checked = (!GridRow.Cells["LabelVisible"].Value.IsNullOrEmpty() && bool.Parse(GridRow.Cells["LabelVisible"].Value.ToString()));
				comboBoxLabelPosition.SelectedValue = GridRow.Cells["LabelPosition"].Value;
				textBoxLabelPattern.Text = GridRow.Cells["LabelTextPattern"].Value.ToString();
				ViewType viewType = (ViewType)int.Parse(GridRow.Cells["ChartType"].Value.ToString());
				Series series = new Series("Temp", viewType);
				comboBoxLabelPosition.Items.Clear();
				if (series.Label.GetType() == typeof(SideBySideBarSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("Auto", 0));
					comboBoxLabelPosition.Items.Add(new NameValue("Top", 1));
					comboBoxLabelPosition.Items.Add(new NameValue("Center", 2));
					comboBoxLabelPosition.Items.Add(new NameValue("TopInside", 3));
					comboBoxLabelPosition.Items.Add(new NameValue("BottomInside", 4));
				}
				else if (series.Label.GetType() == typeof(StackedBarSeriesLabel) || series.Label.GetType() == typeof(FullStackedBarSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("Center", 2));
					comboBoxLabelPosition.Items.Add(new NameValue("TopInside", 3));
					comboBoxLabelPosition.Items.Add(new NameValue("BottomInside", 4));
				}
				else if (series.Label.GetType() == typeof(PieSeriesLabel) || series.Label.GetType() == typeof(DoughnutSeriesLabel) || series.Label.GetType() == typeof(NestedDoughnutSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("Inside", 0));
					comboBoxLabelPosition.Items.Add(new NameValue("Outside", 1));
					comboBoxLabelPosition.Items.Add(new NameValue("TwoColumns", 2));
					comboBoxLabelPosition.Items.Add(new NameValue("Radial", 3));
					comboBoxLabelPosition.Items.Add(new NameValue("Tangent", 4));
				}
				else if (series.Label.GetType() == typeof(PointSeriesLabel) || series.Label.GetType() == typeof(BubbleSeriesLabel) || series.Label.GetType() == typeof(StackedLineSeriesLabel) || series.Label.GetType() == typeof(RadarPointSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("Center", 0));
					comboBoxLabelPosition.Items.Add(new NameValue("Outside", 1));
				}
				else if (series.Label.GetType() == typeof(FunnelSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("LeftColumn", 0));
					comboBoxLabelPosition.Items.Add(new NameValue("Left", 1));
					comboBoxLabelPosition.Items.Add(new NameValue("Center", 2));
					comboBoxLabelPosition.Items.Add(new NameValue("Right", 3));
					comboBoxLabelPosition.Items.Add(new NameValue("RightColumn", 4));
				}
				else if (series.Label.GetType() == typeof(RangeBarSeriesLabel))
				{
					comboBoxLabelPosition.Items.Add(new NameValue("Outside", 0));
					comboBoxLabelPosition.Items.Add(new NameValue("Inside", 1));
					comboBoxLabelPosition.Items.Add(new NameValue("Center", 2));
				}
				else
				{
					comboBoxLabelPosition.Items.Clear();
					comboBoxLabelPosition.Visible = false;
				}
				int num = -1;
				if (!GridRow.Cells["LabelPosition"].Value.IsNullOrEmpty())
				{
					num = int.Parse(GridRow.Cells["LabelPosition"].Value.ToString());
				}
				foreach (NameValue item in comboBoxLabelPosition.Items)
				{
					if (item.ID.ToString() == num.ToString())
					{
						comboBoxLabelPosition.SelectedItem = item;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				GridRow.Cells["LabelVisible"].Value = checkBoxShowLabel.Checked;
				if (!comboBoxLabelPosition.SelectedValue.IsNullOrEmpty())
				{
					GridRow.Cells["LabelPosition"].Value = comboBoxLabelPosition.SelectedValue.ToString();
				}
				else
				{
					GridRow.Cells["LabelPosition"].Value = DBNull.Value;
				}
				GridRow.Cells["LabelTextPattern"].Value = textBoxLabelPattern.Text;
				if (comboBoxLabelPosition.SelectedItem == null)
				{
					GridRow.Cells["LabelPosition"].Value = DBNull.Value;
				}
				else
				{
					GridRow.Cells["LabelPosition"].Value = ((NameValue)comboBoxLabelPosition.SelectedItem).ID;
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomDashboards.ChartSerieSettingDialog));
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			checkBoxShowLabel = new System.Windows.Forms.CheckBox();
			comboBoxLabelPosition = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			textBoxLabelPattern = new Micromind.UISupport.MMTextBox();
			mmLabel10 = new Micromind.UISupport.MMLabel();
			SuspendLayout();
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(384, 236);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 3;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(284, 236);
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
			line1.Location = new System.Drawing.Point(-54, 231);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(586, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			checkBoxShowLabel.AutoSize = true;
			checkBoxShowLabel.Location = new System.Drawing.Point(12, 24);
			checkBoxShowLabel.Name = "checkBoxShowLabel";
			checkBoxShowLabel.Size = new System.Drawing.Size(78, 17);
			checkBoxShowLabel.TabIndex = 6;
			checkBoxShowLabel.Text = "Show label";
			checkBoxShowLabel.UseVisualStyleBackColor = true;
			comboBoxLabelPosition.FormattingEnabled = true;
			comboBoxLabelPosition.Location = new System.Drawing.Point(196, 22);
			comboBoxLabelPosition.Name = "comboBoxLabelPosition";
			comboBoxLabelPosition.Size = new System.Drawing.Size(121, 21);
			comboBoxLabelPosition.TabIndex = 7;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(111, 25);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(75, 13);
			label2.TabIndex = 8;
			label2.Text = "Label position:";
			textBoxLabelPattern.BackColor = System.Drawing.Color.White;
			textBoxLabelPattern.CustomReportFieldName = "";
			textBoxLabelPattern.CustomReportKey = "";
			textBoxLabelPattern.CustomReportValueType = 1;
			textBoxLabelPattern.IsComboTextBox = false;
			textBoxLabelPattern.IsModified = false;
			textBoxLabelPattern.Location = new System.Drawing.Point(124, 49);
			textBoxLabelPattern.MaxLength = 30;
			textBoxLabelPattern.Name = "textBoxLabelPattern";
			textBoxLabelPattern.Size = new System.Drawing.Size(157, 20);
			textBoxLabelPattern.TabIndex = 28;
			mmLabel10.AutoSize = true;
			mmLabel10.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel10.IsFieldHeader = false;
			mmLabel10.IsRequired = false;
			mmLabel10.Location = new System.Drawing.Point(9, 53);
			mmLabel10.Name = "mmLabel10";
			mmLabel10.PenWidth = 1f;
			mmLabel10.ShowBorder = false;
			mmLabel10.Size = new System.Drawing.Size(83, 13);
			mmLabel10.TabIndex = 29;
			mmLabel10.Text = "Legend Pattern:";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(492, 271);
			base.Controls.Add(textBoxLabelPattern);
			base.Controls.Add(mmLabel10);
			base.Controls.Add(label2);
			base.Controls.Add(comboBoxLabelPosition);
			base.Controls.Add(checkBoxShowLabel);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChartSerieSettingDialog";
			Text = "Chart Serie Setting";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
