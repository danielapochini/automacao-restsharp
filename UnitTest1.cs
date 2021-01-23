using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using RestsharpSpecflow.Model;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestsharpSpecflow
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts/{postid}", Method.GET); 
            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["author"];

            var result = response.GetResponseObject("author");
            Assert.Equal("Karthik KK", result);
          
        }

        [Fact]
        public void PostWithAnonymousBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts/{postid}/profile", Method.POST);
           
            request.AddJsonBody(new { name = "Dan" });
            request.AddUrlSegment("postid", 1);
            var response = client.Execute(request);

            var result = response.DeserializeResponse()["name"]; 
            Assert.Equal("Dan", result);
        }

        [Fact]
        public void PostWithTypeClassBody()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            request.AddJsonBody(
                new Posts() { 
                    id = "15", 
                    author = "Daniela", 
                    title = "RestSharp"
                });

            //desserializa todos os valores para o tipo "Posts"
            var response = client.Execute<Posts>(request);
             
            Assert.Equal("Daniela", response.Data.author);
        }

        [Fact] 
        public void PostWithAsync()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("posts", Method.POST);
            
            request.AddJsonBody(
                new Posts()
                {
                    id = "19",
                    author = "Daniela",
                    title = "RestSharp"
                });


            var response = client.ExecutePostAsync<Posts>(request).GetAwaiter().GetResult();
              
            Assert.Equal("Daniela", response.Data.author);
        }

    }
}
