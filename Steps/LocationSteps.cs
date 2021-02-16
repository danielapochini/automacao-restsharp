using RestsharpSpecflow.Base;
using RestsharpSpecflow.Model;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace RestsharpSpecflow.Steps
{
    [Binding]
    class LocationSteps
    {
        private Settings _settings;  
        public LocationSteps(Settings settings) => _settings = settings;

        [When(@"I perform operation for location as ""(.*)""")]
        public void WhenIPerformOperationForLocationAs(int id)
        {
            _settings.Request.AddOrUpdateParameter("id", id.ToString());

            _settings.Response = _settings.RestClient.ExecuteGetAsync<List<LocationModel>>(_settings.Request).GetAwaiter().GetResult();
        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)"" in response")]
        public void ThenIShouldSeeTheNameAsInResponse(string key, string value)
        {
            Assert.Equal(_settings.Response.GetResponseObjectArray(key), value);
        }

    }
}
