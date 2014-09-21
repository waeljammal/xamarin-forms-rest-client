using System;
using Xamarin.Forms;
using RS.Shared.View;

namespace RS.Shared
{
	public static class App
	{
		private static Page homeView;

		public static Page GetMainPage()
		{
			homeView = new HomeView();
			return new NavigationPage(homeView);
		}
	}
}