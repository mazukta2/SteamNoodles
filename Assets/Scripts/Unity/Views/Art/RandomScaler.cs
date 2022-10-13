using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RandomScaler : MonoBehaviour
{
    [SerializeField] float _min;
    [SerializeField] float _max;

    protected void OnEnable()
    {
        if (!Application.isEditor)
            return;

        var scale = UnityEngine.Random.Range(_min, _max);
        gameObject.transform.localScale = new Vector3(scale, scale, scale);
    }
}

