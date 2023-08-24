using System;

namespace Micromind.DataControls.Libraries
{
	public static class StockDataCreator
	{
		private const int BeginDate = 40000;

		private static Random r = new Random();

		public static StockData GetData(int index)
		{
			int num = r.Next(-5, 5);
			return new StockData
			{
				Date = DateTime.FromOADate(40000 + index),
				HighPrice = (decimal)r.Next(20 + num, 30 + num) + (decimal)Math.Round(r.NextDouble(), 2),
				LowPrice = (decimal)r.Next(10 + num, 18 + num) + (decimal)Math.Round(r.NextDouble(), 2),
				OpenPrice = (decimal)r.Next(20 + num, 25 + num) + (decimal)Math.Round(r.NextDouble(), 2),
				ClosePrice = (decimal)r.Next(15 + num, 20 + num) + (decimal)Math.Round(r.NextDouble(), 2),
				Volumne = r.Next(10000, 18000)
			};
		}
	}
}
