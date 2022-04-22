using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedText : Disposable
    {
        private IText _text;
        private ILocalizatedString _localizatedString;
        private ILocalizationManager _localization;

        public LocalizatedText(IText text, ILocalizatedString localizatedString)
        {
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _localizatedString = localizatedString ?? throw new ArgumentNullException(nameof(localizatedString));
            _localization = ILocalizationManager.Default ?? throw new ArgumentNullException(nameof(ILocalizationManager.Default));

            UpdateText();
            _localization.OnChangeLanguage += UpdateText;
        }

        public LocalizatedText(ILocalizationManager manager, IText text, ILocalizatedString localizatedString) : this(text, localizatedString)
        {
            _localization = manager ?? throw new ArgumentNullException(nameof(manager));
        }

        protected override void DisposeInner()
        {
            _localization.OnChangeLanguage -= UpdateText;
        }

        private void UpdateText()
        {
            _text.Value = _localizatedString.Get();
        }
    }
}
