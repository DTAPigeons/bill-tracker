using IdentityModel.Client;
using MonthlyUpdateWorkerService.Api.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdateWorkerService.Api.Services
{
    abstract class BaseService<TEntity> where TEntity:BaseEntity
    {

        private readonly string apiAddress = "https://localhost:44328/api/";
        private readonly string discoveryAddress = "https://localhost:5001/";
        private readonly string clientId = "montly_worker";
        private readonly string apiScope = "trackerApi";

        protected abstract string EndPointAddress { get; }

        protected async Task<string> GetToken(HttpClient client)
        {
            var disco = await client.GetDiscoveryDocumentAsync(discoveryAddress);
            if (disco.IsError)
            {
                return "";
            }

            var tokenResponce = await client.RequestClientCredentialsTokenAsync(
                                                                                new ClientCredentialsTokenRequest()
                                                                                {
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

        public virtual async Task<List<TEntity>> GetAllAsync(string token, HttpClient client)
        {
            client.SetBearerToken(token);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responce = await client.GetAsync(apiAddress + EndPointAddress);

            if (!responce.IsSuccessStatusCode)
            {
                return new List<TEntity>();
            }

            var responceStream = await responce.Content.ReadAsStringAsync();
            List<TEntity> items = JsonConvert.DeserializeObject<List<TEntity>>(responceStream);
            return items;
        }

        public virtual async Task<TEntity> GetById(string token, HttpClient client, string id) {
            client.SetBearerToken(token);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var responce = await client.GetAsync(apiAddress + EndPointAddress + "/" + id);
            var responceStream = await responce.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(responceStream);

        }

        public async Task<string> UpdateEntityAsync(string token, HttpClient client, TEntity item)
        {
            var returnBuilder = new StringBuilder("Updating item: " + item.Id);
            client.SetBearerToken(token);
            var json = JsonConvert.SerializeObject(item);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var responce = await client.PutAsync(apiAddress + EndPointAddress + "/" + item.Id, data);

            returnBuilder.Append(" Satus:" + responce.StatusCode);

            return returnBuilder.ToString();
        }

        public async Task<string> DeleteEntityAsync(string token, HttpClient client, string id)
        {
            var returnBuilder = new StringBuilder("Deleting item: " + id);
            client.SetBearerToken(token);
            var responce = await client.DeleteAsync(apiAddress + EndPointAddress + "/" + id);
            returnBuilder.Append(" Satus:" + responce.StatusCode);

            return returnBuilder.ToString();
        }
    }
}
