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
        obstacles.Add(Instantiate(obstaclePrefab));
        Invoke("spawnObstacle", Random.Range(2f / zoneID, 5f / zoneID));
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
        int prevZoneID = zoneID;
        zoneID = 1 + (int)(distance / zoneSize);
        switch (zoneID) {
            case 1: zoneName = "Troposphere"; break;
            case 2: zoneName = "Stratosphere"; break;
            case 3: zoneName = "Mesosphere"; break;
            case 4: zoneName = "Geostationary Orbit"; break;
            case 5: zoneName = "Lunar Orbit"; break;
            case 6: zoneName = "Martian Orbit"; break;
            case 7: zoneName = "Asteroid Belt"; break;
            case 8: zoneName = "Jupiter Orbit"; break;
            case 9: zoneName =  "Saturn Orbit"; break;
            case 10: zoneName = "Uranus Orbit"; break;
            case 11: zoneName = "Neptune Orbit"; break;
            case 12: zoneName = "Kuiper Belt"; break;
            case 13: zoneName = "Pluto Orbit"; break;
            case 14: zoneName = "Heliosphere"; break;
            case 15: zoneName = "Oort Cloud"; break;
            case 16: zoneName = "Interstellar Space"; break;
            case 17: zoneName = "Alpha Centauri"; break;
            case 18: zoneName = "Interstellar Space"; break;
            case 19: zoneName = ""; break;
            case 20: zoneName = ""; break;
            case 21: zoneName = ""; break;
            case 22: zoneName = ""; break;
            case 23: zoneName = ""; break;
            case 24: zoneName = ""; break;
            case 25: zoneName = ""; break;
            case 26: zoneName = ""; break;
            case 27: zoneName = ""; break;
            case 28: zoneName = ""; break;
            case 29: zoneName = ""; break;
            case 30: zoneName = ""; break;
            case 31: zoneName = ""; break;
            case 32: zoneName = ""; break;
            case 33: zoneName = ""; break;
            case 34: zoneName = ""; break;
            case 35: zoneName = ""; break;
            case 36: zoneName = ""; break;
            case 37: zoneName = ""; break;
            case 38: zoneName = ""; break;
            case 39: zoneName = ""; break;
            case 40: zoneName = ""; break;
            case 41: zoneName = ""; break;
            case 42: zoneName = ""; break;
            case 43: zoneName = ""; break;
            case 44: zoneName = ""; break;
            case 45: zoneName = ""; break;
            case 46: zoneName = ""; break;
            case 47: zoneName = ""; break;
            case 48: zoneName = ""; break;
            case 49: zoneName = ""; break;
            case 50: zoneName = ""; break;
            case 51: zoneName = ""; break;
            default: zoneName = "Space"; break;
        }

        if (prevZoneID < zoneID) {
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
}
