using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System;
using System.Windows.Forms;

namespace Micromind.DataControls.MMSDataGrid
{
	public class CellButtonEditor : ControlContainerEditor
	{
		private ButtonTextBoxControl buttonControl;

		public string Name => buttonControl.Name;

		protected override object RendererValue
		{
			get
			{
				return buttonControl.Text;
			}
			set
			{
				buttonControl.Text = value.ToString();
			}
		}

		public event EventHandler ButtonClicked;

		public CellButtonEditor(string name, string buttonText, HAlign hAlign, Type valueType, string format)
		{
			base.EnterEditModeMouseBehavior = EnterEditModeMouseBehavior.EnterEditModeAndClick;
			buttonControl = new ButtonTextBoxControl(name, buttonText, hAlign, valueType, format);
			buttonControl.ButtonClicked += ButtonControl_ButtonClicked;
			base.RenderingControl = buttonControl;
			ButtonTextBoxControl buttonTextBoxControl = new ButtonTextBoxControl(name, buttonText, hAlign, valueType, format);
			buttonTextBoxControl.BorderStyle = BorderStyle.None;
			buttonTextBoxControl.ButtonClicked += ButtonControl_ButtonClicked;
			base.EditingControl = buttonTextBoxControl;
		}

		public CellButtonEditor(string name, string buttonText, string secondButtonName, string secondButtonText, HAlign hAlign, Type valueType, string format)
		{
			base.EnterEditModeMouseBehavior = EnterEditModeMouseBehavior.EnterEditModeAndClick;
			buttonControl = new ButtonTextBoxControl(name, buttonText, secondButtonName, secondButtonText, hAlign, valueType, format);
			buttonControl.ButtonClicked += ButtonControl_ButtonClicked;
			base.RenderingControl = buttonControl;
			ButtonTextBoxControl buttonTextBoxControl = new ButtonTextBoxControl(name, buttonText, secondButtonName, secondButtonText, hAlign, valueType, format);
			buttonTextBoxControl.BorderStyle = BorderStyle.None;
			buttonTextBoxControl.ButtonClicked += ButtonControl_ButtonClicked;
			base.EditingControl = buttonTextBoxControl;
		}

		private void ButtonControl_ButtonClicked(object sender, EventArgs e)
		{
			if (this.ButtonClicked != null)
			{
				this.ButtonClicked(sender, e);
			}
		}
	}
}
