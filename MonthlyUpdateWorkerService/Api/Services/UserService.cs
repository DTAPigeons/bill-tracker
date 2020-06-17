using IdentityModel.Client;
using MonthlyUpdateWorkerService.Api.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdateWorkerService.Api.Services
{
    class UserService : BaseService<User>
    {
        protected override string EndPointAddress { get { return "Users"; } }

        public async Task<string> UpdateUserSavingsAsync()
        {
            StringBuilder returnBulder = new StringBuilder("Updated Users:" + Environment.NewLine);


            using (var client = new HttpClient())
            {
                var token = await GetToken(client);
                client.SetBearerToken(token);

                List<User> users = await GetAllAsync(token, client);

                foreach (User user in users)
                {
                    user.TotalSavings += user.Income;
                    var responce = await UpdateEntityAsync(token, client, user);
                    returnBulder.Append(responce + Environment.NewLine);
                }

                return returnBulder.ToString();
            }
        }
    }
}
