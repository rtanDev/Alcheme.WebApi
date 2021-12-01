using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Core
{
    public class AlchemeDbConfig
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string DocumentsCollectionName { get; set; }

    }
}
