using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerBalanceSnapDialog : Form
	{
		private IContainer components;

		private Label label1;

		private Label label2;

		private MMTextBox textBoxCode;

		private MMTextBox textBoxName;

		private Label label3;

		private Label label4;

		private Label label5;

		private Line line1;

		private Label label6;

		private Label label7;

		private Label label8;

		private MMTextBox textBoxCreditStatus;

		private MMTextBox textBoxStatus;

		private Button buttonClose;

		private MMTextBox textBoxBalance;

		private MMTextBox textBoxPDC;

		private MMTextBox textBoxNet;

		private Line line2;

		private MMTextBox textBoxNote;

		private MMTextBox textBoxLimit;

		private MMTextBox textBoxRemarks;

		private Label label9;

		private Label label10;

		private MMTextBox mmTextBox1;

		private MMTextBox textBoxUninvoiced;

		private Label label11;

		private MMTextBox textBoxInsAmount;

		private Label labelInsuredAmnt;

		private MMTextBox textBoxDueAmount;

		private Label label13;

		public CustomerBalanceSnapDialog()
		{
			InitializeComponent();
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		public void LoadData(string customerCode)
		{
			try
			{
				textBoxCode.Text = customerCode;
				DataSet customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(textBoxCode.Text);
				CreditLimitTypes creditLimitTypes = CreditLimitTypes.Unlimited;
				bool flag = false;
				bool flag2 = false;
				bool companyOption = CompanyOptions.GetCompanyOption(CompanyOptionsEnum.PDCByMaturity, defaultValue: true);
				bool isPDCInclude = CompanyPreferences.IsPDCInclude;
				bool flag3 = true;
				if (customerSnapBalance != null && customerSnapBalance.Tables.Count > 0)
				{
					DataRow dataRow = customerSnapBalance.Tables["Customer"].Rows[0];
					string text = "";
					if (dataRow["ParentCustomerID"] != DBNull.Value)
					{
						text = dataRow["ParentCustomerID"].ToString();
					}
					textBoxName.Text = dataRow["CustomerName"].ToString();
					if (dataRow["IsInactive"] != DBNull.Value)
					{
						flag = bool.Parse(dataRow["IsInactive"].ToString());
					}
					if (dataRow["IsHold"] != DBNull.Value)
					{
						flag2 = bool.Parse(dataRow["IsHold"].ToString());
					}
					if (dataRow["CreditLimitType"] != DBNull.Value)
					{
						creditLimitTypes = (CreditLimitTypes)byte.Parse(dataRow["CreditLimitType"].ToString());
					}
					if (creditLimitTypes == CreditLimitTypes.ParentSublimit && text != "")
					{
						customerSnapBalance = Factory.CustomerSystem.GetCustomerSnapBalance(text);
						dataRow = customerSnapBalance.Tables["Customer"].Rows[0];
						textBoxNote.Text = "Sublimit of : " + dataRow["CustomerID"].ToString() + "-" + dataRow["CustomerName"].ToString();
						textBoxNote.Visible = true;
						creditLimitTypes = (CreditLimitTypes)((dataRow["CreditLimitType"] == DBNull.Value) ? 1 : byte.Parse(dataRow["CreditLimitType"].ToString()));
					}
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal num = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					bool result6 = false;
					decimal result7 = default(decimal);
					decimal.TryParse(dataRow["Balance"].ToString(), out result);
					decimal.TryParse(dataRow["CreditAmount"].ToString(), out result2);
					decimal.TryParse(dataRow["PDCAmount"].ToString(), out result3);
					decimal.TryParse(dataRow["TempCL"].ToString(), out result4);
					decimal.TryParse(dataRow["PDCUnsecuredLimitAmount"].ToString(), out result5);
					bool.TryParse(dataRow["LimitPDCUnsecured"].ToString(), out result6);
					decimal.TryParse(dataRow["InsApprovedAmount"].ToString(), out result7);
					textBoxInsAmount.Text = result7.ToString(Format.TotalAmountFormat);
					DateTime t = DateTime.MaxValue;
					if (dataRow["CLValidity"] != DBNull.Value)
					{
						t = DateTime.Parse(dataRow["CLValidity"].ToString()).EndOfDay();
					}
					textBoxBalance.Text = result.ToString(Format.TotalAmountFormat);
					textBoxPDC.Text = result3.ToString(Format.TotalAmountFormat);
					decimal result8 = default(decimal);
					if (flag3 && dataRow["OpenDNAmount"] != DBNull.Value)
					{
						decimal.TryParse(dataRow["OpenDNAmount"].ToString(), out result8);
					}
					textBoxUninvoiced.Text = result8.ToString(Format.TotalAmountFormat);
					num = ((!companyOption) ? result : (result - result3));
					if (isPDCInclude && companyOption)
					{
						result3 = default(decimal);
					}
					if (!isPDCInclude && !companyOption)
					{
						result3 = default(decimal);
					}
					result = ((!companyOption) ? (result + result8 + result3) : (result + result8 - result3));
					decimal d = result2 - result + result4;
					if (result6 && d > result5)
					{
						d = result5;
					}
					if (creditLimitTypes != CreditLimitTypes.Unlimited)
					{
						mmTextBox1.Text = d.ToString(Format.TotalAmountFormat);
					}
					else
					{
						mmTextBox1.Text = "Unlimited";
					}
					textBoxNet.Text = num.ToString(Format.TotalAmountFormat);
					if (creditLimitTypes == CreditLimitTypes.Unlimited)
					{
						textBoxLimit.Text = "Unlimited";
					}
					else if (dataRow["CreditAmount"] != DBNull.Value)
					{
						textBoxLimit.Text = (decimal.Parse(dataRow["CreditAmount"].ToString()) + result4).ToString(Format.TotalAmountFormat);
					}
					else
					{
						textBoxLimit.Text = 0.ToString(Format.TotalAmountFormat);
					}
					if ((creditLimitTypes != CreditLimitTypes.Unlimited && result >= result2 + result4) || t < DateTime.Now)
					{
						textBoxCreditStatus.Text = "Over Limit";
						textBoxCreditStatus.ForeColor = Color.Red;
						if (t < DateTime.Now)
						{
							textBoxRemarks.Text = "Credit limit validity is expired.";
						}
					}
					else if (result6 && result - result3 > result5)
					{
						textBoxCreditStatus.Text = "Over Unsecured Limit";
						textBoxCreditStatus.ForeColor = Color.Red;
						textBoxPDC.ForeColor = Color.Red;
						textBoxRemarks.Text = "Not enough PDC against the balance.";
					}
					else
					{
						textBoxCreditStatus.Text = "Available";
						textBoxCreditStatus.ForeColor = Color.DarkGreen;
					}
					if (!flag && dataRow["IsInactive"] != DBNull.Value)
					{
						flag = bool.Parse(dataRow["IsInactive"].ToString());
					}
					if (flag2 && dataRow["IsHold"] != DBNull.Value)
					{
						flag2 = bool.Parse(dataRow["IsHold"].ToString());
					}
					textBoxStatus.ForeColor = Color.Red;
					if (flag)
					{
						textBoxStatus.Text = "Inactive";
					}
					else if (flag2)
					{
						textBoxStatus.Text = "On Hold";
					}
					else
					{
						textBoxStatus.Text = "Active";
						textBoxStatus.ForeColor = Color.DarkGreen;
					}
					GetAgingValue(customerCode);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void GetAgingValue(string customerCode)
		{
			DataSet customerAgingSummary = Factory.CustomerSystem.GetCustomerAgingSummary(customerCode, customerCode, "", "", "", "", "", "", "", "", DateTime.Now, showZeroBalance: false, "", "", "", "", "", "", "", "", "", "", "");
			if (customerAgingSummary != null && customerAgingSummary.Tables.Count > 0 && customerAgingSummary.Tables[0].Rows.Count > 0)
			{
				DataRow dataRow = customerAgingSummary.Tables[0].Rows[0];
				decimal result = default(decimal);
				decimal result2 = default(decimal);
				decimal.TryParse(dataRow["CurrentBalance"].ToString(), out result);
				decimal.TryParse(dataRow["Total"].ToString(), out result2);
				decimal num = result2 - result;
				textBoxDueAmount.Text = num.ToString(Format.TotalAmountFormat);
			}
		}

		private void CustomerBalanceSnapDialog_Load(object sender, EventArgs e)
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
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			label7 = new System.Windows.Forms.Label();
			label8 = new System.Windows.Forms.Label();
			buttonClose = new System.Windows.Forms.Button();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			line2 = new Micromind.UISupport.Line();
			textBoxNet = new Micromind.UISupport.MMTextBox();
			textBoxPDC = new Micromind.UISupport.MMTextBox();
			textBoxBalance = new Micromind.UISupport.MMTextBox();
			textBoxStatus = new Micromind.UISupport.MMTextBox();
			textBoxCreditStatus = new Micromind.UISupport.MMTextBox();
			line1 = new Micromind.UISupport.Line();
			textBoxName = new Micromind.UISupport.MMTextBox();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			textBoxLimit = new Micromind.UISupport.MMTextBox();
			textBoxRemarks = new Micromind.UISupport.MMTextBox();
			label9 = new System.Windows.Forms.Label();
			label10 = new System.Windows.Forms.Label();
			mmTextBox1 = new Micromind.UISupport.MMTextBox();
			textBoxUninvoiced = new Micromind.UISupport.MMTextBox();
			label11 = new System.Windows.Forms.Label();
			textBoxInsAmount = new Micromind.UISupport.MMTextBox();
			labelInsuredAmnt = new System.Windows.Forms.Label();
			textBoxDueAmount = new Micromind.UISupport.MMTextBox();
			label13 = new System.Windows.Forms.Label();
			SuspendLayout();
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(11, 11);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(35, 13);
			label1.TabIndex = 0;
			label1.Text = "Code:";
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(11, 33);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 0;
			label2.Text = "Name:";
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(11, 110);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(49, 13);
			label3.TabIndex = 3;
			label3.Text = "Balance:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(11, 158);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(32, 13);
			label4.TabIndex = 4;
			label4.Text = "PDC:";
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(11, 182);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(27, 13);
			label5.TabIndex = 6;
			label5.Text = "Net:";
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(217, 79);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(70, 13);
			label6.TabIndex = 11;
			label6.Text = "Credit Status:";
			label7.AutoSize = true;
			label7.Location = new System.Drawing.Point(217, 110);
			label7.Name = "label7";
			label7.Size = new System.Drawing.Size(61, 13);
			label7.TabIndex = 10;
			label7.Text = "Credit Limit:";
			label8.AutoSize = true;
			label8.Location = new System.Drawing.Point(11, 79);
			label8.Name = "label8";
			label8.Size = new System.Drawing.Size(40, 13);
			label8.TabIndex = 13;
			label8.Text = "Status:";
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(347, 236);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(77, 23);
			buttonClose.TabIndex = 11;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			textBoxNote.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.IsModified = false;
			textBoxNote.Location = new System.Drawing.Point(78, 53);
			textBoxNote.Name = "textBoxNote";
			textBoxNote.ReadOnly = true;
			textBoxNote.Size = new System.Drawing.Size(342, 20);
			textBoxNote.TabIndex = 2;
			line2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			line2.BackColor = System.Drawing.Color.White;
			line2.DrawWidth = 1;
			line2.IsVertical = false;
			line2.LineBackColor = System.Drawing.Color.Black;
			line2.Location = new System.Drawing.Point(-50, 229);
			line2.Name = "line2";
			line2.Size = new System.Drawing.Size(490, 1);
			line2.TabIndex = 14;
			line2.TabStop = false;
			textBoxNet.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxNet.CustomReportFieldName = "";
			textBoxNet.CustomReportKey = "";
			textBoxNet.CustomReportValueType = 1;
			textBoxNet.IsComboTextBox = false;
			textBoxNet.IsModified = false;
			textBoxNet.Location = new System.Drawing.Point(78, 178);
			textBoxNet.Name = "textBoxNet";
			textBoxNet.ReadOnly = true;
			textBoxNet.Size = new System.Drawing.Size(123, 20);
			textBoxNet.TabIndex = 6;
			textBoxNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxPDC.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxPDC.CustomReportFieldName = "";
			textBoxPDC.CustomReportKey = "";
			textBoxPDC.CustomReportValueType = 1;
			textBoxPDC.IsComboTextBox = false;
			textBoxPDC.IsModified = false;
			textBoxPDC.Location = new System.Drawing.Point(78, 154);
			textBoxPDC.Name = "textBoxPDC";
			textBoxPDC.ReadOnly = true;
			textBoxPDC.Size = new System.Drawing.Size(123, 20);
			textBoxPDC.TabIndex = 5;
			textBoxPDC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxBalance.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxBalance.CustomReportFieldName = "";
			textBoxBalance.CustomReportKey = "";
			textBoxBalance.CustomReportValueType = 1;
			textBoxBalance.IsComboTextBox = false;
			textBoxBalance.IsModified = false;
			textBoxBalance.Location = new System.Drawing.Point(78, 106);
			textBoxBalance.Name = "textBoxBalance";
			textBoxBalance.ReadOnly = true;
			textBoxBalance.Size = new System.Drawing.Size(123, 20);
			textBoxBalance.TabIndex = 4;
			textBoxBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxStatus.CustomReportFieldName = "";
			textBoxStatus.CustomReportKey = "";
			textBoxStatus.CustomReportValueType = 1;
			textBoxStatus.IsComboTextBox = false;
			textBoxStatus.IsModified = false;
			textBoxStatus.Location = new System.Drawing.Point(78, 76);
			textBoxStatus.Name = "textBoxStatus";
			textBoxStatus.ReadOnly = true;
			textBoxStatus.Size = new System.Drawing.Size(123, 20);
			textBoxStatus.TabIndex = 3;
			textBoxCreditStatus.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCreditStatus.CustomReportFieldName = "";
			textBoxCreditStatus.CustomReportKey = "";
			textBoxCreditStatus.CustomReportValueType = 1;
			textBoxCreditStatus.IsComboTextBox = false;
			textBoxCreditStatus.IsModified = false;
			textBoxCreditStatus.Location = new System.Drawing.Point(297, 76);
			textBoxCreditStatus.Name = "textBoxCreditStatus";
			textBoxCreditStatus.ReadOnly = true;
			textBoxCreditStatus.Size = new System.Drawing.Size(123, 20);
			textBoxCreditStatus.TabIndex = 8;
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = true;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(211, 132);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(1, 69);
			line1.TabIndex = 7;
			line1.TabStop = false;
			textBoxName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxName.CustomReportFieldName = "";
			textBoxName.CustomReportKey = "";
			textBoxName.CustomReportValueType = 1;
			textBoxName.IsComboTextBox = false;
			textBoxName.IsModified = false;
			textBoxName.Location = new System.Drawing.Point(78, 30);
			textBoxName.Name = "textBoxName";
			textBoxName.ReadOnly = true;
			textBoxName.Size = new System.Drawing.Size(342, 20);
			textBoxName.TabIndex = 1;
			textBoxCode.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(78, 8);
			textBoxCode.Name = "textBoxCode";
			textBoxCode.ReadOnly = true;
			textBoxCode.Size = new System.Drawing.Size(132, 20);
			textBoxCode.TabIndex = 0;
			textBoxLimit.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxLimit.CustomReportFieldName = "";
			textBoxLimit.CustomReportKey = "";
			textBoxLimit.CustomReportValueType = 1;
			textBoxLimit.IsComboTextBox = false;
			textBoxLimit.IsModified = false;
			textBoxLimit.Location = new System.Drawing.Point(297, 106);
			textBoxLimit.Name = "textBoxLimit";
			textBoxLimit.ReadOnly = true;
			textBoxLimit.Size = new System.Drawing.Size(123, 20);
			textBoxLimit.TabIndex = 7;
			textBoxLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxRemarks.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxRemarks.CustomReportFieldName = "";
			textBoxRemarks.CustomReportKey = "";
			textBoxRemarks.CustomReportValueType = 1;
			textBoxRemarks.IsComboTextBox = false;
			textBoxRemarks.IsModified = false;
			textBoxRemarks.Location = new System.Drawing.Point(78, 203);
			textBoxRemarks.Name = "textBoxRemarks";
			textBoxRemarks.ReadOnly = true;
			textBoxRemarks.Size = new System.Drawing.Size(343, 20);
			textBoxRemarks.TabIndex = 10;
			label9.AutoSize = true;
			label9.Location = new System.Drawing.Point(11, 206);
			label9.Name = "label9";
			label9.Size = new System.Drawing.Size(52, 13);
			label9.TabIndex = 15;
			label9.Text = "Remarks:";
			label10.AutoSize = true;
			label10.Location = new System.Drawing.Point(217, 182);
			label10.Name = "label10";
			label10.Size = new System.Drawing.Size(53, 13);
			label10.TabIndex = 15;
			label10.Text = "Available:";
			mmTextBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			mmTextBox1.CustomReportFieldName = "";
			mmTextBox1.CustomReportKey = "";
			mmTextBox1.CustomReportValueType = 1;
			mmTextBox1.IsComboTextBox = false;
			mmTextBox1.IsModified = false;
			mmTextBox1.Location = new System.Drawing.Point(297, 178);
			mmTextBox1.Name = "mmTextBox1";
			mmTextBox1.ReadOnly = true;
			mmTextBox1.Size = new System.Drawing.Size(123, 20);
			mmTextBox1.TabIndex = 9;
			mmTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxUninvoiced.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxUninvoiced.CustomReportFieldName = "";
			textBoxUninvoiced.CustomReportKey = "";
			textBoxUninvoiced.CustomReportValueType = 1;
			textBoxUninvoiced.IsComboTextBox = false;
			textBoxUninvoiced.IsModified = false;
			textBoxUninvoiced.Location = new System.Drawing.Point(297, 154);
			textBoxUninvoiced.Name = "textBoxUninvoiced";
			textBoxUninvoiced.ReadOnly = true;
			textBoxUninvoiced.Size = new System.Drawing.Size(123, 20);
			textBoxUninvoiced.TabIndex = 16;
			textBoxUninvoiced.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label11.AutoSize = true;
			label11.Location = new System.Drawing.Point(217, 158);
			label11.Name = "label11";
			label11.Size = new System.Drawing.Size(64, 13);
			label11.TabIndex = 17;
			label11.Text = "Uninvoiced:";
			textBoxInsAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxInsAmount.CustomReportFieldName = "";
			textBoxInsAmount.CustomReportKey = "";
			textBoxInsAmount.CustomReportValueType = 1;
			textBoxInsAmount.IsComboTextBox = false;
			textBoxInsAmount.IsModified = false;
			textBoxInsAmount.Location = new System.Drawing.Point(78, 130);
			textBoxInsAmount.Name = "textBoxInsAmount";
			textBoxInsAmount.ReadOnly = true;
			textBoxInsAmount.Size = new System.Drawing.Size(123, 20);
			textBoxInsAmount.TabIndex = 19;
			textBoxInsAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			labelInsuredAmnt.AutoSize = true;
			labelInsuredAmnt.Location = new System.Drawing.Point(11, 134);
			labelInsuredAmnt.Name = "labelInsuredAmnt";
			labelInsuredAmnt.Size = new System.Drawing.Size(66, 13);
			labelInsuredAmnt.TabIndex = 18;
			labelInsuredAmnt.Text = "Ins. Amount:";
			textBoxDueAmount.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxDueAmount.CustomReportFieldName = "";
			textBoxDueAmount.CustomReportKey = "";
			textBoxDueAmount.CustomReportValueType = 1;
			textBoxDueAmount.IsComboTextBox = false;
			textBoxDueAmount.IsModified = false;
			textBoxDueAmount.Location = new System.Drawing.Point(297, 130);
			textBoxDueAmount.Name = "textBoxDueAmount";
			textBoxDueAmount.ReadOnly = true;
			textBoxDueAmount.Size = new System.Drawing.Size(123, 20);
			textBoxDueAmount.TabIndex = 21;
			textBoxDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			label13.AutoSize = true;
			label13.Location = new System.Drawing.Point(217, 134);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(69, 13);
			label13.TabIndex = 20;
			label13.Text = "Due Amount:";
			base.AcceptButton = buttonClose;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(436, 262);
			base.Controls.Add(textBoxDueAmount);
			base.Controls.Add(label13);
			base.Controls.Add(textBoxInsAmount);
			base.Controls.Add(labelInsuredAmnt);
			base.Controls.Add(textBoxUninvoiced);
			base.Controls.Add(label11);
			base.Controls.Add(mmTextBox1);
			base.Controls.Add(label10);
			base.Controls.Add(textBoxRemarks);
			base.Controls.Add(label9);
			base.Controls.Add(textBoxLimit);
			base.Controls.Add(textBoxNote);
			base.Controls.Add(line2);
			base.Controls.Add(textBoxNet);
			base.Controls.Add(textBoxPDC);
			base.Controls.Add(textBoxBalance);
			base.Controls.Add(buttonClose);
			base.Controls.Add(textBoxStatus);
			base.Controls.Add(textBoxCreditStatus);
			base.Controls.Add(label8);
			base.Controls.Add(label6);
			base.Controls.Add(label7);
			base.Controls.Add(line1);
			base.Controls.Add(label5);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(textBoxName);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomerBalanceSnapDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Customer Balance";
			base.Load += new System.EventHandler(CustomerBalanceSnapDialog_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
