using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Vector3 cameraTarget;
    public float cameraTargetSize;

    void Awake() {
        Util.cm = this;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Util.wm.gameActive) {
            cameraTarget = new Vector3(0, Util.rocket.bottomPos.y + 10f - Mathf.Sin(Util.wm.gameTime), -10f);
        }
        else {
            cameraTarget = new Vector3(0, 0, -10f);
        }
        Camera.main.transform.position += (cameraTarget - Camera.main.transform.position) * Time.deltaTime;
        Camera.main.orthographicSize += (cameraTargetSize - Camera.main.orthographicSize) * Time.deltaTime;
    }
}
