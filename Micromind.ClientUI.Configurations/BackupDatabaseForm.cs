using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.DataControls;
using Micromind.UISupport;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class BackupDatabaseForm : Form
	{
		private bool hasSchedule;

		private BackupScheduleForm scheduleForm;

		private XPButton buttonOK;

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog;

		private MMLabel label3;

		private MMTextBox editFileName;

		private System.Windows.Forms.ToolTip toolTip;

		private XPButton buttonBrowse;

		private MMLabel labelWait;

		private Line line1;

		private XPButton buttonSchedule;

		private UltraLabel editCompanyName;

		private DatabaseComboBox comboBoxDatabase;

		private MMLabel label8;

		private MMLabel mmLabel1;

		private MMLabel mmLabel2;

		private IContainer components;

		private bool isFirstActivated = true;

		private ScreenAccessRight screenRight;

		public BackupDatabaseForm()
		{
			InitializeComponent();
			base.Activated += BackupDatabaseForm_Activated;
		}

		private void BackupDatabaseForm_Activated(object sender, EventArgs e)
		{
			if (isFirstActivated)
			{
				isFirstActivated = false;
				comboBoxDatabase.LoadData();
				comboBoxDatabase.SelectedID = Global.CurrentDatabaseName;
			}
		}

		public BackupDatabaseForm(Form form)
		{
			InitializeComponent();
			Init();
			if (form != null)
			{
				form.Text = form.Text;
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
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			buttonOK = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			buttonBrowse = new Micromind.UISupport.XPButton();
			editFileName = new Micromind.UISupport.MMTextBox();
			label3 = new Micromind.UISupport.MMLabel();
			toolTip = new System.Windows.Forms.ToolTip(components);
			labelWait = new Micromind.UISupport.MMLabel();
			line1 = new Micromind.UISupport.Line();
			buttonSchedule = new Micromind.UISupport.XPButton();
			editCompanyName = new Infragistics.Win.Misc.UltraLabel();
			comboBoxDatabase = new Micromind.DataControls.DatabaseComboBox();
			label8 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).BeginInit();
			SuspendLayout();
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(277, 214);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(78, 22);
			buttonOK.TabIndex = 4;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(360, 214);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(78, 22);
			buttonCancel.TabIndex = 5;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			buttonBrowse.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonBrowse.BackColor = System.Drawing.Color.DarkGray;
			buttonBrowse.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonBrowse.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonBrowse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonBrowse.Location = new System.Drawing.Point(415, 133);
			buttonBrowse.Name = "buttonBrowse";
			buttonBrowse.Size = new System.Drawing.Size(24, 22);
			buttonBrowse.TabIndex = 3;
			buttonBrowse.Text = "...";
			buttonBrowse.UseVisualStyleBackColor = false;
			buttonBrowse.Click += new System.EventHandler(buttonBrowse_Click);
			editFileName.BackColor = System.Drawing.Color.WhiteSmoke;
			editFileName.IsComboTextBox = false;
			editFileName.Location = new System.Drawing.Point(11, 134);
			editFileName.MaxLength = 255;
			editFileName.Name = "editFileName";
			editFileName.ReadOnly = true;
			editFileName.Size = new System.Drawing.Size(404, 20);
			editFileName.TabIndex = 2;
			editFileName.Enter += new System.EventHandler(OnFileNameEntered);
			editFileName.Leave += new System.EventHandler(OnFileNameLeave);
			label3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			label3.ForeColor = System.Drawing.Color.Black;
			label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label3.IsFieldHeader = false;
			label3.IsRequired = false;
			label3.Location = new System.Drawing.Point(8, 113);
			label3.Name = "label3";
			label3.PenWidth = 1f;
			label3.ShowBorder = false;
			label3.Size = new System.Drawing.Size(273, 15);
			label3.TabIndex = 90;
			label3.Text = "Specify the location to copy the backup file:";
			label3.Click += new System.EventHandler(label3_Click);
			labelWait.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelWait.ForeColor = System.Drawing.Color.Black;
			labelWait.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			labelWait.IsFieldHeader = false;
			labelWait.IsRequired = false;
			labelWait.Location = new System.Drawing.Point(9, 215);
			labelWait.Name = "labelWait";
			labelWait.PenWidth = 1f;
			labelWait.ShowBorder = false;
			labelWait.Size = new System.Drawing.Size(104, 16);
			labelWait.TabIndex = 92;
			labelWait.Text = "Please Wait...";
			labelWait.Visible = false;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-7, 207);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(528, 1);
			line1.TabIndex = 93;
			line1.TabStop = false;
			buttonSchedule.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSchedule.BackColor = System.Drawing.Color.DarkGray;
			buttonSchedule.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSchedule.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSchedule.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSchedule.Location = new System.Drawing.Point(12, 214);
			buttonSchedule.Name = "buttonSchedule";
			buttonSchedule.Size = new System.Drawing.Size(85, 22);
			buttonSchedule.TabIndex = 6;
			buttonSchedule.Text = "&Schedule...";
			buttonSchedule.UseVisualStyleBackColor = false;
			buttonSchedule.Visible = false;
			buttonSchedule.Click += new System.EventHandler(buttonSchedule_Click);
			appearance.BorderColor = System.Drawing.Color.FromArgb(127, 157, 185);
			appearance.TextVAlignAsString = "Middle";
			editCompanyName.Appearance = appearance;
			editCompanyName.BorderStyleOuter = Infragistics.Win.UIElementBorderStyle.Solid;
			editCompanyName.Location = new System.Drawing.Point(101, 52);
			editCompanyName.Name = "editCompanyName";
			editCompanyName.Size = new System.Drawing.Size(314, 20);
			editCompanyName.TabIndex = 1;
			comboBoxDatabase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			comboBoxDatabase.CheckedListSettings.CheckStateMember = "";
			comboBoxDatabase.DescriptionTextBox = null;
			appearance2.BackColor = System.Drawing.SystemColors.Window;
			appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDatabase.DisplayLayout.Appearance = appearance2;
			comboBoxDatabase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDatabase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance3.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.GroupByBox.Appearance = appearance3;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance4;
			comboBoxDatabase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance5.BackColor2 = System.Drawing.SystemColors.Control;
			appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance5.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.PromptAppearance = appearance5;
			comboBoxDatabase.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDatabase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance6.BackColor = System.Drawing.SystemColors.Window;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDatabase.DisplayLayout.Override.ActiveCellAppearance = appearance6;
			appearance7.BackColor = System.Drawing.SystemColors.Highlight;
			appearance7.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDatabase.DisplayLayout.Override.ActiveRowAppearance = appearance7;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance8.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.CardAreaAppearance = appearance8;
			appearance9.BorderColor = System.Drawing.Color.Silver;
			appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDatabase.DisplayLayout.Override.CellAppearance = appearance9;
			comboBoxDatabase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDatabase.DisplayLayout.Override.CellPadding = 0;
			appearance10.BackColor = System.Drawing.SystemColors.Control;
			appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance10.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.GroupByRowAppearance = appearance10;
			appearance11.TextHAlignAsString = "Left";
			comboBoxDatabase.DisplayLayout.Override.HeaderAppearance = appearance11;
			comboBoxDatabase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDatabase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance12.BackColor = System.Drawing.SystemColors.Window;
			appearance12.BorderColor = System.Drawing.Color.Silver;
			comboBoxDatabase.DisplayLayout.Override.RowAppearance = appearance12;
			comboBoxDatabase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance13.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDatabase.DisplayLayout.Override.TemplateAddRowAppearance = appearance13;
			comboBoxDatabase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDatabase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDatabase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDatabase.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDatabase.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxDatabase.Editable = true;
			comboBoxDatabase.HasAllAccount = false;
			comboBoxDatabase.HasCustom = false;
			comboBoxDatabase.Location = new System.Drawing.Point(101, 30);
			comboBoxDatabase.Name = "comboBoxDatabase";
			comboBoxDatabase.PreferredDropDownSize = new System.Drawing.Size(0, 0);
			comboBoxDatabase.ShowInactiveItems = false;
			comboBoxDatabase.ShowQuickAdd = true;
			comboBoxDatabase.Size = new System.Drawing.Size(314, 20);
			comboBoxDatabase.TabIndex = 0;
			comboBoxDatabase.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			label8.AutoSize = true;
			label8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label8.IsFieldHeader = false;
			label8.IsRequired = false;
			label8.Location = new System.Drawing.Point(10, 32);
			label8.Name = "label8";
			label8.PenWidth = 1f;
			label8.ShowBorder = false;
			label8.Size = new System.Drawing.Size(87, 13);
			label8.TabIndex = 0;
			label8.Text = "&Database Name:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(10, 54);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 0;
			mmLabel1.Text = "Company Name:";
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.ForeColor = System.Drawing.Color.Black;
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(8, 9);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(273, 15);
			mmLabel2.TabIndex = 90;
			mmLabel2.Text = "Select the database to backup:";
			mmLabel2.Click += new System.EventHandler(label3_Click);
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(447, 242);
			base.Controls.Add(editCompanyName);
			base.Controls.Add(buttonSchedule);
			base.Controls.Add(labelWait);
			base.Controls.Add(comboBoxDatabase);
			base.Controls.Add(buttonOK);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(line1);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(label3);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(label8);
			base.Controls.Add(buttonBrowse);
			base.Controls.Add(editFileName);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "BackupDatabaseForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Backup Database";
			base.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		private void Init()
		{
			editFileName.Text = GetIniBackupFile();
			base.HelpRequested += PublicFunctions.OnHelpButtonClick;
			comboBoxDatabase.SelectedIndexChanged += comboBoxDatabase_SelectedIndexChanged;
		}

		private void comboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			editCompanyName.Text = comboBoxDatabase.SelectedName;
		}

		private string GetEncyptedPassword()
		{
			return "";
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Cursor cursor = null;
			if (comboBoxDatabase.Text.Length <= 0)
			{
				ErrorHelper.WarningMessage("Please select a database to backup.");
				comboBoxDatabase.Focus();
				return;
			}
			if (editFileName.Text.Length <= 0)
			{
				ErrorHelper.WarningMessage("Please enter the location and file name where the backup file will be copied to.");
				editFileName.Focus();
				return;
			}
			if (!Directory.Exists(Path.GetDirectoryName(editFileName.Text)))
			{
				ErrorHelper.WarningMessage("The file path you have entered does not exist.");
				editFileName.Focus();
				return;
			}
			FileInfo fileInfo = new FileInfo(editFileName.Text);
			if (fileInfo.Exists && ErrorHelper.QuestionMessageYesNo("This file already exists", "Do you want to replace it?") == DialogResult.No)
			{
				editFileName.Focus();
				editFileName.SelectAll();
				return;
			}
			if (fileInfo.Name.Length == 0)
			{
				ErrorHelper.WarningMessage("Invalid file name. Please enter a valid file name.");
				editFileName.Focus();
				editFileName.SelectAll();
				editFileName.Text = "";
			}
			try
			{
				base.UseWaitCursor = true;
				Application.DoEvents();
				string currentInstanceName = Global.CurrentInstanceName;
				if (!GlobalRules.IsCorrectServerName(currentInstanceName.Trim()))
				{
					ErrorHelper.WarningMessage("Cannot connect to database server.");
				}
				else
				{
					bool num = Factory.DatabaseSystem.BackupDatabaseToDisk(editFileName.Text, "Backup", comboBoxDatabase.Text, currentInstanceName, "", "");
					Cursor = cursor;
					if (!num)
					{
						ErrorHelper.WarningMessage("Unable to backup the database. Please try again!");
					}
					else
					{
						ErrorHelper.InformationMessage("Backup finished successfully.");
						if (hasSchedule)
						{
							Factory.DatabaseSystem.ScheduleDatabaseBackup(comboBoxDatabase.Text + "_Axolon_Backup", scheduleForm.ScheduleName, Global.CurrentInstanceName, comboBoxDatabase.Text, "", GetEncyptedPassword(), scheduleForm.FrequencyType, scheduleForm.FrequencyInterval, scheduleForm.FrequencyRecurrenceFactor, scheduleForm.StartDate, scheduleForm.StartTime, scheduleForm.EndDate, scheduleForm.HasEndDate, editFileName.Text);
						}
						Close();
					}
				}
			}
			catch (SqlException ex)
			{
				if (ex.Number == 15007 || ex.Number == 15247 || ex.Number == 229)
				{
					ErrorHelper.WarningMessage("Unable to backup the database. You do not have sufficient access right.");
				}
				else if (ex.Number == 911)
				{
					ErrorHelper.ProcessError(ex);
					comboBoxDatabase.Focus();
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
			}
			catch (Exception ex2)
			{
				if (ex2.GetType() == typeof(FailedOperationException))
				{
					ErrorHelper.ErrorMessage("Backup faild because of an operation system error. You may not have access to the selected path or invalid path.", "Error Message:" + ex2.Message);
				}
				else
				{
					ErrorHelper.ErrorMessage("Unable to backup the database. Please try again!");
				}
			}
			finally
			{
				base.UseWaitCursor = false;
				labelWait.Visible = false;
				Application.DoEvents();
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

		private void OnFileNameLeave(object o, EventArgs e)
		{
			_ = editFileName.Text.Trim().Length;
			_ = 0;
		}

		private void AddToolTips()
		{
		}

		private void OnServerEntered(object sender, EventArgs e)
		{
		}

		private void OnDatabaseNameEntered(object sender, EventArgs e)
		{
		}

		private void OnFileNameEntered(object sender, EventArgs e)
		{
		}

		private void OnLoginNameEntered(object sender, EventArgs e)
		{
		}

		private void OnLoginPasswordEntered(object sender, EventArgs e)
		{
		}

		private void OnClosing(object sender, CancelEventArgs e)
		{
		}

		private void SaveInitBackupFile(string dir)
		{
			Global.CompanySettings.SaveSetting("InitBackupFile", dir);
		}

		private string GetIniBackupFile()
		{
			object obj = Global.CompanySettings.GetSetting("InitBackupFile");
			try
			{
				PublicFunctions.StartWaiting(this);
				if (obj == null || obj.ToString() == string.Empty || !Directory.Exists(Path.GetDirectoryName(obj.ToString())))
				{
					obj = Factory.DatabaseSystem.GetDefaultBackupDir() + Path.DirectorySeparatorChar.ToString() + "backup " + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + ".bak";
				}
			}
			catch
			{
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
			if (obj == null || obj.ToString().Trim() == string.Empty)
			{
				obj = "";
			}
			else if (Path.GetFileName(obj.ToString()) == string.Empty)
			{
				obj = "";
			}
			if (obj == null || obj.ToString().Trim() == string.Empty)
			{
				obj = Application.StartupPath + Path.DirectorySeparatorChar.ToString() + Global.CurrentDatabaseName;
			}
			return obj.ToString();
		}

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				openFileDialog.InitialDirectory = Path.GetDirectoryName(GetIniBackupFile().ToString());
			}
			catch
			{
			}
			openFileDialog.CheckPathExists = true;
			openFileDialog.CheckFileExists = false;
			openFileDialog.Filter = "Backup Files (*.bak)|*.bak";
			openFileDialog.DefaultExt = "bak";
			if (openFileDialog.InitialDirectory.EndsWith("\\"))
			{
				openFileDialog.FileName = openFileDialog.InitialDirectory + comboBoxDatabase.Text + ".bak";
			}
			else
			{
				openFileDialog.FileName = openFileDialog.InitialDirectory + Path.DirectorySeparatorChar.ToString() + comboBoxDatabase.Text + ".bak";
			}
			openFileDialog.Filter = "Backup Files (*.bak)|*.bak";
			if (openFileDialog.ShowDialog() == DialogResult.OK && (!new FileInfo(openFileDialog.FileName).Exists || ErrorHelper.QuestionMessageYesNo("This file already exists.", "Do you want to replace the file?") != DialogResult.No))
			{
				editFileName.Text = openFileDialog.FileName;
				SaveInitBackupFile(openFileDialog.FileName);
			}
		}

		private void OnDatabaseNameLeave(object sender, EventArgs e)
		{
			comboBoxDatabase.Text = Global.MakeIdentifier(comboBoxDatabase.Text);
		}

		private void label3_Click(object sender, EventArgs e)
		{
		}

		private void buttonBrowseDatabase_Click(object sender, EventArgs e)
		{
			BrowseCompany();
		}

		private void BrowseCompany()
		{
			checked
			{
				try
				{
					Application.DoEvents();
					if (!GlobalRules.IsCorrectServerName(Global.CurrentServerName.Trim()))
					{
						ErrorHelper.WarningMessage("Incorrect server name.");
					}
					else
					{
						DataSet databases = Factory.GetDatabases(Global.CurrentInstanceName);
						databases.Tables[0].Columns["Database_name"].ColumnName = "Database Name";
						databases.Tables[0].Columns["CompanyName"].ColumnName = "Company Name";
						for (int i = 0; i < databases.Tables[0].Columns.Count; i++)
						{
							if (databases.Tables[0].Columns[i].ColumnName != "Database Name" && databases.Tables[0].Columns[i].ColumnName != "Company Name")
							{
								databases.Tables[0].Columns.RemoveAt(i);
								i--;
							}
						}
						for (int j = 0; j < databases.Tables[0].Rows.Count; j++)
						{
							if (databases.Tables[0].Rows[j]["Company Name"].ToString() == "")
							{
								databases.Tables[0].Rows.RemoveAt(j);
								j--;
							}
						}
						SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
						selectDocumentDialog.DataSource = databases;
						selectDocumentDialog.Name = "Select Database";
						if (selectDocumentDialog.ShowDialog() == DialogResult.OK)
						{
							comboBoxDatabase.Text = selectDocumentDialog.GetSelectedCode("Database Name");
						}
					}
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		private void label5_Click(object sender, EventArgs e)
		{
		}

		private void buttonSchedule_Click(object sender, EventArgs e)
		{
			if (scheduleForm == null)
			{
				scheduleForm = new BackupScheduleForm();
			}
			if (scheduleForm.ShowDialog() == DialogResult.OK)
			{
				hasSchedule = true;
			}
		}
	}
}
