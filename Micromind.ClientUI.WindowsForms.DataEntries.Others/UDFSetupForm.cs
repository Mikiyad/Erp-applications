using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class UDFSetupForm : Form, IForm
	{
		private string FormName = "";

		private string TableName = "";

		private DataSet currentData;

		private bool isNewRecord = true;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private MMLabel labelCode;

		private FormManager formManager;

		private DataGridList dataGridList;

		private XPButton buttonAddField;

		private ContextMenuStrip contextMenuStrip;

		private ToolStripMenuItem addNewFieldToolStripMenuItem;

		private ToolStripMenuItem deleteFieldToolStripMenuItem;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripMenuItem editFieldToolStripMenuItem;

		private ToolStripMenuItem changeDataTypeToolStripMenuItem;

		private TextBox textBoxFormName;

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
			}
		}

		public UDFSetupForm()
		{
			InitializeComponent();
			AddEvents();
			buttonAddField.Enabled = false;
		}

		public UDFSetupForm(string formName, string tableName)
		{
			FormName = formName;
			TableName = tableName;
			InitializeComponent();
			AddEvents();
		}

		private void AddEvents()
		{
			base.Load += UDFSetupForm_Load;
		}

		private void comboBoxUDFType_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadData();
			buttonAddField.Enabled = true;
			addNewFieldToolStripMenuItem.Enabled = true;
		}

		private bool GetData()
		{
			try
			{
				new UDFData().UDFTable.NewRow();
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

		public void LoadData()
		{
			try
			{
				currentData = Factory.UDFSystem.GetUDFList(TableName);
				FillData();
				IsNewRecord = false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		private void FillData()
		{
			DataTable dataTable = dataGridList.DataSource as DataTable;
			dataTable.Rows.Clear();
			foreach (DataRow row in currentData.Tables[0].Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["FieldName"] = row["FieldName"];
				dataRow2["DisplayName"] = row["DisplayName"];
				dataRow2["DataType"] = row["DataType"];
				int result = 0;
				int.TryParse(row["FieldSize"].ToString(), out result);
				switch (byte.Parse(row["DataType"].ToString()))
				{
				case 1:
					dataRow2["Description"] = "Length = " + result.ToString();
					break;
				case 6:
					switch (result)
					{
					case 1:
						dataRow2["Description"] = "Integer";
						break;
					case 2:
						dataRow2["Description"] = "Decimal";
						break;
					case 3:
						dataRow2["Description"] = "Money";
						break;
					}
					break;
				case 2:
					dataRow2["Description"] = row["UDListName"].ToString();
					break;
				}
				dataTable.Rows.Add(dataRow2);
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
				_ = isNewRecord;
				return true;
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
			(dataGridList.DataSource as DataTable)?.Rows.Clear();
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
				ErrorHelper.QuestionMessageYesNo(UIMessages.DocumentNumberInUse);
				_ = 7;
				return false;
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

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void UDFSetupForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
				SetSecurity();
				if (!base.IsDisposed)
				{
					IsNewRecord = true;
					ClearForm();
					LoadData();
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

		private void SetupGrid()
		{
			dataGridList.ApplyUIDesign();
			DataTable dataTable = new DataSet().Tables.Add("UDF_Setup");
			dataTable.Columns.Add("FieldName");
			dataTable.Columns.Add("DisplayName");
			dataTable.Columns.Add("DataType", typeof(byte));
			dataTable.Columns.Add("Description", typeof(string));
			dataGridList.DataSource = dataTable;
			ValueList valueList = new ValueList();
			valueList.ValueListItems.Add(1, "Text");
			valueList.ValueListItems.Add(7, "CheckBox");
			valueList.ValueListItems.Add(4, "Date");
			valueList.ValueListItems.Add(2, "List");
			valueList.ValueListItems.Add(3, "Note");
			valueList.ValueListItems.Add(6, "Number");
			valueList.ValueListItems.Add(5, "Time");
			dataGridList.DisplayLayout.Bands[0].Columns["DataType"].ValueList = valueList;
		}

		private void toolStripButtonOpenList_Click(object sender, EventArgs e)
		{
			new FormHelper().ShowList(DataComboType.Area);
		}

		private void buttonAddField_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.None;
			if (new formAddUDF(FormName, TableName).ShowDialog() == DialogResult.OK)
			{
				LoadData();
			}
		}

		private void addNewFieldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			buttonAddField_Click(sender, e);
		}

		private void deleteFieldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string text = "";
			if (dataGridList.ActiveRow != null)
			{
				text = dataGridList.ActiveRow.Cells["Column_Name"].Value.ToString();
			}
			if (!(text == "") && ErrorHelper.QuestionMessageYesNo("Deleting this field may cause losing information. Are you sure you want to delete this field?") == DialogResult.Yes && ErrorHelper.WarningMessageOkCancel("All information saved in this field will be permanently deleted. Click OK to delete this field.") == DialogResult.OK)
			{
				Factory.UDFSystem.DeleteUDFColumn(text, TableName);
				dataGridList.ActiveRow.Delete(displayPrompt: false);
			}
		}

		private void editFieldToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				if (dataGridList.ActiveRow != null)
				{
					text = dataGridList.ActiveRow.Cells["Column_Name"].Value.ToString();
				}
				if (!(text == ""))
				{
					ChangeUDFDisplayNameForm changeUDFDisplayNameForm = new ChangeUDFDisplayNameForm();
					changeUDFDisplayNameForm.DisplayName = text;
					if (changeUDFDisplayNameForm.ShowDialog() == DialogResult.OK && Factory.UDFSystem.UpdateUDFDisplayName(text, TableName, changeUDFDisplayNameForm.DisplayName))
					{
						dataGridList.ActiveRow.Cells["DisplayName"].Value = changeUDFDisplayNameForm.DisplayName;
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void changeDataTypeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				string text = "";
				string value = "";
				int result = 30;
				if (dataGridList.ActiveRow != null)
				{
					text = dataGridList.ActiveRow.Cells["Column_Name"].Value.ToString();
					value = dataGridList.ActiveRow.Cells["Data_Type"].Value.ToString();
					int.TryParse(dataGridList.ActiveRow.Cells["Length"].Value.ToString(), out result);
					if (result < 1)
					{
						result = 1;
					}
				}
				if (!(text == ""))
				{
					ChangeUDFDataTypeForm changeUDFDataTypeForm = new ChangeUDFDataTypeForm();
					changeUDFDataTypeForm.DataType = (SqlDbType)Enum.Parse(typeof(SqlDbType), value, ignoreCase: true);
					changeUDFDataTypeForm.DataLength = result;
					if (changeUDFDataTypeForm.ShowDialog() == DialogResult.OK)
					{
						Factory.UDFSystem.UpdateUDFColumnDataType(text, TableName, changeUDFDataTypeForm.DataType, changeUDFDataTypeForm.DataLength);
						LoadData();
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.GetType() == typeof(SqlException))
				{
					if (((SqlException)ex).Number == 257)
					{
						ErrorHelper.ErrorMessage("Changing the data type to selected type is not allowed.");
					}
				}
				else
				{
					ErrorHelper.ProcessError(ex);
				}
			}
		}

		private void UDFSetupForm_Load_1(object sender, EventArgs e)
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
			components = new System.ComponentModel.Container();
			Infragistics.Win.Appearance appearance = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.UDFSetupForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			labelCode = new Micromind.UISupport.MMLabel();
			formManager = new Micromind.DataControls.FormManager();
			contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(components);
			addNewFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			deleteFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			editFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			changeDataTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			buttonAddField = new Micromind.UISupport.XPButton();
			dataGridList = new Micromind.UISupport.DataGridList(components);
			textBoxFormName = new System.Windows.Forms.TextBox();
			panelButtons.SuspendLayout();
			contextMenuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGridList).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 488);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(728, 40);
			panelButtons.TabIndex = 3;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(728, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(618, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 0;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
			labelCode.AutoSize = true;
			labelCode.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelCode.IsFieldHeader = false;
			labelCode.IsRequired = false;
			labelCode.Location = new System.Drawing.Point(15, 15);
			labelCode.Name = "labelCode";
			labelCode.PenWidth = 1f;
			labelCode.ShowBorder = false;
			labelCode.Size = new System.Drawing.Size(33, 13);
			labelCode.TabIndex = 1;
			labelCode.Text = "Form:";
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
			contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[5]
			{
				addNewFieldToolStripMenuItem,
				deleteFieldToolStripMenuItem,
				toolStripSeparator1,
				editFieldToolStripMenuItem,
				changeDataTypeToolStripMenuItem
			});
			contextMenuStrip.Name = "contextMenuStrip";
			contextMenuStrip.Size = new System.Drawing.Size(201, 98);
			addNewFieldToolStripMenuItem.Name = "addNewFieldToolStripMenuItem";
			addNewFieldToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			addNewFieldToolStripMenuItem.Text = "New Field...";
			addNewFieldToolStripMenuItem.Click += new System.EventHandler(addNewFieldToolStripMenuItem_Click);
			deleteFieldToolStripMenuItem.Name = "deleteFieldToolStripMenuItem";
			deleteFieldToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			deleteFieldToolStripMenuItem.Text = "Delete Field";
			deleteFieldToolStripMenuItem.Click += new System.EventHandler(deleteFieldToolStripMenuItem_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(197, 6);
			editFieldToolStripMenuItem.Name = "editFieldToolStripMenuItem";
			editFieldToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			editFieldToolStripMenuItem.Text = "Change Display Name...";
			editFieldToolStripMenuItem.Click += new System.EventHandler(editFieldToolStripMenuItem_Click);
			changeDataTypeToolStripMenuItem.Name = "changeDataTypeToolStripMenuItem";
			changeDataTypeToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
			changeDataTypeToolStripMenuItem.Text = "Change Data Type...";
			changeDataTypeToolStripMenuItem.Click += new System.EventHandler(changeDataTypeToolStripMenuItem_Click);
			buttonAddField.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonAddField.BackColor = System.Drawing.Color.DarkGray;
			buttonAddField.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonAddField.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonAddField.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonAddField.Image = Micromind.ClientUI.Properties.Resources.add;
			buttonAddField.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonAddField.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonAddField.Location = new System.Drawing.Point(12, 67);
			buttonAddField.Name = "buttonAddField";
			buttonAddField.Size = new System.Drawing.Size(116, 28);
			buttonAddField.TabIndex = 1;
			buttonAddField.Text = "New Field...";
			buttonAddField.UseVisualStyleBackColor = false;
			buttonAddField.Click += new System.EventHandler(buttonAddField_Click);
			dataGridList.AllowUnfittedView = false;
			dataGridList.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			dataGridList.ContextMenuStrip = contextMenuStrip;
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGridList.DisplayLayout.Appearance = appearance;
			dataGridList.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGridList.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGridList.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGridList.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGridList.DisplayLayout.MaxColScrollRegions = 1;
			dataGridList.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridList.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridList.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGridList.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGridList.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGridList.DisplayLayout.Override.CellAppearance = appearance8;
			dataGridList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGridList.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGridList.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGridList.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGridList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGridList.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGridList.DisplayLayout.Override.RowAppearance = appearance11;
			dataGridList.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGridList.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGridList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGridList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGridList.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGridList.LoadLayoutFailed = false;
			dataGridList.Location = new System.Drawing.Point(12, 97);
			dataGridList.Name = "dataGridList";
			dataGridList.ShowDeleteMenu = false;
			dataGridList.ShowMinusInRed = true;
			dataGridList.ShowNewMenu = false;
			dataGridList.Size = new System.Drawing.Size(702, 385);
			dataGridList.TabIndex = 2;
			dataGridList.Text = "dataGridList1";
			textBoxFormName.Location = new System.Drawing.Point(57, 12);
			textBoxFormName.Name = "textBoxFormName";
			textBoxFormName.Size = new System.Drawing.Size(280, 20);
			textBoxFormName.TabIndex = 17;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(728, 528);
			base.Controls.Add(textBoxFormName);
			base.Controls.Add(buttonAddField);
			base.Controls.Add(dataGridList);
			base.Controls.Add(formManager);
			base.Controls.Add(labelCode);
			base.Controls.Add(panelButtons);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(375, 275);
			base.Name = "UDFSetupForm";
			Text = "Setup User Defined Fields";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(UDFSetupForm_Load_1);
			panelButtons.ResumeLayout(false);
			contextMenuStrip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGridList).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
