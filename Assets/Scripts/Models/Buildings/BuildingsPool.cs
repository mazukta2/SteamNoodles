using Assets.Scripts.Data.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Buildings
{
    public class BuildingsPool
    {
        public BuildingScheme[] GetCurrentSchemes() => _schemes.ToArray();
        private List<BuildingScheme> _schemes = new List<BuildingScheme>();

        public BuildingsPool(BuildingsData buildings)
        {
            foreach (var item in buildings.Buildings)
            {
                _schemes.Add(new BuildingScheme(item));
            }
        }
    }
}
