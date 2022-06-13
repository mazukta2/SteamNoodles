using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Level;
using System;

namespace Game.Assets.Scripts.Tests.Models.Constructions
{
    public class ConstructionModelMock : Disposable, IConstructionModel
    {
        public event Action OnDestroyed = delegate { };
        public event Action OnExplostion = delegate { };
        public event Action OnUpdate = delegate { };

        private float _shrink = 1;

        public ConstructionModelMock()
            : this (FieldRotation.Default, GameVector3.Zero)
        {
        }

        public ConstructionModelMock(FieldRotation rotation, GameVector3 worldPosition)
        {
            SetRotation(rotation);
            SetWorldPosition(worldPosition);
        }

        private FieldRotation rotation;

        public FieldRotation GetRotation()
        {
            return rotation;
        }

        public void SetRotation(FieldRotation value)
        {
            rotation = value;
        }

        private GameVector3 worldPosition;

        public GameVector3 GetWorldPosition()
        {
            return worldPosition;
        }

        public void SetWorldPosition(GameVector3 value)
        {
            worldPosition = value;
        }

        public float GhostShrinkDistance => 0;
        public float GhostHalfShrinkDistance => 0;

        public IViewPrefab GetModelAsset()
        {
            return new DefaultViewPrefab(x => new ConstructionModelView(x));
        }

        public float GetShrinkValue()
        {
            return _shrink;
        }

        public void Shake()
        {
        }

        public void SetShrink(float value)
        {
            _shrink = value;
            OnUpdate();
        }

        public void FireExplode()
        {
            OnExplostion();
        }
    }
}
