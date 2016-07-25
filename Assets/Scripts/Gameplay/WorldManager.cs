using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public GameObject rocket;
    public float gameTime = 0;
    public GameObject gm;
    public bool gameActive = false;
    public float clickTime;
    void Awake() {
        Util.wm = this;
    }
	// Use this for initialization
	void Start () {
        gameTime = 0;
        Util.cm.cameraTargetSize = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameActive) {
            gameTime += Time.deltaTime;
        }
	}

    public void play() {
        clickTime = Time.time;
        gameActive = true;
        gameTime = 0;
        Util.gm.play();
    }
}
