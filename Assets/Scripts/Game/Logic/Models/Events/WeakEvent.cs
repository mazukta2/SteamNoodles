using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Events
{
    public class WeakEvent : IDisposable
    {
        private List<IWeakAction> _weakActions = new List<IWeakAction>();

        public void Dispose()
        {
            _weakActions.Clear();
        }

        public void Subscribe<T>(Action<T> action) where T : IStateEntity
        {
            _weakActions.Add(new WeakAction<T>(action.Target, action.Method));
        }

        public void Unsubscribe<T>(Action<T> action) where T : IStateEntity
        {
            _weakActions.RemoveAll(x => x.Target == action.Target && x.MethodInfo == action.Method);
        }

        public void Execute<T>(T state) where T : IStateEntity
        {
            _weakActions.RemoveAll(x => !x.IsAlive);
            foreach (var item in _weakActions.OfType<WeakAction<T>>())
                item.Execute(state);
        }

        private class WeakAction<T> : IWeakAction where T : IStateEntity
        {
            public WeakReference Target { get; set; }
            public MethodInfo MethodInfo { get; set; }

            public WeakAction(object target, MethodInfo methodInfo)
            {
                Target = new WeakReference(target);
                MethodInfo = methodInfo;
            }

            public void Execute(T state)
            {
                Object target = Target.Target;
                if (target != null)
                {
                    MethodInfo.Invoke(target, new object[] { state });
                }
            }

            public bool IsAlive
            {
                get { return Target.IsAlive; }
            }
        }

        private interface IWeakAction
        {
            bool IsAlive { get; }
            WeakReference Target { get; }
            MethodInfo MethodInfo { get;}
        }
    }
}
