using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using GameUnity.Assets.Scripts.Unity.Core;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(GameLevelData))]
    public class GameLevelData : ScriptableObject, ILevelPrototype
    {
        [Scene]
        public string Scene;
        public Point Size;
        public BuildingSchemeData[] Hand;

        public IBuildingPrototype[] StartingHand => Hand;
        Point ILevelPrototype.Size => Size;

        public void Load(Action<ILevelPrototype> onFinished)
        {
            var loading = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Single);
            //if (loading.isDone)
            //    onFinished(this);
            //else
            //    loading.completed += Complited;

            //void Complited(AsyncOperation operation)
            //{
            //    //loading.completed -= Complited;
            //    onFinished(this);
            //}
        }
    }
}
