using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Levels
{
    public class GameLevel
    {
        private BuildingsGrid _grid;
        private BuildingsPool _pool;


        public GameLevel(BuildingsData data)
        {
            _grid = new BuildingsGrid(data);
            _pool = new BuildingsPool(data);
        }

        public BuildingsGrid GetGrid()
        {
            return _grid;
        }

        public BuildingsPool GetBuildingPool()
        {
            return _pool;
        }
    }
}
