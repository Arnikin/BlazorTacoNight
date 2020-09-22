using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Q2.DataCollector;

namespace SRA.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly ILogger<ExperimentController> logger;

        public ExperimentController(ILogger<ExperimentController> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<Experiment> Get()
        {
            return new Experiment[0];

            //return Enumerable.Range(1, 5).Select(index => new Experiment()
            //{
            //    Name = $"Experiment_{index}", 
            //    Settings = new MeasurementSettings(), 
            //})
            //.ToArray();
        }
    }
}