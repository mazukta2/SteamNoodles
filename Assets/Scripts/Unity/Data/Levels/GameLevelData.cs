using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using GameUnity.Assets.Scripts.Unity.Core;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rect = Game.Assets.Scripts.Game.Logic.Common.Math.Rect;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Game/" + nameof(GameLevelData))]
    public class GameLevelData : ScriptableObject, ILevelPrototype
    {
        [Scene]
        public string Scene;
        public Point Size;
        public BuildingSchemeData[] Hand;
        public OrderData[] AvailableOrders;
        public UnityEngine.RectInt UnitsRect;

        public IConstructionPrototype[] StartingHand => Hand;
        public IOrderPrototype[] Orders => AvailableOrders;
        public Rect UnitsSpawnRect => new Rect(UnitsRect.x, UnitsRect.y, UnitsRect.width, UnitsRect.height);

        Point IPlacementPrototype.Size => Size;

        public void Load(Action<ILevelPrototype, ILevelView> onFinished)
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
