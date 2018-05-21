using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPG.Projeto.Mobile.Controls;
using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Models;
using TK.CustomMap;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(IPG.Projeto.Mobile.Services.MockDataStore))]
namespace IPG.Projeto.Mobile.Services
{
    public class MockDataStore : IDataStore<Pin>
    {
        List<Pin> pins;
        ApiServices _apiServices = new ApiServices();
        String[] arrayColorString = new String[] { "#f44336", "#03A9F4", "#4CAF50" }; // cores dos states
        Color[] arrayColorColor = new Color[] { Color.Red, Color.Blue, Color.Green};
        public MockDataStore()
        {
            pins = new List<Pin>();// pinos observable ...
        }

      
        public async Task<bool> AddItemAsync(Pin pin)
        {
            pins.Add(pin);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Pin pin)
        {
            var _item = pins.Where((Pin arg) => arg.Id == pin.Id).FirstOrDefault();
            pins.Remove(_item);
            pins.Add(pin);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Pin pin)
        {
            var _item = pins.Where((Pin arg) => arg.Id == pin.Id).FirstOrDefault();
            pins.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Pin> GetItemAsync(string id)
        {
            return await Task.FromResult(pins.FirstOrDefault(s => s.Item_id == id));
        }

        public async Task<IEnumerable<Pin>> GetItemsAsync(bool forceRefresh = true)
        {
            return await Task.FromResult(pins.OrderByDescending(Item => Item.ReportDate)); // order por data nesta versão);
        }

        public async Task<IEnumerable<Pin>> GetItemsAPIAsync(bool forceRefresh = false)   // Pinos da API
        {
            pins.Clear(); // remove todos antes do refesh (talvez melhorar e importar só os que faltam)
            var me = await Utils.GetCurrentPosition();
            var info = await Utils.RefreshDataAsync(me);
            var json = await _apiServices.GetPinsAsync(info.CouncilInfo[0].Id.ToString(), Settings.AccessToken);
            foreach (var pin in json)
            {
                pin.StatusColor = arrayColorString[pin.State];
                pin.Position = new Position(pin.Latitude, pin.Longitude);
                //Melhorar --
                pin.Title = pin.Text;
                pin.DefaultPinColor = arrayColorColor[pin.State]; ;
                pins.Add(pin);
            }
            return pins;
        }


        //public Position randomPosition()
        //{
        //    Random rng = new Random();

        //        double lat = rng.NextDouble() * (40.7699 - 40.7500) + 40.7500;
        //        double lon = rng.NextDouble() * (7.353372 - 7.300000) + 7.30000;

        //    //Latitude=40.7699,Longitude=-7.353372,

        //    return _position = new Position(lat, lon);


        //}





    }
}