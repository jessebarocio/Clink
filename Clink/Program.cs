using Clink.Logging;
using Clink.Reporters;
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
        static IReporterRepository reporterRepo = new ReporterRepository();
        static readonly ILogger log = Log.GetLogger( typeof( Program ) );

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
                Timestamp = args.Timestamp,
                StatusCode = args.StatusCode
            };
            var endpoint = endpointRepo.Get( args.Url );

            if ( StatusShouldBeReported( endpoint.LastStatus, newStatus ) )
            {
                ReportStatus( endpoint, newStatus );
            }

            endpoint.Statuses.Add( newStatus );
            endpointRepo.SaveChanges();
        }

        private static void ReportStatus( Endpoint endpoint, EndpointStatus newStatus )
        {
            foreach ( var reporter in reporterRepo.GetConfiguredReporters() )
            {
                reporter.Report( endpoint, newStatus );
            }
        }

        private static bool StatusShouldBeReported( EndpointStatus lastStatus, EndpointStatus newStatus )
        {
            if ( lastStatus != null )
            {
                // If the new status is different from the last status then it should be reported.
                if ( lastStatus.Status != newStatus.Status )
                {
                    return true;
                }
            }
            else // If the endpoint has never been checked before but is down then it should be reported.
            {
                if ( newStatus.Status == Status.Down )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
