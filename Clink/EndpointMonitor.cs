using Clink.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Clink
{
    class EndpointCheckedEventArgs
    {
        public string Url
        { get; set; }
        public DateTime Timestamp
        { get; set; }
        public HttpStatusCode StatusCode
        { get; set; }
    }

    delegate void EndpointCheckedHandler( EndpointCheckedEventArgs args );

    class EndpointMonitor : IDisposable
    {
        private Timer timer;
        private readonly double interval;
        private readonly string url;
        private readonly IHttpService httpService;
        private readonly ILogger logger;


        public event EndpointCheckedHandler EndpointChecked;


        public EndpointMonitor( string endpointUrl, double intervalInMilliseconds )
            : this( endpointUrl, intervalInMilliseconds, new HttpService(), Log.GetLogger( typeof( EndpointMonitor ) ) )
        { }

        internal EndpointMonitor( string endpointUrl, double intervalInMilliseconds, IHttpService http, ILogger log )
        {
            url = endpointUrl;
            interval = intervalInMilliseconds;
            httpService = http;
            logger = log;
            timer = new Timer( interval )
            {
                AutoReset = false // The timer will only fire once and must be restarted.
            };
            timer.Elapsed += Timer_Elapsed;
        }


        public void Start()
        {
            timer.Start();
            logger.Info( "Begin monitoring endpoint {0}", url );
        }

        private void Timer_Elapsed( object sender, ElapsedEventArgs e )
        {
            logger.Debug( "Checking endpoint {0}", url );
            var response = httpService.Request( url );
            logger.Debug( "Status code: {0}", response.StatusCode );
            if ( EndpointChecked != null )
            {
                EndpointChecked( new EndpointCheckedEventArgs()
                {
                    Url = url,
                    Timestamp = DateTime.Now,
                    StatusCode = response.StatusCode
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
