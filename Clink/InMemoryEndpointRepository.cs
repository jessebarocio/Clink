using System;
using System.Collections.Generic;
using System.Linq;

namespace Clink
{
    internal class InMemoryEndpointRepository : IEndpointRepository
    {
        private List<Endpoint> endpoints;


        public InMemoryEndpointRepository()
        {
            endpoints = new List<Endpoint>()
                {
                    new Endpoint()
                    {
                        Url = "http://barocio.net",
                        Interval = 5000
                    },
                    new Endpoint()
                    {
                        Url = "http://bmisw.com",
                        Interval = 3000
                    },
                    new Endpoint()
                    {
                        Url = "http://google.com",
                        Interval = 8000
                    },
                    new Endpoint()
                    {
                        Url = "http://whitehouse.gov",
                        Interval = 10000
                    },
                    new Endpoint()
                    {
                        Url = "http://localhost:60591/rest/default",
                        Interval = 2000
                    }
                };
        }


        public IEnumerable<Endpoint> GetAll()
        {
            return endpoints;
        }

        public Endpoint Get( string url )
        {
            return endpoints.SingleOrDefault( e => e.Url == url );
        }

        public void SaveChanges()
        {
            // Do nothing because it's all in memory!
        }
    }
}
