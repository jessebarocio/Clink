using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Logging
{
    class Log4NetLogger : ILogger
    {
        static Log4NetLogger()
        {
            if ( ConfigurationManager.GetSection( "log4net" ) != null )
            {
                XmlConfigurator.Configure();
            }
            else
            {
                BasicConfigurator.Configure( BuildConsoleAppender() );
            }
        }

        ILog log;

        public Log4NetLogger( string loggerName )
        {
            log = log4net.LogManager.GetLogger( loggerName );
        }

        public void Debug( string format, params object[] args )
        {
            log.DebugFormat( format, args );
        }

        public void Debug( string message, Exception e = null )
        {
            log.Debug( message, e );
        }

        public void Error( string format, params object[] args )
        {
            log.ErrorFormat( format, args );
        }

        public void Error( string message, Exception e = null )
        {
            log.Error( message, e );
        }

        public void Fatal( string format, params object[] args )
        {
            log.FatalFormat( format, args );
        }

        public void Fatal( string message, Exception e = null )
        {
            log.Fatal( message, e );
        }

        public void Info( string format, params object[] args )
        {
            log.InfoFormat( format, args );
        }

        public void Info( string message, Exception e = null )
        {
            log.Info( message, e );
        }

        public void Warn( string format, params object[] args )
        {
            log.WarnFormat( format, args );
        }

        public void Warn( string message, Exception e = null )
        {
            log.Warn( message, e );
        }

        private static IAppender BuildConsoleAppender()
        {
            var appender = new ColoredConsoleAppender();
            appender.Layout = new PatternLayout( "%logger% %thread% %message%n" );
            appender.AddFilter( new LevelRangeFilter()
            {
                LevelMin = Level.Debug,
                LevelMax = Level.Fatal
            } );
            appender.AddMapping( new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Fatal,
                ForeColor = ColoredConsoleAppender.Colors.White,
                BackColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity
            } );
            appender.AddMapping( new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Error,
                ForeColor = ColoredConsoleAppender.Colors.Red | ColoredConsoleAppender.Colors.HighIntensity
            } );
            appender.AddMapping( new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Warn,
                ForeColor = ColoredConsoleAppender.Colors.Yellow | ColoredConsoleAppender.Colors.HighIntensity
            } );
            appender.AddMapping( new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Info,
                ForeColor = ColoredConsoleAppender.Colors.White
            } );
            appender.AddMapping( new ColoredConsoleAppender.LevelColors()
            {
                Level = Level.Debug,
                ForeColor = ColoredConsoleAppender.Colors.Green | ColoredConsoleAppender.Colors.HighIntensity
            } );
            appender.ActivateOptions();
            return appender;
        }

    }
}
