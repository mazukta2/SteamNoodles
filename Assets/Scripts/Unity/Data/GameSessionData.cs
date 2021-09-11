using Assets.Scripts.Data.Buildings;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(GameSessionData))]
    public class GameSessionData : ScriptableObject
    {
        public BuildingsData Buildings;
        public GameLevelData StartLevel;
    }
}
