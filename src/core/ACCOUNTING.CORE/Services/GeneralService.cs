using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Models;
using ACCOUNTING.CORE.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ACCOUNTING.CORE.Services
{
    public class GeneralService : IGeneralService
    {

        private string baseUrl = $"{Constants.SERVER_URL}/{Constants.API_VERSION}";

        public async Task<string> SendGetRequest(string route)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:7030/")
                };

                var response = await client.GetFromJsonAsync<dynamic>(route);
                return response.ToString();


                //response.EnsureSuccessStatusCode();
                //var responseBody = await response.Content.ReadAsStringAsync();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error aquii: {ex.Message}");
            }
            return null;
        }


        public async Task<string> SendPostRequest(string route, object body)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.PostAsync(route, new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, MediaTypeNames.Application.Json));
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return "Error";
        }

    }
}
