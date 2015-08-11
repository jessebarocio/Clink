using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    class Program
    {
        static void Main( string[] args )
        {
            var monitors = new List<EndpointMonitor>();
            var endpointRepo = new EndpointRepository();

            foreach ( var endpoint in endpointRepo.GetAll() )
            {
                var monitor = new EndpointMonitor( endpoint );
                monitor.EndpointChecked += Monitor_EndpointChecked;
                monitor.Start();
                monitors.Add( monitor );
            }

            Console.WriteLine( "Press the Enter key to exit." );
            Console.ReadLine();
        }

        private static void Monitor_EndpointChecked( EndpointCheckedEventArgs args )
        {
            int numericStatusCode = (int)args.Status.StatusCode;
            if ( numericStatusCode < 200 || numericStatusCode >= 300 )
            {
                Console.WriteLine( "Endpoint is down: {0}", args.Endpoint.Url );
            }
            else
            {
                Console.WriteLine( "All is well with {0}.", args.Endpoint.Url );
            }
        }
    }
}
