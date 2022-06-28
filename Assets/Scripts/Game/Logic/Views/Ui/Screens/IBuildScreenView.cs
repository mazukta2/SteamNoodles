using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IBuildScreenView : IScreenView, IViewWithGenericPresenter<BuildScreenPresenter>
    {
        public void Init(Uid cardId)
        {
            new BuildScreenPresenter(this, cardId);
        }
    }
}
