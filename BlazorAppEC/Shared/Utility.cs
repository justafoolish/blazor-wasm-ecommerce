using System.Text.Json;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BlazorAppEC.Shared {
    public static class Utility {
        public static T jsonConvert<T>(string data)
        {
            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<T>(data.ToString(), jsonSerializerOptions);
        }
    }
}