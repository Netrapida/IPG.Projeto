using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using IPG.Projeto.Mobile.Models;
using IPG.Projeto.Mobile.Views;
using IPG.Projeto.Mobile.ViewModels;
using Acr.UserDialogs;

namespace IPG.Projeto.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemsPage : ContentPage
	{
        ItemsViewModel viewModel;
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;
            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));
            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }
        async void ListItemComment(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }


        


        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
                 // fazer sempre refrwsh para poder ordenar ao adicionar 
                
            // encontrar alternativa
            //    viewModel.LoadItemsCommand.Execute(null); 

        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            ItemsListView.BeginRefresh();
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                ItemsListView.ItemsSource = viewModel.Items;/*.OrderByDescending(Item => Item.Date);*/
            else
                ItemsListView.ItemsSource = viewModel.Items.Where(i => i.Text.ToUpper().Normalize(NormalizationForm.FormKD).Contains(e.NewTextValue.ToUpper().Normalize(NormalizationForm.FormKD)));
            ItemsListView.EndRefresh();
        }

    }
}