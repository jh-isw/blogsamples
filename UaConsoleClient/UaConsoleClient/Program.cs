using System;

namespace UaConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MyUaConsoleClient(true);
            client.Run();
        }
    }
}
