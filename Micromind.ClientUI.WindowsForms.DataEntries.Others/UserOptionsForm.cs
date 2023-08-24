using Infragistics.Win;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class UserOptionsForm : Form, IForm
	{
		private CompanyInformationData currentData;

		private CompanyOptionData companyOptionData;

		private string TABLENAME_CONST = "";

		private string IDFIELD_CONST = "";

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private FormManager formManager;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl tabPageGeneral;

		private CheckBox checkBoxMDI;

		private CheckBox checkBoxResizeToFit;

		private CheckBox checkBoxShowNavigationBar;

		private UltraTabPageControl ultraTabPageControl1;

		private CheckBox checkBoxMergeMatrix;

		private CheckBox checkBoxPrintReport;

		private CheckBox checkBoxPrintDocument;

		private Label label1;

		private CheckBox checkBoxIgnoreSpace;

		private Label label2;

		private DateControl dateControlDefault;

		private CheckBox checkBoxShowSlNo;

		private CheckBox checkBoxShowFilter;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6002;

		public ScreenTypes ScreenType => ScreenTypes.Setup;

		private bool IsDirty => formManager.GetDirtyStatus();

		private bool IsNewRecord
		{
			get
			{
				return false;
			}
			set
			{
				isNewRecord = value;
			}
		}

		public UserOptionsForm()
		{
			InitializeComponent();
		}

		private bool GetData()
		{
			try
			{
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			SaveData();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == "") && CanClose())
				{
					checkBoxMDI.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.OpenMDIForms.ToString(), false).ToString());
					checkBoxResizeToFit.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ResizeListColumnsToFit.ToString(), true).ToString());
					checkBoxShowNavigationBar.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowNavigationBar.ToString(), true).ToString());
					checkBoxPrintDocument.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.PrintDocumentOnLetterHead.ToString(), false).ToString());
					checkBoxPrintReport.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.PrintReportOnLetterHead.ToString(), false).ToString());
					checkBoxMergeMatrix.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.MergeMatrixItems.ToString(), false).ToString());
					checkBoxIgnoreSpace.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.IgnoreSpaceInComboSearch.ToString(), false).ToString());
					checkBoxShowSlNo.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), false).ToString());
					checkBoxShowFilter.Checked = bool.Parse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.ShowFilter.ToString(), false).ToString());
					DatePeriods datePeriods = DatePeriods.ThisMonthToDate;
					int result = 0;
					int.TryParse(Factory.SettingSystem.GetUserSetting(Global.CurrentUser, UserOptionsEnum.DefaultDate.ToString(), DatePeriods.ThisMonthToDate).ToString(), out result);
					datePeriods = (DatePeriods)result;
					dateControlDefault.SelectedPeriod = datePeriods;
					IsNewRecord = false;
					formManager.ResetDirty();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0)
			{
				_ = currentData.Tables[0].Rows.Count;
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				Close();
				return true;
			}
			if (!IsNewRecord)
			{
				switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
				{
				case DialogResult.No:
					return true;
				case DialogResult.Cancel:
					return false;
				}
			}
			if (!ValidateData())
			{
				return false;
			}
			if (!GetData())
			{
				return false;
			}
			try
			{
				bool flag = true;
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.OpenMDIForms.ToString(), checkBoxMDI.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.ResizeListColumnsToFit.ToString(), checkBoxResizeToFit.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.ShowNavigationBar.ToString(), checkBoxShowNavigationBar.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.PrintDocumentOnLetterHead.ToString(), checkBoxPrintDocument.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.PrintReportOnLetterHead.ToString(), checkBoxPrintReport.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.MergeMatrixItems.ToString(), checkBoxMergeMatrix.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.IgnoreSpaceInComboSearch.ToString(), checkBoxIgnoreSpace.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.ShowSlNo.ToString(), checkBoxShowSlNo.Checked);
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.ShowFilter.ToString(), checkBoxShowFilter.Checked);
				int selectedPeriod = (int)dateControlDefault.SelectedPeriod;
				flag &= Factory.SettingSystem.SaveSetting(Global.CurrentUser, UserOptionsEnum.DefaultDate.ToString(), selectedPeriod);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					CompanyPreferences.LoadCompanyPreferences();
					ErrorHelper.InformationMessage("Some changes may need to restart the application in order to take effect.");
					UserPreferences.LoadUserPreferences();
					ClearForm();
					Close();
				}
				return flag;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private bool ValidateData()
		{
			if (dateControlDefault.SelectedPeriod == DatePeriods.Custom)
			{
				ErrorHelper.ErrorMessage("You cannot set default date period as 'Custom'.");
				return false;
			}
			return true;
		}

		private void buttonNew_Click(object sender, EventArgs e)
		{
			if (IsNewRecord)
			{
				ClearForm();
			}
			else if (SaveData())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private void ClearForm()
		{
			formManager.ResetDirty();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void OnActivated()
		{
		}

		private void SetSecurity()
		{
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private void Form_Load(object sender, EventArgs e)
		{
			try
			{
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					LoadData("1");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.UserOptionsForm));
			tabPageGeneral = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxShowFilter = new System.Windows.Forms.CheckBox();
			checkBoxShowSlNo = new System.Windows.Forms.CheckBox();
			label2 = new System.Windows.Forms.Label();
			dateControlDefault = new Micromind.DataControls.DateControl();
			label1 = new System.Windows.Forms.Label();
			checkBoxIgnoreSpace = new System.Windows.Forms.CheckBox();
			checkBoxShowNavigationBar = new System.Windows.Forms.CheckBox();
			checkBoxResizeToFit = new System.Windows.Forms.CheckBox();
			checkBoxMDI = new System.Windows.Forms.CheckBox();
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			checkBoxMergeMatrix = new System.Windows.Forms.CheckBox();
			checkBoxPrintReport = new System.Windows.Forms.CheckBox();
			checkBoxPrintDocument = new System.Windows.Forms.CheckBox();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			formManager = new Micromind.DataControls.FormManager();
			tabPageGeneral.SuspendLayout();
			ultraTabPageControl1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			tabPageGeneral.Controls.Add(checkBoxShowFilter);
			tabPageGeneral.Controls.Add(checkBoxShowSlNo);
			tabPageGeneral.Controls.Add(label2);
			tabPageGeneral.Controls.Add(dateControlDefault);
			tabPageGeneral.Controls.Add(label1);
			tabPageGeneral.Controls.Add(checkBoxIgnoreSpace);
			tabPageGeneral.Controls.Add(checkBoxShowNavigationBar);
			tabPageGeneral.Controls.Add(checkBoxResizeToFit);
			tabPageGeneral.Controls.Add(checkBoxMDI);
			tabPageGeneral.Location = new System.Drawing.Point(-10000, -10000);
			tabPageGeneral.Name = "tabPageGeneral";
			tabPageGeneral.Size = new System.Drawing.Size(739, 404);
			checkBoxShowFilter.AutoSize = true;
			checkBoxShowFilter.Location = new System.Drawing.Point(16, 103);
			checkBoxShowFilter.Name = "checkBoxShowFilter";
			checkBoxShowFilter.Size = new System.Drawing.Size(115, 17);
			checkBoxShowFilter.TabIndex = 10;
			checkBoxShowFilter.Text = "Show Filter on Grid";
			checkBoxShowFilter.UseVisualStyleBackColor = true;
			checkBoxShowSlNo.AutoSize = true;
			checkBoxShowSlNo.Location = new System.Drawing.Point(16, 81);
			checkBoxShowSlNo.Name = "checkBoxShowSlNo";
			checkBoxShowSlNo.Size = new System.Drawing.Size(136, 17);
			checkBoxShowSlNo.TabIndex = 9;
			checkBoxShowSlNo.Text = "Show Serial No on Grid";
			checkBoxShowSlNo.UseVisualStyleBackColor = true;
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(6, 200);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(79, 13);
			label2.TabIndex = 8;
			label2.Text = "Default Date";
			dateControlDefault.CustomReportFieldName = "";
			dateControlDefault.CustomReportKey = "";
			dateControlDefault.CustomReportValueType = 1;
			dateControlDefault.FromDate = new System.DateTime(2018, 1, 1, 0, 0, 0, 0);
			dateControlDefault.Location = new System.Drawing.Point(13, 216);
			dateControlDefault.Name = "dateControlDefault";
			dateControlDefault.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControlDefault.Size = new System.Drawing.Size(275, 50);
			dateControlDefault.TabIndex = 7;
			dateControlDefault.ToDate = new System.DateTime(2018, 1, 15, 23, 59, 59, 59);
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(6, 140);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(67, 13);
			label1.TabIndex = 6;
			label1.Text = "Data Entry";
			checkBoxIgnoreSpace.AutoSize = true;
			checkBoxIgnoreSpace.Location = new System.Drawing.Point(15, 160);
			checkBoxIgnoreSpace.Name = "checkBoxIgnoreSpace";
			checkBoxIgnoreSpace.Size = new System.Drawing.Size(202, 17);
			checkBoxIgnoreSpace.TabIndex = 5;
			checkBoxIgnoreSpace.Text = "Ignore blank space when filtering lists";
			checkBoxIgnoreSpace.UseVisualStyleBackColor = true;
			checkBoxShowNavigationBar.AutoSize = true;
			checkBoxShowNavigationBar.Location = new System.Drawing.Point(16, 59);
			checkBoxShowNavigationBar.Name = "checkBoxShowNavigationBar";
			checkBoxShowNavigationBar.Size = new System.Drawing.Size(126, 17);
			checkBoxShowNavigationBar.TabIndex = 2;
			checkBoxShowNavigationBar.Text = "Show Navigation Bar";
			checkBoxShowNavigationBar.UseVisualStyleBackColor = true;
			checkBoxResizeToFit.AutoSize = true;
			checkBoxResizeToFit.Location = new System.Drawing.Point(16, 37);
			checkBoxResizeToFit.Name = "checkBoxResizeToFit";
			checkBoxResizeToFit.Size = new System.Drawing.Size(191, 17);
			checkBoxResizeToFit.TabIndex = 1;
			checkBoxResizeToFit.Text = "Resize list columns to fit the screen";
			checkBoxResizeToFit.UseVisualStyleBackColor = true;
			checkBoxMDI.AutoSize = true;
			checkBoxMDI.Location = new System.Drawing.Point(16, 15);
			checkBoxMDI.Name = "checkBoxMDI";
			checkBoxMDI.Size = new System.Drawing.Size(213, 17);
			checkBoxMDI.TabIndex = 0;
			checkBoxMDI.Text = "Open all screens inside the main screen";
			checkBoxMDI.UseVisualStyleBackColor = true;
			ultraTabPageControl1.Controls.Add(checkBoxMergeMatrix);
			ultraTabPageControl1.Controls.Add(checkBoxPrintReport);
			ultraTabPageControl1.Controls.Add(checkBoxPrintDocument);
			ultraTabPageControl1.Location = new System.Drawing.Point(1, 20);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(739, 404);
			checkBoxMergeMatrix.AutoSize = true;
			checkBoxMergeMatrix.Location = new System.Drawing.Point(15, 15);
			checkBoxMergeMatrix.Name = "checkBoxMergeMatrix";
			checkBoxMergeMatrix.Size = new System.Drawing.Size(225, 17);
			checkBoxMergeMatrix.TabIndex = 0;
			checkBoxMergeMatrix.Text = "Merge matrix items in sales invoice printing";
			checkBoxMergeMatrix.UseVisualStyleBackColor = true;
			checkBoxMergeMatrix.Visible = false;
			checkBoxPrintReport.AutoSize = true;
			checkBoxPrintReport.Location = new System.Drawing.Point(15, 62);
			checkBoxPrintReport.Name = "checkBoxPrintReport";
			checkBoxPrintReport.Size = new System.Drawing.Size(156, 17);
			checkBoxPrintReport.TabIndex = 2;
			checkBoxPrintReport.Text = "Print Report on Letter Head";
			checkBoxPrintReport.UseVisualStyleBackColor = true;
			checkBoxPrintDocument.AutoSize = true;
			checkBoxPrintDocument.Location = new System.Drawing.Point(15, 39);
			checkBoxPrintDocument.Name = "checkBoxPrintDocument";
			checkBoxPrintDocument.Size = new System.Drawing.Size(173, 17);
			checkBoxPrintDocument.TabIndex = 1;
			checkBoxPrintDocument.Text = "Print Document on Letter Head";
			checkBoxPrintDocument.UseVisualStyleBackColor = true;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 439);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(761, 40);
			panelButtons.TabIndex = 14;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(761, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(651, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 3;
			xpButton1.Text = "&Cancel";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(buttonClose_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(551, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			ultraTabControl1.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(tabPageGeneral);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Location = new System.Drawing.Point(8, 8);
			ultraTabControl1.MinTabWidth = 80;
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(741, 425);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio2005;
			ultraTabControl1.TabIndex = 1;
			appearance.BackColor = System.Drawing.Color.WhiteSmoke;
			ultraTab.Appearance = appearance;
			ultraTab.TabPage = tabPageGeneral;
			ultraTab.Text = "&General";
			ultraTab2.TabPage = ultraTabPageControl1;
			ultraTab2.Text = "&Printing";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[2]
			{
				ultraTab,
				ultraTab2
			});
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(739, 404);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 15;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton1;
			base.ClientSize = new System.Drawing.Size(761, 479);
			base.Controls.Add(ultraTabControl1);
			base.Controls.Add(panelButtons);
			base.Controls.Add(formManager);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "UserOptionsForm";
			Text = "User Preferences";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form_FormClosing);
			base.Load += new System.EventHandler(Form_Load);
			tabPageGeneral.ResumeLayout(false);
			tabPageGeneral.PerformLayout();
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
