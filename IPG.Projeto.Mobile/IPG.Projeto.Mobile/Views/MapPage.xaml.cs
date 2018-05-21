using Acr.UserDialogs;
using IPG.Projeto.Mobile.Controls;
using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.ViewModels;
using Microcharts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TK.CustomMap;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static IPG.Projeto.Mobile.Controls.Utils;

namespace IPG.Projeto.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
 
 


    public partial class MapPage : ContentPage
    {
        PinsViewModel viewModel;
        ObservableCollection<TKCustomMapPin> _pins;
        TKCustomMap map = new TKCustomMap();

        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PinsViewModel();

            this.CreateView();
        }

        private void CreateView()
        {
         

            map.IsClusteringEnabled = false;
            map.IsRegionChangeAnimated = false;
            map.IsShowingUser = true;
            map.Pins = viewModel.Items;
               try
            {
                map.SetBinding(TKCustomMap.MapRegionProperty, "MapRegion");
                viewModel.LoadMeCommand.Execute(null); // atualiza mapregion com Me atual
                Mapa.Children.Add(map);

            }
            catch (Exception)
            {
                throw;
            }

        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);

        }
     

        private ObservableCollection<TKCustomMapPin> AddPinos(List<Pin> items)
        {
            try
            {
                foreach (var pin in items)
                {
                    var newPin = new TKCustomMapPin
                    {
                        Position = new Position(pin.Latitude, pin.Longitude),
                        IsVisible = true,
                        Title = pin.Text,
                        Subtitle = pin.Description,
                        ShowCallout = true,
                    };

                    newPin.DefaultPinColor = Color.Blue; // Voodoo stuff para mudar customPin color
                    _pins.Add(newPin);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return _pins;
        }


    }
}






/// test stuffs
///                 ////Council council = JsonConvert.DeserializeObject<Council>(json);
//var obj = JObject.Parse(json);
////IEnumerable<JToken> pricyProducts = obj.SelectTokens("$[?]");
////var firstName = obj.Properties().Select(p => p.Name).ToList();
//var rootElement = obj.Properties().Select(p => p.Name).FirstOrDefault();
//var council = JObject.Parse(json).SelectToken(rootElement).ToString();
//var temp = JsonConvert.DeserializeObject<Council>(council);


/*
 ar users = JObject.Parse(s).SelectToken("response").ToString();

var vkUsers = JsonConvert.DeserializeObject<List<VkUser>>(users);

 */



//using (UserDialogs.Instance.Loading("Mapa..."))
//{

//    Plugin.Geolocator.Abstractions.Position position = await GetCurrentPosition();
//    if (position != null)
//    {
//        me = new Position(position.Latitude, position.Longitude);
//        map.MapRegion = MapSpan.FromCenterAndRadius(me, Distance.FromKilometers(2));
//    }
//    Mapa.Children.Add(map);

//}
//map.MapRegion = MapSpan.FromCenterAndRadius(me, Distance.FromKilometers(2));