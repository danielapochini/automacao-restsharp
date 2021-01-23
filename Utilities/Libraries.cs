using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestsharpSpecflow.Utilities
{
    public static class Libraries
    {

        public static Dictionary<string,string> DeserializeResponse(this IRestResponse restResponse)
        {

            var JsonObj = new JsonDeserializer().Deserialize<Dictionary<string,string>>(restResponse);

            return JsonObj;
        }

        public static string GetResponseObject(this IRestResponse response, string responseObject)
        { 
            JObject obs = JObject.Parse(response.Content);
            return obs[responseObject].ToString();
        }
           
    }
}
