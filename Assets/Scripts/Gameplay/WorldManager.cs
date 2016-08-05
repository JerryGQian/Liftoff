using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour {
    public GameObject rocket;
    public Text coinCounter;
    public GameObject plusIcon;
    public Text bestScore;
    public GameObject bestBar;
    public float gameTime = 0;
    public GameObject gm;
    public bool gameActive = false;
    public bool dieScreen = false;
    public float cameraSizePlay;

    public float best;
    public int coins;
    public float totalDistance;
    public int attempts = 0;

    public bool musicMuted = false;
    public bool soundMuted = false;
    public bool scienceMode = false;
    public ControlScheme controlScheme = 0;
    public bool hasCheated = false;
    public bool godmode = false;

    public GameObject settingsPrefab;
    GameObject settings;

    public bool alternate = true;

    void Awake() {
        Util.wm = this;
        Util.coin = GameObject.Find("Coin").GetComponent<RectTransform>();
        Util.canvas = GameObject.Find("Canvas");
    }
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 50;

        cameraSizePlay = 10f * ((Screen.height * 1f / Screen.width) / 1.7777f);
        gameTime = 0;
        Util.cm.cameraTargetSize = 8f;

        Util.saveManager.load();
        updateCoinCount();
        updateBest();
        //Util.scrollManager.spawnShowcase();
        Util.scrollManager.setRocket();
        //Util.rocket.transform.position = new Vector3(0, -100f, 0);
        Util.width = Camera.main.GetComponent<BoxCollider2D>().size.x / 2f;
        Util.even10 = true;
        InvokeRepeating("toggleEven", 0.25f, 0.25f);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (gameActive) {
            gameTime += Time.deltaTime;
        }

        Util.even = !Util.even;
        if (Util.even) {
            Util.even2 = !Util.even2;
            if (Util.even2) {
                Util.even3 = !Util.even3;
            }
        }
	}

    void toggleEven() {
        Util.even10 = !Util.even10;
        alternate = !alternate;
    }

    public void play() {
        if (!gameActive) {
            Util.wm.rocket.SetActive(true);
            gameActive = true;
            dieScreen = false;
            gameTime = 0;
            attempts++;
            Util.wm.bestBar.transform.position = new Vector3(0, Util.wm.best / GameManager.scoreSpeed * GameManager.rocketSpeed - 5f, 0);
            Util.gm.play();
            Destroy(settings);
        }
    }

    public void showSettings() {
        if (settings == null) {
            settings = Instantiate(settingsPrefab);
        }
        else {
            Destroy(settings);
        }
    }

    public void showIAP() {
        Debug.Log("Showing IAP");
        if (!gameActive) {
            if (settings == null) {
                settings = Instantiate(settingsPrefab);
            }
            else {
                Destroy(settings);
            }
        }
    }

    public void leftArrow() {
        ScrollManager.selector--;
        Util.scrollManager.setClosestRocket();
        ScrollManager.selector = ScrollManager.selectedRocket;
    }

    public void rightArrow() {
        ScrollManager.selector++;
        Util.scrollManager.setClosestRocket();
        ScrollManager.selector = ScrollManager.selectedRocket;
    }

    public static void updateCoinCount() {
        Util.wm.coinCounter.text = "" + Util.wm.coins;
        int length = Util.wm.coinCounter.text.Length;
        Util.wm.plusIcon.GetComponent<RectTransform>().localPosition = new Vector3(-135f - (length - 1) * 39f, 0, 0);
    }

    public static void updateBest() {
        Util.wm.bestScore.text = "" + (int)Util.wm.best;
        
    }

    public void toggleGodmode() {
        if (true || Application.platform == RuntimePlatform.WindowsEditor) {
            godmode = !godmode;
            hasCheated = true;
            Util.saveManager.save();
        }
    }

}
