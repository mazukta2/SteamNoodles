using UnityEngine;

namespace Assets.Scripts.Data.Buildings
{
    [CreateAssetMenu(menuName = "Game/" + nameof(BuildingSchemeData))]
    public class BuildingSchemeData : ScriptableObject
    {
        public Sprite BuildingIcon;
        public GameObject Ghost;
        public GameObject View;
    }
}
