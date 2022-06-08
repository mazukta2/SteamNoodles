using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level.Units
{
    public interface IUnitModelView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new UnitModelPresenter(this);
        }

    }
}