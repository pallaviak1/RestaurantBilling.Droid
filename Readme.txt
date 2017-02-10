Xamarin android application using MVVMCross:-
_____________________________________________

This solution is working copy for android project and core library, not written for ios and universal app. 
This is MVVMCross sample referred url is :
https://sites.google.com/site/netdeveloperblog/xamarin/mvvmcross/learn/part1 

Explored thinghs in this application :-
- activities
- sqlite connection 
            (sqlite connection in mvvm structure)
- mvvmcross in xamarin
- portable class library for core functionality
- DialogService 
              (This sample also has dialogservice implementation, showing dialog is in UI specific project however interface is present in core, core is calling show dialog in viewmodel.)
- Messaging 
              (communication in between view models, added plugin in core and droid project - MVVMCross.plugin.messeging)
- show web page in browser using browser plugin
- Localization using resx file
_____________________________________________






____________ Localization ways _____________
Option to translate the app
1. .json files
2. .resx files  - explored - MvxLanguageBinder available in MVVMCross, <TextView local: MvxLang = "Text val, Source=TextSource" in android,
                  
              
3. own implementation (xml)



______________________________________________Extra, some platform specific comments________________________________________________________


sqlite code in fileaccess helper in ios and other projects


In RestaurantBilling.UI.iOS, create a new class called FileAccessHelper with the following code
using System;

namespace RestaurantBilling.UI.iOS
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            // Use the SpecialFolder enum to get the Personal folder on the iOS file system.
            // Then get or create the Library folder within this personal folder.
            // Storing the database here is a best practice.
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = System.IO.Path.Combine(docFolder, "..", "Library");

            if(!System.IO.Directory.Exists(libFolder)) {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            return System.IO.Path.Combine(libFolder, filename);
        }
    }
}
In RestaurantBilling.UI.UWP, create a new class called FileAccessHelper with the following code
namespace RestaurantBilling.UI.UWP
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            // For UWP, we store the database file in our application data's local folder.
            var path = global::Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(path, filename);
        }
    }
}
In RestaurantBilling.UI.WPF, create a new class called FileAccessHelper with the following code
using System;
using System.Reflection;

namespace RestaurantBilling.UI.WPF
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            // In WPF, we have more options for storing the database file.
            // In fact, we can actually store the database file in any folder the user has permissions to access.
            // Here we store the database in the user's local app data folder.
            // This is a recommended location to store application data that users don't interact with directly.

            // Get the title of this application (this is set in our AssemblyInfo.cs file).
            var assembly = Assembly.GetEntryAssembly();
             var titleAttribute =  (AssemblyTitleAttribute)Assembly.GetEntryAssembly().GetCustomAttribute(typeof(AssemblyTitleAttribute));

            // Get the Users/UserName/AppData/Local path and combine it with this application's title.
            var localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var fullPath = System.IO.Path.Combine(localAppDataPath, titleAttribute.Title);

            // .Net 4.5 and later - no need to check if the directory exists first.
            // https://msdn.microsoft.com/en-us/library/54a0at6s%28v=vs.110%29.aspx
            System.IO.Directory.CreateDirectory(fullPath);

            return System.IO.Path.Combine(fullPath, filename);
        }
    }
}
