using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private List<Sounds> _sfxSounds;

    [SerializeField] private AudioSource _backgroundSource;
    [SerializeField] private AudioSource _sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlaySFXSound(string sfxToPlay)
    {
        var sound = _sfxSounds.Find(t => t.SoundName == sfxToPlay);

        if (sound != null)
        {
            _sfxSource.PlayOneShot(sound.AudioClip);
        }
    }

    public void StopBackgroundMusic()
    {
        _backgroundSource.Stop();
    }
}
