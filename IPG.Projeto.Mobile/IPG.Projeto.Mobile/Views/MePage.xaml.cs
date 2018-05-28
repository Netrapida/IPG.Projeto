using IPG.Projeto.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SkiaSharp;
using Microcharts;
using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Services;

namespace IPG.Projeto.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MePage : ContentPage
    {
        MePageViewModel viewModel;
        private readonly ApiServices _apiServices = new ApiServices();

        public MePage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MePageViewModel(); 

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Charts.Count == 0)
                viewModel.LoadStatsCommand.Execute(null);

            //chartView.Chart = viewModel.Charts[0];

            List<Chart> Charts = await _apiServices.ChartUserAsync();
            chartViewReported.Chart = Charts[0];
            chartViewFixed.Chart = Charts[1];
        }


    }
}