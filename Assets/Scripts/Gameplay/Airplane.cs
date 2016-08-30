using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour {
    float duration = 6.5f;
    float direction = -1f;

    float scale = 0.75f;

    public Sprite p1;
    public Sprite p2;
    public Sprite p3;

    public Sprite ufo1;
    public Sprite ufo2;
    public Sprite ufo3;
    public Sprite ufo4;
    public Sprite ufo5;
    public Sprite ufo6;

    Motion motion;

    public AudioSource planeSource;

    public AudioClip planeSound;
    public AudioClip ufoSound;
	// Use this for initialization
	void Start () {
        if (Util.gm.zoneID > 3) {
            switch ((int)Random.Range(0, 5.99f)) {
                case 0: GetComponent<SpriteRenderer>().sprite = ufo1; break;
                case 1: GetComponent<SpriteRenderer>().sprite = ufo2; break;
                case 2: GetComponent<SpriteRenderer>().sprite = ufo3; break;
                case 3: GetComponent<SpriteRenderer>().sprite = ufo4; break;
                case 4: GetComponent<SpriteRenderer>().sprite = ufo5; break;
                case 5: GetComponent<SpriteRenderer>().sprite = ufo6; break;
            }
            planeSource.clip = ufoSound;
        }
        else {
            switch ((int)Random.Range(0, 2.99f)) {
                case 0: GetComponent<SpriteRenderer>().sprite = p1; break;
                case 1: GetComponent<SpriteRenderer>().sprite = p2; break;
                case 2: GetComponent<SpriteRenderer>().sprite = p3; break;
            }
            planeSource.clip = planeSound;
        }


        transform.localScale = new Vector3(scale, scale, scale);
        if (Random.Range(-1f, 1f) < 0) {
            direction = 1f;
            transform.localScale = new Vector3(-scale, scale, scale);
        }
        motion = GetComponent<Motion>();
        motion.duration = duration;
        float impactPoint;
        do {
            impactPoint = Random.Range(0.7f, 0.3f);
        }
        while (Mathf.Abs(impactPoint - 0.5f) < 0.12f) ;
        transform.position = new Vector3(-15f * direction, Util.wm.rocket.transform.position.y + (duration * impactPoint * GameManager.rocketSpeed), 0);
        motion.endPos = new Vector3(transform.position.x * -1f, transform.position.y, 0);
        motion.startPos = transform.position;
        motion.begin();

        if (direction < 0) {
            Invoke("playSound", impactPoint);
        }
        else {
            Invoke("playSound", 1f - impactPoint);
        }
        
	}

    void OnTriggerEnter2D(Collider2D coll) {
        if (Util.wm.gameActive && coll.gameObject.name.Equals("Rocket") && !Util.wm.godmode && !Util.gm.invincible) {
            Util.gm.die();
            //motion.end();
        }
    }

    void playSound() {
        if (!Util.wm.soundMuted) planeSource.Play();
    }
}
