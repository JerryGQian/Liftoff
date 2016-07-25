using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {
    float speed = -15f;
    float life = 1f;
    float growth = 8f;
	// Use this for initialization
	void Start () {
        Invoke("suicide", life);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + new Vector3(0, speed * Time.deltaTime);
        transform.localScale = transform.localScale + new Vector3(growth * Time.deltaTime, growth * Time.deltaTime, growth * Time.deltaTime);
    }

    void suicide() {
        Destroy(gameObject);
    }
}
