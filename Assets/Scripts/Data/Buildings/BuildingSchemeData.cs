using System;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BuildingSchemeData))]
    public class BuildingSchemeData : ScriptableObject
    {
        public Sprite BuildingIcon;
        public GameObject Ghost;
        public GameObject View;
        public Vector2Int Size = Vector2Int.one;
        public Requirements Requirements;
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }
}
