using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Units
{
    public class UnitPresenter
    {
        private IUnitView _view;
        private Unit _model;

        public UnitPresenter(Unit model, IUnitView view)
        {
            _view = view;
            _view.SetPosition(model.Position);
            _model = model;
            _model.OnPositionChanged += _model_OnPositionChanged;
        }

        public void Destroy()
        {
            _model.OnPositionChanged -= _model_OnPositionChanged;
            _view.Destroy();
        }

        public Unit Unit => _model;

        private void _model_OnPositionChanged()
        {
            _view.SetPosition(_model.Position);
        }

    }
}
