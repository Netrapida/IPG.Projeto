using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Services;
using TK.CustomMap;
using System.Collections.ObjectModel;
using Microcharts;

namespace IPG.Projeto.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Pin> DataStore => DependencyService.Get<IDataStore<Pin>>() ?? new MockDataStore();
        public ObservableCollection<Chart> Charts { get; set; }

        // startup da posição do mapa
        MapSpan _mapRegion = MapSpan.FromCenterAndRadius(new Position(40.77196501, -7.35307698), Distance.FromKilometers(200));



        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        /// <summary>
        /// Map region bound to <see cref="TKCustomMap"/>
        /// </summary>
        public MapSpan MapRegion
        {
            get { return _mapRegion; }
            set
            {
                if (_mapRegion != value)
                {
                    _mapRegion = value;
                    OnPropertyChanged("MapRegion");
                }
            }
        }   

















        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
