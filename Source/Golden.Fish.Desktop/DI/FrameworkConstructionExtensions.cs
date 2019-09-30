using Dna;
using Golden.Fish.Core;
using Golden.Fish.Core.Services;
using Golden.Fish.Desktop.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Desktop
{
    public static class FrameworkConstructionExtensions
    {
        /// <summary>
        /// Injects the view models needed for Golden Fish application
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            // Bind to a single instance of Application view model
            construction.Services.AddSingleton<ApplicationViewModel>();

            // TODO: 
            // Bind to a single instance of Settings view model
            //construction.Services.AddSingleton<SettingsViewModel>();

            // Return the construction for chaining
            return construction;
        }
        public static FrameworkConstruction AddClientServices(this FrameworkConstruction construction)
        {

            // Add our task manager
            construction.Services.AddTransient<ITaskManager, BaseTaskManager>();

            // Add our CRON Scheduler
            construction.Services.AddTransient<ICronScheduler, BaseCronScheduler>();

            // Add our Event Scheduler
            construction.Services.AddTransient<IEventScheduler, BaseEventScheduler>();

            // Add our Valve Manager
            construction.Services.AddTransient<IValveManager, BaseValveManager>();

            // Return the construction for chaining
            return construction;
        }
    }
}
