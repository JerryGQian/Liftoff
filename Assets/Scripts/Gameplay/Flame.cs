using UnityEngine;
using System.Collections;

public class Flame : MonoBehaviour {
    

    float speed = -15f;
    float life = 0.6f;
    float growth = 8f;
	// Use this for initialization
	void Start () {
        Invoke("suicide", life);
        switch ((int)Random.Range(0, 1.99f)) {
            case 0: GetComponent<SpriteRenderer>().sprite = Util.nozzle.f1; break;
            case 1: GetComponent<SpriteRenderer>().sprite = Util.nozzle.f2; break;
            //case 2: GetComponent<SpriteRenderer>().sprite = Util.nozzle.f3; break;
        }
        //transform.localScale = new Vector3(1f, 1f, 1f) * Random.Range(0.8f, 1.2f);
        //transform.eulerAngles = new Vector3(0, 1f, 0) * Random.Range(-20f, 20f);
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
