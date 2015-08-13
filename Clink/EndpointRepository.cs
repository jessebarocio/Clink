using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    class EndpointRepository : IEndpointRepository
    {
        private string endpointConfigFile;

        public EndpointRepository() : this( "Endpoints.json" )
        { }

        public EndpointRepository( string endpointConfig )
        {
            endpointConfigFile = endpointConfig;
        }


        private IEnumerable<Endpoint> endpoints;
        private IEnumerable<Endpoint> Endpoints
        {
            get
            {
                return endpoints ?? ( endpoints = LoadEndpoints() );
            }
        }

        private IEnumerable<Endpoint> LoadEndpoints()
        {
            return JsonConvert.DeserializeObject<IEnumerable<Endpoint>>( File.ReadAllText( endpointConfigFile ),
                new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                } );
        }

        public Endpoint Get( string url )
        {
            return Endpoints.SingleOrDefault( e => e.Url == url );
        }

        public IEnumerable<Endpoint> GetAll()
        {
            return Endpoints;
        }

        public void SaveChanges()
        {
            // Do nothing...
        }
    }
}
