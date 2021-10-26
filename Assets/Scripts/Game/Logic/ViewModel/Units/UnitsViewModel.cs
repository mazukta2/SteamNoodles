using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Units
{
    public class UnitsViewModel
    {
        private LevelUnits _model;
        private IUnitsView _view;
        private List<UnitViewModel> _units = new List<UnitViewModel>();

        public UnitsViewModel(LevelUnits model, IUnitsView view)
        {
            _model = model;
            _view = view;

            foreach (var unit in _model.Units)
            {
                SpawnUnit(unit);
            }
            _model.OnUnitSpawn += SpawnUnit;
        }

        public void Destroy()
        {
            _model.OnUnitSpawn -= SpawnUnit;
            foreach (var item in _units)
                item.Destroy();
        }

        private void SpawnUnit(Unit unit)
        {
            _units.Add(new UnitViewModel(unit, _view.CreateUnit()));
        }

    }
}
