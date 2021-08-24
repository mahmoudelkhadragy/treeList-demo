using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeListDemo.BL;
using Newtonsoft.Json;

namespace TreeListDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadDataController : ControllerBase
    {
        private readonly Generator _generator;
        public LoadDataController(Generator generator)
        {
            _generator = generator;
        }
        public string Get(int viewNo, string langCode="en")
        {
            return _generator.LoadData(viewNo);
        }
    }
}
