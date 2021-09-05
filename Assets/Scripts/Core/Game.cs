using Assets.Scripts.Data;
using Assets.Scripts.Models;
using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameMonoBehaviour
    {
        [SerializeField] GameSessionData _gameSessionData;

        protected void Awake()
        {
            SetGameContext(new GameContext(_gameSessionData));
        }

        protected void OnDestroy()
        {
            DisposeGameContext();
        }
    }
}