﻿using BuiltDifferentMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.NetworkServices {
    public interface INetworkService<T> where T : HttpResponseMessage, new() {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<bool> DeleteAsync(string uri);
        Task<TResult> PutAsync<TResult>(string uri, object data);
        Task<TResult> PostAsync<TResult>(string uri, object data);
    }
}
