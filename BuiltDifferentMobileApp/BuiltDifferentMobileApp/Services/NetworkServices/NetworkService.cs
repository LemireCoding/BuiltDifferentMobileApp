using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<T> PostExercise(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
