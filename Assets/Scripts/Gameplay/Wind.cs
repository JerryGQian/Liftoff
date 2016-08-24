using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wind : MonoBehaviour {
    public Text windText;
    public string windPrefix;

    public static float wind = 0;
    public static float maxWind = .03f;
    static float juice = 0.5f;
    static float windChangeDelay = 6f;
    static float targetWind;

    void Awake() {
        Util.wind = this;
    }

	// Use this for initialization
	void Start () {
        newTargetWind();
        Invoke("newTargetWind", Random.Range(windChangeDelay * 0.5f, windChangeDelay));
        setMaxWind(0);
        windPrefix = "WIND ";
	}
	
	// Update is called once per frame
	void Update () {
        if (Util.wm.gameActive) {
            wind += (targetWind - wind) * Time.deltaTime * juice;
            if (maxWind != 0) {
                windText.text = windPrefix + (int)(wind * 500f);
            }
            else {
                windText.text = "";
            }
            if (Util.wm.godmode) wind = 0;
        }
        else {
            windText.text = "";
        }
	}

    public static void setMaxWind(float m) {
        maxWind = m;
        Util.wind.newTargetWind();
    }

    void newTargetWind() {
        do {
            targetWind = Random.Range(-maxWind, maxWind);
        }
        while (Mathf.Abs(targetWind) < maxWind * 0.2f);
        CancelInvoke("newTargetWind");
        Invoke("newTargetWind", Random.Range(windChangeDelay * 0.5f, windChangeDelay));
    }
}
