using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPG.Projeto.API
{
    public class Classes
    {
        public class User
        {
            public string UserID { get; set; }
            public string Password { get; set; }
        }

        public static class Roles
        {
            public const string ROLE_API = "Acesso-API";
        }

        public class TokenConfigurations
        {
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public int Seconds { get; set; }
        }
    }
}
