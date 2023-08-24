using System;

namespace Micromind.Common.Data
{
	public class OrderStatus
	{
		private OrderStatusEnum status;

		public string Name => Enum.GetName(typeof(OrderStatusEnum), status);

		public byte Value
		{
			get
			{
				return checked((byte)status);
			}
			set
			{
				status = (OrderStatusEnum)value;
			}
		}

		public OrderStatus(OrderStatusEnum val)
		{
			status = val;
		}
	}
}
