using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Views.Events;
using Assets.Scripts.Views.Levels;
using UnityEngine;

namespace Assets.Scripts.Views.Session
{
    public class SessionServiceView : GameMonoBehaviour
    {
        [SerializeField] private PrototypeLink _levelPrototype;

        /*
        private SessionService _session;
        private HistoryReader _historyReader;

        protected void Start()
        {
            
            _session = Get<SessionService>();
            _historyReader = new HistoryReader(_session.History);
            _historyReader
                .Subscribe<SessionService.LevelCreatedEvent>(LevelCreated)
                .Update();
            
        }

        protected void Update()
        {
            _historyReader.Update();
        }

        public void LevelCreated(SessionService.LevelCreatedEvent level)
        {
            _levelPrototype.DestroySpawned();
            _levelPrototype.Create<LevelView>(v => v.Set(level.Level));
        }*/
    }
}