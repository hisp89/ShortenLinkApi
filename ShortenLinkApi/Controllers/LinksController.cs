using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShortenLinkApi.Models;
using ShortenLinkApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortenLinkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinksController : ControllerBase
    {
        private readonly LinkServices _service;

        public LinksController(LinkServices service)
        {
            _service = service;
        }

        // GET: api/Links
        [HttpGet]
        public Task<List<Link>> Get() => _service.Get();

        // GET: api/Links/5
        [HttpGet("{shortUrl}", Name = "Get")]
        public Task<Link> Get(string shortUrl) => _service.Get(shortUrl);

        // POST: api/Links
        [HttpPost]
        public Task<Link> Post([FromBody] string url) => _service.Create(url);
    }
}
