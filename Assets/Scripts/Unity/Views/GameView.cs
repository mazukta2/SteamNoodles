using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;
using System.Collections;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views
{
    public class GameView : ViewMonoBehaviour, IGameView
    {
        [SerializeField] PrototypeLink _sessionPrototype;
        public DisposableViewKeeper<IGameSessionView> Session { get; private set; } 

        protected void Awake()
        {
            Session = new DisposableViewKeeper<IGameSessionView>(() => _sessionPrototype.Create<SessionView>());
        }
    }
}