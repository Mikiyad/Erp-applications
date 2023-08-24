using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Others
{
	public class ChangeUDFDataTypeForm : Form, IForm
	{
		private AreaData currentData;

		private const string TABLENAME_CONST = "Area";

		private const string IDFIELD_CONST = "AreaID";

		private bool isNewRecord = true;

		private UDFTypes udfType;

		private ScreenAccessRight screenRight;

		private IContainer components;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton buttonCancel;

		private XPButton buttonSave;

		private FormManager formManager;

		private NumericUpDown textBoxLength;

		private UDFDataTypeComboBox comboBoxDataType;

		private MMLabel mmLabel4;

		private MMLabel mmLabel1;

		public ScreenAreas ScreenArea => ScreenAreas.General;

		public int ScreenID => 6001;

		public ScreenTypes ScreenType => ScreenTypes.Card;

		public int DataLength
		{
			get
			{
				return int.Parse(textBoxLength.Value.ToString());
			}
			set
			{
				textBoxLength.Value = value;
			}
		}

		public SqlDbType DataType
		{
			get
			{
				return (SqlDbType)comboBoxDataType.SelectedItem;
			}
			set
			{
				comboBoxDataType.SelectedItem = value;
			}
		}

		public UDFTypes UDFType
		{
			get
			{
				return udfType;
			}
			set
			{
				udfType = value;
			}
		}

		private bool IsDirty => formManager.GetDirtyStatus();

		public ChangeUDFDataTypeForm()
		{
			InitializeComponent();
			AddEvents();
			comboBoxDataType.LoadData();
		}

		private void comboBoxDataType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((SqlDbType)comboBoxDataType.SelectedItem == SqlDbType.NVarChar)
			{
				textBoxLength.Enabled = true;
			}
			else
			{
				textBoxLength.Enabled = false;
			}
		}

		private void AddEvents()
		{
			base.Load += AreaDetailsForm_Load;
			comboBoxDataType.SelectedIndexChanged += comboBoxDataType_SelectedIndexChanged;
		}

		private bool GetData()
		{
			try
			{
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
			base.DialogResult = DialogResult.OK;
			comboBoxDataType.Focus();
		}

		public void LoadData(string id)
		{
			try
			{
				if (!base.IsDisposed && !(id.Trim() == ""))
				{
					currentData = Factory.AreaSystem.GetAreaByID(id.Trim());
					FillData();
					formManager.ResetDirty();
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

		private void buttonNew_Click(object sender, EventArgs e)
		{
		}

		private void ClearForm()
		{
		}

		private void AreaGroupDetailsForm_Validating(object sender, CancelEventArgs e)
		{
		}

		private void AreaGroupDetailsForm_Validated(object sender, EventArgs e)
		{
		}

		private void xpButton1_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
		}

		private void AreaDetailsForm_Load(object sender, EventArgs e)
		{
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Others.ChangeUDFDataTypeForm));
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			buttonCancel = new Micromind.UISupport.XPButton();
			buttonSave = new Micromind.UISupport.XPButton();
			textBoxLength = new System.Windows.Forms.NumericUpDown();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			comboBoxDataType = new Micromind.DataControls.UDFDataTypeComboBox();
			formManager = new Micromind.DataControls.FormManager();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)textBoxLength).BeginInit();
			SuspendLayout();
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(buttonCancel);
			panelButtons.Controls.Add(buttonSave);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 77);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(390, 36);
			panelButtons.TabIndex = 2;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(390, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			buttonCancel.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonCancel.BackColor = System.Drawing.Color.DarkGray;
			buttonCancel.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonCancel.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonCancel.Location = new System.Drawing.Point(282, 6);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(96, 24);
			buttonCancel.TabIndex = 1;
			buttonCancel.Text = "&Cancel";
			buttonCancel.UseVisualStyleBackColor = false;
			buttonCancel.Click += new System.EventHandler(xpButton1_Click);
			buttonSave.AdjustImageLocation = new System.Drawing.Point(0, 0);
			buttonSave.BackColor = System.Drawing.Color.Silver;
			buttonSave.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			buttonSave.BtnStyle = Micromind.UISupport.XPStyle.Default;
			buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			buttonSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			buttonSave.Location = new System.Drawing.Point(181, 6);
			buttonSave.Name = "buttonSave";
			buttonSave.Size = new System.Drawing.Size(96, 24);
			buttonSave.TabIndex = 0;
			buttonSave.Text = "&OK";
			buttonSave.UseVisualStyleBackColor = false;
			buttonSave.Click += new System.EventHandler(buttonSave_Click);
			textBoxLength.Location = new System.Drawing.Point(283, 21);
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
			textBoxLength.TabIndex = 1;
			textBoxLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			textBoxLength.Value = new decimal(new int[4]
			{
				30,
				0,
				0,
				0
			});
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(242, 23);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(43, 13);
			mmLabel4.TabIndex = 23;
			mmLabel4.Text = "Length:";
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(12, 23);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(34, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Type:";
			comboBoxDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			comboBoxDataType.FormattingEnabled = true;
			comboBoxDataType.Location = new System.Drawing.Point(57, 20);
			comboBoxDataType.Name = "comboBoxDataType";
			comboBoxDataType.Size = new System.Drawing.Size(179, 21);
			comboBoxDataType.TabIndex = 0;
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
			base.AcceptButton = buttonSave;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(390, 113);
			base.Controls.Add(textBoxLength);
			base.Controls.Add(comboBoxDataType);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ChangeUDFDataTypeForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Change Data Type";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)textBoxLength).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
