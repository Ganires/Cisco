using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CiscoService.Jobs;
using Microsoft.AspNetCore.Mvc;
using OpenAlmaty;
namespace CiscoService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/values/
        [HttpGet("{callerId}/{callerNumber}/{callerPassword}")]
        public ActionResult<string> Get(int callerId, string callerNumber, string callerPassword)
        {
            CiscoSheduler.Start(callerId, callerNumber, callerPassword); 
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
