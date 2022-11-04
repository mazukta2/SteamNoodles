using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class SceneTimelineAnimation : MonoBehaviour
{
    [SerializeField] string _tag;

    private static List<SceneTimelineAnimation> _list = new List<SceneTimelineAnimation>();

    protected void Awake()
    {
        _list.Add(this);
    }

    protected void OnDestroy()
    {
        _list.Remove(this);
    }


    public static void Play(string animationName)
    {
        foreach (var item in _list)
        {
            if (item._tag == animationName)
            {
                item.gameObject.GetComponent<PlayableDirector>().Play();
            }
        }
    }
}

