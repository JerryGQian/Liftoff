using UnityEngine;
using System.Collections;

public enum SoundType { rocket, none, jet, drone, car, gun }

public class AudioManager : MonoBehaviour {
    public AudioSource generalSource;
    public AudioSource explosionSource;
    public AudioSource musicSource;

    public AudioClip music;
    public AudioClip birds;

    public AudioClip newBest1;
    public AudioClip newBest2;
    public AudioClip puff;
    public AudioClip lose;
    public AudioClip alertBeep;
    public AudioClip menuSwoosh;
    public AudioClip selectorClick;

    void Awake() {
        Util.audioManager = this;
    }

    void Start() {
        musicSource.clip = birds;
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

    public void playLose() {
        if (!Util.wm.musicMuted) generalSource.PlayOneShot(lose);
    }

    public void playAlertBeep() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(alertBeep);
    }
    public void playMenuSwoosh() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(menuSwoosh);
    }
    public void playSelectorClick() {
        if (!Util.wm.soundMuted) generalSource.PlayOneShot(selectorClick);
    }
}
