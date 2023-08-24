using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DatabaseAttachmentForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog;

		private XPButton buttonOpenFileDialog;

		private MMTextBox editFileName;

		private MMLabel label1;

		private MMTextBox editDatabaseName;

		private MMLabel lblDatabaseName;

		private ToolTip toolTip;

		private Line line1;

		private XPButton buttonNext;

		private MMLabel mmLabel1;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8008;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public DatabaseAttachmentForm()
		{
			InitializeComponent();
		}

		public DatabaseAttachmentForm(Form form)
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
			components = new System.ComponentModel.Container();
			buttonNext = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			editFileName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			editDatabaseName = new Micromind.UISupport.MMTextBox();
			lblDatabaseName = new Micromind.UISupport.MMLabel();
			toolTip = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			SuspendLayout();
			buttonNext.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNext.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonNext.BackColor = System.Drawing.Color.DarkGray;
			buttonNext.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNext.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNext.Location = new System.Drawing.Point(240, 195);
			buttonNext.Name = "buttonNext";
			buttonNext.Size = new System.Drawing.Size(84, 24);
			buttonNext.TabIndex = 3;
			buttonNext.Text = "&OK";
			buttonNext.UseVisualStyleBackColor = false;
			buttonNext.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(326, 195);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(84, 24);
			buttonCancel.TabIndex = 4;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(383, 26);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog.TabIndex = 1;
			buttonOpenFileDialog.Text = "...";
			buttonOpenFileDialog.UseVisualStyleBackColor = false;
			buttonOpenFileDialog.Click += new System.EventHandler(buttonOpenFileDialog_Click);
			editFileName.BackColor = System.Drawing.Color.White;
			editFileName.IsComboTextBox = false;
			editFileName.Location = new System.Drawing.Point(12, 27);
			editFileName.MaxLength = 1000;
			editFileName.Name = "editFileName";
			editFileName.ReadOnly = true;
			editFileName.Size = new System.Drawing.Size(371, 20);
			editFileName.TabIndex = 0;
			editFileName.Enter += new System.EventHandler(OnFileNameEntered);
			label1.AutoSize = true;
			label1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label1.IsFieldHeader = false;
			label1.IsRequired = false;
			label1.Location = new System.Drawing.Point(12, 11);
			label1.Name = "label1";
			label1.PenWidth = 1f;
			label1.ShowBorder = false;
			label1.Size = new System.Drawing.Size(169, 13);
			label1.TabIndex = 19;
			label1.Text = "Select the database  file to attach:";
			editDatabaseName.BackColor = System.Drawing.Color.White;
			editDatabaseName.IsComboTextBox = false;
			editDatabaseName.Location = new System.Drawing.Point(107, 102);
			editDatabaseName.MaxLength = 20;
			editDatabaseName.Name = "editDatabaseName";
			editDatabaseName.Size = new System.Drawing.Size(301, 20);
			editDatabaseName.TabIndex = 2;
			editDatabaseName.Leave += new System.EventHandler(OnDatabaseNameLeave);
			editDatabaseName.Enter += new System.EventHandler(OnDatabaseNameEntered);
			editDatabaseName.Validating += new System.ComponentModel.CancelEventHandler(editDatabaseName_Validating);
			lblDatabaseName.AutoSize = true;
			lblDatabaseName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDatabaseName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblDatabaseName.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDatabaseName.IsFieldHeader = false;
			lblDatabaseName.IsRequired = false;
			lblDatabaseName.Location = new System.Drawing.Point(10, 104);
			lblDatabaseName.Name = "lblDatabaseName";
			lblDatabaseName.PenWidth = 1f;
			lblDatabaseName.ShowBorder = false;
			lblDatabaseName.Size = new System.Drawing.Size(90, 13);
			lblDatabaseName.TabIndex = 0;
			lblDatabaseName.Text = "Database Name :";
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-7, 188);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(440, 1);
			line1.TabIndex = 89;
			line1.TabStop = false;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 86);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(153, 13);
			mmLabel1.TabIndex = 0;
			mmLabel1.Text = "Enter a name for the database:";
			base.AcceptButton = buttonNext;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(421, 224);
			base.Controls.Add(editFileName);
			base.Controls.Add(editDatabaseName);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(lblDatabaseName);
			base.Controls.Add(label1);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOpenFileDialog);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonNext);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DatabaseAttachmentForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Attach Database";
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			base.Activated += new System.EventHandler(DatabaseAttachmentForm_Activated);
			base.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
			ResumeLayout(false);
			PerformLayout();
		}

		public void Init()
		{
		}

		private string GetEncyptedPassword()
		{
			return "";
		}

		private bool ValidateEntries()
		{
			if (editFileName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please specify a valid file path.");
				buttonOpenFileDialog.Focus();
				return false;
			}
			try
			{
				if (!File.Exists(editFileName.Text))
				{
					ErrorHelper.WarningMessage("The file name specified does not exist.", "Please specify a valid file path.");
					editFileName.SelectAll();
					editFileName.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				editFileName.SelectAll();
				editFileName.Focus();
				return false;
			}
			if (editDatabaseName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please specify a name for the database.");
				editDatabaseName.Focus();
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateEntries())
			{
				AttachDatabase();
			}
		}

		private bool AttachDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				int num = 1 & (Factory.DatabaseSystem.AttachDatabase(editFileName.Text, editDatabaseName.Text) ? 1 : 0);
				if (num != 0)
				{
					ErrorHelper.InformationMessage("Database attached successfully.");
					Close();
				}
				return (byte)num != 0;
			}
			catch (SqlException ex)
			{
				if (ex.Number == 15007 || ex.Number == 15247 || ex.Number == 229)
				{
					ErrorHelper.WarningMessage(SR.GetString("00063"), SR.GetString("00064"));
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (CompanyException ex2)
			{
				if (ex2.Number == 1014)
				{
					ErrorHelper.ErrorMessage("This database name is already exist. Please enter another database name.");
					return false;
				}
				if (ex2.Number == 1015)
				{
					ErrorHelper.ErrorMessage("Cannot find the database location. The database may not exists or access denied.\nTry restoring to a new database.");
					return false;
				}
				ErrorHelper.ErrorMessage(ex2.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				editDatabaseName.Focus();
				editDatabaseName.SelectAll();
				return false;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonOpenFileDialog_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Database Files (*.mdf)|*.mdf";
			openFileDialog.DefaultExt = "mdf";
			openFileDialog.FileName = "";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				if (!new FileInfo(openFileDialog.FileName).Exists)
				{
					ErrorHelper.WarningMessage("The database file you have entered does not exist.");
				}
				else
				{
					editFileName.Text = openFileDialog.FileName;
				}
			}
		}

		private void tabDatabase_Click(object sender, EventArgs e)
		{
		}

		private void DatabaseAttachmentForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				if (!base.IsDisposed)
				{
					Init();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
				Dispose();
			}
		}

		public void LoadForm()
		{
		}

		private void AddToolTips()
		{
		}

		private void OnFileNameEntered(object sender, EventArgs e)
		{
		}

		private void OnDestinationFileNameEntered(object sender, EventArgs e)
		{
		}

		private void OnDatabaseNameEntered(object sender, EventArgs e)
		{
		}

		private void OnLoginNameEntered(object sender, EventArgs e)
		{
		}

		private void OnPasswordEntered(object sender, EventArgs e)
		{
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
		}

		private void OnServerEntered(object sender, EventArgs e)
		{
		}

		private void OnDatabaseNameLeave(object sender, EventArgs e)
		{
			editDatabaseName.Text = Global.MakeIdentifier(editDatabaseName.Text);
		}

		private void linkLabelExsistingDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		public CompanyDatabase BringUp(Form parent)
		{
			ShowDialog(parent);
			return database;
		}

		private void DatabaseAttachmentForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void editDatabaseName_Validating(object sender, CancelEventArgs e)
		{
		}
	}
}
