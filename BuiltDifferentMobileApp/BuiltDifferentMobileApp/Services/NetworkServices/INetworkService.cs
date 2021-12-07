using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BuiltDifferentMobileApp.Services.NetworkServices {
    public interface INetworkService<T> where T : HttpResponseMessage, new() {
    }
}
