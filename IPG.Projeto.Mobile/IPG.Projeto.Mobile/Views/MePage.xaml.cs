using IPG.Projeto.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPG.Projeto.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MePage : ContentPage
    {
        MePageViewModel viewModel;
        public MePage ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new MePageViewModel();
        }
	}
}