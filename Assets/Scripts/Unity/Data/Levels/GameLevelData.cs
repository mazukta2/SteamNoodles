using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Game.Logic.Contexts;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Views.Levels;
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

        public void Load(Action<ILevelPrototype, ILevelContext> onFinished)
        {
            var loading = SceneManager.LoadSceneAsync(Scene, LoadSceneMode.Single);
            if (loading.isDone)
                Finish();
            else
                loading.completed += Complited;

            void Complited(AsyncOperation operation)
            {
                loading.completed -= Complited;
                Finish();
            }

            void Finish()
            {
                var view = GameObject.FindObjectOfType<LevelView>();
                if (view == null) throw new Exception("Cant find level view in scene");
                
                onFinished(this, view);
            }
        }
    }
}
