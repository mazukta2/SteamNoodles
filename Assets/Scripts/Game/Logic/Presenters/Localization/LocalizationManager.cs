using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        public event Action OnChangeLanguage = delegate { };
        private Dictionary<string, string> _currentLanguage = new Dictionary<string, string>();
        private string _currentLanguageName;
        private IDefinitions _definitions;

        public LocalizationManager(IDefinitions definitions, string defaultLanguage)
        {
            _currentLanguageName = defaultLanguage ?? throw new ArgumentNullException(nameof(defaultLanguage));
            _definitions = definitions ?? throw new ArgumentNullException(nameof(definitions));
            LoadLanguage();
        }

        private void LoadLanguage()
        {
            var defs = _definitions.Get<LanguageDefinition>($"{_currentLanguageName}");
            _currentLanguage.Clear();
            foreach (var item in defs.Values)
                _currentLanguage.Add(item.Key, item.Value);

            OnChangeLanguage();
        }

        public string Get(string key, params object[] args)
        {
            if (key == null)
                throw new Exception("Key is null");

            if (!_currentLanguage.ContainsKey(key))
            {
                throw new Exception($"Key {key} is not exist in {GetCurrentLanguage()} language json");
            }

            var preparedArgs = new List<object>();
            if (args != null)
            {
                foreach (var item in args)
                {
                    var locStr = (item as ILocalizatedString);
                    if (locStr != null)
                        preparedArgs.Add(locStr.Get());
                    else
                        preparedArgs.Add(item);
                }
            }
           
            var str = _currentLanguage[key];
            var prms = preparedArgs.ToArray();

            try
            {
                return string.Format(str, prms);
            }
            catch
            {
                throw new Exception($"Cant parse key {key} in {GetCurrentLanguage()} language json. Params used incorrectly or there are more of them that code provide.");
            }
        }

        public string GetCurrentLanguage()
        {
            return _currentLanguageName;
        }
    }
}
