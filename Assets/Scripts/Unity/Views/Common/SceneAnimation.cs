using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class SceneAnimation : MonoBehaviour
{
    [SerializeField] string _tag;

    private static List<SceneAnimation> _list = new List<SceneAnimation>();

    protected void Awake()
    {
        _list.Add(this);
    }

    protected void OnDestroy()
    {
        _list.Remove(this);
    }


    public static void Play(string tag, string animationName)
    {
        foreach (var item in _list)
        {
            if (item._tag == tag)
            {
                item.gameObject.GetComponent<Animator>().CrossFade(animationName, 0.1f);
            }
        }
    }
}

