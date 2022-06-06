using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Common.Core
{
    public class RecordList<T> : List<T>
    {
        public override bool Equals(object obj)
        {
            if (obj is IEnumerable<T> other)
            {
                return Enumerable.SequenceEqual(this, other);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
