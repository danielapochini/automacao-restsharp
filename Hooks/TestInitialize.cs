using Microsoft.Extensions.Configuration;
using RestsharpSpecflow.Base;
using System;  
using TechTalk.SpecFlow;

namespace RestsharpSpecflow.Hooks
{
    [Binding]
    public class TestInitialize
    { 
        private Settings _settings; 
        public TestInitialize(Settings settings)
        {
            _settings = settings;
        }

        [BeforeScenario]
        public void TestSetup()
        {
            var config = new ConfigurationBuilder() 
                .AddJsonFile("appsettings.json")
                .Build();
            _settings.BaseUrl = new Uri(config["URL"].ToString()); 
            _settings.RestClient.BaseUrl = _settings.BaseUrl;
             
        }
    }
}
