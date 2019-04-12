using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;

namespace logic
{
    public class MyUaClient
    {
        public static string Greet() => "MyUaClient Demonstrator Class";

        // public static string GetEndpoints(string serverUri){
        //     using (DiscoveryClient client = DiscoveryClient.Create(new Uri(serverUri)))
        //     {
        //         EndpointDescriptionCollection endpoints = client.GetEndpoints(null);

        //         var builder = new StringBuilder();
 
        //         for (int ii = 0; ii < endpoints.Count; ii++)
        //         {
        //             builder.AppendFormat("Endpoint #{0}:", ii);
        //             builder.Append(endpoints[ii].EndpointUrl);
        //             builder.Append(endpoints[ii].SecurityPolicyUri.Substring(
        //                 endpoints[ii].SecurityPolicyUri.LastIndexOf('#')+1));
        //             builder.Append(endpoints[ii].SecurityMode.ToString()); // [None, Sign, SignAndEncrypt]
        //         }

        //         return builder.ToString();
        //     }

        // }

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
                        // arrStrBuilder[ii] = new StringBuilder();
                        // arrStrBuilder[ii].Append(endpoints[ii].EndpointUrl + ",");
                        // arrStrBuilder[ii].Append(endpoints[ii].SecurityPolicyUri.Substring(
                        //     endpoints[ii].SecurityPolicyUri.LastIndexOf('#')+1) + ",");
                        // arrStrBuilder[ii].Append(endpoints[ii].SecurityMode.ToString());
                        // lst.Add(arrStrBuilder[ii].ToString());
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
