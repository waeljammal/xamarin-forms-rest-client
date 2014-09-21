using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using RS.Shared.Model;
using Newtonsoft.Json;

namespace RS.Shared.ViewModel
{
	public class SkillsListViewModel : BaseViewModel
	{
		private ObservableCollection<Skill> _skillItems = new ObservableCollection<Skill>();

		public ObservableCollection<Skill> SkillItems
		{
			get { return _skillItems; }
			set { _skillItems = value; OnPropertyChanged("SkillItems"); }
		}

		private Command loadSkillsCommand;
		public Command LoadAllItemsCommand
		{
			get { return loadSkillsCommand ?? (loadSkillsCommand = new Command (ExecuteLoadSkillsCommand)); }
		}

		private async void ExecuteLoadSkillsCommand ()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			SkillItems.Clear ();

			var client = new System.Net.Http.HttpClient ();

			client.BaseAddress = new Uri("http://10.0.2.2:8080/");

			var response = await client.GetAsync("allSkills.json");

			var skillsJson = response.Content.ReadAsStringAsync().Result;

			var rootobject = JsonConvert.DeserializeObject<Skills>(skillsJson);

			foreach (var skill in rootobject.skills)
				SkillItems.Add (skill);

			IsBusy = false;
		}
	}
}