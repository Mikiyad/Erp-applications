using DevExpress.Utils;
using DevExpress.XtraLayout;
using DevExpress.XtraReports.UI;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinToolTip;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Libraries;
using Micromind.ClientUI.Reports.CustomReports;
using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Reports
{
	public class CustomReportDisplayForm : Form
	{
		private string currentReportID = "";

		private CustomReportData currentData;

		private CustomReport currentReport;

		private IContainer components;

		private Button buttonClose;

		private Button buttonOK;

		private Panel panel1;

		private Line line1;

		private LayoutControl layoutControl;

		private LayoutControlGroup layoutControlGroup1;

		private Button button1;

		private UltraPictureBox ultraPictureBoxInformation;

		private UltraToolTipManager ultraToolTipManager1;

		public bool IsDesignMode
		{
			get;
			set;
		}

		public CustomReportDisplayForm()
		{
			InitializeComponent();
			base.Load += CustomReportDisplayForm_Load;
		}

		private void CustomReportDisplayForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
				if (IsDesignMode)
				{
					layoutControl.ShowCustomizationForm();
				}
				else
				{
					button1.Visible = false;
				}
				foreach (object item in layoutControl.Items)
				{
					if (!(item.GetType() == typeof(LayoutControlGroup)))
					{
						LayoutControlItem layoutControlItem = item as LayoutControlItem;
						if ((layoutControlItem.Control as ICustomReportControl).GetType() == typeof(MMSelectionBox))
						{
							foreach (Control control in layoutControlItem.Control.Controls)
							{
								if (control.GetType() == typeof(Button))
								{
									control.Click += ButtonSelect_Click;
								}
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void LoadReport(string reportID)
		{
			try
			{
				currentReportID = reportID;
				CustomReportData customReportData = currentData = Factory.CustomReportSystem.GetCustomReportByID(reportID);
				layoutControl.Clear();
				DataRow dataRow = customReportData.Tables[0].Rows[0];
				Text = dataRow["CustomReportName"].ToString();
				SetToolTip(dataRow["DisplayNote"].ToString());
				if (dataRow["FormWidth"] != DBNull.Value)
				{
					base.Width = int.Parse(dataRow["FormWidth"].ToString());
				}
				if (dataRow["FormHeight"] != DBNull.Value)
				{
					base.Height = int.Parse(dataRow["FormHeight"].ToString());
				}
				CustomReport customReport = currentReport = (CustomReport)Global.DeserializeFromStream((byte[])dataRow["ReportData"]);
				foreach (CustomReportControl control in customReport.Controls)
				{
					control.ControlName = control.ControlType.ToString() + customReport.Controls.IndexOf(control);
					switch (control.ControlType)
					{
					case CRCTypes.DateRange:
					{
						DateControl c61 = new DateControl();
						AddControl(control, c61);
						break;
					}
					case CRCTypes.CustomerSelectionBox:
					{
						CustomerSelector c60 = new CustomerSelector();
						AddControl(control, c60);
						break;
					}
					case CRCTypes.VendorSelectionBox:
					{
						VendorSelector c59 = new VendorSelector();
						AddControl(control, c59);
						break;
					}
					case CRCTypes.ProductSelectionBox:
					{
						ProductSelector c58 = new ProductSelector();
						AddControl(control, c58);
						break;
					}
					case CRCTypes.EmployeeSelectionBox:
					{
						EmployeeSelector c57 = new EmployeeSelector();
						AddControl(control, c57);
						break;
					}
					case CRCTypes.LocationSelectionBox:
					{
						LocationSelector c56 = new LocationSelector();
						AddControl(control, c56);
						break;
					}
					case CRCTypes.AccountSelectionBox:
					{
						AccountSelector c55 = new AccountSelector();
						AddControl(control, c55);
						break;
					}
					case CRCTypes.AccountAnalysisList:
					{
						AnalysisComboBox c54 = new AnalysisComboBox();
						AddControl(control, c54);
						break;
					}
					case CRCTypes.AccountGroupList:
					{
						AccountGroupComboBox c53 = new AccountGroupComboBox();
						AddControl(control, c53);
						break;
					}
					case CRCTypes.AccountsList:
					{
						AllAccountsComboBox c52 = new AllAccountsComboBox();
						AddControl(control, c52);
						break;
					}
					case CRCTypes.AreaList:
					{
						AreaComboBox c51 = new AreaComboBox();
						AddControl(control, c51);
						break;
					}
					case CRCTypes.BankFacilityGroupList:
					{
						BankFacilityGroupComboBox c50 = new BankFacilityGroupComboBox();
						AddControl(control, c50);
						break;
					}
					case CRCTypes.BankFacilityList:
					{
						BankFacilityComboBox c49 = new BankFacilityComboBox();
						AddControl(control, c49);
						break;
					}
					case CRCTypes.BankList:
					{
						BankComboBox c48 = new BankComboBox();
						AddControl(control, c48);
						break;
					}
					case CRCTypes.BuyerList:
					{
						BuyerComboBox c47 = new BuyerComboBox();
						AddControl(control, c47);
						break;
					}
					case CRCTypes.CampaignList:
					{
						CampaignComboBox c46 = new CampaignComboBox();
						AddControl(control, c46);
						break;
					}
					case CRCTypes.ChequebookList:
					{
						ChequebookComboBox c45 = new ChequebookComboBox();
						AddControl(control, c45);
						break;
					}
					case CRCTypes.CityList:
					{
						CityComboBox c44 = new CityComboBox();
						AddControl(control, c44);
						break;
					}
					case CRCTypes.CompetitorList:
					{
						CompetitorComboBox c43 = new CompetitorComboBox();
						AddControl(control, c43);
						break;
					}
					case CRCTypes.CostCenterList:
					{
						CostCenterComboBox c42 = new CostCenterComboBox();
						AddControl(control, c42);
						break;
					}
					case CRCTypes.CountryList:
					{
						CountryComboBox c41 = new CountryComboBox();
						AddControl(control, c41);
						break;
					}
					case CRCTypes.CRMActivityList:
					{
						ActivityComboBox c40 = new ActivityComboBox();
						AddControl(control, c40);
						break;
					}
					case CRCTypes.CurrencyList:
					{
						CurrencyComboBox c39 = new CurrencyComboBox();
						AddControl(control, c39);
						break;
					}
					case CRCTypes.CustomerClassList:
					{
						CustomerClassComboBox c38 = new CustomerClassComboBox();
						AddControl(control, c38);
						break;
					}
					case CRCTypes.CustomerGroupList:
					{
						CustomerGroupComboBox c37 = new CustomerGroupComboBox();
						AddControl(control, c37);
						break;
					}
					case CRCTypes.CustomerList:
					{
						customersFlatComboBox c36 = new customersFlatComboBox();
						AddControl(control, c36);
						break;
					}
					case CRCTypes.DegreeList:
					{
						DegreeComboBox c35 = new DegreeComboBox();
						AddControl(control, c35);
						break;
					}
					case CRCTypes.DepartmentList:
					{
						DepartmentComboBox c34 = new DepartmentComboBox();
						AddControl(control, c34);
						break;
					}
					case CRCTypes.DestinationList:
					{
						DestinationComboBox c33 = new DestinationComboBox();
						AddControl(control, c33);
						break;
					}
					case CRCTypes.DivisionList:
					{
						DivisionComboBox c32 = new DivisionComboBox();
						AddControl(control, c32);
						break;
					}
					case CRCTypes.DriverList:
					{
						DriverComboBox c31 = new DriverComboBox();
						AddControl(control, c31);
						break;
					}
					case CRCTypes.EmployeeDeductionList:
					{
						DeductionComboBox c30 = new DeductionComboBox();
						AddControl(control, c30);
						break;
					}
					case CRCTypes.EmployeeGroupList:
					{
						EmployeeGroupComboBox c29 = new EmployeeGroupComboBox();
						AddControl(control, c29);
						break;
					}
					case CRCTypes.EmployeeList:
					{
						EmployeeComboBox c28 = new EmployeeComboBox();
						AddControl(control, c28);
						break;
					}
					case CRCTypes.EmployeeLoanList:
					{
						EmployeeLoanComboBox c27 = new EmployeeLoanComboBox();
						AddControl(control, c27);
						break;
					}
					case CRCTypes.FixedAssetList:
					{
						FixedAssetsComboBox c26 = new FixedAssetsComboBox();
						AddControl(control, c26);
						break;
					}
					case CRCTypes.ItemList:
					{
						ProductComboBox c25 = new ProductComboBox();
						AddControl(control, c25);
						break;
					}
					case CRCTypes.LeadList:
					{
						leadsFlatComboBox c24 = new leadsFlatComboBox();
						AddControl(control, c24);
						break;
					}
					case CRCTypes.LocationList:
					{
						LocationComboBox c23 = new LocationComboBox();
						AddControl(control, c23);
						break;
					}
					case CRCTypes.NationalityList:
					{
						NationalityComboBox c22 = new NationalityComboBox();
						AddControl(control, c22);
						break;
					}
					case CRCTypes.OpportunityList:
					{
						OpportunityComboBox c21 = new OpportunityComboBox();
						AddControl(control, c21);
						break;
					}
					case CRCTypes.PortList:
					{
						PortComboBox c20 = new PortComboBox();
						AddControl(control, c20);
						break;
					}
					case CRCTypes.ProductBOMList:
					{
						BOMComboBox c19 = new BOMComboBox();
						AddControl(control, c19);
						break;
					}
					case CRCTypes.ProductCategoryList:
					{
						ProductCategoryComboBox c18 = new ProductCategoryComboBox();
						AddControl(control, c18);
						break;
					}
					case CRCTypes.ProjectCostCategoryList:
					{
						CostCategoryComboBox c17 = new CostCategoryComboBox();
						AddControl(control, c17);
						break;
					}
					case CRCTypes.ProjectList:
					{
						JobComboBox c16 = new JobComboBox();
						AddControl(control, c16);
						break;
					}
					case CRCTypes.SalespersonList:
					{
						SalespersonComboBox c15 = new SalespersonComboBox();
						AddControl(control, c15);
						break;
					}
					case CRCTypes.SponsorList:
					{
						SponsorComboBox c14 = new SponsorComboBox();
						AddControl(control, c14);
						break;
					}
					case CRCTypes.ConsignInSelector:
					{
						ConsignInSelector c13 = new ConsignInSelector();
						AddControl(control, c13);
						break;
					}
					case CRCTypes.PropertySelector:
					{
						PropertySelector c12 = new PropertySelector();
						AddControl(control, c12);
						break;
					}
					case CRCTypes.PropertyUnitSelector:
					{
						PropertyUnitSelector c11 = new PropertyUnitSelector();
						AddControl(control, c11);
						break;
					}
					case CRCTypes.UserList:
					{
						UserComboBox c10 = new UserComboBox();
						AddControl(control, c10);
						break;
					}
					case CRCTypes.VehicleList:
					{
						VehicleComboBox c9 = new VehicleComboBox();
						AddControl(control, c9);
						break;
					}
					case CRCTypes.VendorClassList:
					{
						VendorClassComboBox c8 = new VendorClassComboBox();
						AddControl(control, c8);
						break;
					}
					case CRCTypes.VendorGroupList:
					{
						VendorGroupComboBox c7 = new VendorGroupComboBox();
						AddControl(control, c7);
						break;
					}
					case CRCTypes.VendorList:
					{
						vendorsFlatComboBox c6 = new vendorsFlatComboBox();
						AddControl(control, c6);
						break;
					}
					case CRCTypes.DateTime:
					{
						flatDatePicker c5 = new flatDatePicker();
						AddControl(control, c5);
						break;
					}
					case CRCTypes.TextBox:
					{
						MMTextBox c4 = new MMTextBox();
						AddControl(control, c4);
						break;
					}
					case CRCTypes.Number:
					{
						NumberTextBox c3 = new NumberTextBox();
						AddControl(control, c3);
						break;
					}
					case CRCTypes.SelectionBox:
					{
						MMSelectionBox c2 = new MMSelectionBox();
						AddControl(control, c2);
						break;
					}
					case CRCTypes.DateBetween:
					{
						DateBetween c = new DateBetween();
						AddControl(control, c);
						break;
					}
					}
				}
				if (dataRow["Layout"] != DBNull.Value)
				{
					Stream stream = new MemoryStream((byte[])dataRow["Layout"]);
					if (stream.Length > 0)
					{
						layoutControl.RestoreLayoutFromStream(stream);
					}
				}
				if (IsDesignMode)
				{
					buttonOK.Text = "&Save";
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				currentReportID = "";
				currentData = null;
			}
		}

		private void SalesOrderForm_KeyDown(object sender, EventArgs e)
		{
		}

		private void AddControl(CustomReportControl crc, Control c)
		{
			c.Name = crc.ControlName;
			((ICustomReportControl)c).CustomReportKey = crc.Key;
			((ICustomReportControl)c).CustomReportFieldName = crc.FieldName;
			((ICustomReportControl)c).CustomReportValueType = crc.ValueType;
			if (c.GetType().BaseType == typeof(DateBetween))
			{
				((ICustomReportControlPlus)c).CustomReportKey1 = crc.Key1;
			}
			if (c.GetType().BaseType == typeof(MultiColumnComboBox) || c.GetType() == typeof(MultiColumnComboBox) || c.GetType() == typeof(MMTextBox) || c.GetType() == typeof(NumberTextBox) || c.GetType() == typeof(flatDatePicker))
			{
				c.Width = 200;
				c.MinimumSize = new Size(c.Width, 20);
				c.MaximumSize = new Size(20, 20);
			}
			else
			{
				c.MaximumSize = c.Size;
				c.MinimumSize = c.Size;
			}
			LayoutControlItem layoutControlItem = layoutControl.AddItem(crc.DisplayText + ":", c);
			layoutControlItem.Name = "LayoutItem" + layoutControl.Items.Count;
			layoutControlItem.Height = c.Height;
			layoutControlItem.MaxSize = new Size(0, c.Height);
			layoutControlItem.TextAlignMode = TextAlignModeItem.CustomSize;
			layoutControlItem.TextLocation = Locations.Left;
			layoutControlItem.AppearanceItemCaption.TextOptions.VAlignment = VertAlignment.Top;
		}

		public void SetupGrid()
		{
		}

		private void buttonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private DataSet GetData()
		{
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				foreach (object item in layoutControl.Items)
				{
					if (!(item.GetType() == typeof(LayoutControlGroup)))
					{
						LayoutControlItem layoutControlItem = item as LayoutControlItem;
						ICustomReportControl customReportControl = layoutControlItem.Control as ICustomReportControl;
						sqlCommand.Parameters.AddWithValue(customReportControl.CustomReportKey, customReportControl.GetParameterValue());
						if (customReportControl.GetType() == typeof(DateBetween))
						{
							ICustomReportControlPlus customReportControlPlus = layoutControlItem.Control as ICustomReportControlPlus;
							sqlCommand.Parameters.AddWithValue("@EndDate", customReportControlPlus.GetParameterValue1());
						}
					}
				}
				foreach (ReportParameter parameter in currentReport.Parameters)
				{
					if (!sqlCommand.Parameters.Contains(parameter.ParameterName))
					{
						sqlCommand.Parameters.AddWithValue(parameter.ParameterName, "NULL");
					}
				}
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				foreach (SqlParameter parameter2 in sqlCommand.Parameters)
				{
					arrayList.Add(parameter2.ParameterName);
					if (parameter2.Value.GetType() == typeof(DateTime))
					{
						arrayList2.Add(CommonLib.ToSqlDateTimeString(DateTime.Parse(parameter2.Value.ToString())));
					}
					else
					{
						arrayList2.Add(parameter2.Value.ToString());
					}
				}
				Array array = arrayList.ToArray(typeof(string));
				Array array2 = arrayList2.ToArray(typeof(string));
				return Factory.CustomReportSystem.GetCustomReportData(currentReportID, (string[])array, (string[])array2);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return null;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			if (IsDesignMode)
			{
				MemoryStream memoryStream = new MemoryStream();
				layoutControl.SaveLayoutToStream(memoryStream);
				byte[] layout = memoryStream.ToArray();
				if (Factory.CustomReportSystem.SaveLayout(currentReportID, layout, base.Width, base.Height))
				{
					Close();
				}
				return;
			}
			DataSet data = GetData();
			if (data != null && data.Tables.Count != 0)
			{
				string str = currentData.Tables[0].Rows[0]["TemplateName"].ToString();
				string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + str;
				ReportHelper reportHelper = new ReportHelper();
				reportHelper.AddGeneralReportData(ref data, "");
				reportHelper.AddFilterData(ref data, GetAllFormControls(this));
				str = Path.GetFileNameWithoutExtension(path);
				XtraReport report = reportHelper.GetReport(str);
				if (report == null)
				{
					ErrorHelper.ErrorMessage("Cannot find the report file", "'" + str + ".repx'", "Please make sure you have access to reports path and the files are not corrupted.");
					return;
				}
				report.DataSource = data;
				reportHelper.ShowReport(report);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				base.DialogResult = DialogResult.None;
				if (ErrorHelper.QuestionMessageYesNo("Are you sure you want to reset the layout to default layout?") != DialogResult.No)
				{
					Factory.CustomReportSystem.SaveLayout(currentReportID, null, base.Width, base.Height);
					LoadReport(currentReportID);
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
		}

		private void ButtonSelect_Click(object sender, EventArgs e)
		{
			try
			{
				Micromind.ClientUI.Reports.CustomReports.SelectDocumentDialogForm selectDocumentDialogForm = new Micromind.ClientUI.Reports.CustomReports.SelectDocumentDialogForm();
				selectDocumentDialogForm.Text = "Select Document";
				selectDocumentDialogForm.DataSource = Factory.CustomReportSystem.GetCustomData(currentReportID);
				if (selectDocumentDialogForm.ShowDialog(this) == DialogResult.OK)
				{
					string empty = string.Empty;
					empty = selectDocumentDialogForm.SelectedRow.Cells[0].Text.ToString();
					if (!string.IsNullOrEmpty(empty))
					{
						foreach (object item in layoutControl.Items)
						{
							if (!(item.GetType() == typeof(LayoutControlGroup)))
							{
								LayoutControlItem layoutControlItem = item as LayoutControlItem;
								if ((layoutControlItem.Control as ICustomReportControl).GetType() == typeof(MMSelectionBox))
								{
									(layoutControlItem.Control as ICustomReportControlFeatured).TextValue = empty;
								}
							}
						}
					}
				}
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public IEnumerable<Control> GetAllFormControls(Control control)
		{
			IEnumerable<Control> controls = control.Controls.Cast<Control>();
			return controls.SelectMany((Control ctrl) => GetAllFormControls(ctrl).Concat(controls));
		}

		private void SetToolTip(string text)
		{
			ultraPictureBoxInformation.Visible = true;
			UltraToolTipInfo ultraToolTipInfo = new UltraToolTipInfo(text, ToolTipImage.Info, "Display Note", DefaultableBoolean.True);
			ultraToolTipInfo.Appearance.BackColor = Color.White;
			ultraToolTipInfo.Appearance.BackColor2 = Color.LightBlue;
			ultraToolTipInfo.Appearance.BackGradientStyle = GradientStyle.Circular;
			ultraToolTipInfo.Appearance.ForeColor = Color.Black;
			ultraToolTipManager1.SetUltraToolTip(ultraPictureBoxInformation, ultraToolTipInfo);
			ultraToolTipManager1.DisplayStyle = ToolTipDisplayStyle.BalloonTip;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.Reports.CustomReportDisplayForm));
			buttonClose = new System.Windows.Forms.Button();
			buttonOK = new System.Windows.Forms.Button();
			panel1 = new System.Windows.Forms.Panel();
			button1 = new System.Windows.Forms.Button();
			line1 = new Micromind.UISupport.Line();
			layoutControl = new DevExpress.XtraLayout.LayoutControl();
			layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
			ultraPictureBoxInformation = new Infragistics.Win.UltraWinEditors.UltraPictureBox();
			ultraToolTipManager1 = new Infragistics.Win.UltraWinToolTip.UltraToolTipManager();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)layoutControl).BeginInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
			SuspendLayout();
			buttonClose.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			buttonClose.Location = new System.Drawing.Point(481, 9);
			buttonClose.Name = "buttonClose";
			buttonClose.Size = new System.Drawing.Size(98, 24);
			buttonClose.TabIndex = 2;
			buttonClose.Text = "&Close";
			buttonClose.UseVisualStyleBackColor = true;
			buttonClose.Click += new System.EventHandler(buttonClose_Click);
			buttonOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			buttonOK.Location = new System.Drawing.Point(377, 9);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(98, 24);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "&Display";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += new System.EventHandler(buttonOK_Click);
			panel1.Controls.Add(button1);
			panel1.Controls.Add(line1);
			panel1.Controls.Add(buttonClose);
			panel1.Controls.Add(buttonOK);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 317);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(587, 45);
			panel1.TabIndex = 1;
			panel1.Paint += new System.Windows.Forms.PaintEventHandler(panel1_Paint);
			button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			button1.Location = new System.Drawing.Point(11, 9);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(89, 24);
			button1.TabIndex = 0;
			button1.Text = "&Reset";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(button1_Click);
			line1.BackColor = System.Drawing.Color.White;
			line1.Dock = System.Windows.Forms.DockStyle.Top;
			line1.DrawWidth = 1;
			line1.IsVertical = false;
			line1.LineBackColor = System.Drawing.Color.Black;
			line1.Location = new System.Drawing.Point(0, 0);
			line1.Name = "line1";
			line1.Size = new System.Drawing.Size(587, 1);
			line1.TabIndex = 7;
			line1.TabStop = false;
			layoutControl.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			layoutControl.Location = new System.Drawing.Point(0, 12);
			layoutControl.Name = "layoutControl";
			layoutControl.OptionsView.UseDefaultDragAndDropRendering = false;
			layoutControl.Root = layoutControlGroup1;
			layoutControl.Size = new System.Drawing.Size(585, 298);
			layoutControl.TabIndex = 8;
			layoutControl.Text = "layoutControl1";
			layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
			layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
			layoutControlGroup1.GroupBordersVisible = false;
			layoutControlGroup1.Name = "layoutControlGroup1";
			layoutControlGroup1.Size = new System.Drawing.Size(585, 298);
			layoutControlGroup1.TextVisible = false;
			ultraPictureBoxInformation.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			ultraPictureBoxInformation.BorderShadowColor = System.Drawing.Color.Empty;
			ultraPictureBoxInformation.Image = resources.GetObject("ultraPictureBoxInformation.Image");
			ultraPictureBoxInformation.Location = new System.Drawing.Point(568, 1);
			ultraPictureBoxInformation.Name = "ultraPictureBoxInformation";
			ultraPictureBoxInformation.Size = new System.Drawing.Size(17, 19);
			ultraPictureBoxInformation.TabIndex = 296;
			ultraPictureBoxInformation.Visible = false;
			ultraToolTipManager1.ContainingControl = this;
			base.AcceptButton = buttonOK;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.CancelButton = buttonClose;
			base.ClientSize = new System.Drawing.Size(587, 362);
			base.Controls.Add(ultraPictureBoxInformation);
			base.Controls.Add(layoutControl);
			base.Controls.Add(panel1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.Name = "CustomReportDisplayForm";
			Text = "Custom Report";
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)layoutControl).EndInit();
			((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
			ResumeLayout(false);
		}
	}
}
