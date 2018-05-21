using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models
{
    public class Council
    {
        public int Id { get; set; }
        public int CouncilID { get; set; }  
        public string Name { get; set; }


    }
    public class RootCouncil
    {
        public List<Council> CouncilInfo { get; set; }
    }
}
