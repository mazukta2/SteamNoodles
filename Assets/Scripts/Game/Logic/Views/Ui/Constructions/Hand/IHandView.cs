using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandView : IViewWithGenericPresenter<HandPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Cards { get;  }
        IViewPrefab CardPrototype { get; }
        IButton CancelButton { get; }
        IAnimator Animator { get; }

        static IHandView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this;
            new HandPresenter(IModels.Default.Find<PlayerHand>(), IScreenManagerView.Default.Presenter, this,
                IModels.Default.Find<PlacementField>());
        }
    }
}
