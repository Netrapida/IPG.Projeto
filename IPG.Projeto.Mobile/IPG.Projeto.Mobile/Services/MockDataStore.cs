using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IPG.Projeto.Mobile.Controls;
using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Models;
using Newtonsoft.Json;
using TK.CustomMap;



[assembly: Xamarin.Forms.Dependency(typeof(IPG.Projeto.Mobile.Services.MockDataStore))]
namespace IPG.Projeto.Mobile.Services
{
    public class MockDataStore : IDataStore<Pin>
    {
        List<Pin> pins;

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
            return await Task.FromResult(pins.OrderByDescending(Item => Item.LastUpdate)); // order por LastUpdate nesta versão);
        }


        public async Task<List<Pin>> GetPinsAsync(string council, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "/api/Problems/Council/" + council);
            var pins = JsonConvert.DeserializeObject<List<Pin>>(json);

            return pins;
        }


        public async Task<IEnumerable<Pin>> GetPinsAPIAsync(bool forceRefresh = false)   // Pinos da API
        {
            pins = new List<Pin>();// pinos observable ...
            var me = await Utils.GetCurrentPosition();
            var info = await Utils.RefreshDataAsync(me);
            var json = await GetPinsAsync(info.CouncilInfo[0].Id.ToString(), Settings.AccessToken);
            foreach (var pin in json)
            {
                pin.StatusColor = Constants.arrayColorString[pin.State];
                pin.Position = new Position(pin.Latitude, pin.Longitude);
                //Melhorar --
                pin.Title = pin.Text;
                pin.Subtitle = pin.LastUpdate.ToString();
                pin.DefaultPinColor = Constants.arrayColorColor[pin.State];
                pins.Add(pin);
            }

            return pins;
        }





    }
}