using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    interface IHttpService
    {
        Response Request( string url );
    }

    public class Response
    {
        public HttpStatusCode StatusCode { get; set; }
    }

    class HttpService : IHttpService
    {
        public Response Request( string url )
        {
            RestClient rc = new RestClient( url );
            var request = new RestRequest( "", Method.HEAD );
            var response = rc.Execute( request );
            return new Response()
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
