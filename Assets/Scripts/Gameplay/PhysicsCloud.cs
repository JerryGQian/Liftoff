using UnityEngine;
using System.Collections;

public class PhysicsCloud : MonoBehaviour {
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.name.Equals("Rocket")) {
            rb.velocity = new Vector3((transform.position.x - coll.gameObject.transform.position.x) * 3f, GameManager.rocketSpeed, 0);
            rb.angularVelocity = (transform.position.x - coll.gameObject.transform.position.x) * -50f;
        }
    }
}
