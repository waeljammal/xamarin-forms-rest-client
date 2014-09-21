using System;
using Xamarin.Forms;
using RS.Shared.ViewModel;
using RS.Shared.View;

namespace RS.Shared.View
{
	public class SkillsListView : BaseView
	{
		private SkillsListViewModel ViewModel
		{
			get { return BindingContext as SkillsListViewModel; }
		}

		public SkillsListView()
		{
			BindingContext = new SkillsListViewModel ();

			var refresh = new ToolbarItem {
				Command = ViewModel.LoadAllItemsCommand,
				Icon = "refresh.png",
				Name = "refresh",
				Priority = 0
			};

			ToolbarItems.Add (refresh);

			var stack = new StackLayout {
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness(0, 8, 0, 8)
			};

			var activity = new ActivityIndicator {
				Color = Util.Color.DarkBlue.ToFormsColor(),
				IsEnabled = true
			};
			activity.SetBinding (ActivityIndicator.IsVisibleProperty, "IsBusy");
			activity.SetBinding (ActivityIndicator.IsRunningProperty, "IsBusy");

			stack.Children.Add (activity);

			var listView = new ListView ();
			listView.ItemsSource = ViewModel.SkillItems;

			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "title");

//			listView.ItemTapped +=  (sender, args) => {
//				if(listView.SelectedItem == null)
//					return;
//				this.Navigation.PushAsync(new SkillView(listView.SelectedItem as SkillView));
//				listView.SelectedItem = null;
//			};

			listView.ItemTemplate = cell;

			stack.Children.Add (listView);

			Content = stack;
		}

		protected override void OnAppearing()
		{
			ViewModel.LoadAllItemsCommand.Execute (null);
		}
	}
}