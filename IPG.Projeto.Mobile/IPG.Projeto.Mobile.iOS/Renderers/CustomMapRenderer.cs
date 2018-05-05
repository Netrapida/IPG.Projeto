
namespace IPG.Projeto.Mobile.iOS.Renderers
{
    class CustomMapRenderer /*: MapRenderer*/
    {
        //MKMapView nativeMap;
        //MobileMap map;
        //public bool IsRegionChange = true;

        public CustomMapRenderer() // construtor vazio .. iphone trick
        {

        }

        //protected override async void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //    if (nativeMap == null)
        //    {
        //        nativeMap = Control as MKMapView;
        //        nativeMap.ZoomEnabled = true;
        //        nativeMap.ScrollEnabled = true;
        //    }


        //        map = (MobileMap)sender;
        //        map.Pins.Clear();
        //        if (map.Pins != null)
        //        {
        //            foreach (var customPin in map.Pins)
        //            {
        //                map.Pins.Add(customPin);
        //            }
        //        }
        //        InvokePlotPins();


        //}
        //private void InvokePlotPins()
        //{
        //    if (nativeMap == null)
        //        return;

        //    nativeMap.GetViewForAnnotation = GetViewForAnnotation;
        //}

        //private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        //{
        //    MKAnnotationView annotationView = null;
        //    if (annotation is MKUserLocation)
        //        return null;
        //    var mkAnnotation = annotation as MKPointAnnotation;
        //    if (mkAnnotation == null)
        //        return null;
        //    var customPin = GetCustomPin(mkAnnotation);
        //    if (customPin == null)
        //    {
        //        return null;
        //    }
      
            
        //    return annotationView;
        //}


        //private Pin GetCustomPin(MKPointAnnotation mkAnnotation)
        //{
        //    var position = new Position(mkAnnotation.Coordinate.Latitude, mkAnnotation.Coordinate.Longitude);
        //    foreach (var pin in map.Pins)
        //    {
        //        if (pin.Position == position)
        //        {
        //            if (!string.IsNullOrEmpty(pin.Label))
        //            {
        //                if (pin.Label == mkAnnotation.Title)
        //                    return pin;
        //            }
        //        }
        //    }
        //    return null;
        //}


        //private UIColor GetColor(string color)
        //{
        //    UIColor trackColor = UIColor.Orange;
        //    switch (color)
        //    {
        //        case "1":
        //            trackColor = UIColor.FromRGB(77, 123, 224);
        //            break;
        //        case "2":
        //            trackColor = UIColor.FromRGB(50, 193, 214);
        //            break;
        //        case "3":
        //            trackColor = UIColor.FromRGB(163, 178, 78);
        //            break;

        //        case "4":
        //            trackColor = UIColor.FromRGB(187, 93, 153);
        //            break;
        //        case "5":
        //            trackColor = UIColor.FromRGB(175, 98, 46);
        //            break;
        //    }
        //    return trackColor;
        //}


    }

}

