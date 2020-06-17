using IdentityModel.Client;
using MonthlyUpdateWorkerService.Api.Data;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdateWorkerService.Api.Services
{
    class BillService : BaseService<Bill>
    {
        private UserService _userService;

        public BillService(UserService userService)
        {
            _userService = userService;
        }

        protected override string EndPointAddress { get { return "Bills"; } }

        public async Task<string> UpdateBillsAsync()
        {
            StringBuilder returnBulder = new StringBuilder("Updated Users:" + Environment.NewLine);


            using (var client = new HttpClient())
            {
                var token = await GetToken(client);
                client.SetBearerToken(token);

                List<Bill> bills = await GetAllAsync(token, client);

                foreach(Bill bill in bills)
                {
                    if (!bill.Recuring)
                    {
                        var responce = await DeleteEntityAsync(token, client, bill.Id);
                        returnBulder.Append(responce + Environment.NewLine);
                    }
                    else
                    {
                        User user = await _userService.GetById(token, client, bill.UserId);
                        user.TotalSavings -= bill.Amount;
                        var responce = await _userService.UpdateEntityAsync(token, client, user);
                        returnBulder.Append(responce);
                    }
                }

                return returnBulder.ToString();
            }
        }
    }
}
