namespace Micromind.ClientLibraries
{
	public class MessageTranslator : TranslatorBase
	{
		private static TranslatorBase translator;

		public static TranslatorBase Translator
		{
			get
			{
				return translator;
			}
			set
			{
				translator = value;
			}
		}

		internal MessageTranslator(bool isRead)
			: base(isRead)
		{
		}
	}
}
