using Infragistics.Win.Misc;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.WindowsForms.DataEntries.Others;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Customers
{
	public class CustomerStatementForm : Form, IForm
	{
		private bool showopenInvoices = CompanyPreferences.ShowOpenInvoices;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Button buttonOK;

		private CustomerSelector customerSelector;

		private UltraGroupBox ultraGroupBox1;

		private CheckBox checkBoxShowZero;

		private Button buttonClose;

		private DateControl dateControl1;

		private CheckBox checkBoxAging;

		private Line line1;

		private Label label1;

		private RadioButton radioButtonAccountCurrency;

		private RadioButton radioButtonBase;

		private CheckBox checkBoxPrintPDC;

		private CheckBox checkBoxConsolidated;

		private Button buttonEmail;

		private CheckBox checkBoxHold;

		private CheckBox checkBoxEmailMethod;

		private CheckBox checkBoxCoverLetter;

		private RadioButton radioButtonStyle1;

		private RadioButton radioButtonStyle2;

		private GroupBox groupBoxStyle;

		private CheckBox checkBoxShowUnsettled;

		public ScreenAreas ScreenArea => ScreenAreas.Sales;

		public int ScreenID => 2006;

		public ScreenTypes ScreenType => ScreenTypes.Transaction;

		public CustomerStatementForm()
		{
			InitializeComponent();
			base.Load += CustomerStatementForm_Load;
		}

		private void CustomerStatementForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
				_ = base.IsDisposed;
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
			}
		}

		private DataSet LoadStatement()
		{
			try
			{
				DataSet data = null;
				if (radioButtonStyle1.Checked)
				{
					data = Factory.CustomerSystem.GetCustomerStatement(dateControl1.FromDate, dateControl1.ToDate, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, radioButtonAccountCurrency.Checked, checkBoxShowZero.Checked || customerSelector.IsSingleCustomer, checkBoxConsolidated.Checked, checkBoxHold.Checked, checkBoxEmailMethod.Checked, showopenInvoices || checkBoxShowUnsettled.Checked, customerSelector.MultipleCustomers);
				}
				else if (radioButtonStyle2.Checked)
				{
					data = Factory.CustomerSystem.GetSecondCustomerStatement(dateControl1.FromDate, dateControl1.ToDate, customerSelector.FromCustomer, customerSelector.ToCustomer, customerSelector.FromClass, customerSelector.ToClass, customerSelector.FromGroup, customerSelector.ToGroup, customerSelector.FromArea, customerSelector.ToArea, customerSelector.FromCountry, customerSelector.ToCountry, radioButtonAccountCurrency.Checked, checkBoxShowZero.Checked || customerSelector.IsSingleCustomer, checkBoxConsolidated.Checked, checkBoxHold.Checked, checkBoxEmailMethod.Checked, showopenInvoices || checkBoxShowUnsettled.Checked, customerSelector.MultipleCustomers);
				}
				DataTable dataTable = new DataTable("Setting");
				dataTable.Columns.Add("PrintPDC", typeof(bool));
				dataTable.Columns.Add("PrintAging", typeof(bool));
				dataTable.Columns.Add("PrintCoverLetter", typeof(bool));
				dataTable.Rows.Add(checkBoxPrintPDC.Checked, checkBoxAging.Checked, checkBoxCoverLetter.Checked);
				data.Tables.Add(dataTable);
				ReportHelper reportHelper = new ReportHelper();
				string reportFilter = "";
				reportHelper.AddGeneralReportData(ref data, reportFilter);
				if (data.Tables.Count > 1 && radioButtonStyle1.Checked)
				{
					string baseCurrencyID = Global.BaseCurrencyID;
					data.Tables[1].Columns.Add("CurrencyNote");
					foreach (DataRow row in data.Tables[1].Rows)
					{
						if (row["CurrencyID"] != DBNull.Value && row["CurrencyID"].ToString() != baseCurrencyID)
						{
							row["CurrencyNote"] = "Cur: " + row["CurrencyID"].ToString() + "  -  Rate: " + decimal.Parse(row["CurrencyRate"].ToString()).ToString(Format.QuantityFormat);
							if (data.Tables[1].Columns.Contains("DebitFC"))
							{
								if (row["DebitFC"] != DBNull.Value && row["DebitFC"].ToString() != "" && decimal.Parse(row["DebitFC"].ToString()) != 0m)
								{
									row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Debit: " + decimal.Parse(row["DebitFC"].ToString()).ToString(Format.TotalAmountFormat);
								}
								else if (row["CreditFC"] != DBNull.Value && row["CreditFC"].ToString() != "" && decimal.Parse(row["CreditFC"].ToString()) != 0m)
								{
									row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Credit: " + decimal.Parse(row["CreditFC"].ToString()).ToString(Format.TotalAmountFormat);
								}
							}
							else if (row["Debit"] != DBNull.Value && row["Debit"].ToString() != "" && decimal.Parse(row["Debit"].ToString()) != 0m)
							{
								row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Debit: " + decimal.Parse(row["Debit"].ToString()).ToString(Format.TotalAmountFormat);
							}
							else if (row["Credit"] != DBNull.Value && row["Credit"].ToString() != "" && decimal.Parse(row["Credit"].ToString()) != 0m)
							{
								row["CurrencyNote"] = row["CurrencyNote"].ToString() + "  -  Credit: " + decimal.Parse(row["Credit"].ToString()).ToString(Format.TotalAmountFormat);
							}
						}
					}
				}
				if (data.Tables.Count > 1)
				{
					data.Tables[1].Columns.Add("SysDocTypeName");
					foreach (DataRow row2 in data.Tables[1].Rows)
					{
						if (row2["SysDocType"] != DBNull.Value)
						{
							int sysDocType = int.Parse(row2["SysDocType"].ToString());
							row2["SysDocTypeName"] = PublicFunctions.GetSysDocTypeString(sysDocType);
						}
					}
				}
				data.Tables[1].Columns.Add("Balance", typeof(decimal));
				if (data.Tables[0].Rows.Count > 0 && radioButtonStyle1.Checked)
				{
					foreach (DataRow row3 in data.Tables[0].Rows)
					{
						string str = row3["Customer Code"].ToString();
						DataRow[] array = data.Tables[1].Select("[Customer Code] ='" + str + "'");
						decimal result = default(decimal);
						decimal.TryParse(row3["Opening Balance"].ToString(), out result);
						for (int i = 0; i < array.Length; i = checked(i + 1))
						{
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							DataRow dataRow3 = array[i];
							if (dataRow3["Debit"] != DBNull.Value)
							{
								decimal.TryParse(dataRow3["Debit"].ToString(), out result2);
							}
							if (dataRow3["Credit"] != DBNull.Value)
							{
								decimal.TryParse(dataRow3["Credit"].ToString(), out result3);
							}
							result = result + result2 - result3;
							array[i]["Balance"] = result;
						}
					}
				}
				return data;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			try
			{
				DataSet data = LoadStatement();
				string defaultTemplateName = "Customer Statement";
				if (radioButtonStyle1.Checked)
				{
					defaultTemplateName = "Customer Statement";
				}
				else if (radioButtonStyle2.Checked)
				{
					defaultTemplateName = "Customer Statement Style2";
				}
				PrintHelper.PrintDocument(data, "", defaultTemplateName, SysDocTypes.None, isPrint: false, showPrintDialog: false);
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

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				new StatementData();
				DataSet data = LoadStatement();
				StatementEmailForm statementEmailForm = new StatementEmailForm();
				statementEmailForm.Show();
				statementEmailForm.LoadData(data, dateControl1.FromDate, dateControl1.ToDate);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void CustomerStatementForm_Load_1(object sender, EventArgs e)
		{
		}

		private void checkBoxShowZero_CheckedChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Customers.CustomerStatementForm));
			buttonOK = new System.Windows.Forms.Button();
			ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
			customerSelector = new Micromind.DataControls.CustomerSelector();
			checkBoxShowZero = new System.Windows.Forms.CheckBox();
			buttonClose = new System.Windows.Forms.Button();
			checkBoxAging = new System.Windows.Forms.CheckBox();
			label1 = new System.Windows.Forms.Label();
			radioButtonAccountCurrency = new System.Windows.Forms.RadioButton();
			radioButtonBase = new System.Windows.Forms.RadioButton();
			checkBoxPrintPDC = new System.Windows.Forms.CheckBox();
			checkBoxConsolidated = new System.Windows.Forms.CheckBox();
			buttonEmail = new System.Windows.Forms.Button();
			checkBoxHold = new System.Windows.Forms.CheckBox();
			checkBoxEmailMethod = new System.Windows.Forms.CheckBox();
			checkBoxCoverLetter = new System.Windows.Forms.CheckBox();
			line1 = new Micromind.UISupport.Line();
			radioButtonStyle1 = new System.Windows.Forms.RadioButton();
			radioButtonStyle2 = new System.Windows.Forms.RadioButton();
			dateControl1 = new Micromind.DataControls.DateControl();
			groupBoxStyle = new System.Windows.Forms.GroupBox();
			checkBoxShowUnsettled = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).BeginInit();
			ultraGroupBox1.SuspendLayout();
			groupBoxStyle.SuspendLayout();
			SuspendLayout();
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(288, 476);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 14;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			ultraGroupBox1.Controls.Add(customerSelector);
			ultraGroupBox1.Location = new System.Drawing.Point(12, 12);
			ultraGroupBox1.Name = "ultraGroupBox1";
			ultraGroupBox1.Size = new System.Drawing.Size(476, 187);
			ultraGroupBox1.TabIndex = 0;
			ultraGroupBox1.Text = "Customers";
			customerSelector.BackColor = System.Drawing.Color.Transparent;
			customerSelector.CustomReportFieldName = "";
			customerSelector.CustomReportKey = "";
			customerSelector.CustomReportValueType = 1;
			customerSelector.Location = new System.Drawing.Point(6, 19);
			customerSelector.Name = "customerSelector";
			customerSelector.Size = new System.Drawing.Size(432, 162);
			customerSelector.TabIndex = 0;
			checkBoxShowZero.AutoSize = true;
			checkBoxShowZero.Location = new System.Drawing.Point(12, 339);
			checkBoxShowZero.Name = "checkBoxShowZero";
			checkBoxShowZero.Size = new System.Drawing.Size(198, 17);
			checkBoxShowZero.TabIndex = 8;
			checkBoxShowZero.Text = "Include customers with zero balance";
			checkBoxShowZero.UseVisualStyleBackColor = true;
			checkBoxShowZero.CheckedChanged += new System.EventHandler(checkBoxShowZero_CheckedChanged);
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(392, 476);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 15;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			checkBoxAging.AutoSize = true;
			checkBoxAging.Checked = true;
			checkBoxAging.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxAging.Location = new System.Drawing.Point(12, 318);
			checkBoxAging.Name = "checkBoxAging";
			checkBoxAging.Size = new System.Drawing.Size(123, 17);
			checkBoxAging.TabIndex = 5;
			checkBoxAging.Text = "Print Aging Summary";
			checkBoxAging.UseVisualStyleBackColor = true;
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(9, 292);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(87, 13);
			label1.TabIndex = 2;
			label1.Text = "Report Currency:";
			radioButtonAccountCurrency.AutoSize = true;
			radioButtonAccountCurrency.Location = new System.Drawing.Point(202, 291);
			radioButtonAccountCurrency.Name = "radioButtonAccountCurrency";
			radioButtonAccountCurrency.Size = new System.Drawing.Size(105, 17);
			radioButtonAccountCurrency.TabIndex = 4;
			radioButtonAccountCurrency.Text = "Foreign Currency";
			radioButtonAccountCurrency.UseVisualStyleBackColor = true;
			radioButtonBase.AutoSize = true;
			radioButtonBase.Checked = true;
			radioButtonBase.Location = new System.Drawing.Point(102, 291);
			radioButtonBase.Name = "radioButtonBase";
			radioButtonBase.Size = new System.Drawing.Size(94, 17);
			radioButtonBase.TabIndex = 3;
			radioButtonBase.TabStop = true;
			radioButtonBase.Text = "Base Currency";
			radioButtonBase.UseVisualStyleBackColor = true;
			checkBoxPrintPDC.AutoSize = true;
			checkBoxPrintPDC.Checked = true;
			checkBoxPrintPDC.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxPrintPDC.Location = new System.Drawing.Point(153, 318);
			checkBoxPrintPDC.Name = "checkBoxPrintPDC";
			checkBoxPrintPDC.Size = new System.Drawing.Size(72, 17);
			checkBoxPrintPDC.TabIndex = 6;
			checkBoxPrintPDC.Text = "Print PDC";
			checkBoxPrintPDC.UseVisualStyleBackColor = true;
			checkBoxConsolidated.AutoSize = true;
			checkBoxConsolidated.Location = new System.Drawing.Point(12, 402);
			checkBoxConsolidated.Name = "checkBoxConsolidated";
			checkBoxConsolidated.Size = new System.Drawing.Size(240, 17);
			checkBoxConsolidated.TabIndex = 12;
			checkBoxConsolidated.Text = "Consolidated statement for all child customers";
			checkBoxConsolidated.UseVisualStyleBackColor = true;
			buttonEmail.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			buttonEmail.Location = new System.Drawing.Point(12, 476);
			buttonEmail.Name = "buttonEmail";
			buttonEmail.Size = new System.Drawing.Size(98, 24);
			buttonEmail.TabIndex = 13;
			buttonEmail.Text = "&Email...";
			buttonEmail.UseVisualStyleBackColor = true;
			buttonEmail.Click += new System.EventHandler(button1_Click);
			checkBoxHold.AutoSize = true;
			checkBoxHold.Checked = true;
			checkBoxHold.CheckState = System.Windows.Forms.CheckState.Checked;
			checkBoxHold.Location = new System.Drawing.Point(221, 341);
			checkBoxHold.Name = "checkBoxHold";
			checkBoxHold.Size = new System.Drawing.Size(191, 17);
			checkBoxHold.TabIndex = 9;
			checkBoxHold.Text = "Include customers on HOLD status";
			checkBoxHold.UseVisualStyleBackColor = true;
			checkBoxEmailMethod.AutoSize = true;
			checkBoxEmailMethod.Location = new System.Drawing.Point(12, 360);
			checkBoxEmailMethod.Name = "checkBoxEmailMethod";
			checkBoxEmailMethod.Size = new System.Drawing.Size(230, 17);
			checkBoxEmailMethod.TabIndex = 10;
			checkBoxEmailMethod.Text = "Only customers with 'Email' sending method";
			checkBoxEmailMethod.UseVisualStyleBackColor = true;
			checkBoxCoverLetter.AutoSize = true;
			checkBoxCoverLetter.Location = new System.Drawing.Point(235, 318);
			checkBoxCoverLetter.Name = "checkBoxCoverLetter";
			checkBoxCoverLetter.Size = new System.Drawing.Size(108, 17);
			checkBoxCoverLetter.TabIndex = 7;
			checkBoxCoverLetter.Text = "Print Cover Letter";
			checkBoxCoverLetter.UseVisualStyleBackColor = true;
			line1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			line1.BackColor = System.Drawing.Color.White;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(-1, 468);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(506, 1);
			line1.TabIndex = 6;
			line1.TabStop = false;
			radioButtonStyle1.AutoSize = true;
			radioButtonStyle1.Checked = true;
			radioButtonStyle1.Location = new System.Drawing.Point(6, 16);
			radioButtonStyle1.Name = "radioButtonStyle1";
			radioButtonStyle1.Size = new System.Drawing.Size(57, 17);
			radioButtonStyle1.TabIndex = 15;
			radioButtonStyle1.TabStop = true;
			radioButtonStyle1.Text = "Style 1";
			radioButtonStyle1.UseVisualStyleBackColor = true;
			radioButtonStyle2.AutoSize = true;
			radioButtonStyle2.Location = new System.Drawing.Point(69, 16);
			radioButtonStyle2.Name = "radioButtonStyle2";
			radioButtonStyle2.Size = new System.Drawing.Size(57, 17);
			radioButtonStyle2.TabIndex = 16;
			radioButtonStyle2.Text = "Style 2";
			radioButtonStyle2.UseVisualStyleBackColor = true;
			dateControl1.CustomReportFieldName = "";
			dateControl1.CustomReportKey = "";
			dateControl1.CustomReportValueType = 1;
			dateControl1.FromDate = new System.DateTime(2017, 10, 1, 0, 0, 0, 0);
			dateControl1.Location = new System.Drawing.Point(12, 216);
			dateControl1.Name = "dateControl1";
			dateControl1.SelectedPeriod = Micromind.DataControls.DatePeriods.ThisMonthToDate;
			dateControl1.Size = new System.Drawing.Size(476, 59);
			dateControl1.TabIndex = 1;
			dateControl1.ToDate = new System.DateTime(2017, 10, 18, 23, 59, 59, 59);
			groupBoxStyle.Controls.Add(radioButtonStyle2);
			groupBoxStyle.Controls.Add(radioButtonStyle1);
			groupBoxStyle.Location = new System.Drawing.Point(34, 425);
			groupBoxStyle.Name = "groupBoxStyle";
			groupBoxStyle.Size = new System.Drawing.Size(138, 39);
			groupBoxStyle.TabIndex = 17;
			groupBoxStyle.TabStop = false;
			checkBoxShowUnsettled.AutoSize = true;
			checkBoxShowUnsettled.Location = new System.Drawing.Point(12, 381);
			checkBoxShowUnsettled.Name = "checkBoxShowUnsettled";
			checkBoxShowUnsettled.Size = new System.Drawing.Size(180, 17);
			checkBoxShowUnsettled.TabIndex = 11;
			checkBoxShowUnsettled.Text = "Show only unsettled Transaction";
			checkBoxShowUnsettled.UseVisualStyleBackColor = true;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(500, 508);
			base.Controls.Add(checkBoxShowUnsettled);
			base.Controls.Add(groupBoxStyle);
			base.Controls.Add(checkBoxCoverLetter);
			base.Controls.Add(checkBoxEmailMethod);
			base.Controls.Add(checkBoxHold);
			base.Controls.Add(buttonEmail);
			base.Controls.Add(checkBoxConsolidated);
			base.Controls.Add(checkBoxPrintPDC);
			base.Controls.Add(label1);
			base.Controls.Add(radioButtonAccountCurrency);
			base.Controls.Add(radioButtonBase);
			base.Controls.Add(line1);
			base.Controls.Add(checkBoxAging);
			base.Controls.Add(dateControl1);
			base.Controls.Add(buttonClose);
			base.Controls.Add(checkBoxShowZero);
			base.Controls.Add(ultraGroupBox1);
			base.Controls.Add(buttonOK);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomerStatementForm";
			Text = "Customer Statement";
			base.Load += new System.EventHandler(CustomerStatementForm_Load_1);
			((System.ComponentModel.ISupportInitialize)ultraGroupBox1).EndInit();
			ultraGroupBox1.ResumeLayout(false);
			groupBoxStyle.ResumeLayout(false);
			groupBoxStyle.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
