using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    [SerializeField] float _x;
    [SerializeField] float _y;
    [SerializeField] float _z;

    protected void Update()
    {
        var angles = Quaternion.Euler(_x, _y, _z);
        gameObject.transform.rotation *= angles;
    }
}

