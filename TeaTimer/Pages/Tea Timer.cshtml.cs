using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using TeaTimer.Models;

namespace TeaTimer.Pages
{
    public class Tea_TimerModel : PageModel
    {
        private readonly IConfiguration _config;

        public Tea_TimerModel(IConfiguration config)
        {
            _config = config;
        }

        [BindProperty(SupportsGet = true)]
        public string TeaId { get; set; }


        public Tea TeaTime { get; set; }

        public List<Tea> teaList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (TeaId != null)
            {
                var connectionString = _config["TeaTimerDB"];

                var client = new CosmosClientBuilder(connectionString)
                        .WithSerializerOptions(new CosmosSerializationOptions { PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase })
                        .Build();

                var teaContainer = client.GetContainer("TeaDB", "TeaContainer");

                teaList = teaContainer.GetItemLinqQueryable<Tea>(allowSynchronousQueryExecution: true)
                      .Where(t => t.id.ToString() == TeaId)
                      .ToList();

                TeaTime = teaList.FirstOrDefault();
            }

            return Page();
        }
    }
}
