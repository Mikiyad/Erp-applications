using Infragistics.Win;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.OtherControls
{
	public class ReportFilterControl : UserControl
	{
		private List<ConditionColumn> groupByColumns = new List<ConditionColumn>();

		private List<ConditionColumn> sortColumns = new List<ConditionColumn>();

		private List<ConditionColumn> conditionColumns = new List<ConditionColumn>();

		private IContainer components;

		private ReportFilterGrid reportFilterGrid1;

		private ComboBox comboBoxGroupby;

		private Label label1;

		private Panel panelGroupBy;

		private ComboBox comboBoxASC;

		private ComboBox comboBoxSort;

		private Label label2;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ConditionColumn> ConditionColumns
		{
			get
			{
				return conditionColumns;
			}
			set
			{
				conditionColumns = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ConditionColumn> GroupByColumns
		{
			get
			{
				return groupByColumns;
			}
			set
			{
				groupByColumns = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ConditionColumn> SortColumns
		{
			get
			{
				return sortColumns;
			}
			set
			{
				sortColumns = value;
			}
		}

		public ReportFilterControl()
		{
			InitializeComponent();
			comboBoxASC.SelectedIndex = 0;
		}

		public void SetupControl()
		{
			ValueList valueList = new ValueList();
			if (ConditionColumns != null)
			{
				foreach (ConditionColumn conditionColumn in ConditionColumns)
				{
					valueList.ValueListItems.Add(conditionColumn.ColumnName, conditionColumn.DisplayText);
				}
				reportFilterGrid1.DataGrid.DisplayLayout.Bands[0].Columns["ColumnName"].ValueList = valueList;
			}
			if (GroupByColumns != null)
			{
				foreach (ConditionColumn groupByColumn in GroupByColumns)
				{
					comboBoxGroupby.Items.Add(groupByColumn);
				}
			}
			if (SortColumns != null)
			{
				foreach (ConditionColumn sortColumn in SortColumns)
				{
					comboBoxSort.Items.Add(sortColumn);
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
			comboBoxGroupby = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			panelGroupBy = new System.Windows.Forms.Panel();
			comboBoxASC = new System.Windows.Forms.ComboBox();
			comboBoxSort = new System.Windows.Forms.ComboBox();
			label2 = new System.Windows.Forms.Label();
			reportFilterGrid1 = new Micromind.DataControls.ReportFilterGrid();
			panelGroupBy.SuspendLayout();
			SuspendLayout();
			comboBoxGroupby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxGroupby.FormattingEnabled = true;
			comboBoxGroupby.Location = new System.Drawing.Point(76, 3);
			comboBoxGroupby.Name = "comboBoxGroupby";
			comboBoxGroupby.Size = new System.Drawing.Size(121, 21);
			comboBoxGroupby.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(16, 6);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(54, 13);
			label1.TabIndex = 2;
			label1.Text = "Group By:";
			panelGroupBy.Controls.Add(comboBoxASC);
			panelGroupBy.Controls.Add(comboBoxSort);
			panelGroupBy.Controls.Add(label2);
			panelGroupBy.Controls.Add(comboBoxGroupby);
			panelGroupBy.Controls.Add(label1);
			panelGroupBy.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelGroupBy.Location = new System.Drawing.Point(0, 244);
			panelGroupBy.Name = "panelGroupBy";
			panelGroupBy.Size = new System.Drawing.Size(573, 53);
			panelGroupBy.TabIndex = 3;
			comboBoxASC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxASC.FormattingEnabled = true;
			comboBoxASC.Items.AddRange(new object[2]
			{
				"ASC",
				"DESC"
			});
			comboBoxASC.Location = new System.Drawing.Point(203, 29);
			comboBoxASC.Name = "comboBoxASC";
			comboBoxASC.Size = new System.Drawing.Size(90, 21);
			comboBoxASC.TabIndex = 3;
			comboBoxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxSort.FormattingEnabled = true;
			comboBoxSort.Location = new System.Drawing.Point(76, 29);
			comboBoxSort.Name = "comboBoxSort";
			comboBoxSort.Size = new System.Drawing.Size(121, 21);
			comboBoxSort.TabIndex = 3;
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(16, 32);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(44, 13);
			label2.TabIndex = 4;
			label2.Text = "Sort By:";
			reportFilterGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			reportFilterGrid1.Location = new System.Drawing.Point(0, 0);
			reportFilterGrid1.Name = "reportFilterGrid1";
			reportFilterGrid1.Size = new System.Drawing.Size(573, 244);
			reportFilterGrid1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(reportFilterGrid1);
			base.Controls.Add(panelGroupBy);
			base.Name = "ReportFilterControl";
			base.Size = new System.Drawing.Size(573, 297);
			panelGroupBy.ResumeLayout(false);
			panelGroupBy.PerformLayout();
			ResumeLayout(false);
		}
	}
}
