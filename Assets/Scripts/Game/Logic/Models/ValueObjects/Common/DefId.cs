using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common
{
    public record DefId
    {
        public static readonly DefId None = new DefId("NO_ID");

        public string Path { get; }

        public DefId(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException(nameof(id));
            Path = id;
        }
    }
}
