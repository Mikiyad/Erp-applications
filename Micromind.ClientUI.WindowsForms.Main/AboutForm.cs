using DevExpress.XtraEditors;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Main
{
	public class AboutForm : Micromind.ClientUI.Configurations.DialogBoxBaseForm
	{
		private int runsLeft;

		private XPButton buttonClose;

		private XPButton buttonSystemInfo;

		private Label label1;

		private XPButton buttonLicenseAgreement;

		private Label labelTrial;

		private Panel panel2;

		private Panel panel1;

		private Label labelDaysLeft;

		private Label label2;

		private LabelControl labelEdition;

		private Label labelVersion;

		private Label labelFV;

		private Label labelDBVersion;

		private LinkLabel linklabelVersion;
        private Label label4;
        private Label label3;
        private Container components;

		public AboutForm()
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.buttonClose = new Micromind.UISupport.XPButton();
            this.buttonSystemInfo = new Micromind.UISupport.XPButton();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTrial = new System.Windows.Forms.Label();
            this.buttonLicenseAgreement = new Micromind.UISupport.XPButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelEdition = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linklabelVersion = new System.Windows.Forms.LinkLabel();
            this.labelDBVersion = new System.Windows.Forms.Label();
            this.labelFV = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDaysLeft = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackColor = System.Drawing.Color.DarkGray;
            this.buttonClose.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonClose.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonClose.Location = new System.Drawing.Point(431, 293);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(94, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "&OK";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSystemInfo
            // 
            this.buttonSystemInfo.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSystemInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSystemInfo.BackColor = System.Drawing.Color.DarkGray;
            this.buttonSystemInfo.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSystemInfo.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSystemInfo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSystemInfo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonSystemInfo.Location = new System.Drawing.Point(12, 293);
            this.buttonSystemInfo.Name = "buttonSystemInfo";
            this.buttonSystemInfo.Size = new System.Drawing.Size(120, 23);
            this.buttonSystemInfo.TabIndex = 4;
            this.buttonSystemInfo.Text = "System Info...";
            this.buttonSystemInfo.UseVisualStyleBackColor = false;
            this.buttonSystemInfo.Visible = false;
            this.buttonSystemInfo.Click += new System.EventHandler(this.buttonSystemInfo_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(13, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 16);
            this.label1.TabIndex = 126;
            this.label1.Text = "Copyright Starasoft © 2019-2020";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelTrial
            // 
            this.labelTrial.BackColor = System.Drawing.Color.White;
            this.labelTrial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTrial.ForeColor = System.Drawing.Color.Black;
            this.labelTrial.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelTrial.Location = new System.Drawing.Point(13, 3);
            this.labelTrial.Name = "labelTrial";
            this.labelTrial.Size = new System.Drawing.Size(229, 24);
            this.labelTrial.TabIndex = 128;
            this.labelTrial.Text = "Evaluation Version";
            this.labelTrial.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonLicenseAgreement
            // 
            this.buttonLicenseAgreement.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonLicenseAgreement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLicenseAgreement.BackColor = System.Drawing.Color.DarkGray;
            this.buttonLicenseAgreement.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonLicenseAgreement.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonLicenseAgreement.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLicenseAgreement.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonLicenseAgreement.Location = new System.Drawing.Point(140, 293);
            this.buttonLicenseAgreement.Name = "buttonLicenseAgreement";
            this.buttonLicenseAgreement.Size = new System.Drawing.Size(120, 23);
            this.buttonLicenseAgreement.TabIndex = 130;
            this.buttonLicenseAgreement.Text = "License Agreement...";
            this.buttonLicenseAgreement.UseVisualStyleBackColor = false;
            this.buttonLicenseAgreement.Visible = false;
            this.buttonLicenseAgreement.Click += new System.EventHandler(this.buttonLicenseAgreement_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BackgroundImage = global::Micromind.ClientUI.Properties.Resources.headerbg;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.labelEdition);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(536, 130);
            this.panel2.TabIndex = 140;
            // 
            // labelEdition
            // 
            this.labelEdition.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelEdition.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelEdition.Appearance.Options.UseFont = true;
            this.labelEdition.Appearance.Options.UseForeColor = true;
            this.labelEdition.Location = new System.Drawing.Point(15, 93);
            this.labelEdition.Name = "labelEdition";
            this.labelEdition.Size = new System.Drawing.Size(139, 16);
            this.labelEdition.TabIndex = 132;
            this.labelEdition.Text = "Professional Edition";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.linklabelVersion);
            this.panel1.Controls.Add(this.labelDBVersion);
            this.panel1.Controls.Add(this.labelFV);
            this.panel1.Controls.Add(this.labelVersion);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelDaysLeft);
            this.panel1.Controls.Add(this.labelTrial);
            this.panel1.Location = new System.Drawing.Point(-1, 130);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(537, 155);
            this.panel1.TabIndex = 139;
            // 
            // linklabelVersion
            // 
            this.linklabelVersion.AutoSize = true;
            this.linklabelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.linklabelVersion.Location = new System.Drawing.Point(371, 13);
            this.linklabelVersion.Name = "linklabelVersion";
            this.linklabelVersion.Size = new System.Drawing.Size(155, 13);
            this.linklabelVersion.TabIndex = 131;
            this.linklabelVersion.TabStop = true;
            this.linklabelVersion.Text = "Product Version: 3.0.1006";
            this.linklabelVersion.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklabelVersion_LinkClicked);
            // 
            // labelDBVersion
            // 
            this.labelDBVersion.BackColor = System.Drawing.Color.White;
            this.labelDBVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDBVersion.ForeColor = System.Drawing.Color.Black;
            this.labelDBVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDBVersion.Location = new System.Drawing.Point(364, 27);
            this.labelDBVersion.Name = "labelDBVersion";
            this.labelDBVersion.Size = new System.Drawing.Size(162, 24);
            this.labelDBVersion.TabIndex = 130;
            this.labelDBVersion.Text = "DB Version: 1.1.1000";
            this.labelDBVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFV
            // 
            this.labelFV.BackColor = System.Drawing.Color.White;
            this.labelFV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFV.ForeColor = System.Drawing.Color.Black;
            this.labelFV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelFV.Location = new System.Drawing.Point(372, 125);
            this.labelFV.Name = "labelFV";
            this.labelFV.Size = new System.Drawing.Size(162, 24);
            this.labelFV.TabIndex = 129;
            this.labelFV.Text = "F.V: 1.0.1006";
            this.labelFV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.White;
            this.labelVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.Black;
            this.labelVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelVersion.Location = new System.Drawing.Point(434, 51);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(89, 24);
            this.labelVersion.TabIndex = 129;
            this.labelVersion.Text = "Product Version: 3.0.1006";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelVersion.Visible = false;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(13, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(447, 57);
            this.label2.TabIndex = 126;
            this.label2.Text = resources.GetString("label2.Text");
            this.label2.Click += new System.EventHandler(this.label1_Click);
            // 
            // labelDaysLeft
            // 
            this.labelDaysLeft.BackColor = System.Drawing.Color.White;
            this.labelDaysLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDaysLeft.ForeColor = System.Drawing.Color.Black;
            this.labelDaysLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelDaysLeft.Location = new System.Drawing.Point(13, 27);
            this.labelDaysLeft.Name = "labelDaysLeft";
            this.labelDaysLeft.Size = new System.Drawing.Size(291, 24);
            this.labelDaysLeft.TabIndex = 128;
            this.labelDaysLeft.Text = "Debug";
            this.labelDaysLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(15, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(283, 31);
            this.label3.TabIndex = 133;
            this.label3.Text = "Starasoft © StarERP";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(15, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 24);
            this.label4.TabIndex = 134;
            this.label4.Text = "Business Managment System";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.buttonClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(534, 323);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.buttonLicenseAgreement);
            this.Controls.Add(this.buttonSystemInfo);
            this.Controls.Add(this.buttonClose);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.Text = "About Micromind Axolon";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}

		public string GetProductVersion()
		{
			return UIGlobal.GetCurrentClientVersion().GetVersionString();
		}

		private void Init()
		{
			Text = "About " + base.ProductName;
			if (GlobalRules.IsBeta)
			{
				labelDaysLeft.Visible = true;
			}
			else if (GlobalRules.IsTrial)
			{
				if (GlobalRules.IsTrial)
				{
					labelDaysLeft.Text = "Evaluation copy (" + runsLeft.ToString() + " sessions remaining)";
				}
				else
				{
					labelDaysLeft.Text = "Inactivated version (" + runsLeft.ToString() + " sessions remaining)";
				}
				labelDaysLeft.Visible = true;
			}
			else
			{
				labelDaysLeft.Visible = false;
			}
		}

		private void AboutForm_Load(object sender, EventArgs e)
		{
			try
			{
				InitDialog();
				Init();
				DisplayWarning();
				DisplayCredits();
				if (GlobalRules.IsBeta)
				{
					labelTrial.Text = "Beta Version";
					labelTrial.Visible = true;
				}
				else if (GlobalRules.IsTrial)
				{
					labelTrial.Text = "Evaluation Version";
					Label label = labelTrial;
					label.Text = label.Text + " - " + AxolonLicense.LicenseManager.License.ExpiryDays.ToString() + " days left";
				}
				else
				{
					labelTrial.Text = "Full Version";
					if (AxolonLicense.LicenseManager.License.IsActivated)
					{
						labelTrial.Text += " - Activated";
					}
					else
					{
						labelTrial.Text += " - Inactive";
						Label label2 = labelTrial;
						label2.Text = label2.Text + " - " + AxolonLicense.LicenseManager.TrialDaysLeft().ToString() + " days left";
					}
				}
				labelEdition.Text = GlobalRules.EditionText + " Edition";
				labelVersion.Text = "Product Version: " + GetProductVersion();
				linklabelVersion.Text = "Product Version: " + GetProductVersion();
				labelDBVersion.Text = "DB Version: " + Factory.DatabaseSystem.GetCurrentDBVersion();
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Attribute.GetCustomAttribute(executingAssembly, typeof(AssemblyInformationalVersionAttribute));
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				labelFV.Text = "F.V." + versionInfo.FileVersion;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonSystemInfo_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start("msinfo32.exe");
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void DisplayWarning()
		{
		}

		private void DisplayCredits()
		{
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
		}

		private void buttonLicenseAgreement_Click(object sender, EventArgs e)
		{
			try
			{
				Process.Start(Application.StartupPath + "\\eula.txt");
			}
			catch (Exception ex)
			{
				ErrorHelper.InformationMessage("File not found", ex.Message, "Please add the eula.txt file to '" + Application.StartupPath + "' directory.", "You can obtain EULA from the CD.");
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{
			ErrorHelper.InformationMessage(base.ProductName, "The product key in the registry is not valid.", "Please contact " + Application.CompanyName + " support for further information.");
		}

		private void label1_Click(object sender, EventArgs e)
		{
		}

		private void labelDaysLeft_Click(object sender, EventArgs e)
		{
		}

		private void textBoxKey_Load(object sender, EventArgs e)
		{
		}

		private void label2_Click_1(object sender, EventArgs e)
		{
		}

		private void linklabelVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				Process.Start(Application.StartupPath + "\\updateinfo.html");
			}
			catch (Exception ex)
			{
				ErrorHelper.InformationMessage("File not found", ex.Message, "", "");
			}
		}
	}
}
