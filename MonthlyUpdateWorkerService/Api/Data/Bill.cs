using System;
using System.Collections.Generic;
using System.Text;

namespace MonthlyUpdateWorkerService.Api.Data
{
    class Bill: BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public bool Recuring { get; set; }

        public string UserId { get; set; }
    }
}
