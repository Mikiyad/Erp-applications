using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using LicenseManager;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.Reports;
using Micromind.ClientUI.SoftReg;
using Micromind.ClientUI.WindowsForms.Main;
using Micromind.ClientUI.WindowsForms.Test;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.Securities;
using Micromind.Utilities.AppUpdater;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	[FileIOPermission(SecurityAction.Demand)]
	public class formPOSMain : RibbonForm
	{
		private static bool GUI_TESTING;

		private string updateDirectoryPath;

		private bool isBackForwardButtonPressed;

		private SplashForm splash;

		private bool suppressCloseMessage = true;

		private string updateExecutableFileName = "";

		private bool isFirstTimeActivated = true;

		private bool loaded;

		private bool isLoadingFinished;

		private PrintDialog printDialog1;

		private ContextMenu contextMenu;

		private ToolTip toolTip;

		private ImageList imageListIcons;

		public ImageList imageListPictures;

		private PageSetupDialog pageSetupDialog;

		private System.Windows.Forms.Timer timer;

		private System.Windows.Forms.Timer noteTimer;

		private ImageList imageList3;

		private ImageList imageListSmallIcons;

		private ImageCollection imageCollection1;

		private BarAndDockingController barAndDockingController;

		private ImageList imageListMenuBar;

		private BarSubItem barSubItem3;

		private BarButtonItem barButtonItem36;

		private BarButtonItem barButtonItem43;

		private BarButtonItem barButtonItem44;

		private BarButtonItem barButtonItem45;

		private BarButtonItem barButtonItem46;

		private BarButtonItem barButtonItem47;

		private BarButtonItem barButtonItem48;

		private BarButtonItem barButtonItem49;

		private BarButtonItem barButtonItem50;

		private BarButtonItem barButtonItem10;

		private BarButtonItem barButtonReminders;

		private BarButtonItem barButtonItem5;

		private BarButtonItem barButtonItem6;

		private BarButtonItem barButtonReminder;

		private BarButtonItem barButtonItem37;

		private BarButtonItem barButtonItem38;

		private BarButtonItem barButtonItem39;

		private BarButtonItem barButtonItem40;

		private RibbonControl ribbonControlMain;

		private IContainer components;

		private readonly Navigator navigator;

		private bool isTesting;

		private bool isClosing;

		private bool isCreatingNewCompany;

		private Navigator Navigator => navigator;

		private bool SuppressCloseMessage
		{
			get
			{
				try
				{
					suppressCloseMessage = !bool.Parse(Global.CompanySettings.GetSetting("DisplayConfirmOnExit", true).ToString());
				}
				catch
				{
				}
				return suppressCloseMessage;
			}
			set
			{
				suppressCloseMessage = value;
			}
		}

		private event EventHandler ReminderRequest;

		public formPOSMain()
		{
			InitializeComponent();
			splash = new SplashForm(GlobalRules.IsTrial);
			Init();
			AxolonLicense.LicenseManager.LicenseError += LicenseManager_LicenseError;
		}

		private void LicenseManager_LicenseError(object sender, EventArgs e)
		{
			_ = (LicenseManagerControl.LicenseErrors)sender;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPOSMain));
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.imageListPictures = new System.Windows.Forms.ImageList(this.components);
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.noteTimer = new System.Windows.Forms.Timer(this.components);
            this.imageList3 = new System.Windows.Forms.ImageList(this.components);
            this.imageListSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.imageListMenuBar = new System.Windows.Forms.ImageList(this.components);
            this.barSubItem3 = new DevExpress.XtraBars.BarSubItem();
            this.barButtonItem36 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem43 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem44 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem45 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem46 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem47 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem48 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem49 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem50 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem10 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonReminders = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonReminder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem37 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem38 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem39 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem40 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonControlMain = new DevExpress.XtraBars.Ribbon.RibbonControl();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlMain)).BeginInit();
            this.SuspendLayout();
            // 
            // imageListIcons
            // 
            this.imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListPictures
            // 
            this.imageListPictures.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListPictures.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListPictures.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 200;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // noteTimer
            // 
            this.noteTimer.Enabled = true;
            this.noteTimer.Interval = 6000;
            this.noteTimer.Tick += new System.EventHandler(this.noteTimer_Tick);
            // 
            // imageList3
            // 
            this.imageList3.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList3.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList3.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // imageListSmallIcons
            // 
            this.imageListSmallIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListSmallIcons.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // barAndDockingController
            // 
            this.barAndDockingController.LookAndFeel.SkinName = "iMaginary";
            this.barAndDockingController.LookAndFeel.UseDefaultLookAndFeel = false;
            this.barAndDockingController.PropertiesBar.AllowLinkLighting = false;
            this.barAndDockingController.PropertiesDocking.ViewStyle = DevExpress.XtraBars.Docking2010.Views.DockingViewStyle.Classic;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // imageListMenuBar
            // 
            this.imageListMenuBar.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListMenuBar.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListMenuBar.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // barSubItem3
            // 
            this.barSubItem3.Caption = "Find";
            this.barSubItem3.CategoryGuid = new System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1");
            this.barSubItem3.Description = "Searches for the specified text";
            this.barSubItem3.Hint = "Searches for the specified text";
            this.barSubItem3.Id = 2;
            this.barSubItem3.ImageOptions.ImageIndex = 3;
            this.barSubItem3.ImageOptions.LargeImageIndex = 4;
            this.barSubItem3.Name = "barSubItem3";
            this.barSubItem3.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            // 
            // barButtonItem36
            // 
            this.barButtonItem36.Caption = "barButtonItem35";
            this.barButtonItem36.Id = 115;
            this.barButtonItem36.Name = "barButtonItem36";
            // 
            // barButtonItem43
            // 
            this.barButtonItem43.Id = 128;
            this.barButtonItem43.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem43.Name = "barButtonItem43";
            // 
            // barButtonItem44
            // 
            this.barButtonItem44.Id = 128;
            this.barButtonItem44.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem44.Name = "barButtonItem44";
            // 
            // barButtonItem45
            // 
            this.barButtonItem45.Id = 128;
            this.barButtonItem45.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem45.Name = "barButtonItem45";
            // 
            // barButtonItem46
            // 
            this.barButtonItem46.Id = 128;
            this.barButtonItem46.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem46.Name = "barButtonItem46";
            // 
            // barButtonItem47
            // 
            this.barButtonItem47.Id = 128;
            this.barButtonItem47.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem47.Name = "barButtonItem47";
            // 
            // barButtonItem48
            // 
            this.barButtonItem48.Id = 128;
            this.barButtonItem48.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem48.Name = "barButtonItem48";
            // 
            // barButtonItem49
            // 
            this.barButtonItem49.Id = 129;
            this.barButtonItem49.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.edit;
            this.barButtonItem49.Name = "barButtonItem49";
            // 
            // barButtonItem50
            // 
            this.barButtonItem50.Id = 129;
            this.barButtonItem50.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.edit;
            this.barButtonItem50.Name = "barButtonItem50";
            // 
            // barButtonItem10
            // 
            this.barButtonItem10.Id = 128;
            this.barButtonItem10.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.add;
            this.barButtonItem10.Name = "barButtonItem10";
            // 
            // barButtonReminders
            // 
            this.barButtonReminders.Caption = "Reminders";
            this.barButtonReminders.Id = 122;
            this.barButtonReminders.ImageOptions.LargeImage = global::Micromind.ClientUI.Properties.Resources.reminder;
            this.barButtonReminders.Name = "barButtonReminders";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "barButtonItem5";
            this.barButtonItem5.Id = 144;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Caption = "Reminder";
            this.barButtonItem6.Id = 145;
            this.barButtonItem6.ImageOptions.LargeImage = global::Micromind.ClientUI.Properties.Resources.reminder1;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonReminder
            // 
            this.barButtonReminder.Caption = "Reminder";
            this.barButtonReminder.Id = 145;
            this.barButtonReminder.ImageOptions.LargeImage = global::Micromind.ClientUI.Properties.Resources.reminder1;
            this.barButtonReminder.Name = "barButtonReminder";
            // 
            // barButtonItem37
            // 
            this.barButtonItem37.Caption = "barButtonItem37";
            this.barButtonItem37.Id = 104;
            this.barButtonItem37.Name = "barButtonItem37";
            // 
            // barButtonItem38
            // 
            this.barButtonItem38.Caption = "barButtonItem38";
            this.barButtonItem38.Id = 105;
            this.barButtonItem38.Name = "barButtonItem38";
            // 
            // barButtonItem39
            // 
            this.barButtonItem39.Caption = "barButtonItem39";
            this.barButtonItem39.Id = 106;
            this.barButtonItem39.Name = "barButtonItem39";
            // 
            // barButtonItem40
            // 
            this.barButtonItem40.Caption = "barButtonItem40";
            this.barButtonItem40.Id = 107;
            this.barButtonItem40.Name = "barButtonItem40";
            // 
            // ribbonControlMain
            // 
            this.ribbonControlMain.ApplicationButtonImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("ribbonControlMain.ApplicationButtonImageOptions.Image")));
            this.ribbonControlMain.Categories.AddRange(new DevExpress.XtraBars.BarManagerCategory[] {
            new DevExpress.XtraBars.BarManagerCategory("File", new System.Guid("4b511317-d784-42ba-b4ed-0d2a746d6c1f")),
            new DevExpress.XtraBars.BarManagerCategory("Edit", new System.Guid("7c2486e1-92ea-4293-ad55-b819f61ff7f1")),
            new DevExpress.XtraBars.BarManagerCategory("Format", new System.Guid("d3052f28-4b3e-4bae-b581-b3bb1c432258")),
            new DevExpress.XtraBars.BarManagerCategory("Help", new System.Guid("e07a4c24-66ac-4de6-bbcb-c0b6cfa7798b")),
            new DevExpress.XtraBars.BarManagerCategory("Status", new System.Guid("77795bb7-9bc5-4dd2-a297-cc758682e23d"))});
            this.ribbonControlMain.Controller = this.barAndDockingController;
            this.ribbonControlMain.ExpandCollapseItem.Id = 0;
            this.ribbonControlMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControlMain.ExpandCollapseItem,
            this.barButtonItem37,
            this.barButtonItem38,
            this.barButtonItem39,
            this.barButtonItem40});
            this.ribbonControlMain.LargeImages = this.imageCollection1;
            this.ribbonControlMain.Location = new System.Drawing.Point(0, 0);
            this.ribbonControlMain.MaxItemId = 108;
            this.ribbonControlMain.Name = "ribbonControlMain";
            this.ribbonControlMain.PageCategoryAlignment = DevExpress.XtraBars.Ribbon.RibbonPageCategoryAlignment.Right;
            this.ribbonControlMain.Size = new System.Drawing.Size(1120, 65);
            this.ribbonControlMain.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            this.ribbonControlMain.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // formPOSMain
            // 
            this.Appearance.ForeColor = System.Drawing.Color.Black;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 756);
            this.Controls.Add(this.ribbonControlMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.HelpButton = true;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(688, 520);
            this.Name = "formPOSMain";
            this.Ribbon = this.ribbonControlMain;
            this.Text = "Starasoft StarERP Point of Sale";
            this.Activated += new System.EventHandler(this.formPOSMain_Activated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formPOSMain_Closing);
            this.Load += new System.EventHandler(this.formPOSMain_Load);
            this.Enter += new System.EventHandler(this.formPOSMain_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void AddEvents()
		{
			base.Resize += formPOSMain_Resize;
			base.MdiChildActivate += OnMdiChildActivate;
			Global.OnReconnectionNeeded += OnReconnectionNeeded;
			Global.OnOpenCompany += OnOpenCompany;
			Global.UpdateNeeded += Global_UpdateNeeded;
			base.HelpRequested += OnHelpButtonClick;
			Global.OnChangeStatus += OnStatusMessageChanged;
			Factory.OnConnecting += OnConnecting;
			FormActivator.ScreenClose += FormActivator_ScreenClose;
			FormActivator.ScreenShow += FormActivator_ScreenShow;
			base.FormClosed += formPOSMain_FormClosed;
		}

		private void formPOSMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				Application.Exit();
			}
			catch
			{
			}
		}

		private void EventHelper_LogoutRequested(object sender, EventArgs e)
		{
			if (CloseCompany(isExiting: false))
			{
				Login();
			}
		}

		private void FormActivator_ScreenShow(object sender, EventArgs e)
		{
			BarButtonItem barButtonItem = new BarButtonItem();
			barButtonItem.Caption = ((Form)sender).Text;
			barButtonItem.Tag = ((Form)sender).Name;
			barButtonItem.ItemClick += item_ItemClick;
		}

		private void item_ItemClick(object sender, ItemClickEventArgs e)
		{
			FormActivator.BringFormToFront(e.Item.Tag.ToString());
		}

		private void FormActivator_ScreenClose(object sender, EventArgs e)
		{
		}

		private void formPOSMain_Load(object sender, EventArgs e)
		{
			RegistryHelper registryHelper = new RegistryHelper();
			string stringValue = registryHelper.GetStringValue(registryHelper.CurrentWindowsUserKey, "Skin", "");
			if (stringValue != "")
			{
				SetSking(stringValue);
			}
			else
			{
				SetSking("iMaginary");
			}
			loaded = false;
			LoadIcons();
			_ = splash;
			Application.DoEvents();
			loaded = true;
		}

		private void SetExpired()
		{
		}

		public Image GetVoidImage()
		{
			return imageListPictures.Images[0];
		}

		public Image GetPaidImage()
		{
			return imageListPictures.Images[1];
		}

		public void SetFullScreen(bool fullScreen)
		{
			if (fullScreen)
			{
				base.FormBorderStyle = FormBorderStyle.None;
				base.Menu = null;
				base.WindowState = FormWindowState.Maximized;
			}
			else
			{
				base.FormBorderStyle = FormBorderStyle.Sizable;
			}
		}

		private static void LoadKey()
		{
		}

		[STAThread]
		public static void Main1()
		{
			LoadKey();
			if (!GUI_TESTING)
			{
				if (!GUI_TESTING)
				{
					try
					{
						try
						{
							GlobalRules.CultureName = Global.GlobalSettings.GetSetting("CultureName", string.Empty).ToString();
							Thread.CurrentThread.CurrentUICulture = new CultureInfo(GlobalRules.CultureName, useUserOverride: false);
						}
						catch
						{
							GlobalRules.CultureName = string.Empty;
						}
						Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA);
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(defaultValue: false);
					}
					catch (Exception ex)
					{
						ErrorHelper.ProcessError(ex, "Please send us the error message for further investigation.", "");
						Application.Exit();
					}
				}
				Application.Run(new formPOSMain());
			}
		}

		private void LoadIcons()
		{
		}

		public void SetImages(FormCategory formCategory, Panel panel)
		{
		}

		public void SetImages(ScreenAreas area)
		{
			switch (area)
			{
			case ScreenAreas.Accounts:
			case ScreenAreas.HR:
			case ScreenAreas.ReportsAccounts:
			case ScreenAreas.System:
				break;
			case ScreenAreas.Company:
				SetImages(FormCategory.COMPANY, null);
				break;
			case ScreenAreas.Products:
				SetImages(FormCategory.PRODUCT, null);
				break;
			case ScreenAreas.Sales:
				SetImages(FormCategory.CUSTOMER, null);
				break;
			case ScreenAreas.Purchases:
				SetImages(FormCategory.VENDOR, null);
				break;
			case ScreenAreas.POS:
				SetImages(FormCategory.CUSTOMER, null);
				break;
			case ScreenAreas.Reports:
				SetImages(FormCategory.REPORT, null);
				break;
			case ScreenAreas.General:
				SetImages(FormCategory.HOME, null);
				break;
			}
		}

		private bool HasUniqueScreenID(ArrayList list, int screenID, ArrayList sameIDForms)
		{
			bool result = true;
			foreach (MethodInfo item in list)
			{
				if (int.Parse(item.Invoke(null, null).ToString()) == screenID)
				{
					sameIDForms.Add(item);
					result = false;
				}
			}
			return result;
		}

		public void TestGUI(string logFileName, string c)
		{
			isTesting = true;
			OnActivated(null);
			new ArrayList();
			new ArrayList();
			new StringBuilder();
			if (!(c != "pass7446610"))
			{
				MessageBox.Show("This module works in debug mode only.");
				isTesting = false;
			}
		}

		private void Init()
		{
			if (splash != null)
			{
				splash.Show();
				Application.DoEvents();
			}
			FormActivator.ProgramLoaded = false;
			AddEvents();
			Application.DoEvents();
			SuspendLayout();
			FormActivator.ParentForm = this;
			FormActivator.ProgramLoaded = true;
			ResumeLayout();
			Global.Init();
			FormActivator.SetShowForm(val: false);
			Init2();
			FormActivator.SetShowForm(val: true);
			LoadRegistry();
			noteTimer.Enabled = true;
			noteTimer.Interval = 6000;
			EventHelper.AddEvents();
			Translate();
			Micromind.Securities.RulesReader.VersionNumber = new Version(Application.ProductVersion).Major;
		}

		private void refereshItem_Click(object sender, EventArgs e)
		{
			ChangeLanguage(null);
		}

		private void ChangeLanguage(ToolStripMenuItem item)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				Translator.Translators.IsTranslatorActive = true;
				ChangeStatusMessage("Translating...");
				IBaseForm baseForm = null;
				if (item != null)
				{
					Translator.Translators.ChangeLanguage(item.Text);
					if (base.ActiveMdiChild != null && FormActivator.POSHomeFormObj.GetType() != base.ActiveMdiChild.GetType())
					{
						baseForm = (base.ActiveMdiChild as IBaseForm);
						if (baseForm != null)
						{
							baseForm.Translate();
							base.ActiveMdiChild.Refresh();
						}
					}
					Translate();
					_ = (FormActivator.POSHomeFormObj.GetType() != base.ActiveMdiChild.GetType());
					Form[] mdiChildren = base.MdiChildren;
					foreach (Form form in mdiChildren)
					{
						if ((form != null && base.ActiveMdiChild == null) || (form != null && form.GetType() != base.ActiveMdiChild.GetType()))
						{
							baseForm = (form as IBaseForm);
							if (baseForm != null)
							{
								baseForm.Translate();
								form.Refresh();
							}
						}
					}
					ShowLanguageChangesWarning();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
				ChangeStatusMessage(null);
				_ = Translator.Translators.IsTranslatorActive;
			}
		}

		private void ShowLanguageChangesWarning()
		{
			_ = Translator.Translators.NumberOfTimesLanguagesChanged;
			_ = 1;
		}

		private void languageItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem item = sender as ToolStripMenuItem;
			ChangeLanguage(item);
		}

		private void Init2()
		{
		}

		private void LoadOtherData()
		{
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
		}

		public void RefreshChild()
		{
			lock (this)
			{
				try
				{
					int screenID = UIRefelector.GetScreenID(base.ActiveMdiChild);
					if (screenID != -1)
					{
						if (Security.HasAccessRight(screenID, suppressMessage: false))
						{
							try
							{
								_ = (base.ActiveMdiChild is IDataForm);
							}
							catch (Exception e)
							{
								ErrorHelper.ProcessError(e);
							}
						}
						else
						{
							ChangeStatusMessage(base.ActiveMdiChild.Text + ": " + SR.GetString("00162"));
						}
					}
				}
				catch
				{
					Refresh();
				}
			}
		}

		internal void GoHome()
		{
			lock (this)
			{
				try
				{
					if (Factory.IsDBConnected)
					{
						if (base.ActiveMdiChild != null && FormActivator.POSHomeFormObj.GetType() == base.ActiveMdiChild.GetType())
						{
							if (!FormActivator.POSHomeFormObj.Focused)
							{
								FormActivator.POSHomeFormObj.Focus();
							}
						}
						else
						{
							FormActivator.BringFormToFront(FormActivator.POSHomeFormObj);
							FormActivator.POSHomeFormObj.Show();
							FormActivator.POSHomeFormObj.WindowState = FormWindowState.Maximized;
							FormActivator.POSHomeFormObj.BringToFront();
							FormActivator.POSHomeFormObj.Focus();
							FormActivator.POSHomeFormObj.OnActivated();
							ChangeStatusMessage(FormActivator.POSHomeFormObj.Text);
						}
					}
				}
				catch
				{
				}
			}
		}

		private void ResizeChildren()
		{
			lock (this)
			{
			}
		}

		private void LoadRegistry()
		{
		}

		private void Global_UpdateNeeded(object sender, EventArgs e)
		{
			AutoAppUpdater autoAppUpdater = sender as AutoAppUpdater;
			if (autoAppUpdater != null)
			{
				updateDirectoryPath = autoAppUpdater.UpdateDirectoryPath;
				updateExecutableFileName = autoAppUpdater.executableFileName;
			}
		}

		private void ApplyRules()
		{
		}

		private void Navigator_OnObjectRemoved(object sender, EventArgs e)
		{
			Form form = sender as Form;
			if (form != null && base.ActiveMdiChild != null && form.GetType() != base.ActiveMdiChild.GetType())
			{
				FormActivator.ResetForm(form);
			}
		}

		private void SaveRegistry()
		{
			Global.GlobalSettings.SaveFormProperties(this);
		}

		private void form_HandleCreated(object sender, EventArgs e)
		{
			(sender as Form)?.Close();
		}

		private void form_Paint(object sender, PaintEventArgs e)
		{
		}

		public void ActivateChild(Form form)
		{
			lock (this)
			{
				if (!isTesting && !isCreatingNewCompany && !isClosing && Factory.IsDBConnected && form != null && !form.IsDisposed && loaded)
				{
					Application.DoEvents();
					try
					{
						UIRefelector.GetScreenID(base.ActiveMdiChild);
						(form as IDataForm)?.OnActivated();
					}
					catch (SqlException ex)
					{
						ErrorHelper.ProcessError(ex);
					}
					catch (Exception e)
					{
						ErrorHelper.ProcessError(e);
					}
					form.Activate();
				}
			}
		}

		private void OnMdiChildActivate(object o, EventArgs e)
		{
			Form form = (Form)o;
			if (form != null)
			{
				form = form.ActiveMdiChild;
				if (form != null)
				{
					ActivateChild(form);
				}
			}
		}

		internal void GoHelp()
		{
			HelpProvider helpProvider = new HelpProvider();
			string text = Application.StartupPath + "\\Axolon Help.chm";
			if (!File.Exists(text))
			{
				ErrorHelper.WarningMessage("The help file cannot be found.");
				return;
			}
			helpProvider.HelpNamespace = text;
			helpProvider.SetHelpKeyword(base.ActiveMdiChild, base.ActiveMdiChild.Text.Trim());
			helpProvider.SetHelpNavigator(base.ActiveMdiChild, HelpNavigator.KeywordIndex);
		}

		private void OnHelpButtonClick(object o, HelpEventArgs e)
		{
			GoHelp();
		}

		private void OnReconnectionNeeded(object o, EventArgs e)
		{
		}

		public bool IsActiveChild(Form form)
		{
			if (form == null)
			{
				return false;
			}
			return base.ActiveMdiChild.GetType() == form.GetType();
		}

		private void Connect()
		{
			Global.ConStatus = ConnectionStatus.DisConnected;
		}

		public void Connect(EventHandler handler)
		{
		}

		private void OnConnection(object o, EventArgs e)
		{
			IsConnected(bool.Parse(o.ToString()));
		}

		private void OnConnecting(object o, EventArgs e)
		{
			bool.Parse(o.ToString());
		}

		private void EnableDisableFunctions()
		{
			if (Global.ConStatus != ConnectionStatus.DisConnected && Global.ConStatus != ConnectionStatus.SQLConnected)
			{
				_ = Global.ConStatus;
			}
		}

		private void MakeVisibleInvisible()
		{
			MakeActivateMenuItemVisibleInvisible();
		}

		private void MakeActivateMenuItemVisibleInvisible()
		{
		}

		public void Translate()
		{
			Translator.Translators.Translate(this);
		}

		private void SetEditionFeatures()
		{
		}

		private void GetSettings()
		{
			_ = Global.ConStatus;
		}

		private void Reset(bool isClosing)
		{
			if (!isCreatingNewCompany)
			{
				try
				{
					RefreshTitle();
					EnableDisableFunctions();
					ComboDataHelper.ResetToRefreshAll();
					CombosData.ResetAllCaches();
				}
				catch
				{
				}
			}
		}

		public void RefreshTitle()
		{
			if (Global.ConStatus == ConnectionStatus.Connected)
			{
				Text = " " + Global.CompanyName + " - " + Application.ProductName;
			}
			else
			{
				Text = " " + Application.ProductName;
			}
		}

		private void OnOpenCompany(object o, EventArgs e)
		{
			RefreshTitle();
			if (bool.Parse(o.ToString()))
			{
				Global.LoadCompanySystemSettings();
				LoadShortcuts();
				Security.LoadUserSecurityData();
				SetMenuSecurity();
			}
		}

		private void SetMenuSecurity()
		{
		}

		private void OnUserChanged(object o, EventArgs e)
		{
			if (loaded && Global.ConStatus != ConnectionStatus.DisConnected)
			{
				Form[] mdiChildren = base.MdiChildren;
				foreach (Form form in mdiChildren)
				{
					try
					{
						(form as IDataList)?.ClearView();
					}
					catch
					{
					}
					try
					{
						int screenID = UIRefelector.GetScreenID(base.ActiveMdiChild);
						if (screenID != -1)
						{
							if (!Security.HasAccessRight(screenID, suppressMessage: true))
							{
								form.Enabled = false;
							}
							else
							{
								form.Enabled = true;
							}
						}
					}
					catch
					{
					}
				}
			}
		}

		private void IsConnected(bool val)
		{
		}

		private void OnComboBoxQuickAdd(object sender, EventArgs e)
		{
		}

		private void PriceLevelComboBox_NewPriceLevel(object sender, EventArgs e)
		{
		}

		private void formPOSMain_Resize(object sender, EventArgs e)
		{
			ResizeChildren();
			ArrangeControls();
			SetSking(barAndDockingController.LookAndFeel.SkinName);
		}

		private void ArrangeControls()
		{
		}

		internal void ApplyChildsPreferences()
		{
			Form[] mdiChildren = base.MdiChildren;
			for (int i = 0; i < mdiChildren.Length; i++)
			{
				(mdiChildren[i] as IBaseForm)?.ApplyPreferences();
			}
		}

		internal Form GetActiveChild()
		{
			return base.ActiveMdiChild;
		}

		private void OnStatusMessageChanged(object o, EventArgs e)
		{
			ChangeStatusMessage(o);
		}

		private void ChangeStatusMessage(object o)
		{
		}

		private new void Activate()
		{
			lock (this)
			{
				if (!loaded)
				{
					StoreData storeData = null;
					if (storeData != null && storeData.StoreTable.Rows.Count > 0)
					{
						Global.DefaultStore = storeData.StoreTable.Rows[0];
					}
					loaded = true;
				}
			}
		}

		private bool Login()
		{
			LoginForm loginForm = new LoginForm();
			loginForm.ShowDialog(this);
			if (loginForm.DialogResult == DialogResult.OK)
			{
				Application.DoEvents();
				DatabaseLoginForm databaseLoginForm = new DatabaseLoginForm();
				if (databaseLoginForm.ShowDialog(this) == DialogResult.Cancel)
				{
					if (databaseLoginForm.IsNewCompanyRequested)
					{
						if (!CreateNewCompany())
						{
							return Login();
						}
						return true;
					}
					if (databaseLoginForm.IsRestoreCompanyRequested)
					{
						if (!RestoreDatabase())
						{
							return Login();
						}
						return true;
					}
					if (databaseLoginForm.IsAttachCompanyRequested)
					{
						if (!AttachDatabase())
						{
							return Login();
						}
						return true;
					}
					return Login();
				}
				PrepareAfterLogin();
				return true;
			}
			Application.Exit();
			return false;
		}

		private void PrepareAfterLogin()
		{
			EnableDisableFunctions();
			CompanyPreferences.LoadCompanyPreferences();
			Format.LoadFormats();
			if (Global.ConStatus == ConnectionStatus.Connected)
			{
				GoHome();
			}
		}

		protected override void OnActivated(EventArgs e)
		{
			lock (this)
			{
				if (isFirstTimeActivated)
				{
					isFirstTimeActivated = false;
					CloseCompany(isExiting: false);
					if (splash != null)
					{
						splash.Close();
						splash = null;
					}
					AxolonLicense.LicenseManager.CheckLicense();
					if (!AxolonLicense.LicenseManager.IsTrialKey)
					{
						_ = AxolonLicense.LicenseManager.IsActivated;
					}
					if (AxolonLicense.LicenseManager.HasLicenseError)
					{
						new RegisterForm().ShowDialog();
						if (!AxolonLicense.LicenseManager.CheckLicense())
						{
							Application.Exit();
						}
					}
					if (AxolonLicense.LicenseManager.License.IsTrial)
					{
						RegisterTrial();
					}
					if (!AxolonLicense.LicenseManager.IsTrialKey || !AxolonLicense.LicenseManager.IsTrialExpired)
					{
						goto IL_0101;
					}
					ErrorHelper.InformationMessage("Thank you for evaluating Micromind Axolon.", "Your trial period has been expired. Please register and activate a full version.");
					new RegisterForm().ShowDialog(this);
					if (AxolonLicense.LicenseManager.CheckLicense() && !AxolonLicense.LicenseManager.IsTrialKey)
					{
						goto IL_0101;
					}
					Application.Exit();
				}
				goto end_IL_0004;
				IL_0169:
				if (Login())
				{
					try
					{
						string registryOptionValue = Global.GetRegistryOptionValue("CURRDB", isEncrypt: false);
						PublicFunctions.StartWaiting(this);
						Application.DoEvents();
						if (!Factory.IsDBConnected)
						{
							if (registryOptionValue != null && registryOptionValue != string.Empty)
							{
								using (DatabaseLoginForm databaseLoginForm = new DatabaseLoginForm())
								{
									if (databaseLoginForm.ShowDialog(this) == DialogResult.OK && Factory.IsDBConnected)
									{
										PublicFunctions.StartWaiting(this);
										GoHome();
									}
								}
							}
						}
						else
						{
							GoHome();
							Application.DoEvents();
							Application.DoEvents();
							if (AxolonLicense.LicenseManager.IsTrialKey && (Global.IsTrialLimitReached = Factory.DatabaseSystem.IsTrialLimitReached()))
							{
								ErrorHelper.InformationMessage("Evaluation version is limited in number of transactions. You have reached the limitation. Some functionalities may be disabled.");
							}
						}
						int num = checked(++UILib.NumberOfUse);
						try
						{
							if (AxolonLicense.LicenseManager.License.IsTrial && UILib.IsConnectedToInternet())
							{
								string osName = Environment.OSVersion.ToString();
								string computerName = UILib.GetComputerName();
								string iP = UILib.GetIP();
								Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
								if (!UILib.IsTrialRegistered())
								{
									if (softReg.RegisterTrialSummary2("AF308J3N834JG8258NALPFE", "5", AxolonLicense.LicenseManager.License.Key, iP, computerName, osName, "T", "", "") == "")
									{
										Application.UserAppDataRegistry.SetValue("IsTrialRegistered", true);
									}
								}
								else if (Math.IEEERemainder(num, 10.0) == 0.0)
								{
									softReg.RegisterTrialSummary2("AF308J3N834JG8258NALPFE", "5", AxolonLicense.LicenseManager.License.Key, iP, computerName, osName, "U", num.ToString() + " using", "");
								}
							}
						}
						catch (Exception)
						{
						}
						Application.DoEvents();
						isLoadingFinished = true;
						Global.HomePageLoaded = true;
					}
					finally
					{
						PublicFunctions.EndWaiting(this);
						Activate();
						Focus();
					}
				}
				goto end_IL_0004;
				IL_0101:
				if (AxolonLicense.LicenseManager.IsTrialKey || AxolonLicense.LicenseManager.IsActivated || !AxolonLicense.LicenseManager.IsTrialExpired)
				{
					goto IL_0169;
				}
				ErrorHelper.InformationMessage("The copy of Axolon you are using is not activated. You must activate the product to continue.");
				RegisterForm registerForm = new RegisterForm();
				registerForm.IsUpgrade = false;
				registerForm.ShowDialog(this);
				if (AxolonLicense.LicenseManager.CheckLicense() && AxolonLicense.LicenseManager.IsActivated)
				{
					goto IL_0169;
				}
				Application.Exit();
				end_IL_0004:;
			}
		}

		private void RegisterTrial()
		{
			try
			{
				int num = checked(++UILib.NumberOfUse);
				if (AxolonLicense.LicenseManager.License.IsTrial && UILib.IsConnectedToInternet())
				{
					string osName = Environment.OSVersion.ToString();
					string computerName = UILib.GetComputerName();
					string iP = UILib.GetIP();
					Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
					if (!UILib.IsTrialRegistered())
					{
						if (softReg.RegisterTrialSummary2("AF308J3N834JG8258NALPFE", Global.ProductID.ToString(), AxolonLicense.LicenseManager.License.Key, iP, computerName, osName, "T", "", "") == "")
						{
							Application.UserAppDataRegistry.SetValue("IsTrialRegistered", true);
						}
					}
					else if (Math.IEEERemainder(num, 10.0) == 0.0)
					{
						softReg.RegisterTrialSummary2("AF308J3N834JG8258NALPFE", "2", AxolonLicense.LicenseManager.License.Key, iP, computerName, osName, "U", num.ToString() + " using", "");
					}
				}
			}
			catch (Exception)
			{
			}
		}

		private bool OpenPreviousCompany()
		{
			if (!Global.OpenPreviousCompany)
			{
				return false;
			}
			using (DatabaseLoginForm databaseLoginForm = new DatabaseLoginForm())
			{
				databaseLoginForm.Notify = false;
				databaseLoginForm.SuppressMessage = true;
				databaseLoginForm.ValidateForm = false;
				return databaseLoginForm.OpenPreviousCompany();
			}
		}

		private void menuItemFileNewCompany_Click(object sender, EventArgs e)
		{
			if (!CreateNewCompany())
			{
				OpenCompany();
			}
		}

		private bool CreateNewCompany()
		{
			if (Global.ConStatus != ConnectionStatus.SQLConnected && Global.ConStatus == ConnectionStatus.Connected && ErrorHelper.QuestionMessageYesNo("You must log out from the current company before proceeding.", "Do you want to continue?") == DialogResult.No)
			{
				return false;
			}
			if (CloseCompany(isExiting: false) && new NewCompanyForm().ShowDialog() == DialogResult.OK)
			{
				PrepareAfterLogin();
				return true;
			}
			return false;
		}

		private bool AttachDatabase()
		{
			if (Global.ConStatus != ConnectionStatus.SQLConnected && Global.ConStatus == ConnectionStatus.Connected && ErrorHelper.QuestionMessageYesNo("You must log out from the current company before proceeding.", "Do you want to continue?") == DialogResult.No)
			{
				return false;
			}
			using (DatabaseAttachmentForm databaseAttachmentForm = new DatabaseAttachmentForm())
			{
				return databaseAttachmentForm.ShowDialog() == DialogResult.OK;
			}
		}

		private bool RestoreDatabase()
		{
			if (Global.ConStatus != ConnectionStatus.SQLConnected && Global.ConStatus == ConnectionStatus.Connected && ErrorHelper.QuestionMessageYesNo("You must log out from the current company before proceeding.", "Do you want to continue?") == DialogResult.No)
			{
				return false;
			}
			return new RestoreDatabaseForm().ShowDialog() == DialogResult.OK;
		}

		private void menuItemReportCustomersAndRecevables_Click(object sender, EventArgs e)
		{
		}

		private void menuItem6_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog(this);
		}

		private void menuItemCustomerNewItem_Click(object sender, EventArgs e)
		{
		}

		private void menuItemVendorsNewItem_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyNewShippingMethod_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFileOpenCompany_Click(object sender, EventArgs e)
		{
			OpenCompany();
		}

		private void OpenCompany()
		{
			if (CloseCompany(isExiting: false))
			{
				EnableDisableFunctions();
				using (DatabaseLoginForm databaseLoginForm = new DatabaseLoginForm())
				{
					if (databaseLoginForm.ShowDialog() == DialogResult.OK)
					{
						GoHome();
					}
				}
				EnableDisableFunctions();
			}
		}

		private void menuItemToolsCalculator_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("calc");
			}
			catch (Win32Exception ex)
			{
				ErrorHelper.WarningMessage("Unable to find the calculator (calc.exe).", "Please make sure 'calc.exe' exists in the windows system direcotry.", ex.Message);
			}
		}

		private void menuItemCompanyShippingMethodList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyUnitsList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemEmployeeNewEmployee_Click(object sender, EventArgs e)
		{
		}

		private void menuItemNewItem_Click(object sender, EventArgs e)
		{
		}

		private void menuItemNewItemCategory_Click(object sender, EventArgs e)
		{
		}

		private void menuItemInventoryInventoryAdjustment_Click(object sender, EventArgs e)
		{
		}

		private void menuItemBankingNewBankAccount_Click(object sender, EventArgs e)
		{
		}

		private void menuItemBankingMakeJournalEntry_Click(object sender, EventArgs e)
		{
		}

		private void menuItemBankingBankList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemAccountsNewAccount_Click(object sender, EventArgs e)
		{
		}

		private void menuItemPrintSetup_Click(object sender, EventArgs e)
		{
			pageSetupDialog.AllowMargins = true;
			pageSetupDialog.AllowOrientation = true;
			pageSetupDialog.AllowPaper = true;
			pageSetupDialog.AllowPrinter = true;
			pageSetupDialog.ShowNetwork = true;
			pageSetupDialog.ShowHelp = false;
			pageSetupDialog.Document = Global.printDocument;
			try
			{
				if (pageSetupDialog.ShowDialog() == DialogResult.OK)
				{
					Global.printDocument = pageSetupDialog.Document;
					Global.RegistryPrinterMargin = pageSetupDialog.PageSettings.Margins;
					Global.RegistryPrinterIsLandscape = pageSetupDialog.PageSettings.Landscape;
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void ToolsDatabaseTasksChangeUser_Click(object sender, EventArgs e)
		{
		}

		private void ToolsDatabaseTasksAttachDatabase_Click(object sender, EventArgs e)
		{
			AttachDatabase();
		}

		private void ToolsDatabaseTasksConnectionStatus_Click(object sender, EventArgs e)
		{
		}

		private void ToolsDatabaseTasksDetachDatabase_Click(object sender, EventArgs e)
		{
			using (DatabaseDetachmentForm databaseDetachmentForm = new DatabaseDetachmentForm())
			{
				databaseDetachmentForm.ShowDialog();
			}
		}

		private void menuItemFileExit_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Do you want to exit?") != DialogResult.No)
			{
				Close();
			}
		}

		private void formPOSMain_Closing(object sender, CancelEventArgs e)
		{
			if (SuppressCloseMessage || Global.ConStatus != 0)
			{
				if (!CloseCompany(isExiting: true))
				{
					e.Cancel = true;
					return;
				}
				if (updateExecutableFileName != null && updateExecutableFileName != string.Empty)
				{
					PublicFunctions.StartProcess(updateExecutableFileName);
				}
				DoPostQuit();
			}
			else if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to exit?") == DialogResult.No)
			{
				e.Cancel = true;
			}
			else if (!CloseCompany(isExiting: true))
			{
				e.Cancel = true;
			}
			else
			{
				DoPostQuit();
			}
		}

		private bool DoPostQuit()
		{
			SaveRegistry();
			Factory.DisconnectDB();
			try
			{
				if (updateDirectoryPath != null && updateDirectoryPath != string.Empty)
				{
					PublicFunctions.StartProcess(Application.StartupPath + Path.DirectorySeparatorChar.ToString() + "Interceder.exe", "copy", updateDirectoryPath.Replace(" ", "?"), Application.StartupPath.Replace(" ", "?"), Process.GetCurrentProcess().Id.ToString());
				}
			}
			catch
			{
			}
			return true;
		}

		private void buttonBackMenu_Click(object sender, EventArgs e)
		{
			ArrayList backList = Navigator.GetBackList();
			contextMenu.MenuItems.Clear();
			checked
			{
				for (int num = backList.Count - 1; num >= 0; num--)
				{
					string text = (string)backList[num];
					if (text.Trim() != string.Empty)
					{
						MenuItem menuItem = new MenuItem();
						menuItem.Text = text;
						menuItem.Click += OnMenuBackClicked;
						contextMenu.MenuItems.Add(0, menuItem);
					}
				}
			}
		}

		private void OnMenuBackClicked(object o, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)o;
			if (menuItem == null)
			{
				return;
			}
			int num = checked(menuItem.Index + 1);
			if (num > 0)
			{
				Form item = null;
				Navigator.Back(out item, num);
				if (item != null)
				{
					isBackForwardButtonPressed = true;
					FormActivator.BringFormToFront(item);
					isBackForwardButtonPressed = false;
				}
			}
		}

		private void OnMenuForwardClicked(object o, EventArgs e)
		{
			MenuItem menuItem = (MenuItem)o;
			if (menuItem == null)
			{
				return;
			}
			int num = checked(menuItem.Index + 1);
			if (num > 0)
			{
				Form item = null;
				Navigator.Forward(out item, num);
				if (item != null)
				{
					isBackForwardButtonPressed = true;
					FormActivator.BringFormToFront(item);
					isBackForwardButtonPressed = false;
				}
			}
		}

		private void buttonForwardMenu_Click(object sender, EventArgs e)
		{
			ArrayList forwardList = Navigator.GetForwardList();
			contextMenu.MenuItems.Clear();
			checked
			{
				for (int num = forwardList.Count - 1; num >= 0; num--)
				{
					string text = (string)forwardList[num];
					if (text.Trim() != string.Empty)
					{
						MenuItem menuItem = new MenuItem();
						menuItem.Text = text;
						menuItem.Click += OnMenuForwardClicked;
						contextMenu.MenuItems.Add(0, menuItem);
					}
				}
			}
		}

		private void ToolsDatabaseTasksBackupDatabase_Click(object sender, EventArgs e)
		{
			new BackupDatabaseForm().ShowDialog();
		}

		private void FileBackupCompany_Click(object sender, EventArgs e)
		{
			new BackupDatabaseForm().ShowDialog();
		}

		private void menuItem30_Click(object sender, EventArgs e)
		{
			RestoreDatabase();
		}

		private void menuItemFileDataUtilitiesRestore_Click(object sender, EventArgs e)
		{
			RestoreDatabase();
		}

		private void FileRestore_Click(object sender, EventArgs e)
		{
			RestoreDatabase();
		}

		private void menuItemViewRefresh_Click(object sender, EventArgs e)
		{
			try
			{
				((IDataForm)base.ActiveMdiChild).OnActivated();
			}
			catch
			{
			}
		}

		private void panelToolbar_Paint(object sender, PaintEventArgs e)
		{
		}

		public bool CanCloseChildren()
		{
			bool result = true;
			checked
			{
				try
				{
					for (int i = 0; i < Application.OpenForms.Count; i++)
					{
						Form form = Application.OpenForms[i];
						if (!(form.Name == "formPOSMain") && !(form.Name == "formPOSHome"))
						{
							form.BringToFront();
							form.Close();
							if (!form.IsDisposed)
							{
								return false;
							}
							i--;
						}
					}
					for (int j = 0; j < Application.OpenForms.Count; j++)
					{
						Form form2 = Application.OpenForms[j];
						if (!(form2.Name != "formPOSHome"))
						{
							form2.Close();
							if (!form2.IsDisposed)
							{
								return false;
							}
						}
					}
					return result;
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
					return result;
				}
			}
		}

		private bool CloseCompany(bool isExiting)
		{
			try
			{
				isClosing = true;
				PublicFunctions.StartWaiting(this);
				if (!CanCloseChildren())
				{
					return false;
				}
				UILib.LogoutCurrentCompany();
				EnableDisableFunctions();
				if (!isExiting)
				{
					Reset(isClosing: true);
				}
				else
				{
					SaveRegistry();
				}
			}
			catch
			{
				return true;
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
				isClosing = false;
			}
			return true;
		}

		private void menuItemFileCloseCompany_Click(object sender, EventArgs e)
		{
			if (CloseCompany(isExiting: false))
			{
				Login();
			}
		}

		private void ShowHelp()
		{
			try
			{
				Process.Start(Application.StartupPath + "\\Axolon Help.chm");
			}
			catch (Win32Exception)
			{
				ErrorHelper.WarningMessage("The help file cannot be located.");
			}
			catch (Exception ex2)
			{
				ErrorHelper.WarningMessage(ex2);
			}
		}

		private void menuItemHelpContentAndIndex_Click(object sender, EventArgs e)
		{
			ShowHelp();
		}

		private void buttonHelp_Click(object sender, EventArgs e)
		{
			GoHelp();
		}

		private void buttonStoreCenter_Click(object sender, EventArgs e)
		{
		}

		private void buttonCompany_Click(object sender, EventArgs e)
		{
		}

		private void OnMessageMouseEntered(object sender, EventArgs e)
		{
		}

		private void labelStatusMessage_Click(object sender, EventArgs e)
		{
			SendKeys.Send("{F1}");
		}

		private void menuItemBankList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyBackupDatabase_Click(object sender, EventArgs e)
		{
			new BackupDatabaseForm().ShowDialog();
		}

		private void menuItemCompanyRestoreDatabase_Click(object sender, EventArgs e)
		{
			RestoreDatabase();
		}

		private void menuItem25_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyTasksSwitchUser_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFileAddDatabase_Click(object sender, EventArgs e)
		{
			new DatabaseAttachmentForm().ShowDialog();
		}

		private void menuItemFileRemoveDatabase_Click(object sender, EventArgs e)
		{
			new DatabaseDetachmentForm().ShowDialog();
		}

		private void pictureBoxHome_Click(object sender, EventArgs e)
		{
			GoHome();
		}

		private void pictureBoxRefresh_Click(object sender, EventArgs e)
		{
			RefreshChild();
		}

		private void pictureBoxHelp_Click(object sender, EventArgs e)
		{
			ShowHelp();
		}

		private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void linkLabelRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			RefreshChild();
		}

		private void menuItemWindowsAddressBook_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("wab.exe");
			}
			catch (Win32Exception ex)
			{
				ErrorHelper.WarningMessage("Unable to locate the windows address book (wab.exe).", "Please make sure 'wab.exe' exists in the windows system direcotry.", ex.Message);
			}
		}

		private void linkLabelReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void menuItemHelpSupport_Click(object sender, EventArgs e)
		{
		}

		private void linkLabelPOS_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void pictureBoxPOS_MouseEnter(object sender, EventArgs e)
		{
		}

		private void pictureBoxPOS_MouseLeave(object sender, EventArgs e)
		{
		}

		private void linkLabelPOS_MouseEnter(object sender, EventArgs e)
		{
		}

		private void linkLabelPOS_MouseLeave(object sender, EventArgs e)
		{
		}

		private void menuItemToolsNotes_Click(object sender, EventArgs e)
		{
		}

		private void menuItemToolsUpgradeDatabase_Click(object sender, EventArgs e)
		{
			using (UpgradeDatabaseForm upgradeDatabaseForm = new UpgradeDatabaseForm())
			{
				upgradeDatabaseForm.ShowDialog();
			}
		}

		private void menuItemToolsUpdateDatabase_Click(object sender, EventArgs e)
		{
			using (UpgradeDatabaseForm upgradeDatabaseForm = new UpgradeDatabaseForm())
			{
				upgradeDatabaseForm.ShowDialog();
			}
		}

		private void formPOSMain_Enter(object sender, EventArgs e)
		{
		}

		private void timer_Tick(object sender, EventArgs e)
		{
		}

		private void menuItemFileSaveAsHTML_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.HTML);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemFileSaveAsText_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.Text);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemFileSaveAsXML_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.XML);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemFileSaveAsPDF_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.PDF);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemFileSaveAsMSWord_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.MSWord);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemCompanyCurrencyList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFileSaveAsPDFFormat_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.PDF);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemAccountsBankList_Click(object sender, EventArgs e)
		{
		}

		private void pictureBoxNote_Click(object sender, EventArgs e)
		{
			int screenID = UIRefelector.GetScreenID(base.ActiveMdiChild);
			if (screenID != -1)
			{
				using (ScreenNoteForm screenNoteForm = new ScreenNoteForm())
				{
					Cursor = Cursors.WaitCursor;
					screenNoteForm.LoadNote(screenID);
					if (base.ActiveMdiChild != null)
					{
						screenNoteForm.HeaderText = base.ActiveMdiChild.Text;
					}
					Cursor = Cursors.Default;
					screenNoteForm.ShowDialog();
				}
			}
		}

		private void menuItemInventoryManufacturerList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemInventoryColorList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemInventoryModelsList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemDepartmentList_Click(object sender, EventArgs e)
		{
		}

		private void GetNotes()
		{
			lock (UIGlobal.workerThreadSycRoot)
			{
				if (Global.ConStatus == ConnectionStatus.Connected)
				{
					DataSet dataSet = null;
					if (noteTimer.Interval != 30000)
					{
						noteTimer.Interval = 30000;
					}
					try
					{
						if (Factory.WorkerNoteSystem.GetReminderNoteCount(Global.CurrentUserID, DateTime.Now) > 0 && !Global.IsNotifierVisible)
						{
							foreach (DataRow row in Factory.WorkerNoteSystem.GetReminderNote(Global.CurrentUserID, DateTime.Now).Tables["Notes"].Rows)
							{
								int num = int.Parse(row["NoteID"].ToString());
								if (!Global.AlarmedNotes.Contains(num))
								{
									dataSet = Factory.WorkerNoteSystem.GetNotesByFields(num, "Notes.NoteText");
									if (dataSet != null)
									{
										string text = dataSet.Tables["Notes"].Rows[0]["NoteText"].ToString();
										Note note = new Note();
										note.TextControl.LoadRTFFormatText(text);
										text = note.PlainText;
										if (text.Trim() == "")
										{
											text = "Untitled Note";
										}
										PlayReminderSound();
										Notifier notifier = new Notifier();
										notifier.ShowReminder("     Reminder", text.Trim());
										notifier.Tag = num;
										notifier.CloseClicked += notifier_CloseClicked;
										notifier.ContentClicked += notifier_ContentClicked;
										break;
									}
								}
							}
						}
					}
					catch
					{
					}
					finally
					{
						if (dataSet != null)
						{
							dataSet.Dispose();
							dataSet = null;
						}
					}
				}
			}
		}

		private void noteTimer_Tick(object sender, EventArgs e)
		{
			if (Global.CurrentUserID != -1 && Global.ConStatus == ConnectionStatus.Connected)
			{
				Thread thread = new Thread(GetNotes);
				thread.TrySetApartmentState(ApartmentState.STA);
				thread.IsBackground = true;
				thread.Start();
			}
		}

		private void PlayReminderSound()
		{
			try
			{
				PublicFunctions.PlayAlarmSound();
			}
			catch
			{
			}
		}

		private void pictureBoxNewNotes_Click(object sender, EventArgs e)
		{
		}

		private void formPOSMain_Activated(object sender, EventArgs e)
		{
		}

		private void menuItemInventoryPriceLevelList_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyCreditCards_Click(object sender, EventArgs e)
		{
		}

		private void menuItemCompanyTerms_Click(object sender, EventArgs e)
		{
		}

		private void menuItemHelpActivate_Click(object sender, EventArgs e)
		{
		}

		private void menuItem58_Click(object sender, EventArgs e)
		{
		}

		private void menuItemHelpSendFeedback_Click(object sender, EventArgs e)
		{
			using (FeedbackForm feedbackForm = new FeedbackForm())
			{
				feedbackForm.ShowDialog();
			}
		}

		private void menuItemToolsCloseAllWindows_Click(object sender, EventArgs e)
		{
			FormActivator.ResetAllForms();
		}

		private void menuItemCompanyPaymentMethods_Click(object sender, EventArgs e)
		{
		}

		private void menuItem59_Click(object sender, EventArgs e)
		{
		}

		private void notifier_CloseClicked(object sender, EventArgs e)
		{
			Notifier notifier = sender as Notifier;
			if (notifier != null)
			{
				int num = int.Parse(notifier.Tag.ToString());
				if (!Global.AlarmedNotes.Contains(num))
				{
					Global.AlarmedNotes.Add(num);
				}
			}
		}

		private void notifier_ContentClicked(object sender, EventArgs e)
		{
			Notifier notifier = sender as Notifier;
			if (notifier != null)
			{
				int num = int.Parse(notifier.Tag.ToString());
				if (!Global.AlarmedNotes.Contains(num))
				{
					Global.AlarmedNotes.Add(num);
				}
				ShowNote(num);
				notifier.Hide();
				try
				{
					PublicFunctions.StartWaiting(this);
					Factory.NoteSystem.SetAlarm(num, isAlarm: false);
				}
				catch (SqlException ex)
				{
					ErrorHelper.ProcessError(ex);
				}
				catch (Exception e2)
				{
					ErrorHelper.ProcessError(e2);
				}
				finally
				{
					PublicFunctions.EndWaiting(this);
				}
			}
		}

		private void ShowNote(int id)
		{
			try
			{
				if (UIGlobal.OpenNoteForms.ContainsKey(id))
				{
					NoteViewForm noteViewForm = (NoteViewForm)UIGlobal.OpenNoteForms[id];
					noteViewForm.BringToFront();
					noteViewForm.Focus();
				}
				else
				{
					NoteViewForm noteViewForm = new NoteViewForm();
					noteViewForm.Closed += form_Closed;
					noteViewForm.LoadNote(id);
					noteViewForm.NoteID = id;
					noteViewForm.Show();
					UIGlobal.OpenNoteForms.Add(id, noteViewForm);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void form_Closed(object sender, EventArgs e)
		{
			NoteViewForm noteViewForm = sender as NoteViewForm;
			if (noteViewForm != null)
			{
				UIGlobal.OpenNoteForms.Remove(noteViewForm.NoteID);
			}
		}

		private void menuItemFileLoadPrintTemplate_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFile_Click(object sender, EventArgs e)
		{
		}

		private void menuItemFileDataUtilitiesBackup_Click(object sender, EventArgs e)
		{
			using (BackupDatabaseForm backupDatabaseForm = new BackupDatabaseForm())
			{
				backupDatabaseForm.ShowDialog();
			}
		}

		private void menuItemFileDataUtilitiesAttach_Click(object sender, EventArgs e)
		{
			using (DatabaseAttachmentForm databaseAttachmentForm = new DatabaseAttachmentForm())
			{
				databaseAttachmentForm.ShowDialog();
			}
		}

		private void menuItemFileDataUtilitiesDetach_Click(object sender, EventArgs e)
		{
			using (DatabaseDetachmentForm databaseDetachmentForm = new DatabaseDetachmentForm())
			{
				databaseDetachmentForm.ShowDialog();
			}
		}

		private void menuItemFileDataUtilitiesSettings_Click(object sender, EventArgs e)
		{
			using (ConnectionSettingsDialog connectionSettingsDialog = new ConnectionSettingsDialog())
			{
				connectionSettingsDialog.ShowDialog();
			}
		}

		private void menuItemFileSendToMicrosoftExcel_Click(object sender, EventArgs e)
		{
			try
			{
				PublicFunctions.StartWaiting(this);
				(base.ActiveMdiChild as IExternalReport)?.SaveReport(ExternalReportTypes.MSExcel);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemToolsTranslate_Click(object sender, EventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to translate?") == DialogResult.Yes)
			{
				PublicFunctions.StartWaiting(this);
				Translator translator = new Translator(isRead: false);
				translator.OverwriteExistingKey = true;
				translator.Translate();
				PublicFunctions.EndWaiting(this);
			}
		}

		private void menuItemTestForm_Click(object sender, EventArgs e)
		{
		}

		private void menuItem58_Click_1(object sender, EventArgs e)
		{
			new Test2().Show();
		}

		private void linkLabelHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ShowHelp();
		}

		private void linkLabelHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			GoHome();
		}

		private void customerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerDetailsFormObj);
		}

		private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ComboDataHelper.ResetToRefreshAll();
		}

		private void itemToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductDetailsFormObj);
		}

		private void itemListToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductListFormObj);
		}

		private void rgbiSkins_Gallery_InitDropDownGallery(object sender, InplaceGalleryEventArgs e)
		{
		}

		private void rgbiSkins_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
		{
		}

		private void rgbiFont_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
		{
		}

		private void rgbiFontColor_Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
		{
		}

		private void gddFontColor_Gallery_CustomDrawItemImage(object sender, GalleryItemCustomDrawEventArgs e)
		{
		}

		private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("Blue");
		}

		private void SetSking(string skinName)
		{
			barAndDockingController.LookAndFeel.SkinName = skinName;
			_ = Environment.OSVersion.Version.Major;
			_ = 6;
			RegistryHelper registryHelper = new RegistryHelper();
			registryHelper.SetValue(registryHelper.CurrentWindowsUserKey, "Skin", skinName);
		}

		private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("Caramel");
		}

		private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("Black");
		}

		private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("iMaginary");
		}

		private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("Money Twins");
		}

		private void barButtonItem34_ItemClick(object sender, ItemClickEventArgs e)
		{
			SetSking("Lilian");
		}

		private void f_Popup(object sender, EventArgs e)
		{
		}

		private void ribbonControl2_SelectedPageChanged(object sender, EventArgs e)
		{
		}

		private void loginToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Login();
		}

		private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CloseCompany(isExiting: false))
			{
				Login();
			}
		}

		private void LoadShortcuts()
		{
			try
			{
				foreach (DataRow row in Factory.ShortcutSystem.GetShortcuts(1, Global.CurrentUser).Tables[0].Rows)
				{
					new GalleryItem
					{
						Tag = row["ShortcutKey"].ToString(),
						Caption = row["ShortcutText"].ToString(),
						Hint = row["ShortcutText"].ToString(),
						Image = imageCollection1.Images[0]
					};
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void Gallery_ItemClick(object sender, GalleryItemClickEventArgs e)
		{
			FormActivator.BringFormToFront(e.Item.Tag.ToString());
		}

		private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (ErrorHelper.QuestionMessageYesNo("Do you want to exit?") != DialogResult.No)
			{
				Application.Exit();
			}
		}

		private void barButtonItem6_ItemClick_1(object sender, ItemClickEventArgs e)
		{
			try
			{
				Process.Start("calc.exe");
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void chartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new GeneralReports().ShowAccountListReport();
		}

		private void openOrdersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new GeneralReports().ShowOpenSalesOrderListReport();
		}

		private void openPurchaseOrdersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new GeneralReports().ShowOpenPurchaseOrderListReport();
		}

		private void openShipmentsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new GeneralReports().ShowOpenShipmentsListReport();
		}

		private void vendorStatementToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		private void usersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.UserDetailsFormObj);
		}

		private void userGroupsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.UserGroupDetailsFormObj);
		}

		private void malAccountGroup_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.AccountGroup);
		}

		private void malCostCenter_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CostCenter);
		}

		private void malRegister_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Register);
		}

		private void malAnalysis_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Analysis);
		}

		private void malAnalysisGroup_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.AnalysisGroup);
		}

		private void malBank_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Bank);
		}

		private void malCurrency_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Currency);
		}

		private void malPaymentMethod_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.PaymentMethod);
		}

		private void malPaymentTerm_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.PaymentTerm);
		}

		private void malChequebook_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Chequebook);
		}

		private void malReturnedChequeReason_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ReturnedChequeReason);
		}

		private void mclCustomer_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.CustomerListFormObj);
		}

		private void mclCustomerClass_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomerClass);
		}

		private void mclCustomerGroup_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomerGroup);
		}

		private void mclCustomerAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CustomerAddress);
		}

		private void mclSalesperson_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Salesperson);
		}

		private void mclContact_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Contact);
		}

		private void mclCountry_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Country);
		}

		private void mclArea_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Area);
		}

		private void mclShippingMethod_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ShippingMethod);
		}

		private void mvlVendorClass_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.VendorClass);
		}

		private void mvlVendorGroup_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.VendorGroup);
		}

		private void mvlVendorAddress_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.VendorAddress);
		}

		private void mvlBuyer_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Buyer);
		}

		private void mvlContact_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Contact);
		}

		private void milItem_Click(object sender, EventArgs e)
		{
			FormActivator.BringFormToFront(FormActivator.ProductListFormObj);
		}

		private void milItemClass_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductClass);
		}

		private void milCategory_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductCategory);
		}

		private void milBrand_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductBrand);
		}

		private void milManufacturer_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductManufacturer);
		}

		private void milStyle_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.ProductStyle);
		}

		private void milUOM_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Unit);
		}

		private void milDriver_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Driver);
		}

		private void toolStripMenuItem41_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Employee);
		}

		private void toolStripMenuItem42_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Grade);
		}

		private void mhrlEmployeeDocs_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EmployeeDocument);
		}

		private void mhrlDegree_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Degree);
		}

		private void mhrlDivision_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Division);
		}

		private void mhrlDepartment_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Department);
		}

		private void mhrlPosition_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Position);
		}

		private void mhrlSkill_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Skill);
		}

		private void mhrlEmployeeGroup_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.EmployeeGroup);
		}

		private void mhrlSponsor_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Sponsor);
		}

		private void mhrlNationality_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Nationality);
		}

		private void mhrlReligion_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Religion);
		}

		private void mhrlCompanyDocs_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.CompanyDocument);
		}

		private void mhrlTenancyContract_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.TenancyContract);
		}

		private void mhrlTradeLicense_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.TradeLicense);
		}

		private void mhrlVisa_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Visa);
		}

		private void mhRegister_Click(object sender, EventArgs e)
		{
			RegisterForm registerForm = new RegisterForm();
			registerForm.IsUpgrade = false;
			registerForm.ShowDialog();
		}

		private void upgradeProductToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RegisterForm registerForm = new RegisterForm();
			registerForm.IsUpgrade = true;
			registerForm.ShowDialog();
		}
	}
}
