using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobTest
{
    public class KpiRecalculationMessage
    {
        public Guid Id {get; set; }

        public Guid OrganizationId { get; set; }

        public Guid TaskId { get; set; }

        public KpiRecalculationMessage() { }
    }
}
