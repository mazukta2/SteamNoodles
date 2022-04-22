using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public interface ILocalizationManager
    {
        event Action OnChangeLanguage;
        string Get(string key, params object[] args);
    }
}
