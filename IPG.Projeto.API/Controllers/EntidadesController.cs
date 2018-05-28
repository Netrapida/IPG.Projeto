using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IPG.Projeto.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace IPG.Projeto.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Entidades")]
    public class EntidadesController : Controller
    {
       
        // GET: api/Entidades/-7.356032,40.779701
        [HttpGet("{Long},{Lat}")]
        public async Task<RootCouncil> GetAsync(string Long, string Lat)
        {
            var result = new RootCouncil
            {
                CouncilInfo = new List<Council>()
            };
            var uri = new Uri("http://mapas.distrito.pt/point/4326/" + Long + "," + Lat + "?type=O07,O08");
            HttpClient myClient = new HttpClient();

            var response = await myClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var jo = JObject.Parse(json);
                if (jo.HasValues)
                {
                    var jp = jo.Properties().First();
                    foreach (var prop in jo.Properties())
                    {
                        var p = prop.Value.ToObject<Council>();
                        result.CouncilInfo.Add(p);
                    }

                    return result;
                }
            }
            return result;
        }

      
    
    }
}
