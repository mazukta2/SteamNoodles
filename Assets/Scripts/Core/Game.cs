using Assets.Scripts.Data;
using Assets.Scripts.Models;
using UniRx;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : MonoBehaviour
    {
        [SerializeField] GameSessionData _gameSessionData;

        private GameContext _context;

        protected void Awake()
        {
            _context = new GameContext(MessageBroker.Default, _gameSessionData);
        }

        protected void OnDestroy()
        {
            _context.Dispose();
        }
    }
}