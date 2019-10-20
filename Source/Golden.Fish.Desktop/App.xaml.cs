using Dna;
using Golden.Fish.Core.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Golden.Fish.Rational;
using static Dna.FrameworkDI;
using static Golden.Fish.Desktop.DI;
using static Golden.Fish.Core.CoreDI;
using Golden.Fish.Core.Models;

namespace Golden.Fish.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom startup so we load our IoC immediately before anything else
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            // Let the base application do what it needs
            base.OnStartup(e);

            // Setup the main application 
            await ApplicationSetupAsync().ConfigureAwait(false);

            // Load valves and events
            await LoadAppDataAsync();

            // Log it
            Logger.LogDebugSource("Application starting...");
        }

        /// <summary>
        /// Configures our application ready for use
        /// </summary>
        private async Task ApplicationSetupAsync()
        {
            // Setup the Dna Framework
            Framework.Construct<DefaultFrameworkConstruction>()
                .AddFileLogger()
                .AddClientDataStore()
                .AddClientServices()
                .AddViewModels()
                .Build();

            // Ensure the client data store 
            await ClientDataStore.EnsureDataStoreAsync();


            ViewModelApplication.GoToPage(ApplicationPage.Server);

            // Show the main window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
        private async Task LoadAppDataAsync()
        {
            // Load valves settings
            await ClientDataStore.GetValvesAsync().ConfigureAwait(false);

            // Load stored jobs
            await ClientDataStore.GetScheduledJobsAsync();
        }
    }
}
