using ScrapeSpringfield.Jobs;
using ScrapeSpringfield.Models;
using System;

namespace ScrapeSpringfield
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = new Configuration(args);
                var job = new ScrapeJob(config);

                job.ScrapeAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(new string('-', 30));
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("Done....");
                Console.ReadLine();
            }
        }
    }
}