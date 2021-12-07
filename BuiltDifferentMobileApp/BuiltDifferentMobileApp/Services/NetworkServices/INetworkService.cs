using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BuiltDifferentMobileApp.Services.NetworkServices {
    public interface INetworkService<T> where T : HttpResponseMessage, new() {
        Task<T> PostWorkout(string uri);
    }
}
