using Assets.Scripts.Core;
using Assets.Scripts.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using System;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views
{
    public class SessionView : ViewMonoBehaviour, IGameSessionView
    {
        public ILevelView GetCurrentLevel()
        {
            return GameObject.FindObjectOfType<LevelView>();
        }
       
    }
}