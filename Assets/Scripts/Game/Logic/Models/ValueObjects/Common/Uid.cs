using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions
{
    public record Uid
    {
        public long Value { get; }

        private static long _lastId = 1;

        public Uid()
        {
            Value = _lastId++;
        }

        public Uid(long id)
        {
            if (id < 0) throw new ArgumentException(nameof(id));

            Value = id;
        }
    }
}
