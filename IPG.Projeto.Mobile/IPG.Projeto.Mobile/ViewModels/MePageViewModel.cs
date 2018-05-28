using IPG.Projeto.Mobile.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entry = Microcharts.Entry;
using Xamarin.Forms;
using IPG.Projeto.Mobile.Services;
using IPG.Projeto.Mobile.Helper;
using System.Threading.Tasks;
using IPG.Projeto.Mobile.Views;
using System.Diagnostics;

namespace IPG.Projeto.Mobile.ViewModels
{
	public class MePageViewModel : BaseViewModel
	{
        public Command LoadStatsCommand { get; set; }    
        public ObservableCollection<Stats> Statistics { get; set; }
        public MePageViewModel ()
		{
            Title = "Me";
            LoadStatsCommand = new Command(async () => await ExecuteLoadStatsCommand());   
            MessagingCenter.Subscribe<NewItemPage, Pin>(this, "AddItem", async (obj, item) =>
            {


            });

            Charts = new ObservableCollection<Chart>();
            Statistics = new ObservableCollection<Stats>
            {
                new Stats { Title = "Relatórios enviados", Label1 = "August", Label2 = "July", Value1 = "4,2", Value2 = "24,4" },
                new Stats { Title = "Corrigidos", Label1 = "August", Label2 = "July", Value1 = "4:34", Value2 = "5:02" }
            };


        }


        async Task ExecuteLoadStatsCommand()    
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //Charts.Clear();
                //var charts = await _apiServices.ChartUserAsync();
                //foreach (var chart in charts)
                //{
                //    Charts.Add(chart); // update dos chart REVER não funciona
                //}

                // quando adicionar um pino .. atualizar as metricas aqui

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
        //    var entries = await _apiServices.GetStastReportedAsync(Settings.AccessToken);
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




