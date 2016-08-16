using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float distance;
    public int zoneID;
    public string zoneName;
    public Vector3 diePos;
    public bool invincible = false;
    public static float invincibleTime = 4f;
    int invincibleCountdown = 0;
    bool firstTry;

    public static float scoreSpeed = 15f;
    public static float rocketSpeed = 10f;
    public static float zoneSize = 100f;
    public static float zoneTime;

    public GameObject plumePrefab;
    GameObject plume;

    public GameObject debrisPrefab;
    GameObject debris;

    public GameObject explosionPrefab;
    GameObject explosion;

    public GameObject physicsCloudPrefab;
    public GameObject cloudPrefab;
    public ArrayList clouds;
    int physicsCloudCount = 30;
    int cloudCount = 4;
    int coverCloudCount = 10;

    public GameObject coinPrefab;
    ArrayList coins;

    public GameObject zoneTextPrefab;
    GameObject zoneText;

    public GameObject obstaclePrefab;
    public GameObject asteroidPrefab;
    public GameObject planePrefab;
    ArrayList obstacles;

    public GameObject planetBGPrefab;
    GameObject planetBG;
    public int asteroidBGCount = 0;

    public GameObject countdownPrefab;
    GameObject countdown;

	// Use this for initialization
	void Start () {
        Util.gm = this;
        zoneTime = zoneSize / scoreSpeed;
        clouds = new ArrayList();
        coins = new ArrayList();
        obstacles = new ArrayList();
    }
	
	// Update is called once per frame
	void Update () {
        if (Util.wm.gameActive) {
            if (Util.wm.gameTime < 2f) {
                Util.wm.rocket.transform.position = Util.wm.rocket.transform.position + new Vector3(0, Util.wm.gameTime / 2f * rocketSpeed * Time.deltaTime);
            }
            else {
                Util.wm.rocket.transform.position = Util.wm.rocket.transform.position + new Vector3(0, rocketSpeed * Time.deltaTime);
            }
            distance = distance + scoreSpeed * Time.deltaTime;
            Util.menuManager.updateScore((int)distance);
            if (distance > Util.wm.best) {
                Util.wm.best = distance;
                WorldManager.updateBest();
            }
        }
    }

    public void play() {
        Util.wm.rocket.SetActive(true);
        Util.wm.gameActive = true;
        Util.wm.dieScreen = false;
        Util.wm.gameTime = 0;
        invincible = false;
        firstTry = true;

        Util.rocket.tipRateActual = Util.rocket.tipRate;
        Util.rocket.engineForceActual = Util.rocket.engineForce;

        Wind.setMaxWind(0);
        Util.cm.cameraTargetSize = Util.wm.cameraSizePlay;

        Util.rocket.setup(Util.rocketHolder.getRocket(ScrollManager.selectedRocket));
        Util.wm.rocket.transform.FindChild("Rocket").gameObject.GetComponent<Animator>().SetTrigger("stop");

        Camera.main.GetComponent<Animator>().SetTrigger("Darken");
        Camera.main.transform.position = new Vector3(0, 0, -10);

        Util.wm.rocket.transform.position = new Vector3(0, 0, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);

        Util.nozzle.spew();

        Destroy(plume);
        plume = Instantiate(plumePrefab);
        plume.transform.position = new Vector3(0, 3.62f, 0);

        spawnClouds();

        distance = 0;
        zoneID = 0;
        zoneName = "";

        CancelInvoke("showFailScreen");

        //////clean menus
        Util.menuManager.showPlayScreen();
        Invoke("increaseWind0", 3f);
        Invoke("increaseWind1", zoneTime * 1.5f);
        InvokeRepeating("spawnCoins", 1f, zoneTime);
        InvokeRepeating("updateZone", 0.05f, zoneTime);
        Invoke("spawnObstacle", zoneTime * 1.7f);

        Invoke("spawnPlane", Random.Range(1f, 2.5f));
        Invoke("spawnPlane", Random.Range(zoneTime + 0.5f, zoneTime + 1.3f));
        Invoke("spawnPlane", Random.Range(zoneTime + 1.5f, zoneTime + 4f));
        Invoke("spawnPlane", Random.Range(zoneTime + 3.5f, zoneTime + 8f));
    }

    public void restart() {
        Util.wm.rocket.GetComponent<Motion>().end();
        Util.wm.rocket.SetActive(true);

        Util.wm.gameActive = true;
        Util.wm.dieScreen = false;
        invincible = true;
        firstTry = false;
        Wind.setMaxWind(0);

        Util.wm.rocket.transform.position = diePos - new Vector3(0, rocketSpeed * invincibleTime, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);

        invincibleCountdown = (int)(invincibleTime + 0.0001f) + 1;
        setCountdown();

        Util.nozzle.spew();

        Util.rocket.tipRateActual = Util.rocket.tipRate * 0.0f;
        Util.rocket.engineForceActual = Util.rocket.engineForce * 0.0f;
        Util.rocket.tipAmount = 0;
        Util.rocket.enginePush = 0;
        Util.rocket.finalAngle = 0;

        

        Camera.main.GetComponent<Animator>().SetTrigger("Black");
        Camera.main.transform.position = diePos + new Vector3(0, rocketSpeed * -0.5f * invincibleTime, -10f);
        Util.cm.cameraTargetSize = Util.wm.cameraSizePlay;

        Util.menuManager.showPlayScreen();


        Invoke("increaseWind1", invincibleTime);
        InvokeRepeating("spawnCoins", invincibleTime, zoneTime);
        float delay = (zoneTime - Util.wm.gameTime % zoneTime) + 0.05f;
        InvokeRepeating("updateZone", delay, zoneTime);
        Invoke("spawnObstacle", invincibleTime - 0.3f);
        Invoke("uninvincible", invincibleTime);

        CancelInvoke("showFailScreen");
        CancelInvoke("resetRocket");

        Util.wm.rocket.transform.FindChild("Rocket").gameObject.GetComponent<Animator>().SetTrigger("pulse");
    }

    void setCountdown() {
        invincibleCountdown--;
        if (countdown == null) {
            countdown = Instantiate(countdownPrefab);
            countdown.transform.SetParent(Util.canvas.transform);
            countdown.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 459f);
            countdown.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
        countdown.GetComponent<Text>().text = "" + invincibleCountdown;
        if (invincibleCountdown <= 0) {
            Destroy(countdown);
        }
        else {
            Invoke("setCountdown", 1f);
        }
    }

    void uninvincible() {
        Util.wm.rocket.transform.FindChild("Rocket").gameObject.GetComponent<Animator>().SetTrigger("stop");
        invincible = false;
        Util.rocket.tipRateActual = Util.rocket.tipRate;
        Util.rocket.engineForceActual = Util.rocket.engineForce;

        //Util.wm.rocket.transform.position = diePos;
        //Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);
    }

    void increaseWind0() {
        Wind.setMaxWind(0.03f);
    }
    void increaseWind1() {
        Wind.setMaxWind(0.07f);
    }

    void spawnClouds() {
        GameObject obj;
        for (int i = 0; i < physicsCloudCount; i++) {
            obj = Instantiate(physicsCloudPrefab);
            obj.transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(zoneTime * rocketSpeed - 0.5f, zoneTime * rocketSpeed + 0.5f));
            float scale = Random.Range(0.35f, 0.65f);
            obj.transform.localScale = new Vector3(scale, scale, 1f);
            clouds.Add(obj);
        }
        for (int i = 0; i < cloudCount; i++) {
            obj = Instantiate(cloudPrefab);
            obj.transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(2f * rocketSpeed, zoneTime * rocketSpeed));
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            clouds.Add(obj);
        }
        for (int i = 0; i < coverCloudCount; i++) {
            obj = Instantiate(cloudPrefab);
            obj.transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(zoneTime * rocketSpeed - 0.5f, zoneTime * rocketSpeed + 0.5f));
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            obj.GetComponent<SpriteRenderer>().sortingOrder = 100;
            clouds.Add(obj);
        }
    }

    void spawnCoins() {
        int num = (int)Random.Range(0f, 2.99f);
        GameObject obj;
        for (int i = 0; i < num; i++) {
            obj = Instantiate(coinPrefab);
            obj.transform.localScale = new Vector3(.3f, .3f, .3f);
            obj.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(Util.rocket.transform.position.y + 15f, Util.rocket.transform.position.y + rocketSpeed * zoneTime + 15f));
            coins.Add(obj);
        }
        //spawn pattern
        obj = Instantiate(Util.coinPatternHolder.getCoinPattern());
        //obj.transform.localScale = new Vector3(.3f, .3f, .3f);
        obj.transform.position = new Vector3(Random.Range(-3.3f, 3.3f), Random.Range(Util.rocket.transform.position.y + 20f, Util.rocket.transform.position.y + rocketSpeed * zoneTime));
        coins.Add(obj);
    }

    void spawnObstacle() {
        float difficulty = 1f;
        if (zoneID <= 12) {
            difficulty = 4f / zoneID;
        }
        else {
            difficulty = 4f / 12f;
        }
        if (Random.Range(0, 100f) < zoneID * 1.5f + 10f) {
            spawnAsteroid();
            Invoke("spawnObstacle", Random.Range(difficulty, difficulty * 2f));
        }
        else if (Random.Range(0, 100f) < zoneID * 0.6f + 2f) {
            spawnPlane();
            Invoke("spawnObstacle", Random.Range(difficulty, difficulty * 2f));
        }
        else {
            obstacles.Add(Instantiate(obstaclePrefab));
            Invoke("spawnObstacle", Random.Range(difficulty * 0.9f, difficulty * 1.8f));
        }
    }

    void spawnAsteroid() {
        obstacles.Add(Instantiate(asteroidPrefab));
    }

    void spawnPlane() {
        obstacles.Add(Instantiate(planePrefab));
    }





    //Death

    public void die(string reason) {
        if (Util.wm.godmode || (distance > 300f && (Util.wm.adWatchTimeLife <= 0 && Util.wm.gamesSinceAdWatch > Util.adLifeMinGames) && Random.Range(0, 100f) < 35f && firstTry)) {
            Util.wm.showSecondChance();
        }

        Util.wm.rocket.transform.FindChild("Rocket").gameObject.GetComponent<Animator>().SetTrigger("stop");

        Util.wm.gameActive = false;
        Util.wm.dieScreen = true;
        Invoke("showFailScreen", 1.5f);
        Invoke("resetRocket", 1.5f);
        Util.cm.cameraTargetSize = Util.wm.cameraSizeMenu;

        Wind.setMaxWind(0);
        Wind.wind = 0;

        //spawn explosion+debris
        debris = Instantiate(debrisPrefab);
        debris.transform.position = Util.rocket.transform.position + new Vector3(0, 1f, 0);
        debris.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        debris.transform.SetParent(Util.wm.rocket.transform);

        explosion = Instantiate(explosionPrefab);
        explosion.transform.position = Util.rocket.transform.position + new Vector3(0, 2.25f, 0);
        explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        explosion.transform.SetParent(Util.wm.rocket.transform);

        Util.wm.rocket.GetComponent<Motion>().endPos = Util.rocket.transform.position + new Vector3(0, rocketSpeed * 0.75f);
        Util.wm.rocket.GetComponent<Motion>().begin();

        CancelInvoke("increaseWind0");
        CancelInvoke("increaseWind1");
        CancelInvoke("spawnCoins");
        CancelInvoke("spawnObstacle");
        CancelInvoke("spawnAsteroidBG");
        CancelInvoke("spawnPlane");
        CancelInvoke("updateZone");

        diePos = new Vector3(0, Util.wm.rocket.transform.position.y, 0);

        if (distance > Util.wm.best) {
            Util.wm.best = distance;
            WorldManager.updateBest();
            newBest();
        }

        Util.saveManager.save();

        //Util.wm.rocket.transform.FindChild("Rocket").gameObject.GetComponent<Explodable>().explode();
    }

    public void die() {
        die("");
    }

    void newBest() {
        //YAY! NEW BEST!
    }

    public void resetRocket() {
        Util.wm.rocket.transform.position = new Vector3(0, -100f, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);
        Util.wm.rocket.SetActive(false);
    }

    void removeClouds() {
        foreach (GameObject obj in clouds) {
            Destroy(obj);
        }
        clouds = new ArrayList();
    }

    void removeCoins() {
        foreach (GameObject obj in coins) {
            Destroy(obj);
        }
        coins = new ArrayList();
    }

    void removeObstacles() {
        foreach (GameObject obj in obstacles) {
            Destroy(obj);
        }
        obstacles = new ArrayList();
    }

    void showFailScreen() {
        Util.wm.dieScreen = false;
        
        Camera.main.transform.position = new Vector3(0, -0.75f, -10f);
        Camera.main.GetComponent<Animator>().SetTrigger("Blue");

        Destroy(plume);
        Destroy(debris); 
        Destroy(explosion);
        Destroy(zoneText);
        Destroy(planetBG);
        removeClouds();
        removeCoins();
        removeObstacles();
        Util.rocket.bottomPos = Vector3.zero;
        Util.menuManager.showReplayMenu();

        Util.wind.windPrefix = "WIND ";

    }

    void updateZone() {
        string prevZoneName = zoneName;
        zoneID = 1 + (int)(distance / zoneSize);
        zoneName = getZoneName();

        if (!prevZoneName.Equals(zoneName)) {
            if (zoneText != null) {
                zoneText.GetComponent<ZoneText>().remove();
            }
            zoneText = Instantiate(zoneTextPrefab);
            zoneText.GetComponent<ZoneText>().setText(zoneName);
            if (zoneID < 4) {
                Util.wind.windPrefix = "WIND ";
            }
            else {
                Util.wind.windPrefix = "GRAVITY ";
            }
        }
        asteroidBGCount = 0;
        spawnAsteroidBG();

        if (zoneID == 7 || zoneID == 12) {
            int max = (int)Random.Range(10f, 15f);
            while (asteroidBGCount < max) {
                Invoke("spawnAsteroidBG", Random.Range(0, zoneTime * 0.8f));
                asteroidBGCount++;
            }
        }
        if (zoneID == 3) {
            Invoke("spawnAsteroidBG", Random.Range(0.35f, 0.7f));
            if (Random.Range(-1f, 1f) < 0) Invoke("spawnAsteroidBG", Random.Range(0.4f, 1.4f));
        }
    }

    void spawnAsteroidBG() {
        obstacles.Add(Instantiate(planetBGPrefab));
    }

    string getZoneName() {
        if (Util.wm.scienceMode) {
            switch (zoneID) {
                case 1: return "Troposphere";
                case 2: return "Stratosphere";
                case 3: return "Mesosphere";
                case 4: return "Orbit";
                case 5: return "Lunar Orbit";
                case 6: return "Martian Orbit";
                case 7: return "Asteroid Belt";
                case 8: return "Jupiter Orbit";
                case 9: return "Saturn Orbit";
                case 10: return "Uranus Orbit";
                case 11: return "Neptune Orbit";
                case 12: return "Kuiper Belt";
                case 13: return "Pluto Orbit";
                case 14: return "Heliosphere";
                case 15: return "Oort Cloud";
                case 16: return "Interstellar Space";
                case 17: return "Alpha Centauri";
                case 18: return "Interstellar Space";
                case 19: return "";
                case 20: return "";
                case 21: return "";
                case 22: return "";
                case 23: return "";
                case 24: return "";
                case 25: return "";
                case 26: return "";
                case 27: return "";
                case 28: return "";
                case 29: return "";
                case 30: return "";
                case 31: return "";
                case 32: return "";
                case 33: return "";
                case 34: return "";
                case 35: return "";
                case 36: return "";
                case 37: return "";
                case 38: return "";
                case 39: return "";
                case 40: return "";
                case 41: return "";
                case 42: return "";
                case 43: return "";
                case 44: return "";
                case 45: return "";
                case 46: return "";
                case 47: return "";
                case 48: return "";
                case 49: return "";
                case 50: return "";
                case 51: return "";
                default: return "Space";
            }
        }
        else {
            switch (zoneID) {
                case 1: return "Atmosphere";
                case 2: return "Atmosphere";
                case 3: return "Atmosphere";
                case 4: return "Orbit";
                case 5: return "Moon";
                case 6: return "Mars";
                case 7: return "Asteroid Belt";
                case 8: return "Jupiter";
                case 9: return "Saturn";
                case 10: return "Uranus";
                case 11: return "Neptune";
                case 12: return "Kuiper Belt";
                case 13: return "Pluto";
                case 14: return "Heliosphere";
                case 15: return "Oort Cloud";
                case 16: return "Interstellar Space";
                case 17: return "Alpha Centauri";
                case 18: return "Interstellar Space";
                case 19: return "";
                case 20: return "";
                case 21: return "";
                case 22: return "";
                case 23: return "";
                case 24: return "";
                case 25: return "";
                case 26: return "";
                case 27: return "";
                case 28: return "";
                case 29: return "";
                case 30: return "";
                case 31: return "";
                case 32: return "";
                case 33: return "";
                case 34: return "";
                case 35: return "";
                case 36: return "";
                case 37: return "";
                case 38: return "";
                case 39: return "";
                case 40: return "";
                case 41: return "";
                case 42: return "";
                case 43: return "";
                case 44: return "";
                case 45: return "";
                case 46: return "";
                case 47: return "";
                case 48: return "";
                case 49: return "";
                case 50: return "";
                case 51: return "";
                default: return "Space";
            }
        }
        return "";
    }
}
