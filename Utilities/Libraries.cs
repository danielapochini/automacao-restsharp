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

        public static string GetResponseObjectv2(this IRestResponse response, string responseObject)
        {
            JObject obs = JObject.Parse(response.Content.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));
            return obs[responseObject].ToString();
        }

        public static string GetResponseObjectArray(this IRestResponse response, string responseObject)
        {
            JArray jArray = JArray.Parse(response.Content);
            foreach (var content in jArray.Children<JObject>())
            {
                foreach(JProperty property in content.Properties())
                {
                    if (property.Name == responseObject)
                    {
                        return property.Value.ToString();
                    }
                }
            } 
            return string.Empty;
        }
    }
}
