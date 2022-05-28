using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingPointsService
    {
        private BuildingPointsManager _buildingPoints;

        public BuildingPointsService(BuildingPointsManager buildingPoints)
        {
            _buildingPoints = buildingPoints;
        }

        public void ChangePoints(BuildingPoints points, GameVector3 fromPosition)
        {
            _buildingPoints.Change(points.Value, fromPosition);
        }

        public void ChangePoints(BuildingPoints points)
        {
            _buildingPoints.Change(points.Value);
        }
    }
}
