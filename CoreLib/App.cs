﻿using CoreLib.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Localization;
using MvvmCross.Platform;
using RestaurantBilling.Core.Interfaces;
using RestaurantBilling.Core.Services;
using RestaurantBilling.Localization;

namespace RestaurantBilling.Core
{
    /// <summary>
    /// Main App class inherits from MvxApplication
    /// - Registers the interfaces and implementations the app uses.
    /// - Registers which ViewModel the App will show when it starts.
    /// - Controls how ViewModels are locate, most solutions will use default implementation supplied by MvxApplication.
    /// </summary>
    public class App : MvxApplication
    {

        /// <summary>
        /// Setup IoC registrations.
        /// </summary>
        public App(System.Globalization.CultureInfo currentCulture)
        {
            // Whenever Mvx.Resolve is used, a new instance of Calculation is provided.
            Mvx.RegisterType<IBillCalculator, BillCalculator>();
            var calcExample = Mvx.Resolve<IBillCalculator>();

            // Tells the MvvmCross framework that whenever any code requests an IMvxAppStart reference,
            // the framework should return that same appStart instance.
            var appStart = new CustomAppStart();
            Mvx.RegisterSingleton<IMvxAppStart>(appStart);

            MvvmCross.Plugins.ResxLocalization.PluginLoader.Instance.EnsureLoaded();

            // Another option is to utilize a default App Start object with 
            // var appStart = new MvxAppStart<TipViewModel>();
            Mvx.RegisterSingleton<IMvxTextProvider>
              (new ResxTextProvider(Strings.ResourceManager, currentCulture));

           
     
        }
    }
}
