using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace IPG.Projeto.API.Models
{
    [DataContract] // não fazer serialize das proriedades privadas
    public class ApplicationUser : IdentityUser
    {
        // override para aceder ao Id
        [DataMember]
        public override string Id
        {
            get { return base.Id; }
        }

        [DataMember] // este será serialize
        public string DisplayName { get; set; }
        [DataMember]// este será serialize
        public string ProfilePicture { get; set; }


    }
}
