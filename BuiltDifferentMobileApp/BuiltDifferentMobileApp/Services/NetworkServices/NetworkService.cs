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

        public async Task<T> PostAsync(string uri, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

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

        public async Task<T> PutAsync(string uri, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);

            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpHeaders header = response.Headers;
                //Add auth once implementned
            }
            return (T)response;
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(serialized);
            

            return result;
        }

        public Task<T> GetAsync(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
