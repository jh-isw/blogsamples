using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;

namespace logic
{
    public class MyUaClient
    {
        public static string Greet() => "MyUaClient Demonstrator Class";

        public static List<List<string>> GetEndpoints(Uri uri){
            using (DiscoveryClient client = DiscoveryClient.Create(uri))
            {
                var lst = new List<List<string>>();

                EndpointDescriptionCollection endpoints = client.GetEndpoints(null);

                if(endpoints.Count > 0){
                    StringBuilder[] arrStrBuilder = new StringBuilder[endpoints.Count];
                    List<string>[] arrList = new List<string>[endpoints.Count];

                    for (int ii = 0; ii < endpoints.Count; ii++)
                    {
                        arrList[ii] = new List<string>();
                        arrList[ii].Add(endpoints[ii].EndpointUrl);
                        arrList[ii].Add(endpoints[ii].SecurityPolicyUri.Substring(
                            endpoints[ii].SecurityPolicyUri.LastIndexOf('#')+1));
                        arrList[ii].Add(endpoints[ii].SecurityMode.ToString()); // [None, Sign, SignAndEncrypt]
                        lst.Add(arrList[ii]);
                    }
                }

                return lst;
            }

        }
    }
}
