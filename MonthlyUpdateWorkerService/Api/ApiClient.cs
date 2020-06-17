using IdentityModel.Client;
using MonthlyUpdateWorkerService.Api.Data;
using MonthlyUpdateWorkerService.Api.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdateWorkerService.Api
{
    class ApiClient
    {
        UserService _userService;
        BillService _billService;

        public ApiClient(UserService userService, BillService billService)
        {
            _userService = userService;
            _billService = billService;
        }
        
        public async Task<string> UpdateApi()
        {
            StringBuilder returnBuilder = new StringBuilder("Updating Api"+Environment.NewLine);
            var userResponce = await _userService.UpdateUserSavingsAsync();
            returnBuilder.Append(userResponce);
            var billResponce = await _billService.UpdateBillsAsync();
            returnBuilder.Append(billResponce);

            return returnBuilder.ToString();
        }
        
        /*
        private readonly string apiAddress = "https://localhost:44328/api/";
        private readonly string discoveryAddress = "https://localhost:5001/";
        private readonly string clientId = "montly_worker";
        private readonly string apiScope = "trackerApi";

        public async Task<string> GetToken(HttpClient client)
        {
                var disco = await client.GetDiscoveryDocumentAsync(discoveryAddress);
                if (disco.IsError)
                {
                    return "";
                }

                var tokenResponce = await client.RequestClientCredentialsTokenAsync(
                                                                                    new ClientCredentialsTokenRequest() { 
                                                                                        Address = disco.TokenEndpoint,
                                                                                        ClientId = clientId,
                                                                                        Scope = apiScope,
                                                                                        ClientSecret = clientId
                                                                                    
                                                                                    });
                if (tokenResponce.IsError)
                {
                    return "";
                }

                return tokenResponce.AccessToken;           
            
            
        }

        public async Task<string> UpdateUserSavingsAsync()
        {
            StringBuilder returnBulder = new StringBuilder("Updated Users:"+Environment.NewLine);
            

            using(var client = new HttpClient())
            {
                var token = await GetToken(client);
                client.SetBearerToken(token);

                List<User> users = await GetUsersAsync(token, client);

                foreach(User user in users)
                {
                    user.TotalSavings += user.Income;
                    var responce = await UpdateUserAsync(token, client, user);
                    returnBulder.Append(responce + Environment.NewLine);
                }

                return returnBulder.ToString();
            }
        }

        public async Task<List<User>> GetUsersAsync(string token, HttpClient client)
        {
            client.SetBearerToken(token);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responce = await client.GetAsync(apiAddress + "Users");

            if (!responce.IsSuccessStatusCode)
            {
                return new List<User>();
            }

            var responceStream = await responce.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(responceStream);
            return users;
        }

        public async Task<string> UpdateUserAsync(string token, HttpClient client, User user)
        {
            var returnBuilder = new StringBuilder("Updating user: " + user.Name);
            client.SetBearerToken(token);
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var responce = await client.PutAsync(apiAddress + "Users/" + user.Id, data);

            returnBuilder.Append(" Satus:" + responce.StatusCode);

            return returnBuilder.ToString();
        }
        */
    }
}
