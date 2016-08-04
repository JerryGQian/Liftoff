using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
    public Vector3 cameraTarget;
    public float cameraTargetSize;
    public static float cameraJuice = 1f;

    void Awake() {
        Util.cm = this;
    }

    // Use this for initialization
    void Start() {
        cameraTarget = new Vector3(0, 0, -10f);
        Camera.main.transform.position = cameraTarget;
        Camera.main.orthographicSize = 8f;
    }

    // Update is called once per frame
    void Update() {
        if (Util.wm.gameActive || Util.wm.dieScreen) {
            cameraTarget = new Vector3(0, Util.rocket.bottomPos.y + 15f - Mathf.Sin(Util.wm.gameTime), -10f);
        }
        else {
            cameraTarget = new Vector3(0, 0, -10f);
        }
        Camera.main.transform.position += (cameraTarget - Camera.main.transform.position) * cameraJuice * Time.deltaTime;
        Camera.main.orthographicSize += (cameraTargetSize - Camera.main.orthographicSize) * cameraJuice * Time.deltaTime;
    }
}
