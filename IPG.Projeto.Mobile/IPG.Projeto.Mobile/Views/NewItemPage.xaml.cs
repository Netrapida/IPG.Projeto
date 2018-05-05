using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPG.Projeto.Mobile.Models;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using Plugin.Geolocator;
using System.Diagnostics;
using Acr.UserDialogs;

namespace IPG.Projeto.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        Position position;

        public NewItemPage()
        {
            InitializeComponent();

        }


        protected async override void OnAppearing()
        {

            using (UserDialogs.Instance.Loading("Posição..."))
            {
                position = await GetCurrentPosition();
                if (string.IsNullOrEmpty(position.ToString()))
                {
                    UserDialogs.Instance.Alert("Fail...");
                }

                Item = new Item
                {
                    Text = "Item name",
                    Description = "This is an item description.",
                    Position = new TK.CustomMap.Position(position.Latitude, position.Longitude),
                    ShowCallout = true,
                    Date = DateTime.Now,
                    // DefaultPinColor = Color.Green,
                };

                BindingContext = this;
            }
           

        }



        public static async Task<Position> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 3;

                // CACHE POS...
                //position = await locator.GetLastKnownLocationAsync();
                //if (position != null)
                //{
                //    //got a cahched position, so let's use it.
                //    return position;
                //}

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null, true);

            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;

            
            return position;
        }



        async void Save_Clicked(object sender, EventArgs e)
        {
            Item.Title = Item.Text;
            Item.Subtitle = Item.Description;
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }
    }
}