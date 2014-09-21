using System;
using Xamarin.Forms;
using RS.Shared.ViewModel;
using RS.Shared.Model;

namespace RS.Shared.View
{
	public class HomeView : BaseView
	{
		private SkillsListViewModel ViewModel
		{
			get { return BindingContext as SkillsListViewModel; }
		}

		public HomeView () 
		{
			Title = "Skills";
			BindingContext = new SkillsListViewModel ();

			var refresh = new ToolbarItem {
				Command = ViewModel.LoadAllItemsCommand,
				Icon = "refresh.png",
				Name = "refresh",
				Priority = 0
			};

			ToolbarItems.Add (refresh);

			Label header = new Label
			{
				Text = "Skills View",
				BackgroundColor = Color.Blue,
				Font = Font.BoldSystemFontOfSize(30),
				HorizontalOptions = LayoutOptions.Center
			};

			var listView = new ListView ();
			listView.ItemsSource = ViewModel.SkillItems;

			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "title");

			listView.ItemTemplate = cell;

			listView.ItemTapped += (sender, args) =>
			{
				var skill = args.Item as Skill;

				if (skill == null)
					return;

				Navigation.PushAsync(new SkillView(skill));

				// Reset the selected item
				listView.SelectedItem = null;
			};

			var stackPanel = new StackLayout();
			stackPanel.Padding = new Thickness(0, 0, 0, 0);
			stackPanel.Children.Insert(1, listView);

			var activity = new ActivityIndicator {
				Color = Util.Color.DarkBlue.ToFormsColor(),
				IsEnabled = true
			};
			activity.SetBinding (ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding (ActivityIndicator.IsRunningProperty, "IsBusy");

			stackPanel.Children.Add (activity);

			if (Device.OS == TargetPlatform.WinPhone) {
				stackPanel.Children.Insert (0, header);
			}

			Content = stackPanel;
		}

		protected override void OnAppearing()
		{
			ViewModel.LoadAllItemsCommand.Execute (null);
		}
	}
}

