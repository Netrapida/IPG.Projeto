using System;
using TK.CustomMap;
using Xamarin.Forms;
//using Xamarin.Forms.Maps;

namespace IPG.Projeto.Mobile.Models
{
    public class Pin : TKCustomMapPin
    {
        public string Item_id { get; set; } 
        public string Text { get; set; }
        public string Description { get; set; }
        public string Council_name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime ReportDate { get; set; }
        public int Status { get; set; }
        public String StatusColor { get; set; }

        public string Category { get; set; }
        public int State { get; set; }
        public string ApplicationUserID { get; set; }

        //V_6 --------------
        public string Detail { get; set; }
        public string Photo { get; set; }
        public DateTime? LastUpdate { get; set; }

        // Navigation
        public Council Council { get; set; } // trazer Council com o pino para incluir nome na ListView


        public Pin()// construtor default
        {
            ReportDate = DateTime.Now;
            State = 0;
            DefaultPinColor = Color.Red;
            ShowCallout = true;
        }
    }

    public class CustomPin : Pin
    {
        public string Color { get; set; }
        public string PinImage { get; set; }
    }


}