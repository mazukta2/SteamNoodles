using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Languages;
using Game.Assets.Scripts.Game.Logic.Events.Game;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        //private Dictionary<string, string> _currentLanguage = new Dictionary<string, string>();
        //private string _currentLanguageName;
        //private IGameDefinitions _definitions;
        private LanguagePack _currentLanguage;
        private Dictionary<string, LanguagePack> _languages = new Dictionary<string, LanguagePack>();

        public LocalizationManager(LanguagePack language) : this(new List<LanguagePack>() { language }, language.Name)
        {

        }

        public LocalizationManager(List<LanguagePack> languages, string defaultLanguage)
        {
            foreach (var item in languages)
            {
                _languages.Add(item.Name, item);
            }

            LoadLanguage(defaultLanguage);
        }

        public LocalizationManager(IDefinitions definitions, string defaultLanguage)
        {
            var defs = definitions.GetList<LanguageDefinition>();
            foreach (var item in defs)
            {
                _languages.Add(item.Name, item.Create());
            }

            LoadLanguage(defaultLanguage);
        }

        private void LoadLanguage(string name)
        {
            _currentLanguage = _languages[name];
            //_currentLanguage.Clear();

            new OnLanguageChanged().Fire();
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
           
            var str = _currentLanguage.Get(key);
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
            return _currentLanguage.Name;
        }

        public void ChangeLanguage(string name)
        {
            LoadLanguage(name);
        }
    }
}
