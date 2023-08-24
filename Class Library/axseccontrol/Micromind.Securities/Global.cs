namespace Micromind.Securities
{
	internal sealed class Global
	{
		private Global()
		{
		}

		internal static int GetOffset()
		{
			return int.Parse("103") + RulesReader.VersionNumber * 3;
		}

		internal static int GetIDPrime()
		{
			if (RulesReader.VersionNumber >= 2 && RulesReader.VersionNumber < 3)
			{
				return int.Parse("503");
			}
			if (RulesReader.VersionNumber >= 3 && RulesReader.VersionNumber < 4)
			{
				return int.Parse("509");
			}
			if (RulesReader.VersionNumber >= 4 && RulesReader.VersionNumber < 5)
			{
				return int.Parse("521");
			}
			if (RulesReader.VersionNumber >= 5 && RulesReader.VersionNumber < 6)
			{
				return int.Parse("523");
			}
			if (RulesReader.VersionNumber >= 6 && RulesReader.VersionNumber < 7)
			{
				return int.Parse("541");
			}
			if (RulesReader.VersionNumber >= 7 && RulesReader.VersionNumber < 8)
			{
				return int.Parse("547");
			}
			if (RulesReader.VersionNumber >= 8 && RulesReader.VersionNumber < 9)
			{
				return int.Parse("557");
			}
			if (RulesReader.VersionNumber >= 9 && RulesReader.VersionNumber < 10)
			{
				return int.Parse("569");
			}
			if (RulesReader.VersionNumber >= 10 && RulesReader.VersionNumber < 11)
			{
				return int.Parse("571");
			}
			if (RulesReader.VersionNumber >= 11 && RulesReader.VersionNumber < 12)
			{
				return int.Parse("577");
			}
			if (RulesReader.VersionNumber >= 12 && RulesReader.VersionNumber < 13)
			{
				return int.Parse("587");
			}
			if (RulesReader.VersionNumber >= 13 && RulesReader.VersionNumber < 14)
			{
				return int.Parse("593");
			}
			return int.Parse("599");
		}
	}
}
