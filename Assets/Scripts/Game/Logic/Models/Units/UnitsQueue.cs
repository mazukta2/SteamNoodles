using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public class UnitsQueue
    {
        private Placement _placement;

        public UnitsQueue(Placement placement)
        {
            _placement = placement;
        }
    }
}
