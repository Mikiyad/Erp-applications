using Micromind.Common.Data;

namespace Micromind.Common.Interfaces
{
	public interface IForm
	{
		ScreenAreas ScreenArea
		{
			get;
		}

		int ScreenID
		{
			get;
		}

		ScreenTypes ScreenType
		{
			get;
		}
	}
}
