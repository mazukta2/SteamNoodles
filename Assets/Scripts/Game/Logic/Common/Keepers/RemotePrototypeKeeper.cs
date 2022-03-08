namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    /*
    public abstract class RemotePrototypeKeeper<T> : UnityMonoBehaviour where T : UnityMonoBehaviour
    {
        public T Value { get; private set; }

        protected override void CreatedInner()
        {
        }

        protected override void DisposeInner()
        {
            if (Value != null)
            {
                Value.OnDispose -= Value_OnDisposed;
                Value.Dispose();
            }
        }

        public T Set(ExternalResource access)
        {
            if (Value != null)
                throw new Exception("You can't spawn in existing keeper");

            var goAccess = (GameObjectResource)access;
            var go = goAccess.Create(transform);
            var component = go.GetComponent<T>();
            if (component == null)
                throw new Exception($"Component {typeof(T).Name} doesn't exist in prototype root {go.name}");

            Value = component;
            Value.OnDispose += Value_OnDisposed;
            return Value;
        }

        public void Clear()
        {
            if (Value != null)
            {
                Value.OnDispose -= Value_OnDisposed;
                Value.Dispose();
            }
            Value = null;
        }

        private void Value_OnDisposed()
        {
            Clear();
        }

    }
    */
}
