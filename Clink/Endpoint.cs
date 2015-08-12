using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    class Endpoint
    {
        public Endpoint()
        {
            Statuses = new HashSet<EndpointStatus>();
        }

        public string Description
        { get; set; }
        public string Url
        { get; set; }
        public double Interval
        { get; set; }

        public virtual ICollection<EndpointStatus> Statuses
        { get; set; }

        public EndpointStatus LastStatus
        {
            get
            {
                return Statuses.OrderByDescending( s => s.CheckTime ).FirstOrDefault();
            }
        }
    }
}
