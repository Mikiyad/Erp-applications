using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder.Forms
{
	public class frmAddColumn : frmBaseForm
	{
		private TableCol mi_NewColumn;

		private string ms_DataBase;

		private string ms_Table;

		private Container components;

		private Label label3;

		private Label label4;

		private Label label1;

		private TextBox txtColName;

		private ComboBox comboType;

		private Button btnAdd;

		private Label label2;

		private TextBox txtSeed;

		private Label label5;

		private TextBox txtIncrement;

		private CheckBox checkNull;

		private CheckBox checkIdent;

		private TextBox txtDefault;

		public TableCol NewColumn => mi_NewColumn;

		public frmAddColumn(string s_DataBase, string s_Table)
		{
			ms_DataBase = s_DataBase;
			ms_Table = s_Table;
			InitializeComponent();
			base.DialogResult = DialogResult.Cancel;
		}

		protected override void OnLoadDelayed()
		{
		}

		private void OnCheckIdentity(object sender, EventArgs e)
		{
			txtIncrement.Enabled = checkIdent.Checked;
			txtSeed.Enabled = checkIdent.Checked;
			txtDefault.Enabled = !checkIdent.Checked;
			if (checkIdent.Checked)
			{
				checkNull.Checked = false;
				txtDefault.Text = "";
			}
		}

		private void OnCheckNull(object sender, EventArgs e)
		{
			if (checkNull.Checked)
			{
				checkIdent.Checked = false;
			}
		}

		private void OnButtonAdd(object sender, EventArgs e)
		{
			mi_NewColumn = new TableCol(ms_Table);
			mi_NewColumn.b_Identity = checkIdent.Checked;
			if (checkIdent.Checked)
			{
				mi_NewColumn.s32_Seed = (int)Functions.StrToDouble(txtSeed.Text);
				mi_NewColumn.s32_Increment = (int)Functions.StrToDouble(txtIncrement.Text);
				if (mi_NewColumn.s32_Seed <= 0 || mi_NewColumn.s32_Increment <= 0)
				{
					txtSeed.Focus();
					return;
				}
			}
			mi_NewColumn.b_Nullable = checkNull.Checked;
			mi_NewColumn.s_ColName = txtColName.Text;
			mi_NewColumn.s_FullType = comboType.Text;
			mi_NewColumn.s_Default = txtDefault.Text;
			base.DialogResult = DialogResult.OK;
			Close();
		}

		private void InitializeComponent()
		{
			System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager(typeof(Micromind.DataControls.QueryBuilder.Forms.frmAddColumn));
			txtColName = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			comboType = new System.Windows.Forms.ComboBox();
			btnAdd = new System.Windows.Forms.Button();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			txtDefault = new System.Windows.Forms.TextBox();
			txtSeed = new System.Windows.Forms.TextBox();
			label5 = new System.Windows.Forms.Label();
			txtIncrement = new System.Windows.Forms.TextBox();
			checkNull = new System.Windows.Forms.CheckBox();
			checkIdent = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			txtColName.Location = new System.Drawing.Point(14, 28);
			txtColName.Name = "txtColName";
			txtColName.Size = new System.Drawing.Size(232, 20);
			txtColName.TabIndex = 1;
			txtColName.Text = "";
			label1.Location = new System.Drawing.Point(14, 14);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(230, 14);
			label1.TabIndex = 0;
			label1.Text = "Column name:";
			label2.Location = new System.Drawing.Point(14, 58);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(224, 14);
			label2.TabIndex = 0;
			label2.Text = "Data type:                 Example:  varchar(255)";
			comboType.Location = new System.Drawing.Point(14, 74);
			comboType.MaxDropDownItems = 25;
			comboType.Name = "comboType";
			comboType.Size = new System.Drawing.Size(232, 21);
			comboType.TabIndex = 2;
			btnAdd.Location = new System.Drawing.Point(94, 252);
			btnAdd.Name = "btnAdd";
			btnAdd.TabIndex = 8;
			btnAdd.Text = "Add";
			btnAdd.Click += new System.EventHandler(OnButtonAdd);
			label3.ForeColor = System.Drawing.Color.DarkRed;
			label3.Location = new System.Drawing.Point(12, 204);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(224, 42);
			label3.TabIndex = 0;
			label3.Text = "To specify column properties like UNIQUE, PRIMARY KEY etc... use the toolbar button 'Table Designer' in the Table Editor !";
			label4.Location = new System.Drawing.Point(14, 106);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(232, 14);
			label4.TabIndex = 0;
			label4.Text = "Default value:          Example: 42 or 'Galaxis'";
			txtDefault.Location = new System.Drawing.Point(14, 120);
			txtDefault.Name = "txtDefault";
			txtDefault.Size = new System.Drawing.Size(232, 20);
			txtDefault.TabIndex = 3;
			txtDefault.Text = "";
			txtSeed.Enabled = false;
			txtSeed.Location = new System.Drawing.Point(120, 176);
			txtSeed.Name = "txtSeed";
			txtSeed.Size = new System.Drawing.Size(32, 20);
			txtSeed.TabIndex = 6;
			txtSeed.Text = "1";
			label5.Location = new System.Drawing.Point(166, 178);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(40, 16);
			label5.TabIndex = 0;
			label5.Text = "Increm";
			label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			txtIncrement.Enabled = false;
			txtIncrement.Location = new System.Drawing.Point(212, 176);
			txtIncrement.Name = "txtIncrement";
			txtIncrement.Size = new System.Drawing.Size(32, 20);
			txtIncrement.TabIndex = 7;
			txtIncrement.Text = "1";
			checkNull.Location = new System.Drawing.Point(16, 154);
			checkNull.Name = "checkNull";
			checkNull.Size = new System.Drawing.Size(104, 18);
			checkNull.TabIndex = 4;
			checkNull.Text = "Allow Null";
			checkNull.CheckedChanged += new System.EventHandler(OnCheckNull);
			checkIdent.Location = new System.Drawing.Point(16, 178);
			checkIdent.Name = "checkIdent";
			checkIdent.Size = new System.Drawing.Size(104, 18);
			checkIdent.TabIndex = 5;
			checkIdent.Text = "Identity      Seed";
			checkIdent.CheckedChanged += new System.EventHandler(OnCheckIdentity);
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(264, 285);
			base.Controls.Add(checkIdent);
			base.Controls.Add(checkNull);
			base.Controls.Add(txtIncrement);
			base.Controls.Add(txtSeed);
			base.Controls.Add(txtDefault);
			base.Controls.Add(txtColName);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(btnAdd);
			base.Controls.Add(comboType);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmAddColumn";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = " Add Column";
			ResumeLayout(false);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
