using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public float distance;
    public static float scoreSpeed = 10f;

    public GameObject plumePrefab;
    GameObject plume;

    public GameObject debrisPrefab;
    GameObject debris;

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
        Wind.setMaxWind(0.03f);
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
    }


    public void die(string reason) {
        Util.wm.gameActive = false;
        Util.wm.dieScreen = true;
        Invoke("showFailScreen", 1.5f);
        Util.cm.cameraTargetSize = 8f;

        Wind.setMaxWind(0);
        Wind.wind = 0;

        //spawn explosion+debris
        debris = Instantiate(debrisPrefab);
        debris.transform.position = Util.rocket.transform.position + new Vector3(0, 0.5f, 0);
        debris.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void die() {
        die("");
    }

    void showFailScreen() {
        Util.wm.dieScreen = false;
        
        Camera.main.transform.position = new Vector3(0, 0, -10f);

        Util.wm.rocket.transform.position = new Vector3(0, 0, 0);
        Util.wm.rocket.transform.eulerAngles = new Vector3(0, 0, 90f);

        Destroy(plume);
        Destroy(debris);

        Util.menuManager.showReplayMenu();

    }
}
