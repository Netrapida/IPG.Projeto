using IPG.Projeto.Mobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IPG.Projeto.Mobile.Controls
{
    public static class Utils
    {
        public static async Task<bool> CheckPermissions(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (Device.RuntimePlatform == Device.iOS)
                {

                    var title = $"{permission} Permission";
                    var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (newStatus.ContainsKey(permission) && newStatus[permission] != PermissionStatus.Granted)
                {
                    var title = $"{permission} Permission";
                    var question = $"To use the plugin the {permission} permission is required.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }
                    return false;
                }
            }

            return true;
        }

        // cenas GPS
        public static async Task<Plugin.Geolocator.Abstractions.Position> GetCurrentPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 3;
                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                try
                {
                    position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5), null, true);
                }
                catch (Exception)
                {

                    //CACHE POS...
                    //position = await locator.GetLastKnownLocationAsync();
                    //if (position != null)
                    //{
                    //    //got a cahched position, so let's use it.
                    //    return position;
                    //}
                }

            }
            catch (Exception ex)
            {
                //Debug.WriteLine("Unable to get location: " + ex);
            }

            if (position == null)
                return null;


            return position;
        }

        // receber mais info sobre local ond estou
        public static async Task<RootCouncil> RefreshDataAsync(Position me)// receber mais info sobre local ond estou
        {
            var result = new RootCouncil
            {
                CouncilInfo = new List<Council>()
            };
            var _latitude = string.Format("{0:f6}", me.Latitude).Replace(",", ".");
            var _longitude = string.Format("{0:f6}", me.Longitude).Replace(",", ".");

            var uri = new Uri(Constants.BaseApiAddress + "/api/Entidades/" + _longitude + "," + _latitude);
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                // lista de Councils com informação relevante
                var json = await response.Content.ReadAsStringAsync();
                var Councils = JsonConvert.DeserializeObject<RootCouncil>(json);
                return Councils;
            }
            return result;
        }


        //// stuff com Json .. problema no mapas.distrito. a posição lat long recebe json manhoso
        //public static class JsonUtil
        //{
        //    public static T Deserialize<T>(string json, bool ignoreRoot) where T : class
        //    {
        //        return ignoreRoot
        //            ? JObject.Parse(json)?.Properties()?.First()?.Value?.ToObject<T>()
        //            : JObject.Parse(json)?.ToObject<T>();
        //    }
        //}


    }
}