using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IConstructionModel : IDisposable
    {
        event Action OnDestroyed;
        event Action OnExplostion;

        FieldRotation Rotation { get; }
        GameVector3 WorldPosition { get; }

        public bool IsDisposed { get; }
        int GhostShrinkDistance { get; }
        int GhostHalfShrinkDistance { get; }

        IViewPrefab GetModelAsset();
        void ConnectPresenter(IConstructionModelView modelView);
        void Shake();
        float GetShrinkValue();
    }
}
