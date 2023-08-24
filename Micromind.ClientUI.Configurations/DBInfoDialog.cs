using Micromind.ClientLibraries;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DBInfoDialog : DialogBoxBaseForm
	{
		private XPButton buttonCancel;

		private XPButton buttonOK;

		private Line line1;

		private LinkLabel linkLabelExsistingDB;

		private MMLabel label15;

		private MMTextBox editDBPath;

		private XPButton buttonOpenFileDialog;

		private MMLabel label13;

		private MMTextBox textBoxDatabaseName;

		private OpenFileDialog openFileDialog;

		private Container components;

		public string DatabaseName
		{
			get
			{
				return textBoxDatabaseName.Text;
			}
			set
			{
				textBoxDatabaseName.Text = value;
			}
		}

		public string DatabasePath
		{
			get
			{
				return editDBPath.Text;
			}
			set
			{
				editDBPath.Text = value;
			}
		}

		public DBInfoDialog()
		{
			InitializeComponent();
			Init();
		}

		private void Init()
		{
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
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonOK = new Micromind.UISupport.XPButton();
			line1 = new Micromind.UISupport.Line();
			linkLabelExsistingDB = new System.Windows.Forms.LinkLabel();
			label15 = new Micromind.UISupport.MMLabel();
			editDBPath = new Micromind.UISupport.MMTextBox();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			label13 = new Micromind.UISupport.MMLabel();
			textBoxDatabaseName = new Micromind.UISupport.MMTextBox();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			SuspendLayout();
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(331, 111);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(65, 23);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.Location = new System.Drawing.Point(259, 111);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(65, 23);
			buttonOK.TabIndex = 6;
			buttonOK.Text = "&OK";
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(8, 104);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(392, 1);
			line1.TabIndex = 16;
			line1.TabStop = false;
			linkLabelExsistingDB.ActiveLinkColor = System.Drawing.Color.RoyalBlue;
			linkLabelExsistingDB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			linkLabelExsistingDB.LinkArea = new System.Windows.Forms.LinkArea(0, 26);
			linkLabelExsistingDB.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			linkLabelExsistingDB.Location = new System.Drawing.Point(184, 7);
			linkLabelExsistingDB.Name = "linkLabelExsistingDB";
			linkLabelExsistingDB.Size = new System.Drawing.Size(184, 15);
			linkLabelExsistingDB.TabIndex = 2;
			linkLabelExsistingDB.TabStop = true;
			linkLabelExsistingDB.Text = "View existing databases...";
			linkLabelExsistingDB.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabelExsistingDB_LinkClicked);
			label15.AutoSize = true;
			label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label15.Location = new System.Drawing.Point(8, 56);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(273, 16);
			label15.TabIndex = 3;
			label15.Text = "Enter a &file name and its location to save your data in:";
			editDBPath.AcceptsReturn = false;
			editDBPath.AcceptsTab = false;
			editDBPath.AutoSize = true;
			editDBPath.BackColor = System.Drawing.Color.White;
			editDBPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editDBPath.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			editDBPath.HideSelection = true;
			editDBPath.IsComboTextBox = false;
			editDBPath.Lines = new string[0];
			editDBPath.Location = new System.Drawing.Point(8, 77);
			editDBPath.MaxLength = 1024;
			editDBPath.Multiline = false;
			editDBPath.Name = "editDBPath";
			editDBPath.PasswordChar = '\0';
			editDBPath.ReadOnly = true;
			editDBPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
			editDBPath.Size = new System.Drawing.Size(320, 20);
			editDBPath.TabIndex = 4;
			editDBPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			editDBPath.WordWrap = true;
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(331, 77);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(65, 20);
			buttonOpenFileDialog.TabIndex = 5;
			buttonOpenFileDialog.Text = "&Browse...";
			buttonOpenFileDialog.Click += new System.EventHandler(buttonOpenFileDialog_Click);
			label13.AutoSize = true;
			label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label13.Location = new System.Drawing.Point(8, 7);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(141, 16);
			label13.TabIndex = 0;
			label13.Text = "Enter a name for &database:";
			textBoxDatabaseName.AcceptsReturn = false;
			textBoxDatabaseName.AcceptsTab = false;
			textBoxDatabaseName.AutoSize = true;
			textBoxDatabaseName.BackColor = System.Drawing.Color.White;
			textBoxDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxDatabaseName.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
			textBoxDatabaseName.HideSelection = true;
			textBoxDatabaseName.IsComboTextBox = false;
			textBoxDatabaseName.Lines = new string[0];
			textBoxDatabaseName.Location = new System.Drawing.Point(8, 30);
			textBoxDatabaseName.MaxLength = 30;
			textBoxDatabaseName.Multiline = false;
			textBoxDatabaseName.Name = "textBoxDatabaseName";
			textBoxDatabaseName.PasswordChar = '\0';
			textBoxDatabaseName.ReadOnly = false;
			textBoxDatabaseName.ScrollBars = System.Windows.Forms.ScrollBars.None;
			textBoxDatabaseName.Size = new System.Drawing.Size(320, 20);
			textBoxDatabaseName.TabIndex = 1;
			textBoxDatabaseName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
			textBoxDatabaseName.WordWrap = true;
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			BackColor = System.Drawing.Color.FromArgb(233, 229, 217);
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(402, 140);
			base.Controls.Add(linkLabelExsistingDB);
			base.Controls.Add(label15);
			base.Controls.Add(editDBPath);
			base.Controls.Add(buttonOpenFileDialog);
			base.Controls.Add(label13);
			base.Controls.Add(textBoxDatabaseName);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DBInfoDialog";
			Text = "Database Settings";
			base.Closing += new System.ComponentModel.CancelEventHandler(ChangeUserForm_Closing);
			base.Load += new System.EventHandler(ChangeUserForm_Load);
			ResumeLayout(false);
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (textBoxDatabaseName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please type database name.");
				textBoxDatabaseName.Focus();
			}
			else if (editDBPath.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please type a correct file name.");
				editDBPath.Focus();
			}
			else
			{
				base.DialogResult = DialogResult.OK;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		private void ChangeUserForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ChangeUserForm_Closing(object sender, CancelEventArgs e)
		{
		}

		private void linkLabelExsistingDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			BrowseCompany();
		}

		private void BrowseCompany()
		{
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "MDF files (*.mdf)|*.mdf";
			openFileDialog.DefaultExt = "mdf";
			openFileDialog.InitialDirectory = Application.StartupPath + "\\data";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage(SR.GetString("00059"), SR.GetString("00105"));
				}
				else
				{
					editDBPath.Text = openFileDialog.FileName;
				}
			}
		}
	}
}
