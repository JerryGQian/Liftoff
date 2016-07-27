using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {
    public GameObject rocket;
    public float gameTime = 0;
    public GameObject gm;
    public bool gameActive = false;
    public bool dieScreen = false;
    public float clickTime;

    public float best;
    public int coins;
    public float totalDistance;
    public int attempts = 0;

    public bool muted = false;
    public int controlScheme = 0;
    public bool hasCheated = false;
    void Awake() {
        Util.wm = this;
    }
	// Use this for initialization
	void Start () {
        gameTime = 0;
        Util.cm.cameraTargetSize = 8f;

        Util.saveManager.load();
	}
	
	// Update is called once per frame
	void Update () {
        if (gameActive) {
            gameTime += Time.deltaTime;
        }
	}

    public void play() {
        if (!gameActive) {
            clickTime = Time.time;
            gameActive = true;
            dieScreen = false;
            gameTime = 0;
            attempts++;

            Util.gm.play();
        }
    }
}
