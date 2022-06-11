using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Tests.Models.Constructions
{
    public class ConstructionModelMock : Disposable, IConstructionModel
    {
        public ConstructionModelMock()
            : this (FieldRotation.Default, GameVector3.Zero)
        {
        }

        public ConstructionModelMock(FieldRotation rotation, GameVector3 worldPosition)
        {
            Rotation = rotation;
            WorldPosition = worldPosition;
        }

        public FieldRotation Rotation { get; set; }
        public GameVector3 WorldPosition { get; set; }
        public bool IsModelPresenterConnected { get; set; }

        public int GhostShrinkDistance => throw new NotImplementedException();

        public int GhostHalfShrinkDistance => throw new NotImplementedException();

        public event Action OnDestroyed = delegate { };
        public event Action OnExplostion = delegate { };

        public void ConnectPresenter(IConstructionModelView modelView)
        {
            IsModelPresenterConnected = true;
        }

        public IViewPrefab GetModelAsset()
        {
            return new DefaultViewPrefab(x => new ConstructionModelView(x));
        }

        public float GetShrinkValue()
        {
            return 1;
        }

        public void Shake()
        {
        }
    }
}
