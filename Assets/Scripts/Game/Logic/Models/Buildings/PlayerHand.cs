using System.Collections.Generic;

namespace Assets.Scripts.Models.Buildings
{
    public class PlayerHand
    {
        public BuildingScheme[] CurrentSchemes => _schemes.ToArray();
        private List<BuildingScheme> _schemes = new List<BuildingScheme>();

        /*
        public PlayerHand(BuildingsData buildings)
        {
            foreach (var item in buildings.Buildings)
            {
                _schemes.Add(new BuildingScheme(item));
            }
        }
        */

        //public PlayerHand(PlayerHand origin)
        //{
         //   _schemes = origin._schemes;
        //}
    }
}
