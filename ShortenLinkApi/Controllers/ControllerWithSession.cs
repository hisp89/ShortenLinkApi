using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortenLinkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortenLinkApi.Controllers
{
    public class ControllerWithSession : ControllerBase
    {
        private string sessionkey = ".Session";
        public string Session {
            get
            {
                var sessionid = HttpContext.Request.Cookies.ContainsKey(sessionkey) ? HttpContext.Request.Cookies[sessionkey] : "";
                if (string.IsNullOrWhiteSpace(sessionid))
                {
                    sessionid = Utils.GetRandomString();
                    HttpContext.Response.Cookies.Append(sessionkey, sessionid);
                }
                return sessionid;
            } 
        }
    }
}
