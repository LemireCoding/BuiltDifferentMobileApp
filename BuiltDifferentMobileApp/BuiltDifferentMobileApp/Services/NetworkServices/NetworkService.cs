using BuiltDifferentMobileApp.Models;
using BuiltDifferentMobileApp.Services.AccountServices;
using BuiltDifferentMobileApp.Views.Login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.NetworkServices
{
    public sealed class NetworkService<T> : INetworkService<T> where T : HttpResponseMessage, new()
    {
        private static readonly Lazy<INetworkService<T>> lazy = new Lazy<INetworkService<T>>(() => new NetworkService<T>());

        public static INetworkService<T> Instance { get { return lazy.Value; } }

        private IAccountService accountService = AccountService.Instance;
        private HttpClient httpClient;

        private NetworkService()
        {
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(15);
        }

        private async Task CheckIfSuspendedOrTokenExpired(HttpResponseMessage response) {
            if(response.StatusCode == HttpStatusCode.Forbidden) {
                accountService.RemoveCurrentUser();
                await Application.Current.MainPage.DisplayAlert("Your account requires you to relogin", "You will now be returned to the login page", "OK");
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                throw new OperationCanceledException();
            }
        }

        public async Task<TResult> GetAsync<TResult>(string uri) {
            try {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                await CheckIfSuspendedOrTokenExpired(response);

                string serialized = await response.Content.ReadAsStringAsync();

                if(response.IsSuccessStatusCode) {
                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            }
            catch(OperationCanceledException) {
                return default(TResult);
            }

        }

        public async Task<Stream> GetStreamAsync(string uri) {
            try {
                Stream stream = null;

                var response = await httpClient.GetAsync(uri);

                await CheckIfSuspendedOrTokenExpired(response);

                if(response.IsSuccessStatusCode) {
                    stream = await httpClient.GetStreamAsync(uri);
                }

                return stream ?? null;
            } catch(OperationCanceledException) {
                return null;
            }

        }

        public async Task<TResult> PutAsync<TResult>(string uri, object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(uri, content);

                await CheckIfSuspendedOrTokenExpired(response);

                if (response.IsSuccessStatusCode)
                {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            }
            catch (OperationCanceledException)
            {
                return default(TResult);
            }
        }


        public async Task<TResult> PostAsync<TResult>(string uri, object data, bool MultiPartFormData = false)
        {
            try
            {
                HttpResponseMessage response = null;

                if(!MultiPartFormData) {
                    var json = JsonConvert.SerializeObject(data);
                    var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await httpClient.PostAsync(uri, jsonString);
                } else {
                    response = await httpClient.PostAsync(uri, (MultipartFormDataContent)data);
                }

                await CheckIfSuspendedOrTokenExpired(response);

                if (response.IsSuccessStatusCode)
                {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            }
            catch(OperationCanceledException) {
                return default(TResult);
            }
        }

        // Returning a bool for now, change depending on if API will return what was deleted or just a status
        public async Task<bool> DeleteAsync(string uri)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(uri);

                await CheckIfSuspendedOrTokenExpired(response);

                return response.IsSuccessStatusCode;
            } catch(OperationCanceledException) {
                return false;
            }
        }

        public async Task<HttpStatusCode> LoginAsync(string uri, object user) {
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

                        if(!matchedAccountType) {
                            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                        }
                    }
                }

                return loginResponse.StatusCode;
            } catch(OperationCanceledException) {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task UpdateCurrentUser() {
            try {
                HttpResponseMessage response = await httpClient.GetAsync(APIConstants.GetLoginUri());

                await CheckIfSuspendedOrTokenExpired(response);

                bool matchedAccountType = await accountService.SetCurrentUser(response);
            } catch(Exception) { }
        }

        public async Task<HttpStatusCode> RegisterAsync(string uri, object user) {
            try {
                var json = JsonConvert.SerializeObject(user);
                var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage loginResponse = await httpClient.PostAsync(uri, jsonString);

                return loginResponse.StatusCode;
            } catch(OperationCanceledException) {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public void RemoveJWTToken() {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            accountService.RemoveCurrentUser();
        }


        public async Task<HttpStatusCode> PostAsyncHttpResponseMessage(string uri, object data, bool MultiPartFormData = false)
        {
            try
            {
                HttpResponseMessage response = null;

                if(!MultiPartFormData) {
                    var json = JsonConvert.SerializeObject(data);
                    var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                    response = await httpClient.PostAsync(uri, jsonString);
                } else {
                    response = await httpClient.PostAsync(uri, (MultipartFormDataContent)data);
                }

                await CheckIfSuspendedOrTokenExpired(response);

                return response.StatusCode;
            }
            catch (OperationCanceledException)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public async Task<HttpStatusCode> PutAsyncHttpResponseMessage(string uri, object obj) {
            try {
                var json = JsonConvert.SerializeObject(obj);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PutAsync(uri, content);

                await CheckIfSuspendedOrTokenExpired(response);

                return response.StatusCode;
            } catch(OperationCanceledException) {
                return HttpStatusCode.RequestTimeout;
            }
        }

    }
}