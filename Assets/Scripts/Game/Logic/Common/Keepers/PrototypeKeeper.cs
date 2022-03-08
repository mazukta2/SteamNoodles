namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    /*
    // this class is allow you to create new view and keep link to it inside class, so you cant create 2 instances of same view 
    public abstract class PrototypeKeeper<T> : Disposable, IMaker<T> where T : UnityMonoBehaviour, new() 
    {
        public LocalPrototypeLink Prototype;

        public T Value { get; private set; }

        protected override void DisposeInner()
        {
            if (Value != null)
                Value.OnDispose -= Value_OnDisposed;
        }

        public T Create()
        {
            if (Value != null)
                throw new Exception("You can't spawn in existing keeper");

            var go = Prototype.Create();
            var component = go.GetComponent<T>();
            if (component == null)
                throw new Exception($"Component {typeof(T).Name} doesn't exist in prototype root {go.name}");

            Value = component;

            Value.OnDispose += Value_OnDisposed;
            return Value;
        }

        private void Value_OnDisposed()
        {
            Value.OnDispose -= Value_OnDisposed;
            Value = null;
        }

    }
    */
}
