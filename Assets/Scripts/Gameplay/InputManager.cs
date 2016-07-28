using UnityEngine;
using System.Collections;

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
                case 0:
                    {
                        if (Input.touchCount >= 1) {
                            processScreenPos(Input.GetTouch(0).position);
                        }
                        break;
                    }
                case 1: processAccelerometer(); break;
            }
            
        }
        else {
            processScreenPos(Input.mousePosition);
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
        angleRatio = (Input.acceleration.x) / (0.5f);
        if (Mathf.Abs(angleRatio) > 1f) {
            angleRatio = 1f * Mathf.Sign(angleRatio);
        }
    }
}
