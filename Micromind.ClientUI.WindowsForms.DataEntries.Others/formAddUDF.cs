using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataCaches;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class formAddUDF : Form, IForm
	{
		private UDFData currentData;

		private const string TABLENAME_CONST = "Area";

		private const string IDFIELD_CONST = "AreaID";

		private bool isNewRecord = true;

		private string udfTable = "";

		private string formName = "";

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private XPButton buttonSave;

		private MMLabel labelCode;

		private MMTextBox textBoxCode;

		private MMLabel labelSize;

		private FormManager formManager;

		private MMLabel mmLabel1;

		private MMTextBox textBoxDisplayName;

		private MMLabel mmLabel2;

		private UDFDataTypeComboBox comboBoxDataType;

		private NumericUpDown textBoxLength;

		private ComboBox comboBoxNumberType;

		private UDFListTypesComboBox comboBoxListType;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		private bool IsDirty => formManager.GetDirtyStatus();

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
					textBoxCode.ReadOnly = false;
				}
				else
				{
					textBoxCode.ReadOnly = true;
				}
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
			}
		}

		public formAddUDF()
		{
			InitializeComponent();
			AddEvents();
			base.AcceptButton = buttonSave;
		}

		public formAddUDF(string formName, string tableName)
		{
			udfTable = tableName;
			this.formName = formName;
			InitializeComponent();
			AddEvents();
			base.AcceptButton = buttonSave;
		}

		private void AddEvents()
		{
			base.Load += AreaDetailsForm_Load;
			comboBoxDataType.SelectedIndexChanged += ComboBoxDataType_SelectedIndexChanged;
			textBoxCode.Validating += textBoxCode_Validating;
		}

		private void ComboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBoxLength.Visible = false;
			comboBoxNumberType.Visible = false;
			comboBoxListType.Visible = false;
			labelSize.Visible = false;
			if (comboBoxDataType.SelectedID == 6)
			{
				comboBoxNumberType.Visible = true;
			}
			else if (comboBoxDataType.SelectedID == 2)
			{
				comboBoxListType.Visible = true;
			}
			else if (comboBoxDataType.SelectedID == 1)
			{
				textBoxLength.Visible = true;
				labelSize.Visible = true;
			}
		}

		private void textBoxCode_Validating(object sender, CancelEventArgs e)
		{
			textBoxCode.Text = textBoxCode.Text.Replace(" ", "");
		}

		private bool GetData()
		{
			try
			{
				if (currentData == null || isNewRecord)
				{
					currentData = new UDFData();
				}
				DataRow dataRow = (!isNewRecord) ? currentData.UDFTable.Rows[0] : currentData.UDFTable.NewRow();
				dataRow.BeginEdit();
				dataRow["UDFTableName"] = udfTable;
				dataRow["FormName"] = formName;
				dataRow["FieldName"] = textBoxCode.Text.Trim();
				dataRow["DisplayName"] = textBoxDisplayName.Text;
				dataRow["DataType"] = comboBoxDataType.SelectedID;
				if (comboBoxDataType.SelectedID == 1)
				{
					dataRow["FieldSize"] = textBoxLength.Value;
				}
				else if (comboBoxDataType.SelectedID == 6)
				{
					dataRow["FieldSize"] = comboBoxNumberType.SelectedIndex;
				}
				else
				{
					dataRow["FieldSize"] = 0;
				}
				if (comboBoxDataType.SelectedID == 2)
				{
					int selectedID = comboBoxListType.SelectedID;
					dataRow["ListType"] = selectedID;
					if (selectedID == 1000)
					{
						dataRow["UDListName"] = comboBoxListType.SelectedCustomListCode;
					}
				}
				dataRow.EndEdit();
				if (isNewRecord)
				{
					currentData.UDFTable.Rows.Add(dataRow);
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
			if (SaveData())
			{
				base.DialogResult = DialogResult.OK;
			}
			else
			{
				base.DialogResult = DialogResult.None;
			}
			textBoxCode.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!(id.Trim() == "") && CanClose())
				{
					if (currentData == null || currentData.Tables.Count == 0 || currentData.Tables[0].Rows.Count == 0)
					{
						ClearForm();
						IsNewRecord = true;
						textBoxCode.Text = id;
						textBoxCode.Focus();
					}
					else
					{
						FillData();
						IsNewRecord = false;
						formManager.ResetDirty();
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
		}

		private bool SaveData()
		{
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
				bool flag = Factory.UDFSystem.InsertUpdateUDFColumn(currentData, isUpdate: false);
				if (!flag)
				{
					ErrorHelper.ErrorMessage(UIMessages.UnableToSave);
					base.DialogResult = DialogResult.None;
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
			if (textBoxCode.Text.Trim().Length == 0)
			{
				ErrorHelper.InformationMessage("Please enter required fields.");
				return false;
			}
			if (!char.IsLetter(textBoxCode.Text[0]))
			{
				ErrorHelper.InformationMessage("Field name must start with a letter. Please enter a correct name starting with letter.");
				return false;
			}
			if (comboBoxDataType.SelectedItem == null)
			{
				ErrorHelper.InformationMessage("Please select the data type.");
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
			textBoxCode.Clear();
			formManager.ResetDirty();
			textBoxCode.Focus();
		}

		private void AreaGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AreaGroupDetailsForm_Validated(object sender, EventArgs e)
		{
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
				if (ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse) == DialogResult.No)
				{
					return false;
				}
				bool num = Factory.AreaSystem.DeleteArea(textBoxCode.Text);
				if (num)
				{
					ComboDataHelper.SetRefreshStatus(DataComboType.Area, needRefresh: true);
				}
				return num;
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
			LoadData(DatabaseHelper.GetNextID("Area", "AreaID", textBoxCode.Text));
		}

		private void toolStripButtonPrevious_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetPreviousID("Area", "AreaID", textBoxCode.Text));
		}

		private void toolStripButtonLast_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetLastID("Area", "AreaID"));
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
			LoadData(DatabaseHelper.GetFirstID("Area", "AreaID"));
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private bool CanClose()
		{
			if (IsDirty)
			{
				BringToFront();
				if (IsNewRecord)
				{
					switch (ErrorHelper.QuestionMessageYesNoCancel(UIMessages.DoYouWantToSave))
					{
					case DialogResult.Yes:
						if (!SaveData())
						{
							return false;
						}
						break;
					default:
						return false;
					case DialogResult.No:
						break;
					}
				}
				else if (!SaveData())
				{
					return false;
				}
			}
			return true;
		}

		private void AreaDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				comboBoxDataType.LoadData();
				comboBoxDataType.SelectedIndex = 0;
				comboBoxListType.LoadData();
				comboBoxNumberType.SelectedIndex = 1;
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
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
			new FormHelper().ShowList(DataComboType.Area);
		}

		private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.formAddUDF));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxLength = new System.Windows.Forms.NumericUpDown();
			comboBoxDataType = new Micromind.DataControls.UDFDataTypeComboBox();
			textBoxDisplayName = new Micromind.UISupport.MMTextBox();
			mmLabel2 = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			textBoxCode = new Micromind.UISupport.MMTextBox();
			labelSize = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			labelCode = new Micromind.UISupport.MMLabel();
			comboBoxNumberType = new System.Windows.Forms.ComboBox();
			comboBoxListType = new Micromind.DataControls.UDFListTypesComboBox();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)textBoxLength).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 175);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(450, 40);
			panelButtons.TabIndex = 6;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(450, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(340, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 1;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&Save";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textBoxLength.Location = new System.Drawing.Point(340, 54);
			textBoxLength.Maximum = new decimal(new int[4]
			{
				1000,
				0,
				0,
				0
			});
			textBoxLength.Minimum = new decimal(new int[4]
			{
				1,
				0,
				0,
				0
			});
			textBoxLength.Name = "textBoxLength";
			textBoxLength.Size = new System.Drawing.Size(92, 20);
			textBoxLength.TabIndex = 3;
			textBoxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLength.Value = new decimal(new int[4]
			{
				64,
				0,
				0,
				0
			});
			comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDataType.FormattingEnabled = true;
			comboBoxDataType.Location = new System.Drawing.Point(109, 53);
			comboBoxDataType.Name = "comboBoxDataType";
			comboBoxDataType.Size = new System.Drawing.Size(179, 21);
			comboBoxDataType.TabIndex = 2;
			comboBoxDataType.SelectedIndexChanged += new System.EventHandler(comboBoxDataType_SelectedIndexChanged);
			textBoxDisplayName.BackColor = System.Drawing.Color.White;
			textBoxDisplayName.CustomReportFieldName = "";
			textBoxDisplayName.CustomReportKey = "";
			textBoxDisplayName.CustomReportValueType = 1;
			textBoxDisplayName.IsComboTextBox = false;
			textBoxDisplayName.IsModified = false;
			textBoxDisplayName.Location = new System.Drawing.Point(109, 32);
			textBoxDisplayName.MaxLength = 30;
			textBoxDisplayName.Name = "textBoxDisplayName";
			textBoxDisplayName.Size = new System.Drawing.Size(324, 20);
			textBoxDisplayName.TabIndex = 1;
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(9, 32);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(75, 13);
			mmLabel2.TabIndex = 17;
			mmLabel2.Text = "Display Name:";
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
			textBoxCode.BackColor = System.Drawing.Color.White;
			textBoxCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
			textBoxCode.CustomReportFieldName = "";
			textBoxCode.CustomReportKey = "";
			textBoxCode.CustomReportValueType = 1;
			textBoxCode.IsComboTextBox = false;
			textBoxCode.IsModified = false;
			textBoxCode.Location = new System.Drawing.Point(109, 10);
			textBoxCode.MaxLength = 15;
			textBoxCode.Name = "textBoxCode";
			textBoxCode.Size = new System.Drawing.Size(179, 20);
			textBoxCode.TabIndex = 0;
			labelSize.AutoSize = true;
			labelSize.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelSize.IsFieldHeader = false;
			labelSize.IsRequired = false;
			labelSize.Location = new System.Drawing.Point(293, 57);
			labelSize.Name = "labelSize";
			labelSize.PenWidth = 1f;
			labelSize.ShowBorder = false;
			labelSize.Size = new System.Drawing.Size(43, 13);
			labelSize.TabIndex = 9;
			labelSize.Text = "Length:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = true;
			mmLabel1.Location = new System.Drawing.Point(9, 56);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(39, 13);
			mmLabel1.TabIndex = 3;
			mmLabel1.Text = "Type:";
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = true;
			labelCode.Location = new System.Drawing.Point(9, 10);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(74, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Field Name:";
			comboBoxNumberType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxNumberType.FormattingEnabled = true;
			comboBoxNumberType.Items.AddRange(new object[3]
			{
				"Integer",
				"Decimal",
				"Money"
			});
			comboBoxNumberType.Location = new System.Drawing.Point(109, 78);
			comboBoxNumberType.Name = "comboBoxNumberType";
			comboBoxNumberType.Size = new System.Drawing.Size(121, 21);
			comboBoxNumberType.TabIndex = 4;
			comboBoxListType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxListType.FormattingEnabled = true;
			comboBoxListType.Location = new System.Drawing.Point(109, 78);
			comboBoxListType.Name = "comboBoxListType";
			comboBoxListType.Size = new System.Drawing.Size(179, 21);
			comboBoxListType.TabIndex = 5;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(450, 215);
			base.Controls.Add(textBoxLength);
			base.Controls.Add(comboBoxDataType);
			base.Controls.Add(textBoxDisplayName);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(formManager);
			base.Controls.Add(textBoxCode);
			base.Controls.Add(labelSize);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Controls.Add(comboBoxListType);
			base.Controls.Add(comboBoxNumberType);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "formAddUDF";
			Text = "Add User Defined Field";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)textBoxLength).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
