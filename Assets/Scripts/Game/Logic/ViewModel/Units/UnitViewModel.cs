using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Units
{
    public class UnitViewModel
    {
        private IUnitView _view;

        public UnitViewModel(Unit model, IUnitView view)
        {
            _view = view;
            _view.SetPosition(model.Position);
        }

        public void Destroy()
        {
            _view.Destroy();
        }
    }
}
