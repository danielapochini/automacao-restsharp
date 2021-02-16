using RestSharp;
using RestsharpSpecflow.Base;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace RestsharpSpecflow.Steps
{
    [Binding]
    class FileUploadSteps
    {
        private Settings _settings;
        public FileUploadSteps(Settings settings) => _settings = settings;


        [Given(@"I perform POST operation for ""(.*)""")]
        public void GivenIPerformPOSTOperationFor(string path)
        {
            _settings.Request = new RestRequest(path, Method.POST);
            _settings.Request.AddFile("file", @"C:\Users\pocch\Downloads\test.jpg", "image/jpeg");

            _settings.Response = _settings.RestClient.ExecuteAsPost(_settings.Request, "POST");
        }

        [Then(@"I see the file is being uploaded with response as (.*)")]
        public void ThenISeeTheFileIsBeingUploadedWithResponseAs(string status)
        {
            Assert.Equal(_settings.Response.StatusCode.ToString(), status);
        }
    }
}
