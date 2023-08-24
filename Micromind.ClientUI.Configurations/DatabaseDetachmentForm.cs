using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class DatabaseDetachmentForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private XPButton buttonCancel;

		private OpenFileDialog openFileDialog;

		private System.Windows.Forms.ToolTip toolTip;

		private Line line1;

		private XPButton buttonNext;

		private DatabaseComboBox comboBoxDatabase;

		private MMLabel mmLabel1;

		private UltraLabel editCompanyName;

		private MMLabel label8;

		private MMLabel mmLabel2;

		private IContainer components;

		private ScreenAccessRight screenRight;

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8007;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public DatabaseDetachmentForm()
		{
			InitializeComponent();
		}

		public DatabaseDetachmentForm(Form form)
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
			buttonNext = new Micromind.UISupport.XPButton();
			buttonCancel = new Micromind.UISupport.XPButton();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			toolTip = new System.Windows.Forms.ToolTip(components);
			line1 = new Micromind.UISupport.Line();
			comboBoxDatabase = new Micromind.DataControls.DatabaseComboBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			editCompanyName = new Infragistics.Win.Misc.UltraLabel();
			label8 = new Micromind.UISupport.MMLabel();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).BeginInit();
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
			comboBoxDatabase.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			comboBoxDatabase.DisplayLayout.Appearance = appearance;
			comboBoxDatabase.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			comboBoxDatabase.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			comboBoxDatabase.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			comboBoxDatabase.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			comboBoxDatabase.DisplayLayout.MaxColScrollRegions = 1;
			comboBoxDatabase.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			comboBoxDatabase.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			comboBoxDatabase.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			comboBoxDatabase.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			comboBoxDatabase.DisplayLayout.Override.CellAppearance = appearance8;
			comboBoxDatabase.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			comboBoxDatabase.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			comboBoxDatabase.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			comboBoxDatabase.DisplayLayout.Override.HeaderAppearance = appearance10;
			comboBoxDatabase.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			comboBoxDatabase.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			comboBoxDatabase.DisplayLayout.Override.RowAppearance = appearance11;
			comboBoxDatabase.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			comboBoxDatabase.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			comboBoxDatabase.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			comboBoxDatabase.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			comboBoxDatabase.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			comboBoxDatabase.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Default;
			comboBoxDatabase.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear;
			comboBoxDatabase.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList;
			comboBoxDatabase.Editable = true;
			comboBoxDatabase.HasAllAccount = false;
			comboBoxDatabase.HasCustom = false;
			comboBoxDatabase.Location = new System.Drawing.Point(96, 37);
			comboBoxDatabase.Name = "comboBoxDatabase";
			comboBoxDatabase.ShowInactiveItems = false;
			comboBoxDatabase.ShowQuickAdd = true;
			comboBoxDatabase.Size = new System.Drawing.Size(314, 20);
			comboBoxDatabase.TabIndex = 103;
			comboBoxDatabase.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			comboBoxDatabase.SelectedIndexChanged += new System.EventHandler(comboBoxDatabase_SelectedIndexChanged);
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(5, 61);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(85, 13);
			mmLabel1.TabIndex = 105;
			mmLabel1.Text = "Company Name:";
			appearance13.BorderColor = System.Drawing.Color.FromArgb(127, 157, 185);
			appearance13.TextVAlignAsString = "Middle";
			editCompanyName.Appearance = appearance13;
			editCompanyName.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.Solid;
			editCompanyName.Location = new System.Drawing.Point(96, 59);
			editCompanyName.Name = "editCompanyName";
			editCompanyName.Size = new System.Drawing.Size(314, 20);
			editCompanyName.TabIndex = 104;
			label8.AutoSize = true;
			label8.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label8.IsFieldHeader = false;
			label8.IsRequired = false;
			label8.Location = new System.Drawing.Point(5, 39);
			label8.Name = "label8";
			label8.PenWidth = 1f;
			label8.ShowBorder = false;
			label8.Size = new System.Drawing.Size(87, 13);
			label8.TabIndex = 106;
			label8.Text = "Database Name:";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			mmLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(5, 18);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(199, 13);
			mmLabel2.TabIndex = 106;
			mmLabel2.Text = "Select the database you want to detach:";
			base.AcceptButton = buttonNext;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(421, 224);
			base.Controls.Add(comboBoxDatabase);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(editCompanyName);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(label8);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonNext);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DatabaseDetachmentForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Detach Database";
			base.Load += new System.EventHandler(DatabaseDetachmentForm_Load);
			base.Activated += new System.EventHandler(DatabaseDetachmentForm_Activated);
			base.Closing += new System.ComponentModel.CancelEventHandler(OnClosing);
			((System.ComponentModel.ISupportInitialize)comboBoxDatabase).EndInit();
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
			if (comboBoxDatabase.SelectedID == string.Empty)
			{
				ErrorHelper.WarningMessage("Please select a database.");
				return false;
			}
			return true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (ValidateEntries() && ErrorHelper.QuestionMessageYesNo("Are you sure you want to detach this database?") != DialogResult.No)
			{
				DetachDatabase();
			}
		}

		private bool DetachDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				int num = 1 & (Factory.DatabaseSystem.DetachDatabase(comboBoxDatabase.SelectedID) ? 1 : 0);
				if (num != 0)
				{
					ErrorHelper.InformationMessage("Database detached successfully.");
					Close();
				}
				return (byte)num != 0;
			}
			catch (SqlException ex)
			{
				if (ex.Number == 15007 || ex.Number == 15247 || ex.Number == 229)
				{
					ErrorHelper.WarningMessage("You do not have permission to detach this database.");
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
				return false;
			}
			catch (CompanyException ex2)
			{
				if (ex2.Number == 1016)
				{
					ErrorHelper.ErrorMessage("This database does not exist.");
					return false;
				}
				ErrorHelper.ErrorMessage(ex2.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				comboBoxDatabase.Focus();
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

		private void DatabaseDetachmentForm_Load(object sender, EventArgs e)
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
		}

		private void linkLabelExsistingDB_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		public CompanyDatabase BringUp(Form parent)
		{
			ShowDialog(parent);
			return database;
		}

		private void DatabaseDetachmentForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void editDatabaseName_Validating(object sender, CancelEventArgs e)
		{
		}

		private void comboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
		{
			editCompanyName.Text = comboBoxDatabase.SelectedName;
		}
	}
}
