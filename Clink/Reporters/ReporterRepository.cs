using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Reporters
{
    class ReporterRepository : IReporterRepository
    {
        string reporterConfigFile;

        public ReporterRepository() : this( "Reporters.json" )
        { }

        public ReporterRepository( string reporterConfig )
        {
            reporterConfigFile = reporterConfig;
        }

        public IEnumerable<IReporter> GetConfiguredReporters()
        {
            return JsonConvert.DeserializeObject<IEnumerable<IReporter>>( File.ReadAllText( reporterConfigFile ), new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects
            } );
        }
    }
}
