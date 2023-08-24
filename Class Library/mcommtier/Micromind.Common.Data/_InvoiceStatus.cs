using System;

namespace Micromind.Common.Data
{
	public class _InvoiceStatus
	{
		private InvoiceStatus status;

		public string Name => Enum.GetName(typeof(InvoiceStatus), status);

		public byte Value
		{
			get
			{
				return checked((byte)status);
			}
			set
			{
				status = (InvoiceStatus)value;
			}
		}

		public _InvoiceStatus(InvoiceStatus val)
		{
			status = val;
		}
	}
}
