using System;
using System.Collections.Generic;
using System.Text;

namespace LogProxy.Test.GetJsonResponse
{
    public class Fields
    {
        public string Summary { get; set; }
        public string Message { get; set; }
    }

    public class Record
    {
        public string id { get; set; }
        public Fields fields { get; set; }
        public DateTime createdTime { get; set; }
    }

    public class Root
    {
        public List<Record> records { get; set; }
    }
}
