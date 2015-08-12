using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Logging
{
    interface ILogger
    {
        void Fatal( string message, Exception e = null );
        void Fatal( string format, params object[] args );
        void Error( string message, Exception e = null );
        void Error( string format, params object[] args );
        void Warn( string message, Exception e = null );
        void Warn( string format, params object[] args );
        void Info( string message, Exception e = null );
        void Info( string format, params object[] args );
        void Debug( string message, Exception e = null );
        void Debug( string format, params object[] args );
    }
}