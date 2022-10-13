using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RandomRotator : MonoBehaviour
{
    [SerializeField] bool _x;
    [SerializeField] bool _y;
    [SerializeField] bool _z;

    protected void OnEnable()
    {
        if (!Application.isEditor)
            return;

        var angles = gameObject.transform.rotation.eulerAngles;

        if (_x)
            angles.x = UnityEngine.Random.Range(0, 360);
        if (_y)
            angles.y = UnityEngine.Random.Range(0, 360);
        if (_z)
            angles.z = UnityEngine.Random.Range(0, 360);

        gameObject.transform.rotation = Quaternion.Euler(angles);
    }
}

