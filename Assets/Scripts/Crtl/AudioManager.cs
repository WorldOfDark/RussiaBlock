using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Crtl crtl;

    public AudioClip cursor;
    public AudioClip drop;
    public AudioClip control;
    public AudioClip lineClear;

    private bool isMute = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        crtl = GameObject.Find("Crtl").GetComponent<Crtl>();
    }

    public void PlayCursor()
    { PlayAudio(cursor); }

    public void PlayDrop()
    { PlayAudio(drop); }

    public void PlayControl()
    { PlayAudio(control); }

    public void PlayLineClear()
    {
        PlayAudio(lineClear);
    }

    private void PlayAudio(AudioClip clip)
    {
        if (isMute)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnMuteButtonClick()
    {
        isMute = !isMute;
        crtl.view.SetMuteActive(isMute);
        if (isMute == false)
        {
            PlayCursor();
        }
    }
}
