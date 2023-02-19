using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SocionicTeamBuilder.Mobile
{
    public interface IHttpClientService
    {
        HttpClient Client { get; }
        CookieContainer Cookies { get; set; }

    }
}
