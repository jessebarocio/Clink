﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Clink
{
    class EndpointStatus
    {
        public DateTime CheckTime { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
