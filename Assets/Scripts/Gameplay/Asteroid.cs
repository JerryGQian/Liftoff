using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {
    public GameObject alertPrefab;
    GameObject alert;
    // Use this for initialization
    void Start() {
        GetComponent<SpriteRenderer>().sprite = Util.obstacleHolder.getAsteroid(0);
        float size = Random.Range(2f * 0.9f, 2f * 1.1f);
        float xPos = 0;
        do {
            xPos = Random.Range(-Util.width, Util.width);
        }
        while (Mathf.Abs(xPos) < Util.width * 0.3f);
        transform.position = new Vector3(xPos, Camera.main.transform.position.y + 40f, 0);
        transform.localScale = new Vector3(size, size, size);
        transform.eulerAngles = new Vector3(0, 0, 0);

        //alert = Instantiate(alertPrefab);
        //alert.GetComponent<Alert>().parent = gameObject;

        GetComponent<Motion>().endPos = new Vector3(transform.position.x, Camera.main.transform.position.y, 0);
        GetComponent<Motion>().begin();
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket")) {
            Util.gm.die();
            GetComponent<Motion>().end();
        }
    }
}
