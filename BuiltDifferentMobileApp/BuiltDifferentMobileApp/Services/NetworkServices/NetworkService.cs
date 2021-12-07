using BuiltDifferentMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public sealed class NetworkService<T> : INetworkService<T> where T : HttpResponseMessage, new()
    {
        private static readonly Lazy<INetworkService<T>> lazy = new Lazy<INetworkService<T>>(() => new NetworkService<T>());

        public static INetworkService<T> Instance { get { return lazy.Value; } }

        private HttpClient httpClient;
        private NetworkService()
        {
            httpClient = new HttpClient();
        }

        public async Task<T> PostWorkoutAsync(string uri, Workout workout)
        {
            var json = JsonConvert.SerializeObject(workout);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpHeaders header = response.Headers;
                //Add auth once implementned
            }
            return (T)response;
        }
    }
}
