using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using APIInterfaceLayer.Models;

namespace APIInterfaceLayer
{
    public class WebClientInterface
    {
        IConfigSettings _configSettings;

        public WebClientInterface(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public async Task CallHistoricalAPIAsync(string accessToken, string uri)
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here.  
                doClientAPISettings(client, accessToken);

                HttpResponseMessage response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var historical = await response.Content.ReadAsAsync<Historical>();
                    Console.WriteLine(historical.data.Capacity);
                }
                else
                {
                    throw new HttpRequestException(string.Format("Status Code: {0}, Reason: {1}", response.StatusCode, response.ReasonPhrase));
                }
            }
        }

        private void doClientAPISettings(HttpClient client, string accessToken)
        {
            client.BaseAddress = new Uri(_configSettings.UpstoxBaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer "+ accessToken);
            client.DefaultRequestHeaders.Add("User-Agent", _configSettings.UserAgent);
        }

    }
}
