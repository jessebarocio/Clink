using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    class Program
    {
        static IEndpointRepository endpointRepo = new InMemoryEndpointRepository();

        static void Main( string[] args )
        {
            var monitors = new List<EndpointMonitor>();

            foreach ( var endpoint in endpointRepo.GetAll() )
            {
                var monitor = new EndpointMonitor( endpoint.Url, endpoint.Interval );
                monitor.EndpointChecked += Monitor_EndpointChecked;
                monitor.Start();
                monitors.Add( monitor );
            }

            Console.WriteLine( "Press the Enter key to exit." );
            Console.ReadLine();
        }

        private static void Monitor_EndpointChecked( EndpointCheckedEventArgs args )
        {
            var newStatus = new EndpointStatus()
            {
                CheckTime = args.Timestamp,
                StatusCode = args.StatusCode
            };
            var endpoint = endpointRepo.Get( args.Url );

            if ( StatusShouldBeReported( endpoint.LastStatus, newStatus ) )
            {
                Console.WriteLine( "Status changed. Will report." );
            }

            endpoint.Statuses.Add( newStatus );
            endpointRepo.SaveChanges();
        }

        private static bool StatusShouldBeReported( EndpointStatus lastStatus, EndpointStatus newStatus )
        {
            if ( lastStatus != null )
            {
                // If the new status is different from the last status then it should be reported.
                if ( IsSuccessStatusCode( lastStatus.StatusCode ) != IsSuccessStatusCode( newStatus.StatusCode ) )
                {
                    return true;
                }
            }
            else // If the endpoint has never been checked before but is down then it should be reported.
            {
                if ( !IsSuccessStatusCode( newStatus.StatusCode ) )
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsSuccessStatusCode( HttpStatusCode statusCode )
        {
            int numericStatusCode = (int)statusCode;
            return numericStatusCode < 300 && numericStatusCode >= 200;
        }
    }
}
