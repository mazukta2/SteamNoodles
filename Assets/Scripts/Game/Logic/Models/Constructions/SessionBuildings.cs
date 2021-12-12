

namespace Assets.Scripts.Models.Buildings
{
    public class SessionBuildings 
    {
        //private List<Building> _buildings = new List<Building>();
      
        //public Building[] GetBuildings() => _buildings.ToArray();

        //public bool CanBuild(BuildingScheme scheme, Vector2Int position)
        //{
        //    if (!IsInside(scheme, position))
        //        return false;

        //    var ocupiedPlace = _buildings.SelectMany(x => x.GetOccupiedSpace());
        //    foreach (var pos in scheme.GetOccupiedSpace(position))
        //    {
        //        if (ocupiedPlace.Any(x => x == pos))
        //            return false;
        //    }

        //    return true;
        //}

        //public BuildingPlacedEvent Build(BuildingScheme scheme, Vector2Int position)
        //{
        //    if (!CanBuild(scheme, position))
        //        throw new Exception("Can't build");

        //    var building = new Building(scheme, position);
        //    _buildings.Add(building);

        //    return new BuildingPlacedEvent(building);
        //}

        //public bool IsInside(BuildingScheme scheme, Vector2Int position)
        //{
        //    var rect = GetMapRect();
        //    foreach (var pos in scheme.GetOccupiedSpace(position))
        //    {
        //        if (!rect.Contains(pos))
        //            return false;
        //    }

        //    return true;
        //}
    }
}
