using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;

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
    public float cameraSizeMenu;
    public Vector3 cameraMenuPosition;
    public RectTransform coinIcon;

    public string zoneID;


    public float best;
    public int coins;
    public float totalDistance;
    public int attempts = 0;
    public float adWatchTimeCoins = 0;
    public float adWatchTimeLife = 0;
    public int gamesSinceAdWatch = 6;

    public bool musicMuted = false;
    public bool soundMuted = false;
    public bool scienceMode = false;
    public ControlScheme controlScheme = 0;
    public bool hasCheated = false;
    public bool godmode = false;
    public int lastLaunched = 1;

    public GameObject settingsPrefab;
    GameObject settings;

    public GameObject IAPPrefab;
    GameObject IAP;

    public GameObject secondChancePrefab;
    GameObject secondChance;

    public bool alternate = true;

    void Awake() {
        Util.wm = this;
        Util.coin = GameObject.Find("Coin").GetComponent<RectTransform>();
        Util.canvas = GameObject.Find("Canvas");
        cameraSizePlay = 10f * ((Screen.height * 1f / Screen.width) / 1.7777f);
        cameraSizeMenu = 8.5f;
        cameraMenuPosition = new Vector3(0, -1f, -10f);

        zoneID = "rewardedVideo";
    }
    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 50;

        
        gameTime = 0;
        Util.cm.cameraTargetSize = cameraSizeMenu;
        Camera.main.transform.position = cameraMenuPosition;
        Util.saveManager.load();
        updateCoinCount();
        updateBest();
        //Util.scrollManager.spawnShowcase();
        Util.scrollManager.setRocket();
        //Util.rocket.transform.position = new Vector3(0, -100f, 0);
        Util.width = Camera.main.GetComponent<BoxCollider2D>().size.x / 2f;
        Util.even10 = true;

        //Advertisement.Initialize();

        InvokeRepeating("toggleEven", 0.25f, 0.25f);
        InvokeRepeating("everySecond", 1f, 1f);

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

    void everySecond() {
        adWatchTimeCoins--;
        adWatchTimeLife--;

        if (adWatchTimeCoins < 0) {
            adWatchTimeCoins = 0;
        }
    }

    public void play() {
        if (!gameActive) {
            if (Util.rocketHolder.purchased[ScrollManager.selectedRocket] || ScrollManager.selectedRocket == 0 || godmode) {
                if (secondChance != null) secondChance.GetComponent<SecondChance>().close();
                gamesSinceAdWatch++;
                attempts++;
                if (best > 25f) Util.wm.bestBar.transform.position = new Vector3(0, Util.wm.best / GameManager.scoreSpeed * GameManager.rocketSpeed - 5f, 0);
                lastLaunched = ScrollManager.selectedRocket;
                Util.gm.play();
                if (settings != null) settings.GetComponent<SettingsManager>().close();
                if (IAP != null) IAP.GetComponent<SettingsManager>().close();
            }
            else {
                CancelInvoke("play");
                ScrollManager.selector = lastLaunched;
                Util.scrollManager.setClosestRocket();
                Invoke("play", 0.75f);
            }
        }
    }

    public void showSettings() {
        if (settings == null) {
            settings = Instantiate(settingsPrefab);
            if (IAP != null) IAP.GetComponent<IAP>().close();
            if (secondChance != null) secondChance.GetComponent<SecondChance>().close();
        }
        else {
            settings.GetComponent<SettingsManager>().close();
        }
    }

    public void showLeaderboards() {
        Social.ShowLeaderboardUI();
    }

    public void showIAP() {
        Debug.Log("Showing IAP");
        if (!gameActive) {
            if (IAP == null) {
                IAP = Instantiate(IAPPrefab);
                if (settings != null) settings.GetComponent<SettingsManager>().close();
                if (secondChance != null) secondChance.GetComponent<SecondChance>().close();
            }
            else {
                IAP.GetComponent<IAP>().close();
            }
        }
    }

    public void showSecondChance() {
        secondChance = Instantiate(secondChancePrefab);
        if (IAP != null) IAP.GetComponent<IAP>().close();
        if (settings != null) settings.GetComponent<SettingsManager>().close();
    }


    public void leftArrow() {
        ScrollManager.selector--;
        Util.scrollManager.setClosestRocket();
        ScrollManager.selector = ScrollManager.selectedRocket;
        Util.scrollManager.moveBG();
    }

    public void rightArrow() {
        ScrollManager.selector++;
        Util.scrollManager.setClosestRocket();
        ScrollManager.selector = ScrollManager.selectedRocket;
        Util.scrollManager.moveBG();
    }

    public static void updateCoinCount() {
        Util.wm.coinCounter.text = "" + Util.wm.coins;
        int length = Util.wm.coinCounter.text.Length;
        Util.wm.plusIcon.GetComponent<RectTransform>().localPosition = new Vector3(-140f - (length - 1) * 44f, 0, 0);
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

    public void freeMoney() {
        if (true || Application.platform == RuntimePlatform.WindowsEditor) {
            coins += 100;
            WorldManager.updateCoinCount();
            //Util.saveManager.save();
        }
    }

    public void buy() {
        if (!gameActive && !dieScreen && !Util.scrollManager.ri.purchased) {
            if (coins >= Util.scrollManager.ri.cost) {
                coins -= Util.scrollManager.ri.cost;
                Util.rocketHolder.purchased[ScrollManager.selectedRocket] = true;
                Util.saveManager.save();
                WorldManager.updateCoinCount();
            }
            else {

            }
        }
    }

    void OnApplicationQuit() {
        Util.saveManager.save();
    }
}
