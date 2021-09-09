
using System;
using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BuildingSchemeData))]
    public class BuildingSchemeData : ScriptableObject, IBuildingSchemeViewData
    {
        public Sprite BuildingIcon;
        public GameObject Ghost;
        public GameObject View;
        public Vector2Int Size = Vector2Int.one;
        public Requirements Requirements;

        Sprite IBuildingSchemeViewData.BuildingIcon => BuildingIcon;
        GameObject IBuildingSchemeViewData.Ghost => Ghost;
        GameObject IBuildingSchemeViewData.View => View;
        Vector2Int IBuildingSchemeModelData.Size => Size;
        Requirements IBuildingSchemeModelData.Requirements => Requirements;
    }

    [Serializable]
    public struct Requirements
    {
        public bool DownEdge;
    }

    public interface IBuildingSchemeModelData
    {
        public Vector2Int Size { get; }
        public Requirements Requirements { get; }
    }

    public interface IBuildingSchemeViewData : IBuildingSchemeModelData
    {
        public Sprite BuildingIcon { get; }
        public GameObject Ghost { get; }
        public GameObject View { get; }
    }
}
