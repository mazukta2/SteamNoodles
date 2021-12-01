using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Units
{
    public class UnitPresenter : Disposable
    {
        public Unit Unit => _model;

        private IUnitView _view;
        private Unit _model;

        public UnitPresenter(Unit model, IUnitView view)
        {
            _view = view;
            _view.SetPosition(model.Position);
            _model = model;
            _model.OnPositionChanged += _model_OnPositionChanged;
        }

        protected override void DisposeInner()
        {
            _model.OnPositionChanged -= _model_OnPositionChanged;
            _view.Dispose();
        }

        private void _model_OnPositionChanged()
        {
            _view.SetPosition(_model.Position);
        }
    }
}
