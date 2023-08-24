using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.ClientUI.Libraries;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class AxolonTestForm : Form
	{
		private Button buttonSecurity;

		private Label label1;

		private Label label2;

		private Button button3;

		private TextBox textBox2;

		private TextBox textBox3;

		private Label label3;

		private Label label4;

		private Button button1;

		private IContainer components;

		public AxolonTestForm()
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
			buttonSecurity = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			button3 = new System.Windows.Forms.Button();
			textBox2 = new System.Windows.Forms.TextBox();
			textBox3 = new System.Windows.Forms.TextBox();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			button1 = new System.Windows.Forms.Button();
			SuspendLayout();
			buttonSecurity.Location = new System.Drawing.Point(12, 109);
			buttonSecurity.Name = "buttonSecurity";
			buttonSecurity.Size = new System.Drawing.Size(204, 27);
			buttonSecurity.TabIndex = 0;
			buttonSecurity.Text = "General Security Missing Forms";
			buttonSecurity.UseVisualStyleBackColor = true;
			buttonSecurity.Click += new System.EventHandler(buttonSecurity_Click);
			label1.Location = new System.Drawing.Point(12, 71);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(447, 35);
			label1.TabIndex = 1;
			label1.Text = "Generate list of forms that are missing in the permission list for forms. These forms should be added to FormHelper->GetSecurityFormsList()";
			label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			label2.Location = new System.Drawing.Point(12, 9);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(447, 24);
			label2.TabIndex = 1;
			label2.Text = "This screen provides options to test the application before release.";
			button3.Location = new System.Drawing.Point(54, 459);
			button3.Name = "button3";
			button3.Size = new System.Drawing.Size(151, 37);
			button3.TabIndex = 5;
			button3.Text = "Update Cost";
			button3.UseVisualStyleBackColor = true;
			button3.Click += new System.EventHandler(button3_Click);
			textBox2.Location = new System.Drawing.Point(54, 421);
			textBox2.Name = "textBox2";
			textBox2.Size = new System.Drawing.Size(151, 20);
			textBox2.TabIndex = 6;
			textBox3.Location = new System.Drawing.Point(211, 421);
			textBox3.Name = "textBox3";
			textBox3.Size = new System.Drawing.Size(148, 20);
			textBox3.TabIndex = 7;
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(51, 405);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(58, 13);
			label3.TabIndex = 8;
			label3.Text = "SysDocID:";
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(208, 405);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(61, 13);
			label4.TabIndex = 8;
			label4.Text = "VoucherID:";
			button1.Location = new System.Drawing.Point(54, 225);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(151, 37);
			button1.TabIndex = 9;
			button1.Text = "Update Summary Table";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click_2);
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(730, 582);
			base.Controls.Add(button1);
			base.Controls.Add(label4);
			base.Controls.Add(label3);
			base.Controls.Add(textBox3);
			base.Controls.Add(textBox2);
			base.Controls.Add(button3);
			base.Controls.Add(label2);
			base.Controls.Add(label1);
			base.Controls.Add(buttonSecurity);
			base.Name = "AxolonTestForm";
			Text = "Axolon Functional Test";
			base.Load += new System.EventHandler(TestForm_Load);
			ResumeLayout(false);
			PerformLayout();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
		}

		private void button2_Click(object sender, EventArgs e)
		{
		}

		private ArrayList GetNonSecurityList()
		{
			return new ArrayList
			{
				"BackupScheduleForm",
				"LoginForm",
				"DatabaseLoginForm",
				"FeedbackForm",
				"UpgradeDatabaseForm",
				"NewCompanyForm",
				"ChangePasswordForm",
				"UserGroupAssignForm",
				"CustomGadgetDetailForm",
				"PivotGroupDetailsForm",
				"FilePickerForm",
				"PivotDetailsForm",
				"UpdateCOGSForm",
				"ActivityDetailsForm",
				"CustomerVendorLinkForm",
				"NationalAccountForm",
				"EmployeeProjectSettingForm",
				"LocationAccountsForm",
				"JobFeeForm",
				"MatrixQuantityForm",
				"CreateMatrixComponent",
				"MatrixSelectionForm",
				"CompanyAddressDetailsForm",
				"formAddUDF",
				"ChangeUDFDisplayNameForm",
				"ChangeUDFDataTypeForm",
				"DocManagementForm",
				"UDFSetupForm",
				"GenericListDetailsForm",
				"FiscalYearDetailsForm",
				"CurrencyConvertor",
				"UserOptionsForm",
				"SmartListDetailsForm",
				"NewSmartlistGroup",
				"SmartListForm",
				"TransactionListForm",
				"POSCashRegisterPaymentMethodsForm",
				"POSCashRegisterDetailsForm",
				"POSLocationDetailsForm",
				"ImportFromExcelForm",
				"CalculateSalaryForm",
				"ProductPriceListForm",
				"ProductQuantityForm",
				"ApprovalPasswordForm",
				"ImageViewerForm",
				"GeneralListForm",
				"test",
				"GeneralDetailsForm",
				"CheckReminderSetting",
				"PrintPreviewForm",
				"FTPConnProps",
				"HTTPConnProps",
				"formHome",
				"AboutForm",
				"HelpSupportForm",
				"RegisterForm",
				"ScreenNoteForm",
				"SplashForm",
				"ErrorHelperForm",
				"RegisterModuleForm",
				"formPOSHome",
				"DataAccuracyTestForm",
				"JobEquipmentDetailForm",
				"JobBudgetDetailForm",
				"JobFeeDetailForm",
				"VendorOpeningBalanceBatchForm",
				"UnallocatedPaymentsListForm",
				"EntityCategoryDetailsForm",
				"ProductSizeDetailsForm",
				"Test2",
				"AxolonTestForm"
			};
		}

		private void buttonSecurity_Click(object sender, EventArgs e)
		{
			StreamWriter streamWriter = null;
			try
			{
				streamWriter = new StreamWriter("C:\\x\\SecurityFileList.txt");
				DataSet securityFormList = new FormHelper().GetSecurityFormList();
				ArrayList nonSecurityList = GetNonSecurityList();
				Type[] types = GetType().Assembly.GetTypes();
				foreach (Type type in types)
				{
					Form form = null;
					if (type.BaseType == typeof(Form) || type.BaseType == typeof(DialogBoxBaseForm))
					{
						_ = type.Name;
						form = UIRefelector.GetForm(type);
						if (form != null && !form.Name.Contains("Report") && !form.Name.Contains("Dialog") && !nonSecurityList.Contains(form.Name) && securityFormList.Tables["Screen"].Select("ScreenID = '" + form.Name + "'").Length == 0)
						{
							streamWriter.WriteLine("screenTable.Rows.Add(\"" + form.Name + "\", \"" + form.Text + "\", ScreenAreas.General.ToString(), ScreenTypes.List.ToString());");
						}
					}
				}
				MessageBox.Show("Completed.", "Complete", MessageBoxButtons.OK);
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
			finally
			{
				streamWriter?.Close();
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			try
			{
				Factory.EmailMessageSystem.ProcessEmails();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			DataSet dsTransactions = Factory.EntityDocSystem.TempDataset();
			Factory.EntityDocSystem.GetNotExistingDocs(dsTransactions);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				if (Factory.SalesPOSSystem.ReCostTransaction(textBox2.Text, textBox3.Text))
				{
					ErrorHelper.InformationMessage("Recost done successfully.");
				}
				else
				{
					ErrorHelper.ErrorMessage("Recost failed.");
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void button1_Click_2(object sender, EventArgs e)
		{
			try
			{
				MessageBox.Show(Factory.FiscalYearSystem.CreateSummaryTable("SYS200", "FISC001", "2015").ToString());
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}
	}
}
