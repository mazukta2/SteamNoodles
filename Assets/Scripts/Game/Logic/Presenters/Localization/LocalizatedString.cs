using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedString : Disposable, ILocalizatedString
    {
        private IText _text;
        private string _tag;
        private ILocalizationManager _localization;
        private object[] _args;

        public LocalizatedString(IText text, string tag, params object[] args)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _tag = tag ?? throw new ArgumentNullException(nameof(tag));
            _localization = ILocalizationManager.Default ?? throw new ArgumentNullException(nameof(ILocalizationManager.Default));

            UpdateText();
            _localization.OnChangeLanguage += UpdateText;
        }

        public LocalizatedString(ILocalizationManager manager, IText text, string tag, params object[] args) : this(text, tag, args)
        {
            _localization = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        protected override void DisposeInner()
        {
            _localization.OnChangeLanguage -= UpdateText;
        }

        public string Get()
        {
            return _localization.Get(_tag, _args);
        }

        private void UpdateText()
        {
            _text.Value = Get();
        }

    }
}
