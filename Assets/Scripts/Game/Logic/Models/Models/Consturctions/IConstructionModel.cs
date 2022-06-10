using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IConstructionModel : IDisposable
    {
        FieldRotation Rotation { get; }
        GameVector3 WorldPosition { get; }

        void CreateModel(IViewContainer container);
    }
}
