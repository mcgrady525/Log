using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Log.Entity.RabbitMQ
{
    /// <summary>
    /// add xml日志和性能日志request
    /// </summary>
    public class AddXmlPerformanceLogRequest
    {
        public AddXmlLogRequest XmlLog { get; set; }

        public AddPerformanceLogRequest PerformanceLog { get; set; }

    }
}
