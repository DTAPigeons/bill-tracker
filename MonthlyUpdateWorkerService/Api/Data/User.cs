using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyUpdateWorkerService.Api.Data
{
    class User: BaseEntity
    {
        public string AccountName { get; set; }
        public string Name { get; set; }
        public double Income { get; set; }

        public double TotalSavings { get; set; }
    }
}
