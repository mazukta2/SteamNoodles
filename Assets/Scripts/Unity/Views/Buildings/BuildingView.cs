using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace GameUnity.Assets.Scripts.Unity.Views.Buildings
{
    public class BuildingView : GameMonoBehaviour, IConstructionView
    {
        public void SetPosition(Vector2 pos)
        {
            transform.position = new UnityEngine.Vector2(pos.X, pos.Y);
        }
    }
}
