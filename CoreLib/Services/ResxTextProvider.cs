using MvvmCross.Localization;
using System.Globalization;
using System.Resources;

namespace CoreLib.Services
{
    public class ResxTextProvider : IMvxTextProvider 
    {
        private readonly ResourceManager _resourceManager;

        public ResxTextProvider(ResourceManager resourceManager, System.Globalization.CultureInfo currentCulture)
        {
            _resourceManager = resourceManager;
            CurrentLanguage = currentCulture;   // new CultureInfo("en-US");  //Thread.CurrentThread.CurrentUICulture;  // 
        }

        public CultureInfo CurrentLanguage { get; set; }

        public string GetText(string namespaceKey, string typeKey, string name)
        {
            string resolvedKey = name;

            if (!string.IsNullOrEmpty(typeKey))
            {
                resolvedKey = $"{typeKey}.{resolvedKey}";
            }

            if (!string.IsNullOrEmpty(namespaceKey))
            {
                resolvedKey = $"{namespaceKey}.{resolvedKey}";
            }

            return _resourceManager.GetString(resolvedKey, CurrentLanguage);
        }

        public string GetText(string namespaceKey, string typeKey, string name, params object[] formatArgs)
        {
            string baseText = GetText(namespaceKey, typeKey, name);
         
            if (string.IsNullOrEmpty(baseText))
            {
                return baseText;
            }

            return string.Format(baseText, formatArgs);
        }
    }
}
