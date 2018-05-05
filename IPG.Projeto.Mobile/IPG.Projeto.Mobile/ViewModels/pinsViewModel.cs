using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using IPG.Projeto.Mobile.Controls;
using TK.CustomMap;
using Xamarin.Forms;
using Acr.UserDialogs;
using Entry = Microcharts.Entry;
using SkiaSharp;
using Microcharts;

namespace IPG.Projeto.Mobile.ViewModels
{

    public class PinsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadMeCommand { get; set; }
        public ObservableCollection<Stats> Statistics { get; set; } // testes


    public PinsViewModel()
        {
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", (obj, item) =>
            {
                var _item = item as Item;
                Items.Add(_item); // refresh pinos sem adicionar novo na lista evitar duplicação


            });
           
            LoadMeCommand = new Command(async () => await ExecuteLoadMeCommand()); //me 

            // teste teste teste 
            Statistics = new ObservableCollection<Stats>();

            Statistics.Add(new Stats { Title = "Relatórios", Label1 = "Enviados", Label2 = "Corrigidos", Value1 = "5", Value2 = "1" });
            Statistics.Add(new Stats { Title = "Corrigidos", Label1 = "August", Label2 = "July", Value1 = "4:34", Value2 = "5:02" });
            Statistics.Add(new Stats { Title = "Activities", Label1 = "August", Label2 = "July", Value1 = "1", Value2 = "6" });
            //Statistics.Add(new Stats { Title = "Calories Burned", Label1 = "August", Label2 = "July", Value1 = "341", Value2 = "1.954" });
            //Statistics.Add(new Stats { Title = "Elevation Climb (M)", Label1 = "August", Label2 = "July", Value1 = "29,3", Value2 = "221,1" });
            //Statistics.Add(new Stats { Title = "Time Spent", Label1 = "August", Label2 = "July", Value1 = "19:22", Value2 = "2:02:39" });

    }

        public Chart Chart1 => new LineChart()
        {
            Entries = entries
        };

        public Chart Chart2 => new LineChart()
        {
            Entries = entries2
        };

        List<Entry> entries = new List<Entry>
        {
            new Entry(000)
            {
                Color = SKColor.Parse("#90D585"),


            },
            new Entry(000)
            {

                Color = SKColor.Parse("#90D585"),


              },
            new Entry(000)
            {
                Color = SKColor.Parse("#90D585"),
   
             },
             new Entry(200)
            {
                Color = SKColor.Parse("#90D585"),
  
            },
            new Entry(000)
            {
                Color = SKColor.Parse("#90D585"),

            },
            new Entry(000)
            {
                Color = SKColor.Parse("#90D585"),

            },
            };


        List<Entry> entries2 = new List<Entry>
        {
                   new Entry(000)
            {
                Color = SKColor.Parse("#FF1943"),

            },
            new Entry(000)            {

                Color = SKColor.Parse("#FF1943"),


              },
            new Entry(000)
            {
                Color = SKColor.Parse("#FF1943"),

             },
             new Entry(200)
            {
                Color = SKColor.Parse("#FF1943"),

            },
            new Entry(000)
            {
                Color = SKColor.Parse("#FF1943"),

            },
            new Entry(000)
            {
                Color = SKColor.Parse("#FF1943"),

            },
          };


        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
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
        async Task ExecuteLoadMeCommand()   
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                using (UserDialogs.Instance.Loading("A minha posição..."))
                {
                    var me = await Utils.GetCurrentPosition();
                    MapRegion = MapSpan.FromCenterAndRadius(new Position(me.Latitude, me.Longitude), Distance.FromKilometers(2));

                    UserDialogs.Instance.Loading("Informação adicional...");
                    var info = await Utils.RefreshDataAsync(me);
                    MyPositionCouncil_1 = info.CouncilInfo[1].name;
                    MyPositionCouncil_0 = info.CouncilInfo[0].name;

                }
   
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
        // layout binding------------------------------------------------------------

        // maps council cenas !!!!!!!!!!!!!!!!!!!!!!! REVER COLOCAR numa  LIST<string>
        string myPositionCouncil_0 = string.Empty;
       
        public string MyPositionCouncil_0
        {
            get { return myPositionCouncil_0; }
            set { SetProperty(ref myPositionCouncil_0, value); }
        }
        string myPositionCouncil_1 = string.Empty;
        public string MyPositionCouncil_1
        {
            get { return myPositionCouncil_1; }
            set { SetProperty(ref myPositionCouncil_1, value); }
        }
        //-----------------------------  REVER ------------------------------------------


    }
}