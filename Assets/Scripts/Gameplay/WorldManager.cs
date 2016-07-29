using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {
    public GameObject rocket;
    public Text coinCounter;
    public Text bestScore;
    public GameObject bestBar;
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
        Util.coin = GameObject.Find("Coin").GetComponent<RectTransform>();
        Util.canvas = GameObject.Find("Canvas");
    }
	// Use this for initialization
	void Start () {
        gameTime = 0;
        Util.cm.cameraTargetSize = 8f;

        Util.saveManager.load();
        updateCoinCount();
        updateBest();
        controlScheme = 1;
        //Util.scrollManager.spawnShowcase();
        Util.scrollManager.setRocket();
        Util.rocket.transform.position = new Vector3(0, -100f, 0);
        
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameActive) {
            gameTime += Time.deltaTime;
        }

        Util.even = !Util.even;
        if (Util.even) {
            Util.even2 = !Util.even2;
        }
	}

    public void play() {
        if (!gameActive) {
            clickTime = Time.time;
            gameActive = true;
            dieScreen = false;
            gameTime = 0;
            attempts++;
            Util.wm.bestBar.transform.position = new Vector3(0, Util.wm.best / GameManager.scoreSpeed * GameManager.rocketSpeed - 5f, 0);
            Util.gm.play();
        }
    }

    public static void updateCoinCount() {
        Util.wm.coinCounter.text = "" + Util.wm.coins;
    }

    public static void updateBest() {
        Util.wm.bestScore.text = "" + (int)Util.wm.best;
        
    }
}
