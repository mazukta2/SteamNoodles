using System;

namespace Game.Assets.Scripts.Game.Logic.ValueObjects.Localization
{
    public record LocalizationTag
    {
        public static readonly LocalizationTag None = new LocalizationTag("NO_TAG");

        public LocalizationTag(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value));
            Value = value;
        }

        public string Value { get; }
    }
}
