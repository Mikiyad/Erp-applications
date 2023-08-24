using DevExpress.XtraLayout;
using DevExpress.XtraLayout.Utils;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Micromind.DataControls.FlatDashboard
{
	public class FlatDashboard : UserControl
	{
		public delegate void GadgetDoubleClick(SmartListDrillDownActions action, DataComboType cardType, string sysDocID, string docNumber, bool isPreview);

		private bool isEnlarged;

		private string key = "";

		private bool isLoaded;

		private LayoutControl layoutControl;

		private bool isEditMode;

		private IContainer components;

		public bool IsEnlarged
		{
			get
			{
				return isEnlarged;
			}
			set
			{
				isEnlarged = value;
			}
		}

		public LayoutControl LayoutControl
		{
			get
			{
				return layoutControl;
			}
			set
			{
				layoutControl = value;
			}
		}

		public string Key
		{
			get
			{
				if (LayoutControl != null)
				{
					return LayoutControl.Name;
				}
				return "";
			}
		}

		public bool IsLoaded => isLoaded;

		public event GadgetDoubleClick GadgetListDoubleClick;

		public FlatDashboard()
		{
			InitializeComponent();
		}

		public void OnGadgetDoubleClick(SmartListDrillDownActions action, DataComboType cardType, string sysDocID, string docNumber, bool isPreview)
		{
			if (this.GadgetListDoubleClick != null)
			{
				this.GadgetListDoubleClick(action, cardType, sysDocID, docNumber, isPreview);
			}
		}

		public void Customize()
		{
			try
			{
				if (IsEnlarged)
				{
					ExitEnlargedMode();
				}
				if (new CustomizeDashboardForm
				{
					DashboardKey = Key
				}.ShowDialog() == DialogResult.OK)
				{
					LoadDashboard();
				}
			}
			catch
			{
				throw;
			}
		}

		public bool SaveLayout()
		{
			try
			{
				bool result = true;
				MemoryStream memoryStream = new MemoryStream();
				LayoutControl.SaveLayoutToStream(memoryStream);
				DashboardLayout dashboardLayout = new DashboardLayout();
				dashboardLayout.Layout = memoryStream;
				foreach (LayoutControlItem item2 in LayoutControl.Root.Items)
				{
					GroupLayout item = item2.Tag as GroupLayout;
					dashboardLayout.GadgetsList.Add(item);
				}
				MemoryStream memoryStream2 = Global.SerializeToStream(dashboardLayout);
				Factory.DashboardSystem.SaveLayout(Key, memoryStream2.ToArray());
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void Clear()
		{
			try
			{
				isLoaded = false;
				LayoutControl.BeginUpdate();
				if (LayoutControl != null)
				{
					for (int i = 0; i < LayoutControl.Root.Items.Count; i++)
					{
						LayoutControlItem layoutControlItem = LayoutControl.Root[i] as LayoutControlItem;
						if (layoutControlItem != null && layoutControlItem.Control != null)
						{
							layoutControlItem.Control.Dispose();
							layoutControlItem.Dispose();
							i--;
						}
					}
					LayoutControl.Root.Items.Clear();
					LayoutControl.Clear();
					LayoutControl.Controls.Clear();
					LayoutControl.HiddenItems.Clear();
					LayoutControl.EndUpdate();
				}
			}
			catch
			{
				throw;
			}
		}

		public void SetEditMode(bool isEdit)
		{
			try
			{
				((ILayoutControl)LayoutControl).EnableCustomizationForm = false;
				((ILayoutControl)LayoutControl).EnableCustomizationMode = isEdit;
				if (isEdit && IsEnlarged)
				{
					ExitEnlargedMode();
				}
				isEditMode = isEdit;
			}
			catch
			{
				throw;
			}
		}

		private void ExitEnlargedMode()
		{
			foreach (LayoutControlItem item in LayoutControl.Root.Items)
			{
				if (item.Visibility == LayoutVisibility.Never)
				{
					item.Visibility = LayoutVisibility.Always;
				}
				else
				{
					GadgetContainer gadgetContainer = item.Control as GadgetContainer;
					if (gadgetContainer.IsEnlarged)
					{
						gadgetContainer.IsEnlarged = false;
					}
				}
			}
		}

		public void LoadDashboard()
		{
			try
			{
				LayoutControl.BeginUpdate();
				Clear();
				DataSet dashboardByID = Factory.DashboardSystemAsync.GetDashboardByID(Key, Global.CurrentUser);
				if (dashboardByID != null && dashboardByID.Tables[0].Rows.Count != 0)
				{
					object obj = dashboardByID.Tables[0].Rows[0]["Layout"];
					if (!obj.IsNullOrEmpty())
					{
						byte[] streamBytes = (byte[])obj;
						CustomGadgetData customGadgets = Factory.CustomGadgetSystemAsync.GetCustomGadgets();
						DataTable customGadgetTable = customGadgets.CustomGadgetTable;
						DashboardLayout dashboardLayout = (DashboardLayout)Global.DeserializeFromStream(streamBytes);
						foreach (GroupLayout gadgets in dashboardLayout.GadgetsList)
						{
							if (gadgets != null && Security.GetCustomReportAccessRight(CustomReportTypes.CustomGadget, gadgets.Code).Visible)
							{
								if (gadgets.IsCustom)
								{
									DataRow[] array = customGadgetTable.Select("CustomGadgetID = '" + gadgets.Code + "'");
									if (array.Length == 0)
									{
										continue;
									}
									gadgets.Title = array[0]["CustomGadgetName"].ToString();
									gadgets.ValueColumn = array[0]["ChartValueColumn"].ToString();
									gadgets.ArgumentColumn = array[0]["ChartArgColumn"].ToString();
									if (array[0]["ChartType"] != DBNull.Value)
									{
										gadgets.ChartType = int.Parse(array[0]["ChartType"].ToString());
									}
									if (array[0]["ColorEach"] != DBNull.Value)
									{
										gadgets.ColorEach = bool.Parse(array[0]["ColorEach"].ToString());
									}
									if (array[0]["FilterOption"] != DBNull.Value)
									{
										gadgets.FilterOption = (GadgetFilterOptions)int.Parse(array[0]["FilterOption"].ToString());
									}
									else
									{
										gadgets.FilterOption = GadgetFilterOptions.None;
									}
									if (array[0]["GadgetStyle"] != DBNull.Value)
									{
										gadgets.GadgetStyle = (GadgetStyles)int.Parse(array[0]["GadgetStyle"].ToString());
									}
									else
									{
										gadgets.GadgetStyle = GadgetStyles.List;
									}
									array = customGadgets.ChartSeriesTable.Select("CustomGadgetID = '" + gadgets.Code + "'");
									gadgets.ChartSeries.Clear();
									if (array.Length == 0)
									{
										gadgets.ChartSeries.Add(new ChartSerie("Serie1", gadgets.ValueColumn, gadgets.ChartType, gadgets.Title));
									}
									else
									{
										foreach (DataRow dataRow in array)
										{
											ChartSerie chartSerie = new ChartSerie(dataRow["SeriesID"].ToString(), dataRow["ChartValueColumn"].ToString(), int.Parse(dataRow["ChartType"].ToString()), dataRow["DisplayName"].ToString());
											chartSerie.Color = (dataRow["Color"].IsDBNullOrEmpty() ? (-1) : int.Parse(dataRow["Color"].ToString()));
											chartSerie.LabelPosition = (dataRow["LabelPosition"].IsDBNullOrEmpty() ? (-1) : int.Parse(dataRow["LabelPosition"].ToString()));
											chartSerie.LabelTextPattern = dataRow["LabelTextPattern"].ToString();
											chartSerie.LabelVisible = (!dataRow["LabelVisible"].IsDBNullOrEmpty() && bool.Parse(dataRow["LabelVisible"].ToString()));
											gadgets.ChartSeries.Add(chartSerie);
										}
									}
								}
								LayoutControlItem layoutControlItem = LayoutControl.Root.AddItem();
								GadgetContainer gadgetContainer = new GadgetContainer();
								gadgetContainer.Name = gadgets.Code;
								gadgetContainer.Title = gadgets.Title;
								gadgetContainer.RefreshData += Gadget_RefreshData;
								if (gadgets.GadgetStyle == GadgetStyles.Number)
								{
									gadgetContainer.AdjustForSmallGadgets();
								}
								IGadget gadget = GadgetsHelper.CreateGadget(gadgets);
								Control control = (Control)gadget;
								control.Dock = DockStyle.Fill;
								gadgetContainer.ControlContainer.Controls.Add(control);
								gadget.ParentDashboard = this;
								gadgetContainer.ParentDashboard = this;
								layoutControlItem.Name = gadgets.Code;
								layoutControlItem.Control = gadgetContainer;
								layoutControlItem.Text = gadgetContainer.Title;
								layoutControlItem.TextVisible = false;
								layoutControlItem.Tag = gadgets;
								gadget.LoadData(isRefresh: false);
							}
						}
						dashboardLayout.Layout.Seek(0L, SeekOrigin.Begin);
						LayoutControl.RestoreLayoutFromStream(dashboardLayout.Layout);
					}
					SetEditMode(isEdit: false);
					isLoaded = true;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				LayoutControl.EndUpdate();
			}
		}

		private void Gadget_RefreshData(object sender, EventArgs e)
		{
			try
			{
				GadgetContainer gadgetContainer = sender as GadgetContainer;
				if (gadgetContainer != null)
				{
					foreach (Control control in gadgetContainer.ControlContainer.Controls)
					{
						(control as IGadget).LoadData(isRefresh: true);
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
			SuspendLayout();
			BackColor = System.Drawing.Color.Transparent;
			MaximumSize = new System.Drawing.Size(22, 20);
			MinimumSize = new System.Drawing.Size(22, 20);
			base.Name = "FlatDashboard";
			base.Size = new System.Drawing.Size(22, 20);
			ResumeLayout(false);
		}
	}
}
