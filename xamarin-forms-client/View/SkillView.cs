using System;
using RS.Shared.Model;
using Xamarin.Forms;

namespace RS.Shared.View
{
	public class SkillView : BaseView
	{
		public SkillView (Skill selectedItem)
		{
			Title = "Skill - " + selectedItem.title;

			var listView = new ListView ();
			listView.ItemsSource = selectedItem.levels;

			var cell = new DataTemplate(typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "title");

			listView.ItemTemplate = cell;
			listView.ItemTapped += (sender, args) =>
			{
				listView.SelectedItem = null;
			};

			Content = listView;
		}
	}
}