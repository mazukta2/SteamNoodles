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

        public static string Style(this string text, TextStyles style)
        {
            if (style == TextStyles.Highlight)
                return $"<style=\"Highlight\">{text}</style>";

            if (style == TextStyles.HeavyHighlight)
                return $"<style=\"HeavyHighlight\">{text}</style>";

            return text;
        }

        public enum TextStyles
        {
            None,
            Highlight,
            HeavyHighlight,
        }
    }
}
