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
        Endpoint Get( string url );
        void SaveChanges();
    }
}