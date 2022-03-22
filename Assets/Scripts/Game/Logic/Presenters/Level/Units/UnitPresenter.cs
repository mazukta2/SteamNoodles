using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitPresenter : BasePresenter
    {
        public Unit Unit => _model;

        private UnitView _view;
        private Unit _model;

        public UnitPresenter(Unit model, UnitView view) : base(view)
        {
            _view = view;
            _view.Position.Value = model.Position;
            _model = model;
            _model.OnPositionChanged += _model_OnPositionChanged;
            _model.OnDispose += _model_OnDispose;
        }

        protected override void DisposeInner()
        {
            _model.OnPositionChanged -= _model_OnPositionChanged;
            _model.OnDispose -= _model_OnDispose;
        }

        private void _model_OnPositionChanged()
        {
            _view.Position.Value = _model.Position;
        }

        private void _model_OnDispose()
        {
            _view.Dispose();
        }

    }
}
