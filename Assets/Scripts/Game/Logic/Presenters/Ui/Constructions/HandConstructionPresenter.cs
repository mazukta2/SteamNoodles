using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter
    {
        //private ConstructionCard _model;
        //private IHandConstructionView _view;
        //private Action<ConstructionCard> _onClick;

        public HandConstructionPresenter(View view) : base(view)
        {
        }
        //public HandConstructionPresenter(ConstructionCard model, IHandConstructionView view, Action<ConstructionCard> onClick)
        //{
        //    _model = model ?? throw new ArgumentNullException(nameof(model));
        //    _view = view ?? throw new ArgumentNullException(nameof(view));
        //    _onClick = onClick;

        //    UpdateView();
        //}

        //protected override void DisposeInner()
        //{
        //    _view.Dispose();
        //}

        //public bool Is(ConstructionCard obj)
        //{
        //    return _model == obj;
        //}

        //private void OnClick()
        //{
        //    _onClick(_model);
        //}

        //private void UpdateView()
        //{
        //    _view.SetIcon(_model.HandIcon);
        //    _view.Button.SetAction(OnClick);
        //}
    }
}
