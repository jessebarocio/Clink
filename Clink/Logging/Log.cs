using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Logging
{
    static class Log
    {
        public static ILogger GetLogger( string name )
        {
            return new Log4NetLogger( name );
        }

        public static ILogger GetLogger( Type type )
        {
            return GetLogger( type.ToString() );
        }
    }
}
