using DevExpress.XtraEditors;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.SoftReg;
using Micromind.Common;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.Others.HelpSupports
{
	public class ErrorHelperForm : Form
	{
		private Exception ex;

		private string summary = "";

		private MenuItem menuItemCopy;

		private MenuItem menuItemEmailSupport;

		private MenuItem menuItem3;

		private MenuItem menuItemViewSource;

		private ToolTip toolTip1;

		private XPButton buttonCancel;

		private XPButton buttonSourceSummary;

		private XPButton xpButton1;

		private Label label1;

		private PictureBox pictureBox1;

		private TextBox textBoxMessage;

		private SimpleButton buttonSend;

		private BackgroundWorker backgroundWorker1;

		private IContainer components;

		private ErrorEvent errorEvent;

		private bool isShowSendButton = true;

		public string Message
		{
			set
			{
				summary = value;
				ViewDetails();
			}
		}

		public Exception Error
		{
			set
			{
				ex = value;
			}
		}

		public ErrorEvent ErrorEventData
		{
			set
			{
				if (value.sqlException != null)
				{
					Error = value.sqlException;
				}
				else
				{
					Error = value.exception;
				}
				Message = value.Memo;
				if (value.IsRightToLeft)
				{
					textBoxMessage.RightToLeft = RightToLeft.Yes;
				}
				errorEvent = value;
			}
		}

		public bool IsShowSendButton
		{
			set
			{
				if (!value)
				{
					buttonSend.Visible = false;
					buttonCancel.Text = "Close";
					buttonCancel.Left = buttonSend.Left;
				}
			}
		}

		public ErrorHelperForm()
		{
			InitializeComponent();
			base.Activated += ErrorHelperForm_Activated;
		}

		private void ErrorHelperForm_Activated(object sender, EventArgs e)
		{
			if (Global.ConStatus != 0)
			{
				buttonSend.Enabled = false;
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
            this.components = new System.ComponentModel.Container();
            this.menuItemCopy = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemViewSource = new System.Windows.Forms.MenuItem();
            this.menuItemEmailSupport = new System.Windows.Forms.MenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonCancel = new Micromind.UISupport.XPButton();
            this.buttonSourceSummary = new Micromind.UISupport.XPButton();
            this.xpButton1 = new Micromind.UISupport.XPButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new DevExpress.XtraEditors.SimpleButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Index = -1;
            this.menuItemCopy.Text = "";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = -1;
            this.menuItem3.Text = "";
            // 
            // menuItemViewSource
            // 
            this.menuItemViewSource.Index = -1;
            this.menuItemViewSource.Text = "";
            // 
            // menuItemEmailSupport
            // 
            this.menuItemEmailSupport.Index = -1;
            this.menuItemEmailSupport.Text = "";
            // 
            // buttonCancel
            // 
            this.buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(198, 198);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(122, 35);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "&Don\'t Send";
            this.toolTip1.SetToolTip(this.buttonCancel, "Do not send error report");
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSourceSummary
            // 
            this.buttonSourceSummary.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.buttonSourceSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSourceSummary.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.buttonSourceSummary.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.buttonSourceSummary.Location = new System.Drawing.Point(10, 198);
            this.buttonSourceSummary.Name = "buttonSourceSummary";
            this.buttonSourceSummary.Size = new System.Drawing.Size(94, 35);
            this.buttonSourceSummary.TabIndex = 1;
            this.buttonSourceSummary.Text = "S&ummary";
            this.toolTip1.SetToolTip(this.buttonSourceSummary, "Summary or source for this error");
            this.buttonSourceSummary.Click += new System.EventHandler(this.buttonSourceSummary_Click);
            // 
            // xpButton1
            // 
            this.xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
            this.xpButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
            this.xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
            this.xpButton1.Location = new System.Drawing.Point(112, 198);
            this.xpButton1.Name = "xpButton1";
            this.xpButton1.Size = new System.Drawing.Size(94, 35);
            this.xpButton1.TabIndex = 2;
            this.xpButton1.Text = "&Copy";
            this.toolTip1.SetToolTip(this.xpButton1, "Summary or source for this error");
            this.xpButton1.Click += new System.EventHandler(this.xpButton1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(50, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(459, 37);
            this.label1.TabIndex = 20;
            this.label1.Text = "An error has occured in the application. Please send the error to us to review.  " +
    "The error message is:";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxMessage.BackColor = System.Drawing.Color.White;
            this.textBoxMessage.Location = new System.Drawing.Point(-2, 45);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.ReadOnly = true;
            this.textBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxMessage.Size = new System.Drawing.Size(483, 141);
            this.textBoxMessage.TabIndex = 22;
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonSend.Appearance.Options.UseFont = true;
            this.buttonSend.ImageOptions.Image = global::Micromind.ClientUI.Properties.Resources.send;
            this.buttonSend.Location = new System.Drawing.Point(327, 199);
            this.buttonSend.LookAndFeel.SkinName = "Money Twins";
            this.buttonSend.LookAndFeel.UseDefaultLookAndFeel = false;
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(140, 33);
            this.buttonSend.TabIndex = 23;
            this.buttonSend.Text = "Send Error";
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Micromind.ClientUI.Properties.Resources.ErrorIcon;
            this.pictureBox1.Location = new System.Drawing.Point(7, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(39, 37);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 21;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // ErrorHelperForm
            // 
            this.AcceptButton = this.buttonSend;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(481, 239);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xpButton1);
            this.Controls.Add(this.buttonSourceSummary);
            this.Controls.Add(this.buttonCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(499, 286);
            this.Name = "ErrorHelperForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ErrorHelperForm_Closing);
            this.Load += new System.EventHandler(this.ErrorHelperForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		private void buttonDetails_Click(object sender, EventArgs e)
		{
			ViewDetails();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EmailSupport()
		{
			try
			{
				Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
				string text = "Error Message:" + this.ex.Message;
				if (this.ex.GetType() == typeof(CompanyException))
				{
					text = text + "\nCompany Ex NO:" + ((CompanyException)this.ex).Number;
				}
				else if (this.ex.GetType() == typeof(SqlException))
				{
					text = text + "\nSQL Ex NO:" + ((SqlException)this.ex).Number;
				}
				text = text + "\nSource Object:" + this.ex.Source;
				text = text + "\nDetailed Message:" + this.ex.ToString();
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				Attribute.GetCustomAttribute(executingAssembly, typeof(AssemblyInformationalVersionAttribute));
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
				text = text + "\nException Type:" + this.ex.GetType();
				text = text + "\nF.V." + versionInfo.FileVersion;
				text = text + "\nProduct Version:" + UIGlobal.GetCurrentClientVersion().GetVersionString();
				text = text + "\nUserID:" + Global.CurrentUser;
				text = text + "\nDate:" + DateTime.Now;
				MethodBase targetSite = this.ex.TargetSite;
				if (targetSite != null)
				{
					text = text + "\nModule Name:" + targetSite.Module.Name;
					text = text + "\nMethod Name:" + targetSite.Name;
				}
				softReg.SendError(Global.ProductID.ToString(), Global.GetProductKey(), Global.CompanyName + ":" + Global.ComputerName, text);
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage("Cannot send Error Message.\n" + ex.Message);
			}
		}

		private void ErrorHelperForm_Load(object sender, EventArgs e)
		{
			buttonSourceSummary.Text = "More Info";
			if (Global.ConStatus != 0)
			{
				buttonSend.Enabled = false;
			}
		}

		private void ViewDetails()
		{
			if (buttonSourceSummary.Tag != null && buttonSourceSummary.Tag.ToString() == "1")
			{
				if (ex != null)
				{
					string[] lines = ex.ToString().Split('\n', '\r');
					textBoxMessage.Lines = lines;
					buttonSourceSummary.Text = "Summary";
				}
				buttonSourceSummary.Tag = 0;
			}
			else
			{
				string[] lines2 = summary.Split('\n', '\r');
				textBoxMessage.Lines = lines2;
				buttonSourceSummary.Text = "More Info";
				buttonSourceSummary.Tag = 1;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void buttonSourceSummary_Click(object sender, EventArgs e)
		{
			ViewDetails();
		}

		private void ErrorHelperForm_Closing(object sender, CancelEventArgs e)
		{
			Global.CompanySettings.SaveFormProperties(this);
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(textBoxMessage.Text);
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			backgroundWorker1.RunWorkerAsync();
			Close();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			EmailSupport();
		}
	}
}
