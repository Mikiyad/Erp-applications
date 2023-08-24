using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class SlidingPanelContainer : Panel
	{
		private ArrayList controlsArray = new ArrayList();

		private Container components;

		public SlidingPanelContainer()
		{
			InitializeComponent();
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
			base.Name = "SlidingPanelContainer";
			base.Size = new System.Drawing.Size(424, 240);
		}

		private void SlidingPanelContainer_Load(object sender, EventArgs e)
		{
		}

		public void ArrangeControls()
		{
			int num = 0;
			foreach (object item in controlsArray)
			{
				SlidingPanel slidingPanel = item as SlidingPanel;
				if (slidingPanel != null && slidingPanel.Visible)
				{
					slidingPanel.Top = num;
					num += slidingPanel.Height + 5;
				}
			}
			base.Height = num + 5;
		}

		public bool IsAnyChildPanelVisible()
		{
			foreach (object item in controlsArray)
			{
				SlidingPanel slidingPanel = item as SlidingPanel;
				if (slidingPanel != null && slidingPanel.Visible)
				{
					return true;
				}
			}
			return false;
		}

		public void AddControl(SlidingPanel panel)
		{
			controlsArray.Add(panel);
			panel.SizeChanged += panel_SizeChanged;
		}

		public void ClearControls()
		{
			controlsArray.Clear();
		}

		private void panel_SizeChanged(object sender, EventArgs e)
		{
			ArrangeControls();
		}
	}
}
