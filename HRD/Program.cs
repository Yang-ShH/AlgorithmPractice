using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HRD
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            ApplicationConfiguration.Initialize();
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.Run(new Form1());
        }

        public static MapJson GetConfig()
        {
            //var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json",false, true);
            //IConfigurationRoot configurationRoot = builder.Build();
            //var value = configurationRoot.GetValue<T>(key);
            //return value;

            StreamReader reader = File.OpenText("appsettings.json");
            JsonTextReader jsonTextReader = new JsonTextReader(reader);
            var mapObj = JToken.ReadFrom(jsonTextReader).ToObject<MapJson>();
            return mapObj;
        }
    }
}