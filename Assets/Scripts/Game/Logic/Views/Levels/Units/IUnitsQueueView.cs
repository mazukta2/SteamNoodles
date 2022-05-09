using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitsQueueView : IViewWithPresenter
    {
        ILevelPosition StartPosition { get; }
    }
}