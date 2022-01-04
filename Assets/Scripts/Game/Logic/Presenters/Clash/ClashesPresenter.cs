using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Units
{
    public class ClashesPresenter : Disposable
    {
        private GameClashes _model;
        private IClashesView _view;

        public IClashesView View { get; private set; }

        public ClashesPresenter(GameClashes model, IClashesView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.StartClash.SetAction(HandleStartClash);
            UpdateShowing();
            _model.OnCanStartClashChanged += _model_OnCanStartClashChanged;
        }

        protected override void DisposeInner()
        {
            _model.OnCanStartClashChanged -= _model_OnCanStartClashChanged;
        }

        public void HandleStartClash()
        {
            _model.StartClash();
        }

        private void _model_OnCanStartClashChanged()
        {
            UpdateShowing();
        }

        private void UpdateShowing()
        {
            _view.StartClash.SetShowing(_model.CanStartClash);
            
        }
    }
}
