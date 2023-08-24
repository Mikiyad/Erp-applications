using Micromind.ClientLibraries;
using Micromind.ClientUI.SoftReg;
using Micromind.UISupport;
using Micromind.UISupport.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.Configurations
{
	public class FeedbackForm : Form
	{
		private MMLabel lblName;

		private MMTextBox editServerName;

		private MMLabel lblPortNumber;

		private MMTextBox editUserName;

		private MMLabel lblUserName;

		private MMTextBox editPassword;

		private MMLabel lblPassword;

		private XPButton buttonCancel;

		private MMLabel lblDatabaseName;

		private MMTextBox editDatabaseName;

		private CheckBox checkBoxRememberPassword;

		private ToolTip toolTip;

		private ImageList imageList1;

		private OpenFileDialog openFileDialog;

		private XPButton buttonOpenFileDialog;

		private MMTextBox textBoxFileName;

		private MMLabel label3;

		private MMLabel label4;

		private MMLabel label5;

		private MMLabel label6;

		private MMTextBox editPortNumber;

		private MMLabel label2;

		private ContextMenu contextMenu;

		private MenuItem menuItemRemoveCompanyName;

		private Micromind.UISupport.Controls.TabPage tabServer;

		private Micromind.UISupport.Controls.TabPage tabDatabase;

		private Micromind.UISupport.Controls.TabPage tabOpenFromFile;

		private MMTextBox editCompanyName;

		private Line line1;

		private XPButton buttonOK;

		private TextBox textBoxMemo;

		private TextBox textBox1;

		private Label label1;

		private TextBox textBoxEmail;

		private Label label7;

		private TextBox textBoxName;

		private Label label8;

		private Label label9;

		private TextBox textBoxSubject;

		private ComboBox comboBoxType;

		private Label label10;

		private IContainer components;

		public FeedbackForm()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Configurations.FeedbackForm));
			editPortNumber = new Micromind.UISupport.MMTextBox();
			label6 = new Micromind.UISupport.MMLabel();
			lblPortNumber = new Micromind.UISupport.MMLabel();
			editServerName = new Micromind.UISupport.MMTextBox();
			lblName = new Micromind.UISupport.MMLabel();
			checkBoxRememberPassword = new System.Windows.Forms.CheckBox();
			editDatabaseName = new Micromind.UISupport.MMTextBox();
			lblDatabaseName = new Micromind.UISupport.MMLabel();
			editPassword = new Micromind.UISupport.MMTextBox();
			lblPassword = new Micromind.UISupport.MMLabel();
			editUserName = new Micromind.UISupport.MMTextBox();
			lblUserName = new Micromind.UISupport.MMLabel();
			label2 = new Micromind.UISupport.MMLabel();
			label5 = new Micromind.UISupport.MMLabel();
			label4 = new Micromind.UISupport.MMLabel();
			buttonOpenFileDialog = new Micromind.UISupport.XPButton();
			textBoxFileName = new Micromind.UISupport.MMTextBox();
			label3 = new Micromind.UISupport.MMLabel();
			buttonCancel = new Micromind.UISupport.XPButton();
			contextMenu = new System.Windows.Forms.ContextMenu();
			menuItemRemoveCompanyName = new System.Windows.Forms.MenuItem();
			imageList1 = new System.Windows.Forms.ImageList(components);
			toolTip = new System.Windows.Forms.ToolTip(components);
			tabServer = new Micromind.UISupport.Controls.TabPage();
			tabDatabase = new Micromind.UISupport.Controls.TabPage();
			editCompanyName = new Micromind.UISupport.MMTextBox();
			tabOpenFromFile = new Micromind.UISupport.Controls.TabPage();
			line1 = new Micromind.UISupport.Line();
			openFileDialog = new System.Windows.Forms.OpenFileDialog();
			buttonOK = new Micromind.UISupport.XPButton();
			textBoxMemo = new System.Windows.Forms.TextBox();
			textBox1 = new System.Windows.Forms.TextBox();
			label1 = new System.Windows.Forms.Label();
			textBoxEmail = new System.Windows.Forms.TextBox();
			label7 = new System.Windows.Forms.Label();
			textBoxName = new System.Windows.Forms.TextBox();
			label8 = new System.Windows.Forms.Label();
			label9 = new System.Windows.Forms.Label();
			textBoxSubject = new System.Windows.Forms.TextBox();
			comboBoxType = new System.Windows.Forms.ComboBox();
			label10 = new System.Windows.Forms.Label();
			SuspendLayout();
			editPortNumber.BackColor = System.Drawing.Color.White;
			editPortNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editPortNumber.IsComboTextBox = false;
			editPortNumber.Location = new System.Drawing.Point(0, 0);
			editPortNumber.Name = "editPortNumber";
			editPortNumber.Size = new System.Drawing.Size(200, 13);
			editPortNumber.TabIndex = 0;
			editPortNumber.Text = "textBox1";
			label6.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label6.IsFieldHeader = false;
			label6.IsRequired = false;
			label6.Location = new System.Drawing.Point(0, 0);
			label6.Name = "label6";
			label6.PenWidth = 1f;
			label6.ShowBorder = false;
			label6.Size = new System.Drawing.Size(100, 23);
			label6.TabIndex = 0;
			lblPortNumber.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblPortNumber.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPortNumber.IsFieldHeader = false;
			lblPortNumber.IsRequired = false;
			lblPortNumber.Location = new System.Drawing.Point(0, 0);
			lblPortNumber.Name = "lblPortNumber";
			lblPortNumber.PenWidth = 1f;
			lblPortNumber.ShowBorder = false;
			lblPortNumber.Size = new System.Drawing.Size(100, 23);
			lblPortNumber.TabIndex = 0;
			editServerName.BackColor = System.Drawing.Color.White;
			editServerName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editServerName.IsComboTextBox = false;
			editServerName.Location = new System.Drawing.Point(0, 0);
			editServerName.Name = "editServerName";
			editServerName.Size = new System.Drawing.Size(200, 13);
			editServerName.TabIndex = 0;
			editServerName.Text = "textBox1";
			lblName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblName.IsFieldHeader = false;
			lblName.IsRequired = false;
			lblName.Location = new System.Drawing.Point(0, 0);
			lblName.Name = "lblName";
			lblName.PenWidth = 1f;
			lblName.ShowBorder = false;
			lblName.Size = new System.Drawing.Size(100, 23);
			lblName.TabIndex = 0;
			checkBoxRememberPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			checkBoxRememberPassword.Location = new System.Drawing.Point(0, 0);
			checkBoxRememberPassword.Name = "checkBoxRememberPassword";
			checkBoxRememberPassword.Size = new System.Drawing.Size(104, 24);
			checkBoxRememberPassword.TabIndex = 0;
			editDatabaseName.BackColor = System.Drawing.Color.White;
			editDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editDatabaseName.IsComboTextBox = false;
			editDatabaseName.Location = new System.Drawing.Point(0, 0);
			editDatabaseName.Name = "editDatabaseName";
			editDatabaseName.Size = new System.Drawing.Size(200, 13);
			editDatabaseName.TabIndex = 0;
			editDatabaseName.Text = "textBox1";
			lblDatabaseName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblDatabaseName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblDatabaseName.IsFieldHeader = false;
			lblDatabaseName.IsRequired = false;
			lblDatabaseName.Location = new System.Drawing.Point(0, 0);
			lblDatabaseName.Name = "lblDatabaseName";
			lblDatabaseName.PenWidth = 1f;
			lblDatabaseName.ShowBorder = false;
			lblDatabaseName.Size = new System.Drawing.Size(100, 23);
			lblDatabaseName.TabIndex = 0;
			editPassword.BackColor = System.Drawing.Color.White;
			editPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editPassword.IsComboTextBox = false;
			editPassword.Location = new System.Drawing.Point(0, 0);
			editPassword.Name = "editPassword";
			editPassword.Size = new System.Drawing.Size(200, 13);
			editPassword.TabIndex = 0;
			editPassword.Text = "textBox1";
			lblPassword.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblPassword.IsFieldHeader = false;
			lblPassword.IsRequired = false;
			lblPassword.Location = new System.Drawing.Point(0, 0);
			lblPassword.Name = "lblPassword";
			lblPassword.PenWidth = 1f;
			lblPassword.ShowBorder = false;
			lblPassword.Size = new System.Drawing.Size(100, 23);
			lblPassword.TabIndex = 0;
			editUserName.BackColor = System.Drawing.Color.White;
			editUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editUserName.IsComboTextBox = false;
			editUserName.Location = new System.Drawing.Point(0, 0);
			editUserName.Name = "editUserName";
			editUserName.Size = new System.Drawing.Size(200, 13);
			editUserName.TabIndex = 0;
			editUserName.Text = "textBox1";
			lblUserName.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			lblUserName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			lblUserName.IsFieldHeader = false;
			lblUserName.IsRequired = false;
			lblUserName.Location = new System.Drawing.Point(0, 0);
			lblUserName.Name = "lblUserName";
			lblUserName.PenWidth = 1f;
			lblUserName.ShowBorder = false;
			lblUserName.Size = new System.Drawing.Size(100, 23);
			lblUserName.TabIndex = 0;
			label2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label2.IsFieldHeader = false;
			label2.IsRequired = false;
			label2.Location = new System.Drawing.Point(0, 0);
			label2.Name = "label2";
			label2.PenWidth = 1f;
			label2.ShowBorder = false;
			label2.Size = new System.Drawing.Size(100, 23);
			label2.TabIndex = 0;
			label5.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label5.IsFieldHeader = false;
			label5.IsRequired = false;
			label5.Location = new System.Drawing.Point(0, 0);
			label5.Name = "label5";
			label5.PenWidth = 1f;
			label5.ShowBorder = false;
			label5.Size = new System.Drawing.Size(100, 23);
			label5.TabIndex = 0;
			label4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label4.IsFieldHeader = false;
			label4.IsRequired = false;
			label4.Location = new System.Drawing.Point(0, 0);
			label4.Name = "label4";
			label4.PenWidth = 1f;
			label4.ShowBorder = false;
			label4.Size = new System.Drawing.Size(100, 23);
			label4.TabIndex = 0;
			buttonOpenFileDialog.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOpenFileDialog.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOpenFileDialog.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOpenFileDialog.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOpenFileDialog.Location = new System.Drawing.Point(0, 0);
			buttonOpenFileDialog.Name = "buttonOpenFileDialog";
			buttonOpenFileDialog.Size = new System.Drawing.Size(75, 23);
			buttonOpenFileDialog.TabIndex = 0;
			textBoxFileName.BackColor = System.Drawing.Color.White;
			textBoxFileName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			textBoxFileName.IsComboTextBox = false;
			textBoxFileName.Location = new System.Drawing.Point(0, 0);
			textBoxFileName.Name = "textBoxFileName";
			textBoxFileName.Size = new System.Drawing.Size(200, 13);
			textBoxFileName.TabIndex = 0;
			textBoxFileName.Text = "textBox1";
			label3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			label3.IsFieldHeader = false;
			label3.IsRequired = false;
			label3.Location = new System.Drawing.Point(0, 0);
			label3.Name = "label3";
			label3.PenWidth = 1f;
			label3.ShowBorder = false;
			label3.Size = new System.Drawing.Size(100, 23);
			label3.TabIndex = 0;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ForeColor = System.Drawing.Color.Black;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(592, 431);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(81, 24);
			buttonCancel.TabIndex = 7;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[1]
			{
				menuItemRemoveCompanyName
			});
			menuItemRemoveCompanyName.Index = 0;
			menuItemRemoveCompanyName.Text = "Remove";
			imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			imageList1.Images.SetKeyName(0, "");
			tabServer.Location = new System.Drawing.Point(0, 0);
			tabServer.Name = "tabServer";
			tabServer.Selected = false;
			tabServer.Size = new System.Drawing.Size(200, 100);
			tabServer.TabCellWidth = 0;
			tabServer.TabIndex = 0;
			tabServer.TabVisible = true;
			tabDatabase.Location = new System.Drawing.Point(0, 0);
			tabDatabase.Name = "tabDatabase";
			tabDatabase.Selected = false;
			tabDatabase.Size = new System.Drawing.Size(200, 100);
			tabDatabase.TabCellWidth = 0;
			tabDatabase.TabIndex = 0;
			tabDatabase.TabVisible = true;
			editCompanyName.BackColor = System.Drawing.Color.White;
			editCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.None;
			editCompanyName.IsComboTextBox = false;
			editCompanyName.Location = new System.Drawing.Point(0, 0);
			editCompanyName.Name = "editCompanyName";
			editCompanyName.Size = new System.Drawing.Size(200, 13);
			editCompanyName.TabIndex = 0;
			editCompanyName.Text = "textBox1";
			tabOpenFromFile.Location = new System.Drawing.Point(0, 0);
			tabOpenFromFile.Name = "tabOpenFromFile";
			tabOpenFromFile.Selected = false;
			tabOpenFromFile.Size = new System.Drawing.Size(200, 100);
			tabOpenFromFile.TabCellWidth = 0;
			tabOpenFromFile.TabIndex = 0;
			tabOpenFromFile.TabVisible = true;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.Font = new System.Drawing.Font("Tahoma", 8.25f);
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.LightSteelBlue;
			line1.Location = new System.Drawing.Point(-59, 424);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(754, 1);
			line1.TabIndex = 283;
			line1.TabStop = false;
			buttonOK.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonOK.BackColor = System.Drawing.Color.DarkGray;
			buttonOK.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonOK.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonOK.ForeColor = System.Drawing.Color.Black;
			buttonOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonOK.Location = new System.Drawing.Point(505, 431);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(81, 24);
			buttonOK.TabIndex = 6;
			buttonOK.Text = "&Send";
			buttonOK.UseVisualStyleBackColor = false;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			textBoxMemo.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			textBoxMemo.Location = new System.Drawing.Point(12, 78);
			textBoxMemo.MaxLength = 5000;
			textBoxMemo.Multiline = true;
			textBoxMemo.Name = "textBoxMemo";
			textBoxMemo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			textBoxMemo.Size = new System.Drawing.Size(661, 340);
			textBoxMemo.TabIndex = 5;
			textBox1.Location = new System.Drawing.Point(259, 8);
			textBox1.MaxLength = 50;
			textBox1.Name = "textBox1";
			textBox1.Size = new System.Drawing.Size(166, 20);
			textBox1.TabIndex = 1;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(212, 9);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(41, 13);
			label1.TabIndex = 294;
			label1.Text = "Phone:";
			textBoxEmail.Location = new System.Drawing.Point(469, 8);
			textBoxEmail.MaxLength = 50;
			textBoxEmail.Name = "textBoxEmail";
			textBoxEmail.Size = new System.Drawing.Size(199, 20);
			textBoxEmail.TabIndex = 2;
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(428, 9);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(35, 13);
			label7.TabIndex = 293;
			label7.Text = "Email:";
			textBoxName.Location = new System.Drawing.Point(65, 8);
			textBoxName.MaxLength = 50;
			textBoxName.Name = "textBoxName";
			textBoxName.Size = new System.Drawing.Size(145, 20);
			textBoxName.TabIndex = 0;
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(12, 9);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(38, 13);
			label8.TabIndex = 290;
			label8.Text = "Name:";
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(12, 56);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(47, 13);
			label9.TabIndex = 294;
			label9.Text = "Subject:";
			textBoxSubject.Location = new System.Drawing.Point(65, 53);
			textBoxSubject.MaxLength = 50;
			textBoxSubject.Name = "textBoxSubject";
			textBoxSubject.Size = new System.Drawing.Size(603, 20);
			textBoxSubject.TabIndex = 4;
			comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxType.FormattingEnabled = true;
			comboBoxType.Location = new System.Drawing.Point(65, 30);
			comboBoxType.Name = "comboBoxType";
			comboBoxType.Size = new System.Drawing.Size(145, 21);
			comboBoxType.TabIndex = 3;
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(12, 32);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(35, 13);
			label10.TabIndex = 290;
			label10.Text = "Type:";
			base.AcceptButton = buttonOK;
			AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(683, 460);
			base.Controls.Add(comboBoxType);
			base.Controls.Add(textBoxSubject);
			base.Controls.Add(label9);
			base.Controls.Add(textBox1);
			base.Controls.Add(label1);
			base.Controls.Add(textBoxEmail);
			base.Controls.Add(label7);
			base.Controls.Add(textBoxName);
			base.Controls.Add(label10);
			base.Controls.Add(label8);
			base.Controls.Add(textBoxMemo);
			base.Controls.Add(buttonOK);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			Font = new System.Drawing.Font("Tahoma", 8f);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FeedbackForm";
			Text = "Feedback";
			base.Load += new System.EventHandler(DataUtilitiesDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}

		private void DataUtilitiesDialog_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxType.Items.Add("Suggestion");
				comboBoxType.Items.Add("Question");
				comboBoxType.Items.Add("Bug Report");
				comboBoxType.Items.Add("Support");
				comboBoxType.Items.Add("Sale");
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private bool IsValid()
		{
			return true;
		}

		private void tabControl1_SelectionChanged(object sender, EventArgs e)
		{
		}

		private void textBoxInstanceName_Validated(object sender, EventArgs e)
		{
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (textBoxMemo.Text == "")
				{
					ErrorHelper.InformationMessage("Please enter your feedback first.");
				}
				else
				{
					Micromind.ClientUI.SoftReg.SoftReg softReg = new Micromind.ClientUI.SoftReg.SoftReg();
					string text = "";
					text = "Type:" + comboBoxType.Text;
					text = text + "\nSubject:" + textBoxSubject.Text;
					if (softReg.SaveFeedback(memo: text + "\nNote:" + textBoxMemo.Text, productID: "4", productKey: AxolonLicense.LicenseManager.License.Key, name: textBoxName.Text, email: textBoxEmail.Text + "- Phone:" + textBox1.Text, date: DateTime.Now))
					{
						ErrorHelper.InformationMessage("Your feedback has been sent successfully.", "Thank you for your feedback.");
						Close();
					}
					else
					{
						ErrorHelper.WarningMessage("Unable to send your feedback. Please make sure that you are connected to internet or try later.");
					}
				}
			}
			catch (Exception ex)
			{
				ErrorHelper.ErrorMessage("An error has occured while sending feedback.", ex.Message);
			}
		}

		private void buttonSelectCheque_Click(object sender, EventArgs e)
		{
		}
	}
}
