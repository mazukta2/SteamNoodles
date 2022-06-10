using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
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
        public bool IsModelRequested { get; set; }

        public void CreateModel(IViewContainer container)
        {
            IsModelRequested = true;
        }
    }
}
