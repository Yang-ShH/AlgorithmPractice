using Microsoft.Extensions.Configuration;

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
            Application.Run(new Form1());
        }

        public static T GetConfig<T>(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configurationRoot = builder.Build();
            var value = configurationRoot.GetValue<T>(key);
            return value;
        }
    }
}