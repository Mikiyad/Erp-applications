using System;
using System.Collections;
using System.Windows.Forms;

namespace Micromind.DataControls.QueryBuilder.Forms
{
	public class frmBaseForm : Form
	{
		private static ArrayList mi_OpenWindows = new ArrayList();

		private Timer mi_TimerLoad = new Timer();

		protected int SiblingCount
		{
			get
			{
				int num = 0;
				foreach (object mi_OpenWindow in mi_OpenWindows)
				{
					if (mi_OpenWindow.GetType() == GetType())
					{
						num++;
					}
				}
				return num - 1;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			mi_TimerLoad.Interval = 100;
			mi_TimerLoad.Tick += OnTimerLoad;
			mi_TimerLoad.Start();
		}

		private void OnTimerLoad(object sender, EventArgs e)
		{
			mi_TimerLoad.Stop();
			OnLoadDelayed();
		}

		protected virtual void OnLoadDelayed()
		{
		}

		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			mi_OpenWindows.Remove(this);
		}
	}
}
