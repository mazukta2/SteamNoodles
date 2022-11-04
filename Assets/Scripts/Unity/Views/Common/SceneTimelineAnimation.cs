using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameUnity.Assets.Scripts.Unity.Engine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class SceneTimelineAnimation : MonoBehaviour
{
    [SerializeField] string _tag;

    private static List<SceneTimelineAnimation> _list = new List<SceneTimelineAnimation>();

    protected void Awake()
    {
        _list.Add(this);
        gameObject.GetComponent<PlayableDirector>().stopped += HandleStoped;
    }

    protected void OnDestroy()
    {
        _list.Remove(this);
        gameObject.GetComponent<PlayableDirector>().stopped -= HandleStoped;
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
    
    private void HandleStoped(PlayableDirector obj)
    {
        UnityControls.Controls.FireTimelineFinished(_tag);
    }

}

