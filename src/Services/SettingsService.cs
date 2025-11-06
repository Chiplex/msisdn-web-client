using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSISDNWebClient.Services
{
    public class SettingsService
    {
        private readonly Dictionary<string, string> _settings = new();

        public SettingsService()
        {
            // Inicializar con valores por defecto
            _settings["ApiBaseUrl"] = "https://api.msisdn-web.example.com";
            _settings["Theme"] = "Dark";
        }

        public string? GetSetting(string key)
        {
            return _settings.TryGetValue(key, out var value) ? value : null;
        }

        public void SetSetting(string key, string value)
        {
            _settings[key] = value;
            // TODO: Persistir en Preferences
            Preferences.Set(key, value);
        }

        public async Task<IDictionary<string, string>> GetAllSettingsAsync()
        {
            return await Task.FromResult<IDictionary<string, string>>(_settings);
        }
    }
}