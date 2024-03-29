﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;

namespace Evernote
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string DbName = "EvernoteDatabase";

        private static string _connectionString;

        public static string UserId { get; set; } = string.Empty;

        public static IConfiguration Configuration { get; private set; }

        public static string ConnectionString
        {
            get
            {
                if (Configuration == null)
                {
                    BuildConfiguration();
                }

                return _connectionString;
            }

            set => _connectionString = value;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            BuildConfiguration();
        }

        private static void BuildConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            ConnectionString = Configuration.GetConnectionString(DbName);
        }
    }
}
