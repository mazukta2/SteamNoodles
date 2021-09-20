using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Common
{
    public class UnityView : IVisual
    {
        public GameObject View { get; private set; }

        public UnityView(GameObject go)
        {
            View = go;
        }
    }
}
