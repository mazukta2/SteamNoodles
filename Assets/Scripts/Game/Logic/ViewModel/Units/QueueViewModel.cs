using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Units
{
    public class QueueViewModel
    {
        private UnitsQueue _model;

        public QueueViewModel(UnitsQueue model)
        {
            _model = model;
        }

        public void Destroy()
        {
        }
    }
}
