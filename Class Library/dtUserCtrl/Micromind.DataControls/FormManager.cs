using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using Micromind.ClientLibraries;
using Micromind.Common.Data;
using Micromind.DataControls.MMSDataGrid;
using Micromind.DataControls.OtherControls;
using Micromind.UISupport;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class FormManager : Control
	{
		private bool isApproval;

		private string tableName = "";

		private string idColumnName = "";

		private Hashtable hashTable = new Hashtable();

		private IContainer components;

		public bool IsForcedDirty
		{
			get;
			set;
		}

		public FormManager()
		{
			InitializeComponent();
		}

		public void ResetDirty()
		{
			if (base.Parent != null)
			{
				hashTable.Clear();
				AddControl(base.Parent);
				IsForcedDirty = false;
			}
		}

		private void AddControl(Control parent)
		{
			try
			{
				foreach (Control control in parent.Controls)
				{
					if (!(control.GetType() == typeof(NonDirtyPanel)) && !(control.GetType() == typeof(SysDocComboBox)))
					{
						if (control.Controls.Count > 0)
						{
							AddControl(control);
						}
						if (!hashTable.ContainsKey(control.Name))
						{
							if (control.GetType() == typeof(TextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							if (control.GetType() == typeof(UnitPriceTextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							else if (control.GetType().BaseType == typeof(TextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							else if (control.GetType().BaseType == typeof(MMTextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							else if (control.GetType() == typeof(NumberTextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							else if (control.GetType() == typeof(NumericUpDown))
							{
								hashTable.Add(control.Name, ((NumericUpDown)control).Value);
							}
							else if (control.GetType() == typeof(AmountTextBox))
							{
								hashTable.Add(control.Name, control.Text);
							}
							else if (control.GetType() == typeof(AnalysisGroupComboBox))
							{
								hashTable.Add(control.Name, ((AnalysisGroupComboBox)control).SelectedID);
							}
							else if (control.GetType().BaseType == typeof(MultiColumnComboBox))
							{
								hashTable.Add(control.Name, ((MultiColumnComboBox)control).SelectedID);
							}
							else if (control.GetType().BaseType == typeof(UltraComboEditor))
							{
								hashTable.Add(control.Name, ((UltraComboEditor)control).SelectedIndex);
							}
							else if (control.GetType().BaseType == typeof(SingleColumnComboBox))
							{
								hashTable.Add(control.Name, ((SingleColumnComboBox)control).SelectedIndex);
							}
							else if (control.GetType() == typeof(ComboBox))
							{
								hashTable.Add(control.Name, ((ComboBox)control).SelectedIndex);
							}
							else if (control.GetType().BaseType == typeof(ComboBox))
							{
								hashTable.Add(control.Name, ((ComboBox)control).SelectedIndex);
							}
							else if (control.GetType() == typeof(CheckBox))
							{
								hashTable.Add(control.Name, ((CheckBox)control).Checked);
							}
							else if (control.GetType() == typeof(CurrencySelector))
							{
								hashTable.Add(control.Name, ((CurrencySelector)control).SelectedID + ((CurrencySelector)control).Rate.ToString());
							}
							else if (control.GetType() == typeof(RadioButton))
							{
								hashTable.Add(control.Name, ((RadioButton)control).Checked);
							}
							else if (control.GetType() == typeof(DateTimePicker))
							{
								hashTable.Add(control.Name, ((DateTimePicker)control).Value);
							}
							else if (control.GetType() == typeof(Micromind.UISupport.flatDatePicker))
							{
								hashTable.Add(control.Name, ((Micromind.UISupport.flatDatePicker)control).Value);
							}
							else if (control.GetType() == typeof(flatDatePicker))
							{
								hashTable.Add(control.Name, ((flatDatePicker)control).Value);
							}
							else if (control.GetType() == typeof(MMSDateTimePicker))
							{
								hashTable.Add(control.Name, ((MMSDateTimePicker)control).Value);
							}
							else if (control.GetType() == typeof(ListBox))
							{
								ListBox listBox = control as ListBox;
								string text = "";
								foreach (object item in listBox.Items)
								{
									text += item.GetHashCode().ToString();
								}
								text += listBox.Items.Count;
								hashTable.Add(control.Name, text);
							}
							else if (control.GetType().BaseType == typeof(UltraGrid))
							{
								hashTable.Add(control.Name, "");
							}
							else if (control.GetType() == typeof(POSGrid))
							{
								hashTable.Add(control.Name, "");
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public bool GetDirtyStatus()
		{
			if (isApproval)
			{
				return false;
			}
			if (IsForcedDirty)
			{
				return true;
			}
			if (hashTable.Count == 0)
			{
				return false;
			}
			if (base.Parent == null)
			{
				return false;
			}
			return CheckControlsDirtyStatus(base.Parent);
		}

		private bool CheckControlsDirtyStatus(Control parent)
		{
			try
			{
				if (parent == null)
				{
					return false;
				}
				foreach (Control control in parent.Controls)
				{
					if (!(control.GetType() == typeof(NonDirtyPanel)))
					{
						if (control.Controls.Count > 0 && CheckControlsDirtyStatus(control))
						{
							return true;
						}
						if (GetControlDirtyStatus(control))
						{
							return true;
						}
					}
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			return false;
		}

		public bool GetControlDirtyStatus(Control c)
		{
			try
			{
				if (c == null)
				{
					return false;
				}
				if (!hashTable.Contains(c.Name))
				{
					return false;
				}
				if (c.GetType() == typeof(SysDocComboBox))
				{
					return false;
				}
				if (c.GetType() == typeof(TextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType() == typeof(UnitPriceTextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType().BaseType == typeof(MMTextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType() == typeof(MMTextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType() == typeof(NumberTextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType() == typeof(NumericUpDown) && hashTable[c.Name].ToString() != ((NumericUpDown)c).Value.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(AmountTextBox) && hashTable[c.Name].ToString() != c.Text)
				{
					return true;
				}
				if (c.GetType().BaseType == typeof(MultiColumnComboBox) && c.Visible && hashTable[c.Name].ToString() != ((MultiColumnComboBox)c).SelectedID)
				{
					return true;
				}
				if (c.GetType().BaseType == typeof(UltraComboEditor) && hashTable[c.Name].ToString() != ((UltraComboEditor)c).SelectedIndex.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(ComboBox) && hashTable[c.Name].ToString() != ((ComboBox)c).SelectedIndex.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(CurrencySelector) && hashTable[c.Name].ToString() != ((CurrencySelector)c).SelectedID.ToString() + ((CurrencySelector)c).Rate.ToString())
				{
					return true;
				}
				if (c.GetType().BaseType == typeof(SingleColumnComboBox) && hashTable[c.Name].ToString() != ((SingleColumnComboBox)c).SelectedIndex.ToString())
				{
					return true;
				}
				if (c.GetType().BaseType == typeof(ComboBox) && hashTable[c.Name].ToString() != ((ComboBox)c).SelectedIndex.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(CheckBox) && hashTable[c.Name].ToString() != ((CheckBox)c).Checked.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(RadioButton) && hashTable[c.Name].ToString() != ((RadioButton)c).Checked.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(DateTimePicker) && hashTable[c.Name].ToString() != ((DateTimePicker)c).Value.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(Micromind.UISupport.flatDatePicker) && hashTable[c.Name].ToString() != ((Micromind.UISupport.flatDatePicker)c).Value.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(flatDatePicker) && hashTable[c.Name].ToString() != ((flatDatePicker)c).Value.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(MMSDateTimePicker) && hashTable[c.Name].ToString() != ((MMSDateTimePicker)c).Value.ToString())
				{
					return true;
				}
				if (c.GetType() == typeof(ListBox))
				{
					ListBox listBox = c as ListBox;
					string text = "";
					foreach (object item in listBox.Items)
					{
						text += item.GetHashCode().ToString();
					}
					text += listBox.Items.Count;
					if (hashTable[c.Name].ToString() != text)
					{
						return true;
					}
				}
				if (c.GetType().BaseType == typeof(UltraGrid) || c.GetType() == typeof(POSGrid))
				{
					UltraGrid ultraGrid = c as UltraGrid;
					if (c.GetType() == typeof(POSGrid))
					{
						ultraGrid = ((POSGrid)c).DataGrid;
					}
					if (ultraGrid.DataSource == null)
					{
						return false;
					}
					if (ultraGrid.DataSource.GetType() == typeof(DataTable))
					{
						DataTable dataTable = ultraGrid.DataSource as DataTable;
						if (dataTable != null)
						{
							dataTable = dataTable.GetChanges();
							if (dataTable != null && dataTable.Rows.Count > 0)
							{
								return true;
							}
							return false;
						}
					}
					else
					{
						if (!(ultraGrid.DataSource.GetType() == typeof(DataSet)))
						{
							return false;
						}
						DataSet dataSet = ultraGrid.DataSource as DataSet;
						if (dataSet == null)
						{
							return false;
						}
						foreach (DataTable table in dataSet.Tables)
						{
							if (table != null)
							{
								DataTable changes = table.GetChanges();
								if (changes != null && changes.Rows.Count > 0)
								{
									return true;
								}
								return false;
							}
						}
					}
				}
				return false;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				return false;
			}
		}

		public void SetControlDirtyStatus(Control c, object controlValue)
		{
			if (hashTable.Count != 0 && hashTable.ContainsKey(c.Name))
			{
				hashTable[c.Name] = controlValue;
			}
		}

		public bool IsFieldDirty(Control field)
		{
			return GetControlDirtyStatus(field);
		}

		public void ShowApprovalPanel(int approvalTaskID, string tableName, string idColumnName)
		{
			this.tableName = tableName;
			this.idColumnName = idColumnName;
			isApproval = true;
			foreach (Control control in FindForm().Controls)
			{
				if (control.GetType() == typeof(ApprovalPanel))
				{
					return;
				}
			}
			ApprovalPanel approvalPanel = new ApprovalPanel();
			approvalPanel.TableName = this.tableName;
			approvalPanel.IDColumnName = this.idColumnName;
			GlobalEvents.ApprovalStatusChanged += GlobalEvents_ApprovalStatusChanged;
			FindForm().Controls.Add(approvalPanel);
			approvalPanel.BringToFront();
			approvalPanel.ApprovalTaskID = approvalTaskID;
			approvalPanel.Dock = DockStyle.Bottom;
			approvalPanel.Show();
		}

		private void GlobalEvents_ApprovalStatusChanged(ApprovalStatus newStatus, int taskID)
		{
			ResetDirty();
			FindForm();
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
			BackColor = System.Drawing.Color.RosyBrown;
			Dock = System.Windows.Forms.DockStyle.None;
			MaximumSize = new System.Drawing.Size(20, 20);
			MinimumSize = new System.Drawing.Size(20, 20);
			base.Size = new System.Drawing.Size(20, 20);
			base.Visible = false;
			ResumeLayout(false);
		}
	}
}
