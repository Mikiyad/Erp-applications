using Micromind.ClientLibraries;

namespace Micromind.DataControls
{
	internal class DataControlTranslator : TranslatorBase
	{
		protected static DataControlTranslator translator = new DataControlTranslator(isRead: true);

		internal static DataControlTranslator Translators => translator;

		internal DataControlTranslator(bool isRead)
			: base(isRead)
		{
		}
	}
}
