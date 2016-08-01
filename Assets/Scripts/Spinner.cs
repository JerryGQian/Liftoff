using UnityEngine;
using System.Collections;

public class Spinner : MonoBehaviour {
    public float spinSpeed;
    public bool randomize;
	// Use this for initialization
	void Start () {
        if (randomize) {
            spinSpeed = Random.Range(-spinSpeed, spinSpeed);
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, spinSpeed * Time.deltaTime);
	}
}
