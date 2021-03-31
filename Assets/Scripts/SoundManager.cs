using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // public static SoundManager Instance;
    private AudioClip _swipeSound;
    private AudioSource _source;
    private AudioClip _click;

    private void Start()
    {
        LoadSounds();
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;
    }

    private void LoadSounds()
    {
        _swipeSound = Resources.Load<AudioClip>("audio/swipe-sound");
        _click = Resources.Load<AudioClip>("audio/click");
    }

    public void PlaySwipeSound()
    {
        _source.clip = _swipeSound;
        _source.volume = 0.3f;
        _source.pitch = 1.5f;
        _source.loop = false;
        _source.Play();
    }

    public void PlayClick()
    {
        _source.clip = _click;
        _source.volume = 0.3f;
        _source.pitch = 0.75f;
        _source.loop = false;
        _source.Play();
    }
    
    
}
