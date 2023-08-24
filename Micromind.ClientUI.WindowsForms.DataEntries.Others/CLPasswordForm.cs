using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.ClientUI.SoftReg;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class CLPasswordForm : Form
	{
		public string ApproverID = "";

		public string CustomerID = "";

		public string CustomerName = "";

		public decimal InvoiceAmount;

		private int companyID = 1;

		private IContainer components;

		private TextBox textBoxPassword;

		private Button buttonOK;

		private Button buttonCancel;

		private Line line1;

		private RadioButton radioButtonPassword;

		private RadioButton radioButtonToken;

		private TextBox textBoxToken;

		private Button buttonDownload;

		private TextBox textBoxSystemKey;

		private Button buttonRequest;

		private Panel panelToken;

		public CLPasswordForm()
		{
			InitializeComponent();
			panelToken.Enabled = false;
		}

		private void CLPasswordForm_Activated(object sender, EventArgs e)
		{
			textBoxPassword.Focus();
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (radioButtonPassword.Checked)
			{
				string cLUserApproval = Global.GetCLUserApproval(textBoxPassword.Text);
				if (cLUserApproval != "")
				{
					ApproverID = cLUserApproval;
					base.DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					ErrorHelper.ErrorMessage("Invalid Password.");
					textBoxPassword.Clear();
					textBoxPassword.Focus();
				}
			}
			else if (radioButtonToken.Checked)
			{
				WebHelper webHelper = new WebHelper();
				foreach (DataRow row in Factory.UserSystem.GetCLUserList().Tables[0].Rows)
				{
					string text = row["UserID"].ToString();
					string pass = Factory.Decrypt(row["CLUserPass"].ToString());
					if (webHelper.CreateCLToken(textBoxSystemKey.Text.Replace("-", ""), text.ToLower(), pass) == textBoxToken.Text)
					{
						ApproverID = text;
						base.DialogResult = DialogResult.OK;
						Close();
						return;
					}
				}
				ErrorHelper.WarningMessage("Incorrect token number.");
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			try
			{
				if (radioButtonToken.Checked && !buttonRequest.Enabled)
				{
					new Micromind.ClientUI.SoftReg.SoftReg().CancelCLRequest(companyID, textBoxSystemKey.Text.Replace("-", ""));
				}
				base.DialogResult = DialogResult.Cancel;
				Close();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CLPasswordForm_Load(object sender, EventArgs e)
		{
			try
			{
				int num = new Random().Next(100000, 999999);
				textBoxSystemKey.Text = num.ToString("###-###");
				base.DialogResult = DialogResult.None;
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void radioButtonToken_CheckedChanged(object sender, EventArgs e)
		{
			panelToken.Enabled = radioButtonToken.Checked;
		}

		private void radioButtonPassword_CheckedChanged(object sender, EventArgs e)
		{
			textBoxPassword.Enabled = radioButtonPassword.Checked;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				decimal num = default(decimal);
				decimal num2 = default(decimal);
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				DataRow dataRow = Factory.CustomerSystem.GetCustomerSnapBalance(CustomerID).Tables[0].Rows[0];
				decimal.TryParse(dataRow["OpenDNAmount"].ToString(), out result2);
				decimal.TryParse(dataRow["TempCL"].ToString(), out result);
				num = decimal.Parse(dataRow["CreditAmount"].ToString()) + result;
				num2 = decimal.Parse(dataRow["Balance"].ToString()) + result2;
				if (new WebHelper().RequestCreditLimit(textBoxSystemKey.Text.Replace("-", ""), CustomerID, CustomerName, InvoiceAmount, num, num2))
				{
					ErrorHelper.InformationMessage("Request sent. Please try to download the token or enter token number.");
					buttonRequest.Enabled = false;
				}
				else
				{
					ErrorHelper.ErrorMessage("Could not send request. Please try again or contact your system administrator.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void buttonDownload_Click(object sender, EventArgs e)
		{
			try
			{
				object fieldValue = Factory.DatabaseSystem.GetFieldValue("Company", "CompanyUID", "CompanyID", 1);
				int num = 1;
				if (!fieldValue.IsDBNullOrEmpty())
				{
					num = int.Parse(fieldValue.ToString());
				}
				companyID = num;
				DataSet cLToken = new Micromind.ClientUI.SoftReg.SoftReg().GetCLToken(num, textBoxSystemKey.Text.Replace("-", ""));
				if (cLToken == null || cLToken.Tables[0].Rows.Count == 0)
				{
					ErrorHelper.WarningMessage("Token not found.");
				}
				else
				{
					DataRow dataRow = cLToken.Tables[0].Rows[0];
					int result = 1;
					int.TryParse(dataRow["Status"].ToString(), out result);
					switch (result)
					{
					case 1:
						ErrorHelper.WarningMessage("The request is still pending for approval.");
						break;
					case 3:
						ErrorHelper.WarningMessage("The request is rejected.");
						break;
					case 2:
					{
						string text = dataRow["UserID"].ToString();
						string b = dataRow["TokenNumber"].ToString();
						object fieldValue2 = Factory.DatabaseSystem.GetFieldValue("Users", "CLUserPass", "UserID", text);
						string pass = "";
						if (!fieldValue2.IsDBNullOrEmpty())
						{
							pass = Factory.Decrypt(fieldValue2.ToString());
						}
						string a = new WebHelper().CreateCLToken(textBoxSystemKey.Text.Replace("-", ""), text.ToLower(), pass);
						if (a != "" && a == b)
						{
							ApproverID = text;
							base.DialogResult = DialogResult.OK;
							Close();
						}
						else
						{
							ErrorHelper.WarningMessage("Incorrect token number.");
						}
						break;
					}
					}
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
			textBoxPassword = new System.Windows.Forms.TextBox();
			buttonOK = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			radioButtonPassword = new System.Windows.Forms.RadioButton();
			radioButtonToken = new System.Windows.Forms.RadioButton();
			textBoxToken = new System.Windows.Forms.TextBox();
			textBoxSystemKey = new System.Windows.Forms.TextBox();
			buttonDownload = new System.Windows.Forms.Button();
			buttonRequest = new System.Windows.Forms.Button();
			panelToken = new System.Windows.Forms.Panel();
			panelToken.SuspendLayout();
			SuspendLayout();
			textBoxPassword.Location = new System.Drawing.Point(92, 18);
			textBoxPassword.Name = "textBoxPassword";
			textBoxPassword.Size = new System.Drawing.Size(206, 20);
			textBoxPassword.TabIndex = 1;
			textBoxPassword.UseSystemPasswordChar = true;
			buttonOK.Location = new System.Drawing.Point(131, 142);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(95, 25);
			buttonOK.TabIndex = 2;
			buttonOK.Text = "&OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.Location = new System.Drawing.Point(232, 142);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(95, 25);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += new System.EventHandler(buttonCancel_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-21, 135);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(423, 1);
			line1.TabIndex = 3;
			line1.TabStop = false;
			radioButtonPassword.AutoSize = true;
			radioButtonPassword.Checked = true;
			radioButtonPassword.Location = new System.Drawing.Point(12, 18);
			radioButtonPassword.Name = "radioButtonPassword";
			radioButtonPassword.Size = new System.Drawing.Size(74, 17);
			radioButtonPassword.TabIndex = 4;
			radioButtonPassword.TabStop = true;
			radioButtonPassword.Text = "Password:";
			radioButtonPassword.UseVisualStyleBackColor = true;
			radioButtonPassword.CheckedChanged += new System.EventHandler(radioButtonPassword_CheckedChanged);
			radioButtonToken.AutoSize = true;
			radioButtonToken.Location = new System.Drawing.Point(12, 59);
			radioButtonToken.Name = "radioButtonToken";
			radioButtonToken.Size = new System.Drawing.Size(56, 17);
			radioButtonToken.TabIndex = 4;
			radioButtonToken.Text = "Token";
			radioButtonToken.UseVisualStyleBackColor = true;
			radioButtonToken.CheckedChanged += new System.EventHandler(radioButtonToken_CheckedChanged);
			textBoxToken.Location = new System.Drawing.Point(7, 6);
			textBoxToken.Name = "textBoxToken";
			textBoxToken.Size = new System.Drawing.Size(206, 20);
			textBoxToken.TabIndex = 5;
			textBoxSystemKey.Location = new System.Drawing.Point(7, 30);
			textBoxSystemKey.Name = "textBoxSystemKey";
			textBoxSystemKey.ReadOnly = true;
			textBoxSystemKey.Size = new System.Drawing.Size(206, 20);
			textBoxSystemKey.TabIndex = 7;
			buttonDownload.Image = Micromind.ClientUI.Properties.Resources.download;
			buttonDownload.Location = new System.Drawing.Point(216, 6);
			buttonDownload.Name = "buttonDownload";
			buttonDownload.Size = new System.Drawing.Size(26, 23);
			buttonDownload.TabIndex = 6;
			buttonDownload.UseVisualStyleBackColor = true;
			buttonDownload.Click += new System.EventHandler(buttonDownload_Click);
			buttonRequest.Location = new System.Drawing.Point(7, 52);
			buttonRequest.Name = "buttonRequest";
			buttonRequest.Size = new System.Drawing.Size(72, 23);
			buttonRequest.TabIndex = 8;
			buttonRequest.Text = "Request";
			buttonRequest.UseVisualStyleBackColor = true;
			buttonRequest.Click += new System.EventHandler(button2_Click);
			panelToken.Controls.Add(buttonRequest);
			panelToken.Controls.Add(textBoxSystemKey);
			panelToken.Controls.Add(buttonDownload);
			panelToken.Controls.Add(textBoxToken);
			panelToken.Location = new System.Drawing.Point(84, 55);
			panelToken.Name = "panelToken";
			panelToken.Size = new System.Drawing.Size(244, 76);
			panelToken.TabIndex = 9;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonCancel;
			base.ClientSize = new System.Drawing.Size(334, 174);
			base.Controls.Add(panelToken);
			base.Controls.Add(radioButtonToken);
			base.Controls.Add(radioButtonPassword);
			base.Controls.Add(line1);
			base.Controls.Add(buttonCancel);
			base.Controls.Add(buttonOK);
			base.Controls.Add(textBoxPassword);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CLPasswordForm";
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Password";
			base.Activated += new System.EventHandler(CLPasswordForm_Activated);
			base.Load += new System.EventHandler(CLPasswordForm_Load);
			panelToken.ResumeLayout(false);
			panelToken.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
