using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class PlayAnimation : MonoBehaviour
{
    [SerializeField] GameObject _object;

    public void Play(string animationName)
    {
        _object.GetComponent<Animator>().CrossFade(animationName, 0.1f);
    }

}

