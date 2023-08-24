using System;
using System.Collections;
using System.Threading;

namespace Micromind.Utilities.HTTP.Spider
{
	public class Spider
	{
		private enum Status
		{
			STATUS_FAILED,
			STATUS_SUCCESS,
			STATUS_QUEUED
		}

		private Hashtable m_already;

		private Queue m_workload;

		private Uri m_base;

		private string m_outputPath;

		private int m_urlCount;

		private long m_startTime;

		private Done m_done = new Done();

		private bool m_quit;

		private int urlCount;

		private string lastURL = "";

		private long eTime;

		private long urls;

		public Uri BaseURI
		{
			get
			{
				return m_base;
			}
			set
			{
				m_base = value;
			}
		}

		public string OutputPath
		{
			get
			{
				return m_outputPath;
			}
			set
			{
				m_outputPath = value;
			}
		}

		public bool Quit
		{
			get
			{
				return m_quit;
			}
			set
			{
				m_quit = value;
			}
		}

		public Done SpiderDone => m_done;

		public int URLCount => urlCount;

		public string LastURL => lastURL;

		public long ElapsedTime => eTime;

		public long URLS => urls;

		public event EventHandler URLProcessed;

		public Spider()
		{
			reset();
		}

		public void reset()
		{
			m_already = new Hashtable();
			m_workload = new Queue();
			m_quit = false;
		}

		public void addURI(Uri uri)
		{
			Monitor.Enter(this);
			if (!m_already.Contains(uri))
			{
				m_already.Add(uri, Status.STATUS_QUEUED);
				m_workload.Enqueue(uri);
			}
			Monitor.Pulse(this);
			Monitor.Exit(this);
		}

		public Uri ObtainWork()
		{
			Monitor.Enter(this);
			while (m_workload.Count < 1)
			{
				Monitor.Wait(this);
			}
			Uri uri = (Uri)m_workload.Dequeue();
			lastURL = uri.ToString();
			urlCount = m_urlCount++;
			eTime = (DateTime.Now.Ticks - m_startTime) / 10000000;
			urls = ((eTime == 0L) ? 0 : (m_urlCount / eTime));
			if (this.URLProcessed != null)
			{
				this.URLProcessed(this, null);
			}
			Monitor.Exit(this);
			return uri;
		}

		public void Start(Uri baseURI, int threads)
		{
			m_quit = false;
			m_base = baseURI;
			addURI(m_base);
			m_startTime = DateTime.Now.Ticks;
			m_done.Reset();
			for (int i = 1; i < threads; i++)
			{
				DocumentWorker documentWorker = new DocumentWorker(this);
				documentWorker.Number = i;
				documentWorker.start();
			}
			m_done.WaitBegin();
			m_done.WaitDone();
		}
	}
}
