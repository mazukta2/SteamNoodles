using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building
{
    public class BuildConstructionCommand : ICommand
    {
        public Construction Construction { get; }
        public IViewContainer Container { get; }
        public IViewPrefab Prefab { get; }

        public BuildConstructionCommand(Construction construction,
            IViewContainer container, IViewPrefab prefab)
        {
            Construction = construction;
            Container = container;
            Prefab = prefab;
        }

    }
}
