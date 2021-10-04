using Game.Assets.Scripts.Game.Logic.Views.Common;
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

        public UnityView(IVisual ivisual)
        {
            View = ((UnityView)ivisual).View;
        }
    }
}
