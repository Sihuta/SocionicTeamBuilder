using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace SocionicTeamBuilder.Mobile
{
    public interface IHttpClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
