using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {
    public Text controlButtonText;
    public Image controlButton;

    public Text musicButtonText;
    public Image musicButton;

    public Text sfxButtonText;
    public Image sfxButton;

    public Text smButtonText;
    public Image smButton;

    public Image closeButton;

    Color onColor;
    Color offColor;

    public SmoothMotion sm1;
    public SmoothMotion sm2;
    
	// Use this for initialization
	void Start () {
        transform.SetParent(Util.canvas.transform);
        transform.localScale = new Vector3(1f, 1f, 1f);
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 1500f, 0f);

        sm1.begin();

        onColor = closeButton.color;
        offColor = smButton.color;

        switch (Util.wm.controlScheme) {
            case ControlScheme.tilt:
                {
                    controlButtonText.text = "TILT";
                    break;
                }
            case ControlScheme.tiltInvert:
                {
                    controlButtonText.text = "TILT INV";
                    break;
                }
            case ControlScheme.touch:
                {
                    controlButtonText.text = "TOUCH";
                    break;
                }
            case ControlScheme.touchInvert:
                {
                    controlButtonText.text = "TOUCH INV";
                    break;
                }
        }

        if (Util.wm.musicMuted) {
            setOff(musicButtonText, musicButton);
        }
        else {
            setOn(musicButtonText, musicButton);
        }

        if (Util.wm.soundMuted) {
            setOff(sfxButtonText, sfxButton);
        }
        else {
            setOn(sfxButtonText, sfxButton);
        }

        if (Util.wm.scienceMode) {
            setOn(smButtonText, smButton);
        }
        else {
            setOff(smButtonText, smButton);
        }

    }

    void setOn(Text txt, Image im) {
        txt.text = "ON";
        im.color = onColor;
    }
    void setOff(Text txt, Image im) {
        txt.text = "OFF";
        im.color = offColor;
    }

    public void nextControl() {
        Util.wm.controlsChanged = 2;
        switch (Util.wm.controlScheme) {
            case ControlScheme.tilt:
                {
                    Util.wm.controlScheme = ControlScheme.tiltInvert;
                    controlButtonText.text = "TILT INV";
                    break;
                }
            case ControlScheme.tiltInvert:
                {
                    Util.wm.controlScheme = ControlScheme.touch;
                    controlButtonText.text = "TOUCH";
                    break;
                }
            case ControlScheme.touch:
                {
                    Util.wm.controlScheme = ControlScheme.touchInvert;
                    controlButtonText.text = "TOUCH INV";
                    break;
                }
            case ControlScheme.touchInvert:
                {
                    Util.wm.controlScheme = ControlScheme.tilt;
                    controlButtonText.text = "TILT";
                    break;
                }
        }
    }


    public void toggleMusic() {
        if (!Util.wm.musicMuted) {
            setOff(musicButtonText, musicButton);
            Util.wm.musicMuted = true;
        }
        else {
            setOn(musicButtonText, musicButton);
            Util.wm.musicMuted = false;
        }
    }

    public void toggleSound() {
        if (!Util.wm.soundMuted) {
            setOff(sfxButtonText, sfxButton);
            Util.wm.soundMuted = true;
        }
        else {
            setOn(sfxButtonText, sfxButton);
            Util.wm.soundMuted = false;
        }
    }

    public void toggleScience() {
        if (!Util.wm.scienceMode) {
            setOn(smButtonText, smButton);
            Util.wm.scienceMode = true;
        }
        else {
            setOff(smButtonText, smButton);
            Util.wm.scienceMode = false;
        }
    }

    public void rate() {

    }

    public void credits() {

    }
    

    public void close() {
        sm2.startPos = GetComponent<RectTransform>().anchoredPosition;
        sm2.begin();
    }
}
