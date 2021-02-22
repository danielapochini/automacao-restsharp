using Newtonsoft.Json;
using RestSharp;
using RestsharpSpecflow.Base;
using RestsharpSpecflow.Model;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
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

        [Given(@"I perform POST operation to create new location with following details")]
        public void GivenIPerformPOSTOperationToCreateNewLocationWithFollowingDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _settings.Request = new RestRequest("/location", Method.POST);

            //body

            var body = new LocationModel
            {
                city = (string)data.city,
                country = (string)data.country,
                address = new List<Address>()
                {
                    new Address()
                    {
                        street = (string)data.street,
                        flat_no = (string)data.flatNo,
                        pincode = (int)data.pincode,
                        type = (string)data.type
                    }
                }
            };

            _settings.Request.AddJsonBody(body);
            _settings.Response = _settings.RestClient.Execute(_settings.Request);

        }

        [Given(@"I perform PUT operation to update the address details")]
        public void GivenIPerformPUTOperationToUpdateTheAddressDetails(Table table)
        {
            dynamic data = table.CreateDynamicInstance();

            var dynamicId = _settings.Response.GetResponseObject("id");

            _settings.Request = new RestRequest($"/location/{dynamicId}", Method.PUT);

            //body

            var body = new LocationModel
            {
                city = (string)data.city,
                country = (string)data.country,
                address = new List<Address>()
                {
                    new Address()
                    {
                        street = (string)data.street,
                        flat_no = (string)data.flatNo,
                        pincode = (int)data.pincode,
                        type = (string)data.type
                    }
                }
            };

            _settings.Request.AddJsonBody(body);
            _settings.Response = _settings.RestClient.Execute(_settings.Request);

        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)"" for address")]
        public void ThenIShouldSeeTheNameAsForAddress(string key, string value)
        {
            var locations = Libraries.DeserializeResponse(_settings.Response);

            foreach(var location in locations)
            {
                if(location.Key == key)
                {
                    var address = JsonConvert.DeserializeObject<List<Address>>(location.Value);

                    if (address != null)
                    {
                        Assert.Equal(address.FirstOrDefault().street, value);
                    }

                }
            }
        }

    }
}
