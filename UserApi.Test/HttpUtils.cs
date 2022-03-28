using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Test
{
    public class HttpUtils
    {
        public static HttpContent GetContent(object payload)
        {
            var data = JsonConvert.SerializeObject(payload);
            return new StringContent(data, Encoding.UTF8, "application/json");
        }
    }
}
