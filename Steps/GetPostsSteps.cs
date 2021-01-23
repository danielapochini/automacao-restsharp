using RestSharp;
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
    class GetPostsSteps
    {

        public RestClient client = new RestClient("http://localhost:3000/");
        public RestRequest request = new RestRequest();
        public IRestResponse<Posts> response = new RestResponse<Posts>();

        [Given(@"I perform GET operation for ""(.*)""")]
        public void GivenIPerformGETOperationFor(string url)
        {
            request = new RestRequest(url, Method.GET);
        }

        [When(@"I perform operation for post ""(.*)""")]
        public void WhenIPerformOperationForPost(int postId)
        {
            request.AddUrlSegment("postid", postId.ToString());
            response = client.ExecuteGetAsync<Posts>(request).GetAwaiter().GetResult();
        }

        [Then(@"i should see the ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.Equal(response.GetResponseObject(key),value);
        }

    }
}
