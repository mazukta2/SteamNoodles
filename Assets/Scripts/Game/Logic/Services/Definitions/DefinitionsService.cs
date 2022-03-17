using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services.Ui
{
    public class DefinitionsService : Disposable, IService
    {
        private IDefinitions _definitions;

        public DefinitionsService(IDefinitions definitions)
        {
            _definitions = definitions;
        }

        public IDefinitions Get()
        {
            return _definitions;
        }
    }
}
