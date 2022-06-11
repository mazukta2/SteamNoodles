using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class ConstructionModel : Disposable, IConstructionModel
    {
        public event Action OnDestroyed = delegate { };
        public event Action OnExplostion = delegate { };
        public ConstructionScheme Scheme { get; private set; }
        public FieldRotation Rotation { get; private set; }
        public GameVector3 WorldPosition { get; private set; }

        public int GhostShrinkDistance => throw new NotImplementedException();

        public int GhostHalfShrinkDistance => throw new NotImplementedException();

        private ICommands _commands;
        private readonly IAssets _assets;

        public ConstructionModel(Construction construction,
            GameVector3 worldPosition, ConstructionsRequestsService service, IAssets assets)
        {
            //_commands = commands;
            Rotation = construction.Rotation;
            WorldPosition = worldPosition;
            _assets = assets;
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

        public IViewPrefab GetModelAsset()
        {
            return _assets.GetPrefab(Scheme.LevelViewPath);
        }

        public void ConnectPresenter(IConstructionModelView modelView)
        {
            throw new NotImplementedException();
        }

        public void Shake()
        {
            throw new NotImplementedException();
        }

        public float GetShrinkValue()
        {
            return 1;
        }
    }
}
