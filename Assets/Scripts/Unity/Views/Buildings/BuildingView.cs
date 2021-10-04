using Assets.Scripts.Core;
using Assets.Scripts.Core.Helpers;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Buildings
{
    public class BuildingView : GameMonoBehaviour, IConstructionView
    {
        private UnityView _image;
        [SerializeField] private UnityEngine.Vector3 _offset;

        public IVisual GetImage()
        {
            return _image;
        }

        public void SetImage(IVisual image)
        {
            _image = new UnityView(image);
            UpdateView();
        }

        public void SetPosition(System.Numerics.Vector2 pos)
        {
            transform.position = pos.ToUnityVector(transform.position.z) + _offset;
        }

        public System.Numerics.Vector2 GetPosition()
        {
            return (transform.position - _offset).ToLogicVector();
        }

        private void UpdateView()
        {
            Instantiate(_image.View, transform, false);
        }

    }
}
