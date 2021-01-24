using RestSharp;
using RestsharpSpecflow.Base;
using RestsharpSpecflow.Model;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace RestsharpSpecflow.Steps
{
    [Binding]
    class GetPostsSteps
    {
        private Settings _settings;
        //expression body member
        public GetPostsSteps(Settings settings) => _settings = settings;
         

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        { 
            _settings.Request = new RestRequest(url, Method.GET);
        }

        [When(@"I perform operation for post ""(.*)""")]
        public void WhenIPerformOperationForPost(int postId)
        { 
            _settings.Request.AddUrlSegment("postid", postId.ToString());
            _settings.Response = _settings.RestClient.ExecuteGetAsync<Posts>(_settings.Request).GetAwaiter().GetResult();
        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.Equal(_settings.Response.GetResponseObject(key),value);
        }

    }
}
