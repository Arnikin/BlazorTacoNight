using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Q2.DataCollector;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace SRA.Client.Service
{
    public class ExperimentService : IExperimentService
    {
        public ExperimentService(HttpClient http)
        {
            Http = http;
        }
        HttpClient  Http { get; set; }
        public async Task<Experiment[]> Get()
        {
            return await Http.GetFromJsonAsync<Experiment[]>("Experiment");
            //return new Experiment[] { new Experiment() { Name = "Test" } };
        }

    }
}
