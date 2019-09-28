﻿using Dna;
using Golden.Fish.Core.Services;
using Golden.Fish.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Golden.Fish.Desktop
{
    /// <summary>
    /// A shorthand access class to get DI services with nice clean short code
    /// </summary>
    public static class DI
    {

        /// <summary>
        /// A shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel ViewModelApplication => Framework.Service<ApplicationViewModel>();

        /// <summary>
        /// A shortcut to access toe <see cref="IClientDataStore"/> service
        /// </summary>
        public static IClientDataStore ClientDataStore => Framework.Service<IClientDataStore>();
    }
}
