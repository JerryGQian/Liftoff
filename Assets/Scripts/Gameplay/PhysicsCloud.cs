using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PhysicsCloud : MonoBehaviour {
    Rigidbody2D rb;
    SpriteRenderer img;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        img = GetComponent<SpriteRenderer>();
        //img.color = new Color(1f, 1f, 1f, 0);
        img.enabled = false;
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name.Equals("Rocket")) {
            img.enabled = true;
            //img.color = new Color(1f, 1f, 1f);
            rb.velocity = new Vector3((transform.position.x - coll.gameObject.transform.position.x) * 9f, GameManager.rocketSpeed, 0);
            rb.angularVelocity = (transform.position.x - coll.gameObject.transform.position.x) * -130f;
        }
    }
}
