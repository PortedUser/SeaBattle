using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopGame.Core
{
    class ConfigManager
    {
        public ConfigManager() { }
        public string GetConfig(string key)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? throw new Exception("Не найден config");
                return result;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка при чтении config файла: {e}");
                throw e;
            }
        }

    }
}
