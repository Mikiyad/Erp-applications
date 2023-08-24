using Infragistics.Win;
using Infragistics.Win.UltraWinProgressBar;
using Infragistics.Win.UltraWinTabControl;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.SoftReg;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class RegisterForm : Form
	{
		public bool IsUpgrade = true;

		private IContainer components;

		private Label label1;

		private Label label3;

		private Button buttonOK;

		private Button buttonClose;

		private UltraTabControl ultraTabControl1;

		private UltraTabSharedControlsPage ultraTabSharedControlsPage1;

		private UltraTabPageControl ultraTabPageControl1;

		private UltraTabPageControl ultraTabPageControl2;

		private MaskedTextBox textBoxProductKey;

		private Button buttonActivateOnline;

		private Button buttonEvaluate;

		private Label label7;

		private UltraTabPageControl ultraTabPageControl3;

		private MaskedTextBox textBoxSystemKey;

		private Label label6;

		private MaskedTextBox textBoxProductKey2;

		private Label label5;

		private Label label4;

		private Label label2;

		private TextBox textBoxActivationCode;

		private UltraTabPageControl ultraTabPageControl4;

		private UltraProgressBar progressBar1;

		private Label label9;

		private Label label8;

		private Label label11;

		private Button buttonActivateByPhone;

		private Label label10;

		private Button buttonOnlineActivateNow;

		private Label label12;

		private UltraTabPageControl ultraTabPageControl5;

		private Label label13;

		private Label label14;

		private Button button4;

		private Button button3;

		private Button buttonPhoneActivationActivate;

		private Button buttonDone;

		private Button buttonActivateLater;

		private Label label15;

		private TextBox textBoxCompanyName;

		private Label label19;

		private TextBox textBoxTelephone;

		private Label label18;

		private TextBox textBoxEmail;

		private Label label17;

		private TextBox textBoxLastName;

		private Label label16;

		private TextBox textBoxFirstName;

		private Label label20;

		private Label label21;

		private Label label23;

		private TextBox textBoxCity;

		private Label label22;

		private TextBox textBoxCountry;

		private Button buttonOnlineActivateByPhone;

		private UltraTabPageControl ultraTabPageControl6;

		private UltraTabPageControl ultraTabPageControl7;

		private Label label33;

		private Button buttonRegisterLater;

		private Label label24;

		private TextBox textBoxRegCity;

		private Label label25;

		private TextBox textBoxRegCountry;

		private Label label26;

		private Label label27;

		private TextBox textBoxRegTelephone;

		private Label label28;

		private TextBox textBoxRegEmail;

		private Label label29;

		private TextBox textBoxRegLastName;

		private Label label30;

		private TextBox textBoxRegFirstName;

		private Label label31;

		private TextBox textBoxRegCompanyName;

		private Button buttonRegisterNow;

		private UltraProgressBar progressBar2;

		private Label label32;

		private Label label34;

		private Button buttonRegisterDone;

		public RegisterForm()
		{
			InitializeComponent();
			base.Load += RegisterForm_Load;
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void RegisterForm_Load(object sender, EventArgs e)
		{
			try
			{
				if (!IsUpgrade)
				{
					if (AxolonLicense.LicenseManager.CheckLicense() && !AxolonLicense.LicenseManager.IsTrialKey)
					{
						ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[1];
						textBoxProductKey2.Text = AxolonLicense.LicenseManager.License.Key;
						textBoxSystemKey.Text = AxolonLicense.LicenseManager.GetSystemBoundKey();
					}
					if (AxolonLicense.LicenseManager.IsActivated)
					{
						ultraTabControl1.Tabs["ActivateDone"].Selected = true;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (ultraTabControl1.SelectedTab == ultraTabControl1.Tabs[0])
				{
					SetProductKey();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private bool SetProductKey()
		{
			try
			{
				string key = textBoxProductKey.Text.Replace("-", "");
				if (!AxolonLicense.LicenseManager.SetKey(key))
				{
					ErrorHelper.WarningMessage("Checking.. Please reopen the system..");
					return false;
				}
				if (AxolonLicense.LicenseManager.License.ProductID != Global.ProductID || AxolonLicense.LicenseManager.License.VersionMajor != Global.MajorVersion)
				{
					ErrorHelper.WarningMessage("Checking.. Please reopen the system.");
					return false;
				}
				AxolonLicense.LicenseManager.WriteLicense();
				textBoxProductKey2.Text = AxolonLicense.LicenseManager.License.Key;
				textBoxSystemKey.Text = AxolonLicense.LicenseManager.GetSystemBoundKey();
				if (AxolonLicense.LicenseManager.IsTrialKey)
				{
					ErrorHelper.InformationMessage("The key you have entered is an evaluation key. You will continue evaluating the product now.");
					if (!UILib.IsTrialRegistered())
					{
						ultraTabControl1.Tabs["RegisterEvaluation"].Selected = true;
						return true;
					}
					Close();
				}
				ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[1];
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e, "Error registering the product key. Please contact your system administrator.");
				return false;
			}
		}

		private void buttonActivateOnline_Click(object sender, EventArgs e)
		{
			if (AxolonLicense.LicenseManager.IsTrialKey)
			{
				MessageBox.Show("Evaluation key cannot be activated. Please purchase the product and enter the purchased copy product key.", "Invalid Key", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else
			{
				ultraTabControl1.Tabs["ActivateOnline"].Selected = true;
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ultraTabControl1.SelectedTab = ultraTabControl1.Tabs[0];
		}

		private void ultraTabControl1_SelectedTabChanged(object sender, SelectedTabChangedEventArgs e)
		{
			if (e.Tab.Index == 0)
			{
				buttonActivateOnline.Visible = false;
				buttonOK.Text = "&OK";
			}
			else
			{
				buttonActivateOnline.Visible = true;
				buttonOK.Text = "&Activate";
			}
		}

		private void buttonEvaluate_Click(object sender, EventArgs e)
		{
			string text = "";
			try
			{
				text = File.ReadAllText(Path.GetDirectoryName(Application.ExecutablePath) + "\\trialkey.txt");
			}
			catch
			{
				ErrorHelper.InformationMessage("Evaluation key file is not available.", "Please contact us to get an evaluation key or if you already have a key enter it in the field below.");
				return;
			}
			textBoxProductKey.Text = text;
			SetProductKey();
		}

		private void buttonActivateByPhone_Click(object sender, EventArgs e)
		{
			ultraTabControl1.Tabs["ActivateByPhone"].Selected = true;
		}

		private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
		{
		}

		private void buttonDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonPhoneActivationActivate_Click(object sender, EventArgs e)
		{
			try
			{
				if (AxolonLicense.LicenseManager.IsTrialKey)
				{
					ErrorHelper.InformationMessage("Evaluation key cannot be activated.", "Please purchase the product and enter the purchased copy product key.");
				}
				else if (AxolonLicense.LicenseManager.GetActivationKey(textBoxProductKey2.Text.Replace("-", ""), textBoxSystemKey.Text.Replace("-", "")) == textBoxActivationCode.Text)
				{
					AxolonLicense.LicenseManager.WriteActivation(textBoxActivationCode.Text);
					ultraTabControl1.Tabs["ActivateDone"].Selected = true;
				}
				else
				{
					MessageBox.Show("Checking.. Please reopen the system.", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					Application.Exit();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonOnlineActivateNow_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBoxCompanyName.Text.Trim() == "")
				{
					ErrorHelper.InformationMessage("Please enter the company name.");
					textBoxCompanyName.Focus();
				}
				else
				{
					Cursor = Cursors.WaitCursor;
					Application.DoEvents();
					progressBar1.Visible = true;
					progressBar1.Value = 10;
					Application.DoEvents();
					if (UILib.IsConnectedToInternet())
					{
						progressBar1.Value = 20;
						Application.DoEvents();
						string computerName = UILib.GetComputerName();
						string iP = UILib.GetIP();
						progressBar1.Value = 30;
						Application.DoEvents();
						Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
						progressBar1.Value = 40;
						Application.DoEvents();
						softReg.Timeout = 300000;
						string activationCode = softReg.GetActivationCode("AF308J3N834JG8258NALPFE", Global.ProductID.ToString(), AxolonLicense.LicenseManager.License.Key, AxolonLicense.LicenseManager.GetSystemBoundKey(), iP, computerName, textBoxFirstName.Text, textBoxLastName.Text, textBoxCompanyName.Text, textBoxEmail.Text, textBoxTelephone.Text, textBoxCity.Text, textBoxCountry.Text);
						progressBar1.Value = 90;
						Application.DoEvents();
						int result = 0;
						if (!int.TryParse(activationCode, out result))
						{
							string text = "Unable to activate the product. Please try later or activate by phone/email.";
							if (activationCode.Length > 7)
							{
								text = text + "\n" + activationCode;
							}
							ErrorHelper.ErrorMessage(text);
						}
						else
						{
							switch (result)
							{
							case -2:
								ErrorHelper.ErrorMessage("Unable to activate the product.", "Invalid product key. Please enter a valid product key.");
								break;
							case -1:
								ErrorHelper.ErrorMessage("Unable to activate the product.", "Unauthorized connection to activation server.");
								break;
							case -3:
								ErrorHelper.ErrorMessage("Evaluation keys cannot be activated.", "Please use a purchased copy product key.");
								break;
							case -4:
								ErrorHelper.ErrorMessage("Unable to activate the product key.", "This product key is already used and activated by another user or system.", "Please contact us to get help activating your product or try activate by phone/email.");
								break;
							case -5:
								ErrorHelper.ErrorMessage("Unable to activate the product.", "The product key is not valid for this product. Please use a valid product key.");
								break;
							default:
								if (AxolonLicense.LicenseManager.GetActivationKey(AxolonLicense.LicenseManager.License.Key, AxolonLicense.LicenseManager.GetSystemBoundKey()) == result.ToString())
								{
									AxolonLicense.LicenseManager.WriteActivation(result.ToString());
									ultraTabControl1.Tabs["ActivateDone"].Selected = true;
								}
								break;
							}
						}
					}
					else
					{
						ErrorHelper.WarningMessage("Cannot find an active internet connection.", "Please make sure that you are connected to the internet.");
						progressBar1.Visible = false;
						Application.DoEvents();
					}
				}
			}
			catch (Exception ex)
			{
				ErrorHelper.ProcessError(ex, "Cannot connect to activation server.", "Please try again later.");
			}
			finally
			{
				progressBar1.Visible = false;
				Cursor = Cursors.Default;
				Application.DoEvents();
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonRegisterDone_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonRegisterLater_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonRegisterNow_Click(object sender, EventArgs e)
		{
			try
			{
				progressBar2.Visible = true;
				progressBar2.Value = 10;
				Cursor = Cursors.WaitCursor;
				Application.DoEvents();
				if (UILib.IsConnectedToInternet())
				{
					progressBar2.Value = 20;
					Application.DoEvents();
					string computerName = UILib.GetComputerName();
					string iP = UILib.GetIP();
					progressBar2.Value = 30;
					Application.DoEvents();
					Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
					progressBar2.Value = 40;
					Application.DoEvents();
					string text = "Registeration Failed.";
					softReg.Timeout = 300000;
					text = softReg.RegisterTrialDetail("AF308J3N834JG8258NALPFE", Global.ProductID.ToString(), AxolonLicense.LicenseManager.License.Key, iP, computerName, textBoxRegFirstName.Text, textBoxRegLastName.Text, textBoxRegCompanyName.Text, textBoxRegEmail.Text, textBoxRegTelephone.Text, textBoxRegCity.Text, textBoxRegCountry.Text);
					progressBar2.Value = 70;
					Application.DoEvents();
					if (text == "")
					{
						Application.UserAppDataRegistry.SetValue("IsTrialRegistered", true);
						progressBar2.Value = 100;
						Application.DoEvents();
						ultraTabControl1.Tabs["RegisterDone"].Selected = true;
					}
					else
					{
						ErrorHelper.ErrorMessage("Unable to register the product." + text + "\nPlease try again.");
					}
				}
				else
				{
					ErrorHelper.WarningMessage("Cannot find an active internet connection.", "Please make sure that you are connected to the internet and try again.");
					progressBar2.Visible = false;
					Application.DoEvents();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Cannot connect to registration server. Please try again later.");
			}
			finally
			{
				progressBar1.Visible = false;
				Cursor = Cursors.Default;
				Application.DoEvents();
			}
		}

		private void buttonActivateLater_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			ultraTabControl1.Tabs["ProductKey"].Selected = true;
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
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab2 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab3 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab4 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab5 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab6 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			Infragistics.Win.UltraWinTabControl.UltraTab ultraTab7 = new Infragistics.Win.UltraWinTabControl.UltraTab();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.Main.RegisterForm));
			ultraTabPageControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label7 = new System.Windows.Forms.Label();
			buttonClose = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			buttonEvaluate = new System.Windows.Forms.Button();
			textBoxProductKey = new System.Windows.Forms.MaskedTextBox();
			label3 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			ultraTabPageControl2 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonActivateLater = new System.Windows.Forms.Button();
			label11 = new System.Windows.Forms.Label();
			buttonActivateByPhone = new System.Windows.Forms.Button();
			label10 = new System.Windows.Forms.Label();
			buttonActivateOnline = new System.Windows.Forms.Button();
			label9 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			ultraTabPageControl3 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label21 = new System.Windows.Forms.Label();
			button4 = new System.Windows.Forms.Button();
			button3 = new System.Windows.Forms.Button();
			buttonPhoneActivationActivate = new System.Windows.Forms.Button();
			textBoxSystemKey = new System.Windows.Forms.MaskedTextBox();
			label6 = new System.Windows.Forms.Label();
			textBoxProductKey2 = new System.Windows.Forms.MaskedTextBox();
			label5 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBoxActivationCode = new System.Windows.Forms.TextBox();
			ultraTabPageControl4 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonOnlineActivateByPhone = new System.Windows.Forms.Button();
			label23 = new System.Windows.Forms.Label();
			textBoxCity = new System.Windows.Forms.TextBox();
			label22 = new System.Windows.Forms.Label();
			textBoxCountry = new System.Windows.Forms.TextBox();
			label20 = new System.Windows.Forms.Label();
			label19 = new System.Windows.Forms.Label();
			textBoxTelephone = new System.Windows.Forms.TextBox();
			label18 = new System.Windows.Forms.Label();
			textBoxEmail = new System.Windows.Forms.TextBox();
			label17 = new System.Windows.Forms.Label();
			textBoxLastName = new System.Windows.Forms.TextBox();
			label16 = new System.Windows.Forms.Label();
			textBoxFirstName = new System.Windows.Forms.TextBox();
			label15 = new System.Windows.Forms.Label();
			textBoxCompanyName = new System.Windows.Forms.TextBox();
			label12 = new System.Windows.Forms.Label();
			buttonOnlineActivateNow = new System.Windows.Forms.Button();
			progressBar1 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
			ultraTabPageControl5 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonDone = new System.Windows.Forms.Button();
			label14 = new System.Windows.Forms.Label();
			label13 = new System.Windows.Forms.Label();
			ultraTabPageControl6 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			label33 = new System.Windows.Forms.Label();
			buttonRegisterLater = new System.Windows.Forms.Button();
			label24 = new System.Windows.Forms.Label();
			textBoxRegCity = new System.Windows.Forms.TextBox();
			label25 = new System.Windows.Forms.Label();
			textBoxRegCountry = new System.Windows.Forms.TextBox();
			label26 = new System.Windows.Forms.Label();
			label27 = new System.Windows.Forms.Label();
			textBoxRegTelephone = new System.Windows.Forms.TextBox();
			label28 = new System.Windows.Forms.Label();
			textBoxRegEmail = new System.Windows.Forms.TextBox();
			label29 = new System.Windows.Forms.Label();
			textBoxRegLastName = new System.Windows.Forms.TextBox();
			label30 = new System.Windows.Forms.Label();
			textBoxRegFirstName = new System.Windows.Forms.TextBox();
			label31 = new System.Windows.Forms.Label();
			textBoxRegCompanyName = new System.Windows.Forms.TextBox();
			buttonRegisterNow = new System.Windows.Forms.Button();
			progressBar2 = new Infragistics.Win.UltraWinProgressBar.UltraProgressBar();
			ultraTabPageControl7 = new Infragistics.Win.UltraWinTabControl.UltraTabPageControl();
			buttonRegisterDone = new System.Windows.Forms.Button();
			label32 = new System.Windows.Forms.Label();
			label34 = new System.Windows.Forms.Label();
			ultraTabControl1 = new Infragistics.Win.UltraWinTabControl.UltraTabControl();
			ultraTabSharedControlsPage1 = new Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage();
			ultraTabPageControl1.SuspendLayout();
			ultraTabPageControl2.SuspendLayout();
			ultraTabPageControl3.SuspendLayout();
			ultraTabPageControl4.SuspendLayout();
			ultraTabPageControl5.SuspendLayout();
			ultraTabPageControl6.SuspendLayout();
			ultraTabPageControl7.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).BeginInit();
			ultraTabControl1.SuspendLayout();
			SuspendLayout();
			ultraTabPageControl1.Controls.Add(label7);
			ultraTabPageControl1.Controls.Add(buttonClose);
			ultraTabPageControl1.Controls.Add(buttonOK);
			ultraTabPageControl1.Controls.Add(buttonEvaluate);
			ultraTabPageControl1.Controls.Add(textBoxProductKey);
			ultraTabPageControl1.Controls.Add(label3);
			ultraTabPageControl1.Controls.Add(label1);
			ultraTabPageControl1.Location = new System.Drawing.Point(0, 0);
			ultraTabPageControl1.Name = "ultraTabPageControl1";
			ultraTabPageControl1.Size = new System.Drawing.Size(447, 267);
			label7.Location = new System.Drawing.Point(14, 20);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(437, 19);
			label7.TabIndex = 7;
			label7.Text = "Click 'Evaluate' button to continue evaluating the product.";
			buttonClose.Location = new System.Drawing.Point(346, 227);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(87, 29);
			buttonClose.TabIndex = 3;
			buttonClose.Text = "&Cancel";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			buttonOK.Location = new System.Drawing.Point(253, 227);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(87, 29);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonEvaluate.Location = new System.Drawing.Point(100, 42);
			buttonEvaluate.Name = "buttonEvaluate";
			buttonEvaluate.Size = new System.Drawing.Size(140, 29);
			buttonEvaluate.TabIndex = 6;
			buttonEvaluate.Text = "Evaluate";
			buttonEvaluate.UseVisualStyleBackColor = true;
			buttonEvaluate.Click += new System.EventHandler(buttonEvaluate_Click);
			textBoxProductKey.Location = new System.Drawing.Point(100, 172);
			textBoxProductKey.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
			textBoxProductKey.Name = "textBoxProductKey";
			textBoxProductKey.Size = new System.Drawing.Size(333, 20);
			textBoxProductKey.TabIndex = 5;
			label3.Location = new System.Drawing.Point(14, 118);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(437, 46);
			label3.TabIndex = 4;
			label3.Text = "OR\r\n\r\nPlease enter your product key that is given to you at the time of purchasing:";
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(14, 175);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(80, 13);
			label1.TabIndex = 0;
			label1.Text = "Product Key:";
			ultraTabPageControl2.Controls.Add(buttonActivateLater);
			ultraTabPageControl2.Controls.Add(label11);
			ultraTabPageControl2.Controls.Add(buttonActivateByPhone);
			ultraTabPageControl2.Controls.Add(label10);
			ultraTabPageControl2.Controls.Add(buttonActivateOnline);
			ultraTabPageControl2.Controls.Add(label9);
			ultraTabPageControl2.Controls.Add(label8);
			ultraTabPageControl2.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl2.Name = "ultraTabPageControl2";
			ultraTabPageControl2.Size = new System.Drawing.Size(447, 267);
			buttonActivateLater.Location = new System.Drawing.Point(304, 230);
			buttonActivateLater.Name = "buttonActivateLater";
			buttonActivateLater.Size = new System.Drawing.Size(117, 26);
			buttonActivateLater.TabIndex = 5;
			buttonActivateLater.Text = "Activate Later";
			buttonActivateLater.UseVisualStyleBackColor = true;
			buttonActivateLater.Click += new System.EventHandler(buttonActivateLater_Click);
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(134, 177);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(232, 13);
			label11.TabIndex = 4;
			label11.Text = "Get your activation code via telephone or email.";
			buttonActivateByPhone.Location = new System.Drawing.Point(11, 160);
			buttonActivateByPhone.Name = "buttonActivateByPhone";
			buttonActivateByPhone.Size = new System.Drawing.Size(117, 40);
			buttonActivateByPhone.TabIndex = 3;
			buttonActivateByPhone.Text = "Activate by Phone/Email";
			buttonActivateByPhone.UseVisualStyleBackColor = true;
			buttonActivateByPhone.Click += new System.EventHandler(buttonActivateByPhone_Click);
			label10.Location = new System.Drawing.Point(134, 105);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(287, 30);
			label10.TabIndex = 2;
			label10.Text = "Using internet connection to activate your product. (Recommended)";
			buttonActivateOnline.Location = new System.Drawing.Point(11, 96);
			buttonActivateOnline.Name = "buttonActivateOnline";
			buttonActivateOnline.Size = new System.Drawing.Size(117, 39);
			buttonActivateOnline.TabIndex = 1;
			buttonActivateOnline.Text = "Activate Online";
			buttonActivateOnline.UseVisualStyleBackColor = true;
			buttonActivateOnline.Click += new System.EventHandler(buttonActivateOnline_Click);
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 61);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(361, 13);
			label9.TabIndex = 1;
			label9.Text = "Please select either you want to activate online or activate by phone/email:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(11, 34);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(300, 13);
			label8.TabIndex = 0;
			label8.Text = "To continue using the product you must activate your product.";
			ultraTabPageControl3.Controls.Add(label21);
			ultraTabPageControl3.Controls.Add(button4);
			ultraTabPageControl3.Controls.Add(button3);
			ultraTabPageControl3.Controls.Add(buttonPhoneActivationActivate);
			ultraTabPageControl3.Controls.Add(textBoxSystemKey);
			ultraTabPageControl3.Controls.Add(label6);
			ultraTabPageControl3.Controls.Add(textBoxProductKey2);
			ultraTabPageControl3.Controls.Add(label5);
			ultraTabPageControl3.Controls.Add(label4);
			ultraTabPageControl3.Controls.Add(label2);
			ultraTabPageControl3.Controls.Add(textBoxActivationCode);
			ultraTabPageControl3.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl3.Name = "ultraTabPageControl3";
			ultraTabPageControl3.Size = new System.Drawing.Size(447, 267);
			label21.AutoSize = true;
			label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label21.ForeColor = System.Drawing.Color.Navy;
			label21.Location = new System.Drawing.Point(11, 8);
			label21.Name = "label21";
			label21.Size = new System.Drawing.Size(234, 17);
			label21.TabIndex = 29;
			label21.Text = "Activate by Telephone or Email";
			button4.Location = new System.Drawing.Point(287, 106);
			button4.Name = "button4";
			button4.Size = new System.Drawing.Size(141, 24);
			button4.TabIndex = 21;
			button4.Text = "Change Product Key";
			button4.UseVisualStyleBackColor = true;
			button4.Click += new System.EventHandler(button4_Click);
			button3.Location = new System.Drawing.Point(334, 232);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(94, 24);
			button3.TabIndex = 20;
			button3.Text = "Activate Later";
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(button3_Click);
			buttonPhoneActivationActivate.Location = new System.Drawing.Point(234, 232);
			buttonPhoneActivationActivate.Name = "buttonPhoneActivationActivate";
			buttonPhoneActivationActivate.Size = new System.Drawing.Size(94, 24);
			buttonPhoneActivationActivate.TabIndex = 19;
			buttonPhoneActivationActivate.Text = "Activate";
			buttonPhoneActivationActivate.UseVisualStyleBackColor = true;
			buttonPhoneActivationActivate.Click += new System.EventHandler(buttonPhoneActivationActivate_Click);
			textBoxSystemKey.Location = new System.Drawing.Point(95, 82);
			textBoxSystemKey.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
			textBoxSystemKey.Name = "textBoxSystemKey";
			textBoxSystemKey.ReadOnly = true;
			textBoxSystemKey.Size = new System.Drawing.Size(333, 20);
			textBoxSystemKey.TabIndex = 14;
			label6.AutoSize = true;
			label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label6.Location = new System.Drawing.Point(9, 85);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(76, 13);
			label6.TabIndex = 13;
			label6.Text = "System Key:";
			textBoxProductKey2.Location = new System.Drawing.Point(95, 56);
			textBoxProductKey2.Mask = ">AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA-AAAAA";
			textBoxProductKey2.Name = "textBoxProductKey2";
			textBoxProductKey2.ReadOnly = true;
			textBoxProductKey2.Size = new System.Drawing.Size(333, 20);
			textBoxProductKey2.TabIndex = 12;
			label5.AutoSize = true;
			label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label5.Location = new System.Drawing.Point(9, 59);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(80, 13);
			label5.TabIndex = 11;
			label5.Text = "Product Key:";
			label4.Location = new System.Drawing.Point(12, 145);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(426, 26);
			label4.TabIndex = 16;
			label4.Text = "Enter the activation code that is given to you from us over the phone or email and click 'Activate' button.";
			label2.AutoSize = true;
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 185);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(101, 13);
			label2.TabIndex = 15;
			label2.Text = "Activation Code:";
			textBoxActivationCode.Location = new System.Drawing.Point(119, 182);
			textBoxActivationCode.Name = "textBoxActivationCode";
			textBoxActivationCode.Size = new System.Drawing.Size(168, 20);
			textBoxActivationCode.TabIndex = 17;
			ultraTabPageControl4.Controls.Add(buttonOnlineActivateByPhone);
			ultraTabPageControl4.Controls.Add(label23);
			ultraTabPageControl4.Controls.Add(textBoxCity);
			ultraTabPageControl4.Controls.Add(label22);
			ultraTabPageControl4.Controls.Add(textBoxCountry);
			ultraTabPageControl4.Controls.Add(label20);
			ultraTabPageControl4.Controls.Add(label19);
			ultraTabPageControl4.Controls.Add(textBoxTelephone);
			ultraTabPageControl4.Controls.Add(label18);
			ultraTabPageControl4.Controls.Add(textBoxEmail);
			ultraTabPageControl4.Controls.Add(label17);
			ultraTabPageControl4.Controls.Add(textBoxLastName);
			ultraTabPageControl4.Controls.Add(label16);
			ultraTabPageControl4.Controls.Add(textBoxFirstName);
			ultraTabPageControl4.Controls.Add(label15);
			ultraTabPageControl4.Controls.Add(textBoxCompanyName);
			ultraTabPageControl4.Controls.Add(label12);
			ultraTabPageControl4.Controls.Add(buttonOnlineActivateNow);
			ultraTabPageControl4.Controls.Add(progressBar1);
			ultraTabPageControl4.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl4.Name = "ultraTabPageControl4";
			ultraTabPageControl4.Size = new System.Drawing.Size(447, 267);
			buttonOnlineActivateByPhone.Location = new System.Drawing.Point(327, 228);
			buttonOnlineActivateByPhone.Name = "buttonOnlineActivateByPhone";
			buttonOnlineActivateByPhone.Size = new System.Drawing.Size(108, 28);
			buttonOnlineActivateByPhone.TabIndex = 9;
			buttonOnlineActivateByPhone.Text = "Activate by Phone";
			buttonOnlineActivateByPhone.UseVisualStyleBackColor = true;
			buttonOnlineActivateByPhone.Click += new System.EventHandler(buttonActivateByPhone_Click);
			label23.AutoSize = true;
			label23.Location = new System.Drawing.Point(236, 186);
			label23.Name = "label23";
			label23.Size = new System.Drawing.Size(27, 13);
			label23.TabIndex = 32;
			label23.Text = "City:";
			textBoxCity.Location = new System.Drawing.Point(269, 182);
			textBoxCity.Name = "textBoxCity";
			textBoxCity.Size = new System.Drawing.Size(166, 20);
			textBoxCity.TabIndex = 7;
			label22.AutoSize = true;
			label22.Location = new System.Drawing.Point(11, 184);
			label22.Name = "label22";
			label22.Size = new System.Drawing.Size(46, 13);
			label22.TabIndex = 30;
			label22.Text = "Country:";
			textBoxCountry.Location = new System.Drawing.Point(102, 182);
			textBoxCountry.Name = "textBoxCountry";
			textBoxCountry.Size = new System.Drawing.Size(131, 20);
			textBoxCountry.TabIndex = 6;
			label20.AutoSize = true;
			label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label20.ForeColor = System.Drawing.Color.Navy;
			label20.Location = new System.Drawing.Point(11, 8);
			label20.Name = "label20";
			label20.Size = new System.Drawing.Size(118, 17);
			label20.TabIndex = 28;
			label20.Text = "Activate Online";
			label19.AutoSize = true;
			label19.Location = new System.Drawing.Point(11, 160);
			label19.Name = "label19";
			label19.Size = new System.Drawing.Size(61, 13);
			label19.TabIndex = 27;
			label19.Text = "Telephone:";
			textBoxTelephone.Location = new System.Drawing.Point(102, 157);
			textBoxTelephone.Name = "textBoxTelephone";
			textBoxTelephone.Size = new System.Drawing.Size(333, 20);
			textBoxTelephone.TabIndex = 5;
			label18.AutoSize = true;
			label18.Location = new System.Drawing.Point(11, 136);
			label18.Name = "label18";
			label18.Size = new System.Drawing.Size(35, 13);
			label18.TabIndex = 25;
			label18.Text = "Email:";
			textBoxEmail.Location = new System.Drawing.Point(102, 133);
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(333, 20);
			textBoxEmail.TabIndex = 4;
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(11, 112);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(61, 13);
			label17.TabIndex = 23;
			label17.Text = "Last Name:";
			textBoxLastName.Location = new System.Drawing.Point(102, 109);
			textBoxLastName.Name = "textBoxLastName";
			textBoxLastName.Size = new System.Drawing.Size(333, 20);
			textBoxLastName.TabIndex = 3;
			label16.AutoSize = true;
			label16.Location = new System.Drawing.Point(11, 88);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(60, 13);
			label16.TabIndex = 21;
			label16.Text = "First Name:";
			textBoxFirstName.Location = new System.Drawing.Point(102, 85);
			textBoxFirstName.Name = "textBoxFirstName";
			textBoxFirstName.Size = new System.Drawing.Size(333, 20);
			textBoxFirstName.TabIndex = 2;
			label15.AutoSize = true;
			label15.Location = new System.Drawing.Point(11, 64);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(85, 13);
			label15.TabIndex = 0;
			label15.Text = "Company Name:";
			textBoxCompanyName.Location = new System.Drawing.Point(102, 62);
			textBoxCompanyName.Name = "textBoxCompanyName";
			textBoxCompanyName.Size = new System.Drawing.Size(333, 20);
			textBoxCompanyName.TabIndex = 1;
			label12.AutoSize = true;
			label12.Location = new System.Drawing.Point(11, 44);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(170, 13);
			label12.TabIndex = 17;
			label12.Text = "Please fill your contact information:";
			buttonOnlineActivateNow.Location = new System.Drawing.Point(216, 228);
			buttonOnlineActivateNow.Name = "buttonOnlineActivateNow";
			buttonOnlineActivateNow.Size = new System.Drawing.Size(108, 28);
			buttonOnlineActivateNow.TabIndex = 8;
			buttonOnlineActivateNow.Text = "Activate Now";
			buttonOnlineActivateNow.UseVisualStyleBackColor = true;
			buttonOnlineActivateNow.Click += new System.EventHandler(buttonOnlineActivateNow_Click);
			progressBar1.Location = new System.Drawing.Point(14, 231);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new System.Drawing.Size(196, 21);
			progressBar1.Style = Infragistics.Win.UltraWinProgressBar.ProgressBarStyle.Segmented;
			progressBar1.TabIndex = 0;
			progressBar1.Text = "[Value]";
			progressBar1.UseAppStyling = false;
			progressBar1.Visible = false;
			ultraTabPageControl5.Controls.Add(buttonDone);
			ultraTabPageControl5.Controls.Add(label14);
			ultraTabPageControl5.Controls.Add(label13);
			ultraTabPageControl5.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl5.Name = "ultraTabPageControl5";
			ultraTabPageControl5.Size = new System.Drawing.Size(447, 267);
			buttonDone.Location = new System.Drawing.Point(338, 232);
			buttonDone.Name = "buttonDone";
			buttonDone.Size = new System.Drawing.Size(95, 24);
			buttonDone.TabIndex = 20;
			buttonDone.Text = "Done";
			buttonDone.UseVisualStyleBackColor = true;
			buttonDone.Click += new System.EventHandler(buttonDone_Click);
			label14.Location = new System.Drawing.Point(12, 75);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(421, 45);
			label14.TabIndex = 19;
			label14.Text = "Thank you for activating the product. You can close the form and continue using the product.";
			label13.AutoSize = true;
			label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label13.ForeColor = System.Drawing.Color.DarkGreen;
			label13.Location = new System.Drawing.Point(11, 42);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(282, 24);
			label13.TabIndex = 18;
			label13.Text = "Activation Finished Successfully.";
			ultraTabPageControl6.Controls.Add(label33);
			ultraTabPageControl6.Controls.Add(buttonRegisterLater);
			ultraTabPageControl6.Controls.Add(label24);
			ultraTabPageControl6.Controls.Add(textBoxRegCity);
			ultraTabPageControl6.Controls.Add(label25);
			ultraTabPageControl6.Controls.Add(textBoxRegCountry);
			ultraTabPageControl6.Controls.Add(label26);
			ultraTabPageControl6.Controls.Add(label27);
			ultraTabPageControl6.Controls.Add(textBoxRegTelephone);
			ultraTabPageControl6.Controls.Add(label28);
			ultraTabPageControl6.Controls.Add(textBoxRegEmail);
			ultraTabPageControl6.Controls.Add(label29);
			ultraTabPageControl6.Controls.Add(textBoxRegLastName);
			ultraTabPageControl6.Controls.Add(label30);
			ultraTabPageControl6.Controls.Add(textBoxRegFirstName);
			ultraTabPageControl6.Controls.Add(label31);
			ultraTabPageControl6.Controls.Add(textBoxRegCompanyName);
			ultraTabPageControl6.Controls.Add(buttonRegisterNow);
			ultraTabPageControl6.Controls.Add(progressBar2);
			ultraTabPageControl6.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl6.Name = "ultraTabPageControl6";
			ultraTabPageControl6.Size = new System.Drawing.Size(447, 267);
			label33.Location = new System.Drawing.Point(11, 32);
			label33.Name = "label33";
			label33.Size = new System.Drawing.Size(411, 31);
			label33.TabIndex = 53;
			label33.Text = "By registering the evaluation copy you will receive product updates and information. Please register you evaluation copy now.";
			buttonRegisterLater.Location = new System.Drawing.Point(327, 229);
			buttonRegisterLater.Name = "buttonRegisterLater";
			buttonRegisterLater.Size = new System.Drawing.Size(108, 28);
			buttonRegisterLater.TabIndex = 9;
			buttonRegisterLater.Text = "Don't Register";
			buttonRegisterLater.UseVisualStyleBackColor = true;
			buttonRegisterLater.Click += new System.EventHandler(buttonRegisterLater_Click);
			label24.AutoSize = true;
			label24.Location = new System.Drawing.Point(236, 174);
			label24.Name = "label24";
			label24.Size = new System.Drawing.Size(27, 13);
			label24.TabIndex = 51;
			label24.Text = "City:";
			textBoxRegCity.Location = new System.Drawing.Point(269, 170);
			textBoxRegCity.Name = "textBoxRegCity";
			textBoxRegCity.Size = new System.Drawing.Size(166, 20);
			textBoxRegCity.TabIndex = 7;
			label25.AutoSize = true;
			label25.Location = new System.Drawing.Point(11, 172);
			label25.Name = "label25";
			label25.Size = new System.Drawing.Size(46, 13);
			label25.TabIndex = 49;
			label25.Text = "Country:";
			textBoxRegCountry.Location = new System.Drawing.Point(102, 170);
			textBoxRegCountry.Name = "textBoxRegCountry";
			textBoxRegCountry.Size = new System.Drawing.Size(131, 20);
			textBoxRegCountry.TabIndex = 6;
			label26.AutoSize = true;
			label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label26.ForeColor = System.Drawing.Color.Navy;
			label26.Location = new System.Drawing.Point(11, 9);
			label26.Name = "label26";
			label26.Size = new System.Drawing.Size(232, 17);
			label26.TabIndex = 47;
			label26.Text = "Registering Evaluation Version";
			label27.AutoSize = true;
			label27.Location = new System.Drawing.Point(11, 149);
			label27.Name = "label27";
			label27.Size = new System.Drawing.Size(61, 13);
			label27.TabIndex = 46;
			label27.Text = "Telephone:";
			textBoxRegTelephone.Location = new System.Drawing.Point(102, 146);
			textBoxRegTelephone.Name = "textBoxRegTelephone";
			textBoxRegTelephone.Size = new System.Drawing.Size(333, 20);
			textBoxRegTelephone.TabIndex = 5;
			label28.AutoSize = true;
			label28.Location = new System.Drawing.Point(11, 125);
			label28.Name = "label28";
			label28.Size = new System.Drawing.Size(35, 13);
			label28.TabIndex = 45;
			label28.Text = "Email:";
			textBoxRegEmail.Location = new System.Drawing.Point(102, 122);
			textBoxRegEmail.Name = "textBoxRegEmail";
			textBoxRegEmail.Size = new System.Drawing.Size(333, 20);
			textBoxRegEmail.TabIndex = 4;
			label29.AutoSize = true;
			label29.Location = new System.Drawing.Point(226, 101);
			label29.Name = "label29";
			label29.Size = new System.Drawing.Size(61, 13);
			label29.TabIndex = 44;
			label29.Text = "Last Name:";
			textBoxRegLastName.Location = new System.Drawing.Point(293, 98);
			textBoxRegLastName.Name = "textBoxRegLastName";
			textBoxRegLastName.Size = new System.Drawing.Size(142, 20);
			textBoxRegLastName.TabIndex = 3;
			label30.AutoSize = true;
			label30.Location = new System.Drawing.Point(11, 101);
			label30.Name = "label30";
			label30.Size = new System.Drawing.Size(60, 13);
			label30.TabIndex = 43;
			label30.Text = "First Name:";
			textBoxRegFirstName.Location = new System.Drawing.Point(102, 98);
			textBoxRegFirstName.Name = "textBoxRegFirstName";
			textBoxRegFirstName.Size = new System.Drawing.Size(118, 20);
			textBoxRegFirstName.TabIndex = 2;
			label31.AutoSize = true;
			label31.Location = new System.Drawing.Point(11, 76);
			label31.Name = "label31";
			label31.Size = new System.Drawing.Size(85, 13);
			label31.TabIndex = 0;
			label31.Text = "Company Name:";
			textBoxRegCompanyName.Location = new System.Drawing.Point(102, 74);
			textBoxRegCompanyName.Name = "textBoxRegCompanyName";
			textBoxRegCompanyName.Size = new System.Drawing.Size(333, 20);
			textBoxRegCompanyName.TabIndex = 1;
			buttonRegisterNow.Location = new System.Drawing.Point(216, 229);
			buttonRegisterNow.Name = "buttonRegisterNow";
			buttonRegisterNow.Size = new System.Drawing.Size(108, 28);
			buttonRegisterNow.TabIndex = 8;
			buttonRegisterNow.Text = "Register Now";
			buttonRegisterNow.UseVisualStyleBackColor = true;
			buttonRegisterNow.Click += new System.EventHandler(buttonRegisterNow_Click);
			progressBar2.Location = new System.Drawing.Point(14, 232);
			progressBar2.Name = "progressBar2";
			progressBar2.Size = new System.Drawing.Size(196, 21);
			progressBar2.Style = Infragistics.Win.UltraWinProgressBar.ProgressBarStyle.Segmented;
			progressBar2.TabIndex = 35;
			progressBar2.Text = "[Value]";
			progressBar2.UseAppStyling = false;
			progressBar2.Visible = false;
			ultraTabPageControl7.Controls.Add(buttonRegisterDone);
			ultraTabPageControl7.Controls.Add(label32);
			ultraTabPageControl7.Controls.Add(label34);
			ultraTabPageControl7.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabPageControl7.Name = "ultraTabPageControl7";
			ultraTabPageControl7.Size = new System.Drawing.Size(447, 267);
			buttonRegisterDone.Location = new System.Drawing.Point(338, 232);
			buttonRegisterDone.Name = "buttonRegisterDone";
			buttonRegisterDone.Size = new System.Drawing.Size(95, 24);
			buttonRegisterDone.TabIndex = 22;
			buttonRegisterDone.Text = "Done";
			buttonRegisterDone.UseVisualStyleBackColor = true;
			buttonRegisterDone.Click += new System.EventHandler(buttonRegisterDone_Click);
			label32.Location = new System.Drawing.Point(12, 81);
			label32.Name = "label32";
			label32.Size = new System.Drawing.Size(421, 42);
			label32.TabIndex = 21;
			label32.Text = "Thank you for registering the product. You can close the form and continue evaluating the product.";
			label34.AutoSize = true;
			label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 14f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label34.ForeColor = System.Drawing.Color.DarkGreen;
			label34.Location = new System.Drawing.Point(11, 38);
			label34.Name = "label34";
			label34.Size = new System.Drawing.Size(311, 24);
			label34.TabIndex = 20;
			label34.Text = "Registeration Finished Successfully.";
			ultraTabControl1.Controls.Add(ultraTabSharedControlsPage1);
			ultraTabControl1.Controls.Add(ultraTabPageControl1);
			ultraTabControl1.Controls.Add(ultraTabPageControl2);
			ultraTabControl1.Controls.Add(ultraTabPageControl3);
			ultraTabControl1.Controls.Add(ultraTabPageControl4);
			ultraTabControl1.Controls.Add(ultraTabPageControl5);
			ultraTabControl1.Controls.Add(ultraTabPageControl6);
			ultraTabControl1.Controls.Add(ultraTabPageControl7);
			ultraTabControl1.Location = new System.Drawing.Point(1, 1);
			ultraTabControl1.Name = "ultraTabControl1";
			ultraTabControl1.SharedControlsPage = ultraTabSharedControlsPage1;
			ultraTabControl1.Size = new System.Drawing.Size(447, 267);
			ultraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Wizard;
			ultraTabControl1.TabIndex = 0;
			ultraTab.Key = "ProductKey";
			ultraTab.TabPage = ultraTabPageControl1;
			ultraTab.Text = "tab1";
			ultraTab2.Key = "ActivateType";
			ultraTab2.TabPage = ultraTabPageControl2;
			ultraTab2.Text = "tab2";
			ultraTab3.Key = "ActivateByPhone";
			ultraTab3.TabPage = ultraTabPageControl3;
			ultraTab3.Text = "tab3";
			ultraTab4.Key = "ActivateOnline";
			ultraTab4.TabPage = ultraTabPageControl4;
			ultraTab4.Text = "tab4";
			ultraTab5.Key = "ActivateDone";
			ultraTab5.TabPage = ultraTabPageControl5;
			ultraTab5.Text = "tab5";
			ultraTab6.Key = "RegisterEvaluation";
			ultraTab6.TabPage = ultraTabPageControl6;
			ultraTab6.Text = "tab6";
			ultraTab7.Key = "RegisterDone";
			ultraTab7.TabPage = ultraTabPageControl7;
			ultraTab7.Text = "tab7";
			ultraTabControl1.Tabs.AddRange(new Infragistics.Win.UltraWinTabControl.UltraTab[7]
			{
				ultraTab,
				ultraTab2,
				ultraTab3,
				ultraTab4,
				ultraTab5,
				ultraTab6,
				ultraTab7
			});
			ultraTabControl1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
			ultraTabControl1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			ultraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Standard;
			ultraTabControl1.SelectedTabChanged += new Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventHandler(ultraTabControl1_SelectedTabChanged);
			ultraTabSharedControlsPage1.Location = new System.Drawing.Point(-10000, -10000);
			ultraTabSharedControlsPage1.Name = "ultraTabSharedControlsPage1";
			ultraTabSharedControlsPage1.Size = new System.Drawing.Size(447, 267);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(451, 269);
			base.Controls.Add(ultraTabControl1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RegisterForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Register and Activate";
			base.Load += new System.EventHandler(RegisterForm_Load);
			ultraTabPageControl1.ResumeLayout(false);
			ultraTabPageControl1.PerformLayout();
			ultraTabPageControl2.ResumeLayout(false);
			ultraTabPageControl2.PerformLayout();
			ultraTabPageControl3.ResumeLayout(false);
			ultraTabPageControl3.PerformLayout();
			ultraTabPageControl4.ResumeLayout(false);
			ultraTabPageControl4.PerformLayout();
			ultraTabPageControl5.ResumeLayout(false);
			ultraTabPageControl5.PerformLayout();
			ultraTabPageControl6.ResumeLayout(false);
			ultraTabPageControl6.PerformLayout();
			ultraTabPageControl7.ResumeLayout(false);
			ultraTabPageControl7.PerformLayout();
			((System.ComponentModel.ISupportInitialize)ultraTabControl1).EndInit();
			ultraTabControl1.ResumeLayout(false);
			ResumeLayout(false);
		}
	}
}
