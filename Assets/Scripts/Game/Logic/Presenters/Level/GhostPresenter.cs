using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter
    {
        //private readonly PlayerHand _model;
        //private readonly ScreenManagerPresenter _screenManager;
        //private readonly HandView _view;

        public GhostPresenter(GhostView view) : base(view)
        {
            //_view = view ?? throw new ArgumentNullException(nameof(view));
            //_model = model ?? throw new ArgumentNullException(nameof(model));
            //_screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            //foreach (var item in model.Cards)
            //    ScnemeAddedHandle(item);
            //_model.OnAdded += ScnemeAddedHandle;
        }

        //protected override void DisposeInner()
        //{
        //    _model.OnAdded -= ScnemeAddedHandle;
        //}

        //private void ScnemeAddedHandle(ConstructionCard obj)
        //{
        //    var viewPresenter = _view.CardPrototype.Create<HandConstructionView>(_view.Cards);
        //    viewPresenter.Init(_screenManager, obj);
        //}
    }
}
