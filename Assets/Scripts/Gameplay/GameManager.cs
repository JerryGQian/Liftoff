using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static float distance;
    public static float scoreSpeed;
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
    }
}
