using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioSource _audio;

    public void Play()
    {
        _audio.Play();
    }
}

