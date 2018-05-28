using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using IPG.Projeto.Mobile.Controls;
using TK.CustomMap;
using Xamarin.Forms;
using Acr.UserDialogs;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;
using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.Helper;
using System.Linq;

namespace IPG.Projeto.Mobile.ViewModels
{

    public class PinsViewModel : BaseViewModel
    {
        public ObservableCollection<Pin> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadMeCommand { get; set; }
        public ObservableCollection<Stats> Statistics { get; set; } // testes

        public PinsViewModel()
        {
            Items = new ObservableCollection<Pin>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Pin>(this, "AddItem", (obj, item) =>
            {
                var _item = item as Pin;
                Items.Add(_item); // refresh pinos sem adicionar novo na lista evitar duplicação
            });

            LoadMeCommand = new Command(async () => await ExecuteLoadMeCommand()); //me 


        }

        private readonly ApiServices _apiServices = new ApiServices();
        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        async Task ExecuteLoadMeCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                using (UserDialogs.Instance.Loading("A minha posição..."))
                {
                    var me = await Utils.GetCurrentPosition();
                    MapRegion = MapSpan.FromCenterAndRadius(new Position(me.Latitude, me.Longitude), Distance.FromKilometers(2));

                    UserDialogs.Instance.Loading("Informações sobre a minha localização...");
                    var info = await Utils.RefreshDataAsync(me);
                    MyPositionCouncil_1 = info.CouncilInfo[1].Name;
                    MyPositionCouncil_0 = info.CouncilInfo[0].Name;

                    List<Chart> Charts = await _apiServices.ChartCouncilAsync(info.CouncilInfo[0].Id);
                    ChartReported = Charts[0];
                    ChartFix = Charts[1];


                }

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }


        }


        // maps council cenas !!!!!!!!!!!!!!!!!!!!!!! REVER COLOCAR numa  LIST<string>
        string myPositionCouncil_0 = string.Empty;

        public string MyPositionCouncil_0
        {
            get { return myPositionCouncil_0; }
            set { SetProperty(ref myPositionCouncil_0, value); }
        }
        string myPositionCouncil_1 = string.Empty;
        public string MyPositionCouncil_1
        {
            get { return myPositionCouncil_1; }
            set { SetProperty(ref myPositionCouncil_1, value); }
        }

        //-----------------------------  REVER ------------------------------------------
        Chart chartReported = new LineChart(){ Entries = new[] { new Entry(0) }, };
        public Chart ChartReported
            {
            get { return chartReported; }
            set { SetProperty(ref chartReported, value); }
        }
        Chart chartFix = new LineChart() { Entries = new[] { new Entry(0) }, };
        public Chart ChartFix
        {
            get { return chartFix; }
            set { SetProperty(ref chartFix, value); }
        }
       


    }
}