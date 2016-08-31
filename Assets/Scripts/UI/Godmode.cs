using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Godmode : MonoBehaviour {
    public int godCounter = 0;
    TouchScreenKeyboard kb;
    public int godmodeCode;
    public int godmodeOffCode;


    public GameObject fpsPrefab;
    GameObject fps;
	// Use this for initialization
	void Start () {
        //InvokeRepeating("countDown", 2f, 2f);
        godmodeCode = 1884297456;
        godmodeOffCode = ("off").GetHashCode();
    }

    void countDown() {
        if (godCounter >= 1) {
            godCounter--;
        }
    }

    public void click() {
        if (kb == null) {
            godCounter++;
            CancelInvoke("resetCounter");
            Invoke("resetCounter", 0.4f);
            if (godCounter == 11) {
                kb = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
                Debug.Log("Console Keyboard Opened");
            }
        }
    }

    void resetCounter() {
        godCounter = 0;
    }

    void Update() {
        if (kb != null) {
            int code = kb.text.GetHashCode();
            if (code == godmodeCode) {
                Util.wm.godmode = true;
                Util.wm.hasCheated = true;
                Util.saveManager.save();
            }
            if (code == godmodeOffCode) {
                Util.wm.godmode = false;
            }
        }
    }
}
