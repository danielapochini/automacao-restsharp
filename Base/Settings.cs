using RestSharp;
using RestsharpSpecflow.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestsharpSpecflow.Base
{
    public class Settings
    {
        public Uri BaseUrl { get; set; }
        public IRestResponse<Posts> Response { get; set; }
        public IRestRequest Request { get; set; }
        public RestClient RestClient { get; set; } = new RestClient();

    }
}
