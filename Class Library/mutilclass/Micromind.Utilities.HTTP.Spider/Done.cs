using System.Threading;

namespace Micromind.Utilities.HTTP.Spider
{
	public class Done
	{
		private int m_activeThreads;

		private bool m_started;

		public void WaitDone()
		{
			Monitor.Enter(this);
			while (m_activeThreads > 0)
			{
				Monitor.Wait(this);
			}
			Monitor.Exit(this);
		}

		public void WaitBegin()
		{
			Monitor.Enter(this);
			while (!m_started)
			{
				Monitor.Wait(this);
			}
			Monitor.Exit(this);
		}

		public void WorkerBegin()
		{
			Monitor.Enter(this);
			m_activeThreads++;
			m_started = true;
			Monitor.Pulse(this);
			Monitor.Exit(this);
		}

		public void WorkerEnd()
		{
			Monitor.Enter(this);
			m_activeThreads--;
			Monitor.Pulse(this);
			Monitor.Exit(this);
		}

		public void Reset()
		{
			Monitor.Enter(this);
			m_activeThreads = 0;
			Monitor.Exit(this);
		}
	}
}
