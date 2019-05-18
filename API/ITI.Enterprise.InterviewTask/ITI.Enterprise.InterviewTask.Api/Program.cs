using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ITI.Enterprise.InterviewTask.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel().UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration()
                .UseStartup<Startup>().Build();
    }
}
