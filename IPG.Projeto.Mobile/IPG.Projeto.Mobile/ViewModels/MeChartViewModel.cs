using IPG.Projeto.Mobile.Helper;
using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.Views;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Entry = Microcharts.Entry;


namespace IPG.Projeto.Mobile.ViewModels
{
    class MeChartViewModel : BaseViewModel
    {
        private readonly ApiServices _apiServices = new ApiServices();
        public ObservableCollection<Chart> Charts { get; set; }
 


        public Command LoadChartsCommand { get; set; }

        public MeChartViewModel()
        {
            Title = "Me stats";
            Charts = new ObservableCollection<Chart>();
            LoadChartsCommand = new Command(async () => await ExecuteLoadChartsCommand());

            MessagingCenter.Subscribe<NewItemPage, Pin>(this, "AddItem", async (obj, item) =>
            {
                await ExecuteLoadChartsCommand();
            });

        }

        async Task ExecuteLoadChartsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
             


            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }




        //public async Task<Chart> ChartReportedAsync()         
        //{
        //    var entries = await _apiServices.GetStastAsync(Settings.AccessToken);
        //    entries.Reverse();

        //    Chart Chart = new LineChart()
        //    {
        //        LabelTextSize = 30,
        //        Entries = entries

        //    };
        //    return Chart;
        //}


    }
}
