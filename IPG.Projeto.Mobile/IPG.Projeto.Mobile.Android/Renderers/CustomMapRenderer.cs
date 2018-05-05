using IPG.Projeto.Mobile.Droid.Renderers;
using Xamarin.Forms;
using Android.Content;
using IPG.Projeto.Mobile.ViewModels;
using IPG.Projeto.Mobile.Controls;
using IPG.Projeto.Mobile.Models;

[assembly: ExportRenderer(typeof(MobileMap), typeof(CustomMapRenderer))]
namespace IPG.Projeto.Mobile.Droid.Renderers
{
    public class CustomMapRenderer /*: MapRenderer*/
    {
        bool isDrawn;
        ItemsViewModel viewModel; // reveber contexto dos pins

        //public CustomMapRenderer(Context context) : base(context)
        //{
        //}

        //protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        //{
        //    base.OnElementChanged(e);

        //    if (e.NewElement != null)
        //    {
        //        var formsMap = (MobileMap)e.NewElement;
        //        Control.GetMapAsync(this);
        //    }
        //}
        //protected override MarkerOptions CreateMarker(Pin pin)
        //{
        //    // alterar cor do pin com custompin extended
        //    var cpin = pin as CustomPin;
        //    var hue = GetColor("3");


        //    var marker = new MarkerOptions { };
        //    marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
        //    marker.SetTitle(pin.Label);
        //    marker.SetSnippet(pin.Address);
        //    marker.SetIcon(BitmapDescriptorFactory.DefaultMarker(hue));


        //    return marker;
        //}
        //private float GetColor(string color)
        //{
        //    float trackColor = 0.0f;
        //    switch (color)
        //    {
        //        case "1":
        //            trackColor = BitmapDescriptorFactory.HueRed;
        //            break;
        //        case "2":
        //            trackColor = BitmapDescriptorFactory.HueGreen;
        //            break;
        //        case "3":
        //            trackColor = BitmapDescriptorFactory.HueBlue;
        //            break;
        //        case "4":
        //            trackColor = BitmapDescriptorFactory.HueViolet;
        //            break;
        //        case "5":
        //            trackColor = BitmapDescriptorFactory.HueYellow;
        //            break;
        //    }
        //    return trackColor;
        //}

        //protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);

        //    if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
        //    {
        //        NativeMap.SetPadding(0, 0, 0, 500);

        //        NativeMap.MyLocationEnabled = true;
        //        NativeMap.UiSettings.ZoomControlsEnabled = false;

        //        NativeMap.UiSettings.CompassEnabled = false;
        //        NativeMap.UiSettings.MyLocationButtonEnabled = true;
        //        NativeMap.BuildingsEnabled = false;
        //        NativeMap.TrafficEnabled = true;
        //        NativeMap.UiSettings.MapToolbarEnabled = false;
        //        isDrawn = true;
        //    }
        //}
    }
}