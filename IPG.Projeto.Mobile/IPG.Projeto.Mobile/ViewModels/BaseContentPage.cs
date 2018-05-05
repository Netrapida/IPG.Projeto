using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IPG.Projeto.Mobile.ViewModels
{
    public class BaseContentPage : ContentPage
    {
        public void SendAppearing()
        {
            OnAppearing();
        }

        public void SendDisappearing()
        {
            OnDisappearing();
        }
    }
}
