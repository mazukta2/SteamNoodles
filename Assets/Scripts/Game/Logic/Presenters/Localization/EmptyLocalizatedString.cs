using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class EmptyLocalizatedString : ILocalizatedString
    {
        public EmptyLocalizatedString()
        {

        }

        public string Get()
        {
            return "";
        }


    }
}
