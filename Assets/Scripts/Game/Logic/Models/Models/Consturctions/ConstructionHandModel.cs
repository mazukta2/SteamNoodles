using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class ConstructionHandModel : Disposable, IConstructionHandModel
    {
        public Uid Id { get; private set; }

        public ConstructionHandModel(Uid id)
        {
            Id = id;
        }

        protected override void DisposeInner()
        {
        }

        public void ConnectPresenter(IHandConstructionView view)
        {
        }
    }
}
