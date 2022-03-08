namespace GameCommon.Assets.Scripts.Common.Prototypes
{
    /*
#if UNITY_ENGINE
    [DisallowMultipleComponent]
#endif
    public class RemotePrototypeLink : UnityMonoBehaviour
    {
        public GameObjectResource GetRemoteAccess()
        {
            return new PrototypeAccess(this);
        }

        private class PrototypeAccess : GameObjectResource
        {
            private RemotePrototypeLink _link;
            public PrototypeAccess(RemotePrototypeLink link)
            {
                _link = link;
            }

            public override GameObject Create(Transform transform)
            {
                var go = GameObject.Instantiate(_link.gameObject, transform);
                go.SetActive(true);
                return go;
            }
        }

        protected override void CreatedInner()
        {
            Destroy(this);
        }
        protected override void DisposeInner()
        {
        }

    }
    */
}
