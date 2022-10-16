using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedString : ILocalizatedString
    {
        private string _tag;
        private ILocalizationManager _localization;
        private object[] _args;

        public LocalizatedString(string tag, params object[] args) 
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _localization = ILocalizationManager.Default ?? throw new ArgumentNullException(nameof(ILocalizationManager.Default));

        }

        public LocalizatedString(ILocalizationManager manager, string tag, params object[] args)
        {
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
            _args = args ?? throw new ArgumentNullException(nameof(args));
            _localization = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public string Get()
        {
            return _localization.Get(_tag, _args);
        }


    }
}
