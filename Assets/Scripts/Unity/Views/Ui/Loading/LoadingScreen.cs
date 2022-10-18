using UnityEngine;
using System.Collections;
using Game.Assets.Scripts.Game.Logic.Events;
using Game.Assets.Scripts.Game.Logic.Events.Game;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screen;

    private Subscription<OnLoadingStarted> _loadingScreenStart;
    private Subscription<OnLoadingFinished> _loadingScreenStop;

    public void Awake()
    {
        _loadingScreenStart = new Subscription<OnLoadingStarted>(IEvents.Default, LoadingStarted);
        _loadingScreenStop = new Subscription<OnLoadingFinished>(IEvents.Default, LoadingStopped);
    }

    public void OnDestroy()
    {

    }

    public void LoadingStarted()
    {
        _screen.gameObject.SetActive(true);
    }

    public void LoadingStopped()
    {
        _screen.gameObject.SetActive(false);
    }
}

