using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using TK.CustomMap.Droid;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;


namespace IPG.Projeto.Mobile.Droid
{
    [Activity(Label = "IPG.Projeto.Mobile",
                 Icon = "@drawable/icon",
                 Theme = "@style/MainTheme",
                 MainLauncher = false,
                 ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

           

            UserDialogs.Init(this);

            CachedImageRenderer.Init(true);
            CarouselViewRenderer.Init();
            TKGoogleMaps.Init(this, bundle);

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            LoadApplication(new App());
        }
    }
}

