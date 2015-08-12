using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Reporters
{
    class ConsoleReporter : IReporter
    {
        public void Report( Endpoint endpoint, EndpointStatus status )
        {
            Console.WriteLine( "{0} ({1}) is {2} as of {3}", endpoint.Description, endpoint.Url, status.Status, status.Timestamp );
        }
    }
}
