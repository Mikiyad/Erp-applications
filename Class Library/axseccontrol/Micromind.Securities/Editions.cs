namespace Micromind.Securities
{
	public sealed class Editions
	{
		public static int Basic => int.Parse("1");

		public static int Standard => int.Parse("2");

		public static int Professional => int.Parse("3");

		public static int Premium => int.Parse("4");

		public static int Enterprise => int.Parse("5");

		private Editions()
		{
		}
	}
}
