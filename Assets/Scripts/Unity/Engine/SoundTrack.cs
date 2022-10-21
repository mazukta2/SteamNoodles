using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundTrack : Game.Assets.Scripts.Game.Environment.Engine.ISoundTrack
{
    private string _name;
    private GameObject _gameObject;
    private AudioSource _component;

    public SoundTrack(string name)
    {
        _name = name;
        _gameObject = new GameObject();
        _gameObject.name = "Sound";
        GameObject.DontDestroyOnLoad(_gameObject);
        _component = _gameObject.AddComponent<AudioSource>();
        _component.playOnAwake = false;
        _component.clip = UnityEngine.Resources.Load<AudioClip>("Assets/Sounds/" + name);
        _component.loop = true;
        var mixer = Resources.Load<AudioMixer>("Assets/Sounds/Mixer");
        _component.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
        _component.Play();
    }

    public void SetVolume(float volume)
    {
        _component.volume = volume;
    }

    public float GetVolume()
    {
        return _component.volume;
    }
    public void Dispose()
    {
        GameObject.Destroy(_gameObject);
    }

}

