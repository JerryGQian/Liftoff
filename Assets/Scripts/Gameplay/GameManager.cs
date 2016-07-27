using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float distance;
    public int zoneID;
    public string zoneName;

    public static float scoreSpeed = 15f;
    public static float zoneSize = 100f;

    public GameObject plumePrefab;
    GameObject plume;

    public GameObject debrisPrefab;
    GameObject debris;

    public GameObject explosionPrefab;
    GameObject explosion;

	// Use this for initialization
	void Start () {
        Util.gm = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (Util.wm.gameActive) {
            if (Util.wm.gameTime < 2f) {
                Util.wm.rocket.transform.position = Util.wm.rocket.transform.position + new Vector3(0, Util.wm.gameTime / 2f * 5f * Time.deltaTime);
            }
            else {
                Util.wm.rocket.transform.position = Util.wm.rocket.transform.position + new Vector3(0, 5f * Time.deltaTime);
            }
            distance = distance + scoreSpeed * Time.deltaTime;
            Util.menuManager.updateScore((int)distance);
        }
    }

    public void play() {
        Wind.setMaxWind(0);
        Util.cm.cameraTargetSize = 10f;
        Camera.main.orthographicSize = 8f;
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        Util.wm.rocket.transform.position = new Vector3(0, 0, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);

        Util.nozzle.spew();

        Destroy(plume);
        plume = Instantiate(plumePrefab);
        plume.transform.position = new Vector3(0, 3.62f, 0);

        distance = 0;

        CancelInvoke("showFailScreen");

        //////clean menus
        Util.menuManager.showPlayScreen();
        Invoke("increaseWind0", 5f);
        Invoke("increaseWind1", 15f);
    }

    void increaseWind0() {
        Wind.setMaxWind(0.03f);
    }
    void increaseWind1() {
        Wind.setMaxWind(0.07f);
    }


    public void die(string reason) {
        Util.wm.gameActive = false;
        Util.wm.dieScreen = true;
        Invoke("showFailScreen", 1.5f);
        Invoke("resetRocket", 0.5f);
        Util.cm.cameraTargetSize = 8f;

        Wind.setMaxWind(0);
        Wind.wind = 0;

        //spawn explosion+debris
        debris = Instantiate(debrisPrefab);
        debris.transform.position = Util.rocket.transform.position + new Vector3(0, 1f, 0);
        debris.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

        explosion = Instantiate(explosionPrefab);
        explosion.transform.position = Util.rocket.transform.position + new Vector3(0, 2.25f, 0);
        explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }

    public void die() {
        die("");
    }

    public void resetRocket() {
        Util.wm.rocket.transform.position = new Vector3(0, 0, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);
    }

    void showFailScreen() {
        Util.wm.dieScreen = false;
        
        Camera.main.transform.position = new Vector3(0, 0, -10f);

        Destroy(plume);
        Destroy(debris);
        Destroy(explosion);

        Util.menuManager.showReplayMenu();

    }

    void updateZone() {
        zoneID = 1 + (int)(distance / zoneSize);
        switch (zoneID) {
            case 1: zoneName = "Troposphere"; break;
            case 2: zoneName = "Stratosphere"; break;
            case 3: zoneName = "Mesosphere"; break;
            case 4: zoneName = "Low-Earth Orbit"; break;
            case 5: zoneName = "Geostationary Orbit"; break;
            case 6: zoneName = "Lunar Orbit"; break;
            case 7: zoneName = "Martian Orbit"; break;
            case 8: zoneName = "Asteroid Belt"; break;
            case 9: zoneName = "Jupiter Orbit"; break;
            case 10: zoneName = "Saturn Orbit"; break;
            case 11: zoneName = "Neptune Orbit"; break;
            case 12: zoneName = "Uranus Orbit"; break;
            case 13: zoneName = "Kuiper Belt"; break;
            case 14: zoneName = "Pluto Orbit"; break;
            case 15: zoneName = "Heliosphere"; break;
            case 16: zoneName = "Oort Cloud"; break;
            case 17: zoneName = "Interstellar Space"; break;
            case 18: zoneName = "Alpha Centauri"; break;
            case 19: zoneName = "Interstellar Space"; break;
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
    }
}
