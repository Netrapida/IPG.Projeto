using IPG.Projeto.Mobile.Models;
using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Entry = Microcharts.Entry;
using Xamarin.Forms;

namespace IPG.Projeto.Mobile.ViewModels
{
	public class MePageViewModel : BaseViewModel
	{
        public ObservableCollection<Stats> Statistics { get; set; }
        List<MyChart> MyCharts;

        public class MyChart
        {
            public Chart ChartData { get; set; }
        }

        public Chart Chart1 => new LineChart()
        {
            Entries = entries2
        };
        public Chart Chart2 => new LineChart()
        {
            Entries = entries
        };

        List<Entry> entries = new List<Entry>
        {
            new Entry(200)
            {
                Color = SKColor.Parse("00BFFF"),
                Label ="J",
            },
            new Entry(400)            {

                Color = SKColor.Parse("00BFFF"),
                Label = "F",

              },
            new Entry(000)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "M",
             },
             new Entry(200)
            {
                Color = SKColor.Parse("00BFFF"),
                Label ="Abr",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "Mai",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "Jun",
            },
             new Entry(200)
            {
                Color = SKColor.Parse("00BFFF"),
                Label ="Jul",
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "Ago",
            },
            new Entry(600)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "set",
            },
             new Entry(200)
            {
                Color = SKColor.Parse("00BFFF"),
                Label ="Out",
            },
            new Entry(400)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "Nov",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("00BFFF"),
                Label = "Dez",
            },
            };

        List<Entry> entries2 = new List<Entry>
        {
            new Entry(0)
            {
                Color = SKColor.Parse("#77d065"),
                Label ="J",
            },
            new Entry(0)            {

                Color = SKColor.Parse("#77d065"),
                Label = "F",

              },
            new Entry(000)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "M",
             },
             new Entry(0)
            {
                Color = SKColor.Parse("#77d065"),
                Label ="A",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "M",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "J",
            },
             new Entry(200)
            {
                Color = SKColor.Parse("#77d065"),
                Label ="J",
            },
            new Entry(0)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "A",
            },
            new Entry(600)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "S",
            },
             new Entry(200)
            {
                Color = SKColor.Parse("#77d065"),
                Label ="O",
            },
            new Entry(400)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "N",
            },
            new Entry(000)
            {
                Color = SKColor.Parse("#77d065"),
                Label = "D",
            },
            };





        public MePageViewModel ()
		{

            Statistics = new ObservableCollection<Stats>();

            Statistics.Add(new Stats { Title = "Relatórios enviados", Label1 = "August", Label2 = "July", Value1 = "4,2", Value2 = "24,4" });
            Statistics.Add(new Stats { Title = "Corrigidos", Label1 = "August", Label2 = "July", Value1 = "4:34", Value2 = "5:02" });
            //Statistics.Add(new Stats { Title = "Activities", Label1 = "August", Label2 = "July", Value1 = "1", Value2 = "6" });
            //Statistics.Add(new Stats { Title = "Calories Burned", Label1 = "August", Label2 = "July", Value1 = "341", Value2 = "1.954" });
            //Statistics.Add(new Stats { Title = "Elevation Climb (M)", Label1 = "August", Label2 = "July", Value1 = "29,3", Value2 = "221,1" });
            //Statistics.Add(new Stats { Title = "Time Spent", Label1 = "August", Label2 = "July", Value1 = "19:22", Value2 = "2:02:39" });




        }
      


    }
}