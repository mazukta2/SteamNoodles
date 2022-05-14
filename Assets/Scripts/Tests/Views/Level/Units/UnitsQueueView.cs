using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitsQueueView : ViewWithPresenter<UnitsQueuePresenter>, IUnitsQueueView
    {
        public IPosition StartPosition { get; private set; } = new PositionMock();

        public UnitsQueueView(IViewsCollection level) : base(level)
        {
        }
    }
}