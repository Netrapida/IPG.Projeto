using Acr.UserDialogs;
using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace IPG.Projeto.Mobile.ViewModels
{
    public class LoginViewModel
    {

        private readonly ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        using (UserDialogs.Instance.Loading("Autenticar..."))
                        {
                            var accesstoken = await _apiServices.LoginAsync(Username, Password);
                            Settings.AccessToken = accesstoken;

                            if (string.IsNullOrEmpty(accesstoken))
                            {
                                UserDialogs.Instance.Alert("Login Failed...");
                            }

                            //else 
                            //{ // primeiro Login
                            //    if (!string.IsNullOrEmpty(Settings.Username))
                            //    { Settings.Username = Username;
                            //        Settings.Password = Password;
                            //        // problema com Separation of Con. MVVM!!!!
                            //        App.Current.MainPage = new NavigationPage(new MainPage());
                            //    }

                            //}

                        }
                    }
                    catch (Exception)
                    {
                        // Problemas de ligação
                        // server offline
                        // sem internet
                        UserDialogs.Instance.Alert("Problema ligação...");
                    }
                });
            }
        }
        public LoginViewModel()
        {
            Username = Settings.Username;
            Password = Settings.Password;
        }


    }
}