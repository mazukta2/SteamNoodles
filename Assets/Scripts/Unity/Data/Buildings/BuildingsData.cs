using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName ="Game/" + nameof(BuildingsData))]
    public class BuildingsData : ScriptableObject
    {
        public BuildingSchemeData[] Buildings;
        public Vector2Int MapSize;
    }
}
