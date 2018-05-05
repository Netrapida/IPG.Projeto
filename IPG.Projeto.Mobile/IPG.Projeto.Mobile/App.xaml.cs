using System;
using Acr.UserDialogs;
using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.ViewModels;
using IPG.Projeto.Mobile.Views;
using Xamarin.Forms;

namespace IPG.Projeto.Mobile
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();
            SetMainPage();
        }


        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
                {   // Refresh de novo Token
                    var loginViewModel = new LoginViewModel();
                    loginViewModel.LoginCommand.Execute(null);
                }
                MainPage = new NavigationPage(new MainPage());
                   
            }
            else if (!string.IsNullOrEmpty(Settings.Username)
                  && !string.IsNullOrEmpty(Settings.Password))
            {   // Utilizador removido
                MainPage = new NavigationPage(new LoginPage());
            }

            else
            {   //Primeiro Login na app
                MainPage = new NavigationPage(new MainPage()); 
             
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
            // Handle when your app resumes
        }
	}
}
