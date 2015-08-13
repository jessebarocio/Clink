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
                new ConsoleReporter(),
                new SmtpReporter()
                {
                    SmtpServer = "smtp.gmail.com",
                    SmtpPort = 587,
                    UseSsl = true,
                    UserName = "notification@bmiassociates.com",
                    Password = "Looney123",
                    FromAddress = "notification@bmiassociates.com",
                    ToAddress = "jesseb@bmisw.com"
                }
            };
        }
    }
}
