using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.ViewModels;
using Microcharts;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static IPG.Projeto.Mobile.Controls.Utils;

namespace IPG.Projeto.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        PinsViewModel viewModel;
        private readonly ApiServices _apiServices = new ApiServices();

        public MapPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new PinsViewModel();
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }




        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0) {
                viewModel.LoadItemsCommand.Execute(null);
                viewModel.LoadMeCommand.Execute(null);
            }
           

        }






    }
}



