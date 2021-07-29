using Assets.Scripts.Data.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class Building
    {
        public Vector2Int Position { get; private set; }
        public BuildingSchemeData Scheme { get; private set; }

        public Building(BuildingScheme scheme, Vector2Int position)
        {
            Scheme = scheme.GetData();
            Position = position;
        }

        public Vector2Int[] GetOccupiedSpace()
        {
            return new Vector2Int[] { Position };
        }
    }
}
