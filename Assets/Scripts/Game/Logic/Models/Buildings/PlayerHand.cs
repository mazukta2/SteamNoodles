using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Events;
using System.Collections.Generic;

namespace Assets.Scripts.Models.Buildings
{
    public class PlayerHand
    {
        public ConstructionScheme[] CurrentSchemes => _schemes.ToArray();

        public History History = new History();

        private List<ConstructionScheme> _schemes = new List<ConstructionScheme>();

        public PlayerHand(IConstructionPrototype[] startingHand)
        {
            foreach (var item in startingHand)
            {
                Add(new ConstructionScheme(item));
            }
        }

        public void Add(ConstructionScheme buildingScheme)
        {
            _schemes.Add(buildingScheme);
            History.Add(new SchemeAddedToHandEvent(buildingScheme));
        }
    }
}
