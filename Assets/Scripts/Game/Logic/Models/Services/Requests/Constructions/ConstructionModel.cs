using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions
{
    public class ConstructionModel : Disposable, IConstructionModel
    {
        public event Action OnDestroyed = delegate { };
        public event Action OnExplostion = delegate { };
        public ConstructionScheme Scheme { get; private set; }
        public FieldRotation Rotation { get; private set; }
        public GameVector3 WorldPosition { get; private set; }

        private ICommands _commands;

        public ConstructionModel(Construction construction, 
            GameVector3 worldPosition, ConstructionsRequestProviderService service)
        {
            //_commands = commands;
            Rotation = construction.Rotation;
            WorldPosition = worldPosition;
            Scheme = construction.Scheme;
        }

        public void CreateModel(IViewContainer container)
        {
            //var view = container.Spawn<IConstructionView>(command.Prefab);
            //new ConstructionPresenter(view, command.Construction);

            _commands.Execute(new AddConstructionModelCommand(Scheme, container));
        }

        public void Build()
        {
            _commands.Execute(new ShakeCameraCommand());
        }
    }
}
