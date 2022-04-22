using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedJoinString : ILocalizatedString
    {
        private string _separator;
        private ILocalizatedString[] _strings;
        private ILocalizationManager _localization;

        public LocalizatedJoinString(string separator, params ILocalizatedString[] strings)
        {
            _separator = separator ?? throw new ArgumentNullException(nameof(separator));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
            _localization = ILocalizationManager.Default ?? throw new ArgumentNullException(nameof(ILocalizationManager.Default));
        }

        public LocalizatedJoinString(ILocalizationManager manager, string separator, params ILocalizatedString[] strings) 
            : this(separator, strings)
        {
            _localization = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public string Get()
        {
            return string.Join(_separator, _strings.Select(l => l.Get()));
        }
    }
}
