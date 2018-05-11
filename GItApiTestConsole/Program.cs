using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using GItApiTestConsole.Models;

namespace GItApiTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            var client = new RestClient("https://api.github.com");
            //2
            var request = new RestRequest("users/LeeMellon/repos", Method.GET);
            request.AddHeader("User-Agent", EV.Key);
            //3
            var response = new RestResponse();
            //4
            
            //5
            Task.Run(async () =>
            {
                response = await GetRestResponceContentAsync(client, request) as RestResponse;
            }).Wait();

            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(response.Content);
            var results = JsonConvert.DeserializeObject<List<Repos>>(jsonResponse.ToString());
            Console.WriteLine(results[0].Name);
            Console.WriteLine(results[1].Name);
            Console.WriteLine(results[2].Name);
            Console.WriteLine(results[3].Name);
            Console.ReadLine();
        }

            public static Task<IRestResponse> GetRestResponceContentAsync(RestClient theClient, RestRequest theRequest)
            {
                var tcs = new TaskCompletionSource<IRestResponse>();
                theClient.ExecuteAsync(theRequest, responce =>
                {
                    tcs.SetResult(responce);
                });
                return tcs.Task;
            }
    }

    public class Repos
    {
        public string Name { get; set; }
        public string Html_url { get; set; }
        public string Description { get; set; }
        public string Commits_url { get; set; }
    }
}