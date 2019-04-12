using System;
using System.Collections.Generic;
using logic;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MyUaClient.Greet());
            Console.WriteLine("Type the URL to use and press Enter.");
            System.Console.WriteLine("(default if empty: opc.tcp://localhost:4840): ");
            var serverUrl = Console.ReadLine();
            if(serverUrl == String.Empty){
                serverUrl = "opc.tcp://localhost:4840";
            }
            Console.WriteLine($"Server URL: {serverUrl}");

            Uri myUri;
            try{
                myUri = new Uri(serverUrl);
            }
            catch(UriFormatException e){
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("You entered an invalid URL. Exiting.");
                return;
            }
            
            var lst = MyUaClient.GetEndpoints(myUri);

            int i = 0;
            foreach(var endpoint in lst){
                
                System.Console.WriteLine(@"Endpoint #{0}", i);
                    
                System.Console.WriteLine("\t{0}", endpoint[0]);
                System.Console.WriteLine("\t{0}", endpoint[1]);
                System.Console.WriteLine("\t{0}", endpoint[2]);
            
                i++;
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
