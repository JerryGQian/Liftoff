using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float distance;
    public int zoneID;
    public string zoneName;

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
    int physicsCloudCount = 18;
    int cloudCount = 4;
    int coverCloudCount = 7;

    public GameObject coinPrefab;
    ArrayList coins;

    public GameObject zoneTextPrefab;
    GameObject zoneText;

    public GameObject obstaclePrefab;
    public GameObject asteroidPrefab;
    ArrayList obstacles;

    public GameObject planetBGPrefab;
    GameObject planetBG;

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
        Wind.setMaxWind(0);
        Util.cm.cameraTargetSize = Util.wm.cameraSizePlay;

        Util.rocket.setup(Util.rocketHolder.getRocket(ScrollManager.selectedRocket));

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
        InvokeRepeating("spawnCoins", 1f, 10f);
        InvokeRepeating("updateZone", 0.05f, zoneTime);
        Invoke("spawnObstacle", zoneTime);
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
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            clouds.Add(obj);
        }
        for (int i = 0; i < cloudCount; i++) {
            obj = Instantiate(cloudPrefab);
            obj.transform.position = new Vector3(Random.Range(-6f, 6f), Random.Range(3f * rocketSpeed, zoneTime * rocketSpeed));
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
        int num = (int)Random.Range(2f, 5.99f);
        GameObject obj;
        for (int i = 0; i < num; i++) {
            obj = Instantiate(coinPrefab);
            obj.transform.localScale = new Vector3(.3f, .3f, .3f);
            obj.transform.position = new Vector3(Random.Range(-4.5f, 4.5f), Random.Range(Util.rocket.transform.position.y + 30f, Util.rocket.transform.position.y + rocketSpeed * 10f));
            coins.Add(obj);
        }
    }

    void spawnObstacle() {
        if (Random.Range(0, 100f) < zoneID * 1.5f + 10f) {
            spawnAsteroid();
        }
        else {
            obstacles.Add(Instantiate(obstaclePrefab));
            Invoke("spawnObstacle", Random.Range(3f / zoneID, 6f / zoneID));
        }
    }

    void spawnAsteroid() {
        obstacles.Add(Instantiate(asteroidPrefab));
        Invoke("spawnObstacle", Random.Range(4f / zoneID, 9f / zoneID));
    }





    //Death

    public void die(string reason) {
        

        Util.wm.gameActive = false;
        Util.wm.dieScreen = true;
        Invoke("showFailScreen", 1.5f);
        Invoke("resetRocket", 1.5f);
        Util.cm.cameraTargetSize = 8f;

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

        if (distance > Util.wm.best) {
            Util.wm.best = distance;
            WorldManager.updateBest();
            newBest();
        }

        Util.saveManager.save();

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
        
        Camera.main.transform.position = new Vector3(0, 0, -10f);
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
            planetBG = Instantiate(planetBGPrefab);
        }
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
