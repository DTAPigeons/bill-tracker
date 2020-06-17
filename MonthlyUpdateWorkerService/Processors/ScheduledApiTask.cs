using Microsoft.Extensions.DependencyInjection;
using MonthlyUpdateWorkerService.Api;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MonthlyUpdateWorkerService.Processors
{
    class ScheduledApiTask : ScheduledProcessor
    {
        ApiClient _apiClient;

        public ScheduledApiTask(IServiceScopeFactory serviceScopeFactory, ApiClient apiClient) : base(serviceScopeFactory)
        {
            _apiClient = apiClient;
        }

        protected override string Schedule => "0 0 1 * *"; //Runs At 12:00:00pm, on the 1st day, every month

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            return _apiClient.UpdateApi();
            
        }
    }
}
