using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    internal interface IEndpointRepository
    {
        IEnumerable<Endpoint> GetAll();
    }

    internal class EndpointRepository : IEndpointRepository
    {
        public IEnumerable<Endpoint> GetAll()
        {
            return new List<Endpoint>()
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
                }
            };
        }
    }
}
