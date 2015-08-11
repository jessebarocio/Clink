using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink
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

    class ConsoleLogger : ILogger
    {
        public void Fatal( string message, Exception e = null )
        {
            Console.WriteLine( message );
            if ( e != null )
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public void Fatal( string format, params object[] args )
        {
            Console.WriteLine( format, args );
        }

        public void Error( string message, Exception e = null )
        {
            Console.WriteLine( message );
            if ( e != null )
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public void Error( string format, params object[] args )
        {
            Console.WriteLine( format, args );
        }

        public void Warn( string message, Exception e = null )
        {
            Console.WriteLine( message );
            if ( e != null )
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public void Warn( string format, params object[] args )
        {
            Console.WriteLine( format, args );
        }

        public void Info( string message, Exception e = null )
        {
            Console.WriteLine( message );
            if ( e != null )
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public void Info( string format, params object[] args )
        {
            Console.WriteLine( format, args );
        }

        public void Debug( string message, Exception e = null )
        {
            Console.WriteLine( message );
            if ( e != null )
            {
                Console.WriteLine( e.Message );
                Console.WriteLine( e.StackTrace );
            }
        }

        public void Debug( string format, params object[] args )
        {
            Console.WriteLine( format, args );
        }
    }
}
