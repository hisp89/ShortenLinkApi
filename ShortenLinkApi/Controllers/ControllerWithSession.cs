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
        public string Session {
            get
            {
                var sessionid = HttpContext.Session.GetString(".Session");
                if (sessionid == null)
                {
                    sessionid = Utils.GetRandomString();
                    HttpContext.Session.SetString(".Session", sessionid);
                }
                return sessionid;
            } 
        }
    }
}
