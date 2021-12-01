using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class LevelLoading : Disposable
    {
        public ILevelSettings Prototype { get; private set; }


        public LevelLoading(ILevelsController controller, ILevelSettings levelProto, Action onComplete)
        {
            _onComplete = onComplete;
            Prototype = levelProto;
            controller.Load(levelProto, Finished);
        }

        protected override void DisposeInner()
        {
        }

        private Action _onComplete;

        private void Finished()
        {
            if (IsDisposed)
                return;

            _onComplete();
        }
    }
}
