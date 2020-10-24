
using Microsoft.ML;
using Microsoft.ML.Trainers.LightGbm;
using Newtonsoft.Json;

namespace ImotiPrediction
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.ML;
    using Microsoft.ML.Trainers.LightGbm;
    using Microsoft.AspNetCore.Hosting;

    using Newtonsoft.Json;
    using ImotiPrediction.ViewModels;

    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }

}
