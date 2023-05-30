using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Private.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/private")]
    public class PrivateController : Controller
    {
        private readonly ILogger<PrivateController> _logger;

        public PrivateController(ILogger<PrivateController> logger)
        {
            _logger = logger;
        }
        [HttpGet("First")]
        public string First()
        {
            return "private service - first";
        }
        [HttpGet("Second")]
        public string Second()
        {
            return "private service - second";
        }
    }
}

