using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using RestsharpSpecflow.Model;
using RestsharpSpecflow.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
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

        [Fact]
        public void AuthenticationMechanism()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("auth/login", Method.POST);

            request.AddJsonBody(
                new { 
                    email = "karthik@email.com", 
                    password = "haha123" 
                });

            var response = client.ExecutePostAsync(request).GetAwaiter().GetResult();
            var access_token = response.DeserializeResponse()["access_token"];

            var jwtAuth = new JwtAuthenticator(access_token);
            client.Authenticator = jwtAuth;

            var getRequest = new RestRequest("posts/{postid}", Method.GET);
            getRequest.AddUrlSegment("postid", 5);

            var result = client.ExecuteGetAsync<Posts>(getRequest).GetAwaiter().GetResult();
            Assert.Equal("Karthik KK", result.Data.author);
        }

        [Fact]
        public void AuthenticationMechanismWithJsonFile()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("auth/login", Method.POST);

            var file = @"TestData/Data.json";

            //desserializando o objeto User, pegando os valores do arquivo Data.json e adicionando no body da request
            var jsonData = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            request.AddJsonBody(jsonData);

            var response = client.ExecutePostAsync(request).GetAwaiter().GetResult();
            var access_token = response.DeserializeResponse()["access_token"];

            var jwtAuth = new JwtAuthenticator(access_token);
            client.Authenticator = jwtAuth;

            var getRequest = new RestRequest("posts/{postid}", Method.GET);
            getRequest.AddUrlSegment("postid", 5);

            var result = client.ExecuteGetAsync<Posts>(getRequest).GetAwaiter().GetResult();
            Assert.Equal("Karthik KK", result.Data.author);
        }

        private class User
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}
