using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class AddRelationDialog : Form
	{
		private CustomGadget gadgetData;

		private GadgetRelation relation;

		private IContainer components;

		private TextBox textBoxTableName;

		private Label label1;

		private Label label2;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private ComboBox comboBoxParentTable;

		private Label label3;

		private ComboBox comboBoxChildTable;

		private DataEntryGrid dataGridItems;

		public GadgetRelation Relationship
		{
			get
			{
				return relation;
			}
			set
			{
				relation = value;
			}
		}

		public CustomGadget GadgetData
		{
			get
			{
				return gadgetData;
			}
			set
			{
				gadgetData = value;
				comboBoxParentTable.Items.Clear();
				comboBoxChildTable.Items.Clear();
				foreach (CustomGadgetTable table in gadgetData.Tables)
				{
					comboBoxParentTable.Items.Add(table);
					comboBoxChildTable.Items.Add(table);
				}
			}
		}

		public string RelationshipName
		{
			get
			{
				return textBoxTableName.Text;
			}
			set
			{
				textBoxTableName.Text = value;
			}
		}

		public AddRelationDialog()
		{
			InitializeComponent();
			SetupGrid();
			comboBoxParentTable.SelectedIndexChanged += comboBoxParentTable_SelectedIndexChanged;
			comboBoxChildTable.SelectedIndexChanged += comboBoxChildTable_SelectedIndexChanged;
			textBoxTableName.Validating += textBoxTableName_Validating;
		}

		private void textBoxTableName_Validating(object sender, CancelEventArgs e)
		{
			textBoxTableName.Text.Replace(' ', '_');
		}

		private void comboBoxChildTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxChildTable.SelectedIndex >= 0)
			{
				CustomGadgetTable customGadgetTable = comboBoxChildTable.SelectedItem as CustomGadgetTable;
				DataSet tableSchema = Factory.CustomGadgetSystem.GetTableSchema(customGadgetTable.query);
				ValueList valueList = dataGridItems.DisplayLayout.ValueLists["ChildCols"];
				valueList.ValueListItems.Clear();
				foreach (DataColumn column in tableSchema.Tables[0].Columns)
				{
					valueList.ValueListItems.Add(column.ColumnName);
				}
			}
		}

		private void comboBoxParentTable_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBoxParentTable.SelectedIndex >= 0)
			{
				CustomGadgetTable customGadgetTable = comboBoxParentTable.SelectedItem as CustomGadgetTable;
				DataSet tableSchema = Factory.CustomGadgetSystem.GetTableSchema(customGadgetTable.query);
				ValueList valueList = dataGridItems.DisplayLayout.ValueLists["ParentCols"];
				valueList.ValueListItems.Clear();
				foreach (DataColumn column in tableSchema.Tables[0].Columns)
				{
					valueList.ValueListItems.Add(column.ColumnName);
				}
			}
		}

		private void SetupGrid()
		{
			try
			{
				dataGridItems.SetupUI();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("ParentColumn");
				dataTable.Columns.Add("ChildColumn");
				dataGridItems.DataSource = dataTable;
				dataGridItems.DisplayLayout.ValueLists.Add("ParentCols");
				dataGridItems.DisplayLayout.ValueLists.Add("ChildCols");
				dataGridItems.DisplayLayout.Bands[0].Columns["ParentColumn"].ValueList = dataGridItems.DisplayLayout.ValueLists["ParentCols"];
				dataGridItems.DisplayLayout.Bands[0].Columns["ChildColumn"].ValueList = dataGridItems.DisplayLayout.ValueLists["ChildCols"];
				dataGridItems.DisplayLayout.Bands[0].Columns["ParentColumn"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
				dataGridItems.DisplayLayout.Bands[0].Columns["ChildColumn"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownList;
			}
			catch (Exception e)
			{
				dataGridItems.LoadLayoutFailed = true;
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxTableName.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter a name for the relationship.");
				return;
			}
			if (char.IsDigit(textBoxTableName.Text[0]))
			{
				ErrorHelper.InformationMessage("Please enter a valid name for the relationship. Names cannot start with digit.");
				return;
			}
			if (dataGridItems.Rows.Count == 0)
			{
				ErrorHelper.InformationMessage("Please select enter at least one row for the relationship.");
				return;
			}
			foreach (UltraGridRow row in dataGridItems.Rows)
			{
				if (row.Cells["ParentColumn"].Value == null || row.Cells["ParentColumn"].Value.ToString() == "" || row.Cells["ChildColumn"].Value == null || row.Cells["ChildColumn"].Value.ToString() == "")
				{
					ErrorHelper.InformationMessage("Please select relationship columns.");
					return;
				}
			}
			relation = new GadgetRelation();
			relation.RelationName = textBoxTableName.Text;
			relation.ParentTableName = comboBoxParentTable.Text;
			relation.ChildTableName = comboBoxChildTable.Text;
			string[] array = new string[dataGridItems.Rows.Count];
			string[] array2 = new string[dataGridItems.Rows.Count];
			foreach (UltraGridRow row2 in dataGridItems.Rows)
			{
				array[row2.Index] = row2.Cells["ParentColumn"].Value.ToString();
				array2[row2.Index] = row2.Cells["ChildColumn"].Value.ToString();
			}
			relation.ParentColumns = array;
			relation.ChildColumns = array2;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void AddRelationDialog_Load(object sender, EventArgs e)
		{
		}

		public void LoadGadgetData(CustomGadget gadgetData)
		{
			comboBoxChildTable.Items.Clear();
			comboBoxParentTable.Items.Clear();
			foreach (CustomGadgetTable table in gadgetData.Tables)
			{
				comboBoxChildTable.Items.Add(table);
				comboBoxParentTable.Items.Add(table);
			}
		}

		public void EditRelation(GadgetRelation relation)
		{
			textBoxTableName.Text = relation.RelationName;
			comboBoxParentTable.Text = relation.ParentTableName;
			comboBoxChildTable.Text = relation.ChildTableName;
			DataTable dataTable = dataGridItems.DataSource as DataTable;
			for (int i = 0; i < relation.ParentColumns.Length; i = checked(i + 1))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["ParentColumn"] = relation.ParentColumns[i];
				dataRow["ChildColumn"] = relation.ChildColumns[i];
				dataTable.Rows.Add(dataRow);
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			textBoxTableName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			comboBoxParentTable = new System.Windows.Forms.ComboBox();
			label3 = new System.Windows.Forms.Label();
			comboBoxChildTable = new System.Windows.Forms.ComboBox();
			dataGridItems = new Micromind.DataControls.DataEntryGrid();
			((System.ComponentModel.ISupportInitialize)dataGridItems).BeginInit();
			SuspendLayout();
			textBoxTableName.Location = new System.Drawing.Point(117, 23);
			textBoxTableName.Name = "textBoxTableName";
			textBoxTableName.Size = new System.Drawing.Size(367, 20);
			textBoxTableName.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(99, 13);
			label1.TabIndex = 1;
			label1.Text = "Relationship Name:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 75);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(68, 13);
			label2.TabIndex = 1;
			label2.Text = "Parent Table";
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(407, 349);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(307, 349);
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
			line1.Location = new System.Drawing.Point(-54, 344);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(609, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			comboBoxParentTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxParentTable.FormattingEnabled = true;
			comboBoxParentTable.Location = new System.Drawing.Point(15, 91);
			comboBoxParentTable.Name = "comboBoxParentTable";
			comboBoxParentTable.Size = new System.Drawing.Size(230, 21);
			comboBoxParentTable.TabIndex = 4;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(258, 75);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(60, 13);
			label3.TabIndex = 1;
			label3.Text = "Child Table";
			comboBoxChildTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxChildTable.FormattingEnabled = true;
			comboBoxChildTable.Location = new System.Drawing.Point(261, 91);
			comboBoxChildTable.Name = "comboBoxChildTable";
			comboBoxChildTable.Size = new System.Drawing.Size(240, 21);
			comboBoxChildTable.TabIndex = 4;
			dataGridItems.AllowAddNew = false;
			dataGridItems.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridItems.DisplayLayout.Appearance = appearance;
			dataGridItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridItems.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridItems.DisplayLayout.MaxColScrollRegions = 1;
			dataGridItems.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridItems.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridItems.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			dataGridItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridItems.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridItems.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridItems.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridItems.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridItems.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridItems.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell;
			dataGridItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridItems.ExitEditModeOnLeave = false;
			dataGridItems.Location = new System.Drawing.Point(15, 118);
			dataGridItems.MinimumSize = new System.Drawing.Size(450, 50);
			dataGridItems.Name = "dataGridItems";
			dataGridItems.ShowDeleteMenu = true;
			dataGridItems.ShowInsertMenu = true;
			dataGridItems.Size = new System.Drawing.Size(486, 219);
			dataGridItems.TabIndex = 5;
			dataGridItems.Text = "dataEntryGrid1";
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(515, 384);
			base.Controls.Add(dataGridItems);
			base.Controls.Add(comboBoxChildTable);
			base.Controls.Add(comboBoxParentTable);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(label3);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxTableName);
			base.Name = "AddRelationDialog";
			Text = "Add Relationship";
			base.Load += new System.EventHandler(AddRelationDialog_Load);
			((System.ComponentModel.ISupportInitialize)dataGridItems).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
