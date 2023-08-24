using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class NewCompanyForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog;

		private XPButton buttonOpenFileDialog2;

		private MMTextBox editDestinationFileName;

		private MMLabel label4;

		private MMTextBox textBoxCompanyName;

		private MMLabel label1;

		private MMTextBox editDatabaseName;

		private MMLabel lblDatabaseName;

		private ToolTip toolTip;

		private Line line1;

		private XPButton buttonNext;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8006;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public NewCompanyForm()
		{
			InitializeComponent();
		}

		public NewCompanyForm(Form form)
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
			buttonOpenFileDialog2 = new Micromind.UISupport.XPButton();
			editDestinationFileName = new Micromind.UISupport.MMTextBox();
			label4 = new Micromind.UISupport.MMLabel();
			textBoxCompanyName = new Micromind.UISupport.MMTextBox();
			label1 = new Micromind.UISupport.MMLabel();
			editDatabaseName = new Micromind.UISupport.MMTextBox();
			lblDatabaseName = new Micromind.UISupport.MMLabel();
			toolTip = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			SuspendLayout();
			buttonNext.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNext.BackColor = System.Drawing.Color.DarkGray;
			buttonNext.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNext.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNext.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNext.Location = new System.Drawing.Point(279, 245);
			buttonNext.Name = "buttonNext";
			buttonNext.Size = new System.Drawing.Size(84, 24);
			buttonNext.TabIndex = 6;
			buttonNext.Text = "&OK";
			buttonNext.UseVisualStyleBackColor = false;
			buttonNext.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(365, 245);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(84, 24);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonOpenFileDialog2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog2.BackColor = System.Drawing.Color.DarkGray;
			buttonOpenFileDialog2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog2.Location = new System.Drawing.Point(424, 101);
			buttonOpenFileDialog2.Name = "buttonOpenFileDialog2";
			buttonOpenFileDialog2.Size = new System.Drawing.Size(25, 22);
			buttonOpenFileDialog2.TabIndex = 2;
			buttonOpenFileDialog2.Text = "...";
			buttonOpenFileDialog2.UseVisualStyleBackColor = false;
			buttonOpenFileDialog2.Click += new System.EventHandler(buttonOpenFileDialog2_Click);
			editDestinationFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			editDestinationFileName.IsComboTextBox = false;
			editDestinationFileName.Location = new System.Drawing.Point(107, 102);
			editDestinationFileName.MaxLength = 255;
			editDestinationFileName.Name = "editDestinationFileName";
			editDestinationFileName.ReadOnly = true;
			editDestinationFileName.Size = new System.Drawing.Size(317, 20);
			editDestinationFileName.TabIndex = 1;
			editDestinationFileName.Enter += new System.EventHandler(OnDestinationFileNameEntered);
			label4.AutoSize = true;
			label4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label4.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label4.IsFieldHeader = false;
			label4.IsRequired = false;
			label4.Location = new System.Drawing.Point(11, 104);
			label4.Name = "label4";
			label4.PenWidth = 1f;
			label4.ShowBorder = false;
			label4.Size = new System.Drawing.Size(60, 13);
			label4.TabIndex = 23;
			label4.Text = "File Name :";
			textBoxCompanyName.BackColor = System.Drawing.Color.White;
			textBoxCompanyName.IsComboTextBox = false;
			textBoxCompanyName.Location = new System.Drawing.Point(12, 27);
			textBoxCompanyName.MaxLength = 1000;
			textBoxCompanyName.Name = "textBoxCompanyName";
			textBoxCompanyName.Size = new System.Drawing.Size(412, 20);
			textBoxCompanyName.TabIndex = 0;
			textBoxCompanyName.Enter += new System.EventHandler(OnFileNameEntered);
			textBoxCompanyName.Validating += new System.ComponentModel.CancelEventHandler(editFileName_Validating);
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
			label1.Size = new System.Drawing.Size(133, 13);
			label1.TabIndex = 19;
			label1.Text = "Enter your company name:";
			editDatabaseName.BackColor = System.Drawing.Color.White;
			editDatabaseName.IsComboTextBox = false;
			editDatabaseName.Location = new System.Drawing.Point(107, 80);
			editDatabaseName.MaxLength = 20;
			editDatabaseName.Name = "editDatabaseName";
			editDatabaseName.Size = new System.Drawing.Size(317, 20);
			editDatabaseName.TabIndex = 0;
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
			lblDatabaseName.Location = new System.Drawing.Point(11, 82);
			lblDatabaseName.Name = "lblDatabaseName";
			lblDatabaseName.PenWidth = 1f;
			lblDatabaseName.ShowBorder = false;
			lblDatabaseName.Size = new System.Drawing.Size(90, 13);
			lblDatabaseName.TabIndex = 0;
			lblDatabaseName.Text = "Database Name :";
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-7, 238);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(479, 1);
			line1.TabIndex = 89;
			line1.TabStop = false;
			base.AcceptButton = buttonNext;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(457, 277);
			base.Controls.Add(editDestinationFileName);
			base.Controls.Add(label4);
			base.Controls.Add(textBoxCompanyName);
			base.Controls.Add(editDatabaseName);
			base.Controls.Add(label1);
			base.Controls.Add(lblDatabaseName);
			base.Controls.Add(line1);
			base.Controls.Add(buttonOpenFileDialog2);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonNext);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "NewCompanyForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "New Company";
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			base.Activated += new System.EventHandler(NewCompanyForm_Activated);
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
			if (editDestinationFileName.Text.Trim() == string.Empty)
			{
				ErrorHelper.WarningMessage("Please specify a valid file path.");
				buttonOpenFileDialog2.Focus();
				return false;
			}
			try
			{
				if (!Directory.Exists(Path.GetDirectoryName(editDestinationFileName.Text)))
				{
					ErrorHelper.WarningMessage("The file location you have selected does not exist or access denied.", "Please specify a valid file path.");
					editDestinationFileName.Focus();
					return false;
				}
				if (File.Exists(editDestinationFileName.Text) && ErrorHelper.QuestionMessageYesNo("A file with the same name is already exists. Do you want to overwrite?") == DialogResult.No)
				{
					editDestinationFileName.Focus();
					return false;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				editDestinationFileName.Focus();
				return false;
			}
			if (editDatabaseName.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter a name for the database.");
				editDatabaseName.Focus();
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateEntries())
			{
				if (Global.CurrentUser.ToLower() != "sa")
				{
					ErrorHelper.ErrorMessage("You must be 'sa' to create a new company.");
				}
				else
				{
					CreateDatabase();
				}
			}
		}

		private bool CreateDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				int num = 1 & (Factory.DatabaseSystem.CreateNewCompany(textBoxCompanyName.Text, editDestinationFileName.Text, editDatabaseName.Text) ? 1 : 0);
				if (num != 0)
				{
					Global.CompanySettings.SaveSetting("CurDB", editDatabaseName.Text);
					ErrorHelper.InformationMessage("Company created successfully. You will be connected to this company now.");
					Factory.SetLoginInfo(Application.ProductName, editDatabaseName.Text, Global.CurrentUser, Factory.Encrypt(Global.CurrentPassword));
					Global.SetOpenCompany(editDatabaseName.Text);
					base.DialogResult = DialogResult.OK;
					Close();
				}
				return (byte)num != 0;
			}
			catch (SqlException ex)
			{
				var a = ex;
				if (ex.Number == 15007 || ex.Number == 15247 || ex.Number == 229)
				{
					ErrorHelper.WarningMessage("You do not have permission to create database.\n" + ex.Message);
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
					ErrorHelper.ErrorMessage("Cannot find the database location. The database may not exists or access denied.", "Try restoring to a new database.");
					return false;
				}
				ErrorHelper.ErrorMessage(ex2.Message);
				return false;
			}
			catch (Exception ex3)
			{
				if (ex3.GetType() == typeof(ExecutionFailureException))
				{
					ErrorHelper.ProcessError(ex3.InnerException, "Please make sure that you  have sufficient access right to the path you have specified or select another location.");
					return false;
				}
				if (ex3.GetType() == typeof(FailedOperationException))
				{
					ErrorHelper.ProcessError(ex3.InnerException, "Please make sure that you  have sufficient access right to the path you have specified or select another location.");
					return false;
				}
				ErrorHelper.ProcessError(ex3);
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

		private void buttonOpenFileDialog2_Click(object sender, EventArgs e)
		{
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			if (editDestinationFileName.Text != "")
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(editDestinationFileName.Text);
			}
			else
			{
				openFileDialog.InitialDirectory = Application.StartupPath;
			}
			if (editDestinationFileName.Text != "")
			{
				openFileDialog.FileName = Path.GetFileNameWithoutExtension(editDestinationFileName.Text);
			}
			else
			{
				openFileDialog.FileName = "";
			}
			openFileDialog.Filter = "Database Files (*.mdf)|*.mdf";
			if (openFileDialog.ShowDialog() == DialogResult.OK && (!new FileInfo(openFileDialog.FileName).Exists || ErrorHelper.QuestionMessageYesNo("This file already exists.", "Do you want to replace?") != DialogResult.No))
			{
				editDestinationFileName.Text = openFileDialog.FileName;
			}
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

		private void NewCompanyForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void editDatabaseName_Validating(object sender, CancelEventArgs e)
		{
			editDatabaseName.Text = editDatabaseName.Text.Trim();
			editDatabaseName.Text.Replace(" ", "");
			if (Format.StartsWithDigit(editDatabaseName.Text))
			{
				ErrorHelper.InformationMessage("The database name cannot start with numbers or special characters.");
				e.Cancel = true;
			}
		}

		private void editFileName_Validating(object sender, CancelEventArgs e)
		{
			if (textBoxCompanyName.Text != "" && editDatabaseName.Text == "")
			{
				editDatabaseName.Text = textBoxCompanyName.Text.Replace(" ", "");
				editDatabaseName.Text = Global.MakeIdentifier(editDatabaseName.Text);
			}
			if (textBoxCompanyName.Text != "" && editDestinationFileName.Text == "")
			{
				editDestinationFileName.Text = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + editDatabaseName.Text + ".mdf";
			}
		}
	}
}
