using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public interface ILocalizationManager
    {
        string Get(string key, params object[] args);
        static ILocalizationManager Default { get; set; }
    }
}
