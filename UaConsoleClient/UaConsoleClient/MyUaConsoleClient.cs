using Opc.Ua;
using Opc.Ua.Configuration;
using System;
using System.Threading.Tasks;

namespace UaConsoleClient
{
    public class MyUaConsoleClient
    {
        static bool autoAccept = false;

        public MyUaConsoleClient(bool _autoAccept)
        {
            autoAccept = _autoAccept;
        }
        
        public void Run()
        {
            try
            {
                MainTask().Wait();
            }
            catch (Exception ex)
            {
                Utils.Trace("ServiceResultException:" + ex.Message);
                Console.WriteLine("Exception: {0}", ex.Message);
                return;
            }
        }

        private async Task MainTask()
        {
            ApplicationInstance application = new ApplicationInstance
            {
                ApplicationName = "UA Console Client",
                ApplicationType = ApplicationType.Client,
                ConfigSectionName = "UaConsoleClient"
            };
            
            // load the application configuration.
            ApplicationConfiguration config = await application.LoadApplicationConfiguration(false);

            // check the application certificate.
            bool haveAppCertificate = await application.CheckApplicationInstanceCertificate(false, 0);
            if (!haveAppCertificate)
            {
                throw new Exception("Application instance certificate invalid!");
            }

            if (haveAppCertificate)
            {
                config.ApplicationUri = Utils.GetApplicationUriFromCertificate(config.SecurityConfiguration.ApplicationCertificate.Certificate);
                if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
                {
                    autoAccept = true;
                }
                config.CertificateValidator.CertificateValidation += new CertificateValidationEventHandler(CertificateValidator_CertificateValidation);
            }
            else
            {
                Console.WriteLine("    WARN: missing application certificate, using unsecure connection.");
            }

            using (DiscoveryClient client = DiscoveryClient.Create(new Uri("opc.tcp://localhost:4840")))
            {
                EndpointDescriptionCollection endpoints = client.GetEndpoints(null);
 
                for (int ii = 0; ii < endpoints.Count; ii++)
                {
                    Console.WriteLine("Endpoint #{0}:", ii);
                    Console.WriteLine(endpoints[ii].EndpointUrl);
                    Console.WriteLine(endpoints[ii].SecurityPolicyUri.Substring(
                        endpoints[ii].SecurityPolicyUri.LastIndexOf('#')+1));
                    Console.WriteLine(endpoints[ii].SecurityMode.ToString()); // [None, Sign, SignAndEncrypt]
                }
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void CertificateValidator_CertificateValidation(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            if (e.Error.StatusCode == StatusCodes.BadCertificateUntrusted)
            {
                e.Accept = autoAccept;
                if (autoAccept)
                {
                    Console.WriteLine("Accepted Certificate: {0}", e.Certificate.Subject);
                }
                else
                {
                    Console.WriteLine("Rejected Certificate: {0}", e.Certificate.Subject);
                }
            }
        }
    }
}
