using System;
using System.Collections.Generic;
using System.Text;
using LogProxy.Test.PostJsonRequest;

namespace LogProxy.Test.Sample
{
    public class SamplePostData
    {
        public static Root Get()
        {
            var sample = new Root()
            {
                records = new List<Record>
                {
                    new Record(){ fields = new Fields(){ id = "1", Summary = "This My Test Summery1", Message = "Exceptiion occured at ...", receivedAt = DateTime.UtcNow} },
                    new Record(){ fields = new Fields(){ id = "2", Summary = "This My Test Summery2", Message = "Exceptiion occured at ...", receivedAt = DateTime.UtcNow} },
                    new Record(){ fields = new Fields(){ id = "3", Summary = "This My Test Summery3", Message = "Exceptiion occured at ...", receivedAt = DateTime.UtcNow} }
                }
            };
            return sample;
        }
    }
}
