using UnityEngine;
using System.Collections;
public enum ObstacleType { meteor, asteroid, satellite, other }

public class Obstacle : MonoBehaviour {
    public ObstacleType type;
    public AudioSource swooshSource;

    public GameObject alertPrefab;
    GameObject alert;
	// Use this for initialization
	void Start () {
        if (Random.Range(-1f, 1f) < 0) type = ObstacleType.meteor;
        switch (Util.gm.zoneID) {
            case 2: type = ObstacleType.meteor; break;
            case 3: type = ObstacleType.meteor; break;
            case 7: type = ObstacleType.meteor; break;
            case 12: type = ObstacleType.meteor; break;
        }
        switch (type) {
            case ObstacleType.meteor: GetComponent<SpriteRenderer>().sprite = Util.obstacleHolder.getMeteor(0); break;
            case ObstacleType.satellite: GetComponent<SpriteRenderer>().sprite = Util.obstacleHolder.getSatellite(0); break;
        }
        float size = Random.Range(0.7f * 0.8f, 0.7f * 1.2f);
        transform.position = new Vector3(Random.Range(-Util.width, Util.width), Camera.main.transform.position.y + 30f, 0);
        transform.localScale = new Vector3(size, size, size);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 360f));

        alert = Instantiate(alertPrefab);
        alert.GetComponent<Alert>().parent = gameObject;

        GetComponent<Motion>().endPos = new Vector3(transform.position.x, Camera.main.transform.position.y, 0);
        GetComponent<Motion>().begin();

        Invoke("playSwoosh", 1.5f);

        swooshSource.pitch = Random.Range(0.9f, 1.25f);
	}
	
	// Update is called once per frame
	void Update () {
        if (swooshSource.isPlaying) swooshSource.volume = 1f - (Mathf.Abs(transform.position.x - Util.wm.rocket.transform.position.x) - 1f) / 4f;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket") && !Util.wm.godmode && !Util.gm.invincible) {
            Util.gm.die();
            GetComponent<Motion>().end();
        }
    }

    void playSwoosh() {
        if (!Util.wm.soundMuted) swooshSource.Play();
    }
}
