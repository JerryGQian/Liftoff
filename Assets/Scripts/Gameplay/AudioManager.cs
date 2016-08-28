using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public AudioSource generalSource;
    public AudioSource explosionSource;
    public AudioSource musicSource;

    public AudioClip newBest1;
    public AudioClip newBest2;
    public AudioClip puff;

    void Awake() {
        Util.audioManager = this;
    }

    void Start() {
        
    }

    public void setMute() {
        if (Util.wm.musicMuted) {
            musicSource.Stop();
        }
        else {
            musicSource.Play();
        }
    }

    public void playNewBest1() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(newBest1);
    }

    public void playNewBest2() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(newBest2);
    }

    public void playPuff() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(puff);
    }
}
