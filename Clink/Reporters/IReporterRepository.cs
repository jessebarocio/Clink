using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clink.Reporters
{
    interface IReporterRepository
    {
        IEnumerable<IReporter> GetConfiguredReporters();
    }
}
