using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SRA.Client.Service;
using SRA.Client.Models;
using SRA.Client.ViewModel;
using Blazored.Modal;

namespace SRA.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<IExperimentService, ExperimentService>();
            builder.Services.AddSingleton<IModelRepository, ModelRepository>();
            builder.Services.AddTransient<IExperimentsViewModel, ExperimentsViewModel>();
            builder.Services.AddTransient<IExperimentViewModel, ExperimentViewModel>();
            builder.Services.AddTransient<IPlateViewModel, PlateViewModel>();

            await builder.Build().RunAsync();
        }
    }
}
