using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using TeaTimer.Models;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace TeaTimer.Pages
{
    public class Tea_CollectionModel : PageModel
    {
        private readonly IConfiguration _config;

        public Tea_CollectionModel(IConfiguration config)
        {
            _config = config;
        }

        public List<Tea> teaList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string DeleteId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Temperature { get; set; }

        public async Task OnGetAsync()
        {
            var connectionString = _config["TeaTimerDB"];

            var client = new CosmosClientBuilder(connectionString)
                    .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                    .Build();

            var teaContainer = client.GetContainer("TeaDB", "TeaContainer");

            if(DeleteId != null)
            {
                await teaContainer.DeleteItemAsync<Tea>(DeleteId, new PartitionKey(Temperature));
            }

            teaList = teaContainer.GetItemLinqQueryable<Tea>(allowSynchronousQueryExecution: true)
                    .OrderBy(t => t.Name)
                    .ToList();

        }
    }
}
