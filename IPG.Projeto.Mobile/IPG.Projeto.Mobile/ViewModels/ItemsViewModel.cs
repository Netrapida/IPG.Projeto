using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Views;


namespace IPG.Projeto.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Pin> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public bool Search { get; set; } = false;
        //PinsViewModel viewModel; // para o refresh (observable não funciona de jeito)


        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Pin>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Pin>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Pin;
                Items.Insert(0,_item); // inserir na primeira posição do listview (.add vai para ultima)
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                Items.Clear();
                if (Search.Equals(true))
                {
                    var items = await DataStore.GetItemsAsync(true);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                    Search = false;
                }
                else // receber de JSON --- MELHORAR!!!!
                {
                    var items = await DataStore.GetPinsAPIAsync(true);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
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


    }
}