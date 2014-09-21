using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace RS.Shared.ViewModel
{
	public class BaseViewModel : INotifyPropertyChanged, INotifyPropertyChanging
	{
		private bool isBusy;
		/// <summary>
		/// Gets or sets if the view is busy.
		/// </summary>
		public const string IsBusyPropertyName = "IsBusy";
		public bool IsBusy 
		{
			get { return isBusy; }
			set { SetProperty (ref isBusy, value, IsBusyPropertyName);}
		}

		public BaseViewModel ()
		{
		}

		protected void SetProperty<T>(ref T backingStore, T value,
			string propertyName, 
			Action onChanged = null,
			Action<T> onChanging = null) 
		{
			if (EqualityComparer<T>.Default.Equals(backingStore, value)) 
				return;

			if (onChanging != null) 
				onChanging(value);

			OnPropertyChanging(propertyName);

			backingStore = value;

			if (onChanged != null) 
				onChanged();

			OnPropertyChanged(propertyName);
		}
		
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

		#endregion

		#region INotifyPropertyChanging implementation

		public event PropertyChangingEventHandler PropertyChanging;

		public void OnPropertyChanging(string propertyName)
		{
			if (PropertyChanging == null)
				return;

			PropertyChanging (this, new PropertyChangingEventArgs (propertyName));
		}

		#endregion
	}
}