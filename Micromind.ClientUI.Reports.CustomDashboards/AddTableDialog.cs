using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.DataControls.QueryBuilder;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class AddTableDialog : Form
	{
		private CustomGadgetTable gadgetTable;

		private DataSet data = new DataSet();

		public DataSet dataSetValue = new DataSet();

		public bool isBind;

		private IContainer components;

		private TextBox textBoxTableName;

		private Label label1;

		private Label label2;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private Button buttonValidate;

		public QueryEditor textBoxQuery;

		public CustomGadgetTable GadgetTable => gadgetTable;

		public string TableName
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

		public bool IsBind
		{
			get
			{
				return isBind;
			}
			set
			{
				isBind = value;
			}
		}

		public DataSet DataSetValue
		{
			get
			{
				return dataSetValue;
			}
			set
			{
				dataSetValue = value;
			}
		}

		public string Query
		{
			get
			{
				return textBoxQuery.Text;
			}
			set
			{
				textBoxQuery.Text = value;
			}
		}

		public AddTableDialog()
		{
			InitializeComponent();
			textBoxQuery.GotFocus += TextBoxQuery_GotFocus;
			textBoxQuery.LostFocus += TextBoxQuery_LostFocus;
			base.CancelButton = buttonCancel;
		}

		private void TextBoxQuery_LostFocus(object sender, EventArgs e)
		{
			base.AcceptButton = buttonOK;
		}

		private void TextBoxQuery_GotFocus(object sender, EventArgs e)
		{
			base.AcceptButton = null;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxTableName.Text.Trim() == "")
			{
				ErrorHelper.InformationMessage("Please enter a name for the table.");
				return;
			}
			if (char.IsDigit(textBoxTableName.Text[0]))
			{
				ErrorHelper.InformationMessage("Please enter a valid name for the table. Names cannot start with digit.");
				return;
			}
			gadgetTable = new CustomGadgetTable();
			gadgetTable.query = textBoxQuery.Text;
			gadgetTable.tableName = textBoxTableName.Text;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public void EditTable(CustomGadgetTable table)
		{
			if (table != null)
			{
				textBoxTableName.Text = table.TableName;
				textBoxQuery.Text = table.query;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet reportByQuery = Factory.SmartListSystem.GetReportByQuery(textBoxQuery.Text, DateTime.Now, DateTime.Now);
				ErrorHelper.InformationMessage("Query executed successfully.");
				IsBind = true;
				DataSetValue = reportByQuery;
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage("Wrong query.", ex.Message);
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
			textBoxTableName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			textBoxQuery = new Micromind.DataControls.QueryBuilder.QueryEditor();
			buttonValidate = new System.Windows.Forms.Button();
			SuspendLayout();
			textBoxTableName.Location = new System.Drawing.Point(101, 23);
			textBoxTableName.Name = "textBoxTableName";
			textBoxTableName.Size = new System.Drawing.Size(350, 20);
			textBoxTableName.TabIndex = 0;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 26);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(68, 13);
			label1.TabIndex = 1;
			label1.Text = "Table Name:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 60);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 1;
			label2.Text = "Query:";
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(585, 408);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(485, 408);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(94, 29);
			buttonOK.TabIndex = 3;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-54, 402);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(787, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			textBoxQuery.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxQuery.BackColor = System.Drawing.Color.White;
			textBoxQuery.DetectUrls = false;
			textBoxQuery.FirstVisibleLine = 1;
			textBoxQuery.HideSelection = false;
			textBoxQuery.Location = new System.Drawing.Point(12, 76);
			textBoxQuery.Name = "textBoxQuery";
			textBoxQuery.Size = new System.Drawing.Size(667, 320);
			textBoxQuery.TabIndex = 1;
			textBoxQuery.Text = "";
			textBoxQuery.TextModified = false;
			buttonValidate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonValidate.Location = new System.Drawing.Point(11, 408);
			buttonValidate.Name = "buttonValidate";
			buttonValidate.Size = new System.Drawing.Size(104, 29);
			buttonValidate.TabIndex = 2;
			buttonValidate.Text = "&Validate Query";
			buttonValidate.UseVisualStyleBackColor = true;
			buttonValidate.Click += new System.EventHandler(button1_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(693, 442);
			base.Controls.Add(buttonValidate);
			base.Controls.Add(textBoxQuery);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxTableName);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			MinimumSize = new System.Drawing.Size(476, 265);
			base.Name = "AddTableDialog";
			base.ShowIcon = false;
			Text = "Add Table";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
