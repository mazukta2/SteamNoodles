using System;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record LocalizationTag
    {
        public LocalizationTag(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value));
            Value = value;
        }

        public string Value { get; }
    }
}
