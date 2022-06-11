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
    public class GhostModelMock : Disposable, IGhostModel
    {
        public GhostModelMock()
        {
        }

        public bool IsActive => throw new NotImplementedException();

        public event Action OnUpdate = delegate { };

        public GameVector3 GetTargetPosition()
        {
            throw new NotImplementedException();
        }

        public GameVector3 WorldPosition()
        {
            throw new NotImplementedException();
        }

        public void FireUpdate()
        {
            OnUpdate();
        }
    }
}
