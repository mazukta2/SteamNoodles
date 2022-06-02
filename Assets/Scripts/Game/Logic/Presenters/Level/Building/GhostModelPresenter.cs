using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostModelPresenter : BasePresenter<IConstructionModelView>
    {
        public GhostModelPresenter(IConstructionModelView view) : base(view)
        {
        }

        protected override void DisposeInner()
        {
        }
    }
}
