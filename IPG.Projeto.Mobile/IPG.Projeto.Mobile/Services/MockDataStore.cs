using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IPG.Projeto.Mobile.Models;
using TK.CustomMap;

[assembly: Xamarin.Forms.Dependency(typeof(IPG.Projeto.Mobile.Services.MockDataStore))]
namespace IPG.Projeto.Mobile.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        Position _position = new Position(40.7142700, -7.0059700);

        public MockDataStore()
        {
            items = new List<Item>();

            var mockItems = new List<Item>
            {


                //new Item {Position= randomPosition(),  Item_id = Guid.NewGuid().ToString(), ShowCallout = true, Title = "First item", Text = "First item", Description="This is an item description.", Latitude=40.7699,Longitude=-7.353372,User_Id="Cenas" },
                //new Item {Position= randomPosition(), Item_id = Guid.NewGuid().ToString(),  ShowCallout = true, Title = "First item",Text = "Second item", Description="This is an item description.", Latitude=40.763019,Longitude=-7.361672,User_Id="Cenas" },
                //new Item {Position= randomPosition(), Item_id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Third item", Description="This is an item description." , Latitude=40.766019,Longitude=-7.361672,User_Id="Cenas" },
                //new Item {Position= randomPosition(), Item_id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Fourth item", Description="This is an item description.", Latitude=40.7319,Longitude=-7.341672,User_Id="Cenas",Council_name="União das Freguesia de Vila Nova de Paiva, Alhais e Fráguas" },
                //new Item {Position= randomPosition(), Item_id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Escadas junto ao Palácio dos Melos, Danificadas e sujas", Description="Um corrimão desapareceu, o chão está escrito com tinta, há mais de 20 degraus partidos, para além de vomitado e lixo. É uma das principais ligações entre o Polo I e a Alta e Baixa. Por ali passam diariamente largas centenas de residentes, estudantes e turistas.", Latitude=40.769019,Longitude=-7.351672,User_Id="Cenas" },
                //new Item {Position= randomPosition(), Item_id = Guid.NewGuid().ToString(),  ShowCallout = true, Title = "First item",Text = "First item", Description="This is an item description.", Latitude=40.7699,Longitude=-7.353372,User_Id="Cenas" },
                ////new Item {Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Second item", Description="This is an item description.", Latitude=40.723019,Longitude=-7.363672,User_Id="Cenas",Council="M1" },
                ////new Item {Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true, Title = "First item", Text = "Third item", Description="This is an item description." , Latitude=40.746019,Longitude=-7.362672,User_Id="Cenas",Council="M1" },
                //new Item {Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Fourth item", Description="This is an item description.", Latitude=40.7219,Longitude=-7.341672,User_Id="Cenas",Council="M1" },
                //new Item {Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true,  Title = "First item",Text = "Escadas junto ao Palácio dos Melos, Danificadas e sujas", Description="Um corrimão desapareceu, o chão está escrito com tinta, há mais de 20 degraus partidos, para além de vomitado e lixo. É uma das principais ligações entre o Polo I e a Alta e Baixa. Por ali passam diariamente largas centenas de residentes, estudantes e turistas.", Latitude=40.769019,Longitude=-7.351672,User_Id="Cenas",Council="M1" },

                // new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true, Text = "First item", Description="This is an item description.", Latitude=40.7699,Longitude=-7.353372,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(),ShowCallout = true,  Text = "Second item", Description="This is an item description.", Latitude=40.762019,Longitude=-7.366372,User_Id="Cenas",Council="M1" },
                //new Item {Position= randomPosition(), _Id  = Guid.NewGuid().ToString(), ShowCallout = true, Text = "Third item", Description="This is an item description." , Latitude=40.746019,Longitude=-7.363672,User_Id="Cenas",Council="M1" },
                //new Item {Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true, Text = "Fourth item", Description="This is an item description.", Latitude=40.7619,Longitude=-7.34272,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "Escadas junto ao Palácio dos Melos, Danificadas e sujas", Description="Um corrimão desapareceu, o chão está escrito com tinta, há mais de 20 degraus partidos, para além de vomitado e lixo. É uma das principais ligações entre o Polo I e a Alta e Baixa. Por ali passam diariamente largas centenas de residentes, estudantes e turistas.", Latitude=40.769019,Longitude=-7.351672,User_Id="Cenas",Council="M1" },

                //            new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description.", Latitude=40.7699,Longitude=-7.353372,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(),ShowCallout = true,  Text = "Second item", Description="This is an item description.", Latitude=40.763019,Longitude=-7.361672,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(),ShowCallout = true,  Text = "Third item", Description="This is an item description." , Latitude=40.766019,Longitude=-7.361672,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), ShowCallout = true, Text = "Fourth item", Description="This is an item description.", Latitude=40.7319,Longitude=-7.381672,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(),ShowCallout = true,  Text = "Escadas junto ao Palácio dos Melos, Danificadas e sujas", Description="Um corrimão desapareceu, o chão está escrito com tinta, há mais de 20 degraus partidos, para além de vomitado e lixo. É uma das principais ligações entre o Polo I e a Alta e Baixa. Por ali passam diariamente largas centenas de residentes, estudantes e turistas.", Latitude=40.769019,Longitude=-7.351672,User_Id="Cenas",Council="M1" },

                //            new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description.", Latitude=40.7699,Longitude=-7.353372,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description.", Latitude=40.663019,Longitude=-7.341672,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." , Latitude=40.763019,Longitude=-7.351672,User_Id="Cenas",Council="M1" },
                //new Item { Position= randomPosition(), _Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description.", Latitude=40.7719,Longitude=-7.346672,User_Id="Cenas",Council="M1" },
                //new Item { _Id = Guid.NewGuid().ToString(), Text = "Escadas junto ao Palácio dos Melos, Danificadas e sujas", Description="Um corrimão desapareceu, o chão está escrito com tinta, há mais de 20 degraus partidos, para além de vomitado e lixo. É uma das principais ligações entre o Polo I e a Alta e Baixa. Por ali passam diariamente largas centenas de residentes, estudantes e turistas.", Latitude=40.769019,Longitude=-7.351672,User_Id="Cenas",Council="M1" },

            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Item_id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items.OrderByDescending(Item => Item.Date)); // order por data nesta versão);
        }



        public Position randomPosition()
        {
            Random rng = new Random();

                double lat = rng.NextDouble() * (40.7699 - 40.7500) + 40.7500;
                double lon = rng.NextDouble() * (7.353372 - 7.300000) + 7.30000;
         
            //Latitude=40.7699,Longitude=-7.353372,

            return _position = new Position(lat, lon);


        }





    }
}