using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using TeaTimer.Models;

namespace TeaTimer.Pages
{
    public class AddNewTeaModel : PageModel
    {
        private readonly IConfiguration _config;

        public AddNewTeaModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Type { get; set; }

        [BindProperty]
        public string Temperature { get; set; }

        [BindProperty]
        public string Time { get; set; }

        


        public async Task OnPostAsync()
        {
            if (Name != "")
            {

                var connectionString = _config["TeaTimerDB"];

                var client = new CosmosClientBuilder(connectionString)
                    .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                    .Build();


                Tea tea = new Tea();

                tea.id = Guid.NewGuid();
                tea.Name = Name;
                tea.Sort = Type;
                tea.Temperature = Temperature;
                tea.BrewTime = Time;

                var teaContainer = client.GetContainer("TeaDB", "TeaContainer");

                await teaContainer.CreateItemAsync(tea);

            }

        }
    }
}
