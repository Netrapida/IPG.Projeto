using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using IPG.Projeto.Mobile.ViewModels;
using IPG.Projeto.Mobile.Views;
using Xamarin.Forms;
using Acr.UserDialogs;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace IPG.Projeto.Mobile.Services
{
    internal class ApiServices
    {
        // Login | Registo -------------  REVER 
        public async Task<bool> RegisterUserAsync(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel
            {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(
                Constants.BaseApiAddress + "api/Account/Register", httpContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            JObject oJsonObject = new JObject
            {
                { "userID", userName },
                { "password", password },
                { "grant_type", "password" }
            };

            var request = new HttpRequestMessage(
            HttpMethod.Post, Constants.BaseApiAddress + "/api/login")
            {
                Content = new StringContent(JsonConvert.SerializeObject(oJsonObject), Encoding.UTF8, "application/json")
            };


            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<JObject>(content);
            var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiration");
            var accessToken = jwtDynamic.Value<string>("accessToken");

            Settings.AccessTokenExpirationDate = accessTokenExpiration;

            if (!string.IsNullOrEmpty(accessToken))
            {
                // primeiro Login Na applicação .. autenticaçºao válida
                if (string.IsNullOrEmpty(Settings.Username))
                {
                    Settings.Username = userName;
                    Settings.Password = password;
                    App.Current.MainPage = new NavigationPage(new MainPage());
                }// Só deve ser executado uma única vez.. ou quando for desinstalada a app

            }
            else
            {
                UserDialogs.Instance.Alert("Login Failed...");
            }

            return accessToken;
        }


        //public async Task PutIdeaAsync(Idea idea, string accessToken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    var json = JsonConvert.SerializeObject(idea);
        //    HttpContent content = new StringContent(json);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PutAsync(
        //        Constants.BaseApiAddress + "api/Ideas/" + idea.Id, content);
        //}

        //public async Task DeleteIdeaAsync(int ideaId, string accessToken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    var response = await client.DeleteAsync(
        //        Constants.BaseApiAddress + "api/Ideas/" + ideaId);
        //}

        //public async Task<List<Idea>> SearchIdeasAsync(string keyword, string accessToken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //        "Bearer", accessToken);

        //    var json = await client.GetStringAsync(
        //        Constants.BaseApiAddress + "api/ideas/Search/" + keyword);

        //    var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

        //    return ideas;
        //}

        //Postar um problems

        public async Task PostProblemAsync(Problem problem, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(problem);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Constants.BaseApiAddress + "/api/Problems", content);
        }
       

        //  stats -----------------------------------------------------------------------------------
        // receber stasts individuais .. quantos envios e quantos fix (ultimos 6 meses, send+fix)
        public async Task<List<List<Entry>>> GetStatsUserAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "/api/Problems/stats/user/" + Settings.Username);
            var entries = JsonConvert.DeserializeObject<List<List<Entry>>>(json);

            foreach (var entrie in entries[0]) { entrie.Color = SKColor.Parse("f44336"); } // color para a template  ENVIADOS                
            foreach (var entrie in entries[1]) { entrie.Color = SKColor.Parse("90D585"); }// color para a template FIXED
            entries[0].Reverse();
            entries[1].Reverse();
            return entries;  
        }


        // receber stasts council .. quantos envios e quantos fix
        public async Task<List<List<Entry>>> GetStatsCouncilAsync(int council, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "/api/Problems/stats/council/" + council);
            var entries = JsonConvert.DeserializeObject<List<List<Entry>>>(json);

            foreach (var entrie in entries[0]) { entrie.Color = SKColor.Parse("f44336"); } // color para a template  ENVIADOS                
            foreach (var entrie in entries[1]) { entrie.Color = SKColor.Parse("90D585"); }// color para a template FIXED
            entries[0].Reverse();
            entries[1].Reverse();
            return entries;
        }


        public async Task<List<Chart>> ChartUserAsync()
        {
            List<Chart> Charts = new List<Chart>();

            var entries = await GetStatsUserAsync(Settings.AccessToken);

            Chart ChartReported = new LineChart()
            {
                LabelTextSize = 30,
                Entries = entries[0],
                MinValue = 0

            };
            Chart ChartFixed = new LineChart()
            {
                LabelTextSize = 30,
                Entries = entries[1],
                MinValue = 0

            };


            var max = ChartReported.MaxValue;
            if (ChartFixed.MaxValue > max) max = ChartFixed.MaxValue;
            ChartFixed.MaxValue = max;
            ChartReported.MaxValue = max;

            Charts.Add(ChartReported);
            Charts.Add(ChartFixed);

            return Charts;
        }


        public async Task<List<Chart>> ChartCouncilAsync(int council)  
        {
            List<Chart> Charts = new List<Chart>();

            var entries = await GetStatsCouncilAsync(council, Settings.AccessToken);

            Chart ChartReported = new LineChart()
            {
                LabelTextSize = 30,
                Entries = entries[0],
                MinValue = 0

            };
            Chart ChartFixed = new LineChart()
            {
                LabelTextSize = 30,
                Entries = entries[1],
                MinValue = 0

            };


            var max = ChartReported.MaxValue;
            if (ChartFixed.MaxValue > max) max = ChartFixed.MaxValue;
            ChartFixed.MaxValue = max;
            ChartReported.MaxValue = max;

            Charts.Add(ChartReported);
            Charts.Add(ChartFixed);

            return Charts;
        }


    }


}