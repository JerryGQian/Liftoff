using UnityEngine;
using System.Collections;
using System;

[Serializable]
public enum ControlScheme { tilt, tiltInvert, touch, touchInvert, tilt2, other };

public class InputManager : MonoBehaviour {
    public float angleRatio = 0;

    private float activeScreenPercentage = 0.8f;
    private float middleX;
    private float middleY;
    void Awake() {
        Util.im = this;
    }

	// Use this for initialization
	void Start () {
        middleX = Screen.width / 2f;
        middleY = Screen.height / 2f;
	}
	
	// Update is called once per frame
	void Update () {
        if (Application.platform != RuntimePlatform.WindowsEditor) {
            switch (Util.wm.controlScheme) {
                case ControlScheme.touch:
                    {
                        if (Input.touchCount >= 1) {
                            processScreenPos(Input.GetTouch(0).position);
                        }
                        break;
                    }
                case ControlScheme.touchInvert:
                    {
                        if (Input.touchCount >= 1) {
                            processScreenPos(Input.GetTouch(0).position);
                            angleRatio *= -1f;
                        }
                        break;
                    }
                case ControlScheme.tilt: processAccelerometer(); break;
                case ControlScheme.tiltInvert: processAccelerometer(); angleRatio *= -1f; break;
            }
            
        }
        else {
            processScreenPos(Input.mousePosition);
            angleRatio *= 0.3f;
            if (Util.wm.controlScheme == ControlScheme.touchInvert || Util.wm.controlScheme == ControlScheme.tiltInvert) {
                angleRatio *= -1f;
            }
        }
	}

    void processScreenPos(Vector2 pos) {
        pos = pos - new Vector2(middleX, 0);
        angleRatio = pos.x / (middleX * activeScreenPercentage);
        if (Mathf.Abs(angleRatio) > 1f) {
            angleRatio = 1f * Mathf.Sign(angleRatio);
        }
    }

    void processAccelerometer() {
        angleRatio = (Input.acceleration.x) / (0.60f);
        if (Mathf.Abs(angleRatio) > 1f) {
            angleRatio = 1f * Mathf.Sign(angleRatio);
        }
    }
}
