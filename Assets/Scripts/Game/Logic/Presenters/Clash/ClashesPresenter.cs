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

            _view.StartClash.SetAction(OnStartClash);
            _view.StartClash.SetShowing(true);
            _model.OnClashStarted += _model_OnClashStarted;
            _model.OnClashEnded += _model_OnClashEnded;
        }

        protected override void DisposeInner()
        {
            _model.OnClashStarted -= _model_OnClashStarted;
            _model.OnClashEnded -= _model_OnClashEnded;
            _view.Dispose();
        }

        public void OnStartClash()
        {
            _model.StartClash();
        }

        private void _model_OnClashStarted()
        {
            _view.StartClash.SetShowing(false);
        }

        private void _model_OnClashEnded()
        {
            _view.StartClash.SetShowing(true); ;
        }
    }
}
