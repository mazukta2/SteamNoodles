using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.CommandHandlers.Constructions
{
    public class ConstructionCommandHadlerService : IService,
        ICommandHandler<BuildConstructionCommand>
    {
        public void Handle(BuildConstructionCommand command)
        {
            //var view = command.Container.Spawn<IConstructionView>(command.Prefab);
            //new ConstructionPresenter(view, command.Construction);
        }
    }
}
