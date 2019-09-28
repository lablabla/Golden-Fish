using Golden.Fish.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Golden.Fish.Desktop.ValueConverters
{
    /// <summary>
    /// Converts the <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public static class ApplicationPageHelpers
    {
        /// <summary>
        /// Takes a <see cref="ApplicationPage"/> and a view model, if any, and creates the desired page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public static BasePage ToBasePage(this ApplicationPage page, object viewModel = null)
        {
            // Find the appropriate page
            switch (page)
            {
                case ApplicationPage.Main:
                    return new MainPage(viewModel as MainViewModel);

                //case ApplicationPage.ValvesSetup:
                //    return new ValvesSettingPage(viewModel as ValveSettingViewModel);

                default:
                    Debugger.Break();
                    return null;
            }
        }

        /// <summary>
        /// Converts a <see cref="BasePage"/> to the specific <see cref="ApplicationPage"/> that is for that type of page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static ApplicationPage ToApplicationPage(this BasePage page)
        {
            // Find application page that matches the base page
            if (page is MainPage)
            {
                return ApplicationPage.Main;
            }

            //if (page is ValveSettingPage)
            //{
            //    return ApplicationPage.ValvesSetup;
            //}

            // Alert developer of issue
            Debugger.Break();
            return default(ApplicationPage);
        }
    }
}
