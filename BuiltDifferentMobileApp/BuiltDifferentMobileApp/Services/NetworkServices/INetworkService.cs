using BuiltDifferentMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BuiltDifferentMobileApp.Services.NetworkServices {
    public interface INetworkService<T> where T : HttpResponseMessage, new() {

        Task<TResult> GetAsync<TResult>(string uri);
        Task<bool> DeleteAsync(string uri);
        Task<TResult> PutAsync<TResult>(string uri, object data);
        Task<TResult> PostAsync<TResult>(string uri, object data, bool IsEncoded = false);
        Task<HttpStatusCode> LoginAsync(string uri, object user);
        Task<HttpStatusCode> RegisterAsync(string uri, object user);
        void RemoveJWTToken();
        Task<HttpStatusCode> PostAsyncHttpResponseMessage(string uri, object data, bool IsEncoded = false);
        Task<HttpStatusCode> PutAsyncHttpResponseMessage(string uri, object obj);
    }
}