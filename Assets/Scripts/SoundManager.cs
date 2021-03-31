using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // public static SoundManager Instance;
    private AudioClip _swipeSound;
    private AudioSource _source;
    private AudioClip _click;
    private AudioClip _win;
    private AudioClip _lose;

    private void Start()
    {
        LoadSounds();
        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;

        GridManager.OnWin += PlayWin;
        GridManager.OnLose += PlayLose;
    }

    private void LoadSounds()
    {
        _swipeSound = Resources.Load<AudioClip>("audio/swipe-sound");
        _click = Resources.Load<AudioClip>("audio/click");
        _win = Resources.Load<AudioClip>("audio/win");
        _lose = Resources.Load<AudioClip>("audio/lose");
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

    public void PlayWin()
    {
        _source.clip = _win;
        _source.volume = 0.3f;
        _source.pitch = 0.7f;
        _source.loop = false;
        _source.Play();
    }

    public void PlayLose()
    {
        _source.clip = _lose;
        _source.volume = 0.3f;
        _source.pitch = 0.7f;
        _source.loop = false;
        _source.Play();
    }
    
    
}
