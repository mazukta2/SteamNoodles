using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedFormatString : ILocalizatedString
    {
        private ILocalizationManager _localization;

        private string _format;
        private object[] _params;

        public LocalizatedFormatString(string format, params object[] paramList)
        {
            _format = format;
            _params = paramList;
            _localization = ILocalizationManager.Default ?? throw new ArgumentNullException(nameof(ILocalizationManager.Default));
        }

        public LocalizatedFormatString(ILocalizationManager manager, string format, params object[] paramList)
            : this(format, paramList)
        {
            _localization = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        public string Get()
        {
            var list = new List<object>();
            foreach (var item in _params)
            {
                var str = item as ILocalizatedString;
                if (str != null)
                    list.Add(str.Get());
                else
                    list.Add(item);
            }

            return string.Format(_format, list.ToArray());
        }
    }
}
