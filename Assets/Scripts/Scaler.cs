using UnityEngine;
using System.Collections;

public class Scaler : MonoBehaviour {
    public float scaleSpeed;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.localScale = transform.localScale + new Vector3(1f, 1f, 1f) * scaleSpeed * Time.deltaTime;
	}
}
