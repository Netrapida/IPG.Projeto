using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models
{
    [DataContract] // não fazer serialize das proriedades privadas
    public class Council
    {
        [DataMember] // este será serialize
        public int Id { get; set; }
        [DataMember] // este será serialize
        public string Name { get; set; }
        [DataMember] // este será serialize
        public int Reported { get; set; }
        [DataMember] // este será serialize
        public int ReportedFix { get; set; }
        public bool Deleted { get; set; }

    }
    public class RootCouncil
    {
        public List<Council> CouncilInfo { get; set; }
    }
}
