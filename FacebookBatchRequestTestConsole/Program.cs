using System;
using System.Collections.Generic;
using System.Text;
using FacebookBatchRequestLib;

namespace FacebookBatchRequestTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            FacebookBatchRequest FBBatchRequestObj = new FacebookBatchRequest("XYZ|XYZ");
            FBBatchRequestObj.AddBatchUrl(RequestMethod.GET, "something1/events?access_token=XYZ|XYZ&fields=name,description,start_time,end_time,location,id&since=2013-03-08&limit=15");
            FBBatchRequestObj.AddBatchUrl(RequestMethod.GET, "something2/events?access_token=XYZ|XYZ&fields=name,description,start_time,end_time,location,id&since=2013-03-08&limit=15");

            List<string> RetJSONs = FBBatchRequestObj.Send();
        }
    }
}
