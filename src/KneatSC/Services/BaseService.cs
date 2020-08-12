using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KneatSC.Services
{
    public class BaseService
    {
        public async Task<T> Get<T>(string url, Func<string, T> callBack)
        {
            using (var client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(url))
                using (HttpContent content = res.Content)
                {
                    string data = await content.ReadAsStringAsync();

                    if (data == null)
                    {
                        return default;
                    }
                    else
                    {
                        return callBack(data);
                    }
                }
            }
        }

        public async Task<T> Get<T>(string api)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(api);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception("The request to get all the starships was not successful.");
                    }

                    string result = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
