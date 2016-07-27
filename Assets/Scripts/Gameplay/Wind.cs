using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Wind : MonoBehaviour {
    public Text windText;

    public static float wind = 0;
    static float maxWind = .03f;
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
	}
	
	// Update is called once per frame
	void Update () {
        wind += (targetWind - wind) * Time.deltaTime * juice;
        windText.text = "WIND " + (int)(wind * 500f);
	}

    public static void setMaxWind(float m) {
        maxWind = m;
        Util.wind.newTargetWind();
    }

    void newTargetWind() {
        do {
            targetWind = Random.Range(-maxWind, maxWind);
        }
        while (Mathf.Abs(targetWind) > maxWind * 0.3f);
        CancelInvoke("newTargetWind");
        Invoke("newTargetWind", Random.Range(windChangeDelay * 0.5f, windChangeDelay));
    }
}
