using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Clink
{
    class EndpointCheckedEventArgs
    {
        public Endpoint Endpoint { get; set; }
        public EndpointStatusCheck Status { get; set; }
    }
    
    delegate void EndpointCheckedHandler( EndpointCheckedEventArgs args );

    class EndpointMonitor : IDisposable
    {
        private Timer timer;
        private readonly Endpoint endpoint;
        private readonly IHttpService httpService;
        private readonly ILogger logger;


        public event EndpointCheckedHandler EndpointChecked;


        public EndpointMonitor( Endpoint e )
            : this( e, new HttpService(), new ConsoleLogger() ) { }
            
        internal EndpointMonitor( Endpoint e, IHttpService http, ILogger log )
        {
            endpoint = e;
            httpService = http;
            logger = log;
            timer = new Timer( endpoint.Interval )
            {
                AutoReset = false // The timer will only fire once and must be restarted.
            };
            timer.Elapsed += Timer_Elapsed;
        }
        

        public void Start()
        {
            timer.Start();
            logger.Info( "Begin monitoring endpoint {0}", endpoint.Url );
        }

        private void Timer_Elapsed( object sender, ElapsedEventArgs e )
        {
            logger.Info( "Checking endpoint {0}...", endpoint.Url );
            var response = httpService.Request( endpoint.Url );
            logger.Debug( "Response Code: {0}", response.StatusCode );
            var status = new EndpointStatusCheck()
            {
                CheckTime = DateTime.Now,
                StatusCode = response.StatusCode
            };
            if(EndpointChecked != null)
            {
                EndpointChecked( new EndpointCheckedEventArgs()
                {
                    Endpoint = endpoint,
                    Status = status
                } );
            }
            timer.Start();
        }

        #region IDisposable Implementation
        public void Dispose()
        {
            if ( timer != null )
            {
                timer.Dispose();
                timer = null;
            }
        }
        #endregion
    }
}
