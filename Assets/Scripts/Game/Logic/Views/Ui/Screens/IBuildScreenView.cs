using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IBuildScreenView : IScreenView, IViewWithGenericPresenter<BuildScreenPresenter>
    {
        public void Init(IDataProvider<ConstructionCardData> card)
        {
            new BuildScreenPresenter(this, card);
        }
    }
}
