using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class SMSAlertForm : Form, IForm
	{
		private EventData currentData;

		private const string TABLENAME_CONST = "Events";

		private const string IDFIELD_CONST = "EventID";

		private bool isNewRecord = true;

		private string smsType = "";

		private string smsMessage = "";

		private string customerID = "";

		private string smsRemainder = "";

		private ScreenAccessRight screenRight;

		private DataSet companyInformation;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonDelete;

		private XPButton xpButton1;

		private XPButton buttonNew;

		private XPButton buttonSave;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private ToolStripButton toolStripButtonOpenList;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonAttach;

		private MMTextBox textBoxNote;

		private ToolStripButton toolStripButtonInformation;

		private XPButton buttonAdd;

		private ListBox checkedListBoxTo;

		private GroupBox groupBox1;

		private XPButton xpButton2;

		private GroupBox groupBox2;

		private XPButton xpButton3;

		private CheckBox checkBoxupdate;

		private RadioButton radioButtonArabic;

		private RadioButton radioButtonEnglish;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

		public string CustomerID
		{
			get
			{
				return customerID;
			}
			set
			{
				customerID = value;
			}
		}

		public string SMSType
		{
			get
			{
				return smsType;
			}
			set
			{
				smsType = value;
			}
		}

		public string SMSMessage
		{
			get
			{
				return smsMessage;
			}
			set
			{
				smsMessage = value;
			}
		}

		public string SMSRemainder
		{
			get
			{
				return smsRemainder;
			}
			set
			{
				smsRemainder = value;
			}
		}

		private bool IsNewRecord
		{
			get
			{
				return isNewRecord;
			}
			set
			{
				isNewRecord = value;
				if (value)
				{
					buttonNew.Text = UIMessages.ClearButtonText;
					buttonDelete.Enabled = false;
				}
				else
				{
					buttonNew.Text = UIMessages.NewButtonText;
					buttonDelete.Enabled = true;
				}
				toolStripButtonAttach.Enabled = !value;
				if (!screenRight.New && isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else if (!screenRight.Edit && !isNewRecord)
				{
					buttonSave.Enabled = false;
				}
				else
				{
					buttonSave.Enabled = true;
				}
				if (!screenRight.Delete)
				{
					buttonDelete.Enabled = false;
				}
			}
		}

		public SMSAlertForm()
		{
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += EventDetailsForm_Load;
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new EventData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.EventTable.Rows[0] : currentData.EventTable.NewRow();
				dataRow.BeginEdit();
				dataRow["Note"] = textBoxNote.Text;
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.EventTable.Rows.Add(dataRow);
				}
				currentData.EventEmployeeTable.Rows.Clear();
				foreach (object item in checkedListBoxTo.Items)
				{
					_ = item;
					DataRow dataRow2 = currentData.EventEmployeeTable.NewRow();
					dataRow2.EndEdit();
					currentData.EventEmployeeTable.Rows.Add(dataRow2);
				}
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
				if (!base.IsDisposed && !(id.Trim() == ""))
				{
					if (customerID != "")
					{
						buttonAdd.Visible = false;
						base.Size = new Size(493, 330);
						DataSet dataSet = new DataSet();
						List<string> list = new List<string>();
						dataSet = Factory.CustomerSystem.GetCustomerMobileNo(customerID);
						foreach (NameValue item3 in checkedListBoxTo.Items)
						{
							if (item3.ID != null && item3.ID != "")
							{
								string item = item3.ID + item3.Name;
								if (!list.Contains(item))
								{
									list.Add(item);
								}
							}
						}
						foreach (DataRow row in dataSet.Tables[0].Rows)
						{
							string text = row["Doc ID"].ToString();
							string text2 = row["Number"].ToString();
							bool flag = false;
							foreach (NameValue item4 in checkedListBoxTo.Items)
							{
								if (item4.ID + item4.Name == text + text2)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								NameValue item2 = new NameValue(text2, text);
								if (text != "")
								{
									checkedListBoxTo.Items.Add(item2);
								}
								else
								{
									MessageBox.Show("Please input valid Mobile No:" + text2);
								}
							}
						}
					}
					if (SMSType == "Thanks")
					{
						textBoxNote.Text = Global.GetRegistryOptionValue("SMStxt", isEncrypt: true);
					}
					else if (SMSType == "Remainder")
					{
						checkBoxupdate.Visible = true;
						textBoxNote.Text = smsMessage;
					}
					else if (SMSType == "Alert")
					{
						textBoxNote.Text = Global.GetRegistryOptionValue("SMSalert", isEncrypt: true);
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			if (currentData != null && currentData.Tables.Count != 0 && currentData.Tables[0].Rows.Count != 0)
			{
				DataRow dataRow = currentData.Tables[0].Rows[0];
				textBoxNote.Text = dataRow["Note"].ToString();
				checkedListBoxTo.Items.Clear();
				if (currentData.Tables.Contains("Event_Employee"))
				{
					foreach (DataRow row in currentData.Tables["Event_Employee"].Rows)
					{
						NameValue item = new NameValue(row["Number"].ToString(), row["Doc ID"].ToString());
						checkedListBoxTo.Items.Add(item);
					}
				}
			}
		}

		private bool SaveData()
		{
			if (!IsDirty)
			{
				if (!IsNewRecord)
				{
					IsNewRecord = true;
					ClearForm();
				}
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
				bool flag;
				if (isNewRecord)
				{
					flag = Factory.EventSystem.CreateEvent(currentData);
					if (flag)
					{
						ComboDataHelper.SetRefreshStatus(DataComboType.Event, needRefresh: true);
					}
				}
				else
				{
					flag = Factory.EventSystem.UpdateEvent(currentData);
				}
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
				}
				else
				{
					IsNewRecord = true;
					ClearForm();
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
			if (!screenRight.New && isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionNew);
				return false;
			}
			if (!screenRight.Edit && !isNewRecord)
			{
				ErrorHelper.WarningMessage(UIMessages.NoPermissionEdit);
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
			checkedListBoxTo.Items.Clear();
			Application.DoEvents();
			formManager.ResetDirty();
		}

		private void buttonDelete_Click(object sender, EventArgs e)
		{
			if (Delete())
			{
				ClearForm();
				IsNewRecord = true;
			}
		}

		private bool Delete()
		{
			try
			{
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to delete the record?") == DialogResult.No)
				{
					return false;
				}
				return true;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void toolStripButtonNext_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Events", "EventID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Events", "EventID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Events", "EventID", toolStripTextBoxFind.Text.Trim()))
				{
					LoadData(toolStripTextBoxFind.Text.Trim());
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				if (MessageBox.Show("Do you really want to exit?", "SMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					e.Cancel = false;
				}
				else
				{
					e.Cancel = true;
				}
			}
			else
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				return true;
			}
			return false;
		}

		private void EventDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetSecurity();
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

		private void SetSecurity()
		{
			screenRight = Security.GetScreenAccessRight(base.Name);
			if (!screenRight.View)
			{
				ErrorHelper.ErrorMessage(UIMessages.NoPermissionView);
				Close();
			}
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Event);
		}

		private void toolStripButtonAttach_Click(object sender, EventArgs e)
		{
			try
			{
				if (!isNewRecord)
				{
					new DocManagementForm().ShowDialog(this);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void toolStripButtonInformation_Click(object sender, EventArgs e)
		{
		}

		private void buttonAdd_Click(object sender, EventArgs e)
		{
			DataSet dataSet = new DataSet();
			List<string> list = new List<string>();
			dataSet = Factory.CustomerSystem.GetSMSCustomerList();
			foreach (NameValue item3 in checkedListBoxTo.Items)
			{
				if (item3.ID != null && item3.ID != "")
				{
					string item = item3.ID + item3.Name;
					if (!list.Contains(item))
					{
						list.Add(item);
					}
				}
			}
			SelectDocumentDialog selectDocumentDialog = new SelectDocumentDialog();
			selectDocumentDialog.DataSource = dataSet;
			selectDocumentDialog.IsMultiSelect = true;
			selectDocumentDialog.SelectedDocuments = list;
			selectDocumentDialog.Text = "Select Customer";
			DialogResult num = selectDocumentDialog.ShowDialog(this);
			checkedListBoxTo.Items.Clear();
			if (num == DialogResult.OK)
			{
				list = selectDocumentDialog.SelectedDocuments;
				foreach (UltraGridRow selectedRow in selectDocumentDialog.SelectedRows)
				{
					string text = selectedRow.Cells["Doc ID"].Value.ToString();
					string text2 = selectedRow.Cells["Number"].Value.ToString();
					bool flag = false;
					foreach (NameValue item4 in checkedListBoxTo.Items)
					{
						if (item4.ID + item4.Name == text + text2)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						NameValue item2 = new NameValue(text2, text);
						if (text != "")
						{
							checkedListBoxTo.Items.Add(item2);
						}
						else
						{
							MessageBox.Show("Please input valid Mobile No:" + text2);
						}
					}
				}
			}
		}

		private void form_ValidateSelection(object sender, EventArgs e)
		{
			foreach (UltraGridRow row in (sender as SelectDocumentDialog).Grid.Rows)
			{
				if (row.Cells["C"].Value != null && row.Cells["C"].Value.ToString() != "" && bool.Parse(row.Cells["C"].Value.ToString()))
				{
					row.Cells["Customer"].Value.ToString();
				}
			}
		}

		private void xpButton2_Click(object sender, EventArgs e)
		{
			SMSCAPI sMSCAPI = new SMSCAPI();
			companyInformation = Factory.CompanyInformationSystem.GetCompanyInformation();
			string text = "";
			string text2 = "";
			string text3 = "";
			text = companyInformation.Tables[0].Rows[0]["SMSUserName"].ToString();
			text2 = companyInformation.Tables[0].Rows[0]["SMSPassword"].ToString();
			text3 = companyInformation.Tables[0].Rows[0]["SMSMobileNo"].ToString();
			string text4 = "";
			string mtype = "";
			if (radioButtonEnglish.Checked)
			{
				mtype = "N";
			}
			else if (radioButtonArabic.Checked)
			{
				mtype = "LNG";
			}
			foreach (object item in checkedListBoxTo.Items)
			{
				NameValue nameValue = item as NameValue;
				if (!checkBoxupdate.Checked)
				{
					text4 = sMSCAPI.SendSMS(text, text2, nameValue.ID, textBoxNote.Text.Trim(), mtype, "Y", text3);
				}
			}
			if (text4.Contains("OK") || text4.Contains("~B"))
			{
				text4 = "Message sent Successfully";
			}
			if (SMSType == "Thanks")
			{
				Global.SaveRegistryOptionValue("SMStxt", textBoxNote.Text.ToString(), isEncrypt: true);
			}
			else
			{
				if (SMSType == "Remainder" && checkBoxupdate.Checked)
				{
					if (checkBoxupdate.Checked)
					{
						Global.SaveRegistryOptionValue("SMSrem", textBoxNote.Text.ToString(), isEncrypt: true);
					}
					ErrorHelper.InformationMessage("Message updated");
					return;
				}
				if (SMSType == "Alert")
				{
					Global.SaveRegistryOptionValue("SMSalert", textBoxNote.Text.ToString(), isEncrypt: true);
				}
			}
			ErrorHelper.InformationMessage(text4);
		}

		private void xpButton3_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void checkBoxupdate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxupdate.Checked)
			{
				xpButton2.Text = "Update";
				textBoxNote.Text = Global.GetRegistryOptionValue("SMSrem", isEncrypt: true);
			}
			else
			{
				textBoxNote.Text = smsMessage;
				xpButton2.Text = "Send";
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.SMSAlertForm));
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonOpenList = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonAttach = new System.Windows.Forms.ToolStripButton();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonDelete = new Micromind.UISupport.XPButton();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonNew = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			checkedListBoxTo = new System.Windows.Forms.ListBox();
			groupBox1 = new System.Windows.Forms.GroupBox();
			groupBox2 = new System.Windows.Forms.GroupBox();
			textBoxNote = new Micromind.UISupport.MMTextBox();
			xpButton2 = new Micromind.UISupport.XPButton();
			buttonAdd = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			xpButton3 = new Micromind.UISupport.XPButton();
			checkBoxupdate = new System.Windows.Forms.CheckBox();
			radioButtonArabic = new System.Windows.Forms.RadioButton();
			radioButtonEnglish = new System.Windows.Forms.RadioButton();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			groupBox1.SuspendLayout();
			groupBox2.SuspendLayout();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[13]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButtonOpenList,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonAttach,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(20, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(457, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(49, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(23, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(23, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(23, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(23, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButtonOpenList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonOpenList.Image = Micromind.ClientUI.Properties.Resources.list;
			toolStripButtonOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonOpenList.Name = "toolStripButtonOpenList";
			toolStripButtonOpenList.Size = new System.Drawing.Size(23, 22);
			toolStripButtonOpenList.Text = "Open List";
			toolStripButtonOpenList.Click += new System.EventHandler(toolStripButtonOpenList_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(47, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonAttach.Image = Micromind.ClientUI.Properties.Resources.attach_24x24;
			toolStripButtonAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonAttach.Name = "toolStripButtonAttach";
			toolStripButtonAttach.Size = new System.Drawing.Size(78, 22);
			toolStripButtonAttach.Text = "Attach File";
			toolStripButtonAttach.Click += new System.EventHandler(toolStripButtonAttach_Click);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(23, 22);
			toolStripButtonInformation.Text = "Document Information";
			toolStripButtonInformation.Click += new System.EventHandler(toolStripButtonInformation_Click);
			panelButtons.Anchor = System.Windows.Forms.AnchorStyles.None;
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonDelete);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonNew);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Location = new System.Drawing.Point(8, 110);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(457, 25);
			panelButtons.TabIndex = 8;
			panelButtons.Visible = false;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(457, 1);
			linePanelDown.TabIndex = 1;
			linePanelDown.TabStop = false;
			linePanelDown.Visible = false;
			buttonDelete.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonDelete.BackColor = System.Drawing.Color.DarkGray;
			buttonDelete.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonDelete.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonDelete.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonDelete.Location = new System.Drawing.Point(216, 8);
			buttonDelete.Name = "buttonDelete";
			buttonDelete.Size = new System.Drawing.Size(96, 24);
			buttonDelete.TabIndex = 9;
			buttonDelete.Text = "De&lete";
			buttonDelete.UseVisualStyleBackColor = false;
			buttonDelete.Click += new System.EventHandler(buttonDelete_Click);
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(335, 5);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 10;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			buttonNew.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonNew.BackColor = System.Drawing.Color.DarkGray;
			buttonNew.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonNew.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonNew.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonNew.Location = new System.Drawing.Point(114, 8);
			buttonNew.Name = "buttonNew";
			buttonNew.Size = new System.Drawing.Size(96, 24);
			buttonNew.TabIndex = 8;
			buttonNew.Text = "Ne&w...";
			buttonNew.UseVisualStyleBackColor = false;
			buttonNew.Click += new System.EventHandler(buttonNew_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(12, 8);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 7;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			checkedListBoxTo.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			checkedListBoxTo.FormattingEnabled = true;
			checkedListBoxTo.Location = new System.Drawing.Point(5, 13);
			checkedListBoxTo.Name = "checkedListBoxTo";
			checkedListBoxTo.Size = new System.Drawing.Size(383, 56);
			checkedListBoxTo.TabIndex = 128;
			groupBox1.Controls.Add(checkedListBoxTo);
			groupBox1.Location = new System.Drawing.Point(7, 4);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new System.Drawing.Size(393, 77);
			groupBox1.TabIndex = 129;
			groupBox1.TabStop = false;
			groupBox1.Text = "To";
			groupBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			groupBox2.Controls.Add(textBoxNote);
			groupBox2.Controls.Add(panelButtons);
			groupBox2.Location = new System.Drawing.Point(7, 80);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new System.Drawing.Size(393, 188);
			groupBox2.TabIndex = 130;
			groupBox2.TabStop = false;
			groupBox2.Text = "Message";
			textBoxNote.BackColor = System.Drawing.Color.White;
			textBoxNote.CustomReportFieldName = "";
			textBoxNote.CustomReportKey = "";
			textBoxNote.CustomReportValueType = 1;
			textBoxNote.IsComboTextBox = false;
			textBoxNote.Location = new System.Drawing.Point(7, 15);
			textBoxNote.MaxLength = 255;
			textBoxNote.Multiline = true;
			textBoxNote.Name = "textBoxNote";
			textBoxNote.Size = new System.Drawing.Size(379, 166);
			textBoxNote.TabIndex = 7;
			textBoxNote.Text = "Thank you for choosing our service.\r\nPlease Come Again!";
			xpButton2.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton2.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			xpButton2.BackColor = System.Drawing.Color.DarkGray;
			xpButton2.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton2.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton2.Location = new System.Drawing.Point(129, 270);
			xpButton2.Name = "xpButton2";
			xpButton2.Size = new System.Drawing.Size(96, 24);
			xpButton2.TabIndex = 130;
			xpButton2.Text = "&Send";
			xpButton2.UseVisualStyleBackColor = false;
			xpButton2.Click += new System.EventHandler(xpButton2_Click);
			buttonAdd.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
			buttonAdd.BackColor = System.Drawing.Color.DarkGray;
			buttonAdd.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAdd.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAdd.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAdd.Location = new System.Drawing.Point(403, 24);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new System.Drawing.Size(69, 24);
			buttonAdd.TabIndex = 45;
			buttonAdd.Text = "&Add";
			buttonAdd.UseVisualStyleBackColor = false;
			buttonAdd.Click += new System.EventHandler(buttonAdd_Click);
			formManager.BackColor = System.Drawing.Color.RosyBrown;
			formManager.Dock = System.Windows.Forms.DockStyle.Left;
			formManager.IsForcedDirty = false;
			formManager.Location = new System.Drawing.Point(0, 0);
			formManager.MaximumSize = new System.Drawing.Size(20, 20);
			formManager.MinimumSize = new System.Drawing.Size(20, 20);
			formManager.Name = "formManager";
			formManager.Size = new System.Drawing.Size(20, 20);
			formManager.TabIndex = 16;
			formManager.Text = "formManager1";
			formManager.Visible = false;
			xpButton3.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			xpButton3.BackColor = System.Drawing.Color.DarkGray;
			xpButton3.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton3.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton3.Location = new System.Drawing.Point(231, 270);
			xpButton3.Name = "xpButton3";
			xpButton3.Size = new System.Drawing.Size(96, 24);
			xpButton3.TabIndex = 131;
			xpButton3.Text = "&Cancel";
			xpButton3.UseVisualStyleBackColor = false;
			xpButton3.Click += new System.EventHandler(xpButton3_Click);
			checkBoxupdate.AutoSize = true;
			checkBoxupdate.Location = new System.Drawing.Point(413, 274);
			checkBoxupdate.Name = "checkBoxupdate";
			checkBoxupdate.Size = new System.Drawing.Size(59, 17);
			checkBoxupdate.TabIndex = 132;
			checkBoxupdate.Text = "update";
			checkBoxupdate.UseVisualStyleBackColor = true;
			checkBoxupdate.Visible = false;
			checkBoxupdate.CheckedChanged += new System.EventHandler(checkBoxupdate_CheckedChanged);
			radioButtonArabic.AutoSize = true;
			radioButtonArabic.Location = new System.Drawing.Point(408, 85);
			radioButtonArabic.Name = "radioButtonArabic";
			radioButtonArabic.Size = new System.Drawing.Size(55, 17);
			radioButtonArabic.TabIndex = 134;
			radioButtonArabic.Text = "Arabic";
			radioButtonArabic.UseVisualStyleBackColor = true;
			radioButtonEnglish.AutoSize = true;
			radioButtonEnglish.Checked = true;
			radioButtonEnglish.Location = new System.Drawing.Point(407, 62);
			radioButtonEnglish.Name = "radioButtonEnglish";
			radioButtonEnglish.Size = new System.Drawing.Size(59, 17);
			radioButtonEnglish.TabIndex = 133;
			radioButtonEnglish.TabStop = true;
			radioButtonEnglish.Text = "English";
			radioButtonEnglish.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = xpButton3;
			base.ClientSize = new System.Drawing.Size(477, 299);
			base.Controls.Add(radioButtonArabic);
			base.Controls.Add(radioButtonEnglish);
			base.Controls.Add(checkBoxupdate);
			base.Controls.Add(xpButton3);
			base.Controls.Add(xpButton2);
			base.Controls.Add(buttonAdd);
			base.Controls.Add(toolStrip1);
			base.Controls.Add(groupBox1);
			base.Controls.Add(groupBox2);
			base.Controls.Add(formManager);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "SMSAlertForm";
			Text = "SMS Reminder";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			groupBox1.ResumeLayout(false);
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
