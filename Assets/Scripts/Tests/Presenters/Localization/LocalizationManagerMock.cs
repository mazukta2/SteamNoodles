using System;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Tests.Presenters.Localization
{
    public class LocalizationManagerMock : ILocalizationManager
    {
        public event Action OnChangeLanguage = delegate { };
        public LocalizationManagerMock()
        {
        }

        public string Get(string key, params object[] args)
        {
            return key;
        }
    }
}
