using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    enum Status
    {
        Up,
        Down
    }

    class EndpointStatus
    {
        public DateTime Timestamp
        { get; set; }
        public HttpStatusCode StatusCode
        { get; set; }
        public Status Status
        {
            get
            {
                int numericStatusCode = (int)StatusCode;
                return ( numericStatusCode < 300 && numericStatusCode >= 200 ) ? Status.Up : Status.Down;
            }
        }
    }
}
