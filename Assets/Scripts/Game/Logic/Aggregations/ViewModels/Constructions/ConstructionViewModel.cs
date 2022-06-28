using System;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions
{
    public class ConstructionViewModel : Disposable
    {
        public event Action OnShrinkChanged = delegate {  };

        public Uid Id { get; private set; }
        public GameVector3 WorldPosition { get; set; }
        public FieldRotation Rotation { get; set; }
        public IViewPrefab Prefab { get; set; }
        public float Shrink { get; private set; }

        public ConstructionViewModel(Uid id)
        {
            Id = id;
        }

        public void ChangeShrink(float shrink)
        {
            Shrink = shrink;
            OnShrinkChanged();
        }
    }
}