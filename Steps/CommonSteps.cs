using RestSharp;
using RestSharp.Authenticators;
using RestsharpSpecflow.Base;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestsharpSpecflow.Steps
{
    [Binding]
    public class CommonSteps
    {
        private Settings _settings; 
        public CommonSteps(Settings settings)
        {
            _settings = settings;
        }
        [Given(@"I get JWT authentication of User with following values")]
        public void GivenIGetJWTAuthenticationOfUserWithFollowingValues(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
             
            _settings.Request = new RestRequest("auth/login", Method.POST);

            _settings.Request.AddJsonBody(new { email = (string)data.email, password = (string)data.password });

            //pegar access token
            _settings.Response = _settings.RestClient.ExecutePostAsync(_settings.Request).GetAwaiter().GetResult();
            var access_token = _settings.Response.GetResponseObject("access_token");

            //realizando autenticação
            var authenticator = new JwtAuthenticator(access_token);
            _settings.RestClient.Authenticator = authenticator;
        }

    }
}
