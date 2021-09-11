using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : GameMonoBehaviour
    {
        [SerializeField] GameSessionData _gameSessionData;

        private GameLogicViewModel _viewModel;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _viewModel = new GameLogicViewModel(new GameLogic());
            _viewModel.StartGame(_gameSessionData.StartLevel);
        }

        protected void OnDestroy()
        {
        }
    }
}