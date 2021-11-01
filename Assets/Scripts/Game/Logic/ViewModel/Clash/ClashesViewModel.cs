using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Units
{
    public class ClashesViewModel
    {
        private GameClashes _model;
        private IClashesView _view;

        public ClashesViewModel(GameClashes model, IClashesView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (view == null) throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;
            _view.SetStartClashAction(OnStartClash);
            _model.OnClashStarted += _model_OnClashStarted;
            _model.OnClashEnded += _model_OnClashEnded;
        }

        public void Destroy()
        {
            _model.OnClashStarted -= _model_OnClashStarted;
            _model.OnClashEnded -= _model_OnClashEnded;
        }

        public void OnStartClash()
        {
            _model.StartClash();
        }

        private void _model_OnClashStarted()
        {
            _view.ShowButton(false);
        }

        private void _model_OnClashEnded()
        {
            _view.ShowButton(true);
        }

    }
}
