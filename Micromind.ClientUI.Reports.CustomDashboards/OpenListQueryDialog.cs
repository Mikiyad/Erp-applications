using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.DataControls.QueryBuilder;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports.CustomDashboards
{
	public class OpenListQueryDialog : Form
	{
		private CustomGadgetTable gadgetTable;

		private IContainer components;

		private Label label2;

		private Button buttonCancel;

		private Button buttonOK;

		private Line line1;

		private MMTextBox textBoxName;

		private MMTextBox textBoxCode;

		private MMLabel mmLabel3;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private MMTextBox textBoxDocType;

		public QueryEditor textBoxQuery;

		private Button buttonValidate;

		public CustomGadgetTable GadgetTable => gadgetTable;

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

		public string DocID
		{
			set
			{
				textBoxCode.Text = value;
			}
		}

		public string DocName
		{
			set
			{
				textBoxName.Text = value;
			}
		}

		public string DocType
		{
			set
			{
				textBoxDocType.Text = value;
			}
		}

		public OpenListQueryDialog()
		{
			InitializeComponent();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
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
				textBoxQuery.Text = table.query;
			}
		}

		private void buttonValidate_Click(object sender, EventArgs e)
		{
			try
			{
				Factory.SmartListSystem.GetReportByQuery(textBoxQuery.Text, DateTime.Now, DateTime.Now);
				ErrorHelper.InformationMessage("Query executed successfully.");
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
			label2 = new System.Windows.Forms.Label();
			buttonCancel = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxDocType = new Micromind.UISupport.MMTextBox();
			textBoxQuery = new Micromind.DataControls.QueryBuilder.QueryEditor();
			buttonValidate = new System.Windows.Forms.Button();
			SuspendLayout();
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 60);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 1;
			label2.Text = "Query:";
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.Location = new System.Drawing.Point(494, 285);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(94, 29);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(394, 285);
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
			line1.Location = new System.Drawing.Point(-2, 274);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(606, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(94, 30);
			textBoxName.MaxLength = 64;
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(341, 20);
			textBoxName.TabIndex = 5;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(366, 7);
			textBoxCode.MaxLength = 7;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 4;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(12, 33);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(61, 13);
			mmLabel3.TabIndex = 21;
			mmLabel3.Text = "Doc Name:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(316, 10);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(44, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Doc ID:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(12, 10);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(57, 13);
			mmLabel2.TabIndex = 24;
			mmLabel2.Text = "Doc Type:";
			textBoxDocType.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDocType.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxDocType.CustomReportFieldName = "";
			textBoxDocType.CustomReportKey = "";
			textBoxDocType.CustomReportValueType = 1;
			textBoxDocType.IsComboTextBox = false;
			textBoxDocType.IsModified = false;
			textBoxDocType.Location = new System.Drawing.Point(94, 7);
			textBoxDocType.MaxLength = 7;
			textBoxDocType.Name = "textBoxDocType";
			textBoxDocType.ReadOnly = true;
			textBoxDocType.Size = new System.Drawing.Size(179, 20);
			textBoxDocType.TabIndex = 23;
			textBoxQuery.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxQuery.BackColor = System.Drawing.Color.White;
			textBoxQuery.DetectUrls = false;
			textBoxQuery.FirstVisibleLine = 1;
			textBoxQuery.HideSelection = false;
			textBoxQuery.Location = new System.Drawing.Point(12, 76);
			textBoxQuery.Name = "textBoxQuery";
			textBoxQuery.Size = new System.Drawing.Size(576, 192);
			textBoxQuery.TabIndex = 25;
			textBoxQuery.Text = "";
			textBoxQuery.TextModified = false;
			buttonValidate.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonValidate.Location = new System.Drawing.Point(12, 285);
			buttonValidate.Name = "buttonValidate";
			buttonValidate.Size = new System.Drawing.Size(104, 29);
			buttonValidate.TabIndex = 26;
			buttonValidate.Text = "&Validate Query";
			buttonValidate.UseVisualStyleBackColor = true;
			buttonValidate.Click += new System.EventHandler(buttonValidate_Click);
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(602, 320);
			base.Controls.Add(buttonValidate);
			base.Controls.Add(textBoxQuery);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxDocType);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(label2);
			base.Name = "OpenListQueryDialog";
			base.ShowIcon = false;
			Text = "Open List Query ";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
