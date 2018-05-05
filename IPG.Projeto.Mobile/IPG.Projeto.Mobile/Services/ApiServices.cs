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

namespace IPG.Projeto.Mobile.Services
{
    internal class ApiServices
    {
        public async Task<bool> RegisterUserAsync(
            string email, string password, string confirmPassword)
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
                JObject oJsonObject = new JObject();
                oJsonObject.Add("userID", userName);
                oJsonObject.Add("password", password);
                oJsonObject.Add("grant_type", "password");

                var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "/api/login");

                request.Content = new StringContent(JsonConvert.SerializeObject(oJsonObject), Encoding.UTF8, "application/json");


                var client = new HttpClient();
                var response = await client.SendAsync(request);

                var content = await response.Content.ReadAsStringAsync();

                JObject jwtDynamic = JsonConvert.DeserializeObject<JObject>(content);

                var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiration");
                var accessToken = jwtDynamic.Value<string>("accessToken");

                Settings.AccessTokenExpirationDate = accessTokenExpiration;

            



                //    request.Content = new StringContent(
                //        JsonConvert.SerializeObject(oJsonObject), Encoding.UTF8, "application/json");

                //    var client = new HttpClient();
                //    var response = await client.PostAsync(Constants.BaseApiAddress + "/api/login", request).ConfigureAwait(false); ;
                //    if (response.IsSuccessStatusCode)
                //    {
                //        Debug.WriteLine("POST Ok");
                //        var content = await response.Content.ReadAsStringAsync();

                //        JObject jwtDynamic = JsonConvert.DeserializeObject<JObject>(content);

                //        var accessTokenExpiration = jwtDynamic.Value<DateTime>("expiration");
                //        accessToken = jwtDynamic.Value<string>("accessToken");
                //        var msg = jwtDynamic.Value<string>("message");
                //        Settings.AccessTokenExpirationDate = accessTokenExpiration;

                //            if (string.IsNullOrEmpty(accessToken))
                //            {
                //                UserDialogs.Instance.Alert("Login Failed...");
                //            }
                //    }


                //}
                //catch (Exception)
                //{

                //    throw;
                //}





                //if (string.IsNullOrEmpty(accessToken))
                //{ // falha TOKEN

                //}
                //else if (string.IsNullOrEmpty(Settings.Password))
                //{   // Primeiro Login na aplicação, depois da instalação

                //    Settings.Username = userName;
                //    Settings.Password = password;
                //    Settings.AccessToken = accessToken;
                //    App.Current.MainPage = new NavigationPage(new MainPage());
                //}


                return accessToken;
        }


        //public async Task<List<Idea>> GetIdeasAsync(string accessToken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
        //        "Bearer", accessToken);

        //    var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/ideas");

        //    var ideas = JsonConvert.DeserializeObject<List<Idea>>(json);

        //    return ideas;
        //}

        //public async Task PostIdeaAsync(Idea idea, string accessToken)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        //    var json = JsonConvert.SerializeObject(idea);
        //    HttpContent content = new StringContent(json);
        //    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PostAsync(Constants.BaseApiAddress + "api/Ideas", content);
        //}

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
    }
}