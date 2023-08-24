using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder.Forms
{
	public class frmFind : frmBaseForm
	{
		public class cParam
		{
			public string s_Find = "";

			public string s_Replace = "";

			public bool b_Replace;

			public bool b_WholeWord;

			public bool b_CheckExtra1;

			public string s_CheckExtra1;
		}

		public delegate string delFindReplace(cParam i_Param);

		private delFindReplace mi_Callback;

		private int ms32_Line;

		private QueryEditor queryEditor1;

		private int ms32_TotalLines;

		private TextBox txtInput;

		private Label lblHeading;

		private Button btnExecute;

		private TextBox txtReplace;

		private Button btnReplace;

		private Label lblReplace;

		private CheckBox checkWholeWord;

		private Container components;

		private CheckBox checkExtra1;

		public int LineNumber => ms32_Line;

		public frmFind(int s32_TotalLines, cParam i_Param, delFindReplace i_Callback)
		{
			InitializeComponent();
			ms32_TotalLines = s32_TotalLines;
			mi_Callback = i_Callback;
			base.DialogResult = DialogResult.Cancel;
			string[] array = i_Param.s_Find.Replace("\r", "").Split('\n');
			txtInput.Text = Functions.Left(array[0], 50);
			txtReplace.Text = i_Param.s_Replace;
			checkWholeWord.Checked = i_Param.b_WholeWord;
			if (i_Param.s_CheckExtra1 != null)
			{
				checkExtra1.Text = i_Param.s_CheckExtra1;
				checkExtra1.Checked = i_Param.b_CheckExtra1;
			}
			else
			{
				checkExtra1.Visible = false;
			}
			lblReplace.Enabled = i_Param.b_Replace;
			txtReplace.Enabled = i_Param.b_Replace;
			btnReplace.Enabled = i_Param.b_Replace;
			if (mi_Callback == null)
			{
				Text = " Goto";
				btnExecute.Text = "Goto";
				lblHeading.Text = "Line";
				checkWholeWord.Enabled = false;
				return;
			}
			if (i_Param.b_Replace)
			{
				Text = " Find / Replace";
			}
			else
			{
				Text = " Find";
			}
			btnExecute.Text = "Find";
			lblHeading.Text = "Search Text";
		}

		private void OnButtonKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void OnTxtInputKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
			if (e.KeyCode == Keys.Return)
			{
				OnButtonExecute(sender, e);
			}
		}

		private void OnTxtReplaceKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}

		private void OnButtonReplace(object sender, EventArgs e)
		{
			if (txtInput.Text.Length != 0)
			{
				new cParam
				{
					b_Replace = true,
					b_WholeWord = checkWholeWord.Checked,
					b_CheckExtra1 = checkExtra1.Checked,
					s_Find = txtInput.Text,
					s_Replace = txtReplace.Text
				};
			}
		}

		private void OnButtonExecute(object sender, EventArgs e)
		{
			if (mi_Callback != null)
			{
				if (txtInput.Text.Length != 0)
				{
					new cParam
					{
						b_Replace = false,
						b_WholeWord = checkWholeWord.Checked,
						b_CheckExtra1 = checkExtra1.Checked,
						s_Find = txtInput.Text
					};
				}
			}
			else
			{
				try
				{
					ms32_Line = int.Parse(txtInput.Text);
				}
				catch
				{
					ms32_Line = -1;
				}
				if (ms32_Line > 0 && ms32_Line <= ms32_TotalLines)
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
		}

		private void OnTxtBoxEnter(object sender, EventArgs e)
		{
			((TextBox)sender).SelectAll();
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.DataControls.QueryBuilder.Forms.frmFind));
			txtInput = new System.Windows.Forms.TextBox();
			lblHeading = new System.Windows.Forms.Label();
			btnExecute = new System.Windows.Forms.Button();
			txtReplace = new System.Windows.Forms.TextBox();
			btnReplace = new System.Windows.Forms.Button();
			lblReplace = new System.Windows.Forms.Label();
			checkWholeWord = new System.Windows.Forms.CheckBox();
			checkExtra1 = new System.Windows.Forms.CheckBox();
			queryEditor1 = new Micromind.DataControls.QueryBuilder.QueryEditor();
			SuspendLayout();
			txtInput.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			txtInput.Location = new System.Drawing.Point(8, 20);
			txtInput.Name = "txtInput";
			txtInput.Size = new System.Drawing.Size(336, 20);
			txtInput.TabIndex = 1;
			txtInput.Enter += new System.EventHandler(OnTxtBoxEnter);
			txtInput.KeyUp += new System.Windows.Forms.KeyEventHandler(OnTxtInputKeyUp);
			lblHeading.Location = new System.Drawing.Point(8, 4);
			lblHeading.Name = "lblHeading";
			lblHeading.Size = new System.Drawing.Size(178, 12);
			lblHeading.TabIndex = 0;
			lblHeading.Text = "Text";
			btnExecute.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			btnExecute.Location = new System.Drawing.Point(354, 20);
			btnExecute.Name = "btnExecute";
			btnExecute.Size = new System.Drawing.Size(80, 22);
			btnExecute.TabIndex = 3;
			btnExecute.Text = "Exec";
			btnExecute.Click += new System.EventHandler(OnButtonExecute);
			btnExecute.KeyUp += new System.Windows.Forms.KeyEventHandler(OnButtonKeyUp);
			txtReplace.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			txtReplace.Location = new System.Drawing.Point(8, 64);
			txtReplace.Name = "txtReplace";
			txtReplace.Size = new System.Drawing.Size(336, 20);
			txtReplace.TabIndex = 2;
			txtReplace.Enter += new System.EventHandler(OnTxtBoxEnter);
			txtReplace.KeyUp += new System.Windows.Forms.KeyEventHandler(OnTxtReplaceKeyUp);
			btnReplace.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			btnReplace.Location = new System.Drawing.Point(354, 62);
			btnReplace.Name = "btnReplace";
			btnReplace.Size = new System.Drawing.Size(80, 22);
			btnReplace.TabIndex = 4;
			btnReplace.Text = "Replace";
			btnReplace.Click += new System.EventHandler(OnButtonReplace);
			btnReplace.KeyUp += new System.Windows.Forms.KeyEventHandler(OnButtonKeyUp);
			lblReplace.Location = new System.Drawing.Point(8, 48);
			lblReplace.Name = "lblReplace";
			lblReplace.Size = new System.Drawing.Size(178, 12);
			lblReplace.TabIndex = 0;
			lblReplace.Text = "Replace with";
			checkWholeWord.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			checkWholeWord.Checked = true;
			checkWholeWord.CheckState = System.Windows.Forms.CheckState.Checked;
			checkWholeWord.Location = new System.Drawing.Point(8, 90);
			checkWholeWord.Name = "checkWholeWord";
			checkWholeWord.Size = new System.Drawing.Size(418, 16);
			checkWholeWord.TabIndex = 5;
			checkWholeWord.Text = "Search only whole words";
			checkExtra1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			checkExtra1.Location = new System.Drawing.Point(8, 110);
			checkExtra1.Name = "checkExtra1";
			checkExtra1.Size = new System.Drawing.Size(418, 16);
			checkExtra1.TabIndex = 6;
			checkExtra1.Text = "Checkbox Extra 1";
			queryEditor1.BackColor = System.Drawing.Color.White;
			queryEditor1.DetectUrls = false;
			queryEditor1.FirstVisibleLine = 1;
			queryEditor1.HideSelection = false;
			queryEditor1.Location = new System.Drawing.Point(225, 90);
			queryEditor1.Name = "queryEditor1";
			queryEditor1.Size = new System.Drawing.Size(129, 67);
			queryEditor1.TabIndex = 7;
			queryEditor1.Text = "";
			queryEditor1.TextModified = false;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			base.ClientSize = new System.Drawing.Size(442, 158);
			base.Controls.Add(queryEditor1);
			base.Controls.Add(checkExtra1);
			base.Controls.Add(checkWholeWord);
			base.Controls.Add(txtReplace);
			base.Controls.Add(txtInput);
			base.Controls.Add(lblReplace);
			base.Controls.Add(btnReplace);
			base.Controls.Add(btnExecute);
			base.Controls.Add(lblHeading);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmFind";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = " SqlBuilder";
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
