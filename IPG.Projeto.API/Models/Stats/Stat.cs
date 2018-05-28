using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models.Stats
{
    public class Stat
    {
        public string Label { get; set; }   
        public int Value { get; set; }  
        public int Fix { get; set; }
        public string ValueLabel { get; set; }  
    }
}
