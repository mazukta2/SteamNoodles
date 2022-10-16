using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Events;
using Game.Assets.Scripts.Game.Logic.Events.Game;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LocalizatedText : Disposable
    {
        private readonly IEvents _events;
        private IText _text;
        private ILocalizatedString _localizatedString;
        private Subscription<OnLanguageChanged> _languageChangedSubscription;

        public LocalizatedText(IText text, ILocalizatedString localizatedString) : this(IEvents.Default, text, localizatedString)
        {
        }

        public LocalizatedText(IEvents events, IText text, ILocalizatedString localizatedString)
        {
            _events = events ?? throw new ArgumentNullException(nameof(events));
            _text = text ?? throw new ArgumentNullException(nameof(text));
            _localizatedString = localizatedString ?? throw new ArgumentNullException(nameof(localizatedString));

            UpdateText();

            _languageChangedSubscription = new Subscription<OnLanguageChanged>(_events, UpdateText);
        }

        protected override void DisposeInner()
        {
            _languageChangedSubscription.Dispose();
        }

        private void UpdateText()
        {
            _text.Value = _localizatedString.Get();
        }
    }
}
