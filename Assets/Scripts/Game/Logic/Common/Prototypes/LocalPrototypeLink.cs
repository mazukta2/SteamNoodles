namespace Game.Assets.Scripts.Game.Logic.Common.Prototypes
{
    /*
#if UNITY_ENGINE
    [DisallowMultipleComponent]
#endif
public class LocalPrototypeLink : UnityMonoBehaviour
{
    private bool _itsOriginal = true;

    public GameObject Create()
    {
        var go = GameObject.Instantiate(gameObject, gameObject.transform.parent);
        var child = go.GetComponent<LocalPrototypeLink>();
        child._itsOriginal = false;
        go.SetActive(true);
        return go;
    }

    protected override void CreatedInner()
    {
        if (!_itsOriginal)
            Destroy(this);
        else
            gameObject.SetActive(false);
    }
    protected override void DisposeInner()
    {
    }
    }
    */
}
