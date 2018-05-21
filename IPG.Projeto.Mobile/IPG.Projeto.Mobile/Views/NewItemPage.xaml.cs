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
using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.Controls;
using IPG.Projeto.Mobile.Helper;

namespace IPG.Projeto.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewItemPage : ContentPage
    {
        public Pin Pin { get; set; }
        public Problem  Problem { get; set; }
        public Council Council { get; set; }


        ApiServices _apiServices = new ApiServices();

        public NewItemPage()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {

            using (UserDialogs.Instance.Loading("Posição..."))
            {
                var me = await Utils.GetCurrentPosition();
                if (string.IsNullOrEmpty(me.ToString()))
                {
                    UserDialogs.Instance.Alert("Fail...");
                }
                var info = await Utils.RefreshDataAsync(me);

                Council = new Council
                {
                    Name = info.CouncilInfo[0].Name
                };

                Pin = new Pin // para colocar logon o Pin
                {
                    Position = new TK.CustomMap.Position(me.Latitude, me.Longitude),
                    Council_name = Council.Name,
                    StatusColor = "#f44336",
                };

                Problem = new Problem  // default Problem construção da view (ALTERAR para  VIEWMODEL)!
                {
                    CouncilID = info.CouncilInfo[0].Id,
                    Latitude = me.Latitude,
                    Longitude = me.Longitude,
                    Text = "Colocar Título de Problema",
                    Detail = "Uma Descrição Detalhada do Problema",
                    ReportDate = DateTime.Now,

                };
                BindingContext = this;
            }

        }

     
        async void Save_Clicked(object sender, EventArgs e)
        {
            // updates do Pino e Problem com informações introduzidas pelo user
            Pin.Title = Problem.Text;
            Pin.Text = Problem.Text;
            Pin.Detail = Problem.Detail; // Rever !!! utilizar+ foto

            Problem.Text = Problem.Text;
            Problem.Detail = Problem.Detail;


            MessagingCenter.Send(this, "AddItem", Pin);

            using (UserDialogs.Instance.Loading("Enviar problema..."))
            {
                await _apiServices.PostProblemAsync(Problem, Settings.AccessToken);
                Task.Delay(3000).Wait(); // simular tempo de envio da foto
            }
            
            UserDialogs.Instance.Alert("O seu problema foi enviado com sucesso, será encaminhado para :"+ Council.Name);
            await Navigation.PopModalAsync();
        }


    }
}