using System;

using IPG.Projeto.Mobile.Models;

namespace IPG.Projeto.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Pin Item { get; set; }
        public ItemDetailViewModel(Pin item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
