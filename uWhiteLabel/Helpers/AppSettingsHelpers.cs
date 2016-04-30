using System.Configuration;

namespace uWhiteLabel.Helpers
{
    public static class AppSettingsHelper
    {
        #region helpers
        public static void CreateAppSettingsKey(string key, string value)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection AppSettings = (AppSettingsSection)config.GetSection("appSettings");

            AppSettings.Settings.Remove(key);

            AppSettings.Settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);
        }

        public static void RemoveAppSettingsKey(string key)
        {
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection AppSettings = (AppSettingsSection)config.GetSection("appSettings");

            AppSettings.Settings.Remove(key);

            config.Save(ConfigurationSaveMode.Modified);
        }
        #endregion
    }
}
