using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public class Coins
    {
        public event Action OnChanged = delegate { };

        public int Value { get; private set; }

        public void Change(int value)
        {
            Value += value;
            if (Value < 0)
                Value = 0;

            OnChanged();
        }

        internal void Change(object baseCoins)
        {
            throw new NotImplementedException();
        }
    }
}
