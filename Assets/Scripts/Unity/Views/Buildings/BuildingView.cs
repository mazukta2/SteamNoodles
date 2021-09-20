using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace GameUnity.Assets.Scripts.Unity.Views.Buildings
{
    public class BuildingView : GameMonoBehaviour, IConstructionView
    {
        private UnityView _image;

        public IVisual GetImage()
        {
            return _image;
        }

        public void SetImage(IVisual image)
        {
            _image = (UnityView)image;
            UpdateView();
        }

        public void SetPosition(Vector2 pos)
        {
            var npos = pos.ToUnityVector();
            transform.position = new UnityEngine.Vector3(npos.x, npos.y, transform.position.z);
        }
        public Vector2 GetPosition()
        {
            return transform.position.ToLogicVector();
        }

        private void UpdateView()
        {
            Instantiate(_image.View, transform, false);
        }

    }
}
