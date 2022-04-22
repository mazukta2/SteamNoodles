using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
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
