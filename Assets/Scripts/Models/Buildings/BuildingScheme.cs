using Assets.Scripts.Data.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Models.Buildings
{
    public class BuildingScheme
    {
        private BuildingSchemeData _data;

        public BuildingScheme(BuildingSchemeData data)
        {
            _data = data;
        }

        public BuildingSchemeData GetData() => _data;
        public Sprite GetImage() => _data.BuildingIcon;
        public GameObject GetGhostPrefab() => _data.Ghost;
        public GameObject GetViewPrefab() => _data.View;

        public Vector2Int[] GetOccupiedSpace(Vector2Int position)
        {
            return new Vector2Int[] { position };
        }
    }
}
