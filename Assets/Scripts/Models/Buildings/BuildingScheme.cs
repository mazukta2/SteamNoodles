using Assets.Scripts.Data.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Vector2Int Size => _data.Size;

        public Vector2Int[] GetOccupiedSpace(Vector2Int position)
        {
            var result = new List<Vector2Int>();
            for (int x = 0; x < _data.Size.x; x++)
            {
                for (int y = 0; y < _data.Size.y; y++)
                {
                    result.Add(position + new Vector2Int(x, y));
                }
            }
            return result.ToArray();
        }

        public Vector2Int[] GetPlaceablePositions(BuildingsGrid grid)
        {
            var rect = grid.GetRect();
            var list = new List<Vector2Int>();
            for (int x = rect.x; x < rect.width; x++)
            {
                for (int y = rect.y; y < rect.height; y++)
                {
                    var pos = new Vector2Int(x, y);
                    if (IsPossibleToPlace(grid, pos))
                        list.Add(pos);
                }
            }
            return list.ToArray();
        }

        public bool CanBuild(BuildingsGrid grid, Vector2Int position)
        {
            return GetOccupiedSpace(position).All(x => IsPossibleToPlace(grid, position));
        }

        private bool IsPossibleToPlace(BuildingsGrid grid, Vector2Int pos)
        {
            if (_data.Requirements.DownEdge)
            {
                var rect = grid.GetRect();
                return rect.y == pos.y || rect.y + 1 == pos.y;
            }
            return true;
        }

    }
}
