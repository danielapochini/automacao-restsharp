using RestSharp;
using RestsharpSpecflow.Base;
using RestsharpSpecflow.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RestsharpSpecflow.Steps
{
    [Binding]
    public class PostProfileSteps
    {
        private Settings _settings; 
        public PostProfileSteps(Settings settings) => _settings = settings;

        [Given(@"I perform POST operation for ""(.*)"" with body")]
        public void GivenIPerformPOSTOperationForWithBody(string url, Table table)
        { 
            //  O método CreateDynamicInstance() criará o objeto dinâmico que conterá os valores da tabela
            // que são passados ​​como um argumento para os steps 

            dynamic data = table.CreateDynamicInstance();

            _settings.Request = new RestRequest(url, Method.POST);

            _settings.Request.AddJsonBody(
                new 
                {
                   name = data.name.ToString()
                });

            _settings.Request.AddUrlSegment("profileId", ((int)data.profile).ToString());

            _settings.Response = _settings.RestClient.ExecutePostAsync<Posts>(_settings.Request).GetAwaiter().GetResult();
        }

    }
}
