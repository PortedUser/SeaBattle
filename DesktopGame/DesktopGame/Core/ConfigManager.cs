using System;
using System.Configuration;
using System.Windows;

namespace DesktopGame.Core
{
    class ConfigManager
    {
        public ConfigManager() { }
        public string GetConfig(string key)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings[key];
            return result;
            //try
            //{
            //    var appSettings = ConfigurationManager.AppSettings;
            //    string result = appSettings[key];
            //    return result;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show($"Ошибка при чтении config файла: {e}");
            //    throw new Exception($"Ошибка при чтении config файла: {e}");
            //}
        }

    }
}
