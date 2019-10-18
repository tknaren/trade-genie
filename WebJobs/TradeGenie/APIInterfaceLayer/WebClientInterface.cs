using APIInterfaceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace APIInterfaceLayer
{
    public class WebClientInterface
    {
        private readonly IConfigSettings _configSettings;

        public WebClientInterface(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public Historical GetHistorical(string accessToken, string uri)
        {
            using (var client = new HttpClient())
            {
                //Send HTTP requests from here.  
                doClientAPISettings(client, accessToken);

                Task<HttpResponseMessage> responseTask = Task.Run(async () => await client.GetAsync(uri));
                responseTask.Wait();
                HttpResponseMessage httpResponse = responseTask.Result;

                if (httpResponse.IsSuccessStatusCode)
                {
                    Task<Historical> historicalTask = httpResponse.Content.ReadAsAsync<Historical>();
                    historicalTask.Wait();
                    return historicalTask.Result;
                }
                else
                {
                    throw new HttpRequestException(string.Format("Status Code: {0}, Reason: {1}", httpResponse.StatusCode, httpResponse.ReasonPhrase));
                }
            }
        }



        private void doClientAPISettings(HttpClient client, string accessToken)
        {
            client.BaseAddress = new Uri(_configSettings.UpstoxBaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            client.DefaultRequestHeaders.Add("User-Agent", "Visual Studio");
        }

    }
}
