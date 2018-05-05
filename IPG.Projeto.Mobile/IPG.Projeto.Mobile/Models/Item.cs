using System;
using TK.CustomMap;
//using Xamarin.Forms.Maps;

namespace IPG.Projeto.Mobile.Models
{
    public class Item : TKCustomMapPin
    {
        public string Item_id { get; set; }
        public string User_Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public Council Council  { get; set; }
        public string Council_name { get; set; }    
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string Category { get; set; }




    }
    public class CustomPin : Item
    {
        public string Color { get; set; }
        public string PinImage { get; set; }
    }
}