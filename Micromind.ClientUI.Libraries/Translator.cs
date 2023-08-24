using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraNavBar;
using Infragistics.Win.FormattedLinkLabel;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinTabControl;
using Infragistics.Win.UltraWinTree;
using Micromind.ClientLibraries;
using Micromind.ClientUI.Configurations;
using Micromind.Common.Libraries;
using Micromind.DataControls;
using Micromind.UISupport;
using Micromind.UISupport.Controls;
using Micromind.UISupport.GUIControls.Others;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Micromind.ClientUI.Libraries
{
	internal class Translator : TranslatorBase
	{
		private static Translator translator = new Translator(isRead: true);

		private int numberOfTimesLanguagesChanged;

		internal static Translator Translators => translator;

		public int NumberOfTimesLanguagesChanged => numberOfTimesLanguagesChanged;

		internal Translator(bool isRead)
			: base(isRead)
		{
			MessageTranslator.Translator = this;
		}

		public new void ChangeLanguage(string languageName)
		{
			checked
			{
				numberOfTimesLanguagesChanged++;
				base.ChangeLanguage(languageName);
			}
		}

		public void Translate()
		{
			if (TranslatorBase.isRead)
			{
				throw new ApplicationException("This module is for creating an xml file.");
			}
			base.OverwriteExistingKey = false;
			CreateFile();
			TranslatorBase.isFileExist = true;
			isCreatingTranslation = true;
			bool isTranslatorActive = TranslatorBase.isTranslatorActive;
			TranslatorBase.isTranslatorActive = true;
			Refresh();
			try
			{
				Type[] types = GetType().Assembly.GetTypes();
				foreach (Type type in types)
				{
					Form form = null;
					if (type.BaseType == typeof(Form) || type.BaseType == typeof(Micromind.ClientUI.Configurations.DialogBoxBaseForm))
					{
						_ = type.Name;
						form = UIRefelector.GetForm(type);
						if (form != null)
						{
							TranslatorBase.isRead = false;
							Translate(form);
						}
					}
				}
				for (int j = 0; j < Messages.Count; j = checked(j + 1))
				{
					WriteText(Messages.GetOriginalMessage(j));
				}
				PublicFunctions.StartProcess(XMLReader.FilePath);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
			finally
			{
				isCreatingTranslation = false;
				TranslatorBase.isTranslatorActive = isTranslatorActive;
			}
		}

		public void Translate(Form form)
		{
			if (!TranslatorBase.isTranslatorActive || !TranslatorBase.isFileExist || form == null)
			{
				return;
			}
			foreach (Control control in form.Controls)
			{
				Translate(control);
			}
			if (!TranslatorBase.isRead)
			{
				WriteText(form.Text);
			}
			else
			{
				string text = GetText(form.Text);
				if (text != null && text.Trim() != string.Empty)
				{
					form.Text = text;
				}
			}
			MenuStrip mainMenuStrip = form.MainMenuStrip;
			if (mainMenuStrip != null)
			{
				Translate(mainMenuStrip);
			}
			ContextMenu contextMenu = form.ContextMenu;
			if (contextMenu != null)
			{
				Translate(contextMenu);
			}
		}

		public void Translate(Control control)
		{
			if (!TranslatorBase.isFileExist || control == null)
			{
				return;
			}
			if (control.ContextMenuStrip != null)
			{
				foreach (ToolStripItem item in control.ContextMenuStrip.Items)
				{
					Translate(item);
				}
			}
			if (control.GetType() == typeof(TextBox) || (control.GetType().BaseType != null && control.GetType().BaseType == typeof(MultiColumnComboBox)) || (control.GetType().BaseType != null && control.GetType().BaseType.BaseType != null && control.GetType().BaseType.BaseType == typeof(TextBox)) || control.GetType() == typeof(Line) || control.GetType() == typeof(DateTimePicker) || control.GetType() == typeof(flatDatePicker) || control.GetType() == typeof(PictureBox) || control.GetType() == typeof(CurrencySelector) || control.GetType() == typeof(FormManager) || control.GetType() == typeof(MMTextBox))
			{
				return;
			}
			if (control.GetType() == typeof(Label) || control.GetType() == typeof(MMLabel) || control.GetType() == typeof(UltraButton) || control.GetType() == typeof(UltraLabel) || control.GetType() == typeof(BALinkLabel) || control.GetType() == typeof(Button) || control.GetType() == typeof(XPButton) || control.GetType() == typeof(CheckBox) || control.GetType() == typeof(RadioButton))
			{
				if (!TranslatorBase.isRead)
				{
					WriteText(control.Text);
				}
				else
				{
					string text = GetText(control.Text);
					if (text != null && text.Trim() != string.Empty)
					{
						control.Text = text;
						Font font = null;
						font = (control.Font = ((control.Font.Unit == GraphicsUnit.Point) ? new Font("Tahoma", control.Font.Size, control.Font.Style, GraphicsUnit.Point) : new Font("Tahoma", 8f, control.Font.Style, GraphicsUnit.Point)));
					}
				}
			}
			else if (control.GetType() == typeof(UltraFormattedLinkLabel))
			{
				if (!TranslatorBase.isRead)
				{
					WriteText(control.Text);
				}
				else
				{
					string text2 = GetText(control.Text);
					if (text2 != null && text2.Trim() != string.Empty)
					{
						(control as UltraFormattedLinkLabel).Value = text2;
						Font font3 = null;
						font3 = (control.Font = ((control.Font.Unit == GraphicsUnit.Point) ? new Font("Tahoma", control.Font.Size, control.Font.Style, GraphicsUnit.Point) : new Font("Tahoma", 8f, control.Font.Style, GraphicsUnit.Point)));
					}
				}
			}
			else if (control.GetType() == typeof(LinkLabel))
			{
				if (!TranslatorBase.isRead)
				{
					WriteText(control.Text);
				}
				else
				{
					string text3 = GetText(control.Text);
					if (text3 != null && text3.Trim() != string.Empty)
					{
						(control as LinkLabel).Text = text3;
						Font font5 = null;
						font5 = (control.Font = ((control.Font.Unit == GraphicsUnit.Point) ? new Font("Tahoma", control.Font.Size, control.Font.Style, GraphicsUnit.Point) : new Font("Tahoma", 8f, control.Font.Style, GraphicsUnit.Point)));
					}
				}
			}
			else if (control.GetType() == typeof(Statistics))
			{
				Statistics statistics = control as Statistics;
				string text4 = GetText(statistics.TitleText);
				if (text4 != null && text4.Trim() != string.Empty)
				{
					statistics.TitleText = text4;
				}
			}
			else if (control.GetType() == typeof(RibbonControl))
			{
				foreach (RibbonPage page in (control as RibbonControl).Pages)
				{
					string text5 = GetText(page.Text);
					if (text5 != null && text5.Trim() != string.Empty)
					{
						page.Text = text5;
					}
					foreach (RibbonPageGroup group in page.Groups)
					{
						text5 = GetText(group.Text);
						if (text5 != null && text5.Trim() != string.Empty)
						{
							group.Text = text5;
						}
						foreach (BarItemLink itemLink in group.ItemLinks)
						{
							text5 = GetText(itemLink.Caption);
							if (text5 != null && text5.Trim() != string.Empty)
							{
								itemLink.Caption = text5;
							}
						}
					}
				}
			}
			else if (control.GetType() == typeof(NavBarControl))
			{
				NavBarControl navBarControl = control as NavBarControl;
				string text6 = GetText(navBarControl.Text);
				if (text6 != null && text6.Trim() != string.Empty)
				{
					navBarControl.Text = text6;
				}
				Translate(navBarControl);
			}
			else if (control.GetType() == typeof(Micromind.UISupport.Controls.TabControl))
			{
				Micromind.UISupport.Controls.TabControl list2 = control as Micromind.UISupport.Controls.TabControl;
				Translate(list2);
				foreach (Control control7 in control.Controls)
				{
					Translate(control7);
				}
			}
			else if (control.GetType() == typeof(MenuStrip))
			{
				foreach (ToolStripMenuItem item2 in ((MenuStrip)control).Items)
				{
					Translate(item2);
				}
			}
			else if (control.GetType() == typeof(ToolStrip))
			{
				foreach (ToolStripItem item3 in ((ToolStrip)control).Items)
				{
					Translate(item3);
				}
			}
			else if (control.GetType() == typeof(Dashboard))
			{
				Dashboard dashboard = control as Dashboard;
				Translate(dashboard);
				foreach (Control control8 in dashboard.Controls)
				{
					Translate(control8);
				}
			}
			else if (control.GetType() == typeof(UltraTabControl))
			{
				UltraTabControl ultraTabControl = control as UltraTabControl;
				if (!TranslatorBase.isFileExist)
				{
					return;
				}
				foreach (UltraTab tab in ultraTabControl.Tabs)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(tab.Text);
					}
					else
					{
						string text7 = GetText(tab.Text);
						if (text7 != null && text7.Trim() != string.Empty)
						{
							tab.Text = text7;
						}
					}
				}
				foreach (Control control9 in control.Controls)
				{
					Translate(control9);
				}
			}
			else if (control.GetType().BaseType != null && control.GetType().BaseType == typeof(UltraGrid))
			{
				UltraGrid list5 = control as UltraGrid;
				Translate(list5);
			}
			else if (control.GetType().BaseType != null && control.GetType() == typeof(UltraTree))
			{
				UltraTree list6 = control as UltraTree;
				Translate(list6);
			}
			else if (control.GetType() == typeof(GridControl))
			{
				GridControl list7 = control as GridControl;
				Translate(list7);
			}
			else if (control.GetType() == typeof(UltraGroupBox))
			{
				UltraGroupBox ultraGroupBox = control as UltraGroupBox;
				string text8 = GetText(ultraGroupBox.Text);
				if (text8 != null && text8.Trim() != string.Empty)
				{
					ultraGroupBox.Text = text8;
				}
				foreach (Control control10 in control.Controls)
				{
					Translate(control10);
				}
			}
			else
			{
				_ = (control.GetType() != typeof(Panel));
				foreach (Control control11 in control.Controls)
				{
					Translate(control11);
				}
			}
			ContextMenu contextMenu = control.ContextMenu;
			if (contextMenu != null)
			{
				Translate(contextMenu);
			}
		}

		public void Translate(MenuItem list)
		{
			if (TranslatorBase.isFileExist && list != null)
			{
				try
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(list.Text);
					}
					else
					{
						string text = GetText(list.Text);
						if (text != null && text.Trim() != string.Empty)
						{
							list.Text = text;
						}
					}
					foreach (MenuItem menuItem in list.MenuItems)
					{
						Translate(menuItem);
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public void Translate(ToolStripItem list)
		{
			if (TranslatorBase.isFileExist && list != null)
			{
				try
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(list.Text);
					}
					else
					{
						string text = GetText(list.Text);
						if (text != null && text.Trim() != string.Empty)
						{
							list.Text = text;
						}
					}
					if (list.GetType() == typeof(ToolStripDropDownButton))
					{
						foreach (ToolStripItem dropDownItem in ((ToolStripDropDownButton)list).DropDownItems)
						{
							Translate(dropDownItem);
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public void Translate(ToolStripMenuItem list)
		{
			if (TranslatorBase.isFileExist && list != null)
			{
				try
				{
					if (!(list.GetType() == typeof(ToolStripSeparator)))
					{
						if (!TranslatorBase.isRead)
						{
							WriteText(list.Text);
						}
						else
						{
							string text = GetText(list.Text);
							if (text != null && text.Trim() != string.Empty)
							{
								list.Text = text;
							}
						}
						foreach (ToolStripItem dropDownItem in list.DropDownItems)
						{
							if (!(dropDownItem.GetType() == typeof(ToolStripSeparator)))
							{
								Translate((ToolStripMenuItem)dropDownItem);
							}
						}
					}
				}
				catch (Exception e)
				{
					ErrorHelper.ProcessError(e);
				}
			}
		}

		public void Translate(ContextMenu list)
		{
			if (TranslatorBase.isFileExist)
			{
				foreach (MenuItem menuItem in list.MenuItems)
				{
					Translate(menuItem);
				}
			}
		}

		public void Translate(MainMenu list)
		{
			if (TranslatorBase.isFileExist)
			{
				foreach (MenuItem menuItem in list.MenuItems)
				{
					Translate(menuItem);
				}
			}
		}

		public void Translate(ListView list)
		{
			if (TranslatorBase.isFileExist)
			{
				foreach (System.Windows.Forms.ColumnHeader column in list.Columns)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(column.Text);
					}
					else
					{
						string text = GetText(column.Text);
						if (text != null && text.Trim() != string.Empty)
						{
							column.Text = text;
						}
					}
				}
			}
		}

		public void Translate(Dashboard dashboard)
		{
			if (TranslatorBase.isFileExist)
			{
				foreach (NavBarGroup leftGadget in dashboard.LeftGadgets)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(leftGadget.Caption);
					}
					else
					{
						string text = GetText(leftGadget.Caption);
						if (text != null && text.Trim() != string.Empty)
						{
							leftGadget.Caption = text;
						}
					}
					Translate(leftGadget);
				}
				foreach (NavBarGroup rightGadget in dashboard.RightGadgets)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(rightGadget.Caption);
					}
					else
					{
						string text2 = GetText(rightGadget.Caption);
						if (text2 != null && text2.Trim() != string.Empty)
						{
							rightGadget.Caption = text2;
						}
					}
					Translate(rightGadget);
				}
			}
		}

		public void Translate(NavBarGroup group)
		{
			if (!TranslatorBase.isRead)
			{
				WriteText(group.Caption);
			}
			else
			{
				string text = GetText(group.Caption);
				if (text != null && text.Trim() != string.Empty)
				{
					group.Caption = text;
				}
			}
			if (group.ItemLinks != null)
			{
				foreach (NavBarItemLink itemLink in group.ItemLinks)
				{
					Translate(itemLink);
				}
			}
			if (group.ControlContainer != null)
			{
				foreach (Control control in group.ControlContainer.Controls)
				{
					Translate(control);
				}
			}
		}

		public void Translate(NavBarItemLink link)
		{
			if (!TranslatorBase.isRead)
			{
				WriteText(link.Caption);
				return;
			}
			string text = GetText(link.Caption);
			if (text != null && text.Trim() != string.Empty)
			{
				link.Item.Caption = text;
			}
		}

		public void Translate(UltraGrid list)
		{
			if (TranslatorBase.isFileExist && list.DisplayLayout.Bands.Count != 0)
			{
				foreach (UltraGridColumn column in list.DisplayLayout.Bands[0].Columns)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(column.Header.Caption);
					}
					else
					{
						string text = GetText(column.Header.Caption);
						if (text != null && text.Trim() != string.Empty)
						{
							column.Header.Caption = text;
						}
					}
				}
			}
		}

		public void Translate(GridControl list)
		{
			if (TranslatorBase.isFileExist && list.FocusedView != null)
			{
				foreach (GridColumn column in ((ColumnView)list.FocusedView).Columns)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(column.FieldName);
					}
					else
					{
						string text = GetText(column.FieldName);
						if (text != null && text.Trim() != string.Empty)
						{
							column.FieldName = text;
						}
					}
				}
			}
		}

		public void Translate(NavBarControl list)
		{
			if (TranslatorBase.isFileExist && list.Groups.Count != 0)
			{
				foreach (NavBarGroup group in list.Groups)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(group.Caption);
					}
					else
					{
						string text = GetText(group.Caption);
						if (text != null && text.Trim() != string.Empty)
						{
							group.Caption = text;
						}
					}
					if (group.ControlContainer != null)
					{
						foreach (Control control in group.ControlContainer.Controls)
						{
							Translate(control);
						}
					}
				}
			}
		}

		public void Translate(UltraTreeNode node)
		{
			if (!TranslatorBase.isRead)
			{
				WriteText(node.Text);
			}
			else
			{
				string text = GetText(node.Text);
				if (text != null && text.Trim() != string.Empty)
				{
					node.Text = text;
				}
			}
			foreach (UltraTreeNode node2 in node.Nodes)
			{
				Translate(node2);
			}
		}

		public void Translate(UltraTree list)
		{
			if (TranslatorBase.isFileExist && list.Nodes.Count != 0)
			{
				foreach (UltraTreeNode node in list.Nodes)
				{
					Translate(node);
				}
			}
		}

		public void Translate(Micromind.UISupport.Controls.TabControl list)
		{
			if (TranslatorBase.isFileExist)
			{
				foreach (Micromind.UISupport.Controls.TabPage tabPage in list.TabPages)
				{
					if (!TranslatorBase.isRead)
					{
						WriteText(tabPage.Title);
					}
					else
					{
						string text = GetText(tabPage.Title);
						if (text != null && text.Trim() != string.Empty)
						{
							tabPage.Title = text;
						}
					}
				}
			}
		}

		public void SubstituteFormName(Form form)
		{
			DataSet formMenuSubstitutes = Factory.CompanyInformationSystem.GetFormMenuSubstitutes();
			List<string> list = (from x in formMenuSubstitutes.Tables[0].AsEnumerable()
				select x["FormText"].ToString()).ToList();
			if (list.Count > 0 && list.Contains(form.Text) && form.Text != "" && form.Text != string.Empty)
			{
				string filterExpression = "FormText ='" + form.Text + "'";
				DataRow[] array = formMenuSubstitutes.Tables[0].Select(filterExpression);
				form.Text = array[0]["AliasName"].ToString();
			}
		}
	}
}
