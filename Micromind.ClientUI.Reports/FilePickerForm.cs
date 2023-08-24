using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class FilePickerForm : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		public ExternalReportTypes ReportType = ExternalReportTypes.All;

		private Label label1;

		private MMTextBox textBoxFile;

		private XPButton buttonBrowse;

		private XPButton buttonOk;

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog;

		private MMTextBox textBoxSep;

		private Label labelSep;

		private CheckBox checkBoxRunApplication;

		private CheckBox checkBoxPrint;

		private Line line2;

		private CheckBox checkBoxEmailAfterSaving;

		private Container components;

		public string Separator
		{
			get
			{
				return textBoxSep.Text;
			}
			set
			{
				textBoxSep.Text = value;
			}
		}

		public string FileName
		{
			get
			{
				return textBoxFile.Text;
			}
			set
			{
				openFileDialog.FileName = value;
			}
		}

		public bool Email => checkBoxEmailAfterSaving.Checked;

		public bool RunApplication => checkBoxRunApplication.Checked;

		public bool PrintFile => checkBoxPrint.Checked;

		public FilePickerForm()
		{
			InitializeComponent();
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
			label1 = new System.Windows.Forms.Label();
			textBoxFile = new Micromind.UISupport.MMTextBox();
			buttonBrowse = new Micromind.UISupport.XPButton();
			buttonOk = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			textBoxSep = new Micromind.UISupport.MMTextBox();
			labelSep = new System.Windows.Forms.Label();
			checkBoxRunApplication = new System.Windows.Forms.CheckBox();
			checkBoxPrint = new System.Windows.Forms.CheckBox();
			line2 = new Micromind.UISupport.Line();
			checkBoxEmailAfterSaving = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			label1.AutoSize = true;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.Location = new System.Drawing.Point(8, 16);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(26, 16);
			label1.TabIndex = 0;
			label1.Text = "&File:";
			textBoxFile.AcceptsReturn = false;
			textBoxFile.AcceptsTab = false;
			textBoxFile.AutoSize = true;
			textBoxFile.BackColor = System.Drawing.Color.White;
			textBoxFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxFile.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxFile.Enabled = false;
			textBoxFile.HideSelection = true;
			textBoxFile.IsComboTextBox = false;
			textBoxFile.Lines = new string[0];
			textBoxFile.Location = new System.Drawing.Point(64, 16);
			textBoxFile.MaxLength = 255;
			textBoxFile.Multiline = false;
			textBoxFile.Name = "textBoxFile";
			textBoxFile.PasswordChar = '\0';
			textBoxFile.ReadOnly = false;
			textBoxFile.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxFile.Size = new System.Drawing.Size(280, 20);
			textBoxFile.TabIndex = 1;
			textBoxFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxFile.WordWrap = true;
			buttonBrowse.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonBrowse.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonBrowse.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonBrowse.Location = new System.Drawing.Point(352, 16);
			buttonBrowse.Name = "buttonBrowse";
			buttonBrowse.Size = new System.Drawing.Size(64, 24);
			buttonBrowse.TabIndex = 2;
			buttonBrowse.Text = "&Browse...";
			buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);
			buttonOk.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOk.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOk.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOk.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOk.Location = new System.Drawing.Point(283, 120);
			buttonOk.Name = "buttonOk";
			buttonOk.Size = new System.Drawing.Size(64, 24);
			buttonOk.TabIndex = 8;
			buttonOk.Text = "&OK";
			buttonOk.Click += new System.EventHandler(buttonOk_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(352, 120);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(64, 24);
			buttonCancel.TabIndex = 9;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			textBoxSep.AcceptsReturn = false;
			textBoxSep.AcceptsTab = false;
			textBoxSep.AutoSize = true;
			textBoxSep.BackColor = System.Drawing.Color.White;
			textBoxSep.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxSep.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxSep.HideSelection = true;
			textBoxSep.IsComboTextBox = false;
			textBoxSep.Lines = new string[1]
			{
				","
			};
			textBoxSep.Location = new System.Drawing.Point(65, 40);
			textBoxSep.MaxLength = 2;
			textBoxSep.Multiline = false;
			textBoxSep.Name = "textBoxSep";
			textBoxSep.PasswordChar = '\0';
			textBoxSep.ReadOnly = false;
			textBoxSep.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxSep.Size = new System.Drawing.Size(39, 20);
			textBoxSep.TabIndex = 4;
			textBoxSep.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxSep.Visible = false;
			textBoxSep.WordWrap = true;
			labelSep.AutoSize = true;
			labelSep.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelSep.Location = new System.Drawing.Point(8, 40);
			labelSep.Name = "labelSep";
			labelSep.Size = new System.Drawing.Size(57, 16);
			labelSep.TabIndex = 3;
			labelSep.Text = "&Separator:";
			labelSep.Visible = false;
			checkBoxRunApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxRunApplication.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxRunApplication.Location = new System.Drawing.Point(144, 80);
			checkBoxRunApplication.Name = "checkBoxRunApplication";
			checkBoxRunApplication.Size = new System.Drawing.Size(128, 16);
			checkBoxRunApplication.TabIndex = 6;
			checkBoxRunApplication.Text = "&View after saving";
			checkBoxRunApplication.CheckedChanged += new System.EventHandler(checkBoxRunApplication_CheckedChanged);
			checkBoxPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxPrint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxPrint.Location = new System.Drawing.Point(288, 80);
			checkBoxPrint.Name = "checkBoxPrint";
			checkBoxPrint.Size = new System.Drawing.Size(128, 16);
			checkBoxPrint.TabIndex = 7;
			checkBoxPrint.Text = "Prin&t after saving";
			checkBoxPrint.CheckedChanged += new System.EventHandler(checkBoxPrint_CheckedChanged);
			line2.BackColor = System.Drawing.Color.White;
			line2.DrawWidth = 1;
			line2.IsVertical = false;
			line2.LineBackColor = System.Drawing.Color.Black;
			line2.Location = new System.Drawing.Point(8, 112);
			line2.Name = "line2";
			line2.Size = new System.Drawing.Size(408, 1);
			line2.TabIndex = 132;
			line2.TabStop = false;
			checkBoxEmailAfterSaving.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			checkBoxEmailAfterSaving.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxEmailAfterSaving.Location = new System.Drawing.Point(16, 80);
			checkBoxEmailAfterSaving.Name = "checkBoxEmailAfterSaving";
			checkBoxEmailAfterSaving.Size = new System.Drawing.Size(128, 16);
			checkBoxEmailAfterSaving.TabIndex = 5;
			checkBoxEmailAfterSaving.Text = "&Email after saving";
			checkBoxEmailAfterSaving.CheckedChanged += new System.EventHandler(checkBoxEmailAfterSaving_CheckedChanged);
			base.AcceptButton = buttonOk;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(426, 151);
			base.Controls.Add(checkBoxEmailAfterSaving);
			base.Controls.Add(line2);
			base.Controls.Add(checkBoxPrint);
			base.Controls.Add(checkBoxRunApplication);
			base.Controls.Add(textBoxSep);
			base.Controls.Add(labelSep);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOk);
			base.Controls.Add(buttonBrowse);
			base.Controls.Add(textBoxFile);
			base.Controls.Add(label1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FilePickerForm";
			Text = "Select a File";
			base.Load += new System.EventHandler(FilePickerForm_Load);
			ResumeLayout(false);
		}

		private void buttonOk_Click(object sender, EventArgs e)
		{
			if (textBoxFile.Text.Trim().Length == 0)
			{
				ErrorHelper.WarningMessage(SR.GetString("00218") + ".");
				return;
			}
			if (File.Exists(textBoxFile.Text))
			{
				if (ErrorHelper.QuestionMessageYesNo(SR.GetString("00059") + ".", SR.GetString("00060") + "?") != DialogResult.Yes)
				{
					base.DialogResult = DialogResult.None;
					return;
				}
				base.DialogResult = DialogResult.OK;
				Close();
			}
			base.DialogResult = DialogResult.OK;
		}

		private void SetFilter()
		{
			string text = "";
			switch (ReportType)
			{
			default:
				text = SR.GetString("00024") + " (*.*)|*.*";
				break;
			case ExternalReportTypes.Text:
			{
				MMTextBox mMTextBox = textBoxSep;
				bool visible = labelSep.Visible = true;
				mMTextBox.Visible = visible;
				text = SR.GetString("00174") + " (*.txt)|*.txt|" + SR.GetString("00024") + " (*.*)|*.*";
				break;
			}
			case ExternalReportTypes.HTML:
				text = SR.GetString("00172") + " (*.html)|*.html|" + SR.GetString("00173") + " (*.htm)|*.htm";
				break;
			case ExternalReportTypes.PDF:
				text = SR.GetString("00171") + " (*.pdf)|*.pdf";
				break;
			case ExternalReportTypes.XML:
				text = SR.GetString("00170") + " (*.xml)|*.xml";
				break;
			case ExternalReportTypes.MSWord:
				text = SR.GetString("00169") + " (*.doc)|*.doc";
				break;
			}
			openFileDialog.Filter = text;
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			SetFilter();
			openFileDialog.CheckPathExists = false;
			openFileDialog.CheckFileExists = false;
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				textBoxFile.Text = openFileDialog.FileName;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void checkBoxRunApplication_CheckedChanged(object sender, EventArgs e)
		{
			Global.CompanySettings.SaveSetting(base.Name + ":" + checkBoxRunApplication.Name, checkBoxRunApplication.Checked);
		}

		private void FilePickerForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				try
				{
					checkBoxRunApplication.Checked = bool.Parse(Global.CompanySettings.GetSetting(base.Name + ":" + checkBoxRunApplication.Name).ToString());
				}
				catch
				{
				}
				try
				{
					checkBoxPrint.Checked = bool.Parse(Global.CompanySettings.GetSetting(base.Name + ":" + checkBoxPrint.Name).ToString());
				}
				catch
				{
				}
				try
				{
					checkBoxEmailAfterSaving.Checked = bool.Parse(Global.CompanySettings.GetSetting(base.Name + ":" + checkBoxEmailAfterSaving.Name).ToString());
				}
				catch
				{
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void checkBoxPrint_CheckedChanged(object sender, EventArgs e)
		{
			Global.CompanySettings.SaveSetting(base.Name + ":" + checkBoxPrint.Name, checkBoxPrint.Checked);
		}

		private void checkBoxEmailAfterSaving_CheckedChanged(object sender, EventArgs e)
		{
			Global.CompanySettings.SaveSetting(base.Name + ":" + checkBoxEmailAfterSaving.Name, checkBoxEmailAfterSaving.Checked);
		}
	}
}
