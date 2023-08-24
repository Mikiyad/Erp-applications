using DevExpress.XtraWizard;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.UISupport;
using Microsoft.SqlServer.Dac;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class UpgradeDBDataForm : Form, IForm
	{
		private CompanyDatabase database = new CompanyDatabase();

		private OpenFileDialog openFileDialog;

		private System.Windows.Forms.ToolTip toolTip;

		private WizardControl wizardControl1;

		private WelcomeWizardPage wizardWelcomePage;

		private CompletionWizardPage wizardCompletionPage;

		private WizardPage wizardSelectDatabasePage;

		private MMLabel lblRestoreMessage;

		private ImageList imageList;

		private PictureBox pictureBox1;

		private DataGridList dataGridListTest;

		private IContainer components;

		private string databaseName = "";

		private ScreenAccessRight screenRight;

		private bool allDone;

		public string DatabaseName
		{
			get
			{
				return databaseName;
			}
			set
			{
				databaseName = value;
			}
		}

		public ScreenAreas ScreenArea => ScreenAreas.Company;

		public int ScreenID => 8005;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		public CompanyDatabase Database => database;

		public UpgradeDBDataForm()
		{
			InitializeComponent();
			openFileDialog.FileOk += openFileDialog_FileOk;
		}

		private void openFileDialog_FileOk(object sender, CancelEventArgs e)
		{
		}

		public UpgradeDBDataForm(Form form)
		{
			InitializeComponent();
		}

		private void comboBoxDatabase_SelectedIndexChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.UpgradeDBDataForm));
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			toolTip = new System.Windows.Forms.ToolTip(components);
			wizardControl1 = new DevExpress.XtraWizard.WizardControl();
			wizardWelcomePage = new DevExpress.XtraWizard.WelcomeWizardPage();
			wizardCompletionPage = new DevExpress.XtraWizard.CompletionWizardPage();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			lblRestoreMessage = new Micromind.UISupport.MMLabel();
			wizardSelectDatabasePage = new DevExpress.XtraWizard.WizardPage();
			dataGridListTest = new Micromind.UISupport.DataGridList(components);
			imageList = new System.Windows.Forms.ImageList(components);
			((System.ComponentModel.ISupportInitialize)wizardControl1).BeginInit();
			wizardControl1.SuspendLayout();
			wizardCompletionPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			wizardSelectDatabasePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridListTest).BeginInit();
			SuspendLayout();
			wizardControl1.Appearance.Page.BackColor = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.BackColor2 = System.Drawing.Color.Transparent;
			wizardControl1.Appearance.Page.Options.UseBackColor = true;
			wizardControl1.Controls.Add(wizardWelcomePage);
			wizardControl1.Controls.Add(wizardCompletionPage);
			wizardControl1.Controls.Add(wizardSelectDatabasePage);
			wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			wizardControl1.ImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			wizardControl1.Location = new System.Drawing.Point(0, 0);
			wizardControl1.Name = "wizardControl1";
			wizardControl1.Pages.AddRange(new DevExpress.XtraWizard.BaseWizardPage[3]
			{
				wizardWelcomePage,
				wizardSelectDatabasePage,
				wizardCompletionPage
			});
			wizardControl1.Size = new System.Drawing.Size(597, 384);
			wizardControl1.UseCancelButton = false;
			wizardControl1.CancelClick += new System.ComponentModel.CancelEventHandler(wizardControl1_CancelClick);
			wizardControl1.FinishClick += new System.ComponentModel.CancelEventHandler(wizardControl1_FinishClick);
			wizardControl1.NextClick += new DevExpress.XtraWizard.WizardCommandButtonClickEventHandler(wizardControl1_NextClick);
			wizardControl1.CustomizeCommandButtons += new DevExpress.XtraWizard.WizardCustomizeCommandButtonsEventHandler(wizardControl1_CustomizeCommandButtons);
			wizardWelcomePage.IntroductionText = "This wizard helps you to restore your database";
			wizardWelcomePage.Name = "wizardWelcomePage";
			wizardWelcomePage.Size = new System.Drawing.Size(380, 251);
			wizardWelcomePage.Visible = false;
			wizardCompletionPage.Controls.Add(pictureBox1);
			wizardCompletionPage.Controls.Add(lblRestoreMessage);
			wizardCompletionPage.FinishText = "";
			wizardCompletionPage.Name = "wizardCompletionPage";
			wizardCompletionPage.Size = new System.Drawing.Size(380, 251);
			pictureBox1.Image = Micromind.ClientUI.Properties.Resources.completed;
			pictureBox1.Location = new System.Drawing.Point(25, 63);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(20, 19);
			pictureBox1.TabIndex = 27;
			pictureBox1.TabStop = false;
			lblRestoreMessage.AutoSize = true;
			lblRestoreMessage.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblRestoreMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			lblRestoreMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f);
			lblRestoreMessage.ImageAlign = System.Drawing.ContentAlignment.TopRight;
			lblRestoreMessage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblRestoreMessage.IsFieldHeader = false;
			lblRestoreMessage.IsRequired = false;
			lblRestoreMessage.Location = new System.Drawing.Point(46, 63);
			lblRestoreMessage.Name = "lblRestoreMessage";
			lblRestoreMessage.PenWidth = 1f;
			lblRestoreMessage.ShowBorder = false;
			lblRestoreMessage.Size = new System.Drawing.Size(239, 17);
			lblRestoreMessage.TabIndex = 26;
			lblRestoreMessage.Text = "All data patches applied successfully";
			lblRestoreMessage.Visible = false;
			wizardSelectDatabasePage.Controls.Add(dataGridListTest);
			wizardSelectDatabasePage.DescriptionText = "The following patches should be applied on the database";
			wizardSelectDatabasePage.Name = "wizardSelectDatabasePage";
			wizardSelectDatabasePage.Size = new System.Drawing.Size(565, 239);
			wizardSelectDatabasePage.Text = "Pending Data Patches";
			dataGridListTest.AllowUnfittedView = false;
			dataGridListTest.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridListTest.DisplayLayout.Appearance = appearance;
			dataGridListTest.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridListTest.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListTest.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridListTest.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridListTest.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridListTest.DisplayLayout.MaxColScrollRegions = 1;
			dataGridListTest.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridListTest.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridListTest.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridListTest.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridListTest.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridListTest.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridListTest.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridListTest.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridListTest.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridListTest.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridListTest.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridListTest.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridListTest.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridListTest.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridListTest.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridListTest.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridListTest.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridListTest.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridListTest.LoadLayoutFailed = false;
			dataGridListTest.Location = new System.Drawing.Point(3, 0);
			dataGridListTest.Name = "dataGridListTest";
			dataGridListTest.ShowDeleteMenu = false;
			dataGridListTest.ShowMinusInRed = true;
			dataGridListTest.ShowNewMenu = false;
			dataGridListTest.Size = new System.Drawing.Size(559, 224);
			dataGridListTest.TabIndex = 1;
			dataGridListTest.Text = "dataGridList1";
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "wait");
			imageList.Images.SetKeyName(1, "tick");
			imageList.Images.SetKeyName(2, "failed");
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(597, 384);
			base.Controls.Add(wizardControl1);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "UpgradeDBDataForm";
			Text = "Update Database Data";
			base.Activated += new System.EventHandler(UpgradeDBDataForm_Activated);
			base.Load += new System.EventHandler(DatabaseAttachmentForm_Load);
			((System.ComponentModel.ISupportInitialize)wizardControl1).EndInit();
			wizardControl1.ResumeLayout(false);
			wizardCompletionPage.ResumeLayout(false);
			wizardCompletionPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			wizardSelectDatabasePage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridListTest).EndInit();
			ResumeLayout(false);
		}

		public void Init()
		{
			AddToolTips();
			Global.ChangeApplicationStatusMessage(Text);
		}

		private string GetEncyptedPassword()
		{
			return "";
		}

		private bool ValidateEntries()
		{
			return true;
		}

		public void LoadData()
		{
			try
			{
				DataSet pendingDataPatches = Factory.DatabaseSystem.GetPendingDataPatches();
				dataGridListTest.ApplyUIDesign();
				dataGridListTest.ContextMenuStrip = null;
				DataTable dataTable = new DataTable
				{
					Columns = 
					{
						{
							"Status",
							typeof(int)
						},
						{
							"PatchID",
							typeof(string)
						},
						{
							"Description",
							typeof(string)
						}
					}
				};
				foreach (DataRow row in pendingDataPatches.Tables[0].Rows)
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["Status"] = row["Status"];
					dataRow2["PatchID"] = row["PatchID"];
					dataRow2["Description"] = row["PatchDescription"];
					dataTable.Rows.Add(dataRow2);
				}
				dataGridListTest.DataSource = dataTable;
				dataGridListTest.DisplayLayout.Bands[0].Columns["PatchID"].Hidden = true;
				dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].Header.Caption = "";
				UltraGridColumn ultraGridColumn = dataGridListTest.DisplayLayout.Bands[0].Columns["Status"];
				UltraGridColumn ultraGridColumn2 = dataGridListTest.DisplayLayout.Bands[0].Columns["Status"];
				int num2 = dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].MinWidth = 18;
				int num5 = ultraGridColumn.Width = (ultraGridColumn2.MaxWidth = num2);
				dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].AllowRowSummaries = AllowRowSummaries.False;
				dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].CellDisplayStyle = CellDisplayStyle.FullEditorDisplay;
				ValueList valueList = new ValueList();
				ValueListItem item = new ValueListItem(1, " ");
				valueList.ValueListItems.Add(item);
				item = new ValueListItem(3, " ");
				item.Appearance.Image = imageList.Images["wait"];
				valueList.ValueListItems.Add(item);
				item = new ValueListItem(2, " ");
				item.Appearance.Image = imageList.Images["tick"];
				valueList.ValueListItems.Add(item);
				item = new ValueListItem(4, " ");
				item.Appearance.Image = imageList.Images["failed"];
				valueList.ValueListItems.Add(item);
				dataGridListTest.DisplayLayout.Bands[0].Columns["Status"].ValueList = valueList;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
		}

		private bool ExecutePendingPatches()
		{
			UltraGridRow ultraGridRow = null;
			try
			{
				foreach (UltraGridRow row in dataGridListTest.Rows)
				{
					int num = 1;
					ultraGridRow = row;
					if (!row.Cells["Status"].Value.IsNullOrEmpty())
					{
						num = int.Parse(row.Cells["Status"].Value.ToString());
					}
					if (num != 2)
					{
						row.Cells["Status"].Value = 3;
						Application.DoEvents();
						string patchID = row.Cells["PatchID"].Value.ToString();
						if (!Factory.DatabaseSystem.ExecuteDataPatch(patchID))
						{
							row.Cells["Status"].Value = 4;
							return false;
						}
						row.Cells["Status"].Value = 2;
						Application.DoEvents();
					}
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				ultraGridRow.Cells["Status"].Value = 4;
				return false;
			}
		}

		private bool UpgradeDatabase()
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Application.DoEvents();
				_ = Global.CurrentInstanceName;
				return true;
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
				if (ex2.Number == 1042)
				{
					ErrorHelper.ErrorMessage("Cannot connect to the database.", "Please make sure that the user id and password are correct.");
					return false;
				}
				return false;
			}
			catch (DacServicesException ex3)
			{
				ErrorHelper.ErrorMessage("Unable to upgrade the database. Make sure that the user id and password are correct and you have administrator right.", "Error is:" + ex3.Message);
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
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
					LoadData();
					wizardControl1.NextText = "Execute";
					wizardControl1.SelectedPage.AllowBack = false;
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
				base.Visible = false;
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

		public CompanyDatabase BringUp(Form parent)
		{
			ShowDialog(parent);
			return database;
		}

		private void UpgradeDBDataForm_Activated(object sender, EventArgs e)
		{
			Application.DoEvents();
		}

		private void wizardControl1_NextClick(object sender, WizardCommandButtonClickEventArgs e)
		{
			if (!SetWizardPage(e.Page) || !allDone)
			{
				e.Handled = true;
			}
		}

		private bool SetWizardPage(BaseWizardPage page)
		{
			if (page.Name == "wizardSelectDatabasePage")
			{
				bool flag = true;
				if (!allDone)
				{
					flag = ExecutePendingPatches();
				}
				if (!flag)
				{
					return false;
				}
				wizardControl1.NextText = "Next";
				allDone = true;
			}
			return true;
		}

		private void wizardControl1_FinishClick(object sender, CancelEventArgs e)
		{
			Close();
		}

		private void wizardControl1_CancelClick(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			if (ErrorHelper.QuestionMessageYesNo("Are you sure to cancel the wizard?") == DialogResult.Yes)
			{
				Close();
			}
		}

		private void radioButtonOverwrite_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void radioButtonNew_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void wizardControl1_CustomizeCommandButtons(object sender, CustomizeCommandButtonsEventArgs e)
		{
			if (e.Page.Name == "wizardUpgradeSummaryPage")
			{
				e.NextButton.Text = "Upgrade";
			}
			if (e.Page.Name == "wizardCompletionPage")
			{
				e.PrevButton.Visible = false;
				e.CancelButton.Visible = false;
			}
		}
	}
}
