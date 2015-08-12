using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Reporters
{
    class InMemoryReporterRepository : IReporterRepository
    {
        public IEnumerable<IReporter> GetConfiguredReporters()
        {
            return new List<IReporter>()
            {
                new ConsoleReporter()
            };
        }
    }
}
