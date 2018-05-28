using Microcharts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPG.Projeto.Mobile.Models
{
    public class Stats
    {
        public string Title { get; set; }
        public string Value1 { get; set; }
        public string Label1 { get; set; }
        public string Value2 { get; set; }
        public string Label2 { get; set; }

    }

    public class Stat : Entry
    {
        public Stat(float value) : base(value)
        {
        }
    }

}

