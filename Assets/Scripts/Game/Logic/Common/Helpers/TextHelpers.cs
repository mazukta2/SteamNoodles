using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Common.Helpers
{
    public static class TextHelpers
    {
        public static string GetSignedNumber(this int value)
        {
            return value.ToString("+#;-#;0");
        }
    }
}
