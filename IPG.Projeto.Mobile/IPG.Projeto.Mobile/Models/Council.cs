using System;
using System.Collections.Generic;
using System.Text;

namespace IPG.Projeto.Mobile.Models
{


    public class Council
    {
        public int generation_high { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public string type_name { get; set; }
        public int generation_low { get; set; }
        public string country_name { get; set; }
        public string type { get; set; }
    }
    public class RootCouncil
    {
        public List<Council> CouncilInfo { get; set; }
    }


}
