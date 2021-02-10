using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Test.PostJsonRequest
{
    public class Fields
    {
        public string id { get; set; }
        public string Summary { get; set; }
        public string Message { get; set; }
        public DateTime receivedAt { get; set; }
    }

    public class Record
    {
        public Fields fields { get; set; }
    }

    public class Root
    {
        public List<Record> records { get; set; }
    }
}
