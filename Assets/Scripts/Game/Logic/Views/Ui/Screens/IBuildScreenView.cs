using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IBuildScreenView : IScreenView, IViewWithGenericPresenter<BuildScreenPresenter>
    {
        public void Init(ConstructionCard card)
        {
            new BuildScreenPresenter(this, card);
        }
    }
}
