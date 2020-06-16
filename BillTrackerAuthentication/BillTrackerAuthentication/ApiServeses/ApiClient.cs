using BillTrackerAuthentication.Quickstart.Account;
using IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace BillTrackerAuthentication.ApiServeses
{
    public class ApiClient
    {
        private readonly IdentityServerTools _identityServerTools;
        private readonly string apiAddress = "https://localhost:44328/api/";

        public ApiClient(IdentityServerTools identityServerTools)
        {
            _identityServerTools = identityServerTools;
        }

        public async Task<HttpResponseMessage> PostUser(UserRegisterViewModel viewModel)
        {

            UserCreateModel user = new UserCreateModel(viewModel);
            using(HttpClient client = new HttpClient())
            {
                var token = await _identityServerTools.IssueClientJwtAsync(
                                                                            clientId: "identity_server",
                                                                            lifetime: 3600,
                                                                            audiences: new[] { "trackerApi" });

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var json = JsonConvert.SerializeObject(user);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(apiAddress + "Users", data);

                return result;
            }
        }
    }
}
