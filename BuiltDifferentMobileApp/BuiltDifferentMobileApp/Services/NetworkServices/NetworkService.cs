using BuiltDifferentMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private AccountService accountService = AccountService.Instance;
        private HttpClient httpClient;
        private NetworkService()
        {
            httpClient = new HttpClient();
        }

        /*
         *  For now, we're just catching TaskCanceledException and returning null, could catch more stuff in the future
         *  TaskCanceledException: request timed out, for example if the API is not running a request will timeout causing the app to crash
         */

        public async Task<TResult> GetAsync<TResult>(string uri) {

            HttpResponseMessage response = await httpClient.GetAsync(uri);


            string serialized = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<TResult>(serialized);

            return result;

        }

        public async Task<TResult> PutAsync<TResult>(string uri, object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            }
            catch (TaskCanceledException)
            {
                return default(TResult);
            }
        }


        public async Task<TResult> PostAsync<TResult>(string uri, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(uri, jsonString);

                if (response.IsSuccessStatusCode)
                {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            }
            catch (TaskCanceledException)
            {
                return default(TResult);
            }
        }

        // Returning a bool for now, change depending on if API will return what was deleted or just a status
        public async Task<bool> DeleteAsync(string uri)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(uri);

                return response.IsSuccessStatusCode;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        public async Task<bool> LoginAsync(string uri, object user) {
            try {
                var json = JsonConvert.SerializeObject(user);
                var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage loginResponse = await httpClient.PostAsync(uri, jsonString);

                if(loginResponse.IsSuccessStatusCode) {
                    HttpHeaders headers = loginResponse.Headers;

                    if(headers.TryGetValues("Authorization", out IEnumerable<string> values)) {
                        var JWTToken = values.First();

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);

                        HttpResponseMessage profileResponse = await httpClient.GetAsync(uri);

                        bool matchedAccountType = await accountService.SetCurrentUser(profileResponse);

                        if(matchedAccountType) {
                            return true;
                        } else {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                            return false;
                        }
                    }
                }

                return false;
            } catch(TaskCanceledException) {
                return false;
            }
        }

    }
}