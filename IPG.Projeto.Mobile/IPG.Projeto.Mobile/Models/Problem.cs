using System;
using System.Collections.Generic;
using System.Text;

namespace IPG.Projeto.Mobile.Models
{
    public class Problem
    {
        //Onde se Encontra
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int CouncilID { get; set; }
        public int State { get; set; }

        //Detalhes do problema
        public string Text { get; set; }
        public string Detail { get; set; }
        public string Photo { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime LastUpdate { get; set; }

        //Quem envia
        public bool Anonymous { get; set; }
        //public string ApplicationUserID { get; set; }
        public ApplicationUser User { get; set; }


        public Problem()// construtor default
        {
            ReportDate = DateTime.Now;
            LastUpdate = DateTime.Now;
            Anonymous = false;
        }


    }
}


