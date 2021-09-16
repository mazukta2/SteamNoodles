using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Common
{
    public class UnitySprite : ISprite
    {
        public Sprite Sprite { get; private set; }

        public UnitySprite(Sprite sprite)
        {
            Sprite = sprite;
        }
    }
}
