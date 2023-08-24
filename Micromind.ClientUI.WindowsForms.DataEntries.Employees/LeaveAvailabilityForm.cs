using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Properties;
using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.DataControls;
using Micromind.UISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms.DataEntries.Employees
{
	public class LeaveAvailabilityForm : Form, IForm
	{
		public class Leavelist
		{
			private DateTime start;

			private DateTime end;

			public DateTime Start
			{
				get
				{
					return start;
				}
				set
				{
					start = value;
				}
			}

			public DateTime End
			{
				get
				{
					return end;
				}
				set
				{
					end = value;
				}
			}

			public Leavelist(DateTime Dtstart, DateTime Dtend)
			{
				start = Dtstart;
				end = Dtend;
			}
		}

		private DataSet currentData;

		private const string TABLENAME_CONST = "Product";

		private const string IDFIELD_CONST = "ProductID";

		private bool isLoading;

		private bool isFirstTime = true;

		private decimal minusLeaves;

		private IContainer components;

		private ToolStrip toolStrip1;

		private ToolStripButton toolStripButtonPrint;

		private Panel panelButtons;

		private Line linePanelDown;

		private XPButton xpButton1;

		private ToolStripButton toolStripButtonFirst;

		private ToolStripButton toolStripButtonPrevious;

		private ToolStripButton toolStripButtonNext;

		private ToolStripButton toolStripButtonLast;

		private ToolStripSeparator toolStripSeparator1;

		private ToolStripButton toolStripButtonFind;

		private ToolStripTextBox toolStripTextBoxFind;

		private ToolStripSeparator toolStripSeparator2;

		private FormManager formManager;

		private TextBox textBoxEmployeeID;

		private MMLabel mmLabel1;

		private DataGridList dataGrid;

		private MMLabel mmLabel2;

		private ToolStripButton toolStripButton1;

		private ToolStripSeparator toolStripSeparator3;

		private ToolStripButton toolStripButtonInformation;

		private TextBox textBoxEmployeeName;

		private MMLabel mmLabel3;

		private MMLabel mmLabel4;

		private MMLabel labelAson;

		public ScreenAreas ScreenArea => ScreenAreas.Products;

		public int ScreenID => 4014;

		public ScreenTypes ScreenType => ScreenTypes.Dialog;

		private bool IsDirty => formManager.GetDirtyStatus();

		public decimal MinusLeaves
		{
			get
			{
				return minusLeaves;
			}
			set
			{
				minusLeaves = value;
			}
		}

		public LeaveAvailabilityForm()
		{
			InitializeComponent();
			textBoxEmployeeID.TextChanged += textBoxItemCode_TextChanged;
			base.StartPosition = FormStartPosition.Manual;
			base.Top = checked(Screen.PrimaryScreen.Bounds.Height - base.Height) / 2;
			base.Left = checked(Screen.PrimaryScreen.Bounds.Width - base.Width) / 2;
		}

		private void textBoxItemCode_TextChanged(object sender, EventArgs e)
		{
		}

		private void comboBoxUnit_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
		}

		public void LoadData(string EmployeeID, string EmployeeName, DateTime AsonDate, DateTime ToDate)
		{
			checked
			{
				try
				{
					List<Leavelist> list = new List<Leavelist>();
					isLoading = true;
					if (CanClose())
					{
						textBoxEmployeeID.Text = EmployeeID;
						textBoxEmployeeName.Text = EmployeeName;
						mmLabel3.Text = "";
						labelAson.Text = "";
						currentData = Factory.EmployeeLeaveDetailSystem.GetEmployeeLeaveAvailability(EmployeeID, AsonDate, ToDate, "");
						if (currentData.Tables.Count == 0)
						{
							ErrorHelper.InformationMessage("Leave Settings in Employee Class has not Activated.");
						}
						else if (currentData.Tables[0].Rows.Count == 0)
						{
							ErrorHelper.InformationMessage("Leave Settings in Employee Class has not Activated.");
						}
						else if (currentData.Tables.Count > 0)
						{
							decimal result = default(decimal);
							decimal result2 = default(decimal);
							decimal result3 = default(decimal);
							int result4 = 0;
							int result5 = 0;
							int result6 = 0;
							int result7 = 0;
							bool result8 = false;
							bool result9 = false;
							decimal num = default(decimal);
							int num2 = 0;
							int result10 = 0;
							DateTime result11 = new DateTime(2014, 1, 1);
							DateTime result12 = new DateTime(2014, 1, 1);
							DateTime d = new DateTime(1, 1, 1);
							if (currentData.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "On Account")
							{
								for (int i = 0; i < currentData.Tables[0].Rows.Count; i++)
								{
									decimal.TryParse(currentData.Tables[0].Rows[i]["1SET"].ToString(), out result);
									decimal.TryParse(currentData.Tables[0].Rows[i]["2SET"].ToString(), out result2);
									decimal.TryParse(currentData.Tables[0].Rows[i]["3SET"].ToString(), out result3);
									int.TryParse(currentData.Tables[0].Rows[i]["openingLeavesTaken"].ToString(), out result4);
									int.TryParse(currentData.Tables[0].Rows[i]["TotalTaken"].ToString(), out result5);
									int.TryParse(currentData.Tables[0].Rows[i]["LeaveDayswithType"].ToString(), out result6);
									bool.TryParse(currentData.Tables[0].Rows[i]["AnnualEligible"].ToString(), out result8);
									bool.TryParse(currentData.Tables[0].Rows[i]["IsAnnual"].ToString(), out result9);
									if (result != 0m || result2 != 0m || result3 != 0m || (!result8 && result9))
									{
										result6 = 0;
									}
									decimal num3 = default(decimal);
									decimal num4 = default(decimal);
									num3 = result + result2 + result3 + (decimal)result6;
									currentData.Tables[0].Rows[i]["TotalLeaves"] = num3;
									num4 = result + result2 + result3 - (decimal)result4 + (decimal)result6 - (decimal)result5;
									currentData.Tables[0].Rows[i]["LeavesRemaining"] = num4;
								}
								currentData.Tables[0].Columns.Remove("1SET");
								currentData.Tables[0].Columns.Remove("2SET");
								currentData.Tables[0].Columns.Remove("3SET");
								currentData.Tables[0].Columns.Remove("EmployeeID");
								currentData.Tables[0].Columns.Remove("AnnualEligible");
							}
							else if (currentData.Tables[0].Rows[0]["Basedon"].ToString().Trim() == "Calendar Year")
							{
								List<string> list2 = new List<string>();
								for (int j = 0; j < currentData.Tables[0].Rows.Count; j++)
								{
									int.TryParse(currentData.Tables[0].Rows[j]["openingLeavesTaken"].ToString(), out result4);
									int.TryParse(currentData.Tables[0].Rows[j]["TotalTaken"].ToString(), out result5);
									int.TryParse(currentData.Tables[0].Rows[j]["LeaveDayswithType"].ToString(), out result6);
									DateTime.TryParse(currentData.Tables[0].Rows[j]["FromDate"].ToString(), out result11);
									DateTime.TryParse(currentData.Tables[0].Rows[j]["ToDate"].ToString(), out result12);
									string text = currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString();
									result9 = bool.Parse(currentData.Tables[0].Rows[j]["IsAnnual"].ToString());
									DataView defaultView = currentData.Tables[1].DefaultView;
									bool result13 = false;
									bool.TryParse(Factory.DatabaseSystem.GetFieldValue("Leave_Type", "ActivateHC", "LeaveTypeID", text).ToString(), out result13);
									if (result9 && result11 != d && result12 != d)
									{
										defaultView.RowFilter = "FromDate='" + result11.ToString() + "' AND ToDate='" + result12.ToString() + "' AND LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									else if (result9 && result11 == d && result12 == d)
									{
										defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									else if (!result9)
									{
										defaultView.RowFilter = "LeaveTypeID='" + text.ToString() + "' AND IsAnnual='" + result9.ToString() + "'";
									}
									DataSet dataSet = new DataSet();
									DataTable dataTable = defaultView.ToTable();
									dataSet.Tables.Add(dataTable);
									bool flag = false;
									for (int k = 0; k < list.Count; k++)
									{
										DateTime start = list[k].Start;
										DateTime end = list[k].End;
										if (result11 >= start && result12 <= end)
										{
											flag = true;
										}
									}
									object obj;
									object obj2;
									if ((result11 != d && result12 != d && !flag) & result9)
									{
										DataTable dataTable2 = new DataTable();
										dataTable2 = dataTable.DefaultView.ToTable(true, "DaysTaken");
										new DataTable();
										DataTable dataTable3 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
										obj = dataTable2.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
										obj2 = dataTable3.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
										list.Add(new Leavelist(result11, result12));
									}
									else if (((result11 == d && result12 == d) & result13) && !result9)
									{
										DataTable dataTable4 = new DataTable();
										dataTable4 = dataTable.DefaultView.ToTable(true, "DaysTaken");
										new DataTable();
										DataTable dataTable5 = dataTable.DefaultView.ToTable(true, "ToLessTaken");
										obj = dataTable4.Compute("Sum(DaysTaken)", "DaysTaken <> 0");
										obj2 = dataTable5.Compute("Sum(ToLessTaken)", "ToLessTaken <> 0");
									}
									else
									{
										obj = DBNull.Value;
										obj2 = DBNull.Value;
									}
									if ((obj != DBNull.Value && obj2 != DBNull.Value) & result13)
									{
										currentData.Tables[0].Rows[j]["TotalTaken"] = int.Parse(obj.ToString()) - int.Parse(obj2.ToString());
									}
									else if (obj != DBNull.Value)
									{
										currentData.Tables[0].Rows[j]["TotalTaken"] = obj;
									}
									else if (obj == DBNull.Value && result9)
									{
										currentData.Tables[0].Rows[j]["TotalTaken"] = 0;
									}
									int.TryParse(currentData.Tables[0].Rows[j]["TotalTaken"].ToString(), out result10);
									int.TryParse(currentData.Tables[0].Rows[j]["AnnualAllowedDays"].ToString(), out result7);
									if (result9)
									{
										result6 = 0;
									}
									currentData.Tables[0].Rows[j]["TotalLeaves"] = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7;
									if (result9 && num2 != result10 && !flag)
									{
										num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
										num2 = result10;
									}
									else if (!result9)
									{
										num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result5;
									}
									else if ((result9 && num2 != result10) & flag)
									{
										num = (decimal)result7 + MinusLeaves;
										currentData.Tables[0].Rows[j]["TotalTaken"] = DBNull.Value;
									}
									else if (result9 && num2 == result10 && !flag)
									{
										num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
									}
									else if (result9 && num2 == 0 && result10 == 0)
									{
										num = result + result2 + result3 - (decimal)result4 + (decimal)result6 + (decimal)result7 - (decimal)result10 + MinusLeaves;
									}
									if (num >= 0m && result9 && MinusLeaves != 0m)
									{
										currentData.Tables[0].Rows[j]["TotalTaken"] = -1m * MinusLeaves;
										currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
										MinusLeaves = 0m;
									}
									else if (num >= 0m && result9 && MinusLeaves == 0m)
									{
										currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
									}
									else if (num < 0m && result9)
									{
										currentData.Tables[0].Rows[j]["TotalTaken"] = result7;
										currentData.Tables[0].Rows[j]["LeavesRemaining"] = 0;
										MinusLeaves = num;
									}
									else if (!result9)
									{
										currentData.Tables[0].Rows[j]["LeavesRemaining"] = num;
									}
									if (result11.Date < AsonDate.Date && result12.Date < AsonDate.Date && result7 != 0)
									{
										currentData.Tables[0].Rows[j].Delete();
									}
									else
									{
										if (result7 == 0 && !list2.Contains(currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString()))
										{
											list2.Add(currentData.Tables[0].Rows[j]["LeaveTypeID"].ToString());
										}
										else if (result7 == 0)
										{
											currentData.Tables[0].Rows[j].Delete();
											continue;
										}
										int result14 = 0;
										int.TryParse(currentData.Tables[0].Rows[j]["LeavesRemaining"].ToString(), out result14);
									}
								}
								currentData.Tables[0].Columns.Remove("LeaveDayswithType");
								currentData.Tables[0].Columns.Remove("EmployeeID");
								currentData.AcceptChanges();
							}
							FillData();
							if (currentData.Tables[0].Rows.Count > 0)
							{
								textBoxEmployeeName.Text = currentData.Tables[0].Rows[0]["Name"].ToString();
								mmLabel3.Text = currentData.Tables[0].Rows[0]["Basedon"].ToString();
								labelAson.Text = AsonDate.ToString("dd-MMM-yyyy");
								currentData.Tables[0].Columns.Remove("Name");
								currentData.Tables[0].Columns.Remove("Basedon");
								currentData.Tables[0].Columns.Remove("LeaveTypeID");
								currentData.Tables[0].Columns.Remove("IsAnnual");
								if (currentData.Tables[0].Columns.Contains("ToLessTaken"))
								{
									currentData.Tables[0].Columns.Remove("ToLessTaken");
								}
								currentData.AcceptChanges();
							}
							dataGrid.DisplayLayout.AutoFitStyle = AutoFitStyle.ResizeAllColumns;
							dataGrid.ContextMenuStrip = null;
							dataGrid.DisplayLayout.Bands[0].Columns["openingLeavesTaken"].Header.Caption = "OpeningLeavesTaken";
							dataGrid.DisplayLayout.Bands[0].Columns["TotalLeaves"].Header.Caption = "Leaves Assigned";
							dataGrid.DisplayLayout.Bands[0].Summaries.Add("TotalLeavesTaken", SummaryType.Sum, dataGrid.DisplayLayout.Bands[0].Columns["TotalTaken"], SummaryPosition.UseSummaryPositionColumn);
							formManager.ResetDirty();
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
				finally
				{
					isLoading = false;
				}
			}
		}

		private void FillData()
		{
			dataGrid.DataSource = currentData;
		}

		private void ClearForm()
		{
			if (dataGrid.DataSource != null)
			{
				((DataTable)dataGrid.DataSource).Rows.Clear();
			}
			formManager.ResetDirty();
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
		}

		private void toolStripButtonFirst_Click(object sender, EventArgs e)
		{
		}

		private void toolStripButtonFind_Click(object sender, EventArgs e)
		{
			Find();
		}

		private void Find()
		{
			try
			{
				if (toolStripTextBoxFind.Text.Trim() == "")
				{
					toolStripTextBoxFind.Focus();
				}
				else if (Factory.DatabaseSystem.ExistFieldValue("Product", "ProductID", toolStripTextBoxFind.Text.Trim()))
				{
					textBoxEmployeeID.Text = toolStripTextBoxFind.Text.Trim();
				}
				else
				{
					ErrorHelper.InformationMessage("Item not found.");
					toolStripTextBoxFind.SelectAll();
					toolStripTextBoxFind.Focus();
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public void OnActivated()
		{
		}

		private void AccountGroupDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!CanClose())
			{
				e.Cancel = true;
			}
		}

		private bool CanClose()
		{
			return true;
		}

		private void AnalysisGroupDetailsForm_Load(object sender, EventArgs e)
		{
			try
			{
				SetupGrid();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2);
			}
		}

		public void SetupGrid()
		{
			dataGrid.ApplyUIDesign();
			dataGrid.ContextMenuStrip = null;
		}

		private void textBoxItemCode_ValueChanged(object sender, EventArgs e)
		{
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
		}

		private void toolStripTextBoxFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				Find();
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micromind.ClientUI.WindowsForms.DataEntries.Employees.LeaveAvailabilityForm));
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
			toolStrip1 = new System.Windows.Forms.ToolStrip();
			toolStripButtonPrint = new System.Windows.Forms.ToolStripButton();
			toolStripButtonFirst = new System.Windows.Forms.ToolStripButton();
			toolStripButtonPrevious = new System.Windows.Forms.ToolStripButton();
			toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
			toolStripButtonLast = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			toolStripTextBoxFind = new System.Windows.Forms.ToolStripTextBox();
			toolStripButtonFind = new System.Windows.Forms.ToolStripButton();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			toolStripButtonInformation = new System.Windows.Forms.ToolStripButton();
			panelButtons = new System.Windows.Forms.Panel();
			linePanelDown = new Micromind.UISupport.Line();
			xpButton1 = new Micromind.UISupport.XPButton();
			formManager = new Micromind.DataControls.FormManager();
			textBoxEmployeeID = new System.Windows.Forms.TextBox();
			mmLabel1 = new Micromind.UISupport.MMLabel();
			dataGrid = new Micromind.UISupport.DataGridList(components);
			mmLabel2 = new Micromind.UISupport.MMLabel();
			textBoxEmployeeName = new System.Windows.Forms.TextBox();
			mmLabel3 = new Micromind.UISupport.MMLabel();
			mmLabel4 = new Micromind.UISupport.MMLabel();
			labelAson = new Micromind.UISupport.MMLabel();
			toolStrip1.SuspendLayout();
			panelButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dataGrid).BeginInit();
			SuspendLayout();
			toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[12]
			{
				toolStripButtonPrint,
				toolStripButtonFirst,
				toolStripButtonPrevious,
				toolStripButtonNext,
				toolStripButtonLast,
				toolStripSeparator1,
				toolStripButton1,
				toolStripSeparator3,
				toolStripTextBoxFind,
				toolStripButtonFind,
				toolStripSeparator2,
				toolStripButtonInformation
			});
			toolStrip1.Location = new System.Drawing.Point(0, 0);
			toolStrip1.Name = "toolStrip1";
			toolStrip1.Size = new System.Drawing.Size(534, 25);
			toolStrip1.TabIndex = 0;
			toolStrip1.Text = "toolStrip1";
			toolStrip1.Visible = false;
			toolStripButtonPrint.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			toolStripButtonPrint.Image = Micromind.ClientUI.Properties.Resources.printer;
			toolStripButtonPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrint.Name = "toolStripButtonPrint";
			toolStripButtonPrint.Size = new System.Drawing.Size(57, 22);
			toolStripButtonPrint.Text = "&Print";
			toolStripButtonPrint.ToolTipText = "Print (Ctrl+P)";
			toolStripButtonPrint.Visible = false;
			toolStripButtonFirst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonFirst.Image = Micromind.ClientUI.Properties.Resources.first;
			toolStripButtonFirst.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFirst.Name = "toolStripButtonFirst";
			toolStripButtonFirst.Size = new System.Drawing.Size(28, 22);
			toolStripButtonFirst.Text = "First";
			toolStripButtonFirst.Click += new System.EventHandler(toolStripButtonFirst_Click);
			toolStripButtonPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonPrevious.Image = Micromind.ClientUI.Properties.Resources.prev;
			toolStripButtonPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonPrevious.Name = "toolStripButtonPrevious";
			toolStripButtonPrevious.Size = new System.Drawing.Size(28, 22);
			toolStripButtonPrevious.Text = "Previous";
			toolStripButtonPrevious.Click += new System.EventHandler(toolStripButtonPrevious_Click);
			toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonNext.Image = Micromind.ClientUI.Properties.Resources.next;
			toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonNext.Name = "toolStripButtonNext";
			toolStripButtonNext.Size = new System.Drawing.Size(28, 22);
			toolStripButtonNext.Text = "Next";
			toolStripButtonNext.Click += new System.EventHandler(toolStripButtonNext_Click);
			toolStripButtonLast.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonLast.Image = Micromind.ClientUI.Properties.Resources.last;
			toolStripButtonLast.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonLast.Name = "toolStripButtonLast";
			toolStripButtonLast.Size = new System.Drawing.Size(28, 22);
			toolStripButtonLast.Text = "Last";
			toolStripButtonLast.Click += new System.EventHandler(toolStripButtonLast_Click);
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButton1.Image = (System.Drawing.Image)resources.GetObject("toolStripButton1.Image");
			toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButton1.Name = "toolStripButton1";
			toolStripButton1.Size = new System.Drawing.Size(28, 22);
			toolStripButton1.Text = "Refresh";
			toolStripButton1.Click += new System.EventHandler(toolStripButton1_Click);
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			toolStripTextBoxFind.Name = "toolStripTextBoxFind";
			toolStripTextBoxFind.Size = new System.Drawing.Size(100, 25);
			toolStripTextBoxFind.KeyDown += new System.Windows.Forms.KeyEventHandler(toolStripTextBoxFind_KeyDown);
			toolStripButtonFind.Image = Micromind.ClientUI.Properties.Resources.find;
			toolStripButtonFind.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonFind.Name = "toolStripButtonFind";
			toolStripButtonFind.Size = new System.Drawing.Size(55, 22);
			toolStripButtonFind.Text = "Find";
			toolStripButtonFind.Click += new System.EventHandler(toolStripButtonFind_Click);
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			toolStripButtonInformation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			toolStripButtonInformation.Image = Micromind.ClientUI.Properties.Resources.docinfo_24x24;
			toolStripButtonInformation.ImageTransparentColor = System.Drawing.Color.Magenta;
			toolStripButtonInformation.Name = "toolStripButtonInformation";
			toolStripButtonInformation.Size = new System.Drawing.Size(28, 22);
			toolStripButtonInformation.Text = "Document Information";
			panelButtons.Controls.Add(linePanelDown);
			panelButtons.Controls.Add(xpButton1);
			panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelButtons.Location = new System.Drawing.Point(0, 247);
			panelButtons.Name = "panelButtons";
			panelButtons.Size = new System.Drawing.Size(844, 40);
			panelButtons.TabIndex = 4;
			linePanelDown.BackColor = System.Drawing.Color.White;
			linePanelDown.Dock = System.Windows.Forms.DockStyle.Top;
			linePanelDown.DrawWidth = 1;
			linePanelDown.IsVertical = false;
			linePanelDown.LineBackColor = System.Drawing.Color.Silver;
			linePanelDown.Location = new System.Drawing.Point(0, 0);
			linePanelDown.Name = "linePanelDown";
			linePanelDown.Size = new System.Drawing.Size(844, 1);
			linePanelDown.TabIndex = 14;
			linePanelDown.TabStop = false;
			xpButton1.AdjustImageLocation = new System.Drawing.Point(0, 0);
			xpButton1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			xpButton1.BackColor = System.Drawing.Color.DarkGray;
			xpButton1.BtnShape = Micromind.UISupport.BtnShape.Rectangle;
			xpButton1.BtnStyle = Micromind.UISupport.XPStyle.Default;
			xpButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			xpButton1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			xpButton1.Location = new System.Drawing.Point(736, 8);
			xpButton1.Name = "xpButton1";
			xpButton1.Size = new System.Drawing.Size(96, 24);
			xpButton1.TabIndex = 0;
			xpButton1.Text = "&Close";
			xpButton1.UseVisualStyleBackColor = false;
			xpButton1.Click += new System.EventHandler(xpButton1_Click);
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
			textBoxEmployeeID.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeID.Location = new System.Drawing.Point(92, 30);
			textBoxEmployeeID.Name = "textBoxEmployeeID";
			textBoxEmployeeID.ReadOnly = true;
			textBoxEmployeeID.Size = new System.Drawing.Size(67, 20);
			textBoxEmployeeID.TabIndex = 0;
			mmLabel1.AutoSize = true;
			mmLabel1.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel1.IsFieldHeader = false;
			mmLabel1.IsRequired = false;
			mmLabel1.Location = new System.Drawing.Point(11, 60);
			mmLabel1.Name = "mmLabel1";
			mmLabel1.PenWidth = 1f;
			mmLabel1.ShowBorder = false;
			mmLabel1.Size = new System.Drawing.Size(138, 13);
			mmLabel1.TabIndex = 22;
			mmLabel1.Text = "Leave availability based on:";
			dataGrid.AllowUnfittedView = false;
			dataGrid.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
			appearance.BackColor = System.Drawing.SystemColors.Window;
			appearance.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			dataGrid.DisplayLayout.Appearance = appearance;
			dataGrid.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			dataGrid.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
			appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
			appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
			appearance2.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.GroupByBox.Appearance = appearance2;
			appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
			dataGrid.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
			appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
			appearance4.BackColor2 = System.Drawing.SystemColors.Control;
			appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
			dataGrid.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
			dataGrid.DisplayLayout.MaxColScrollRegions = 1;
			dataGrid.DisplayLayout.MaxRowScrollRegions = 1;
			appearance5.BackColor = System.Drawing.SystemColors.Window;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGrid.DisplayLayout.Override.ActiveCellAppearance = appearance5;
			appearance6.BackColor = System.Drawing.SystemColors.Highlight;
			appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
			dataGrid.DisplayLayout.Override.ActiveRowAppearance = appearance6;
			dataGrid.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
			dataGrid.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
			appearance7.BackColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.CardAreaAppearance = appearance7;
			appearance8.BorderColor = System.Drawing.Color.Silver;
			appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
			dataGrid.DisplayLayout.Override.CellAppearance = appearance8;
			dataGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
			dataGrid.DisplayLayout.Override.CellPadding = 0;
			appearance9.BackColor = System.Drawing.SystemColors.Control;
			appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
			appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
			appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
			appearance9.BorderColor = System.Drawing.SystemColors.Window;
			dataGrid.DisplayLayout.Override.GroupByRowAppearance = appearance9;
			appearance10.TextHAlignAsString = "Left";
			dataGrid.DisplayLayout.Override.HeaderAppearance = appearance10;
			dataGrid.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			dataGrid.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
			appearance11.BackColor = System.Drawing.SystemColors.Window;
			appearance11.BorderColor = System.Drawing.Color.Silver;
			dataGrid.DisplayLayout.Override.RowAppearance = appearance11;
			dataGrid.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
			dataGrid.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
			dataGrid.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			dataGrid.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			dataGrid.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
			dataGrid.Location = new System.Drawing.Point(11, 79);
			dataGrid.Name = "dataGrid";
			dataGrid.ShowDeleteMenu = false;
			dataGrid.ShowMinusInRed = true;
			dataGrid.ShowNewMenu = false;
			dataGrid.Size = new System.Drawing.Size(820, 162);
			dataGrid.TabIndex = 3;
			dataGrid.Text = "dataGridList1";
			mmLabel2.AutoSize = true;
			mmLabel2.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel2.IsFieldHeader = false;
			mmLabel2.IsRequired = false;
			mmLabel2.Location = new System.Drawing.Point(4, 32);
			mmLabel2.Name = "mmLabel2";
			mmLabel2.PenWidth = 1f;
			mmLabel2.ShowBorder = false;
			mmLabel2.Size = new System.Drawing.Size(87, 13);
			mmLabel2.TabIndex = 292;
			mmLabel2.Text = "Employee Name:";
			textBoxEmployeeName.BackColor = System.Drawing.Color.WhiteSmoke;
			textBoxEmployeeName.Location = new System.Drawing.Point(161, 30);
			textBoxEmployeeName.Name = "textBoxEmployeeName";
			textBoxEmployeeName.ReadOnly = true;
			textBoxEmployeeName.Size = new System.Drawing.Size(356, 20);
			textBoxEmployeeName.TabIndex = 293;
			mmLabel3.AutoSize = true;
			mmLabel3.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel3.IsFieldHeader = false;
			mmLabel3.IsRequired = false;
			mmLabel3.Location = new System.Drawing.Point(151, 60);
			mmLabel3.Name = "mmLabel3";
			mmLabel3.PenWidth = 1f;
			mmLabel3.ShowBorder = false;
			mmLabel3.Size = new System.Drawing.Size(45, 13);
			mmLabel3.TabIndex = 294;
			mmLabel3.Text = "base on";
			mmLabel4.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			mmLabel4.AutoSize = true;
			mmLabel4.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			mmLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			mmLabel4.IsFieldHeader = false;
			mmLabel4.IsRequired = false;
			mmLabel4.Location = new System.Drawing.Point(740, 60);
			mmLabel4.Name = "mmLabel4";
			mmLabel4.PenWidth = 1f;
			mmLabel4.ShowBorder = false;
			mmLabel4.Size = new System.Drawing.Size(37, 13);
			mmLabel4.TabIndex = 295;
			mmLabel4.Text = "As on:";
			labelAson.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			labelAson.AutoSize = true;
			labelAson.BorderColor = System.Drawing.Color.FromArgb(78, 122, 171);
			labelAson.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			labelAson.IsFieldHeader = false;
			labelAson.IsRequired = false;
			labelAson.Location = new System.Drawing.Point(776, 60);
			labelAson.Name = "labelAson";
			labelAson.PenWidth = 1f;
			labelAson.ShowBorder = false;
			labelAson.Size = new System.Drawing.Size(30, 13);
			labelAson.TabIndex = 296;
			labelAson.Text = "Date";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.Color.WhiteSmoke;
			base.ClientSize = new System.Drawing.Size(844, 287);
			base.Controls.Add(labelAson);
			base.Controls.Add(mmLabel4);
			base.Controls.Add(mmLabel3);
			base.Controls.Add(textBoxEmployeeName);
			base.Controls.Add(mmLabel2);
			base.Controls.Add(textBoxEmployeeID);
			base.Controls.Add(dataGrid);
			base.Controls.Add(mmLabel1);
			base.Controls.Add(formManager);
			base.Controls.Add(panelButtons);
			base.Controls.Add(toolStrip1);
			base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MinimumSize = new System.Drawing.Size(542, 302);
			base.Name = "LeaveAvailabilityForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Leave Availability";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(AccountGroupDetailsForm_FormClosing);
			base.Load += new System.EventHandler(AnalysisGroupDetailsForm_Load);
			toolStrip1.ResumeLayout(false);
			toolStrip1.PerformLayout();
			panelButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dataGrid).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
